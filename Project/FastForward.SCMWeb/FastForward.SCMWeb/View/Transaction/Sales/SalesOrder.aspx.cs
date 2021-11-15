using AjaxControlToolkit;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.Sales;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Sales
{
    public partial class SalesOrder : BasePage
    {
        #region Variables

        private List<InvoiceItem> _invoiceItemList = null; private List<InvoiceItem> _invoiceItemListWithDiscount = null; private List<RecieptItem> _recieptItem = null; private List<RecieptItem> _newRecieptItem = null;
        private MasterBusinessEntity _businessEntity = null; private List<MasterItemComponent> _masterItemComponent = null; private PriceBookLevelRef _priceBookLevelRef = null; private List<PriceBookLevelRef> _priceBookLevelRefList = null;
        private List<PriceDetailRef> _priceDetailRef = null; private MasterBusinessEntity _masterBusinessCompany = null; private List<PriceSerialRef> _MainPriceSerial = null; private List<PriceSerialRef> _tempPriceSerial = null; private List<PriceCombinedItemRef> _MainPriceCombinItem = null; private List<PriceCombinedItemRef> _tempPriceCombinItem = null;
        private bool _isInventoryCombineAdded = false; private Int32 ScanSequanceNo = 0; private List<ReptPickSerials> ScanSerialList = null; private bool IsPriceLevelAllowDoAnyStatus = false; private string WarrantyRemarks = string.Empty; private Int32 WarrantyPeriod = 0; private string ScanSerialNo = string.Empty; private string DefaultItemStatus = string.Empty;
        private List<InvoiceSerial> InvoiceSerialList = null; private List<ReptPickSerials> InventoryCombinItemSerialList = null; private List<ReptPickSerials> PriceCombinItemSerialList = null; private List<ReptPickSerials> BuyBackItemList = null;
        private Int32 _lineNo = 0; private bool _isEditPrice = false; private bool _isEditDiscount = false; private decimal GrndSubTotal = 0; private decimal GrndDiscount = 0; private decimal GrndTax = 0; private decimal _toBePayNewAmount = 0;
        protected bool _isCompleteCode { get { return (bool)Session["_isCompleteCode"]; } set { Session["_isCompleteCode"] = value; } }
        protected List<CashGeneralEntiryDiscountDef> _CashGeneralEntiryDiscount { get { return (List<CashGeneralEntiryDiscountDef>)Session["_CashGeneralEntiryDiscount"]; } set { Session["_CashGeneralEntiryDiscount"] = value; } }
        public decimal SSPriceBookPrice = 0; public string SSPriceBookSequance = string.Empty; public string SSPriceBookItemSequance = string.Empty; public string SSIsLevelSerialized = string.Empty; public string SSPromotionCode = string.Empty; public string SSCirculerCode = string.Empty; public Int32 SSPRomotionType = 0; public Int32 SSCombineLine = 0;
        private Dictionary<decimal, decimal> ManagerDiscount = null; private CashGeneralEntiryDiscountDef GeneralDiscount = null; private string DefaultBook = string.Empty; private string DefaultLevel = string.Empty; private string DefaultInvoiceType = string.Empty; private string DefaultStatus = string.Empty; private string DefaultBin = string.Empty; private MasterItem _itemdetail = null;
        private List<MasterItemTax> MainTaxConstant = null; private List<ReptPickSerials> _promotionSerial = null; private List<ReptPickSerials> _promotionSerialTemp = null;
        private bool _isBackDate = false; private MasterProfitCenter _MasterProfitCenter = null; private List<PriceDefinitionRef> _PriceDefinitionRef = null; private const string InvoiceBackDateName = "SALESENTRY"; private static int VirtualCounter = 1;
        private bool _isGiftVoucherCheckBoxClick = false; private DataTable MasterChannel = null; private bool IsSaleFigureRoundUp = false; private DataTable _tblExecutive = null; private bool IsFwdSaleCancelAllowUser = false; private bool IsDlvSaleCancelAllowUser = false; private bool _IsVirtualItem = false; private string technicianCode = string.Empty; private bool _iswhat = false;
        private DataTable _tblPromotor = null;
        private bool _serialMatch = true; private PriortyPriceBook _priorityPriceBook = null;
        private bool _processMinusBalance = false;
        private int _discountSequence;
        private bool _isRegistrationMandatory = false;
        private bool _isNeedRegistrationReciept = false;
        private decimal _totalRegistration = 0;
        //private List<RegistrationList> _List = new List<RegistrationList>();
        private LoyaltyType _loyaltyType;
        private int _proVouInvcLine = 0;
        private string _proVouInvcItem = string.Empty;
        private Boolean _isGroup = false;
        private DateTime _serverDt = DateTime.Now.Date;
        private bool _isCombineAdding = false;
        private bool _isNewPromotionProcess = false;
        private List<PriceDetailRef> _PriceDetailRefPromo = null;
        private List<PriceSerialRef> _PriceSerialRefPromo = null;
        private List<PriceSerialRef> _PriceSerialRefNormal = null;
        private List<PurchaseOrderDetail> _POItemList = new List<PurchaseOrderDetail>();
        private List<PurchaseOrderDelivery> _POItemDel = new List<PurchaseOrderDelivery>();
        private List<PurchaseReq> _PurchaseReqList = new List<PurchaseReq>();
        private List<InventoryRequestItem> _InventoryRequestItem = new List<InventoryRequestItem>();
        private MasterBusinessEntity _supDet = new MasterBusinessEntity();
        protected List<MasterItemStatus> oMasterItemStatuss { get { return (List<MasterItemStatus>)Session["oMasterItemStatuss"]; } set { Session["oMasterItemStatuss"] = value; } }
        string _serialstatus = string.Empty;
        Int32 _delLineNo = 0;

        #endregion Variables

        private DataTable _levelStatus = null;
        string _userid = string.Empty;
        Decimal exchangerate;
        MasterBusinessEntity _entity = new MasterBusinessEntity();
        private bool _stopit = false;
        DataTable uniqueitems = new DataTable();
        DataTable dtiInvtems = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    gvInvoiceItem.DataSource = new int[] { };
                    gvInvoiceItem.DataBind();

                    gvPopSerial.DataSource = new int[] { };
                    gvPopSerial.DataBind();

                    gvGiftVoucher.DataSource = new int[] { };
                    gvGiftVoucher.DataBind();

                    gvBuyBack.DataSource = new int[] { };
                    gvBuyBack.DataBind();

                    //grdInventoryBalance.DataSource = new int[] { };
                    //grdInventoryBalance.DataBind();

                    grvwarehouseitms.DataSource = new int[] { };
                    grvwarehouseitms.DataBind();

                    txtresno.Text = "N/A";

                    DateTime orddate = DateTime.Now;
                    txtdate.Text = orddate.ToString("dd/MMM/yyyy");

                    Invoice();
                    InitializeValuesNDefaultValueSet();
                    Invoice_Load();

                    LoadExecutive();
                    LoadPromotor();
                    LoadInvoiceType();

                    ViewState["ITEMSTABLE"] = null;
                    DataTable dtitems = new DataTable();
                    dtitems.Columns.AddRange(new DataColumn[15] { new DataColumn("soi_itm_line"), new DataColumn("soi_itm_cd"), new DataColumn("description"), new DataColumn("soi_itm_stus"), new DataColumn("soi_qty"), new DataColumn("soi_unit_rt"), new DataColumn("soi_unit_amt"), new DataColumn("soi_disc_rt"), new DataColumn("soi_disc_amt"), new DataColumn("soi_itm_tax_amt"), new DataColumn("soi_tot_amt"), new DataColumn("soi_pbook"), new DataColumn("soi_pb_lvl"), new DataColumn("soi_res_no"), new DataColumn("itri_seq_no") });
                    ViewState["ITEMSTABLE"] = dtitems;
                    this.BindItemsGrid();

                    ViewState["SERIALSTABLE"] = null;
                    DataTable dtserials = new DataTable();
                    dtserials.Columns.AddRange(new DataColumn[9] { new DataColumn("sose_itm_line"), new DataColumn("sose_itm_cd"), new DataColumn("Model"), new DataColumn("Status"), new DataColumn("StatusDesc"), new DataColumn("Qty"), new DataColumn("sose_ser_1"), new DataColumn("sose_ser_2"), new DataColumn("Warranty") });
                    ViewState["SERIALSTABLE"] = dtserials;
                    this.BindSerialsGrid();

                    string SearchParamsTown = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                    DataTable result = CHNLSVC.CommonSearch.GetTown(SearchParamsTown, null, null);
                    ddltown.DataSource = result;
                    ddltown.DataTextField = "TOWN";
                    ddltown.DataValueField = "CODE";
                    if (result.Rows.Count > 0)
                    {
                        ddltown.DataBind();
                    }
                    ddltown.Items.Insert(0, new ListItem("Select", "0"));

                    txtDisRate.Text = DoFormat(0);
                    txtDisAmt.Text = DoFormat(0);
                    txtTaxAmt.Text = DoFormat(0);
                    txtLineTotAmt.Text = DoFormat(0);

                    txtdocrefno.Focus();

                    //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16011))
                    //{
                    //    lbtnapprove.Enabled = false;
                    //    lbtnapprove.CssClass = "buttoncolor";
                    //    lbtnapprove.OnClientClick = "return Enable();";
                    //    lbtnapprove.ToolTip = "You dont have permission to approve .Permission code : 16011";
                    //}
                    //else
                    //{
                    //    lbtnapprove.Enabled = true;
                    //    lbtnapprove.CssClass = "buttonUndocolor";
                    //    lbtnapprove.OnClientClick = "ConfirmApproveOrder();";
                    //}

                   

                    //string word = "Select";

                    //if (cmbStatus.Items.FindByText(word.ToUpper()) == null)
                    //{
                    //    cmbStatus.Items.Insert(0, new ListItem("Select", "0"));
                    //}

                    //hiddndv.Visible = false;
                    txtcurrency.Text = "LKR";
                    lblcurrency.Text = "SRI LANKA RUPEE";


                    lblGrndSubTotal.Text = DoFormat(0);
                    lblGrndDiscount.Text = DoFormat(0);
                    lblGrndAfterDiscount.Text = DoFormat(0);
                    lblGrndTax.Text = DoFormat(0);
                    lblGrndTotalAmount.Text = DoFormat(0);
                    txtQuotation.ReadOnly = true;

                    btnSave.CssClass = "buttonUndocolor";
                    btnSave.OnClientClick = "ConfirmPlaceOrder()";

                    ////lbtnapprove.CssClass = "buttoncolor";
                    ////lbtnapprove.OnClientClick = "return Enable();";

                    ////lbtnreject.CssClass = "buttoncolor";
                    ////lbtnreject.OnClientClick = "return Enable();";

                    ////lbtncancel.CssClass = "buttoncolor";
                    ////lbtncancel.OnClientClick = "return Enable();";
                    txtCustomer.Text = string.Empty;
                    dvhiddendel.Visible = true;
                    cmbBook.SelectedValue = "ABANS";
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
            else
            {
                //if (Session["c"] != null)
                //{
                //    if (Session["c"].ToString() == "true")
                //    {
                //        CustomerPopoup.Show();
                //    }
                //    else
                //    {
                //        CustomerPopoup.Hide();
                //    }
                //}

                if (Session["_cusCode"] != null)
                {
                    txtCustomer.Text = Session["_cusCode"].ToString();
                    LoadCustomerDetailsByCustomer();
                    Session["_cusCode"] = null;
                    Session["c"] = "false";
                }

                string cuspopup = (string)Session["CUSPOPUP"];

                if (!string.IsNullOrEmpty(cuspopup))
                {
                    CustomerPopoup.Show();
                }
                else
                {
                    CustomerPopoup.Hide();
                }
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
        protected void BindItemsGrid()
        {
            try
            {
                gvInvoiceItem.DataSource = (DataTable)ViewState["ITEMSTABLE"];
                gvInvoiceItem.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void BindSerialsGrid()
        {
            try
            {
                gvPopSerial.DataSource = (DataTable)ViewState["SERIALSTABLE"];
                gvPopSerial.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "13")
                {
                    txtCustomer.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadCustomerDetailsByCustomer();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                else if (lblvalue.Text == "13a")
                {
                    txtdelcuscode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtdelname.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtdelad1.Text = grdResult.SelectedRow.Cells[3].Text;
                    txtdelad2.Text = string.Empty;

                    string city = grdResult.SelectedRow.Cells[4].Text;

                    if (ddltown.Items.FindByValue(city.ToUpper()) != null)
                    {
                        ddltown.SelectedValue = city.ToUpper();
                    }
                    else
                    {
                        ddltown.SelectedIndex = 0;
                    }


                    MpDelivery.Show();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                else if (lblvalue.Text == "32")
                {
                    txtNIC.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadCustomerDetailsByNIC();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                else if (lblvalue.Text == "33")
                {
                    txtMobile.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadCustomerDetailsByMobile();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                //if (lblvalue.Text == "168")
                //{
                //    txtLoyalty.Text = grdResult.SelectedRow.Cells[1].Text;
                //}

                else if (lblvalue.Text == "76")
                {
                    txtSerialNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    Session["WARRANTY"]= grdResult.SelectedRow.Cells[2].Text;
                    CheckSerialAvailability();

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                else if (lblvalue.Text == "134")
                {
                    txtSerialNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    Session["WARRANTY"] = grdResult.SelectedRow.Cells[4].Text;
                    CheckSerialAvailability();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                else if (lblvalue.Text == "158")
                {
                    txtSerialNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "16")
                {
                    txtCustomer.Text = grdResult.SelectedRow.Cells[2].Text;
                    LoadCusData();
                    LoadCusLoyalityNo();
                    string Reqno = grdResult.SelectedRow.Cells[1].Text;
                    LoadInvItems(Convert.ToInt32(grdResult.SelectedRow.Cells[3].Text), Reqno);

                    foreach (GridViewRow row in gvInvoiceItem.Rows)
                    {
                        Decimal qty = 0;
                        Decimal upri = 0;
                        Decimal dis = 0;
                        Decimal tax = 0;

                        if ((string.IsNullOrEmpty(row.Cells[4].Text.ToString())) || (row.Cells[4].Text.ToString() == "&nbsp;"))
                        {
                            qty = 0;
                        }
                        else
                        {
                            qty = Convert.ToDecimal(row.Cells[4].Text);
                        }

                        if ((string.IsNullOrEmpty(row.Cells[5].Text.ToString())) || (row.Cells[5].Text.ToString() == "&nbsp;"))
                        {
                            upri = 0;
                        }
                        else
                        {
                            upri = Convert.ToDecimal(row.Cells[5].Text);
                        }

                        if ((string.IsNullOrEmpty(row.Cells[8].Text.ToString())) || (row.Cells[8].Text.ToString() == "&nbsp;"))
                        {
                            dis = 0;
                        }
                        else
                        {
                            dis = Convert.ToDecimal(row.Cells[8].Text);
                        }

                        if ((string.IsNullOrEmpty(row.Cells[9].Text.ToString())) || (row.Cells[9].Text.ToString() == "&nbsp;"))
                        {
                            tax = 0;
                        }
                        else
                        {
                            tax = Convert.ToDecimal(row.Cells[9].Text);
                        }

                        CalculateGrandTotalNew(qty, upri, dis, tax, true);
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "14")
                {
                    txtcurrency.Text = grdResult.SelectedRow.Cells[1].Text;
                    lblcurrency.Text = grdResult.SelectedRow.Cells[2].Text;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "81")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadItemDetail(txtItem.Text);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                    //MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());

                    //if (_itemdetail.Mi_is_ser1 == 1)
                    //{
                    //    if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('You have to select the serial number for the serialized item !!!');", true);
                    //        txtQty.ReadOnly = true;
                    //    }
                    //}
                    //else
                    //{
                    //    txtQty.ReadOnly = false;
                    //}
                    List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                    _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtdate.Text));
                    if (_priceDetailRef.Count <= 0)
                    {
                        //if (!_isCompleteCode)
                        //{
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There is no price for the selected item !!!')", true);

                        SetDecimalTextBoxForZero(true, false, true);

                        //}
                        //else
                        //{
                        txtUnitPrice.Text = "0";
                        return;
                        //}
                    }
                    else
                    {
                        //if (_isCompleteCode)
                        //{
                        List<PriceDetailRef> _new = new List<PriceDetailRef>();
                        _new = _priceDetailRef;
                        _priceDetailRef = new List<PriceDetailRef>();
                        var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                        if (_p != null)
                            if (_p.Count > 0)
                            {
                                if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                                _priceDetailRef.Add(_p[0]);
                            }
                        //}
                        if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                        {
                            var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                            if (_isSuspend > 0)
                            {
                              //  ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Price has been suspended. Please contact IT dept !!!')", true);
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Price has been suspended.')", true);

                                return;
                            }
                        }
                        if (_priceDetailRef.Count > 1)
                        {
                            SetColumnForPriceDetailNPromotion(false);
                            BindNonSerializedPrice(_priceDetailRef);
                            return;
                        }
                        else if (_priceDetailRef.Count == 1)
                        {
                            var _one = from _itm in _priceDetailRef
                                       select _itm;
                            int _priceType = 0;
                            foreach (var _single in _one)
                            {
                                _priceType = _single.Sapd_price_type;
                                PriceTypeRef _promotion = TakePromotion(_priceType);
                                decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                                txtUnitPrice.Text = (Convert.ToString(UnitPrice));
                                txtUnitPrice.Text = UnitPrice.ToString("N2");
                                WarrantyRemarks = _single.Sapd_warr_remarks;
                                SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                                Int32 _pbSq = _single.Sapd_pb_seq;
                                Int32 _pbiSq = _single.Sapd_seq_no;
                                string _mItem = _single.Sapd_itm_cd;
                                SetColumnForPriceDetailNPromotion(false);
                                BindNonSerializedPrice(_priceDetailRef);

                                if (_isCombineAdding == false) txtUnitPrice.Focus();
                            }
                        }
                    }
                
                    //grdInventoryBalance.DataSource = null;
                    //grdInventoryBalance.DataBind();

                    //DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), string.Empty);
                    //grdInventoryBalance.DataSource = null;
                    //grdInventoryBalance.AutoGenerateColumns = false;
                    //grdInventoryBalance.DataSource = _inventoryLocation;
                    //grdInventoryBalance.DataBind();

                }

                else if (lblvalue.Text == "421")
                {
                    txtInvoiceNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadSoData(txtInvoiceNo.Text, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                    LoadSoItemData(txtInvoiceNo.Text);
                    LoadSoSerialData(txtInvoiceNo.Text);
                    CheckInvoiceNo();

                    _masterBusinessCompany = new MasterBusinessEntity();
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());

                    if (_masterBusinessCompany.Mbe_is_tax) 
                    { 
                        chkTaxPayable.Checked = true; 
                        chkTaxPayable.Enabled = true; 
                    } 
                    else 
                    { 
                        chkTaxPayable.Checked = false;
                        chkTaxPayable.Enabled = false;
                    }
                    ViewCustomerAccountDetail(txtCustomer.Text.Trim());
                    lblordstus.Text = grdResult.SelectedRow.Cells[4].Text;

                    string stus = (string)Session["STUS"];

                    if (stus == "R")
                    {
                        btnSave.CssClass = "buttoncolor";
                        btnSave.OnClientClick = "return Enable();";

                        lbtnapprove.CssClass = "buttoncolor";
                        lbtnapprove.OnClientClick = "return Enable();";

                        lbtncancel.CssClass = "buttoncolor";
                        lbtncancel.OnClientClick = "return Enable();";

                        lbtnreject.CssClass = "buttoncolor";
                        lbtnreject.OnClientClick = "return Enable();";
                    }

                    else if (stus == "A")
                    {
                        btnSave.CssClass = "buttoncolor";
                        btnSave.OnClientClick = "return Enable();";

                        lbtnapprove.CssClass = "buttoncolor";
                        lbtnapprove.OnClientClick = "return Enable();";

                        lbtnreject.CssClass = "buttoncolor";
                        lbtnreject.OnClientClick = "return Enable();";

                        lbtncancel.CssClass = "buttonUndocolor";
                        lbtncancel.OnClientClick = "ConfirmCancelOrder();";
                    }

                    else if (stus == "C")
                    {
                        btnSave.CssClass = "buttoncolor";
                        btnSave.OnClientClick = "return Enable();";

                        lbtnapprove.CssClass = "buttoncolor";
                        lbtnapprove.OnClientClick = "return Enable();";

                        lbtnreject.CssClass = "buttoncolor";
                        lbtnreject.OnClientClick = "return Enable();";

                        lbtncancel.CssClass = "buttoncolor";
                        lbtncancel.OnClientClick = "return Enable();";
                    }
                    else if (stus == "S")
                    {
                        btnSave.CssClass = "buttoncolor";
                        btnSave.OnClientClick = "return Enable();";

                        lbtnapprove.CssClass = "buttonUndocolor";
                        lbtnapprove.OnClientClick = "ConfirmApproveOrder();";

                        lbtnreject.CssClass = "buttonUndocolor";
                        lbtnreject.OnClientClick = "ConfirmRejectOrder();";

                        lbtncancel.CssClass = "buttoncolor";
                        lbtncancel.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        btnSave.CssClass = "buttonUndocolor";
                        btnSave.OnClientClick = "ConfirmPlaceOrder();";

                        lbtnapprove.CssClass = "buttonUndocolor";
                        lbtnapprove.OnClientClick = "ConfirmApproveOrder();";

                        lbtnreject.CssClass = "buttonUndocolor";
                        lbtnreject.OnClientClick = "ConfirmRejectOrder();";

                        lbtncancel.CssClass = "buttonUndocolor";
                        lbtncancel.OnClientClick = "ConfirmCancelOrder();";
                    }

                    foreach (GridViewRow row in gvInvoiceItem.Rows)
                    {
                        CalculateGrandTotalNew(Convert.ToDecimal(row.Cells[4].Text), Convert.ToDecimal(row.Cells[5].Text), Convert.ToDecimal(row.Cells[8].Text), Convert.ToDecimal(row.Cells[9].Text), true);
                    }


                    Decimal grandtot = Convert.ToDecimal(lblGrndAfterDiscount.Text) + Convert.ToDecimal(lblGrndTax.Text);
                    lblGrndTotalAmount.Text = DoFormat(grandtot);

                    dvhiddendel.Visible = false;

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                else if (lblvalue.Text == "1")
                {
                    txtlocation.Text = grdResult.SelectedRow.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                else if (lblvalue.Text == "5")
                {
                    txtdellocation.Text = grdResult.SelectedRow.Cells[1].Text;
                    SIPopup.Hide();
                    MpDelivery.Show();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void CheckInvoiceNo()
        {
            //if (string.IsNullOrEmpty(txtInvoiceNo.Text)) { txtCustomer.Focus(); return; }
            try
            {
            //    if (IsToken)
            //    {
            //        if (IsNumeric(txtInvoiceNo.Text.Trim()) == false)
            //        { 
            //            using (new CenterWinDialog(this)) 
            //            { 
            //                MessageBox.Show("Token should be consist of numeric only", "Token", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            //            } 
            //            txtInvoiceNo.Clear(); 
            //            txtInvoiceNo.Focus(); 
            //            return; 
            //        }

            //        DataTable _token = CHNLSVC.Inventory.GetAvailableToken(DateTime.Now.Date, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToInt32(txtInvoiceNo.Text.Trim()));
            //        if (_token == null || _token.Rows.Count <= 0)
            //        { 
            //            using (new CenterWinDialog(this)) 
            //            { 
            //                MessageBox.Show("Select token is not valid or incorrect. Please check the no", "Token", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            } 
            //            txtInvoiceNo.Clear(); 
            //            txtInvoiceNo.Focus(); 
            //            return; 
            //        }
            //        return;
                //}

                //DecideTokenInvoice();
                //RecallInvoice();

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

        private void RecallInvoice()
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                return;
            }

            InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text);

            if (_hdr == null) 
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid sales order !!!')", true);
                txtInvoiceNo.Text = string.Empty; 
                return;  
            }
            
            if (_hdr.Sah_pc != Session["UserDefProf"].ToString().ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid sales order !!!')", true);
                txtInvoiceNo.Text = string.Empty;
                return; 
            }

            if (_hdr.Sah_tp != "INV") 
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid sales order !!!')", true);
                txtInvoiceNo.Text = string.Empty;
                return; 
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid sales order !!!')", true);
                txtInvoiceNo.Text = string.Empty;
                return; 
            }

            AssignInvoiceHeaderDetail(_hdr);

            List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoiceNo.Text.Trim());
            _invoiceItemList = _list;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            InvoiceSerialList = new List<InvoiceSerial>();
            ScanSerialList = new List<ReptPickSerials>();
            InvoiceSerialList = CHNLSVC.Sales.GetInvoiceSerial(txtInvoiceNo.Text.Trim());
            foreach (InvoiceItem itm in _list)
            { CalculateGrandTotalNew(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true); _lineNo += 1; SSCombineLine += 1; }
            if (InvoiceSerialList == null)
                InvoiceSerialList = new List<InvoiceSerial>();
            gvInvoiceItem.DataSource = _list;

            
            if (InvoiceSerialList != null && InvoiceSerialList.Count > 0)
            {
                foreach (InvoiceSerial invSer in InvoiceSerialList)
                {
                    ReptPickSerials _rept = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), invSer.Sap_itm_cd, invSer.Sap_ser_1, "N/A", "");
                    if (_rept != null)
                    {
                        List<InvoiceItem> _item = (from _res in _invoiceItemList
                                                   where _res.Sad_itm_cd == invSer.Sap_itm_cd &&
                                                   _res.Sad_itm_line == invSer.Sap_itm_line
                                                   select _res).ToList<InvoiceItem>();
                        if (_item == null || _item.Count <= 0)
                        {
                            string msg = "Error occurred while recalling invoice\nItem - " + invSer.Sap_itm_cd + " not found on item list !!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msg +"')", true);
                            return;
                        }
                        _rept.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                        _rept.Tus_base_itm_line = _item[0].Sad_itm_line;
                        _rept.Tus_usrseq_no = -100;
                        _rept.Tus_unit_price = _rept.Tus_unit_price;
                        MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), invSer.Sap_itm_cd);

                        _rept.Tus_new_status = _item[0].Mi_itm_stus;
                        _rept.ItemType = msitem.Mi_itm_tp;
                        ScanSerialList.Add(_rept);
                    }
                }
            }
            gvPopSerial.AutoGenerateColumns = false;
            gvPopSerial.DataSource = ScanSerialList;

            List<RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(txtInvoiceNo.Text.Trim());
            //ucPayModes1.RecieptItemList = _itms;
            _recieptItem = _itms;
            //ucPayModes1.LoadRecieptGrid();

            //ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
           // ucPayModes1.LoadData();

            if (_hdr.Sah_stus != "H")
            {
                btnSave.Enabled = false;
                txtItem.Enabled = false;
                txtSerialNo.Enabled = false;
                lbtnadditems.Visible = false;
            }
            else
            {
                btnSave.Enabled = true;
                txtItem.Enabled = true;
                txtSerialNo.Enabled = true;
                lbtnadditems.Visible = true;
            }
        }

        private void ClearItemData()
        {
            txtSerialNo.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
            txtUnitAmt.Text = string.Empty;
            txtDisRate.Text = string.Empty;
            txtDisAmt.Text = string.Empty;
            txtTaxAmt.Text = string.Empty;
            txtLineTotAmt.Text = string.Empty;
            txtresno.Text = string.Empty;
            cmbBook.SelectedIndex = 0;
            cmbLevel.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
        }
        private void AssignInvoiceHeaderDetail(InvoiceHeader _hdr)
        {
            cmbInvType.Text = _hdr.Sah_inv_tp;
            txtdate.Text = _hdr.Sah_dt.ToString("dd/MM/yyyy");
            txtCustomer.Text = _hdr.Sah_cus_cd;
            //txtLoyalty.Text = _hdr.Sah_anal_6;
            _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text, string.Empty, string.Empty, "C");
            SetCustomerAndDeliveryDetails(true, _hdr);
            ViewCustomerAccountDetail(txtCustomer.Text);
            //txtExecutive.Text = _hdr.Sah_sales_ex_cd;
            DataTable _recallemp = CHNLSVC.Sales.GetinvEmp(Session["UserCompanyCode"].ToString(), _hdr.Sah_sales_ex_cd);
            string _name = string.Empty;
            string _code = "";
            if (_recallemp != null && _recallemp.Rows.Count > 0)
            {
                _name = _recallemp.Rows[0].Field<string>("esep_first_name");
                _code = _recallemp.Rows[0].Field<string>("esep_epf");
            }
            cmbExecutive.SelectedValue = _code;
            txtcurrency.Text = _hdr.Sah_currency;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            chkTaxPayable.Checked = _hdr.Sah_tax_inv ? true : false;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            txtdocrefno.Text = _hdr.Sah_ref_doc;
            txtPoNo.Text = _hdr.Sah_anal_4;
            //txtRemarks.Text = _hdr.Sah_remarks;

            string COMPANYCURRE = "";
            DataTable CompanyCurrancytbl = CHNLSVC.CommonSearch.SearchCompanyCurrancy(Session["UserCompanyCode"].ToString());
            if (CompanyCurrancytbl.Rows.Count > 0)
            {
                COMPANYCURRE = CompanyCurrancytbl.Rows[0]["CURRENCY"].ToString();
            }
            DataTable ERateTbl = CHNLSVC.Financial.GetExchangeRate(Session["UserCompanyCode"].ToString(), txtcurrency.Text, COMPANYCURRE);
            if (ERateTbl.Rows.Count > 0)
            {

                lblcurrency.Text = txtcurrency.Text.ToString() + "(" + ERateTbl.Rows[0][5].ToString() + ")";
            }

        }
        private void CheckSerialAvailability()
        {
            if (_stopit) return;
            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim())) return;

            //if (txtSerialNo.Text.Trim().ToUpper() == "N/A" || txtSerialNo.Text.Trim().ToUpper() == "NA")
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Selected serial number is invalid or not available in your location. Please check your inventory !!!";
            //    ClearItemData();
            //    return;
            //}

            //DataTable _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerialwithoutItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtSerialNo.Text.Trim());
            DataTable _dtSer = CHNLSVC.Inventory.GetMultipleItemforOneSerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, txtSerialNo.Text.Trim(), string.Empty);

            if (_dtSer.Rows.Count > 1)
            {
                dgvItem.DataSource = new DataTable();
                dgvItem.DataSource = _dtSer;
                dgvItem.DataBind();

                lblcaption.Text = "Select Item Code";
                mpexcel.Show();
                return;
            }
            else
            {
                txtItem.Text = string.Empty;
                lblItemDescription.Text = string.Empty;
                lblItemModel.Text = string.Empty;
                lblItemBrand.Text = string.Empty;

                try
                {
                    if (chkPickGV.Checked)
                    {
                        if (!IsNumeric(txtSerialNo.Text.Trim(), NumberStyles.Float))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please check the gift voucher !!!')", true);
                            txtSerialNo.Text = string.Empty;
                            txtSerialNo.Focus();
                            return;
                        }

                        DataTable _giftVoucher = CHNLSVC.Inventory.GetDetailByGiftVoucher(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToInt32(txtSerialNo.Text.Trim()), "ITEM");
                        if (_giftVoucher == null || _giftVoucher.Rows.Count <= 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There is no such gift voucher. Please check the gift voucher inventory !!!')", true);
                            txtSerialNo.Text = string.Empty;
                            txtSerialNo.Focus();
                            return;
                        }

                        string _item = _giftVoucher.Rows[0].Field<string>("gvp_gv_cd");
                        LoadItemDetail(_item); txtItem.Text = _item; txtQty.Text = "1";
                    }
                    else
                    {
                        DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                        Int32 _isAvailable = _multiItemforSerial.Rows.Count;

                        if (_isAvailable <= 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected serial number is invalid or not available in your location. Please check your inventory !!!')", true);
                            //ClearItemData();
                            txtSerialNo.Text = string.Empty;
                            return;
                        }

                        string _item = _multiItemforSerial.Rows[0].Field<string>("Item");
                        List<ReptPickSerials> _one = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                        if (!string.IsNullOrEmpty(Convert.ToString(cmbLevel.Text)) && !string.IsNullOrEmpty(Convert.ToString(cmbBook.Text)))
                        {
                            bool _isAgeLevel = false;
                            int _noofday = 0;
                            CheckNValidateAgeItem(_item, string.Empty, cmbBook.Text, cmbLevel.Text, cmbStatus.Text, out _isAgeLevel, out _noofday);
                            if (_isAgeLevel)
                                _one = GetAgeItemList(Convert.ToDateTime(txtdate.Text), _isAgeLevel, _noofday, _one);
                            if (_one == null || _one.Count <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This serial can't select under ageing price level. Please check the ageing status with IT dept !!!')", true);
                              
                                txtSerialNo.Text = string.Empty;
                                txtItem.Text = string.Empty;
                                txtSerialNo.Focus();
                                return;
                            }
                        }
                        if (_one != null && _one.Count > 0 && IsPriceLevelAllowDoAnyStatus == false)
                        {
                            _serialstatus = _one[0].Tus_itm_stus;

                            ListItem li = new ListItem();
                            li.Text = _serialstatus;
                            //bool _exist = cmbStatus.Items.Contains(li);
                            bool _exist = false;


                            if (cmbStatus.Items.FindByValue(_serialstatus) != null)
                            {
                                _exist = true;
                            }
                            else
                            {
                                _exist = false;
                            }

                            if (_exist == false)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected serial item inventory status not available in price level status collection. Please contact IT dept !!!')", true);
                                txtSerialNo.Text = string.Empty;
                                txtItem.Text = string.Empty;
                                txtSerialNo.Focus();
                                return;
                            }
                            else
                            {
                                cmbStatus.SelectedValue = _serialstatus;
                                Session["SERIAL_STATUS"] = _serialstatus;
                            }
                        }

                        if (LoadMultiCombinItem(_item) == false)
                        { 
                            LoadItemDetail(_item); txtItem.Text = _item; txtQty.Text = DoFormat(1); _stopit = true; CheckQty(true); 
                        }
                        else
                        {
                            DataTable _invnetoryCombinAnalalize = CHNLSVC.Inventory.GetCompeleteItemfromAssambleItem(_item);
                            dgvItem.DataSource = _invnetoryCombinAnalalize;
                            dgvItem.DataBind();

                            lblcaption.Text = "Select Item Code";
                            mpexcel.Show();
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
        }

        private void CheckSerialAvailabilityMultiple()
        {
            if (_stopit) return;
            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim())) return;

            //if (txtSerialNo.Text.Trim().ToUpper() == "N/A" || txtSerialNo.Text.Trim().ToUpper() == "NA")
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Selected serial number is invalid or not available in your location. Please check your inventory !!!";
            //    ClearItemData();
            //    return;
            //}

            txtItem.Text = string.Empty;
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;

            try
            {
                if (chkPickGV.Checked)
                {
                    if (!IsNumeric(txtSerialNo.Text.Trim(), NumberStyles.Float))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please check the gift voucher !!!')", true);

                        txtSerialNo.Text = string.Empty;
                        txtSerialNo.Focus();
                        return;
                    }

                    DataTable _giftVoucher = CHNLSVC.Inventory.GetDetailByGiftVoucher(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToInt32(txtSerialNo.Text.Trim()), "ITEM");
                    if (_giftVoucher == null || _giftVoucher.Rows.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There is no such gift voucher.Please check the gift voucher inventory !!!')", true);
                        
                        txtSerialNo.Text = string.Empty;
                        txtSerialNo.Focus();
                        return;
                    }

                    string _item = _giftVoucher.Rows[0].Field<string>("gvp_gv_cd");
                    LoadItemDetail(_item); txtItem.Text = _item; txtQty.Text = "1";
                }
                else
                {
                    DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                    Int32 _isAvailable = _multiItemforSerial.Rows.Count;

                    if (_isAvailable <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected serial number is invalid or not available in your location.Please check your inventory !!!')", true);
                        
                        txtSerialNo.Text = string.Empty;
                        //ClearItemData();
                        return;
                    }

                    string _item = _multiItemforSerial.Rows[0].Field<string>("Item");
                    List<ReptPickSerials> _one = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                    if (!string.IsNullOrEmpty(Convert.ToString(cmbLevel.Text)) && !string.IsNullOrEmpty(Convert.ToString(cmbBook.Text)))
                    {
                        bool _isAgeLevel = false;
                        int _noofday = 0;
                        CheckNValidateAgeItem(_item, string.Empty, cmbBook.Text, cmbLevel.Text, cmbStatus.Text, out _isAgeLevel, out _noofday);
                        if (_isAgeLevel)
                            _one = GetAgeItemList(Convert.ToDateTime(txtdate.Text), _isAgeLevel, _noofday, _one);
                        if (_one == null || _one.Count <= 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This serial can't select under ageing price level. Please check the ageing status with IT dept !!!')", true);

                            txtSerialNo.Text = string.Empty;
                            txtItem.Text = string.Empty;
                            txtSerialNo.Focus();
                            return;
                        }
                    }
                    if (_one != null && _one.Count > 0 && IsPriceLevelAllowDoAnyStatus == false)
                    {
                        string _serialstatus = _one[0].Tus_itm_stus;

                        ListItem li = new ListItem();
                        li.Text = _serialstatus;
                        //bool _exist = cmbStatus.Items.Contains(li);
                        bool _exist = false;


                        if (cmbStatus.Items.FindByValue(_serialstatus) != null)
                        {
                            _exist = true;
                        }
                        else
                        {
                            _exist = false;
                        }

                        if (_exist == false)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected serial item inventory status not available in price level status collection. Please contact IT dept !!!')", true);

                            txtSerialNo.Text = string.Empty;
                            txtItem.Text = string.Empty;
                            txtSerialNo.Focus();
                            return;
                        }
                        else
                        {
                            cmbStatus.SelectedValue = _serialstatus;
                            Session["SERIAL_STATUS"] = _serialstatus;
                        }
                    }

                    if (LoadMultiCombinItem(_item) == true)
                    { 
                        LoadItemDetail(_item); txtItem.Text = _item; txtQty.Text = DoFormat(1);
                        _stopit = true; CheckQty(true); 
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
        private bool BindItemComponent(string _item)
        {
            _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(_item);
            if (_masterItemComponent != null)
            {
                if (_masterItemComponent.Count > 0)
                {
                    _masterItemComponent.ForEach(X => X.Micp_must_scan = false);
                    if (_masterItemComponent != null)
                    {
                        if (_masterItemComponent.Count > 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else return false;
            }
            else return false;
        }

        private bool CheckInventoryCombine()
        {
            bool _IsTerminate = false;
            _isCompleteCode = false;

            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                MasterItem _itemDet = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
                    _isCompleteCode = BindItemComponent(txtItem.Text.Trim());

                if (_isCompleteCode)
                {
                    if (_masterItemComponent != null)
                    {
                        if (_masterItemComponent.Count > 0)
                        {
                            _isInventoryCombineAdded = false;
                            _isCompleteCode = true;
                            _IsTerminate = false;
                           // return _IsTerminate;
                        }
                        else
                        {
                            _isCompleteCode = false;
                           // _IsTerminate = true;
                        }
                    }
                    else
                    {
                        _isCompleteCode = false;
                        _IsTerminate = true;
                    }
                }
            }
            else
            {
                _isCompleteCode = false;
                _IsTerminate = true;
            }

            return _IsTerminate;
        }

        public static decimal RoundUpForPlace(decimal input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * Convert.ToDecimal(multiplier)) / Convert.ToDecimal(multiplier);
        }
        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            //if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
           // else return Math.Round(value, 2);
            return value;
        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    if (Convert.ToDateTime(txtdate.Text) == _serverDt)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT");
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            if (lblVatExemptStatus.Text != "Available")
                            {
                                if (_isTaxfaction == false)
                                    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                else
                                    if (_isVATInvoice)
                                    {
                                        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                    }
                                    else
                                        _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                            }
                            else
                            {
                                if (_isTaxfaction) _pbUnitPrice = 0;
                            }
                        }
                    }
                    else
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTaxEffDt(Session["UserCompanyCode"].ToString(), _item, _status,Convert.ToDateTime(txtdate.Text)); else _taxs = CHNLSVC.Sales.GetItemTaxEffDt(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", Convert.ToDateTime(txtdate.Text));
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        if (_taxs.Count > 0)
                        {
                            foreach (MasterItemTax _one in _Tax)
                            {
                                if (lblVatExemptStatus.Text != "Available")
                                {
                                    if (_isTaxfaction == false)
                                        _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                }
                                else
                                {
                                    if (_isTaxfaction) _pbUnitPrice = 0;
                                }
                            }
                        }
                        else
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false) _taxsEffDt = CHNLSVC.Sales.GetTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, Convert.ToDateTime(txtdate.Text)); else _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", Convert.ToDateTime(txtdate.Text));
                            var _TaxEffDt = from _itm in _taxsEffDt
                                            select _itm;
                            foreach (LogMasterItemTax _one in _TaxEffDt)
                            {
                                if (lblVatExemptStatus.Text != "Available")
                                {
                                    if (_isTaxfaction == false)
                                        _pbUnitPrice = _pbUnitPrice * _one.Lict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Lict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _pbUnitPrice = (_pbUnitPrice * _one.Lict_tax_rate / 100) * _qty;
                                }
                                else
                                {
                                    if (_isTaxfaction) _pbUnitPrice = 0;
                                }
                            }
                        }
                    }
                }
                else
                    if (_isTaxfaction) _pbUnitPrice = 0;
            return _pbUnitPrice;
        }

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = (Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));
                txtUnitAmt.Text = DoFormat(Convert.ToDecimal(txtUnitAmt.Text));

                decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
                txtTaxAmt.Text = DoFormat(_vatPortion);

                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    if (_isVATInvoice)
                        _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                    else
                    {
                        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                        if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        {
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
                                txtTaxAmt.Text = DoFormat(Convert.ToDecimal(txtTaxAmt.Text));
                            }
                        }
                    }

                    txtDisAmt.Text = DoFormat(_disAmt);
                }

                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                }

                txtLineTotAmt.Text = DoFormat(_totalAmount);
            }
        }


        private bool CheckQtyPriliminaryRequirements()
        {
            bool _IsTerminate = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                _IsTerminate = true; return _IsTerminate;
            }
            if (!IsNumeric(txtQty.Text.Trim(), NumberStyles.Float))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid qty !!!')", true);
                
                _IsTerminate = true;
                txtQty.Focus();
                return _IsTerminate; ;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) { _IsTerminate = true; return _IsTerminate; };

            if (_itemdetail.Mi_is_ser1 == 1 && !string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                txtQty.Text = DoFormat(1);
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            if (string.IsNullOrEmpty(txtQty.Text)) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }
            if (Convert.ToDecimal(txtQty.Text) <= 0) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }
            if (string.IsNullOrEmpty(cmbInvType.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select order type !!!')", true);
                
                _IsTerminate = true;
                cmbInvType.Focus();
                return _IsTerminate;
            }
            
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the item !!!')", true);
                
                _IsTerminate = true;
                txtItem.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbBook.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Price book not select !!!')", true);
             
                _IsTerminate = true;
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbLevel.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the price level !!!')", true);
                
                _IsTerminate = true;
                cmbLevel.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbStatus.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the item status !!!')", true);
              
                _IsTerminate = true;
                cmbStatus.Focus();
                return _IsTerminate;
            }
            return _IsTerminate;
        }

        private bool CheckTaxAvailability()
        {
            bool _IsTerminate = false;
            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
            if (!_isCompleteCode)
            {
                if (Convert.ToDateTime(txtdate.Text) == _serverDt)
                {
                    List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty);

                        if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                            _IsTerminate = true;
                        if (_tax.Count <= 0)
                            _IsTerminate = true;
                }
                else
                {
                    List<MasterItemTax> _taxEff = CHNLSVC.Sales.GetItemTaxEffDt(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, Convert.ToDateTime(txtdate.Text));
                    if (_taxEff.Count <= 0)
                    {
                        List<LogMasterItemTax> _tax = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, Convert.ToDateTime(txtdate.Text));
                        if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                            _IsTerminate = true;
                        if (_tax.Count <= 0)
                            _IsTerminate = true;
                    }
                }
            }
            return _IsTerminate;
        }

        private void CheckItemTax(string _item)
        {
            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef != null)
            {
                if (_priceBookLevelRef.Sapl_vat_calc == true)
                {
                    MainTaxConstant = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, cmbStatus.Text.Trim());
                }
            }
        }

        private void SetDecimalTextBoxForZero(bool _isUnit, bool _isAccBal, bool _isQty)
        {
            txtDisRate.Text = DoFormat(0);
            txtDisAmt.Text = DoFormat(0);
            if (_isQty)
                txtQty.Text = DoFormat(1);
            txtTaxAmt.Text = DoFormat(0);
            if (_isUnit) txtUnitPrice.Text = DoFormat(0);
            txtUnitAmt.Text = DoFormat(0);
            txtLineTotAmt.Text = DoFormat(0);
            if (_isAccBal)
            {
                lblAccountBalance.Text = DoFormat(0);
                lblAvailableCredit.Text = DoFormat(0);
            }
        }

        private bool CheckProfitCenterAllowForWithoutPrice()
        {
            bool _isAvailable = false;
            if ((_MasterProfitCenter !=null) && (_priceBookLevelRef!=null))
            {
                if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
                {
                    SetDecimalTextBoxForZero(false, false, false);
                    _isAvailable = true;
                    return _isAvailable;
                }
            }
            return _isAvailable;
        }

        private decimal CheckSubItemTax(string _item)
        {
            LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            decimal _fraction = 1;
            List<MasterItemTax> TaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                TaxConstant = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, cmbStatus.Text.Trim());
                if (TaxConstant != null)
                    if (TaxConstant.Count > 0)
                        _fraction = TaxConstant[0].Mict_tax_rate;
            }
            return _fraction;
        }

        protected void BindConsumableItem(List<InventoryBatchRefN> _consumerpricelist)
        {
            _consumerpricelist.ForEach(x => x.Inb_unit_cost = x.Inb_unit_price * CheckSubItemTax(x.Inb_itm_cd));
        }

        private bool ConsumerItemProduct()
        {
            bool _isAvailable = false;
            bool _isMRP = _itemdetail.Mi_anal3;
            if (1 == 1)
            {
                List<InventoryBatchRefN> _batchRef = new List<InventoryBatchRefN>();
                if (_priceBookLevelRef != null)
                {
                    if (_priceBookLevelRef.Sapl_chk_st_tp) _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim()); else _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), string.Empty);
                }
                    if (_batchRef.Count > 0)
                    if (_batchRef.Count > 1)
                    {
                        BindConsumableItem(_batchRef);
                    }
                    else if (_batchRef.Count == 1)
                    {
                        if (_batchRef[0].Inb_free_qty < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            string msg = "Sales order qty is " + txtQty.Text + " and inventory available qty having only " + _batchRef[0].Inb_free_qty.ToString();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg +" !!!')", true);
                            
                            _isAvailable = true;
                            return _isAvailable;
                        }
                        txtUnitPrice.Text = (Convert.ToString(Convert.ToDecimal(_batchRef[0].Inb_unit_price * CheckSubItemTax(_batchRef[0].Inb_itm_cd))));
                        txtUnitPrice.Text = DoFormat(Convert.ToDecimal(txtUnitPrice.Text));
                        txtUnitPrice.Focus();
                        _isAvailable = false;
                    }
                _isEditPrice = false;
                _isEditDiscount = false;
                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = "0";
                decimal val = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = (Convert.ToString(val));
                CalculateItem();
                _isAvailable = true;
            }
            return _isAvailable;
        }

        private bool CheckItemPromotion()
        {
            //_isNewPromotionProcess = false;
            //if (string.IsNullOrEmpty(txtItem.Text))
            //{ using (new CenterWinDialog(this)) { MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } return false; }
            //_PriceDetailRefPromo = null;
            //_PriceSerialRefPromo = null;
            //_PriceSerialRefNormal = null;
            //CHNLSVC.Sales.GetPromotion(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtItem.Text.Trim(), txtDate.Value.Date, txtCustomer.Text.Trim(), out _PriceDetailRefPromo, out _PriceSerialRefPromo, out _PriceSerialRefNormal);
            //if (_PriceDetailRefPromo == null && _PriceSerialRefPromo == null && _PriceSerialRefNormal == null) return false;
            //if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            //{
            //    var _isSerialAvailable = _PriceSerialRefNormal.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
            //    if (_isSerialAvailable != null && _isSerialAvailable.Count > 0)
            //    {
            //        DialogResult _normalSerialized = new DialogResult();
            //        using (new CenterWinDialog(this)) { _normalSerialized = MessageBox.Show("This item is having normal serialized price.\nDo you need to select normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
            //        if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            _isNewPromotionProcess = true;
            //            CheckSerializedPriceLevelAndLoadSerials(true);
            //            return true;
            //        }
            //    }
            //    else
            //    {
            //        _isNewPromotionProcess = false;
            //        _PriceSerialRefNormal = null;
            //    }
            //}
            //else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && string.IsNullOrEmpty(txtSerialNo.Text))
            //{
            //    DialogResult _normalSerialized = new System.Windows.Forms.DialogResult();
            //    using (new CenterWinDialog(this)) { _normalSerialized = MessageBox.Show("This item having normal serialized price. Do you need to continue with normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
            //    if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        _isNewPromotionProcess = true;
            //        CheckSerializedPriceLevelAndLoadSerials(true);
            //        return true;
            //    }
            //    else
            //    {
            //        _isNewPromotionProcess = false;
            //        _PriceSerialRefNormal = null;
            //    }
            //}
            //if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            //{
            //    var _isSerialPromoAvailable = _PriceSerialRefPromo.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
            //    if (_isSerialPromoAvailable != null && _isSerialPromoAvailable.Count > 0)
            //    {
            //        DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
            //        using (new CenterWinDialog(this)) { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.\nDo you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
            //        if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            _isNewPromotionProcess = true;
            //            CheckSerializedPriceLevelAndLoadSerials(true);
            //            return true;
            //        }
            //        else
            //        {
            //            _isNewPromotionProcess = false;
            //            _PriceSerialRefPromo = null;
            //        }
            //    }
            //    else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && string.IsNullOrEmpty(txtSerialNo.Text))
            //    {
            //        DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
            //        using (new CenterWinDialog(this)) { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.\nDo you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
            //        if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            _isNewPromotionProcess = true;
            //            CheckSerializedPriceLevelAndLoadSerials(true);
            //            return true;
            //        }
            //        else
            //        {
            //            _isNewPromotionProcess = false;
            //            _PriceSerialRefPromo = null;
            //        }
            //    }
            //}
            //if (_PriceDetailRefPromo != null && _PriceDetailRefPromo.Count > 0)
            //{
            //    DialogResult _promo = new System.Windows.Forms.DialogResult();
            //    using (new CenterWinDialog(this)) { _promo = MessageBox.Show("This item is having promotions. Do you need to continue with the available promotions?", "Promotion", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
            //    if (_promo == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        SetColumnForPriceDetailNPromotion(false);
            //        gvNormalPrice.DataSource = new List<PriceDetailRef>();
            //        gvPromotionPrice.DataSource = new List<PriceDetailRef>();
            //        gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
            //        gvPromotionSerial.DataSource = new List<ReptPickSerials>();
            //        BindNonSerializedPrice(_PriceDetailRefPromo);
            //        pnlPriceNPromotion.Visible = true;
            //        pnlMain.Enabled = false;
            //        _isNewPromotionProcess = true;
            //        return true;
            //    }
            //    else
            //    {
            //        _isNewPromotionProcess = false;
            //        return false;
            //    }
            //}
            return false;
        }

        protected void BindSerializedPrice(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = CheckSubItemTax(x.Sars_itm_cd) * x.Sars_itm_price);
            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();
        }

        protected void BindPriceAndPromotion(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = x.Sars_itm_price * CheckSubItemTax(x.Sars_itm_cd));
            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();
        }

        private void DisplayAvailableQty(string _item, Label _withStatus, Label _withoutStatus, string _status)
        {
            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item.Trim(), string.Empty);
            if (_inventoryLocation != null)
                if (_inventoryLocation.Count > 0)
                {
                    var _woStatus = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                    var _wStatus = _inventoryLocation.Where(x => x.Inl_itm_stus == _status).Select(x => x.Inl_free_qty).Sum();
                    _withStatus.Text = FormatToQty(Convert.ToString(_wStatus));
                    _withoutStatus.Text = FormatToQty(Convert.ToString(_woStatus));
                }
                else { _withStatus.Text = FormatToQty("0"); _withoutStatus.Text = FormatToQty("0"); }
            else { _withoutStatus.Text = FormatToQty("0"); _withStatus.Text = FormatToQty("0"); }
        }

        private bool CheckSerializedPriceLevelAndLoadSerials(bool _isSerialized)
        {
            bool _isAvailable = false;
            if (_isSerialized)
            {
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are selected a serialized price level, hence you have not select the serial no. Please select the serial no !!!')", true);
   
                    _isAvailable = true;
                    return _isAvailable;
                }
                List<PriceSerialRef> _list = null;
                if (_isNewPromotionProcess == false)
                    _list = CHNLSVC.Sales.GetAllPriceSerialFromSerial(cmbBook.Text, cmbLevel.Text, txtItem.Text, Convert.ToDateTime(txtdate.Text), txtCustomer.Text, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtSerialNo.Text.Trim());
                else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0)
                    _list = _PriceSerialRefNormal;
                else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0)
                    _list = _PriceSerialRefPromo;
                _tempPriceSerial = new List<PriceSerialRef>();
                _tempPriceSerial = _list;
                if (_list != null)
                {
                    if (_list.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There are no serials available for the selected item !!!')", true);
                       
                        txtQty.Text = "0";
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }
                    var _oneSerial = _list.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                    _list = _oneSerial;
                    if (_list.Count < Convert.ToDecimal(txtQty.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected qty is exceeds available serials at the price definition !!!')", true);
                      
                       
                        txtQty.Text = "0";
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }
                    if (_list.Count == 1)
                    {
                        string _book = _list[0].Sars_pbook;
                        string _level = _list[0].Sars_price_lvl;
                        cmbBook.Text = _book;
                        cmbLevel.Text = _level;
                        if (!_isSerialized)
                            cmbLevel_SelectedIndexChanged(null, null);

                        int _priceType = 0;
                        _priceType = _list[0].Sars_price_type;
                        PriceTypeRef _promotion = TakePromotion(_priceType);
                        decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _list[0].Sars_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false);

                        txtUnitPrice.Text = DoFormat(UnitPrice);
                        WarrantyRemarks = _list[0].Sars_warr_remarks;
                        SetSSPriceDetailVariable(_list[0].Sars_circular_no, "0", Convert.ToString(_list[0].Sars_pb_seq), Convert.ToString(_list[0].Sars_itm_price), _list[0].Sars_promo_cd, Convert.ToString(_list[0].Sars_price_type));

                        Int32 _pbSq = _list[0].Sars_pb_seq;
                        string _mItem = _list[0].Sars_itm_cd;
                        _isAvailable = true;
                        SetColumnForPriceDetailNPromotion(true);
                        BindSerializedPrice(_list);

                        //if (gvPromotionPrice.RowCount > 0)
                        //{
                        //    gvPromotionPrice_CellDoubleClick(0, false, _isSerialized);
                        //    pnlPriceNPromotion.Visible = true;
                        //    pnlMain.Enabled = false;
                        //    return _isAvailable;
                        //}
                        //else
                            if (_isCombineAdding == false) txtUnitPrice.Focus();
                       
                        return _isAvailable;
                    }
                    if (_list.Count > 1)
                    {
                        SetColumnForPriceDetailNPromotion(true);
                        BindPriceAndPromotion(_list);
                        //DisplayAvailableQty(txtItem.Text, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, cmbStatus.Text);
                       
                        _isAvailable = true;
                        return _isAvailable;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There are no serials available for the selected item !!!')", true);
                   
                   
                    txtQty.Text = "0";
                    _isAvailable = true;
                    txtQty.Focus();
                    return _isAvailable;
                }
            }
            return _isAvailable;
        }

        private void SetSSPriceDetailVariable(string _circuler, string _pblineseq, string _pbseqno, string _pbprice, string _promotioncd, string _promotiontype)
        {
            SSCirculerCode = _circuler;
            SSPriceBookItemSequance = _pblineseq;
            SSPriceBookPrice = Convert.ToDecimal(_pbprice);
            SSPriceBookSequance = _pbseqno;
            SSPromotionCode = _promotioncd;
            if (string.IsNullOrEmpty(_promotioncd) || _promotioncd.Trim().ToUpper() == "N/A") SSPromotionCode = string.Empty;
            SSPRomotionType = Convert.ToInt32(_promotiontype);
        }

        protected PriceTypeRef TakePromotion(Int32 _priceType)
        {
            List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            var _ptype = from _types in _type
                         where _types.Sarpt_indi == _priceType
                         select _types;
            PriceTypeRef _list = new PriceTypeRef();
            foreach (PriceTypeRef _ones in _ptype)
            {
                _list = _ones;
            }
            return _list;
        }

        private bool IsGiftVoucher(string _type)
        {
            if (_type == "G")
                return true;
            else
                return false;
        }

        protected void BindNonSerializedPrice(List<PriceDetailRef> _list)
        {
            foreach (var myitem in _list)
            {
                myitem.Sapd_cre_by = Convert.ToString(myitem.Sapd_itm_price);
                myitem.Sapd_itm_price =  CheckSubItemTax(myitem.Sapd_itm_cd) * myitem.Sapd_itm_price;
            }

            var _normal = _list.Where(x => x.Sapd_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sapd_price_type != 0).ToList();
        }

        private bool IsVirtual(string _type)
        { if (_type == "V") { _IsVirtualItem = true; return true; } else { _IsVirtualItem = false; return false; } }


        private decimal CalculateItemTem(decimal _qty, decimal _unitPrice, decimal _disAmount, decimal _disRt)
        {
            string unitAmt = (Convert.ToString(FigureRoundUp(Convert.ToDecimal(_unitPrice) * Convert.ToDecimal(_qty), true)));

            decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(_qty), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), Convert.ToDecimal(_disAmount), Convert.ToDecimal(_disRt), true), true);
            string tax = (Convert.ToString(_vatPortion));

            decimal _totalAmount = Convert.ToDecimal(_qty) * Convert.ToDecimal(_unitPrice);
            decimal _disAmt = 0;

            if (_disRt != 0)
            {
                bool _isVATInvoice = false;
                if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                else _isVATInvoice = false;

                if (_isVATInvoice)
                    _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(_disRt) / 100), true);
                else
                {
                    _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(_disRt) / 100), true);
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    {
                        List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                        if (_tax != null && _tax.Count > 0)
                        {
                            decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            tax = Convert.ToString(FigureRoundUp(_vatval, true));
                        }
                    }
                }

                Convert.ToString(_disAmt);
            }

            if (!string.IsNullOrEmpty(tax))
            {
                if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                else
                    _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(tax) - _disAmt, true);
            }

            return _totalAmount;
        }


        protected bool CheckQty(bool _isSearchPromotion)
        {
            txtDisRate.Text = DoFormat(0);
            txtDisAmt.Text = DoFormat(0);
            WarrantyPeriod = 0;
            WarrantyRemarks = string.Empty;
            bool _IsTerminate = false;
            Dictionary<decimal,decimal> ManagerDiscount = new Dictionary<decimal, decimal>();
            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;
            //if (_isCompleteCode == false)
                if (CheckInventoryCombine())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This compete code does not having a collection. Please contact inventory !!!')", true);

                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (CheckQtyPriliminaryRequirements()) return true;

            if (_isCombineAdding == false)
                if (CheckTaxAvailability())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Tax rates not setup for selected item code and item status.Please contact Inventory Department !!!')", true);
                 
                    _IsTerminate = true;
                    return _IsTerminate;
                }

            if (_isCombineAdding == false) CheckItemTax(txtItem.Text.Trim());
            if (_isCombineAdding == false)
                if (CheckProfitCenterAllowForWithoutPrice())
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (_isCombineAdding == false)
                if (ConsumerItemProduct())
                {
                    _IsTerminate = true;
                    //return _IsTerminate;
                }
            if (_isSearchPromotion) if (CheckItemPromotion()) { _IsTerminate = true; return _IsTerminate; }

            if (_priceBookLevelRef !=null)
            {
                if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized)
                    if (CheckSerializedPriceLevelAndLoadSerials(true))
                    {
                        _IsTerminate = true;
                        //return _IsTerminate;
                    }
            }
            if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) return true;

            

            if (IsVirtual(_itemdetail.Mi_itm_tp) && _isCompleteCode == false)
            {
                //txtUnitPrice.ReadOnly = false;
                //txtDisRate.ReadOnly = false;
                //txtDisAmt.ReadOnly = false;
                //txtUnitAmt.ReadOnly = true;
                //txtTaxAmt.ReadOnly = true;
                //txtLineTotAmt.ReadOnly = true;
                return true;
            }
            else
            {
                //txtUnitPrice.ReadOnly = true;
                //txtUnitAmt.ReadOnly = true;
                //txtTaxAmt.ReadOnly = true;
                //txtLineTotAmt.ReadOnly = true;
                if (_itemdetail.Mi_itm_tp == "V")
                {
                    txtDisRate.ReadOnly = true;
                    txtDisAmt.ReadOnly = true;
                }
                else
                {
                    txtDisRate.ReadOnly = false;
                    txtDisAmt.ReadOnly = false;
                }
            }
            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtdate.Text));
            if (_priceDetailRef.Count <= 0)
            {
                if (!_isCompleteCode)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There is no price for the selected item !!!')", true);
                    
                    SetDecimalTextBoxForZero(true, false, true);
                    _IsTerminate = true;
                    return _IsTerminate;
                }
                else
                {
                    txtUnitPrice.Text = DoFormat(0);
                }
            }
            else
            {
                if (_isCompleteCode)
                {
                    List<PriceDetailRef> _new = _priceDetailRef;
                    _priceDetailRef = new List<PriceDetailRef>();
                    var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                    if (_p != null)
                        if (_p.Count > 0)
                        {
                            if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                            _priceDetailRef.Add(_p[0]);
                        }
                }
                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                    if (_isSuspend > 0)
                    {
                       // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Price has been suspended. Please contact IT dept !!!')", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Price has been suspended.')", true);

                        _IsTerminate = true;
                        return _IsTerminate;
                    }
                }
                if (_priceDetailRef.Count > 1)
                {
                    SetColumnForPriceDetailNPromotion(false);
                    //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    BindNonSerializedPrice(_priceDetailRef);
                    _IsTerminate = true;

                    return _IsTerminate;
                }
                else if (_priceDetailRef.Count == 1)
                {
                    var _one = from _itm in _priceDetailRef
                               select _itm;
                    int _priceType = 0;
                    foreach (var _single in _one)
                    {
                        _priceType = _single.Sapd_price_type;
                        PriceTypeRef _promotion = TakePromotion(_priceType);
                        decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                        txtUnitPrice.Text = DoFormat(UnitPrice);
                        WarrantyRemarks = _single.Sapd_warr_remarks;
                        SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                        Int32 _pbSq = _single.Sapd_pb_seq;
                        Int32 _pbiSq = _single.Sapd_seq_no;
                        string _mItem = _single.Sapd_itm_cd;
                      
                        SetColumnForPriceDetailNPromotion(false);
                        //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                        //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                        //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                        //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                        BindNonSerializedPrice(_priceDetailRef);

                        
                        if (_isCombineAdding == false) txtUnitPrice.Focus();
                    }
                }
            }
            _isEditPrice = false;
            _isEditDiscount = false;
            if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = "0";
            decimal vals = Convert.ToDecimal(txtQty.Text);
            txtQty.Text = (Convert.ToString(vals));
            CalculateItem();

           
            if (_priorityPriceBook != null && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb_lvl)
            {
                decimal normalPrice = Convert.ToDecimal(txtLineTotAmt.Text);

                _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, _priorityPriceBook.Sppb_pb, _priorityPriceBook.Sppb_pb_lvl, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtdate.Text));
                string _unitPrice = "";
                if (_priceDetailRef.Count <= 0)
                {
                    return false;
                }

                if (_priceDetailRef.Count <= 0)
                {
                    if (!_isCompleteCode)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There is no price for the selected item !!!')", true);
                     
                        SetDecimalTextBoxForZero(true, false, true);
                        return false;
                    }
                    else
                    {
                        _unitPrice = "0";
                    }
                }
                else
                {
                    if (_isCompleteCode)
                    {
                        List<PriceDetailRef> _new = _priceDetailRef;
                        _priceDetailRef = new List<PriceDetailRef>();
                        var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                        if (_p != null)
                            if (_p.Count > 0)
                            {
                                if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                                _priceDetailRef.Add(_p[0]);
                            }
                    }
                    if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                    {
                        var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                        if (_isSuspend > 0)
                        {
                            return false;
                        }
                    }
                    if (_priceDetailRef.Count > 1)
                    {
                        return false;
                    }
                    else if (_priceDetailRef.Count == 1)
                    {
                        var _one = from _itm in _priceDetailRef
                                   select _itm;
                        int _priceType = 0;
                        foreach (var _single in _one)
                        {
                            _priceType = _single.Sapd_price_type;
                            PriceTypeRef _promotion = TakePromotion(_priceType);
                            decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                            _unitPrice = Convert.ToString(UnitPrice);
                            WarrantyRemarks = _single.Sapd_warr_remarks;
                            Int32 _pbSq = _single.Sapd_pb_seq;
                            Int32 _pbiSq = _single.Sapd_seq_no;
                            string _mItem = _single.Sapd_itm_cd;
                            SetColumnForPriceDetailNPromotion(false);
                            //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                            //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                            //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                            //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                            BindNonSerializedPrice(_priceDetailRef);
                            if (_isCombineAdding == false) txtUnitPrice.Focus();
                        }
                    }
                }
                _isEditPrice = false;
                _isEditDiscount = false;
                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = "0";
                decimal vals1 = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = (Convert.ToString(vals1));
                decimal otherPrice = 0;
                if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(_unitPrice))
                {
                    decimal _disRate = 0;
                    decimal _disAmt = 0;
                    if (!string.IsNullOrEmpty(txtDisRate.Text))
                    {
                        _disRate = Convert.ToDecimal(txtDisRate.Text);
                    }
                    if (!string.IsNullOrEmpty(txtDisAmt.Text))
                    {
                        _disAmt = Convert.ToDecimal(txtDisAmt.Text);
                    }

                    otherPrice = CalculateItemTem(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(_unitPrice), _disAmt, _disRate);
                }
                else
                    return false;
                if (otherPrice < normalPrice)
                {
                   // using (new CenterWinDialog(this)) { _result = MessageBox.Show(_priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Price - " + (otherPrice.ToString()) + "\nDo you want to select " + _priorityPriceBook.Sppb_pb + " Price?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }

                    //if (_result == DialogResult.Yes)
                    //{
                    //    txtUnitPrice.Text = ("0");
                    //    txtUnitAmt.Text = ("0");
                    //    txtDisRate.Text = ("0");
                    //    txtDisAmt.Text = ("0");
                    //    txtTaxAmt.Text = ("0");
                    //    txtLineTotAmt.Text = ("0");
                    //    cmbBook.Text = _priorityPriceBook.Sppb_pb;
                    //    cmbLevel.Text = _priorityPriceBook.Sppb_pb_lvl;
                    //    CheckQty(false);
                    //}
                    //else
                    //{
                    //    SSPRomotionType = 0;
                    //    SSPromotionCode = string.Empty;
                    //}
                }
            }

            return _IsTerminate;
        }

        private bool LoadMultiCombineItem(string _item)
        {
            bool _isMultiCom = false;
            DataTable _invnetoryCombinAnalalize = CHNLSVC.Inventory.GetCompeleteItemfromAssambleItem(_item);
            if (_invnetoryCombinAnalalize != null)
                if (_invnetoryCombinAnalalize.Rows.Count > 0)
                {
                    //gvMultiCombineItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //gvMultiCombineItem.DataSource = _invnetoryCombinAnalalize;
                    _isMultiCom = true;
                    //pnlMain.Enabled = false;
                    //pnlMultiCombine.Visible = true;
                    //gvMultiCombineItem.Focus();
                }
            return _isMultiCom;
        }
        private bool LoadMultiCombinItem(string _item)
        {
            bool _isManyItem = false;
            if (LoadMultiCombineItem(_item))
            {
                _isManyItem = true;
            }
            return _isManyItem;
        }

        private List<ReptPickSerials> GetAgeItemList(DateTime _date, bool _isAgePriceLevel, int _noOfDays, List<ReptPickSerials> _referance)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();
            if (_isAgePriceLevel)
                _ageLst = _referance.Where(x => x.Tus_exist_grndt <= _date.AddDays(-_noOfDays)).ToList();
            else
                _ageLst = _referance;
            return _ageLst;
        }

        private void CheckNValidateAgeItem(string _itemc, string _itemcategory, string _bookc, string _levelc, string _status, out bool IsAgePriceLevel, out int AgeDays)
        {
            bool _isAgePriceLevel = false;
            int _ageingDays = -1;
            MasterItem _item = null;
            if (string.IsNullOrEmpty(_itemcategory))
            { _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemc); if (_item != null) _itemcategory = _item.Mi_cate_1; }
            List<PriceBookLevelRef> _level = _priceBookLevelRefList;
            if (_level != null)
                if (_level.Count > 0)
                {
                    var _lvl = _level.Where(x => x.Sapl_isage && x.Sapl_itm_stuts == _status).ToList();
                    if (_lvl != null) if (_lvl.Count() > 0)
                            _isAgePriceLevel = true;
                }
            if (_isAgePriceLevel)
            {
                DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_itemcategory);
                if (_categoryDet != null && _categoryDet.Rows.Count > 0)
                {
                    if (_categoryDet.Rows[0]["ric1_age"] != DBNull.Value)
                        _ageingDays = Convert.ToInt32(_categoryDet.Rows[0].Field<Int16>("ric1_age"));
                    else _ageingDays = 0;
                }
            }

            IsAgePriceLevel = _isAgePriceLevel;
            AgeDays = _ageingDays;
        }

        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            lblItemSerialStatus.Text = string.Empty;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                _isValid = true;
                string _description = _itemdetail.Mi_longdesc;
                string _model = _itemdetail.Mi_model;
                string _brand = _itemdetail.Mi_brand;
                string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                lblItemDescription.Text = _description;
                lblItemModel.Text = _model;
                lblItemBrand.Text = _brand;
                lblItemSerialStatus.Text =  _serialstatus;
            }
            else _isValid = false;
            return _isValid;
        }

        protected void lbtnkititem_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.Sales.SearchSalesRequest(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnupload_Click(object sender, EventArgs e)
        {
            try
            {
                MpDeliveryShow();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            try
            {
                MPPV.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void BindGeneralDiscount()
        {
            MPDis.Show();
            gvDisItem.DataSource = new List<CashGeneralEntiryDiscountDef>();
            gvDisItem.DataBind();

        }
        protected void lbtndiscount_Click(object sender, EventArgs e)
        {
            MPDis.Show();

            txtDisAmount.ReadOnly = false;
            txtDisAmount.Focus();

            ddlDisCategory.Enabled = true;
            try
            {
                BindGeneralDiscount();
                ddlDisCategory.Text = "Customer";

                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the customer !!!')", true);
                    MPDis.Hide();
                    return;
                }

                if (txtCustomer.Text == "CASH")
                {
                    MPDis.Hide();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid customer. Customer should be registered !!!')", true);
                    return;
                }

                //if (_invoiceItemList != null)
                //    if (_invoiceItemList.Count > 0)
                //    {
                //        ddlDisCategory.Enabled = true;
                //    }
                //    else
                //    {
                //        ddlDisCategory.Text = "Customer";
                //        ddlDisCategory.Enabled = false;
                //    }
                //else
                //{
                //    ddlDisCategory.Text = "Customer";
                //    //ddlDisCategory.Enabled = false;
                //}
                DataTable _invoiceItemListTbl = ViewState["ITEMSTABLE"] as DataTable;

                gvDisItem.Columns[6].Visible = false;

                if (_invoiceItemListTbl != null)
                {
                    if (_invoiceItemListTbl.Rows.Count > 0)
                    {
                        _CashGeneralEntiryDiscount = new List<CashGeneralEntiryDiscountDef>();
                        foreach (DataRow _i in _invoiceItemListTbl.Rows)
                        {
                            CashGeneralEntiryDiscountDef _one = new CashGeneralEntiryDiscountDef();

                            var _dup = from _l in _CashGeneralEntiryDiscount
                                       where _l.Sgdd_itm == _i["soi_itm_cd"].ToString() && _l.Sgdd_pb == _i["soi_pbook"].ToString() && _l.Sgdd_pb_lvl == _i["soi_pb_lvl"].ToString()
                                       select _l;

                            if (_dup == null || _dup.Count() <= 0)
                            {
                                _one.Sgdd_itm = _i["soi_itm_cd"].ToString();
                                _one.Sgdd_pb = _i["soi_pbook"].ToString();
                                _one.Sgdd_pb_lvl = _i["soi_pb_lvl"].ToString();

                                _CashGeneralEntiryDiscount.Add(_one);
                            }
                        }
                        gvDisItem.DataSource = _CashGeneralEntiryDiscount;
                        gvDisItem.DataBind();
                    }
                }

                ddlDisCategory_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void ddlDisCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPDis.Show();
            if (ddlDisCategory.SelectedValue.ToString() == "Item")
            {
                //txtDisAmount.ReadOnly = true;
                txtDisAmount.Text = "";
                for (int i = 0; i < gvDisItem.Rows.Count; i++)
                {
                    GridViewRow dr = gvDisItem.Rows[i];
                    CheckBox DisItem_Select = (CheckBox)dr.FindControl("DisItem_Select");
                    TextBox txtDisItem_Amount = (TextBox)dr.FindControl("txtDisItem_Amount");
                    DisItem_Select.Enabled = true;
                    txtDisItem_Amount.Enabled = true;

                }
            }
            else
            {
                txtDisAmount.ReadOnly = false;
                txtDisAmount.Text = "";
                for (int i = 0; i < gvDisItem.Rows.Count; i++)
                {
                    GridViewRow dr = gvDisItem.Rows[i];
                    CheckBox DisItem_Select = (CheckBox)dr.FindControl("DisItem_Select");
                    TextBox txtDisItem_Amount = (TextBox)dr.FindControl("txtDisItem_Amount");
                    DisItem_Select.Enabled = false;
                    txtDisItem_Amount.Enabled = false;
                }
            }
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            try
            {
                MPBB.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + cmbInvType.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + cmbInvType.Text + seperator + cmbBook.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + cmbBook.Text + seperator + cmbLevel.Text + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
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

                case CommonUIDefiniton.SearchUserControlType.QuotationForInvoice:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + txtdate.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "ITEM" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {
                        paramsText.Append(txtCustomer.Text.Trim() + seperator + txtdate.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesOrderNew:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append(Session["_SerialSearchType"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItem.Text.ToUpper() + seperator);
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

        public void BindUCtrlDDLDataCustom(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if (col.ColumnName != "Status")
                {
                    this.ddlSearchbykey.Items.Add(col.ColumnName); 
                }
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "32")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "32";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "13")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "13";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "13a")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "13a";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "33")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "33";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "168")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                    DataTable result = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "168";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "76")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                    DataTable result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "76";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "134")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                    DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "134";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "158")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher);
                    DataTable result = CHNLSVC.CommonSearch.SearchAvailableGiftVoucher(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "158";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "16")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    DataTable result = CHNLSVC.Sales.SearchSalesRequest(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "16";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "14")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                    DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "14";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "81")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "81";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "421")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                    DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "421";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "1")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "1";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "1a")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "1a";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "5")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "5";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;

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
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtncode_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "13";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnSearch_NIC_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "32";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnSearch_Mobile_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "33";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnSearch_Loyalty_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select customer code !!!')", true);

                    lbtncode.Focus();
                    return;
                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable result = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "168";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadExecutive()
        {
            cmbExecutive.DataSource = null;
            DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            if (_tblExecutive != null)
            {
                var _lst = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") != "TECH").ToList();
                cmbExecutive.DataSource = _lst.CopyToDataTable();
                cmbExecutive.DataValueField = "esep_epf"; cmbExecutive.DataTextField = "esep_first_name";
                if (_lst != null && _lst.Count > 0)
                {
                    cmbExecutive.DataBind();
                }
                if (_tblExecutive != null)
                {
                    cmbExecutive.SelectedValue = _MasterProfitCenter.Mpc_man;
                }
                if (_MasterProfitCenter.Mpc_chnl == "ELITE")
                {
                    cmbExecutive.SelectedIndex = 0;
                }

                cmbExecutive.Items.Insert(0, new ListItem("Select", "0"));

                //txtExecutive.Text = _MasterProfitCenter.Mpc_man;
                //AutoCompleteStringCollection _string = new AutoCompleteStringCollection();
                //Parallel.ForEach(_lst, x => _string.Add(x.Field<string>("esep_first_name")));
                //cmbExecutive.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //cmbExecutive.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cmbExecutive.AutoCompleteCustomSource = _string;
                //AutoCompleteStringCollection _string0 = new AutoCompleteStringCollection();
                //var _lst0 = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") == "TECH").ToList();
                //if (_lst0 != null && _lst0.Count > 0) cmbTechnician.DataSource = null;
                //cmbTechnician.Width = 200; if (_lst0 != null && _lst0.Count > 0)
                //{ Parallel.ForEach(_lst0, x => _string0.Add(x.Field<string>("esep_first_name"))); cmbTechnician.AutoCompleteSource = AutoCompleteSource.CustomSource; cmbTechnician.AutoCompleteMode = AutoCompleteMode.SuggestAppend; cmbTechnician.AutoCompleteCustomSource = _string0; }
                //var _manname = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_epf") == _MasterProfitCenter.Mpc_man).ToList();
                //if (_manname != null && _manname.Count > 0) { string _name = _manname[0].Field<string>("esep_first_name") + " (" + _MasterProfitCenter.Mpc_man + ")"; this.Text = "Invoice | Manager : " + _name; }
            }
        }

        private void SetGridViewAutoColumnGenerate()
        {
            gvInvoiceItem.AutoGenerateColumns = false;
            gvPopSerial.AutoGenerateColumns = false;
            gvGiftVoucher.AutoGenerateColumns = false;
            //gvDisItem.AutoGenerateColumns = false; 
            //gvNormalPrice.AutoGenerateColumns = false; 
            //gvPopComItem.AutoGenerateColumns = false; 
            //gvPopComItemSerial.AutoGenerateColumns = false; 
            //gvPopConsumPricePick.AutoGenerateColumns = false; 
            //gvPromotionItem.AutoGenerateColumns = false; 
            //gvPromotionPrice.AutoGenerateColumns = false; 
            //gvPromotionSerial.AutoGenerateColumns = false; 
            //gvRePayment.AutoGenerateColumns = false;
        }

        public void Invoice()
        {
            try
            {
                gvBuyBack.AutoGenerateColumns = false;
                if (string.IsNullOrEmpty(Session["UserDefProf"].ToString()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You do not have assigned a profit center. Order is terminating now !!!')", true);

                    return;
                }
                if (string.IsNullOrEmpty(Session["UserDefLoca"].ToString()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You do not have assigned a delivery location. order is de-activating delivery option now !!!')", true);

                    return;
                }
                else
                {
                    LoadCachedObjects();
                    SetGridViewAutoColumnGenerate();
                    InitializeValuesNDefaultValueSet();
                    TextBox _txt = new TextBox();
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

        private void LoadCachedObjects()
        { _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString()); _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString()); MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString()); IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString()); }

        private bool LoadPriceLevel(string _invoiceType, string _book)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
            {
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                    cmbLevel.DataSource = _levels;
                    cmbLevel.DataBind();
                    cmbLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbBook.Text)) cmbLevel.Text = DefaultLevel;
                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _book.Trim(), cmbLevel.Text.Trim());
                    LoadPriceLevelMessage();
                    cmbLevel.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    cmbLevel.DataSource = null;
                }
            }
            else
            {
                cmbLevel.DataSource = null;
            }
            return _isAvailable;
        }

        private void LoadPriceDefaultValue()
        {
            try
            {
                if (_PriceDefinitionRef != null)
                    if (_PriceDefinitionRef.Count > 0)
                    {
                        var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();
                        if (_defaultValue != null)
                            if (_defaultValue.Count > 0) { DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp; DefaultBook = _defaultValue[0].Sadd_pb; DefaultLevel = _defaultValue[0].Sadd_p_lvl; DefaultStatus = _defaultValue[0].Sadd_def_stus; DefaultItemStatus = _defaultValue[0].Sadd_def_stus; LoadInvoiceType(); LoadPriceBook(cmbInvType.Text); LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim()); LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim()); CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim()); }
                    } cmbTitle.SelectedIndex = 0;
            }
            catch
            {

            }
        }

        private void CheckPriceLevelStatusForDoAllow(string _level, string _book)
        {
            if (!string.IsNullOrEmpty(_level.Trim()) && !string.IsNullOrEmpty(_book.Trim()))
            {
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _bool = (from _l in _lvl where _l.Sapl_chk_st_tp == true select _l.Sapl_chk_st_tp).ToList();
                        if (_bool != null && _bool.Count() > 0) IsPriceLevelAllowDoAnyStatus = false; else IsPriceLevelAllowDoAnyStatus = true;
                    }
            }
            else
                IsPriceLevelAllowDoAnyStatus = true;
        }

        private bool LoadLevelStatus(string _invType, string _book, string _level)
        {
            _levelStatus = null;
            bool _isAvailable = false;
            string _initPara = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus);
            _levelStatus = CHNLSVC.CommonSearch.GetPriceLevelItemStatusData(_initPara, null, null);
            if (_levelStatus != null)
            {
                if (_levelStatus.Rows.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();

                    string serialstus = (string)Session["SERIAL_STATUS"];

                    if (Session["SERIAL_STATUS"] != null)
                    {
                        _serialstatus = serialstus;
                    }

                    cmbStatus.DataTextField = "Description";
                    cmbStatus.DataValueField = "Code";
                    cmbStatus.DataSource = _levelStatus;
                    cmbStatus.DataBind();

                    if (!string.IsNullOrEmpty(_serialstatus))
                    {
                        cmbStatus.SelectedValue = _serialstatus; 
                    }
                   
                    if (!string.IsNullOrEmpty(DefaultInvoiceType))
                        cmbStatus.SelectedValue = DefaultStatus;
                    _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
                    LoadPriceLevelMessage();

                    string word = "Select";

                    if (cmbStatus.Items.FindByText(word.ToUpper()) == null)
                    {
                        cmbStatus.Items.Insert(0, new ListItem("Select", "0"));
                    }

                    


                    //_isAvailable = true;
                    //var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
                    //cmbStatus.DataSource = _types;
                    //cmbStatus.DataBind();

                    //string serialstus = (string)Session["SERIAL_STATUS"];

                    //cmbStatus.SelectedValue = serialstus;
                    //if (!string.IsNullOrEmpty(DefaultInvoiceType)) cmbStatus.Text = DefaultStatus;
                    //_priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
                    //LoadPriceLevelMessage();
                    //cmbStatus.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    cmbStatus.DataSource = null;
                }
            }
            else
            {
                cmbStatus.DataSource = null;
            }
            return _isAvailable;
        }

        protected void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
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
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnSearch_Serial_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                    Session["_SerialSearchType"] = "SER1_WOITEM";
                else
                    Session["_SerialSearchType"] = "SER1_WITEM";

                if (!chkPickGV.Checked)
                {
                    if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                    {
                        ViewState["SEARCH"] = null;
                        txtSearchbyword.Text = string.Empty;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                        DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, null, null);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        lblvalue.Text = "134";
                        BindUCtrlDDLData(result);
                        ViewState["SEARCH"] = result;
                        SIPopup.Show();
                        txtSearchbyword.Focus();

                    }
                    else
                    {
                        ViewState["SEARCH"] = null;
                        txtSearchbyword.Text = string.Empty;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                        DataTable result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(SearchParams, null, null);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        lblvalue.Text = "76";
                        BindUCtrlDDLData(result);
                        ViewState["SEARCH"] = result;
                        SIPopup.Show();
                        txtSearchbyword.Focus();
                    }
                }
                else
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher);
                    DataTable result = CHNLSVC.CommonSearch.SearchAvailableGiftVoucher(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "158";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void BackDatePermission()
        {
            try
            {
                _isBackDate = false;
                bool _allowCurrentTrans = false;
                //IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty, Session["UserDefProf"].ToString(), "m_Trans_Sales_SalesOrder", txtdate,System.Web.UI.ImageClickEventArgs, string.Empty, out _allowCurrentTrans);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void Invoice_Load()
        {
            try
            {
                BackDatePermission();
                List<PriceDefinitionRef> tem = (from _res in _PriceDefinitionRef
                                                where _res.Sadd_def_pb
                                                select _res).ToList<PriceDefinitionRef>();
                if (tem != null && tem.Count > 0)
                {
                    _priorityPriceBook = new PriortyPriceBook();
                    _priorityPriceBook.Sppb_pb = tem[0].Sadd_pb;
                    _priorityPriceBook.Sppb_pb_lvl = tem[0].Sadd_p_lvl;
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

        private bool LoadInvoiceType()
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _PriceDefinitionRef.Where(X => !X.Sadd_doc_tp.Contains("HS")).Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    if (_types.Count > 0)
                    {
                        if (cmbInvType.Items.Count == 0)
                        {
                            var distinctNames = (from d in _types select d).Distinct();

                            cmbInvType.DataSource = distinctNames;
                            cmbInvType.DataTextField = "";
                            cmbInvType.DataValueField = "";
                            cmbInvType.DataBind();
                            //cmbInvType.Items.Insert(0, new ListItem("Select", "0"));
                        }
                    }
                   
                    if (!string.IsNullOrEmpty(DefaultInvoiceType))
                    {
                        cmbInvType.Text = DefaultInvoiceType;
                    }
                }
                else
                    cmbInvType.DataSource = null;
            else
                cmbInvType.DataSource = null;

            return _isAvailable;
        }

        protected void lbtncustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_isFromOther"] = "true";
                Session["c"] = "true";
                Session["CUSPOPUP"] = "YES";
                CustomerPopoup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        private void LoadCusData()
        {
            try
            {
                DataTable dtcusdata = CHNLSVC.Sales.SearchCustomer(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                cmbTitle.SelectedIndex = 0;
                if (dtcusdata.Rows.Count > 0)
                {
                    foreach (DataRow item in dtcusdata.Rows)
                    {
                        txtNIC.Text = item[0].ToString();
                        txtMobile.Text = item[1].ToString();
                        txtCusName.Text = item[3].ToString();
                        txtAddress1.Text = item[4].ToString();
                        txtAddress2.Text = item[5].ToString();
                        if (!string.IsNullOrEmpty(item[2].ToString()))
                        {
                            cmbTitle.SelectedValue = item[2].ToString(); 
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadCusLoyalityNo()
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable dtloyal = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, null, null);
                if (dtloyal.Rows.Count > 0)
                {
                    foreach (DataRow item in dtloyal.Rows)
                    {
                        //txtLoyalty.Text = item[0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void ClearCustomer(bool _isCustomer)
        {
            try
            {
                if (_isCustomer)
                {
                    txtCustomer.Text = string.Empty;
                }
                txtCusName.Text = string.Empty;
                txtAddress1.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtMobile.Text = string.Empty;
                txtNIC.Text = string.Empty;
                chkTaxPayable.Checked = false;
                //txtLoyalty.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void SetCustomerAndDeliveryDetailsGroup(GroupBussinessEntity _cust)
        {
            try
            {
                txtCustomer.Text = _cust.Mbg_cd;
                txtCusName.Text = _cust.Mbg_name;
                txtAddress1.Text = _cust.Mbg_add1;
                txtAddress2.Text = _cust.Mbg_add2;
                txtMobile.Text = _cust.Mbg_mob;
                txtNIC.Text = _cust.Mbg_nic;

                txtdelad1.Text = _cust.Mbg_add1;
                txtdelad2.Text = _cust.Mbg_add2;
                txtdelcuscode.Text = _cust.Mbg_cd;
                txtdelname.Text = _cust.Mbg_name;

                chkTaxPayable.Checked = false;
                chkTaxPayable.Enabled = false;

                cmbTitle.Text = _cust.Mbg_tit;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            try
            {
                txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                txtCusName.Text = _masterBusinessCompany.Mbe_name;
                txtAddress1.Text = _masterBusinessCompany.Mbe_add1;
                txtAddress2.Text = _masterBusinessCompany.Mbe_add2;
                txtMobile.Text = _masterBusinessCompany.Mbe_mob;
                txtNIC.Text = _masterBusinessCompany.Mbe_nic;

                if (!string.IsNullOrEmpty(_masterBusinessCompany.MBE_TIT))
                {
                    cmbTitle.Text = _masterBusinessCompany.MBE_TIT;  
                }
                

                _entity = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text, string.Empty, string.Empty, "C");
                if (_entity != null)
                    if (_entity.Mbe_cd != null)
                        if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                        { Session["CUSHASCOMPANY"] = true; Session["CUSCOM"] = _entity.Mbe_cust_com; Session["CUSLOC"] = _entity.Mbe_cust_loc; }

                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_town_cd.ToUpper()))
                {
                    ddltown.SelectedItem.Text = _masterBusinessCompany.Mbe_town_cd.ToUpper(); 
                }
                

                if (_isRecall == false)
                {
                    txtdelad1.Text = _masterBusinessCompany.Mbe_add1;

                    txtdelad2.Text = _masterBusinessCompany.Mbe_add2;

                    txtdelcuscode.Text = _masterBusinessCompany.Mbe_cd;

                    txtdelname.Text = _masterBusinessCompany.Mbe_name;
                }
                else
                {
                    txtCusName.Text = _hdr.Sah_cus_name;
                    txtAddress1.Text = _hdr.Sah_cus_add1;
                    txtAddress2.Text = _hdr.Sah_cus_add2;

                    txtdelad1.Text = _hdr.Sah_d_cust_add1;
                    txtdelad2.Text = _hdr.Sah_d_cust_add2;
                    txtdelcuscode.Text = _hdr.Sah_d_cust_cd;
                    txtdelname.Text = _hdr.Sah_d_cust_name;
                    ddltown.SelectedValue = _hdr.Sah_del_loc;
                }

                if (_isRecall == false)
                {
                    if (_masterBusinessCompany.Mbe_is_tax) { chkTaxPayable.Checked = true; chkTaxPayable.Enabled = true; } else { chkTaxPayable.Checked = false; chkTaxPayable.Enabled = false; }
                }

                if (string.IsNullOrEmpty(txtNIC.Text)) { cmbTitle.SelectedIndex = 0; return; }
                if (IsValidNIC(txtNIC.Text) == false) { cmbTitle.SelectedIndex = 0; return; }
                GetNICGender();
                if (string.IsNullOrEmpty(txtCusName.Text)) txtCusName.Text = cmbTitle.Text.Trim();
                else
                {
                    string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                    ListItem li = new ListItem();
                    li.Text = _title;
                    bool _exist = cmbTitle.Items.Contains(li);
                    if (_exist)
                        cmbTitle.Text = _title;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public string ExtractTitleByCustomerName(string _customerName)
        {
            string[] _splits = _customerName.Split('.');
            StringBuilder _actualTitle = new StringBuilder(string.Empty);
            if (_splits.Length > 1)
            {
                string _title = _splits[0];
                _actualTitle.Append(_title.Substring(0, 1)); _actualTitle.Append(_title.Substring(1, _title.Length - 1).ToLower()); _actualTitle.Append(".");
            }
            return _actualTitle.ToString();
        }
        private void GetNICGender()
        {
            try
            {
                String nic_ = txtNIC.Text.Trim().ToUpper();
                char[] nicarray = nic_.ToCharArray();
                string thirdNum = (nicarray[2]).ToString();
                if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                {
                    cmbTitle.Text = "MS.";
                }
                else
                {
                    cmbTitle.Text = "MR.";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            try
            {
                lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
                lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void ViewCustomerAccountDetail(string _customer)
        {
            try
            {
                if (string.IsNullOrEmpty(_customer.Trim())) return;
                if (_customer != "CASH")
                {
                    CustomerAccountRef _account = CHNLSVC.Sales.GetCustomerAccount(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                    lblAccountBalance.Text = _account.Saca_acc_bal.ToString();
                    lblAvailableCredit.Text = (_account.Saca_crdt_lmt - _account.Saca_ord_bal - _account.Saca_acc_bal).ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public GroupBussinessEntity GetbyCustCDGrup(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(custCD, null, null, null, null, null);
        }

        private string ReturnLoyaltyNo()
        {
            string _no = string.Empty;
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, null, null);

                if (_result != null && _result.Rows.Count > 0)
                {
                    if (_result.Rows.Count > 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Customer having multiple loyalty cards. Please select one of them !!!')", true);

                        //txtLoyalty.BackColor = Color.White;
                        return _no;
                    }
                    _no = _result.Rows[0].Field<string>("Card No");
                }
                else
                {
                 //   txtLoyalty.BackColor = Color.White;
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
            return _no;
        }

        private void EnableDisableCustomer()
        {
            try
            {
                if (txtCustomer.Text == "CASH")
                {
                    txtCustomer.Enabled = true;
                    txtCusName.Enabled = true;
                    txtAddress1.Enabled = true;
                    txtAddress2.Enabled = true;
                    txtMobile.Enabled = true;
                    txtNIC.Enabled = true;

                    btnSearch_NIC.Enabled = true;
                    lbtncode.Enabled = true;
                    btnSearch_Mobile.Enabled = true;
                }
                else
                {
                    txtCusName.Enabled = false;
                    txtAddress1.Enabled = false;
                    txtAddress2.Enabled = false;
                    txtMobile.Enabled = false;
                    txtNIC.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LoadCustomerDetailsByCustomer()
        {
            if (string.IsNullOrEmpty(txtCustomer.Text)) return;
            try
            {
                if (cmbInvType.Text.Trim() == "CRED" && txtCustomer.Text.Trim() == "CASH")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You can not select customer as CASH, because your invoice type is " + cmbInvType.Text + " !!!')", true);
                   
                    ClearCustomer(false);
                    txtCustomer.Focus();
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();

                if (!string.IsNullOrEmpty(txtCustomer.Text))
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());

                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This customer already inactive. Please contact IT dept !!!')", true);

                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }

                    DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                    if (_table != null && _table.Rows.Count > 0)
                    {
                        var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                        if (_isAvailable == null || _isAvailable.Count <= 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Customer is not allow for enter transaction under selected invoice type !!!')", true);

                            ClearCustomer(true);
                            txtCustomer.Focus();
                            return;
                        }
                    }
                    else if (cmbInvType.Text != "CS")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected customer is not allow to enter transaction under selected invoice type !!!')", true);

                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                        SetCustomerAndDeliveryDetails(false, null);
                    }
                    else
                    {
                        LoadTaxDetail(_masterBusinessCompany);
                        SetCustomerAndDeliveryDetails(false, null);
                    }

                    ViewCustomerAccountDetail(txtCustomer.Text);
                }
                else
                {
                    GroupBussinessEntity _grupProf = GetbyCustCDGrup(txtCustomer.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;

                        DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                        if (_table != null && _table.Rows.Count > 0)
                        {
                            var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                            if (_isAvailable == null || _isAvailable.Count <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Customer is not allow for enter transaction under selected invoice type !!!')", true);

                                ClearCustomer(true);
                                txtCustomer.Focus();
                                return;
                            }
                        }
                        else if (cmbInvType.Text != "CS")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected Customer is not allow for enter transaction under selected invoice type !!!')", true);
                           
                            ClearCustomer(true);
                            txtCustomer.Focus();
                            return;
                        }
                    }
                    else
                    {
                        _isGroup = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid customer !!!')", true);
                       
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }

                }
                //txtLoyalty.Text = ReturnLoyaltyNo();
                //txtLoyalty_TextChanged(null, null);
                EnableDisableCustomer();
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

        public GroupBussinessEntity GetbyNICGrup(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, nic, null, null, null, null);
        }

        protected void LoadCustomerDetailsByNIC()
        {
            if (string.IsNullOrEmpty(txtNIC.Text)) { return; }
            _masterBusinessCompany = new MasterBusinessEntity();
            try
            {
                if (!string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (!IsValidNIC(txtNIC.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid NIC !!!')", true);

                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(Session["UserCompanyCode"].ToString(), string.Empty, txtNIC.Text, string.Empty, "C");
                  

                    if (cmbInvType.Text.Trim() == "CRED")
                    {
                        if (_custList != null && _custList.Count > 0) 
                        {
                            foreach (MasterBusinessEntity _cust in _custList)
                            {
                                if (_cust.Mbe_is_suspend == true)
                                {
                                    string msg = "Customer suspend !!! - [" + _cust.Mbe_cd + " | " + _cust.Mbe_name + " For more information, please contact Accounts Dept !!!";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                                    
                                }
                            }
                        }
                    }

                    
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, txtNIC.Text, null, null, null, Session["UserCompanyCode"].ToString());
                }
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        LoadTaxDetail(_masterBusinessCompany);
                        SetCustomerAndDeliveryDetails(false, null);
                        ViewCustomerAccountDetail(txtCustomer.Text);
                        GetNICGender();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This customer is already inactive. Please contact accounts dept !!!')", true);
                      
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }
                }
                else
                {

                    GroupBussinessEntity _grupProf = GetbyNICGrup(txtNIC.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        GetNICGender();
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;

                        DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                        if (_table != null && _table.Rows.Count > 0)
                        {
                            var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                            if (_isAvailable == null || _isAvailable.Count <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Customer is not allow for enter transaction under selected order type !!!')", true);
                               
                                ClearCustomer(true);
                                txtCustomer.Focus();
                                return;
                            }
                        }
                        else if (cmbInvType.Text != "CS")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected Customer is not allow for enter transaction under selected order type !!!')", true);

                            ClearCustomer(true);
                            txtCustomer.Focus();
                            return;
                        }
                    }

                }

                //txtLoyalty.Text = ReturnLoyaltyNo();
               // txtLoyalty_TextChanged(null, null);
                EnableDisableCustomer();
                txtMobile.Focus();
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
    
        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetailsByCustomer();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        //protected void txtLoyalty_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DivsHide();
        //        //if (string.IsNullOrEmpty(txtLoyalty.Text)) return;

        //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
        //        DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, null, null);
        //        if (_result != null) if (_result.Rows.Count > 0)
        //            {
        //                var _results = _result.AsEnumerable().ToList().Where(x => x.Field<string>("Card No") == txtLoyalty.Text.Trim()).ToList();
        //                if (_results == null || _results.Count <= 0)
        //                {
        //                    divalert.Visible = true;
        //                    lblalert.Text = "Please check the loyalty card !!!";
        //                    //txtLoyalty.Text = string.Empty;
        //                    //txtLoyalty.Focus();
        //                    return;
        //                }
        //                else
        //                {
        //                    string _tem = _results.AsEnumerable().Select(x => x.Field<string>("Type")).ToList()[0];
        //                    _loyaltyType = CHNLSVC.Sales.GetLoyaltyType(_tem);
        //                    if (_loyaltyType == null)
        //                    {
        //                        divalert.Visible = true;
        //                        lblalert.Text = "Loyalty Card Type not found !!!";
        //                        //txtLoyalty.Text = string.Empty;
        //                    }
        //                }
        //            }
        //    }
        //    catch (Exception ex)
        //    {
        //        divalert.Visible = true;
        //        lblalert.Text = ex.Message;
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseAllChannels();
        //    }
        //}

        private void LoadInvItems(Int32 seqno,string reqno)
        {
            try
            {
                DataTable dtHeaders = CHNLSVC.CommonSearch.SearchPRNHeader(reqno);

                dtiInvtems = CHNLSVC.Sales.GetInvReqItems(seqno.ToString(),Session["UserCompanyCode"].ToString(),Session["UserDefProf"].ToString());
                if (dtiInvtems.Rows.Count > 0)
                {
                    gvInvoiceItem.DataSource = null;
                    gvInvoiceItem.DataBind();

                    gvInvoiceItem.DataSource = dtiInvtems;
                    gvInvoiceItem.DataBind();
                }

                ViewState["ITEMSTABLE"] = null;
                this.BindItemsGrid();

                ViewState["ITEMSTABLE"] = dtiInvtems;
                this.BindItemsGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private bool LoadPriceBook(string _invoiceType)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
            {
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text).Select(x => x.Sadd_pb).Distinct().ToList();

                    cmbBook.DataSource = _books;
                    cmbBook.DataBind();
                    cmbBook.SelectedIndex = cmbBook.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultBook)) cmbBook.Text = DefaultBook;
                    cmbBook.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    cmbBook.DataSource = null;
                }
            }
            else
            {
                cmbBook.DataSource = null;
            }
            return _isAvailable;
        }

        private void LoadPriceLevelMessage()
        {
            try
            {
                DataTable _msg = CHNLSVC.Sales.GetPriceLevelMessage(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
                if (_msg != null && _msg.Rows.Count > 0) lblLvlMsg.Text = _msg.Rows[0].Field<string>("Sapl_spmsg");
                else lblLvlMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadPromotor()
        {
            cmbTechnician.DataSource = null;
            txtPromotor.Text = "";
            DataTable _tblPromotor = CHNLSVC.General.GetProfitCenterAllocatedPromotors(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());

            if (_tblPromotor != null)
            {
                var _lst0 = _tblPromotor.AsEnumerable().ToList();
                cmbTechnician.DataSource = _tblPromotor;
                cmbTechnician.DataValueField = "mpp_promo_cd";
                cmbTechnician.DataTextField = "mpp_promo_name";
                cmbTechnician.DataBind();

                if (_tblPromotor.Rows.Count > 0)
                {
                    cmbTechnician.DataBind();
                }
                if (_lst0 != null && _lst0.Count > 0) cmbTechnician.DataSource = _lst0.CopyToDataTable();
                {
                    cmbTechnician.DataValueField = "mpp_promo_cd"; cmbTechnician.DataTextField = "mpp_promo_name";
                    cmbTechnician.DataBind();
                }
                cmbTechnician.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        private void VaribleClear()
        { _lineNo = 1; _isEditPrice = false; _isEditDiscount = false; GrndSubTotal = 0; GrndDiscount = 0; GrndTax = 0; SSCombineLine = 1; _proVouInvcLine = 0; _proVouInvcItem = string.Empty; }

        private void LoadInvoiceProfitCenterDetail()
        { if (_MasterProfitCenter != null) if (_MasterProfitCenter.Mpc_cd != null) 
        { if (!_MasterProfitCenter.Mpc_edit_price) txtUnitPrice.ReadOnly = true;
            txtCustomer.Text = _MasterProfitCenter.Mpc_def_customer; 
            //txtExecutive.Text = _MasterProfitCenter.Mpc_man;
            _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text, string.Empty, string.Empty, "C"); } }

        private void LoadCancelPermission()
        { IsFwdSaleCancelAllowUser = false; IsDlvSaleCancelAllowUser = false; lbtncancel.Visible = false; string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString(); if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10002)) { IsFwdSaleCancelAllowUser = true; lbtncancel.Visible = true; } else { IsFwdSaleCancelAllowUser = false; lbtncancel.Visible = false; } if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10042)) { IsFwdSaleCancelAllowUser = true; IsDlvSaleCancelAllowUser = true; lbtncancel.Visible = true; } else { if (!IsFwdSaleCancelAllowUser) { IsDlvSaleCancelAllowUser = false; lbtncancel.Visible = false; } } }


        private void InitializeValuesNDefaultValueSet()
        {
            try
            {
                txtdate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
                _serverDt = CHNLSVC.Security.GetServerDateTime().Date;
                VaribleClear();
                //VariableInitialization();
                LoadInvoiceProfitCenterDetail();
                LoadPriceDefaultValue();
                LoadCancelPermission();
                SetDecimalTextBoxForZero(true, true, true);
                //LoadPayMode(); LoadControl();
                //lblBackDateInfor.Text = string.Empty;
                BuyBackItemList = null;
                txtPromotor.Text = "";
                LoadInvoiceType();
                if (cmbInvType.Text.Trim() != "CRED")
                {
                    LoadCustomerDetailsByCustomer();
                    cmbTitle_SelectedIndexChanged(null, null);
                }
                if (_MasterProfitCenter.Mpc_chnl == "AUTO_DEL")
                { txtManualRefNo.Enabled = true; }
                else
                {
                    txtManualRefNo.Enabled = true;
                }

                _isRegistrationMandatory = false;

                //txtPromoVouNo.Clear();
                //lblPromoVouNo.Text = "";
                //lblPromoVouUsedFlag.Text = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtncurrency_Click(object sender, EventArgs e)
        {
            try
            {

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "14";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private bool ValidateSO()
        {
            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select customer !!!')", true);
               
                lbtncode.Focus();
                return false;
            }

            if (cmbTitle.SelectedItem.Text == "Select")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select title !!!')", true);
              
                cmbTitle.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtlocation.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the dispatch location !!!')", true);

                txtlocation.Focus();
                return false;
            }

            if (gvInvoiceItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter order items !!!')", true);
               
                txtItem.Focus();
                return false;
            }

            //if (cmbInvType.SelectedIndex == 0)
            //{
            //    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select type !!!')", true);
            //    divalert.Visible = true;
            //    lblalert.Text = "Please select type !!!";
            //    cmbInvType.Focus();
            //    return false;
            //}

            if (cmbExecutive.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select sales executive !!!')", true);
              
                cmbExecutive.Focus();
                return false;
            }

            if (cmbTechnician.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select promotor !!!')", true);
              
                cmbTechnician.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateSOItems()
        {
            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select items by item or by serial !!!')", true);
                
                lbtncode.Focus();
                return false;
            }

            if (cmbStatus.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select status !!!')", true);
            
                cmbStatus.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbStatus.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select status !!!')", true);
               
                cmbStatus.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter qty !!!')", true);
              
                txtQty.Focus();
                return false;
            }

            if (!IsNumeric(txtQty.Text.Trim(), NumberStyles.Float))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid number for qty !!!')", true);
             
                txtQty.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('0 qty is not allowed !!!')", true);
                
                txtQty.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
                
                txtQty.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Unit Price !!!')", true);
                
                txtUnitPrice.Focus();
                return false;
            }

            //if (!IsNumeric(txtUnitPrice.Text.Trim(), NumberStyles.Float))
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Please enter valid number for Unit Price !!!";
            //    txtUnitPrice.Focus();
            //    return false;
            //}

            if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('0 Unit price is not allowed !!!')", true);
               
                txtUnitPrice.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
                
                txtUnitPrice.Focus();
                return false;
            }

            //if (!IsNumeric(txtDisRate.Text.Trim(), NumberStyles.Float))
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Please enter valid number for Dis.Rate !!!";
            //    txtDisRate.Focus();
            //    return false;
            //}

            if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
               
                txtDisRate.Focus();
                return false;
            }

            //if (!IsNumeric(txtDisAmt.Text.Trim(), NumberStyles.Float))
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Please enter valid number for Dis.Rate !!!";
            //    txtDisAmt.Focus();
            //    return false;
            //}

            if (Convert.ToDecimal(txtDisAmt.Text.Trim()) < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
              
                txtDisAmt.Focus();
                return false;
            }

            //if (!IsNumeric(txtTaxAmt.Text.Trim(), NumberStyles.Float))
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Please enter valid number for tax !!!";
            //    txtTaxAmt.Focus();
            //    return false;
            //}

            if (Convert.ToDecimal(txtTaxAmt.Text.Trim()) < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
             
                txtTaxAmt.Focus();
                return false;
            }

            //if (!IsNumeric(txtLineTotAmt.Text.Trim(), NumberStyles.Float))
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Please enter valid number for Dis.Rate !!!";
            //    txtLineTotAmt.Focus();
            //    return false;
            //}

            if (Convert.ToDecimal(txtLineTotAmt.Text.Trim()) < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
          
                txtLineTotAmt.Focus();
                return false;
            }

            if (cmbBook.SelectedIndex == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select type !!!')", true);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select book !!!')", true);
               
                cmbBook.Focus();
                return false;
            }

            if (cmbLevel.SelectedIndex == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select type !!!')", true);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select level !!!')", true);
               
                cmbLevel.Focus();
                return false;
            }

            if (cmbStatus.SelectedIndex == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select type !!!')", true);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select status !!!')", true);
              
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        public bool CheckServerDateTime()
        {
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine date conflict with the server date!.please contact system administrator !!!')", true);

                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine time zone conflict with the server time zone!.please contact system administrator !!!')", true);

                return false;
            }
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmplaceord.Value == "Yes")
                {
                    bool isvalidate = ValidateSO();
                    if (isvalidate == false)
                    {
                        return;
                    }
                    SaveAll();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private bool IsBackDateOk(bool _isDelverNow, bool _isBuyBackItemAvailable)
        {
            bool _isOK = true;
            _isBackDate = true;
            bool _allowCurrentTrans = false;
            //if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty.ToUpper().ToString(), Session["UserDefProf"].ToString(), "m_Trans_Sales_SalesOrder", txtdate, lblBackDateInfor, txtdate.Text, out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtdate.Text) != DateTime.Now.Date)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Back date not allow for selected date for the profit center " + Session["UserDefProf"].ToString() + " !!!')", true);
                        
                        txtdate.Focus();
                        _isOK = false;
                        _isBackDate = false;
                        return _isOK;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Back date not allow for selected date for the profit center " + Session["UserDefProf"].ToString() + " !!!')", true);
                
                    txtdate.Focus();
                    _isOK = false;
                    _isBackDate = false;
                    return _isOK;
                }
            }
            return _isOK;
        }

        public bool IsReferancedDocDateAppropriate(List<ReptPickSerials> _reptPrickSerialList, DateTime _processDate)
        {
            bool _appropriate = true;
            if (_reptPrickSerialList != null)
            {
                var _isInAppropriate = _reptPrickSerialList.Where(x => x.Tus_doc_dt.Date > _processDate.Date).ToList();
                if (_isInAppropriate == null || _isInAppropriate.Count <= 0) _appropriate = true;
                else _appropriate = false;
                if (_appropriate == false)
                {
                    StringBuilder _documents = new StringBuilder();
                    foreach (ReptPickSerials _one in _isInAppropriate)
                    {
                        if (string.IsNullOrEmpty(_documents.ToString()))
                            _documents.Append(_one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                        else
                            _documents.Append(" and " + _one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                    }
                    string msg = "The Inward documents " + _documents.ToString() + " equal or grater than to a this Outward document " + _processDate.Date.ToShortDateString() + " date !!!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg +"')", true);
                    
                }
            }
            return _appropriate;
        }

        private decimal GetExchangeRate()
        {
            try
            {
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), txtcurrency.Text.Trim(), DateTime.Now, _pc.Mpc_def_exrate, string.Empty);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                }
                return _exchangRate;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return 0;
            }
        }
        private void SaveOrderHeaderWithDetails()
        {
            try
            {
                _userid = (string)Session["UserID"];

                string _customerCompany = (string)Session["CUSCOM"];
                string _customerLocation = (string)Session["CUSLOC"];
                string _cushascompany = (string)Session["CUSHASCOMPANY"];

                string _dcuscode = (string)Session["DCUSCODE"];
                string _dcusname = (string)Session["DCUSNAME"];
                string _dcustown = (string)Session["DTOWN"];
                string _dcusadd1 = (string)Session["DCUSADD1"];
                string _dcusadd2 = (string)Session["DCUSADD2"];
                string _dcusloc =  (string)Session["DCUSLOC"];

                if (!string.IsNullOrEmpty(txtcurrency.Text.Trim()))
                {
                    exchangerate = GetExchangeRate();
                }

                Int32 so_rest_stk = 0;
                Int32 mpc_so_res = 0;
                List<Int32> qtylist = new List<int>();

                if (!string.IsNullOrEmpty(txtlocation.Text))
                {
                    DataTable dtchkpc_rest_stk = CHNLSVC.Sales.Check_PC_SO_REST_STK(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());

                    if (dtchkpc_rest_stk.Rows.Count > 0)
                    {
                        foreach (DataRow ddr in dtchkpc_rest_stk.Rows)
                        {
                            if (!string.IsNullOrEmpty(ddr["mpc_so_rest_stk"].ToString()))
                            {
                                so_rest_stk = Convert.ToInt32(ddr["mpc_so_rest_stk"].ToString());
                            }
                            if (!string.IsNullOrEmpty(ddr["mpc_so_res"].ToString()))
                            {
                                mpc_so_res = Convert.ToInt32(ddr["mpc_so_res"].ToString());
                            }
                        }
                    }

                    if (so_rest_stk == 1)
                    {
                        foreach (GridViewRow ddr in gvInvoiceItem.Rows)
                        {
                            DataTable dtstatusBAL = CHNLSVC.Sales.GetItemStatusVal(ddr.Cells[3].Text);

                            string _StatusBAL = string.Empty;

                            if (dtstatusBAL.Rows.Count > 0)
                            {
                                foreach (DataRow ddr2BAL in dtstatusBAL.Rows)
                                {
                                    _StatusBAL = ddr2BAL[0].ToString();
                                }
                            }

                            DataTable dtbal = CHNLSVC.Sales.CheckLocationBaBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddr.Cells[1].Text, _StatusBAL);
                            Decimal balance = 0;

                            foreach (DataRow item in dtbal.Rows)
                            {
                                balance = Convert.ToDecimal(item["inl_free_qty"].ToString());
                            }

                            if (balance > 0)
                            {
                                qtylist.Add(0);
                            }
                            else
                            {
                                qtylist.Add(1);
                            }
                        }
                    }
                }

                if (qtylist.Contains(1))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Stock is not available for added item/items at the dispatch location')", true);
                    return;
                }

                #region Header

                string SBU_Character = Session["UserSBU"].ToString();
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserDefProf"].ToString();
                mastAutoNo.Aut_cate_tp = "LOC";
                mastAutoNo.Aut_direction = 0;
                mastAutoNo.Aut_moduleid = "SO";
                mastAutoNo.Aut_start_char = "CS";
                mastAutoNo.Aut_year = DateTime.Now.Year;

                SalesOrderHeader SalesOrder = new SalesOrderHeader();

                string seqnofororder = (string)Session["SOSEQNO"];

                if (string.IsNullOrEmpty(seqnofororder))
                {
                    seqnofororder = "0";
                }

                SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
                SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
                SalesOrder.SOH_TP = "INV";
                SalesOrder.SOH_SO_TP = cmbInvType.SelectedValue;
                SalesOrder.SOH_SO_SUB_TP ="SA";
                SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
                SalesOrder.SOH_DT = Convert.ToDateTime(txtdate.Text);
                SalesOrder.SOH_MANUAL = 0;
                SalesOrder.SOH_MAN_REF = txtManualRefNo.Text.Trim();
                SalesOrder.SOH_REF_DOC = txtdocrefno.Text.Trim();
                SalesOrder.SOH_CUS_CD = txtCustomer.Text.Trim();
                SalesOrder.SOH_CUS_NAME = txtCusName.Text.Trim();
                SalesOrder.SOH_CUS_ADD1 = txtAddress1.Text.Trim();
                SalesOrder.SOH_CUS_ADD2 = txtAddress2.Text.Trim();
                SalesOrder.SOH_CURRENCY = txtcurrency.Text.Trim();
                SalesOrder.SOH_EX_RT = exchangerate;
                SalesOrder.SOH_TOWN_CD = ddltown.SelectedValue;//_dcustown;
                SalesOrder.SOH_D_CUST_CD = txtdelcuscode.Text.Trim();//_dcuscode;
                SalesOrder.SOH_D_CUST_ADD1 = txtdelad1.Text.Trim();//_dcusadd1;
                SalesOrder.SOH_D_CUST_ADD2 = txtdelad2.Text.Trim(); //_dcusadd2;
                SalesOrder.SOH_MAN_CD = cmbExecutive.SelectedValue;
                SalesOrder.SOH_SALES_EX_CD = cmbExecutive.SelectedValue;
                SalesOrder.SOH_SALES_STR_CD = string.Empty;
                SalesOrder.SOH_SALES_SBU_CD = string.Empty;
                SalesOrder.SOH_SALES_SBU_MAN = string.Empty;
                SalesOrder.SOH_SALES_CHN_CD = string.Empty;
                SalesOrder.SOH_SALES_CHN_MAN = string.Empty;
                SalesOrder.SOH_SALES_REGION_CD = string.Empty;
                SalesOrder.SOH_SALES_REGION_MAN = string.Empty;
                SalesOrder.SOH_SALES_ZONE_CD = string.Empty;
                SalesOrder.SOH_SALES_ZONE_MAN = string.Empty;
                SalesOrder.SOH_STRUCTURE_SEQ = txtQuotation.Text.Trim();
                SalesOrder.SOH_ESD_RT = 0;
                SalesOrder.SOH_WHT_RT = 0;
                SalesOrder.SOH_EPF_RT = 0;
                SalesOrder.SOH_PDI_REQ = 0;
                SalesOrder.SOH_REMARKS = string.Empty;
                SalesOrder.SOH_IS_ACC_UPLOAD = 0;

                if (_userid == "ADMIN")
                {
                    SalesOrder.SOH_STUS = "A";
                }
                else
                {
                    SalesOrder.SOH_STUS = "S";
                }
                
                SalesOrder.SOH_CRE_BY = _userid;
                SalesOrder.SOH_CRE_WHEN = CHNLSVC.Security.GetServerDateTime();
                SalesOrder.SOH_MOD_BY = _userid;
                SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
                SalesOrder.SOH_SESSION_ID = Session["SessionID"].ToString();
                SalesOrder.SOH_ANAL_1 = txtPromotor.Text.Trim();
                SalesOrder.SOH_ANAL_2 = string.Empty;
                SalesOrder.SOH_ANAL_3 = string.Empty;
                SalesOrder.SOH_ANAL_4 = txtPoNo.Text.Trim();
                SalesOrder.SOH_ANAL_5 = cmbTechnician.SelectedValue;
                SalesOrder.SOH_ANAL_6 = string.Empty;
                SalesOrder.SOH_ANAL_7 = 0;
                SalesOrder.SOH_ANAL_8 = Convert.ToInt32(0);
                SalesOrder.SOH_ANAL_9 = Convert.ToInt32(0);
                SalesOrder.SOH_ANAL_10 = 0;
                SalesOrder.SOH_ANAL_11 = 0;
                SalesOrder.SOH_ANAL_12 = Convert.ToDateTime(DateTime.Now);
                SalesOrder.SOH_DIRECT = 1;
                SalesOrder.SOH_TAX_INV = chkTaxPayable.Checked ? 1 : 0;
                SalesOrder.SOH_GRUP_CD = string.Empty;
                SalesOrder.SOH_ACC_NO = string.Empty;
                SalesOrder.SOH_TAX_EXEMPTED = lblVatExemptStatus.Text == "Available" ? 1 : 0;
                SalesOrder.SOH_IS_SVAT = lblSVatStatus.Text == "Available" ? 1 : 0;
                SalesOrder.SOH_FIN_CHRG = 1;
                SalesOrder.SOH_DEL_LOC = txtdellocation.Text; //_dcusloc;
                SalesOrder.SOH_GRN_COM = _customerCompany;
                SalesOrder.SOH_GRN_LOC = txtlocation.Text.Trim();
                SalesOrder.SOH_IS_GRN = Convert.ToInt32(_cushascompany);
                SalesOrder.SOH_D_CUST_NAME = txtdelname.Text.Trim();// _dcusname;
                SalesOrder.SOH_IS_DAYEND = 0;
                SalesOrder.SOH_SCM_UPLOAD = 0;
                SalesOrder.SOH_SEQ_NO = Convert.ToInt32(seqnofororder);

                Tuple<int, string> outopno = CHNLSVC.Sales.PlaceSalesOrder(SalesOrder, mastAutoNo);

                string newseqno = string.Empty;
                string outputopno = string.Empty;

                if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    newseqno = outopno.Item1.ToString();
                    outputopno = outopno.Item2.ToString();
                }
                else
                {
                    newseqno = seqnofororder;
                    outputopno = txtInvoiceNo.Text.Trim();
                }

                if (Convert.ToInt32(newseqno) == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    return;
                }

                #endregion

                List<SalesOrderItems> _SalesOrderItemsList = new List<SalesOrderItems>();
                #region Items

                if (gvInvoiceItem.Rows.Count > 0)
                {
                    foreach (GridViewRow ddr in gvInvoiceItem.Rows)
                    {
                        DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(ddr.Cells[3].Text);

                        string _Status = string.Empty;

                        if (dtstatus.Rows.Count > 0)
                        {
                            foreach (DataRow ddr2 in dtstatus.Rows)
                            {
                                _Status = ddr2[0].ToString();
                            }
                        }

                        SalesOrderItems SalesOrderItems = new SalesOrderItems();
                        SalesOrderItems.SOI_SEQ_NO = Convert.ToInt32(newseqno);
                        SalesOrderItems.SOI_ITM_LINE = Convert.ToInt32(ddr.Cells[0].Text);
                        SalesOrderItems.SOI_SO_NO = outputopno;
                        SalesOrderItems.SOI_ITM_CD = ddr.Cells[1].Text;
                        SalesOrderItems.SOI_ITM_STUS = _Status;
                        SalesOrderItems.SOI_ITM_TP = string.Empty;
                        SalesOrderItems.SOI_UOM = string.Empty;
                        SalesOrderItems.SOI_QTY = Convert.ToDecimal(ddr.Cells[4].Text);
                        SalesOrderItems.SOI_INV_QTY = Convert.ToDecimal(ddr.Cells[4].Text);
                        SalesOrderItems.SOI_UNIT_RT = Convert.ToDecimal(ddr.Cells[5].Text);
                        SalesOrderItems.SOI_UNIT_AMT = Convert.ToDecimal(ddr.Cells[6].Text);
                        SalesOrderItems.SOI_DISC_RT = Convert.ToDecimal(ddr.Cells[7].Text);
                        SalesOrderItems.SOI_DISC_AMT = Convert.ToDecimal(ddr.Cells[8].Text);
                        SalesOrderItems.SOI_ITM_TAX_AMT = Convert.ToDecimal(ddr.Cells[9].Text);
                        SalesOrderItems.SOI_TOT_AMT = Convert.ToDecimal(ddr.Cells[10].Text);
                        SalesOrderItems.SOI_PBOOK = ddr.Cells[11].Text;
                        SalesOrderItems.SOI_PB_LVL = ddr.Cells[12].Text;
                        SalesOrderItems.SOI_PB_PRICE = 0;
                        SalesOrderItems.SOI_SEQ = 0;
                        SalesOrderItems.SOI_ITM_SEQ = 0;
                        SalesOrderItems.SOI_WARR_PERIOD = 0;
                        SalesOrderItems.SOI_WARR_REMARKS = string.Empty;
                        SalesOrderItems.SOI_IS_PROMO = 0;
                        SalesOrderItems.SOI_PROMO_CD = string.Empty;
                        SalesOrderItems.SOI_ALT_ITM_CD = string.Empty;
                        SalesOrderItems.SOI_ALT_ITM_DESC = string.Empty;
                        SalesOrderItems.SOI_PRINT_STUS = 0;
                        SalesOrderItems.SOI_RES_NO = ddr.Cells[13].Text;
                        SalesOrderItems.SOI_RES_LINE_NO = 0;
                        SalesOrderItems.SOI_JOB_NO = string.Empty;
                        SalesOrderItems.SOI_WARR_BASED = 0;
                        SalesOrderItems.SOI_MERGE_ITM = string.Empty;
                        SalesOrderItems.SOI_JOB_LINE = 0;
                        SalesOrderItems.SOI_OUTLET_DEPT = string.Empty;
                        SalesOrderItems.SOI_TRD_SVC_CHRG = 0;
                        SalesOrderItems.SOI_DIS_SEQ = 0;
                        SalesOrderItems.SOI_DIS_LINE = 0;
                        SalesOrderItems.SOI_DIS_TYPE = string.Empty;
                        SalesOrderItems.SOI_ANAL1 = string.Empty;
                        SalesOrderItems.SOI_ANAL2 = string.Empty;
                        SalesOrderItems.SOI_ANAL3 = string.Empty;
                        SalesOrderItems.SOI_ANAL4 = 1;
                        SalesOrderItems.SOI_ANAL5 = 1;

                        _SalesOrderItemsList.Add(SalesOrderItems);
                        Int32 SuccessSOItems = CHNLSVC.Sales.SaveSOItems(SalesOrderItems);

                        if (Convert.ToInt32(SuccessSOItems) == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            return;
                        }

                        Int32 UpdatedInrLoc = 0;
                        if (mpc_so_res == 1)
                        {
                            UpdatedInrLoc = CHNLSVC.Sales.UpdateINRLoc(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddr.Cells[1].Text, _Status, Convert.ToDecimal(ddr.Cells[4].Text));
                        }

                        if (Convert.ToInt32(UpdatedInrLoc) == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            return;
                        }
                    }
                }
                #endregion

                string isfromrequest = (string)Session["RSEQNO"];

                if (!string.IsNullOrEmpty(isfromrequest))
                {
                    if (isfromrequest != "N/A")
                    {
                        foreach (GridViewRow itemInv in gvInvoiceItem.Rows)
                        {
                            INT_REQ_ITM _ReqItem = new INT_REQ_ITM();
                            _ReqItem.ITRI_QTY = Convert.ToDecimal(itemInv.Cells[4].Text);
                            _ReqItem.ITRI_SEQ_NO = Convert.ToInt32(itemInv.Cells[14].Text);
                            _ReqItem.ITRI_ITM_CD = itemInv.Cells[1].Text;
                            Int32 SuccessR = CHNLSVC.Sales.BalanceItemStock(_ReqItem);
                        }
                    }
                }

                #region SalesOrderItemTax

                foreach (GridViewRow ddr in gvInvoiceItem.Rows)
                {
                    SalesOrderItemTax SalesOrderItemTax = new SalesOrderItemTax();
                    SalesOrderItemTax.SOTX_SO_NO = outputopno;
                    SalesOrderItemTax.SOTX_ITM_CD = ddr.Cells[1].Text;
                    SalesOrderItemTax.SOTX_ITM_TAX_TP = "VAT";
                    SalesOrderItemTax.SOTX_ITM_TAX_RT = 0;
                    SalesOrderItemTax.SOTX_ITM_TAX_AMT = Convert.ToDecimal(ddr.Cells[9].Text);
                    SalesOrderItemTax.SOTX_JOB_NO = string.Empty;
                    SalesOrderItemTax.SOTX_JOB_LINE = Convert.ToInt32(ddr.Cells[0].Text);
                    SalesOrderItemTax.SOTX_SEQ_NO = Convert.ToInt32(newseqno);
                    SalesOrderItemTax.SOTX_ITM_LINE = Convert.ToInt32(ddr.Cells[0].Text);
                    Int32 SuccessSOItemTax = CHNLSVC.Sales.SaveSOItemTax(SalesOrderItemTax);

                    if (Convert.ToInt32(SuccessSOItemTax) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        return;
                    }
                }

                #endregion

               List<SalesOrderSer> _SalesOrderSerList = new List<SalesOrderSer>();
                #region SalesOrderSer

                if (gvPopSerial.Rows.Count > 0)
                {
                    foreach (GridViewRow ddr in gvPopSerial.Rows)
                    {
                        SalesOrderSer _SalesOrderSer = new SalesOrderSer();
                        _SalesOrderSer.SOSE_SEQ_NO = Convert.ToInt32(newseqno);
                        _SalesOrderSer.SOSE_ITM_LINE = Convert.ToInt32(ddr.Cells[0].Text);
                        _SalesOrderSer.SOSE_SO_NO = outputopno;
                        _SalesOrderSer.SOSE_ITM_CD = ddr.Cells[1].Text;
                        _SalesOrderSer.SOSE_SER_1 = ddr.Cells[6].Text;
                        _SalesOrderSer.SOSE_REMARKS = string.Empty;
                        _SalesOrderSer.SOSE_SEV_LOC = string.Empty;
                        _SalesOrderSer.SOSE_DEL_LOC = _dcusloc;
                        _SalesOrderSer.SOSE_SER_LINE = 0;
                        _SalesOrderSer.SOSE_SER_2 = string.Empty;
                        _SalesOrderSerList.Add(_SalesOrderSer);
                        Int32 SuccessSOSer = CHNLSVC.Sales.SaveSOSer(_SalesOrderSer);

                        if (Convert.ToInt32(SuccessSOSer) == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            return;
                        }


                        //Int32 UpdateSerial = CHNLSVC.Sales.UpdateSerialAvailability(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddr.Cells[5].Text.Trim());

                        //if (Convert.ToInt32(UpdateSerial) == -1)
                        //{
                        //    divalert.Visible = true;
                        //    lblalert.Text = "Error in processing !!!";
                        //    return;
                        //}

                    }
                }
                #endregion

                

                string purname = string.Empty;
                string purmessage=string.Empty;

                if (chkpo.Checked == true)
                {
                    purname = SavePOHeader(); 
                }

                if (chkpo.Checked == true)
                {
                    purmessage = ".Successfully created purchase order !!! " + purname;
                }

                if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    string msg = "Successfully created sales order !!! " + outputopno + " " + purmessage;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg + "')", true);
                }
                else
                {
                    string msg2 = "Successfully updated sales order " + txtInvoiceNo.Text.Trim();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg2 + "')", true);
                }
                Clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void saveNew()
        {
            string _customerCompany = (string)Session["CUSCOM"];
            string _customerLocation = (string)Session["CUSLOC"];
            string _cushascompany = (string)Session["CUSHASCOMPANY"];

            if (!string.IsNullOrEmpty(txtcurrency.Text.Trim()))
            {
                exchangerate = GetExchangeRate();
            }
            Int32 so_rest_stk = 0;
            Int32 mpc_so_res = 0;
            List<Int32> qtylist = new List<int>();
            #region checkvalidation
            if (!string.IsNullOrEmpty(txtlocation.Text))
            {
                DataTable dtchkpc_rest_stk = CHNLSVC.Sales.Check_PC_SO_REST_STK(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());

                if (dtchkpc_rest_stk.Rows.Count > 0)
                {
                    foreach (DataRow ddr in dtchkpc_rest_stk.Rows)
                    {
                        if (!string.IsNullOrEmpty(ddr["mpc_so_rest_stk"].ToString()))
                        {
                            so_rest_stk = Convert.ToInt32(ddr["mpc_so_rest_stk"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ddr["mpc_so_res"].ToString()))
                        {
                            mpc_so_res = Convert.ToInt32(ddr["mpc_so_res"].ToString());
                        }
                    }
                }

                if (so_rest_stk == 1)
                {
                    foreach (GridViewRow ddr in gvInvoiceItem.Rows)
                    {
                        DataTable dtstatusBAL = CHNLSVC.Sales.GetItemStatusVal(ddr.Cells[3].Text);

                        string _StatusBAL = string.Empty;

                        if (dtstatusBAL.Rows.Count > 0)
                        {
                            foreach (DataRow ddr2BAL in dtstatusBAL.Rows)
                            {
                                _StatusBAL = ddr2BAL[0].ToString();
                            }
                        }

                        DataTable dtbal = CHNLSVC.Sales.CheckLocationBaBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddr.Cells[1].Text, _StatusBAL);
                        Decimal balance = 0;

                        foreach (DataRow item in dtbal.Rows)
                        {
                            balance = Convert.ToDecimal(item["inl_free_qty"].ToString());
                        }

                        if (balance > 0)
                        {
                            qtylist.Add(0);
                        }
                        else
                        {
                            qtylist.Add(1);
                        }
                    }
                }
            }

            if (qtylist.Contains(1))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Stock is not available for added item/items at the dispatch location')", true);
                return;
            }
            #endregion
            #region Header

            string SBU_Character = Session["UserSBU"].ToString();
            MasterAutoNumber mastAutoNo = new MasterAutoNumber();
            mastAutoNo.Aut_cate_cd = Session["UserDefProf"].ToString();
            mastAutoNo.Aut_cate_tp = "LOC";
            mastAutoNo.Aut_direction = 0;
            mastAutoNo.Aut_moduleid = "SO";
            mastAutoNo.Aut_start_char = "CS";
            mastAutoNo.Aut_year = DateTime.Now.Year;

            SalesOrderHeader SalesOrder = new SalesOrderHeader();

            string seqnofororder = (string)Session["SOSEQNO"];

            if (string.IsNullOrEmpty(seqnofororder))
            {
                seqnofororder = "0";
            }

            SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
            SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
            SalesOrder.SOH_TP = "INV";
            SalesOrder.SOH_SO_TP = cmbInvType.SelectedValue;
            SalesOrder.SOH_SO_SUB_TP = "SA";
            SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
            SalesOrder.SOH_DT = Convert.ToDateTime(txtdate.Text);
            SalesOrder.SOH_MANUAL = 0;
            SalesOrder.SOH_MAN_REF = txtManualRefNo.Text.Trim();
            SalesOrder.SOH_REF_DOC = txtdocrefno.Text.Trim();
            SalesOrder.SOH_CUS_CD = txtCustomer.Text.Trim();
            SalesOrder.SOH_CUS_NAME = txtCusName.Text.Trim();
            SalesOrder.SOH_CUS_ADD1 = txtAddress1.Text.Trim();
            SalesOrder.SOH_CUS_ADD2 = txtAddress2.Text.Trim();
            SalesOrder.SOH_CURRENCY = txtcurrency.Text.Trim();
            SalesOrder.SOH_EX_RT = exchangerate;
            SalesOrder.SOH_TOWN_CD = ddltown.SelectedValue;//_dcustown;
            SalesOrder.SOH_D_CUST_CD = txtdelcuscode.Text.Trim();//_dcuscode;
            SalesOrder.SOH_D_CUST_ADD1 = txtdelad1.Text.Trim();//_dcusadd1;
            SalesOrder.SOH_D_CUST_ADD2 = txtdelad2.Text.Trim(); //_dcusadd2;
            SalesOrder.SOH_MAN_CD = cmbExecutive.SelectedValue;
            SalesOrder.SOH_SALES_EX_CD = cmbExecutive.SelectedValue;
            SalesOrder.SOH_SALES_STR_CD = string.Empty;
            SalesOrder.SOH_SALES_SBU_CD = string.Empty;
            SalesOrder.SOH_SALES_SBU_MAN = string.Empty;
            SalesOrder.SOH_SALES_CHN_CD = string.Empty;
            SalesOrder.SOH_SALES_CHN_MAN = string.Empty;
            SalesOrder.SOH_SALES_REGION_CD = string.Empty;
            SalesOrder.SOH_SALES_REGION_MAN = string.Empty;
            SalesOrder.SOH_SALES_ZONE_CD = string.Empty;
            SalesOrder.SOH_SALES_ZONE_MAN = string.Empty;
            SalesOrder.SOH_STRUCTURE_SEQ = txtQuotation.Text.Trim();
            SalesOrder.SOH_ESD_RT = 0;
            SalesOrder.SOH_WHT_RT = 0;
            SalesOrder.SOH_EPF_RT = 0;
            SalesOrder.SOH_PDI_REQ = 0;
            SalesOrder.SOH_REMARKS = string.Empty;
            SalesOrder.SOH_IS_ACC_UPLOAD = 0;

            if (_userid == "ADMIN")
            {
                SalesOrder.SOH_STUS = "A";
            }
            else
            {
                SalesOrder.SOH_STUS = "S";
            }

            SalesOrder.SOH_CRE_BY = _userid;
            SalesOrder.SOH_CRE_WHEN = CHNLSVC.Security.GetServerDateTime();
            SalesOrder.SOH_MOD_BY = _userid;
            SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
            SalesOrder.SOH_SESSION_ID = Session["SessionID"].ToString();
            SalesOrder.SOH_ANAL_1 = txtPromotor.Text.Trim();
            SalesOrder.SOH_ANAL_2 = string.Empty;
            SalesOrder.SOH_ANAL_3 = string.Empty;
            SalesOrder.SOH_ANAL_4 = txtPoNo.Text.Trim();
            SalesOrder.SOH_ANAL_5 = cmbTechnician.SelectedValue;
            SalesOrder.SOH_ANAL_6 = string.Empty;
            SalesOrder.SOH_ANAL_7 = 0;
            SalesOrder.SOH_ANAL_8 = Convert.ToInt32(0);
            SalesOrder.SOH_ANAL_9 = Convert.ToInt32(0);
            SalesOrder.SOH_ANAL_10 = 0;
            SalesOrder.SOH_ANAL_11 = 0;
            SalesOrder.SOH_ANAL_12 = Convert.ToDateTime(DateTime.Now);
            SalesOrder.SOH_DIRECT = 1;
            SalesOrder.SOH_TAX_INV = chkTaxPayable.Checked ? 1 : 0;
            SalesOrder.SOH_GRUP_CD = string.Empty;
            SalesOrder.SOH_ACC_NO = string.Empty;
            SalesOrder.SOH_TAX_EXEMPTED = lblVatExemptStatus.Text == "Available" ? 1 : 0;
            SalesOrder.SOH_IS_SVAT = lblSVatStatus.Text == "Available" ? 1 : 0;
            SalesOrder.SOH_FIN_CHRG = 1;
            SalesOrder.SOH_DEL_LOC = txtdellocation.Text; //_dcusloc;
            SalesOrder.SOH_GRN_COM = _customerCompany;
            SalesOrder.SOH_GRN_LOC = txtlocation.Text.Trim();
            SalesOrder.SOH_IS_GRN = Convert.ToInt32(_cushascompany);
            SalesOrder.SOH_D_CUST_NAME = txtdelname.Text.Trim();// _dcusname;
            SalesOrder.SOH_IS_DAYEND = 0;
            SalesOrder.SOH_SCM_UPLOAD = 0;
            SalesOrder.SOH_SEQ_NO = Convert.ToInt32(seqnofororder);
 
            #endregion

            List<SalesOrderItems> _SalesOrderItemsList = new List<SalesOrderItems>();
            #region Items
            if (gvInvoiceItem.Rows.Count > 0)
            {
                foreach (GridViewRow ddr in gvInvoiceItem.Rows)
                {
                    DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(ddr.Cells[3].Text);

                    string _Status = string.Empty;

                    if (dtstatus.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtstatus.Rows)
                        {
                            _Status = ddr2[0].ToString();
                        }
                    }

                    SalesOrderItems SalesOrderItems = new SalesOrderItems();
                  //  SalesOrderItems.SOI_SEQ_NO = Convert.ToInt32(newseqno);
                    SalesOrderItems.SOI_ITM_LINE = Convert.ToInt32(ddr.Cells[0].Text);
                   // SalesOrderItems.SOI_SO_NO = outputopno;
                    SalesOrderItems.SOI_ITM_CD = ddr.Cells[1].Text;
                    SalesOrderItems.SOI_ITM_STUS = _Status;
                    SalesOrderItems.SOI_ITM_TP = string.Empty;
                    SalesOrderItems.SOI_UOM = string.Empty;
                    SalesOrderItems.SOI_QTY = Convert.ToDecimal(ddr.Cells[4].Text);
                    SalesOrderItems.SOI_INV_QTY = Convert.ToDecimal(ddr.Cells[4].Text);
                    SalesOrderItems.SOI_UNIT_RT = Convert.ToDecimal(ddr.Cells[5].Text);
                    SalesOrderItems.SOI_UNIT_AMT = Convert.ToDecimal(ddr.Cells[6].Text);
                    SalesOrderItems.SOI_DISC_RT = Convert.ToDecimal(ddr.Cells[7].Text);
                    SalesOrderItems.SOI_DISC_AMT = Convert.ToDecimal(ddr.Cells[8].Text);
                    SalesOrderItems.SOI_ITM_TAX_AMT = Convert.ToDecimal(ddr.Cells[9].Text);
                    SalesOrderItems.SOI_TOT_AMT = Convert.ToDecimal(ddr.Cells[10].Text);
                    SalesOrderItems.SOI_PBOOK = ddr.Cells[11].Text;
                    SalesOrderItems.SOI_PB_LVL = ddr.Cells[12].Text;
                    SalesOrderItems.SOI_PB_PRICE = 0;
                    SalesOrderItems.SOI_SEQ = 0;
                    SalesOrderItems.SOI_ITM_SEQ = 0; //add rukshan 
                   // SalesOrderItems.SOI_ITM_SEQ = Convert.ToInt32((ddr.FindControl("itri_seq_no") as Label).Text);
                    SalesOrderItems.SOI_WARR_PERIOD = 0;
                    SalesOrderItems.SOI_WARR_REMARKS = string.Empty;
                    SalesOrderItems.SOI_IS_PROMO = 0;
                    SalesOrderItems.SOI_PROMO_CD = string.Empty;
                    SalesOrderItems.SOI_ALT_ITM_CD = string.Empty;
                    SalesOrderItems.SOI_ALT_ITM_DESC = string.Empty;
                    SalesOrderItems.SOI_PRINT_STUS = 0;
                    SalesOrderItems.SOI_RES_NO = ddr.Cells[13].Text;
                    SalesOrderItems.SOI_RES_LINE_NO = 0;
                    SalesOrderItems.SOI_JOB_NO = string.Empty;
                    SalesOrderItems.SOI_WARR_BASED = 0;
                    SalesOrderItems.SOI_MERGE_ITM = string.Empty;
                    SalesOrderItems.SOI_JOB_LINE = 0;
                    SalesOrderItems.SOI_OUTLET_DEPT = string.Empty;
                    SalesOrderItems.SOI_TRD_SVC_CHRG = 0;
                    SalesOrderItems.SOI_DIS_SEQ = 0;
                    SalesOrderItems.SOI_DIS_LINE = 0;
                    SalesOrderItems.SOI_DIS_TYPE = string.Empty;
                    SalesOrderItems.SOI_ANAL1 = string.Empty;
                    SalesOrderItems.SOI_ANAL2 = string.Empty;
                    SalesOrderItems.SOI_ANAL3 = string.Empty;
                    SalesOrderItems.SOI_ANAL4 = 1;
                    SalesOrderItems.SOI_ANAL5 = 1;

                    _SalesOrderItemsList.Add(SalesOrderItems);
                }
            }
            #endregion
            List<SalesOrderSer> _SalesOrderSerList = new List<SalesOrderSer>();
            #region SalesOrderSer

            if (gvPopSerial.Rows.Count > 0)
            {
                foreach (GridViewRow ddr in gvPopSerial.Rows)
                {
                    SalesOrderSer _SalesOrderSer = new SalesOrderSer();
                    //_SalesOrderSer.SOSE_SEQ_NO = Convert.ToInt32(newseqno);
                    _SalesOrderSer.SOSE_ITM_LINE = Convert.ToInt32(ddr.Cells[0].Text);
                    //_SalesOrderSer.SOSE_SO_NO = outputopno;
                    _SalesOrderSer.SOSE_ITM_CD = ddr.Cells[1].Text;
                    _SalesOrderSer.SOSE_SER_1 = ddr.Cells[6].Text;
                    _SalesOrderSer.SOSE_REMARKS = string.Empty;
                    _SalesOrderSer.SOSE_SEV_LOC = string.Empty;
                    _SalesOrderSer.SOSE_DEL_LOC = txtdellocation.Text;
                    _SalesOrderSer.SOSE_SER_LINE = 0;
                    _SalesOrderSer.SOSE_SER_2 = string.Empty;
                    _SalesOrderSerList.Add(_SalesOrderSer);
                }
            }
            #endregion

            string _msg = string.Empty;
            Int32 _result = CHNLSVC.Sales.SaveSalesOrder(SalesOrder, mastAutoNo, _SalesOrderItemsList, _SalesOrderSerList, out _msg);
            if (_result > 0)
            {
                string msg = "Successfully created sales order- " + _msg;
                DisplayMessage(msg, 3);

            }
            else
            {
                DisplayMessage(_msg, 4);
            }
        }

        private void SessionClear()
        {
            try
            {
                Session["SOSEQNO"] = null;
                Session["SONO"] = null;
                Session["CUSCOM"] = null;
                Session["CUSLOC"] = null;
                Session["CUSHASCOMPANY"] = null;
                Session["WARRANTY"] = null;

                Session["DCUSCODE"] = null;
                Session["DCUSNAME"] = null;
                Session["DTOWN"] = null;
                Session["DTOWNNAME"] = null;
                Session["DCUSADD1"] = null;
                Session["DCUSADD2"] = null;
                Session["DCUSLOC"] = null;

                Session["SOSEQNO"] = null;
                Session["RSEQNO"] = null;

                ViewState["POItemList"] = null;
                ViewState["POItemDel"] = null;
                Session["PRN_No"] = null;
                Session["_BQty"] = null;
                Session["PoQty"] = null;
                Session["_ItemLine"] = null;
                ViewState["PurchaseReqList"] = null;
                ViewState["InventoryRequestItem"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void Clear()
        {
            try
            {
                txtdocrefno.Text = string.Empty;
                txtManualRefNo.Text = string.Empty;
                txtInvoiceNo.Text = string.Empty;
                txtCustomer.Text = string.Empty;
                txtNIC.Text = string.Empty;
                txtMobile.Text = string.Empty;
                //txtLoyalty.Text = string.Empty;
                cmbTitle.SelectedIndex = 0;
                txtCusName.Text = string.Empty;
                txtAddress1.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtlocation.Text = string.Empty;
                lblAccountBalance.Text = string.Empty;
                lblAvailableCredit.Text = string.Empty;
                chkTaxPayable.Checked = false;
                lblSVatStatus.Text = string.Empty;
                lblVatExemptStatus.Text = string.Empty;
                txtPoNo.Text = string.Empty;
                //txtExecutive.Text = string.Empty;
                chkQuotation.Checked = false;
                txtQuotation.Text = string.Empty;
                txtPromotor.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
                txtItem.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;
                txtUnitAmt.Text = string.Empty;
                txtDisRate.Text = string.Empty;
                txtDisAmt.Text = string.Empty;
                txtTaxAmt.Text = string.Empty;
                txtLineTotAmt.Text = string.Empty;
                txtresno.Text = string.Empty;
                lblItemDescription.Text = string.Empty;
                lblItemModel.Text = string.Empty;
                lblItemBrand.Text = string.Empty;
                lblItemSerialStatus.Text = string.Empty;

                SessionClear();

                ViewState["ITEMSTABLE"] = null;
                DataTable dtitems = new DataTable();
                dtitems.Columns.AddRange(new DataColumn[15] { new DataColumn("soi_itm_line"), new DataColumn("soi_itm_cd"), new DataColumn("description"), new DataColumn("soi_itm_stus"), new DataColumn("soi_qty"), new DataColumn("soi_unit_rt"), new DataColumn("soi_unit_amt"), new DataColumn("soi_disc_rt"), new DataColumn("soi_disc_amt"), new DataColumn("soi_itm_tax_amt"), new DataColumn("soi_tot_amt"), new DataColumn("soi_pbook"), new DataColumn("soi_pb_lvl"), new DataColumn("soi_res_no"), new DataColumn("itri_seq_no") });
                ViewState["ITEMSTABLE"] = dtitems;
                this.BindItemsGrid();

                ViewState["SERIALSTABLE"] = null;
                DataTable dtserials = new DataTable();
                dtserials.Columns.AddRange(new DataColumn[9] { new DataColumn("sose_itm_line"), new DataColumn("sose_itm_cd"), new DataColumn("Model"), new DataColumn("Status"), new DataColumn("StatusDesc"), new DataColumn("Qty"), new DataColumn("sose_ser_1"), new DataColumn("sose_ser_2"), new DataColumn("Warranty") });
                ViewState["SERIALSTABLE"] = dtitems;
                this.BindSerialsGrid();

                grvwarehouseitms.DataSource = null;
                grvwarehouseitms.DataBind();

                cmbTechnician.SelectedIndex = 0;
                cmbExecutive.SelectedIndex = 0;
                cmbLevel.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
                cmbInvType.SelectedIndex = 0;
                cmbBook.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
                Session["_SerialSearchType"] = null;
                lblordstus.Text = string.Empty;

                txtcurrency.Text = "LKR";
                lblcurrency.Text = "SRI LANKA RUPEE";
                Session["CUSPOPUP"] = null;
                txtQty.ReadOnly = false;
                Session["STUS"] = null;

                btnSave.CssClass = "buttonUndocolor";
                btnSave.OnClientClick = "ConfirmPlaceOrder();";

                lbtnapprove.CssClass = "buttonUndocolor";
                lbtnapprove.OnClientClick = "ConfirmApproveOrder();";

                lbtnreject.CssClass = "buttonUndocolor";
                lbtnreject.OnClientClick = "ConfirmRejectOrder();";

                lbtncancel.CssClass = "buttonUndocolor";
                lbtncancel.OnClientClick = "ConfirmCancelOrder();";

                txtDisRate.Text = DoFormat(0);
                txtDisAmt.Text = DoFormat(0);
                txtTaxAmt.Text = DoFormat(0);
                txtLineTotAmt.Text = DoFormat(0);

                lblGrndSubTotal.Text = DoFormat(0);
                lblGrndDiscount.Text = DoFormat(0);
                lblGrndAfterDiscount.Text = DoFormat(0);
                lblGrndTax.Text = DoFormat(0);
                lblGrndTotalAmount.Text = DoFormat(0);
                txtQuotation.ReadOnly = true;
                lblAccountBalance.Text = DoFormat(0);
                lblAvailableCredit.Text = DoFormat(0);
                cmbBook.SelectedValue = "ABANS";
                cmbLevel.SelectedItem.Text = "A";
                cmbInvType.SelectedItem.Text = "CS";
                txtQty.Text = DoFormat(1);
                txtUnitPrice.Text = DoFormat(0);
                txtUnitAmt.Text = DoFormat(0);

                Session["SERIAL_STATUS"] = null;
                _CashGeneralEntiryDiscount = new List<CashGeneralEntiryDiscountDef>();

                txtdellocation.Text = string.Empty;
                txtdelcuscode.Text = string.Empty;
                txtdelname.Text = string.Empty;
                txtdelad1.Text = string.Empty;
                txtdelad2.Text = string.Empty;

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16012))
                {
                    lbtncancel.Enabled = false;
                    lbtncancel.CssClass = "buttoncolor";
                    lbtncancel.OnClientClick = "return Enable();";
                    lbtncancel.ToolTip = "You dont have permission to cancel .Permission code : 16012";
                }
                else
                {
                    lbtncancel.Enabled = true;
                    lbtncancel.CssClass = "buttonUndocolor";
                    lbtncancel.OnClientClick = "ConfirmCancelOrder();";
                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16013))
                {
                    lbtnreject.Enabled = false;
                    lbtnreject.CssClass = "buttoncolor";
                    lbtnreject.OnClientClick = "return Enable();";
                    lbtnreject.ToolTip = "You dont have permission to reject .Permission code : 16013";
                }
                else
                {
                    lbtnreject.Enabled = true;
                    lbtnreject.CssClass = "buttonUndocolor";
                    lbtnreject.OnClientClick = "ConfirmRejectOrder();";
                }
                dvhiddendel.Visible = true;
                cmbLevel.SelectedValue = "A";
                cmbInvType.SelectedValue = "CS";
            }
            catch 
            {
                
            }
        }
        private void SaveAll()
        {
            try
            {
                saveNew();
               // SaveOrderHeaderWithDetails();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnsupplier_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "421";
                BindUCtrlDDLDataCustom(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();

                grvwarehouseitms.DataSource = null;
                grvwarehouseitms.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnapprove_Click(object sender, EventArgs e)
        {
            if (txtapprove.Value == "Yes")
            {
                try
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16011))
                    {
                        string msg = "You dont have permission to approve .Permission code : 16011";
                        DisplayMessage(msg, 1);
                        return;
                    }
                    
                    string ordstatus = (string)Session["STATUS"];
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);
                     
                        lbtnsupplier.Focus();
                        return;
                    }

                    if (ordstatus == "A")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already approved !!!')", true);
                       
                        return;
                    }

                    _userid = (string)Session["UserID"];

                    SalesOrderHeader _SalesOrder = new SalesOrderHeader();

                    _SalesOrder.SOH_STUS = "A";
                    _SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
                    _SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
                    _SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
                    _SalesOrder.SOH_MOD_BY= _userid;
                    _SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
                    string error = string.Empty;
                    Int32 outputresultapprove = CHNLSVC.Sales.UpdateSOStatus(_SalesOrder, out error);

                    if (outputresultapprove == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully approved !!!')", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            if (txtcancel.Value == "Yes")
            {
                try
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16012))
                    {
                        string msg= "You dont have permission to cancel .Permission code : 16012";
                        DisplayMessage(msg, 1);
                        return;
                    }
                   


                    string ordstatus = (string)Session["STATUS"];
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);
                        lbtnsupplier.Focus();
                        return;
                    }

                    if (ordstatus == "C")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already cancelled !!!')", true);

                        return;
                    }

                    _userid = (string)Session["UserID"];

                    SalesOrderHeader _SalesOrder = new SalesOrderHeader();

                    _SalesOrder.SOH_STUS = "C";
                    _SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
                    _SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
                    _SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
                    _SalesOrder.SOH_MOD_BY = _userid;
                    _SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
                    string error = string.Empty;
                    Int32 outputresultapprove = CHNLSVC.Sales.UpdateSOStatus(_SalesOrder, out error);

                    if (outputresultapprove == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully cancelled !!!')", true);

                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        protected void lbtnreject_Click(object sender, EventArgs e)
        {
            if (txtreject.Value == "Yes")
            {
                try
                {


                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16013))
                    {
                        string msg = "You dont have permission to reject .Permission code : 16013";
                        DisplayMessage(msg, 1);
                        return;
                    }

                    string ordstatus = (string)Session["STATUS"];
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);

                        lbtnsupplier.Focus();
                        return;
                    }

                    if (ordstatus == "R")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already rejected !!!')", true);

                        return;
                    }

                    _userid = (string)Session["UserID"];

                    SalesOrderHeader _SalesOrder = new SalesOrderHeader();

                    _SalesOrder.SOH_STUS = "R";
                    _SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
                    _SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
                    _SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
                    _SalesOrder.SOH_MOD_BY = _userid;
                    _SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
                    string error = string.Empty;
                    Int32 outputresultapprove = CHNLSVC.Sales.UpdateSOStatus(_SalesOrder,out error);

                    if (outputresultapprove == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully rejected !!!')", true);

                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
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
        protected void InsertToItemsGrid()
        {
            try
            {

                Int32 gridline = gvInvoiceItem.Rows.Count;

                //foreach (GridViewRow row in gvInvoiceItem.Rows)
                //{
                //    if (row.RowIndex == gvInvoiceItem.SelectedIndex)
                //    {
                //        if (row.BackColor.Name != "0")
                //        {
                //            DataTable dtdup = ViewState["ITEMSTABLE"] as DataTable;
                //            dtdup.Rows[row.RowIndex].Delete();
                //            dtdup.AcceptChanges();
                //            ViewState["ITEMSTABLE"] = dtdup;
                //            BindItemsGrid();
                //        }
                //    }
                //}

                string _statustxt = string.Empty;

                DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(cmbStatus.SelectedValue);

                if (dtstatustx.Rows.Count > 0)
                {
                    foreach (DataRow ddr2 in dtstatustx.Rows)
                    {
                        _statustxt = ddr2[0].ToString();
                    }
                } 

                string _seqnoforrequest = (string)Session["RSEQNO"];

                DataTable dt = (DataTable)ViewState["ITEMSTABLE"];
                dt.Rows.Add(gridline + 1, txtItem.Text.Trim(), lblItemDescription.Text.Trim(), _statustxt, txtQty.Text.Trim(), txtUnitPrice.Text.Trim(), txtUnitAmt.Text.Trim(), txtDisRate.Text.Trim(), txtDisAmt.Text.Trim(), txtTaxAmt.Text.Trim(), txtLineTotAmt.Text.Trim(), cmbBook.SelectedValue, cmbLevel.SelectedValue, txtresno.Text.Trim(), _seqnoforrequest);
                ViewState["ITEMSTABLE"] = dt;

                //uniqueitems = RemoveDuplicateRows(dt, "soi_itm_cd");

                gvInvoiceItem.DataSource = dt;
                gvInvoiceItem.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void InsertToSerialsGrid()
        {
            try
            {
                string warranty = (string)Session["WARRANTY"];
                DataTable dt1 = (DataTable)ViewState["SERIALSTABLE"];
                dt1.Rows.Add(gvPopSerial.Rows.Count + 1, txtItem.Text.Trim(), lblItemModel.Text.Trim(), cmbStatus.SelectedValue,cmbStatus.SelectedValue, txtQty.Text.Trim(), txtSerialNo.Text.Trim(), string.Empty, warranty);
                ViewState["SERIALSTABLE"] = dt1;

                uniqueitems = RemoveDuplicateRows(dt1, "sose_ser_1");

                gvPopSerial.DataSource = uniqueitems;
                gvPopSerial.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private decimal TaxCalculationActualCost(string _com, string _item, string _status, decimal _UnitPrice, string _supTaxCate, decimal _actTaxVal)
        {
            decimal _totNBT = 0;
            decimal _NBT = 0;
            decimal _oTax = 0;
            decimal _claimTaxRt = 0;

            _actTaxVal = 0;

            List<MasterItemTax> _taxs = new List<MasterItemTax>();
            _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);
            var _Tax = from _itm in _taxs
                       select _itm;

            MasterItemTaxClaim _claimTax = new MasterItemTaxClaim();
            _claimTax = CHNLSVC.Sales.GetTaxClaimDet(_com, _item, _supTaxCate);


            foreach (MasterItemTax _one in _Tax)
            {
                if (_one.Mict_tax_cd == "NBT")
                {
                    _NBT = _UnitPrice * _one.Mict_tax_rate / 100;
                    _actTaxVal = _actTaxVal + _NBT;
                    _totNBT = _totNBT + _NBT;
                }
                //_TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
            }

            foreach (MasterItemTax _two in _Tax)
            {
                if (_two.Mict_tax_cd != "NBT")
                {
                    if (_claimTax != null)
                    {
                        _claimTaxRt = _two.Mict_tax_rate - _claimTax.Mic_claim;
                    }
                    else
                    {
                        _claimTaxRt = _two.Mict_tax_rate;
                    }

                    _oTax = (_UnitPrice + _totNBT) * _claimTaxRt / 100;
                    _actTaxVal = _actTaxVal + _oTax;

                }
            }


            return _actTaxVal;
        }

        protected void lbtnadditems_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkQuotation.Checked)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allow to add additional items for the selected quotation !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text)) return;
                if (string.IsNullOrEmpty(txtQty.Text)) return;

                #region Check Customer has promotion voucher avoid the discount :: Chamal 04/Jul/2014

                if (!string.IsNullOrEmpty(txtCustomer.Text))
                {
                    if (txtCustomer.Text != "CASH")
                    {
                        if ((string.IsNullOrEmpty(txtDisRate.Text) && string.IsNullOrEmpty(txtDisAmt.Text)))
                        {
                            CashGeneralEntiryDiscountDef _discVou = CHNLSVC.Sales.CheckCustHaveDiscountPromoVoucher(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCustomer.Text, Convert.ToDateTime(txtdate.Text), cmbBook.Text, cmbLevel.Text, txtItem.Text, string.Empty, txtNIC.Text, txtMobile.Text);
                            if (_discVou != null)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Promotion voucher discount available for this item  !!!')", true);

                                return;
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtDisRate.Text) <= 0 && Convert.ToDecimal(txtDisAmt.Text) <= 0)
                            {
                                CashGeneralEntiryDiscountDef _discVou = CHNLSVC.Sales.CheckCustHaveDiscountPromoVoucher(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCustomer.Text, Convert.ToDateTime(txtdate.Text), cmbBook.Text, cmbLevel.Text, txtItem.Text, string.Empty, txtNIC.Text, txtMobile.Text);
                                if (_discVou != null)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Promotion voucher discount available for this item !!!')", true);

                                    return;
                                }
                            }
                        }
                    }
                }

                #endregion Check Customer has promotion voucher avoid the discount :: Chamal 04/Jul/2014

                if (!string.IsNullOrEmpty(SSPromotionCode))
                {
                    List<PriceDetailRef> _promoList = CHNLSVC.Sales.GetPriceByPromoCD(SSPromotionCode);
                    if (_promoList == null && _promoList.Count <= 0)
                    {
                        return;
                    }
                    else
                    {
                        decimal qty = _promoList[0].Sapd_qty_to;
                        List<InvoiceItem> _alredyAddList = (from _res in _invoiceItemList
                                                            where _res.Sad_itm_cd == txtItem.Text.Trim() && _res.Sad_itm_stus == cmbStatus.Text
                                                            select _res).ToList<InvoiceItem>();
                        if (_alredyAddList != null)
                        {
                            qty = qty + _alredyAddList.Count;
                        }
                        if (Convert.ToDecimal(txtQty.Text) > qty)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Order qty exceed promotion allow qty !!!')", true);

                            return;
                        }
                    }
                }

                List<MasterItemComponent> _com = CHNLSVC.Inventory.GetItemComponents(txtItem.Text.Trim());
                if (_com != null && _com.Count > 0)
                {
                    foreach (MasterItemComponent _itmCom in _com)
                    {
                        MasterItem _temItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itmCom.ComponentItem.Mi_cd);
                        if (_isRegistrationMandatory)
                        {
                            if (_temItm.Mi_need_reg)
                            {
                                _isNeedRegistrationReciept = true;
                            }
                        }
                    }
                }
                else
                {
                    MasterItem _temItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                    if (_isRegistrationMandatory)
                    {
                        if (_temItm.Mi_need_reg)
                        {
                            _isNeedRegistrationReciept = true;
                        }
                    }
                }

                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    if (_priceDetailRef[0].Sapd_customer_cd == txtCustomer.Text.Trim())
                    {
                        txtCustomer.ReadOnly = true;
                        lbtncode.Enabled = false;
                    }
                }
                AddItemDisableCustomer(true);


                bool isvalidateitm = ValidateSOItems();
                if (isvalidateitm == false)
                {
                    return;
                }

                InsertToItemsGrid();
                if (!string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                {
                    InsertToSerialsGrid();
                }

                foreach (GridViewRow row in gvInvoiceItem.Rows)
                {
                    CalculateGrandTotalNew(Convert.ToDecimal(row.Cells[4].Text), Convert.ToDecimal(row.Cells[5].Text), Convert.ToDecimal(row.Cells[8].Text), Convert.ToDecimal(row.Cells[9].Text), true);
                }

                #region POITEMS
                _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
                _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
                if (ViewState["POItemList"] == null)
                {
                    _POItemList = new List<PurchaseOrderDetail>();
                    _POItemDel = new List<PurchaseOrderDelivery>();
                }

                Boolean _isVatClaim = false;
                string _suppTaxCate = "";


                var currrange = (from cur in _POItemList
                                 where cur.Pod_itm_cd == txtItem.Text.Trim() && cur.Pod_unit_price == Convert.ToDecimal(txtUnitPrice.Text) && cur.Pod_itm_stus == cmbStatus.SelectedValue.Trim()
                                 select cur).ToList();

                //if (currrange.Count > 0)// ||currrange !=null)
                //{
                //    ErrorMsgPOrder("Selected item already exsist with same price.");
                //    txtItem.Focus();
                //    return;
                //}

                PurchaseOrderDetail _tmpPoDetails = new PurchaseOrderDetail();
                PurchaseOrderDelivery _tmpPoDel = new PurchaseOrderDelivery();
                MasterItem _tmpItem = new MasterItem();

                _tmpItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());

                if ((_POItemList.Count > 0))
                {
                    var max_Query =
                 (from tab1 in _POItemList
                  select tab1.Pod_line_no).Max();

                    _lineNo = max_Query;
                }
                else
                {
                    _lineNo = 0;
                }

                _supDet = new MasterBusinessEntity();
                _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim(), null, null, "C");

                if (_supDet != null)
                {
                    _isVatClaim = _supDet.Mbe_is_tax;
                    _suppTaxCate = _supDet.Mbe_cate;
                }

                decimal _taxForActual = 0;

                if (_tmpItem != null)
                {
                    _lineNo = _lineNo + 1;

                    if (string.IsNullOrEmpty(_suppTaxCate))
                    {
                        _tmpPoDetails.Pod_act_unit_price = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) - Convert.ToDecimal(txtDisAmt.Text) + Convert.ToDecimal(txtTaxAmt.Text)) / Convert.ToDecimal(txtQty.Text);
                    }
                    else
                    {
                        decimal _unitVal = Convert.ToDecimal(txtUnitPrice.Text);
                        decimal _qty = Convert.ToDecimal(txtQty.Text);
                        decimal _amt = _unitVal * _qty;
                        decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                        decimal _disAmount = _amt * _disRate / 100;

                        _taxForActual = TaxCalculationActualCost(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.Trim(), _amt - _disAmount, _suppTaxCate, 0);
                        _tmpPoDetails.Pod_act_unit_price = ((Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) - Convert.ToDecimal(txtDisAmt.Text)) + _taxForActual) / Convert.ToDecimal(txtQty.Text);
                    }
                    _tmpPoDetails.Pod_dis_amt = Convert.ToDecimal(txtDisAmt.Text);
                    _tmpPoDetails.Pod_dis_rt = Convert.ToDecimal(txtDisRate.Text);
                    _tmpPoDetails.Pod_grn_bal = Convert.ToDecimal(txtQty.Text);
                    _tmpPoDetails.Pod_item_desc = _tmpItem.Mi_shortdesc;
                    _tmpPoDetails.Pod_itm_cd = txtItem.Text.Trim();
                    if (cmbStatus.SelectedItem.Text == "CONS")
                    {
                        _tmpPoDetails.Pod_itm_stus = "CONS";
                    }
                    else
                    {
                        _tmpPoDetails.Pod_itm_stus = cmbStatus.SelectedValue.Trim();
                    }

                    _tmpPoDetails.Pod_itm_tp = _tmpItem.Mi_itm_tp;
                    _tmpPoDetails.Pod_kit_itm_cd = "";
                    _tmpPoDetails.Pod_kit_line_no = 0;
                    _tmpPoDetails.Pod_lc_bal = 0;
                    _tmpPoDetails.Pod_line_amt = Convert.ToDecimal(txtLineTotAmt.Text);
                    _tmpPoDetails.Pod_line_no = _lineNo;
                    _tmpPoDetails.Pod_line_tax = Convert.ToDecimal(txtTaxAmt.Text);
                    _tmpPoDetails.Pod_line_val = Convert.ToDecimal(txtUnitAmt.Text);
                    _tmpPoDetails.Pod_nbt = 0;
                    _tmpPoDetails.Pod_nbt_before = 0;
                    _tmpPoDetails.Pod_pi_bal = 0;
                    _tmpPoDetails.Pod_qty = Convert.ToDecimal(txtQty.Text);
                    _tmpPoDetails.Pod_ref_no = txtCustomer.Text;
                    _tmpPoDetails.Pod_seq_no = 12;
                    _tmpPoDetails.Pod_si_bal = 0;
                    _tmpPoDetails.Pod_tot_tax_before = 0;
                    _tmpPoDetails.Pod_unit_price = Convert.ToDecimal(txtUnitPrice.Text);
                    _tmpPoDetails.Pod_uom = "";
                    _tmpPoDetails.Pod_vat = 0;
                    _tmpPoDetails.Pod_vat_before = 0;


                    _POItemList.Add(_tmpPoDetails);

                    if (_POItemDel.Count > 0)
                    {
                        var result = (from rs in _POItemDel where rs.Podi_line_no == _lineNo select rs.Podi_del_line_no).ToList();
                        if (result != null && result.Count > 0)
                        {
                            _delLineNo = Convert.ToInt32(result.Max());
                        }
                        else _delLineNo = 0;
                    }
                    else
                    {
                        _delLineNo = 0;
                    }

                    ViewState["POItemList"] = _POItemList;

                    _delLineNo = _delLineNo + 1;
                    _tmpPoDel.Podi_seq_no = 12;
                    _tmpPoDel.Podi_line_no = _lineNo;
                    _tmpPoDel.Podi_del_line_no = _delLineNo;
                    _tmpPoDel.Podi_loca = Session["UserDefLoca"].ToString();
                    _tmpPoDel.Podi_itm_cd = txtItem.Text.Trim();
                    if (cmbStatus.SelectedItem.Text == "CONS")
                    {
                        _tmpPoDel.Podi_itm_stus = "CONS";
                    }
                    else
                    {
                        _tmpPoDel.Podi_itm_stus = cmbStatus.SelectedValue.Trim();
                    }


                    _tmpPoDel.Podi_qty = Convert.ToDecimal(txtQty.Text);
                    _tmpPoDel.Podi_bal_qty = Convert.ToDecimal(txtQty.Text);

                    _POItemDel.Add(_tmpPoDel);

                    ViewState["POItemDel"] = _POItemDel;

                    Session["PRN_No"] = string.Empty;

                    if (Session["PRN_No"].ToString() != "")
                    {
                        decimal oldPoQty = 0;
                        Decimal CurrentQty = Convert.ToDecimal(Session["_BQty"].ToString());
                        string PoQty = Session["PoQty"].ToString();
                        Decimal newQty = Convert.ToDecimal(txtQty.Text);
                        if (CurrentQty < newQty)
                        {
                            txtQty.Text = "";
                            txtUnitPrice.Text = "";
                            txtUnitAmt.Text = "";
                            txtDisAmt.Text = "";
                            txtDisRate.Text = "";
                            txtTaxAmt.Text = "";
                            txtLineTotAmt.Text = "";
                            return;
                        }
                        PurchaseReq _PurchaseReq = new PurchaseReq();
                        _PurchaseReq.Por_act = true;
                        _PurchaseReq.Por_cre_by = Session["UserID"].ToString();
                        _PurchaseReq.Por_cre_dt = System.DateTime.Now;
                        _PurchaseReq.Por_itm_cd = txtItem.Text;
                        _PurchaseReq.Por_itm_stus = cmbStatus.SelectedValue;
                        _PurchaseReq.Por_qty = Convert.ToDecimal(txtQty.Text);
                        _PurchaseReq.Por_req_no = Session["PRN_No"].ToString();
                        string lint = Session["_ItemLine"].ToString();
                        _PurchaseReq.Por_req_line = Convert.ToInt32(lint);
                        _PurchaseReqList.Add(_PurchaseReq);
                        ViewState["PurchaseReqList"] = _PurchaseReqList;

                        InventoryRequestItem _Req = new InventoryRequestItem();
                        _Req.Itri_res_no = Session["PRN_No"].ToString();
                        _Req.Itri_itm_cd = txtItem.Text;
                        _Req.Itri_bqty = CurrentQty - newQty;
                        _Req.Itri_itm_stus = cmbStatus.SelectedValue;
                        if ((PoQty == "") || (PoQty == null))
                        {
                            oldPoQty = 0;
                        }
                        else
                        {
                            oldPoQty = Convert.ToDecimal(PoQty);
                        }
                        _Req.Itri_po_qty = newQty + oldPoQty;

                        _InventoryRequestItem = ViewState["InventoryRequestItem"] as List<InventoryRequestItem>;
                        if (_InventoryRequestItem == null)
                        {
                            _InventoryRequestItem = new List<InventoryRequestItem>();
                        }

                        _InventoryRequestItem.Add(_Req);
                        ViewState["InventoryRequestItem"] = _InventoryRequestItem;
                        Session["_BQty"] = _Req.Itri_bqty;
                    }
                }
                #endregion

                txtSerialNo.Text = string.Empty;
                txtItem.Text = string.Empty;
                cmbStatus.SelectedIndex = 0;
                txtQty.Text = DoFormat(1);
                txtUnitPrice.Text = DoFormat(0);
                txtUnitAmt.Text = DoFormat(0);
                txtDisRate.Text = DoFormat(0);
                txtDisAmt.Text = DoFormat(0);
                txtTaxAmt.Text = DoFormat(0);
                txtLineTotAmt.Text = DoFormat(0);
                txtresno.Text = string.Empty;
                lblItemDescription.Text = string.Empty;
                lblItemModel.Text = string.Empty;
                lblItemBrand.Text = string.Empty;
                lblItemSerialStatus.Text = string.Empty;
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

        private void AddItemDisableCustomer(bool _disable)
        {
            if (_disable == false)
            {
                txtCustomer.Enabled = true;
                btnSearch_NIC.Enabled = true;
                lbtncode.Enabled = true;
                btnSearch_Mobile.Enabled = true;
                txtdate.Enabled = true;
            }
            else
            {
                if (txtCustomer.Text.ToString() != "CASH")
                {
                    txtCustomer.Enabled = false;
                    btnSearch_NIC.Enabled = false;
                    lbtncode.Enabled = false;
                    btnSearch_Mobile.Enabled = false;
                }
                txtdate.Enabled = false;
            }
        }

        protected void gvInvoiceItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string item = e.Row.Cells[1].Text;
                    foreach (LinkButton button in e.Row.Cells[15].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete item " + item + "?')){ return false; };";
                        }
                    }

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvInvoiceItem, "Select$" + e.Row.RowIndex);
                        e.Row.Attributes["style"] = "cursor:pointer";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void gvInvoiceItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Myindex = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["ITEMSTABLE"] as DataTable;
                dt.Rows[Myindex].Delete();
                dt.AcceptChanges();
                ViewState["ITEMSTABLE"] = dt;
                BindItemsGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtNIC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetailsByNIC();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LoadCustomerDetailsByMobile()
        {
            if (string.IsNullOrEmpty(txtMobile.Text)) return;
            _masterBusinessCompany = new MasterBusinessEntity();
            try
            {
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    if (!IsValidMobileOrLandNo(txtMobile.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid mobile !!!')", true);
                        
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMobile.Text, "C");
                 

                    if (!string.IsNullOrEmpty(txtCustomer.Text) && txtCustomer.Text.Trim() != "CASH")
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim(), string.Empty, txtMobile.Text, "C");
                    else
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMobile.Text, "C");
                   // ucPayModes1.Mobile = txtMobile.Text.Trim();

                }
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        LoadTaxDetail(_masterBusinessCompany);
                        SetCustomerAndDeliveryDetails(false, null);
                        ViewCustomerAccountDetail(txtCustomer.Text);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This customer already inactive.Please contact accounts dept !!!')", true);

                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }
                }
                else
                {
                    GroupBussinessEntity _grupProf = GetbyMobGrup(txtMobile.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;
                        DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                        if (_table != null && _table.Rows.Count > 0)
                        {
                            var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                            if (_isAvailable == null || _isAvailable.Count <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Customer is not allow for enter transaction under selected order type !!!')", true);

                                ClearCustomer(true);
                                txtCustomer.Focus();
                                return;
                            }
                        }
                        else if (cmbInvType.Text != "CS")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected Customer is not allow for enter transaction under selected order type !!!')", true);

                            ClearCustomer(true);
                            txtCustomer.Focus();
                            return;
                        }
                    }
                    else
                    {
                        _isGroup = false;
                    }
                }

                //txtLoyalty.Text = ReturnLoyaltyNo();
                //txtLoyalty_TextChanged(null, null);
                EnableDisableCustomer();
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

        public GroupBussinessEntity GetbyMobGrup(string mobNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, mobNo);
        }

        protected void txtMobile_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetailsByMobile();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnloc_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "1";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void CalculateGrandTotal(decimal _qty, decimal _uprice, decimal _discount, decimal _tax, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndSubTotal = GrndSubTotal + Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount + Convert.ToDecimal(_discount);
                GrndTax = GrndTax + Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = (Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = (Convert.ToString(GrndDiscount));
                lblGrndTax.Text = (Convert.ToString(GrndTax));
            }
            else//--
            {
                GrndSubTotal = GrndSubTotal - Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount - Convert.ToDecimal(_discount);
                GrndTax = GrndTax - Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = (Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = (Convert.ToString(GrndDiscount));
                lblGrndTax.Text = (Convert.ToString(GrndTax));
            }

            lblGrndAfterDiscount.Text = (Convert.ToString(GrndSubTotal - GrndDiscount));
            if (_invoiceItemList != null || _invoiceItemList.Count > 0)
            {
                lblGrndTotalAmount.Text = (Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt)));
            }
            else
            {
                lblGrndTotalAmount.Text = (Convert.ToString("0"));
            }
        }

        private void CalculateGrandTotalNew(Decimal _qty, Decimal _uprice, Decimal _discount, Decimal _tax, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndSubTotal = GrndSubTotal + Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount + Convert.ToDecimal(_discount);
                GrndTax = GrndTax + Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = DoFormat(GrndSubTotal);
                lblGrndDiscount.Text = DoFormat(GrndDiscount);
                lblGrndTax.Text = DoFormat(GrndTax);
            }
            else//--
            {
                GrndSubTotal = GrndSubTotal - Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount - Convert.ToDecimal(_discount);
                GrndTax = GrndTax - Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = DoFormat(GrndSubTotal);
                lblGrndDiscount.Text = DoFormat(GrndDiscount);
                lblGrndTax.Text = DoFormat(GrndTax);
            }

            lblGrndAfterDiscount.Text = (Convert.ToString(GrndSubTotal - GrndDiscount));
            lblGrndAfterDiscount.Text = DoFormat(GrndSubTotal - GrndDiscount);
            //if (_invoiceItemList != null || _invoiceItemList.Count > 0)
            //{
            //    lblGrndTotalAmount.Text = (Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt)));
            //}
            //else
            //{
            //    lblGrndTotalAmount.Text = (Convert.ToString("0"));
            //}

            Decimal grand = GrndSubTotal + GrndTax;
            lblGrndTotalAmount.Text = DoFormat(grand);
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
        protected void txtQuotation_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuotation.Text.Trim())) return;
            try
            {
                QuotationHeader _saveHdr = CHNLSVC.Sales.Get_Quotation_HDR(txtQuotation.Text);
                if (_saveHdr != null && !string.IsNullOrEmpty(_saveHdr.Qh_no))
                {
                    if (_saveHdr.Qh_stus == "D")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('This quotation is already used !!!');", true);
                        txtQuotation.Text = "";
                        return;
                    }
                    txtCustomer.Text = _saveHdr.Qh_del_cuscd;
                    txtCustomer_TextChanged(null, null);
                }

                _invoiceItemList = CHNLSVC.Sales.GetQuotationDetail(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtQuotation.Text.Trim(), Convert.ToDateTime(txtdate.Text));
                //get serial
                List<QuotationSerial> _serialList = CHNLSVC.Sales.GetQuoSerials(txtQuotation.Text.Trim());
                if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                {
                    _invoiceItemList.ForEach(X => X.Sad_job_line = X.Sad_itm_line);

                    #region Check For Inventory Balance if Delivered Now

                    if (1 == 1)
                    {
                        bool _isPricelevelallowforDOanystatus = false;
                        string _balanceexceedList = string.Empty;
                        foreach (InvoiceItem _itm in _invoiceItemList)
                        {
                            //------------------------------------------------------------------------------------------------
                            if (!string.IsNullOrEmpty(_itm.Sad_pbook) && !string.IsNullOrEmpty(_itm.Sad_pb_lvl))
                            {
                                List<PriceBookLevelRef> _lvl = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _itm.Sad_pbook, _itm.Sad_pb_lvl);
                                if (_lvl != null)
                                    if (_lvl.Count > 0)
                                    {
                                        var _bool = from _l in _lvl where _l.Sapl_chk_st_tp == true select _l.Sapl_chk_st_tp;
                                        if (_bool != null)
                                            if (_bool.Count() > 0)
                                                _isPricelevelallowforDOanystatus = false;
                                            else
                                                _isPricelevelallowforDOanystatus = true;
                                        else
                                            _isPricelevelallowforDOanystatus = true;
                                    }
                            }
                            else
                                _isPricelevelallowforDOanystatus = true;

                            //------------------------------------------------------------------------------------------------
                            decimal _pickQty = 0;
                            if (_isPricelevelallowforDOanystatus)
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _itm.Sad_itm_cd).ToList().Select(x => x.Sad_qty).Sum();
                            else
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _itm.Sad_itm_cd && x.Mi_itm_stus == _itm.Mi_itm_stus).ToList().Select(x => x.Sad_qty).Sum();

                            _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Sad_itm_cd, _itm.Mi_itm_stus);

                            if (_inventoryLocation != null && _inventoryLocation.Count > 0)
                            {
                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                if (_pickQty > _invBal)

                                    if (string.IsNullOrEmpty(_balanceexceedList))
                                        _balanceexceedList = _itm.Sad_itm_cd;
                                    else
                                        _balanceexceedList = ", " + _itm.Sad_itm_cd;
                            }
                            else
                                if (string.IsNullOrEmpty(_balanceexceedList))
                                    _balanceexceedList = _itm.Sad_itm_cd;
                                else
                                    _balanceexceedList = ", " + _itm.Sad_itm_cd;
                        }

                        if (!string.IsNullOrEmpty(_balanceexceedList))
                        {
                            _invoiceItemList = new List<InvoiceItem>();
                            ScanSerialList = new List<ReptPickSerials>();
                            InvoiceSerialList = new List<InvoiceSerial>();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Item(s) inventory balance exceeds !!!');", true);
                            return;
                        }
                        //InvItm_SerialAdd.Visible = true;
                    }

                    #endregion Check For Inventory Balance if Delivered Now

                    GrndSubTotal = 0;
                    GrndDiscount = 0;
                    GrndTax = 0;

                    foreach (InvoiceItem itm in _invoiceItemList)
                    {
                        CalculateGrandTotalNew(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true);
                        _lineNo += 1;
                        SSCombineLine += 1;
                    }

                    var _invlst = new BindingList<InvoiceItem>(_invoiceItemList);
                    DataTable dt = new DataTable();
                    dt = _invlst.ToDataTable();

                    DataTable dtInvitems = new DataTable();
                    dtInvitems.Columns.AddRange(new DataColumn[15] { new DataColumn("soi_itm_line"), new DataColumn("soi_itm_cd"), new DataColumn("description"), new DataColumn("soi_itm_stus"), new DataColumn("soi_qty"), new DataColumn("soi_unit_rt"), new DataColumn("soi_unit_amt"), new DataColumn("soi_disc_rt"), new DataColumn("soi_disc_amt"), new DataColumn("soi_itm_tax_amt"), new DataColumn("soi_tot_amt"), new DataColumn("soi_pbook"), new DataColumn("soi_pb_lvl"), new DataColumn("soi_res_no"), new DataColumn("itri_seq_no") });

                    foreach (DataRow DDR in dt.Rows)
                    {
                        dtInvitems.Rows.Add(DDR["Sad_itm_line"].ToString(), DDR["Sad_itm_cd"].ToString(), DDR["Mi_longdesc"].ToString(), DDR["Sad_itm_stus"].ToString(), DDR["Sad_qty"].ToString(), DDR["Sad_unit_rt"].ToString(), DDR["Sad_unit_amt"].ToString(), DDR["sad_disc_rt"].ToString(), DDR["sad_disc_amt"].ToString(), DDR["sad_itm_tax_amt"].ToString(), DDR["sad_tot_amt"].ToString(), DDR["sad_pbook"].ToString(), DDR["sad_pb_lvl"].ToString(), DDR["sad_res_no"].ToString(), DDR["Sad_seq_no"].ToString());
                    }

                    //gvInvoiceItem.DataSource = setItemDescriptions(_invoiceItemList);
                    //gvInvoiceItem.DataBind();
                    List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                    oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                    if (dtInvitems != null)
                    {
                        foreach (DataRow itemSer in dtInvitems.Rows)
                        {
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                itemSer["soi_itm_stus"] = oItemStaus.Find(x => x.Mis_cd == itemSer["soi_itm_stus"].ToString()).Mis_desc;
                            }
                        }
                    }
                    gvInvoiceItem.DataSource = dtInvitems;
                    gvInvoiceItem.DataBind();

                    //for (int i = 0; i < gvInvoiceItem.Rows.Count; i++)
                    //{
                    //    GridViewRow dr = gvInvoiceItem.Rows[i];
                    //    LinkButton btnAddSerials = dr.FindControl("btnAddSerials") as LinkButton;
                    //    btnAddSerials.Visible = true;
                    //}

                    ScanSerialList = new List<ReptPickSerials>();
                    string _defbin = CHNLSVC.Inventory.GetDefaultBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                    foreach (InvoiceItem _itm in _invoiceItemList)
                    {
                        MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
                        if (_item.Mi_is_ser1 == 0)
                        {
                            List<ReptPickSerials> _nonserLst = null;
                            if (IsPriceLevelAllowDoAnyStatus == false)
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Sad_itm_cd, _itm.Sad_itm_stus, Convert.ToDecimal(_itm.Sad_qty));
                            else
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Sad_itm_cd, string.Empty, Convert.ToDecimal(_itm.Sad_qty));
                            _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(-100));
                            _nonserLst.ForEach(x => x.Tus_base_itm_line = _itm.Sad_itm_line);
                            _nonserLst.ForEach(x => x.Tus_usrseq_no = -100);
                            _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                            _nonserLst.ForEach(x => x.Tus_serial_id = string.Empty);
                            _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                            _nonserLst.ForEach(x => x.Tus_new_status = string.Empty);
                            ScanSerialList.AddRange(_nonserLst);
                        }
                        else if (_item.Mi_is_ser1 == -1)
                        {
                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                            _reptPickSerial_.Tus_base_doc_no = Convert.ToString(-100);
                            _reptPickSerial_.Tus_base_itm_line = _itm.Sad_itm_line;
                            _reptPickSerial_.Tus_bin = _defbin;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_cre_dt = DateTime.Now;
                            _reptPickSerial_.Tus_cross_batchline = 0;
                            _reptPickSerial_.Tus_cross_itemline = 0;
                            _reptPickSerial_.Tus_cross_seqno = 0;
                            _reptPickSerial_.Tus_cross_serline = 0;
                            _reptPickSerial_.Tus_doc_dt = Convert.ToDateTime(txtdate.Text);
                            _reptPickSerial_.Tus_doc_no = string.Empty;
                            _reptPickSerial_.Tus_exist_grncom = string.Empty;
                            _reptPickSerial_.Tus_isapp = 1;
                            _reptPickSerial_.Tus_iscovernote = 1;
                            _reptPickSerial_.Tus_itm_brand = _itm.Mi_brand;
                            _reptPickSerial_.Tus_itm_cd = _itm.Sad_itm_cd;
                            _reptPickSerial_.Tus_itm_desc = _itm.Mi_longdesc;
                            _reptPickSerial_.Tus_itm_line = 0;
                            _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                            _reptPickSerial_.Tus_itm_stus = _itm.Sad_itm_stus;
                            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                            _reptPickSerial_.Tus_new_status = string.Empty;
                            _reptPickSerial_.Tus_qty = _itm.Sad_qty;
                            _reptPickSerial_.Tus_ser_1 = "N/A";
                            _reptPickSerial_.Tus_ser_2 = "N/A";
                            _reptPickSerial_.Tus_ser_id = 0;
                            _reptPickSerial_.Tus_ser_line = 0;
                            _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                            _reptPickSerial_.Tus_unit_cost = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_usrseq_no = -100;
                            _reptPickSerial_.Tus_warr_no = "N/A";
                            _reptPickSerial_.Tus_warr_period = 0;
                            _reptPickSerial_.Tus_new_remarks = string.Empty;
                            ScanSerialList.Add(_reptPickSerial_);
                        }
                        else
                        {
                            if (_serialList != null && _serialList.Count > 0)
                            {
                                List<QuotationSerial> _itmSerial = (from _res in _serialList
                                                                    where _res.Qs_item == _itm.Sad_itm_cd && _res.Qs_main_line == _itm.Sad_itm_line
                                                                    select _res).ToList<QuotationSerial>();
                                if (_itmSerial != null && _itmSerial.Count > 0)
                                {
                                    List<InventorySerialRefN> _invSerials = CHNLSVC.Inventory.GetSerialByID(_itmSerial[0].Qs_ser_id.ToString(), Session["UserDefLoca"].ToString());
                                    if (_invSerials == null && _invSerials.Count <= 0)
                                    {
                                        string msg1 = "Quotation serial id not found on inventory.SERIAL ID - " + _itmSerial[0].Qs_ser_id;
                                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('" + msg1 + "');", true);
                                        return;
                                    }

                                    _invSerials = (from _res in _invSerials
                                                   where _res.Ins_available == -1 || _res.Ins_available == 1 // added by Nadeeka
                                                   select _res).ToList<InventorySerialRefN>();
                                    if (_invSerials != null && _invSerials.Count > 0)
                                    {
                                        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                        _reptPickSerial_.Tus_base_doc_no = Convert.ToString(-100);
                                        _reptPickSerial_.Tus_base_itm_line = _itm.Sad_itm_line;
                                        _reptPickSerial_.Tus_bin = _invSerials[0].Ins_bin;
                                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                        _reptPickSerial_.Tus_cre_dt = DateTime.Now;
                                        _reptPickSerial_.Tus_cross_batchline = _invSerials[0].Ins_cross_batchline;
                                        _reptPickSerial_.Tus_cross_itemline = _invSerials[0].Ins_cross_itmline;
                                        _reptPickSerial_.Tus_cross_seqno = _invSerials[0].Ins_cross_seqno;
                                        _reptPickSerial_.Tus_cross_serline = _invSerials[0].Ins_cross_serline;
                                        _reptPickSerial_.Tus_doc_dt = _invSerials[0].Ins_doc_dt;
                                        _reptPickSerial_.Tus_doc_no = _invSerials[0].Ins_doc_no;
                                        _reptPickSerial_.Tus_exist_grncom = _invSerials[0].Ins_exist_grncom;
                                        _reptPickSerial_.Tus_isapp = 1;
                                        _reptPickSerial_.Tus_iscovernote = 1;
                                        _reptPickSerial_.Tus_itm_brand = _itm.Mi_brand;
                                        _reptPickSerial_.Tus_itm_cd = _itm.Sad_itm_cd;
                                        _reptPickSerial_.Tus_itm_desc = _itm.Mi_longdesc;
                                        _reptPickSerial_.Tus_itm_line = _itm.Sad_itm_line;
                                        _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                                        _reptPickSerial_.Tus_itm_stus = _itm.Sad_itm_stus;
                                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                        _reptPickSerial_.Tus_new_status = string.Empty;
                                        _reptPickSerial_.Tus_qty = _itm.Sad_qty;
                                        _reptPickSerial_.Tus_ser_1 = _invSerials[0].Ins_ser_1;
                                        _reptPickSerial_.Tus_ser_2 = _invSerials[0].Ins_ser_2;
                                        _reptPickSerial_.Tus_ser_id = _invSerials[0].Ins_ser_id;
                                        _reptPickSerial_.Tus_ser_line = _invSerials[0].Ins_ser_line;
                                        _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                                        _reptPickSerial_.Tus_unit_cost = _invSerials[0].Ins_unit_cost;
                                        _reptPickSerial_.Tus_unit_price = _invSerials[0].Ins_unit_price;
                                        _reptPickSerial_.Tus_usrseq_no = -100;
                                        _reptPickSerial_.Tus_warr_no = _invSerials[0].Ins_warr_no;
                                        _reptPickSerial_.Tus_warr_period = _invSerials[0].Ins_warr_period;
                                        _reptPickSerial_.Tus_new_remarks = string.Empty;
                                        ScanSerialList.Add(_reptPickSerial_);
                                    }
                                }
                            }
                        }
                    }

                    List<InvoiceSerial> InvoiceSerialListDup = new List<InvoiceSerial>();

                    foreach (ReptPickSerials item in ScanSerialList)
                    {
                        InvoiceSerial _invser = new InvoiceSerial();
                        _invser.Sap_del_loc = Session["UserDefLoca"].ToString();
                        _invser.Sap_itm_cd = item.Tus_itm_cd;
                        _invser.Sap_itm_line = item.Tus_itm_line;
                        _invser.Sap_remarks = string.Empty;

                        if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                        {
                            InvoiceItem oItem = _invoiceItemList.Find(x => x.Sad_itm_cd == item.Tus_itm_cd && x.Sad_itm_line == item.Tus_itm_line);
                            _invser.Sap_seq_no = oItem.Sad_seq;
                        }

                        _invser.Sap_ser_1 = item.Tus_ser_1;
                        _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;

                        InvoiceSerialListDup.Add(_invser);
                    }

                    var _serlst = new BindingList<ReptPickSerials>(ScanSerialList);

                    DataTable dt1 = new DataTable();
                    dt1 = _serlst.ToDataTable();

                    DataTable dtSer = new DataTable();
                    dtSer.Columns.AddRange(new DataColumn[9] { new DataColumn("sose_itm_line"), new DataColumn("sose_itm_cd"), new DataColumn("Model"), new DataColumn("Status"), new DataColumn("StatusDesc"), new DataColumn("Qty"), new DataColumn("sose_ser_1"), new DataColumn("sose_ser_2"), new DataColumn("Warranty") });

                    foreach (DataRow DDR1 in dt1.Rows)
                    {
                        dtSer.Rows.Add(DDR1["tus_itm_line"].ToString(), DDR1["tus_itm_cd"].ToString(), DDR1["Tus_itm_model"].ToString(), DDR1["Tus_itm_stus"].ToString(), DDR1["Tus_itm_stus"].ToString(), DDR1["Tus_qty"].ToString(), DDR1["Tus_ser_1"].ToString(), DDR1["Tus_ser_2"].ToString(), DDR1["Tus_warr_no"].ToString());
                    }

                    //gvPopSerial.DataSource = _serlst;
                    //gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList);
                    //gvPopSerial.DataBind();

                    gvPopSerial.DataSource = dtSer;
                    gvPopSerial.DataBind();

                    decimal _tobepays = 0;
                    if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                    else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                    InvoiceSerialList = InvoiceSerialListDup;
                    //ucPayModes1.SerialList = InvoiceSerialList;
                    //ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                    //ucPayModes1.Amount.Text = FormatToCurrency(lblGrndTotalAmount.Text.Trim());
                    //ucPayModes1.LoadData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid quotation number !!!');", true);
                    _invoiceItemList = new List<InvoiceItem>();
                    var _nulllst = new BindingList<InvoiceItem>(_invoiceItemList);
                    gvInvoiceItem.DataSource = setItemDescriptions(_invoiceItemList);
                    gvInvoiceItem.DataBind();
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

        private List<ReptPickSerials> setSerialStatusDescriptions(List<ReptPickSerials> itemList)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (ReptPickSerials item in itemList)
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
            return itemList;
        }
        protected void cmbTechnician_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(cmbTechnician.SelectedValue)))
            {
                if (_tblPromotor != null)
                {
                    var _find = (from DataRow _l in _tblPromotor.Rows where _l.Field<string>("mpp_promo_name") == cmbTechnician.Text select _l).ToList();
                    if (_find != null && _find.Count > 0)
                    {
                        txtPromotor.Text = Convert.ToString(cmbTechnician.SelectedValue);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the correct sales promotor !!!')", true);
                       
                        txtPromotor.Text = string.Empty;
                        cmbTechnician.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                txtPromotor.Text = string.Empty;
                cmbTechnician.SelectedIndex = 0;
            }
        }

        protected void gvPopSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string item = e.Row.Cells[1].Text;
                    string serial = e.Row.Cells[6].Text;
                    foreach (LinkButton button in e.Row.Cells[9].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete serial " + serial + "?')){ return false; };";
                        }
                    }


                    for (int colIndex = 0; colIndex < e.Row.Cells.Count; colIndex++)
                    {
                        string stats = e.Row.Cells[3].Text;

                        DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(stats);
                        string ststustext = string.Empty;

                        if (dtstatustx.Rows.Count > 0)
                        {
                            foreach (DataRow ddr2 in dtstatustx.Rows)
                            {
                                ststustext = ddr2[0].ToString();
                            }
                        }

                        string ToolTipString = ststustext;
                        e.Row.Cells[3].Attributes.Add("title", ToolTipString);
                        e.Row.Cells[4].Text = ToolTipString;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void gvPopSerial_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Myindex = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["SERIALSTABLE"] as DataTable;
                dt.Rows[Myindex].Delete();
                dt.AcceptChanges();
                ViewState["SERIALSTABLE"] = dt;
                BindSerialsGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadSoData(string sono,string company, string pc)
        {
            try
            {
                DataTable dtHeadersSo = CHNLSVC.Sales.SearchSalesOrdData(sono, company, pc);
                if (dtHeadersSo.Rows.Count > 0)
                 {
                     foreach (DataRow item in dtHeadersSo.Rows)
                     {
                         Session["SOSEQNO"] = item[25].ToString();
                         Session["STUS"] = item[19].ToString();
                         DateTime oreddate = Convert.ToDateTime(item[3].ToString());
                         string oreddatetext = oreddate.ToString("dd/MMM/yyyy");
                         txtdate.Text = oreddatetext;

                         cmbInvType.SelectedValue = item[1].ToString();
                         txtManualRefNo.Text = item[5].ToString();
                         txtdocrefno.Text = item[6].ToString();
                         txtCustomer.Text = item[7].ToString();
                         txtCusName.Text = item[8].ToString();
                         txtAddress1.Text = item[9].ToString();
                         txtAddress2.Text = item[10].ToString();
                         txtcurrency.Text = item[11].ToString();
                         txtPoNo.Text = item[21].ToString();
                         if (cmbExecutive.Items.FindByValue(item[17].ToString()) != null)
                         {
                             cmbExecutive.SelectedValue = item[17].ToString();
                         }
                         else
                         {
                             cmbExecutive.SelectedIndex = 0;
                         }


                         txtQuotation.Text = item[18].ToString();
                         txtPromotor.Text = item[20].ToString();
                         //txtLoyalty.Text = item[26].ToString();

                         if (item[27].ToString() == "1")
                         {
                             chkTaxPayable.Checked = true;
                         }
                         else
                         {
                             chkTaxPayable.Checked = false;
                         }

                         if (item[28].ToString() == "1")
                         {
                             lblVatExemptStatus.Text = "Available";
                         }
                         else
                         {
                             lblVatExemptStatus.Text = "None";
                         }

                         if (item[22].ToString() == "1")
                         {
                             lblSVatStatus.Text = "Available";
                         }
                         else
                         {
                             lblSVatStatus.Text = "None";
                         }

                         txtlocation.Text = item[29].ToString();

                         if (cmbTechnician.Items.FindByValue(item[30].ToString()) != null)
                         {
                             cmbTechnician.SelectedValue = item[30].ToString();
                         }
                         else
                         {
                             cmbTechnician.SelectedIndex = 0;
                         }

                         Session["DCUSCODE"] = item[13].ToString();
                         Session["DCUSNAME"] = item[24].ToString();
                         Session["DTOWN"] = item[12].ToString();
                         Session["DCUSADD1"] = item[14].ToString();
                         Session["DCUSADD2"] = item[15].ToString();
                         Session["DCUSLOC"] = item[23].ToString();

                         DataTable dttowns = CHNLSVC.General.GetTownByCode(item[12].ToString());
                         string townname = string.Empty;

                         if (dttowns.Rows.Count > 0)
                         {
                             foreach (DataRow ddrin in dttowns.Rows)
                             {
                                 townname = ddrin["mt_desc"].ToString();
                             }
                         }

                         Session["DTOWNNAME"] = townname;
                     }
                 }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid sales order number !!!')", true);
                    txtInvoiceNo.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadSoItemData(string sono)
        {
            try
            {

                List<SalesOrderItems> dtitems = new List<SalesOrderItems>();
                dtitems = CHNLSVC.Sales.SearchSalesOrdItems(sono);
                if (dtitems.Count > 0)
                {
                    gvInvoiceItem.DataSource = null;
                    gvInvoiceItem.DataBind();

                    gvInvoiceItem.DataSource = dtitems;
                    gvInvoiceItem.DataBind();
                }


                ViewState["ITEMSTABLE"] = null;
                this.BindItemsGrid();

                ViewState["ITEMSTABLE"] = dtitems;
                this.BindItemsGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadSoSerialData(string sono)
        {
            try
            {
                DataTable dtserialdata = CHNLSVC.Sales.SearchSalesOrdSerials(sono);

                DataTable uniqueCols = RemoveDuplicateRows(dtserialdata, "sose_ser_1");

                if (dtserialdata.Rows.Count > 0)
                {
                    gvPopSerial.DataSource = null;
                    gvPopSerial.DataBind();

                    gvPopSerial.DataSource = uniqueCols;
                    gvPopSerial.DataBind();
                }

                ViewState["SERIALSTABLE"] = null;
                this.BindSerialsGrid();

                ViewState["SERIALSTABLE"] = uniqueCols;
                this.BindSerialsGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtSerialNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckSerialAvailability();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void SetColumnForPriceDetailNPromotion(bool _isSerializedPriceLevel)
        {
            if (_isSerializedPriceLevel)
            {
               // NorPrice_Select.Visible = true;

            //    NorPrice_Serial.DataPropertyName = "sars_ser_no";
            //    NorPrice_Serial.Visible = true;
            //    NorPrice_Item.DataPropertyName = "Sars_itm_cd";
            //    NorPrice_Item.Visible = true;
            //    NorPrice_UnitPrice.DataPropertyName = "sars_itm_price";
            //    NorPrice_UnitPrice.Visible = true;
            //    NorPrice_Circuler.DataPropertyName = "sars_circular_no";
            //    NorPrice_PriceType.DataPropertyName = "sars_price_type";
            //    NorPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
            //    NorPrice_ValidTill.DataPropertyName = "sars_val_to";
            //    NorPrice_ValidTill.Visible = true;
            //    NorPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
            //    NorPrice_PbLineSeq.DataPropertyName = "1";
            //    NorPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
            //    NorPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
            //    NorPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
            //    NorPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
            //    NorPrice_Book.DataPropertyName = "sars_pbook";
            //    NorPrice_Level.DataPropertyName = "sars_price_lvl";

            //    PromPrice_Select.Visible = true;

            //    PromPrice_Serial.DataPropertyName = "sars_ser_no";
            //    PromPrice_Serial.Visible = true;
            //    PromPrice_Item.DataPropertyName = "Sars_itm_cd";
            //    PromPrice_Item.Visible = true;
            //    PromPrice_UnitPrice.DataPropertyName = "sars_itm_price";
            //    PromPrice_UnitPrice.Visible = true;
            //    PromPrice_Circuler.DataPropertyName = "sars_circular_no";
            //    PromPrice_PriceType.DataPropertyName = "sars_price_type";
            //    PromPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
            //    PromPrice_ValidTill.DataPropertyName = "sars_val_to";
            //    PromPrice_ValidTill.Visible = true;
            //    PromPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
            //    //PromPrice_PbLineSeq.DataPropertyName = "1";
            //    PromPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
            //    PromPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
            //    PromPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
            //    PromPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
            //    PromPrice_Book.DataPropertyName = "sars_pbook";
            //    PromPrice_Level.DataPropertyName = "sars_price_lvl";
            //}
            //else
            //{
            //    NorPrice_Select.Visible = false;

            //    NorPrice_Serial.Visible = false;
            //    NorPrice_Item.DataPropertyName = "sapd_itm_cd";
            //    NorPrice_Item.Visible = true;
            //    NorPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
            //    NorPrice_UnitPrice.Visible = true;
            //    NorPrice_Circuler.DataPropertyName = "Sapd_circular_no";
            //    NorPrice_Circuler.Visible = true;
            //    NorPrice_PriceType.DataPropertyName = "Sarpt_cd";
            //    NorPrice_PriceTypeDescription.DataPropertyName = "SARPT_CD";
            //    NorPrice_ValidTill.DataPropertyName = "Sapd_to_date";
            //    NorPrice_ValidTill.Visible = true;
            //    NorPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
            //    NorPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
            //    NorPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
            //    NorPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
            //    NorPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
            //    NorPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";
            //    NorPrice_Book.DataPropertyName = "sapd_pb_tp_cd";
            //    NorPrice_Level.DataPropertyName = "sapd_pbk_lvl_cd";

            //    PromPrice_Select.Visible = true;

            //    PromPrice_Serial.Visible = false;
            //    PromPrice_Item.DataPropertyName = "sapd_itm_cd";
            //    PromPrice_Item.Visible = true;
            //    PromPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
            //    PromPrice_UnitPrice.Visible = true;
            //    PromPrice_Circuler.DataPropertyName = "Sapd_circular_no";
            //    PromPrice_Circuler.Visible = true;
            //    PromPrice_PriceType.DataPropertyName = "sapd_price_type"; //"Sarpt_cd";
            //    PromPrice_PriceTypeDescription.DataPropertyName = "Sarpt_cd";
            //    PromPrice_ValidTill.DataPropertyName = "Sapd_to_date";
            //    PromPrice_ValidTill.Visible = true;
            //    PromPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
            //    PromPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
            //    PromPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
            //    PromPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
            //    PromPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
            //    PromPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";
            //    PromPrice_Book.DataPropertyName = "sapd_pb_tp_cd";
            //    PromPrice_Level.DataPropertyName = "sapd_pbk_lvl_cd";
            }
        }

        protected void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkPickGV.Checked) return;
            try
            {
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                SetColumnForPriceDetailNPromotion(_priceBookLevelRef.Sapl_is_serialized);
                if (_priceBookLevelRef.Sapl_is_serialized && string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are going to select a serialized price level without serial.Please select the serial !!!')", true);
                    
                    txtSerialNo.Text = string.Empty;
                    return;
                }
                CheckQty(false);
            }
            catch
            {
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void CheckUnitPrice()
        {
            if (txtUnitPrice.ReadOnly) return;

            if (chkPickGV.Checked) return;
            if (_IsVirtualItem) { CalculateItem(); return; }
            try
            {
                if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtUnitPrice.Text.Trim()) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Not allow price edit for com codes !!!')", true);
                    
                    //using (new CenterWinDialog(this)) { MessageBox.Show("Not allow price edit for com codes!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text)) return;
                if (!IsNumeric(txtQty.Text.Trim(), NumberStyles.Float))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid qty !!!')", true);
                   
                   // using (new CenterWinDialog(this)) { MessageBox.Show("Please select valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

                if ((_MasterProfitCenter !=null) && (_priceBookLevelRef !=null))
                {
                    if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
                    {
                        if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = DoFormat(0);
                        decimal vals = Convert.ToDecimal(txtUnitPrice.Text);
                        txtUnitPrice.Text = DoFormat(vals);
                        CalculateItem();
                        return;
                    } 
                }
                if (!_isCompleteCode)
                {
                    //check minus unit price validation
                    decimal _unitAmt = 0;
                    bool _isUnitAmt = Decimal.TryParse(txtUnitPrice.Text, out _unitAmt);
                    if (!_isUnitAmt)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Unit Price has to be number !!!')", true);
                      
                        //using (new CenterWinDialog(this)) { MessageBox.Show("Unit Price has to be number!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        return;
                    }
                    if (_unitAmt <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Unit Price has to be greater than 0 !!!')", true);
             
                        //using (new CenterWinDialog(this)) { MessageBox.Show("Unit Price has to be greater than 0!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtUnitPrice.Text) && _isEditDiscount == false)
                    {
                        decimal _pb_price;
                        if (SSPriceBookPrice == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Price not define. Please check the system updated price !!!')", true);

                            //using (new CenterWinDialog(this)) { MessageBox.Show("Price not define. Please check the system updated price.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            //txtUnitPrice.Text = "0";
                            return;
                        }
                        _pb_price = SSPriceBookPrice;
                        decimal _txtUprice = Convert.ToDecimal(txtUnitPrice.Text);
                        if (_MasterProfitCenter.Mpc_edit_price)
                        {
                            if (_pb_price > _txtUprice)
                            {
                                decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                                if (_diffPecentage > _MasterProfitCenter.Mpc_edit_rate)
                                {
                                  
                                    string msgf = "You can not deduct price more than " + _MasterProfitCenter.Mpc_edit_rate + "% from the price book price !!!";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgf + "')", true);
                                    //using (new CenterWinDialog(this)) { MessageBox.Show("You can not deduct price more than " + _MasterProfitCenter.Mpc_edit_rate + "% from the price book price.", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    txtUnitPrice.Text = DoFormat(_pb_price);
                                    _isEditPrice = false;
                                    return;
                                }
                                else
                                {
                                    _isEditPrice = true;
                                }
                            }
                        }
                        else
                        {
                            txtUnitPrice.Text = DoFormat(_pb_price);
                            _isEditPrice = false;
                        }
                    }
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = DoFormat(0);
                decimal val = Convert.ToDecimal(txtUnitPrice.Text);
                txtUnitPrice.Text = DoFormat(val);
                CalculateItem();
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

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckUnitPrice();
                if ((!string.IsNullOrEmpty(txtQty.Text.Trim())) && (!string.IsNullOrEmpty(txtUnitPrice.Text.Trim())))
                {
                    CalCulateVal();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void CalCulateVal()
        {
            try
            {
                Decimal totval = Convert.ToDecimal(txtQty.Text.Trim()) * Convert.ToDecimal(txtUnitPrice.Text.Trim());
                txtUnitAmt.Text = DoFormat(totval);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(txtQty.Text.Trim())) && (!string.IsNullOrEmpty(txtUnitPrice.Text.Trim())))
                {
                    CalCulateVal();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void ddltown_SelectedIndexChanged(object sender, EventArgs e)
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

        private void MpDeliveryShow()
        {
            try
            {
                string recall = (string)Session["STUS"];

                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());

                if (!IsPostBack)
                {
                    ddltown.SelectedIndex = 0;
                }

                if ((string.IsNullOrEmpty(txtInvoiceNo.Text.Trim())) && (string.IsNullOrEmpty(txtCustomer.Text.Trim())) && (string.IsNullOrEmpty(recall)))
                {
                    txtdellocation.Text = string.Empty;
                    txtdelcuscode.Text = string.Empty;
                    txtdelname.Text = string.Empty;
                    txtdelad1.Text = string.Empty;
                    txtdelad2.Text = string.Empty;
                    ddltown.SelectedIndex = 0;
                }
                else
                {
                    string towncode = _masterBusinessCompany.Mbe_town_cd;

                    string _dcuscode = (string)Session["DCUSCODE"];
                    string _dcusname = (string)Session["DCUSNAME"];
                    string _dcustown = (string)Session["DTOWN"];
                    string _dcusadd1 = (string)Session["DCUSADD1"];
                    string _dcusadd2 = (string)Session["DCUSADD2"];
                    string _dcusloc = (string)Session["DCUSLOC"];
                    string _deltownnname = (string)Session["DTOWNNAME"];

                    if (string.IsNullOrEmpty(_dcusloc))
                    {
                        txtdellocation.Text = txtlocation.Text.Trim();
                    }
                    else
                    {
                        txtdellocation.Text = _dcusloc;
                    }

                    if (string.IsNullOrEmpty(_dcuscode))
                    {
                        txtdelcuscode.Text = txtCustomer.Text.Trim();
                    }
                    else
                    {
                        txtdelcuscode.Text = _dcuscode;
                    }

                    if (string.IsNullOrEmpty(_dcusname))
                    {
                        txtdelname.Text = txtCusName.Text.Trim();
                    }
                    else
                    {
                        txtdelname.Text = _dcusname;
                    }

                    if (string.IsNullOrEmpty(_dcusadd1))
                    {
                        txtdelad1.Text = txtAddress1.Text.Trim();
                    }
                    else
                    {
                        txtdelad1.Text = _dcusadd1;
                    }

                    if (string.IsNullOrEmpty(_dcusadd2))
                    {
                        txtdelad2.Text = txtAddress2.Text.Trim();
                    }
                    else
                    {
                        txtdelad2.Text = _dcusadd2;
                    }

                    if (string.IsNullOrEmpty(_dcustown))
                    {
                        if (ddltown.Items.FindByText(towncode.ToUpper()) != null)
                        {
                            ddltown.SelectedItem.Text = towncode;
                        }
                        else
                        {
                            ddltown.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if (ddltown.Items.FindByText(_deltownnname.ToUpper()) != null)
                        {
                            ddltown.SelectedItem.Text = _deltownnname;
                        }
                        else
                        {
                            ddltown.SelectedIndex = 0;
                        }
                    }
                }
                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtndconfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Session["DCUSCODE"] = txtdelcuscode.Text.Trim();
                Session["DCUSNAME"] = txtdelname.Text.Trim();
                Session["DTOWN"] = ddltown.SelectedValue;
                Session["DTOWNNAME"] = ddltown.SelectedItem.Text;
                Session["DCUSADD1"] = txtdelad1.Text.Trim();
                Session["DCUSADD2"] = txtdelad2.Text.Trim();
                Session["DCUSLOC"] = txtdellocation.Text;
                MpDelivery.Show();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Confirmed !!!')", true);
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
                ddltown.SelectedIndex = 0;
               // txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                txtdelcuscode.Text = txtCustomer.Text;
              txtdelname.Text =  txtCusName.Text;
              txtdelad1.Text=  txtAddress1.Text ;
              txtdelad2.Text=  txtAddress2.Text ;
                MpDeliveryShow();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtndclear_Click(object sender, EventArgs e)
        {
            try
            {
                txtdelad1.Text = string.Empty;
                txtdelad2.Text = string.Empty;
                txtdelcuscode.Text = string.Empty;
                txtdellocation.Text = string.Empty;
                txtdelname.Text = string.Empty;
                ddltown.SelectedIndex = 0;

                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btncClose_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerPopoup.Hide();
                Session["c"] = "false";
                Session["CUSPOPUP"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnprintord_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    Clear();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        protected void gvInvoiceItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //txtItem.Text = gvInvoiceItem.SelectedRow.Cells[1].Text;

                //lblItemDescription.Text = gvInvoiceItem.SelectedRow.Cells[2].Text;

                //if ((!string.IsNullOrEmpty(gvInvoiceItem.SelectedRow.Cells[11].Text)) && ((gvInvoiceItem.SelectedRow.Cells[11].Text != "&nbsp;")))
                //{
                //    cmbBook.SelectedValue = gvInvoiceItem.SelectedRow.Cells[11].Text;
                //}

                //if ((!string.IsNullOrEmpty(gvInvoiceItem.SelectedRow.Cells[12].Text)) && ((gvInvoiceItem.SelectedRow.Cells[12].Text != "&nbsp;")))
                //{
                //    cmbLevel.SelectedValue = gvInvoiceItem.SelectedRow.Cells[12].Text;
                //}

               
                cmbStatus.SelectedIndex = 0;
                
                
                //txtQty.Text = gvInvoiceItem.SelectedRow.Cells[4].Text;
                //txtUnitPrice.Text = DoFormat(Convert.ToDecimal(gvInvoiceItem.SelectedRow.Cells[5].Text));
                //txtUnitAmt.Text = DoFormat(Convert.ToDecimal(gvInvoiceItem.SelectedRow.Cells[6].Text));
                //txtDisRate.Text = DoFormat(Convert.ToDecimal(gvInvoiceItem.SelectedRow.Cells[7].Text));
                //txtDisAmt.Text = DoFormat(Convert.ToDecimal(gvInvoiceItem.SelectedRow.Cells[8].Text));
                //txtTaxAmt.Text =  DoFormat(Convert.ToDecimal(gvInvoiceItem.SelectedRow.Cells[9].Text));
                //txtLineTotAmt.Text = DoFormat(Convert.ToDecimal(gvInvoiceItem.SelectedRow.Cells[10].Text));
                //lblItemDescription.Text = gvInvoiceItem.SelectedRow.Cells[2].Text;

                foreach (GridViewRow row in gvInvoiceItem.Rows)
                {
                    if (row.RowIndex == gvInvoiceItem.SelectedIndex)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    }
                }

                Session["RSEQNO"] = gvInvoiceItem.SelectedRow.Cells[14].Text;

                LoadDispatchDetails(Session["UserCompanyCode"].ToString(),txtlocation.Text,gvInvoiceItem.SelectedRow.Cells[1].Text,gvInvoiceItem.SelectedRow.Cells[3].Text);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        

        private void LoadDispatchDetails(string company,string loc,string item,string itm_status)
        {
            try
            {
                DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(itm_status);

                string _itemval = string.Empty;

                foreach (DataRow itemval in dtstatus.Rows)
                {
                    _itemval = itemval["MIS_CD"].ToString();
                }

                DataTable dtitemsdata = CHNLSVC.Sales.GetWareHousetemsData(company, loc, item, _itemval);
                if (dtitemsdata.Rows.Count > 0)
                {
                    grvwarehouseitms.DataSource = null;
                    grvwarehouseitms.DataBind();

                    grvwarehouseitms.DataSource = dtitemsdata;
                    grvwarehouseitms.DataBind();
                }
                else
                {
                    grvwarehouseitms.DataSource = null;
                    grvwarehouseitms.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void cmbTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusName.Text))
                {
                    txtCusName.Text = cmbTitle.Text.Trim();
                    txtCusName.Text = string.Empty;
                }
                else
                {
                    string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                    if (string.IsNullOrEmpty(_title))
                    {
                        txtCusName.Text = cmbTitle.Text.Trim() + txtCusName.Text;
                        txtCusName.Text = string.Empty;
                    }
                    else
                    {
                        ListItem li = new ListItem();
                        li.Text = _title;
                        bool _exist = cmbTitle.Items.Contains(li);
                        if (_exist)
                        {
                            string _currentCustomerName = txtCusName.Text.Trim();
                            txtCusName.Text = _currentCustomerName.Replace(_title.ToUpper(), cmbTitle.Text.Trim().ToUpper());
                            txtCusName.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            LoadItemDetail(txtItem.Text);

            MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());

            if (_itemdetail == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid item code.');", true);
                txtItem.Text = string.Empty;
                return;
            }

            //if (_itemdetail.Mi_is_ser1 == 1)
            //{
            //    if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('You have to select the serial number for the serialized item !!!');", true);
                  
            //        txtQty.ReadOnly = true;
            //    }
            //}
            //else
            //{
                txtQty.ReadOnly = false;


                List <PriceDetailRef>_priceDetailRef = new List<PriceDetailRef>();
                _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtdate.Text));
                if (_priceDetailRef.Count <= 0)
                {
                    //if (!_isCompleteCode)
                    //{
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There is no price for the selected item !!!')", true);

                    SetDecimalTextBoxForZero(true, false, true);
                        
                    //}
                    //else
                    //{
                        txtUnitPrice.Text = "0";
                        return;
                    //}
                }
                else
                {
                    //if (_isCompleteCode)
                    //{
                        List<PriceDetailRef> _new = new List<PriceDetailRef>();
                        _new = _priceDetailRef;
                        _priceDetailRef = new List<PriceDetailRef>();
                        var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                        if (_p != null)
                            if (_p.Count > 0)
                            {
                                if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                                _priceDetailRef.Add(_p[0]);
                            }
                    //}
                    if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                    {
                        var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                        if (_isSuspend > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Price has been suspended.')", true);
 
                            return;
                        }
                    }
                    if (_priceDetailRef.Count > 1)
                    {
                        SetColumnForPriceDetailNPromotion(false);
                        BindNonSerializedPrice(_priceDetailRef);
                        return;
                    }
                    else if (_priceDetailRef.Count == 1)
                    {
                        var _one = from _itm in _priceDetailRef
                                   select _itm;
                        int _priceType = 0;
                        foreach (var _single in _one)
                        {
                            _priceType = _single.Sapd_price_type;
                            PriceTypeRef _promotion = TakePromotion(_priceType);
                            decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                            txtUnitPrice.Text = (Convert.ToString(UnitPrice));
                            txtUnitPrice.Text = UnitPrice.ToString("N2");
                            WarrantyRemarks = _single.Sapd_warr_remarks;
                            SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                            Int32 _pbSq = _single.Sapd_pb_seq;
                            Int32 _pbiSq = _single.Sapd_seq_no;
                            string _mItem = _single.Sapd_itm_cd;
                            SetColumnForPriceDetailNPromotion(false);
                            BindNonSerializedPrice(_priceDetailRef);

                            if (_isCombineAdding == false) txtUnitPrice.Focus();
                        }
                    }
                }
           // }

            //grdInventoryBalance.DataSource = null;
            //grdInventoryBalance.DataBind();

            //DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), string.Empty);
            //grdInventoryBalance.DataSource = null;
            //grdInventoryBalance.AutoGenerateColumns = false;
            //grdInventoryBalance.DataSource = _inventoryLocation;
            //grdInventoryBalance.DataBind();

           // if (_itemdetail.Mi_is_ser1 != 1)
           // {
                CalculateItem();
           // }
        }

        protected void txtDisRate_TextChanged(object sender, EventArgs e)
        {
            if (chkPickGV.Checked) return;
            if (_IsVirtualItem)
            {
                txtDisRate.Text = "";
                txtDisAmt.Text = "";
                txtDisAmt.Text = DoFormat(0);
                txtDisRate.Text = DoFormat(0);
                return;
            }
            try
            {
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
                {
                }

                //if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                //{
                    if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allow discount for com codes !!!')", true);
                        txtDisRate.Text = "";
                        txtDisRate.Text = DoFormat(0);
                        return;
                    }
                //}
                else
                {
                    if (Convert.ToDecimal(txtQty.Text) != 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Promotion voucher allow for only one(1) item !!!')", true);
                        txtDisRate.Text = "";
                        txtDisRate.Text = DoFormat(0);
                        return;
                    }
                }
                CheckNewDiscountRate();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }

        protected bool CheckNewDiscountRate()
        {
            if (string.IsNullOrEmpty(txtItem.Text))
                return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid quantity !!!')", true);
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                bool _IsPromoVou = false;
                if (_disRate > 0)
                {
                    if (GeneralDiscount == null)
                        GeneralDiscount = new CashGeneralEntiryDiscountDef();
                    //if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                    //{
                    _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(txtdate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                       
                    //CashGeneralEntiryDiscountDef GeneralDiscount
                    //}
                    //else
                    //{
                    //    GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCustomer.Text.Trim(), Convert.ToDateTime(txtdate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.Trim(), string.Empty);
                    //    if (GeneralDiscount != null && GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00)
                    //    {
                    //        _IsPromoVou = true;
                    //       // GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
                    //    }
                    //}
                    if (GeneralDiscount != null && (GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00))
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        //if (lblPromoVouUsedFlag.Text.Contains("U") == true)
                        //{
                        //    DisplayMessage("Voucher already used!");
                        //    txtDisRate.Text = FormatToCurrency("0");
                        //    _isEditDiscount = false;
                        //    return false;
                        //}

                        if (_IsPromoVou == true)
                        {
                            if (rates == 0 && vals > 0)
                            {
                                CalculateItem();
                                if (Convert.ToDecimal(txtDisAmt.Text) > vals)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Voucher discount amount should be " + vals + ".Not allowed discount rate " + _disRate + "% !!!')", true);
                                    txtDisRate.Text = DoFormat(0);
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                            else
                            {
                                if (rates != _disRate)
                                {
                                    CalculateItem();
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Voucher discount rate should be " + rates + "% !.Not allowed discount rate " + _disRate + "% discounted price is " + txtLineTotAmt.Text + " !!!')", true);
                                    txtDisRate.Text = DoFormat(0);
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (rates < _disRate)
                            {
                                CalculateItem();
                                string msgText = "Exceeds maximum discount allowed " + rates + "% !." + _disRate + "% discounted price is " + txtLineTotAmt.Text;
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + " !!!')", true);
                                txtDisRate.Text = DoFormat(0);
                                CalculateItem();
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                _isEditDiscount = true;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allow for apply discount !!!')", true);
                        txtDisRate.Text = DoFormat(0);
                        _isEditDiscount = false;
                        return false;
                    }

                    if (_isEditDiscount == true)
                    {
                        if (_IsPromoVou == true)
                        {
                            _proVouInvcItem = txtItem.Text.ToUpper().ToString();
                        }
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                txtDisRate.Text = DoFormat(0);
            }
            if (string.IsNullOrEmpty(txtDisRate.Text)) txtDisRate.Text = DoFormat(0);
            decimal val = Convert.ToDecimal(txtDisRate.Text);
            txtDisRate.Text = DoFormat(val);
            CalculateItem();
            return true;
        }

        private bool CheckNewDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid quantity !!!')", true);
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;
            if (!string.IsNullOrEmpty(txtDisAmt.Text) && _isEditPrice == false && !string.IsNullOrEmpty(txtQty.Text))
            {
                decimal _disAmt = Convert.ToDecimal(txtDisAmt.Text);
                decimal _uRate = Convert.ToDecimal(txtUnitPrice.Text);
                decimal _qty = Convert.ToDecimal(txtQty.Text);
                decimal _totAmt = _uRate * _qty;
                decimal _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;

                _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
                GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(txtdate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);


                if (_disAmt > 0)
                {
                    if (GeneralDiscount != null && (GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00))
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        if (vals < _disAmt && rates == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You cannot discount price more than " + vals + " !!!')", true);
                            txtDisAmt.Text = DoFormat(0);
                            txtDisRate.Text = DoFormat(0);
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = DoFormat(0);
                            CalculateItem();
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(txtLineTotAmt.Text)) * 100 : 0;
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) 
                                txtDisRate.Text = DoFormat(_percent);
                            CalculateItem();
                            CheckNewDiscountRate();
                            _isEditDiscount = true;
                        }
                    }
                    else
                    {
                        if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                        bool _IsPromoVou = false;
                        //if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                        //{
                        //    GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(txtdate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        //}
                        //else
                        {
                            GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCustomer.Text.Trim(), Convert.ToDateTime(txtdate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.Trim(), string.Empty);
                            if (GeneralDiscount != null && (GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00))
                            {
                                _IsPromoVou = true;
                                //GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
                            }
                        }

                        if (GeneralDiscount != null && (GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00))
                        {
                            decimal vals = GeneralDiscount.Sgdd_disc_val;
                            decimal rates = GeneralDiscount.Sgdd_disc_rt;

                            if (_IsPromoVou == true)
                            {
                                if (vals < _disAmt && rates == 0)
                                {
                                    string msg = "Voucher discount amount should be " + vals + "!./nNot allowed discount amount " + _disAmt + " discounted price is " + txtLineTotAmt.Text;

                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + " !!!')", true);

                                    txtDisAmt.Text = DoFormat(0);
                                    txtDisRate.Text = DoFormat(0);
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }

                            if (vals < _disAmt && rates == 0)
                            {
                                string msg1 = "You cannot discount price more than " + vals + "!!!";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg1 + " !!!')", true);
                                txtDisAmt.Text = DoFormat(0);
                                txtDisRate.Text = DoFormat(0);
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = DoFormat(0);
                                CalculateItem();
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) 
                                    txtDisRate.Text = DoFormat(_percent);
                                CalculateItem();
                                CheckNewDiscountRate();
                                _isEditDiscount = true;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allow for discount !!!')", true);
                            txtDisAmt.Text = DoFormat(0);
                            txtDisRate.Text = DoFormat(0);
                            _isEditDiscount = false;
                            return false;
                        }
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                txtDisAmt.Text = DoFormat(0);
                txtDisRate.Text = DoFormat(0);
            }

            if (string.IsNullOrEmpty(txtDisAmt.Text))
                txtDisAmt.Text = DoFormat(0);
            decimal val = Convert.ToDecimal(txtDisAmt.Text);
            txtDisAmt.Text = DoFormat(val);
            CalculateItem();
            return true;
        }
        protected void txtDisAmt_TextChanged(object sender, EventArgs e)
        {
            if (chkPickGV.Checked) return;
            if (_IsVirtualItem)
            {
                txtDisRate.Text = "";
                txtDisAmt.Text = "";
                txtDisAmt.Text = DoFormat(0);
                txtDisRate.Text = DoFormat(0);
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(txtDisAmt.Text))
                    return;
                if (Convert.ToDecimal(txtDisAmt.Text) < 0)
                {
                }
                CheckNewDiscountAmount();
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

        protected void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(cmbStatus.SelectedValue);

                if (dtstatustx.Rows.Count > 0)
                {
                    foreach (DataRow ddr2 in dtstatustx.Rows)
                    {
                        cmbStatus.ToolTip = ddr2[0].ToString();
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

        protected void chkQuotation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQuotation.Checked == true)
            {
                txtQuotation.ReadOnly = false;
            }
            else
            {
                txtQuotation.ReadOnly = true;
            }
        }

        protected void txtresno_TextChanged(object sender, EventArgs e)
        {

        }

        protected void LinkButton27_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a customer !!!')", true);
                return;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an item code !!!')", true);
                return;
            }
            if (cmbStatus.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an item status !!!')", true);
                return;
            }
            List<INR_RES> oINR_RESs = CHNLSVC.Sales.GET_RESERVATION_HDR(Session["UserCompanyCode"].ToString(), txtCustomer.Text, "A",txtdellocation.Text);
            if (oINR_RESs != null && oINR_RESs.Count > 0)
            {
                dgvReservation.DataSource = null;
                dgvReservation.DataSource = oINR_RESs;
                dgvReservation.DataBind();
                mpReservations.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No Records found !!!')", true);
            }
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

        protected void btnReservationConfirm_Click(object sender, EventArgs e)
        {
            mpReservations.Show();
            if (dgvReservation.Rows.Count > 0)
            {
                for (int i = 0; i < dgvReservation.Rows.Count; i++)
                {
                    GridViewRow dr = dgvReservation.Rows[i];
                    CheckBox chkSel = (CheckBox)dr.FindControl("chkSelect");
                    Label lblirs_res_no = (Label)dr.FindControl("lblirs_res_no");
                    Label lblirs_seq = (Label)dr.FindControl("lblirs_seq");

                    if (chkSel.Checked)
                    {
                        lblSelectRevervation.Text = lblirs_res_no.Text;
                        txtresno.Text = lblirs_res_no.Text;

                        List<INR_RES_DET> oINR_RES_DETs = CHNLSVC.Sales.GET_RESERVATION_DET(Convert.ToInt32(lblirs_seq.Text),null);

                        DataTable dt = GlobalMethod.ToDataTable(oINR_RES_DETs);

                        if (oINR_RES_DETs != null && oINR_RES_DETs.Count > 0)
                        {
                            INR_RES_DET oINR_RES_DET = oINR_RES_DETs.Find(x => x.IRD_ITM_CD == txtItem.Text && x.IRD_ITM_STUS == cmbStatus.SelectedValue.ToString());
                            if (oINR_RES_DET != null && oINR_RES_DET.IRD_RES_NO != null)
                            {
                                decimal UsedQty = 0;

                                List<InvoiceItem> oSaveDInvoiceItem = CHNLSVC.Sales.GET_INV_ITM_BY_RESNO_LINE(lblirs_res_no.Text, oINR_RES_DET.IRD_LINE);
                                if (oSaveDInvoiceItem != null && oSaveDInvoiceItem.Count > 0)
                                {
                                    UsedQty = oSaveDInvoiceItem.Sum(x => x.Sad_qty);
                                }
                                if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                                {
                                    UsedQty = UsedQty + _invoiceItemList.Where(x => x.Sad_res_no == oINR_RES_DET.IRD_RES_NO && x.Sad_res_line_no == oINR_RES_DET.IRD_LINE).Sum(x => x.Sad_qty);
                                }
                                if (oINR_RES_DET.IRD_RES_BQTY <= UsedQty || oINR_RES_DET.IRD_RES_BQTY < Convert.ToDecimal(txtQty.Text))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed reserved quantity !!!')", true);
                                    lblSelectRevervation.Text = "";
                                    txtresno.Text = "";
                                    mpReservations.Show();
                                    return;
                                }

                                lblSelectRevLine.Text = oINR_RES_DET.IRD_LINE.ToString();
                                //DisplayMessageJS("Successfully reservation added.");
                                mpReservations.Hide();
                                return;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot find a reservation for selected item and status !!!')", true);
                                lblSelectRevervation.Text = "";
                                txtresno.Text = "";
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot find a reservation details !!!')", true);
                            lblSelectRevervation.Text = "";
                            txtresno.Text = "";
                            return;
                        }
                    }
                }
            }
        }


        protected void btnCLoseReservation_Click(object sender, EventArgs e)
        {
            mpReservations.Hide();
        }


        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvReservation.Rows.Count > 0)
            {
                for (int i = 0; i < dgvReservation.Rows.Count; i++)
                {
                    GridViewRow dr = dgvReservation.Rows[i];
                    CheckBox chkSel = (CheckBox)dr.FindControl("chkSelect");
                    chkSel.Checked = false;
                }
            }

            GridViewRow drClick = (sender as CheckBox).NamingContainer as GridViewRow;
            CheckBox chkSel1 = (CheckBox)drClick.FindControl("chkSelect");
            chkSel1.Checked = true;
            mpReservations.Show();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

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

        protected void DrugDetailGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MIS_DESC")) == cmbStatus.SelectedItem.Text)
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                }
            }
        }

        protected void gvitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string itemcode = (dgvItem.SelectedRow.FindControl("lblitemcodepopup") as Label).Text;
                txtItem.Text = itemcode;
                CheckSerialAvailabilityMultiple();
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

        protected void btnLoadDisReqs_Click(object sender, EventArgs e)
        {
            loadDiscountRequests();

            MPDis.Show();
        }

        private void loadDiscountRequests()
        {
            List<CashGeneralEntiryDiscountDef> oItems = CHNLSVC.Sales.GET_DIS_REQ_BY_CUSTOMER(cmbInvType.SelectedValue.ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(txtdate.Text).Date, txtCustomer.Text.Trim());
            if (oItems != null && oItems.Count > 0)
            {
                //oItems = oItems.FindAll(x => x.Sgdd_stus == true);
                if (oItems != null && oItems.Count > 0)
                {
                    ddlDisCategory.Enabled = false;

                    List<String> itemsList = oItems.Select(x => x.Sgdd_itm).Distinct().ToList();
                    itemsList.RemoveAll(str => String.IsNullOrEmpty(str));

                    //string[] itemsList = oItems.Select(x => x.Sgdd_itm).Distinct().ToArray();

                    if (itemsList.Count > 0)
                    {
                        gvDisItem.DataSource = oItems;
                        gvDisItem.DataBind();

                        gvDisItem.Columns[6].Visible = true;

                        for (int i = 0; i < gvDisItem.Rows.Count; i++)
                        {
                            GridViewRow dr = gvDisItem.Rows[i];
                            Label lblSgdd_stus = dr.FindControl("lblSgdd_stus") as Label;
                            Label lblSgdd_itm = dr.FindControl("lblSgdd_itm") as Label;

                            if (lblSgdd_stus.Text.ToUpper() == "TRUE")
                            {
                                lblSgdd_stus.Text = "Active";
                            }
                            else
                            {
                                lblSgdd_stus.Text = "Inactive";
                            }

                            if (lblSgdd_itm == null || string.IsNullOrEmpty(lblSgdd_itm.Text))
                            {
                                dr.Visible = false;
                            }
                        }

                        for (int i = 0; i < oItems.Count; i++)
                        {
                            CashGeneralEntiryDiscountDef item = oItems[i];
                            GridViewRow dr = gvDisItem.Rows[i];
                            DropDownList ddlDiscReqType = dr.FindControl("ddlDiscReqType") as DropDownList;
                            TextBox txtDisItem_Amount = dr.FindControl("txtDisItem_Amount") as TextBox;

                            if (item.Sgdd_disc_rt > 0 & item.Sgdd_disc_val == 0)
                            {
                                ddlDiscReqType.SelectedIndex = 0;
                            }
                            else
                            {
                                txtDisItem_Amount.Text = item.Sgdd_disc_val.ToString();
                                ddlDiscReqType.SelectedIndex = 1;
                            }

                            ddlDiscReqType.Enabled = false;
                        }
                    }
                    else
                    {
                        txtDisAmount.Text = oItems[0].Sgdd_disc_rt.ToString("N2");
                        gvDisItem.DataSource = new int[] { };
                        gvDisItem.DataBind();
                    }

                    if (itemsList.Count != oItems.Count)
                    {
                        List<CashGeneralEntiryDiscountDef> oItemsTemp = oItems.FindAll(y => y.Sgdd_stus == true && y.Sgdd_itm == "");
                        if (oItemsTemp != null && oItemsTemp.Count > 0 && oItemsTemp.FindAll(x => x.Sgdd_cre_dt == oItemsTemp.Max(y => y.Sgdd_cre_dt)).Count > 0)
                        {
                            txtDisAmount.Text = oItemsTemp.FindAll(x => x.Sgdd_cre_dt == oItemsTemp.Max(y => y.Sgdd_cre_dt))[0].Sgdd_disc_rt.ToString("N2");
                        }
                    }
                }
            }
        }

        protected void LinkButton21_Click(object sender, EventArgs e)
        {
            MPDis.Show();
            List<MsgInformation> _infor = CHNLSVC.General.GetMsgInformation(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString());
            if (_infor == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your location does not setup detail which the request need to corroborate. Please contact IT dept !!!')", true);

                return;
            }
            if (_infor.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your location does not setup detail which the request need to corroborate. Please contact IT dept !!!')", true);

                return;
            }
            var _available = _infor.Where(x => x.Mmi_msg_tp != "A" && x.Mmi_receiver == Session["UserID"].ToString()).ToList();
            if (_available == null || _available.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your user ID is not setup. Please contact IT department for more details !!!')", true);

                return;
            }
            List<CashGeneralEntiryDiscountDef> _list = new List<CashGeneralEntiryDiscountDef>();
            if (ddlDisCategory.Text == "Customer")
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the discount rate !!!')", true);

                    return;
                }

                if (IsNumeric(txtDisAmount.Text) == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid discount rate !!!')", true);
              
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) > 100)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Discount rate cannot exceed the 100% !!!')", true);

                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Discount rate cannot exceed the 0% !!!')", true);
               
                    return;
                }
            }
            if (ddlDisCategory.Text == "Item")
            {
                if (gvDisItem.Rows.Count > 0)
                {
                    bool _isCheckSingle = false;
                    for (int i = 0; i < gvDisItem.Rows.Count; i++)
                    {
                        GridViewRow dr = gvDisItem.Rows[i];
                        CheckBox DisItem_Select = (CheckBox)dr.FindControl("DisItem_Select");
                        if (DisItem_Select.Checked)
                        {
                            _isCheckSingle = true;
                            break;
                        }
                    }

                    if (_isCheckSingle == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the item which you need to request !!!')", true);

                        return;
                    }
                }
            }
            string _customer = txtCustomer.Text;
            string _customerReq = "DISREQ" + Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            bool _isSuccessful = true;
            if (gvDisItem.Rows.Count > 0 && ddlDisCategory.Text == "Item")
            {
                for (int i = 0; i < gvDisItem.Rows.Count; i++)
                {
                    GridViewRow dr = gvDisItem.Rows[i];
                    CheckBox DisItem_Select = (CheckBox)dr.FindControl("DisItem_Select");
                    DropDownList ddlDiscReqType = (DropDownList)dr.FindControl("ddlDiscReqType");
                    TextBox txtDisItem_Amount = (TextBox)dr.FindControl("txtDisItem_Amount");
                    Label lblSgdd_itm = (Label)dr.FindControl("lblSgdd_itm");
                    Label lblSgdd_pb = (Label)dr.FindControl("lblSgdd_pb");
                    Label lblSgdd_pb_lvl = (Label)dr.FindControl("lblSgdd_pb_lvl");

                    if (DisItem_Select.Checked)
                    {

                        string _item = lblSgdd_itm.Text.Trim();
                        string _pricebook = lblSgdd_pb.Text.Trim();
                        string _pricelvl = lblSgdd_pb_lvl.Text.Trim();

                        if (string.IsNullOrEmpty(Convert.ToString(txtDisItem_Amount.Text).Trim()))
                        {
                            
                            string msg1 = "Please select the amount for " + _item;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg1 +" !!!')", true);
                            _isSuccessful = false;
                            break;
                        }

                        if (!IsNumeric(Convert.ToString(txtDisItem_Amount.Text).Trim()))
                        {
                            
                            string msg2 = "Please select the valid amount for " + _item;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + " !!!')", true);
                            _isSuccessful = false;
                            break;
                        }

                        if (Convert.ToDecimal(Convert.ToString(txtDisItem_Amount.Text).Trim()) <= 0)
                        {
                           
                            string msg3 = "Please select the valid amount for " + _item;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg3 + " !!!')", true);
                            _isSuccessful = false;
                            break;
                        }

                        if (Convert.ToDecimal(Convert.ToString(txtDisItem_Amount.Text).Trim()) > 100 && ddlDiscReqType.SelectedValue.ToString().Contains("Rate"))
                        {
                            
                            string msg4 = "Rate cannot be exceed the 100% in " + _item;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg4+ " !!!')", true);
                            _isSuccessful = false;
                            break;
                        }

                        CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                        _discount.Sgdd_com = Session["UserCompanyCode"].ToString();
                        _discount.Sgdd_cre_by = Session["UserID"].ToString();
                        _discount.Sgdd_cre_dt = DateTime.Now.Date;
                        _discount.Sgdd_cust_cd = _customer;
                        if (ddlDiscReqType.SelectedValue.ToString().Contains("Rate"))
                            _discount.Sgdd_disc_rt = Convert.ToDecimal(txtDisItem_Amount.Text.Trim());
                        else
                            _discount.Sgdd_disc_val = Convert.ToDecimal(txtDisItem_Amount.Text.Trim());
                        _discount.Sgdd_from_dt = Convert.ToDateTime(txtdate.Text);
                        _discount.Sgdd_itm = _item;
                        _discount.Sgdd_mod_by = Session["UserID"].ToString();
                        _discount.Sgdd_mod_dt = DateTime.Now.Date;
                        _discount.Sgdd_no_of_times = 1;
                        _discount.Sgdd_no_of_used_times = 0;
                        _discount.Sgdd_pb = _pricebook;
                        _discount.Sgdd_pb_lvl = _pricelvl;
                        _discount.Sgdd_pc = Session["UserDefProf"].ToString();
                        _discount.Sgdd_req_ref = _customerReq;
                        _discount.Sgdd_sale_tp = cmbInvType.Text.Trim();
                        _discount.Sgdd_seq = 0;
                        _discount.Sgdd_stus = false;
                        _discount.Sgdd_to_dt = Convert.ToDateTime(txtdate.Text);
                        _list.Add(_discount);
                    }
                }
            }
            else
            {
                CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                _discount.Sgdd_com = Session["UserCompanyCode"].ToString();
                _discount.Sgdd_cre_by = Session["UserID"].ToString();
                _discount.Sgdd_cre_dt = DateTime.Now.Date;
                _discount.Sgdd_cust_cd = _customer;
                _discount.Sgdd_disc_rt = Convert.ToDecimal(txtDisAmount.Text.Trim());
                _discount.Sgdd_from_dt = Convert.ToDateTime(txtdate.Text);
                _discount.Sgdd_itm = string.Empty;
                _discount.Sgdd_mod_by = Session["UserID"].ToString();
                _discount.Sgdd_mod_dt = DateTime.Now.Date;
                _discount.Sgdd_no_of_times = 1;
                _discount.Sgdd_no_of_used_times = 0;
                _discount.Sgdd_pb = cmbBook.Text.Trim();
                _discount.Sgdd_pb_lvl = cmbLevel.Text.Trim();
                _discount.Sgdd_pc = Session["UserDefProf"].ToString();
                _discount.Sgdd_req_ref = _customerReq;
                _discount.Sgdd_sale_tp = cmbInvType.Text.Trim();
                _discount.Sgdd_seq = 0;
                _discount.Sgdd_stus = false;
                _discount.Sgdd_to_dt = Convert.ToDateTime(txtdate.Text);
                _list.Add(_discount);
            }

            if (_isSuccessful)
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text)) txtDisAmount.Text = "0.0";
                try
                {
                    int _effect = CHNLSVC.Sales.SaveCashGeneralEntityDiscountWindows(CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _customerReq, Session["UserID"].ToString(), _list, txtCustomer.Text.Trim(), Convert.ToDecimal(txtDisAmount.Text.Trim()));
                    string Msg = string.Empty;
                    if (_effect > 0)
                    {
                        Msg = "Successfully saved! Document number: " + _customerReq + ".";
                        txtDisAmount.Text = "";
                    }
                    else
                    {
                        Msg = "Document not processed! please try again.";
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + " !!!')", true);
               
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }


        protected void gvDisItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            MPDis.Show();
            gvDisItem.EditIndex = e.NewEditIndex;
            gvDisItem.DataSource = _CashGeneralEntiryDiscount;
            gvDisItem.DataBind();
        }

        protected void btnDiscountEditItem_Click(object sender, EventArgs e)
        {
            MPDis.Show();

        }

        protected void btnDiscountUpdate_Click(object sender, EventArgs e)
        {
            MPDis.Show();
            gvDisItem.EditIndex = -1;
        }

        protected void btnDiscountCancelEdit_Click(object sender, EventArgs e)
        {
            try
            {
                MPDis.Show();
                gvDisItem.EditIndex = -1;
                gvDisItem.DataSource = _CashGeneralEntiryDiscount;
                gvDisItem.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "13a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtPoNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPoNo.Text.Trim()) && !string.IsNullOrEmpty(txtCustomer.Text.Trim()))
            {
                MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());
                if (_masterBusinessCompany != null && _masterBusinessCompany.Mbe_intr_com && _masterBusinessCompany.Mbe_tp == "C")
                {
                    List<InterCompanySalesParameter> oInterCompanySalesParameters = CHNLSVC.Sales.GET_INTERCOM_PAR_BY_CUST(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                    if (oInterCompanySalesParameters != null && oInterCompanySalesParameters.Count > 0)
                    {
                        btnSave.OnClientClick = "ConfirmPlaceOrder();";
                        btnSave.CssClass = "buttonUndocolor";

                        PurchaseOrder oPurchaseOrder = CHNLSVC.Inventory.GetPurchaseOrderHeaderDetails(oInterCompanySalesParameters[0].Sritc_to_com, txtPoNo.Text.Trim());
                        if (oPurchaseOrder != null && !string.IsNullOrEmpty(oPurchaseOrder.Poh_doc_no))
                        {
                            if (oInterCompanySalesParameters.FindAll(x => x.Sritc_sup == oPurchaseOrder.Poh_supp).Count > 0)
                            {
                                if (oPurchaseOrder.Poh_stus == "C")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected purchase order is canceled !!!')", true);
                                    txtPoNo.Text = "";
                                    return;
                                }
                                else if (oPurchaseOrder.Poh_stus == "P")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected purchase order is not approved !!!')", true);
                                    txtPoNo.Text = "";
                                    return;
                                }
                                else if (oPurchaseOrder.Poh_stus == "U")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected purchase order is already used !!!')", true);
                                    txtPoNo.Text = "";
                                    return;
                                }
                                else
                                {
                                    DataTable dtpoitems = CHNLSVC.Sales.LoadPoData(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtPoNo.Text.Trim());

                                    gvInvoiceItem.DataSource = null;
                                    gvInvoiceItem.DataBind();

                                    gvInvoiceItem.DataSource = dtpoitems;
                                    gvInvoiceItem.DataBind();

                                    DataTable dt1 = (DataTable)ViewState["SERIALSTABLE"];

                                    foreach (GridViewRow item in gvInvoiceItem.Rows)
                                    {
                                        dt1.Rows.Add(gvPopSerial.Rows.Count + 1, item.Cells[1].Text, string.Empty, item.Cells[3].Text, item.Cells[1].Text, item.Cells[4].Text, string.Empty, string.Empty, string.Empty);
                                        ViewState["SERIALSTABLE"] = dt1;
                                    }

                                    gvPopSerial.DataSource = dt1;
                                    gvPopSerial.DataBind();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected purchase order supplier is not related for the customer !!!')", true);
                                txtPoNo.Text = "";
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Purchase order is invalid !!!')", true);
                            txtPoNo.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Inter company parameters are not setuped !!!')", true);
                        btnSave.OnClientClick = "";
                        btnSave.CssClass = "buttoncolor";
                        return;
                    }
                }
            }
        }

        private string SavePOHeader()
        {
            try
            {
                string[] _purOrNo = new string[] { };
                Int32 row_aff = 0;
                string _PONo = string.Empty;
                string _msg = string.Empty;
                Int32 _isBaseCons = 0;
                string _type = string.Empty;

                _type = "N";
                _isBaseCons = 0;

                FF.BusinessObjects.PurchaseOrder _PurchaseOrder = new FF.BusinessObjects.PurchaseOrder();
                _PurchaseOrder.Poh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "PO", 1, Session["UserCompanyCode"].ToString());
                _PurchaseOrder.Poh_tp = "L";//"L";
                _PurchaseOrder.Poh_sub_tp = _type;
                _PurchaseOrder.Poh_doc_no = string.Empty;
                _PurchaseOrder.Poh_com = Session["UserCompanyCode"].ToString();
                _PurchaseOrder.Poh_ope = "INV";
                _PurchaseOrder.Poh_profit_cd = Session["UserDefProf"].ToString();
                _PurchaseOrder.Poh_dt = Convert.ToDateTime(txtdate.Text).Date;
                _PurchaseOrder.Poh_ref = txtCustomer.Text;
                _PurchaseOrder.Poh_job_no = string.Empty;
                _PurchaseOrder.Poh_pay_term = "0";
                _PurchaseOrder.Poh_supp = txtCustomer.Text;
                _PurchaseOrder.Poh_cur_cd = txtcurrency.Text;
                _PurchaseOrder.Poh_ex_rt = Convert.ToDecimal(1);
                _PurchaseOrder.Poh_trans_term = "";
                _PurchaseOrder.Poh_port_of_orig = "";
                _PurchaseOrder.Poh_cre_period = "0";
                _PurchaseOrder.Poh_frm_yer = Convert.ToDateTime(txtdate.Text).Year;
                _PurchaseOrder.Poh_frm_mon = Convert.ToDateTime(txtdate.Text).Month;
                _PurchaseOrder.Poh_to_yer = Convert.ToDateTime(txtdate.Text).Year;
                _PurchaseOrder.Poh_to_mon = Convert.ToDateTime(txtdate.Text).Month;
                _PurchaseOrder.Poh_preferd_eta = Convert.ToDateTime(txtdate.Text).Date;
                _PurchaseOrder.Poh_contain_kit = false;
                _PurchaseOrder.Poh_sent_to_vendor = false;
                _PurchaseOrder.Poh_sent_by = "";
                _PurchaseOrder.Poh_sent_via = "";
                _PurchaseOrder.Poh_sent_add = "";
                _PurchaseOrder.Poh_stus = "P";
                _PurchaseOrder.Poh_remarks = string.Empty;
                _PurchaseOrder.Poh_sub_tot = Convert.ToDecimal(lblGrndSubTotal.Text);
                _PurchaseOrder.Poh_tax_tot = Convert.ToDecimal(lblGrndTax.Text);
                _PurchaseOrder.Poh_dis_rt = 0;
                _PurchaseOrder.Poh_dis_amt = Convert.ToDecimal(lblGrndDiscount.Text);
                _PurchaseOrder.Poh_oth_tot = 0;
                _PurchaseOrder.Poh_tot = Convert.ToDecimal(lblGrndTotalAmount.Text);
                _PurchaseOrder.Poh_reprint = false;
                _PurchaseOrder.Poh_tax_chg = false;
                _PurchaseOrder.poh_is_conspo = _isBaseCons;
                _PurchaseOrder.Poh_cre_by = Session["UserID"].ToString();


                List<PurchaseOrderDetail> _POItemListSave = new List<PurchaseOrderDetail>();
                _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;

                foreach (PurchaseOrderDetail line in _POItemList)
                {
                    line.Pod_seq_no = _PurchaseOrder.Poh_seq_no;
                    line.Pod_pi_bal = line.Pod_qty;
                    _POItemListSave.Add(line);
                }

                List<PurchaseOrderDelivery> _PODelSave = new List<PurchaseOrderDelivery>();
                _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
                foreach (PurchaseOrderDelivery line in _POItemDel)
                {
                    line.Podi_seq_no = _PurchaseOrder.Poh_seq_no;
                    _PODelSave.Add(line);
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                masterAuto.Aut_cate_tp = "COM";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "PUR";
                masterAuto.Aut_number = 5;
                masterAuto.Aut_start_char = "PUR";
                masterAuto.Aut_year = null;

                string QTNum;
                _PurchaseReqList = ViewState["PurchaseReqList"] as List<PurchaseReq>;
                _InventoryRequestItem = ViewState["InventoryRequestItem"] as List<InventoryRequestItem>;

                row_aff = (Int32)CHNLSVC.Inventory.SaveNewPO(_PurchaseOrder, _POItemListSave, _PODelSave, null, masterAuto, _PurchaseReqList, _InventoryRequestItem, out QTNum);

                if (row_aff == 1)
                {
                    //SuccessMsgPOrder("Purchase order generated.PO # : " + QTNum);

                    //ClearTextBox();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        //ErrorMsgPOrder(_msg);
                    }
                    else
                    {
                        //ErrorMsgPOrder("Faild to generate");
                    }
                }
                return QTNum;
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                return string.Empty;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadSoData(txtInvoiceNo.Text, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                LoadSoItemData(txtInvoiceNo.Text);
                LoadSoSerialData(txtInvoiceNo.Text);
                CheckInvoiceNo();

                _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());

                if (_masterBusinessCompany.Mbe_is_tax)
                {
                    chkTaxPayable.Checked = true;
                    chkTaxPayable.Enabled = true;
                }
                else
                {
                    chkTaxPayable.Checked = false;
                    chkTaxPayable.Enabled = false;
                }
                ViewCustomerAccountDetail(txtCustomer.Text.Trim());

                string stus = (string)Session["STUS"];

                if (stus == "R")
                {
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";

                    lbtnapprove.CssClass = "buttoncolor";
                    lbtnapprove.OnClientClick = "return Enable();";

                    lbtncancel.CssClass = "buttoncolor";
                    lbtncancel.OnClientClick = "return Enable();";

                    lbtnreject.CssClass = "buttoncolor";
                    lbtnreject.OnClientClick = "return Enable();";
                }

                else if (stus == "A")
                {
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";

                    lbtnapprove.CssClass = "buttoncolor";
                    lbtnapprove.OnClientClick = "return Enable();";

                    lbtnreject.CssClass = "buttoncolor";
                    lbtnreject.OnClientClick = "return Enable();";

                    lbtncancel.CssClass = "buttonUndocolor";
                    lbtncancel.OnClientClick = "ConfirmCancelOrder();";
                }

                else if (stus == "C")
                {
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";

                    lbtnapprove.CssClass = "buttoncolor";
                    lbtnapprove.OnClientClick = "return Enable();";

                    lbtnreject.CssClass = "buttoncolor";
                    lbtnreject.OnClientClick = "return Enable();";

                    lbtncancel.CssClass = "buttoncolor";
                    lbtncancel.OnClientClick = "return Enable();";
                }
                else if (stus == "S")
                {
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";

                    lbtnapprove.CssClass = "buttonUndocolor";
                    lbtnapprove.OnClientClick = "ConfirmApproveOrder();";

                    lbtnreject.CssClass = "buttonUndocolor";
                    lbtnreject.OnClientClick = "ConfirmRejectOrder();";

                    lbtncancel.CssClass = "buttoncolor";
                    lbtncancel.OnClientClick = "return Enable();";
                }
                else
                {
                    btnSave.CssClass = "buttonUndocolor";
                    btnSave.OnClientClick = "ConfirmPlaceOrder();";

                    lbtnapprove.CssClass = "buttonUndocolor";
                    lbtnapprove.OnClientClick = "ConfirmApproveOrder();";

                    lbtnreject.CssClass = "buttonUndocolor";
                    lbtnreject.OnClientClick = "ConfirmRejectOrder();";

                    lbtncancel.CssClass = "buttonUndocolor";
                    lbtncancel.OnClientClick = "ConfirmCancelOrder();";
                }

                foreach (GridViewRow row in gvInvoiceItem.Rows)
                {
                    CalculateGrandTotalNew(Convert.ToDecimal(row.Cells[4].Text), Convert.ToDecimal(row.Cells[5].Text), Convert.ToDecimal(row.Cells[8].Text), Convert.ToDecimal(row.Cells[9].Text), true);
                }


                Decimal grandtot = Convert.ToDecimal(lblGrndAfterDiscount.Text) + Convert.ToDecimal(lblGrndTax.Text);
                lblGrndTotalAmount.Text = DoFormat(grandtot);

                dvhiddendel.Visible = false;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtndelitem_Click(object sender, EventArgs e)
        {
            if (gvInvoiceItem.Rows.Count == 0) return;
                var lbtn = (LinkButton)sender;
                var row = (GridViewRow)lbtn.NamingContainer;
                if (row != null)
                {
                    DataTable _itmtbl = ViewState["ITEMSTABLE"] as DataTable;
                    DataTable _serialtbl = ViewState["SERIALSTABLE"] as DataTable;
                    if (_itmtbl.Rows.Count > 0)
                    {
                       // string itm = (row.FindControl("soi_itm_cd") as Label).Text;
                        string itm = row.Cells[1].Text;
                        var row_i = _itmtbl.Select("soi_itm_cd =  '"+itm+"'");
                        foreach (var row_d in row_i)
                            row_d.Delete();

                        if (_serialtbl.Rows.Count >0)
                        {
                            var row_s = _itmtbl.Select("sose_itm_cd =  '" + itm + "'");
                            foreach (var row_d in row_s)
                                row_d.Delete();
                        }
                        gvInvoiceItem.DataSource = _itmtbl;
                        gvInvoiceItem.DataBind();
                        gvPopSerial.DataSource = _serialtbl;
                        gvPopSerial.DataBind();
                        ViewState["ITEMSTABLE"] = _itmtbl;
                        ViewState["SERIALSTABLE"] = _serialtbl;
                    }

                   
                }
                
            
        }


        protected void lbtndelserial_Click(object sender, EventArgs e)
        {
            if (gvPopSerial.Rows.Count == 0) return;
            var lbtn = (LinkButton)sender;
            var row = (GridViewRow)lbtn.NamingContainer;
            if (row != null)
            {
                DataTable _itmtbl = ViewState["ITEMSTABLE"] as DataTable;
                DataTable _serialtbl = ViewState["SERIALSTABLE"] as DataTable;
                if (_itmtbl.Rows.Count > 0)
                {
                    // string itm = (row.FindControl("soi_itm_cd") as Label).Text;
                    string itm = row.Cells[1].Text;
                    var row_i = _itmtbl.Select("soi_itm_cd =  '" + itm + "'");
                    foreach (var row_d in row_i)
                        row_d.Delete();

                    if (_serialtbl.Rows.Count > 0)
                    {
                        var row_s = _itmtbl.Select("sose_itm_cd =  '" + itm + "'");
                        foreach (var row_d in row_s)
                            row_d.Delete();
                    }
                    gvInvoiceItem.DataSource = _itmtbl;
                    gvInvoiceItem.DataBind();
                    gvPopSerial.DataSource = _serialtbl;
                    gvPopSerial.DataBind();
                    ViewState["ITEMSTABLE"] = _itmtbl;
                    ViewState["SERIALSTABLE"] = _serialtbl;
                }


            }


        }

        protected void btnInvItemDisRate_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                lblDiscountRowNum.Text = dr.RowIndex.ToString();
                mpDiscountRate.Show();
                txtDisRateInvItem.Text = "";
                txtDisRateInvItem.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnApplyDiscountRate_Click(object sender, EventArgs e)
        {
            mpDiscountRate.Show();
            DataTable _invoiceItemTbl = ViewState["ITEMSTABLE"] as DataTable; 
            GridViewRow dr = gvInvoiceItem.Rows[Convert.ToInt32(lblDiscountRowNum.Text)];
            Label lblsad_disc_rt = dr.FindControl("lblsad_disc_rt") as Label;
            Label lblsad_itm_line = dr.FindControl("lblsad_itm_line") as Label;
            Label InvItm_Book = dr.FindControl("InvItm_Book") as Label;
            Label InvItm_Level = dr.FindControl("InvItm_Level") as Label;
            Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
            Label lblsad_itm_stus = dr.FindControl("lblsad_itm_stus") as Label;

            decimal _prevousDisRate = Convert.ToDecimal(lblsad_disc_rt.Text);
            int _lineno0 = Convert.ToInt32(dr.Cells[0].Text.ToString());
            string _book = Convert.ToString(dr.Cells[11].Text.ToString());
            string _level = Convert.ToString(dr.Cells[12].Text.ToString());
            string _item = Convert.ToString(dr.Cells[1].Text.ToString());
            string _status = Convert.ToString(dr.Cells[3].Text.ToString());
            bool _isSerialized = false;

            string _userDisRate = txtDisRateInvItem.Text.Trim();
            if (string.IsNullOrEmpty(_userDisRate))
                return;
            if (IsNumeric(_userDisRate) == false || Convert.ToDecimal(_userDisRate) > 100 || Convert.ToDecimal(_userDisRate) < 0)
            {

                DisplayMessage("Please select the valid discount rate", 2);
                return;
            }
            decimal _disRate = Convert.ToDecimal(_userDisRate);
            bool _IsPromoVou = false;
            if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();

            //if (string.IsNullOrEmpty(lblPromoVouNo.Text))
            //{
            GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(txtdate.Text.Trim()).Date, _book, _level, txtCustomer.Text.Trim(), _item, _isSerialized, false);
            //}
            //else
            //{
            //GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCustomer.Text.Trim(), Convert.ToDateTime(txtdate.Text.Trim()).Date, _book, _level, _item, lblPromoVouNo.Text.Trim());
            //if (GeneralDiscount != null)
            //{
            //    _IsPromoVou = true;
            //    GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
            //}
            // }


            if (GeneralDiscount != null)
            {
                decimal vals = GeneralDiscount.Sgdd_disc_val;
                decimal rates = GeneralDiscount.Sgdd_disc_rt;

                //if (lblPromoVouUsedFlag.Text.Contains("U") == true)
                //{
                //    DisplayMessage("Voucher already used!");
                //    txtDisRate.Text = FormatToCurrency("0");
                //    _isEditDiscount = false;
                //    return;
                //}
                if (_IsPromoVou == true)
                {
                    if (rates == 0 && vals > 0)
                    {
                        // CalculateItem();
                        if (Convert.ToDecimal(txtDisAmt.Text) > vals)
                        {
                            DisplayMessage("Voucher discount amount should be " + vals + ".\nNot allowed discount rate " + _disRate + "%", 2);
                            _isEditDiscount = false;
                            return;
                        }
                    }
                    else
                    {
                        if (rates != _disRate)
                        {
                            DisplayMessage("Voucher discount rate should be " + rates + "% !.\nNot allowed discount rate " + _disRate + "% discounted price is " + txtLineTotAmt.Text, 2);
                            _isEditDiscount = false;
                            return;
                        }
                    }
                }
                else
                {
                    if (rates < _disRate)
                    {
                        DisplayMessage("You cannot apply discount more than " + rates + "%.", 2);
                        return;
                    }
                }
            }
            else
            {
                DisplayMessage("You are not allow for apply discount", 2);
                return;
            }

            if (_isEditDiscount == true)
            {
                if (_IsPromoVou == true)
                {
                    //lblPromoVouUsedFlag.Text = "U";
                    _proVouInvcItem = _item;
                }
            }

            var _itm = _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList();
            if (_item != null && _item.Count() > 0) foreach (InvoiceItem _one in _itm)
                {
                    CalculateGrandTotal(_one.Sad_qty, _one.Sad_unit_rt, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, false);
                    decimal _unitRate = _one.Sad_unit_rt;
                    decimal _unitAmt = _one.Sad_unit_amt;
                    decimal _disVal = 0;
                    decimal _vatPortion = 0;
                    decimal _lineamount = 0;
                    decimal _newvatval = 0;

                    bool _isTaxDiscount = chkTaxPayable.Checked ? true : (lblSVatStatus.Text == "Available") ? true : false;

                    if (_isTaxDiscount)
                    {
                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                        _disVal = FigureRoundUp(_unitAmt * _disRate / 100, true);
                        _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                    }
                    else
                    {
                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                        _disVal = FigureRoundUp((_unitAmt + _vatPortion) * _disRate / 100, true);

                        if (_disRate > 0)
                        {
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                _newvatval = ((_unitRate * _one.Sad_qty + _vatPortion - _disVal) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            }
                        }
                        if (_disRate > 0)
                        {
                            _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                            _vatPortion = FigureRoundUp(_newvatval, true);
                        }
                        else
                        {
                            _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                        }
                    }
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_rt = _disRate);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_amt = _disVal);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_itm_tax_amt = _vatPortion);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_tot_amt = FigureRoundUp(_lineamount, true));
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_seq = GeneralDiscount.Sgdd_seq);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_type = "M");

                    //if (_proVouInvcItem == txtItem.Text.ToString())
                    //{
                    //    if (string.IsNullOrEmpty(lblPromoVouUsedFlag.Text) && !string.IsNullOrEmpty(lblPromoVouNo.Text))
                    //    {
                    //        lblPromoVouUsedFlag.Text = "U";
                    //        _proVouInvcLine = _lineno0;
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString()));
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_res_no = "PROMO_VOU");
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_seq = Convert.ToInt32(lblPromoVouNo.Text.ToString()));
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_type = "M");
                    //        //_tempItem.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString());
                    //        //_tempItem.Sad_res_no = "PROMO_VOU";
                    //    }
                    //}
                    BindAddItem();
                    CalculateGrandTotal(_one.Sad_qty, _unitRate, _disVal, _vatPortion, true);
                    decimal _tobepays = 0;
                    if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()); else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                    //ucPayModes1.TotalAmount = _tobepays;
                    ////update promotion
                    //List<InvoiceItem> _temItems = (from _invItm in _invoiceItemList
                    //                               where !string.IsNullOrEmpty(_invItm.Sad_promo_cd)
                    //                               select _invItm).ToList<InvoiceItem>();
                    //if (_temItems != null && _temItems.Count > 0)
                    //{
                    //    ucPayModes1.ISPromotion = true;
                    //}
                    //else
                    //    ucPayModes1.ISPromotion = false;
                    //ucPayModes1.InvoiceItemList = _invoiceItemList;
                    //ucPayModes1.SerialList = InvoiceSerialList;
                    //ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                    //ucPayModes1.lblBalanceAmountPub.Text = _tobepays.ToString("N2"); ;
                    //if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                    //{
                    //    ucPayModes1.LoadData();
                    //}
                    mpDiscountRate.Hide();
                }

        }
        protected void btnCLoseDisRate_Click(object sender, EventArgs e)
        {
            txtDisRateInvItem.Text = "";
            mpDiscountRate.Hide();
        }
        protected void BindAddItem()
        {
            DataTable _invoiceItemTbl = ViewState["ITEMSTABLE"] as DataTable; 
            gvInvoiceItem.DataSource = new List<InvoiceItem>();
            gvInvoiceItem.DataSource = _invoiceItemTbl;
            gvInvoiceItem.DataBind();

            if (_invoiceItemTbl == null)
            {
                AddItemDisableCustomer(false);
            }
            if (_invoiceItemTbl.Rows.Count <= 0)
            {
                AddItemDisableCustomer(false);
            }

            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (InvoiceItem item in _invoiceItemList)
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

                gvInvoiceItem.DataSource = new List<InvoiceItem>();
                gvInvoiceItem.DataSource = _invoiceItemTbl;
                gvInvoiceItem.DataBind();
                ViewState["ITEMSTABLE"] = _invoiceItemTbl;
            }
        }
       
        protected void lbtnApplyUnitRate_Click(object sender, EventArgs e)
        {
            MpPriceEdit.Show();

            GridViewRow dr = gvInvoiceItem.Rows[Convert.ToInt32(lblDiscountRowNum.Text)];
            Label lblsad_disc_rt = dr.FindControl("lblsad_disc_rt") as Label;
            Label lblsad_itm_line = dr.FindControl("lblsad_itm_line") as Label;
            Label InvItm_Book = dr.FindControl("InvItm_Book") as Label;
            Label InvItm_Level = dr.FindControl("InvItm_Level") as Label;
            Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
            Label lblsad_itm_stus = dr.FindControl("lblsad_itm_stus") as Label;
            Label InvItm_UPrice = dr.FindControl("InvItm_UPrice") as Label;
            Label InvItm_Qty = dr.FindControl("InvItm_Qty") as Label;


            decimal _prevousDisRate = Convert.ToDecimal(lblsad_disc_rt.Text);
            int _lineno0 = Convert.ToInt32(lblsad_itm_line.Text);
            string _book = Convert.ToString(InvItm_Book.Text);
            string _level = Convert.ToString(InvItm_Level.Text);
            string _item = Convert.ToString(InvItm_Item.Text);
            string _status = Convert.ToString(lblsad_itm_stus.Text);
            bool _isSerialized = false;

            string _userUnitRate = txtPriceEdit.Text.Trim();
            if (string.IsNullOrEmpty(_userUnitRate))
                return;
            SarDocumentPriceDefn GeneralDiscount_new = new SarDocumentPriceDefn();
            GeneralDiscount_new = CHNLSVC.Sales.GetDocPriceDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _book, _level, cmbInvType.Text.ToString());

            decimal _pb_price;
            decimal _qty = Convert.ToDecimal(InvItm_Qty.Text);
            decimal _disRate = Convert.ToDecimal(lblsad_disc_rt.Text);
            decimal _txtUprice = Convert.ToDecimal(_userUnitRate);
            _pb_price = Convert.ToDecimal(InvItm_UPrice.Text);
            if (_pb_price == 0)
            {
               DisplayMessage("Not allow price edit for price!",2);
                return;
            }
            decimal _Amt;
            bool _isUnitAmt = Decimal.TryParse(InvItm_UPrice.Text, out _Amt);
            if (!_isUnitAmt)
            {
                DisplayMessage("Unit Price has to be number!",2);
                MpPriceEdit.Show();
                return;
            }
            if (_Amt <= 0)
            {
                DisplayMessage("Unit Price has to be greater than 0!",2);
                MpPriceEdit.Show();
                return;
            }
            if (GeneralDiscount_new.SADD_IS_EDIT == 0 && _txtUprice > 0)
            {
                DisplayMessage("Not allow price edit for price!",2);
                MpPriceEdit.Show();
                return;
            }
            if (GeneralDiscount_new.SADD_IS_EDIT == 1)
            {
                if (_pb_price > _txtUprice)
                {
                    decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                    if (_diffPecentage > GeneralDiscount_new.SADD_EDIT_RT)
                    {
                        DisplayMessage("You cannot deduct price more than " + GeneralDiscount_new.SADD_EDIT_RT + "% from the price book price.",2);
                        //txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                        //txtUnitPrice.Text = _pb_price.ToString("N2");
                        //_isEditPrice = false;
                        MpPriceEdit.Show();
                        return;
                    }
                    else
                    {
                        _isEditPrice = true;
                    }
                }
                else
                {
                    if (GeneralDiscount_new.SADD_IS_EDIT == 1)
                    {
                        _isEditPrice = true;
                    }
                    else
                    {
                        decimal _diffPecentage = ((_txtUprice - _pb_price) / _txtUprice) * 100;
                        if (_diffPecentage > GeneralDiscount_new.SADD_EDIT_RT)
                        {
                            DisplayMessage("You cannot increase price more than " + GeneralDiscount_new.SADD_EDIT_RT + "% from the price book price.", 2);
                            // txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                            //txtUnitPrice.Text = _pb_price.ToString("N2");
                            //_isEditPrice = false;
                            MpPriceEdit.Show();
                            return;
                        }
                        else
                        {
                            _isEditPrice = true;
                        }
                    }
                }
            }


            var _itm = _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList();
            if (_item != null && _item.Count() > 0) foreach (InvoiceItem _one in _itm)
                {
                    CalculateGrandTotal(_one.Sad_qty, _one.Sad_unit_rt, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, false);
                    decimal _unitRate = _one.Sad_unit_rt;
                    decimal _unitAmt = _one.Sad_unit_amt;
                    decimal _disVal = 0;
                    decimal _vatPortion = 0;
                    decimal _lineamount = 0;
                    decimal _newvatval = 0;

                    bool _isTaxDiscount = chkTaxPayable.Checked ? true : (lblSVatStatus.Text == "Available") ? true : false;

                    if (_isTaxDiscount)
                    {
                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                        _disVal = FigureRoundUp(_unitAmt * _disRate / 100, true);
                        _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                    }
                    else
                    {
                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                        _disVal = FigureRoundUp((_unitAmt + _vatPortion) * _disRate / 100, true);

                        if (_disRate > 0)
                        {
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                _newvatval = ((_unitRate * _one.Sad_qty + _vatPortion - _disVal) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            }
                        }
                        if (_disRate > 0)
                        {
                            _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                            _vatPortion = FigureRoundUp(_newvatval, true);
                        }
                        else
                        {
                            _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                        }
                    }
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_rt = _disRate);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_amt = _disVal);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_itm_tax_amt = _vatPortion);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_tot_amt = FigureRoundUp(_lineamount, true));
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_seq = GeneralDiscount.Sgdd_seq);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_type = "M");

                    //if (_proVouInvcItem == txtItem.Text.ToString())
                    //{
                    //    if (string.IsNullOrEmpty(lblPromoVouUsedFlag.Text) && !string.IsNullOrEmpty(lblPromoVouNo.Text))
                    //    {
                    //        lblPromoVouUsedFlag.Text = "U";
                    //        _proVouInvcLine = _lineno0;
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString()));
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_res_no = "PROMO_VOU");
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_seq = Convert.ToInt32(lblPromoVouNo.Text.ToString()));
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_type = "M");
                    //        //_tempItem.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString());
                    //        //_tempItem.Sad_res_no = "PROMO_VOU";
                    //    }
                    //}
                    BindAddItem();
                    CalculateGrandTotal(_one.Sad_qty, _unitRate, _disVal, _vatPortion, true);
                    decimal _tobepays = 0;
                    if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()); else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                    //ucPayModes1.TotalAmount = _tobepays;
                    ////update promotion
                    //List<InvoiceItem> _temItems = (from _invItm in _invoiceItemList
                    //                               where !string.IsNullOrEmpty(_invItm.Sad_promo_cd)
                    //                               select _invItm).ToList<InvoiceItem>();
                    //if (_temItems != null && _temItems.Count > 0)
                    //{
                    //    ucPayModes1.ISPromotion = true;
                    //}
                    //else
                    //    ucPayModes1.ISPromotion = false;
                    //ucPayModes1.InvoiceItemList = _invoiceItemList;
                    //ucPayModes1.SerialList = InvoiceSerialList;
                    //ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                    //ucPayModes1.lblBalanceAmountPub.Text = _tobepays.ToString("N2"); ;
                    //if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                    //{
                    //    ucPayModes1.LoadData();
                    //}
                    //mpDiscountRate.Hide();
                }

        }
        //protected void btnCLoseDisRate_Click(object sender, EventArgs e)
        //{
        //    txtDisRateInvItem.Text = "";
        //    mpDiscountRate.Hide();
           

        //}
    }
}