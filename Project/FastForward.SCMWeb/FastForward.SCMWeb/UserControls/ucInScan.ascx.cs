using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.UserControls
{
    public partial class ucInScan : System.Web.UI.UserControl
    {
        Base _base = new Base();
        int cRow;
        private string _chargeType = string.Empty;
        private MasterItem _itemdetail = null;
        bool _isDecimalAllow = false;
        private List<InventoryRequestItem> ItemList = new List<InventoryRequestItem>();
        private List<ReptPickSerials> reptPickSerial = new List<ReptPickSerials>();
        List<ReptPickSerials> _serList = new List<ReptPickSerials>();
        public delegate void OnButtonClick(string strValue);
        public event OnButtonClick btnHandler;
        protected List<ReptPickSerialsSub> _reptPickSerialsSub { get { return (List<ReptPickSerialsSub>)Session["_reptPickSerialsSub"]; } set { Session["_reptPickSerialsSub"] = value; } }

        int _itemSerializedStatus = 0; //0 -> Non-Serialized, -1 -> Non-SerializedDecimalAllow, 1 -> Serialized, 2 -> Chassis Available

        #region public properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string _mainModuleName
        {
            get { if (Session["_mainModuleName"] != null) { return (string)Session["_mainModuleName"]; } else { return ""; } }
            set { Session["_mainModuleName"] = value; }
        }
        public string adjustmentTypeValue
        {
            get { return adjustmentTypeValue; }
            set { ViewState["adjustmentTypeValue"] = value; }
        }
        public string adjustmentType
        {
            get { return adjustmentType; }
            set { ViewState["adjustmentType"] = value; }
        }
        public string doc_tp
        {
            get { return doc_tp; }
            set { ViewState["doc_tp"] = value; }
        }
     //   public int baseitemline { get { return (int)Session["baseitemline"]; } set { Session["baseitemline"] = value; } }

        public int baseitemline
        {
            get { if (Session["baseitemline"] != null) { return (int)Session["baseitemline"]; } else { return 0; } }
            set { Session["baseitemline"] = value; }
        }
     
        public string userSeqNo
        {
            get { if (ViewState["userSeqNo"] == null) return null; return ViewState["userSeqNo"].ToString(); }
            set { ViewState["userSeqNo"] = value; }
        }
        public List<InventoryRequestItem> ScanItemList
        {
            get { if (ViewState["ItemList"] != null) ItemList = (List<InventoryRequestItem>)ViewState["ItemList"]; return ItemList; }
            set { ItemList = value; ViewState["ItemList"] = value; }
        }
        public List<ReptPickSerials> PickSerial
        {
            get { if (ViewState["reptPickSerial"] != null) reptPickSerial = (List<ReptPickSerials>)ViewState["reptPickSerial"]; return reptPickSerial; }
            set { reptPickSerial = value; ViewState["reptPickSerial"] = value; }
        }

        //public Panel PNLTobechange
        //{
        //    get { return pnlTobechange; }
        //    set { pnlTobechange = value; }
        //}
        public TextBox TXTItemCode
        {
            get { return txtItemCode; }
            set { txtItemCode = value; }
        }
        public TextBox TXTSuppliercode
        {
            get { return txtSupplier; }
            set { txtSupplier = value; }
        }
        public TextBox TXTUnitcost
        {
            get { return txtCost; }
            set { txtCost = value; }
        }
        public LinkButton LBtnItemCode
        {
            get { return lbtnItemCode; }
            set { lbtnItemCode = value; }
        }
        
        public DropDownList DDL_STATUS
        {
            get { return ddlStatus; }
            set { ddlStatus = value; }
        }

        public LinkButton LBTNAdd
        {
            get { return lbtnAdd; }
            set { lbtnAdd = value; }
        }
        //Lakshan 2016 Apr 06
        private bool grnNoRequired;
        public bool GrnNoRequired
        {
            get { return grnNoRequired; }
            set { grnNoRequired = value; }
        }
        //Added by Dulaj 2018/Jun/05 for stock adjusment asset to change lable names
        public string lockItemname { get; set;}
      
        #endregion public properties



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["SENT_TO_CRN"] = "1";
                if (!IsPostBack)
                {
                    PageClear();
                    ListClear();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnBinCode_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                DataTable result = _base.CHNLSVC.General.GetWarehouseBinByLoc(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Bin";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                DateRow.Visible = false;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
            }
        }
        protected void txtBinCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                DataTable result = _base.CHNLSVC.General.GetWarehouseBinByLoc(SearchParams, "CODE", txtBinCode.Text);

                if (result.Rows.Count <= 0)
                {
                    txtBinCode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please Enter a valid bin code');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
            }
        }
        protected void lbtnSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = _base.CHNLSVC.CommonSearch.GetSupplierData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                DateRow.Visible = false;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
            }
        }
        protected void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();
                if (!string.IsNullOrEmpty(txtSupplier.Text))
                {
                    _custList = _base.CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), txtSupplier.Text, string.Empty, string.Empty, "S");
                    if (_custList == null || _custList.Count == 0)
                    {
                        txtSupplier.Text = "";
                        txtSupplier.ToolTip = "";
                        txtSupplier.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid supplier.');", true);
                        return;
                    }
                    else
                    {
                        txtSupplier.ToolTip = _custList[0].Mbe_name.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
            }
        }
        protected void lbtntxtGRNNo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNNo);
                DataTable result = _base.CHNLSVC.CommonSearch.searchGRNData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "342";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                DateRow.Visible = false;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
            }
        }
        protected void txtGRNNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                InventoryHeader _invH = new InventoryHeader();
                _invH = _base.CHNLSVC.Inventory.Get_Int_Hdr(txtGRNNo.Text);
                if (_invH != null)
                {
                    txtGRNDate.Text = Convert.ToDateTime(_invH.Ith_doc_date).ToString("dd/MMM/yyyy");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
            }
        }
        protected void chkLockItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (chkLockItem.Checked == true)
                //{
                //    if (string.IsNullOrEmpty(txtItemCode.Text))
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter the item code.');", true);
                //        return;
                //    }
                //    txtItemCode.ReadOnly = true;
                //}
                //else
                //{
                //    txtItemCode.ReadOnly = false;
                //}
                if (chkLockItem.Checked == true)
                {
                    txtItemCode.Focus();
                }
                else
                {
                    PageDataClear();
                    txtItemCode.Focus();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
            }
        }
        protected void lbtnItemCode_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "3";
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
        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string glbStatus = ddlStatus.SelectedValue;
                if (string.IsNullOrEmpty(txtItemCode.Text))
                {
                    PageClear();
                    txtItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter ItemCode.');", true);
                    return;
                }

                if (ddlItemType.SelectedItem.Value == "M")
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, "Model", txtItemCode.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "3";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    DateRow.Visible = false;

                    ddlSearchbykey.SelectedItem.Text = "Model";
                    ddlSearchbykey.DataBind();
                    txtSearchbyword.Text = txtItemCode.Text;
                    txtItemCode.Text = string.Empty;

                    UserPopup.Show();

                }
                if (ddlItemType.SelectedItem.Value == "P")
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, "Part No", txtItemCode.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "3";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    DateRow.Visible = false;

                    ddlSearchbykey.SelectedItem.Text = "Part";
                    ddlSearchbykey.DataBind();
                    txtSearchbyword.Text = txtItemCode.Text;
                    txtItemCode.Text = string.Empty;

                    UserPopup.Show();
                }

                if (LoadItemDetail(txtItemCode.Text.Trim()) == false)
                {
                    txtItemCode.Focus();
                    return;
                }
                txtItemCode.Text = txtItemCode.Text.Trim();

                if (ViewState["adjustmentTypeValue"] == null)
                {
                    txtItemCode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please set the adjustment type!.');", true);
                    return;
                }
                if (ViewState["adjustmentTypeValue"].ToString() == "0")
                {
                    txtItemCode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the adjustment type!.');", true);
                    return;
                }

                DataTable _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.Trim(), string.Empty);
                //DataTable bins = _base.CHNLSVC.Inventory.LoadDistinctBins(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.Trim());
                //if (bins == null || bins.Rows.Count <= 0)
                //{
                //    PageClear();
                //    txtItemCode.Focus();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('No stock bins.');", true);
                //    return;
                //}
                //else
                //{
                //    ddlBinCode.DataSource = null;
                //    ddlBinCode.DataSource = bins;
                //    ddlBinCode.DataTextField = "INB_BIN";
                //    ddlBinCode.DataValueField = "INB_BIN";
                //    ddlBinCode.DataBind();
                //}

                //if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                //{
                //    PageClear();
                //    txtItemCode.Focus();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('No stock balance available!.');", true);
                //    return;
                //}
                //else
                //{
                //    ddlStatus.DataSource = null;
                //    ddlStatus.DataSource = _inventoryLocation;
                //    ddlStatus.DataTextField = "mis_desc";
                //    ddlStatus.DataValueField = "inl_itm_stus";
                //    ddlStatus.DataBind();

                if (chkLockItem.Checked == true)
                {
                    txtItemCode.ReadOnly = true;

                }
                else
                {
                    txtItemCode.ReadOnly = false;
                }
                if (pnlSerialized.Visible == true)
                {
                    txtQty.Text = "1";
                    txtQty.ReadOnly = true;
                    //txtSerialI.Focus();
                    txtCost.Focus();
                }
                else
                {
                    txtQty.ReadOnly = false;
                    txtQty.Enabled = true;
                    txtQty.Focus();
                    //if (bins.Rows.Count == 1)
                    //{
                    //    ddlStatus.Focus();
                    //    if (_inventoryLocation.Rows.Count == 1)
                    //        txtQty.Focus();
                    //    else
                    //        ddlStatus.Focus();
                    //}
                    //else
                    //    ddlBinCode.Focus();
                }

                //} 
                grdInventoryBalance.DataSource = null;
                grdInventoryBalance.AutoGenerateColumns = false;
                grdInventoryBalance.DataSource = _inventoryLocation;
                grdInventoryBalance.DataBind();
                BindUserCompanyItemStatusDDLData(ddlStatus);
                ddlStatus.SelectedValue =glbStatus;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        public void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnHandler != null)
                    btnHandler(string.Empty);

                if (string.IsNullOrEmpty(txtItemCode.Text.Trim()))
                {
                    txtItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the item.');", true);                    
                    return;
                }
                if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                {
                    ddlStatus.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the status.');", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    txtQty.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the qty.');", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtCost.Text))
                {
                    txtCost.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the Unit Cost.');", true);
                    return;
                }
                #region validate qty regarding to the item type 04 Jan 2017
                if (ViewState["adjustmentTypeValue"].ToString() == "+")
                {
                    decimal _qtyVal = 0, _tmpQty = 0;
                    _qtyVal = decimal.TryParse(txtQty.Text, out _tmpQty) ? Convert.ToDecimal(txtQty.Text) : 0;
                    if (_qtyVal <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the valid qty !');", true);
                        return;
                    }
                    MasterItem _mstItm = _base.CHNLSVC.General.GetItemMaster(txtItemCode.Text.Trim());
                    if (_mstItm != null)
                    {
                        if (_mstItm.Mi_is_ser1 != -1)
                        {
                            if (!((_qtyVal % 1) == 0))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the valid qty !');", true);
                                txtQty.Focus();
                                return;
                            }
                        }
                    }
                }
                
                #endregion
                if (pnlSerialized.Visible == true)
                {

                    if ((txtSerialI.ReadOnly == false) && string.IsNullOrEmpty(txtSerialI.Text))
                    {
                        txtSerialI.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter the SerialI.');", true);
                        return;
                    }
                    if ((txtSerialII.ReadOnly == false) && string.IsNullOrEmpty(txtSerialII.Text))
                    {
                        txtSerialII.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter the SerialII.');", true);
                        return;
                    }
                    if ((txtSerialIII.ReadOnly == false) && string.IsNullOrEmpty(txtSerialIII.Text))
                    {
                        txtSerialIII.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter the SerialIII.');", true);
                        return;
                    }
                    if ((txtSerialI.ReadOnly == false) )
                    {
                        DataTable _dtser1 = _base.CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", txtItemCode.Text, txtSerialI.Text);
                        if (_dtser1 != null)
                        {
                            if (
                               _dtser1.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Serial no 1 already exist.');", true);
                                return;
                            }
                        }
                    }
                    if ((txtSerialII.ReadOnly == false) )
                    {
                        DataTable _dtser2 = _base.CHNLSVC.Inventory.CheckSerialAvailability("SERIAL2", txtItemCode.Text, txtSerialII.Text);
                        if (_dtser2 != null)
                        {
                            if (_dtser2.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Serial no 2  already exist.');", true);
                                return;
                            }
                        }
                    }
                   
                }
                if (pnlExpiryDate.Visible == true)
                {
                    if (string.IsNullOrEmpty(txtManufactureDate.Text))
                    {
                        txtManufactureDate.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter the Manufacture Date.');", true);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtExpiryDate.Text))
                    {
                        txtExpiryDate.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter the Expiry Date.');", true);
                        return;
                    }

                    if (Convert.ToDateTime(txtExpiryDate.Text)  < DateTime.Now.Date)
                        {
                            txtExpiryDate.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter valid Expiry Date.');", true);
                            return;
                        }
                }
                if (!_base.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11067))
                {
                    if (string.IsNullOrEmpty(txtSupplier.Text))
                    {
                        txtSupplier.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select the Supplier Code.');", true);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtGRNNo.Text) && !GrnNoRequired)
                    {
                        txtGRNNo.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select the GRN #.');", true);
                        return;
                    }
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You do not have permission for this function.Permission Code :- 16033 !!!')", true);
                   
                }
                
            

               

                List<InventoryLocation> _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.Trim(), ddlStatus.SelectedValue.ToString());

                if (_inventoryLocation != null)
                {
                    if (_inventoryLocation.Count == 1)
                    {
                        foreach (InventoryLocation _loc in _inventoryLocation)
                        {
                            if (ViewState["adjustmentTypeValue"].ToString() != "+")
                            {
                                decimal _formQty = Convert.ToDecimal(txtQty.Text);
                                if (_formQty > _loc.Inl_free_qty)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed the required qty !!!')", true);
                                    txtQty.Text = string.Empty;
                                    txtQty.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }

                //if (pnlSup.Visible == true)
                //    if (dtGRN.Value.Date > DateTime.Now.Date)
                //    {
                //        MessageBox.Show("Invalid GRN Date", "GRN Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }

                //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                //{
                //    MasterItem _mstItm = new MasterItem();
                //    _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
                //    if (_mstItm.Mi_is_ser1 == 1)    //kapila 26/8/2015
                //    {
                //        bool _permission = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11065);
                //        if (!_permission)
                //        {
                //            if (string.IsNullOrEmpty(txtGRNno.Text) || string.IsNullOrEmpty(txtSup.Text) || string.IsNullOrEmpty(txtBatch.Text))
                //            {
                //                MessageBox.Show("Please enter supplier/GRN No/Batch No (Permission code-11067)", "Unit Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            if (MessageBox.Show("GRN details not entered. Are you sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No) return;
                //        }
                //    }
                //}


                if (ViewState["adjustmentTypeValue"].ToString() == "+" && ViewState["adjustmentType"].ToString() != "AFSL")
                {
                    if (string.IsNullOrEmpty(txtCost.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter unit cost amount.');", true);
                        txtCost.Focus();
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtCost.Text.ToString()) <= 0 && ddlStatus.Text.ToString().ToUpper() != "CONS")
                        {
                            if (_chargeType != "FOC")
                            {
                                txtCost.Text = string.Empty;
                                txtCost.Focus();
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Unit cost can not be less than or equal to zero amount.');", true);
                                return;
                            }
                        }
                        else
                        {
                            if (ddlStatus.Text.ToString().ToUpper() == "CONSIGNMENT")
                            {
                                if (Convert.ToDecimal(txtCost.Text.ToString()) != 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Unit cost should be zero(0.00) for CONSIGNMENT status.');", true);
                                    txtCost.Text = string.Empty;
                                    txtCost.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    txtCost.Text = "0";
                }

                //this.Cursor = Cursors.WaitCursor;
                MasterItem _itms = new MasterItem();
                _itms = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.Trim());
                InventoryRequestItem _itm = new InventoryRequestItem();

                //Added by Prabhath on 17/12/2013 ************* start **************
                Decimal _ucost = Convert.ToDecimal(txtCost.Text.Trim());
                if (ViewState["adjustmentType"].ToString() == "AFSL")
                {
                    _ucost = _base.CHNLSVC.Inventory.GetLatestCost(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.Trim(), "GOD");
                    //Common function written by Sachith, Where used it in Revert Module Line 816 - 836, pick it as same
                    DateTime txtDate = Convert.ToDateTime(((TextBox)this.Parent.FindControl("txtDate")).Text);
                    List<InventoryCostRate> _costList = _base.CHNLSVC.Inventory.GetInventoryCostRate(Session["UserCompanyCode"].ToString(), "AFSLRV", Convert.ToString(ddlStatus.SelectedValue), (((txtDate.Year - txtDate.Year) * 12) + txtDate.Month - txtDate.Month), "GOD");
                    if (_costList != null && _costList.Count > 0)
                        if (_costList[0].Rcr_rt == 0) _ucost = Math.Round(_ucost - _costList[0].Rcr_val, 2);
                        else _ucost = Math.Round(_ucost - ((_ucost * _costList[0].Rcr_rt) / 100), 2);
                }
                //Added by Prabhath on 17/12/2013 ************* end **************

                int itemline = 0;

                if (ScanItemList != null)
                    if (ScanItemList.Count > 0)
                    {
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == txtItemCode.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString()
                                         select _ls;

                        if (_duplicate != null)
                            if (_duplicate.Count() <= 0)
                            {
                                //ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Selected item already available.');", true);
                                //return;

                                var _maxline = (from _ls in ScanItemList
                                                select _ls.Itri_line_no).Max();
                                _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                                _itm.Itri_itm_cd = txtItemCode.Text.Trim();
                                _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                                _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                                _itm.Itri_qty = Convert.ToDecimal(txtQty.Text);
                                _itm.Mi_longdesc = _itms.Mi_longdesc;
                                _itm.Mi_model = _itms.Mi_model;
                                _itm.Mi_brand = _itms.Mi_brand;
                                //Added by Prabhath on 17/12/2013 ************* start **************
                                _itm.Itri_unit_price = _ucost;
                                //Added by Prabhath on 17/12/2013 ************* end **************
                                //kapila 3/7/2015
                                _itm.Itri_batchno = txtBatch.Text;

                                if (pnlExpiryDate.Visible == true)
                                    _itm.Itri_expdate = Convert.ToDateTime(txtExpiryDate.Text);

                                if (!string.IsNullOrEmpty(txtGRNDate.Text.Trim()))
                                {
                                    _itm.Itri_grndate = Convert.ToDateTime(txtGRNDate.Text); 
                                }
                                
                                _itm.Itri_grnno = txtGRNNo.Text;
                                _itm.Itri_supplier = txtSupplier.Text; 

                                itemline = Convert.ToInt32(_maxline) + 1;

                                ScanItemList.Add(_itm);
                                ScanItemList = ScanItemList;
                            }
                            else
                            {
                                var line = from d in _duplicate
                                           select d.Itri_line_no;
                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline //&& res.Sad_itm_stus == _itemstatus
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_qty = x.Itri_qty + Convert.ToDecimal(txtQty.Text.ToString()));

                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline //&& res.Sad_itm_stus == _itemstatus
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_app_qty = x.Itri_app_qty + Convert.ToDecimal(txtQty.Text.ToString()));



                                ScanItemList = ScanItemList;
                            }

                        //var _maxline = (from _ls in ScanItemList
                        //                select _ls.Itri_line_no).Max();
                        //_itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                        //_itm.Itri_itm_cd = txtItemCode.Text.Trim();
                        //_itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                        //_itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                        //_itm.Itri_qty = 0;
                        //_itm.Mi_longdesc = _itms.Mi_longdesc;
                        //_itm.Mi_model = _itms.Mi_model;
                        //_itm.Mi_brand = _itms.Mi_brand;
                        ////Added by Prabhath on 17/12/2013 ************* start **************
                        //_itm.Itri_unit_price = _ucost;
                        ////Added by Prabhath on 17/12/2013 ************* end **************
                        ////kapila 3/7/2015
                        //_itm.Itri_batchno = txtBatch.Text;

                        //if (pnlExpiryDate.Visible == true)
                        //    _itm.Itri_expdate = Convert.ToDateTime(txtExpiryDate.Text);

                        ////_itm.Itri_grndate = dtGRN.Value.Date;
                        ////_itm.Itri_grnno = txtGRNno.Text;
                        ////_itm.Itri_supplier = txtSup.Text;                             
                    }
                    else
                    {
                        _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                        _itm.Itri_itm_cd = txtItemCode.Text.Trim();
                        _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                        _itm.Itri_line_no = 1;
                        _itm.Itri_qty = 0;
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        //Added by Prabhath on 17/12/2013 ************* start **************
                        _itm.Itri_unit_price = _ucost;
                        //Added by Prabhath on 17/12/2013 ************* end **************
                        //kapila 3/7/2015
                        _itm.Itri_batchno = txtBatch.Text;
                        if ((pnlExpiryDate.Visible == true) && (!string.IsNullOrEmpty(txtExpiryDate.Text)))
                        {
                            _itm.Itri_expdate = Convert.ToDateTime(txtExpiryDate.Text);
                        }

                        if (!string.IsNullOrEmpty(txtGRNDate.Text))
                        {
                            _itm.Itri_grndate = Convert.ToDateTime(txtGRNDate.Text); 
                        }
                        
                        _itm.Itri_grnno = txtGRNNo.Text;
                        _itm.Itri_supplier = txtSupplier.Text;  

                        itemline = 1;

                        ScanItemList.Add(_itm);
                        ScanItemList = ScanItemList;
                    }                

                if ((ViewState["userSeqNo"] == null))
                {
                    GenerateNewUserSeqNo();
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(ViewState["userSeqNo"].ToString());
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    //_reptitm.Tui_pic_itm_ = 
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    if (ViewState["adjustmentTypeValue"].ToString() == "+")
                    {
                       // _reptitm.Tui_pic_itm_qty = _addedItem.Itri_app_qty;
                    }
                    _reptitm.Tui_sup = txtSupplier.Text;
                    _reptitm.Tui_grn = txtGRNNo.Text;
                    _reptitm.Tui_batch = txtBatch.Text;
                    if (ViewState["adjustmentTypeValue"].ToString() != "+" && _mainModuleName != "StockADJ")
                    {
                        _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    }
                    if (!string.IsNullOrEmpty(txtGRNDate.Text.Trim()))
                    {
                        _reptitm.Tui_grn_dt = Convert.ToDateTime(txtGRNDate.Text);
                    }
                    
                    if (pnlExpiryDate.Visible == true)
                        _reptitm.Tui_exp_dt = Convert.ToDateTime(txtExpiryDate.Text);                                       

                    _saveonly.Add(_reptitm);
                }

                //DELETE all items and again add all
                if (ViewState["adjustmentTypeValue"].ToString() == "+" && _mainModuleName == "StockADJ")
                {
                    _base.CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32((ViewState["userSeqNo"].ToString())), string.Empty, string.Empty, 0, 3);
                }
                _base.CHNLSVC.Inventory.SavePickedItems(_saveonly);

                //gvItems.DataSource = null;
                //gvItems.DataSource = ScanItemList;

                //grdItem.DataSource = ScanItemList;
                //grdItem.DataBind();

                string popup = (string)Session["POPUP"];
                string basedoc = string.Empty;

                if (string.IsNullOrEmpty(popup))
                {
                    if (((GridView)this.Parent.FindControl("grdItems")) == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('grdItems not found.');", true);
                        return;
                    }
                    ((GridView)this.Parent.FindControl("grdItems")).DataSource = ScanItemList;
                    ((GridView)this.Parent.FindControl("grdItems")).DataBind();
                }
                else
                {
                    basedoc = (string)Session["PO_NO"];
                }

                int _serID = _base.CHNLSVC.Inventory.IsExistInSerialMaster("", txtItemCode.Text, txtSerialI.Text);

                //decimal _appQty = Convert.ToDecimal(lblPopupQty.Text.ToString());
                //if (_appQty < num_of_checked_itms)
                //{
                //    MessageBox.Show("Can't exceed Approved Qty. You can add only " + Convert.ToString(_appQty - _appQty) + " item more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return;
                //}

                //#region Adj
                //Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", Session["UserCompanyCode"].ToString(), ScanDocument, 0);
                //if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                //{
                //    generated_seq = user_seq_num;
                //}
                //else
                //{
                //    generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                //    //assign user_seqno
                //    ReptPickHeader RPH = new ReptPickHeader();
                //    RPH.Tuh_doc_tp = "ADJ";
                //    RPH.Tuh_cre_dt = DateTime.Today;
                //    RPH.Tuh_ischek_itmstus = true;
                //    RPH.Tuh_ischek_reqqty = true;
                //    RPH.Tuh_ischek_simitm = true;
                //    RPH.Tuh_session_id = Session["SessionID"].ToString();
                //    RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                //    RPH.Tuh_usr_id = Session["UserID"].ToString();
                //    RPH.Tuh_usrseq_no = generated_seq;

                //    RPH.Tuh_direct = false;
                //    RPH.Tuh_doc_no = ScanDocument;
                //    //write entry to TEMP_PICK_HDR
                //    int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

                //}

                MasterItem msitem = new MasterItem();
                msitem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text);
                if (msitem.Mi_is_ser1 == 1)
                {
                    //MasterItem msitem = new MasterItem();
                    //msitem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text);            
                    //if (msitem.Mi_is_ser1 != -1)
                    ////change msitem.Mi_is_ser1 == true
                    //{
                    //    int rowCount = 0;

                    //    foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                    //    {
                    //        Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);

                    //        DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                    //        if (Convert.ToBoolean(chkSelect.Value) == true)
                    //        {
                    //            //-------------
                    //            string binCode = gvr.Cells["ser_Bin"].Value.ToString();
                    //            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, itemCode, serID);
                    //            //Update_inrser_INS_AVAILABLE
                    //            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), itemCode, serID, -1);

                    //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    //            _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    //            _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                    //            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                    //            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    //            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    //            _reptPickSerial_.Tus_job_no = JobNo;
                    //            _reptPickSerial_.Tus_job_line = JobLineNo;
                    //            //enter row into TEMP_PICK_SER
                    //            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                    //            rowCount++;
                    //            //isManualscan = true;

                    //        }

                    //    }
                    //}
                    //else
                    //{
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = Convert.ToInt32(ViewState["userSeqNo"].ToString());//generated_seq;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_base_doc_no = basedoc;// ScanDocument;
                    _reptPickSerial_.Tus_doc_no = basedoc;
                    _reptPickSerial_.Tus_job_line = itemline;
                    _reptPickSerial_.Tus_base_itm_line = baseitemline;//Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                    _reptPickSerial_.Tus_itm_desc = lblDescription.Text;//msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = lblModel.Text;//msitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_bin = txtBinCode.Text;//Session["GlbDefaultBin"].ToString();
                    _reptPickSerial_.Tus_itm_cd = txtItemCode.Text;//itemCode;
                    _reptPickSerial_.Tus_itm_stus = ddlStatus.SelectedItem.Value;//ItemStatus;
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtQty.Text);//Convert.ToDecimal(txtPopupQty.Text.ToString());
                    _reptPickSerial_.Tus_ser_1 = txtSerialI.Text;
                    _reptPickSerial_.Tus_ser_2 = txtSerialII.Text;
                    _reptPickSerial_.Tus_ser_3 = txtSerialIII.Text;
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    if (_serID > 0)
                    { _reptPickSerial_.Tus_ser_id = _serID; }
                    else
                    { _reptPickSerial_.Tus_ser_id = _base.CHNLSVC.Inventory.GetSerialID(); }
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_unit_cost = Convert.ToDecimal(txtCost.Text);
                    _reptPickSerial_.Tus_unit_price = Convert.ToDecimal(txtCost.Text);
                    //_reptPickSerial_.Tus_unit_price = 0;
                    //_reptPickSerial_.Tus_job_no = JobNo;
                    //-reptPickSerial_.Tus_job_line = JobLineNo;
                    //enter row into TEMP_PICK_SER

                    _reptPickSerial_.Tus_exist_supp = txtSupplier.Text;
                    _reptPickSerial_.Tus_exist_grnno = txtGRNNo.Text;
                    if (!string.IsNullOrEmpty(txtGRNDate.Text.Trim()))
                    {
                        _reptPickSerial_.Tus_exist_grndt = Convert.ToDateTime(txtGRNDate.Text);
                    }
                    
                    _reptPickSerial_.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_exist_grncom = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Mis_desc = ddlStatus.SelectedItem.Text;
                    
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(ViewState["userSeqNo"].ToString()), ViewState["doc_tp"].ToString());
                    
                    
                    if (_serList != null)
                        if (_serList.Count > 0)
                        {
                            var _duplicateSerial = from _ls in _serList
                                                   where _ls.Tus_itm_cd == txtItemCode.Text.Trim() && _ls.Tus_ser_1 == txtSerialI.Text.Trim()
                                                   select _ls;

                            if (_duplicateSerial != null)
                            {
                                if (msitem.Mi_is_ser1 == 1)
                                {
                                    if (_duplicateSerial.Count() > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Selected serial already available.');", true);
                                        return;
                                    }
                                }
                                else 
                                {
                                    if (_duplicateSerial.Count() == 1)
                                    {
                                        //_base.CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus);
                                        //Delete_Serials(txtItemCode.Text, ddlStatus.SelectedItem.Value, _reptPickSerial_.Tus_ser_id, "");
                                    }                          
                                }
                            }
                        }

                    



                    //Write to the Picked Sub Serial .
                    if (msitem.Mi_is_scansub == true)
                    {
                        _reptPickSerialsSub = new List<ReptPickSerialsSub>();
                        int _rowno = 0;
                        DataTable _tblSub = _base.CHNLSVC.Inventory.getSubitemComponent(txtItemCode.Text);
                        foreach (DataRow row in _tblSub.Rows)
                        {
                            if (msitem.Mi_is_ser1 == 1)
                            {
                                ReptPickSerialsSub _ReptPickSerialsSub = new ReptPickSerialsSub();
                                #region Fill Pick Sub Serial Object
                                _ReptPickSerialsSub.Tpss_itm_brand = msitem.Mi_brand;
                                _ReptPickSerialsSub.Tpss_itm_cd = row[0].ToString();
                                _ReptPickSerialsSub.Tpss_itm_desc = lblDescription.Text;
                                _ReptPickSerialsSub.Tpss_itm_model =lblModel.Text;
                                _ReptPickSerialsSub.Tpss_itm_stus = ddlStatus.SelectedValue;
                                _ReptPickSerialsSub.Tpss_m_itm_cd = txtItemCode.Text;
                                _ReptPickSerialsSub.Tpss_m_ser = txtSerialI.Text;
                                _ReptPickSerialsSub.Tpss_mfc = "";
                                _ReptPickSerialsSub.Tpss_ser_id = _reptPickSerial_.Tus_ser_id;

                                if (_rowno == 0)
                                {
                                    _ReptPickSerialsSub.Tpss_sub_ser = txtSerialI.Text;
                                }
                                else { _ReptPickSerialsSub.Tpss_sub_ser = "N/A"; }


                                _ReptPickSerialsSub.Tpss_tp = row["micp_itm_tp"].ToString();
                                _ReptPickSerialsSub.Tpss_usrseq_no = Convert.ToInt32(ViewState["userSeqNo"].ToString());//generated_seq;
                                //_ReptPickSerialsSub.Tpss_warr_no = _warrantyno;
                                //_ReptPickSerialsSub.Tpss_warr_period = "";
                                // _ReptPickSerialsSub.Tpss_warr_rem = "";  
                                _reptPickSerialsSub.Add(_ReptPickSerialsSub);
                                _rowno++;
                                #endregion
                            }
                           
                        }


                       

                    }
                    Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, _reptPickSerialsSub);
                }
                else if (msitem.Mi_is_ser1 == 0 || msitem.Mi_is_ser1 ==-1) //change for mac item 3000 to 1 loop
                {
                    decimal _actualQty = Convert.ToDecimal(txtQty.Text.Trim());
                    string _warrantyno = string.Empty;

                   // for (int i = 0; i < _actualQty; i++)
                     for (int i = 0; i < 1; i++)
                    {
                        ReptPickSerials _newReptPickSerials = new ReptPickSerials();
                        #region Fill Pick Serial Object
                        _newReptPickSerials.Tus_usrseq_no = Convert.ToInt32(ViewState["userSeqNo"].ToString());
                        _newReptPickSerials.Tus_base_doc_no = basedoc;// ScanDocument;
                        _newReptPickSerials.Tus_base_itm_line = itemline;
                        _newReptPickSerials.Tus_doc_no = basedoc;
                        _newReptPickSerials.Tus_job_line = itemline;
                        _newReptPickSerials.Tus_itm_line = 0;
                        _newReptPickSerials.Tus_batch_line = 0;
                        _newReptPickSerials.Tus_ser_line = 0;
                        _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                        _newReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                        _newReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                        _newReptPickSerials.Tus_bin = txtBinCode.Text;
                        _newReptPickSerials.Tus_itm_cd = txtItemCode.Text.Trim();
                        _newReptPickSerials.Tus_itm_stus = ddlStatus.SelectedItem.Value;
                        _newReptPickSerials.Tus_unit_cost = Convert.ToDecimal(txtCost.Text);
                        _newReptPickSerials.Tus_unit_price = Convert.ToDecimal(txtCost.Text);
                        _newReptPickSerials.Tus_qty = _actualQty; //1;
                        if (ViewState["doc_tp"].ToString() == "ADJ" && ViewState["adjustmentTypeValue"].ToString() == "+")
                        {
                            _newReptPickSerials.Tus_ser_id = 0;
                        }
                        else
                        {
                            _newReptPickSerials.Tus_ser_id = _base.CHNLSVC.Inventory.GetSerialID();
                        }
                        _newReptPickSerials.Tus_ser_1 = "N/A";
                        _newReptPickSerials.Tus_ser_2 = "N/A";
                        _newReptPickSerials.Tus_ser_3 = "N/A";
                        _newReptPickSerials.Tus_warr_no = _warrantyno;
                        _newReptPickSerials.Tus_itm_desc = msitem.Mi_shortdesc;
                        _newReptPickSerials.Tus_itm_model = msitem.Mi_model;
                        _newReptPickSerials.Tus_itm_brand = msitem.Mi_brand;
                        _newReptPickSerials.Tus_itm_line = itemline;
                        _newReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                        _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                        // _newReptPickSerials.Tus_session_id = Session["SessionID"].ToString();

                        //kapila 3/7/2015
                        _newReptPickSerials.Tus_orig_supp = txtSupplier.Text.Trim();
                        _newReptPickSerials.Tus_exist_supp = txtSupplier.Text.Trim();
                        _newReptPickSerials.Tus_orig_grnno = txtGRNNo.Text.Trim();
                        _newReptPickSerials.Tus_exist_grnno = txtGRNNo.Text.Trim();

                        if (!string.IsNullOrEmpty(txtGRNDate.Text.Trim()))
                        {
                            _newReptPickSerials.Tus_orig_grndt = Convert.ToDateTime(txtGRNDate.Text);
                            _newReptPickSerials.Tus_exist_grndt = Convert.ToDateTime(txtGRNDate.Text); 
                        }

                        _newReptPickSerials.Tus_batch_no = txtBatch.Text;
                        _newReptPickSerials.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                        _newReptPickSerials.Tus_exist_grncom = Session["UserCompanyCode"].ToString();

                        MasterItem _selectedItem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.Trim());
                        if (_selectedItem.MI_IS_EXP_DT == 1)    //27/8/2015
                        {
                            _newReptPickSerials.Tus_exp_dt = Convert.ToDateTime(txtExpiryDate.Text);
                            _newReptPickSerials.Tus_manufac_dt = Convert.ToDateTime(txtManufactureDate.Text);

                        }
                        _newReptPickSerials.Mis_desc = ddlStatus.SelectedItem.Text;
                        //if (DocumentType == "ADJ")
                        if (ViewState["doc_tp"] == "ADJ")
                        {
                            MasterItemWarrantyPeriod _period = _base.CHNLSVC.Inventory.GetItemWarrantyDetail(txtItemCode.Text.Trim(), ddlStatus.SelectedItem.Value);
                            if (_period != null)
                                _newReptPickSerials.Tus_warr_period = _period.Mwp_val;
                        }

                        #endregion
                        //Save to the temp table.
                        //CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);//commented by Shani
                        //_newReptPickSerials = null; //commented by Shani

                        //-------added by Shani
                        // List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                        // var serCount = 0;
                        // if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                        // {
                        //     serCount = (from c in _resultItemsSerialList
                        //                 where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == ItemLineNo
                        //                 select c).Count();
                        // }
                        //for non serial, decimal allowed
                        //var serCount_2 = (from c in _resultItemsSerialList
                        //                  select c.Tus_qty).Sum();

                        //if (Convert.ToDecimal(serCount) < Convert.ToDecimal(lblDocQty.Text.Trim()))
                        // {
                        //   lblScanQty.Text = serCount.ToString();
                        //   CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);
                        //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
                        //   ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                        //   _newReptPickSerials = null;

                        //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                        //  List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                        //  GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();


                        //SubmitItemSerialData();

                        //}

                        Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);
                    }
                }
                //else if (msitem.Mi_is_ser1 == -1)
                //{
                //    int rowCount = 0;

                //    List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();
                //  //  foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                //  //  {
                //   //     DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                //     //   Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);
                //     //   if (Convert.ToBoolean(chkSelect.Value) == true)
                //     //   {
                //         //   string binCode = gvr.Cells[5].Value.ToString();
                //        //    ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, itemCode, serID);
                //    actual_non_ser_List.Tus_cre_by = BaseCls.GlbUserName;
                //        //    _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;
                //        //    Decimal pending_amt_ = Convert.ToDecimal(lblDocQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                //        //    if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) - pending_amt_ == 0)
                //       //     {
                //        //        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), itemCode, serID, -1);
                //       //     }
                //    actual_non_ser_List.Tus_cre_by = BaseCls.GlbUserName;
                //    actual_non_ser_List.Tus_base_doc_no = _scanDocument;

                //            MasterItem mi = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itemCode);
                //            actual_non_ser_List.Tus_itm_desc = mi.Mi_shortdesc;
                //            actual_non_ser_List.Tus_itm_model = mi.Mi_model;
                //            actual_non_ser_List.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.Trim());
                //            actual_non_ser_List.Tus_new_status = "1";
                //            _reptPickSerial_nonSer.Tus_new_remarks = "1";

                //            SelectedItemList.Add(_reptPickSerial_nonSer);
                //            rowCount++;
                //            actual_non_ser_List.Add(_reptPickSerial_nonSer);
                //     //   }
                //   // }
                //}
                else
                {
                    // MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // return;
                }
                        
                    
            
                //}
                //#endregion
                btnserialI_Click(null, null);
                LoadItems(ViewState["userSeqNo"].ToString());
                
                if (chkLockItem.Checked == true)
                {
                    txtSerialI.Text = string.Empty;
                    txtSerialII.Text = string.Empty;
                    txtSerialIII.Text = string.Empty;
                    txtQty.Enabled = false;
                    txtSerialI.Focus();
                }
                else
                {
                    PageClear();
                    if (_serList!=null)
                    {
                        if (_serList.Count > 0)
                        {
                            ddlStatus.SelectedValue = _serList.LastOrDefault().Tus_itm_stus;
                        }
                    }
                }



              

                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyNoticeToast('Item added successfully.');", true);

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
                //if (hdnClear.Value == "Yes")
                //{
                PageClear();
                ListClear();
                //}
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

                //WHAREHOUSE
                if (lblvalue.Text == "Bin")
                {
                    txtBinCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBinCode_TextChanged(null, null);
                }
                //Item
                if (lblvalue.Text == "3")
                {
                    txtItemCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    ddlItemType.SelectedValue = "I";
                    ddlItemType.DataBind();
                    txtItemCode_TextChanged(null, null);
                }
                //Supplier.
                if (lblvalue.Text == "16")
                {
                    txtSupplier.Text = grdResult.SelectedRow.Cells[1].Text;
                    //txtSupplier_TextChanged(null, null);
                }
                //GRNNo.
                if (lblvalue.Text == "342")
                {
                    txtGRNNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtGRNNo_TextChanged(null, null);
                }


                ViewState["SEARCH"] = null;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                    UserPopup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
            }
        }

        //#region Add new serial/ new qty
        //private void AddItemQuantites()
        //{
        //    try
        //    {
        //        if (Convert.ToDateTime(txtExpiryDate.Text).Date < DateTime.Now.Date)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter valid expiry date!');", true);
        //            return;
        //        }

        //        if (Convert.ToDateTime(txtManufactureDate.Text).Date > DateTime.Now.Date)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter valid manufacture date!');", true);
        //            return;
        //        }
        //        int _userSeqNo = 0;
        //        //Need to check Whether that is there any record in temp_pick_hdr table in SCMREP DB.
        //        //_userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(Session["UserCompanyCode"].ToString(), _scanDocument);

        //        ////_userSeqNo = _base.CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(DocumentType, Session["UserCompanyCode"].ToString(), _scanDocument, 1);
        //        ////if (_userSeqNo == -1)
        //        ////{
        //        ////    _userSeqNo = GenerateNewUserSeqNo();
        //        ////}

        //        //using (TransactionScope _tr = new TransactionScope())
        //        //{
        //        #region TransactionScope

        //        ////if (_userSeqNo == 0) //Create a new seq no & insert it to the scmrep.temp_pick_hdr header table.  
        //        ////{
        //        ////    _userSeqNo = _base.CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 1, Session["UserCompanyCode"].ToString());

        //        ////    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

        //        ////    _inputReptPickHeader.Tuh_direct = true;
        //        ////    _inputReptPickHeader.Tuh_doc_no = _scanDocument;
        //        ////    _inputReptPickHeader.Tuh_doc_tp = DocumentType;
        //        ////    _inputReptPickHeader.Tuh_ischek_itmstus = false;
        //        ////    _inputReptPickHeader.Tuh_ischek_reqqty = false;
        //        ////    _inputReptPickHeader.Tuh_ischek_simitm = false;
        //        ////    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
        //        ////    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
        //        ////    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
        //        ////    _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;

        //        ////    //Save it to the scmrep.temp_pick_hdr header table. 
        //        ////    Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
        //        ////}

        //        //Get the selected Item
        //        //MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), PopupItemCode);
        //        string _binCode = txtBinCode.Text;
        //        string _itemStatus = ddlStatus.SelectedItem.Text;

        //        if (_itemSerializedStatus == 1 || _itemSerializedStatus == 2 || _itemSerializedStatus == 3)
        //        {
        //            #region Serialized
        //            string _serialNo1 = txtSerialI.Text.Trim();
        //            string _serialNo2 = txtSerialII.Text.Trim();
        //            string _serialNo3 = txtSerialII.Text.Trim();
        //            string _warrantyno = string.Empty;
        //            int _serID = _base.CHNLSVC.Inventory.IsExistInSerialMaster("", txtItemCode.Text, _serialNo1);
        //            InventorySerialMaster _serIDMst = new InventorySerialMaster();
        //            _serIDMst = _base.CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serID);

        //            DataTable _dtser1 = _base.CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", txtItemCode.Text, _serialNo1);
        //            if (_dtser1 != null)
        //            {
        //                if (_dtser1.Rows.Count > 0)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Serial no 1 is already exist.');", true);
        //                    return;
        //                }
        //            }
        //            _dtser1.Dispose();

        //            if ((_base.CHNLSVC.Inventory.IsExistInTempPickSerial(Session["UserCompanyCode"].ToString(), _userSeqNo.ToString(), txtItemCode.Text, _serialNo1)) > 0)
        //            {
        //                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Serial no 1 is already in use.');", true);
        //                return;
        //            }

        //            if (_itemSerializedStatus == 2)
        //            {
        //                DataTable _dtser2 = _base.CHNLSVC.Inventory.CheckSerialAvailability("SERIAL2", txtItemCode.Text, _serialNo2);
        //                if (_dtser2 != null)
        //                {
        //                    if (_dtser2.Rows.Count > 0)
        //                    {
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Serial no 2 is already exist.');", true);
        //                        return;
        //                    }
        //                }
        //                _dtser2.Dispose();

        //                if ((_base.CHNLSVC.Inventory.IsExistInTempPickSerial(Session["UserCompanyCode"].ToString(), _userSeqNo.ToString(), "SER_2", _serialNo2)) > 0)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Serial no 2 is already in use.');", true);
        //                    return;
        //                }
        //            }

        //            _warrantyno = _serIDMst.Irsm_warr_no;

        //            //Write to the Picked items serial table.
        //            ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
        //            #region Fill Pick Serial Object
        //            _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
        //            _inputReptPickSerials.Tus_doc_no = "";//_scanDocument;
        //            _inputReptPickSerials.Tus_seq_no = 0;
        //            _inputReptPickSerials.Tus_itm_line = 0;
        //            _inputReptPickSerials.Tus_batch_line = 0;
        //            _inputReptPickSerials.Tus_ser_line = 0;
        //            _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
        //            _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
        //            _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
        //            _inputReptPickSerials.Tus_bin = _binCode;
        //            _inputReptPickSerials.Tus_itm_cd = txtItemCode.Text;
        //            _inputReptPickSerials.Tus_itm_stus = _itemStatus;
        //            _inputReptPickSerials.Tus_unit_cost = Convert.ToDecimal(txtCost.Text);
        //            _inputReptPickSerials.Tus_unit_price = Convert.ToDecimal(txtCost.Text);
        //            _inputReptPickSerials.Tus_qty = 1;
        //            if (_serID > 0)
        //            { _inputReptPickSerials.Tus_ser_id = _serID; }
        //            else
        //            { _inputReptPickSerials.Tus_ser_id = _base.CHNLSVC.Inventory.GetSerialID(); }
        //            _inputReptPickSerials.Tus_ser_1 = _serialNo1;
        //            _inputReptPickSerials.Tus_ser_2 = _serialNo2;
        //            _inputReptPickSerials.Tus_ser_3 = _serialNo3;
        //            if (string.IsNullOrEmpty(_warrantyno)) _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + Session["UserDefLoca"].ToString() + "-P01-" + _inputReptPickSerials.Tus_ser_id.ToString();
        //            _inputReptPickSerials.Tus_warr_no = _warrantyno;
        //            _inputReptPickSerials.Tus_itm_desc = lblDescription.Text;
        //            _inputReptPickSerials.Tus_itm_model = lblModel.Text;
        //            _inputReptPickSerials.Tus_itm_brand = lblBrand.Text;
        //            _inputReptPickSerials.Tus_itm_line = ItemLineNo;
        //            _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
        //            _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
        //            _inputReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
        //            _inputReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
        //            _inputReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
        //            _inputReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;

        //            //kapila 3/7/2015
        //            _inputReptPickSerials.Tus_orig_supp = _supplier;
        //            _inputReptPickSerials.Tus_exist_supp = _supplier;
        //            _inputReptPickSerials.Tus_orig_grnno = _grnNo;
        //            _inputReptPickSerials.Tus_exist_grnno = _grnNo;
        //            _inputReptPickSerials.Tus_orig_grndt = _grnDate;
        //            _inputReptPickSerials.Tus_exist_grndt = _grnDate;
        //            _inputReptPickSerials.Tus_batch_no = txtBatch.Text;
        //            MasterItem _selectedItem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), PopupItemCode);
        //            if (_selectedItem.MI_IS_EXP_DT == 1)    //27/8/2015
        //                _inputReptPickSerials.Tus_exp_dt = dtExp.Value.Date;

        //            _inputReptPickSerials.Tus_manufac_dt = dtManufc.Value.Date;
        //            _inputReptPickSerials.Tus_new_status = IsNew.ToString();

        //            if (DocumentType == "ADJ")
        //            {
        //                MasterItemWarrantyPeriod _period = _base.CHNLSVC.Inventory.GetItemWarrantyDetail(PopupItemCode, _itemStatus);
        //                if (_period != null)
        //                    _inputReptPickSerials.Tus_warr_period = _period.Mwp_val;
        //            }

        //            #endregion
        //            List<ReptPickSerials> _resultItemsSerialList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);

        //            //-------added by Shani
        //            var serCount = 0;
        //            if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
        //            {
        //                serCount = (from c in _resultItemsSerialList
        //                            where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == ItemLineNo
        //                            select c).Count();
        //            }
        //            //for non serials
        //            //var serCount_2 = (from c in _resultItemsSerialList
        //            //                  select c.Tus_qty).Sum();


        //            if (serCount < Convert.ToDecimal(lblDocQty.Text.Trim()))
        //            {
        //                lblScanQty.Text = serCount.ToString();
        //                CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
        //                //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
        //                ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
        //            }
        //            else
        //            {
        //                MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }
        //            //-------added by Shani
        //            //Save to the temp table.


        //            //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
        //            List<ReptPickSerials> _ResultItemsSerialList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
        //            GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();


        //            //SubmitItemSerialData(); pls do this Chamal

        //            #endregion
        //        }
        //        else if (_itemSerializedStatus == 0)
        //        {
        //            #region Non-serialized
        //            int _actualQty = Convert.ToInt32(txtPopupQty.Text.Trim());
        //            string _warrantyno = string.Empty;

        //            //if (!string.IsNullOrEmpty(txtWarrantyNo.Text))
        //            //    if (txtWarrantyNo.Text.Trim() != "N/A" && txtWarrantyNo.Text.Trim() != "NA")
        //            //    {
        //            //        if (_actualQty > 1 && !string.IsNullOrEmpty(txtWarrantyNo.Text.Trim()))
        //            //            throw new UIValidationException("Can not enter a warranty no for more than one qty");

        //            //        if ((CHNLSVC.Inventory.IsExistInWarrantyMaster(GlbUserComCode, _warrantyno)) > 0)
        //            //            throw new UIValidationException("Warranty is already exist.");

        //            //        if ((CHNLSVC.Inventory.IsExistWarrantyInTempPickSerial(GlbUserComCode, _warrantyno)) > 0)
        //            //            throw new UIValidationException("Warranty is already in use. Enter with different Warranty");
        //            //    }


        //            for (int i = 0; i < _actualQty; i++)
        //            {
        //                //Write to the Picked items serials table.
        //                ReptPickSerials _newReptPickSerials = new ReptPickSerials();
        //                #region Fill Pick Serial Object
        //                _newReptPickSerials.Tus_usrseq_no = _userSeqNo;
        //                _newReptPickSerials.Tus_doc_no = _scanDocument;
        //                _newReptPickSerials.Tus_seq_no = 0;
        //                _newReptPickSerials.Tus_itm_line = 0;
        //                _newReptPickSerials.Tus_batch_line = 0;
        //                _newReptPickSerials.Tus_ser_line = 0;
        //                _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
        //                _newReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
        //                _newReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
        //                _newReptPickSerials.Tus_bin = _binCode;
        //                _newReptPickSerials.Tus_itm_cd = PopupItemCode;
        //                _newReptPickSerials.Tus_itm_stus = _itemStatus;
        //                _newReptPickSerials.Tus_unit_cost = _unitCost;
        //                _newReptPickSerials.Tus_unit_price = _unitPrice;
        //                _newReptPickSerials.Tus_qty = 1;
        //                _newReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
        //                _newReptPickSerials.Tus_ser_1 = "N/A";
        //                _newReptPickSerials.Tus_ser_2 = "N/A";
        //                _newReptPickSerials.Tus_ser_3 = "N/A";
        //                _newReptPickSerials.Tus_warr_no = _warrantyno;
        //                _newReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
        //                _newReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
        //                _newReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;
        //                _newReptPickSerials.Tus_itm_line = ItemLineNo;
        //                _newReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
        //                _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
        //                _newReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
        //                _newReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
        //                _newReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
        //                _newReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;

        //                //kapila 3/7/2015
        //                _newReptPickSerials.Tus_orig_supp = _supplier;
        //                _newReptPickSerials.Tus_exist_supp = _supplier;
        //                _newReptPickSerials.Tus_orig_grnno = _grnNo;
        //                _newReptPickSerials.Tus_exist_grnno = _grnNo;
        //                _newReptPickSerials.Tus_orig_grndt = _grnDate;
        //                _newReptPickSerials.Tus_exist_grndt = _grnDate;
        //                _newReptPickSerials.Tus_batch_no = txtBatch.Text;
        //                MasterItem _selectedItem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), PopupItemCode);
        //                if (_selectedItem.MI_IS_EXP_DT == 1)    //27/8/2015
        //                    _newReptPickSerials.Tus_exp_dt = dtExp.Value.Date;
        //                _newReptPickSerials.Tus_manufac_dt = dtManufc.Value.Date;

        //                if (DocumentType == "ADJ")
        //                {
        //                    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(PopupItemCode, _itemStatus);
        //                    if (_period != null)
        //                        _newReptPickSerials.Tus_warr_period = _period.Mwp_val;
        //                }

        //                #endregion
        //                //Save to the temp table.
        //                //CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);//commented by Shani
        //                //_newReptPickSerials = null; //commented by Shani

        //                //-------added by Shani
        //                List<ReptPickSerials> _resultItemsSerialList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
        //                var serCount = 0;
        //                if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
        //                {
        //                    serCount = (from c in _resultItemsSerialList
        //                                where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == ItemLineNo
        //                                select c).Count();
        //                }
        //                //for non serial, decimal allowed
        //                //var serCount_2 = (from c in _resultItemsSerialList
        //                //                  select c.Tus_qty).Sum();

        //                if (Convert.ToDecimal(serCount) < Convert.ToDecimal(lblDocQty.Text.Trim()))
        //                {
        //                    lblScanQty.Text = serCount.ToString();
        //                    CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);
        //                    //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
        //                    ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
        //                    _newReptPickSerials = null;

        //                    //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
        //                    List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
        //                    GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();


        //                    //SubmitItemSerialData();

        //                }
        //                else
        //                {
        //                    MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    return;
        //                }
        //                //-------added by Shani
        //                //Save to the temp table.

        //            }
        //            #endregion
        //        }
        //        else if (_itemSerializedStatus == -1) //(Non serialize decimal Item = -1))
        //        {
        //            #region Non-serialized Decimal Allow
        //            decimal _actualQty = Convert.ToDecimal(txtPopupQty.Text.Trim());

        //            //Write to the Picked items serials table.
        //            ReptPickSerials _decimalReptPickSerials = new ReptPickSerials();
        //            #region Fill Temp Pick Serial List
        //            _decimalReptPickSerials.Tus_usrseq_no = _userSeqNo;
        //            _decimalReptPickSerials.Tus_doc_no = _scanDocument;
        //            _decimalReptPickSerials.Tus_seq_no = 0;
        //            _decimalReptPickSerials.Tus_itm_line = 0;
        //            _decimalReptPickSerials.Tus_batch_line = 0;
        //            _decimalReptPickSerials.Tus_ser_line = 0;
        //            _decimalReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
        //            _decimalReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
        //            _decimalReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
        //            _decimalReptPickSerials.Tus_bin = _binCode;
        //            _decimalReptPickSerials.Tus_itm_cd = PopupItemCode;
        //            _decimalReptPickSerials.Tus_itm_stus = _itemStatus;
        //            _decimalReptPickSerials.Tus_unit_cost = _unitCost;
        //            _decimalReptPickSerials.Tus_unit_price = _unitPrice;
        //            _decimalReptPickSerials.Tus_qty = _actualQty;
        //            //_decimalReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
        //            _decimalReptPickSerials.Tus_ser_1 = "N/A";
        //            _decimalReptPickSerials.Tus_ser_2 = "N/A";
        //            _decimalReptPickSerials.Tus_ser_3 = "N/A";
        //            _decimalReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
        //            _decimalReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
        //            _decimalReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;
        //            _decimalReptPickSerials.Tus_itm_line = ItemLineNo;
        //            _decimalReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
        //            _decimalReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
        //            _decimalReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
        //            _decimalReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
        //            _decimalReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
        //            _decimalReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;

        //            //kapila 3/7/2015
        //            _decimalReptPickSerials.Tus_orig_supp = _supplier;
        //            _decimalReptPickSerials.Tus_exist_supp = _supplier;
        //            _decimalReptPickSerials.Tus_orig_grnno = _grnNo;
        //            _decimalReptPickSerials.Tus_exist_grnno = _grnNo;
        //            _decimalReptPickSerials.Tus_orig_grndt = _grnDate;
        //            _decimalReptPickSerials.Tus_exist_grndt = _grnDate;
        //            _decimalReptPickSerials.Tus_batch_no = txtBatch.Text;
        //            MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), PopupItemCode);
        //            if (_selectedItem.MI_IS_EXP_DT == 1)    //27/8/2015
        //                _decimalReptPickSerials.Tus_exp_dt = dtExp.Value.Date;

        //            if (DocumentType == "ADJ")
        //            {
        //                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(PopupItemCode, _itemStatus);
        //                if (_period != null)
        //                    _decimalReptPickSerials.Tus_warr_period = _period.Mwp_val;
        //            }
        //            #endregion
        //            List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
        //            if (_resultItemsSerialList == null)
        //            {
        //                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_decimalReptPickSerials, null);
        //                List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
        //                GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
        //            }
        //            else
        //            {
        //                //for non serial, decimal allowed
        //                var serCount_2 = (from c in _resultItemsSerialList
        //                                  where c.Tus_itm_cd == PopupItemCode
        //                                  select c.Tus_qty).Sum();

        //                if (Convert.ToDecimal(serCount_2) < Convert.ToDecimal(lblDocQty.Text.Trim()))
        //                {
        //                    lblScanQty.Text = serCount_2.ToString();
        //                    CHNLSVC.Inventory.SaveAllScanSerials(_decimalReptPickSerials, null);
        //                    ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
        //                    _decimalReptPickSerials = null;

        //                    //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
        //                    List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
        //                    GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();

        //                    //SubmitItemSerialData();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    return;
        //                }
        //            }
        //            #endregion
        //        }

        //        //_tr.Complete();
        //        #endregion
        //        //}

        //    }
        //    catch (Exception err)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        CHNLSVC.CloseChannel();
        //        MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}

        //#region Generate new user seq no
        //private Int32 GenerateNewUserSeqNo()
        //{
        //    Int32 generated_seq = 0;
        //    generated_seq = _base.CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
        //    ReptPickHeader RPH = new ReptPickHeader();
        //    RPH.Tuh_doc_tp = DocumentType;
        //    RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
        //    RPH.Tuh_ischek_itmstus = true;//might change 
        //    RPH.Tuh_ischek_reqqty = true;//might change
        //    RPH.Tuh_ischek_simitm = true;//might change
        //    RPH.Tuh_session_id = Session["SessionID"].ToString();
        //    RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change 
        //    RPH.Tuh_usr_id = Session["UserID"].ToString();
        //    RPH.Tuh_usrseq_no = generated_seq;

        //    RPH.Tuh_direct = true; //direction always (-) for change status
        //    RPH.Tuh_doc_no = _scanDocument;
        //    //write entry to TEMP_PICK_HDR
        //    int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
        //    if (affected_rows == 1)
        //    {
        //        return generated_seq;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        //#endregion
        //#endregion
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.WHAREHOUSE:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GRNNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
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
        private void FilterData()
        {
            //WHAREHOUSE
            if (lblvalue.Text == "Bin")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                DataTable result = _base.CHNLSVC.General.GetWarehouseBinByLoc(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Bin";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //Item
            if (lblvalue.Text == "3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "3";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //Supplier
            if (lblvalue.Text == "16")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = _base.CHNLSVC.CommonSearch.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //GRNNo
            if (lblvalue.Text == "342")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNNo);
                DataTable result = _base.CHNLSVC.CommonSearch.searchGRNData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "342";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
        }
        private void LoadItems(string _seqNo)
        {
            try
            {
                string popup = (string)Session["POPUP"];
                int _direction = 0;
                if (ViewState["adjustmentTypeValue"].ToString() == "+")
                {
                    _direction = 1;
                }

                Int32 user_seq_num = _base.CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ViewState["doc_tp"].ToString(), Session["UserID"].ToString(), _direction, _seqNo);
                if (user_seq_num == -1)
                {
                    if ((ViewState["userSeqNo"] == null))
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }
                    else
                    {
                        user_seq_num = Convert.ToInt32(ViewState["userSeqNo"].ToString());
                    }
                }
                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = _base.CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = _base.CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                    _itm.Itri_qty = _reptitem.Tui_req_itm_qty;
                    if (ViewState["adjustmentTypeValue"].ToString() == "+")
                    {
                        _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    }
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(TXTUnitcost.Text);
                    _itm.Itri_supplier = _reptitem.Tui_sup;
                    _itm.Itri_batchno = _reptitem.Tui_batch;
                    _itm.Itri_grnno = _reptitem.Tui_grn;
                    _itm.Itri_grndate = _reptitem.Tui_grn_dt;
                    _itm.Itri_expdate = _reptitem.Tui_exp_dt;

                   
                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        _itm.Itri_itm_stus_desc = oItemStaus.Find(x => x.Mis_cd == _itm.Itri_itm_stus).Mis_desc;
                    }
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;

                //gvItems.AutoGenerateColumns = false;
                //gvItems.DataSource = ScanItemList;
               
                  
                  if (string.IsNullOrEmpty(popup))
                  {
                      if (((GridView)this.Parent.FindControl("grdItems")) == null)
                      {
                          ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('grdItems not found.');", true);
                          return;
                      }
                      ((GridView)this.Parent.FindControl("grdItems")).DataSource = ScanItemList;
                      ((GridView)this.Parent.FindControl("grdItems")).DataBind();
                  }

                
                _serList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ViewState["doc_tp"].ToString());
                foreach (ReptPickSerials _ser in _serList)
                {
                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        _ser.Tus_itm_stus_Desc = oItemStaus.Find(x => x.Mis_cd == _ser.Tus_itm_stus).Mis_desc;
                    }
                }
                
                if (_serList != null)
                {
                    if (_direction == 0)
                    {
                        if (string.IsNullOrEmpty(popup))
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                            foreach (var itm in _scanItems)
                            {
                                foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grdItems")).Rows)
                                {
                                    if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                                    {
                                        ((Label)row.FindControl("lblitm_PickQty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });

                        if (string.IsNullOrEmpty(popup))
                        {
                                foreach (var itm in _scanItems)
                                {
                                    foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grdItems")).Rows)
                                    {
                                        if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                                        {
                                            ((Label)row.FindControl("lblitri_qty")).Text = Convert.ToDecimal(itm.theCount).ToString("#,##0.####"); // Current scan qty
                                            if (ViewState["adjustmentTypeValue"].ToString() == "+")
                                            {
                                                ((Label)row.FindControl("lblitri_app_qty")).Text = Convert.ToDecimal(itm.theCount).ToString("#,##0.####"); // Current scan qty
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grdItems")).Rows)
                            {
                                if (txtItemCode.Text == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString())
                                {
                                    Decimal pickqtylbl = Convert.ToDecimal(((Label)row.FindControl("lblpickqty")).Text);
                                    Decimal pickqty = Convert.ToDecimal(txtQty.Text.Trim());
                                    Decimal poqty = Convert.ToDecimal(((Label)row.FindControl("lblitri_qty")).Text);
                                    Decimal remainqty = Convert.ToDecimal(((Label)row.FindControl("lblremainqty")).Text);
                                    Decimal actualremainqty = Convert.ToDecimal(remainqty - pickqty);

                                    ((Label)row.FindControl("lblpickqty")).Text = DoFormat(Convert.ToDecimal(pickqtylbl + pickqty));

                                    ((Label)row.FindControl("lblremainqty")).Text = DoFormat(Convert.ToDecimal(actualremainqty));
                                }
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(popup))
                    {
                        if (((GridView)this.Parent.FindControl("grdSerial")) == null)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdSerial not found.');", true);
                            return;
                        }
                        ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serList;
                        ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                    }
                    else
                    {
                        DataTable dtserdup = new DataTable();
                        dtserdup.Columns.AddRange(new DataColumn[13] { new DataColumn("TUS_USRSEQ_NO"), new DataColumn("TUS_BIN"), new DataColumn("TUS_ITM_CD"), new DataColumn("TUS_ITM_DESC"), new DataColumn("TUS_ITM_MODEL"), new DataColumn("TUS_ITM_BRAND"), new DataColumn("TUS_ITM_STUS"), new DataColumn("TUS_SER_1"), new DataColumn("TUS_SER_2"), new DataColumn("TUS_SER_3"), new DataColumn("TUS_WARR_NO"), new DataColumn("TUS_SER_ID"), new DataColumn("Mis_desc") });

                        foreach (ReptPickSerials serial in _serList)
                        {
                            dtserdup.Rows.Add(serial.Tus_seq_no, serial.Tus_bin, serial.Tus_itm_cd, serial.Tus_itm_desc, serial.Tus_itm_model, serial.Tus_itm_brand, serial.Tus_itm_stus, serial.Tus_ser_1, serial.Tus_ser_2,serial.Tus_ser_3, serial.Tus_warr_no, serial.Tus_ser_id,serial.Mis_desc);
                        }
                        ((GridView)this.Parent.FindControl("grdSerial")).DataSource = dtserdup;
                        ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                    }
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    if (((GridView)this.Parent.FindControl("grdSerial")) == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdSerial not found.');", true);
                        return;
                    }
                    ((GridView)this.Parent.FindControl("grdSerial")).DataSource = emptyGridList;
                    ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }
        private bool LoadItemDetail(string _item)
        {
            bool _isValid = false;
            try
            {
                _chargeType = string.Empty;
                //lblDescription.Text = string.Empty;
                //lblModel.Text = string.Empty;
                //lblBrand.Text = string.Empty;
                //lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                PageClear();
                txtItemCode.Text = _item;
                _itemdetail = new MasterItem();

                if (!string.IsNullOrEmpty(_item)) _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_itemdetail != null)
                {
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        if (_itemdetail.Mi_itm_tp == "V")
                        {
                            //txtItemCode.Text = string.Empty;
                            //lblDescription.Text = string.Empty;
                            //lblModel.Text = string.Empty;
                            //lblBrand.Text = string.Empty;
                            //lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                            //txtUnitCost.Text = string.Empty;
                            //txtQty.Text = string.Empty;
                            PageClear();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Virtual item not allowed.');", true);
                            return true;
                        }

                        _isValid = true;
                        _chargeType = _itemdetail.Mi_chg_tp;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _partNo = _itemdetail.Mi_part_no;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Serialized" : "Non-Serialized";
                        Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;

                        Session["Subserial"] = _itemdetail.Mi_is_scansub;
                       

                        if (_itemdetail.Mi_is_ser1 == 1)
                        {
                            pnlSerialized.Visible = true;
                            txtSerialI.ReadOnly = !Convert.ToBoolean(_itemdetail.Mi_is_ser1);
                            txtSerialII.ReadOnly = !Convert.ToBoolean(_itemdetail.Mi_is_ser2);
                            txtSerialIII.ReadOnly = !Convert.ToBoolean(_itemdetail.Mi_is_ser3);
                        }
                        else
                            pnlSerialized.Visible = false;


                        lblDescription.Text = _description;
                        lblModel.Text = _model;
                        lblBrand.Text = _brand;
                        lblPart.Text = _partNo;
                        //lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;
                        _isDecimalAllow = _base.CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                        if (_itemdetail.MI_IS_EXP_DT == 1)
                        {
                            pnlExpiryDate.Visible = true;
                            //dtExp.Visible = true;
                        }
                        else
                        {
                            pnlExpiryDate.Visible = false;
                            //dtExp.Visible = false;
                        }
                    }
                }
                else
                {
                    txtItemCode.Text = string.Empty;
                    lblDescription.Text = string.Empty;
                    lblModel.Text = string.Empty;
                    lblBrand.Text = string.Empty;
                    //lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                    txtCost.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid item code.');", true);
                }
            }
            catch (Exception ex)
            {
                _isValid = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
                return _isValid;
            }
            return _isValid;
        }
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;
            if (ViewState["adjustmentTypeValue"].ToString() == "+")
            {
                _direction = 1;
            }
            generated_seq = _base.CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), ViewState["doc_tp"].ToString(), _direction, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();

            if (ViewState["doc_tp"] == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please set doc_tp.')", true);
                return 0;
            }
            if (((TextBox)this.Parent.FindControl("txtUserSeqNo")) == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('txtUserSeqNo not found.')", true);
                return 0;
            }

            RPH.Tuh_doc_tp = ViewState["doc_tp"].ToString();
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
            int affected_rows = _base.CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                userSeqNo = generated_seq.ToString();
                ((TextBox)this.Parent.FindControl("txtUserSeqNo")).Text = ViewState["userSeqNo"].ToString();
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }
        private void BindUserCompanyItemStatusDDLData(DropDownList ddl)
        {
            DataTable _tbl = _base.CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());

            if (ViewState["doc_tp"] == "ADJ")
            {
                for (int i = _tbl.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = _tbl.Rows[i];
                    if (dr[1].ToString() == "CONS")
                    {
                        dr.Delete();
                    } 
                }
                _tbl.AcceptChanges();
               
            }
            var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();
            var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
            _s.Insert(0, _n);
            ddl.DataSource = _s;
            ddl.DataTextField = "MIS_DESC";
            ddl.DataValueField = "MIC_CD";
            ddl.DataBind();
            ddl.SelectedValue = "GOD";
          
        }
        public void PopulateData(string itemCode, int rowindex)
        {

            txtItemCode.Text = ScanItemList[rowindex].Itri_itm_cd;
            //txtBinCode.Text = 
            //LoadItemDetail(txtItemCode.Text);
            txtItemCode_TextChanged(null, null);
            ddlStatus.SelectedValue = ScanItemList[rowindex].Itri_itm_stus;
            txtBatch.Text = ScanItemList[rowindex].Itri_batchno;
            txtCost.Text = ScanItemList[rowindex].Itri_unit_price.ToString("N2");

            txtSupplier.Text = ScanItemList[rowindex].Itri_supplier;
            List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();
            _custList = _base.CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), txtSupplier.Text, string.Empty, string.Empty, "S");
            if (_custList.Count != 0)
                txtSupplier.ToolTip = _custList[0].Mbe_name.ToString();
            txtGRNNo.Text = ScanItemList[rowindex].Itri_grnno;
            txtGRNDate.Text = ScanItemList[rowindex].Itri_grndate.ToString("dd/MMM/yyyy");

            txtSerialI.Focus();

        }
        //void Delete_Serials(string _itemCode, string _itemStatus, Int32 _serialID, string serialI)
        //{
        //    MasterItem _masterItem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
        //    if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
        //    {
        //        _base.CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), _itemCode, serialI);
        //        _base.CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode, _serialID, 1);
        //    }
        //    else
        //    {
        //        _base.CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus);
        //    }
        //}
        public void PageClear()
        {
            Session["Subserial"] = false;
            if (Session["CLEARITEM"] == null)
            {
                txtItemCode.Text = string.Empty;
            }
            txtItemCode.ReadOnly = false;
            txtBinCode.Text = Session["GlbDefaultBin"].ToString();
            ddlStatus.SelectedIndex = -1;
            txtQty.Text = string.Empty;
            txtQty.ReadOnly = false;
            txtCost.Text = string.Empty;
            txtBatch.Text = string.Empty;

            txtManufactureDate.Text = string.Empty;
            txtExpiryDate.Text = string.Empty;

            txtSerialI.Text = string.Empty;
            txtSerialII.Text = string.Empty;
            txtSerialIII.Text = string.Empty;
            txtSerialI.ReadOnly = false;
            txtSerialII.ReadOnly = false;
            txtSerialIII.ReadOnly = false;

            txtSupplier.Text = string.Empty;
            txtSupplier.ToolTip = string.Empty;
            txtGRNNo.Text = string.Empty;
            txtGRNDate.Text = string.Empty;

            lblDescription.Text = string.Empty;
            lblModel.Text = string.Empty;
            lblBrand.Text = string.Empty;
            lblPart.Text = string.Empty;
            
            grdInventoryBalance.DataSource = new int[] { };
            grdInventoryBalance.DataBind();

            BindUserCompanyItemStatusDDLData(ddlStatus);

            //ScanItemList = null;
            //PickSerial = null;
        }
        public void ListClear()
        {
            ScanItemList.Clear();
            PickSerial = null;
            _serList.Clear();
        }
        public void PageDataClear()
        {
            txtItemCode.Text = string.Empty;
            txtItemCode.ReadOnly = false;
            txtBinCode.Text = Session["GlbDefaultBin"].ToString();
            ddlStatus.SelectedIndex = -1;
            txtQty.Text = string.Empty;
            txtQty.ReadOnly = false;
            txtCost.Text = string.Empty;
            txtBatch.Text = string.Empty;

            txtManufactureDate.Text = string.Empty;
            txtExpiryDate.Text = string.Empty;

            txtSerialI.Text = string.Empty;
            txtSerialII.Text = string.Empty;
            txtSerialIII.Text = string.Empty;
            txtSerialI.ReadOnly = false;
            txtSerialII.ReadOnly = false;
            txtSerialIII.ReadOnly = false;

            txtSupplier.Text = string.Empty;
            txtSupplier.ToolTip = string.Empty;
            txtGRNNo.Text = string.Empty;
            txtGRNDate.Text = string.Empty;

            lblDescription.Text = string.Empty;
            lblModel.Text = string.Empty;
            lblBrand.Text = string.Empty;
            lblPart.Text = string.Empty;

            BindUserCompanyItemStatusDDLData(ddlStatus);

            ViewState["userSeqNo"] = null;
        }

        public void ItemCodeChange()
        {
            txtItemCode_TextChanged(null, null);
        }


        #region prefix
        protected void txtStartPage_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(txtStartPage.Text, out value))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Enter numeric value');", true);
                txtStartPage.Text = "";
                txtlastPages.Text = "";
                txtStartPage.Focus();
                userPrefixSerial.Show();
                return;
            }
            int Pages = (Convert.ToInt32(txtStartPage.Text) + Convert.ToInt32(txtNoOfPages.Text)) - 1;
            txtlastPages.Text = Pages.ToString();
            userPrefixSerial.Show();
        }

        protected void lbtnSavePreFix_Click(object sender, EventArgs e)
        {
            Session["ItemPreFix"] = "true";
           // lbtnItemAdd_Click(null, null);
            txtFix.Text = "";
            txtNoOfPages.Text = "";
            txtStartPage.Text = "";
            txtlastPages.Text = "";
        }

        private DataTable CheckPreFix(string _Icode)
        {
            DataTable _result = _base.CHNLSVC.Inventory.CheckitemPreFix(_Icode);
            return _result;
        }
