using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.AssetManagement
{
    public partial class AssetMaster : BasePage
    {
        MasterItem _item = new MasterItem();
        List<mst_itm_fg_cost> _lstfg_cost = new List<mst_itm_fg_cost>();
        List<mst_itm_com_reorder> _lstreorder = new List<mst_itm_com_reorder>();
        List<MasterCompanyItem> _lstcomItem = new List<MasterCompanyItem>();
        List<BusEntityItem> _lstcusItem = new List<BusEntityItem>();
        List<BusEntityItem> _lstsupItem = new List<BusEntityItem>();
        List<mst_itm_redeem_com> _lstredCom = new List<mst_itm_redeem_com>();
        List<mst_itm_mrn_com> _lstmrn = new List<mst_itm_mrn_com>();
        List<mst_itm_replace> _lstreplace = new List<mst_itm_replace>();
        List<MasterItemComponent> _lstkit = new List<MasterItemComponent>();
        List<MasterItemTaxClaim> _lsttaxClaim = new List<MasterItemTaxClaim>();
        List<mst_itm_sevpd> _lstitmPrd = new List<mst_itm_sevpd>();
        List<MasterItemWarrantyPeriod> _lstitmWrd = new List<MasterItemWarrantyPeriod>();
        List<mst_itm_pc_warr> _lstpcWrd = new List<mst_itm_pc_warr>();
        List<mst_itm_channlwara> _lstchannelWrd = new List<mst_itm_channlwara>();
        List<mst_itm_container> _lstcont = new List<mst_itm_container>();
        List<ItemPrefix> _itemPrefix = new List<ItemPrefix>();
        Boolean _isAutoCode = false;
        private List<mst_itm_com_reorder> _lstreorder2 { get { return (List<mst_itm_com_reorder>)Session["lstreorder"]; } set { Session["lstreorder"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageClear();
                BindCombo();
                hdfCurrDate.Value = DateTime.Now.ToString("dd/MMM/yyyy");
                ddlprefixallow.SelectedIndex = ddlprefixallow.Items.IndexOf(ddlprefixallow.Items.FindByValue("NO"));
                ddlbooktype.SelectedIndex = ddlbooktype.Items.IndexOf(ddlbooktype.Items.FindByValue("NO"));
            }
            else
            {
                txtEffectiveDate.Text = Request[txtEffectiveDate.UniqueID];
                txtpcfrom.Text = Request[txtpcfrom.UniqueID];
                txtpcto.Text = Request[txtpcto.UniqueID];
                //txtserdfrom.Text = Request[txtpcto.UniqueID];
                //txtserdto.Text = Request[txtpcto.UniqueID];
            }
        }
        private void pageClear()
        {
            //ddlCostMeth.SelectedIndex = 1;
       //     ddlCostMeth_SelectedIndexChanged(null, null);
            Session["Multiplecom"] = "";
            ddlvalus();
            Session["_isAutoCode"] = "false";
     //       grdStatus.DataSource = new List<mst_itm_fg_cost>();
     //       grdStatus.DataBind();
            grdReorder.DataSource = new List<mst_itm_com_reorder>();
            grdReorder.DataBind();
            grdItemCost.DataSource = new List<mst_itm_com_reorder>();
            grdItemCost.DataBind();
            grdComItem.DataSource = new List<MasterCompanyItem>();
            grdComItem.DataBind();
            //grdRedeemCom.DataSource = new List<mst_itm_redeem_com>();
          //  grdRedeemCom.DataBind();
            grdCustomer.DataSource = new List<BusEntityItem>();
            grdCustomer.DataBind();
           // grdMRN.DataSource = new List<mst_itm_mrn_com>();
         //   grdMRN.DataBind();
            grdSupplier.DataSource = new List<BusEntityItem>();
            grdSupplier.DataBind();
            //grdRepalced.DataSource = new List<mst_itm_replace>();
            //grdRepalced.DataBind();
        //    grdKit.DataSource = new int[] { };
        //    grdKit.DataBind();
       //     grdCont.DataSource = new List<mst_itm_container>();
       //     grdCont.DataBind();
            grdTaxClaim.DataSource = new List<MasterItemTaxClaim>();
            grdTaxClaim.DataBind();
            grdservice.DataSource = new List<mst_itm_sevpd>();
            grdservice.DataBind();
            grdWarranty.DataSource = new List<MasterItemWarrantyPeriod>();
            grdWarranty.DataBind();
            grdPcwarranty.DataSource = new List<mst_itm_pc_warr>();
            grdPcwarranty.DataBind();
            grdcannelWara.DataSource = new List<mst_itm_channlwara>();
            grdcannelWara.DataBind();
            grdprefix.DataSource = new List<ItemPrefix>();
            grdprefix.DataBind();
            // txtEffectiveFrom.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
            txtEffectiveDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
            txtpcfrom.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
            txtpcto.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
            //txtserdfrom.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
            //txtserdto.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
            pnlsavechk.Visible = true;
            chkSave.Checked = false;
            txtpagecount.Text = string.Empty;
            txtbookprefix.Text = string.Empty;
            txtpreDescription.Text = string.Empty;
            txtpagecount.Text = "0";
            rdvolume.Checked = false;
            rdweight.Checked = false;
            txtwarehousvolum.Text = "0";
            txtResidual.Text = string.Empty;
            txtPurComp.Text = Session["UserCompanyCode"].ToString();
            // grdprefix
            //  Load_items();
            Session["chkfirst"] = "";
            txtMainWaraUOM.Text = "NOS";
        }

        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "masterItem")
            {
                txtItem.Text = ID;
                lblvalue.Text = "";
                Load_items();
                return;
            }
            if (lblvalue.Text == "ProductionPlan")
            {
                txtPackCode.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ItemBrand")
            {
                txtBrand.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ModelMaster")
            {
                txtModel.Text = ID;
                lblvalue.Text = "";
                IsDiscontinuedModel();
                //LoadCompanyByModel();
                List<MasterItemModel> _lstmodeltem = CHNLSVC.General.GetItemModel(ID);
                txtMainCat.Text = _lstmodeltem.FirstOrDefault().Mm_cat1;
                txtCat1.Text = _lstmodeltem.FirstOrDefault().Mm_cat2;
                txtCat2.Text = _lstmodeltem.FirstOrDefault().Mm_cat3;
                txtCat3.Text = _lstmodeltem.FirstOrDefault().Mm_cat4;
                return;
            }
            if (lblvalue.Text == "masterUOM")
            {
                txtUOM.Text = ID;
                lblvalue.Text = "";
                txtWaraUOM.Text = ID;
                txtMainWaraUOM.Text = ID;
                return;
            }
            if (lblvalue.Text == "masterCat1")
            {
                txtMainCat.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "masterCat2")
            {
                txtCat1.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "masterCat3")
            {
                txtCat2.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "masterCat4")
            {
                txtCat3.Text = ID;
                lblvalue.Text = "";
                return;
            }
            //if (lblvalue.Text == "masterCat5")
            //{
            //    txtCat4.Text = ID;
            //    lblvalue.Text = "";
            //    return;
            //}
            if (lblvalue.Text == "masterTax")
            {
                txtTaxStucture.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "masterColor")
            {
                txtColorExt.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "masterColor2")
            {
                txtColorInt.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Pc_HIRC_Company")
            {
                txtPurComp.Text = ID;
                lblvalue.Text = "";
                return;
            }
            //if (lblvalue.Text == "masterContry")
            //{
            //    txtCountry.Text = ID;
            //    lblvalue.Text = "";
            //    return;
            //}
            //if (lblvalue.Text == "InvoiceItemUnAssableByModel")
            //{
            //    txtFgood.Text = ID;
            //    lblvalue.Text = "";
            //    return;
            //}
            if (lblvalue.Text == "Pc_HIRC_Company2")
            {
                txtCompany.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Pc_HIRC_Company3")
            {
                string Des = grdResult.SelectedRow.Cells[2].Text;
                txtItemCom.Text = ID;
                txtitemDes.Text = Des;
                txtSupCom.Text = ID;
                txtCuscom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Pc_HIRC_Company4")
            {
                string Des = grdResult.SelectedRow.Cells[2].Text;
               // txtReCom.Text = ID;
                ///txtReDes.Text = Des;
                lblvalue.Text = "";
                return;
            }

            if (lblvalue.Text == "Customer")
            {
                string Des = grdResult.SelectedRow.Cells[2].Text;
                txtCust.Text = ID;
                txtCustName.Text = Des;
                lblvalue.Text = "";
                return;
            }
            //if (lblvalue.Text == "Pc_HIRC_Company5")
            //{
            //    string Des = grdResult.SelectedRow.Cells[2].Text;
            //    txtMrnCom.Text = ID;
            //    txtMrndes.Text = Des;
            //    lblvalue.Text = "";
            //    return;
            //}
            if (lblvalue.Text == "Supplier")
            {
                string Des = grdResult.SelectedRow.Cells[2].Text;
                txtSupp.Text = ID;
                txtSupName.Text = Des;
                lblvalue.Text = "";
                return;
            }
            //if (lblvalue.Text == "Rep.product")
            //{
            //    string Des = grdResult.SelectedRow.Cells[2].Text;
            //    txtrepItem.Text = ID;
            //    txtrepDes.Text = Des;
            //    lblvalue.Text = "";
            //    return;
            //}
            //if (lblvalue.Text == "Kit")
            //{
            //    string _id = grdResult.SelectedRow.Cells[2].Text.Trim();
            //    txtkitItem.Text = _id;
            //    lblvalue.Text = "";
            //    return;
            //}
            if (lblvalue.Text == "WeightUOM")
            {
                txtwuom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "DiamentionsUOM")
            {
                txtduom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Pc_HIRC_Company6")
            {
                txtclaimcom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ServiceUOM")
            {
                txtseruom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ServiceUOM2")
            {
                txtserduom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "WarrantyUOM")
            {
                txtWaraUOM.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "MainWarrantyUOM")
            {
                txtMainWaraUOM.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Pc_HIRC_Company7")
            {
                txtpcCom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Pc_HIRC_Location")
            {
                txtpcCom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Pc_HIRC_Company8")
            {
                txtpcCom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Loc_HIRC_Channel")
            {
                // txtChan.Text = ID;
                txtpcCom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Pc_HIRC_Company9")
            {
                txtwarrantycompany.Text = ID;
                lblvalue.Text = "";
                return;
            }


        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grdResult.PageIndex = e.NewPageIndex;
            //if (lblvalue.Text == "masterItem")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams,null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "ItemBrand")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "ModelMaster")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            //    DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterUOM")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterCat1")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterCat2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterCat3")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterCat4")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterCat5")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterTax")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterColor")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterColor2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if ((lblvalue.Text == "Pc_HIRC_Company") || (lblvalue.Text == "Pc_HIRC_Company2") || (lblvalue.Text == "Pc_HIRC_Company3")
            //    || (lblvalue.Text == "Pc_HIRC_Company4") || (lblvalue.Text == "Pc_HIRC_Company5") || (lblvalue.Text == "Pc_HIRC_Company6")
            //    || (lblvalue.Text == "Pc_HIRC_Company7") || (lblvalue.Text == "Pc_HIRC_Company8") || (lblvalue.Text == "Pc_HIRC_Company9"))
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            //    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterContry")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterContry);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "InvoiceItemUnAssableByModel")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}


            //if (lblvalue.Text == "Customer")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Supplier")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            //    DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Rep.product")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Kit")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "WeightUOM")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "DiamentionsUOM")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "ServiceUOM")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "ServiceUOM2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "WarrantyUOM")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Pc_HIRC_Location")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            //    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Loc_HIRC_Channel")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
            //    DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}

            try
            {

                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
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
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        private void FilterData()
        {
            try
            {
                #region Filter
                if (lblvalue.Text == "masterItem")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "ProductionPlan")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.Trim().ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "ItemBrand")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "ModelMaster")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                    DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterUOM")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterCat1")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterCat2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterCat3")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterCat4")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterCat5")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterTax")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterColor")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterColor2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if ((lblvalue.Text == "Pc_HIRC_Company") || (lblvalue.Text == "Pc_HIRC_Company2") || (lblvalue.Text == "Pc_HIRC_Company3")
                    || (lblvalue.Text == "Pc_HIRC_Company4") || (lblvalue.Text == "Pc_HIRC_Company5") || (lblvalue.Text == "Pc_HIRC_Company9")
                    || (lblvalue.Text == "Pc_HIRC_Company6") || (lblvalue.Text == "Pc_HIRC_Company7") || (lblvalue.Text == "Pc_HIRC_Company8"))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "masterContry")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterContry);
                    DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "InvoiceItemUnAssableByModel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "Customer")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "Supplier")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "Rep.product")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "Kit")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "WeightUOM")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;

                }
                if (lblvalue.Text == "DiamentionsUOM")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "ServiceUOM")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "ServiceUOM2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "WarrantyUOM")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "MainWarrantyUOM")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "Pc_HIRC_Location")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }

                if (lblvalue.Text == "Loc_HIRC_Channel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                #endregion
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        #endregion

        private void loadDefault()
        {
            txtwarehousvolum.Text = "0";
            // ddlActive.SelectedValue = "YES";
            ddlPayType.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlSerilize.SelectedIndex = 1;
            ddlWarranty.SelectedIndex = 1;
            ddlChassis.SelectedIndex = 0;
            ddlScanSub.SelectedIndex = 1;
            ddlIsSubItem.SelectedIndex = 1;
            ddlhpSalesAccept.SelectedIndex = 0;
            //ddlIsRegister.SelectedIndex = 1;
            txtColorExt.Text = "N/A";
            //txtCountry.Text = "SL";
            txtColorInt.Text = "N/A";
            txtPurComp.Text = Session["UserCompanyCode"].ToString();
            // txtItemCom.Text = Session["UserCompanyCode"].ToString();
            txtPrefix.Text = ".";
            ddlFoc.SelectedIndex = 1;
            ddlitemStatus.SelectedIndex = 0;
            //ddlwStatus.SelectedIndex = 0;
            //ddlwPeriod.SelectedIndex = 0;
            //ddlwarsPrd.SelectedIndex = 0;
            txtWarRem.Text = "NO WARRANTY";
            txtWaraUOM.Text = "MTH";
            txtUOM.Text = "NOS";
            txtCapacity.Text = "0";
            ttxHsCode.Text = "N/A";
            // ddlwStatus.Text = "GOOD LP";
            txtTaxStucture.Text = "STUC01";
            txtPackCode.Text = "N/A";
            ddlAgecType.Text = "N/A";
            txtSupCom.Text = Session["UserCompanyCode"].ToString();

            txtSdes.Text = string.Empty;
            txtLdes.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtPartNo.Text = string.Empty;
            txtPackCode.Text = string.Empty;
            txtUOM.Text = string.Empty;
            txtMainCat.Text = string.Empty;
            txtCat1.Text = string.Empty;
            txtCat2.Text = string.Empty;
            txtCat3.Text = string.Empty;
            //txtCat4.Text = string.Empty;

            // txtCapacity.Text= string.Empty;
            // txtColorExt.Text= string.Empty;
            // txtColorInt.Text= string.Empty;

            txtPrefix.Text = string.Empty;

            //txtPurComp.Text= string.Empty;
            ttxHsCode.Text = string.Empty;
            //txtCountry.Text = string.Empty;
            txtTaxStucture.Text = string.Empty;

            txtItem.Text = string.Empty;
            txttrimLeft.Text = string.Empty;
            txttrimRight.Text = string.Empty;
            txtSupCom.Text = string.Empty;
            txtgross.Text = string.Empty;
            txtnet.Text = string.Empty;
            txtwuom.Text = string.Empty;
            txthight.Text = string.Empty;
            txtwidth.Text = string.Empty;
            txtbreath.Text = string.Empty;
            txtduom.Text = string.Empty;
            //  txtWaraUOM.Text = string.Empty;
            txtWarRem.Text = string.Empty;
            // txtImagePath.Text = string.Empty;
            chkwarrprint.Checked = false;
           // chkAdditem.Checked = false;
            //chkAlterSer.Checked = false;
            chkFreeSer.Checked = false;
            //chkProRegis.Checked = false;
            //chkProInsurance.Checked = false;
        //    chkSerReq.Checked = false;
           // chkDiscont.Checked = false;
            chkMaintSupp.Checked = false;
         //   chkAppCond.Checked = false;
            //chkStcokMain.Checked = true;
       //     chkcust.Checked = false;
            chkIsExpired.Checked = false;
            chkSave.Checked = false;

            //chkAlowHP.Checked = false;
            //chkAlowInsu.Checked = false;
            //chkAlowVehReg.Checked = false;
            //chkAlowVehInsu.Checked = false;


        }
        void BindCombo()
        {
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
            if (string.IsNullOrEmpty(_masterComp.Mc_anal23) == false)
            {
                if (_masterComp.Mc_anal23 == "Y")
                {
                    _isAutoCode = true;
                    Session["_isAutoCode"] = "true";
                }
            }

            DataTable param = new DataTable();
            DataRow dr;
            param.Clear();

            param.Columns.Add("code", typeof(string));
            param.Columns.Add("des", typeof(string));


            dr = param.NewRow();
            dr["code"] = "F";
            dr["des"] = "Finish Good";
            param.Rows.Add(dr);
            dr = param.NewRow();
            dr["code"] = "R";
            dr["des"] = "Row Material";
            param.Rows.Add(dr);


            ddlStatus.DataSource = param;
            ddlStatus.DataTextField = "des";
            ddlStatus.DataValueField = "code";
            ddlStatus.DataBind();
            // ddlStatus.SelectedIndex = -1;
            //ddlStatus.Items.Insert(0, new ListItem("--Select-", ""));
            //ddlStatus.SelectedIndex = 0;


            DataTable param2 = new DataTable();
            DataRow dr2;

            param2.Columns.Add("code", typeof(string));
            param2.Columns.Add("des", typeof(string));
            param2.Clear();

            dr2 = param2.NewRow();
            dr2["code"] = "VAT_C";
            dr2["des"] = "Tax Claimable";
            param2.Rows.Add(dr2);

            dr2 = param2.NewRow();
            dr2["code"] = "VAT_UC";
            dr2["des"] = "Tax Unclaimable";
            param2.Rows.Add(dr2);


            ddlclaimcate.DataSource = param2;
            ddlclaimcate.DataTextField = "des";
            ddlclaimcate.DataValueField = "code";
            ddlclaimcate.DataBind();
            ddlclaimcate.SelectedIndex = -1;







            DataTable param3 = new DataTable();
            DataRow dr3;

            param3.Columns.Add("code", typeof(string));
            param3.Columns.Add("des", typeof(string));
            param3.Clear();

            dr3 = param3.NewRow();
            dr3["code"] = "A";
            dr3["des"] = "Allow";
            param3.Rows.Add(dr3);

            dr3 = param3.NewRow();
            dr3["code"] = "N";
            dr3["des"] = "Not Allow";
            param3.Rows.Add(dr3);


            ddlFoc.DataSource = param3;
            ddlFoc.DataTextField = "des";
            ddlFoc.DataValueField = "code";
            ddlFoc.DataBind();
            ddlFoc.SelectedIndex = -1;





            DataTable dtcnt = CHNLSVC.General.GetContainerType();
            //if (dtcnt != null && dtcnt.Rows.Count > 0)
            //{
            //    ddlContType.DataSource = dtcnt;
            //    ddlContType.DataTextField = "MCT_DESC";
            //    ddlContType.DataValueField = "MCT_TP";
            //    ddlContType.DataBind();
            //    ddlContType.SelectedIndex = -1;
            //    ddlContainerAct.SelectedIndex = ddlContainerAct.Items.IndexOf(ddlContainerAct.Items.FindByValue("YES"));

            //}
            DataTable dtwar = CHNLSVC.General.GetWarrantyPeriod();
            if (dtwar != null && dtwar.Rows.Count > 0)
            {
                ddlwarsPrd.DataSource = dtwar;
                ddlwarsPrd.DataTextField = "wp_des";
                ddlwarsPrd.DataValueField = "wp_period";
                ddlwarsPrd.DataBind();

                //dilshan -----------
                ddlSupPrd.DataSource = dtwar;
                ddlSupPrd.DataTextField = "wp_des";
                ddlSupPrd.DataValueField = "wp_period";
                ddlSupPrd.DataBind();
                //-------------------

                ddlwPeriod.DataSource = dtwar;
                ddlwPeriod.DataTextField = "wp_des";
                ddlwPeriod.DataValueField = "wp_period";
                ddlwPeriod.DataBind();
                ddlwdur.DataSource = dtwar;
                ddlwdur.DataTextField = "wp_des_al";
                ddlwdur.DataValueField = "wp_warr_prd_alt";
                ddlwdur.DataBind();
                ddlwdur.SelectedIndex = -1;


                ddlpcPrd.DataSource = dtwar;
                ddlpcPrd.DataTextField = "wp_des";
                ddlpcPrd.DataValueField = "wp_period";
                ddlpcPrd.DataBind();
                ddlpcPrd.Items.Insert(0, new ListItem("--Select--", ""));
                ddlpcPrd.SelectedIndex = 0;


                ddlChanPrd.DataSource = dtwar;
                ddlChanPrd.DataTextField = "wp_des";
                ddlChanPrd.DataValueField = "wp_period";
                ddlChanPrd.DataBind();
                ddlChanPrd.SelectedIndex = -1;

                loadDefault();

            }

            DataTable dtpl = CHNLSVC.General.GetItemTpAll();
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                ddlItemType.DataSource = dtpl;
                ddlItemType.DataTextField = "mstp_desc";
                ddlItemType.DataValueField = "mstp_cd";
                ddlItemType.DataBind();
                ddlItemType.Items.Insert(0, new ListItem("--Select--", ""));
                ddlItemType.SelectedIndex = 0;

            }

            DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
            if (_status != null && _status.Rows.Count > 0)
            {
                ddlwStatus.DataSource = _status;
                ddlwStatus.DataTextField = "MIS_DESC";
                ddlwStatus.DataValueField = "MIS_CD";
                ddlwStatus.DataBind();
                ddlwStatus.SelectedIndex = 0;
                ddlwStatus.Items.Insert(0, new ListItem("GOOD", "GOD"));
                ddlwStatus.SelectedIndex = 0;

                ddlPcstatus.DataSource = _status;
                ddlPcstatus.DataTextField = "MIS_DESC";
                ddlPcstatus.DataValueField = "MIS_CD";
                ddlPcstatus.DataBind();
                ddlPcstatus.Items.Insert(0, new ListItem("--Select--", ""));
                ddlPcstatus.SelectedIndex = 0;



                ddlchanStatus.DataSource = _status;
                ddlchanStatus.DataTextField = "MIS_DESC";
                ddlchanStatus.DataValueField = "MIS_CD";
                ddlchanStatus.DataBind();
                ddlchanStatus.SelectedIndex = -1;


                ddlchanStatus.DataSource = _status;
                ddlchanStatus.DataTextField = "MIS_DESC";
                ddlchanStatus.DataValueField = "MIS_CD";
                ddlchanStatus.DataBind();
                ddlchanStatus.SelectedIndex = -1;


                ddlserSts.DataSource = _status;
                ddlserSts.DataTextField = "MIS_DESC";
                ddlserSts.DataValueField = "MIS_CD";
                ddlserSts.DataBind();
                ddlserSts.Items.Insert(0, new ListItem("--Select--", ""));
                ddlserSts.SelectedIndex = 0;


                DataTable paramCH = new DataTable();
                DataRow drCH;

                paramCH.Columns.Add("code", typeof(string));
                paramCH.Columns.Add("des", typeof(string));
                paramCH.Clear();

                drCH = paramCH.NewRow();
                drCH["code"] = "CHA";
                drCH["des"] = "CHA";
                paramCH.Rows.Add(drCH);

                drCH = paramCH.NewRow();
                drCH["code"] = "FOC";
                drCH["des"] = "FOC";
                paramCH.Rows.Add(drCH);


                ddlPayType.DataSource = paramCH;
                ddlPayType.DataTextField = "des";
                ddlPayType.DataValueField = "code";
                ddlPayType.DataBind();
                ddlPayType.SelectedIndex = -1;
            }


        }
        void Load_items()
        {
            MasterItem _item = new MasterItem();
            _item = CHNLSVC.General.GetItemMaster(txtItem.Text.Trim().ToUpper());
            if (_item != null)
            {

                txtSdes.Text = _item.Mi_shortdesc;
                txtLdes.Text = _item.Mi_longdesc;
                txtMainCat.Text = _item.Mi_cate_1;
                txtCat1.Text = _item.Mi_cate_2;
                txtCat2.Text = _item.Mi_cate_3;
                txtCat3.Text = _item.Mi_cate_4;
                //txtCat4.Text = _item.Mi_cate_5;
                txtBrand.Text = _item.Mi_brand;
                txtModel.Text = _item.Mi_model;
                txtPartNo.Text = _item.Mi_part_no;
                txtColorInt.Text = _item.Mi_color_int;
                chkColorInt.Checked = _item.Mi_is_reqcolorint;
                txtColorExt.Text = _item.Mi_color_ext;
                chkColorExt.Checked = _item.Mi_is_reqcolorext;
                ddlItemType.SelectedValue = _item.Mi_itm_tp;
                ddlStcokMain.Text = _item.Mi_is_stockmaintain == "1" ? "YES" : "NO";
                // chkStcokMain.Checked = _item.Mi_is_stockmaintain == "1" ? true : false;

                ttxHsCode.Text = _item.Mi_hs_cd;
                txtUOM.Text = _item.Mi_itm_uom;
                txtseruom.Text = _item.Mi_itm_uom;
                txtWaraUOM.Text = _item.Mi_itm_uom;
                txtduom.Text = _item.Mi_dim_uom;
                txtbreath.Text = Convert.ToString(_item.Mi_dim_length);
                txtwidth.Text = Convert.ToString(_item.Mi_dim_width);
                txthight.Text = Convert.ToString(_item.Mi_dim_height);
                txtwuom.Text = _item.Mi_weight_uom;
                txtgross.Text = Convert.ToString(_item.Mi_gross_weight);
                txtnet.Text = Convert.ToString(_item.Mi_net_weight);
                txtResidual.Text = Convert.ToString(_item.MI_RESIDUAL_VAL);
                // txtImagePath.Text = _item.Mi_image_path;
                // ddlSerilize.SelectedItem.Text == "Decimal"
                // ddlSerilize.Text = _item.Mi_is_ser1 == 1 ? "YES" : "NO";
                string _isSerialized = _item.Mi_is_ser1 == 1 ? "YES" : _item.Mi_is_ser1 == -1 ? "DECIMAL" : "NO";
                ddlSerilize.SelectedIndex = ddlSerilize.Items.IndexOf(ddlSerilize.Items.FindByText(_isSerialized));
                ddlChassis.SelectedValue = _item.Mi_is_ser2.ToString();
                if (ddlChassis.SelectedValue == "2")
                {
                    lbtnbooktype.Visible = true;
                }
                else
                {
                    lbtnbooktype.Visible = false;
                }
                //_item.Mi_is_ser3
                ddlWarranty.Text = _item.Mi_warr == true ? "YES" : "NO";
                chkwarrprint.Checked = _item.Mi_warr_print;
                // ddlhpSalesAccept.Text = _item.Mi_hp_allow == true ? "YES" : "NO";
                //ddlIsRegister.Text = _item.Mi_insu_allow == true ? "YES" : "NO";
                //txtCountry.Text = _item.Mi_country_cd;
                txtPurComp.Text = _item.Mi_purcom_cd;
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(_item.Mi_itm_stus));
               // txtFgood.Text = _item.Mi_fgitm_cd;
               // txtAmount.Text = Convert.ToString(_item.Mi_itmtot_cost);
                ddlPayType.SelectedValue = _item.Mi_chg_tp;
                //ddlScanSub.Text = _item.Mi_is_scansub == true ? "YES" : "NO";
                string _isScan = _item.Mi_is_scansub == true ? "YES" : "NO";
                ddlScanSub.SelectedIndex = ddlScanSub.Items.IndexOf(ddlScanSub.Items.FindByValue(_isScan));
                string _isSubItm = _item.Mi_is_subitem == true ? "YES" : "NO";
                ddlIsSubItem.SelectedIndex = ddlIsSubItem.Items.IndexOf(ddlIsSubItem.Items.FindByValue(_isSubItm));
                chkTrim.Checked = _item.Mi_is_barcodetrim;
                txttrimLeft.Text = Convert.ToString(_item.Mi_ltrim_val);
                txttrimRight.Text = Convert.ToString(_item.Mi_rtrim_val);
                //_item.Mi_is_editshortdesc
                //_item.Mi_is_editlongdesc
                txtPrefix.Text = _item.Mi_ser_prefix;
                txtTaxStucture.Text = _item.Mi_anal1;
                txtSize.Text = _item.Mi_size;
                txtMainWaraUOM.Text = _item.Mi_uom_warrperiodmain;
                //_item.Mi_uom_warrperiodsub1
                //_item.Mi_uom_warrperiodsub2
                //_item.Mi_is_editser1
                //_item.Mi_is_editser2
                //_item.Mi_is_editser3
                //_item.Mi_std_cost
                //_item.Mi_std_price
                ddlActive.Text = _item.Mi_act == true ? "YES" : "NO";

                //_item.Mi_session_id
                //_item.Mi_anal1
                if (_item.Mi_anal2 == "V")
                {
                    rdvolume.Checked = true;
                }
                if (_item.Mi_anal2 == "W")
                {
                    rdweight.Checked = true;
                }

                txtwarehousvolum.Text = _item.Mi_capacity.ToString();
                //_item.Mi_anal3
                //_item.Mi_anal4
                //_item.Mi_anal5
                //_item.Mi_anal6
                //_item.Mi_is_subitm = chkIssub.Checked;
                //chkProRegis.Checked = _item.Mi_need_reg;
                //chkProInsurance.Checked = _item.Mi_need_insu;
                //ddlProInsurance.Text = _item.Mi_need_insu == true ? "YES" : "NO";

                //chkAlowHP.Checked = _item.Mi_hp_allow;
                //chkAlowInsu.Checked = _item.Mi_insu_allow;
                //chkAlowVehReg.Checked = _item.Mi_need_reg;
                //chkAlowVehInsu.Checked = _item.Mi_need_insu;

                chkFreeSer.Checked = _item.Mi_need_freesev == 1 ? true : false;
                //_item.Mi_comm_israte
                //_item.Mi_comm_val
                //_item.Mi_fac_base
                //_item.Mi_fac_val
                //_item.Mi_is_cond
                txtPackCode.Text = _item.Mi_packing_cd;
                //  txtPartNo.Text=   _item.Mi_part_cd ;
                //chkDiscont.Checked = _item.Mi_is_discont == 1 ? true : false;

                chkMaintSupp.Checked = _item.Mi_is_sup_wara == 1 ? true : false;
                ddlMaintSupp.SelectedValue = _item.Mi_is_sup_wara.ToString();

               // chkcust.Checked = _item.MI_CHK_CUST == 1 ? true : false;

                txtCapacity.Text = Convert.ToString(_item.Mi_capacity);
                //if(Convert.ToDateTime(_item.Mi_is_exp_dt).Date !=Convert.ToDateTime("01/Jan/0001 12:00:00 AM").Date )
                //{
                //    dtExpDate.Value = Convert.ToDateTime(_item.Mi_is_exp_dt).Date;
                //}


                chkIsExpired.Checked = _item.MI_IS_EXP_DT == 1 ? true : false; ;
          //      chkAdditem.Checked = _item.Mi_add_itm_des == true ? true : false;
                //chkAlterSer.Checked = _item.Mi_edit_alt_ser == true ? true : false;
             //   chkSerReq.Checked = _item.Mi_ser_rq_cus == true ? true : false;
                chkMaintSupp.Checked = _item.Mi_main_supp == true ? true : false;
                ddlMaintSupp.SelectedValue = _item.Mi_main_supp.ToString();
              //  chkAppCond.Checked = _item.Mi_app_itm_cond == true ? true : false;
             //   chkCounterfoil.Checked = _item.MI_COUNTER_FOIL == true ? true : false;
                string _isPg = _item.Mi_is_pgs == true ? "YES" : "NO";
                ddlbooktype.SelectedIndex = ddlbooktype.Items.IndexOf(ddlbooktype.Items.FindByValue(_isPg));
                //if (ddlbooktype.Text == "YES")
                //{
                //    lbtnbooktype.Visible = true;
                //}
                //else
                //{
                //    lbtnbooktype.Visible = false;
                //}



                string Mi_is_mult_prefix = _item.Mi_is_mult_prefix == true ? "YES" : "NO";
                ddlprefixallow.SelectedIndex = ddlprefixallow.Items.IndexOf(ddlprefixallow.Items.FindByValue(Mi_is_mult_prefix));
                if (ddlprefixallow.Text == "YES")
                {
                    pnlmultipleprefix.Visible = true;
                }
                else
                {
                    pnlmultipleprefix.Visible = false;
                }

                txtpagecount.Text = _item.Mi_pgd_count.ToString();


                //_lstfg_cost = new List<mst_itm_fg_cost>();
                _lstfg_cost = CHNLSVC.General.GetFinishGood(txtItem.Text.Trim().ToUpper());
              //  grdStatus.DataSource = null;
              //  grdStatus.DataSource = new List<mst_itm_fg_cost>();
             /////   grdStatus.DataSource = _lstfg_cost;
                //grdStatus.DataBind();
                ViewState["_lstfg_cost"] = _lstfg_cost;
                //  _lstreorder = new List<mst_itm_com_reorder>();
                //_lstreorder = CHNLSVC.MsgPortal.GetReorderDataWithItemCost(txtItem.Text);
                _lstreorder = CHNLSVC.General.GetReOrder(txtItem.Text.Trim().ToUpper());
                grdReorder.DataSource = null;
                grdReorder.DataSource = new List<mst_itm_com_reorder>();
                grdReorder.DataSource = _lstreorder;
                grdReorder.DataBind();
                ViewState["_lstreorder"] = _lstreorder;
                //  _lstcomItem = new List<mst_com_itm>();

                //List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                //oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                //if (_lstreorder != null)
                //{
                //    foreach (mst_itm_com_reorder itemSer in _lstreorder)
                //    {
                //        if (oItemStaus != null && oItemStaus.Count > 0)
                //        {
                //            itemSer.Icr_Status_Des = oItemStaus.Find(x => x.Mis_cd == itemSer.Icr_itm_sts).Mis_desc;
                //        }
                //    }
                //}
                //grdItemCost.DataSource = null;
                //grdItemCost.DataSource = new List<mst_itm_com_reorder>();
                //grdItemCost.DataSource = _lstreorder;
                //grdItemCost.DataBind();

                _lstcomItem = CHNLSVC.General.GetComItem(txtItem.Text.Trim().ToUpper());
                if (_lstcomItem != null)
                {
                    _lstcomItem.Where(w => w.Mci_act == true).ToList().ForEach(s => s.Mci_act_status = "YES");
                    _lstcomItem.Where(w => w.Mci_act == false).ToList().ForEach(s => s.Mci_act_status = "NO");
                    _lstcomItem.Where(w => w.Mci_isfoc == true).ToList().ForEach(s => s.Mci_isfoc_status = "YES");
                    _lstcomItem.Where(w => w.Mci_isfoc == false).ToList().ForEach(s => s.Mci_isfoc_status = "NO");
                }

                grdComItem.DataSource = null;
                grdComItem.AutoGenerateColumns = false;
                grdComItem.DataSource = new List<MasterCompanyItem>();
                grdComItem.DataSource = _lstcomItem;
                grdComItem.DataBind();

                ddlItemcompany.DataSource = _lstcomItem;
                ddlItemcompany.DataTextField = "Mci_com";
                ddlItemcompany.DataValueField = "Mci_com";
                ddlItemcompany.DataBind();
                ViewState["_lstcomItem"] = _lstcomItem;

                //    _lstcusItem = new List<BusEntityItem>();
                _lstcusItem = CHNLSVC.General.GetBuninessEntityItem(txtItem.Text.Trim().ToUpper(), "C");
                if (_lstcusItem != null)
                {
                    _lstcusItem.Where(w => w.MBII_ACT == 1).ToList().ForEach(s => s.MBII_ACT_status = "YES");
                    _lstcusItem.Where(w => w.MBII_ACT == 0).ToList().ForEach(s => s.MBII_ACT_status = "NO");
                }

                grdCustomer.DataSource = null;
                grdCustomer.DataSource = new List<BusEntityItem>();
                grdCustomer.DataSource = _lstcusItem;
                grdCustomer.DataBind();
                ViewState["_lstcusItem"] = _lstcusItem;
                // _lstsupItem = new List<BusEntityItem>();
                _lstsupItem = CHNLSVC.General.GetBuninessEntityItem(txtItem.Text.Trim().ToUpper(), "S");
                if (_lstsupItem != null)
                {
                    _lstsupItem.Where(w => w.MBII_ACT == 1).ToList().ForEach(s => s.MBII_ACT_status = "YES");
                    _lstsupItem.Where(w => w.MBII_ACT == 0).ToList().ForEach(s => s.MBII_ACT_status = "NO");
                }

                grdSupplier.DataSource = null;
                grdSupplier.DataSource = new List<BusEntityItem>();
                grdSupplier.DataSource = _lstsupItem;
                grdSupplier.DataBind();
                ViewState["_lstsupItem"] = _lstsupItem;
                // _lstredCom = new List<mst_itm_redeem_com>();
                _lstredCom = CHNLSVC.General.GetRedeem(txtItem.Text.Trim().ToUpper());
                if (_lstredCom != null)
                {
                    _lstredCom.Where(w => w.Red_active == 1).ToList().ForEach(s => s.Red_active_status = "YES");
                    _lstredCom.Where(w => w.Red_active == 0).ToList().ForEach(s => s.Red_active_status = "NO");
                }

                //grdRedeemCom.DataSource = null;
                //grdRedeemCom.DataSource = new List<mst_itm_redeem_com>();
                //grdRedeemCom.DataSource = _lstredCom;
                //grdRedeemCom.DataBind();
                ViewState["_lstredCom"] = _lstredCom;
                //     _lstmrn = new List<mst_itm_mrn_com>();
                _lstmrn = CHNLSVC.General.getItemMRN(txtItem.Text.Trim().ToUpper());
                if (_lstmrn != null)
                {
                    _lstmrn.Where(w => w.Imc_active == 1).ToList().ForEach(s => s.Imc_active_status = "YES");
                    _lstmrn.Where(w => w.Imc_active == 0).ToList().ForEach(s => s.Imc_active_status = "NO");
                }

            //    grdMRN.DataSource = null;
           //     grdMRN.DataSource = new List<mst_itm_mrn_com>();
            //    grdMRN.DataSource = _lstmrn;
                //grdMRN.DataBind();
                ViewState["_lstmrn"] = _lstmrn;
                //   _lstreplace = new List<mst_itm_replace>();
                _lstreplace = CHNLSVC.General.getReplaceItem(txtItem.Text.Trim().ToUpper());
                if (_lstreplace != null)
                {
                    _lstreplace.Where(w => w.Rpl_active == 1).ToList().ForEach(s => s.Rpl_active_status = "YES");
                    _lstreplace.Where(w => w.Rpl_active == 0).ToList().ForEach(s => s.Rpl_active_status = "NO");
                }

                //grdRepalced.DataSource = null;
                //grdRepalced.DataSource = new List<mst_itm_replace>();
                //grdRepalced.DataSource = _lstreplace;
                //grdRepalced.DataBind();
                ViewState["_lstreplace"] = _lstreplace;
                //   _lstkit = new List<MasterItemComponent>();
                _lstkit = CHNLSVC.General.getitemComponent(txtItem.Text.Trim().ToUpper());
                if (_lstkit != null)
                {
                    _lstkit.Where(w => w.Micp_act == true).ToList().ForEach(s => s.Micp_act_status = "YES");
                    _lstkit.Where(w => w.Micp_act == false).ToList().ForEach(s => s.Micp_act_status = "NO");
                }

                //grdKit.DataSource = new int[] { };
                //if (_lstkit != null)
                //{
                //    if (_lstkit.Count > 0)
                //    {
                //        grdKit.DataSource = _lstkit;
                //    }
                //}
                //grdKit.DataBind();
                ViewState["_lstkit"] = _lstkit;
                _lstkit = ViewState["_lstkit"] as List<MasterItemComponent>;
                //   _lsttaxClaim = new List<MasterItemTaxClaim>();
                _lsttaxClaim = CHNLSVC.General.getitemTaxClaim(txtItem.Text.Trim().ToUpper());
                grdTaxClaim.DataSource = null;
                grdTaxClaim.DataSource = new List<MasterItemTaxClaim>();
                grdTaxClaim.DataSource = _lsttaxClaim;
                grdTaxClaim.DataBind();
                ViewState["_lsttaxClaim"] = _lsttaxClaim;

                //   _lstitmPrd = new List<mst_itm_sevpd>();
                _lstitmPrd = CHNLSVC.General.getServiceSchedule(txtItem.Text.Trim().ToUpper());
                if (_lstitmPrd != null)
                {
                    _lstitmPrd.Where(w => w.Msp_isfree == 1).ToList().ForEach(s => s.Msp_isfree_status = "YES");
                    _lstitmPrd.Where(w => w.Msp_isfree == 0).ToList().ForEach(s => s.Msp_isfree_status = "NO");
                }

                grdservice.DataSource = null;
                grdservice.DataSource = new List<mst_itm_sevpd>();
                grdservice.DataSource = _lstitmPrd;
                grdservice.DataBind();
                ViewState["_lstitmPrd"] = _lstitmPrd;
                //  _lstitmWrd = new List<MasterItemWarrantyPeriod>();
                _lstitmWrd = CHNLSVC.General.getitemWarranty(txtItem.Text.Trim().ToUpper());
                if (_lstitmWrd != null)
                {
                    _lstitmWrd = _lstitmWrd.Where(c => c.Mwp_act == true).ToList();
                }
                grdWarranty.DataSource = null;
                grdWarranty.DataSource = new List<MasterItemWarrantyPeriod>();
                grdWarranty.DataSource = _lstitmWrd;
                grdWarranty.DataBind();
                ViewState["_lstitmWrd"] = _lstitmWrd;
                //  _lstpcWrd = new List<mst_itm_pc_warr>();
                _lstpcWrd = CHNLSVC.General.getPcWarrantNew(txtItem.Text.Trim().ToUpper());
                List<mst_itm_pc_warr> _newwarrlist = new List<mst_itm_pc_warr>();

                if (_lstpcWrd != null && _lstpcWrd.Count > 200)
                {
                    _newwarrlist = _lstpcWrd.Take(200).ToList();
                }
                else
                {
                    _newwarrlist = _lstpcWrd;
                }
                grdPcwarranty.DataSource = null;
                grdPcwarranty.DataSource = new List<mst_itm_pc_warr>();
                grdPcwarranty.DataSource = _newwarrlist;
                grdPcwarranty.DataBind();
                ViewState["_lstpcWrd"] = _lstpcWrd;

                //   _lstchannelWrd = new List<mst_itm_channlwara>();
                _lstchannelWrd = CHNLSVC.General.getChannelWarranty(txtItem.Text.Trim().ToUpper());
                grdcannelWara.DataSource = null;
                grdcannelWara.DataSource = new List<mst_itm_channlwara>();
                grdcannelWara.DataSource = _lstchannelWrd;
                grdcannelWara.DataBind();
                ViewState["_lstchannelWrd"] = _lstchannelWrd;
                //   _lstcont = new List<mst_itm_container>();
                _lstcont = CHNLSVC.General.getRContainerItem(txtItem.Text.Trim().ToUpper());
                //grdCont.DataSource = null;
        //        grdCont.DataSource = new List<mst_itm_container>();
          //      grdCont.DataSource = _lstcont;
          //      grdCont.DataBind();
                ViewState["_lstcont"] = _lstcont;


                //   _itemPrefix = new List<ItemPrefix>();
                _itemPrefix = CHNLSVC.General.GET_ITM_PREFIX(txtItem.Text.Trim().ToUpper());
                if (_itemPrefix != null)
                {
                    _itemPrefix.Where(w => w.MI_ACT == 1).ToList().ForEach(s => s.MI_ACTIVE_STATUS = "YES");
                    _itemPrefix.Where(w => w.MI_ACT == 0).ToList().ForEach(s => s.MI_ACTIVE_STATUS = "NO");
                }

                grdprefix.DataSource = null;
                grdprefix.DataSource = new List<ItemPrefix>();
                grdprefix.DataSource = _itemPrefix;
                grdprefix.DataBind();
                ViewState["_itemPrefix"] = _itemPrefix;

                //pnlsavechk.Visible = false;
            }
            else
            {
                DisplayMessage("Invalid code", 2);

            }

        }

        private void ddlvalus()
        {
            ddlWarranty.Items.Clear();
            ddlActive.Items.Clear();
            ddlSerilize.Items.Clear();
            ddlChassis.Items.Clear();
            ddlScanSub.Items.Clear();
            // ddlhpSalesAccept.Items.Clear();
            //ddlIsRegister.Items.Clear();
            ddlprefixallow.Items.Clear();
            ddlbooktype.Items.Clear();
            ddlprfix.Items.Clear();
            ddlStcokMain.Items.Clear();
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("NO", "NO"));
            items.Add(new ListItem("YES", "YES"));


            ddlWarranty.Items.AddRange(items.ToArray());
            ddlActive.Items.AddRange(items.ToArray());

            List<ListItem> _serItemList = new List<ListItem>();
            _serItemList.Add(new ListItem("NO", "NO"));
            _serItemList.Add(new ListItem("YES", "YES"));
            _serItemList.Add(new ListItem("DECIMAL", "DECIMAL"));
            ddlSerilize.Items.AddRange(_serItemList.ToArray());
            //ddlChassis.Items.AddRange(items.ToArray());
            ddlScanSub.Items.AddRange(items.ToArray());
            ddlStcokMain.Items.AddRange(items.ToArray());
            // ddlActive.SelectedValue ="YES";
            //List<ListItem> items2 = new List<ListItem>();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            while (ddlhpSalesAccept.Items.Count > 1)
            {
                ddlhpSalesAccept.Items.RemoveAt(1);
            }
            DataTable items2 = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);
            ddlhpSalesAccept.DataSource = items2;
            ddlhpSalesAccept.DataTextField = "srtp_desc";
            ddlhpSalesAccept.DataValueField = "srtp_cd";
            ddlhpSalesAccept.DataBind();
            //items2.Add(new ListItem("Hire Sales", "Hire Sales"));
            //items2.Add(new ListItem("Cash Sales", "Cash Sales"));
            //items2.Add(new ListItem("Credit Sales", "Credit Sales"));
            //ddlhpSalesAccept.Items.AddRange(items2.ToArray());

            //ddlProInsurance.Items.AddRange(items.ToArray());
            //ddlIsRegister.Items.AddRange(items.ToArray());
            ddlbooktype.Items.AddRange(items.ToArray());
            ddlprefixallow.Items.AddRange(items.ToArray());
            ddlprfix.Items.AddRange(items.ToArray());

            DataTable _UOM = CHNLSVC.CommonSearch.GET_UOM_CAT("SIZE");
            ddluomwidth.DataSource = _UOM;
            ddluomwidth.DataTextField = "msuc_cd";
            ddluomwidth.DataValueField = "msuc_cd";
            ddluomwidth.DataBind();
            ddluomwidth.Items.Insert(0, new ListItem("--Select-", ""));
            ddluomwidth.SelectedIndex = 0;
            ddlprefixallow.SelectedIndex = ddlprefixallow.Items.IndexOf(ddlprefixallow.Items.FindByValue("NO"));
            //DataTable _REplace = CHNLSVC.CommonSearch.GET_ITM_REPL_REASON();
            //ddlReplaceProduct.DataSource = _REplace;
            //ddlReplaceProduct.DataTextField = "rir_desc";
            //ddlReplaceProduct.DataValueField = "rir_tp";
            //ddlReplaceProduct.DataBind();

            DataTable _Ser2 = CHNLSVC.General.GET_REF_SER2();
            ddlChassis.DataSource = _Ser2;
            ddlChassis.DataTextField = "rs2_desc";
            ddlChassis.DataValueField = "rs2_id";
            ddlChassis.DataBind();
            ddlprefixallow.SelectedIndex = ddlprefixallow.Items.IndexOf(ddlprefixallow.Items.FindByValue("NO"));
            ddlbooktype.SelectedIndex = ddlbooktype.Items.IndexOf(ddlbooktype.Items.FindByValue("NO"));
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            if (Session["UserCompanyCode"] == null)
            {
                Response.Redirect("/Login.aspx");
            }
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.masterItem:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ProductionPlan:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat1:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat1.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat2:
                    {

                        paramsText.Append(txtMainCat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat3:
                    {
                        paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat3.ToString() + seperator + "" + seperator + "CAT_Sub2" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat4:
                    {
                        paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat4.ToString() + seperator + "CAT_Sub3" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat5:
                    {
                        paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + seperator + "CAT_Sub4" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {

                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(txtCuscom.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        paramsText.Append(txtSupCom.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterUOM:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterColor:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterTax:
                    {
                        paramsText.Append("");
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(txtChaCom.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + null + seperator + null + seperator + null + seperator + null + seperator + null + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
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

        private void CollectMaster()
        {
            _item.Mi_cd = txtItem.Text.Trim().ToUpper();
            _item.Mi_shortdesc = txtSdes.Text;
            _item.Mi_longdesc = txtLdes.Text;
            _item.Mi_cate_1 = txtMainCat.Text;
            _item.Mi_cate_2 = txtCat1.Text;
            _item.Mi_cate_3 = txtCat2.Text;
            _item.Mi_cate_4 = txtCat3.Text;
            //_item.Mi_cate_5 = txtCat4.Text;
            _item.Mi_brand = txtBrand.Text;
            _item.Mi_model = txtModel.Text;
            _item.Mi_part_no = txtPartNo.Text;
            _item.Mi_color_int = txtColorInt.Text;
            _item.Mi_is_reqcolorint = chkColorInt.Checked;
            _item.Mi_color_ext = txtColorExt.Text;
            _item.Mi_is_reqcolorext = chkColorExt.Checked;
            _item.Mi_itm_tp = ddlItemType.SelectedValue.ToString();
            // _item.Mi_is_stockmaintain = chkStcokMain.Checked == true ? "1" : "0";
            _item.Mi_is_stockmaintain = ddlStcokMain.SelectedItem.Text == "YES" ? "1" : "0";
            _item.Mi_hs_cd = ttxHsCode.Text;
            _item.Mi_itm_uom = txtUOM.Text;
            _item.Mi_dim_uom = txtduom.Text;
          //  _item.MI_COUNTER_FOIL = chkCounterfoil.Checked;
            if (string.IsNullOrEmpty(txtbreath.Text))
            {
                txtbreath.Text = "0";
            }
            _item.Mi_dim_length = Convert.ToDecimal(txtbreath.Text);
            if (string.IsNullOrEmpty(txtwidth.Text))
            {
                txtwidth.Text = "0";
            }
            _item.Mi_dim_width = Convert.ToDecimal(txtwidth.Text);
            if (string.IsNullOrEmpty(txthight.Text))
            {
                txthight.Text = "0";
            }
            _item.Mi_dim_height = Convert.ToDecimal(txthight.Text);
            _item.Mi_weight_uom = txtwuom.Text;
            if (string.IsNullOrEmpty(txtgross.Text))
            {
                txtgross.Text = "0";
            }
            _item.Mi_gross_weight = Convert.ToDecimal(txtgross.Text);
            if (string.IsNullOrEmpty(txtnet.Text))
            {
                txtnet.Text = "0";
            }
            _item.Mi_net_weight = Convert.ToDecimal(txtnet.Text);

            //if (string.IsNullOrEmpty(txtAmount.Text))
            //{
            //    txtAmount.Text = "0";
            //}

            if (string.IsNullOrEmpty(txttrimLeft.Text))
            {
                txttrimLeft.Text = "0";
            }

            if (string.IsNullOrEmpty(txttrimRight.Text))
            {
                txttrimRight.Text = "0";
            }


            //_item.Mi_image_path = txtImagePath.Text;
            _item.Mi_is_ser1 = ddlSerilize.SelectedItem.Text == "YES" ? 1 : ddlSerilize.SelectedItem.Text == "DECIMAL" ? -1 : 0;
            _item.Mi_is_ser2 = Convert.ToInt32(ddlChassis.SelectedValue);
            //_item.Mi_is_ser3
            _item.Mi_warr = ddlWarranty.SelectedItem.Text == "YES" ? true : false;
            _item.Mi_warr_print = chkwarrprint.Checked;
            //_item.Mi_hp_allow = chkAlowHP.Checked;
            //_item.Mi_insu_allow = chkAlowInsu.Checked;
            //_item.Mi_need_reg = chkAlowVehReg.Checked;
            //_item.Mi_need_insu = chkAlowVehInsu.Checked;
            //_item.Mi_hp_allow = ddlhpSalesAccept.SelectedItem.Text == "YES" ? true : false;
            //_item.Mi_insu_allow = ddlIsRegister.SelectedItem.Text == "YES" ? true : false;
            //_item.Mi_country_cd = txtCountry.Text;
            _item.Mi_purcom_cd = txtPurComp.Text;
            _item.Mi_itm_stus = ddlStatus.SelectedValue.ToString();
           // _item.Mi_fgitm_cd = txtFgood.Text;
           // _item.Mi_itmtot_cost = Convert.ToInt32(txtAmount.Text);
            _item.Mi_chg_tp = ddlPayType.SelectedItem.ToString();
            _item.Mi_is_scansub = ddlScanSub.SelectedValue == "NO" ? false : true;
            _item.Mi_is_subitem = ddlIsSubItem.SelectedValue == "NO" ? false : true;
            _item.Mi_is_barcodetrim = chkTrim.Checked;
            _item.Mi_ltrim_val = Convert.ToInt32(txttrimLeft.Text);
            _item.Mi_rtrim_val = Convert.ToInt32(txttrimRight.Text);
            //_item.Mi_is_editshortdesc
            //_item.Mi_is_editlongdesc
            _item.Mi_ser_prefix = txtPrefix.Text;
            _item.Mi_refitm_cd = "N/A";
            _item.Mi_uom_warrperiodmain = txtMainWaraUOM.Text;
            _item.Mi_uom_warrperiodsub1 = "N/A";
            _item.Mi_uom_warrperiodsub2 = "N/A";
            //_item.Mi_is_editser1
            //_item.Mi_is_editser2
            //_item.Mi_is_editser3
            //_item.Mi_std_cost
            //_item.Mi_std_price
            _item.Mi_act = ddlActive.SelectedItem.Text == "YES" ? true : false;
            _item.Mi_cre_by = Session["UserID"].ToString();
            _item.Mi_cre_dt = DateTime.Now;
            _item.Mi_mod_by = Session["UserID"].ToString();
            _item.Mi_mod_dt = DateTime.Now;
            _item.Mi_session_id = Session["SessionID"].ToString();
            _item.Mi_anal1 = txtTaxStucture.Text;
            _item.Mi_size = txtSize.Text;
            if (rdvolume.Checked == true)
            {
                _item.Mi_anal2 = "V";
            }
            else if (rdweight.Checked == true) { _item.Mi_anal2 = "W"; }
            if (txtwarehousvolum.Text != "")
            {
                _item.Mi_capacity = Convert.ToDecimal(txtwarehousvolum.Text);
            }
            else
            {
                _item.Mi_capacity = 0;
            }
            // _item.Mi_capacity =Convert.ToDecimal(txtwarehousvolum.Text);
            // _item.Mi_anal4 = txtTaxStucture.Text;
            //_item.Mi_anal5
            //_item.Mi_anal6
            //_item.Mi_is_subitm = chkIssub.Checked;
            //_item.Mi_need_reg = chkProRegis.Checked;
            //_item.Mi_need_insu = ddlProInsurance.SelectedItem.Text == "YES" ? true : false;//chkProInsurance.Checked;
            _item.Mi_need_freesev = chkFreeSer.Checked == true ? 1 : 0;
            //_item.Mi_comm_israte
            //_item.Mi_comm_val
            //_item.Mi_fac_base
            //_item.Mi_fac_val
            //_item.Mi_is_cond
            _item.Mi_packing_cd = txtPackCode.Text;
            _item.Mi_part_cd = txtPartNo.Text;
           // _item.Mi_is_discont = chkDiscont.Checked == true ? 1 : 0;

            _item.Mi_is_sup_wara = chkMaintSupp.Checked == true ? 1 : 0;
            _item.Mi_is_sup_wara = ddlMaintSupp.SelectedValue == "0" ? 0 : 1;

           // _item.MI_CHK_CUST = chkcust.Checked == true ? 1 : 0;
            _item.MI_IS_EXP_DT = chkIsExpired.Checked == true ? 1 : 0;
            // _item.Mi_capacity = Convert.ToDecimal(txtCapacity.Text);
            //_item.Mi_add_itm_des = chkAdditem.Checked == true ? true : false;
            //_item.Mi_is_editshortdesc = chkAdditem.Checked == true ? true : false;
            //_item.Mi_is_editlongdesc = chkAdditem.Checked == true ? true : false;
            //_item.Mi_edit_alt_ser = chkAlterSer.Checked == true ? true : false;
           // _item.Mi_ser_rq_cus = chkSerReq.Checked == true ? true : false;
            //  _item.Mi_main_supp = chkMaintSupp.Checked == true ? true : false;
          //  _item.Mi_app_itm_cond = chkAppCond.Checked == true ? true : false;
            _item.Mi_is_pgs = ddlbooktype.SelectedItem.Text == "YES" ? true : false;
            _item.Mi_is_mult_prefix = ddlprefixallow.SelectedItem.Text == "YES" ? true : false;
            if (string.IsNullOrEmpty(txtpagecount.Text))
            {
                txtpagecount.Text = "0";
            }
            _item.Mi_pgd_count = Convert.ToInt32(txtpagecount.Text);

        }

        #region Basic Item Details
        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            grdItemCost.DataSource = new int[] { };
            grdItemCost.DataBind();
            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                if (!validateinputString(txtItem.Text))
                {
                    DisplayMessage("Invalid charactor found in item code.", 2);
                    txtItem.Focus();
                    return;
                }
                if (chkSave.Checked == false)
                {
                    Load_items();
                    DataTable _dtResults = CHNLSVC.General.GetItemAvailability(txtItem.Text.Trim().ToUpper(), Session["UserCompanyCode"].ToString());
                    if (Convert.ToInt32(_dtResults.Rows[0]["AVACOUNT"].ToString()) != 0)
                    {
                        ddlSerilize.Enabled = false;
                    }
                    else
                    {
                        ddlSerilize.Enabled = true;
                    }
                }
                else
                {
                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(txtItem.Text.Trim().ToUpper());
                    if (_mstItm != null)
                    {
                        Load_items();
                        DataTable _dtResults = CHNLSVC.General.GetItemAvailability(txtItem.Text.Trim().ToUpper(), Session["UserCompanyCode"].ToString());
                        if (Convert.ToInt32(_dtResults.Rows[0]["AVACOUNT"].ToString()) != 0)
                        {
                            ddlSerilize.Enabled = false;
                        }
                        else
                        {
                            ddlSerilize.Enabled = true;
                        }
                    }
                }
            }
        }

        protected void lbtnSrchCode_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterItem";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }


        }

        protected void txtBrand_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtBrand.Text))
            {
                return;
            }
            if (!validateinputStringWithSpace(txtBrand.Text))
            {
                DisplayMessage("Invalid charactor found in brand code.", 2);
                txtBrand.Focus();
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtBrand.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid brand", 2);
                txtBrand.Text = string.Empty;
                txtBrand.Focus();
                return;
            }
        }

        protected void lbtnSrchBrand_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "ItemBrand";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtModel.Text))
            {
                return;
            }
            if (!validateinputStringWithHash(txtModel.Text))
            {
                DisplayMessage("Invalid charactor found in model code.", 2);
                txtModel.Focus();
                return;
            }
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, "Code", txtModel.Text);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtModel.Text).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid model", 2);
                txtModel.Text = string.Empty;
                txtModel.Focus();
                txtTaxStucture.Text = string.Empty;
                return;
            }
            else
            {
                string modelCode = txtModel.Text.Trim().ToUpper();

                List<MasterItemModel> _lstmodeltem = new List<MasterItemModel>();
                List<MasterItemModel> _lstmodel = new List<MasterItemModel>();
                _lstmodeltem = CHNLSVC.General.GetItemModel(modelCode);
                if (_lstmodeltem != null)
                {
                    _lstmodel = _lstmodeltem.Where(x => x.Mm_cd == txtModel.Text).ToList();
                    if (_lstmodel[0].Mm_is_dis == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + "Discontinue model!!" + "');", true);
                    }
                    txtMainCat.Text = _lstmodel[0].Mm_cat1;
                    txtCat1.Text = _lstmodel[0].Mm_cat2;
                    txtCat2.Text = _lstmodel[0].Mm_cat3;
                    txtCat3.Text = _lstmodel[0].Mm_cat4;
                   // txtCat4.Text = _lstmodel[0].Mm_cat5;
                    txtTaxStucture.Text = _lstmodel[0].Mm_taxstruc_cd;
                    txtTaxStucture_TextChanged(null, null);
                    txtBrand.Text = _lstmodel[0].Mm_brand;
                    txtBrand_TextChanged(null, null);
                    txtUOM.Text = _lstmodel[0].Mm_uom;
                    txtUOM_TextChanged(null, null);
                }

            }
            IsDiscontinuedModel();
        }

        protected void lbtnSrchModel_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "ModelMaster";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }

        }

        protected void txtUOM_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUOM.Text))
            {
                return;
            }
            if (!validateinputString(txtUOM.Text))
            {
                DisplayMessage("Invalid charactor found in UOM code.", 2);
                txtUOM.Focus();
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtUOM.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid UOM", 2);
                txtUOM.Text = string.Empty;
                txtUOM.Focus();
                return;
            }
            txtWaraUOM.Text = txtUOM.Text.Trim();
            txtMainWaraUOM.Text = txtUOM.Text.Trim();
        }

        protected void lbtnsrhBaseUOM_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterUOM";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnSrch_mainCat_Click(object sender, EventArgs e)
        {

            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat1";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtMainCat_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMainCat.Text))
            {
                return;
            }
            if (!validateinputString(txtMainCat.Text))
            {
                DisplayMessage("Invalid charactor found in Main Category code.", 2);
                txtMainCat.Focus();
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtMainCat.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtMainCat.Text = string.Empty;
                txtMainCat.Focus();
                return;
            }

        }

        protected void txtCat1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat1.Text))
            {
                return;
            }
            if (!validateinputString(txtCat1.Text))
            {
                DisplayMessage("Invalid charactor found in Sub Level 1 code.", 2);
                txtCat1.Focus();
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat1.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtCat1.Text = string.Empty;
                txtCat1.Focus();
                return;
            }
        }

        protected void lbtnSrch_cat1_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat2";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCat2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat2.Text))
            {
                return;
            }
            if (!validateinputString(txtCat2.Text))
            {
                DisplayMessage("Invalid charactor found in Sub Level 2 code.", 2);
                txtCat2.Focus();
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat2.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtCat2.Text = string.Empty;
                txtCat2.Focus();
                return;
            }
        }

        protected void lbtnSrch_cat2_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat3";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCat3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat3.Text))
            {
                return;
            }
            if (!validateinputString(txtCat3.Text))
            {
                DisplayMessage("Invalid charactor found in Sub Level 3 code.", 2);
                txtCat3.Focus();
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat3.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtCat3.Text = string.Empty;
                txtCat3.Focus();
                return;
            }
        }

        protected void lbtnSrch_cat3_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat4";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCat4_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtCat4.Text))
            //{
            //    return;
            //}
            //if (!validateinputString(txtCat4.Text))
            //{
            //    DisplayMessage("Invalid charactor found in Sub Level 4 code.", 2);
            //    txtCat4.Focus();
            //    return;
            //}
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            //var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat4.Text.Trim()).ToList();
            //if (_validate == null || _validate.Count <= 0)
            //{
            //    DisplayMessage("Invalid category", 2);
            //    //txtCat4.Text = string.Empty;
            //    //txtCat4.Focus();
            //    return;
            //}
        }

        protected void lbtnSrch_cat4_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat5";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtTaxStucture_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaxStucture.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtTaxStucture.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid structure", 2);
                txtTaxStucture.Text = string.Empty;
                txtTaxStucture.Focus();
                return;
            }
        }

        protected void lbtnsrcTax_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, "COMPANY", Session["UserCompanyCode"].ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterTax";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtColorExt_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtColorExt.Text))
            {
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtColorExt.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid color", 2);
                txtColorExt.Text = string.Empty;
                txtColorExt.Focus();
                return;
            }
        }

        protected void lbtnshcextcolor_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterColor";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtColorInt_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtColorInt.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtColorInt.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid color", 2);
                txtColorInt.Text = string.Empty;
                txtColorInt.Focus();
                return;
            }
        }

        protected void lbtnsrhintcolor_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterColor2";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtPurComp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPurComp.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtPurComp.Text.Trim());
                    if (com == null)
                    {
                        DisplayMessage("Invalid company", 2);
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

        protected void lbtnSrhPurCom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCountry_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtCountry.Text))
            //{
            //    return;
            //}

            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            //DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);

            //var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCountry.Text.Trim()).ToList();
            //if (_validate == null || _validate.Count <= 0)
            //{
            //    DisplayMessage("Invalid country", 2);
            //    txtCountry.Text = string.Empty;
            //    txtCountry.Focus();
            //    return;
            //}
        }

        protected void lbtnSrchCounty_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterContry);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterContry";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void txtFgood_TextChanged(object sender, EventArgs e)
        //{
        //    MasterItem _itemdetail = null;
        //    if (!string.IsNullOrEmpty(txtFgood.Text))
        //    {

        //        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtFgood.Text);
        //        if (_itemdetail == null)
        //        {
        //            DisplayMessage("Please check the item code", 2);
        //            txtFgood.Text = string.Empty;
        //            txtFgood.Focus();
        //            return;
        //        }

        //        if (_itemdetail.Mi_itm_tp == "R")
        //        {
        //            DisplayMessage("Please check the item code", 2);
        //            txtFgood.Text = string.Empty;
        //            txtFgood.Focus();
        //            return;
        //        }
        //        if (!string.IsNullOrEmpty(txtFgood.Text))
        //        {
        //            if (txtFgood.Text == txtItem.Text.Trim().ToUpper())
        //            {
        //                DisplayMessage("Finish good item and can not be same as item code", 2);
        //                //txtrepItem.Text=string.Empty;
        //                //txtrepItem.Focus();
        //                return;
        //            }
        //        }
        //    }
        //}

        protected void lbtnSearchFG_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "InvoiceItemUnAssableByModel";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void lbtnAddstatus_Click(object sender, EventArgs e)
        //{
        //    _lstfg_cost = ViewState["_lstfg_cost"] as List<mst_itm_fg_cost>;
        //    if (string.IsNullOrEmpty(txtFgood.Text))
        //    {
        //        DisplayMessage("Enter finish good", 2);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(txtCostElement.Text))
        //    {
        //        DisplayMessage("Enter cost elemet", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtAmount.Text))
        //    {
        //        DisplayMessage("Enter cost amount", 2);
        //        return;
        //    }
        //    if (_lstfg_cost != null)
        //    {
        //        mst_itm_fg_cost result = _lstfg_cost.Find(x => x.Ifc_fg_item_code == txtFgood.Text);
        //        if (result != null)
        //        {
        //            DisplayMessage("This finish good already exist ", 2);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        _lstfg_cost = new List<mst_itm_fg_cost>();
        //    }
        //    mst_itm_fg_cost _itm = new mst_itm_fg_cost();
        //    _itm.Ifc_item_code = txtItem.Text.Trim().ToUpper();
        //    _itm.Ifc_fg_item_code = txtFgood.Text;
        //    _itm.Ifc_cost_type = txtCostElement.Text;
        //    _itm.Ifc_cost_amount = Convert.ToDecimal(txtAmount.Text);
        //    _itm.Ifc_currency_code = "LKR";
        //    _itm.Ifc_create_by = Session["UserID"].ToString();
        //    _itm.Ifc_last_modify_by = Session["UserID"].ToString();
        //    _lstfg_cost.Add(_itm);

        //    grdStatus.DataSource = null;
        //    grdStatus.DataSource = new List<mst_itm_fg_cost>();
        //    grdStatus.DataSource = _lstfg_cost;
        //    grdStatus.DataBind();
        //    ViewState["_lstfg_cost"] = _lstfg_cost;

        //    txtFgood.Text = "";
        //    txtCostElement.Text = "";
        //    txtAmount.Text = "";

        //}

        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCompany.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtCompany.Text.Trim());
                    if (com == null)
                    {
                        DisplayMessage("Invalid company", 2);
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

        protected void lbtnSearchreCom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company2";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddReorder_Click(object sender, EventArgs e)
        {
            _lstreorder = ViewState["_lstreorder"] as List<mst_itm_com_reorder>;
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                DisplayMessage("Enter company", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtRoQty.Text))
            {
                DisplayMessage("Enter reorder qty", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtsaftystock.Text))
            {
                DisplayMessage("Enter safty stock", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtRoLevel.Text))
            {
                DisplayMessage("Enter reorder level", 2);
                return;
            }

            if (_lstreorder != null)
            {
                mst_itm_com_reorder result = _lstreorder.Find(x => x.Icr_com_code == txtCompany.Text);
                if (result != null)
                {
                    DisplayMessage("This company already exist", 2);
                    return;
                }

            }

            else
            {
                _lstreorder = new List<mst_itm_com_reorder>();
            }

            mst_itm_com_reorder _itm = new mst_itm_com_reorder();
            _itm.Icr_com_code = txtCompany.Text;
            _itm.Icr_itm_code = txtItem.Text.Trim().ToUpper();
            _itm.Icr_itm_sts = "GOD";
            _itm.Icr_re_order_qty = Convert.ToDecimal(txtRoQty.Text);
            _itm.Icr_re_order_lvl = Convert.ToDecimal(txtRoLevel.Text);
            _itm.Icr_class = ddlClassification.SelectedItem.Text;
            _itm.Icr_safety_qty = Convert.ToDecimal(txtsaftystock.Text);
            _itm.Icr_created_by = Session["UserID"].ToString();
            _itm.Icr_last_modify_by = Session["UserID"].ToString();

            _lstreorder.Add(_itm);

            grdReorder.DataSource = null;
            grdReorder.DataSource = new List<mst_itm_com_reorder>();
            grdReorder.DataSource = _lstreorder;
            grdReorder.DataBind();
            ViewState["_lstreorder"] = _lstreorder;
            txtCompany.Text = "";
            txtsaftystock.Text = "";
            txtRoQty.Text = "";
            txtRoLevel.Text = "";
            ddlClassification.SelectedIndex = -1;

        }


        #endregion

        #region AssignItem
        protected void txtItemCom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtItemCom.Text.Trim().ToUpper());
                    if (com == null)
                    {

                        DisplayMessage("Invalid Company", 2);
                        return;
                    }
                    else
                    {
                        txtitemDes.Text = com.Mc_desc;
                    }

                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnSearchitemCom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company3";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnsearchComItemAdd_Click(object sender, EventArgs e)
        {
            _lstcomItem = ViewState["_lstcomItem"] as List<MasterCompanyItem>;


            if (string.IsNullOrEmpty(ddlitemStatus.SelectedItem.Text))
            {
                DisplayMessage("Select item status", 2);
                return;
            }

            if (string.IsNullOrEmpty(ddlFoc.SelectedItem.Text))
            {
                DisplayMessage("Select FOC allow or not", 2);
                return;
            }
            if (Session["Multiplecom"] == "true")
            {
                foreach (ListItem Item in chklstbox.Items)
                {
                    if (Item.Selected == true)
                    {
                        if (_lstcomItem != null)
                        {
                            MasterCompanyItem result = _lstcomItem.Find(x => x.Mci_com == Item.Text);
                            if (result != null)
                            {
                                DisplayMessage("This company already exist", 2);
                                return;
                            }

                        }
                        else { _lstcomItem = new List<MasterCompanyItem>(); }
                        MasterCompanyItem _itmMul = new MasterCompanyItem();

                        _itmMul.Mci_itm_cd = txtItem.Text.Trim().ToUpper();
                        _itmMul.Mci_com = Item.Text;
                        _itmMul.Msi_restric_inv_tp = ddlhpSalesAccept.SelectedValue;
                        if (ddlitemStatus.SelectedItem.Text == "YES")
                        {
                            _itmMul.Mci_act = true;
                            _itmMul.Mci_act_status = "YES";
                        }
                        else
                        {
                            _itmMul.Mci_act = false;
                            _itmMul.Mci_act_status = "NO";
                        }
                        if (ddlFoc.SelectedItem.Text == "Allow")
                        {
                            _itmMul.Mci_isfoc = true;
                            _itmMul.Mci_isfoc_status = "YES";
                        }
                        else
                        {
                            _itmMul.Mci_isfoc = false;
                            _itmMul.Mci_isfoc_status = "NO";
                        }

                        _itmMul.Mci_age_type = ddlAgecType.SelectedItem.Text;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                        DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, "Code", Item.Value);
                        if (_result.Rows.Count > 0)
                        {
                            _itmMul.Mci_comDes = _result.Rows[0][1].ToString();
                        }



                        _lstcomItem.Add(_itmMul);

                        grdComItem.DataSource = null;
                        grdComItem.DataSource = new List<MasterCompanyItem>();
                        grdComItem.DataSource = _lstcomItem;
                        grdComItem.DataBind();
                        ViewState["_lstcomItem"] = _lstcomItem;




                    }
                }
                txtItemCom.Text = "";
                ddlitemStatus.SelectedIndex = -1;
                ddlFoc.SelectedIndex = -1;
                ddlAgecType.SelectedIndex = -1;
                txtitemDes.Text = "";
                Session["Multiplecom"] = "";
                return;
            }
            //if (string.IsNullOrEmpty(ddlAgecType.SelectedItem.Text))
            //{
            //    DisplayMessage("Select agency type", 2);
            //    return;
            //}
            if (string.IsNullOrEmpty(txtItemCom.Text))
            {
                DisplayMessage("Enter reorder level", 2);
                return;
            }
            if (_lstcomItem != null)
            {
                MasterCompanyItem result = _lstcomItem.Find(x => x.Mci_com == txtItemCom.Text);
                if (result != null)
                {
                    if (ddlitemStatus.SelectedItem.Text == "YES")
                    {
                        result.Mci_act = true;
                        result.Mci_act_status = "YES";
                    }
                    else
                    {
                        result.Mci_act = false;
                        result.Mci_act_status = "NO";
                    }
                    result.Msi_restric_inv_tp = ddlhpSalesAccept.SelectedValue;
                    if (ddlitemStatus.SelectedItem.Text == "YES")
                    {
                        result.Mci_act = true;
                        result.Mci_act_status = "YES";
                    }
                    else
                    {
                        result.Mci_act = false;
                        result.Mci_act_status = "NO";
                    }
                    if (ddlFoc.SelectedItem.Text == "Allow")
                    {
                        result.Mci_isfoc = true;
                        result.Mci_isfoc_status = "YES";
                    }
                    else
                    {
                        result.Mci_isfoc = false;
                        result.Mci_isfoc_status = "NO";
                    }

                    result.Mci_age_type = ddlAgecType.SelectedItem.Text;
                    result.Mci_comDes = txtitemDes.Text;

                    //DisplayMessage("This company already exist", 2);
                    //return;
                }
                else
                {
                    MasterCompanyItem _itm = new MasterCompanyItem();

                    _itm.Mci_itm_cd = txtItem.Text.Trim().ToUpper();
                    _itm.Mci_com = txtItemCom.Text;
                    _itm.Msi_restric_inv_tp = ddlhpSalesAccept.SelectedValue;
                    if (ddlitemStatus.SelectedItem.Text == "YES")
                    {
                        _itm.Mci_act = true;
                        _itm.Mci_act_status = "YES";
                    }
                    else
                    {
                        _itm.Mci_act = false;
                        _itm.Mci_act_status = "NO";
                    }
                    if (ddlFoc.SelectedItem.Text == "Allow")
                    {
                        _itm.Mci_isfoc = true;
                        _itm.Mci_isfoc_status = "YES";
                    }
                    else
                    {
                        _itm.Mci_isfoc = false;
                        _itm.Mci_isfoc_status = "NO";
                    }

                    _itm.Mci_age_type = ddlAgecType.SelectedItem.Text;
                    _itm.Mci_comDes = txtitemDes.Text;


                    _lstcomItem.Add(_itm);
                }
            }
            else
            {
                _lstcomItem = new List<MasterCompanyItem>();
                MasterCompanyItem _itm = new MasterCompanyItem();

                _itm.Mci_itm_cd = txtItem.Text.Trim().ToUpper();
                _itm.Mci_com = txtItemCom.Text;
                _itm.Msi_restric_inv_tp = ddlhpSalesAccept.SelectedValue;
                if (ddlitemStatus.SelectedItem.Text == "YES")
                {
                    _itm.Mci_act = true;
                    _itm.Mci_act_status = "YES";
                }
                else
                {
                    _itm.Mci_act = false;
                    _itm.Mci_act_status = "NO";
                }
                if (ddlFoc.SelectedItem.Text == "Allow")
                {
                    _itm.Mci_isfoc = true;
                    _itm.Mci_isfoc_status = "YES";
                }
                else
                {
                    _itm.Mci_isfoc = false;
                    _itm.Mci_isfoc_status = "NO";
                }

                _itm.Mci_age_type = ddlAgecType.SelectedItem.Text;
                _itm.Mci_comDes = txtitemDes.Text;


                _lstcomItem.Add(_itm);
            }


            grdComItem.DataSource = null;
            grdComItem.DataSource = new List<MasterCompanyItem>();
            grdComItem.DataSource = _lstcomItem;
            grdComItem.DataBind();
            ViewState["_lstcomItem"] = _lstcomItem;

            txtItemCom.Text = "";
            ddlitemStatus.SelectedIndex = -1;
            ddlFoc.SelectedIndex = -1;
            ddlAgecType.SelectedIndex = -1;
            txtitemDes.Text = "";


        }

        protected void txtReCom_TextChanged(object sender, EventArgs e)
        {
          //  try
            //{
            //    if (txtReCom.Text.Trim() != "")
            //    {
            //        //TODO: LOAD COMPANY DESCRIPTION
            //        MasterCompany com = CHNLSVC.General.GetCompByCode(txtReCom.Text.Trim());
            //        if (com == null)
            //        {
            //            DisplayMessage("Invalid company", 2);
            //            return;
            //        }
            //        else
            //        {
            //            txtReDes.Text = com.Mc_desc;
            //        }

            //    }
          //  }
            //catch (Exception ex)
            //{
            //    string _Msg = "Error Occurred while processing..!";
            //    DisplayMessage(_Msg, 4);
            //}
        }

        protected void lbtnSearchRedeem_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company4";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void lbtnsearchRedeemCom_Click(object sender, EventArgs e)
        //{
        //    _lstredCom = ViewState["_lstredCom"] as List<mst_itm_redeem_com>;

        //    if (string.IsNullOrEmpty(ddlReStatus.SelectedItem.Text))
        //    {
        //        DisplayMessage("Select status", 2);
        //        return;
        //    }

        //    if (Session["Multiplecom"] == "true")
        //    {
        //        foreach (ListItem Item in chklstbox.Items)
        //        {
        //            if (Item.Selected == true)
        //            {
        //                if (_lstredCom != null)
        //                {
        //                    mst_itm_redeem_com result = _lstredCom.Find(x => x.Red_com_code == Item.Text);
        //                    if (result != null)
        //                    {
        //                        DisplayMessage("This company already exist", 2);
        //                        return;
        //                    }
        //                }
        //                else
        //                {
        //                    _lstredCom = new List<mst_itm_redeem_com>();
        //                }

        //                mst_itm_redeem_com _itmMul = new mst_itm_redeem_com();
        //                _itmMul.Red_item_code = txtItem.Text.Trim().ToUpper();

        //                _itmMul.Red_com_code = Item.Text;
        //                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
        //                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, "Code", Item.Value);
        //                if (_result.Rows.Count > 0)
        //                {
        //                    _itmMul.Red_com_des = _result.Rows[0][1].ToString();
        //                }
        //                // _itm.Red_com_des = Item.Text;

        //              //  _itmMul.Red_active = ddlReStatus.SelectedItem.Text == "YES" ? 1 : 0;
        //              //  _itmMul.Red_active_status = ddlReStatus.SelectedItem.Text;
        //               // _itmMul.Red_create_by = Session["UserID"].ToString();

        //                _lstredCom.Add(_itmMul);

        //             //   grdRedeemCom.DataSource = null;

        //             //   grdRedeemCom.DataSource = new List<mst_itm_redeem_com>();
        //             //   grdRedeemCom.DataSource = _lstredCom;
        //            ///    grdRedeemCom.DataBind();
        //                ViewState["_lstredCom"] = _lstredCom;



        //            }
        //        }
        //      ///  txtReCom.Text = "";
        //       // txtReDes.Text = "";
        //      //  ddlReStatus.SelectedIndex = -1;
        //        Session["Multiplecom"] = "";
        //        return;
        //    }


        //    //if (string.IsNullOrEmpty(txtReCom.Text))
        //    //{
        //    //    DisplayMessage("Enter company", 2);
        //    //    return;
        //    //}


        //    //if (_lstredCom != null)
        //    //{
        //    //    mst_itm_redeem_com result = _lstredCom.Find(x => x.Red_com_code == txtReCom.Text);
        //    //    if (result != null)
        //    //    {
        //    //        result.Red_active = ddlReStatus.SelectedItem.Text == "YES" ? 1 : 0;
        //    //        result.Red_active_status = ddlReStatus.SelectedItem.Text;
        //    //        result.Red_create_by = Session["UserID"].ToString();
        //    //    }
        //    //    else
        //    //    {
        //    //        mst_itm_redeem_com _itm = new mst_itm_redeem_com();
        //    //        _itm.Red_item_code = txtItem.Text.Trim().ToUpper();
        //    //        _itm.Red_com_code = txtReCom.Text;
        //    //        _itm.Red_com_des = txtReDes.Text;
        //    //        _itm.Red_active = ddlReStatus.SelectedItem.Text == "YES" ? 1 : 0;
        //    //        _itm.Red_active_status = ddlReStatus.SelectedItem.Text;
        //    //        _itm.Red_create_by = Session["UserID"].ToString();

        //    //        _lstredCom.Add(_itm);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    _lstredCom = new List<mst_itm_redeem_com>();
        //    //    mst_itm_redeem_com _itm = new mst_itm_redeem_com();
        //    //    _itm.Red_item_code = txtItem.Text.Trim().ToUpper();
        //    //    _itm.Red_com_code = txtReCom.Text;
        //    //    _itm.Red_com_des = txtReDes.Text;
        //    //    _itm.Red_active = ddlReStatus.SelectedItem.Text == "YES" ? 1 : 0;
        //    //    _itm.Red_active_status = ddlReStatus.SelectedItem.Text;
        //    //    _itm.Red_create_by = Session["UserID"].ToString();

        //    //    _lstredCom.Add(_itm);
        //    //}



        //    //grdRedeemCom.DataSource = null;

        //    //grdRedeemCom.DataSource = new List<mst_itm_redeem_com>();
        //    //grdRedeemCom.DataSource = _lstredCom;
        //    //grdRedeemCom.DataBind();
        //    //ViewState["_lstredCom"] = _lstredCom;


        //    //txtReCom.Text = "";
        //    //txtReDes.Text = "";
        //    //ddlReStatus.SelectedIndex = -1;
        //}

        protected void txtCuscom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCuscom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtCuscom.Text.Trim());
                    if (com == null)
                    {
                        DisplayMessage("Invalid company", 2);
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

        protected void lbtnSearchCusitem_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company3";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCust_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCust.Text))
            {
                MasterBusinessEntity _masterBusinessCompany = null;
                _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtCust.Text))
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCust.Text, null, null, null, null, txtCuscom.Text);

                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        DisplayMessage("This customer already inactive. Please contact IT dept", 2);
                        return;
                    }

                    else
                    {
                        txtCustName.Text = _masterBusinessCompany.Mbe_name;
                    }
                }
                else
                {
                    DisplayMessage("Invalid customer", 2);

                    return;
                }
            }

        }

        protected void lbtnCusSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Customer";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnsearchCustomer_Click(object sender, EventArgs e)
        {
            _lstcusItem = ViewState["_lstcusItem"] as List<BusEntityItem>;
            if (string.IsNullOrEmpty(txtCuscom.Text))
            {
                DisplayMessage("Enter customer company", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtCust.Text))
            {
                DisplayMessage("Enter customer", 2);
                return;
            }
            if (string.IsNullOrEmpty(ddlCustStatus.Text))
            {
                DisplayMessage("Select status", 2);
                return;
            }


            if (_lstcusItem != null)
            {
                BusEntityItem result = _lstcusItem.Find(x => x.MBII_COM == txtCuscom.Text && x.MBII_CD == txtCust.Text);
                if (result != null)
                {
                    DisplayMessage("This customer already exist", 2);
                    return;
                }
            }
            else
            {
                _lstcusItem = new List<BusEntityItem>();
            }
            BusEntityItem _itm = new BusEntityItem();
            _itm.MBII_ITM_CD = txtItem.Text.Trim().ToUpper();
            _itm.MBII_COM = txtCuscom.Text;
            _itm.MBII_CD = txtCust.Text;
            _itm.MBII_ACT = ddlCustStatus.SelectedItem.Text == "YES" ? 1 : 0;
            _itm.MBII_ACT_status = ddlCustStatus.SelectedItem.Text;
            _itm.MBII_CUSTNAME = txtCustName.Text;
            _itm.MBII_CRE_BY = Session["UserID"].ToString();
            _itm.MBII_MOD_BY = Session["UserID"].ToString();
            _itm.MBII_TP = "C";
            //dilshan
            _itm.MBII_WARR_PERI = Convert.ToInt32(ddlSupPrd.SelectedValue.ToString());
            _itm.MBII_WARR_RMK = txtSupRem.Text;
            _lstcusItem.Add(_itm);

            grdCustomer.DataSource = null;
            grdCustomer.AutoGenerateColumns = false;
            grdCustomer.DataSource = new List<BusEntityItem>();
            grdCustomer.DataSource = _lstcusItem;
            grdCustomer.DataBind();
            ViewState["_lstcusItem"] = _lstcusItem;
            txtCustName.Text = "";
            txtCuscom.Text = "";

            txtCust.Text = "";
            ddlCustStatus.SelectedIndex = -1;
        }

        //protected void txtMrnCom_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtMrnCom.Text.Trim() != "")
        //        {
        //            //TODO: LOAD COMPANY DESCRIPTION
        //            MasterCompany com = CHNLSVC.General.GetCompByCode(txtMrnCom.Text.Trim());
        //            if (com == null)
        //            {
        //                DisplayMessage("Invalid company", 2);
        //                return;
        //            }
        //            else
        //            {
        //                txtMrndes.Text = com.Mc_desc;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string _Msg = "Error Occurred while processing..!";
        //        DisplayMessage(_Msg, 4);
        //    }
        //}

        protected void lbtnSearchmrn_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company5";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void lbtnSearchgvMRN_Click(object sender, EventArgs e)
        //{
        //    //_lstmrn = ViewState["_lstmrn"] as List<mst_itm_mrn_com>;
        //    //if (string.IsNullOrEmpty(txtMrnCom.Text))
        //    //{
        //    //    DisplayMessage("Select MRN company", 2);
        //    //    return;
        //    //}
        //    //if (string.IsNullOrEmpty(ddlmrnStatus.Text))
        //    //{
        //    //    DisplayMessage("Select status", 2);
        //    //    return;
        //    //}
        //    //if (_lstmrn != null)
        //    //{
        //    //    mst_itm_mrn_com result = _lstmrn.Find(x => x.Imc_com == txtMrnCom.Text);
        //    //    if (result != null)
        //    //    {
        //    //        DisplayMessage("This company already exist", 2);
        //    //        return;
        //    //    }

        //    //}
        //    //else
        //    //{
        //    //    _lstmrn = new List<mst_itm_mrn_com>();
        //    //}

        //    //mst_itm_mrn_com _itm = new mst_itm_mrn_com();
        //    //_itm.Imc_itemcode = txtItem.Text.Trim().ToUpper();
        //    //_itm.Imc_com = txtMrnCom.Text;
        //    //_itm.Imc_comdes = txtMrndes.Text;
        //    //_itm.Imc_active = ddlmrnStatus.SelectedItem.ToString() == "YES" ? 1 : 0;
        //    //_itm.Imc_active_status = ddlmrnStatus.SelectedItem.ToString();
        //    //_itm.Imc_create_by = Session["UserID"].ToString();
        //    //_itm.Imc_modified_by = Session["UserID"].ToString();
        //    //_lstmrn.Add(_itm);

        //    //grdMRN.DataSource = null;
        //    //grdMRN.DataSource = new List<mst_itm_mrn_com>();
        //    //grdMRN.DataSource = _lstmrn;
        //    //grdMRN.DataBind();
        //    //ViewState["_lstmrn"] = _lstmrn;


        //    //txtMrnCom.Text = "";
        //    //txtMrndes.Text = "";
        //    //ddlmrnStatus.SelectedIndex = -1;


        //}

        protected void txtSupCom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSupCom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtSupCom.Text.Trim());
                    if (com == null)
                    {
                        DisplayMessage("Invalid company", 2);
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

        protected void lbtnsrhSupCom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company3";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtSupp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSupp.Text))
                {
                    if (!CHNLSVC.Inventory.IsValidSupplier(txtSupCom.Text.Trim().ToUpper(), txtSupp.Text, 1, "S"))
                    {
                        DisplayMessage("Invalid supplier code", 2);
                        txtSupp.Text = "";
                        txtSupp.Focus();
                        return;
                    }
                    else
                    {
                        MasterBusinessEntity _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(txtSupCom.Text.Trim().ToUpper(), txtSupp.Text, null, null, "S");

                        if (_supDet.Mbe_cd == null)
                        {
                            DisplayMessage("Invalid supplier code", 2);
                            txtSupp.Text = "";
                            txtSupName.Text = "";

                            return;
                        }
                        else
                        {
                            txtSupName.Text = _supDet.Mbe_name;

                        }

                    }


                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnSearchSupp_Click(object sender, EventArgs e)
        {
            try
            {
                // string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                if (string.IsNullOrEmpty(txtSupCom.Text))
                {
                    DisplayMessage("Enter supplier company", 2);
                    return;
                }
                string SearchParams = txtSupCom.Text + "|";
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Supplier";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnSearchgvSupplier_Click(object sender, EventArgs e)
        {
            _lstsupItem = ViewState["_lstsupItem"] as List<BusEntityItem>;
            if (string.IsNullOrEmpty(txtSupCom.Text))
            {
                DisplayMessage("Enter supplier company", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtSupp.Text))
            {
                DisplayMessage("Enter supplier", 2);
                return;
            }
            if (_lstsupItem != null)
            {
                BusEntityItem result = _lstsupItem.Find(x => x.MBII_CD == txtSupp.Text && x.MBII_COM == txtSupCom.Text && x.MBII_WARR_PERI == Convert.ToInt32(ddlSupPrd.SelectedValue.ToString()));
                if (result != null)
                {
                    DisplayMessage("This supplier already exist with this warranty period", 2);
                    return;
                }
            }
            else
            {
                _lstsupItem = new List<BusEntityItem>();
            }

            BusEntityItem _itm = new BusEntityItem();
            _itm.MBII_CD = txtSupp.Text;
            _itm.MBII_ITM_CD = txtItem.Text.Trim().ToUpper();
            _itm.MBII_COM = txtSupCom.Text;
            _itm.MBII_ACT = ddlsupStatus.SelectedItem.Text == "YES" ? 1 : 0;
            _itm.MBII_ACT_status = ddlsupStatus.SelectedItem.Text;
            // _itm.Msi_curr_code = "LKR";
            //   _itm.Msi_price_quoted = 0;
            _itm.MBII_CUSTNAME = txtSupName.Text;
            _itm.MBII_TP = "S";
            _itm.MBII_CRE_BY = Session["UserID"].ToString();
            _itm.MBII_MOD_BY = Session["UserID"].ToString();
            //dilshan
            _itm.MBII_CRE_DT = DateTime.Now;
            _itm.MBII_MOD_DT = DateTime.Now;
            _itm.MBII_WARR_PERI = Convert.ToInt32(ddlSupPrd.SelectedValue.ToString());
            _itm.MBII_WARR_RMK = txtSupRem.Text;
            _lstsupItem.Add(_itm);

            grdSupplier.DataSource = null;
            grdSupplier.DataSource = new List<BusEntityItem>();
            grdSupplier.DataSource = _lstsupItem;
            grdSupplier.DataBind();

            ViewState["_lstsupItem"] = _lstsupItem;
            txtSupCom.Text = "";
            txtSupp.Text = "";
            txtSupName.Text = "";
            ddlsupStatus.SelectedIndex = -1;
            txtSupRem.Text = "";

        }

        #endregion

        //protected void txtrepItem_TextChanged(object sender, EventArgs e)
        //{
        //    MasterItem _itemdetail = null;
        //    if (!string.IsNullOrEmpty(txtrepItem.Text))
        //    {

        //        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtrepItem.Text);
        //        if (_itemdetail == null)
        //        {
        //            DisplayMessage("Please check the item code", 2);
        //            txtrepItem.Text=string.Empty;
        //            txtrepItem.Focus();
        //            return;
        //        }

        //        if (_itemdetail.Mi_itm_tp == "R")
        //        {
        //            DisplayMessage("Please check the item code", 2);
        //            txtrepItem.Text=string.Empty;
        //            txtrepItem.Focus();
        //            return;
        //        }

        //        //if (txtrepItem.Text == txtItem.Text)
        //        //{
        //        //    DisplayMessage("Replace item and can be same as item code", 2);                   
        //        //    txtrepItem.Text=string.Empty;
        //        //    txtrepItem.Focus();
        //        //    return;
        //        //}

        //        txtrepDes.Text = _itemdetail.Mi_shortdesc;
        //    }

        //}

        protected void lbtnSerchRep_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Rep.product";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void lbtnAddRepalced_Click(object sender, EventArgs e)
        //{
        //    _lstreplace = ViewState["_lstreplace"] as List<mst_itm_replace>;
        //    if (string.IsNullOrEmpty(txtrepItem.Text))
        //    {
        //        DisplayMessage("Select replaced item", 2);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(ddlrepStatus.Text))
        //    {
        //        DisplayMessage("Select status", 2);
        //        return;
        //    }

        //    if (_lstreplace != null)
        //    {
        //        mst_itm_replace result = _lstreplace.Find(x => x.Rpl_replaceditem == txtrepItem.Text);
        //        if (result != null)
        //        {
        //            DisplayMessage("This item already exist", 2);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        _lstreplace = new List<mst_itm_replace>();
        //    }

        //    mst_itm_replace _itm = new mst_itm_replace();
        //    _itm.Rpl_item = txtItem.Text;
        //    _itm.Rpl_replaceditem = txtrepItem.Text;
        //    _itm.Rpl_itemdes = txtrepDes.Text;
        //    _itm.Rpl_active = ddlrepStatus.SelectedItem.ToString() == "YES" ? 1 : 0;
        //    _itm.Rpl_active_status = ddlrepStatus.SelectedItem.ToString();
        //    _itm.Rpl_repl_reson = Convert.ToInt32(ddlReplaceProduct.SelectedValue);
        //    _itm.Rpl_effect_dt = Convert.ToDateTime(txtEffectiveFrom.Text);
        //    _itm.Rpl_cre_by = Session["UserID"].ToString();
        //    _itm.Rpl_mod_by = Session["UserID"].ToString();
        //    _lstreplace.Add(_itm);

        //    //grdRepalced.DataSource = null;
        //    //grdRepalced.DataSource = new List<mst_itm_replace>();
        //    //grdRepalced.DataSource = _lstreplace;
        //    //grdRepalced.DataBind();

        //    //ViewState["_lstreplace"] = _lstreplace;

        //    //txtrepItem.Text = "";
        //    //txtrepDes.Text = "";
        //    //ddlrepStatus.SelectedIndex = -1;

        //}

        //protected void txtkitItem_TextChanged(object sender, EventArgs e)
        //{
        //    MasterItem _itemdetail = null;
        //    if (!string.IsNullOrEmpty(txtkitItem.Text))
        //    {

        //        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtkitItem.Text);
        //        if (_itemdetail == null)
        //        {
        //            DisplayMessage("Please check the item code", 2);
        //            txtkitItem.Text = string.Empty;
        //            txtkitItem.Focus();
        //            return;
        //        }

        //        if (_itemdetail.Mi_itm_tp == "R")
        //        {
        //            DisplayMessage("Please check the item code", 2);
        //            txtkitItem.Text = string.Empty;
        //            txtkitItem.Focus();
        //            return;
        //        }

        //        if (txtkitItem.Text == txtItem.Text.Trim().ToUpper())
        //        {
        //            DisplayMessage("KIT/SKU item and can be same as item code", 2);
        //            txtkitItem.Text = string.Empty;
        //            txtkitItem.Focus();
        //            return;
        //        }
        //    }
        //}

        protected void lbtnSearchKit_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, null, null);
                grdResult.DataSource = _result;
                ViewState["SEARCH"] = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Kit";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void lbtnAddKit_Click(object sender, EventArgs e)
        //{
        //    _lstkit = ViewState["_lstkit"] as List<MasterItemComponent>;
        //    if (string.IsNullOrEmpty(txtkitItem.Text))
        //    {
        //        DisplayMessage("Select item components", 2);
        //        return;
        //    }
        //    #region Add by Lakshan 06 Oct 2016
        //    if (ddlCostMeth.SelectedIndex < 1)
        //    {
        //        DispMsg("Please select the cost method !"); return;
        //    }
        //    #endregion
        //    if (string.IsNullOrEmpty(txtkitcost.Text))
        //    {
        //        DisplayMessage("Select item cost percentage", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(ddlKitItemType.SelectedItem.Text))
        //    {
        //        DisplayMessage("Select item type", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(ddlScan.SelectedItem.Text))
        //    {
        //        DisplayMessage("Select scan or not", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtunits.Text))
        //    {
        //        DisplayMessage("Select units", 2);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(ddlKitActive.Text))
        //    {
        //        DisplayMessage("Select Status", 2);
        //        return;
        //    }
        //    if (ddlCostMeth.SelectedValue == "1")
        //    {
        //        if (Convert.ToDecimal(txtkitcost.Text) > 100)
        //        {
        //            txtkitcost.Text = "";
        //            DisplayMessage("Please enter valid percentage ", 2);
        //            return;
        //        }
        //    }

        //    if (_lstkit != null)
        //    {
        //        if (_lstkit.Count > 0)
        //        {
        //            if (_lstkit[0].MICP_IS_PERCENTAGE != Convert.ToInt32(ddlCostMeth.SelectedValue))
        //            {
        //                DispMsg("Cost method is invalid !"); return;
        //            }
        //            var _perList = _lstkit.Where(c => c.MICP_IS_PERCENTAGE == 1).ToList();
        //            if (_perList.Count > 0)
        //            {
        //                decimal _totPrec = _lstkit.Sum(c => c.Micp_cost_percentage);
        //                if (Session["iskitcheckboxcheck"] != "1")
        //                {
        //                    _totPrec = _totPrec + Convert.ToDecimal(txtkitcost.Text);
        //                }
        //                if (_totPrec > 100)
        //                {
        //                    DispMsg("Main item cost precentage is exceed in KIT SETUP !"); return;
        //                }
        //            }
        //        }
        //        bool act = ddlKitActive.SelectedItem.Text == "YES" ? true : false;
        //        if (Session["iskitcheckboxcheck"] == "1")
        //        {
        //            MasterItemComponent resultupdate = _lstkit.SingleOrDefault(x => x.Micp_comp_itm_cd == txtkitItem.Text);
        //            var _perList = _lstkit.Where(c => c.MICP_IS_PERCENTAGE == 1).ToList();
        //            if (resultupdate != null)
        //            {
        //                if (_perList.Count > 0)
        //                {
        //                    decimal _totPrec = _lstkit.Sum(c => c.Micp_cost_percentage);
        //                    _totPrec = _totPrec + Convert.ToDecimal(txtkitcost.Text) - resultupdate.Micp_cost_percentage;
        //                    if (_totPrec > 100)
        //                    {
        //                        DispMsg("Main item cost precentage is exceed in KIT SETUP !"); return;
        //                    }
        //                }
        //            }
        //            if (resultupdate != null)
        //            {
        //                resultupdate.Micp_comp_itm_cd = txtkitItem.Text;
        //                resultupdate.Micp_cost_percentage = Convert.ToDecimal(txtkitcost.Text);
        //                resultupdate.Micp_itm_tp = ddlKitItemType.SelectedItem.Text;
        //                resultupdate.Micp_must_scan = ddlScan.SelectedItem.Text == "YES" ? true : false;
        //                resultupdate.Micp_qty = Convert.ToInt32(txtunits.Text);
        //                resultupdate.Micp_act = ddlKitActive.SelectedItem.Text == "YES" ? true : false;
        //                resultupdate.Micp_act_status = ddlKitActive.SelectedItem.Text;
        //                resultupdate.Micp_cate = ddlkitCate.SelectedItem.Text;
        //                resultupdate.MICP_IS_PERCENTAGE = Convert.ToInt32(ddlCostMeth.SelectedValue);
        //            }

        //            grdKit.DataSource = null;
        //            grdKit.DataSource = new List<MasterItemComponent>();
        //            grdKit.DataSource = _lstkit;
        //            grdKit.DataBind();
        //            ViewState["_lstkit"] = _lstkit;
        //            txtkitItem.Text = "";
        //            Session["iskitcheckboxcheck"] = "";
        //            ddlCostMeth.SelectedIndex = 1;
        //            ddlCostMeth_SelectedIndexChanged(null, null);
        //            txtkitcost.Text = "";
        //            ddlKitItemType.SelectedIndex = -1;
        //            ddlScan.SelectedIndex = -1;
        //            txtunits.Text = "";
        //            ddlKitActive.SelectedIndex = -1;
        //            return;
        //        }

        //        MasterItemComponent result = _lstkit.Find(x => x.Micp_comp_itm_cd == txtkitItem.Text && x.Micp_act == act);

        //        if (result != null)
        //        {

        //            DisplayMessage("This item components already exist", 2);
        //            return;
        //        }
        //        else
        //        {
        //            MasterItemComponent _itm = new MasterItemComponent();
        //            _itm.Micp_itm_cd = txtItem.Text.Trim().ToUpper();
        //            _itm.Micp_comp_itm_cd = txtkitItem.Text;
        //            _itm.Micp_cost_percentage = Convert.ToDecimal(txtkitcost.Text);
        //            _itm.Micp_itm_tp = ddlKitItemType.SelectedItem.Text;
        //            _itm.Micp_must_scan = ddlScan.SelectedItem.Text == "YES" ? true : false;
        //            _itm.Micp_qty = Convert.ToInt32(txtunits.Text);
        //            _itm.Micp_act = ddlKitActive.SelectedItem.Text == "YES" ? true : false;
        //            _itm.Micp_act_status = ddlKitActive.SelectedItem.Text;
        //            _itm.Micp_cate = ddlkitCate.SelectedItem.Text;
        //            _itm.MICP_IS_PERCENTAGE = Convert.ToInt32(ddlCostMeth.SelectedValue);
        //            _lstkit.Add(_itm);
        //        }
        //    }
        //    else
        //    {
        //        _lstkit = new List<MasterItemComponent>();


        //        MasterItemComponent _itm = new MasterItemComponent();
        //        _itm.Micp_itm_cd = txtItem.Text.Trim().ToUpper();
        //        _itm.Micp_comp_itm_cd = txtkitItem.Text;
        //        _itm.Micp_cost_percentage = Convert.ToDecimal(txtkitcost.Text);
        //        _itm.Micp_itm_tp = ddlKitItemType.SelectedItem.Text;
        //        _itm.Micp_must_scan = ddlScan.SelectedItem.Text == "YES" ? true : false;
        //        _itm.Micp_qty = Convert.ToInt32(txtunits.Text);
        //        _itm.Micp_act = ddlKitActive.SelectedItem.Text == "YES" ? true : false;
        //        _itm.Micp_act_status = ddlKitActive.SelectedItem.Text;
        //        _itm.Micp_cate = ddlkitCate.SelectedItem.Text;
        //        _itm.Micp_cate = ddlkitCate.SelectedItem.Text;
        //        _itm.MICP_IS_PERCENTAGE = Convert.ToInt32(ddlCostMeth.SelectedValue);
        //        _lstkit.Add(_itm);
        //    }

        //    grdKit.DataSource = null;
        //    grdKit.DataSource = new List<MasterItemComponent>();
        //    grdKit.DataSource = _lstkit;
        //    grdKit.DataBind();
        //    ViewState["_lstkit"] = _lstkit;
        //    txtkitItem.Text = "";
        //    ddlCostMeth.SelectedIndex = 1;
        //    ddlCostMeth_SelectedIndexChanged(null, null);
        //    txtkitcost.Text = "";
        //    ddlKitItemType.SelectedIndex = -1;
        //    ddlScan.SelectedIndex = -1;
        //    txtunits.Text = "";
        //    ddlKitActive.SelectedIndex = -1;


        //}
        //protected void chk_Kitcheck_Click(object sender, EventArgs e)
        //{
        //    ddlCostMeth.SelectedIndex = 1;
        //    ddlCostMeth_SelectedIndexChanged(null, null);
        //    txtkitItem.Text = "";
        //    ddlkitCate.SelectedIndex = 0;
        //    ddlKitItemType.SelectedIndex = 0;
        //    txtkitcost.Text = "";
        //    ddlScan.SelectedIndex = 0;
        //    txtunits.Text = "";
        //    ddlKitActive.SelectedIndex = 0;
        //    Session["iskitcheckboxcheck"] = "0";

        //    if (grdKit.Rows.Count == 0) return;
        //    var lb = (CheckBox)sender;
        //    var row = (GridViewRow)lb.NamingContainer;
        //    if (row != null)
        //    {
        //        if (lb.Checked == true)
        //        {
        //            string kit_item = (row.FindControl("kit_item") as Label).Text;
        //            string kit_cat = (row.FindControl("kit_cat") as Label).Text;
        //            string kit_type = (row.FindControl("kit_type") as Label).Text;
        //            string kit_cost = (row.FindControl("kit_cost") as Label).Text;
        //            string kit_scan = (row.FindControl("kit_scan") as Label).Text;
        //            string kit_units = (row.FindControl("kit_units") as Label).Text;
        //            string Micp_act_status = (row.FindControl("Micp_act_status") as Label).Text;
        //            string lblmicp_is_percentage = (row.FindControl("lblmicp_is_percentage") as Label).Text;
        //            ddlCostMeth.SelectedIndex = ddlCostMeth.Items.IndexOf(ddlCostMeth.Items.FindByValue(lblmicp_is_percentage));
        //            ddlCostMeth_SelectedIndexChanged(null, null);
        //            txtkitItem.Text = kit_item;
        //            ddlkitCate.Text = kit_cat;
        //            ddlKitItemType.Text = kit_type;
        //            txtkitcost.Text = kit_cost;
        //            ddlScan.Text = kit_scan;
        //            txtunits.Text = kit_units;
        //            ddlKitActive.Text = Micp_act_status;
        //            Session["iskitcheckboxcheck"] = "1";
        //        }
        //    }
        //}
        protected void txtwuom_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtwuom.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtwuom.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid UOM", 2);
                txtwuom.Text = string.Empty;
                txtwuom.Focus();
                return;
            }
        }

        protected void lbtnSrhWeightUOM_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "WeightUOM";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtduom_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtduom.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtduom.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid UOM", 2);
                txtduom.Text = string.Empty;
                txtduom.Focus();
                return;
            }
        }

        protected void lbtnsrhdimenuom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "DiamentionsUOM";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddCont_Click(object sender, EventArgs e)
        {
            _lstcont = ViewState["_lstcont"] as List<mst_itm_container>;
            //if (string.IsNullOrEmpty(ddlContType.SelectedItem.Text))
            //{
            //    DisplayMessage("Select container Type", 2);
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtContUnits.Text))
            //{
            //    DisplayMessage("Enter no of units", 2);
            //    return;
            //}
            if (_lstcont != null)
            {
                //mst_itm_container result = _lstcont.Find(x => x.Ic_container_type_code == ddlContType.SelectedValue.ToString());
                //if (result != null)
                //{
                //    DisplayMessage("This record already exist", 2);
                //    return;
                //    result.Ic_no_of_unit_per_one_item = Convert.ToInt32(txtContUnits.Text);
                //    result.Ic_act = ddlContainerAct.SelectedValue == "YES" ? 1 : 0;
                //}
                //else
                //{
                //    mst_itm_container _itm = new mst_itm_container();
                //    _itm.Ic_item_code = txtItem.Text.Trim().ToUpper();
                //  //  _itm.Ic_container_type_code = ddlContType.SelectedValue.ToString();
                //  //  _itm.Ic_no_of_unit_per_one_item = Convert.ToInt32(txtContUnits.Text);
                //    _itm.Ic_create_by = Session["UserID"].ToString();
                //    _itm.Ic_last_modify_by = Session["UserID"].ToString();
                //    //Randima pls add description here 
                //    _itm.Ic_description = "Test";
                ////    _itm.Ic_act = ddlContainerAct.SelectedValue == "YES" ? 1 : 0;
                //    _lstcont.Add(_itm);
                //}
            }
            else
            {
                _lstcont = new List<mst_itm_container>();
                mst_itm_container _itm = new mst_itm_container();
                _itm.Ic_item_code = txtItem.Text.Trim().ToUpper();
               // _itm.Ic_container_type_code = ddlContType.SelectedValue.ToString();
             //   _itm.Ic_no_of_unit_per_one_item = Convert.ToInt32(txtContUnits.Text);
                _itm.Ic_create_by = Session["UserID"].ToString();
                _itm.Ic_last_modify_by = Session["UserID"].ToString();
                //Randima pls add description here 
                _itm.Ic_description = "Test";
            //    _itm.Ic_act = ddlContainerAct.SelectedValue == "YES" ? 1 : 0;

                _lstcont.Add(_itm);
            }

            //grdCont.DataSource = null;
            //grdCont.DataSource = new List<mst_itm_container>();
            //grdCont.DataSource = _lstcont;
            //grdCont.DataBind();

            //ViewState["_lstcont"] = _lstcont;
            //ddlContType.SelectedIndex = -1;
            //txtContUnits.Text = "";
        }

        protected void txtclaimcom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtclaimcom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtclaimcom.Text.Trim());
                    if (com == null)
                    {
                        DisplayMessage("Invalid company", 2);
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

        protected void lbtnsrhTaxCom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company6";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddTaxClaim_Click(object sender, EventArgs e)
        {
            _lsttaxClaim = ViewState["_lsttaxClaim"] as List<MasterItemTaxClaim>;
            if (string.IsNullOrEmpty(txtclaim.Text))
            {
                DisplayMessage("Enter claimable percentage", 2);
                return;
            }

            if (string.IsNullOrEmpty(ddlclaimcate.SelectedItem.Text))
            {
                DisplayMessage("Select tax category", 2);
                return;
            }
            if (string.IsNullOrEmpty(txttaxRate.Text))
            {
                DisplayMessage("Select Rate", 2);
                return;
            }
            if (Session["Multiplecom"] == "true")
            {
                foreach (ListItem Item in chklstbox.Items)
                {
                    if (Item.Selected == true)
                    {
                        if (_lsttaxClaim != null)
                        {
                            MasterItemTaxClaim result = _lsttaxClaim.Find(x => x.Mic_com == Item.Text
                                                        && x.Mic_tax_rt == Convert.ToDecimal(txttaxRate.Text) && x.Mic_tax_cd == ddlclaimcate.SelectedValue.ToString());
                            if (result != null)
                            {

                                result.Mic_stus = ddlTaxClAc.SelectedItem.Text == "YES" ? true : false;
                                DisplayMessage("This record already exist", 2);
                                grdTaxClaim.DataSource = null;
                                grdTaxClaim.DataSource = new List<MasterItemTaxClaim>();
                                grdTaxClaim.DataSource = _lsttaxClaim;
                                grdTaxClaim.DataBind();

                                ViewState["_lsttaxClaim"] = _lsttaxClaim;

                                txttaxRate.Text = "";
                                return;
                            }
                        }
                        else
                        {
                            _lsttaxClaim = new List<MasterItemTaxClaim>();
                        }

                        MasterItemTaxClaim _itmMul = new MasterItemTaxClaim();
                        _itmMul.Mic_claim = Convert.ToDecimal(txtclaim.Text);
                        _itmMul.Mic_com = Item.Text;
                        _itmMul.Mic_itm_cd = txtItem.Text.Trim().ToUpper();
                        _itmMul.Mic_stus = ddlTaxClAc.SelectedItem.Text == "YES" ? true : false;
                        _itmMul.Mic_tax_cd = ddlclaimcate.SelectedValue.ToString();
                        _itmMul.Mic_tax_rt = Convert.ToDecimal(txttaxRate.Text);
                        _itmMul.Mic_cre_by = Session["UserID"].ToString();

                        _lsttaxClaim.Add(_itmMul);

                        grdTaxClaim.DataSource = null;
                        grdTaxClaim.DataSource = new List<MasterItemTaxClaim>();
                        grdTaxClaim.DataSource = _lsttaxClaim;
                        grdTaxClaim.DataBind();

                        ViewState["_lsttaxClaim"] = _lsttaxClaim;

                    }
                }
                txtclaim.Text = "";
                txtclaimcom.Text = "";
                ddlclaimcate.SelectedIndex = -1;
                txttaxRate.Text = "";
                Session["Multiplecom"] = "";
                return;
            }


            if (string.IsNullOrEmpty(txtclaimcom.Text))
            {
                DisplayMessage("Select company", 2);
                return;
            }

            if (_lsttaxClaim != null)
            {
                MasterItemTaxClaim result = _lsttaxClaim.Find(x => x.Mic_com == txtclaimcom.Text && x.Mic_tax_rt == Convert.ToDecimal(txttaxRate.Text));
                if (result != null)
                {

                    result.Mic_stus = ddlTaxClAc.SelectedItem.Text == "YES" ? true : false;
                    // DisplayMessage("This record already exist", 2);
                    grdTaxClaim.DataSource = null;
                    grdTaxClaim.DataSource = new List<MasterItemTaxClaim>();
                    grdTaxClaim.DataSource = _lsttaxClaim;
                    grdTaxClaim.DataBind();

                    ViewState["_lsttaxClaim"] = _lsttaxClaim;
                    txtclaim.Text = "";
                    txtclaimcom.Text = "";
                    ddlclaimcate.SelectedIndex = -1;
                    txttaxRate.Text = "";
                    return;
                }
            }
            else
            {
                _lsttaxClaim = new List<MasterItemTaxClaim>();
            }

            MasterItemTaxClaim _itm = new MasterItemTaxClaim();
            _itm.Mic_claim = Convert.ToDecimal(txtclaim.Text);
            _itm.Mic_com = txtclaimcom.Text;
            _itm.Mic_itm_cd = txtItem.Text.Trim().ToUpper();
            _itm.Mic_stus = ddlTaxClAc.SelectedItem.Text == "YES" ? true : false;
            _itm.Mic_tax_cd = ddlclaimcate.SelectedValue.ToString();
            _itm.Mic_tax_rt = Convert.ToDecimal(txttaxRate.Text);
            _itm.Mic_cre_by = Session["UserID"].ToString();

            _lsttaxClaim.Add(_itm);

            grdTaxClaim.DataSource = null;
            grdTaxClaim.DataSource = new List<MasterItemTaxClaim>();
            grdTaxClaim.DataSource = _lsttaxClaim;
            grdTaxClaim.DataBind();

            ViewState["_lsttaxClaim"] = _lsttaxClaim;
            txtclaim.Text = "";
            txtclaimcom.Text = "";
            ddlclaimcate.SelectedIndex = -1;
            txttaxRate.Text = "";
        }

        protected void txtseruom_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtseruom.Text))
            {
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtseruom.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid UOM", 2);
                txtseruom.Text = string.Empty;
                txtseruom.Focus();
                return;
            }
        }

        protected void lbtnsrhserUOM_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "ServiceUOM";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtserduom_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtserduom.Text))
            {
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtserduom.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid UOM", 2);
                txtserduom.Text = string.Empty;
                txtserduom.Focus();
                return;
            }
        }

        protected void lbtnsrhseraUOM_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "ServiceUOM2";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddservice_Click(object sender, EventArgs e)
        {
            _lstitmPrd = ViewState["_lstitmPrd"] as List<mst_itm_sevpd>;
            if (ddlserSts.SelectedIndex == 0)
            {
                DisplayMessage("Select status", 2);
                return;
            }
            if (string.IsNullOrEmpty(ddlserTerm.SelectedItem.Text))
            {
                DisplayMessage("Select Term", 2);
                return;
            }


            if (string.IsNullOrEmpty(txtserfrom.Text))
            {
                DisplayMessage("Select Term", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtserto.Text))
            {
                DisplayMessage("Select To", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtseruom.Text))
            {
                DisplayMessage("Select UOM", 2);
                return;
            }

            if (_lstitmPrd != null)
            {
                mst_itm_sevpd result = _lstitmPrd.Find(x => x.Msp_itm_stus == ddlserSts.SelectedValue.ToString() && x.Msp_term == Convert.ToInt32(ddlserTerm.SelectedValue.ToString()));
                if (result != null)
                {
                    DisplayMessage("This record already exist", 2);
                    return;
                }
            }

            else
            {
                _lstitmPrd = new List<mst_itm_sevpd>();
            }
            mst_itm_sevpd _itm = new mst_itm_sevpd();
            _itm.Msp_itm_stus = ddlserSts.SelectedValue.ToString();
            _itm.Msp_itm_cd = txtItem.Text.Trim().ToUpper();
            _itm.Msp_term = Convert.ToInt32(ddlserTerm.SelectedItem.ToString());
            _itm.Msp_pd_from = Convert.ToDecimal(txtserfrom.Text);
            _itm.Msp_pd_to = Convert.ToDecimal(txtserto.Text);
            _itm.Msp_pd_uom = txtseruom.Text;
            _itm.Msp_pdalt_from = Convert.ToDecimal(txtserdfrom.Text);
            _itm.Msp_pdalt_to = Convert.ToDecimal(txtserdto.Text);
            _itm.Msp_pdalt_uom = txtserduom.Text;
            _itm.Msp_isfree = ddlserisfree.SelectedItem.ToString() == "YES" ? 1 : 0;
            _itm.Msp_isfree_status = ddlserisfree.SelectedItem.ToString();
            _itm.Msp_status_de = ddlserSts.SelectedItem.Text;

            _lstitmPrd.Add(_itm);

            grdservice.DataSource = null;
            grdservice.AutoGenerateColumns = false;
            grdservice.DataSource = new List<mst_itm_sevpd>();
            grdservice.DataSource = _lstitmPrd;
            grdservice.DataBind();

            ViewState["_lstitmPrd"] = _lstitmPrd;
            ddlserSts.SelectedIndex = -1;
            ddlserTerm.SelectedIndex = -1;
            txtserfrom.Text = "";
            txtserto.Text = "";
            txtseruom.Text = "";
            txtserdfrom.Text = "";
            txtserdto.Text = "";
            txtserduom.Text = "";
            ddlserisfree.SelectedIndex = -1;
        }

        protected void txtWaraUOM_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWaraUOM.Text))
            {
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtWaraUOM.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid UOM", 2);
                txtWaraUOM.Text = string.Empty;
                txtWaraUOM.Focus();
                return;
            }
        }

        protected void lbtnsrhwarauom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "WarrantyUOM";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddWarranty_Click(object sender, EventArgs e)
        {
            _lstitmWrd = ViewState["_lstitmWrd"] as List<MasterItemWarrantyPeriod>;

            if (string.IsNullOrEmpty(txtWaraUOM.Text))
            {
                DisplayMessage("Select UOM", 2);
                return;
            }
            //if ((ddlwPeriod.SelectedIndex  == 0 ) && (ddlwarsPrd.SelectedIndex == 0))
            //{
            //    DisplayMessage("Enter warranty period", 2);
            //    return;
            //}
            if (_lstitmWrd == null)
            {
                _lstitmWrd = new List<MasterItemWarrantyPeriod>();
            }
            if (Session["Multiplestatus"] != null)
            {
                if (Session["Multiplestatus"].ToString() == "true")
                {
                    #region MyRegion
                    foreach (GridViewRow Item in grdMultiplestatus.Rows)
                    {
                        CheckBox chkMultiplestatus = Item.FindControl("chkMultiplestatus") as CheckBox;
                        Label lblCode = Item.FindControl("Icr_com_code") as Label;
                        Label lblCodeDes = Item.FindControl("Icr_itm_code") as Label;
                        if (chkMultiplestatus.Checked == true)
                        {
                            string Status = string.Empty;
                            //DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                            //if ((_status != null) && (_status.Rows.Count > 0))
                            //{
                            //    DataRow[] drs = _status.Select("MIS_DESC='" + lblCode.ToString() + "'");
                            //    Status = drs[0][0].ToString();
                            //}
                            if (_lstitmWrd != null)
                            {


                                MasterItemWarrantyPeriod result = _lstitmWrd.Find(x => x.Mwp_itm_stus == Status);
                                if (result != null)
                                {
                                    DisplayMessage("This record already exist", 2);
                                    return;
                                }
                                else
                                {
                                    MasterItemWarrantyPeriod _itm = new MasterItemWarrantyPeriod();
                                    _itm.Mwp_itm_cd = txtItem.Text.Trim().ToUpper();
                                    _itm.Mwp_itm_stus = lblCode.Text;//Status;
                                    _itm.Mwp_item_st_des = lblCodeDes.Text;
                                    _itm.Mwp_val = Convert.ToInt32(ddlwPeriod.SelectedValue.ToString());
                                    _itm.Mwp_sup_warranty_prd = Convert.ToInt32(ddlwarsPrd.SelectedValue.ToString());
                                    _itm.Mwp_warr_tp = txtWaraUOM.Text;
                                    //   _itm.Mwp_sup_warr_prd_alt = Convert.ToInt32(cmbwarsdur.Text);
                                    _itm.Mwp_rmk = txtWarRem.Text;
                                    _itm.Mwp_effect_dt = Convert.ToDateTime(txtEffectiveDate.Text);//dtpEffectiveDate.Value.Date;

                                    _itm.Mwp_sup_wara_rem = txtwarsRem.Text;
                                    _itm.Mwp_cre_by = Session["UserID"].ToString();
                                    _itm.Mwp_mod_by = Session["UserID"].ToString();
                                    _itm.Mwp_cre_dt = DateTime.Now;
                                    _itm.Mwp_mod_dt = DateTime.Now;
                                    _itm.Mwp_act = true;
                                    _itm.Mwp_def = true;
                                    _lstitmWrd.Add(_itm);

                                }
                            }
                            else
                            {
                                MasterItemWarrantyPeriod _itm = new MasterItemWarrantyPeriod();
                                _itm.Mwp_itm_cd = txtItem.Text.Trim().ToUpper();
                                _itm.Mwp_itm_stus = Status;
                                _itm.Mwp_item_st_des = lblCode.ToString();
                                _itm.Mwp_val = Convert.ToInt32(ddlwPeriod.SelectedValue.ToString());
                                _itm.Mwp_sup_warranty_prd = Convert.ToInt32(ddlwarsPrd.SelectedValue.ToString());
                                _itm.Mwp_warr_tp = txtWaraUOM.Text;
                                //   _itm.Mwp_sup_warr_prd_alt = Convert.ToInt32(cmbwarsdur.Text);
                                _itm.Mwp_rmk = txtWarRem.Text;
                                _itm.Mwp_effect_dt = Convert.ToDateTime(txtEffectiveDate.Text);//dtpEffectiveDate.Value.Date;

                                _itm.Mwp_sup_wara_rem = txtwarsRem.Text;
                                _itm.Mwp_cre_by = Session["UserID"].ToString();
                                _itm.Mwp_mod_by = Session["UserID"].ToString();
                                _itm.Mwp_cre_dt = DateTime.Now;
                                _itm.Mwp_mod_dt = DateTime.Now;
                                _itm.Mwp_act = true;
                                _itm.Mwp_def = true;
                                _lstitmWrd.Add(_itm);

                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region
                    //if (ddlwStatus.SelectedIndex == 0)
                    //{
                    //    DisplayMessage("Select item status", 2);
                    //    return;
                    //}
                    if (_lstitmWrd != null)
                    {

                        MasterItemWarrantyPeriod result = _lstitmWrd.Find(x => x.Mwp_itm_stus == ddlwStatus.SelectedValue.ToString());
                        if (result != null)
                        {
                            DisplayMessage("This record already exist", 2);
                            return;
                        }
                    }
                    else
                    {
                        _lstitmWrd = new List<MasterItemWarrantyPeriod>();
                    }


                    MasterItemWarrantyPeriod _itm = new MasterItemWarrantyPeriod();
                    _itm.Mwp_itm_cd = txtItem.Text.Trim().ToUpper();
                    _itm.Mwp_itm_stus = ddlwStatus.SelectedValue.ToString();
                    _itm.Mwp_item_st_des = ddlwStatus.SelectedItem.Text;
                    _itm.Mwp_val = Convert.ToInt32(ddlwPeriod.SelectedValue.ToString());
                    Int32 _tmp = 0;
                    _itm.Mwp_sup_warranty_prd = Int32.TryParse(ddlwarsPrd.SelectedValue, out _tmp) ? Convert.ToInt32(ddlwarsPrd.SelectedValue) : 0;
                    _itm.Mwp_warr_tp = txtWaraUOM.Text;
                    //   _itm.Mwp_sup_warr_prd_alt = Convert.ToInt32(cmbwarsdur.Text);
                    _itm.Mwp_rmk = txtWarRem.Text;
                    _itm.Mwp_effect_dt = Convert.ToDateTime(txtEffectiveDate.Text);//dtpEffectiveDate.Value.Date;

                    _itm.Mwp_sup_wara_rem = txtwarsRem.Text;
                    _itm.Mwp_cre_by = Session["UserID"].ToString();
                    _itm.Mwp_mod_by = Session["UserID"].ToString();
                    _itm.Mwp_cre_dt = DateTime.Now;
                    _itm.Mwp_mod_dt = DateTime.Now;
                    _itm.Mwp_act = true;
                    _itm.Mwp_def = true;
                    _lstitmWrd.Add(_itm);
                    #endregion
                }
            }
            else
            {
                //if (ddlwStatus.SelectedIndex == 0)
                //{
                //    DisplayMessage("Select item status", 2);
                //    return;
                //}
                if (_lstitmWrd != null)
                {

                    MasterItemWarrantyPeriod result = _lstitmWrd.Find(x => x.Mwp_itm_stus == ddlwStatus.SelectedValue.ToString());
                    if (result != null)
                    {
                        DisplayMessage("This record already exist", 2);
                        return;
                    }
                }
                else
                {
                    _lstitmWrd = new List<MasterItemWarrantyPeriod>();
                }


                MasterItemWarrantyPeriod _itm = new MasterItemWarrantyPeriod();
                _itm.Mwp_itm_cd = txtItem.Text.Trim().ToUpper();
                _itm.Mwp_itm_stus = ddlwStatus.SelectedValue.ToString();
                _itm.Mwp_item_st_des = ddlwStatus.SelectedItem.Text;
                _itm.Mwp_val = Convert.ToInt32(ddlwPeriod.SelectedValue.ToString());
                Int32 _tmp = 0;
                _itm.Mwp_sup_warranty_prd = Int32.TryParse(ddlwarsPrd.SelectedValue, out _tmp) ? Convert.ToInt32(ddlwarsPrd.SelectedValue) : 0;
                _itm.Mwp_warr_tp = txtWaraUOM.Text;
                //   _itm.Mwp_sup_warr_prd_alt = Convert.ToInt32(cmbwarsdur.Text);
                _itm.Mwp_rmk = txtWarRem.Text;
                _itm.Mwp_effect_dt = Convert.ToDateTime(txtEffectiveDate.Text);//dtpEffectiveDate.Value.Date;

                _itm.Mwp_sup_wara_rem = txtwarsRem.Text;
                _itm.Mwp_cre_by = Session["UserID"].ToString();
                _itm.Mwp_mod_by = Session["UserID"].ToString();
                _itm.Mwp_cre_dt = DateTime.Now;
                _itm.Mwp_mod_dt = DateTime.Now;
                _itm.Mwp_act = true;
                _itm.Mwp_def = true;
                _lstitmWrd.Add(_itm);
            }


            grdWarranty.DataSource = null;
            grdWarranty.DataSource = new List<MasterItemWarrantyPeriod>();
            grdWarranty.DataSource = _lstitmWrd;
            grdWarranty.DataBind();


            ViewState["_lstitmWrd"] = _lstitmWrd;
            ddlwStatus.SelectedIndex = -1;
            ddlwarsPrd.SelectedIndex = -1;
            ddlSupPrd.SelectedIndex = -1;
            ddlwPeriod.SelectedIndex = -1;
            txtWarRem.Text = "";
            txtWaraUOM.Text = "MTH";
            txtwarsRem.Text = "";
        }

        protected void txtpcCom_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSearchpcWara_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company7";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtpc_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtpc.Text))
            {
                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(Session["UserCompanyCode"].ToString(), "PC", txtpc.Text);
                if (LocDes.Rows.Count == 0)
                {
                    DisplayMessage("Invalid profit center", 2);
                    return;
                    txtpc.Focus();
                }
            }
        }

        protected void lbtnSearchpc_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Location";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddPcwarranty_Click(object sender, EventArgs e)
        {
            _lstpcWrd = ViewState["_lstpcWrd"] as List<mst_itm_pc_warr>;
            if (string.IsNullOrEmpty(txtwarrantycompany.Text))
            {
                DisplayMessage("Enter company", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtpcCom.Text))
            {
                DisplayMessage("Enter company/PC/Channel", 2);
                return;
            }
            //if (string.IsNullOrEmpty(txtpc.Text))
            //{
            //    DisplayMessage("Enter pc", 2);
            //    return;
            //}
            if (ddlPcstatus.SelectedIndex == 0)
            {
                DisplayMessage("Enter item status", 2);
                return;
            }
            if (ddlpcPrd.SelectedIndex == 0)
            {
                DisplayMessage("Enter warranty period", 2);
                return;
            }

            if (Convert.ToDateTime(txtpcfrom.Text).Date > Convert.ToDateTime(txtpcto.Text).Date)
            {
                DisplayMessage("From date must be lesst than to date", 2);
                return;
            }
            DateTime _dt = DateTime.Today;
            if (_dt > Convert.ToDateTime(txtpcfrom.Text).Date)
            {
                DispMsg("Please select a valid from date !"); return;
            }
            if (_dt > Convert.ToDateTime(txtpcto.Text).Date)
            {
                DispMsg("Please select a valid to date !"); return;
            }
            if (_lstpcWrd != null)
            {
                mst_itm_pc_warr result = _lstpcWrd.Find(x => x.Pc_com == txtpcCom.Text && x.Pc_code == txtpc.Text && x.Pc_item_stus == ddlPcstatus.SelectedValue.ToString());
                if (result != null)
                {
                    DisplayMessage("This record already exist", 2);
                    return;
                }
            }
            else
            {
                _lstpcWrd = new List<mst_itm_pc_warr>();
            }

            mst_itm_pc_warr _itm = new mst_itm_pc_warr();
            _itm.Pc_com = txtwarrantycompany.Text;
            _itm.Pc_code = txtpcCom.Text;
            _itm.Pc_item_code = txtItem.Text.Trim().ToUpper();
            _itm.Pc_item_stus = ddlPcstatus.SelectedValue.ToString();
            _itm.Pc_item_st_des = ddlPcstatus.SelectedItem.Text;
            _itm.Pc_wara_period = Convert.ToInt32(ddlpcPrd.SelectedValue.ToString());
            _itm.Pc_wara_rmk = txtpcRem.Text;
            _itm.Pc_valid_from = Convert.ToDateTime(txtpcfrom.Text).Date;
            _itm.Pc_create_when = System.DateTime.Now;
            _itm.Pc_valid_to = Convert.ToDateTime(txtpcto.Text).Date;
            _itm.Pc_create_by = Session["UserID"].ToString();
            _itm.Pc_type = ddlPC_Ch_Com.SelectedValue;

            _lstpcWrd.Add(_itm);

            grdPcwarranty.DataSource = null;
            grdPcwarranty.AutoGenerateColumns = false;
            grdPcwarranty.DataSource = new List<mst_itm_pc_warr>();
            grdPcwarranty.DataSource = _lstpcWrd;
            grdPcwarranty.DataBind();


            ViewState["_lstpcWrd"] = _lstpcWrd;
            txtpcCom.Text = "";
            txtpc.Text = "";
            ddlPcstatus.SelectedIndex = -1;
            ddlpcPrd.SelectedIndex = -1;
            txtpcRem.Text = "";
        }

        protected void txtChaCom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtChaCom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtChaCom.Text.Trim());
                    if (com == null)
                    {
                        DisplayMessage("Invalid company", 2);
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

        protected void lbtnChacom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company8";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtChan_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnsrhchannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtwarrantycompany.Text == "")
                {
                    DisplayMessage("Select the company", 2);
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Loc_HIRC_Channel";
                UserPopoup.Show();
            }
            catch (Exception err)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddcannelWara_Click(object sender, EventArgs e)
        {
            _lstchannelWrd = ViewState["_lstchannelWrd"] as List<mst_itm_channlwara>;
            if (string.IsNullOrEmpty(txtChaCom.Text))
            {
                DisplayMessage("Enter company", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtChan.Text))
            {
                DisplayMessage("Enter channel", 2);
                return;
            }

            if (string.IsNullOrEmpty(ddlchanStatus.Text))
            {
                DisplayMessage("Enter item status", 2);
                return;
            }

            if (string.IsNullOrEmpty(ddlChanPrd.SelectedItem.Text))
            {
                DisplayMessage("Enter warranty period", 2);
                return;
            }

            if (_lstchannelWrd != null)
            {
                mst_itm_channlwara result = _lstchannelWrd.Find(x => x.Cw_com == txtChaCom.Text && x.Cw_channel == txtChan.Text && x.Cw_item_status == ddlchanStatus.SelectedValue.ToString());
                if (result != null)
                {
                    DisplayMessage("This record already exist", 2);
                    return;
                }
            }
            else
            { _lstchannelWrd = new List<mst_itm_channlwara>(); }


            mst_itm_channlwara _itm = new mst_itm_channlwara();

            _itm.Cw_item_code = txtItem.Text.Trim().ToUpper();
            _itm.Cw_channel = txtChan.Text;
            _itm.Cw_item_status = ddlchanStatus.SelectedValue.ToString();
            _itm.Cw_item_st_des = ddlchanStatus.SelectedItem.Text;
            _itm.Cw_warranty_prd = Convert.ToInt32(ddlChanPrd.SelectedValue.ToString());
            _itm.Cw_active = 1;
            _itm.Cw_com = txtChaCom.Text;
            _itm.Cw_create_by = Session["UserID"].ToString();
            _itm.Cw_modify_by = Session["UserID"].ToString();
            _lstchannelWrd.Add(_itm);

            grdcannelWara.DataSource = null;
            grdcannelWara.DataSource = new List<mst_itm_channlwara>();
            grdcannelWara.DataSource = _lstchannelWrd;
            grdcannelWara.DataBind();

            ViewState["_lstchannelWrd"] = _lstchannelWrd;
            txtChan.Text = "";
            ddlchanStatus.SelectedIndex = -1;
            ddlChanPrd.SelectedIndex = -1;
            ddlwarsdur.SelectedIndex = -1;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }

          //  string _isAutoCode_s = Session["_isAutoCode"].ToString();

      // //     _isAutoCode = Convert.ToBoolean(_isAutoCode);
     //       if (_isAutoCode == false)
     ///       {

      //          if (string.IsNullOrEmpty(txtItem.Text))
       //         {
         //           DisplayMessage("Enter Item code", 2);
         //           return;
         //       }
         //   }


            if (string.IsNullOrEmpty(txtSdes.Text)) { DisplayMessage("Enter short description", 2); return; }
            if (string.IsNullOrEmpty(txtLdes.Text)) { DisplayMessage("Enter long description", 2); return; }
            if (string.IsNullOrEmpty(txtModel.Text)) { DisplayMessage("Enter model", 2); return; }
            if (string.IsNullOrEmpty(txtBrand.Text)) { DisplayMessage("Enter brand", 2); return; }
            //if (string.IsNullOrEmpty(txtPartNo.Text)) { DisplayMessage("Enter part No", 2); return; }
            //  if (string.IsNullOrEmpty(txtPackCode.Text)) { DisplayMessage("Enter paking code", 2); return; }
            if (string.IsNullOrEmpty(txtUOM.Text)) { DisplayMessage("Enter UOM", 2); return; }
            if (string.IsNullOrEmpty(txtMainCat.Text)) { DisplayMessage("Enter main category", 2); return; }
            if (string.IsNullOrEmpty(txtCat1.Text)) { DisplayMessage("Enter sub category 1", 2); return; }
            if (string.IsNullOrEmpty(txtCat2.Text)) { DisplayMessage("Enter sub category 2", 2); return; }
            //if (string.IsNullOrEmpty(txtCat3.Text)) { DisplayMessage("Enter sub category 3", 2); return; }
            // if (string.IsNullOrEmpty(txtCat4.Text)) { DisplayMessage("Enter sub category 4", 2); return; }
            if (string.IsNullOrEmpty(ddlItemType.Text)) { DisplayMessage("Enter item type", 2); return; }
            //if (string.IsNullOrEmpty(txtCapacity.Text)) { DisplayMessage("Enter capacity", 2); return; }
            if (string.IsNullOrEmpty(txtColorExt.Text)) { DisplayMessage("Enter external color", 2); return; }
            if (string.IsNullOrEmpty(txtColorInt.Text)) { DisplayMessage("Enter internal color", 2); return; }
            if (string.IsNullOrEmpty(ddlPayType.Text)) { DisplayMessage("Enter pay type", 2); return; }
            if (string.IsNullOrEmpty(txtPrefix.Text)) { DisplayMessage("Enter prefix", 2); return; }
            if (string.IsNullOrEmpty(ddlStatus.Text)) { DisplayMessage("Enter status", 2); return; }
            if (string.IsNullOrEmpty(ddlSerilize.Text)) { DisplayMessage("Enter serialize status", 2); return; }
            if (string.IsNullOrEmpty(ddlWarranty.Text)) { DisplayMessage("Enter warranty status", 2); return; }
            if (string.IsNullOrEmpty(ddlChassis.Text)) { DisplayMessage("Enter chassis status", 2); return; }
            if (string.IsNullOrEmpty(ddlScanSub.Text)) { DisplayMessage("Enter suba serial status", 2); return; }
            // if (string.IsNullOrEmpty(ddlhpSalesAccept.Text)) { DisplayMessage("Enter registration status", 2); return; }
            //if (string.IsNullOrEmpty(ddlIsRegister.Text)) { DisplayMessage("Enter registration status", 2); return; }
            if (string.IsNullOrEmpty(txtPurComp.Text)) { DisplayMessage("Enter purchase company", 2); return; }
            // if (string.IsNullOrEmpty(ttxHsCode.Text)) { DisplayMessage("Enter HS code", 2); return; }
           // if (string.IsNullOrEmpty(txtCountry.Text)) { DisplayMessage("Enter country", 2); return; }
            if (string.IsNullOrEmpty(txtTaxStucture.Text)) { DisplayMessage("Enter tax structure", 2); return; }
            if (string.IsNullOrEmpty(txtMainWaraUOM.Text)) { DisplayMessage("Enter Main Warranty UOM", 2); return; }

            if (!validateinputString(txtItem.Text))
            {
                DisplayMessage("Invalid charactor found in Item Code.", 2);
                txtItem.Focus();
                return;
            }
            if (!validateinputString(txtBrand.Text))
            {
                DisplayMessage("Invalid charactor found in Brand Code.", 2);
                txtBrand.Focus();
                return;
            }
            if (!validateinputStringWithHash(txtModel.Text))
            {
                DisplayMessage("Invalid charactor found in Model Code.", 2);
                txtModel.Focus();
                return;
            }
            if (!validateinputString(txtUOM.Text))
            {
                DisplayMessage("Invalid charactor found in UOM Code.", 2);
                txtUOM.Focus();
                return;
            }
            if (!validateinputStringWithSpace(txtSdes.Text))
            {
                DisplayMessage("Invalid charactor found in Short Description.", 2);
                txtSdes.Focus();
                return;
            }
            if (!validateinputStringWithSpace(txtLdes.Text))
            {
                DisplayMessage("Invalid charactor found in Long Description.", 2);
                txtLdes.Focus();
                return;
            }
            if (!validateinputString(txtPartNo.Text))
            {
                DisplayMessage("Invalid charactor found in Part No.", 2);
                txtPartNo.Focus();
                return;
            }
            if (!validateinputString(txtMainCat.Text))
            {
                DisplayMessage("Invalid charactor found in Main Category Code.", 2);
                txtMainCat.Focus();
                return;
            }
            if (!validateinputString(txtCat1.Text))
            {
                DisplayMessage("Invalid charactor found in Sub Level 1 Code.", 2);
                txtCat1.Focus();
                return;
            }
            if (!validateinputString(txtCat2.Text))
            {
                DisplayMessage("Invalid charactor found in Sub Level 2 Code.", 2);
                txtCat2.Focus();
                return;
            }
            if (!validateinputString(txtCat3.Text))
            {
                DisplayMessage("Invalid charactor found in Sub Level 3 Code.", 2);
                txtCat3.Focus();
                return;
            }
            //if (!validateinputString(txtCat4.Text))
            //{
            //    DisplayMessage("Invalid charactor found in Sub Level 4 Code.", 2);
            //    txtCat4.Focus();
            //    return;
            //}
            CollectMaster();
            _lstchannelWrd = ViewState["_lstchannelWrd"] as List<mst_itm_channlwara>;
            _lstpcWrd = ViewState["_lstpcWrd"] as List<mst_itm_pc_warr>;
            _lstitmWrd = ViewState["_lstitmWrd"] as List<MasterItemWarrantyPeriod>;
            _lstitmPrd = ViewState["_lstitmPrd"] as List<mst_itm_sevpd>;
            _lsttaxClaim = ViewState["_lsttaxClaim"] as List<MasterItemTaxClaim>;
            _lstcont = ViewState["_lstcont"] as List<mst_itm_container>;
            _lstkit = ViewState["_lstkit"] as List<MasterItemComponent>;
            _lstreplace = ViewState["_lstreplace"] as List<mst_itm_replace>;
            _lstmrn = ViewState["_lstmrn"] as List<mst_itm_mrn_com>;
            _lstredCom = ViewState["_lstredCom"] as List<mst_itm_redeem_com>;
            _lstsupItem = ViewState["_lstsupItem"] as List<BusEntityItem>;
            _lstcusItem = ViewState["_lstcusItem"] as List<BusEntityItem>;
            _lstcomItem = ViewState["_lstcomItem"] as List<MasterCompanyItem>;
            _lstreorder = ViewState["_lstreorder"] as List<mst_itm_com_reorder>;
            _lstfg_cost = ViewState["_lstfg_cost"] as List<mst_itm_fg_cost>;
            _itemPrefix = ViewState["_itemPrefix"] as List<ItemPrefix>;
            string _err;

            #region Add by Lakshan All component item cost methode as Precentage 06 Oct 2016 as per the christina
            if (_lstkit != null)
            {
                var _perList = _lstkit.Where(c => c.MICP_IS_PERCENTAGE == 1).ToList();
                if (_perList.Count > 0)
                {
                    decimal _totPrec = _lstkit.Sum(c => c.Micp_cost_percentage);
                    if (_totPrec > 100)
                    {
                        DispMsg("Main item cost precentage is exceed in KIT SETUP !"); return;
                    }
                }
            }
            #endregion

            // _lstcomItem = null;
            _item.Tmp_user_id = Session["UserID"].ToString();

            //Added New 2018/May/10 Residual Value
            //txtResidual
            //txtResidual.Text
            if(!(txtResidual.Text.Equals(string.Empty)))
            { 
            _item.MI_RESIDUAL_VAL = txtResidual.Text;
            }
            int row_aff = CHNLSVC.General.SaveItemMaster(_item, _lstchannelWrd, _lstpcWrd, _lstitmWrd, null, _lsttaxClaim, _lstcont, _lstkit, _lstreplace, _lstmrn, _lstredCom, _lstsupItem, _lstcusItem, _lstcomItem, _lstreorder, _lstfg_cost, _itemPrefix, _isAutoCode, Session["UserCompanyCode"].ToString(), out _err);
            if (row_aff == 1)
            {
                DisplayMessage(_err, 3);

                pageClear();
                loadDefault();
            }
            else
            {
                string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                DisplayMessage(Msg, 2);
            }
        }

        protected void txtbreath_TextChanged(object sender, EventArgs e)
        {
            if (txtwidth.Text == "")
            {
                txtwidth.Text = "0";
            }
            if (txthight.Text == "")
            {
                txthight.Text = "0";
            }
            if (txtbreath.Text == "")
            {
                txtbreath.Text = "0";
            }
            decimal volu = Convert.ToDecimal(txtwidth.Text) * Convert.ToDecimal(txthight.Text) * Convert.ToDecimal(txtbreath.Text);
            txtwarehousvolum.Text = volu.ToString();
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
        protected void lbtnbooktype_Click(object sender, EventArgs e)
        {
            txtpagecount.Text = string.Empty;
            txtbookprefix.Text = string.Empty;
            txtpreDescription.Text = string.Empty;
            prefixpopup.Show();
        }

        protected void lbtnAddPrefix_Click(object sender, EventArgs e)
        {
            _itemPrefix = ViewState["_itemPrefix"] as List<ItemPrefix>;
            if (string.IsNullOrEmpty(txtbookprefix.Text))
            {
                DisplayMessage("Enter Prefix", 2);
                prefixpopup.Show();
                return;
            }

            if (string.IsNullOrEmpty(txtpreDescription.Text))
            {
                DisplayMessage("Enter Descriptions", 2);
                prefixpopup.Show();
                return;
            }

            if (string.IsNullOrEmpty(ddlprfix.Text))
            {
                DisplayMessage("Enter Prefix status", 2);
                prefixpopup.Show();
                return;
            }



            if (_itemPrefix != null)
            {
                ItemPrefix result = _itemPrefix.Find(x => x.MIP_COM == ddlItemcompany.SelectedItem.Text && x.MIP_CD == txtItem.Text.Trim().ToUpper() && x.MI_PREFIX == txtbookprefix.Text);
                if (result != null)
                {
                    DisplayMessage("This record already exist", 2);
                    prefixpopup.Show();
                    return;
                }
            }
            else
            { _itemPrefix = new List<ItemPrefix>(); }


            ItemPrefix _itm = new ItemPrefix();

            _itm.MIP_COM = ddlItemcompany.SelectedItem.Text;
            _itm.MIP_CD = txtItem.Text.Trim().ToUpper();
            _itm.MI_PREFIX = txtbookprefix.Text;
            _itm.MI_MOD_SESSION = Session["SessionID"].ToString();
            _itm.MI_MOD_DT = System.DateTime.Now;
            _itm.MI_MOD_BY = Session["UserID"].ToString();
            _itm.MI_DESC = txtpreDescription.Text;
            _itm.MI_CRE_SESSION = Session["SessionID"].ToString();
            _itm.MI_CRE_DT = System.DateTime.Now;
            _itm.MI_CRE_BY = Session["UserID"].ToString();
            _itm.MI_ACT = ddlprfix.SelectedItem.Text == "YES" ? 1 : 0;
            _itm.MI_ACTIVE_STATUS = ddlprfix.SelectedItem.Text;
            _itemPrefix.Add(_itm);

            grdprefix.DataSource = null;
            grdprefix.DataSource = new List<ItemPrefix>();
            grdprefix.DataSource = _itemPrefix;
            grdprefix.DataBind();

            ViewState["_itemPrefix"] = _itemPrefix;
            txtbookprefix.Text = "";
            txtpreDescription.Text = "";
            ddlprfix.SelectedIndex = -1;

            prefixpopup.Show();
        }

        protected void ddlbooktype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbooktype.SelectedItem.Text == "YES")
            {
                lbtnbooktype.Visible = true;
                return;
            }
            lbtnbooktype.Visible = false;
        }

        protected void ddlprefixallow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlprefixallow.SelectedItem.Text == "YES")
            {
                pnlmultipleprefix.Visible = true;
                prefixpopup.Show();
                return;
            }
            pnlmultipleprefix.Visible = false;
            prefixpopup.Show();
        }

        protected void ddlPC_Ch_Com_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPC_Ch_Com.SelectedValue.ToString() == "COM")
            {
                lblPC_Ch_Com.Text = "Company";
                lbtnsrhchannel.Visible = false;
                lbtnChacom.Visible = true;
                lbtnSearchpc.Visible = false;
            }
            else if (ddlPC_Ch_Com.SelectedValue.ToString() == "CHNL")
            {
                lblPC_Ch_Com.Text = "Channel";
                lbtnsrhchannel.Visible = true;
                lbtnChacom.Visible = false;
                lbtnSearchpc.Visible = false;
            }
            else if (ddlPC_Ch_Com.SelectedValue.ToString() == "PC")
            {
                lblPC_Ch_Com.Text = "PC";
                lbtnsrhchannel.Visible = false;
                lbtnChacom.Visible = false;
                lbtnSearchpc.Visible = true;
            }
        }

        protected void lbtnviewwarranty_Click(object sender, EventArgs e)
        {
            _lstitmWrd = ViewState["_lstitmWrd"] as List<MasterItemWarrantyPeriod>;
            if (_lstitmWrd != null)
            {
                _lstitmWrd = _lstitmWrd.Where(c => c.Mwp_act == true).ToList();
            }
            grdWarranty.DataSource = null;
            grdWarranty.DataSource = new List<MasterItemWarrantyPeriod>();
            grdWarranty.DataSource = _lstitmWrd;
            grdWarranty.DataBind();
            ViewState["_lstitmWrd"] = _lstitmWrd;
            itemwarrantypopup.Show();
        }

        protected void lbtnMultipleCom_Click(object sender, EventArgs e)
        {
            chklstbox.Items.Clear();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
            foreach (DataRow drow in _result.Rows)
            {
                Session["Multiplecom"] = "true";
                chklstbox.Items.Add(drow["Code"].ToString());

            }
            MultipleCom.Show();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblalert.Visible = false;
            if (fileupexcelupload.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                {

                    lblalert.Visible = true;
                    lblalert.Text = "Please select a valid excel (.xls) file";
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
                Import_To_Grid(FilePath, Extension, null);
            }
            else
            {
                lblalert.Visible = true;
                lblalert.Text = "Please select the correct upload file path";
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

        public string ConnectionString2(string FileName, string Header)
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

        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtItem.Text))
            //{
            //    DisplayMessage("Please select the Item code",2);
            //    return;
            //}

            DisplayMessage("Please unselect blank cell/cells which you have selected in excel sheet", 1);
            lblalert.Text = "";
            lblsuccess.Text = "";
            lblsuccess.Visible = false;
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
            excelUpload.Show();
        }

        protected void btnprocess_Click(object sender, EventArgs e)
        {
            string _err;
            //DataTable[] GetExecelTbl = LoadData(Session["FilePath"].ToString());
            DataTable[] GetExecelTbl = LoadData2(Session["FilePath"].ToString());
            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                    List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                    oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());

                    //var duplicates = GetExecelTbl[0].AsEnumerable()
                    //    .GroupBy(i => new { status = i.Field<string>("itemStatus").Trim(), code = i.Field<string>("itemcode").Trim() })
                    //    .Where(g => g.Count() > 1)
                    //    .Select(g => new { g.Key.status, g.Key.code }).ToList();
                    //if (duplicates.Count > 0)
                    //{
                    //    DisplayMessage("This record already exist", 2);
                    //    lblalert.Visible = true;
                    //    lblalert.Text = "This record already exist - Item :" + duplicates[0].code.ToString() + " Status :" + duplicates[0].status.ToString();
                    //    excelUpload.Show();
                    //    return;
                    //}
                    for (int i = 0; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        try
                        {
                            _lstitmWrd = ViewState["_lstitmWrd"] as List<MasterItemWarrantyPeriod>;
                            MasterItemWarrantyPeriod _itm = new MasterItemWarrantyPeriod();
                            if (_lstitmWrd == null)
                            {
                                _lstitmWrd = new List<MasterItemWarrantyPeriod>();
                            }
                            //if (_lstitmWrd != null)
                            //{
                            //    MasterItemWarrantyPeriod result = _lstitmWrd.Find(x => x.Mwp_itm_stus == GetExecelTbl[0].Rows[i][0].ToString() && x.Mwp_itm_cd == GetExecelTbl[0].Rows[i][7].ToString());
                            //    if (result != null)
                            //    {
                            //        DisplayMessage("This record already exist", 2);
                            //        lblalert.Visible = true;
                            //        lblalert.Text = "This record already exist - " + GetExecelTbl[0].Rows[i][7].ToString();
                            //        excelUpload.Show();
                            //        return;
                            //    }
                            //}
                            //else
                            //{
                            //    _lstitmWrd = new List<MasterItemWarrantyPeriod>();
                            //}

                            _itm.Mwp_itm_stus = GetExecelTbl[0].Rows[i][0].ToString().Trim();
                            _itm.Mwp_itm_cd = GetExecelTbl[0].Rows[i][7].ToString().Trim();//txtItem.Text; //30/05/2017


                            _itm.Mwp_item_st_des = oItemStaus.Find(x => x.Mis_cd == _itm.Mwp_itm_stus).Mis_desc.Trim();

                            // _itm.Mwp_item_st_des = ddlwStatus.SelectedItem.Text;
                            _itm.Mwp_val = Convert.ToInt32(GetExecelTbl[0].Rows[i][2].ToString().Trim());
                            _itm.Mwp_sup_warranty_prd = (GetExecelTbl[0].Rows[i][4] == null || GetExecelTbl[0].Rows[i][4].ToString() == "") ? 0 : Convert.ToInt32(GetExecelTbl[0].Rows[i][4].ToString().Trim());
                            _itm.Mwp_warr_tp = GetExecelTbl[0].Rows[i][1].ToString().Trim();

                            _itm.Mwp_rmk = GetExecelTbl[0].Rows[i][3].ToString().Trim();
                            _itm.Mwp_effect_dt = Convert.ToDateTime(GetExecelTbl[0].Rows[i][6].ToString().Trim());//dtpEffectiveDate.Value.Date;

                            _itm.Mwp_sup_wara_rem = (GetExecelTbl[0].Rows[i][5] == null || GetExecelTbl[0].Rows[i][5].ToString() == "") ? string.Empty : GetExecelTbl[0].Rows[i][5].ToString().Trim();
                            _itm.Mwp_cre_by = Session["UserID"].ToString();
                            _itm.Mwp_cre_dt = DateTime.Now;
                            _itm.Mwp_mod_by = Session["UserID"].ToString();
                            _itm.Mwp_mod_dt = DateTime.Now;
                            _itm.Mwp_act = true;
                            _itm.Mwp_def = true;
                            _lstitmWrd.Add(_itm);
                            ViewState["_lstitmWrd"] = _lstitmWrd;
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                            lblalert.Visible = true;
                            lblalert.Text = "Excel Data Invalid Please check Excel File and Upload " + ex.Message;
                            excelUpload.Show();
                        }
                    }
                    /*
                    grdWarranty.DataSource = null;
                    grdWarranty.DataSource = new List<MasterItemWarrantyPeriod>();
                    grdWarranty.DataSource = _lstitmWrd;
                    grdWarranty.DataBind();
                     * */

                    _lstpcWrd = null;
                    _lstitmWrd = ViewState["_lstitmWrd"] as List<MasterItemWarrantyPeriod>;

                    List<MasterItemWarrantyPeriod> duplicates = _lstitmWrd.GroupBy(i => new { i.Mwp_itm_cd, i.Mwp_itm_stus })
                  .Where(g => g.Count() > 1)
                  .Select(g => g.First()).ToList();

                    if (duplicates.Count > 0)
                    {
                        DisplayMessage("This record already exist", 2);
                        lblalert.Visible = true;
                        lblalert.Text = "This record already exist - " + duplicates[0].Mwp_itm_cd.ToString();
                        excelUpload.Show();
                        return;
                    }
                    int row_aff = CHNLSVC.General.saveItemWarrenty(_lstpcWrd, _lstitmWrd, out _err);
                    if (row_aff == 1)
                    {
                        DisplayMessage("Successfully saved the uploaded excel sheet data...!", 3);
                        //lblsuccess.Visible = true;

                        lblalert.Visible = true;
                        lblalert.Text = "Successfully saved the uploaded excel sheet data...!";
                        excelUpload.Show();
                        return;
                    }
                    else
                    {
                        string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                        //DisplayMessage(Msg, 2);
                        lblalert.Visible = true;
                        lblalert.Text = Msg;
                        excelUpload.Show();
                        return;
                    }

                    lblalert.Visible = false;
                    divUpcompleted.Visible = false;
                    excelUpload.Show();
                    ViewState["_lstitmWrd"] = null;
                    _lstitmWrd = null;
                }
            }
        }
        protected void btnprocess2_Click(object sender, EventArgs e)
        {
            string _err;
            // DataTable[] GetExecelTbl = Import_To_Grid(Session["FilePath"].ToString(), ".xls", "Yes");
            DataTable[] GetExecelTbl2 = LoadData2(Session["FilePath"].ToString());
            if (GetExecelTbl2 != null)
            {
                if (GetExecelTbl2[0].Rows.Count > 0)
                {
                    // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                    for (int i = 0; i < GetExecelTbl2[0].Rows.Count; i++)
                    {
                        try
                        {
                            _lstpcWrd = ViewState["_lstpcWrd"] as List<mst_itm_pc_warr>;
                            mst_itm_pc_warr _itm = new mst_itm_pc_warr();

                            if (_lstpcWrd != null)
                            {
                                //mst_itm_pc_warr result = _lstpcWrd.Find(x => x.Pc_com == GetExecelTbl2[0].Rows[i][0].ToString() && x.Pc_code == GetExecelTbl2[0].Rows[i][1].ToString() && x.Pc_item_stus == GetExecelTbl2[0].Rows[i][2].ToString());
                                mst_itm_pc_warr result = _lstpcWrd.Find(x => x.Pc_com == Session["UserDefProf"].ToString() && x.Pc_code == GetExecelTbl2[0].Rows[i][0].ToString() && x.Pc_item_stus == GetExecelTbl2[0].Rows[i][2].ToString());
                                if (result != null)
                                {
                                    DisplayMessage("This record already exist", 2);
                                    lblalert2.Visible = true;
                                    lblalert2.Text = "This record already exist";
                                    exceluploadCompanywarranty.Show();
                                    return;
                                }
                            }
                            else
                            {
                                _lstpcWrd = new List<mst_itm_pc_warr>();
                            }
                            _itm.Pc_com = txtwarrantycompany.Text;//GetExecelTbl2[0].Rows[i][0].ToString(); //remove excel read this column 30/05/2017
                            _itm.Pc_code = GetExecelTbl2[0].Rows[i][0].ToString();//GetExecelTbl2[0].Rows[i][1].ToString(); //remove excel read this column 30/05/2017
                            _itm.Pc_item_code = GetExecelTbl2[0].Rows[i][1].ToString();//txtItem.Text; //remove excel read this column 30/05/2017
                            _itm.Pc_item_stus = GetExecelTbl2[0].Rows[i][2].ToString();

                            List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                            oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                            _itm.Pc_item_st_des = oItemStaus.Find(x => x.Mis_cd == _itm.Pc_item_stus).Mis_desc;
                            //_itm.Pc_item_st_des = ddlPcstatus.SelectedItem.Text;

                            _itm.Pc_wara_period = Convert.ToInt32(GetExecelTbl2[0].Rows[i][3].ToString());
                            _itm.Pc_wara_rmk = GetExecelTbl2[0].Rows[i][4].ToString(); ;
                            _itm.Pc_valid_from = Convert.ToDateTime(GetExecelTbl2[0].Rows[i][5].ToString());
                            _itm.Pc_create_when = System.DateTime.Now;
                            _itm.Pc_valid_to = Convert.ToDateTime(GetExecelTbl2[0].Rows[i][6].ToString());
                            _itm.Pc_create_by = Session["UserID"].ToString();
                            _itm.Pc_type = ddlPC_Ch_Com.SelectedValue;//GetExecelTbl2[0].Rows[i][7].ToString(); //remove excel read this column 30/05/2017
                            _lstpcWrd.Add(_itm);
                            ViewState["_lstpcWrd"] = _lstpcWrd; //add because want to add more to grid 30/05/2017
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                            lblalert2.Visible = true;
                            lblalert2.Text = "Excel Data Invalid Please check Excel File and Upload " + ex.Message;
                            exceluploadCompanywarranty.Show();
                        }
                    }

                    //remove excel read this column 30/05/2017
                    /*
                    grdPcwarranty.DataSource = null;
                    grdPcwarranty.DataSource = new List<mst_itm_pc_warr>();
                    grdPcwarranty.DataSource = _lstpcWrd;
                    grdPcwarranty.DataBind();
                     * */

                    //save DB 30/05/2017
                    _lstitmWrd = null;
                    _lstpcWrd = ViewState["_lstpcWrd"] as List<mst_itm_pc_warr>;
                    int row_aff = CHNLSVC.General.saveItemWarrenty(_lstpcWrd, _lstitmWrd, out _err);
                    if (row_aff == 1)
                    {
                        lblsuccess2.Text = "Successfully saved the uploaded excel sheet data...!";
                        lblsuccess2.Visible = true;
                    }
                    else
                    {
                        string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                        DisplayMessage(Msg, 2);
                    }
                    lblalert2.Visible = false;
                    divUpcompleted2.Visible = false;
                    exceluploadCompanywarranty.Show();
                    _lstpcWrd = new List<mst_itm_pc_warr>();
                    ViewState["_lstpcWrd"] = null;
                }
            }
        }

        protected void lbtnExceluploadcompWar_Click(object sender, EventArgs e)
        {
            //remove excel read this column 30/05/2017
            //if (string.IsNullOrEmpty(txtItem.Text))
            //{
            //    DisplayMessage("Please select the Item code", 2);
            //    return;
            //}

            DisplayMessage("Please unselect blank cell/cells which you have selected in excel sheet", 1);
            lblalert2.Text = "";
            lblsuccess2.Text = "";
            lblsuccess2.Visible = false;
            lblalert2.Visible = false;
            divUpcompleted2.Visible = false;

            exceluploadCompanywarranty.Show();
        }

        protected void txtwarrantycompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtwarrantycompany.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtwarrantycompany.Text.Trim());
                    if (com == null)
                    {
                        DisplayMessage("Invalid company", 2);
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

        protected void lbtnwarrantycompany_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company9";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void btnUpload2_Click(object sender, EventArgs e)
        {
            lblalert2.Visible = false;
            if (string.IsNullOrEmpty(txtwarrantycompany.Text))
            {
                lblalert2.Visible = true;
                lblalert2.Text = "Please select warranty company before excel upload...!!!";
                exceluploadCompanywarranty.Show();
                return;
            }
            if (fileupexcelupload2.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload2.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload2.PostedFile.FileName);

                if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                {
                    lblalert2.Visible = true;
                    lblalert2.Text = "Please select a valid excel (.xls or .xlsx) file";
                    exceluploadCompanywarranty.Show();
                    return;
                }
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //LoadData(FolderPath + FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                fileupexcelupload2.SaveAs(FilePath);
                Session["FilePath"] = FilePath;
                divUpcompleted2.Visible = true;
                DisplayMessage("Excel file upload completed. Do you want to process?", 1);
                exceluploadCompanywarranty.Show();
                Import_To_Grid(FilePath, Extension, null); // uncomment check from QA 27/05/2017
            }
            else
            {
                lblalert2.Visible = true;
                lblalert2.Text = "Please select the correct upload file path";
                DisplayMessage("Please select the correct upload file path", 2);
                exceluploadCompanywarranty.Show();
                // divalert.Visible = true;
                // lblalert.Text = "Please select an excel file";
            }
        }
        public DataTable[] LoadData(string FileName)
        {
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
                    cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7 From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Tax);



                }
                catch (Exception ex)
                {

                    lblalert.Visible = true;
                    lblalert.Text = "Invalid data found from the excel sheet. Please check data ...";
                    excelUpload.Show();
                    return new DataTable[] { Tax };

                }
                return new DataTable[] { Tax };
            }
        }
        public DataTable[] LoadData2(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();
            using (OleDbConnection cn2 = new OleDbConnection { ConnectionString = ConnectionString(FileName, "Yes") })
            {
                try
                {
                    cn2.Open();
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dtExcelSchema;
                    cmdExcel.Connection = cn2;

                    dtExcelSchema = cn2.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    cn2.Close();

                    //Read Data from First Sheet
                    cn2.Open();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Tax);



                }
                catch (Exception ex)
                {

                    lblalert.Visible = true;
                    lblalert.Text = "Invalid data found from the excel sheet. Please check data...." + ex.Message;
                    excelUpload.Show();
                    return new DataTable[] { Tax };

                }
                return new DataTable[] { Tax };
            }
        }
        private DataTable[] Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            DataTable dt = new DataTable();
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls":
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx":
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                    case ".XLS":
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".XLSX":
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();


                cmdExcel.Connection = connExcel;
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();


                connExcel.Open();
                cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8 From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);

                string sessiontype1 = string.Empty;
                string sessiontype2 = string.Empty;

                sessiontype1 = (string)Session["ROUTES"];
                sessiontype2 = (string)Session["EXCEL_SCH"];



                connExcel.Close();
            }
            catch (Exception ex)
            {
                return new DataTable[] { dt };
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            return new DataTable[] { dt };
        }

        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Modified by Kelum : 2016-June-16

            //if (ddlItemType.SelectedIndex == 6)
            //{
            //    ddlStcokMain.SelectedIndex = 0;
            //}
            if (ddlItemType.SelectedValue == "M")
            {
                ddlStcokMain.SelectedValue = "YES";
            }
            else if (ddlItemType.SelectedValue == "V")
            {
                ddlStcokMain.SelectedIndex = ddlStcokMain.Items.IndexOf(ddlStcokMain.Items.FindByValue("NO"));
            }
            else
            {
                ddlStcokMain.SelectedIndex = 1;
            }
        }

        protected void chkSave_CheckedChanged(object sender, EventArgs e)
        {
            txtLdes.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtUOM.Text = string.Empty;
            //txtCountry.Text = string.Empty;
            txtMainCat.Text = string.Empty;
            txtCat1.Text = string.Empty;
            txtCat2.Text = string.Empty;
            txtCat3.Text = string.Empty;
            //txtCat4.Text = string.Empty;
            txtwuom.Text = string.Empty;
            txtgross.Text = string.Empty;
            txtnet.Text = string.Empty;
            txtduom.Text = string.Empty;
            txthight.Text = string.Empty;
            txtwidth.Text = string.Empty;
            txtbreath.Text = string.Empty;
            txtwarehousvolum.Text = string.Empty;
            txttrimLeft.Text = string.Empty;
            txttrimRight.Text = string.Empty;
            txtPrefix.Text = ".";


            ddlItemType.SelectedIndex = -1;
            txtColorExt.Text = "N/A";
            //grdKit.DataSource = new List<MasterItemComponent>();
     //       grdKit.DataBind();
            ViewState["_lstkit"] = "";
            //grdRepalced.DataSource = new List<mst_itm_replace>();
            //grdRepalced.DataBind();
            ViewState["_lstreplace"] = "";
            _lstsupItem = new List<BusEntityItem>();
            grdSupplier.DataSource = _lstsupItem;
            grdSupplier.DataBind();
            ViewState["_lstsupItem"] = _lstsupItem;
            _lstpcWrd = new List<mst_itm_pc_warr>();
            ViewState["_lstpcWrd"] = _lstpcWrd;
            grdPcwarranty.DataSource = _lstpcWrd;
            grdPcwarranty.DataBind();
        }

        protected void ddlChassis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChassis.SelectedValue == "2")
            {
                lbtnbooktype.Visible = true;
            }
            else
            {
                lbtnbooktype.Visible = false;
            }
        }

        protected void txtwidth_TextChanged(object sender, EventArgs e)
        {
            if (txtwidth.Text == "")
            {
                txtwidth.Text = "0";
            }
            if (txthight.Text == "")
            {
                txthight.Text = "0";
            }
            if (txthight.Text == "")
            {
                txthight.Text = "0";
            }
            if (string.IsNullOrEmpty(txtbreath.Text))
            {
                txtbreath.Text = "0";
            }
            decimal volu = Convert.ToDecimal(txtwidth.Text) * Convert.ToDecimal(txthight.Text) * Convert.ToDecimal(txtbreath.Text);
            txtwarehousvolum.Text = volu.ToString();
        }

        protected void txthight_TextChanged(object sender, EventArgs e)
        {
            if (txtwidth.Text == "")
            {
                txtwidth.Text = "0";
            }
            if (txthight.Text == "")
            {
                txtwidth.Text = "0";
            }
            if (txthight.Text == "")
            {
                txtbreath.Text = "0";
            }
            if (string.IsNullOrEmpty(txtbreath.Text))
            {
                txtbreath.Text = "0";
            }
            decimal volu = Convert.ToDecimal(txtwidth.Text) * Convert.ToDecimal(txthight.Text) * Convert.ToDecimal(txtbreath.Text);
            txtwarehousvolum.Text = volu.ToString();
        }

        protected void txtSdes_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtSdes.Text))
            {
                DisplayMessage("Invalid charactor found in short description.", 2);
                txtSdes.Focus();
                return;
            }
            txtLdes.Text = txtSdes.Text;
        }

        protected void lbtntxD_Click(object sender, EventArgs e)
        {
            List<mst_itm_tax_structure_det> _lstTaxDet = new List<mst_itm_tax_structure_det>();
            //List<mst_itm_tax_structure_hdr> _lstTaxhdr = new List<mst_itm_tax_structure_hdr>();
            //_lstTaxhdr = CHNLSVC.General.GetItemTaxStructureHeader(_code);
            _lstTaxDet = CHNLSVC.General.getItemTaxStructure(txtTaxStucture.Text);
            _lstTaxDet = _lstTaxDet.OrderBy(c => c.Its_com).ToList();
            if (_lstTaxDet != null && _lstTaxDet.Count > 0)
            {

                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                if (_lstTaxDet != null)
                {
                    foreach (mst_itm_tax_structure_det itemSer in _lstTaxDet)
                    {
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            var v = oItemStaus.Find(x => x.Mis_cd == itemSer.Its_stus);
                            if (v != null)
                            {
                                itemSer.Its_stus_Des = v.Mis_desc;
                            }
                            else
                            {
                                itemSer.Its_stus_Des = itemSer.Its_stus;
                            }
                        }
                    }
                }
                grdTax.DataSource = null;
                grdTax.DataSource = new List<mst_itm_tax_structure_det>();
                grdTax.DataSource = _lstTaxDet;
                grdTax.DataBind();
                taxDetailspopup.Show();
            }
            else
            {
                DisplayMessage("Invalid Tax structure", 2);
                //MessageBox.Show("Invalid Tax structure", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTaxStucture.Text = "";
                txtTaxStucture.Focus();
            }
        }

        protected void lbtnMultiplestatus_Click(object sender, EventArgs e)
        {

            if (Session["chkfirst"].ToString() == "")
            {
                DataTable grdMultiplestatusss = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                //foreach (DataRow drow in _status.Rows)
                //{
                Session["Multiplestatus"] = "true";
                //    chkMultiplestatus.Items.Add(drow["MIS_DESC"].ToString());

                //}
                //grdMultiplestatus.DataSource = grdMultiplestatus;
                //grdMultiplestatus.DataBind();
                grdMultiplestatus.DataSource = grdMultiplestatusss;
                grdMultiplestatus.DataBind();
                Session["chkfirst"] = "bind";
            }

            Multiplestatus.Show();


        }

        protected void btnClose2_Click(object sender, EventArgs e)
        {
            lblalert2.Text = "";
            lblsuccess2.Text = "";
            lblsuccess2.Visible = false;
            lblalert2.Visible = false;
            divUpcompleted2.Visible = false;
        }
        protected void btnClose3_Click(object sender, EventArgs e)
        {
            lblalert.Text = "";
            lblsuccess.Text = "";
            lblsuccess.Visible = false;
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
        }

        protected void ddlwStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Multiplestatus"] = "";
        }

        public void IsDiscontinuedModel()
        {

            MasterItemModel _model = new MasterItemModel();
            _model = CHNLSVC.General.GetItemModel(txtModel.Text).FirstOrDefault();
            if (_model != null)
            {
                txtTaxStucture.Text = _model.Mm_taxstruc_cd;

                if (_model.Mm_is_dis)
                {
                    // DisplayMessage("Model is discontinue,do you need to proceed", 2);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + "Discontinue model!!" + "');", true);
                }
            }

        }

        public void LoadModelDetails()
        {

            MasterItemModel _model = new MasterItemModel();
            _model = CHNLSVC.General.GetItemModel(txtModel.Text).FirstOrDefault();
            if (_model != null)
            {
                txtTaxStucture.Text = _model.Mm_taxstruc_cd;

            }

        }

        //public void LoadCompanyByModel()
        //{
        //    List<mst_commodel> _comModel = new List<mst_commodel>();
        //    _comModel = CHNLSVC.General.GetCompanyByModel(txtModel.Text);
        //    if (_comModel != null)
        //    {
        //        grdComItem.DataSource = null;
        //        grdComItem.DataSource = new List<mst_commodel>();
        //        grdComItem.DataSource = _comModel;
        //        grdComItem.DataBind();
        //    }

        //}

        //protected void ChkStatusall_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow item in grdMultiplestatus.Rows)
        //    {

        //         CheckBox chkMultiplestatus = item.FindControl("chkMultiplestatus") as CheckBox;
        //         if (ChkStatusall.Checked)
        //         {
        //             chkMultiplestatus.Checked = true;
        //         }
        //         else
        //         {
        //             chkMultiplestatus.Checked = true;
        //         }




        //    }
        //}

        protected void txtMainWaraUOM_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMainWaraUOM.Text))
            {
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtMainWaraUOM.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid UOM", 2);
                txtMainWaraUOM.Text = string.Empty;
                txtMainWaraUOM.Focus();
                return;
            }
        }

        protected void lbtnsrhMainwarauom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "MainWarrantyUOM";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void ddlCostMeth_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lblCost.Text = "Cost";
        //    if (ddlCostMeth.SelectedIndex > 0)
        //    {
        //        lblCost.Text = ddlCostMeth.SelectedValue == "1" ? "Cost %" : "Cost";
        //    }
        //}

        protected void lbtndelete_Click(object sender, EventArgs e)
        {
            _lstreorder2 = ViewState["_lstreorder"] as List<mst_itm_com_reorder>;
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label COM = (Label)row.FindControl("col_Icr_com_code");
            var itemToRemove = _lstreorder2.Single(r => r.Icr_com_code == COM.Text.ToString());
            _lstreorder2.Remove(itemToRemove);

            grdReorder.DataSource = _lstreorder2;
            grdReorder.DataBind();
        }

        protected void ChkStatusall_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow Item in grdMultiplestatus.Rows)
            {
                CheckBox chkMultiplestatus = Item.FindControl("chkMultiplestatus") as CheckBox;
                if (ChkStatusall.Checked == true)
                {
                    chkMultiplestatus.Checked = true;
                }
                else
                {
                    chkMultiplestatus.Checked = false;
                }

            }
            Multiplestatus.Show();
        }

        protected void lbtnSePackCd_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                ViewState["SEARCH"] = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "ProductionPlan";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtPackCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPackCode.Text))
            {
                if (!validateinputString(txtPackCode.Text))
                {
                    DisplayMessage("Invalid charactor found in Packing Material code.", 2);
                    txtPackCode.Focus();
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, "ITEM", txtPackCode.Text.Trim().ToUpper());

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == txtPackCode.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Invalid packing material code", 2);
                    txtPackCode.Text = string.Empty;
                    txtPackCode.Focus();
                    return;
                }
            }
        }

        //protected void txtunits_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(txtunits.Text))
        //        {
        //            if (string.IsNullOrEmpty(txtkitItem.Text))
        //            {
        //                DispMsg("Please enter item code !"); txtkitItem.Focus(); return;
        //            }
        //            bool _isDecimalAllow = false;
        //            MasterItem _mstItem = CHNLSVC.General.GetItemMaster(txtkitItem.Text.Trim());
        //            if (_mstItem != null)
        //            {
        //                List<MasterUOM> _uomList = CHNLSVC.General.GetItemUOM();
        //                MasterUOM _uom = _uomList.Where(c => c.Msu_cd == _mstItem.Mi_itm_uom).FirstOrDefault();
        //                if (_uom != null)
        //                {
        //                    if (_uom.Msu_isdecimal == 1)
        //                    {
        //                        _isDecimalAllow = true;
        //                    }
        //                }
        //            }
        //            decimal _units = 0, _tmpUnits = 0, _val = 0, _desAv = 0;
        //            _units = decimal.TryParse(txtunits.Text, out _tmpUnits) ? Convert.ToDecimal(txtunits.Text) : 0;
        //            _desAv = _units % 1;
        //            if (!_isDecimalAllow)
        //            {
        //                if (!(_desAv == 0))
        //                {
        //                    DispMsg("Please enter valid no of units !"); txtunits.Text = ""; txtunits.Focus(); return;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DispMsg(ex.Message, "E");
        //    }
        //}

        protected void lbtnGetCostData_Click(object sender, EventArgs e)
        {
            try
            {
                grdItemCost.DataSource = new int[] { };
                grdItemCost.DataBind();
                List<mst_itm_com_reorder> _costData = CHNLSVC.MsgPortal.GetItemMasterCostDataByItem(txtItem.Text.Trim().ToUpper());

                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                if (_costData != null)
                {
                    foreach (mst_itm_com_reorder itemSer in _costData)
                    {
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            itemSer.Icr_Status_Des = oItemStaus.Find(x => x.Mis_cd == itemSer.Icr_itm_sts).Mis_desc;
                        }
                    }
                }
                grdItemCost.DataSource = _costData;
                grdItemCost.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        protected void lbtnRemoveWarra_Click(object sender, EventArgs e)
        {
            try
            {
                grdWarranty.DataSource = new int[] { };
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                LinkButton lbtnRemoveWarra = row.FindControl("lbtnRemoveWarra") as LinkButton;
                Label iw_itemsts = row.FindControl("iw_itemsts") as Label;
                Label warauom = row.FindControl("warauom") as Label;
                if (_lstitmWrd != null)
                {
                    _lstitmWrd = ViewState["_lstitmWrd"] as List<MasterItemWarrantyPeriod>;
                    var v = _lstitmWrd.Where(c => c.Mwp_itm_stus == iw_itemsts.Text).FirstOrDefault();
                    if (v != null)
                    {
                        v.Mwp_act = false;
                    }
                    if (_lstitmWrd != null)
                    {
                        _lstitmWrd = _lstitmWrd.Where(c => c.Mwp_act == true).ToList();
                        grdWarranty.DataSource = _lstitmWrd;
                    }
                }
                grdWarranty.DataBind();
                ViewState["_lstitmWrd"] = _lstitmWrd;
                itemwarrantypopup.Show();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        protected void lbtnRemovePcWarra_Click(object sender, EventArgs e)
        {
            try
            {
                grdPcwarranty.DataSource = new int[] { };
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                LinkButton lbtnRemovePcWarra = row.FindControl("lbtnRemovePcWarra") as LinkButton;
                Label pcw_com = row.FindControl("pcw_com") as Label;
                Label pcw_pc = row.FindControl("pcw_pc") as Label;
                Label pcw_status = row.FindControl("pcw_status") as Label;
                if (_lstpcWrd != null)
                {
                    _lstpcWrd = ViewState["_lstpcWrd"] as List<mst_itm_pc_warr>;
                    var v = _lstpcWrd.Where(c => c.Pc_com == pcw_com.Text
                        && c.Pc_code == pcw_pc.Text
                        && c.Pc_item_stus == pcw_status.Text).FirstOrDefault();
                    if (v != null)
                    {
                        _lstpcWrd.Remove(v);
                    }
                    if (_lstpcWrd != null)
                    {
                        grdPcwarranty.DataSource = _lstpcWrd;
                    }
                }
                grdPcwarranty.DataBind();
                ViewState["_lstpcWrd"] = _lstpcWrd;
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        public bool validateinputString(string input)
        {
            Match match = Regex.Match(input, @"([~!@$%^&*]+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return false;
            }
            return true;
        }
        public bool validateinputStringWithSpace(string input)
        {
            Match match = Regex.Match(input, @"([~!@$%^&*]+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return false;
            }
            return true;
        }

        public bool validateinputStringWithHash(string input)
        {
            Match match = Regex.Match(input, @"([~!@$%^&*]+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return false;
            }
            return true;
        }
        protected void txtLdes_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtLdes.Text))
            {
                DisplayMessage("Invalid charactor found in long description.", 2);
                txtLdes.Focus();
                return;
            }
        }
    }
}