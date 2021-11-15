using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class ProductionPlan : Base
    {
        protected MasterBusinessEntity _masterBusinessCompany { get { return (MasterBusinessEntity)Session["_masterBusinessCompany"]; } set { Session["_masterBusinessCompany"] = value; } }
        protected List<PriceDefinitionRef> _PriceDefinitionRef { get { return (List<PriceDefinitionRef>)Session["_PriceDefinitionRef"]; } set { Session["_PriceDefinitionRef"] = value; } }
        protected List<SatProjectDetails> _SatProjectDetails { get { return (List<SatProjectDetails>)Session["_SatProjectDetails"]; } set { Session["_SatProjectDetails"] = value; } }
        protected MasterItem _itemdetail { get { return (MasterItem)Session["_itemdetail"]; } set { Session["_itemdetail"] = value; } }
        protected SatProjectHeader _SatProjectHeader { get { return (SatProjectHeader)Session["_SatProjectHeader"]; } set { Session["_SatProjectHeader"] = value; } }

        protected List<ItemKitComponent> _ItemKitComponent { get { return (List<ItemKitComponent>)Session["_ItemKitComponent"]; } set { Session["_ItemKitComponent"] = value; } }
        protected List<ProductionFinGood> _ProductionFinGood { get { return (List<ProductionFinGood>)Session["_ProductionFinGood"]; } set { Session["_ProductionFinGood"] = value; } }
        protected List<ProductionPlaneDetails> _ProductionPlaneDetails { get { return (List<ProductionPlaneDetails>)Session["_ProductionPlaneDetails"]; } set { Session["_ProductionPlaneDetails"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageClear();
                LoadCachedObjects();
                LoadPriceBook();
            }
            else
            {

                caltotal();
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16067))
                {
                    pnlchart.Visible = false;
                }
                else
                {
                    pnlchart.Visible = true;
                }
            }
        }
        private void PageClear()
        {
            DateTime orddate = DateTime.Now;
            txtDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtcompletedate.Text = orddate.ToString("dd/MMM/yyyy");
            txtend.Text = orddate.ToString("dd/MMM/yyyy");
            txtlcomm.Text = orddate.ToString("dd/MMM/yyyy");         
            txtFDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtTDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtkititemcode.Text = string.Empty;
            txtkitqty.Text = "1";
            _ItemKitComponent = new List<ItemKitComponent>();
            _ProductionFinGood = new List<ProductionFinGood>();
            _ProductionPlaneDetails = new List<ProductionPlaneDetails>();
            _masterBusinessCompany = new MasterBusinessEntity();
            _PriceDefinitionRef = new List<PriceDefinitionRef>();
            _SatProjectDetails = new List<SatProjectDetails>();
            _SatProjectHeader = new SatProjectHeader();
            grdCost.DataSource = new int[] { };
            grdCost.DataBind();
            LoadProductLineNumber();
            lblTotalCost.Text = "0.0";
            lblTotalRevenue.Text = "0.0";
            grdFGood.DataSource = _ProductionFinGood;
            grdFGood.DataBind();
            grdProLine.DataSource = _ProductionPlaneDetails;
            grdProLine.DataBind();
            lblcost.Text = "0.0";
            lblItemBrand.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblprofit.Text = "0.0";
            lblprofitvalue.Text = "0.0";
            lblRevenue.Text = "0.0";
            lblstatus.Text = string.Empty;
            lblTotalCost.Text = "0.0";
            lblTotalRevenue.Text = "0.0";
            ClearCustomer(true);
            txtdoc.Text = string.Empty;
            txtlocation.Text = string.Empty;
            txtref.Text = string.Empty;

            DataTable dt = GetData(0, 0);
            LoadChartData(dt);
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
            if (lblvalue.Text == "Customer")
            {
                txtCustomer.Text = grdResult.SelectedRow.Cells[1].Text;
                LoadCustomerDetailsByCustomer();
                return;
            }
            if (lblvalue.Text == "Sale_Ex")
            {
                //txtexcutive.Text = grdResult.SelectedRow.Cells[1].Text;
                //txtexcutive.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                //lblSalesEx.Text = grdResult.SelectedRow.Cells[2].Text;
            }
            if (lblvalue.Text == "Item")
            {
                txtCItem.Text = grdResult.SelectedRow.Cells[1].Text;
                txtItem_TextChanged(null, null);
            }
            if (lblvalue.Text == "Item_2")
            {
                txtFinItem.Text = grdResult.SelectedRow.Cells[1].Text;
                
            }
            if (lblvalue.Text == "location")
            {
                txtlocation.Text = grdResult.SelectedRow.Cells[1].Text;
            }
            if (lblvalue.Text == "kit")
            {
                txtkititemcode.Text = grdResult.SelectedRow.Cells[1].Text;
            }
            lblvalue.Text = "";
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "kit")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.GetKitItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "location")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "NIC")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                grdResult.DataSource = result2;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Mobile")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                grdResult.DataSource = result2;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Sale_Ex")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Item"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Item_2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_2);
                DataTable result = CHNLSVC.CommonSearch.GetItemforKit(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
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
            if (lblvalue.Text == "kit")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable result = CHNLSVC.CommonSearch.GetKitItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykeyD.SelectedItem.Text, "%" + txtSearchbywordD.Text);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "location")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Sale_Ex")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Sale_Ex";
                txtSearchbyword.Focus();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "NIC")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                grdResult.DataSource = result2;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Mobile")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                grdResult.DataSource = result2;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Item") )
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result2;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Item_2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_2);
                DataTable result2 = CHNLSVC.CommonSearch.GetItemforKit(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result2;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
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

        #endregion
        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
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
                catch (Exception ex)
                {
                    //divalert.Visible = true;
                    DisplayMessage(ex.Message, 4);
                }
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
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void LoadCustomerDetailsByCustomer()
        {
            if (string.IsNullOrEmpty(txtCustomer.Text)) return;
            try
            {

                _masterBusinessCompany = new MasterBusinessEntity();

                if (!string.IsNullOrEmpty(txtCustomer.Text))
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());

                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        DisplayMessage("This customer already inactive. Please contact IT dept", 1);
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }

                    txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                    txtCusName.Text = _masterBusinessCompany.Mbe_name;
                    txtAddress1.Text = _masterBusinessCompany.Mbe_add1;
                    txtAddress2.Text = _masterBusinessCompany.Mbe_add2;
                    txtMobile.Text = _masterBusinessCompany.Mbe_mob;
                    txtNIC.Text = _masterBusinessCompany.Mbe_nic;
                    if (_masterBusinessCompany.MBE_TIT == "")
                    {
                        cmbTitle.SelectedItem.Text = "MR.";
                    }
                    else
                    {
                        cmbTitle.SelectedItem.Text = _masterBusinessCompany.MBE_TIT;

                    }
                    EnableDisableCustomer();
                }
                else
                {
                    DisplayMessage("Please select the valid customer",1);
                    ClearCustomer(true);
                    txtCustomer.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }
        private void EnableDisableCustomer()
        {
            try
            {

                txtCusName.Enabled = false;
                txtAddress1.Enabled = false;
                txtAddress2.Enabled = false;
                txtMobile.Enabled = false;
                txtNIC.Enabled = false;

            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void LoadProductLineNumber()
        {
            List<ProductionLine> _line = new List<ProductionLine>();
            ProductionLine _lineobj = new ProductionLine();
            _lineobj.MPL_COM=Session["UserCompanyCode"].ToString();
            _lineobj.MPL_LOC=Session["UserDefLoca"].ToString();
            _line = CHNLSVC.Inventory.GET_PRODUCTLINE(_lineobj);
            ddlLine.DataSource = _line;
            ddlLine.DataTextField = "mpl_line_desc";
            ddlLine.DataValueField = "MPL_LINE_CD";
            ddlLine.DataBind();
            ddlLine.Items.Insert(0, new ListItem("--Select--", "0"));
        }
       private void LoadCachedObjects()
       {

           _PriceDefinitionRef = (List<PriceDefinitionRef>)Session["_PriceDefinitionRef_1"];// CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());

       }
       private void LoadPriceBook()
       {
           bool _isAvailable = false;
           if (_PriceDefinitionRef != null)
               if (_PriceDefinitionRef.Count > 0)
               {
                   _isAvailable = true;
                   var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();
                   var _books = _PriceDefinitionRef.Select(x => x.Sadd_pb).Distinct().ToList();
                   _books.Add("");
                   ddlPriceBook.DataTextField = "";
                   ddlPriceBook.DataValueField = "";
                   ddlPriceBook.DataSource = _books;
                   ddlPriceBook.DataBind();
                   if (_defaultValue.Count > 0)
                       ddlPriceBook.SelectedValue = _defaultValue[0].Sadd_pb;
                   LoadPriceLevel(ddlPriceBook.Text);


               }
               else
                   ddlPriceBook.DataSource = null;
           else
               ddlPriceBook.DataSource = null;


       }
       private void LoadPriceLevel(string _book)
       {
           bool _isAvailable = false;
           if (_PriceDefinitionRef != null)
               if (_PriceDefinitionRef.Count > 0)
               {
                   _isAvailable = true;
                   var _levels = _PriceDefinitionRef.Where(x => x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                   _levels.Add("");
                   //filter non for sales 20/Apr/2016
                   for (int i = _levels.Count - 1; i >= 0; i--)
                   {
                       PriceBookLevelRef _filterLevel = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), _book, _levels[i]);
                       if (_filterLevel.Sapl_is_sales == false)
                       {
                           _levels.Remove(_levels[i]);
                       }

                   }


                   ddlLevel.DataTextField = "";
                   ddlLevel.DataValueField = "";
                   ddlLevel.DataSource = _levels;
                   ddlLevel.DataBind();
                   //cmbLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                   var _def = _PriceDefinitionRef.Where(x => x.Sadd_pb == _book && x.Sadd_def == true).ToList();
                   //var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();

                   if ((_def != null) && (_def.Count > 0))
                   {
                       if (_def.Count >= 1)
                       {
                           ddlLevel.SelectedValue = _def[0].Sadd_p_lvl;
                       }
                       else
                       {
                           ddlLevel.SelectedIndex = 0;
                       }
                   }
                   else
                   {

                       // cmbLevel.SelectedIndex = 0;
                   }


               }
               else
                   ddlLevel.DataSource = null;
           else ddlLevel.DataSource = null;


       }
       private void DisplayMessage(String Msg, Int32 option, Exception ex = null)
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
           }
       }
       private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
       {
           StringBuilder paramsText = new StringBuilder();
           string seperator = "|";
           paramsText.Append(((int)_type).ToString() + ":");

           switch (_type)
           {
                     case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
               case CommonUIDefiniton.SearchUserControlType.UserLocation:
                   {
                       paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                       break;
                   }
               case CommonUIDefiniton.SearchUserControlType.Customer:
                   {
                       paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                       break;
                   }
               case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                   {
                       paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                       break;
                   }
               case CommonUIDefiniton.SearchUserControlType.DocNo:
                   {
                       paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "PRO" + seperator);
                       break;
                   }
               case CommonUIDefiniton.SearchUserControlType.Item_2:
                   {
                       paramsText.Append(txtkititemcode.Text + seperator);
                       break;
                   }
               default:
                   break;
           }

           return paramsText.ToString();
       }
       protected void txtkititemcode_TextChanged(object sender, EventArgs e)
       {
           txtkititemcode.Text = txtkititemcode.Text.Trim().ToUpper();
           if (!string.IsNullOrEmpty(txtkititemcode.Text))
           {
               _itemdetail = CHNLSVC.General.GetItemMaster(txtkititemcode.Text.Trim().ToUpper());
               if (_itemdetail != null && _itemdetail.Mi_cd != null)
               {
                   if (_itemdetail.Mi_itm_tp == "K")
                   {

                   }
                   else
                   {
                       DisplayMessage("Please enter a valid kIT item code", 2);
                       txtkititemcode.Text = string.Empty;
                   }
               }
               else
               {
                   DisplayMessage("Please enter a valid kIT item code", 2);
                   txtkititemcode.Text = string.Empty;
               }
                 
           }
       }
       private bool LoadItemDetail(string _item)
       {
           lblItemDescription.Text = string.Empty;
           lblItemModel.Text = string.Empty;
           lblItemBrand.Text = string.Empty;
           _itemdetail = new MasterItem();

           bool _isValid = false;

           if (!string.IsNullOrEmpty(_item))
           {
               _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
           }
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
               Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;
           }
           else _isValid = false;
           return _isValid;
       }
       protected void lbtnKitadd_Click(object sender, EventArgs e)
       {
           try
           {
               if (string.IsNullOrEmpty(txtkititemcode.Text))
               {
                   DisplayMessage("Please enter kit item code", 1);
                   txtkitqty.Focus();
                   return;
               }
               if (string.IsNullOrEmpty(txtkitqty.Text))
               {
                   DisplayMessage("Please enter Qty", 1);
                   txtkitqty.Focus();
                   return;
               }
               decimal qty = Convert.ToDecimal(txtkitqty.Text);
               if (qty <= 0)
               {
                   DisplayMessage("canot add zero or minus qty", 1);
                   txtkitqty.Focus();
                   return;
               }
               List<ItemKitComponent> _kitlist = new List<ItemKitComponent>();
               decimal KitQty =Convert.ToDecimal(txtkitqty.Text);
               ItemKitComponent _kit = new ItemKitComponent();
               _kit.MIKC_ITM_CODE_MAIN = txtkititemcode.Text.Trim().ToUpper();
               if (_ItemKitComponent.Count > 0)
               {
                   var _filter = _ItemKitComponent.Where(x => x.MIKC_ITM_CODE_MAIN == _kit.MIKC_ITM_CODE_MAIN).ToList();
                   if (_filter.Count > 0)
                   {
                       DisplayMessage("already add line", 1);
                       return;
                   }
               }
               _kitlist = CHNLSVC.Inventory.GetItemKitComponent_ProductPlane(_kit, Session["UserDefLoca"].ToString());
               if (_ItemKitComponent.Count > 0)
               {
                  _ItemKitComponent.AddRange(_kitlist);
                   foreach (ItemKitComponent _obj in _ItemKitComponent)
                   {
                       DataTable unitprice = CHNLSVC.Sales.GetUnitPriceNew(_obj.MIKC_ITM_CODE_COMPONENT, Session["UserDefLoca"].ToString());
                       if (unitprice.Rows.Count > 0)
                       {
                           string unitpricess = unitprice.Rows[0]["UNITPRICE"].ToString();
                           _obj.MIKC_COST = Convert.ToDecimal(unitpricess);
                       }
                       else
                       {
                          // txtunitcost.Text = FormatToCurrency("0");
                       }

                       _obj.MIKC_NO_OF_UNIT = _obj.MIKC_NO_OF_UNIT * KitQty;
                       
                   }
               }
               else
               {
                   _ItemKitComponent = _kitlist;
                   foreach (ItemKitComponent _obj in _ItemKitComponent)
                   {
                       DataTable unitprice = CHNLSVC.Sales.GetUnitPriceNew(_obj.MIKC_ITM_CODE_COMPONENT, Session["UserDefLoca"].ToString());
                       if (unitprice.Rows.Count > 0)
                       {
                           string unitpricess = unitprice.Rows[0]["UNITPRICE"].ToString();
                           _obj.MIKC_COST = Convert.ToDecimal(unitpricess);
                       }
                       else
                       {
                           // txtunitcost.Text = FormatToCurrency("0");
                       }

                       _obj.MIKC_NO_OF_UNIT = _obj.MIKC_NO_OF_UNIT * KitQty;

                   }
               }


               grdCost.DataSource = _ItemKitComponent;
               grdCost.DataBind();

               txtlineTQty.Text = "0";
               caltotal();

           }
           catch (Exception ex)
           {
               DisplayMessage(ex.Message, 4);
           }
       }
       protected void btnSearch_Item_Click(object sender, EventArgs e)
       {
           try
           {
               ViewState["SEARCH"] = null;
               txtSearchbyword.Text = string.Empty;
               txtSearchbyword.Text = "";
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
               DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
               grdResult.DataSource = result;
               grdResult.DataBind();
               lblvalue.Text = "Item";
               BindUCtrlDDLData(result);
               txtSearchbyword.Focus();
               UserPopoup.Show();
           }
           catch (Exception ex)
           {
               //divalert.Visible = true;
               DisplayMessage(ex.Message, 4);
           }
       }
       protected void txtdoc_TextChanged(object sender, EventArgs e)
       {
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
           DataTable result = CHNLSVC.General.SearchProDocNo(SearchParams, "DOC NO", txtdoc.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
           if (result.Rows.Count == 0)
           {
               txtdoc.Text = string.Empty;
               DisplayMessage("Please enter valid Production plane number", 1);
               return;
           }
           int _re = 0;
           Loadpro(txtdoc.Text, out _re);
           if (_re == 1)
           {
               txtdoc.Text = string.Empty;
               string msg = "selected production number cannot view please select profit center  " + _SatProjectHeader.SPH_PC;
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msg + "');", true);
           }
       }
       protected void txtItem_TextChanged(object sender, EventArgs e)
       {

          
           try
           {
               if (!LoadItemDetail(txtCItem.Text.ToUpper()))
               {
                   DisplayMessage("Please check the item code", 1);
                   txtCItem.Text = "";
                   txtCItem.Focus();

                   return;
               }
               txtCQty.Focus();
               if (string.IsNullOrEmpty(txtCQty.Text))
               {
                   txtCQty.Text = "1";
               }
               if (string.IsNullOrEmpty(txtCItem.Text.ToUpper().Trim()))
               {
                   txtCtotal.Text = FormatToCurrency("0");
                   txtunitcost.Text = FormatToCurrency("0");
                   return;
               }
               else
               {
                   DataTable unitprice = CHNLSVC.Sales.GetUnitPriceNew(txtCItem.Text.ToString(), Session["UserDefLoca"].ToString());
                   if (unitprice.Rows.Count > 0)
                   {
                       string unitpricess = unitprice.Rows[0]["UNITPRICE"].ToString();
                       txtunitcost.Text = FormatToCurrency(unitpricess);
                       decimal tot = Convert.ToDecimal(txtunitcost.Text.ToString()) * 1;
                       txtCtotal.Text = FormatToCurrency(tot.ToString());
                   }
                   else
                   {
                       txtunitcost.Text = FormatToCurrency("0");
                   }

               }


           }
           catch (Exception ex)
           {
               DisplayMessage(ex.Message, 4);
           }

       }
       protected void txtQty_TextChanged(object sender, EventArgs e)
       {
           try
           {

               if ((_itemdetail.Mi_is_ser1 == 1) || (_itemdetail.Mi_is_ser1 == 0))
               {
                   decimal number = Convert.ToDecimal(txtCQty.Text);
                   decimal fractionalPart = number % 1;
                   if (fractionalPart != 0)
                   {
                       DisplayMessage("only allow numeric value", 1);
                       return;
                   }


               }
               if (Convert.ToDecimal(txtCQty.Text.Trim()) < 0)
               {
                   DisplayMessage("Quantity should be positive value.", 1);
                   return;
               }

               decimal val = Convert.ToDecimal(txtunitcost.Text);
               decimal Total = val * Convert.ToDecimal(txtCQty.Text);
               txtCtotal.Text = FormatToCurrency(Convert.ToString(Total));
               txtunitcost.Focus();
           }
           catch (Exception ex)
           {
               txtCQty.Text = FormatToQty("1");
               DisplayMessage(ex.Message, 4);
           }
       }


       protected void txtunitcost_TextChanged(object sender, EventArgs e)
       {

           try
           {

               if (string.IsNullOrEmpty(txtCItem.Text)) return;
               if (IsNumeric(txtCQty.Text) == false)
               {
                   DisplayMessage("Please select valid quantity", 1);
                   return;
               }
               if (Convert.ToDecimal(txtCQty.Text.Trim()) == 0) return;


               //check minus unit price validation
               decimal _unitAmt = 0;
               bool _isUnitAmt = Decimal.TryParse(txtunitcost.Text, out _unitAmt);
               if (!_isUnitAmt)
               {
                   DisplayMessage("Unit Price has to be number!", 1);
                   return;
               }
               if (_unitAmt <= 0)
               {
                   DisplayMessage("Unit Price has to be greater than 0!", 1);
                   return;
               }
               if (string.IsNullOrEmpty(txtunitcost.Text)) txtunitcost.Text = FormatToCurrency("0");

               decimal val = Convert.ToDecimal(txtunitcost.Text);
               decimal Total = val * Convert.ToDecimal(txtCQty.Text);
               txtCtotal.Text = FormatToCurrency(Convert.ToString(Total));
               // txtCtotal.Text = val.ToString("N2");
           }
           catch (Exception ex)
           {

               DisplayMessage(ex.Message, 4);
           }
       }
       protected void lbtnaddcost_Click(object sender, EventArgs e)
       {
           try
           {
               if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16066))
               {
                   //lbtncancel.Enabled = true;
                   string msg = "You dont have permission to add item .Permission code : 16066";
                   DisplayMessage(msg, 1);
                   return;
               }
               if (string.IsNullOrEmpty(txtCItem.Text))
               {
                   DisplayMessage("Please enter item code", 1);
                   txtkitqty.Focus();
                   return;
               }
               if (string.IsNullOrEmpty(txtCQty.Text))
               {
                   DisplayMessage("Please enter qty", 1);
                   txtCQty.Focus();
                   txtCQty.Text = "1";
                   return;
               }
               if (string.IsNullOrEmpty(txtunitcost.Text))
               {
                   DisplayMessage("Please enter unit cost", 1);
                   txtunitcost.Focus();
                   txtunitcost.Text = "0";
                   return;
               }
               decimal KitQty = Convert.ToDecimal(txtCQty.Text);
               decimal UnitQty = Convert.ToDecimal(txtunitcost.Text);

               if (KitQty <=0)
               {
                   DisplayMessage("canot add zero or minus qty", 1);
                   txtkitqty.Focus();
                   return;
               }
               if (UnitQty < 0)
               {
                   DisplayMessage("canot add  minus cost", 1);
                   txtunitcost.Focus();
                   return;
               }
               if (_ItemKitComponent.Count > 0)
               {
                   var _filter = _ItemKitComponent.Where(x => x.MIKC_ITM_CODE_COMPONENT == txtCItem.Text.ToUpper().Trim() && x.MIKC_ACTIVE==1).ToList();
                   if (_filter.Count > 0)
                   {
                       DisplayMessage("already add item", 1);
                       return;
                   }
               }
               ItemKitComponent _kit = new ItemKitComponent();
               _kit.MIKC_ITM_CODE_COMPONENT = txtCItem.Text.ToUpper().Trim();
               _kit.MIKC_COST = UnitQty;
               _kit.MIKC_DESC_COMPONENT = lblItemDescription.Text;
               _kit.MIKC_NO_OF_UNIT = KitQty;
               _kit.MIKC_ACTIVE = 1;
               _ItemKitComponent.Add(_kit);
               grdCost.DataSource = _ItemKitComponent.Where(x => x.MIKC_ACTIVE == 1).ToList();
               grdCost.DataBind();

               txtCItem.Text = string.Empty;
               txtCQty.Text = "0.0";
               txtunitcost.Text = "0.0";
               txtCtotal.Text = "0.0";
               lblItemDescription.Text = string.Empty;
               lblItemModel.Text = string.Empty;
               lblItemBrand.Text = string.Empty;
               caltotal();
           }
           catch (Exception ex)
           {
               DisplayMessage(ex.Message, 4);
           }
       }


       protected void lbtncode_Click(object sender, EventArgs e)
       {
           try
           {

               ViewState["SEARCH"] = null;
               txtSearchbyword.Text = string.Empty;
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
               DataTable result2 = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
               grdResult.DataSource = result2;
               grdResult.DataBind();
               lblvalue.Text = "Customer";
               BindUCtrlDDLData(result2);
               txtSearchbyword.Text = "";
               txtSearchbyword.Focus();
               UserPopoup.Show();
           }
           catch (Exception ex)
           {
               //divalert.Visible = true;
               DisplayMessage(ex.Message, 4);
           }
       }
       private MasterAutoNumber GenerateMasterAutoNumber()
       {
           MasterAutoNumber masterAuto = new MasterAutoNumber();
           masterAuto.Aut_cate_tp = "LOC";
           masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString(); // string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
           masterAuto.Aut_direction = null;
           masterAuto.Aut_modify_dt = null;
           masterAuto.Aut_moduleid = "PRO";
           masterAuto.Aut_number = 0;
           masterAuto.Aut_start_char = "PRO";
           masterAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
           return masterAuto;
       }

       private void SaveProjectPlane()
       {
           int rowsAffected = 0;
           string _docNo = string.Empty;
           if (_SatProjectHeader == null)
           {
               _SatProjectHeader = new SatProjectHeader();
           }

           _SatProjectHeader.SPH_COM = Session["UserCompanyCode"].ToString();
           _SatProjectHeader.SPH_CRE_BY = Session["UserID"].ToString();
           _SatProjectHeader.SPH_CUS_ADD1 = txtAddress1.Text;
           _SatProjectHeader.SPH_CUS_ADD2 = txtAddress2.Text;
           _SatProjectHeader.SPH_CUS_ADD3 = "N/A";
           _SatProjectHeader.SPH_CUS_CD = txtCustomer.Text;
           _SatProjectHeader.SPH_CUS_NAME = txtCusName.Text;
           _SatProjectHeader.SPH_DT = Convert.ToDateTime(txtDate.Text);
          // _SatProjectHeader.SPH_EST_COST = Convert.ToDecimal(lblcost.Text);
           //_SatProjectHeader.SPH_EST_REV = Convert.ToDecimal(lblRevenue.Text);
           //_SatProjectHeader.SPH_EX = txtexcutive.Text;
           _SatProjectHeader.SPH_LOC_DESC = "N/A";
           _SatProjectHeader.SPH_MOD_BY = Session["UserID"].ToString();
           _SatProjectHeader.SPH_OTH_EX = "N/A";
           _SatProjectHeader.SPH_PB = ddlPriceBook.Text;
           _SatProjectHeader.SPH_PB_LVL = ddlLevel.Text;
           _SatProjectHeader.SPH_PC = Session["UserDefProf"].ToString();
           _SatProjectHeader.SPH_PRO_LOC = txtlocation.Text;
           _SatProjectHeader.SPH_REF = txtref.Text;
           _SatProjectHeader.SPH_ANAL2 = "PRO";
          // _SatProjectHeader.SPH_RMK = txtremark.Text;
           _SatProjectHeader.SPH_STATUS = "P";
           _SatProjectHeader.SPH_COM_DT = Convert.ToDateTime(txtcompletedate.Text.ToString());
           if (chkCus.Checked)
           {
               _SatProjectHeader.SPH_ANAL1="C";
           }
           else { _SatProjectHeader.SPH_ANAL1 = "S"; }


           rowsAffected = CHNLSVC.Inventory.SaveProjectPlane(_SatProjectHeader, GenerateMasterAutoNumber(), _ItemKitComponent, _ProductionFinGood, _ProductionPlaneDetails, out _docNo);

           if (rowsAffected != -1)
           {
               string Msg = "Successfully saved. " + _docNo;
               DisplayMessage(Msg, 3);
               PageClear();
           }
           else
           {
               DisplayMessage(_docNo, 4);
           }
       }



       protected void lbtnSave_Click(object sender, EventArgs e)
       {
           if (txtSavelconformmessageValue.Value == "No")
           {
               return;
           }
           //if (lblstatus.Text == "PENDING")
           //{
           //    DisplayMessage("This document has already saved.", 1);
           //    return;
           //}
           if (lblstatus.Text == "APPROVED")
           {
               DisplayMessage("This document has already approved.", 1);
               return;
           }
           if (validation())
           {
           SaveProjectPlane();
          }

       }




       protected void lbtnloc_Click(object sender, EventArgs e)
       {
           ViewState["SEARCH"] = null;
           txtSearchbyword.Text = string.Empty;
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
           DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
           grdResult.DataSource = result;
           grdResult.DataBind();
           lblvalue.Text = "location";
           BindUCtrlDDLData(result);
           txtSearchbyword.Focus();
           UserPopoup.Show();

       }
       protected void txtlocation_TextChanged(object sender, EventArgs e)
       {
           txtlocation.Text = txtlocation.Text.ToUpper().Trim();
           DataTable result = new DataTable();
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
           result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtlocation.Text);
           if (result.Rows.Count == 0)
           {
               txtlocation.Text = string.Empty;
               DisplayMessage("Please enter valid location", 1);
           }



               
           
       }
       protected void lbtnaddfgood_Click(object sender, EventArgs e)
       {
           try
           {
               txtFinItem.Text=txtFinItem.Text.ToUpper().Trim();
               if (string.IsNullOrEmpty(txtFinItem.Text))
               {
                   DisplayMessage("Please enter item", 1);
                   txtFinItem.Focus();
                   return;
               }
               if (string.IsNullOrEmpty(txtfqty.Text))
               {
                   DisplayMessage("Please enter Qty", 1);
                   txtfqty.Focus();
                   return;
               }
               decimal Qty = Convert.ToDecimal(txtfqty.Text);
               if (Qty <=0)
               {
                   DisplayMessage("canot add zero or minus cost", 1);
                   txtfqty.Focus();
                   return;
               }
               if (_ProductionFinGood != null)
               {
                   if (_ProductionFinGood.Count > 0)
                   {
                       var _filter = _ProductionFinGood.Where(x => x.SPF_ITM == txtFinItem.Text && x.SPF_ACT == 1).ToList();
                       if (_filter.Count > 0)
                       {
                           DisplayMessage("already add item", 1);
                           return;
                       }
                   }
               }
               else
               {
                   _ProductionFinGood = new List<ProductionFinGood>();
               }
               DateTime orddate = DateTime.Now;
               List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
               _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), null, ddlPriceBook.Text,ddlLevel.Text,
                   null, txtFinItem.Text, Qty, orddate.Date);

               ProductionFinGood _FinsItem = new ProductionFinGood();
               _FinsItem.SPF_ACT = 1;
               _FinsItem.SPF_CRE_BY = Session["UserID"].ToString();
               _FinsItem.SPF_ITM = txtFinItem.Text.ToUpper().Trim();
               _FinsItem.SPF_QTY = Qty;
               _FinsItem.SPF_BQTY = Qty;
               if (_priceDetailRef.Count > 0)
               {
                   _FinsItem.SPF_ANAL_1 = System.Math.Round(_priceDetailRef[0].Sapd_itm_price, 2);
               }
               else
               {
                   _FinsItem.SPF_ANAL_1 = 0;
               }
               _ProductionFinGood.Add(_FinsItem);
               grdFGood.DataSource = _ProductionFinGood.Where(x => x.SPF_ACT == 1).ToList();
               grdFGood.DataBind();

               txtFinItem.Text = string.Empty;
               txtfqty.Text = "0.0";
               caltotal();
           }
           catch (Exception ex)
           {
               DisplayMessage(ex.Message, 4);
           }
       }



       protected void lbtnaddPline_Click(object sender, EventArgs e)
       {
           try
           {
               if (string.IsNullOrEmpty(txtlineTQty.Text))
               {
                   DisplayMessage("Please enter Qty", 1);
                   txtlineTQty.Focus();
                   return;
               }
               decimal Qty = Convert.ToDecimal(txtlineTQty.Text);
               if (ddlLine.SelectedIndex==0)
               {
                   DisplayMessage("Please enter line number", 1);
                   return;
               }
               DateTime _comdate = Convert.ToDateTime(txtlcomm.Text);
               DateTime _enddate = Convert.ToDateTime(txtend.Text);
               if (_comdate > _enddate)
               {
                   DisplayMessage("Do not enter end date less than commence date", 1);
                   return;
               }
               if (Qty  <= 0)
               {
                   DisplayMessage("canot add zero or minus qty", 1);
                   txtlineTQty.Focus();
                   return;
               }
          

               if (_ProductionPlaneDetails.Count > 0)
               {
                   var _filter = _ProductionPlaneDetails.Where(x => x.SPL_PRO_LINE == ddlLine.SelectedValue && x.SPL_ACT == 1).ToList();
                   if (_filter.Count > 0)
                   {
                       DisplayMessage("already add line", 1);
                       return;
                   }
               }
               ProductionPlaneDetails _line = new ProductionPlaneDetails();
               _line.SPL_ACT = 1;
               _line.SPL_CRE_BY = Session["UserID"].ToString();
               _line.SPL_EN_DT = Convert.ToDateTime(txtend.Text);
              // _line.SPL_LINE = Session["UserID"].ToString();
               _line.SPL_MOD_BY = Session["UserID"].ToString();
               _line.SPL_PRO_LINE = ddlLine.SelectedValue;
               _line.SPL_PRO_LIN_DES = ddlLine.SelectedItem.Text;
               _line.SPL_QTY = Convert.ToDecimal(txtlineTQty.Text);
              // _line.SPL_RMK
               _line.SPL_ST_DT = Convert.ToDateTime(txtlcomm.Text);
              // _line.
               _ProductionPlaneDetails.Add(_line);
               grdProLine.DataSource = _ProductionPlaneDetails.Where(x => x.SPL_ACT == 1).ToList();
               grdProLine.DataBind();

               ddlLine.SelectedIndex = 0;
               txtlineTQty.Text = "0.0";
               DateTime orddate = DateTime.Now;
               txtcompletedate.Text = orddate.ToString("dd/MMM/yyyy");
               txtend.Text = orddate.ToString("dd/MMM/yyyy");
           }
           catch (Exception ex)
           {
               DisplayMessage(ex.Message, 4);
           }
       }


       protected void txtCustomer_TextChanged(object sender, EventArgs e)
       {
           try
           {
               txtCusName.Text = txtCustomer.Text.ToUpper();
               LoadCustomerDetailsByCustomer();

               //if (!string.IsNullOrEmpty(txtCustomer.Text))
               //{
               //    txtdelcuscode.Text = txtCustomer.Text;
               //}
           }
           catch (Exception ex)
           {
               //divalert.Visible = true;
               DisplayMessage(ex.Message, 4);
           }
       }
       protected void lbtnProCode_Click(object sender, EventArgs e)
       {
           ViewState["SEARCH"] = null;
           txtSearchbyword.Text = string.Empty;
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
           DataTable result = CHNLSVC.General.SearchProDocNo(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);



           grdResultD.DataSource = result;
           grdResultD.DataBind();
           lblvalue.Text = "doc";
           BindUCtrlDDLData2(result);
           txtSearchbyword.Focus();
           UserDPopoup.Show();

       }
       #region Modal Popup 2
       protected void btnDClose_Click(object sender, EventArgs e)
       {
           txtSearchbywordD.Text = "";
           UserDPopoup.Hide();
           // UserPopup.Hide();
       }
       protected void lbtnDateS_Click(object sender, EventArgs e)
       {
           if (lblvalue.Text == "doc")
           {
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
               DataTable result = CHNLSVC.General.SearchProDocNo(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
               grdResultD.DataSource = result;
               grdResultD.DataBind();
               grdResultD.PageIndex = 0;
               UserDPopoup.Show();
               return;
           }

       }
       protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
       {
           ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
           string Name = grdResultD.SelectedRow.Cells[1].Text;
           if (lblvalue.Text == "doc")
           {
               txtdoc.Text = grdResultD.SelectedRow.Cells[1].Text;
               int _re=0;
               Loadpro(txtdoc.Text,out _re);
               if (_re==1)
               {
                   txtdoc.Text = string.Empty;
                   string msg = "selected production number cannot view please select profit center  " + _SatProjectHeader.SPH_PC;
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('"+msg+"');", true);
               }
           }


       }
       protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
       {
           grdResultD.PageIndex = e.NewPageIndex;
           try
           {
               grdResultD.DataSource = null;
               DataTable _tbl = (DataTable)ViewState["SEARCH"];
               grdResultD.DataSource = _tbl;

               grdResultD.DataBind();
               UserDPopoup.Show();
               Session["DPopup"] = "DPopup";
               txtSearchbywordD.Focus();
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
               //divalert.Visible = true;
               DisplayMessage(ex.Message, 4);
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
               //divalert.Visible = true;
               DisplayMessage(ex.Message, 4);
           }

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

       #endregion

       private void Loadpro(string doc,out int _re)
       {
           _ProductionFinGood = new List<ProductionFinGood>();
           _ProductionPlaneDetails = new List<ProductionPlaneDetails>();
           _SatProjectDetails = new List<SatProjectDetails>();
           _SatProjectHeader = CHNLSVC.Sales.GETBOQHDR(Session["UserCompanyCode"].ToString(), doc);
           _re = 0;
           if (_SatProjectHeader != null)
           {
               if (_SatProjectHeader.SPH_PC != Session["UserDefProf"].ToString()) 
               {
                   _re = 1;
                 
                   return;
               }


               txtAddress1.Text = _SatProjectHeader.SPH_CUS_ADD1;
               txtAddress2.Text = _SatProjectHeader.SPH_CUS_ADD2;
               txtCustomer.Text = _SatProjectHeader.SPH_CUS_CD;
               txtCusName.Text = _SatProjectHeader.SPH_CUS_NAME;
               LoadCustomerDetailsByCustomer();
               txtDate.Text = _SatProjectHeader.SPH_DT.ToString("dd/MMM/yyyy");

               ddlPriceBook.Text = _SatProjectHeader.SPH_PB;
               ddlLevel.Text = _SatProjectHeader.SPH_PB_LVL;
               txtlocation.Text = _SatProjectHeader.SPH_PRO_LOC;
               txtref.Text = _SatProjectHeader.SPH_REF;
     
               txtcompletedate.Text = _SatProjectHeader.SPH_COM_DT.ToString("dd/MMM/yyyy");
               if (_SatProjectHeader.SPH_STATUS == "P")
               {
                   lblstatus.Text = "PENDING";
               }
               else if (_SatProjectHeader.SPH_STATUS == "A")
               {
                   lblstatus.Text = "APPROVED";
               }
               if (!string.IsNullOrEmpty(doc))
               {
                   _SatProjectDetails = CHNLSVC.Sales.GETBOQDETAILS(doc);
                   if (_SatProjectDetails != null)
                   {
                       if (_SatProjectDetails.Count > 0)
                       {
                           foreach (SatProjectDetails item in _SatProjectDetails)
                           {
                                 ItemKitComponent _kit = new ItemKitComponent();
                                 _kit.MIKC_ITM_CODE_COMPONENT = item.SPD_ITM;
                                 _kit.MIKC_DESC_COMPONENT = item.SPD_ITM_DESC;
                                 _kit.MIKC_COST = item.SPD_EST_COST;
                                 _kit.MIKC_NO_OF_UNIT = item.SPD_EST_QTY;
                                 _kit.MIKC_LINE = item.SPD_LINE;
                                 _kit.MIKC_SEQ_NO = item.SPD_SEQ;
                                 _kit.MIKC_DOCNO = item.SPD_NO;
                                 _kit.MIKC_ACTIVE = item.SPD_ACTVE;
                                 _ItemKitComponent.Add(_kit);
                           }
                           grdCost.DataSource = _ItemKitComponent.Where(x => x.MIKC_ACTIVE == 1).ToList();
                           grdCost.DataBind();
                          
                       }
                   }
                   _ProductionFinGood = CHNLSVC.Sales.GETFINGOD(doc);
                   if (_ProductionFinGood != null)
                   {
                       if (_ProductionFinGood.Count > 0)
                       {

                           grdFGood.DataSource = _ProductionFinGood.Where(x=>x.SPF_ACT==1).ToList();
                           grdFGood.DataBind();

                       }
                   }
                   _ProductionPlaneDetails = CHNLSVC.Sales.GETSATPROJ_LINE(doc);
                   if (_ProductionPlaneDetails != null)
                   {
                       if (_ProductionPlaneDetails.Count > 0)
                       {
                           List<ProductionLine> _line = new List<ProductionLine>();
                           ProductionLine _lineobj = new ProductionLine();
                           _lineobj.MPL_COM = Session["UserCompanyCode"].ToString();
                           _lineobj.MPL_LOC = Session["UserDefLoca"].ToString();
                           _line = CHNLSVC.Inventory.GET_PRODUCTLINE(_lineobj);
                           if (_line.Count > 0)
                           {
                               foreach (ProductionPlaneDetails _de in _ProductionPlaneDetails)
                               {
                                   _de.SPL_PRO_LIN_DES = _line.Find(x => x.MPL_LINE_CD == _de.SPL_PRO_LINE.ToString()).MPL_LINE_DESC;
                               }
                           }

                           grdProLine.DataSource = _ProductionPlaneDetails.Where(x => x.SPL_ACT == 1).ToList();
                           grdProLine.DataBind();

                       }
                   }
               }
               caltotal();
           }
       }


       protected void lbtnkititem_Click(object sender, EventArgs e)
       {


           try
           {


               ViewState["SEARCH"] = null;
               txtSearchbyword.Text = string.Empty;
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
               DataTable result = CHNLSVC.CommonSearch.GetKitItemforInvoiceSearchData(SearchParams, null, null);
               grdResult.DataSource = result;
               grdResult.DataBind();
               lblvalue.Text = "kit";
               BindUCtrlDDLData(result);
               ViewState["SEARCH"] = result;
               UserPopoup.Show();
           }
           catch (Exception ex)
           {
               //divalert.Visible = true;
               DisplayMessage(ex.Message, 4);
           }
       }



       protected void lblFItem_Click(object sender, EventArgs e)
       {
           try
           {
               ViewState["SEARCH"] = null;
               txtSearchbyword.Text = string.Empty;
               txtSearchbyword.Text = "";
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_2);
               DataTable result = CHNLSVC.CommonSearch.GetItemforKit(SearchParams, null, null);
               grdResult.DataSource = result;
               grdResult.DataBind();
               lblvalue.Text = "Item_2";
               BindUCtrlDDLData(result);
               txtSearchbyword.Focus();
               UserPopoup.Show();
           }
           catch (Exception ex)
           {
               //divalert.Visible = true;
               DisplayMessage(ex.Message, 4);
           }
       }
       protected void txtFinItem_TextChanged(object sender, EventArgs e)
       {


           try
           {
               txtFinItem.Text = txtFinItem.Text.ToUpper().Trim();
               //if (!LoadItemDetail(txtFinItem.Text.ToUpper()))
               //{
               //    DisplayMessage("Please check the item code", 1);
               //    txtFinItem.Text = "";
               //    txtFinItem.Focus();

               //    return;
               //}

               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_2);
               DataTable result = CHNLSVC.CommonSearch.GetItemforKit(SearchParams, "ITEM", txtFinItem.Text);
               if (result.Rows.Count == 0)
               {
                   DisplayMessage("Please check the item code", 1);
                   txtFinItem.Text = "";
                   txtFinItem.Focus();

                   return;
               }

           }
           catch (Exception ex)
           {
               DisplayMessage(ex.Message, 4);
           }

       }

       protected void lbtnPlDelete_Click(object sender, EventArgs e)
       {
           if (txtDeleteconformmessageValue.Value == "No")
           {
               return;
           }
           if (grdProLine.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _line = (row.FindControl("lbSPL_PRO_LINE") as Label).Text;
                if (_ProductionPlaneDetails != null)
                {
                    var _filter = _ProductionPlaneDetails.SingleOrDefault(x => x.SPL_PRO_LINE == _line);
                    if (string.IsNullOrEmpty(_filter.SPL_PRO_NO))
                    {
                        _ProductionPlaneDetails.RemoveAll(x => x.SPL_PRO_LINE == _line);
                    }
                    else
                    {
                        _filter.SPL_ACT = 0;
                       //_ProductionPlaneDetails= _ProductionPlaneDetails.Where(x => x.SPL_ACT == 1).ToList();
                    }

                    grdProLine.DataSource = _ProductionPlaneDetails.Where(x => x.SPL_ACT == 1).ToList();
                    grdProLine.DataBind();
                    caltotal();
                }
            }
       }

       protected void lbtnGFDelete_Click(object sender, EventArgs e)
       {
           if (txtDeleteconformmessageValue.Value == "No")
           {
               return;
           }
           if (grdFGood.Rows.Count == 0) return;
           var lb = (LinkButton)sender;
           var row = (GridViewRow)lb.NamingContainer;
           if (row != null)
           {
               string _ltem = (row.FindControl("lblspd_itm") as Label).Text;
               if (_ProductionFinGood != null)
               {
                   var _filter = _ProductionFinGood.SingleOrDefault(x => x.SPF_ITM == _ltem);
                   if (string.IsNullOrEmpty(_filter.SPF_PRO_NO))
                   {
                       _ProductionFinGood.RemoveAll(x => x.SPF_ITM == _ltem);
                   }
                   else
                   {
                       _filter.SPF_ACT = 0;
                      // _ProductionFinGood.RemoveAll(x => x.SPL_ACT == 0);
                      // _ProductionFinGood = _ProductionFinGood.Where(x => x.SPL_ACT == 1).ToList();
                   }

                   grdFGood.DataSource = _ProductionFinGood.Where(x => x.SPF_ACT == 1).ToList();
                   grdFGood.DataBind();
                   caltotal();
               }
           }
       }


       protected void lbtnkitDelete_Click(object sender, EventArgs e)
       {
           if (txtDeleteconformmessageValue.Value == "No")
           {
               return;
           }
           if (grdCost.Rows.Count == 0) return;
           var lb = (LinkButton)sender;
           var row = (GridViewRow)lb.NamingContainer;
           if (row != null)
           {
               string _ltem = (row.FindControl("lblmikc_itm_code_main") as Label).Text;
               if (_ItemKitComponent != null)
               {
                   //var _filter = _ItemKitComponent.SingleOrDefault(x => x.MIKC_ITM_CODE_COMPONENT == _ltem);
                   //if (_filter.MIKC_SEQ_NO==0)
                   //{
                   //    _ItemKitComponent.RemoveAll(x => x.MIKC_ITM_CODE_COMPONENT == _ltem);
                   //}
                   //else
                   //{
                   //    _filter.MIKC_ACTIVE = 0;
                   //    //_ItemKitComponent.RemoveAll(x => x.MIKC_ACTIVE == 0);
                   //    //_ItemKitComponent = _ItemKitComponent.Where(x => x.MIKC_ACTIVE == 1).ToList();
                   //}

                   foreach (var _list in _ItemKitComponent)
                   {
                       if (_list.MIKC_ITM_CODE_COMPONENT == _ltem)
                       {
                           _list.MIKC_ACTIVE = 0;
                       }
                   }


                   grdCost.DataSource = _ItemKitComponent.Where(x => x.MIKC_ACTIVE == 1).ToList();
                   grdCost.DataBind();
                   caltotal();
               }
           }
       }


       protected void lbtnapprove_Click(object sender, EventArgs e)
       {
           if (txtApprovalconformmessageValue.Value == "No")
           {
               return;
           }
           if (lblstatus.Text == "APPROVED")
           {
               DisplayMessage("This document has already approved.", 1);
               return;
           }
           if (txtdoc.Text == "")
           {
               DisplayMessage("Please save the project", 1);
               return;
           }
           if (validation())
           {
               ApproveBOQ();
           }
       }
       private bool validation()
       {
          
           if (string.IsNullOrEmpty(txtlocation.Text))
           {
               DisplayMessage("Please select location", 1);
               return false;
           }
           if (ddlPriceBook.Text == "")
           {
               DisplayMessage("Please select price book", 1);
               return false;
           }
           if (ddlLevel.Text == "")
           {
               DisplayMessage("Please select price book level", 1);
               return false;
           }
           if (_ItemKitComponent.Count == 0)
           {
               DisplayMessage("Please add kit components", 1);
               return false;
           }
           if (lblstatus.Text == "APPROVED")
           {
               DisplayMessage("This document has already approved.", 1);
               return false;
           }
           return true;
       }

       private void ApproveBOQ()
       {
           int rowsAffected = 0;
           string _docNo = string.Empty;
           if (_SatProjectHeader == null)
           {
               string Msg = "Please select BOQ number";
               DisplayMessage(Msg, 1);
           }
           _SatProjectHeader.SPH_COM = Session["UserCompanyCode"].ToString();
           _SatProjectHeader.SPH_NO = txtdoc.Text;
           _SatProjectHeader.SPH_APP_BY = Session["UserID"].ToString();
           rowsAffected = CHNLSVC.Sales.APPROVEBOQHDD(_SatProjectHeader, out _docNo);

           if (rowsAffected != -1)
           {
               string Msg = "Successfully Approve. " + _docNo;
               DisplayMessage(Msg, 3);
               lblstatus.Text = "";
               PageClear();
           }
           else
           {
               DisplayMessage(_docNo, 4);
           }
       }
    
       private void caltotal()
       {
           if (_ItemKitComponent != null)
           {
               List<ItemKitComponent> _actkitcom = _ItemKitComponent.Where(a => a.MIKC_ACTIVE == 1).ToList();


               decimal total = 0;//_ItemKitComponent.Sum(item => item.MIKC_COST);
               foreach (ItemKitComponent _kit in _actkitcom)
               {
                   decimal subtotal = 0;
                   subtotal =  _kit.MIKC_NO_OF_UNIT;
                   total = subtotal + total;
               }
               lblTotalCost.Text = FormatToCurrency(Convert.ToString(total));
           }
           if (_ProductionFinGood != null)
           {
               decimal totalRev = 0;//_ProductionFinGood.Sum(item => item.SPF_ANAL_1);
               foreach (ProductionFinGood _kit in _ProductionFinGood)
               {
                   decimal subtotalRev = 0;
                   subtotalRev = _kit.SPF_QTY;
                   totalRev = subtotalRev + totalRev;
               }
               lblTotalRevenue.Text = FormatToCurrency(Convert.ToString(totalRev));
           }
               

               //clearitem();
               AmountCal();
           
       }

       private void AmountCal()
       {
           lblcost.Text = lblTotalCost.Text;
           lblRevenue.Text = lblTotalRevenue.Text;

           decimal cost = Convert.ToDecimal(lblcost.Text);
           decimal revenue = Convert.ToDecimal(lblRevenue.Text);

           decimal profit = revenue - cost;
           if (profit > 0)
           {
               decimal profitp = (profit / revenue) * 100;
               lblprofitvalue.Text = FormatToCurrency(Convert.ToString(profit));
               lblprofit.Text = FormatToCurrency(Convert.ToString(profitp)) + "%";
               HiddenField1.Value = lblcost.Text;
           }
           DataTable dt = GetData(cost, revenue);
           LoadChartData(dt);

       }
       private DataTable GetData(decimal _cost, decimal _revenue)
       {

           DataTable dt = new DataTable();
           dt.Columns.Add("Data", Type.GetType("System.String"));
           dt.Columns.Add("Value1", Type.GetType("System.Decimal"));
           dt.Columns.Add("Value2", Type.GetType("System.Decimal"));
           DataRow dr1 = dt.NewRow();
           dr1["Data"] = "Cost";
           dr1["Value1"] = _cost;
           dr1["Value2"] = 0;
           dt.Rows.Add(dr1);
           DataRow dr2 = dt.NewRow();
           dr2["Data"] = "Revenue";
           dr2["Value1"] = _revenue;
           dr2["Value2"] = 0;
           dt.Rows.Add(dr2);

           return dt;
       }
       private void LoadChartData(DataTable initialDataSource)
       {
           COSTTestChart.Series.Clear();
           for (int i = 1; i < initialDataSource.Columns.Count; i++)
           {
               Series series = new Series();
               foreach (DataRow dr in initialDataSource.Rows)
               {
                   decimal y = (decimal)dr[i];
                   series.Points.AddXY(dr["Data"].ToString(), y);
               }
               COSTTestChart.Series.Add(series);
           }
           COSTTestChart.ChartAreas["ChartArea1"].AxisX.Title = "Axis X Name";
           COSTTestChart.ChartAreas["ChartArea1"].AxisY.Title = "Axis y Name";
       }
    }
}