#endregion prefix

         #region subserial
        protected void btnserialI_Click(object sender, EventArgs e)
        {
            Session["MainItemSerial"] = txtSerialI.Text;
            int serialize = (int)Session["_itemSerializedStatus"];
            bool subserial = (bool)Session["Subserial"];
            if (serialize == 1)
            {

                DataTable _AllowPreFix = CheckPreFix(txtItemCode.Text);
                if (_AllowPreFix.Rows.Count > 0)
                {
                    string AllowpreFix = (_AllowPreFix.Rows[0][1].ToString());
                    if ((AllowpreFix == "") || (AllowpreFix == "0"))
                    {
                        txtNoOfPages.Text = _AllowPreFix.Rows[0][2].ToString();
                        ddlPreFix.Visible = false;
                        txtFix.Visible = true;
                        txtFix.Text = _AllowPreFix.Rows[0][0].ToString();
                        Session["Defalt"] = "true";
                    }
                    else
                    {
                        txtNoOfPages.Text = _AllowPreFix.Rows[0][2].ToString();
                        txtFix.Text = _AllowPreFix.Rows[0][0].ToString();
                        DataTable _PreFix = _base.CHNLSVC.Inventory.GetitemPreFix(txtItemCode.Text, Session["UserCompanyCode"].ToString());
                        ddlPreFix.DataSource = _PreFix;
                        ddlPreFix.DataTextField = "mi_prefix";
                        ddlPreFix.DataValueField = "mi_prefix";
                        ddlPreFix.DataBind();
                        ddlPreFix.Visible = true;
                        txtFix.Visible = false;
                        Session["Defalt"] = "false";
                    }
                    txtStartPage.Focus();
                    userPrefixSerial.Show();
                    return;
                }
                else if (subserial == true)
                {
                    tstSubSerial.Focus();

                    //lbtnItemAdd_Click(null, null);
                    //// ClientScript.RegisterStartupScript(this.GetType(), "key", "userSubSerial();", true);
                    DataTable _result = _base.CHNLSVC.Inventory.GetSubSerials(txtItemCode.Text, Convert.ToInt32(ViewState["userSeqNo"]), txtSerialI.Text);
                    SubItemList(_result);
                    getRowValue(1);
                    int count = 1;

                    foreach (DataRow row in _result.Rows)
                    {
                        // your index is in i
                        count = _result.Rows.IndexOf(row);
                    }
                    Session["subItemRowCount"] = count.ToString();
                    Session["subItemCurrentRowCount"] = "1";

                }
                else
                {
                   // lbtnItemAdd_Click(null, null);
                }
            }
            if (Session["_itemSerializedStatus"].ToString() == "1")
            {
               // tstSubSerial.Focus();
            }
            else
            {
               // txtSerial2.Focus();
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
                    dr["tpss_m_ser"] = txtSerialI.Text;
                    dr["tpss_itm_cd"] = tblr["tpss_itm_cd"].ToString();
                    dr["tpss_tp"] = tblr["tpss_tp"].ToString();
                    dr["mis_desc"] = tblr["mis_desc"].ToString();
                    dr["tpss_itm_stus"] = tblr["tpss_itm_stus"].ToString();
                    dr["tpss_sub_ser"] = tblr["tpss_sub_ser"].ToString();
                    _SubItemTbl.Rows.Add(dr);
                    row++;
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
        protected void tstSubSerial_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(tstSubSerial.Text))
            {
                userSubSerial.Show();
                return;
            }
            List<ReptPickSerialsSub> _listSubSer = _base.CHNLSVC.Inventory.GET_TEMP_PICK_SER_SUB(new ReptPickSerialsSub()
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

            List<InventorySubSerialMaster> _subSerList = _base.CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster()
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
                        txtSerialI.Text = "";
                        txtSerialII.Text = "";
                        txtSerialI.Focus();
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

            Int32 Result = _base.CHNLSVC.Inventory.UpdateAllScanSubSerials(_reptPickSerialsSub);
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

        #endregion

        protected void test_Click(object sender, EventArgs e)
        {
            lbtnAdd_Click(null,null);
        }

        //Adde By Dulaj 2018/Jun/05
        public void changeName()
        {
            lockItem.InnerHtml = "Lock Asset Code";
            itemCodeDiv.InnerHtml = "Asset Code";
        }
    }
}