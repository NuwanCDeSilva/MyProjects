using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Pricing
{
    public partial class AdditionalParameters : BasePage
    {
        protected string _Stype { get { return (string)Session["_Stype"]; } set { Session["_Stype"] = value; } }
        protected Deposit_Bank_Pc_wise objdefaultupdate { get { return (Deposit_Bank_Pc_wise)Session["objdefaultupdate"]; } set { Session["objdefaultupdate"] = value; } }
        protected Deposit_Bank_Pc_wise objpromoupdate { get { return (Deposit_Bank_Pc_wise)Session["objpromoupdate"]; } set { Session["objpromoupdate"] = value; } }
        protected Boolean _isrecallDef { get { return (Boolean)Session["_isrecallDef"]; } set { Session["_isrecallDef"] = value; } }
        protected List<sar_pb_def_det> ItemBrandCat_List { get { return (List<sar_pb_def_det>)Session["ItemBrandCat_List"]; } set { Session["ItemBrandCat_List"] = value; } }
        protected Boolean _pbIssrl { get { return (Boolean)Session["_pbIssrl"]; } set { Session["_pbIssrl"] = value; } }
        protected List<Tuple<string, decimal>> _ageItems { get { return (List<Tuple<string, decimal>>)Session["_ageItems"]; } set { Session["_ageItems"] = value; } }
        protected DataTable select_ITEMS_List { get { return (DataTable)Session["select_ITEMS_List"]; } set { Session["select_ITEMS_List"] = value; } }
        protected Boolean _isSimilarRecal { get { return (Boolean)Session["_isSimilarRecal"]; } set { Session["_isSimilarRecal"] = value; } }
        protected List<MasterItemSimilar> _similarDetails { get { return (List<MasterItemSimilar>)Session["_similarDetails"]; } set { Session["_similarDetails"] = value; } }
        protected string _Ltype { get { return (string)Session["_Ltype"]; } set { Session["_Ltype"] = value; } }
        protected List<MasterItemSubCate> _lstcate2 { get { return (List<MasterItemSubCate>)Session["_lstcate2"]; } set { Session["_lstcate2"] = value; } }
        protected Boolean isRedeemConfirms { get { return (Boolean)Session["isRedeemConfirms"]; } set { Session["isRedeemConfirms"] = value; } }
        protected List<string> _itemLst { get { return (List<string>)Session["_itemLst"]; } set { Session["_itemLst"] = value; } }
        protected List<GiftVoucherPages> _lstgvPages { get { return (List<GiftVoucherPages>)Session["_lstgvPages"]; } set { Session["_lstgvPages"] = value; } }
        protected List<PromoVoucherDefinition> _schemeProcess { get { return (List<PromoVoucherDefinition>)Session["_schemeProcess"]; } set { Session["_schemeProcess"] = value; } }
        protected List<PromotionVoucherPara> _promoVouPara { get { return (List<PromotionVoucherPara>)Session["_promoVouPara"]; } set { Session["_promoVouPara"] = value; } }
        protected Boolean _isUpdate { get { return (Boolean)Session["_isUpdate"]; } set { Session["_isUpdate"] = value; } }
        /*Lakshan 26-Feb/2016*/
        protected List<PriceDefinitionRef> _priceDefRef { get { return (List<PriceDefinitionRef>)Session["_priceDefRef"]; } set { Session["_priceDefRef"] = value; } }
        protected PriceDefinitionRef _priceRef { get { return (PriceDefinitionRef)Session["_priceRef"]; } set { Session["_priceRef"] = value; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clearAll();
                btnClearDefPara_Click(null, null);
                btnAgingClear_Click(null, null);
            }
            else
            {
                optVou.Items[0].Attributes.CssStyle.Add("margin-right", "35px");

                if (Session["SIPopup"] == "SIPopup")
                {
                    mpSearch.Show();
                }
            }
        }

        private void clearAll()
        {
            txtAddBook.Text = "";
            txtAddLevel.Text = "";
            txtMsg.Text = "";
            chkAge.Checked = false;
            chkCusMan.Checked = false;

            TextBoxLocation.Text = "";
            txtDPricebook.Text = "";
            txtDPriceLevel.Text = "";
            txtItemStatus.Text = "";
            txtAlertPriceBook.Text = "";
            txtAlertPriceLevel.Text = "";

            grvSalesTypes.DataSource = new int[] { };
            grvSalesTypes.DataBind();

            grvItemList.DataSource = new int[] { };
            grvItemList.DataBind();

            gvPreview.DataSource = new int[] { };
            gvPreview.DataBind();

            _Stype = string.Empty;

            objdefaultupdate = new Deposit_Bank_Pc_wise();
            objpromoupdate = new Deposit_Bank_Pc_wise();
            _isrecallDef = false;
            ItemBrandCat_List = new List<sar_pb_def_det>();

            cmbSelectCat.SelectedIndex = 0;
            cmbSelectCat_SelectedIndexChanged(null, null);

            _ageItems = new List<Tuple<string, decimal>>();
            select_ITEMS_List = new DataTable();

            dgvSimDet.DataSource = new int[] { };
            dgvSimDet.DataBind();

            dtpSFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            dtpSTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            _isSimilarRecal = false;
            _similarDetails = new List<MasterItemSimilar>();

            _Ltype = string.Empty;

            dgvDefDetails_pv.DataSource = new int[] { };
            dgvDefDetails_pv.DataBind();

            _lstcate2 = new List<MasterItemSubCate>();

            cmbProVouDefType.SelectedIndex = 0;
            cmbProVouDefType_SelectedIndexChanged(null, null);

            pnlListItems.Visible = true;
            pnlSubItems.Visible = false;

            gvVouCat.DataSource = new int[] { };
            gvVouCat.DataBind();

            _itemLst = new List<string>();

            cmbDefby_pv.SelectedIndex = 0;
            cmbDefby_pv_SelectedIndexChanged(null, null);

            dtpFromDate__pv.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            dtpToDate_pv.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            grvRedeemItems.DataSource = new int[] { };
            grvRedeemItems.DataBind();

            _lstgvPages = new List<GiftVoucherPages>();

            _schemeProcess = new List<PromoVoucherDefinition>();

            grvVouPara.DataSource = new int[] { };
            grvVouPara.DataBind();

            BindPriceType();
            bindPayTypes();

            _promoVouPara = new List<PromotionVoucherPara>();

            _isUpdate = false;

            ClearPV();
            Clear_Base();
            ClearDiscount();
        }

        #region Messages
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

        private void DisplayMessage(String Msg)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        #endregion

        #region Search
        protected void btnCloseSearchMP_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            mpSearch.Hide();
            Session["SIPopup"] = null;

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
                DisplayMessage(ex.Message, 4);
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
                mpSearch.Show(); Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "PriceBookByCompany")
                {
                    txtAddBook.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAddBook_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceLevelByBook")
                {
                    txtAddLevel.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAddLevel_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceLevelByBookDis")
                {
                    txtPriceLevel.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtPriceLevel_TextChanged(null, null);
                }
                else if (lblvalue.Text == "UserProfitCenter")
                {
                    TextBoxLocation.Text = grdResult.SelectedRow.Cells[1].Text;
                    TextBoxLocation_TextChanged(null, null);
                }
                else if (lblvalue.Text == "UserProfitCenterDis")
                {
                    txtProfCenter.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtProfCenter_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CircularDef")
                {
                    txtBaseCircular.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBaseCircular_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceBookByCompany_SACD")
                {
                    txtNpb.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtNpb_TextChanged(null, null);
                }
               
                else if (lblvalue.Text == "PriceLevelByBook_SACD")
                {
                    txtNpl.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtNpl_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceBookByCompany_BPB")
                {
                    txtBasepb.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBasepb_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceLevelByBook_BPL")
                {
                    txtBasepbl.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBasepbl_TextChanged(null, null);
                }
                else if (lblvalue.Text == "ItemBrand")
                {
                    txtbbrd.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtbbrd_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Main")
                {
                    txtCate1.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCate1_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Sub1")
                {
                    txtCate2.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCate2_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Sub2")
                {
                    txtCate3.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCate3_TextChanged(null, null);
                }
                else if (lblvalue.Text == "InvoiceItemUnAssable")
                {
                    txtBaseItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBaseItem_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Item")
                {
                    txtCharge.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCharge_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceBookByCompany_PC")
                {
                    txtAgeOriPb.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAgeOriPb_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceLevelByBook_PC")
                {
                    txtAgeOriPlevel.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAgeOriPlevel_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceBookByCompany_PC2")
                {
                    txtAgeCloPb.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAgeCloPb_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceLevelByBook_PC2")
                {
                    txtAgeCloPlevl.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAgeCloPlevl_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Brand_PC")
                {
                    txtAgeBrand.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAgeBrand_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Main_PC")
                {
                    txtAgeCate1.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAgeCate1_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Item_PC")
                {
                    txtItemCD.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemCD_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Sub1_PC")
                {
                    txtAgeCate2.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAgeCate2_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Item_SI")
                {
                    txtMainItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtMainItem_TextChanged(null, null);
                }
                else if (lblvalue.Text == "GetCompanyInvoice")
                {
                    txtInvoiceNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInvoiceNo_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Main_SI")
                {
                    txtMainCate.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtMainCate_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Sub1_SI")
                {
                    txtSubCate.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSubCate_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Sub2_SI")
                {
                    txtItemRange.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemRange_TextChanged(null, null);
                }
                else if (lblvalue.Text == "ItemBrand_SI")
                {
                    txtBrand_SI.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBrand_SI_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Item_SI_2")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItem_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Pc_HIRC_Channel")
                {
                    txtSChannel.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSChannel_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Pc_HIRC_SubChannel")
                {
                    txtSSubChannel.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSSubChannel_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Pc_HIRC_Location")
                {
                    txtSPc.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSPc_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Circular")
                {
                    txtSCir.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSCir_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Promotion")
                {
                    txtSpromo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSpromo_TextChanged(null, null);
                }
                else if (lblvalue.Text == "DisVouTp")
                {
                    txtVochCode_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtVochCode_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceBookByCompany_PV")
                {
                    txtPB_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtPB_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Main_PV")
                {
                    txtCat1_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCat1_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Sub2_PV")
                {
                    txtCat3_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCat3_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Item_PV")
                {
                    txtItem_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItem_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceLevelByBook_PV")
                {
                    txtPL_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtPL_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CAT_Sub1_PV")
                {
                    txtCat2_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCat2_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "ItemBrand_PV")
                {
                    txtBrand_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBrand_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Pc_HIRC_Channel_PV")
                {
                    txtChnnl_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtChnnl_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Pc_HIRC_SubChannel_PV")
                {
                    txtSChnnl_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSChnnl_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Pc_HIRC_Location_PV")
                {
                    txtPC_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtPC_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PromotionVoucherCricular")
                {
                    txtCircular_pv.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCircular_pv_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Item_PV2")
                {
                    txtItem_rd.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItem_rd_TextChanged(null, null);
                    pnlRedeemItemsMP.Show();
                }
                else if (lblvalue.Text == "PriceBookByCompany_PV2")
                {
                    txtRdmComPB.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRdmComPB_TextChanged(null, null);
                    pnlRedeemItemsMP.Show();
                }
                else if (lblvalue.Text == "PriceLevelByBook_PV2")
                {
                    txtRdmComPBLvl.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRdmComPBLvl_TextChanged(null, null);
                    pnlRedeemItemsMP.Show();
                }
                else if (lblvalue.Text == "Sales_Type")
                {
                    //txtSaleTp.Text = grdResult.SelectedRow.Cells[1].Text;
                    //txtSaleTp_TextChanged(null, null);
                }
                else if (lblvalue.Text == "DisVouTp_PV")
                {
                    txtProVouType.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtProVouType_TextChanged(null, null);
                }
                else if (lblvalue.Text == "InvoiceType")
                {
                    txtInvType.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInvType_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceBook")
                {
                    txtPriceBook.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtPriceBook_TextChanged(null, null);
                }
                else if (lblvalue.Text == "PriceLevel")
                {
                    txtPriceLevel.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtPriceLevel_TextChanged(null, null);
                }
                mpSearch.Hide();
                Session["SIPopup"] = null;
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void FilterData()
        {
            try
            {
                #region old
                if (lblvalue.Text == "PriceBookByCompany" || lblvalue.Text == "PriceBookByCompany_SACD" || lblvalue.Text == "PriceBookByCompany_BPB" || lblvalue.Text == "PriceBookByCompany_PC" || lblvalue.Text == "PriceBookByCompany_PC2" || lblvalue.Text == "PriceBookByCompany_PV" || lblvalue.Text == "PriceBookByCompany_PV2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "PriceLevelByBook" || lblvalue.Text == "PriceLevelByBook_SACD" || lblvalue.Text == "PriceLevelByBook_BPL" || lblvalue.Text == "PriceLevelByBook_PC" || lblvalue.Text == "PriceLevelByBook_PC2" || lblvalue.Text == "PriceLevelByBook_PV" || lblvalue.Text == "PriceLevelByBook_PV2")
                {
                    string SearchParams = string.Empty;
                    if (lblvalue.Text == "PriceLevelByBook_PC")
                    {
                        SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    }
                    else if (lblvalue.Text == "PriceLevelByBook_PC2")
                    {
                        SearchParams = SetCommonSearchInitialParameters2(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    }
                    else
                    {
                        SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    }
                    DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "UserProfitCenter")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "UserProfitCenter";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "UserProfitCenterDis")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "UserProfitCenterDis";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "PriceBookByCompany_Dis")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "CircularDef")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularDef);
                    DataTable result = CHNLSVC.CommonSearch.GetPriceDefCircularSearch(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "CircularDef";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "ItemBrand" || lblvalue.Text == "ItemBrand_SI" || lblvalue.Text == "ItemBrand_PV")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                    DataTable result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "CAT_Main" || lblvalue.Text == "CAT_Main_SI" || lblvalue.Text == "CAT_Main_PV")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "CAT_Sub1" || lblvalue.Text == "CAT_Sub1_SI" || lblvalue.Text == "CAT_Sub1_PV")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "CAT_Sub2" || lblvalue.Text == "CAT_Sub2_SI" || lblvalue.Text == "CAT_Sub2_PV")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "InvoiceItemUnAssable")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Item" || lblvalue.Text == "Item_SI" || lblvalue.Text == "Item_SI_2" || lblvalue.Text == "Item_PV" || lblvalue.Text == "Item_PV2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Brand_PC")
                {
                    string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.Brand);
                    DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "CAT_Main_PC")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Item_PC")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "CAT_Sub1_PC")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "GetCompanyInvoice")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetCompanyInvoice);
                    DataTable result = CHNLSVC.CommonSearch.GetComInvoice(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Pc_HIRC_Channel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Pc_HIRC_SubChannel" || lblvalue.Text == "Pc_HIRC_SubChannel_PV")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Pc_HIRC_Location" || lblvalue.Text == "Pc_HIRC_Location_PV")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Circular")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                    DataTable result = CHNLSVC.CommonSearch.GetPromotionSearch(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Promotion")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                    DataTable result = CHNLSVC.CommonSearch.GetPromotionSearch(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "DisVouTp" || lblvalue.Text == "DisVouTp_PV")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                    DataTable result = CHNLSVC.CommonSearch.GetDisVouTp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Pc_HIRC_Channel_PV")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "PromotionVoucherCricular")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionVoucherCricular);
                    DataTable result = CHNLSVC.CommonSearch.GetPromotionVoucherByCircular(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Sales_Type")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                    DataTable result = CHNLSVC.General.GetSalesTypes(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                #endregion
                else if (lblvalue.Text == "InvoiceType")
                {
                    _priceRef = new PriceDefinitionRef()
                    {
                        Sadd_com = Session["UserCompanyCode"].ToString(),
                        Sadd_pc = txtProfCenter.Text
                    };
                    DataTable result = CHNLSVC.CommonSearch.SearchDocPriceDefDocTp(_priceRef, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "PriceBook")
                {
                    _priceRef = new PriceDefinitionRef()
                    {
                        Sadd_com = Session["UserCompanyCode"].ToString(),
                        Sadd_pc = txtProfCenter.Text.ToUpper(),
                        Sadd_doc_tp = txtInvType.Text.ToUpper()
                    };
                    DataTable result = CHNLSVC.CommonSearch.SearchDocPriceDefPrBook(_priceRef, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "PriceLevel")
                {
                    _priceRef = new PriceDefinitionRef()
                    {
                        Sadd_com = Session["UserCompanyCode"].ToString(),
                        Sadd_pc = txtProfCenter.Text.ToUpper(),
                        Sadd_doc_tp = txtInvType.Text.ToUpper(),
                        Sadd_pb = txtPriceBook.Text.ToUpper()
                    };
                    DataTable result = CHNLSVC.CommonSearch.SearchDocPriceDefPrLVL(_priceRef, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        if (_Stype == "PromoVou")
                        {
                            paramsText.Append(cmbRdmAllowCompany.SelectedValue + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                            break;
                        }

                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {
                        if (_Stype == "Maintain")
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtMBook.Text.Trim() + seperator);
                        }
                        else if (_Stype == "Additional")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtAddBook.Text.Trim() + seperator);
                        }
                        else if (_Stype == "AdditionalDis")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtAddBook.Text.Trim() + seperator);
                        }
                        else if (_Stype == "PromoVou")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtPB_pv.Text.Trim().ToUpper() + seperator);
                        }
                        else if (_Stype == "PromoVouRedeem")
                        {
                            paramsText.Append(cmbRdmAllowCompany.SelectedValue + seperator + txtRdmComPB.Text.Trim() + seperator);
                        }

                        else if (_Stype == "DefMaintain")
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtDpb.Text.Trim() + seperator);
                        }
                        else if (_Stype == "PBDEF1")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtNpb.Text.Trim().ToUpper() + seperator);
                        }
                        else if (_Stype == "PBDEF2")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtBasepb.Text.Trim().ToUpper() + seperator);
                        }
                        else
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtBook.Text.Trim() + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CircularDef:
                    {
                        paramsText.Append(string.Empty + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        if (_Stype == "PromoVou")
                        {
                            paramsText.Append(txtCat1_pv.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;

                        }
                        else if (_Stype == "PBDEF")
                        {
                            paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(txtMainCate.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        if (_Stype == "PBDEF")
                        {
                            paramsText.Append(txtCate1.Text + seperator + txtCate3.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(txtMainCate.Text + seperator + txtSubCate.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.GetCompanyInvoice:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        if (_Ltype == "Similar")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtSChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else if (_Ltype == "Maintain")
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtmChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else if (_Ltype == "PromoVou")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtChnnl_pv.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }

                        else if (_Ltype == "DefMaintain")
                        {
                            // paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtDchannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        if (_Ltype == "Similar")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtSChannel.Text + seperator + txtSSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        else if (_Ltype == "Maintain")
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtmChannel.Text + seperator + txtmSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtChnnl_pv.Text + seperator + txtSChnnl_pv.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append(string.Empty + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(string.Empty + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DisVouTp:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromotionVoucherCricular:
                    {
                        paramsText.Append(txtCircular_pv.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private string SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        if (_Stype == "PromoVou")
                        {
                            paramsText.Append(cmbRdmAllowCompany.SelectedValue + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                            break;
                        }

                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtAgeCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtAgeCate1.Text + seperator + txtAgeCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtAgeOriPb.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private string SetCommonSearchInitialParameters2(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtAgeCloPb.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private string SetCommonSearchInitialParametersDiscount(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtAgeCloPb.Text.Trim().ToUpper() + seperator);
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

        #endregion

        #region Additional Parameters

        protected void btnAddUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtAddBook.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtAddBook.Text = "";
                    txtAddBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtAddLevel.Text))
                {
                    DisplayMessage("Please select the price level");
                    txtAddLevel.Text = "";
                    txtAddLevel.Focus();
                    return;
                }

                row_aff = (Int32)CHNLSVC.Sales.UpdateAddPricingParam(Session["UserID"].ToString(), chkAge.Checked, txtMsg.Text, chkCusMan.Checked, txtAddLevel.Text, txtAddBook.Text, Session["UserCompanyCode"].ToString());

                if (row_aff > 0)
                {
                    DisplayMessage("Additional parameters updated.");
                    Clear_Add();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        DisplayMessage(_msg);
                    }
                    else
                    {
                        DisplayMessage("Failed to update.");
                    }
                }

            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnAddClear_Click(object sender, EventArgs e)
        {
            Clear_Add();
        }

        protected void txtAddBook_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAddBook.Text))
                    return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtAddBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter valid price book");
                    txtAddBook.Text = "";
                    txtAddBook.Focus();
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnSearchAddbook_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceBookByCompany";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtAddLevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAddLevel.Text))
                    return;
                if (string.IsNullOrEmpty(txtAddBook.Text))
                {
                    DisplayMessage("Please select the price book.");
                    txtAddLevel.Text = "";
                    txtAddBook.Focus();
                    return;
                }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtAddBook.Text.Trim(), txtAddLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    DisplayMessage("Please enter valid price level.");
                    txtAddLevel.Text = "";
                    txtAddLevel.Focus();
                    return;
                }
                else
                {
                    txtMsg.Text = _tbl.Sapl_spmsg;
                    chkAge.Checked = _tbl.Sapl_isage;
                    chkCusMan.Checked = _tbl.Sapl_needcus;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnSearchAddLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAddBook.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtAddBook.Text = "";
                    txtAddBook.Focus();
                    return;
                }

                _Stype = "Additional";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceLevelByBook";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void Clear_Add()
        {
            txtAddBook.Text = "";
            txtAddLevel.Text = "";
            txtMsg.Text = "";
            chkAge.Checked = false;
            chkCusMan.Checked = false;
            txtAddBook.Focus();
        }

        protected void TextBoxLocation_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxLocation.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text);
                if (_IsValid == false)
                {
                    DisplayMessage("Invalid profit center.");
                    TextBoxLocation.Text = "";
                    TextBoxLocation.Focus();
                    txtDPriceLevel.Text = "";
                    txtDPricebook.Text = "";
                    txtItemStatus.Text = "";
                    txtAlertPriceBook.Text = "";
                    txtAlertPriceLevel.Text = "";
                    return;
                }

                else
                {
                    txtDPricebook.Text = "";
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";
                    txtAlertPriceLevel.Text = "";
                    txtAlertPriceBook.Text = "";
                    DataTable dt = CHNLSVC.Sales.Load_Default_PriceBook(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text);
                    DataTable dtPromo = CHNLSVC.Sales.Load_Promotion_PriceBook(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtDPricebook.Text = dt.Rows[0]["SADD_PB"].ToString();
                        txtDPriceLevel.Text = dt.Rows[0]["SADD_P_LVL"].ToString();
                        txtItemStatus.Text = dt.Rows[0]["SADD_DEF_STUS"].ToString();
                    }
                    if (dtPromo != null && dtPromo.Rows.Count > 0)
                    {
                        txtAlertPriceBook.Text = dtPromo.Rows[0]["SADD_PB"].ToString();
                        txtAlertPriceLevel.Text = dtPromo.Rows[0]["SADD_P_LVL"].ToString();
                    }
                }
            }
        }

        protected void btnProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "UserProfitCenter";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtDPricebook_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    DisplayMessage("Please select the profit center.");
                    txtDPricebook.Text = "";
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";
                    return;
                }


                if (string.IsNullOrEmpty(txtDPricebook.Text.Trim())) return;

                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text.Trim(), "", txtDPricebook.Text.Trim(), "");
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter valid price book");
                    txtDPricebook.Text = "";
                    txtDPricebook.Focus();
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";
                    //return;
                }
                else
                {
                    //txtDPriceLevel.Text = "";
                    //txtItemStatus.Text = "";
                    //return;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnDPriceBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                DisplayMessage("Please select the profit center.");
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();

                return;
            }
            DataTable dtpb = CHNLSVC.Sales.Load_PcWise_PriceBook(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text.Trim());
            if (dtpb != null && dtpb.Rows.Count > 0)
            {
                lblSelection.Text = "Default Price Book";
                dtpb.Columns[0].ColumnName = "Code";
                dtpb.Columns[1].ColumnName = "Description";
                dgvPriceBookDetails.DataSource = dtpb;
                dgvPriceBookDetails.DataBind();
            }
            else
            {
                DisplayMessage("No data found!");
            }
        }

        protected void dgvPriceBookDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblSelection.Text == "Default Price Book")
            {
                txtDPricebook.Text = dgvPriceBookDetails.SelectedRow.Cells[1].Text;
            }
            else if (lblSelection.Text == "Default Price Level")
            {
                txtDPriceLevel.Text = dgvPriceBookDetails.SelectedRow.Cells[1].Text;
            }
            else if (lblSelection.Text == "Default Item Status")
            {
                txtItemStatus.Text = dgvPriceBookDetails.SelectedRow.Cells[1].Text;
            }
            else if (lblSelection.Text == "Price Book")
            {
                txtAlertPriceBook.Text = dgvPriceBookDetails.SelectedRow.Cells[1].Text;
            }
            else if (lblSelection.Text == "Price Level")
            {
                txtAlertPriceLevel.Text = dgvPriceBookDetails.SelectedRow.Cells[1].Text;
            }
        }

        protected void txtDPriceLevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    DisplayMessage("Please select the profit center.");
                    txtDPriceLevel.Text = "";
                    txtDPricebook.Text = "";
                    txtItemStatus.Text = "";
                    return;
                }


                if (string.IsNullOrEmpty(txtDPriceLevel.Text.Trim())) return;
                if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
                {
                    DisplayMessage("Please select the price book.");
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";
                    return;
                }
                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text.Trim(), txtDPriceLevel.Text.Trim(), txtDPricebook.Text.Trim(), "");
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter valid price level");
                    txtDPriceLevel.Text = "";
                    txtDPriceLevel.Focus();
                    txtItemStatus.Text = "";
                }
                else
                {
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnDPriceLevel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                DisplayMessage("Please select the profit center.");
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
            {
                DisplayMessage("Please select the price book.");
                txtDPricebook.Text = "";
                txtDPricebook.Focus();
                return;
            }
            DataTable dtpl = CHNLSVC.Sales.Load_PcWise_Price_level(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text.Trim(), txtDPricebook.Text.Trim());
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                //pnlPriceLevel.Visible = true;
                //dgvPricelevel.AutoGenerateColumns = false;
                //dgvPricelevel.DataSource = dtpl;
                lblSelection.Text = "Default Price Level";
                dtpl.Columns[0].ColumnName = "Code";
                dtpl.Columns[1].ColumnName = "Description";
                dgvPriceBookDetails.DataSource = dtpl;
                dgvPriceBookDetails.DataBind();
            }
            else
            {
                DisplayMessage("No data found!");
            }
        }

        protected void txtItemStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    DisplayMessage("Please select the profit center.");
                    txtDPriceLevel.Text = "";
                    txtDPricebook.Text = "";
                    txtItemStatus.Text = "";
                    return;
                }
                if (string.IsNullOrEmpty(txtItemStatus.Text.Trim())) return;
                if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
                {
                    DisplayMessage("Please select the price book.");
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";

                    return;
                }
                if (string.IsNullOrEmpty(txtDPriceLevel.Text.Trim()))
                {
                    DisplayMessage("Please select the price level.");
                    txtItemStatus.Text = "";
                    return;
                }

                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(Session["UserCompanyCode"].ToString(), "", txtDPriceLevel.Text.Trim(), txtDPricebook.Text.Trim(), txtItemStatus.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter valid price level");
                    txtItemStatus.Text = "";
                    txtItemStatus.Focus();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnItemStatusSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                DisplayMessage("Please select the profit center.");
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
            {
                DisplayMessage("Please select the price book.");
                txtDPricebook.Text = "";
                txtDPricebook.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDPriceLevel.Text.Trim()))
            {
                DisplayMessage("Please select the price level.");
                txtDPriceLevel.Text = "";
                txtDPriceLevel.Focus();
                return;
            }
            DataTable dtpl = CHNLSVC.Sales.Load_Item_dets(Session["UserCompanyCode"].ToString(), txtDPriceLevel.Text.Trim(), txtDPricebook.Text.Trim());
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                //pnlItemStatus.Visible = true;
                //dgvItemStatus.AutoGenerateColumns = false;
                //dgvItemStatus.DataSource = dtpl;

                lblSelection.Text = "Default Item Status";
                dtpl.Columns[0].ColumnName = "Code";
                dtpl.Columns[1].ColumnName = "Description";
                dgvPriceBookDetails.DataSource = dtpl;
                dgvPriceBookDetails.DataBind();
            }
            else
            {
                DisplayMessage("No data found!");
            }
        }

        protected void txtAlertPriceBook_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    DisplayMessage("Please select the profit center.");
                    txtAlertPriceBook.Text = "";
                    txtAlertPriceLevel.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim())) return;

                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text.Trim(), "", txtAlertPriceBook.Text.Trim(), "");
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter valid price book");
                    txtAlertPriceBook.Text = "";
                    txtAlertPriceBook.Focus();
                    txtAlertPriceLevel.Text = "";
                }
                else
                {
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message);
            }
        }

        protected void btnAlertPbookSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                DisplayMessage("Please select the profit center.");
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();

                return;
            }
            DataTable dtpb = CHNLSVC.Sales.Load_PcWise_PriceBook(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text.Trim());
            if (dtpb != null && dtpb.Rows.Count > 0)
            {
                //pnlAlertPB.Visible = true;
                //dgvAlertPb.AutoGenerateColumns = false;
                //dgvAlertPb.DataSource = dtpb;

                lblSelection.Text = "Price Book";
                dtpb.Columns[0].ColumnName = "Code";
                dtpb.Columns[1].ColumnName = "Description";
                dgvPriceBookDetails.DataSource = dtpb;
                dgvPriceBookDetails.DataBind();
            }
            else
            {
                DisplayMessage("No data found!");
            }
        }

        protected void txtAlertPriceLevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    DisplayMessage("Please select the profit center.");
                    txtAlertPriceBook.Text = "";
                    txtAlertPriceLevel.Text = "";
                    return;
                }


                if (string.IsNullOrEmpty(txtAlertPriceLevel.Text.Trim())) return;
                if (string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim()))
                {
                    DisplayMessage("Please select the price book.");
                    txtAlertPriceLevel.Text = "";
                    return;
                }
                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text.Trim(), txtAlertPriceLevel.Text.Trim(), txtAlertPriceBook.Text.Trim(), "");
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter valid price level");
                    txtAlertPriceLevel.Text = "";
                    txtAlertPriceLevel.Focus();
                    // return;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message);
            }
        }

        protected void btnAlertPlevelSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                DisplayMessage("Please select the profit center.");
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim()))
            {
                DisplayMessage("Please select the price book.");
                txtAlertPriceBook.Text = "";
                txtAlertPriceBook.Focus();
                return;
            }
            DataTable dtpl = CHNLSVC.Sales.Load_PcWise_Price_level(Session["UserCompanyCode"].ToString(), TextBoxLocation.Text.Trim(), txtAlertPriceBook.Text.Trim());
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                //pnlAlertPriceLevel.Visible = true;
                //dgvAlertPricelevel.AutoGenerateColumns = false;
                //dgvAlertPricelevel.DataSource = dtpl;

                lblSelection.Text = "Price Level";
                dtpl.Columns[0].ColumnName = "Code";
                dtpl.Columns[1].ColumnName = "Description";
                dgvPriceBookDetails.DataSource = dtpl;
                dgvPriceBookDetails.DataBind();
            }
            else
            {
                DisplayMessage("No data found!");
            }
        }

        protected void btnUpdateDefPara_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                DisplayMessage("Please select the profit center.");
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
            {
                DisplayMessage("Please select the default price book.");
                txtDPricebook.Text = "";
                txtDPricebook.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDPriceLevel.Text.Trim()))
            {
                DisplayMessage("Please select the default price level.");
                txtDPriceLevel.Text = "";
                txtDPriceLevel.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtItemStatus.Text.Trim()))
            {
                DisplayMessage("Please select the item Status.");
                txtItemStatus.Text = "";
                txtItemStatus.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim()))
            {
                if (string.IsNullOrEmpty(txtAlertPriceLevel.Text.Trim()))
                {
                    DisplayMessage("Please select the promotion price level.");
                    txtAlertPriceLevel.Text = "";
                    txtAlertPriceLevel.Focus();
                    return;
                }

            }
            if (!string.IsNullOrEmpty(txtAlertPriceLevel.Text.Trim()))
            {
                if (string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim()))
                {
                    DisplayMessage("Please select the promotion price book.");
                    txtAlertPriceBook.Text = "";
                    txtAlertPriceBook.Focus();
                    return;
                }
            }

            try
            {
                objdefaultupdate = new Deposit_Bank_Pc_wise();
                objdefaultupdate.Stus = txtItemStatus.Text.Trim();
                objdefaultupdate.Profit_center = TextBoxLocation.Text.Trim();
                objdefaultupdate.Company = Session["UserCompanyCode"].ToString();
                objdefaultupdate.Price_book = txtDPricebook.Text.Trim();
                objdefaultupdate.Price_lvl = txtDPriceLevel.Text.Trim();
                objdefaultupdate.Modifyby = Session["UserID"].ToString();

                objpromoupdate = new Deposit_Bank_Pc_wise();
                objpromoupdate.Profit_center = TextBoxLocation.Text.Trim();
                objpromoupdate.Company = Session["UserCompanyCode"].ToString();
                objpromoupdate.Promo_p_book = txtAlertPriceBook.Text.Trim();
                objpromoupdate.Promo_price_lvl = txtAlertPriceLevel.Text.Trim();
                objpromoupdate.Modifyby = Session["UserID"].ToString();

                string _error = "";
                int result = CHNLSVC.Sales.Update_To_Parameters(objdefaultupdate, objpromoupdate, out _error);
                if (result == -1)
                {
                    DisplayMessage("Error occurred while processing\n" + _error, 4);
                    return;
                }
                else
                {
                    DisplayMessage("Records updated successfully", 3);
                    btnClearDefPara_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(ex.Message, 4);
            }

        }

        protected void btnClearDefPara_Click(object sender, EventArgs e)
        {
            TextBoxLocation.Text = "";
            txtDPricebook.Text = "";
            txtDPriceLevel.Text = "";
            txtItemStatus.Text = "";
            txtAlertPriceLevel.Text = "";
            txtAlertPriceBook.Text = "";
            dgvPriceBookDetails.DataSource = new int[] { };
            dgvPriceBookDetails.DataBind();
        }

        #endregion

        #region Setup Auto Definition
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtBaseCircular.Text))
                {
                    DisplayMessage("Please enter circular");
                    txtBaseCircular.Text = "";
                    txtBaseCircular.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNpb.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtNpb.Text = "";
                    txtNpb.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNpl.Text))
                {
                    DisplayMessage("Please select the price level");
                    txtNpl.Text = "";
                    txtNpl.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(txtBasepb.Text))
                {
                    DisplayMessage("Please select the base price book");
                    txtBasepb.Text = "";
                    txtBasepb.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtBasepbl.Text))
                {
                    DisplayMessage("Please select the base price level");
                    txtBasepbl.Text = "";
                    txtBasepbl.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(cmbActivebase.Text))
                {
                    DisplayMessage("Please select the status");
                    cmbActivebase.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbCate.Text))
                {
                    DisplayMessage("Please select the category");
                    cmbCate.Focus();
                    return;
                }

                if (Convert.ToDateTime(dtpBaseFrom.Text).Date > Convert.ToDateTime(dtpBaseTo.Text).Date)
                {
                    DisplayMessage("From date can't be higher than the To date");
                    dtpBaseFrom.Focus();
                    return;
                }

                if (_isrecallDef == false)
                {
                    if (Convert.ToDateTime(dtpBaseFrom.Text).Date < DateTime.Today.Date)
                    {
                        DisplayMessage("From date can't be lesser than the current date");
                        dtpBaseFrom.Focus();
                        return;
                    }

                    if (ItemBrandCat_List.Count == 0)
                    {
                        DisplayMessage("Please select details ");
                        return;
                    }
                }

                if (grvSalesTypes.Rows.Count == 0)
                {
                    DisplayMessage("Please add items");
                    return;
                }

                for (int i = 0; i < grvSalesTypes.Rows.Count; i++)
                {
                    GridViewRow dr = grvSalesTypes.Rows[i];
                    Label lblSpdd_item = dr.FindControl("lblSpdd_item") as Label;
                    CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;

                    //if (chkSelectPC.Checked)
                    {
                        sar_pb_def_det oItem = ItemBrandCat_List.Find(x => x.Spdd_item == lblSpdd_item.Text.Trim());
                        if (oItem != null)
                        {
                            oItem.Spdd_active = 1;
                        }
                    }
                }

                sar_pb_def _pbdef = new sar_pb_def();
                _pbdef.Spd_seq = 0;
                _pbdef.Spd_com = Session["UserCompanyCode"].ToString();
                _pbdef.Spd_circular = txtBaseCircular.Text.ToUpper();
                _pbdef.Spd_base_pb = txtBasepb.Text.ToUpper();
                _pbdef.Spd_base_pblvl = txtBasepbl.Text.ToUpper();
                _pbdef.Spd_pb = txtNpb.Text.ToUpper();
                _pbdef.Spd_pblvl = txtNpl.Text.ToUpper();
                _pbdef.Spd_type = cmbSelectCat.SelectedValue.ToString();
                _pbdef.Spd_from = Convert.ToDateTime(dtpBaseFrom.Text).Date;
                _pbdef.Spd_to = Convert.ToDateTime(dtpBaseTo.Text).Date;
                _pbdef.Spd_cate = cmbCate.SelectedValue.ToString();
                if (cmbActivebase.SelectedValue == "1")
                {
                    _pbdef.Spd_act = 1;
                }
                else
                {
                    _pbdef.Spd_act = 0;
                }

                _pbdef.Spd_cre_by = Session["UserID"].ToString();
                _pbdef.Spd_mod_by = Session["UserID"].ToString();

                foreach (sar_pb_def_det item in ItemBrandCat_List)
                {
                    if (cmbActivebase.SelectedValue == "1")
                    {
                        item.Spdd_active = 1;
                    }
                    else
                    {
                        item.Spdd_active = 0;
                    }
                }

                foreach (sar_pb_def_det item in ItemBrandCat_List)
                {
                    MasterItem _msItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Spdd_item);
                    if (_msItem != null && !string.IsNullOrEmpty(_msItem.Mi_cd) && cmbSelectCat.SelectedValue.ToString().Trim() != "7")
                    {
                        item.Spdd_cat1 = _msItem.Mi_cate_1;
                        item.Spdd_cat2 = _msItem.Mi_cate_2;
                        item.Spdd_cat3 = _msItem.Mi_cate_3;
                        item.Spdd_brand = _msItem.Mi_brand;
                    }

                    if (cmbSelectCat.SelectedValue.ToString().Trim() != "3")
                    {
                        if (cmbSelectCat.SelectedValue.ToString().Trim() == "5")
                        {
                            item.Spdd_cat3 = item.Spdd_item;
                            item.Spdd_cat2 = "";
                        }
                        else if (cmbSelectCat.SelectedValue.ToString().Trim() == "4")
                        {
                            item.Spdd_cat3 = "";
                            item.Spdd_cat2 = item.Spdd_item;
                        }
                        else if (cmbSelectCat.SelectedValue.ToString().Trim() == "6")
                        {
                            item.Spdd_item = "";
                            item.Spdd_cat2 = "";
                            item.Spdd_cat3 = "";
                        }
                        else if (cmbSelectCat.SelectedValue.ToString().Trim() == "7")
                        {
                            item.Spdd_item = "";
                            item.Spdd_cat2 = "";
                            item.Spdd_cat3 = "";
                            item.Spdd_cat1 = "";
                        }
                        else if (cmbSelectCat.SelectedValue.ToString().Trim() == "8")
                        {
                            item.Spdd_cat3 = "";
                            item.Spdd_cat1 = "";
                            item.Spdd_cat2 = item.Spdd_item;
                        }
                        else if (cmbSelectCat.SelectedValue.ToString().Trim() == "9")
                        {
                            item.Spdd_cat2 = "";
                            item.Spdd_cat1 = "";
                            item.Spdd_cat3 = item.Spdd_item;
                        }
                        else if (cmbSelectCat.SelectedValue.ToString().Trim() == "10")
                        {
                            item.Spdd_cat2 = "";
                            item.Spdd_cat3 = "";
                        }
                        item.Spdd_item = "";
                    }
                }

                string err;
                row_aff = (Int32)CHNLSVC.Sales.SavePriceBookDefinition(_pbdef, ItemBrandCat_List, out err);

                if (row_aff > 0)
                {

                    DisplayMessage("Price Book definition successfully added.", 3);

                    _isrecallDef = false;
                    Clear_Base();
                }
                else
                {
                    if (!string.IsNullOrEmpty(err))
                    {
                        DisplayMessage(err, 4);
                    }
                    else
                    {
                        DisplayMessage("Failed to update.", 4);
                    }
                }

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Base();
        }

        private void Clear_Base()
        {
            _isrecallDef = false;
            txtBaseCircular.Text = "";
            txtNpb.Text = "";
            txtNpl.Text = "";
            txtBasepb.Text = "";
            txtBasepbl.Text = "";
            cmbSelectCat.SelectedIndex = 0;
            txtbbrd.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            txtCate3.Text = "";
            txtMarkup.Text = "";
            txtCharge.Text = "";
            txtBaseItem.Text = "";
            cmbCate.SelectedIndex = -1;
            cmbActivebase.SelectedIndex = -1;
            ItemBrandCat_List = new List<sar_pb_def_det>();
            grvSalesTypes.DataSource = new int[] { };
            grvSalesTypes.DataBind();
            _pbIssrl = false;
            cmbSelectCat.SelectedIndex = 0;
            cmbSelectCat_SelectedIndexChanged(null, null);
            dtpBaseFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            dtpBaseTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            DataTable items2 = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);
            ddlhpSalesAccept.DataSource = items2;
            ddlhpSalesAccept.DataTextField = "srtp_desc";
            ddlhpSalesAccept.DataValueField = "srtp_cd";
            ddlhpSalesAccept.DataBind();
        }

        protected void txtBaseCircular_TextChanged(object sender, EventArgs e)
        {
            List<sar_pb_def> _hdr = null;
            List<sar_pb_def_det> _item = new List<sar_pb_def_det>();
            try
            {
                if (txtBaseCircular.Text != "")
                {
                    _hdr = CHNLSVC.Sales.GetPriceDefHeader(txtBaseCircular.Text.ToUpper());
                    if (_hdr != null && _hdr.Count > 0)
                    {
                        _isrecallDef = true;
                        //_hdr[0].Spd_seq = 0;

                        txtBasepb.Text = _hdr[0].Spd_base_pb;
                        txtBasepbl.Text = _hdr[0].Spd_base_pblvl;
                        txtNpb.Text = _hdr[0].Spd_pb;
                        txtNpl.Text = _hdr[0].Spd_pblvl;
                        cmbSelectCat.SelectedValue = _hdr[0].Spd_type;
                        cmbSelectCat_SelectedIndexChanged(null, null);
                        dtpBaseFrom.Text = _hdr[0].Spd_from.ToString("dd/MMM/yyyy");
                        dtpBaseTo.Text = _hdr[0].Spd_to.ToString("dd/MMM/yyyy");
                        cmbCate.SelectedValue = _hdr[0].Spd_cate;
                        cmbActivebase.SelectedValue = _hdr[0].Spd_act.ToString();

                        _item = CHNLSVC.Sales.GetPriceDefDet(_hdr[0].Spd_seq);

                        List<sar_pb_def_det> _itemList = new List<sar_pb_def_det>();

                        if (_item != null && _item.Count > 0)
                        {
                            foreach (sar_pb_def_det _itm in _item)
                            {
                                string brand = txtBrand.Text;
                                sar_pb_def_det obj = new sar_pb_def_det(); //for display purpose
                                if (_hdr[0].Spd_type == "4" || _hdr[0].Spd_type == "5" || _hdr[0].Spd_type == "6")
                                {
                                    obj.Spdd_brand = _itm.Spdd_brand;
                                    obj.Spdd_cat1 = _itm.Spdd_cat1;
                                    obj.Spdd_cat2 = _itm.Spdd_cat2;
                                    obj.Spdd_cat3 = _itm.Spdd_cat3;
                                    if (_hdr[0].Spd_type == "4")
                                    {
                                        obj.Spdd_Des = _itm.Spdd_cat2;
                                        grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";
                                        obj.Spdd_item = _itm.Spdd_cat2;
                                    }
                                    if (_hdr[0].Spd_type == "5")
                                    {
                                        obj.Spdd_Des = _itm.Spdd_cat3;
                                        grvSalesTypes.Columns[1].HeaderText = "Cat.";
                                        obj.Spdd_item = _itm.Spdd_cat3;
                                    }
                                    if (_hdr[0].Spd_type == "6")
                                    {
                                        obj.Spdd_Des = _itm.Spdd_cat1;
                                        grvSalesTypes.Columns[1].HeaderText = "Main Cat.";
                                        obj.Spdd_item = _itm.Spdd_cat1;
                                    }
                                }

                                else if (_hdr[0].Spd_type == "3")
                                {
                                    obj.Spdd_brand = _itm.Spdd_brand;
                                    obj.Spdd_item = _itm.Spdd_item;
                                    obj.Spdd_Des = _itm.Spdd_item;
                                    grvSalesTypes.Columns[1].HeaderText = "Item";
                                }
                                else if (_hdr[0].Spd_type == "7")
                                {
                                    obj.Spdd_brand = _itm.Spdd_brand;
                                    //  obj.Spdd_Des = _itm.Spdd_brand;
                                }
                                else if (_hdr[0].Spd_type == "10")
                                {
                                    obj.Spdd_cat1 = _itm.Spdd_cat1;
                                    obj.Spdd_item = _itm.Spdd_cat1;
                                    obj.Spdd_Des = _itm.Spdd_cat1;
                                    grvSalesTypes.Columns[1].HeaderText = "Main Cat.";
                                }
                                else if (_hdr[0].Spd_type == "9")
                                {
                                    obj.Spdd_item = _itm.Spdd_cat3;
                                    obj.Spdd_cat3 = _itm.Spdd_cat3;
                                    obj.Spdd_Des = _itm.Spdd_cat3;
                                    grvSalesTypes.Columns[1].HeaderText = "Cat.";
                                }
                                else if (_hdr[0].Spd_type == "8")
                                {
                                    obj.Spdd_cat3 = _itm.Spdd_cat3;
                                    obj.Spdd_Des = _itm.Spdd_cat3;
                                    grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";
                                    obj.Spdd_item = _itm.Spdd_cat3;

                                    obj.Spdd_item = _itm.Spdd_cat2;
                                    obj.Spdd_cat2 = _itm.Spdd_cat2;
                                    obj.Spdd_Des = _itm.Spdd_cat2;

                                }
                                else
                                {
                                    obj.Spdd_brand = string.Empty;
                                }

                                //   obj.Spdd_Des = code;
                                obj.Spdd_margin = _itm.Spdd_margin;
                                obj.Spdd_ch_code = _itm.Spdd_ch_code;

                                try
                                {
                                    // obj.Spdd_Des = "";
                                }
                                catch (Exception)
                                {
                                    //obj.Spdd_Des = "";
                                }
                                _itemList.Add(obj);
                            }
                        }

                        grvSalesTypes.AutoGenerateColumns = false;
                        grvSalesTypes.DataSource = _itemList;
                        grvSalesTypes.DataBind();

                        for (int i = 0; i < grvSalesTypes.Rows.Count; i++)
                        {
                            GridViewRow dr = grvSalesTypes.Rows[i];
                            Label lblSpdd_active = dr.FindControl("lblSpdd_active") as Label;
                            CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;

                            if (lblSpdd_active.Text.ToUpper() == "0")
                            {
                                chkSelectPC.Checked = true;
                            }
                        }

                        ItemBrandCat_List = _itemList;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSrhCircular_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularDef);
                DataTable result = CHNLSVC.CommonSearch.GetPriceDefCircularSearch(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CircularDef";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtNpb_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNpb.Text))
                return;
            DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtNpb.Text.Trim().ToUpper());
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                DisplayMessage("Please enter valid price book");
                txtNpb.Text = "";
                txtNpb.Focus();
            }
        }

        protected void btnSrhPbd_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceBookByCompany_SACD";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtNpl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNpl.Text))
                    return;

                if (string.IsNullOrEmpty(txtNpb.Text))
                {
                    DisplayMessage("Please select the price book.");
                    txtNpl.Text = "";
                    txtNpb.Focus();
                    return;
                }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtNpb.Text.Trim().ToUpper(), txtNpl.Text.Trim().ToUpper());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    DisplayMessage("Please enter valid price level.");
                    txtNpl.Text = "";
                    txtNpl.Focus();
                    return;
                }
                else
                {
                    _pbIssrl = _tbl.Sapl_is_serialized;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnSrhPbld_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNpb.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtNpb.Text = "";
                    txtNpb.Focus();
                    return;
                }

                _Stype = "PBDEF1";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceLevelByBook_SACD";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtBasepb_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBasepb.Text))
                return;
            DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtBasepb.Text.Trim().ToUpper());
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                DisplayMessage("Please enter valid price book");
                txtBasepb.Text = "";
                txtBasepb.Focus();
            }

            if (!string.IsNullOrEmpty(txtBasepb.Text) && !string.IsNullOrEmpty(txtNpb.Text))
            {
                if (txtBasepb.Text.ToUpper() == txtNpb.Text.ToUpper())
                {
                }
            }
        }

        protected void btnSrhPbdBase_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceBookByCompany_BPB";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtBasepbl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBasepbl.Text)) return;
                if (string.IsNullOrEmpty(txtBasepb.Text))
                {
                    DisplayMessage("Please select the price book.");
                    txtBasepbl.Text = "";
                    txtBasepb.Focus(); return;
                }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtBasepb.Text.Trim().ToUpper(), txtBasepbl.Text.Trim().ToUpper());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    DisplayMessage("Please enter valid price level.");
                    txtBasepbl.Text = "";
                    txtBasepbl.Focus();
                    return;
                }
                else
                {
                    if (_pbIssrl != _tbl.Sapl_is_serialized)
                    {
                        DisplayMessage("Base price level type and price level type should be equal.");
                        txtBasepbl.Text = "";
                        txtBasepbl.Focus();
                        return;
                    }
                }


                if (!string.IsNullOrEmpty(txtBasepb.Text) && !string.IsNullOrEmpty(txtNpb.Text) && !string.IsNullOrEmpty(txtBasepbl.Text) && !string.IsNullOrEmpty(txtNpl.Text))
                {
                    if (txtBasepb.Text.ToUpper() == txtNpb.Text.ToUpper() && txtBasepbl.Text.ToUpper() == txtNpl.Text.ToUpper())
                    {
                        DisplayMessage("Base price book/ Price level can't be same as price book/price level ");
                        txtBasepb.Text = "";
                        txtBasepb.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnSrhPbldBase_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBasepb.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtBasepb.Text = "";
                    txtBasepb.Focus();
                    return;
                }
                _Stype = "PBDEF2";

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceLevelByBook_BPL";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void cmbSelectCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectCat.SelectedValue == "-1")
            {
                txtCate3.Enabled = false;
                btnsrhpbcat3.Enabled = false;
                txtCate2.Enabled = false;
                btnsrhpbcat2.Enabled = false;
                txtCate1.Enabled = false;
                btnsrhpbcat1.Enabled = false;
                txtBaseItem.Enabled = false; ;
                btnItem.Enabled = false;
                txtbbrd.Enabled = false;
                btnsrhpbbrand.Enabled = false;
                txtCharge.Enabled = false;
                btnItemCharge.Enabled = false;

                btnEnableDisable(false, btnsrhpbcat3);
                btnEnableDisable(false, btnsrhpbcat2);
                btnEnableDisable(false, btnsrhpbcat1);
                btnEnableDisable(false, btnItem);
                btnEnableDisable(false, btnsrhpbbrand);
                btnEnableDisable(false, btnItemCharge);
                return;
            }

            ItemBrandCat_List = new List<sar_pb_def_det>();
            grvSalesTypes.DataSource = null;
            txtCate3.Enabled = false;
            btnsrhpbcat3.Enabled = false;
            txtCate2.Enabled = false;
            btnsrhpbcat2.Enabled = false;
            txtCate1.Enabled = false;
            btnsrhpbcat1.Enabled = false;
            txtBaseItem.Enabled = false; ;
            btnItem.Enabled = false;
            txtbbrd.Enabled = false;
            btnsrhpbbrand.Enabled = false;
            txtCharge.Enabled = false;
            btnItemCharge.Enabled = false;

            txtbbrd.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            txtCate3.Text = "";
            txtBaseItem.Text = "";
            txtCharge.Text = "";
            txtMarkup.Text = "";

            btnEnableDisable(false, btnsrhpbcat3);
            btnEnableDisable(false, btnsrhpbcat2);
            btnEnableDisable(false, btnsrhpbcat1);
            btnEnableDisable(false, btnItem);
            btnEnableDisable(false, btnsrhpbbrand);
            btnEnableDisable(false, btnItemCharge);

            if (cmbSelectCat.SelectedValue.ToString() == "10")
            {
                txtCate1.Enabled = true;
                btnsrhpbcat1.Enabled = true;
                btnEnableDisable(true, btnsrhpbcat1);
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "9")
            {
                txtCate2.Enabled = true;
                btnsrhpbcat2.Enabled = true;
                btnEnableDisable(true, btnsrhpbcat2);
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "8")
            {
                txtCate3.Enabled = true;
                btnsrhpbcat3.Enabled = true;
                btnEnableDisable(true, btnsrhpbcat3);
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "7")
            {
                txtbbrd.Enabled = true;
                btnsrhpbbrand.Enabled = true;
                btnEnableDisable(true, btnsrhpbbrand);
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "6")
            {
                txtbbrd.Enabled = true;
                btnsrhpbbrand.Enabled = true;
                btnEnableDisable(true, btnsrhpbbrand);

                txtCate1.Enabled = true;
                btnsrhpbcat1.Enabled = true;
                btnEnableDisable(true, btnsrhpbcat1);

            }
            else if (cmbSelectCat.SelectedValue.ToString() == "5")
            {
                txtbbrd.Enabled = true;
                btnsrhpbbrand.Enabled = true;
                btnEnableDisable(true, btnsrhpbbrand);

                txtCate2.Enabled = true;
                btnsrhpbcat2.Enabled = true;
                btnEnableDisable(true, btnsrhpbcat2);

            }
            else if (cmbSelectCat.SelectedValue.ToString() == "4")
            {

                txtbbrd.Enabled = true;
                btnsrhpbbrand.Enabled = true;
                btnEnableDisable(true, btnsrhpbbrand);

                txtCate3.Enabled = true;
                btnsrhpbcat3.Enabled = true;
                btnEnableDisable(true, btnsrhpbcat3);

            }
            else if (cmbSelectCat.SelectedValue.ToString() == "3")
            {
                txtBaseItem.Enabled = true;
                btnItem.Enabled = true;
                btnEnableDisable(true, btnItem);
                txtBaseItem.ReadOnly = false;
            }


            if (cmbSelectCat.SelectedValue.ToString() == "10")
            {
                grvSalesTypes.Columns[1].HeaderText = "Main Cat.";
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "9")
            {
                grvSalesTypes.Columns[1].HeaderText = "Cat.";
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "8")
            {
                grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "7")
            {
                grvSalesTypes.Columns[1].HeaderText = "Item";
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "6")
            {
                grvSalesTypes.Columns[1].HeaderText = "Main Cat.";
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "5")
            {
                grvSalesTypes.Columns[1].HeaderText = "Cat.";
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "4")
            {
                grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "3")
            {
                grvSalesTypes.Columns[1].HeaderText = "Item";
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "2")
            {
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "1")
            {
            }

            ItemBrandCat_List = new List<sar_pb_def_det>();
            grvSalesTypes.DataSource = ItemBrandCat_List;
            grvSalesTypes.DataBind();

        }

        protected void txtbbrd_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnsrhpbbrand_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "ItemBrand";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCate1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnsrhpbcat1_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Main";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCate2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnsrhpbcat2_Click(object sender, EventArgs e)
        {
            try
            {
                _Stype = "PBDEF";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Sub1";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCate3_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnsrhpbcat3_Click(object sender, EventArgs e)
        {
            try
            {
                _Stype = "PBDEF";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Sub2";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtBaseItem_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtCharge_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnItemCharge_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtMarkup_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "InvoiceItemUnAssable";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void btnEnableDisable(bool isEnable, LinkButton btn)
        {
            if (isEnable)
            {
                btn.Enabled = true;
                btn.CssClass = "buttonUndocolor";
            }
            else
            {
                btn.Enabled = false;
                btn.CssClass = "buttoncolor";
            }
        }

        protected void btnAddCat_Click(object sender, EventArgs e)
        {
            /*
             
         Serial=1
         Promotion=2
         Item=3
         Brand & Sub cat=4
         Brand & Cat=5
         Brand & main cat=6
         Brand=7
         Sub cat=8
         Cat=9
         Main cat=10
           
          */
            if (ItemBrandCat_List == null)
            {
                ItemBrandCat_List = new List<sar_pb_def_det>();
            }
            try
            {
                if (cmbSelectCat.SelectedIndex == 0)
                {
                    DisplayMessage("Please a select type");
                    return;
                }

                if (string.IsNullOrEmpty(txtMarkup.Text))
                {
                    DisplayMessage("Please enter markup");
                    txtMarkup.Focus();
                    return;
                }
                if (cmbCate.SelectedIndex == 0)
                {
                    DisplayMessage("Please select category");
                    cmbCate.Focus();
                    return;
                }


                if (cmbCate.SelectedItem.ToString() == "C")
                {
                    if (string.IsNullOrEmpty(txtCharge.Text))
                    {
                        DisplayMessage("Please enter charge code");
                        txtCharge.Focus();
                        return;
                    }
                }

                if (cmbSelectCat.SelectedValue.ToString() == "4")
                {
                    if (txtCate3.Text == string.Empty)
                    {
                        DisplayMessage("Specify sub category!");
                        return;
                    }
                }
                if (cmbSelectCat.SelectedValue.ToString() == "5")
                {
                    if (txtCate2.Text == string.Empty)
                    {
                        DisplayMessage("Specify  category!");
                        return;
                    }
                }

                if (cmbSelectCat.SelectedValue.ToString() == "6")
                {
                    if (txtCate1.Text == string.Empty)
                    {
                        DisplayMessage("Specify main category!");
                        return;
                    }
                }

                if (cmbSelectCat.SelectedValue.ToString() == "7")
                {
                    if (txtbbrd.Text == string.Empty)
                    {
                        DisplayMessage("Specify brand name!");
                        return;
                    }
                }

                if (cmbSelectCat.SelectedValue.ToString() == "5" || cmbSelectCat.SelectedValue.ToString() == "6" || cmbSelectCat.SelectedValue.ToString() == "4")
                {
                    if (txtbbrd.Text == string.Empty)
                    {
                        DisplayMessage("Specify brand also!");
                        return;
                    }
                }

                if (cmbSelectCat.SelectedValue.ToString() == "3" && string.IsNullOrEmpty(txtBaseItem.Text))
                {
                    DisplayMessage("Select the item");
                    return;
                }

                string selection = "";
                if (cmbSelectCat.SelectedValue.ToString() == "10")
                {
                    selection = "CATE1";
                    grvSalesTypes.Columns[1].HeaderText = "Main Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "9")
                {
                    selection = "CATE2";
                    grvSalesTypes.Columns[1].HeaderText = "Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "8")
                {
                    selection = "CATE3";
                    grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "7")
                {
                    selection = "BRAND";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "6")
                {
                    selection = "BRAND_CATE1";
                    grvSalesTypes.Columns[1].HeaderText = "Main Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "5")
                {
                    selection = "BRAND_CATE2";
                    grvSalesTypes.Columns[1].HeaderText = "Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "4")
                {
                    selection = "BRAND_CATE3";
                    grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "3")
                {
                    selection = "ITEM";
                    grvSalesTypes.Columns[1].HeaderText = "Item";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "2")
                {
                    selection = "PROMOTION";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "1")
                {
                    selection = "SERIAL";
                }
                //ItemBrandCat_List = new List<CashCommissionDetailRef>();

                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtbbrd.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text, txtBaseItem.Text.Trim().ToUpper(), null, txtCircular.Text.Trim(), null);

                if (dt.Rows.Count > 0)
                {
                    //  grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                }
                List<sar_pb_def_det> addList = new List<sar_pb_def_det>();
                foreach (DataRow dr in dt.Rows)
                {
                    string code = dr["code"].ToString();
                    string brand = txtbbrd.Text;
                    sar_pb_def_det obj = new sar_pb_def_det(); //for display purpose


                    MasterItem _msItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), code);

                    if (cmbSelectCat.SelectedValue.ToString() == "4" || cmbSelectCat.SelectedValue.ToString() == "5" || cmbSelectCat.SelectedValue.ToString() == "6")
                    {
                        obj.Spdd_item = code;
                        obj.Spdd_brand = brand;
                        obj.Spdd_cat1 = txtCate1.Text.Trim();
                        obj.Spdd_cat2 = txtCate2.Text.Trim();
                        obj.Spdd_cat3 = txtCate3.Text;
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "3")
                    {
                        obj.Spdd_item = code;
                        obj.Spdd_brand = _msItem.Mi_brand;
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "7")
                    {
                        obj.Spdd_brand = brand;
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "10")
                    {
                        obj.Spdd_item = code;
                        obj.Spdd_cat1 = code;
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "9")
                    {
                        obj.Spdd_item = code;
                        obj.Spdd_cat2 = code;
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "8")
                    {
                        obj.Spdd_item = code;
                        obj.Spdd_cat2 = code;
                    }
                    else
                    {
                        obj.Spdd_brand = string.Empty;
                    }

                    obj.Spdd_Des = code;
                    obj.Spdd_margin = Convert.ToDecimal(txtMarkup.Text);
                    obj.Spdd_ch_code = txtCharge.Text;
                    try
                    {
                        //  obj.Spdd_Des = dr["descript"].ToString();
                    }
                    catch (Exception)
                    {
                        obj.Spdd_cat1 = "";
                    }
                    obj.Spdd_active = 1;
                    var _duplicate = from _dup in ItemBrandCat_List
                                     where _dup.Spdd_Des == obj.Spdd_Des && _dup.Spdd_brand == obj.Spdd_brand
                                     select _dup;

                    if (_duplicate.Count() == 0)
                    {
                        addList.Add(obj);
                    }
                    if (_duplicate.Count() > 0)
                    {
                        DisplayMessage("Duplicate record");
                        return;
                    }

                }
                if (addList == null || addList.Count <= 0)
                {
                    DisplayMessage("Invalid search term, no data found");
                    return;
                }
                grvSalesTypes.AutoGenerateColumns = false;
                ItemBrandCat_List.AddRange(addList);
                grvSalesTypes.AutoGenerateColumns = false;
                grvSalesTypes.DataSource = ItemBrandCat_List;
                grvSalesTypes.DataBind();

                if (dt.Rows.Count > 0)
                {
                    grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.SelectedItem.Text;
                }

                for (int i = 0; i < grvSalesTypes.Rows.Count; i++)
                {
                    GridViewRow dr = grvSalesTypes.Rows[i];
                    CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                    chkSelectPC.Checked = true;
                }

                txtItemCD.Text = "";
                txtCate1.Text = "";
                txtCate2.Text = "";
                txtCate3.Text = "";
                txtBaseItem.Text = "";
                txtCharge.Text = "";
            }
            catch (Exception ex)
            {
                DisplayMessage("Error occurred while processing...\n" + ex.Message, 4);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnRemoveProfitCenter_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            if (select_ITEMS_List != null && select_ITEMS_List.Rows.Count > 0)
            {
                select_ITEMS_List.Rows.RemoveAt(dr.RowIndex);
                grvRedeemItems.DataSource = select_ITEMS_List;
                grvRedeemItems.DataBind();
            }
            pnlRedeemItemsMP.Show();
        }

        protected void btnAllpb_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvSalesTypes.Rows.Count; i++)
            {
                GridViewRow dr = grvSalesTypes.Rows[i];
                CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                chkSelectPC.Checked = true;
            }

        }

        protected void btnAllnone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvSalesTypes.Rows.Count; i++)
            {
                GridViewRow dr = grvSalesTypes.Rows[i];
                CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                chkSelectPC.Checked = false;
            }

        }

        protected void btnAllclr_Click(object sender, EventArgs e)
        {
            grvSalesTypes.DataSource = new int[] { };
            grvSalesTypes.DataBind();
            ItemBrandCat_List = new List<sar_pb_def_det>();
        }

        protected void btnUploadItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FileUploadControl.HasFile)
                {
                    DisplayMessage("Please select a excel file to upload");
                    return;
                }
                if (cmbSelectCat.SelectedIndex == 0)
                {
                    DisplayMessage("Please select a category selection type");
                    return;
                }

                string FileName = Path.GetFileName(FileUploadControl.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUploadControl.PostedFile.FileName);

                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                    default:
                        {
                            DisplayMessageJS("Please select a valid excel file");
                            return;
                        }
                }


                string FolderPath = @"UploadFiles\";

                string FilePath = Server.MapPath(FolderPath + FileName);

                foreach (string filename in Directory.GetFiles(Server.MapPath(FolderPath)))
                {
                    File.Delete(filename);
                }

                FileUploadControl.SaveAs(FilePath);

                FilePath = Server.MapPath(FolderPath + FileName);

                conStr = String.Format(conStr, FilePath, "Yes");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                if (dt.Rows.Count <= 0)
                {
                    DisplayMessage("No data found!");
                    return;
                }

                string _msg = string.Empty;
                StringBuilder _errorLst = new StringBuilder();
                ItemBrandCat_List = new List<sar_pb_def_det>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(_dr[5].ToString()))
                        {
                            continue;
                        }
                        if (cmbSelectCat.SelectedValue.ToString() == "3")
                        {
                            if (string.IsNullOrEmpty(_dr[0].ToString()))
                            {
                                continue;
                            }
                            MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _dr[0].ToString());
                            if (_item == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString()))
                                {
                                    _errorLst.Append("Invalid Item - " + _dr[0].ToString());
                                }
                                else
                                {
                                    _errorLst.Append(" and Invalid Item  - " + _dr[0].ToString());
                                }
                                continue;
                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[0].ToString()
                                                 select _dup;
                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Item " + _dr[0].ToString() + " duplicate");
                                    else _errorLst.Append(" and Item " + _dr[0].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_item = _dr[0].ToString();
                            _ref.Spdd_brand = _dr[1].ToString();
                            _ref.Spdd_Des = _dr[0].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            ItemBrandCat_List.Add(_ref);
                        }
                        else if (cmbSelectCat.SelectedValue.ToString() == "10")// Main category
                        {
                            if (string.IsNullOrEmpty(_dr[2].ToString())) continue;

                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                            if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                                continue;
                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[2].ToString()
                                                 select _dup;


                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category " + _dr[2].ToString() + " duplicate");
                                    else _errorLst.Append(" and main category " + _dr[2].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_cat1 = _dr[2].ToString();
                            _ref.Spdd_Des = _dr[2].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            _ref.Spdd_item = _dr[2].ToString();
                            ItemBrandCat_List.Add(_ref);

                        }
                        else if (cmbSelectCat.SelectedValue.ToString() == "7")// Brand
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr[1].ToString());
                                else _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                                continue;
                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[1].ToString()
                                                 select _dup;
                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand" + _dr[1].ToString() + " duplicate");
                                    else _errorLst.Append(" and brand" + _dr[1].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_brand = _dr[1].ToString();
                            //_ref.Spdd_Des = _dr[1].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            ItemBrandCat_List.Add(_ref);
                        }

                        else if (cmbSelectCat.SelectedValue.ToString() == "9")//  sub category
                        {
                            if (!string.IsNullOrEmpty(_dr[3].ToString()))
                            {
                                MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(_dr[3].ToString());
                                if (subCate.Ric2_cd1 == null)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid sub category - " + _dr[3].ToString());
                                    else _errorLst.Append(" and invalid sub category  - " + _dr[3].ToString());
                                    continue;
                                }
                                if (ItemBrandCat_List != null)
                                {
                                    var _duplicate = from _dup in ItemBrandCat_List
                                                     where _dup.Spdd_Des == _dr[3].ToString()
                                                     select _dup;

                                    if (_duplicate.Count() > 0)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("  category " + _dr[3].ToString() + " duplicate");
                                        else _errorLst.Append(" and   category " + _dr[3].ToString() + " duplicate");
                                        continue;
                                    }
                                }

                                sar_pb_def_det _ref = new sar_pb_def_det();
                                //  _ref.Spdd_cat1 = _dr[0].ToString();
                                _ref.Spdd_cat2 = _dr[3].ToString();
                                _ref.Spdd_Des = _dr[3].ToString();
                                _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                                _ref.Spdd_ch_code = _dr[6].ToString();
                                _ref.Spdd_item = _dr[4].ToString();
                                ItemBrandCat_List.Add(_ref);
                            }
                        }

                        else if (cmbSelectCat.SelectedValue.ToString() == "8")//    category 3
                        {
                            if (!string.IsNullOrEmpty(_dr[4].ToString()))
                            {
                                //   MasterItemSubCate subCate = CHNLSVC.Sales.GetItemran(_dr[4].ToString());

                                //  if (subCate.Ric2_cd1 == null)
                                //  {
                                //    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid sub category - " + _dr[4].ToString());
                                //    else _errorLst.Append(" and invalid sub category  - " + _dr[4].ToString());
                                //    continue;
                                //}
                                //DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                                //if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                                //{
                                //    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                //    else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                                //    continue;
                                //}

                                if (ItemBrandCat_List != null)
                                {
                                    var _duplicate = from _dup in ItemBrandCat_List
                                                     where _dup.Spdd_Des == _dr[4].ToString()
                                                     select _dup;


                                    if (_duplicate.Count() > 0)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("sub category " + _dr[4].ToString() + " duplicate");
                                        else _errorLst.Append(" and sub category " + _dr[4].ToString() + " duplicate");
                                        continue;
                                    }
                                }
                                sar_pb_def_det _ref = new sar_pb_def_det();
                                //  _ref.Spdd_cat1 = _dr[0].ToString();
                                _ref.Spdd_cat3 = _dr[4].ToString();
                                _ref.Spdd_Des = _dr[4].ToString();
                                _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                                _ref.Spdd_ch_code = _dr[6].ToString();
                                _ref.Spdd_item = _dr[3].ToString();
                                ItemBrandCat_List.Add(_ref);
                            }
                        }

                        else if (cmbSelectCat.SelectedValue.ToString() == "6")// Brand/ cat1
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                            if (string.IsNullOrEmpty(_dr[2].ToString())) continue;
                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr[1].ToString());
                                else _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                                continue;
                            }

                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                            if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                                continue;
                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[2].ToString() && _dup.Spdd_brand == _dr[1].ToString()
                                                 select _dup;

                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand/main category " + _dr[2].ToString() + " duplicate");
                                    else _errorLst.Append(" and brand/main category " + _dr[2].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_brand = _dr[1].ToString();
                            _ref.Spdd_cat1 = _dr[2].ToString();
                            _ref.Spdd_Des = _dr[2].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            _ref.Spdd_item = _dr[2].ToString();
                            ItemBrandCat_List.Add(_ref);
                        }

                        else if (cmbSelectCat.SelectedValue.ToString() == "5")// Brand/ cat2
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                            if (string.IsNullOrEmpty(_dr[3].ToString())) continue;
                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString()))
                                    _errorLst.Append("invalid brand - " + _dr[1].ToString());
                                else
                                    _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                                continue;
                            }

                            MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(_dr[3].ToString());

                            if (subCate.Ric2_cd1 == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid   category - " + _dr[3].ToString());
                                else _errorLst.Append(" and invalid   category  - " + _dr[3].ToString());
                                continue;
                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[3].ToString() && _dup.Spdd_brand == _dr[1].ToString()
                                                 select _dup;

                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString()))
                                        _errorLst.Append("brand/  category " + _dr[3].ToString() + " duplicate");
                                    else
                                        _errorLst.Append(" and brand/  category " + _dr[3].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_brand = _dr[1].ToString();
                            _ref.Spdd_cat2 = _dr[3].ToString();
                            _ref.Spdd_cat3 = _dr[4].ToString();
                            _ref.Spdd_Des = _dr[3].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            _ref.Spdd_item = _dr[4].ToString();
                            ItemBrandCat_List.Add(_ref);
                        }

                        else if (cmbSelectCat.SelectedValue.ToString() == "4")// Brand/ cat3
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                            if (string.IsNullOrEmpty(_dr[4].ToString())) continue;

                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString()))
                                    _errorLst.Append("invalid brand - " + _dr[1].ToString());
                                else
                                    _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                                continue;
                            }

                            //MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(_dr[3].ToString());

                            //if (subCate.Ric2_cd1 == null)
                            //{
                            //    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid   category - " + _dr[3].ToString());
                            //    else _errorLst.Append(" and invalid   category  - " + _dr[3].ToString());
                            //    continue;
                            //}
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[4].ToString() && _dup.Spdd_brand == _dr[1].ToString()
                                                 select _dup;

                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand/sub category " + _dr[4].ToString() + " duplicate");
                                    else _errorLst.Append(" and brand/sub   category " + _dr[4].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_brand = _dr[1].ToString();
                            _ref.Spdd_cat2 = _dr[3].ToString();
                            _ref.Spdd_cat3 = _dr[4].ToString();
                            _ref.Spdd_Des = _dr[4].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            _ref.Spdd_item = _dr[3].ToString();
                            ItemBrandCat_List.Add(_ref);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(_errorLst.ToString()))
                {
                    DisplayMessage(_errorLst.ToString());
                    ItemBrandCat_List = new List<sar_pb_def_det>();
                    grvSalesTypes.AutoGenerateColumns = false;
                    grvSalesTypes.DataSource = ItemBrandCat_List;
                    return;
                }

                //foreach (sar_pb_def_det item in ItemBrandCat_List)
                //{
                //    MasterItem _msItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Spdd_item);
                //    if (_msItem != null && !string.IsNullOrEmpty(_msItem.Mi_cd))
                //    {
                //        item.Spdd_cat1 = _msItem.Mi_cate_1;
                //        item.Spdd_cat2 = _msItem.Mi_cate_2;
                //        item.Spdd_cat3 = _msItem.Mi_cate_3;
                //        item.Spdd_brand = _msItem.Mi_brand;
                //    }
                //}

                grvSalesTypes.AutoGenerateColumns = false;
                grvSalesTypes.DataSource = ItemBrandCat_List;
                grvSalesTypes.DataBind();

                DisplayMessage("Upload successfully");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab('SetupAutoDefinition');", true);
            }
            catch (Exception ex)
            {
                DisplayMessage("please upload a correct excel file", 4);
            }
        }

        #endregion

        #region Price Clone

        protected void txtAgeOriPb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeOriPb.Text))
                    return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtAgeOriPb.Text.Trim().ToUpper());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter valid price book");
                    txtAgeOriPb.Text = "";
                    txtAgeOriPb.Focus();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtAgeOriPlevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeOriPlevel.Text))
                    return;
                if (string.IsNullOrEmpty(txtAgeOriPb.Text))
                {
                    DisplayMessage("Please select the price book.");
                    txtAgeOriPb.Text = "";
                    txtAgeOriPb.Focus();
                    return;
                }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtAgeOriPb.Text.Trim().ToUpper(), txtAgeOriPlevel.Text.Trim().ToUpper());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    DisplayMessage("Please enter valid price level.");
                    txtAgeOriPlevel.Text = "";
                    txtAgeOriPlevel.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnAgeOriPb_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceBookByCompany_PC";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnAgeOriPlevl_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeOriPb.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtAgeOriPb.Text = "";
                    txtAgeOriPb.Focus();
                    return;
                }
                _Stype = "";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceLevelByBook_PC";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }

        protected void txtAgeCloPb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeCloPb.Text))
                    return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtAgeCloPb.Text.Trim().ToUpper());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter valid price book");
                    txtAgeCloPb.Text = "";
                    txtAgeCloPb.Focus();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnAgeCloPb_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceBookByCompany_PC2";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtAgeCloPlevl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeCloPlevl.Text))
                    return;
                if (string.IsNullOrEmpty(txtAgeCloPb.Text))
                {
                    DisplayMessage("Please select the price book.");
                    txtAgeCloPb.Text = "";
                    txtAgeCloPb.Focus();
                    return;
                }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtAgeCloPb.Text.Trim().ToUpper(), txtAgeCloPlevl.Text.Trim().ToUpper());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    DisplayMessage("Please enter valid price level.");
                    txtAgeCloPlevl.Text = "";
                    txtAgeCloPlevl.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnAgeCloPlevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeCloPb.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtAgeCloPb.Text = "";
                    txtAgeCloPb.Focus();
                    return;
                }
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters2(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceLevelByBook_PC2";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }


        protected void txtAgeBrand_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAgeBrand.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, "CODE", txtAgeBrand.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {
                    txtAgeBrand.ToolTip = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("please select valid brand");
                }
            }
        }

        protected void btnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Brand_PC";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtAgeCate1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAgeCate1.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, "CODE", txtAgeCate1.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {
                    txtAgeCate1.ToolTip = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("please select valid main category");
                }
            }
        }

        protected void btnMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Main_PC";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtAgeCate2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAgeCate2.Text))
            {
                if (string.IsNullOrEmpty(txtAgeCate1.Text))
                {
                    DisplayMessage("Please enter main category code");
                    txtAgeCate1.Focus();
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, "CODE", txtAgeCate1.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {
                    txtAgeCate2.ToolTip = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("please select valid category");
                }
            }
        }

        protected void txtItemCD_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCD.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, "CODE", txtItemCD.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {
                    txtItemCD.ToolTip = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("please select valid item");
                }
            }
        }

        protected void btnItemCode_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item_PC";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnCat_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Sub1_PC";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnAddItems_Click(object sender, EventArgs e)
        {
            if (txtAgeBrand.Text.Trim() == "" && txtAgeCate1.Text.Trim() == "" && txtAgeCate2.Text.Trim() == "" && txtItemCD.Text.Trim().ToUpper() == "")
            {
                DisplayMessage("Please select a searching value");
                return;
            }

            DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems("ITEM", txtAgeBrand.Text.Trim(), txtAgeCate1.Text.Trim(), txtAgeCate2.Text.Trim(), null, txtItemCD.Text.Trim(), string.Empty, string.Empty, string.Empty);
            if (dt == null || dt.Rows.Count == 0)
            {
                DisplayMessage("Cannot find the item with given criterias.");
                return;
            }
            select_ITEMS_List.Merge(dt);
            grvItemList.DataSource = null;
            grvItemList.AutoGenerateColumns = false;
            grvItemList.DataSource = select_ITEMS_List;
            grvItemList.DataBind();
        }

        protected void btnItemClear_Click(object sender, EventArgs e)
        {
            grvItemList.DataSource = new int[] { };
            grvItemList.DataBind();
            select_ITEMS_List = new DataTable();
        }

        protected void btnItemNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvItemList.Rows.Count; i++)
            {
                GridViewRow dr = grvItemList.Rows[i];
                CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                chkSelectPC.Checked = false;
            }
        }

        protected void btnItemAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvItemList.Rows.Count; i++)
            {
                GridViewRow dr = grvItemList.Rows[i];
                CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                chkSelectPC.Checked = true;
            }
        }

        protected void txtAgeCircular_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.Sales.GetPromoCodesByCir(txtCircular.Text, "", "", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                DisplayMessage("Entered sircular number already in use.");
                txtCircular.Text = "";
                return;
            }
        }

        protected void btnAgeUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (select_ITEMS_List != null && select_ITEMS_List.Rows.Count > 0)
                {
                    DisplayMessage("You have selected Items in list also. Please clear item list before upload.");
                    return;
                }

                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtAgeCircular.Text))
                {
                    DisplayMessage("Please enter circular #.");
                    txtCircular.Text = "";
                    txtCircular.Focus();
                    return;
                }
                if (!FileUpload2.HasFile)
                {
                    DisplayMessage("Please select a excel file to upload");
                    return;
                }
                //if (cmbSelectCat.SelectedIndex == 0)
                //{
                //    DisplayMessage("Please select a category Selection type");
                //    return;
                //}

                string FileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload2.PostedFile.FileName);

                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                    default:
                        {
                            DisplayMessageJS("Please select a valid excel file");
                            return;
                        }
                }


                string FolderPath = @"UploadFiles\";

                string FilePath = Server.MapPath(FolderPath + FileName);

                foreach (string filename in Directory.GetFiles(Server.MapPath(FolderPath)))
                {
                    File.Delete(filename);
                }

                FileUpload2.SaveAs(FilePath);

                FilePath = Server.MapPath(FolderPath + FileName);

                conStr = String.Format(conStr, FilePath, "Yes");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                if (dt.Rows.Count <= 0)
                {
                    DisplayMessage("No data found!");
                    return;
                }

                List<string> _itemLst = new List<string>();
                //add to list
                foreach (DataRow _row in dt.Rows)
                {
                    string _item = _row[0].ToString();
                    string _discount = _row[1].ToString();
                    MasterItem _msItem;
                    if (!string.IsNullOrEmpty(_item))
                    {
                        _msItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                        if (_msItem == null)
                        {
                            DisplayMessage("Invalid item code.");
                            return;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    decimal _tempDisc = 0;
                    bool _isDiscNumber = Decimal.TryParse(_discount, out  _tempDisc);
                    if (!_isDiscNumber)
                    {
                        DisplayMessage("Item - " + _item + "\n discount rate is not number");
                        return;
                    }

                    var _dup = _itemLst.Where(x => x == _row[0].ToString()).ToList();
                    if (_dup != null && _dup.Count > 0)
                    {
                        DisplayMessage("Item - " + _row[0] + "\n is duplicate.");
                        return;
                    }

                    _itemLst.Add(_row[0].ToString().Trim());
                    _ageItems.Add(new Tuple<string, decimal>(_msItem.Mi_cd, Convert.ToDecimal(_discount)));
                }

                DisplayMessage("Upload successfully");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab('PriceClone');", true);
            }
            catch (Exception ex)
            {
                DisplayMessage("Please upload a correct excel file", 4);
            }
        }

        protected void btnAgingSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeCircular.Text))
                {
                    DisplayMessage("Please enter circular number.");
                    return;
                }
                if (string.IsNullOrEmpty(txtAgeOriPb.Text))
                {
                    DisplayMessage("Please enter original price book.");
                    return;
                }
                if (string.IsNullOrEmpty(txtAgeOriPlevel.Text))
                {
                    DisplayMessage("Please enter original price Level.");
                    return;
                }
                if (string.IsNullOrEmpty(txtAgeCloPb.Text))
                {
                    DisplayMessage("Please enter clone price book.");
                    return;
                }
                if (string.IsNullOrEmpty(txtAgeCloPlevl.Text))
                {
                    DisplayMessage("Please enter clone price level.");
                    return;
                }
                if (string.IsNullOrEmpty(txtAgeDisc.Text) && _ageItems.Count == 0)
                {
                    DisplayMessage("Please enter discount rate.");
                    return;
                }
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11054))
                {
                    DisplayMessage("You do not have permission 11054");
                    return;
                }

                if (select_ITEMS_List != null && select_ITEMS_List.Rows.Count > 0)
                {
                    _ageItems = new List<Tuple<string, decimal>>();
                    decimal _Disc = 0;
                    bool isDiscNum = Decimal.TryParse(txtAgeDisc.Text, out _Disc);
                    if (!isDiscNum)
                    {
                        DisplayMessage("Entered discount is not a number.");
                        return;
                    }
                    foreach (DataRow _dr in select_ITEMS_List.Rows)
                    {
                        _ageItems.Add(new Tuple<string, decimal>(_dr["Code"].ToString(), _Disc));
                    }
                }

                string _error;
                int _result = CHNLSVC.Sales.SaveAgePriceActivation(_ageItems, txtAgeOriPb.Text.Trim().ToUpper(), txtAgeOriPlevel.Text.Trim().ToUpper(), txtAgeCloPb.Text.Trim().ToUpper(), txtAgeCloPlevl.Text.Trim().ToUpper(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), txtAgeCircular.Text.ToUpper(), out _error, Convert.ToDateTime(dtAgFrom.Text).Date, Convert.ToDateTime(dtAgTo.Text).Date);
                if (_result == -1)
                {
                    DisplayMessage("Error occurred while processing\n" + _error, 3);
                }
                else
                {
                    DisplayMessage("Successfully saved.", 3);
                }
                btnAgingClear_Click(null, null);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnAgingClear_Click(object sender, EventArgs e)
        {
            txtAgeCloPb.Text = "";
            txtAgeCloPlevl.Text = "";
            txtAgeOriPb.Text = "";
            txtAgeOriPlevel.Text = "";
            txtAgeCircular.Text = "";
            txtAgeBrand.Text = "";
            txtAgeCate1.Text = "";
            txtAgeCate2.Text = "";
            txtItemCD.Text = "";
            gvPreview.DataSource = null;
            gvPreview.DataBind();
            grvItemList.DataSource = null;
            grvItemList.DataBind();
            btnAgingSave.Enabled = true;
            dtAgFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            dtAgTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            select_ITEMS_List = new DataTable();
            _ageItems = new List<Tuple<string, decimal>>();
            txtAgeDisc.Text = "";
        }

        protected void btnAgeView_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeCircular.Text))
                {
                    DisplayMessage("Please enter circular number.");
                    txtAgeCircular.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeOriPb.Text))
                {
                    DisplayMessage("Please enter original price book.");
                    txtAgeOriPb.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeOriPlevel.Text))
                {
                    DisplayMessage("Please enter original price Level.");
                    txtAgeOriPb.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeCloPb.Text))
                {
                    DisplayMessage("Please enter clone price book.");
                    txtAgeOriPb.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeCloPlevl.Text))
                {
                    DisplayMessage("Please enter clone price Level.");
                    txtAgeOriPb.Focus();
                }

                if (select_ITEMS_List != null && select_ITEMS_List.Rows.Count > 0)
                {
                    _ageItems = new List<Tuple<string, decimal>>();
                    decimal _Disc = 0;
                    bool isDiscNum = Decimal.TryParse(txtAgeDisc.Text, out _Disc);
                    if (!isDiscNum)
                    {
                        DisplayMessage("Entered discount is not a number.");
                        return;
                    }
                    foreach (DataRow _dr in select_ITEMS_List.Rows)
                    {
                        _ageItems.Add(new Tuple<string, decimal>(_dr["Code"].ToString(), _Disc));
                    }
                }
                if (_ageItems.Count == 0)
                {
                    DisplayMessage("Please add item to view");
                    return;
                }

                gvPreview.AutoGenerateColumns = false;
                string _error;
                DataTable _priceList;
                //List<MasterCompany> _s = new List<MasterCompany>();
                //_s.ToDataTable();
                CHNLSVC.Sales.GetAgePriceActivation(_ageItems, txtAgeOriPb.Text.Trim().ToUpper(), txtAgeOriPlevel.Text.Trim().ToUpper(), txtAgeCloPb.Text.Trim().ToUpper(), txtAgeCloPlevl.Text.Trim().ToUpper(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), txtAgeCircular.Text.ToUpper(), out _error, out _priceList);
                if (_priceList.Rows.Count > 0)
                {
                    gvPreview.DataSource = _priceList;
                    gvPreview.DataBind();
                }
                else
                {
                    DisplayMessage("No records found.");
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void btnDelgrvItemList_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                if (select_ITEMS_List != null && select_ITEMS_List.Rows.Count > 0)
                {
                    select_ITEMS_List.Rows.RemoveAt(dr.RowIndex);
                    grvItemList.DataSource = select_ITEMS_List;
                    grvItemList.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        #endregion

        #region Similar Item Setup
        protected void btnSaveSimilar_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtMainItem.Text))
                {
                    DisplayMessage("Please select main item.");
                    txtMainItem.Focus();
                    return;
                }

                if (_similarDetails == null || _similarDetails.Count == 0)
                {
                    DisplayMessage("Similar item details are missing.");
                    return;
                }

                if (dgvSimDet.Rows.Count == 0)
                {
                    DisplayMessage("Similar item details are missing.");
                    return;
                }

                List<MasterItemSimilar> _UpdateList = new List<MasterItemSimilar>();

                if (_isSimilarRecal == true)
                {
                    for (int i = 0; i < dgvSimDet.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvSimDet.Rows[i];
                        Label lblmisi_seq_no = dr.FindControl("lblmisi_seq_no") as Label;
                        CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;

                        foreach (MasterItemSimilar _tmpSList in _similarDetails)
                        {
                            if (Convert.ToInt32(lblmisi_seq_no.Text) == _tmpSList.Misi_seq_no)
                            {
                                _tmpSList.Misi_act = chkSelectPC.Checked;
                                _UpdateList.Add(_tmpSList);
                                goto L2;
                            }
                        }
                    L2: Int16 I = 1;
                    }

                    row_aff = (Int32)CHNLSVC.Sales.UpdateSimilarItems(_UpdateList);

                    if (row_aff == 1)
                    {
                        DisplayMessage("Similar item definition updated.", 3);
                        clear_Similar();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            DisplayMessage(_msg, 4);
                        }
                        else
                        {
                            DisplayMessage("Failed to update.");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dgvSimDet.Rows.Count; i++)
                    {

                        GridViewRow dr = dgvSimDet.Rows[i];
                        Label lblmisi_seq_no = dr.FindControl("lblmisi_seq_no") as Label;
                        CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;

                        Boolean _Newact = chkSelectPC.Checked;
                        Int32 _seq = Convert.ToInt32(lblmisi_seq_no.Text.Trim());
                        if (_Newact == true)
                        {
                            foreach (MasterItemSimilar _tmpSList in _similarDetails)
                            {
                                if (_seq == _tmpSList.Misi_seq_no)
                                {
                                    _tmpSList.Misi_act = _Newact;
                                    _UpdateList.Add(_tmpSList);
                                    goto L3;
                                }

                            }
                        L3: Int16 I = 1;
                        }
                    }

                    row_aff = (Int32)CHNLSVC.Sales.SaveSimilarItems(_UpdateList);

                    if (row_aff == 1)
                    {

                        DisplayMessage("Similar item definition created.");
                        clear_Similar();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            DisplayMessage(_msg, 4);
                        }
                        else
                        {
                            DisplayMessage("Field to update.", 4);
                        }
                    }
                }

            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnClearSimilar_Click(object sender, EventArgs e)
        {
            clear_Similar();
        }

        private void clear_Similar()
        {
            btnSimApply.Enabled = true;
            _isSimilarRecal = false;
            txtMainItem.Text = "";
            lblMainModel.Text = "";
            lblMainDesc.Text = "";
            dtpSFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            dtpSTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtInvoiceNo.Text = "";
            txtMainCate.Text = "";
            txtSubCate.Text = "";
            txtItemRange.Text = "";
            txtBrand.Text = "";
            txtItem.Text = "";
            lstItem.Items.Clear();
            txtSChannel.Text = "";
            txtSSubChannel.Text = "";
            txtSPc.Text = "";
            lstSpc.Items.Clear();
            txtSCir.Text = "";
            txtSpromo.Text = "";
            txtMainItem.Enabled = true;
            _similarDetails = new List<MasterItemSimilar>();
            _Stype = "";
            _Ltype = "";
            dgvSimDet.AutoGenerateColumns = false;
            dgvSimDet.DataSource = new List<MasterItemSimilar>();
            dgvSimDet.DataBind();
            lstsPromo.Items.Clear();
            txtBrand_SI.Text = "";
        }

        protected void txtMainItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMainItem.Text))
                {
                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtMainItem.Text.ToUpper());
                    if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        DisplayMessage("Please check the item code");
                        lblMainModel.Text = "";
                        lblMainDesc.Text = "";
                        txtMainItem.Text = "";
                        txtMainItem.Focus();
                    }
                    else
                    {
                        lblMainModel.Text = _itemdetail.Mi_model;
                        lblMainDesc.Text = _itemdetail.Mi_shortdesc;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnSearchMainItem_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item_SI";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                {
                    if (string.IsNullOrEmpty(txtMainItem.Text))
                    {
                        DisplayMessage("Please select item.");
                        txtInvoiceNo.Text = "";
                        txtMainItem.Focus();
                        return;
                    }

                    InvoiceHeader _tmpInv = new InvoiceHeader();
                    _tmpInv = CHNLSVC.Sales.GetInvoiceHdrByCom(Session["UserCompanyCode"].ToString(), txtInvoiceNo.Text.Trim());

                    if (_tmpInv.Sah_inv_no == null)
                    {
                        DisplayMessage("Invalid invoice #");
                        txtInvoiceNo.Text = "";
                        txtInvoiceNo.Focus();
                        return;
                    }

                    InvoiceItem _tmpInvItm = new InvoiceItem();
                    _tmpInvItm = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInvoiceNo.Text.Trim(), txtMainItem.Text.Trim());

                    if (_tmpInvItm == null)
                    {
                        DisplayMessage("No such item found on this invoice");
                        txtInvoiceNo.Text = "";
                        txtInvoiceNo.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnSearchInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetCompanyInvoice);
                DataTable result = CHNLSVC.CommonSearch.GetComInvoice(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "GetCompanyInvoice";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSimHis_Click(object sender, EventArgs e)
        {
            try
            {
                _isSimilarRecal = false;
                if (string.IsNullOrEmpty(txtMainItem.Text))
                {
                    DisplayMessage("Please enter main item.");
                    txtMainItem.Focus();
                    _isSimilarRecal = false;
                    return;
                }

                _similarDetails = new List<MasterItemSimilar>();

                _similarDetails = CHNLSVC.Sales.GetSimilarSetupDet(Session["UserCompanyCode"].ToString(), txtMainItem.Text.Trim().ToUpper(), "S");

                if (_similarDetails.Count == 0)
                {
                    DisplayMessage("Cannot find any setup details for this item.");
                    txtMainItem.Focus();
                    _isSimilarRecal = false;
                    btnSimApply.Enabled = true;
                    return;
                }
                else
                {
                    _isSimilarRecal = true;
                    btnSimApply.Enabled = false;
                }

                dgvSimDet.AutoGenerateColumns = false;
                dgvSimDet.DataSource = _similarDetails;
                dgvSimDet.DataBind();

                if (_similarDetails != null)
                {
                    foreach (MasterItemSimilar _chk in _similarDetails)
                    {
                        for (int i = 0; i < dgvSimDet.Rows.Count; i++)
                        {
                            GridViewRow dr = dgvSimDet.Rows[i];
                            Label lblmisi_seq_no = dr.FindControl("lblmisi_seq_no") as Label;
                            CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                            Label lblMisi_act = dr.FindControl("lblMisi_act") as Label;

                            Int32 _pbSeq = Convert.ToInt32(lblmisi_seq_no.Text);

                            if (_pbSeq == _chk.Misi_seq_no)
                            {
                                chkSelectPC.Checked = true;
                            }

                            if (lblMisi_act.Text == "False")
                            {
                                chkSelectPC.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtMainCate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMainCate.Text))
                    return;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtMainCate.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Please select the valid main category code");
                    txtMainCate.Text = "";
                    txtMainCate.Focus();
                    return;
                }
                txtSubCate.Text = "";
                txtItemRange.Text = "";

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnSearchMainCate_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Main_SI";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtSubCate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCate.Text))
                    return;
                if (!string.IsNullOrEmpty(txtMainCate.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtSubCate.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        DisplayMessage("Please select the valid category code");
                        txtSubCate.Text = "";
                        txtSubCate.Focus();
                        return;
                    }
                }
                else
                {
                    MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(txtSubCate.Text);
                    if (subCate.Ric2_cd == null)
                    {
                        DisplayMessage("Please select the valid category code");
                        txtSubCate.Text = "";
                        txtSubCate.Focus();
                        return;
                    }
                }
                txtItemRange.Text = "";

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void txtItemRange_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItemRange.Text))
                    return;
                if (string.IsNullOrEmpty(txtItemRange.Text))
                {
                    DisplayMessage("Please select the main category first", 4);
                    txtItemRange.Text = "";
                    txtItemRange.Focus();
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtItemRange.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Please select the valid item range.");
                    txtItemRange.Text = "";
                    txtItemRange.Focus();
                    return;
                }

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnSearchRange_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Sub2_SI";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtBrand_SI_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBrand.Text))
                {
                    MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(txtBrand.Text.Trim());

                    if (_brd.Mb_cd == null)
                    {
                        DisplayMessage("Please select the valid brand.");
                        txtBrand.Text = "";
                        txtBrand.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnSearchBrand_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "ItemBrand_SI";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearchSimilarItem_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item_SI_2";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnLoadProducts_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMainCate.Text) && string.IsNullOrEmpty(txtSubCate.Text) && string.IsNullOrEmpty(txtItemRange.Text) && string.IsNullOrEmpty(txtBrand.Text) && string.IsNullOrEmpty(txtItem.Text))
                {
                    DisplayMessage("Please enter searching parameters.");
                    txtMainCate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMainCate.Text) && (!string.IsNullOrEmpty(txtSubCate.Text)))
                {
                    DisplayMessage("Cannot search by sub category only. Please select main category as well.");
                    txtSubCate.Text = "";
                    txtMainCate.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtItemRange.Text) && (string.IsNullOrEmpty(txtSubCate.Text)))
                {
                    DisplayMessage("Cannot search by item range without selecting sub category.");
                    txtItemRange.Text = "";
                    txtSubCate.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    lstItem.Items.Add(txtItem.Text.Trim());
                }
                else
                {
                    lstItem.Items.Clear();
                    List<MasterItem> _tmpItem = CHNLSVC.Sales.GetItemsByCateAndBrand(txtMainCate.Text, txtSubCate.Text, txtItemRange.Text, txtBrand.Text, Session["UserCompanyCode"].ToString());
                    foreach (MasterItem _temp in _tmpItem)
                    {
                        lstItem.Items.Add(_temp.Mi_cd);
                    }
                }

                txtMainCate.Text = "";
                txtSubCate.Text = "";
                txtItemRange.Text = "";
                txtBrand.Text = "";
                txtItem.Text = "";
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnSearchSubCate_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Sub1_SI";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSelectAllItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem Item in lstItem.Items)
                {
                    Item.Selected = true;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnUnselectItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem Item in lstItem.Items)
                {
                    Item.Selected = false;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnClearItem_Click(object sender, EventArgs e)
        {
            lstItem.Items.Clear();
            txtMainCate.Text = "";
            txtSubCate.Text = "";
            txtItemRange.Text = "";
            txtBrand.Text = "";
            txtItem.Text = "";
            txtMainCate.Focus();
        }

        protected void txtSChannel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearchSChannel_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Pc_HIRC_Channel";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtSSubChannel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearchSSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSChannel.Text))
                {
                    DisplayMessage("Please select channel.");
                    txtSChannel.Text = "";
                    txtSChannel.Focus();
                    return;
                }

                _Ltype = "Similar";

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Pc_HIRC_SubChannel";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtSPc_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearchSPc_Click(object sender, EventArgs e)
        {
            try
            {
                _Ltype = "Similar";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Pc_HIRC_Location";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListItem Item in lstSpc.Items)
            {
                Item.Selected = true;
            }
        }

        protected void btnSUnselect_Click(object sender, EventArgs e)
        {
            foreach (ListItem Item in lstSpc.Items)
            {
                Item.Selected = false;
            }
        }

        protected void btnSClear_Click(object sender, EventArgs e)
        {
            txtSChannel.Text = "";
            txtSSubChannel.Text = "";
            txtSPc.Text = "";
            lstSpc.Items.Clear();
            txtSChannel.Focus();
        }

        protected void btnAddSPC_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(Session["UserCompanyCode"].ToString(), txtSChannel.Text, txtSSubChannel.Text, null, null, null, txtSPc.Text.ToUpper());
                foreach (DataRow drow in dt.Rows)
                {
                    lstSpc.Items.Add(drow["PROFIT_CENTER"].ToString());
                }

                txtSChannel.Text = "";
                txtSSubChannel.Text = "";
                txtSPc.Text = "";
                txtSPc.Focus();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void txtSCir_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearchSCir_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                DataTable result = CHNLSVC.CommonSearch.GetPromotionSearch(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Circular";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSearchSPromo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable result = CHNLSVC.CommonSearch.GetPromotionSearch(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Promotion";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSPSelect_Click(object sender, EventArgs e)
        {
            foreach (ListItem Item in lstsPromo.Items)
            {
                Item.Selected = true;
            }
        }

        protected void btnSPUnselect_Click(object sender, EventArgs e)
        {
            foreach (ListItem Item in lstsPromo.Items)
            {
                Item.Selected = false;
            }
        }

        protected void btnSPClear_Click(object sender, EventArgs e)
        {
            txtSCir.Text = "";
            txtSpromo.Text = "";
            txtSCir.Focus();
        }

        protected void txtSpromo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSPromoAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSCir.Text) && string.IsNullOrEmpty(txtSpromo.Text))
                {
                    DisplayMessage("Please enter circular number or promotion code.");
                    txtSCir.Text = "";
                    txtSCir.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtSpromo.Text))
                {
                    lstsPromo.Items.Add(txtSpromo.Text.Trim());
                    return;
                }

                lstsPromo.Items.Clear();
                DataTable dt = CHNLSVC.Sales.GetPromoCodesByCir(txtSCir.Text.Trim(), "", "", "");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        lstsPromo.Items.Add(drow["sapd_promo_cd"].ToString());
                    }
                }
                else
                {
                    DisplayMessage("Please check enter circular #.");
                    txtSCir.Text = "";
                    txtSCir.Focus();
                    return;
                }

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnSimApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMainItem.Text))
                {
                    DisplayMessage("Please enter main item.");
                    txtMainItem.Focus();
                    return;
                }

                Boolean _isValidItm = false;
                foreach (ListItem Item in lstItem.Items)
                {
                    string _item = Item.Text;

                    if (Item.Selected == true)
                    {
                        _isValidItm = true;
                        goto L3;
                    }
                }
            L3:

                if (_isValidItm == false)
                {
                    DisplayMessage("No any applicable similar items are selected.");
                    return;
                }

                MasterItemSimilar _tmpList = new MasterItemSimilar();

                Boolean _isValidPc = false;
                foreach (ListItem Item in lstSpc.Items)
                {
                    string _item = Item.Text;

                    if (Item.Selected == true)
                    {
                        _isValidPc = true;
                        goto L4;
                    }
                }
            L4: Int16 x = 1;

                Boolean _isValidPromo = false;
                foreach (ListItem Item in lstsPromo.Items)
                {
                    string _item = Item.Text;

                    if (Item.Selected == true)
                    {
                        _isValidPromo = true;
                        goto L5;
                    }
                }
            L5: Int16 y = 1;

                if (_isValidPc == false && _isValidPromo == false)
                {
                    foreach (ListItem itmList in lstItem.Items)
                    {
                        string itm = itmList.Text;

                        if (itmList.Selected == true)
                        {
                            _tmpList = new MasterItemSimilar();
                            _tmpList.Misi_act = true;
                            _tmpList.Misi_com = Session["UserCompanyCode"].ToString();
                            _tmpList.Misi_cre_by = Session["UserID"].ToString();
                            _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                            _tmpList.Misi_from = Convert.ToDateTime(dtpSFrom.Text).Date;
                            _tmpList.Misi_to = Convert.ToDateTime(dtpSTo.Text).Date;
                            _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                            _tmpList.Misi_loc = null;
                            _tmpList.Misi_mod_by = Session["UserID"].ToString();
                            _tmpList.Misi_pc = null;
                            _tmpList.Misi_promo = null;
                            _tmpList.Misi_seq_no = _tmpList.Misi_seq_no + 1;
                            _tmpList.Misi_sim_itm_cd = itm;
                            _tmpList.Misi_tp = "S";
                            _similarDetails.Add(_tmpList);
                        }
                    }
                }
                else if (_isValidPc == true && _isValidPromo == false)
                {
                    foreach (ListItem itmList in lstItem.Items)
                    {
                        string itm = itmList.Text;

                        if (itmList.Selected == true)
                        {
                            foreach (ListItem pcList in lstSpc.Items)
                            {
                                string _pc = pcList.Text;

                                if (pcList.Selected == true)
                                {
                                    _tmpList = new MasterItemSimilar();
                                    _tmpList.Misi_act = true;
                                    _tmpList.Misi_com = Session["UserCompanyCode"].ToString();
                                    _tmpList.Misi_cre_by = Session["UserID"].ToString();
                                    _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                                    _tmpList.Misi_from = Convert.ToDateTime(dtpSFrom.Text).Date;
                                    _tmpList.Misi_to = Convert.ToDateTime(dtpSTo.Text).Date;
                                    _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                                    _tmpList.Misi_loc = null;
                                    _tmpList.Misi_mod_by = Session["UserID"].ToString();
                                    _tmpList.Misi_pc = _pc;
                                    _tmpList.Misi_promo = null;
                                    _tmpList.Misi_seq_no = _similarDetails.Count + 1;
                                    _tmpList.Misi_sim_itm_cd = itm;
                                    _tmpList.Misi_tp = "S";
                                    _similarDetails.Add(_tmpList);
                                }
                            }
                        }
                    }
                }
                else if (_isValidPc == false && _isValidPromo == true)
                {
                    foreach (ListItem itmList in lstItem.Items)
                    {
                        string itm = itmList.Text;

                        if (itmList.Selected == true)
                        {
                            foreach (ListItem promoList in lstsPromo.Items)
                            {
                                string _promo = promoList.Text;

                                if (promoList.Selected == true)
                                {
                                    _tmpList = new MasterItemSimilar();
                                    _tmpList.Misi_act = true;
                                    _tmpList.Misi_com = Session["UserCompanyCode"].ToString();
                                    _tmpList.Misi_cre_by = Session["UserID"].ToString();
                                    _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                                    _tmpList.Misi_from = Convert.ToDateTime(dtpSFrom.Text).Date;
                                    _tmpList.Misi_to = Convert.ToDateTime(dtpSTo.Text).Date;
                                    _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                                    _tmpList.Misi_loc = null;
                                    _tmpList.Misi_mod_by = Session["UserID"].ToString();
                                    _tmpList.Misi_pc = null;
                                    _tmpList.Misi_promo = _promo;
                                    _tmpList.Misi_seq_no = _similarDetails.Count + 1;
                                    _tmpList.Misi_sim_itm_cd = itm;
                                    _tmpList.Misi_tp = "S";
                                    _similarDetails.Add(_tmpList);
                                }
                            }
                        }
                    }
                }
                else if (_isValidPc == true && _isValidPromo == true)
                {
                    foreach (ListItem itmList in lstItem.Items)
                    {
                        string itm = itmList.Text;

                        if (itmList.Selected == true)
                        {
                            foreach (ListItem pcList in lstSpc.Items)
                            {
                                string _pc = pcList.Text;

                                if (pcList.Selected == true)
                                {

                                    foreach (ListItem promoList in lstsPromo.Items)
                                    {
                                        string _promo = promoList.Text;

                                        if (promoList.Selected == true)
                                        {
                                            _tmpList = new MasterItemSimilar();
                                            _tmpList.Misi_act = true;
                                            _tmpList.Misi_com = Session["UserCompanyCode"].ToString();
                                            _tmpList.Misi_cre_by = Session["UserID"].ToString();
                                            _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                                            _tmpList.Misi_from = Convert.ToDateTime(dtpSFrom.Text).Date;
                                            _tmpList.Misi_to = Convert.ToDateTime(dtpSTo.Text).Date;
                                            _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                                            _tmpList.Misi_loc = null;
                                            _tmpList.Misi_mod_by = Session["UserID"].ToString();
                                            _tmpList.Misi_pc = _pc;
                                            _tmpList.Misi_promo = _promo;
                                            _tmpList.Misi_seq_no = _tmpList.Misi_seq_no + 1;
                                            _tmpList.Misi_sim_itm_cd = itm;
                                            _tmpList.Misi_tp = "S";
                                            _similarDetails.Add(_tmpList);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                lstItem.Items.Clear();
                lstSpc.Items.Clear();
                lstsPromo.Items.Clear();
                txtMainItem.Enabled = false;
                dgvSimDet.AutoGenerateColumns = false;
                dgvSimDet.DataSource = _similarDetails;
                dgvSimDet.DataBind();

                for (int i = 0; i < dgvSimDet.Rows.Count; i++)
                {
                    GridViewRow dr = dgvSimDet.Rows[i];
                    CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                    chkSelectPC.Checked = true;
                }

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region Promotional Vouchers
        protected void btnProVouSave_Click(object sender, EventArgs e)
        {
            if (hdfTabIndex2.Value != "#profile12")
            {
                if (dgvDefDetails_pv.Rows.Count > 0)
                {
                    int row_aff = CHNLSVC.General.SavePromoVouDefinition(Session["UserID"].ToString());

                    if (row_aff != -99 && row_aff >= 0)
                    {
                        CHNLSVC.Sales.DeleteTempPromoVoucher(Session["UserID"].ToString());
                        DisplayMessage("Successfully saved", 3);
                        ClearPV();
                    }
                    else
                    {
                        DisplayMessage("Error occurred while processing", 4);
                    }
                }
                else
                {
                    DisplayMessage("Please fill details.");
                    return;
                }
            }
            else // promotion voucher types
            {
                if (string.IsNullOrEmpty(txtMinVal.Text))// Nadeeka 20-10-2015
                {
                    DisplayMessage("Enter minimum value.");
                    txtMinVal.Focus();
                    return;
                }

                if (_isUpdate == true) // Update 
                {
                    string P_cd = txtProVouType.Text.Trim().ToUpper();
                    int p_status = 0;
                    Int32 p_qty_wise = 0;
                    Int32 p_SMS = 0;
                    if (optVou.SelectedIndex == 1)
                    {
                        p_status = 1;
                    }
                    else if (optVou.SelectedIndex == 0)
                    {
                        p_status = 0;
                    }
                    if (chkIssueQtywise.Checked == true)
                    {
                        p_qty_wise = 1;
                    }
                    else
                    {
                        p_qty_wise = 0;
                    }

                    if (chkSMS.Checked == true)
                    {
                        p_SMS = 1;
                    }
                    else
                    {
                        p_SMS = 0;
                    }

                    //int row_aff = CHNLSVC.General.UpdateProVouTypes(Session["UserCompanyCode"].ToString(), P_cd, txtProVouTypeDesc.Text.Trim(), p_status, Session["UserID"].ToString(), DateTime.Today.Date, p_qty_wise, p_SMS, txtPurSMS.Text.Trim(), txtRedeemSMS.Text.Trim(), Convert.ToDecimal(txtMinVal.Text), txtCond.Text, _promoVouPara);
                    //if (row_aff != -99 && row_aff >= 0)
                    //{
                    //    DisplayMessage("Successfully updated", 3);
                    //    ClearPV();
                    //}
                    //else
                    //{
                    //    DisplayMessage("Error occurred while processing", 4);
                    //}
                }
                else // insert
                {
                    string P_cd = txtProVouType.Text.Trim().ToUpper();
                    int p_status = 0;
                    Int32 p_qty_wise = 0;
                    Int32 p_SMS = 0;
                    if (optVou.SelectedIndex == 0)
                    {
                        p_status = 1;
                    }
                    else if (optVou.SelectedIndex == 1)
                    {
                        p_status = 0;
                    }

                    if (chkIssueQtywise.Checked == true)
                    {
                        p_qty_wise = 1;
                    }
                    else
                    {
                        p_qty_wise = 0;
                    }
                    if (chkSMS.Checked == true)
                    {
                        p_SMS = 1;
                    }
                    else
                    {
                        p_SMS = 0;
                    }

                    //int row_aff = CHNLSVC.General.SavePromoVouType(Session["UserCompanyCode"].ToString(), P_cd, txtProVouTypeDesc.Text.Trim(), p_status, Session["UserID"].ToString(), p_qty_wise, p_SMS, txtPurSMS.Text.Trim(), txtRedeemSMS.Text.Trim(), Convert.ToDecimal(txtMinVal.Text), txtCond.Text, _promoVouPara);
                    //if (row_aff != -99 && row_aff >= 0)
                    //{
                    //    DisplayMessage("Successfully saved. Code number - " + txtProVouType.Text, 3);
                    //    ClearPV();
                    //}
                    //else
                    //{
                    //    DisplayMessage("Error occurred while processing", 4);
                    //}

                }
                ClearPromotionPoucher();
            }
        }

        protected void btnPDCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnProVouClear_Click(object sender, EventArgs e)
        {
            ClearPV();
        }

        protected void cmbProVouDefType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlListItems.Visible = true;
            pnlSubItems.Visible = false;
            gvVouCat.Visible = false;

            pnlProductBrandCategory.Enabled = true;
            pnlVoucherTypes.Enabled = false;

            if (cmbProVouDefType.Text.Trim() == "Select")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab1('');", true);
                txtCat1_pv.Enabled = false;
                txtCat2_pv.Enabled = false;
                txtCat3_pv.Enabled = false;
                txtItem_pv.Enabled = false;
                txtBrand_pv.Enabled = false;
                lstPDItems.Enabled = false;
                FileUpload3.Enabled = false;
                btnUploadFile_spv.Enabled = false;
                btnLoadPara_pv.Enabled = false;
                lstPDItems.Enabled = false;
                btnSelectAll_pv.Enabled = false;
                btnUnselectAll_pv.Enabled = false;
                btnClear_pv.Enabled = false;
                txtPB_pv.Enabled = false;
                txtPL_pv.Enabled = false;
                txtPB_pv.Focus();
            }
            else if (cmbProVouDefType.Text.Trim() == "Voucher Type(s)")
            {
                pnlProductBrandCategory.Enabled = false;
                pnlVoucherTypes.Enabled = true;

                ClearProVouItems();
                txtProVouType.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2('');", true);
            }
            else if (cmbProVouDefType.Text.Trim() == "Product Wise")
            {
                ClearProVouItems();
                //_tbMainCommDef.SelectedTab = tbProVouItem;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab1('');", true);
                txtCat1_pv.Enabled = true;
                txtCat2_pv.Enabled = true;
                txtCat3_pv.Enabled = true;
                txtItem_pv.Enabled = true;
                txtBrand_pv.Enabled = true;
                lstPDItems.Enabled = true;
                FileUpload3.Enabled = true;
                btnUploadFile_spv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSelectAll_pv.Enabled = true;
                btnUnselectAll_pv.Enabled = true;
                btnClear_pv.Enabled = true;
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                txtPB_pv.Focus();
            }
            else if (cmbProVouDefType.Text.Trim() == "Brand Wise")
            {
                ClearProVouItems();
                //_tbMainCommDef.SelectedTab = tbProVouItem;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab1('');", true);
                txtBrand_pv.Enabled = true;
                txtBrand_pv.Focus();
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSelectAll_pv.Enabled = true;
                btnUnselectAll_pv.Enabled = true;
                btnClear_pv.Enabled = true;

                lstPDItems.Enabled = true;
                FileUpload3.Enabled = true;
                btnUploadFile_spv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;

                txtPB_pv.Focus();
            }
            else if (cmbProVouDefType.Text.Trim() == "Main Category Wise")
            {
                ClearProVouItems();
                //_tbMainCommDef.SelectedTab = tbProVouItem;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab1('');", true);
                txtCat1_pv.Enabled = true;
                txtCat1_pv.Focus();
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSelectAll_pv.Enabled = true;
                btnUnselectAll_pv.Enabled = true;
                btnClear_pv.Enabled = true;

                lstPDItems.Enabled = true;
                FileUpload3.Enabled = true;
                btnUploadFile_spv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                txtPB_pv.Focus();
            }
            else if (cmbProVouDefType.Text.Trim() == "Sub Category Wise")
            {
                pnlListItems.Visible = false;
                pnlSubItems.Visible = true;

                ClearProVouItems();
                //_tbMainCommDef.SelectedTab = tbProVouItem;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab1('');", true);
                txtCat1_pv.Enabled = true;
                txtCat2_pv.Enabled = true;
                txtCat1_pv.Focus();
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSelectAll_pv.Enabled = true;
                btnUnselectAll_pv.Enabled = true;
                btnClear_pv.Enabled = true;

                gvVouCat.Visible = true;

                lstPDItems.Enabled = true;
                FileUpload3.Enabled = true;
                btnUploadFile_spv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                txtPB_pv.Focus();
            }
            else if (cmbProVouDefType.Text.Trim() == "Price Book Wise")
            {
                ClearProVouItems();
                //_tbMainCommDef.SelectedTab = tbProVouItem;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab1('');", true);
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                txtPB_pv.Focus();
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                txtPB_pv.Focus();
            }
            //if (cmbProVouDefType.Text.Trim() != "Voucher Type(s)")
            //{
            //    txtVochCode_pv.Focus();
            //}
        }

        private void ClearProVouItems()
        {
            txtPB_pv.Text = "";
            txtPL_pv.Text = "";
            txtCat1_pv.Text = "";
            txtCat2_pv.Text = "";
            txtCat3_pv.Text = "";
            txtItem_pv.Text = "";
            txtBrand_pv.Text = "";
            lstPDItems.Items.Clear();
            txtProVouType.Text = "";
            txtProVouTypeDesc.Text = "";
            optVou.SelectedIndex = 0;

            txtPB_pv.Enabled = false;
            txtPB_pv.Enabled = false;
            txtPL_pv.Enabled = false;
            txtCat1_pv.Enabled = false;
            txtCat2_pv.Enabled = false;
            txtCat3_pv.Enabled = false;
            txtItem_pv.Enabled = false;
            txtBrand_pv.Enabled = false;
            FileUpload3.Enabled = false;
            btnUploadFile_spv.Enabled = false;
            btnLoadPara_pv.Enabled = false;
            lstPDItems.Enabled = false;
            btnSelectAll_pv.Enabled = false;
            btnUnselectAll_pv.Enabled = false;
            btnClear_pv.Enabled = false;

            _Stype = "";
            _Ltype = "";
        }

        private void ClearPV()
        {
            cmbProVouDefType.SelectedIndex = 0;
            cmbProVouDefType_SelectedIndexChanged(null, null);

            txtVochCode_pv.Text = "";
            lblVouchDesc.Text = "";
            txtPB_pv.Text = "";
            txtCat1_pv.Text = "";
            txtCat3_pv.Text = "";
            txtItem_pv.Text = "";
            txtPL_pv.Text = "";
            txtCat2_pv.Text = "";
            txtBrand_pv.Text = "";
            //FileUpload3.PostedFiles.Clear();
            lstPDItems.Items.Clear();
            txtMinVal.Text = "";
            ClearPromotionPoucher();

            cmbDefby_pv.SelectedIndex = 0;
            cmbDefby_pv_SelectedIndexChanged(null, null);
            txtChnnl_pv.Text = "";
            txtSChnnl_pv.Text = "";
            txtPC_pv.Text = "";
            lstLocations.Items.Clear();
            txtCircular_pv.Text = "";

            dtpFromDate__pv.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            dtpToDate_pv.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            cmbDisType_pv.SelectedIndex = 0;
            txtDis_pv.Text = "";
            dgvDefDetails_pv.DataSource = new int[] { };
            dgvDefDetails_pv.DataBind();

            ClearRedeemItemPln();
            btnCommAdd__pv.Enabled = true;

            //clear temp table
            CHNLSVC.Sales.DeleteTempPromoVoucher(Session["UserID"].ToString());

            isRedeemConfirms = false;

            lblcount.Text = "";
            lblSaveCount.Text = "";

            ClearRedeemItemPln();

            //_foreach (Control c in pnlRedeemItems.Controls)
            //_{
            //_    c.Enabled = true;
            //_}

            //__schemeProcess.Clear();

            //_pnlRedeemItems.Visible = false;

            lblPVStatus.Text = "";
            lblPVStatus.ForeColor = Color.Blue;

            _lstcate2 = new List<MasterItemSubCate>();
            gvVouCat.AutoGenerateColumns = false;

            gvVouCat.DataSource = new int[] { };
            gvVouCat.DataBind();

            _itemLst = new List<string>();

            grvRedeemItems.DataSource = new int[] { };
            grvRedeemItems.DataBind();

            select_ITEMS_List = new DataTable();

            _lstgvPages = new List<GiftVoucherPages>();
            _schemeProcess = new List<PromoVoucherDefinition>();

            grvVouPara.DataSource = new int[] { };
            grvVouPara.DataBind();

            _promoVouPara = new List<PromotionVoucherPara>();

            _isUpdate = false;

            //txtSaleTp.Text = "";
            optPriceTp.Checked = false;
            cmbPriceTp.SelectedIndex = 0;
            optPayTp.Checked = false;
            comboBoxPayModes.SelectedIndex = 0;
            txtPrd.Text = "";

            chkSMS.Checked = false;
            chkSMS_CheckedChanged(null, null);
            BindCompany();
            optVou.SelectedIndex = 0;
        }

        private void ClearPromotionPoucher()
        {
            txtProVouType.Text = "";
            txtProVouTypeDesc.Text = "";
            chkIssueQtywise.Checked = false;
            chkSMS.Checked = false;
            optVou.ClearSelection();
            txtRedeemSMS.Text = "";
            txtPurSMS.Text = "";
            txtRedeemSMS.Enabled = false;
            txtPurSMS.Enabled = false;
            _itemLst = new List<string>();
        }

        private void ClearRedeemItemPln()
        {
            txtItem_rd.Text = "";
            grvRedeemItems.DataSource = new int[] { };
            grvRedeemItems.DataBind();

            cmbRdmAllowCompany.SelectedIndex = 0;
            txtRdmComPB.Text = "";
            txtRdmComPBLvl.Text = "";
            select_ITEMS_List = new DataTable();
            pnlRedeemItemsInner.Enabled = true;
            txtPVRDMValiedPeriod.Text = "";
        }

        protected void txtVochCode_pv_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVochCode_pv.Text))
                    return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable _result = CHNLSVC.CommonSearch.GetDisVouTp(SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtVochCode_pv.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Please select the valid voucher code");
                    txtVochCode_pv.Text = "";
                    txtVochCode_pv.Focus();
                    lblVouchDesc.Text = "";
                    return;
                }
                else
                {
                    lblVouchDesc.Text = _validate[0].ItemArray[1].ToString();
                    txtPB_pv.Focus();
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnVouchAtt_pv_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable result = CHNLSVC.CommonSearch.GetDisVouTp(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "DisVouTp";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtPB_pv_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPB_pv.Text))
                    return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtPB_pv.Text.Trim().ToUpper());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter valid price book");
                    txtPB_pv.Text = "";
                    txtPB_pv.Focus();
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnPB_spv_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceBookByCompany_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCat1_pv_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCat1_pv.Text))
                    return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCat1_pv.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Please select the valid main category code");
                    txtCat1_pv.Text = "";
                    txtCat1_pv.Focus();
                    return;
                }
                txtCat2_pv.Text = "";
                txtCat3_pv.Text = "";

            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnCat1_spv_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Main_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCat3_pv_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCat3_pv.Text))
                {
                    if (string.IsNullOrEmpty(txtCat1_pv.Text))
                    {
                        DisplayMessage("Please select the main category code");
                        txtCat1_pv.Text = "";
                        txtCat1_pv.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtCat2_pv.Text))
                    {
                        DisplayMessage("Please select the sub category code");
                        txtCat2_pv.Text = "";
                        txtCat2_pv.Focus();
                        return;
                    }

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCat3_pv.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        DisplayMessage("Please select the valid item range.");
                        txtCat3_pv.Text = "";
                        txtCat3_pv.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnCat3_spv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCat1_pv.Text))
                {
                    DisplayMessage("Please select the main category code");
                    txtCat1_pv.Text = "";
                    txtCat1_pv.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCat2_pv.Text))
                {
                    DisplayMessage("Please select the sub category code");
                    txtCat2_pv.Text = "";
                    txtCat2_pv.Focus();
                    return;
                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Sub2_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtItem_pv_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItem_pv.Text))
                {
                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem_pv.Text);
                    if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        DisplayMessage("Please check the item code");
                        txtItem_pv.Text = "";
                        txtItem_pv.Focus();
                    }
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnItem_spv_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtPL_pv_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPB_pv.Text))
                {
                    PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtPB_pv.Text.ToUpper().Trim(), txtPL_pv.Text.Trim());
                    if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                    {
                        DisplayMessage("Please enter valid price level.");
                        txtPL_pv.Text = "";
                        txtPL_pv.Focus();
                        return;
                    }
                }

            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnPL_spv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPB_pv.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtPB_pv.Text = "";
                    txtPB_pv.Focus();
                    return;
                }

                _Stype = "PromoVou";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceLevelByBook_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCat2_pv_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtCat2_pv.Text))
                {
                    if (string.IsNullOrEmpty(txtCat1_pv.Text))
                    {
                        DisplayMessage("Please select the main category code");
                        txtCat1_pv.Text = "";
                        txtCat1_pv.Focus();
                        return;
                    }

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCat2_pv.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        DisplayMessage("Please select the valid sub category code");
                        txtCat2_pv.Text = "";
                        txtCat2_pv.Focus();
                        return;
                    }

                    txtCat3_pv.Text = "";
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnCat2_spv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCat1_pv.Text))
                {
                    DisplayMessage("Please select the main category code");
                    txtCat1_pv.Text = "";
                    txtCat1_pv.Focus();
                    return;
                }
                _Stype = "PromoVou";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CAT_Sub1_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtBrand_pv_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtBrand_pv.Text))
                {
                    MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(txtBrand_pv.Text.Trim());
                    if (_brd.Mb_cd == null)
                    {
                        DisplayMessage("Please select the valid brand.");
                        txtBrand_pv.Text = "";
                        txtBrand_pv.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnBrand_spv_Click(object sender, EventArgs e)
        {
            try
            {
                _Stype = "PromoVou";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "ItemBrand_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnLoadPara_pv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCat1_pv.Text) && string.IsNullOrEmpty(txtCat2_pv.Text) && string.IsNullOrEmpty(txtCat3_pv.Text) && string.IsNullOrEmpty(txtBrand_pv.Text) && string.IsNullOrEmpty(txtItem_pv.Text))
                {
                    DisplayMessage("Please enter searching parameters.");
                    txtCat1_pv.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtItem_pv.Text))
                {
                    MasterCompanyItem Item = CHNLSVC.Sales.GetAllCompanyItems(Session["UserCompanyCode"].ToString(), txtItem_pv.Text, 1);
                    if (Item != null)
                    {
                        foreach (ListItem item in lstPDItems.Items)
                        {
                            if (item.Text == txtItem_pv.Text.Trim())
                            {
                                DisplayMessage("Already Exist.");
                                txtItem_pv.Focus();
                                return;
                            }
                        }
                        lstPDItems.Items.Add(txtItem_pv.Text.Trim());
                    }
                }

                else if (cmbProVouDefType.Text.Trim() == "Brand Wise" && !string.IsNullOrEmpty(txtBrand_pv.Text))
                {

                    foreach (ListItem item in lstPDItems.Items)
                    {
                        if (item.Text == txtBrand_pv.Text.Trim())
                        {
                            DisplayMessage("Already Exist.");
                            txtBrand_pv.Focus();
                            return;
                        }
                    }
                    lstPDItems.Items.Add(txtBrand_pv.Text.Trim());
                }
                else if (cmbProVouDefType.Text.Trim() == "Main Category Wise" && !string.IsNullOrEmpty(txtCat1_pv.Text))
                {
                    foreach (ListItem item in lstPDItems.Items)
                    {
                        if (item.Text == txtCat1_pv.Text.Trim())
                        {
                            DisplayMessage("Already Exist.");
                            txtCat1_pv.Focus();
                            return;
                        }
                    }
                    lstPDItems.Items.Add(txtCat1_pv.Text.Trim());
                }
                else if (cmbProVouDefType.Text.Trim() == "Sub Category Wise")
                {
                    if (_lstcate2 == null)
                    {
                        _lstcate2 = new List<MasterItemSubCate>();
                    }

                    if (_lstcate2 != null)
                    {
                        MasterItemSubCate result = _lstcate2.Find(x => x.Ric2_cd == txtCat2_pv.Text && x.Ric2_cd1 == txtCat1_pv.Text);
                        if (result != null)
                        {
                            DisplayMessage("Already Exist");
                            return;
                        }
                    }

                    MasterItemSubCate _cate2 = new MasterItemSubCate();
                    _cate2.Ric2_cd = txtCat2_pv.Text;
                    _cate2.Ric2_cd1 = txtCat1_pv.Text;
                    _lstcate2.Add(_cate2);

                    gvVouCat.AutoGenerateColumns = false;
                    gvVouCat.DataSource = _lstcate2;
                    gvVouCat.DataBind();
                }
                else
                {
                    DataTable _dtItm = CHNLSVC.Sales.GetItemsByCateAndBrandNew(txtCat1_pv.Text, txtCat2_pv.Text, txtCat3_pv.Text, txtBrand_pv.Text, Session["UserCompanyCode"].ToString());

                    if (_dtItm.Rows.Count > 0)
                    {
                        DataTable dtCompanyItems = CHNLSVC.General.GetCompanyItemsByCompany(Session["UserCompanyCode"].ToString());

                        foreach (DataRow drow in _dtItm.Rows)
                        {
                            if (dtCompanyItems.Select("mci_itm_cd = '" + drow["mi_cd"].ToString() + "'").Length > 0)
                            {
                                lstPDItems.Items.Add(drow["mi_cd"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void btnDeleteSubItems_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblRic2_cd = dr.FindControl("lblRic2_cd") as Label;

                if (_lstcate2 != null && _lstcate2.Count > 0)
                {
                    _lstcate2.RemoveAll(x => x.Ric2_cd == lblRic2_cd.Text.Trim());
                }

                gvVouCat.DataSource = _lstcate2;
                gvVouCat.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnUploadFile_spv_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            lstPDItems.Items.Clear();
            if (!FileUpload3.HasFile)
            {
                DisplayMessage("Please select a excel file to upload");
                return;
            }

            string FileName = Path.GetFileName(FileUpload3.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload3.PostedFile.FileName);

            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
                default:
                    {
                        DisplayMessageJS("Please select a valid excel file");
                        return;
                    }
            }


            string FolderPath = @"UploadFiles\";

            string FilePath = Server.MapPath(FolderPath + FileName);

            foreach (string filename in Directory.GetFiles(Server.MapPath(FolderPath)))
            {
                File.Delete(filename);
            }

            FileUpload3.SaveAs(FilePath);

            FilePath = Server.MapPath(FolderPath + FileName);

            conStr = String.Format(conStr, FilePath, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            cmdExcel.Connection = connExcel;

            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(_dt);
            connExcel.Close();

            _itemLst = new List<string>();
            StringBuilder _errorLst = new StringBuilder();
            if (_dt == null || _dt.Rows.Count <= 0)
            {
                DisplayMessage("The excel file is empty. Please check the file.");
                return;
            }

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in _dt.Rows)
                {
                    if (cmbProVouDefType.Text.Trim() == "Product Wise")
                    {
                        DataTable dtCompanyItems = CHNLSVC.General.GetCompanyItemsByCompany(Session["UserCompanyCode"].ToString());
                        if (string.IsNullOrEmpty(_dr[0].ToString())) continue;

                        MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _dr[0].ToString().Trim());
                        if (_item == null)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid item - " + _dr[0].ToString());
                            else _errorLst.Append(" and invalid item - " + _dr[0].ToString());
                            continue;
                        }
                        var _dup = _itemLst.Where(x => x == _dr[0].ToString()).ToList();
                        if (_dup != null && _dup.Count > 0)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("item " + _dr[0].ToString() + " duplicate");
                            else _errorLst.Append(" and item " + _dr[0].ToString() + " duplicate");
                            continue;
                        }

                        if (dtCompanyItems.Select("mci_itm_cd = '" + _dr[0].ToString().Trim() + "'").Length > 0)
                        {
                            lstPDItems.Items.Add(_dr[0].ToString().Trim());
                        }
                    }
                    else if (cmbProVouDefType.Text.Trim() == "Brand Wise")
                    {
                        if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                        MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                        if (_brd.Mb_cd == null)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr[1].ToString());
                            else _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                            continue;
                        }
                        var _dup = _itemLst.Where(x => x == _dr[1].ToString()).ToList();
                        if (_dup != null && _dup.Count > 1)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand " + _dr[1].ToString() + " duplicate");
                            else _errorLst.Append(" and brand " + _dr[1].ToString() + " duplicate");
                            continue;
                        }
                        lstPDItems.Items.Add(_dr[1].ToString().Trim());
                    }

                    else if (cmbProVouDefType.Text.Trim() == "Main Category Wise")
                    {
                        if (string.IsNullOrEmpty(_dr[2].ToString())) continue;

                        DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                        if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                            else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                            continue;
                        }
                        var _dup = _itemLst.Where(x => x == _dr[2].ToString()).ToList();
                        if (_dup != null && _dup.Count > 2)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category " + _dr[2].ToString() + " duplicate");
                            else _errorLst.Append(" and main category " + _dr[2].ToString() + " duplicate");
                            continue;
                        }

                        lstPDItems.Items.Add(_dr[2].ToString().Trim());
                    }

                    else if (cmbProVouDefType.Text.Trim() == "Sub Category Wise")
                    {
                        if (!string.IsNullOrEmpty(_dr[2].ToString()) && !string.IsNullOrEmpty(_dr[3].ToString()))
                        {
                            if (_lstcate2 == null)
                            {
                                _lstcate2 = new List<MasterItemSubCate>();
                            }

                            MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(_dr[3].ToString());

                            if (subCate.Ric2_cd1 == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid sub category - " + _dr[3].ToString());
                                else _errorLst.Append(" and invalid sub category  - " + _dr[3].ToString());
                                continue;
                            }
                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                            if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                                continue;
                            }

                            if (_lstcate2 != null)
                            {
                                MasterItemSubCate result = _lstcate2.Find(x => x.Ric2_cd == _dr[3].ToString().Trim() && x.Ric2_cd1 == _dr[2].ToString().Trim());
                                if (result != null)
                                {

                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category/Sub Category " + _dr[2].ToString() + "/" + _dr[3].ToString() + " duplicate");
                                    else _errorLst.Append(" and main category/Sub Category  " + _dr[2].ToString() + "/" + _dr[3].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            MasterItemSubCate _cate2 = new MasterItemSubCate();
                            _cate2.Ric2_cd = _dr[3].ToString().Trim();
                            _cate2.Ric2_cd1 = _dr[2].ToString().Trim();
                            _lstcate2.Add(_cate2);

                            gvVouCat.DataSource = _lstcate2;
                            gvVouCat.DataBind();
                        }
                    }
                }
            }
        }

        protected void btnSelectAll_pv_Click(object sender, EventArgs e)
        {
            if (cmbProVouDefType.Text.Trim() == "Sub Category Wise")
            {
                for (int i = 0; i < gvVouCat.Rows.Count; i++)
                {
                    GridViewRow dr = gvVouCat.Rows[i];
                    CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                    chkSelectPC.Checked = true;
                }
            }
            else
            {
                foreach (ListItem Item in lstPDItems.Items)
                {
                    Item.Selected = true;
                }
            }
        }

        protected void btnUnselectAll_pv_Click(object sender, EventArgs e)
        {
            if (cmbProVouDefType.Text.Trim() == "Sub Category Wise")
            {
                for (int i = 0; i < gvVouCat.Rows.Count; i++)
                {
                    GridViewRow dr = gvVouCat.Rows[i];
                    CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                    chkSelectPC.Checked = false;
                }
            }
            else
            {
                foreach (ListItem Item in lstPDItems.Items)
                {
                    Item.Selected = false;
                }
            }
        }

        protected void btnClear_pv_Click(object sender, EventArgs e)
        {
            txtPB_pv.Text = "";
            txtPL_pv.Text = "";
            txtCat1_pv.Text = "";
            txtCat2_pv.Text = "";
            txtCat3_pv.Text = "";
            txtItem_pv.Text = "";
            txtBrand_pv.Text = "";
            lstPDItems.Items.Clear();
            _lstcate2 = new List<MasterItemSubCate>();
            gvVouCat.DataSource = new int[] { };
            gvVouCat.DataBind();
        }

        protected void txtChnnl_pv_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtChnnl_pv.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, "CODE", txtChnnl_pv.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {


                }
                else
                {
                    DisplayMessage("Please delete valid channel");
                    txtChnnl_pv.Text = "";
                    txtChnnl_pv.Focus();
                }
            }
        }

        protected void btnChnnl_spv_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Pc_HIRC_Channel_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtSChnnl_pv_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSChnnl_pv.Text))
            {
                if (string.IsNullOrEmpty(txtChnnl_pv.Text))
                {
                    DisplayMessage("Please select channel.");
                    txtChnnl_pv.Text = "";
                    txtChnnl_pv.Focus();
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, "CODE", txtSChnnl_pv.Text.ToUpper().Trim());
                if (result != null && result.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("Please delete valid sub channel");
                    txtSChnnl_pv.Text = "";
                    txtSChnnl_pv.Focus();
                }
            }
        }

        protected void btnSChnnl_spv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChnnl_pv.Text))
                {
                    DisplayMessage("Please select channel.");
                    txtChnnl_pv.Text = "";
                    txtChnnl_pv.Focus();
                    return;
                }
                _Ltype = "PromoVou";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Pc_HIRC_SubChannel_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtPC_pv_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPC_pv.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, "CODE", txtPC_pv.Text.ToUpper().Trim());
                if (result != null && result.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("Please delete valid sub channel");
                    txtSChnnl_pv.Text = "";
                    txtSChnnl_pv.Focus();
                }
            }
        }

        protected void btnPC_spv_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Pc_HIRC_Location_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnLocSelectAll_spc_Click(object sender, EventArgs e)
        {
            foreach (ListItem Item in lstLocations.Items)
            {
                Item.Selected = true;
            }
        }

        protected void btnLocUnselect_spc_Click(object sender, EventArgs e)
        {
            foreach (ListItem Item in lstLocations.Items)
            {
                Item.Selected = false;
            }
        }

        protected void btnLocClr_spc_Click(object sender, EventArgs e)
        {
            txtChnnl_pv.Text = "";
            txtSChnnl_pv.Text = "";
            txtPC_pv.Text = "";
            lstLocations.Items.Clear();
            txtChnnl_pv.Focus();
        }

        protected void btnSelectLocs_pv_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDefby_pv.Text.Trim() == "Profit Center")
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(Session["UserCompanyCode"].ToString(), txtChnnl_pv.Text, txtSChnnl_pv.Text.ToUpper(), null, null, null, txtPC_pv.Text.ToUpper());
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (!checkItemExists(drow["PROFIT_CENTER"].ToString()))
                        {
                            lstLocations.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                    }
                }
                else if (cmbDefby_pv.Text == "Sub Channel")
                {
                    if (string.IsNullOrEmpty(txtSChnnl_pv.Text.Trim()))
                    {
                        DisplayMessage("Select sub channel");
                        return;
                    }
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);

                    if (!string.IsNullOrEmpty(txtSChnnl_pv.Text))
                    {
                        if (!checkItemExists(txtSChnnl_pv.Text))
                        {
                            lstLocations.Items.Add(txtSChnnl_pv.Text);
                        }

                        return;
                    }

                    if (!string.IsNullOrEmpty(txtChnnl_pv.Text))
                    {
                        _Ltype = "PromoVou";
                        string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        DataTable _resultNew = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams2, null, null);
                        foreach (DataRow drow in _resultNew.Rows)
                        {
                            if (!checkItemExists(drow["CODE"].ToString()))
                            {
                                lstLocations.Items.Add(drow["CODE"].ToString());
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (!checkItemExists(drow["CODE"].ToString()))
                            {
                                lstLocations.Items.Add(drow["CODE"].ToString());
                            }
                        }
                        return;
                    }
                }
                txtChnnl_pv.Text = "";
                txtSChnnl_pv.Text = "";
                txtPC_pv.Text = "";
                txtPC_pv.Focus();
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void cmbDefby_pv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDefby_pv.Text == "Profit Center")
            {
                txtChnnl_pv.Text = "";
                txtSChnnl_pv.Text = "";
                txtPC_pv.Text = "";
                txtChnnl_pv.Enabled = true;
                txtPC_pv.Enabled = true;
                txtSChnnl_pv.Enabled = true;
                btnChnnl_spv.Enabled = true;
                btnPC_spv.Enabled = true;
                btnSChnnl_spv.Enabled = true;
                lstLocations.Items.Clear();
                txtPC_pv.Focus();

                txtChnnl_pv.Enabled = false;
                txtSChnnl_pv.Enabled = false;
                btnSChnnl_spv.Enabled = false;
                btnChnnl_spv.Enabled = false;

                btnPC_spv.Enabled = true;

            }
            else if (cmbDefby_pv.Text == "Sub Channel")
            {
                txtChnnl_pv.Text = "";
                txtSChnnl_pv.Text = "";
                txtPC_pv.Text = "";
                txtChnnl_pv.Enabled = true;
                txtPC_pv.Enabled = false;
                txtSChnnl_pv.Enabled = true;
                btnChnnl_spv.Enabled = true;
                btnPC_spv.Enabled = false;
                btnSChnnl_spv.Enabled = true;
                lstLocations.Items.Clear();
                txtChnnl_pv.Focus();

                txtPC_pv.Enabled = false;
                btnPC_spv.Enabled = false;

                txtChnnl_pv.Enabled = true;
                txtSChnnl_pv.Enabled = true;
                btnSChnnl_spv.Enabled = true;
                btnChnnl_spv.Enabled = true;
            }
        }

        protected void txtCircular_pv_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCircular_pv.Text.Trim()))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionVoucherCricular);
                DataTable result = CHNLSVC.CommonSearch.GetPromotionVoucherByCircular(SearchParams, "CIRCULAR", txtCircular_pv.Text.Trim().ToUpper());
                if (result != null && result.Rows.Count > 0)
                {
                    FillPromotionVoucherDefinitionByCircular();
                }
            }
        }

        protected void btnSearchCircular_pv_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionVoucherCricular);
                DataTable result = CHNLSVC.CommonSearch.GetPromotionVoucherByCircular(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PromotionVoucherCricular";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private bool checkItemExists(string ItemText)
        {
            bool isExists = false;

            foreach (ListItem item in lstLocations.Items)
            {
                if (item.Text == ItemText)
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }

        protected void btnRedeemItem_pv_Click(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Show();
            BindCompany();
        }

        protected void btnGv_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtAgeCircular.Text))
            //{
            //    DisplayMessage("Please enter circular #.");
            //    txtCircular.Text = "";
            //    txtCircular.Focus();
            //    return;
            //}

            pnlGVMP.Show();
        }

        protected void btnCommAdd__pv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVochCode_pv.Text))
                {
                    DisplayMessage("Please select voucher code.");
                    txtVochCode_pv.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbProVouDefType.Text))
                {
                    DisplayMessage("Please select definition type.");
                    cmbProVouDefType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPB_pv.Text))
                {
                    DisplayMessage("Please select Price Book.");
                    txtPB_pv.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPL_pv.Text))
                {
                    DisplayMessage("Please select Price Level.");
                    txtPL_pv.Focus();
                    return;
                }
                if (select_ITEMS_List.Rows.Count < 0)
                {
                    DisplayMessage("Please select Redeem Items.");
                    pnlRedeemItems.Visible = true;
                    return;
                }

                //Redeem Item panel validation
                if (string.IsNullOrEmpty(cmbRdmAllowCompany.Text))
                {
                    DisplayMessage("Please select Redeem company.");
                    cmbRdmAllowCompany.Focus();
                    pnlRedeemItemsMP.Show();
                    show_PromoVouch_Product();
                    return;
                }
                if (string.IsNullOrEmpty(txtRdmComPB.Text))
                {
                    DisplayMessage("Please enter the price book for redeem items.");
                    pnlRedeemItems.Visible = true;
                    txtRdmComPB.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtRdmComPBLvl.Text))
                {
                    DisplayMessage("Please enter the price level for redeem items.");
                    pnlRedeemItems.Visible = true;
                    txtRdmComPBLvl.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPVRDMValiedPeriod.Text))
                {
                    DisplayMessage("Please enter the price level Valid period for redeem items.");
                    pnlRedeemItems.Visible = true;
                    txtPVRDMValiedPeriod.Focus();
                    return;
                }

                List<PromoVoucherDefinition> _recallList = new List<PromoVoucherDefinition>();

                _recallList = CHNLSVC.Sales.GetProVouhByCir(txtCircular_pv.Text.ToUpper().Trim()).FindAll(x => x.Spd_com == Session["UserCompanyCode"].ToString());

                if (_recallList.Count > 0 && _recallList != null)
                {

                    var _record = (from _lst in _recallList
                                   select _lst.Spd_stus).Distinct().ToList();


                    foreach (var tmpRec in _record)
                    {
                        if (tmpRec == false)
                        {
                            DisplayMessage("The particular circular is already cancelled.You cannot use this circular.");
                            return;
                        }
                        else if (tmpRec == true)
                        {
                            DisplayMessage("The particular circular is already approved.You cannot use this circular.");
                            return;
                        }
                    }

                }

                if (string.IsNullOrEmpty(txtCircular_pv.Text))
                {
                    DisplayMessage("Please enter circular #.");
                    txtCircular_pv.Focus();
                    return;
                }
                if (Convert.ToDateTime(Convert.ToDateTime(dtpFromDate__pv.Text)).Date < CHNLSVC.Security.GetServerDateTime().Date)
                {
                    DisplayMessage("Valid date cannot back date.");
                    dtpFromDate__pv.Focus();
                    return;
                }
                if (Convert.ToDateTime(Convert.ToDateTime(dtpFromDate__pv.Text)).Date > Convert.ToDateTime(Convert.ToDateTime(dtpToDate_pv.Text)).Date)
                {
                    DisplayMessage("Valid To date cannot less than from date.");
                    dtpToDate_pv.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDis_pv.Text))
                {
                    DisplayMessage("Please enter discount rate / amount.");
                    txtDis_pv.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbDisType_pv.Text))
                {
                    DisplayMessage("Please select discount type.");
                    cmbDisType_pv.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbPVInvoiceType.Text))
                {
                    DisplayMessage("Please select Invoice type.");
                    cmbPVInvoiceType.Focus();
                    return;
                }

                if (cmbDisType_pv.SelectedIndex == 1 && Convert.ToDecimal(txtDis_pv.Text) > 100)
                {
                    DisplayMessage("Discount rate can not be grater than 100.");
                    cmbPVInvoiceType.Focus();
                    return;
                }

                //SchemeCreation ss = new SchemeCreation();
                Boolean _isDisRate = false;
                if (cmbDisType_pv.Text == "RATE")
                { _isDisRate = true; }
                else
                { _isDisRate = false; }

                if (cmbProVouDefType.Text.Trim() == "Price Book Wise")
                {
                    if (string.IsNullOrEmpty(txtPB_pv.Text))
                    {
                        DisplayMessage("Please enter price book.");
                        txtPB_pv.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPL_pv.Text))
                    {
                        DisplayMessage("Please enter price level.");
                        txtPL_pv.Focus();
                        return;
                    }
                }
                else if (cmbProVouDefType.Text.Trim() == "Main Category Wise")
                {
                    if (string.IsNullOrEmpty(txtCat1_pv.Text))
                    {
                        //MessageBox.Show("Please enter main category.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtCat1_pv.Focus();
                        //return;
                    }

                    //if (string.IsNullOrEmpty(txtBrand_pv.Text))
                    //{
                    //    MessageBox.Show("Please enter brand.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtBrand_pv.Focus();
                    //    return;
                    //}
                }
                else if (cmbProVouDefType.Text.Trim() == "Sub Category Wise")
                {
                    if (string.IsNullOrEmpty(txtCat2_pv.Text))
                    {
                        //MessageBox.Show("Please enter sub category.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtCat2_pv.Focus();
                        //return;
                    }
                }
                else if (cmbProVouDefType.Text.Trim() == "Brand Wise")
                {
                    if (string.IsNullOrEmpty(txtBrand_pv.Text))
                    {
                        //MessageBox.Show("Please enter brand.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtBrand_pv.Focus();
                        //return;
                    }


                }
                else if (cmbProVouDefType.Text.Trim() == "Product Wise")
                {
                    Boolean _isValidItm = false;
                    foreach (ListItem Item in lstPDItems.Items)
                    {
                        string _item = Item.Text;

                        if (Item.Selected == true)
                        {
                            _isValidItm = true;
                            goto L3;
                        }
                    }
                L3:

                    if (_isValidItm == false)
                    {
                        DisplayMessage("No any applicable items are selected.");
                        return;
                    }
                }

                Boolean _isPCFound = false;
                foreach (ListItem Item in lstLocations.Items)
                {
                    string pc = Item.Text;
                    if (Item.Selected == true)
                    {
                        _isPCFound = true;
                        goto L1;
                    }
                }
            L1:
                if (_isPCFound == false)
                {
                    DisplayMessage("No any applicable profit center(s)/Channel(s) selected.");
                    return;
                }

                PromoVoucherDefinition _tempProcess = new PromoVoucherDefinition();

                btnProVouSave.Enabled = false;
                btnCommAdd__pv.Enabled = false;
                int index = 0;

                if (cmbProVouDefType.Text.Trim() == "Product Wise" || cmbProVouDefType.Text.Trim() == "Brand Wise" || cmbProVouDefType.Text.Trim() == "Main Category Wise")
                {
                    foreach (ListItem pcList in lstLocations.Items)
                    {
                        if (pcList.Selected == true)
                        {
                            foreach (ListItem itmList in lstPDItems.Items)
                            {
                                if (itmList.Selected == true)
                                {
                                    _tempProcess = new PromoVoucherDefinition();
                                    _tempProcess.Spd_seq = index;
                                    _tempProcess.Spd_com = Session["UserCompanyCode"].ToString();
                                    _tempProcess.Spd_sale_tp = cmbPVInvoiceType.Text;
                                    _tempProcess.Spd_stus = true;
                                    _tempProcess.Spd_vou_cd = txtVochCode_pv.Text;
                                    if (cmbProVouDefType.Text.Trim() == "Product Wise")
                                    {
                                        _tempProcess.Spd_brd = null;
                                        _tempProcess.Spd_cat = null;
                                        _tempProcess.Spd_main_cat = null;
                                        _tempProcess.Spd_itm = itmList.Text.ToUpper().ToString();
                                    }
                                    else if (cmbProVouDefType.Text.Trim() == "Brand Wise")
                                    {
                                        _tempProcess.Spd_brd = itmList.Text.ToUpper().ToString();
                                        _tempProcess.Spd_cat = null;
                                        _tempProcess.Spd_main_cat = null;
                                        _tempProcess.Spd_itm = null;
                                    }
                                    else if (cmbProVouDefType.Text.Trim() == "Main Category Wise")
                                    {
                                        _tempProcess.Spd_brd = null;
                                        _tempProcess.Spd_cat = null;
                                        _tempProcess.Spd_main_cat = itmList.Text.ToUpper().ToString();
                                        _tempProcess.Spd_itm = null;
                                    }


                                    _tempProcess.Spd_circular_no = txtCircular_pv.Text.ToUpper();
                                    _tempProcess.Spd_cre_by = Session["UserID"].ToString();
                                    _tempProcess.Spd_mod_by = Session["UserID"].ToString();
                                    _tempProcess.Spd_disc = Convert.ToDecimal(txtDis_pv.Text);
                                    _tempProcess.Spd_disc_isrt = _isDisRate;
                                    _tempProcess.Spd_from_dt = Convert.ToDateTime(dtpFromDate__pv.Text).Date;
                                    _tempProcess.Spd_to_dt = Convert.ToDateTime(dtpToDate_pv.Text).Date;

                                    _tempProcess.Spd_pb = txtPB_pv.Text.ToUpper();
                                    _tempProcess.Spd_pb_lvl = txtPL_pv.Text;
                                    _tempProcess.Spd_pty_cd = pcList.Text.ToUpper().ToString();

                                    _tempProcess.Spd_period = Convert.ToInt16(txtPVRDMValiedPeriod.Text);
                                    _tempProcess.Spd_rdm_com = cmbRdmAllowCompany.SelectedValue.ToString();
                                    _tempProcess.Spd_rdm_pb = txtRdmComPB.Text;
                                    _tempProcess.Spd_rdm_pb_lvl = txtRdmComPBLvl.Text;

                                    if (cmbDefby_pv.Text == "Profit Center")
                                    { _tempProcess.Spd_pty_tp = "PC"; }
                                    else
                                    { _tempProcess.Spd_pty_tp = "SCHNL"; }
                                    _schemeProcess.Add(_tempProcess);
                                }
                            }
                        }
                        index++;
                    }
                }

                else if (cmbProVouDefType.Text.Trim() == "Sub Category Wise")
                {
                    foreach (ListItem pcList in lstLocations.Items)
                    {

                        if (pcList.Selected == true)
                        {
                            foreach (MasterItemSubCate itmList in _lstcate2)
                            {
                                _tempProcess = new PromoVoucherDefinition();
                                _tempProcess.Spd_seq = index;
                                _tempProcess.Spd_com = Session["UserCompanyCode"].ToString();
                                _tempProcess.Spd_sale_tp = cmbPVInvoiceType.Text;
                                _tempProcess.Spd_stus = true;
                                _tempProcess.Spd_vou_cd = txtVochCode_pv.Text;

                                _tempProcess.Spd_brd = null;
                                _tempProcess.Spd_cat = itmList.Ric2_cd;
                                _tempProcess.Spd_main_cat = itmList.Ric2_cd1;
                                _tempProcess.Spd_itm = null;

                                _tempProcess.Spd_circular_no = txtCircular_pv.Text.ToUpper();
                                _tempProcess.Spd_cre_by = Session["UserID"].ToString();
                                _tempProcess.Spd_mod_by = Session["UserID"].ToString();
                                _tempProcess.Spd_disc = Convert.ToDecimal(txtDis_pv.Text);
                                _tempProcess.Spd_disc_isrt = _isDisRate;
                                _tempProcess.Spd_from_dt = Convert.ToDateTime(dtpFromDate__pv.Text).Date;
                                _tempProcess.Spd_to_dt = Convert.ToDateTime(dtpToDate_pv.Text).Date;

                                _tempProcess.Spd_pb = txtPB_pv.Text.ToUpper();
                                _tempProcess.Spd_pb_lvl = txtPL_pv.Text;
                                _tempProcess.Spd_pty_cd = pcList.Text.ToUpper().ToString();

                                _tempProcess.Spd_period = Convert.ToInt16(txtPVRDMValiedPeriod.Text);
                                _tempProcess.Spd_rdm_com = cmbRdmAllowCompany.SelectedValue.ToString();
                                _tempProcess.Spd_rdm_pb = txtRdmComPB.Text;
                                _tempProcess.Spd_rdm_pb_lvl = txtRdmComPBLvl.Text;

                                if (cmbDefby_pv.Text == "Profit Center")
                                { _tempProcess.Spd_pty_tp = "PC"; }
                                else
                                { _tempProcess.Spd_pty_tp = "SCHNL"; }
                                _schemeProcess.Add(_tempProcess);

                            }
                        }
                        index++;
                    }
                }

                else
                {
                    foreach (ListItem pcList in lstLocations.Items)
                    {
                        if (pcList.Selected == true)
                        {
                            _tempProcess = new PromoVoucherDefinition();
                            _tempProcess.Spd_com = Session["UserCompanyCode"].ToString();
                            _tempProcess.Spd_sale_tp = cmbPVInvoiceType.Text;
                            _tempProcess.Spd_stus = true;
                            _tempProcess.Spd_vou_cd = txtVochCode_pv.Text;
                            _tempProcess.Spd_brd = txtBrand_pv.Text;
                            _tempProcess.Spd_cat = txtCat2_pv.Text;
                            _tempProcess.Spd_circular_no = txtCircular_pv.Text.ToUpper();
                            _tempProcess.Spd_cre_by = Session["UserID"].ToString();
                            _tempProcess.Spd_disc = Convert.ToDecimal(txtDis_pv.Text);
                            _tempProcess.Spd_disc_isrt = _isDisRate;
                            _tempProcess.Spd_from_dt = Convert.ToDateTime(dtpFromDate__pv.Text).Date;
                            _tempProcess.Spd_to_dt = Convert.ToDateTime(dtpToDate_pv.Text).Date;
                            _tempProcess.Spd_itm = null;
                            _tempProcess.Spd_main_cat = txtCat1_pv.Text;
                            _tempProcess.Spd_pb = txtPB_pv.Text.ToUpper();
                            _tempProcess.Spd_pb_lvl = txtPL_pv.Text;
                            _tempProcess.Spd_pty_cd = pcList.Text.ToUpper().ToString();
                            _tempProcess.Spd_mod_by = Session["UserID"].ToString();

                            _tempProcess.Spd_period = Convert.ToInt16(txtPVRDMValiedPeriod.Text);
                            _tempProcess.Spd_rdm_com = cmbRdmAllowCompany.SelectedValue.ToString();
                            _tempProcess.Spd_rdm_pb = txtRdmComPB.Text;
                            _tempProcess.Spd_rdm_pb_lvl = txtRdmComPBLvl.Text;

                            if (cmbDefby_pv.Text == "Profit Center")
                            {
                                _tempProcess.Spd_pty_tp = "PC";
                            }
                            else
                            {
                                _tempProcess.Spd_pty_tp = "SCHNL";
                            }
                            _schemeProcess.Add(_tempProcess);
                        }
                    }
                }

                dgvDefDetails_pv.AutoGenerateColumns = false;
                dgvDefDetails_pv.DataSource = _schemeProcess;
                dgvDefDetails_pv.DataBind();

                var _tempDO = (from _lst in _schemeProcess
                               select _lst.Spd_pty_cd).ToList().Distinct();

                Int32 _saveCount = 0;

                //Clrear Temp table
                CHNLSVC.Sales.DeleteTempPromoVoucher(Session["UserID"].ToString());

                foreach (string _pc in _tempDO)
                {
                    List<PromoVoucherDefinition> tempDo = (from _lst in _schemeProcess
                                                           where _lst.Spd_pty_cd == _pc
                                                           select _lst).ToList();
                    DataTable dt = ConvertToDataTable(tempDo);
                    dt.TableName = "schSchemeCommDef";
                    int _pro = (Int16)(CHNLSVC.Sales.SaveTempPromoVoucher(dt));
                    _saveCount = _saveCount + tempDo.Count;
                    lblcount.Text = _saveCount.ToString();
                    lblSaveCount.Text = _saveCount.ToString();
                }

                SavePromotionVouItemsTemp();

                cmbProVouDefType.Enabled = true;
                //cmbCommDefType_SelectedIndexChanged(null, null);
                btnCommAdd__pv.Enabled = true;
                btnProVouSave.Enabled = true;
                TimeSpan end1 = DateTime.Now.TimeOfDay;

                // MessageBox.Show("Succesfully Saved.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception err)
            {
                btnProVouSave.Enabled = true;
                btnCommAdd__pv.Enabled = true;
                DisplayMessage(err.Message, 4);
            }
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private void SavePromotionVouItemsTemp()
        {
            DataTable DtTemp = new DataTable("tblPromoVouItems");
            DtTemp.Columns.Add("Spi_seq", typeof(Int32));
            DtTemp.Columns.Add("Spi_itm_seq", typeof(Int32));
            DtTemp.Columns.Add("Spi_itm", typeof(string));
            DtTemp.Columns.Add("Spi_brand", typeof(string));
            DtTemp.Columns.Add("Spi_cat1", typeof(string));
            DtTemp.Columns.Add("Spi_cat2", typeof(string));
            DtTemp.Columns.Add("Spi_itm_stus", typeof(string));
            DtTemp.Columns.Add("Spi_act", typeof(Int32));
            DtTemp.Columns.Add("Spi_cre_by", typeof(string));
            DtTemp.Columns.Add("Spi_mod_by", typeof(string));

            for (int i = 0; i < select_ITEMS_List.Rows.Count; i++)
            {
                DataRow drTemp = DtTemp.NewRow();
                drTemp["Spi_seq"] = i.ToString();
                drTemp["Spi_itm_seq"] = i.ToString();
                drTemp["Spi_itm"] = select_ITEMS_List.Rows[i]["CODE"].ToString();
                drTemp["Spi_brand"] = select_ITEMS_List.Rows[i]["BRAND"].ToString();
                drTemp["Spi_cat1"] = select_ITEMS_List.Rows[i]["CAT1"].ToString();
                drTemp["Spi_cat2"] = select_ITEMS_List.Rows[i]["CAT2"].ToString();
                drTemp["Spi_itm_stus"] = "GOD";
                drTemp["Spi_act"] = "1";
                drTemp["Spi_cre_by"] = Session["UserID"].ToString();
                drTemp["Spi_mod_by"] = Session["UserID"].ToString();
                DtTemp.Rows.Add(drTemp);
            }

            int _pro = (Int32)(CHNLSVC.Sales.SaveTempPromoVoucherItems(DtTemp));
        }


        #region Redeem Items Panal
        protected void btnClosePVRedeem_Click(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Hide();
        }

        protected void txtItem_rd_TextChanged(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Show();

        }

        protected void btnItem_rd_Click(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Show();

            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item_PV2";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();

                pnlRedeemItemsMP.Hide();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnUploadFile_rd_Click(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Show();

        }

        private void BindCompany()
        {
            try
            {
                List<MasterCompany> _lst = CHNLSVC.General.GetALLMasterCompaniesData();
                var _n = new MasterCompany();
                _n.Mc_cd = string.Empty;
                _n.Mc_cd = string.Empty;
                _lst.Insert(0, _n);
                cmbRdmAllowCompany.DataSource = _lst;
                cmbRdmAllowCompany.DataTextField = "Mc_desc";
                cmbRdmAllowCompany.DataValueField = "Mc_cd";
                cmbRdmAllowCompany.SelectedValue = Session["UserCompanyCode"].ToString();
                cmbRdmAllowCompany.DataBind();
            }
            catch (Exception)
            {

            }
        }

        protected void txtRdmComPB_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRdmComPB.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, "Book", txtRdmComPB.Text.ToUpper().Trim());
                if (result != null && result.Rows.Count > 0)
                {
                }
                else
                {
                    DisplayMessage("Select a valid price book");
                    txtRdmComPB.Text = "";
                }
            }
            pnlRedeemItemsMP.Show();
        }

        protected void btnRdmComPB_Click(object sender, EventArgs e)
        {
            try
            {
                _Stype = "PromoVou";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceBookByCompany_PV2";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();

                pnlRedeemItemsMP.Hide();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtRdmComPBLvl_TextChanged(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Show();
            if (!string.IsNullOrEmpty(txtRdmComPBLvl.Text))
            {
                if (string.IsNullOrEmpty(txtRdmComPB.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtRdmComPBLvl.Text = "";
                    txtRdmComPBLvl.Focus();
                    return;
                }

                //_Stype = "PromoVouRedeem";
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                //DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, "Price Level", txtRdmComPB.Text);
                //if (result != null && result.Rows.Count > 0)
                //{
                //}
                //else
                //{
                //    DisplayMessage("Select a valid price level");
                //    txtRdmComPBLvl.Text = "";
                //}

                if (!string.IsNullOrEmpty(txtRdmComPB.Text))
                {
                    PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(cmbRdmAllowCompany.SelectedValue.ToString(), txtRdmComPB.Text.Trim(), txtRdmComPBLvl.Text.Trim());
                    if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                    {
                        DisplayMessage("Please enter valid price level.");
                        txtRdmComPBLvl.Text = "";
                        return;
                    }
                }
            }
        }

        protected void btnRdmComPBLvl_Click(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Show();
            try
            {
                if (string.IsNullOrEmpty(txtRdmComPB.Text))
                {
                    DisplayMessage("Please select the price book");
                    txtRdmComPBLvl.Text = "";
                    txtRdmComPBLvl.Focus();
                    return;
                }
                _Stype = "PromoVouRedeem";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceLevelByBook_PV2";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();

                pnlRedeemItemsMP.Hide();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnRedmPnlConfirm_Click(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Show();
            if (string.IsNullOrEmpty(cmbRdmAllowCompany.Text))
            {
                DisplayMessage("Please select Redeem company.");
                cmbRdmAllowCompany.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRdmComPB.Text))
            {
                DisplayMessage("Please enter the price book for redeem items.");
                txtRdmComPB.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRdmComPBLvl.Text))
            {
                DisplayMessage("Please enter the price level for redeem items.");
                txtRdmComPBLvl.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPVRDMValiedPeriod.Text))
            {
                DisplayMessage("Please enter the Valid period for redeem items.");
                txtPVRDMValiedPeriod.Focus();
                return;
            }
            if (grvRedeemItems.Rows.Count == 0)
            {
                DisplayMessage("Please Add Items.");
                txtItem_rd.Focus();
                return;
            }

            isRedeemConfirms = true;
            pnlRedeemItemsInner.Enabled = false;
            pnlRedeemItemsMP.Hide();
        }

        protected void btnRedmPnlClear_Click(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Show();
            ClearRedeemItemPln();
        }

        protected void btnLoadPara_rd_Click(object sender, EventArgs e)
        {
            pnlRedeemItemsMP.Show();
            if (string.IsNullOrEmpty(txtItem_rd.Text))
            {
                DisplayMessage("Please select an item code");
                return;
            }
            if (select_ITEMS_List.Rows.Count > 0)
            {
                if (select_ITEMS_List.Select("CODE = '" + txtItem_rd.Text + "'").Length > 0)
                {
                    DisplayMessage("Selected item is already in the list.");
                    txtItem_rd.Text = "";
                    return;
                }
            }

            DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems("ITEM", string.Empty, string.Empty, string.Empty, string.Empty, txtItem_rd.Text.Trim().ToUpper(), string.Empty, string.Empty, string.Empty);
            dt.Columns.Add("BRAND");
            dt.Columns.Add("CAT1");
            dt.Columns.Add("CAT2");
            MasterItem _itemdetail = new MasterItem();
            _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem_rd.Text.Trim().ToUpper());
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                dt.Rows[0]["BRAND"] = _itemdetail.Mi_brand;
                dt.Rows[0]["CAT1"] = _itemdetail.Mi_cate_1;
                dt.Rows[0]["CAT2"] = _itemdetail.Mi_cate_2;
            }
            select_ITEMS_List.Merge(dt);
            grvRedeemItems.AutoGenerateColumns = false;
            grvRedeemItems.DataSource = select_ITEMS_List;
            grvRedeemItems.DataBind();
            txtItem_rd.Text = "";
        }

        protected void btnGVClose_Click(object sender, EventArgs e)
        {
            pnlGVMP.Hide();
        }

        protected void btnGvUpload_Click(object sender, EventArgs e)
        {
            pnlGVMP.Show();

            try
            {
                if (select_ITEMS_List != null && select_ITEMS_List.Rows.Count > 0)
                {
                    DisplayMessage("You have selected Items in list also. Please Clear Item list before upload.");
                    return;
                }

                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtCircular_pv.Text))
                {
                    DisplayMessage("Please enter circular #.");
                    txtCircular.Text = "";
                    txtCircular.Focus();
                    return;
                }
                if (!fileupexcelupload2.HasFile)
                {
                    DisplayMessage("Please select a excel file to upload");
                    return;
                }
                if (cmbProVouDefType.SelectedIndex == 0)
                {
                    DisplayMessage("Please select a category Selection type");
                    return;
                }

                string FileName = Path.GetFileName(fileupexcelupload2.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload2.PostedFile.FileName);

                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                    default:
                        {
                            DisplayMessageJS("Please select a valid excel file");
                            return;
                        }
                }

                string FolderPath = @"UploadFiles\";

                string FilePath = Server.MapPath(FolderPath + FileName);

                foreach (string filename in Directory.GetFiles(Server.MapPath(FolderPath)))
                {
                    File.Delete(filename);
                }

                fileupexcelupload2.SaveAs(FilePath);

                FilePath = Server.MapPath(FolderPath + FileName);

                conStr = String.Format(conStr, FilePath, "Yes");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();
                string _custCode = "";

                if (dt.Rows.Count <= 0)
                {
                    DisplayMessage("No data found!");
                    return;
                }

                if (dt.Rows.Count > 0)
                {
                    #region validate excel
                    DataRow _drCheck = dt.Rows[0];
                    //foreach (DataRow _dr in dt.Rows)
                    //{
                    //    if (row != _dr)
                    //    {
                    if (string.IsNullOrEmpty(_drCheck[1].ToString()))
                    {
                        DisplayMessage("Process halted. Invalid book number found !");
                        return;
                    }
                    if (string.IsNullOrEmpty(_drCheck[5].ToString()))
                    {
                        DisplayMessage("Process halted. Invalid value found !");
                        return;
                    }

                    if (string.IsNullOrEmpty(_drCheck[0].ToString()))
                    {
                        DisplayMessage("Process halted. Empty GV code found !");
                        return;
                    }
                    MasterItem _Mstitm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _drCheck[0].ToString());
                    if (_Mstitm == null)
                    {
                        DisplayMessage("Process halted. Invalid GV code found !");
                        return;
                    }

                    Int32 _bkno = 0;
                    _bkno = Convert.ToInt32(_drCheck[1].ToString());

                    DataTable _dtlgvbokk = CHNLSVC.Inventory.GetAvailable_GV_books(_bkno, _drCheck[0].ToString(), Session["UserCompanyCode"].ToString());


                    if (_dtlgvbokk.Rows.Count > 0)
                    {
                        DisplayMessage("Process halted. Voucher book already exist !");
                        return;
                    }

                    _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(Session["UserCompanyCode"].ToString(), _drCheck[3].ToString(), string.Empty, string.Empty, "C");
                    if (_custList == null && _custList.Count == 0)
                    {
                        DisplayMessage("Process halted. Registered customer not found !");
                        return;
                    }
                    //    }
                    //}

                    #endregion
                    Int32 _gvline = 0;
                    _lstgvPages = new List<GiftVoucherPages>();
                    DataRow row1 = dt.Rows[0];
                    foreach (DataRow _dr in dt.Rows)
                    {
                        //if (row1 != _dr)
                        {
                            _custCode = "";
                            Int32 _count = _lstgvPages.Where(X => X.Gvp_gv_cd == _dr[0].ToString() && X.Gvp_book == Convert.ToInt32(_dr[1]) && X.Gvp_page == Convert.ToInt32(_dr[4])).Count();
                            if (_count > 0)
                                break;

                            _gvline = _gvline + 1;
                            GiftVoucherPages _gvou = new GiftVoucherPages();
                            _gvou.Gvp_gv_tp = _dr[2].ToString();// "VALUE";
                            _gvou.Gvp_amt = Convert.ToDecimal(_dr[5]);
                            _gvou.Gvp_app_by = Session["UserID"].ToString();
                            _gvou.Gvp_bal_amt = Convert.ToDecimal(_dr[5]);
                            _gvou.Gvp_book = Convert.ToInt32(_dr[1]);
                            _gvou.Gvp_line = _gvline;
                            _gvou.Gvp_can_by = "";
                            _gvou.Gvp_can_dt = DateTime.Now.Date;
                            _gvou.Gvp_com = Session["UserCompanyCode"].ToString();
                            _gvou.Gvp_cre_by = Session["UserID"].ToString();
                            _gvou.Gvp_cre_dt = DateTime.Now.Date;
                            _gvou.Gvp_cus_add1 = _dr[9].ToString();
                            _gvou.Gvp_cus_add2 = _dr[10].ToString();
                            _gvou.Gvp_cus_cd = _dr[3].ToString();
                            _gvou.Gvp_cus_mob = _dr[6].ToString();
                            _gvou.Gvp_cus_name = _dr[8].ToString();
                            _gvou.Gvp_gv_cd = _dr[0].ToString();
                            _gvou.Gvp_gv_prefix = "P_GV";
                            _gvou.Gvp_is_allow_promo = false;
                            _gvou.Gvp_issu_itm = 0;
                            _gvou.Gvp_issue_by = "";
                            _gvou.Gvp_issue_dt = DateTime.Now.Date;
                            //_gvou.Gvp_line = i;
                            _gvou.Gvp_mod_by = "";
                            _gvou.Gvp_mod_dt = DateTime.Now.Date;
                            _gvou.Gvp_noof_itm = 1;
                            _gvou.Gvp_oth_ref = "";
                            _gvou.Gvp_page = Convert.ToInt32(_dr[4]);
                            _gvou.Gvp_pc = "HO";
                            _gvou.Gvp_ref = "";
                            _gvou.Gvp_stus = "A";
                            _gvou.Gvp_valid_from = Convert.ToDateTime(_dr[11].ToString());
                            _gvou.Gvp_valid_to = Convert.ToDateTime(_dr[12].ToString());
                            _gvou.Gvp_cus_nic = _dr[7].ToString();

                            _lstgvPages.Add(_gvou);
                        }
                    }
                }

                if (_lstgvPages.Count == 0)
                {
                    DisplayMessage("Gift voucher details not found !");
                    return;
                }
                Int32 _eff = CHNLSVC.Sales.SaveGiftVoucherPages(_lstgvPages);
                DisplayMessage("Upload Successfully");
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        #endregion

        protected void btnRemoveVouchpara_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Int32 rowIndex = dr.RowIndex;
                if (_promoVouPara != null && _promoVouPara.Count > 0)
                {
                    _promoVouPara.RemoveAt(rowIndex);
                }

                grvVouPara.DataSource = _promoVouPara;
                grvVouPara.DataBind();
                setDesc();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtSaleTp_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
        }

        protected void btn_srch_sale_tp_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable result = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Sales_Type";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void BindPriceType()
        {
            List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            _list.Add(new PriceTypeRef { Sarpt_cd = "", Sarpt_indi = -1 });
            //cmbPriceType.DataSource = _list;
            //cmbPriceType.DisplayMember = "Sarpt_cd";
            //cmbPriceType.ValueMember = "Sarpt_indi";

            cmbPriceTp.Items.Clear();
            cmbPriceTp.DataSource = _list;
            cmbPriceTp.DataTextField = "Sarpt_cd";
            cmbPriceTp.DataValueField = "Sarpt_indi";
            cmbPriceTp.DataBind();
        }

        private void bindPayTypes()
        {
            List<string> payTypes = new List<string>();
            DataTable _paymentTypeRef = CHNLSVC.Sales.GetPossiblePayTypes(Session["UserCompanyCode"].ToString(), "ALL", Session["UserDefProf"].ToString(), null, DateTime.Now.Date);
            for (int i = 0; i < _paymentTypeRef.Rows.Count; i++)
            {
                payTypes.Add(_paymentTypeRef.Rows[i]["Stp_pay_tp"].ToString());
            }
            comboBoxPayModes.DataSource = payTypes;
            comboBoxPayModes.DataBind();
        }

        protected void btnAddSaleTp_Click(object sender, EventArgs e)
        {
            if (optPayTp.Checked == false && optSaleTp.Checked == false && optPriceTp.Checked == false)
            {
                DisplayMessage("Please select the option");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
                return;
            }

            if (_promoVouPara == null)
            {
                _promoVouPara = new List<PromotionVoucherPara>();
            }

            PromotionVoucherPara _vouPara = new PromotionVoucherPara();
            _vouPara.Spdp_vou_cd = txtProVouType.Text;

            if (optSaleTp.Checked == true)
            {
                _vouPara.Spdp_tp = optSaleTp.ToolTip.ToString();
                _vouPara.Spdp_sale_tp = ddlhpSalesAccept.SelectedValue.ToString();

                if (_promoVouPara.FindAll(x => x.Spdp_tp == _vouPara.Spdp_tp && x.Spdp_sale_tp == _vouPara.Spdp_sale_tp).Count > 0)
                {
                    DisplayMessage("Invoice type is already added.");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
                    return;
                }
            }
            if (optPayTp.Checked == true)
            {
                if (string.IsNullOrEmpty(txtPrd.Text))
                {
                    DisplayMessage("please enter period");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
                    return;
                }
                _vouPara.Spdp_tp = optPayTp.ToolTip.ToString();
                _vouPara.Spdp_pay_tp = comboBoxPayModes.Text;
                _vouPara.Spdp_pay_prd = Convert.ToInt32(txtPrd.Text);

                if (_promoVouPara.FindAll(x => x.Spdp_tp == _vouPara.Spdp_tp && x.Spdp_pay_tp == _vouPara.Spdp_pay_tp).Count > 0)
                {
                    DisplayMessage("Price type is already added.");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
                    return;
                }
            }
            if (optPriceTp.Checked == true)
            {
                _vouPara.Spdp_tp = optPriceTp.ToolTip.ToString();
                _vouPara.Spdp_price_tp = cmbPriceTp.Text;

                if (_promoVouPara.FindAll(x => x.Spdp_tp == _vouPara.Spdp_tp && x.Spdp_price_tp == _vouPara.Spdp_price_tp).Count > 0)
                {
                    DisplayMessage("Pay Mode is already added.");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
                    return;
                }
            }

            _promoVouPara.Add(_vouPara);

            grvVouPara.AutoGenerateColumns = false;
            grvVouPara.DataSource = _promoVouPara;
            grvVouPara.DataBind();
            setDesc();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
        }

        protected void txtProVouType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProVouType.Text))
                    return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable _result = CHNLSVC.General.GetProVoutype(Session["UserCompanyCode"].ToString(), txtProVouType.Text.Trim());
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("spt_vou_cd") == txtProVouType.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    txtProVouTypeDesc.Focus();
                }
                else
                {
                    _isUpdate = true;
                    txtProVouTypeDesc.Text = _validate[0]["spt_vou_desc"].ToString();
                    txtCond.Text = _validate[0]["spt_cond"].ToString();
                    if (Convert.ToInt32(_validate[0]["SPT_ACT"].ToString()) == 1)
                    {
                        optVou.SelectedIndex = 0;
                    }
                    else
                    {
                        optVou.SelectedIndex = 1;
                    }

                    if (Convert.ToInt32(_validate[0]["spt_is_qtywise"].ToString()) == 1)
                    {
                        chkIssueQtywise.Checked = true;
                    }
                    else
                    {
                        chkIssueQtywise.Checked = false;
                    }

                    if (Convert.ToInt32(_validate[0]["spt_sms_alert"].ToString()) == 1)
                    {
                        chkSMS.Checked = true;
                        txtPurSMS.Enabled = true;
                        txtRedeemSMS.Enabled = true;
                        txtPurSMS.Text = _validate[0]["spt_cus_pur_sms"].ToString();
                        txtRedeemSMS.Text = _validate[0]["spt_cus_red_sms"].ToString();
                        txtMinVal.Text = _validate[0]["spt_min_val"].ToString();
                    }
                    else
                    {
                        chkSMS.Checked = false;
                        txtPurSMS.Enabled = false;
                        txtRedeemSMS.Enabled = false;
                        txtPurSMS.Text = "";
                        txtRedeemSMS.Text = "";
                        txtMinVal.Text = "";
                    }
                    _promoVouPara = CHNLSVC.General.GET_VOUPARA_BY_CD(txtProVouType.Text.Trim().ToUpper());
                    grvVouPara.DataSource = _promoVouPara;
                    grvVouPara.DataBind();
                    setDesc();
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
        }

        protected void btnProVouType_spv_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable result = CHNLSVC.CommonSearch.GetPromotionVoucherAll(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "DisVouTp_PV";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void chkSMS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSMS.Checked == true)
            {
                txtPurSMS.Enabled = true;
                txtRedeemSMS.Enabled = true;
                txtMinVal.Enabled = true;
            }
            else
            {
                txtPurSMS.Enabled = false;
                txtRedeemSMS.Enabled = false;
                txtMinVal.Enabled = false;
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
        }

        private void show_PromoVouch_Product()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab1();", true);
        }

        private void show_PromoVouch_Vouchers()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "SetTab2();", true);
        }

        private void FillPromotionVoucherDefinitionByCircular()
        {
            List<PromoVoucherDefinition> _recallList = new List<PromoVoucherDefinition>();
            _recallList = CHNLSVC.Sales.GetProVouhByCir(txtCircular_pv.Text.Trim().ToUpper());

            if (_recallList.Count > 0)
            {
                ClearPV();
                txtCircular_pv.Text = _recallList[0].Spd_circular_no;
                dgvDefDetails_pv.AutoGenerateColumns = false;
                dgvDefDetails_pv.DataSource = _recallList;
                dgvDefDetails_pv.DataBind();
                lblcount.Text = _recallList.Count.ToString();

                int ItemCount = _recallList.FindAll(x => x.Spd_itm != null || x.Spd_itm != string.Empty).Count;

                if (ItemCount > 0)
                {
                    if (!string.IsNullOrEmpty(_recallList[0].Spd_pb) && !string.IsNullOrEmpty(_recallList[0].Spd_pb_lvl))
                    {
                        //cmbProVouDefType.SelectedItem.Text = "Price Book Wise";
                        cmbProVouDefType.SelectedIndex = 6;
                    }
                    if (!string.IsNullOrEmpty(_recallList[0].Spd_pb) && !string.IsNullOrEmpty(_recallList[0].Spd_pb_lvl) && !string.IsNullOrEmpty(_recallList[0].Spd_main_cat))
                    {
                        //cmbProVouDefType.Text = "Main Category Wise";
                        cmbProVouDefType.SelectedIndex = 4;
                    }
                    if (!string.IsNullOrEmpty(_recallList[0].Spd_pb) && !string.IsNullOrEmpty(_recallList[0].Spd_pb_lvl) && !string.IsNullOrEmpty(_recallList[0].Spd_main_cat) && !string.IsNullOrEmpty(_recallList[0].Spd_cat))
                    {
                        //cmbProVouDefType.Text = "Sub Category Wise";
                        cmbProVouDefType.SelectedIndex = 5;
                    }
                    if (!string.IsNullOrEmpty(_recallList[0].Spd_pb) && !string.IsNullOrEmpty(_recallList[0].Spd_pb_lvl) && !string.IsNullOrEmpty(_recallList[0].Spd_brd))
                    {
                        //cmbProVouDefType.Text = "Brand Wise";
                        cmbProVouDefType.SelectedIndex = 3;
                    }
                    txtVochCode_pv.Text = _recallList[0].Spd_vou_cd;
                    txtVochCode_pv_TextChanged(null, null);
                    txtPB_pv.Text = _recallList[0].Spd_pb;
                    txtPL_pv.Text = _recallList[0].Spd_pb_lvl;
                    txtCat1_pv.Text = _recallList[0].Spd_main_cat;
                    txtCat2_pv.Text = _recallList[0].Spd_cat;
                    txtBrand_pv.Text = _recallList[0].Spd_brd;
                    if (_recallList[0].Spd_disc_isrt)
                    {
                        //cmbDisType_pv.Text = "RATE";
                        cmbDisType_pv.SelectedIndex = 1;
                    }
                    else
                    {
                        //cmbDisType_pv.Text = "VALUE";
                        cmbDisType_pv.SelectedIndex = 0;
                    }
                    txtDis_pv.Text = _recallList[0].Spd_disc.ToString("N");
                    dtpFromDate__pv.Text = _recallList[0].Spd_from_dt.ToString("dd/MMM/yyyy");
                    dtpToDate_pv.Text = _recallList[0].Spd_to_dt.ToString("dd/MMM/yyyy");

                    if (_recallList[0].Spd_pty_tp == "PC")
                    {
                        //cmbDefby_pv.Text = "Profit Center";
                        cmbDefby_pv.SelectedIndex = 1;
                    }
                    else
                    {
                        //cmbDefby_pv.Text = "Sub Channel";
                        cmbDefby_pv.SelectedIndex = 0;
                    }
                    foreach (PromoVoucherDefinition item in _recallList)
                    {
                        if (!checkItemExists(item.Spd_pty_cd))
                        {
                            lstLocations.Items.Add(item.Spd_pty_cd);
                        }
                    }

                    if (_recallList[0].Spd_stus == true)
                    {
                        lblPVStatus.ForeColor = Color.Blue;
                        lblPVStatus.Text = "Active";

                    }
                    else
                    {
                        lblPVStatus.ForeColor = Color.Red;
                        lblPVStatus.Text = "De-Active";
                    }

                    cmbPVInvoiceType.Text = _recallList[0].Spd_sale_tp;

                    var seletedItems = (from _temp in _recallList select _temp.Spd_itm).Distinct().ToList();

                    foreach (var item in seletedItems)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            lstPDItems.Items.Add(item);
                            //cmbProVouDefType.Text = "Product Wise";
                            cmbProVouDefType.SelectedIndex = 2;
                        }
                    }

                    cmbRdmAllowCompany.SelectedValue = _recallList[0].Spd_rdm_com;

                    txtRdmComPB.Text = _recallList[0].Spd_rdm_pb;
                    txtRdmComPBLvl.Text = _recallList[0].Spd_rdm_pb_lvl;
                    txtPVRDMValiedPeriod.Text = _recallList[0].Spd_period.ToString();

                    DataTable dtRedeemItems = new DataTable();
                    dtRedeemItems.Merge(CHNLSVC.Sales.GetPromotionItemsByBatchSeq(_recallList[0].SPD_BATCH_SEQ));

                    if (select_ITEMS_List.Columns.Count == 0)
                    {
                        select_ITEMS_List.Columns.Add("CODE");
                        select_ITEMS_List.Columns.Add("BRAND");
                        select_ITEMS_List.Columns.Add("CAT1");
                        select_ITEMS_List.Columns.Add("CAT2");
                    }

                    if (dtRedeemItems.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtRedeemItems.Rows)
                        {
                            DataRow drRDItem = select_ITEMS_List.NewRow();
                            drRDItem["CODE"] = item["spi_itm"].ToString();
                            drRDItem["BRAND"] = item["spi_brand"].ToString();
                            drRDItem["CAT1"] = item["spi_cat1"].ToString();
                            drRDItem["CAT2"] = item["spi_cat2"].ToString();
                            select_ITEMS_List.Rows.Add(drRDItem);
                        }
                        grvRedeemItems.AutoGenerateColumns = false;
                        grvRedeemItems.DataSource = select_ITEMS_List;
                        grvRedeemItems.DataBind();
                    }
                    dgvDefDetails_pv.Focus();
                }
            }
        }

        protected void btnRemoveDetails_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;

            if (_schemeProcess != null && _schemeProcess.Count > 0)
            {
                _schemeProcess.RemoveAt(dr.RowIndex);
                dgvDefDetails_pv.DataSource = _schemeProcess;
                dgvDefDetails_pv.DataBind();
            }
        }

        protected void btnDeleteSalesTypes_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                if (ItemBrandCat_List != null && ItemBrandCat_List.Count > 0)
                {
                    ItemBrandCat_List.RemoveAt(dr.RowIndex);
                    DisplayMessage("Successfully deleted");
                }

                grvSalesTypes.AutoGenerateColumns = false;
                grvSalesTypes.DataSource = ItemBrandCat_List;
                grvSalesTypes.DataBind();



            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void setDesc()
        {
            try
            {
                if (grvVouPara.Rows.Count > 0)
                {
                    List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(string.Empty);
                    for (int i = 0; i < grvVouPara.Rows.Count; i++)
                    {
                        GridViewRow dr = grvVouPara.Rows[i];
                        Label lblSPDP_PRICE_TP = dr.FindControl("lblSPDP_PRICE_TP") as Label;
                        if (!string.IsNullOrEmpty(lblSPDP_PRICE_TP.Text) && _list.FindAll(x => x.Sarpt_indi == Convert.ToInt32(lblSPDP_PRICE_TP.Text.Trim())).Count > 0)
                        {
                            lblSPDP_PRICE_TP.Text = _list.Find(x => x.Sarpt_indi == Convert.ToInt32(lblSPDP_PRICE_TP.Text.Trim())).Sarpt_desc;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        protected void cmbCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCate.SelectedValue != "I")
            {
                txtCharge.Enabled = true;
                btnEnableDisable(true, btnItemCharge);
            }
            else
            {
                btnEnableDisable(false, btnItemCharge);
                txtCharge.Enabled = false;
            }
        }

        protected void lbtnSeProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "UserProfitCenterDis";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSeInvTp_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProfCenter.Text))
                {
                    DisplayMessage("Please select a profit center !!!", 1); return;
                }
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                _priceRef = new PriceDefinitionRef() {
                    Sadd_com = Session["UserCompanyCode"].ToString(),
                    Sadd_pc = txtProfCenter.Text
                };
                DataTable result = CHNLSVC.CommonSearch.SearchDocPriceDefDocTp(_priceRef, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "InvoiceType";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
           }
            catch (Exception err)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }

        protected void lbtnSePriceBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvType.Text))
                {
                    DisplayMessage("Please select a invoice type !!!", 1); return;
                }
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                _priceRef = new PriceDefinitionRef()
                {
                    Sadd_com = Session["UserCompanyCode"].ToString(),
                    Sadd_pc = txtProfCenter.Text.ToUpper(),
                    Sadd_doc_tp=txtInvType.Text.ToUpper()
                };
                DataTable result = CHNLSVC.CommonSearch.SearchDocPriceDefPrBook(_priceRef, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceBook";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception err)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }
        protected void lbtnSePrLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text))
                {
                    DisplayMessage("Please select a price book !!!", 1); return;
                }
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                _priceRef = new PriceDefinitionRef()
                {
                    Sadd_com = Session["UserCompanyCode"].ToString(),
                    Sadd_pc = txtProfCenter.Text.ToUpper(),
                    Sadd_doc_tp = txtInvType.Text.ToUpper(),
                    Sadd_pb=txtPriceBook.Text.ToUpper()
                };
                DataTable result = CHNLSVC.CommonSearch.SearchDocPriceDefPrLVL(_priceRef, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "PriceLevel";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception err)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {

        }
        private void BindDisGrid()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProfCenter.Text))
                {
                    _priceDefRef = CHNLSVC.Sales.GetPriceDefinitionRefs(new PriceDefinitionRef()
                    {
                        Sadd_com = Session["UserCompanyCode"].ToString(),
                        Sadd_pc = txtProfCenter.Text.ToUpper()
                    });
                    if (_priceDefRef.Count > 0)
                    {
                        dgvDiscount.DataSource = _priceDefRef;
                        dgvDiscount.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }
        private void ClearDiscount() {
            txtProfCenter.Text = "";
            txtInvType.Text = "";
            txtPriceBook.Text = "";
            txtPriceLevel.Text = "";
            chkAllowDiscount.Checked = false;
            txtDisRate.Text = "";
            txtDisRate.Enabled = false;
            chkAllowEditPrice.Checked = false;
            txtEditRate.Text = "";
            txtEditRate.Enabled = false;
            dgvDiscount.DataSource = new int[] { };
            dgvDiscount.DataBind();
        }

        protected void txtProfCenter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDiscount.DataSource = new int[] { };
                dgvDiscount.DataBind();
                txtInvType.Text = "";
                txtPriceBook.Text = "";
                txtPriceLevel.Text = "";
                chkAllowDiscount.Checked = false;
                chkAllowDiscount_CheckedChanged(null,null);
                chkAllowEditPrice.Checked = false;
                chkAllowEditPrice_CheckedChanged(null, null);
                if (!string.IsNullOrEmpty(txtProfCenter.Text))
                {
                    Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(Session["UserCompanyCode"].ToString(), txtProfCenter.Text);
                    if (!_IsValid)
                    {
                        txtProfCenter.Text = "";
                        txtProfCenter.Focus();
                        DisplayMessage("Please select a valid profit center !!!", 1); return;
                    }
                    BindDisGrid();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }

        protected void txtInvType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPriceBook.Text = "";
                txtPriceLevel.Text = "";
                chkAllowDiscount.Checked = false;
                chkAllowDiscount_CheckedChanged(null, null);
                chkAllowEditPrice.Checked = false;
                chkAllowEditPrice_CheckedChanged(null, null);
                if (string.IsNullOrEmpty(txtProfCenter.Text))
                {
                    DisplayMessage("Please select a valid profit center !!!", 1); return;
                }
                if (string.IsNullOrEmpty(txtInvType.Text))
                    return;
                _priceRef = new PriceDefinitionRef()
                {
                    Sadd_com = Session["UserCompanyCode"].ToString(),
                    Sadd_pc = txtProfCenter.Text
                };
                DataTable _tbl = CHNLSVC.CommonSearch.SearchDocPriceDefDocTp(_priceRef,"Invoice Type", txtInvType.Text.ToUpper());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter a valid invoice type !!!", 1);
                    txtInvType.Text = "";
                    txtInvType.Focus();
                }
            }
            catch (Exception err)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }

        protected void txtPriceBook_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPriceLevel.Text = "";
                chkAllowDiscount.Checked = false;
                chkAllowDiscount_CheckedChanged(null, null);
                chkAllowEditPrice.Checked = false;
                chkAllowEditPrice_CheckedChanged(null, null);
                if (string.IsNullOrEmpty(txtProfCenter.Text))
                {
                    DisplayMessage("Please select a valid profit center !!!", 1); return;
                }
                if (string.IsNullOrEmpty(txtInvType.Text))
                {
                    DisplayMessage("Please select a invoice type !!!", 1); return;
                }
                if (string.IsNullOrEmpty(txtPriceBook.Text))
                    return;
                _priceRef = new PriceDefinitionRef()
                {
                    Sadd_com = Session["UserCompanyCode"].ToString(),
                    Sadd_pc = txtProfCenter.Text.ToUpper(),
                    Sadd_doc_tp = txtInvType.Text.ToUpper()
                };
                DataTable _tbl = CHNLSVC.CommonSearch.SearchDocPriceDefPrBook(_priceRef, "Price Book", txtPriceBook.Text.ToUpper());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter a valid price book !!!", 1);
                    txtPriceBook.Text = "";
                    txtPriceBook.Focus();
                }
            }
            catch (Exception err)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }

        protected void txtPriceLevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProfCenter.Text))
                {
                    DisplayMessage("Please select a valid profit center !!!", 1); return;
                }
                if (string.IsNullOrEmpty(txtInvType.Text))
                {
                    DisplayMessage("Please select a invoice type !!!", 1); return;
                }
                if (string.IsNullOrEmpty(txtPriceBook.Text))
                {
                    DisplayMessage("Please select a price !!!", 1); return;
                }
                if (string.IsNullOrEmpty(txtPriceLevel.Text))
                    return;
                _priceRef = new PriceDefinitionRef()
                {
                    Sadd_com = Session["UserCompanyCode"].ToString(),
                    Sadd_pc = txtProfCenter.Text.ToUpper(),
                    Sadd_doc_tp = txtInvType.Text.ToUpper(),
                    Sadd_pb = txtPriceBook.Text.ToUpper()
                };
                DataTable _tbl = CHNLSVC.CommonSearch.SearchDocPriceDefPrLVL(_priceRef, "Price Level", txtPriceLevel.Text.ToUpper());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please enter a valid price level !!!", 1);
                    txtPriceLevel.Text = "";
                    txtPriceLevel.Focus();
                }
            }
            catch (Exception err)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }

        protected void txtDisRate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtEditRate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chkAllowDiscount_CheckedChanged(object sender, EventArgs e)
        {
            txtDisRate.Enabled = chkAllowDiscount.Checked;
            txtDisRate.Text = "";
        }

        protected void chkAllowEditPrice_CheckedChanged(object sender, EventArgs e)
        {
            txtEditRate.Enabled = chkAllowEditPrice.Checked;
            txtEditRate.Text = "";
        }

        protected void btnDisSelect_Click(object sender, EventArgs e)
        {
            try
            {
                txtInvType.Text = "";
                txtPriceBook.Text = "";
                txtPriceLevel.Text = "";
                chkAllowDiscount.Checked = false;
                chkAllowDiscount_CheckedChanged(null, null);
                txtDisRate.Text =  "";
                chkAllowEditPrice.Checked = false;
                chkAllowEditPrice_CheckedChanged(null, null);
                txtEditRate.Text = "";

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                Label lblProCenter = (Label)row.FindControl("lblProCenter");
                Label lblInvTp = (Label)row.FindControl("lblInvTp");
                Label lblPrBook = (Label)row.FindControl("lblPrBook");
                Label lblPrLevel = (Label)row.FindControl("lblPrLevel");
                CheckBox chkAllowDis = (CheckBox)row.FindControl("chkAllowDis");
                Label lblDisRate = (Label)row.FindControl("lblDisRate");
                CheckBox chkAllowEdPrice = (CheckBox)row.FindControl("chkAllowEdPrice");
                Label lblEditRate = (Label)row.FindControl("lblEditRate");

                txtInvType.Text = lblInvTp.Text;
                txtPriceBook.Text = lblPrBook.Text;
                txtPriceLevel.Text = lblPrLevel.Text;
                chkAllowDiscount.Checked = chkAllowDis.Checked;
                chkAllowDiscount_CheckedChanged(null, null);
                txtDisRate.Text = chkAllowDis.Checked ? lblDisRate .Text: "";
                chkAllowEditPrice.Checked = chkAllowEdPrice.Checked;
                chkAllowEditPrice_CheckedChanged(null, null);
                txtEditRate.Text = chkAllowEdPrice.Checked ? lblEditRate.Text : "";
            }
            catch (Exception err)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }

        protected void lbtnSaveDis_Click(object sender, EventArgs e)
        {
            string err="";
            Int32 _effect = 0;
            try
            {
                if (string.IsNullOrEmpty(txtProfCenter.Text))
                {
                    txtProfCenter.Focus();
                    DisplayMessage("Please select a profit center !!!", 1); return;
                }
                else if (string.IsNullOrEmpty(txtInvType.Text))
                {
                    txtInvType.Focus();
                    DisplayMessage("Please select a invoice type !!!", 1); return;
                }
                else if (string.IsNullOrEmpty(txtInvType.Text))
                {
                    txtInvType.Focus();
                    DisplayMessage("Please select a invoice type !!!", 1); return;
                }
                else if (string.IsNullOrEmpty(txtPriceBook.Text))
                {
                    txtPriceBook.Focus();
                    DisplayMessage("Please select a price book !!!", 1); return;
                }
                else if (string.IsNullOrEmpty(txtPriceLevel.Text))
                {
                    txtPriceLevel.Focus();
                    DisplayMessage("Please select a price level !!!", 1); return;
                }
                else if (string.IsNullOrEmpty(txtDisRate.Text) && chkAllowDiscount.Checked)
                {
                    txtDisRate.Focus();
                    DisplayMessage("Please enter a discount rate !!!", 1); return;
                }
                else if (string.IsNullOrEmpty(txtEditRate.Text) && chkAllowEditPrice.Checked)
                {
                    txtEditRate.Focus();
                    DisplayMessage("Please enter a edit rate !!!", 1); return;
                }
                _priceRef = new PriceDefinitionRef(); 
                _priceRef.Sadd_pc = txtProfCenter.Text.ToUpper();
                _priceRef.Sadd_doc_tp  = txtInvType.Text.ToUpper();                  
                _priceRef.Sadd_p_lvl=txtPriceLevel.Text.ToUpper();                     
                //_priceRef.Sadd_is_bank_ex_rt=             
                _priceRef.Sadd_is_disc =chkAllowDiscount.Checked;
                _priceRef.Sadd_disc_rt = string.IsNullOrEmpty(txtDisRate.Text) ? 0 : Convert.ToDecimal(txtDisRate.Text);                  
                _priceRef.Sadd_com=Session["UserCompanyCode"].ToString();                       
                //_priceRef.Sadd_chk_credit_bal            
                _priceRef.Sadd_cre_by = Session["UserID"].ToString();
                _priceRef.Sadd_cre_when = DateTime.Now;
                _priceRef.Sadd_mod_by = Session["UserID"].ToString();
                _priceRef.Sadd_mod_when = DateTime.Now;
                _priceRef.Sadd_pb = txtPriceBook.Text.ToUpper();                        
                //_priceRef.sadd_prefix                    
                //_priceRef.sadd_def                       
                //_priceRef.sadd_def_stus                  
                //_priceRef.sadd_def_pb                    
                //_priceRef.sadd_is_rep                    
                //_priceRef.sadd_rep_order                 
                _priceRef.Sadd_is_edit = chkAllowEditPrice.Checked ? 1 : 0; ;
                _priceRef.Sadd_edit_rt =string.IsNullOrEmpty(txtEditRate.Text)?0: Convert.ToDecimal(txtEditRate.Text);
                 _effect = CHNLSVC.Sales.UpdatePriceDefinitionRef(_priceRef, out err);
                 if (_effect == 1)
                 {
                     DisplayMessage("Price definition ref updated successfully !!!", 3);
                     ClearDiscount();
                 }
                 else if (_effect == -1)
                 {
                     DisplayMessage("Error Occurred while processing !!!", 4);
                 }
            }
            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing !!!", 4);
            }
        }

        protected void lbtnClearDis_Click(object sender, EventArgs e)
        {
            ClearDiscount();
        }

        
       
    }
}