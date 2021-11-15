using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.General;
using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Sales
{
    public partial class BillOfQuantitiesNew : Base
    {
        #region add by Dulaj 24Nov2017
        OrderPlanExcelUploader _ordPlnUpItm = new OrderPlanExcelUploader();
        List<OrderPlanExcelUploader> _ordPlnUpItmList
        {
            get { if (Session["_ordPlnUpItmList"] != null) { return (List<OrderPlanExcelUploader>)Session["_ordPlnUpItmList"]; } else { return new List<OrderPlanExcelUploader>(); } }
            set { Session["_ordPlnUpItmList"] = value; }
        }
        List<BillOfQuantityExcelUpload> _billogQuantitieslist
        {
            get { if (Session["_billogQuantitieslist"] != null) { return (List<BillOfQuantityExcelUpload>)Session["_billogQuantitieslist"]; } else { return new List<BillOfQuantityExcelUpload>(); } }
            set { Session["_billogQuantitieslist"] = value; }
        }

        BillOfQuantityExcelUpload _billOfQuantityExcelUpload = new BillOfQuantityExcelUpload();

        bool _showExcelPop
        {
            get { if (Session["_showExcelPopOrPlan"] != null) { return (bool)Session["_showExcelPopOrPlan"]; } else { return false; } }
            set { Session["_showExcelPopOrPlan"] = value; }
        }
        bool _showErrPop
        {
            get { if (Session["_showErrPopOrPlan"] != null) { return (bool)Session["_showErrPopOrPlan"]; } else { return false; } }
            set { Session["_showErrPopOrPlan"] = value; }
        }
        string _filPath
        {
            get { if (Session["_filPathOrPlan"] != null) { return (string)Session["_filPathOrPlan"]; } else { return ""; } }
            set { Session["_filPathOrPlan"] = value; }
        }
        #endregion
        protected MasterBusinessEntity _masterBusinessCompany { get { return (MasterBusinessEntity)Session["_masterBusinessCompany"]; } set { Session["_masterBusinessCompany"] = value; } }
        protected List<PriceDefinitionRef> _PriceDefinitionRef { get { return (List<PriceDefinitionRef>)Session["_PriceDefinitionRef"]; } set { Session["_PriceDefinitionRef"] = value; } }
        protected List<SatProjectDetails> _SatProjectDetails { get { return (List<SatProjectDetails>)Session["_SatProjectDetails"]; } set { Session["_SatProjectDetails"] = value; } }
        protected MasterItem _itemdetail { get { return (MasterItem)Session["_itemdetail"]; } set { Session["_itemdetail"] = value; } }
        protected SatProjectHeader _SatProjectHeader { get { return (SatProjectHeader)Session["_SatProjectHeader"]; } set { Session["_SatProjectHeader"] = value; } }
        protected List<ItemKitComponent> _ItemKitComponent { get { return (List<ItemKitComponent>)Session["_ItemKitComponent"]; } set { Session["_ItemKitComponent"] = value; } }
        protected List<SatProjectDetails> revdet { get { return (List<SatProjectDetails>)Session["revdet"]; } set { Session["revdet"] = value; } }

        protected List<SatProjectKitDetails> satKitDetails { get { return (List<SatProjectKitDetails>)Session["kitdet"]; } set { Session["kitdet"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageClear();
                LoadCachedObjects();
                LoadPriceBook();
            }
            if (this.IsPostBack)
            {
                TabName.Value = Request.Form[TabName.UniqueID];
            }
        }
        #region Modalpopup
        protected void btnClose_Click(object sender, EventArgs e)
        {
            //  txtSearchbyword.Text = "";
            //  costTab.Attributes["class"] = "active";
            btnClose.Attributes.Add("OnClientClick", "return false;");
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            // string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Customer")
            {
                txtCustomer.Text = grdResult.SelectedRow.Cells[1].Text;
                LoadCustomerDetailsByCustomer();
                return;
            }
            if (lblvalue.Text == "NIC")
            {
                txtNIC.Text = grdResult.SelectedRow.Cells[1].Text;
                LoadCustomerDetailsByNIC();
            }
            if (lblvalue.Text == "kit")
            {
                txtkititemcode.Text = grdResult.SelectedRow.Cells[1].Text;
                txtkitqty.Focus();
            }
            if (lblvalue.Text == "Mobile")
            {
                txtMobile.Text = grdResult.SelectedRow.Cells[1].Text;
                LoadCustomerDetailsByMobile();
            }
            if (lblvalue.Text == "Sale_Ex")
            {
                txtexcutive.Text = grdResult.SelectedRow.Cells[1].Text;
                txtexcutive.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                txtExcDesc.Text = grdResult.SelectedRow.Cells[3].Text;
                //lblSalesEx.Text = grdResult.SelectedRow.Cells[2].Text;
            }
            if (lblvalue.Text == "Item")
            {
                txtCItem.Text = grdResult.SelectedRow.Cells[1].Text;
                txtItem_TextChanged(null, null);
            }
            if (lblvalue.Text == "Item_2")
            {
                txtRitem.Text = grdResult.SelectedRow.Cells[1].Text;
                txtRitem_TextChanged(null, null);
            }
            if (lblvalue.Text == "location")
            {
                txtlocation.Text = grdResult.SelectedRow.Cells[1].Text;
                txtLocDesc.Text = grdResult.SelectedRow.Cells[2].Text;
            }
            //if (lblvalue.Text == "doc")
            //{
            //    txtdoc.Text = grdResult.SelectedRow.Cells[1].Text;
            //    LoadBOQ(txtdoc.Text);
            //}
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
            //if (lblvalue.Text == "doc")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            //    DataTable result = CHNLSVC.General.SearchProjectCode(SearchParams, null, null);  //SearchBOQDocNo
            //    grdResult.DataSource = result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
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
            if ((lblvalue.Text == "Item") || (lblvalue.Text == "Item_2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
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
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.GetKitItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.projectCode);
                DataTable result = CHNLSVC.General.SearchProjectCode(SearchParams, ddlSearchbykeyD.SelectedItem.Text, "%" + txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), "S");  //SearchBOQDocNo
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "location")//GetUserLocationSearchData
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
            if ((lblvalue.Text == "Item") || (lblvalue.Text == "Item_2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result2;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
        }
        #endregion

        private void PageClear()
        {
            DateTime orddate = DateTime.Now;
            txtDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtcompletedate.Text = orddate.ToString("dd/MMM/yyyy");
            txtComme.Text = orddate.ToString("dd/MMM/yyyy");
            lblcost.Text = "0.0";
            lblprofit.Text = "0.0";
            lblprofitvalue.Text = "0.0";
            lblRevenue.Text = "0.0";

            txtCQty.Text = "1";
            txtunitcost.Text = "0.0";
            txtCtotal.Text = "0.0";
            txtCItem.Text = string.Empty;
            lblTotalCost.Text = "0.0";
            lblTotalRevenue.Text = "0.0";

            txtRQty.Text = "1";
            txtunitRevenue.Text = "0.0";
            txtRtotal.Text = "0.0";

            grdCost.DataSource = new int[] { };
            grdCost.DataBind();
            grdRevenue.DataSource = new int[] { };
            grdRevenue.DataBind();
            SatProjectGridView.DataSource = new int[] { };
            grdRevenue.DataBind();
            _SatProjectHeader = new SatProjectHeader();
            _masterBusinessCompany = new MasterBusinessEntity();
            _PriceDefinitionRef = new List<PriceDefinitionRef>();
            _SatProjectDetails = new List<SatProjectDetails>();
            revdet = new List<SatProjectDetails>();
            satKitDetails = new List<SatProjectKitDetails>();
            SatProjectGridView.DataSource = satKitDetails;
            SatProjectGridView.DataBind();
            _ItemKitComponent = new List<ItemKitComponent>();
            _itemdetail = new MasterItem();

            txtexcutive.Text = string.Empty;
            txtlocation.Text = string.Empty;
            txtref.Text = string.Empty;
            txtdoc.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            cmbTitle.SelectedIndex = -1;
            txtCusName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtNIC.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtremark.Text = string.Empty;
            txtkititemcode.Text = string.Empty;
            txtkitqty.Text = string.Empty;
            txtLocDesc.Text = string.Empty;
            txtExcDesc.Text = string.Empty;
            lblstatus.Text = string.Empty;
            //DataTable dt = GetData(1, 1);
            //LoadChartData(dt);
        }

        private void DisplayMessage(String Msg, Int32 option)
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
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.projectCode:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void LoadCachedObjects()
        {

            _PriceDefinitionRef = (List<PriceDefinitionRef>)Session["_PriceDefinitionRef_1"];// CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());

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

            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

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
                        DisplayMessage("Please select the valid NIC", 1);
                    }


                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, txtNIC.Text, null, null, null, Session["UserCompanyCode"].ToString());
                }
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
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
                        txtMobile.Focus();
                    }
                    else
                    {
                        DisplayMessage("This customer already inactive. Please contact accounts dept", 1);
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }
                }
                else
                {

                }


            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
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
                        DisplayMessage("Please select the valid mobile", 1);
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
                        ClearCustomer(true);
                        DisplayMessage("This customer already inactive. Please contact accounts dept", 1);
                        txtCustomer.Focus();
                        return;
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

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
                        ddlPriceBook.SelectedValue = _defaultValue[0].Sadd_pb == null ? ddlPriceBook.SelectedValue : _defaultValue[0].Sadd_pb;
                    LoadPriceLevel(ddlPriceBook.Text);


                }
                else
                    ddlPriceBook.DataSource = null;
            else
                ddlPriceBook.DataSource = null;


        }
        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblUom.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            lblItemSerialStatus.Text = string.Empty;
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
                lblUom.Text = _itemdetail.Mi_itm_uom;
                lblItemBrand.Text = _brand;
                lblItemSerialStatus.Text = _serialstatus;
                Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;
            }
            else _isValid = false;
            return _isValid;
        }
        private bool LoadItemDetail_2(string _item)
        {
            lblRItemDescription.Text = string.Empty;
            lblRItemModel.Text = string.Empty;
            lblRItemBrand.Text = string.Empty;
            lblUom.Text = string.Empty;
            lblRItemSerialStatus.Text = string.Empty;
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

                lblRItemDescription.Text = _description;
                lblRItemModel.Text = _model;
                lblRItemBrand.Text = _brand;
                lblUom.Text = _itemdetail.Mi_itm_uom;
                lblRItemSerialStatus.Text = _serialstatus;
                Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;
            }
            else _isValid = false;
            return _isValid;
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
                            ddlLevel.SelectedValue = _def[0].Sadd_p_lvl == null ? ddlLevel.SelectedValue : _def[0].Sadd_p_lvl;
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

        private void LoadBOQ(string doc)
        {
            _SatProjectHeader = CHNLSVC.Sales.GETBOQHDR(Session["UserCompanyCode"].ToString(), doc);
            if (_SatProjectHeader != null)
            {
                txtAddress1.Text = _SatProjectHeader.SPH_CUS_ADD1;
                txtAddress2.Text = _SatProjectHeader.SPH_CUS_ADD2;
                txtCustomer.Text = _SatProjectHeader.SPH_CUS_CD;
                txtCusName.Text = _SatProjectHeader.SPH_CUS_NAME;
                LoadCustomerDetailsByCustomer();
                txtDate.Text = _SatProjectHeader.SPH_DT.ToString("dd/MMM/yyyy");
                lblcost.Text = _SatProjectHeader.SPH_EST_COST.ToString();
                lblRevenue.Text = _SatProjectHeader.SPH_EST_REV.ToString();
                txtexcutive.Text = _SatProjectHeader.SPH_EX;
                ddlPriceBook.Text = _SatProjectHeader.SPH_PB != ddlPriceBook.Text ? ddlPriceBook.Text : _SatProjectHeader.SPH_PB;
                ddlLevel.Text = _SatProjectHeader.SPH_PB_LVL != ddlLevel.Text ? ddlLevel.Text : _SatProjectHeader.SPH_PB_LVL;
                txtlocation.Text = _SatProjectHeader.SPH_PRO_LOC;
                txtref.Text = _SatProjectHeader.SPH_REF;
                txtremark.Text = _SatProjectHeader.SPH_RMK;
                txtcompletedate.Text = _SatProjectHeader.SPH_COM_DT.ToString("dd/MMM/yyyy");
                txtComme.Text = _SatProjectHeader.SPH_OTH_DT.ToString("dd/MMM/yyyy");
                MasterLocation mLoc = CHNLSVC.General.GetAllLocationByLocCode(Session["UserCompanyCode"].ToString(), txtlocation.Text, 0);
                txtLocDesc.Text = mLoc.Ml_loc_desc;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable dt = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, "Code", "%" + txtexcutive.Text);
                txtExcDesc.Text = (from DataRow dr in dt.Rows
                                   select (string)dr["Description"]).FirstOrDefault();

                if (_SatProjectHeader.SPH_STATUS == "P")
                {
                    lblstatus.Text = "PENDING";
                }
                else if (_SatProjectHeader.SPH_STATUS == "A")
                {
                    lblstatus.Text = "APPROVED";
                }
                else if (_SatProjectHeader.SPH_STATUS == "U")
                {
                    lblstatus.Text = "USED";
                }
                if (!string.IsNullOrEmpty(doc))
                {
                    gridClean();
                    _SatProjectDetails = CHNLSVC.Sales.GETBOQDETAILS(doc);
                    if (_SatProjectDetails != null)
                    {
                        CalculateMrnCost(doc);
                        if (_SatProjectDetails.Count > 0)
                        {
                            BindUomGrid(_SatProjectDetails);
                            kitCode.Visible = false;
                            //boqAct.SelectedValue = _SatProjectDetails.Select(r => r.SPD_ACTVE).ToString();
                            grdCost.DataSource = _SatProjectDetails;
                            grdCost.DataBind();
                            grdRevenue.DataSource = _SatProjectDetails;
                            grdRevenue.DataBind();
                            Session["revdlist"] = _SatProjectDetails;
                            caltotal();
                            BindKitCodeDetaisl(doc);//Added By Dulaj 2018/Aug/14
                        }
                    }
                    else
                    {
                        kitCode.Visible = true;
                    }
                }
            }
            //DataTable mrnDt = null;//CHNLSVC.General.GetMrnByBoqNo(doc);
            //string mrnNo = string.Empty;
            //if (mrnDt != null)
            //{
            //    if (mrnDt.Rows.Count == 1)
            //    {
            //        mrnNo = mrnDt.Rows[0]["ITR_REQ_NO"].ToString();
            //    }
            //}
            //if (!(string.IsNullOrEmpty(mrnNo)))
            //{
            //CalculateMrnCost(doc,mrnNo);//Addd By Dulaj 2018/Aug/16
            //}

        }
        //Added By Dulaj 2018/Aug/14
        private void BindKitCodeDetaisl(string doc)
        {
            satKitDetails = new List<SatProjectKitDetails>();
            satKitDetails = CHNLSVC.Inventory.GetSatKitItems(doc);
            SatProjectGridView.DataSource = satKitDetails;
            SatProjectGridView.DataBind();
        }
        private void CalculateMrnCost(string doc)
        {        //Dulaj 2018/Aug/06

            //InventoryRequest _inputInvReq = new InventoryRequest();
            //_inputInvReq.Itr_req_no = mrnNo;
            //InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
            //decimal itemPrice = 0;
            ////labelTextCurrentCost.Text =  itemPrice
            //foreach (var item in _selectedInventoryRequest.InventoryRequestItemList)
            //{
            //    decimal cuurentItemCostDt = CHNLSVC.Inventory.GetLatestCost(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), item.Itri_itm_cd, item.Itri_itm_stus);
            //    //  if(cuurentItemCostDt!=null)
            //    //  {
            //    //      if(cuurentItemCostDt.Rows.Count>0)
            //    //      {
            //    decimal cost = cuurentItemCostDt;
            //    itemPrice = itemPrice + cost;
            //    //   }
            //    //  }
            //}
            decimal totalADOCost = 0;
            DataTable costDt = CHNLSVC.Inventory.getItemCostByJobNo(doc);
            if (costDt != null)
            {
                foreach (DataRow dt in costDt.Rows)
                {
                    decimal cost = Convert.ToDecimal(dt["itb_unit_cost"].ToString());
                    decimal qty = Convert.ToDecimal(dt["itb_qty"].ToString());
                    totalADOCost = totalADOCost + (cost * qty);
                }
            }
            currentUtilization.Text = totalADOCost.ToString("0.00");
            LabelCurrentCost.Text = totalADOCost.ToString("0.00");
        }
        private List<SatProjectDetails> BindUomGrid(List<SatProjectDetails> _list)
        {
            MasterItem _mstItm = new MasterItem();
            foreach (var item in _list)
            {
                _mstItm = CHNLSVC.General.GetItemMaster(item.SPD_ITM);
                if (_mstItm != null)
                {
                    item.SPD_MI_ITM_UOM = _mstItm.Mi_itm_uom;
                }
            }
            return _list;
        }
        protected void gridClean()
        {
            grdCost.DataSource = null;
            grdCost.DataBind();
            grdRevenue.DataSource = null;
            grdRevenue.DataBind();
            _SatProjectDetails = null;
            lblTotalCost.Text = string.Empty;
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

        private void clearitem()
        {
            txtCItem.Text = string.Empty;
            txtCQty.Text = "1";
            txtunitcost.Text = "0.0";
            txtCtotal.Text = "0.0";
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblUom.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            lblItemSerialStatus.Text = string.Empty;

            txtRitem.Text = string.Empty;
            txtRQty.Text = "1";
            txtunitRevenue.Text = "0.0";
            txtRtotal.Text = "0.0";
            lblRItemDescription.Text = string.Empty;
            lblRItemModel.Text = string.Empty;
            lblRItemBrand.Text = string.Empty;
            lblUom.Text = string.Empty;
            lblRItemSerialStatus.Text = string.Empty;
            // currentUtilization.Text = string.Empty;
            //LabelCurrentCost.Text = string.Empty;
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

        private void LoadChartData(DataTable initialDataSource)
        {
            for (int i = 1; i < initialDataSource.Columns.Count; i++)
            {
                Series series = new Series();
                foreach (DataRow dr in initialDataSource.Rows)
                {
                    decimal y = (decimal)dr[i];
                    series.Points.AddXY(dr["Data"].ToString(), y);
                }
                cTestChart.Series.Add(series);
            }
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

        private bool validation()
        {
            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                //DisplayMessage("Please select customer", 1);
                // return false;
            }
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

            return true;
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

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetailsByCustomer();

            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtUnitCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //KitTotalCost txtkitqty
                if (!(string.IsNullOrEmpty(txtkitqty.Text)))
                {
                    decimal qty = Convert.ToDecimal(txtkitqty.Text);
                    if (qty <= 0)
                    {
                        DisplayMessage("Please enter valid qty", 4);
                    }
                    else
                    {
                        if (!(string.IsNullOrEmpty(KitCostTextBox.Text)))
                        {
                            decimal cost = Convert.ToDecimal(KitCostTextBox.Text);
                            if (cost <= 0)
                            {
                                DisplayMessage("Please enter valid cost", 4);
                            }
                            else
                            {
                                KitTotalCost.Text = (cost * qty).ToString();
                                KitPriceTextBox.Focus();
                            }
                        }
                    }
                }
                else
                {
                    DisplayMessage("Please enter valid qty", 4);
                }

            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }
        }
        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtkititemcode.Text)) return;
                if (IsNumeric(txtCQty.Text) == false)
                {
                    DisplayMessage("Please select valid quantity", 1);
                    return;
                }
                if (Convert.ToDecimal(txtkitqty.Text.Trim()) == 0) return;


                //check minus unit price validation
                decimal _unitAmt = 0;
                bool _isUnitAmt = Decimal.TryParse(KitPriceTextBox.Text, out _unitAmt);
                if (!_isUnitAmt)
                {
                    DisplayMessage("Unit Price has to be number!", 1);
                    KitPriceTextBox.Focus();
                    return;
                }
                if (_unitAmt <= 0)
                {
                    DisplayMessage("Unit Price has to be greater than 0!", 1);
                    KitPriceTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(KitPriceTextBox.Text)) KitPriceTextBox.Text = FormatToCurrency("0");

                decimal val = Convert.ToDecimal(KitPriceTextBox.Text);
                decimal Total = val * Convert.ToDecimal(txtkitqty.Text);
                KitTotalPrice.Text = FormatToCurrency(Convert.ToString(Total));
                // txtCtotal.Text = val.ToString("N2");
                TextBoxRemarks.Focus();
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }
        }
        protected void CostdetClick(object sender, EventArgs e)
        {
            itemdetailsdiv.Visible = true;
            kitdetailsdiv.Visible = false;
        }
        protected void KitdetClick(object sender, EventArgs e)
        {
            itemdetailsdiv.Visible = false;
            kitdetailsdiv.Visible = true;
        }
        protected void lbtncode_Click(object sender, EventArgs e)
        {
            try
            {
                //    if (string.IsNullOrEmpty(txtexcutive.Text))
                //    {
                //        DisplayMessage("Please select sales executive", 1);
                //        return;
                //    }

                //if (IsAllovcateCustomer(txtexcutive.Text))
                //{
                //    ViewState["SEARCH"] = null;
                //    txtSearchbyword.Text = string.Empty;
                //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                //    DataTable result = CHNLSVC.CommonSearch.GetCustomerBYsalesExe(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                //    grdResult.DataSource = result;
                //    grdResult.DataBind();
                //    lblvalue.Text = "Customer_13";
                //    BindUCtrlDDLData(result);
                //    ViewState["SEARCH"] = result;
                //    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                //    txtSearchbyword.Text = "";
                //    txtSearchbyword.Focus();
                //    return;
                //}


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

        protected void txtNIC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetailsByNIC();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch_NIC_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "NIC";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtMobile_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetailsByMobile();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch_Mobile_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Mobile";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void txtexcutive_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, "CODE", txtexcutive.Text);
                if (result.Rows.Count == 1)
                {
                    txtexcutive.Text = result.Rows[0][0].ToString();
                    txtexcutive.ToolTip = result.Rows[0][1].ToString();
                    // lblSalesEx.Text = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("Invalid sales executive code", 1);
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnEx_Click(object sender, EventArgs e)
        {
            try
            {

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Sale_Ex";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }

        protected void ddlPriceBook_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                //DefaultLevel = "";
                LoadPriceLevel(ddlPriceBook.Text);

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
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
        protected void lbtnProCode_Click(object sender, EventArgs e)
        {
            txtFDate.Text = DateTime.Now.ToLongDateString();
            txtTDate.Text = DateTime.Now.ToLongDateString();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.projectCode);
            DataTable result = CHNLSVC.General.SearchProjectCode(SearchParams, (ddlSearchbykey.SelectedValue == null ? null : ddlSearchbykey.SelectedValue), txtSearchbyword.Text, DateTime.Now, DateTime.Now, "V");//SearchBOQDocNo
            grdResultD.DataSource = result;
            grdResultD.DataBind();
            ViewState["SEARCH"] = result;
            lblvalue.Text = "doc";
            BindUCtrlDDLData2(result);
            txtSearchbyword.Focus();
            UserDPopoup.Show();
        }
        private void SaveBOQ()
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
            _SatProjectHeader.SPH_EST_COST = Convert.ToDecimal(lblcost.Text);
            _SatProjectHeader.SPH_EST_REV = Convert.ToDecimal(lblRevenue.Text);
            _SatProjectHeader.SPH_EX = txtexcutive.Text;
            _SatProjectHeader.SPH_LOC_DESC = "N/A";
            _SatProjectHeader.SPH_MOD_BY = Session["UserID"].ToString();
            _SatProjectHeader.SPH_OTH_EX = "N/A";
            _SatProjectHeader.SPH_PB = ddlPriceBook.Text;
            _SatProjectHeader.SPH_PB_LVL = ddlLevel.Text;
            _SatProjectHeader.SPH_PC = Session["UserDefProf"].ToString();
            _SatProjectHeader.SPH_PRO_LOC = txtlocation.Text;
            _SatProjectHeader.SPH_REF = txtref.Text;
            _SatProjectHeader.SPH_RMK = txtremark.Text;
            _SatProjectHeader.SPH_STATUS = "P";
            _SatProjectHeader.SPH_ANAL2 = "BOQ";
            _SatProjectHeader.SPH_COM_DT = Convert.ToDateTime(txtcompletedate.Text.ToString());
            //_SatProjectDetails.Add(new SatProjectDetails() { SPD_ACTVE = Convert.ToInt16(boqAct.SelectedValue) });
            _SatProjectHeader.SPH_OTH_DT = Convert.ToDateTime(txtComme.Text.ToString());

            rowsAffected = CHNLSVC.Sales.SaveBOQHDD(_SatProjectHeader, GenerateMasterAutoNumber(), _SatProjectDetails, out _docNo);
            SaveKitItems(_docNo, rowsAffected);
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
        //Dulaj 2018/Aug/13 save BOQ kit items
        private void SaveKitItems(string docNo, int afftected)
        {
            if (afftected != -1 && !String.IsNullOrEmpty(docNo))
            {
                int lineno = 1;
                if (satKitDetails.Count > 0)
                {
                    foreach (var kitItem in satKitDetails)
                    {
                        /* kitItem.SPK_LINE;
                       
                        kitItem.SPK_KIT_CD,
                        kitItem.SPK_KIT_DESC;
                        kitItem.SPK_KIT_MODEL;
                        kitItem.SPK_QTY;*/
                        kitItem.SPK_NO = docNo;
                        kitItem.SPK_ACTIVE = 1;
                        kitItem.SPK_SESSION = Session["SessionID"].ToString();
                        int saved = CHNLSVC.Inventory.SaveSatProjectKitDetails(kitItem);
                        lineno++;

                    }
                }
            }
        }
        private void UpdateBOQ()
        {
            int rowsAffected = 0;
            string _docNo = txtdoc.Text.ToString();

            _SatProjectHeader.SPH_COM = Session["UserCompanyCode"].ToString();
            _SatProjectHeader.SPH_CRE_BY = Session["UserID"].ToString();
            _SatProjectHeader.SPH_CUS_ADD1 = txtAddress1.Text;
            _SatProjectHeader.SPH_CUS_ADD2 = txtAddress2.Text;
            _SatProjectHeader.SPH_CUS_ADD3 = "N/A";
            _SatProjectHeader.SPH_CUS_CD = txtCustomer.Text;
            _SatProjectHeader.SPH_CUS_NAME = txtCusName.Text;
            _SatProjectHeader.SPH_DT = Convert.ToDateTime(txtDate.Text);
            _SatProjectHeader.SPH_EST_COST = Convert.ToDecimal(lblcost.Text);
            _SatProjectHeader.SPH_EST_REV = Convert.ToDecimal(lblRevenue.Text);
            _SatProjectHeader.SPH_EX = txtexcutive.Text;
            _SatProjectHeader.SPH_LOC_DESC = "N/A";
            _SatProjectHeader.SPH_MOD_BY = Session["UserID"].ToString();
            _SatProjectHeader.SPH_OTH_EX = "N/A";
            _SatProjectHeader.SPH_PB = ddlPriceBook.Text;
            _SatProjectHeader.SPH_PB_LVL = ddlLevel.Text;
            _SatProjectHeader.SPH_PC = Session["UserDefProf"].ToString();
            _SatProjectHeader.SPH_PRO_LOC = txtlocation.Text;
            _SatProjectHeader.SPH_REF = txtref.Text;
            _SatProjectHeader.SPH_RMK = txtremark.Text;
            _SatProjectHeader.SPH_STATUS = "";
            _SatProjectHeader.SPH_COM_DT = Convert.ToDateTime(txtcompletedate.Text.ToString());
            _SatProjectHeader.SPH_ANAL2 = "BOQ";
            //_SatProjectDetails.Add(new SatProjectDetails() { SPD_ACTVE = Convert.ToInt16(boqAct.SelectedValue) });
            _SatProjectHeader.SPH_RMK = txtremark.Text;
            rowsAffected = CHNLSVC.Sales.UpdateBOQHDD(_SatProjectHeader, _SatProjectDetails, satKitDetails, out _docNo); //UpdateBOQsatProHdr

            if (rowsAffected != -1)
            {
                string Msg = "Successfully Updated. " + _docNo;
                lblstatus.Text = "";
                DisplayMessage(Msg, 3);
                PageClear();
            }
            else
            {
                DisplayMessage(_docNo, 4);
            }
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

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString(); // string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "BOQ";
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = "BOQ";
            masterAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            return masterAuto;
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            //string SearchParams = string.Empty;
            //SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            //DataTable cus_result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
            //SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            //DataTable loc_result = CHNLSVC.CommonSearch.GetUserLocationByRoleAndCompany(SearchParams, null, null);
            //SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
            //DataTable salesEx_result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, null, null);
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            if (lblstatus.Text == "PENDING")
            {
                DisplayMessage("This document has already saved.", 1);
                return;
            }
            if (lblstatus.Text == "APPROVED")
            {
                DisplayMessage("This document has already approved.", 1);
                return;
            }
            //31/05/2017
            //var cusCode = from row in cus_result.AsEnumerable() where row.Field<string>("CODE").Trim() == txtCustomer.Text select row;
            //List<DataRow> cCode = cusCode.ToList();
            //if (cCode.Count <= 0)
            //{
            //    DisplayMessage("Please Select Valid Customer Code", 1);
            //    return;
            //}
            //var locCode = from row in loc_result.AsEnumerable() where row.Field<string>("CODE").Trim() == txtlocation.Text select row;
            //List<DataRow> lCode = locCode.ToList();
            //if (lCode.Count <= 0)
            //{
            //    DisplayMessage("Please Select Valid Location Code", 1);
            //    return;
            //}
            //var salesExCode = from row in loc_result.AsEnumerable() where row.Field<string>("CODE").Trim() == txtexcutive.Text select row;
            //List<DataRow> sECode = salesExCode.ToList();
            //if (lCode.Count <= 0)
            //{
            //    DisplayMessage("Please Select Valid sales executive Code", 1);
            //    return;
            //}
            if (Convert.ToDateTime(txtDate.Text).Date > Convert.ToDateTime(txtcompletedate.Text).Date)
            {
                DisplayMessage("Complete Date Must Be Greater Than Starting Date...!", 1);
                return;
            }

            if (validation())
            {
                SaveBOQ();
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
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            //string SearchParams = string.Empty;
            //SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            //DataTable cus_result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
            //SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            //DataTable loc_result = CHNLSVC.CommonSearch.GetUserLocationByRoleAndCompany(SearchParams, null, null);
            if (txtUpdateconformmessageValue.Value == "No")
            {
                return;
            }
            if (lblstatus.Text == "APPROVED")
            {
                DisplayMessage("This document has already approved.", 1);
                return;
            }
            if (_SatProjectHeader == null)
            {
                string Msg = "Please select BOQ number";
                DisplayMessage(Msg, 1);
            }
            if (_SatProjectHeader.SPH_NO == null)
            {
                string Msg = "Please select BOQ number";
                DisplayMessage(Msg, 1);
            }
            //var cusCode = from row in cus_result.AsEnumerable() where row.Field<string>("CODE").Trim() == txtCustomer.Text select row;
            //List<DataRow> cCode = cusCode.ToList();
            //if (cCode.Count <= 0)
            //{
            //    DisplayMessage("Please Select Valid Customer Code", 1);
            //    return;
            //}
            //var locCode = from row in loc_result.AsEnumerable() where row.Field<string>("CODE").Trim() == txtlocation.Text select row;
            //List<DataRow> lCode = locCode.ToList();
            //if (lCode.Count <= 0)
            //{
            //    DisplayMessage("Please Select Valid Location Code", 1);
            //    return;
            //}
            //var salesExCode = from row in loc_result.AsEnumerable() where row.Field<string>("CODE").Trim() == txtexcutive.Text select row;
            //List<DataRow> sECode = salesExCode.ToList();
            //if (lCode.Count <= 0)
            //{
            //    DisplayMessage("Please Select Valid sales executive Code", 1);
            //    return;
            //}
            if (Convert.ToDateTime(txtDate.Text).Date > Convert.ToDateTime(txtcompletedate.Text).Date)
            {
                DisplayMessage("Complete Date Must Be Greater Than Starting Date...!", 1);
                return;
            }

            if (validation())
            {
                UpdateBOQ();
            }

        }

        protected void lbtnaddcost_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtunitcost.Text == "0.00")
                {
                    DisplayMessage("canot add zero cost", 1);
                    txtCItem.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCItem.Text))
                {
                    DisplayMessage("Select a item code", 1);
                    txtCItem.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCQty.Text))
                {
                    DisplayMessage("Enter quantity", 1);
                    txtCQty.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtunitcost.Text))
                {
                    DisplayMessage("Enter unit cost", 1);
                    txtCQty.Focus();
                    return;
                }
                if (_SatProjectDetails == null)
                {
                    _SatProjectDetails = new List<SatProjectDetails>();
                }
                SatProjectDetails _cost = new SatProjectDetails();
                _cost.SPD_ACT_COST = Convert.ToDecimal(txtunitcost.Text);
                _cost.SPD_NO = txtdoc.Text;
                _cost.SPD_ITM = txtCItem.Text;
                _cost.SPD_ITM_DESC = lblItemDescription.Text;
                _cost.SPD_MODEL = lblItemModel.Text;
                _cost.SPD_MI_ITM_UOM = lblUom.Text;
                _cost.SPD_EST_QTY = Convert.ToDecimal(txtCQty.Text);
                decimal itemTotal = Convert.ToDecimal(txtunitcost.Text) * Convert.ToDecimal(txtCQty.Text);
                string itemTotalfomat = FormatToCurrency(itemTotal.ToString());
                _cost.SPD_EST_COST = Convert.ToDecimal(itemTotalfomat);
                _cost.SPD_ACTVE = 1;// Convert.ToInt16(boqAct.SelectedValue);
                if (_SatProjectDetails.Count > 0)
                {
                    var _fiilterItem = _SatProjectDetails.SingleOrDefault(x => x.SPD_ITM == _cost.SPD_ITM);
                    if (_fiilterItem != null)
                    {
                        _fiilterItem.SPD_MRN_BAL = Convert.ToDecimal(txtCQty.Text);
                        _fiilterItem.SPD_EST_QTY = Convert.ToDecimal(txtCQty.Text);
                        _fiilterItem.SPD_EST_COST = Convert.ToDecimal(txtunitcost.Text);
                    }
                    else
                    {
                        _cost.SPD_MRN_BAL = _cost.SPD_EST_QTY;
                        _SatProjectDetails.Add(_cost);
                    }
                }
                else
                {
                    _cost.SPD_MRN_BAL = _cost.SPD_EST_QTY;
                    _SatProjectDetails.Add(_cost);
                }


                grdCost.DataSource = _SatProjectDetails;
                grdCost.DataBind();




                //   _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), string.Empty, ddlPriceBook.Text.ToString(), ddlLevel.Text.ToString(), txtCustomer.Text, ITM, Convert.ToDecimal(1), Convert.ToDateTime(DateTime.Today));
                //Revenue 
                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                //if (revdet == null)
                // {
                List<SatProjectDetails> revdet = new List<SatProjectDetails>();
                //}

                foreach (var prdata in _SatProjectDetails)
                {

                    _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), string.Empty, ddlPriceBook.Text.ToString(), ddlLevel.Text.ToString(), txtCustomer.Text, prdata.SPD_ITM, Convert.ToDecimal(prdata.SPD_EST_QTY), Convert.ToDateTime(DateTime.Today));
                    if (_priceDetailRef.Count > 0)
                    {
                        SatProjectDetails obrevdet = new SatProjectDetails();
                        obrevdet.SPD_ACT_COST = prdata.SPD_ACT_COST;
                        obrevdet.SPD_ACT_QTY = prdata.SPD_ACT_QTY;
                        obrevdet.SPD_ACT_REV = prdata.SPD_ACT_REV;
                        obrevdet.SPD_ACTVE = prdata.SPD_ACTVE;
                        obrevdet.SPD_EST_COST = prdata.SPD_EST_COST;
                        obrevdet.SPD_EST_QTY = prdata.SPD_EST_QTY;
                        obrevdet.SPD_EST_REV = ((Convert.ToDecimal(_priceDetailRef.FirstOrDefault().Sapd_itm_price.ToString()) * prdata.SPD_EST_QTY) - Convert.ToDecimal(prdata.SPD_EST_COST.ToString()));
                        obrevdet.SPD_INV_PRT_DESC = prdata.SPD_INV_PRT_DESC;
                        obrevdet.SPD_LINE = prdata.SPD_LINE;
                        obrevdet.SPD_MODEL = prdata.SPD_MODEL;
                        obrevdet.SPD_NO = prdata.SPD_NO;
                        obrevdet.SPD_SEQ = prdata.SPD_SEQ;
                        obrevdet.SPD_ITM = prdata.SPD_ITM;
                        obrevdet.SPD_ITM_DESC = prdata.SPD_ITM_DESC;
                        revdet.Add(obrevdet);

                    }
                    else
                    {
                        SatProjectDetails obrevdet = new SatProjectDetails();
                        obrevdet.SPD_ACT_COST = prdata.SPD_ACT_COST;
                        obrevdet.SPD_ACT_QTY = prdata.SPD_ACT_QTY;
                        obrevdet.SPD_ACT_REV = prdata.SPD_ACT_REV;
                        obrevdet.SPD_ACTVE = prdata.SPD_ACTVE;
                        obrevdet.SPD_EST_COST = prdata.SPD_EST_COST;
                        obrevdet.SPD_EST_QTY = prdata.SPD_EST_QTY;
                        obrevdet.SPD_EST_REV = prdata.SPD_EST_REV;
                        obrevdet.SPD_INV_PRT_DESC = prdata.SPD_INV_PRT_DESC;
                        obrevdet.SPD_LINE = prdata.SPD_LINE;
                        obrevdet.SPD_MODEL = prdata.SPD_MODEL;
                        obrevdet.SPD_NO = prdata.SPD_NO;
                        obrevdet.SPD_SEQ = prdata.SPD_SEQ;
                        obrevdet.SPD_ITM = prdata.SPD_ITM;
                        obrevdet.SPD_ITM_DESC = prdata.SPD_ITM_DESC;
                        revdet.Add(obrevdet);
                    }



                }

                Session["revdlist"] = revdet;
                grdRevenue.DataSource = revdet;
                grdRevenue.DataBind();

                //
                if (satKitDetails.Count >= 0)
                {
                    /*
                       _cost.SPD_EST_QTY = Convert.ToDecimal(txtCQty.Text);
                decimal itemTotal = Convert.ToDecimal(txtunitcost.Text) * Convert.ToDecimal(txtCQty.Text);
                     */

                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(txtCItem.Text);
                    var _filter = satKitDetails.Where(x => x.SPK_KIT_CD == _mstItm.Mi_cd).ToList();
                    if (_filter.Count == 0)
                    {
                        SatProjectKitDetails satKitDet = new SatProjectKitDetails();
                        satKitDet.SPK_ACTIVE = 1;
                        satKitDet.SPK_CRE_BY = Session["UserID"].ToString();
                        satKitDet.SPK_KIT_CD = _mstItm.Mi_cd;
                        satKitDet.SPK_KIT_DESC = _mstItm.Mi_shortdesc;
                        satKitDet.SPK_MOD_BY = Session["UserID"].ToString();
                        satKitDet.SPK_MOD_DT = DateTime.Now;
                        satKitDet.SPK_QTY = Convert.ToDecimal(txtCQty.Text);
                        satKitDet.SPK_KIT_MODEL = _mstItm.Mi_model;
                        satKitDet.SPK_COST = Convert.ToDecimal(txtunitcost.Text);
                        satKitDet.SPK_UNIT_PRICE = 0;
                        satKitDet.SPK_TOTAL_COST = Convert.ToDecimal(txtunitcost.Text) * Convert.ToDecimal(txtCQty.Text);
                        satKitDet.SPK_TOTAL_PRICE = 0;
                        satKitDet.SPK_RMK = "testt";
                        satKitDetails.Add(satKitDet);
                        SatProjectGridView.DataSource = satKitDetails;
                        SatProjectGridView.DataBind();
                    }
                }
                if (_SatProjectDetails.Count > 0)
                {
                    caltotal();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void caltotal()
        {
            decimal total = _SatProjectDetails.Sum(item => item.SPD_EST_COST);
            lblTotalCost.Text = FormatToCurrency(Convert.ToString(total));

            List<SatProjectDetails> revdlist = new List<SatProjectDetails>();
            revdlist = (List<SatProjectDetails>)Session["revdlist"];
            decimal totalRev = revdlist.Sum(item => item.SPD_EST_REV);
            lblTotalRevenue.Text = FormatToCurrency(Convert.ToString(totalRev));
            clearitem();
            AmountCal();
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

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {

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


            }
            catch (Exception ex)
            {
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

        protected void lbtnDetaltecost_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "Yes")
            {
                if (_SatProjectDetails.Count > 0)
                {
                    GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                    Label Item = (Label)dr.FindControl("lblspd_itm");
                    Int32 rowIndex = dr.RowIndex;
                    var _update = _SatProjectDetails.Single(x => x.SPD_ITM == Item.Text);


                    List<SatProjectDetails> revdlist = new List<SatProjectDetails>();
                    revdlist = (List<SatProjectDetails>)Session["revdlist"];
                    var _updaterevdlist = revdlist.Single(x => x.SPD_ITM == Item.Text);
                    _update.SPD_ACTVE = 0;
                    _updaterevdlist.SPD_ACTVE = 0;

                    //if (_update.SPD_SEQ == 0)
                    //{
                    //    _SatProjectDetails.RemoveAt(rowIndex);
                    //}
                    //if (_updaterevdlist.SPD_SEQ == 0)
                    //{
                    //    revdlist.RemoveAt(rowIndex);
                    //}

                    Label lbl = grdCost.Rows[rowIndex].FindControl("lblspd_itm") as Label;
                    int sqnNo = _SatProjectDetails.Where(r => r.SPD_ITM == lbl.Text).FirstOrDefault().SPD_SEQ;
                    int lineNo = _SatProjectDetails.Where(r => r.SPD_ITM == lbl.Text).FirstOrDefault().SPD_LINE;
                    string spdNo = _SatProjectDetails.Where(r => r.SPD_ITM == lbl.Text).FirstOrDefault().SPD_NO;
                    ViewState["itemNo"] = Item.Text;
                    ViewState["sqnNo"] = sqnNo;
                    ViewState["lineNo"] = lineNo;
                    ViewState["spdNo"] = spdNo;
                    try
                    {
                        int delBoqNo = CHNLSVC.Sales.BOQCostDelete(spdNo, sqnNo, lineNo, lbl.Text);
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(ex.Message, 4);
                    }

                    if (_SatProjectDetails.Count > rowIndex)
                    {
                        _SatProjectDetails.RemoveAt(rowIndex);
                    }

                    var newtotal = _SatProjectDetails.Where(x => x.SPD_ACTVE == 1).Sum(y => y.SPD_EST_COST);
                    lblTotalCost.Text = newtotal.ToString();
                    grdCost.DataSource = _SatProjectDetails.Where(x => x.SPD_ACTVE == 1);
                    grdCost.DataBind();

                    grdRevenue.DataSource = revdlist.Where(x => x.SPD_ACTVE == 1);
                    grdRevenue.DataBind();
                }
            }
            else
            {
                return;
            }


        }

        protected void lbtnDetalteKit_Click(object sender, EventArgs e)
        {
            //   if (txtDeleteconformmessageValue.Value == "Yes")
            //{

            if (_SatProjectDetails.Count > 0)
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label _kitCode = (Label)dr.FindControl("lblspk_kit_cd");
                Int32 rowIndex = dr.RowIndex;

                var kitdetails = satKitDetails.Where(x => x.SPK_KIT_CD == _kitCode.Text).ToList();
                // SatProjectGridView.DeleteRow(rowIndex);
                foreach (var kit in kitdetails)
                {
                    kit.SPK_ACTIVE = 0;
                    if (kit.SPK_SEQ > 0)
                    {
                        int satKitDel = CHNLSVC.Inventory.DeleteKitItemsBySeq(kit.SPK_SEQ);
                    }
                }
                satKitDetails = kitdetails;
                satKitDetails = satKitDetails.Where(x => x.SPK_ACTIVE == 1).ToList();
                var _updateDetailsList = _SatProjectDetails.Where(x => x.SPD_KIT_ITM == _kitCode.Text).ToList();
                List<SatProjectDetails> revdlist = new List<SatProjectDetails>();
                SatProjectGridView.DataSource = satKitDetails;
                SatProjectGridView.DataBind();
                revdlist = (List<SatProjectDetails>)Session["revdlist"];

                foreach (var item in _SatProjectDetails)
                {
                    item.SPD_ACTVE = 0;
                }

                _SatProjectDetails = _SatProjectDetails.Where(x => x.SPD_ACTVE == 1).ToList();


                grdRevenue.DataSource = revdlist.Where(x => x.SPD_ACTVE == 1).ToList();
                grdRevenue.DataBind();
                var newtotal = _SatProjectDetails.Where(x => x.SPD_ACTVE == 1).Sum(y => y.SPD_EST_COST);
                lblTotalCost.Text = newtotal.ToString();
                grdCost.DataSource = null;
                grdCost.DataBind();
            }
        }
        protected void lbtngrdRevenuEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    grdRevenue.EditIndex = grdr.RowIndex;//e.NewEditIndex;

                    grdRevenue.DataSource = _SatProjectDetails;
                    grdRevenue.DataBind();

                }


            }
            catch (Exception ex)
            {

            }
        }
        protected void lbtngrdKitEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    SatProjectGridView.EditIndex = grdr.RowIndex;//e.NewEditIndex;

                    SatProjectGridView.DataSource = satKitDetails;
                    SatProjectGridView.DataBind();

                }

            }
            catch (Exception ex)
            {

            }
        }
        protected void lbtngrdKitUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    string _kitCode = (row.FindControl("lblspk_kit_cd") as Label).Text;
                    string description = (row.FindControl("txtspkKittextBox") as TextBox).Text;
                    List<SatProjectKitDetails> kitList = new List<SatProjectKitDetails>();
                    kitList = (List<SatProjectKitDetails>)Session["kitdet"];

                    if (kitList.Count > 0)
                    {
                        SatProjectGridView.EditIndex = -1;
                        var _fiilterItem = kitList.SingleOrDefault(x => x.SPK_KIT_CD == _kitCode);
                        if (_fiilterItem != null)
                        {
                            _fiilterItem.SPK_KIT_DESC = description;
                        }

                        var item = kitList.Where(r => r.SPK_KIT_CD == _kitCode).FirstOrDefault();
                        if (item != null)
                        {
                            item.SPK_KIT_DESC = description;
                        }

                        SatProjectGridView.DataSource = kitList;
                        SatProjectGridView.DataBind();
                        //caltotal();
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtngrdRevenueUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    string _item = (row.FindControl("lblspd_itm") as Label).Text;
                    string revenue = (row.FindControl("txtspd_est_rev") as TextBox).Text;

                    List<SatProjectDetails> revdlist = new List<SatProjectDetails>();
                    revdlist = (List<SatProjectDetails>)Session["revdlist"];

                    if (revdlist.Count > 0)
                    {
                        grdRevenue.EditIndex = -1;
                        var _fiilterItem = revdlist.SingleOrDefault(x => x.SPD_ITM == _item);
                        if (_fiilterItem != null)
                        {
                            _fiilterItem.SPD_EST_REV = Convert.ToDecimal(revenue);
                        }

                        var item = _SatProjectDetails.Where(r => r.SPD_ITM == _item).FirstOrDefault();
                        if (item != null)
                        {
                            item.SPD_EST_REV = Convert.ToDecimal(revenue);
                        }

                        grdRevenue.DataSource = revdlist;
                        grdRevenue.DataBind();
                        caltotal();
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }


        protected void lbtnSearch_Item2_Click(object sender, EventArgs e)
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

        protected void txtRitem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRQty.Text))
            {
                txtRQty.Text = "1";
            }
            if (string.IsNullOrEmpty(txtRitem.Text.ToUpper().Trim()))
            {
                txtRQty.Text = FormatToCurrency("0");
                txtunitRevenue.Text = FormatToCurrency("0");
                return;
            }

            try
            {
                if (!LoadItemDetail_2(txtRitem.Text.ToUpper()))
                {
                    DisplayMessage("Please check the item code", 1);
                    txtRitem.Text = "";
                    txtRitem.Focus();

                    return;
                }



            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }

        protected void lbtnaddRevenue_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtunitRevenue.Text == "0.0")//txtRtotal
                {
                    DisplayMessage("canot add zero revenue", 1);
                    txtCItem.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtRitem.Text))
                {
                    DisplayMessage("Select a item code", 1);
                    txtRitem.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtRQty.Text))
                {
                    DisplayMessage("Enter quantity", 1);
                    txtCQty.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtunitRevenue.Text))
                {
                    DisplayMessage("Enter unit cost", 1);
                    txtCQty.Focus();
                    return;
                }
                if (_SatProjectDetails == null)
                {
                    _SatProjectDetails = new List<SatProjectDetails>();
                }
                SatProjectDetails _cost = new SatProjectDetails();


                _cost.SPD_NO = txtdoc.Text;
                _cost.SPD_ITM = txtRitem.Text;
                _cost.SPD_ITM_DESC = lblRItemDescription.Text;
                _cost.SPD_MODEL = lblRItemModel.Text;
                _cost.SPD_EST_QTY = Convert.ToDecimal(txtRQty.Text);
                _cost.SPD_EST_REV = Convert.ToDecimal(txtunitRevenue.Text);
                _cost.SPD_ACTVE = 1;
                if (_SatProjectDetails.Count > 0)
                {
                    var _fiilterItem = _SatProjectDetails.SingleOrDefault(x => x.SPD_ITM == _cost.SPD_ITM);
                    if (_fiilterItem != null)
                    {
                        _fiilterItem.SPD_EST_QTY = Convert.ToDecimal(txtRQty.Text);
                        _fiilterItem.SPD_EST_REV = Convert.ToDecimal(txtunitRevenue.Text);
                    }
                    else
                    {
                        _SatProjectDetails.Add(_cost);
                    }
                }
                else
                {
                    _SatProjectDetails.Add(_cost);
                }


                grdCost.DataSource = _SatProjectDetails;
                grdCost.DataBind();
                grdRevenue.DataSource = _SatProjectDetails;
                grdRevenue.DataBind();
                if (_SatProjectDetails.Count > 0)
                {
                    caltotal();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtunitRevenue_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(txtRitem.Text)) return;
                if (IsNumeric(txtCQty.Text) == false)
                {
                    DisplayMessage("Please select valid quantity", 1);
                    return;
                }
                if (Convert.ToDecimal(txtRQty.Text.Trim()) == 0) return;


                //check minus unit price validation
                decimal _unitAmt = 0;
                bool _isUnitAmt = Decimal.TryParse(txtunitRevenue.Text, out _unitAmt);
                if (!_isUnitAmt)
                {
                    DisplayMessage("Unit revenue has to be number!", 1);
                    return;
                }
                if (_unitAmt <= 0)
                {
                    DisplayMessage("Unit revenue has to be greater than 0!", 1);
                    return;
                }
                if (string.IsNullOrEmpty(txtunitRevenue.Text)) txtunitRevenue.Text = FormatToCurrency("0");

                decimal val = Convert.ToDecimal(txtunitRevenue.Text);
                decimal Total = val * Convert.ToDecimal(txtRQty.Text);
                txtRtotal.Text = FormatToCurrency(Convert.ToString(Total));
                // txtCtotal.Text = val.ToString("N2");
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
                string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                if ((_itemSerializedStatus == "1") || (_itemSerializedStatus == "0"))
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

        protected void txtRQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                if ((_itemSerializedStatus == "1") || (_itemSerializedStatus == "0"))
                {
                    decimal number = Convert.ToDecimal(txtRQty.Text);
                    decimal fractionalPart = number % 1;
                    if (fractionalPart != 0)
                    {
                        DisplayMessage("only allow numeric value", 1);
                        return;
                    }


                }
                if (Convert.ToDecimal(txtRQty.Text.Trim()) < 0)
                {
                    DisplayMessage("Quantity should be positive value.", 1);
                    return;
                }
            }
            catch (Exception ex)
            {
                txtRQty.Text = FormatToQty("1");
                DisplayMessage(ex.Message, 4);
            }
        }

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
            else if (txtClearlconformmessageValue.Value == "No")
            {
                return;
            }
        }

        protected void txtdoc_TextChanged(object sender, EventArgs e)
        {
            LoadBOQ(txtdoc.Text.ToString());
        }

        protected void txtkititemcode_TextChanged(object sender, EventArgs e)
        {

            txtkititemcode.Text = txtkititemcode.Text.Trim().ToUpper();
            txtkitqty.Focus();
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
                        DisplayMessage("Please enter a valid Kit Item code", 2);
                        txtkititemcode.Text = string.Empty;
                        txtkititemcode.Focus();
                    }
                }
                else
                {
                    DisplayMessage("Please enter a valid Kit Item Code", 2);
                    txtkititemcode.Text = string.Empty;
                    txtkititemcode.Focus();
                }

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
                if (txtkitqty.Text.Contains("-"))
                {
                    DisplayMessage("Cannot add zero or minus qty", 1);
                    txtkitqty.Focus();
                    return;
                }

                decimal qty = Convert.ToDecimal(txtkitqty.Text);
                if (qty <= 0)
                {
                    DisplayMessage("Cannot add zero or minus qty", 1);
                    txtkitqty.Focus();
                    return;
                }
                List<ItemKitComponent> _kitlist = new List<ItemKitComponent>();
                decimal KitQty = Convert.ToDecimal(txtkitqty.Text);
                ItemKitComponent _kit = new ItemKitComponent();
                _kit.MIKC_ITM_CODE_MAIN = txtkititemcode.Text.Trim().ToUpper();
                if (_ItemKitComponent.Count > 0)
                {
                    var _filter = _ItemKitComponent.Where(x => x.MIKC_ITM_CODE_MAIN == _kit.MIKC_ITM_CODE_MAIN).ToList();
                    if (_filter.Count > 0)
                    {
                        DisplayMessage("Kit item has been already added", 1);
                        return;
                    }
                }
                _kitlist = CHNLSVC.Inventory.GetItemKitComponent_ProductPlane(_kit, Session["UserDefLoca"].ToString());
                if (_kitlist.Count < 1)
                {
                    DisplayMessage("Kit items not available to this kit code!", 1);
                    return;
                }
                if (_ItemKitComponent.Count > 0)
                {
                    _ItemKitComponent.AddRange(_kitlist);
                    foreach (ItemKitComponent _obj in _ItemKitComponent)
                    {
                        DataTable unitprice = CHNLSVC.Sales.GetUnitPriceNew(_obj.MIKC_ITM_CODE_COMPONENT, Session["UserDefLoca"].ToString());
                        SatProjectDetails _cost = new SatProjectDetails();
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
                        ////Added By Dulaj
                        //SatProjectKitDetails satKitDet = new SatProjectKitDetails();
                        //satKitDet.SPK_ACTIVE = 1;
                        //satKitDet.SPK_CRE_BY = Session["UserID"].ToString();
                        //satKitDet.SPK_KIT_CD = _itemdetail.Mi_cd;
                        //satKitDet.SPK_KIT_DESC = _itemdetail.Mi_shortdesc;
                        //satKitDet.SPK_MOD_BY = Session["UserID"].ToString();
                        //satKitDet.SPK_MOD_DT = DateTime.Now;
                        //satKitDet.SPK_QTY = KitQty;
                        //satKitDet.SPK_KIT_MODEL = _itemdetail.Mi_model;
                        ////satKitDet.SPK_SESSION = 
                        //satKitDetails.Add(satKitDet);

                        //SatProjectGridView.DataSource = satKitDetails;
                        //SatProjectGridView.DataBind();
                    }
                }
                else
                {
                    _ItemKitComponent = _kitlist;
                    //Added By Dulaj
                    //2018/Aug/29
                    decimal kitcost = 0;
                    decimal kitunitPrice = 0;
                    if (!(string.IsNullOrEmpty(KitPriceTextBox.Text)))
                    {
                        kitunitPrice = Convert.ToDecimal(KitPriceTextBox.Text);
                    }
                    if (!(string.IsNullOrEmpty(KitCostTextBox.Text)))
                    {
                        kitcost = Convert.ToDecimal(KitCostTextBox.Text);
                    }
                    //
                    if (satKitDetails.Count >= 0 && _kitlist.Count > 0)
                    {
                        MasterItem _mstItm = CHNLSVC.General.GetItemMaster(_kit.MIKC_ITM_CODE_MAIN);
                        var _filter = satKitDetails.Where(x => x.SPK_KIT_CD == _kit.MIKC_ITM_CODE_MAIN).ToList();
                        if (_filter.Count == 0)
                        {
                            SatProjectKitDetails satKitDet = new SatProjectKitDetails();
                            satKitDet.SPK_ACTIVE = 1;
                            satKitDet.SPK_CRE_BY = Session["UserID"].ToString();
                            satKitDet.SPK_KIT_CD = _mstItm.Mi_cd;
                            satKitDet.SPK_KIT_DESC = _mstItm.Mi_shortdesc;
                            satKitDet.SPK_MOD_BY = Session["UserID"].ToString();
                            satKitDet.SPK_MOD_DT = DateTime.Now;
                            satKitDet.SPK_QTY = KitQty;
                            satKitDet.SPK_KIT_MODEL = _mstItm.Mi_model;
                            satKitDet.SPK_COST = kitcost;
                            satKitDet.SPK_UNIT_PRICE = kitunitPrice;
                            satKitDet.SPK_TOTAL_COST = kitcost * KitQty;
                            satKitDet.SPK_TOTAL_PRICE = kitunitPrice * KitQty;
                            satKitDet.SPK_RMK = TextBoxRemarks.Text;
                            satKitDet.SPK_LINE = satKitDetails.Count + 1;
                            satKitDetails.Add(satKitDet);
                            SatProjectGridView.DataSource = satKitDetails;
                            SatProjectGridView.DataBind();
                        }
                    }


                }

                foreach (ItemKitComponent _obj in _ItemKitComponent)
                {
                    DataTable unitprice = CHNLSVC.Sales.GetUnitPriceNew(_obj.MIKC_ITM_CODE_COMPONENT, Session["UserDefLoca"].ToString());
                    SatProjectDetails _cost = new SatProjectDetails();
                    if (unitprice.Rows.Count > 0)
                    {
                        string unitpricess = unitprice.Rows[0]["UNITPRICE"].ToString();
                        //_obj.MIKC_COST = Convert.ToDecimal(unitpricess);
                        _cost.SPD_ACT_COST = Convert.ToDecimal(unitpricess);
                    }
                    else
                    {
                        // txtunitcost.Text = FormatToCurrency("0");
                        _cost.SPD_ACT_COST = 0;
                    }

                    _obj.MIKC_NO_OF_UNIT = _obj.MIKC_NO_OF_UNIT * KitQty;
                    //_cost.MIKC_COST = _obj.MIKC_COST;


                    _cost.SPD_NO = txtdoc.Text;
                    _cost.SPD_ITM = _obj.MIKC_ITM_CODE_COMPONENT;
                    _cost.SPD_ITM_DESC = _obj.MIKC_DESC_COMPONENT;
                    //_cost.SPD_MODEL = _obj.;
                    _cost.SPD_EST_QTY = _obj.MIKC_NO_OF_UNIT;
                    decimal itemTotal = _cost.SPD_ACT_COST * _cost.SPD_EST_QTY;
                    string itemTotalfomat = FormatToCurrency(itemTotal.ToString());
                    _cost.SPD_EST_COST = Convert.ToDecimal(itemTotalfomat);
                    _cost.SPD_ACTVE = 1;
                    _cost.SPD_KIT_ITM = _obj.MIKC_ITM_CODE_MAIN;
                    _cost.SPD_MRN_BAL = _obj.MIKC_NO_OF_UNIT;

                    if (_SatProjectDetails == null)
                    {
                        _SatProjectDetails = new List<SatProjectDetails>();
                    }
                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(_cost.SPD_ITM);
                    if (_mstItm != null)
                    {
                        _cost.SPD_MI_ITM_UOM = _mstItm.Mi_itm_uom;
                    }
                    if (_SatProjectDetails.Count > 0)
                    {
                        var _fiilterItem = _SatProjectDetails.SingleOrDefault(x => x.SPD_ITM == _cost.SPD_ITM);
                        if (_fiilterItem != null)
                        {
                            //_fiilterItem.SPD_EST_QTY = Convert.ToDecimal(txtCQty.Text);
                            //_fiilterItem.SPD_EST_COST = Convert.ToDecimal(txtunitcost.Text);
                        }
                        else
                        {
                            _SatProjectDetails.Add(_cost);
                        }
                    }
                    else
                    {
                        _SatProjectDetails.Add(_cost);
                    }
                }

                grdCost.DataSource = _SatProjectDetails;
                grdCost.DataBind();
                _ItemKitComponent = new List<ItemKitComponent>();

                //   _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), string.Empty, ddlPriceBook.Text.ToString(), ddlLevel.Text.ToString(), txtCustomer.Text, ITM, Convert.ToDecimal(1), Convert.ToDateTime(DateTime.Today));
                //Revenue 
                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                //if (revdet == null)
                // {
                List<SatProjectDetails> revdet = new List<SatProjectDetails>();
                //}

                foreach (var prdata in _SatProjectDetails)
                {

                    _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), string.Empty, ddlPriceBook.Text.ToString(), ddlLevel.Text.ToString(), txtCustomer.Text, prdata.SPD_ITM, Convert.ToDecimal(prdata.SPD_EST_QTY), Convert.ToDateTime(DateTime.Today));
                    if (_priceDetailRef.Count > 0)
                    {
                        SatProjectDetails obrevdet = new SatProjectDetails();
                        obrevdet.SPD_ACT_COST = prdata.SPD_ACT_COST;
                        obrevdet.SPD_ACT_QTY = prdata.SPD_ACT_QTY;
                        obrevdet.SPD_ACT_REV = prdata.SPD_ACT_REV;
                        obrevdet.SPD_ACTVE = prdata.SPD_ACTVE;
                        obrevdet.SPD_EST_COST = prdata.SPD_EST_COST;
                        obrevdet.SPD_EST_QTY = prdata.SPD_EST_QTY;
                        obrevdet.SPD_EST_REV = ((Convert.ToDecimal(_priceDetailRef.FirstOrDefault().Sapd_itm_price.ToString()) * prdata.SPD_EST_QTY) - Convert.ToDecimal(prdata.SPD_EST_COST.ToString()));
                        obrevdet.SPD_INV_PRT_DESC = prdata.SPD_INV_PRT_DESC;
                        obrevdet.SPD_LINE = prdata.SPD_LINE;
                        obrevdet.SPD_MODEL = prdata.SPD_MODEL;
                        obrevdet.SPD_NO = prdata.SPD_NO;
                        obrevdet.SPD_SEQ = prdata.SPD_SEQ;
                        obrevdet.SPD_ITM = prdata.SPD_ITM;
                        obrevdet.SPD_ITM_DESC = prdata.SPD_ITM_DESC;
                        revdet.Add(obrevdet);
                    }
                    else
                    {
                        SatProjectDetails obrevdet = new SatProjectDetails();
                        obrevdet.SPD_ACT_COST = prdata.SPD_ACT_COST;
                        obrevdet.SPD_ACT_QTY = prdata.SPD_ACT_QTY;
                        obrevdet.SPD_ACT_REV = prdata.SPD_ACT_REV;
                        obrevdet.SPD_ACTVE = prdata.SPD_ACTVE;
                        obrevdet.SPD_EST_COST = prdata.SPD_EST_COST;
                        obrevdet.SPD_EST_QTY = prdata.SPD_EST_QTY;
                        obrevdet.SPD_EST_REV = prdata.SPD_EST_REV;
                        obrevdet.SPD_INV_PRT_DESC = prdata.SPD_INV_PRT_DESC;
                        obrevdet.SPD_LINE = prdata.SPD_LINE;
                        obrevdet.SPD_MODEL = prdata.SPD_MODEL;
                        obrevdet.SPD_NO = prdata.SPD_NO;
                        obrevdet.SPD_SEQ = prdata.SPD_SEQ;
                        obrevdet.SPD_ITM = prdata.SPD_ITM;
                        obrevdet.SPD_ITM_DESC = prdata.SPD_ITM_DESC;
                        revdet.Add(obrevdet);
                    }
                }

                Session["revdlist"] = revdet;
                grdRevenue.DataSource = revdet;
                grdRevenue.DataBind();
                if (_SatProjectDetails.Count > 0)
                {
                    caltotal();
                }
                clearAddKit();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        private void clearAddKit()
        {
            txtkititemcode.Text = "";
            txtkitqty.Text = "";
            KitCostTextBox.Text = "";
            KitPriceTextBox.Text = "";
            KitTotalCost.Text = "";
            KitTotalPrice.Text = "";
            TextBoxRemarks.Text = "";
        }
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "doc")
            {
                txtdoc.Text = grdResultD.SelectedRow.Cells[1].Text;
                LoadBOQ(txtdoc.Text);
            }
            txtSearchbywordD.Text = string.Empty;
        }

        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            //if (lblvalue.Text == "doc")
            //{
            //    DataTable _tbl = (DataTable)ViewState["SEARCH"];
            //    grdResultD.DataSource = _tbl;
            //    grdResultD.DataBind();
            //    grdResultD.PageIndex = 0;
            //    UserDPopoup.Show();
            //    return;
            //}
            if (lblvalue.Text == "doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.projectCode);
                DataTable result = CHNLSVC.General.SearchProjectCode(SearchParams, ddlSearchbykeyD.SelectedItem.Text, "%" + txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), "S");  //SearchBOQDocNo
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                grdResultD.PageIndex = 0;
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

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            txtSearchbywordD.Text = string.Empty;
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.projectCode);
                DataTable result = CHNLSVC.General.SearchProjectCode(SearchParams, ddlSearchbykeyD.SelectedItem.Text, "%" + txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), "D");  //SearchBOQDocNo
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
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

        protected void lbtCancle_Click(object sender, EventArgs e)
        {
            if (txtCancelconformmessageValue.Value == "No")
            {
                return;
            }
            else if (txtCancelconformmessageValue.Value == "Yes")
            {
                if (txtdoc.Text != null && txtdoc.Text != "")
                {
                    if (lblstatus.Text == "PENDING")
                    {
                        Int32 effect = CHNLSVC.Sales.BOQCanclation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtdoc.Text.ToString());
                        if (effect != -1)
                        {
                            string Msg = "Successfully cancelled. " + txtdoc.Text;
                            DisplayMessage(Msg, 3);
                            PageClear();
                        }
                    }
                    else
                        DisplayMessage("Selected BOQ No already approved", 1);
                }
                else
                    DisplayMessage("Please select BOQ No", 1);
            }
            //Response.Redirect(Request.RawUrl, false);
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
        protected void lbtnKitBrekUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (_SatProjectDetails != null)
                {
                    if (_SatProjectDetails.Count > 0)
                    {
                        var _tmpList = _SatProjectDetails.Where(c => c.SPD_KIT_ITM != null && c.SPD_KIT_ITM != "").ToList();
                        if (_tmpList != null)
                        {
                            List<ItemKitComponent> _kitList = new List<ItemKitComponent>();
                            ItemKitComponent _kit = new ItemKitComponent();
                            foreach (var item in _tmpList)
                            {
                                _kit = new ItemKitComponent();
                                _kit.MIKC_ITM_CODE_MAIN = item.SPD_KIT_ITM;
                                var _kitava = _kitList.Where(c => c.MIKC_ITM_CODE_MAIN == item.SPD_KIT_ITM).FirstOrDefault();
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

        protected void txtKitCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtKitCode.Text))
                {
                    grdCost.DataSource = new int[] { };
                    if (_SatProjectDetails != null)
                    {
                        if (_SatProjectDetails.Count > 0)
                        {
                            var _kitList = _SatProjectDetails.Where(c => c.SPD_KIT_ITM == txtKitCode.Text.Trim().ToUpper()).ToList();
                            if (_kitList != null)
                            {
                                if (_kitList.Count > 0)
                                {
                                    grdCost.DataSource = _kitList;
                                }
                            }
                        }
                    }
                    grdCost.DataBind();
                }
                else
                {
                    grdCost.DataSource = new int[] { };
                    if (_SatProjectDetails != null)
                    {
                        if (_SatProjectDetails.Count > 0)
                        {
                            grdCost.DataSource = _SatProjectDetails;
                        }
                    }
                    grdCost.DataBind();
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
        protected void btnExcelDataUpload_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            _showExcelPop = true;
            popupExcel.Show();
        }
        protected void lbtnExcelUploadClose_Click(object sender, EventArgs e)
        {
            _showExcelPop = false;
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

                    if (Extension == ".xls" || Extension == ".XLS" || Extension == ".xlsx" || Extension == ".XLSX")
                    {

                    }
                    else
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = "Please select a valid excel (.xls or .xlsx) file";
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
        private void UploadData()
        {
            try
            {
                //   orderplancontanierForExcel = new List<ImportsBLContainer>();
                _ordPlnUpItmList = new List<OrderPlanExcelUploader>();
                _billogQuantitieslist = new List<BillOfQuantityExcelUpload>();
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
                for (int i = 1; i < _dtExData.Rows.Count; i++)
                {
                    #region column null check
                    if (string.IsNullOrEmpty(_dtExData.Rows[i][0].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][1].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][2].ToString())
                        )
                    {
                        i = _dtExData.Rows.Count;
                    }
                    #endregion
                    #region itm
                    _billOfQuantityExcelUpload = new BillOfQuantityExcelUpload();
                    string itemCode = string.IsNullOrEmpty(_dtExData.Rows[i][0].ToString()) ? "" : _dtExData.Rows[i][0].ToString().Trim().ToUpper();
                    string qty = string.IsNullOrEmpty(_dtExData.Rows[i][2].ToString()) ? "" : _dtExData.Rows[i][2].ToString().Trim().ToUpper();
                    string unitPrice = string.IsNullOrEmpty(_dtExData.Rows[i][3].ToString()) ? "" : _dtExData.Rows[i][3].ToString().Trim().ToUpper();

                    _billOfQuantityExcelUpload.Itm_cd = itemCode;
                    _billOfQuantityExcelUpload.Uom = string.IsNullOrEmpty(_dtExData.Rows[i][4].ToString()) ? "" : _dtExData.Rows[i][4].ToString().Trim().ToUpper();
                    _billOfQuantityExcelUpload.KitCode = string.IsNullOrEmpty(_dtExData.Rows[i][5].ToString()) ? "" : _dtExData.Rows[i][5].ToString().Trim().ToUpper();
                    _billOfQuantityExcelUpload.MrnBal = string.IsNullOrEmpty(_dtExData.Rows[i][6].ToString()) ? "" : _dtExData.Rows[i][6].ToString().Trim().ToUpper();

                    decimal number = 0;
                    bool isNumeric = decimal.TryParse(qty, out number);
                    if (!isNumeric)
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = "Row " + i.ToString() + " Quantity is invalid";
                        _showExcelPop = true;
                        popupExcel.Show();
                        return;
                    }
                    if (Convert.ToDecimal(qty) < 0)
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = "Row " + i.ToString() + " Quantity is invalid";
                        _showExcelPop = true;
                        popupExcel.Show();
                        return;
                    }
                    _billOfQuantityExcelUpload.Qty = Convert.ToDecimal(qty);
                    decimal price = 0;
                    isNumeric = decimal.TryParse(unitPrice, out price);
                    if (!isNumeric)
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = "Row " + i.ToString() + " Price is invalid";
                        _showExcelPop = true;
                        popupExcel.Show();
                        return;
                    }
                    if (Convert.ToDecimal(unitPrice) < 0)
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = "Row " + i.ToString() + " Price is invalid";
                        _showExcelPop = true;
                        popupExcel.Show();
                        return;
                    }
                    _billOfQuantityExcelUpload.Unit_price = Convert.ToDecimal(unitPrice);


                    _billOfQuantityExcelUpload.Line = 2;
                    if (_billogQuantitieslist.Count > 0)
                    {
                        _billOfQuantityExcelUpload.Line = _billogQuantitieslist.Max(c => c.Line) + 1;
                    }
                    _billogQuantitieslist.Add(_billOfQuantityExcelUpload);
                    #endregion
                }
                popupExcel.Hide();
                popOpExcSave.Show();
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

        private decimal ToDecimal(string _qty)
        {
            decimal d = 0, td = 0;
            if (!string.IsNullOrEmpty(_qty))
            {
                d = decimal.TryParse(_qty, out td) ? Convert.ToDecimal(_qty) : 0;
            }
            return d;
        }
        private Int32 ToInteger(string _qty)
        {
            Int32 d = 0, td = 0;
            d = Int32.TryParse(_qty, out td) ? Convert.ToInt32(_qty) : 0;
            return d;
        }
        protected void lbtnExcClose_Click(object sender, EventArgs e)
        {

        }
        protected void btnGenOrdPlans_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessExcelData();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        protected void btnCancelProcess_Click(object sender, EventArgs e)
        {

        }
        private void ProcessExcelData()
        {
            List<BillOfQuantityExcelUpload> _errList = new List<BillOfQuantityExcelUpload>();
            BillOfQuantityExcelUpload _err = new BillOfQuantityExcelUpload();

            List<MasterUOM> _uomList = CHNLSVC.General.GetItemUOM();
            List<MasterCurrency> _currList = CHNLSVC.General.GetAllCurrency("");
            MasterBusinessEntity _busEntity = new MasterBusinessEntity();

            foreach (var item in _billogQuantitieslist)
            {
                _err = new BillOfQuantityExcelUpload();
                _err.Line = item.Line;
                _err.Itm_cd = item.Itm_cd;
                _err.Model = item.Model;
                _err.Tmp_err_text = "";
                _err.Tmp_err = "";
                _err.KitCode = item.KitCode;
                string _erMsg = "";

                if (string.IsNullOrEmpty(item.Itm_cd) && string.IsNullOrEmpty(item.Model))
                {
                    _erMsg = "Please enter item code or model # ";
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (!string.IsNullOrEmpty(item.Itm_cd))
                {

                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(item.Itm_cd);
                    if (_mstItm == null)
                    {
                        _erMsg = "Invalid item code ! ";
                        _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Itm_cd : _err.Tmp_err_text;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                }
                if (!string.IsNullOrEmpty(item.KitCode))
                {

                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(item.KitCode);
                    if (_mstItm == null)
                    {
                        _erMsg = "Invalid Kit code ! ";
                        _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.KitCode : _err.Tmp_err_text;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                }
                if (!(_erMsg.Equals(string.Empty)))
                {
                    _errList.Add(_err);
                }
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
                foreach (var item in _billogQuantitieslist)
                {
                    SatProjectDetails _cost = new SatProjectDetails();
                    _cost.SPD_NO = txtdoc.Text;
                    _cost.SPD_ACT_COST = Convert.ToDecimal(item.Unit_price);
                    _cost.SPD_ITM = item.Itm_cd;
                    _cost.SPD_ITM_DESC = item.Itm_cd;
                    _cost.SPD_EST_QTY = item.Qty;
                    decimal itemTotal = item.Unit_price * item.Qty;
                    string itemTotalfomat = FormatToCurrency(itemTotal.ToString());
                    _cost.SPD_EST_COST = Convert.ToDecimal(itemTotalfomat);
                    _cost.SPD_ACTVE = 1;
                    _cost.SPD_KIT_ITM = item.KitCode;
                    _cost.SPD_MRN_BAL = Convert.ToDecimal(item.MrnBal);

                    if (_SatProjectDetails == null)
                    {
                        _SatProjectDetails = new List<SatProjectDetails>();
                    }
                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(_cost.SPD_ITM);
                    if (_mstItm != null)
                    {
                        _cost.SPD_MI_ITM_UOM = _mstItm.Mi_itm_uom;
                        _cost.SPD_MODEL = _mstItm.Mi_model;
                        _cost.SPD_ITM_DESC = _mstItm.Mi_shortdesc;
                    }
                    if (_SatProjectDetails.Count > 0)
                    {
                        var _fiilterItem = _SatProjectDetails.SingleOrDefault(x => x.SPD_ITM == _cost.SPD_ITM);
                        if (_fiilterItem != null)
                        {
                            _SatProjectDetails.Remove(_fiilterItem);
                            _SatProjectDetails.Add(_cost);
                        }
                        else
                        {
                            _SatProjectDetails.Add(_cost);
                        }
                    }
                    else
                    {
                        _SatProjectDetails.Add(_cost);
                    }
                }
                grdCost.DataSource = _SatProjectDetails;
                grdCost.DataBind();
                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                List<SatProjectDetails> revdet = new List<SatProjectDetails>();

                foreach (var prdata in _SatProjectDetails)
                {

                    _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), string.Empty, ddlPriceBook.Text.ToString(), ddlLevel.Text.ToString(), txtCustomer.Text, prdata.SPD_ITM, Convert.ToDecimal(prdata.SPD_EST_QTY), Convert.ToDateTime(DateTime.Today));
                    if (_priceDetailRef.Count > 0)
                    {
                        SatProjectDetails obrevdet = new SatProjectDetails();
                        obrevdet.SPD_ACT_COST = prdata.SPD_ACT_COST;
                        obrevdet.SPD_ACT_QTY = prdata.SPD_ACT_QTY;
                        obrevdet.SPD_ACT_REV = prdata.SPD_ACT_REV;
                        obrevdet.SPD_ACTVE = prdata.SPD_ACTVE;
                        obrevdet.SPD_EST_COST = prdata.SPD_EST_COST;
                        obrevdet.SPD_EST_QTY = prdata.SPD_EST_QTY;
                        obrevdet.SPD_EST_REV = ((Convert.ToDecimal(_priceDetailRef.FirstOrDefault().Sapd_itm_price.ToString()) * prdata.SPD_EST_QTY) - Convert.ToDecimal(prdata.SPD_EST_COST.ToString()));
                        obrevdet.SPD_INV_PRT_DESC = prdata.SPD_INV_PRT_DESC;
                        obrevdet.SPD_LINE = prdata.SPD_LINE;
                        obrevdet.SPD_MODEL = prdata.SPD_MODEL;
                        obrevdet.SPD_NO = prdata.SPD_NO;
                        obrevdet.SPD_SEQ = prdata.SPD_SEQ;
                        obrevdet.SPD_ITM = prdata.SPD_ITM;
                        obrevdet.SPD_ITM_DESC = prdata.SPD_ITM_DESC;
                        revdet.Add(obrevdet);

                    }
                    else
                    {
                        SatProjectDetails obrevdet = new SatProjectDetails();
                        obrevdet.SPD_ACT_COST = prdata.SPD_ACT_COST;
                        obrevdet.SPD_ACT_QTY = prdata.SPD_ACT_QTY;
                        obrevdet.SPD_ACT_REV = prdata.SPD_ACT_REV;
                        obrevdet.SPD_ACTVE = prdata.SPD_ACTVE;
                        obrevdet.SPD_EST_COST = prdata.SPD_EST_COST;
                        obrevdet.SPD_EST_QTY = prdata.SPD_EST_QTY;
                        obrevdet.SPD_EST_REV = prdata.SPD_EST_REV;
                        obrevdet.SPD_INV_PRT_DESC = prdata.SPD_INV_PRT_DESC;
                        obrevdet.SPD_LINE = prdata.SPD_LINE;
                        obrevdet.SPD_MODEL = prdata.SPD_MODEL;
                        obrevdet.SPD_NO = prdata.SPD_NO;
                        obrevdet.SPD_SEQ = prdata.SPD_SEQ;
                        obrevdet.SPD_ITM = prdata.SPD_ITM;
                        obrevdet.SPD_ITM_DESC = prdata.SPD_ITM_DESC;
                        revdet.Add(obrevdet);
                    }



                }

                Session["revdlist"] = revdet;
                grdRevenue.DataSource = revdet;
                grdRevenue.DataBind();
                if (_SatProjectDetails.Count > 0)
                {
                    caltotal();
                }
            }


        }
        protected void lbtnexcel_download(object sender, EventArgs e)
        {
            //_export = "Y";
            //_opt = "rad24";
            //_isSelected = true;
            //update_PC_List_RPTDB();

            //_invRepPara._GlbReportOtherLoc = txtotherloc.Text;

            string _err = "";
            string _filePath = "";
            DataTable costdt = ToDataTable(_SatProjectDetails);
            if (costdt.Rows.Count < 1)
            {
                DisplayMessage("No Records to Save", 2);
                return;

            }
            costdt.Columns.Remove("SPD_SEQ");
            costdt.Columns.Remove("SPD_LINE");
            costdt.Columns.Remove("SPD_NO");
            costdt.Columns.Remove("SPD_EST_COST");
            costdt.Columns.Remove("SPD_EST_REV");
            costdt.Columns.Remove("SPD_ACT_REV");
            costdt.Columns.Remove("SPD_INV_QTY");
            costdt.Columns.Remove("SPD_INV_PRT_DESC");
            costdt.Columns.Remove("SPD_ACTVE");
            costdt.Columns.Remove("SPD_MODEL");
            costdt.Columns.Remove("SPD_ACT_QTY");
            //costdt.Columns.Remove("");
            _filePath = CHNLSVC.MsgPortal.ExportExcel2007(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), costdt, out _err);

            if (!string.IsNullOrEmpty(_err))
            {
                // lbtnView.Enabled = true;
                DispMsg(_err, "W");
                return;
            }
            if (string.IsNullOrEmpty(_filePath))
            {
                DispMsg("The excel file path cannot identify. Please contact IT Dept", "W");
                return;
            }
            // string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString();
            _copytoLocal(_filePath);
            // Process p = new Process();
            //  p.StartInfo = new ProcessStartInfo("C:/Download_excel/" + filenamenew + ".xlsx");
            //  p.Start();
            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xls','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

        }
        private void _copytoLocal(string _filePath)
        {
            //  string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);

            string path = @"C:\Download_excel";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (file.Exists)
            {
                System.IO.File.Copy(@"\\" + _filePath, Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".xls", true);
            }
            else
            {
                DisplayMessage("This file does not exist.", 1);
            }
        }

        //private DataTable cost()
        //{
        //    DataTable dt = new DataTable();
        //    foreach(GridViewRow row in grdCost.Rows)
        //    {

        //    }
        //    return dt;
        //}

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

    }
}