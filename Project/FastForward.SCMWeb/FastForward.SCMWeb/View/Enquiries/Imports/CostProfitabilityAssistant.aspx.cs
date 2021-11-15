using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
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

namespace FastForward.SCMWeb.View.Enquiries.Imports
{
    public partial class Cost_and_Profitability_Assistant : Base
    {
        private string panal { get { return (string)Session["panal"]; } set { Session["panal"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageClear();
            }
            else
            {
                txtBLFrom.Text = Request[txtBLFrom.UniqueID];
                txtBLTo.Text = Request[txtBLTo.UniqueID];
                txtCostFrom.Text = Request[txtCostFrom.UniqueID];
                txtCostTo.Text = Request[txtCostTo.UniqueID];
                txtGRNFrom.Text = Request[txtGRNFrom.UniqueID];
                txtGRNTo.Text = Request[txtGRNTo.UniqueID];
            }
        }
        private void PageClear()
        {
            textModel.Text = string.Empty;
            txtItem.Text = string.Empty;
            textBLNo.Text = string.Empty;
            txtRecCount.Text = string.Empty;
            txtStatus.Text = string.Empty;
            //txtReccount.Text = string.Empty;
            txtBLFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtBLTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtCostFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtCostTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtGRNFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtGRNTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtFDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtTDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            ImportGrid.DataSource = new int[] { };
            ImportGrid.DataBind();
            LocalGrid.DataSource = new int[] { };
            LocalGrid.DataBind();
            GrdPreOrdDet.DataSource = new int[] { };
            GrdPreOrdDet.DataBind();
            GrdPrePrice.DataSource = new int[] { };
            GrdPrePrice.DataBind();
            grdCostGPView.DataSource = new int[] { };
            grdCostGPView.DataBind();
            Session["itmList"] = null;
            //GrdGPCal.DataSource = new int[] { };
            //GrdGPCal.DataBind();

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)

            if (orderFrom.SelectedValue == "I")
            {
                CostFrom.Enabled = true;
                CostTo.Enabled = true;
                chkCostDate.Enabled = true;
                LocalGridPanel.Attributes.Add("style", "display:none");
                ImportGridPanel.Attributes.Remove("style");
            }
            else if (orderFrom.SelectedValue == "L")
            {
                CostFrom.Enabled = false;
                CostTo.Enabled = false;
                chkCostDate.Enabled = false;
                ImportGridPanel.Attributes.Add("style", "display:none");
                LocalGridPanel.Attributes.Remove("style");
                GrdPreOrdDet.Columns[3].Visible = false;
            }

            //GrdGPCal.Columns[7].Visible = false;
            //GrdGPCal.Columns[9].Visible = false;

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)
        }
        private void DisplayMessage(String Msg, Int32 option)
        {
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
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterItem:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        // paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator);
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + 0 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append(Session["_SerialSearchType"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtGPItem.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtGPCat1.Text.ToUpper() + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtGPCat1.Text.ToUpper() + seperator + txtGPCat2.Text.ToUpper() + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circualr:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtGPPriBk.Text.Trim() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
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
        protected void lbtnModel_Click(object sender, EventArgs e)
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
                ViewState["SEARCH"] = _result;
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtnItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textModel.Text.ToString() == "")
                {
                    Session["Item"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
                    grdResult.DataSource = null;
                    grdResult.DataSource = _result;
                    Session["Item"] = _result;
                    BindUCtrlDDLData(_result);
                    grdResult.DataBind();


                    lblvalue.Text = "masterItem";
                    txtSearchbyword.Text = "";
                    txtSearchbyword.Focus();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    if (grdResult.PageIndex > 0)
                    { grdResult.SetPageIndex(0); }


                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, "Model", "%" + textModel.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "masterItem";
                    txtSearchbyword.Text = string.Empty;
                    txtSearchbyword.Focus();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }



            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtnBLNo_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);

                if (orderFrom.SelectedValue == "I")
                {
                    DataTable result = CHNLSVC.CommonSearch.SearchBLHeaderCosting(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    // DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, null, null);
                    grdResultD.DataSource = result;
                    grdResultD.DataBind();
                    lblvalue.Text = "BLHeader";
                    BindUCtrlDDLData2(result);
                    Session["POPUP_LOADED"] = "1";
                    UserDPopoup.Show();
                }

                if (orderFrom.SelectedValue == "L")
                {
                    DataTable result = CHNLSVC.CommonSearch.SearchPurOrdCosting(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    grdResultD.DataSource = result;
                    grdResultD.DataBind();
                    lblvalue.Text = "POHeader";
                    BindUCtrlDDLData2(result);
                    Session["POPUP_LOADED"] = "1";
                    UserDPopoup.Show();
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

            string ID = grdResult.SelectedRow.Cells[1].Text;

            if (lblvalue.Text == "masterItem")
            {
                string model = grdResult.SelectedRow.Cells[3].Text;
                txtItem.Text = ID;
                textModel.Text = model;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ModelMaster")
            {
                textModel.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "costGPViewItem")
            {
                txtGPItem.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "itmSerialI")
            {
                txtGPSerial.Text = ID;
                txtGPItem.Text = grdResult.SelectedRow.Cells[4].Text;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ItmMainCat")
            {
                txtGPCat1.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ItmCat2")
            {
                txtGPCat2.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ItmCat3")
            {
                txtGPCat3.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ItmBrand")
            {
                txtGPBrand.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "PriceBook")
            {
                txtGPPriBk.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "PriceBook2")
            {
                txtGPPnlPB.Text = ID;
                lblvalue.Text = "";
                mpCostGPView.Show();
                return;
            }
            if (lblvalue.Text == "PriceLevel")
            {
                txtGPPriLvl.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "PriceLevel2")
            {
                txtGPPnlPL.Text = ID;
                lblvalue.Text = "";
                mpCostGPView.Show();
                return;
            }
            if (lblvalue.Text == "Promotion")
            {
                txtGPPromoCd.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Promotion2")
            {
                txtGPPnlPromoCd.Text = ID;
                lblvalue.Text = "";
                mpCostGPView.Show();
                return;
            }
            if (lblvalue.Text == "Circular")
            {
                txtGPCircular.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Circular2")
            {
                txtGPPnlCir.Text = ID;
                lblvalue.Text = "";
                mpCostGPView.Show();
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grdResult.PageIndex = e.NewPageIndex;

            //if (lblvalue.Text == "masterItem")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "ModelMaster")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            //    DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "costGPViewItem")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "itmSerialI")
            //{
            //    if (string.IsNullOrEmpty(txtGPItem.Text))
            //        Session["_SerialSearchType"] = "SER1_WOITEM";
            //    else
            //        Session["_SerialSearchType"] = "SER1_WITEM";

            //    txtSearchbyword.Text = string.Empty;
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
            //    DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, null, null);
            //    int count = result.Rows.Count;
            //    for (int x = count - 1; x >= 500; x--)
            //    {
            //        DataRow dr = result.Rows[x];
            //        dr.Delete();
            //    }
            //    result.AcceptChanges();
            //    grdResult.DataSource = result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "itmSerialI";
            //    BindUCtrlDDLData(result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "ItmMainCat")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    BindUCtrlDDLData(_result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "ItmCat2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    BindUCtrlDDLData(_result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "ItmCat3")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    BindUCtrlDDLData(_result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "ItmBrand")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    BindUCtrlDDLData(_result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "PriceBook")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            //    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "PriceBook";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "PriceBook2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            //    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "PriceBook2";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "PriceLevel")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            //    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "PriceLevel";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "PriceLevel2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            //    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "PriceLevel2";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "Circular")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "Circular";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "Circular2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "Circular2";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "Promotion")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
            //    DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "Promotion";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "Promotion2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
            //    DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "Promotion2";
            //    UserPopoup.Show();
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
            try
            {
                #region Filter
                if ((lblvalue.Text == "Pc_HIRC_Company"))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }

                if (lblvalue.Text == "masterItem")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = null;
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
                if (lblvalue.Text == "ItemGRN")
                {
                    //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                    DataTable _result = CHNLSVC.CommonSearch.SearchItemGRN(ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, "GRN", textBLNo.Text, txtItem.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "costGPViewItem")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
                if (lblvalue.Text == "itmSerialI")
                {
                    if (string.IsNullOrEmpty(txtGPItem.Text))
                        Session["_SerialSearchType"] = "SER1_WOITEM";
                    else
                        Session["_SerialSearchType"] = "SER1_WITEM";

                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                    DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    int count = result.Rows.Count;
                    for (int x = count - 1; x >= 500; x--)
                    {
                        DataRow dr = result.Rows[x];
                        dr.Delete();
                    }
                    result.AcceptChanges();
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "itmSerialI";
                    BindUCtrlDDLData(result);
                    UserPopoup.Show();
                    ViewState["SEARCH"] = result;
                }
                if (lblvalue.Text == "ItmMainCat")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "ItmCat2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "ItmCat3")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "ItmBrand")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "PriceBook")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "PriceBook";
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "PriceBook2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "PriceBook2";
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "PriceLevel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "PriceLevel";
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "PriceLevel2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "PriceLevel2";
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "Circular")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
                    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "Circular";
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "Circular2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
                    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "Circular2";
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "Promotion")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
                    DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "Promotion";
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                if (lblvalue.Text == "Promotion2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
                    DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "Promotion2";
                    UserPopoup.Show();
                    ViewState["SEARCH"] = _result;
                }
                #endregion
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            FilterData();

        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "BLHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable result = CHNLSVC.CommonSearch.SearchBLHeaderCosting(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "BLHeader";
                Session["BLHeader"] = "true";
                UserDPopoup.Show();
            }

            if (lblvalue.Text == "POHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable result = CHNLSVC.CommonSearch.SearchPurOrdCosting(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "POHeader";
                Session["POHeader"] = "true";
                UserDPopoup.Show();
            }

        }
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);

            if (lblvalue.Text == "BLHeader")
            {
                string Name = grdResultD.SelectedRow.Cells[2].Text;
                textBLNo.Text = Name;
                lblvalue.Text = "";

                UserDPopoup.Hide();

                return;
            }

            if (lblvalue.Text == "POHeader")
            {
                string Name = grdResultD.SelectedRow.Cells[1].Text;
                textBLNo.Text = Name;
                lblvalue.Text = "";
                //Session["Doc"] = "";
                //Session["DocType"] = "Doc";
                //GetDocData(Name);
                //LoadHeader(false);
                //Session["documntNo"] = Name;

                //btnSave.Visible = false;

                UserDPopoup.Hide();

                return;
            }

        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "BLHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable _result = CHNLSVC.CommonSearch.SearchBLHeaderCosting(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "BLHeader";
                UserDPopoup.Show();
            }

            if (lblvalue.Text == "POHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable _result = CHNLSVC.CommonSearch.SearchPurOrdCosting(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "POHeader";
                UserDPopoup.Show();
            }

        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "BLHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable result = CHNLSVC.CommonSearch.SearchBLHeaderCosting(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "BLHeader";
                Session["BLHeader"] = "true";
                UserDPopoup.Show();
            }

            if (lblvalue.Text == "POHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable result = CHNLSVC.CommonSearch.SearchPurOrdCosting(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "POHeader";
                Session["POHeader"] = "true";
                UserDPopoup.Show();
            }

        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            FilterData();

            //if ((lblvalue.Text == "Pc_HIRC_Company"))
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            //    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "masterItem")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    UserPopoup.Show();
            //    ViewState["SEARCH"] = _result;
            //    return;
            //}
            //if (lblvalue.Text == "ModelMaster")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            //    DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    UserPopoup.Show();
            //    return;
            //}

            //if (lblvalue.Text == "ItemGRN")
            //{
            //    //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            //    DataTable _result = CHNLSVC.CommonSearch.SearchItemGRN(ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, "GRN", textBLNo.Text, txtItem.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "costGPViewItem")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "itmSerialI")
            //{
            //    if (string.IsNullOrEmpty(txtGPItem.Text))
            //        Session["_SerialSearchType"] = "SER1_WOITEM";
            //    else
            //        Session["_SerialSearchType"] = "SER1_WITEM";

            //    txtSearchbyword.Text = string.Empty;
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
            //    DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    int count = result.Rows.Count;
            //    for (int x = count - 1; x >= 500; x--)
            //    {
            //        DataRow dr = result.Rows[x];
            //        dr.Delete();
            //    }
            //    result.AcceptChanges();
            //    grdResult.DataSource = result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "itmSerialI";
            //    BindUCtrlDDLData(result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "ItmMainCat")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    BindUCtrlDDLData(_result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "ItmCat2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    BindUCtrlDDLData(_result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "ItmCat3")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    BindUCtrlDDLData(_result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "ItmBrand")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            //    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    BindUCtrlDDLData(_result);
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "PriceBook")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            //    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "PriceBook";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "PriceBook2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            //    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "PriceBook2";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "PriceLevel")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            //    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "PriceLevel";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "PriceLevel2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            //    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "PriceLevel2";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "Circular")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "Circular";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "Circular2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
            //    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "Circular2";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "Promotion")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
            //    DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "Promotion";
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "Promotion2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
            //    DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "Promotion2";
            //    UserPopoup.Show();
            //}
        }
        protected void orderFrom_DataBound(object sender, EventArgs e)
        {
            if (orderFrom.SelectedValue == "I")
            {

                CostFrom.Enabled = true;
                CostTo.Enabled = true;
                chkCostDate.Enabled = true;
                LocalGridPanel.Attributes.Add("style", "display:none");
                ImportGridPanel.Attributes.Remove("style");
                //txtCostFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                //txtCostTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            }
            else
            {

                CostFrom.Enabled = false;
                CostTo.Enabled = false;
                chkCostDate.Enabled = false;
                ImportGridPanel.Attributes.Add("style", "display:none");
                LocalGridPanel.Attributes.Remove("style");
                //txtCostFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                //txtCostTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
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
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "BLHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                if (txtSearchbywordD.Text != "")
                {
                    DataTable _result = CHNLSVC.CommonSearch.SearchBLHeaderCosting(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(DateTime.Now.AddYears(-3)), Convert.ToDateTime(DateTime.Now));
                    grdResultD.DataSource = _result;
                    grdResultD.DataBind();
                    lblvalue.Text = "BLHeader";
                    Session["BLHeader"] = "true";
                    UserDPopoup.Show();
                }
                else
                {
                    DataTable _result = CHNLSVC.CommonSearch.SearchBLHeaderCosting(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    grdResultD.DataSource = _result;
                    grdResultD.DataBind();
                    lblvalue.Text = "BLHeader";
                    Session["BLHeader"] = "true";
                    UserDPopoup.Show();
                }

            }

            if (lblvalue.Text == "POHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable _result = CHNLSVC.CommonSearch.SearchPurOrdCosting(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataBind();
                lblvalue.Text = "POHeader";
                Session["POHeader"] = "true";
                UserDPopoup.Show();
            }
        }
        protected void lbtnSearchall_Click(object sender, EventArgs e)
        {
            ImportGrid.DataSource = new int[] { };
            ImportGrid.DataBind();
            LocalGrid.DataSource = new int[] { };
            LocalGrid.DataBind();
            GrdPreOrdDet.DataSource = new int[] { };
            GrdPreOrdDet.DataBind();
            GrdPrePrice.DataSource = new int[] { };
            GrdPrePrice.DataBind();
            //GrdGPCal.DataSource = new int[] { };
            //GrdGPCal.DataBind();
            //GrdGPCal.Columns[7].Visible = false;
            //GrdGPCal.Columns[9].Visible = false;

            //date validation
            DateTime fromdate1 = Convert.ToDateTime(txtBLFrom.Text);
            DateTime todate1 = Convert.ToDateTime(txtBLTo.Text);
            if (chkBLDate.Checked)
            {
                if (fromdate1 > todate1)
                {
                    DisplayMessage("Not a Valid Date Range", 2);
                    return;
                }
            }


            DateTime fromdate2 = Convert.ToDateTime(txtCostFrom.Text);
            DateTime todate2 = Convert.ToDateTime(txtCostTo.Text);
            if (chkCostDate.Checked)
            {
                if (fromdate2 > todate2)
                {
                    DisplayMessage("Not a Valid Date Range", 2);
                    return;
                }
            }


            DateTime fromdate3 = Convert.ToDateTime(txtGRNFrom.Text);
            DateTime todate3 = Convert.ToDateTime(txtGRNTo.Text);
            if (chkGRNDate.Checked)
            {
                if (fromdate3 > todate3)
                {
                    DisplayMessage("Not a Valid Date Range", 2);
                    return;
                }
            }

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes) 
            DataTable validTbl = new DataTable();
            string type = "";
            if (!string.IsNullOrEmpty(textBLNo.Text))
            {
                validTbl = CHNLSVC.General.checkOrdType(textBLNo.Text);
                if (validTbl.Rows.Count > 0)
                    type = validTbl.Rows[0].Field<string>("poh_tp").ToString();
            }

            if (orderFrom.SelectedValue == "L")
            {
                if (type == "I")
                {
                    DisplayMessage("Not a Local Order", 2);
                }
                else if (type == "X")
                {
                    DisplayMessage("Not an Order No", 4);
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    LocalGrid_DataBound(SearchParams);

                    lblvalue.Text = "POHeader";
                }
            }

            if (orderFrom.SelectedValue == "I")
            {
                if (type == "L")
                {
                    DisplayMessage("Not a Import Order", 2);
                }
                else if (type == "X")
                {
                    DisplayMessage("Not an Order No", 4);
                }
                else
                {

                    ImportGrid_DataBound();

                    lblvalue.Text = "BLHeader";
                }
            }
        }
        protected void itemLbl_DataBound(string SearchParams, string item)
        {

            DataTable itmtbl = CHNLSVC.CommonSearch.GetGRNItemSearchData(SearchParams, "ITEM", item);
            if (itmtbl.Rows.Count > 0)
            {
                lbItem.Text = itmtbl.Rows[0].IsNull("Item") ? "NAN" : itmtbl.Rows[0].Field<string>("Item").ToString();
                lbDes.Text = itmtbl.Rows[0].IsNull("Description") ? "NAN" : itmtbl.Rows[0].Field<string>("Description").ToString();
                lbBrand.Text = itmtbl.Rows[0].IsNull("Brand") ? "NAN" : itmtbl.Rows[0].Field<string>("Brand").ToString();
            }
            DataTable vatTbl = CHNLSVC.General.GetVATRate(Session["UserCompanyCode"].ToString(), item);
            if (vatTbl.Rows.Count > 0)
            {
                lbVAT.Text = vatTbl.Rows[0].IsNull("VAT") ? "Not Assigned" : vatTbl.Rows[0].Field<decimal>("VAT").ToString();
            }
        }
        protected void LocalGrid_DataBound(string SearchParams)
        {
            DataTable ordTbl = new DataTable();
            //check model change
            if (txtItem.Text != "")
            {
                DataTable _result = CHNLSVC.CommonSearch.SearchItemGRNnew(null, null, "GRN", textBLNo.Text, txtItem.Text);
                if (_result != null)
                {
                    if (_result.Rows.Count > 0)
                    {
                        txtItem.Text = _result.Rows[0]["ITEM"].ToString();
                    }

                }

            }



            if (lblDtvalue.Text == "PODate")
            {
                ordTbl = CHNLSVC.General.SearchPurOrdCostingDet(SearchParams, textBLNo.Text, txtItem.Text, textModel.Text, "ord", Convert.ToDateTime(txtBLFrom.Text), Convert.ToDateTime(txtBLTo.Text));

            }
            else if (lblDtvalue.Text == "GRNDate")
            {
                ordTbl = CHNLSVC.General.SearchPurOrdCostingDet(SearchParams, textBLNo.Text, txtItem.Text, textModel.Text, "GRN", Convert.ToDateTime(txtGRNFrom.Text), Convert.ToDateTime(txtGRNTo.Text));

            }
            else
            {
                ordTbl = CHNLSVC.General.SearchPurOrdCostingDet(SearchParams, textBLNo.Text, txtItem.Text, textModel.Text, "", null, null);
            }

            //DataRow lastRow = ordTbl.Rows[ordTbl.Rows.Count - 1];
            LocalGrid.DataSource = ordTbl;
            LocalGrid.DataBind();


        }
        protected void ImportGrid_DataBound()
        {
            if (textBLNo.Text == "" && txtItem.Text == "")
            {
                DisplayMessage("Please Enter Item Code Or SI No!!", 2);
            }
            else
            {
                DataTable ordTbl = new DataTable();
                //check model change
                if (txtItem.Text != "" && textBLNo.Text != "")
                {
                    DataTable _result = CHNLSVC.CommonSearch.SearchItemGRNnew(null, null, "GRN", textBLNo.Text, txtItem.Text);
                    txtItem.Text = _result.Rows[0]["ITEM"].ToString();
                }

                if (lblDtvalue.Text == "CostingDate")
                {
                    ordTbl = CHNLSVC.General.SerachBLOrdDet(textBLNo.Text, txtItem.Text, "cost", Convert.ToDateTime(txtCostFrom.Text), Convert.ToDateTime(txtCostTo.Text), Session["UserCompanyCode"].ToString());

                }
                else if (lblDtvalue.Text == "PODate")
                {
                    ordTbl = CHNLSVC.General.SerachBLOrdDet(textBLNo.Text, txtItem.Text, "ord", Convert.ToDateTime(txtBLFrom.Text), Convert.ToDateTime(txtBLTo.Text), Session["UserCompanyCode"].ToString());
                }
                else if (lblDtvalue.Text == "GRNDate")
                {
                    ordTbl = CHNLSVC.General.SerachBLOrdDet(textBLNo.Text, txtItem.Text, "GRN", Convert.ToDateTime(txtGRNFrom.Text), Convert.ToDateTime(txtGRNTo.Text), Session["UserCompanyCode"].ToString());
                }
                else
                {
                    ordTbl = CHNLSVC.General.SerachBLOrdDet(textBLNo.Text, txtItem.Text, "", null, null, Session["UserCompanyCode"].ToString());
                    //DisplayMessage("Please Select Date Selection option!!", 2);
                    //return;
                    //ordTbl.Select("", "Costing_Dt DESC");
                }
                int i = 0;
                foreach (DataRow ordexdt in ordTbl.Rows)
                {
                    decimal costingCusdecRt = 1;
                    if (ordexdt["CusdecRate"].ToString() != "")
                    {
                        costingCusdecRt = Convert.ToDecimal(ordexdt["CusdecRate"].ToString());
                    }
                    if (ordTbl.Rows[i]["Forcast"].ToString() == "") ordTbl.Rows[i]["Forcast"] = "0";

                    decimal EXRATE = 0;
                    if (ordTbl.Rows[i]["FOB"].ToString() == "") ordTbl.Rows[i]["FOB"] = "0";
                    if (Convert.ToDecimal(ordTbl.Rows[i]["FOB"].ToString()) == 0) ordTbl.Rows[i]["FOB"] = "1";

                    if (ordTbl.Rows[i]["Buying"].ToString() == "") ordTbl.Rows[i]["Buying"] = "0";
                    if (Convert.ToDecimal(ordTbl.Rows[i]["Buying"].ToString()) == 0) ordTbl.Rows[i]["Buying"] = "1";

                    if (ordTbl.Rows[i]["Costing"].ToString() == "") ordTbl.Rows[i]["Costing"] = "0";
                    EXRATE = Convert.ToDecimal(ordTbl.Rows[i]["Costing"].ToString());

                    if (ordTbl.Rows[i]["ice_ele_rt"].ToString() == "") ordTbl.Rows[i]["ice_ele_rt"] = "0";
                    ordTbl.Rows[i]["Costing"] = (Convert.ToDecimal(ordTbl.Rows[i]["ice_ele_rt"].ToString()) / Convert.ToDecimal(ordTbl.Rows[i]["FOB"].ToString()));
                    //ordTbl.Rows[i]["DF_D"] = (Convert.ToDecimal(ordTbl.Rows[i]["DF_D"].ToString()) / Convert.ToDecimal(ordTbl.Rows[i]["Buying"].ToString()));
                    //ordTbl.Rows[i]["DF_PAL_D"] = (Convert.ToDecimal(ordTbl.Rows[i]["DF_PAL_Rs"].ToString()) / Convert.ToDecimal(ordTbl.Rows[i]["Costing"].ToString())) + (Convert.ToDecimal(ordTbl.Rows[i]["DF"].ToString()) / Convert.ToDecimal(ordTbl.Rows[i]["Costing"].ToString()));
                    //ordTbl.Rows[i]["DP"] = Convert.ToDecimal(ordTbl.Rows[i]["DF"].ToString()) - Convert.ToDecimal(ordTbl.Rows[i]["DP"].ToString());
                    // ordTbl.Rows[i]["DF_PAL_Rs"] = Convert.ToDecimal(ordTbl.Rows[i]["DF_PAL_Rs"].ToString()) + Convert.ToDecimal(ordTbl.Rows[i]["DF"].ToString());

                    //DF/DP

                    List<ImportsCostElementItem> oImportsCostElementItems = CHNLSVC.Financial.GET_IMP_CST_ELE_ITM_BY_ITM(Convert.ToInt32(ordTbl.Rows[i]["ibi_seq_no"].ToString()), ordTbl.Rows[i]["ibi_doc_no"].ToString(), ordTbl.Rows[i]["Item"].ToString());
                    Session["oImportsCostElementItems"] = oImportsCostElementItems;

                    // 2016-01-20 Set Bond values

                    List<ImportsCostElementItem> oFilterd = oImportsCostElementItems.FindAll(x => x.Ice_stus == 1 && x.Ice_ref_line == Convert.ToInt32(ordTbl.Rows[i]["ibi_ref_line"].ToString()));
                    //oFilterd.Where(x => x.Ice_ele_cat == "CUSTM").

                    DataTable dtTemp = GlobalMethod.ToDataTable(oFilterd);
                    foreach (ImportsCostElementItem oitm in oFilterd)
                    {
                        oitm.Ice_pre_rt = oitm.Ice_pre_rt - oitm.Ice_pre_amt_claim;
                        oitm.Ice_actl_rt = oitm.Ice_actl_rt - oitm.Ice_actl_amt_claim;
                        oitm.Ice_finl_rt = oitm.Ice_finl_rt - oitm.Ice_finl_amt_claim;
                    }

                    foreach (ImportsCostElementItem item in oFilterd)
                    {
                        //Calculate tobond values
                        if (item.Ice_ele_tp != "DUTY")
                        {
                            item.Ice_pre_rt = item.Ice_actl_rt;
                            item.Ice_actl_rt = item.Ice_actl_rt;
                            item.Ice_finl_rt = item.Ice_actl_rt;

                        }
                        else if (item.Ice_ele_tp == "DUTY" && item.Ice_ele_cd == "PAL")
                        {

                            item.Ice_actl_rt = item.Ice_actl_rt;
                            item.Ice_pre_rt = 0;

                        }
                        else
                        {
                            item.Ice_pre_rt = 0;
                            item.Ice_actl_rt = 0;
                        }


                    }

                    string dutyfree = oImportsCostElementItems.Where(x => x.Ice_stus == 1 && x.Ice_ref_line == Convert.ToInt32(ordTbl.Rows[i]["ibi_ref_line"].ToString())).Sum(z => z.Ice_pre_rt).ToString("N2");
                    string dutyfreepal = oImportsCostElementItems.Where(x => x.Ice_stus == 1 && x.Ice_ref_line == Convert.ToInt32(ordTbl.Rows[i]["ibi_ref_line"].ToString())).Sum(z => z.Ice_actl_rt).ToString("N2");
                    string dutypaid = oImportsCostElementItems.Where(x => x.Ice_stus == 1 && x.Ice_ref_line == Convert.ToInt32(ordTbl.Rows[i]["ibi_ref_line"].ToString())).Sum(z => z.Ice_finl_rt).ToString("N2");
                   
                    ordTbl.Rows[i]["DP"] = dutypaid;
                    ordTbl.Rows[i]["DF_PAL_Rs"] = dutyfreepal;
                    decimal forecast = 0;
                    decimal exchngeRt = Convert.ToDecimal(ordTbl.Rows[i]["Costing"].ToString());
                    decimal buyingRt = Convert.ToDecimal(ordTbl.Rows[i]["Buying"].ToString());
                    decimal dfD = Convert.ToDecimal(dutyfree) / buyingRt;
                    ordTbl.Rows[i]["DFUSD"] = dfD.ToString();
                    if (exchngeRt != 0)
                    {
                         forecast = Convert.ToDecimal(ordTbl.Rows[i]["Forcast"].ToString());
                         ordTbl.Rows[i]["ForeCastedCost"] = (Convert.ToDecimal(dutypaid) / costingCusdecRt) * forecast;
                    }
                    ordTbl.Rows[i]["DF"] = dutyfree;
                    
                    ordTbl.Rows[i]["DF_PAL_D"] = Convert.ToDecimal(dutyfreepal) / Convert.ToDecimal(ordTbl.Rows[i]["Buying"].ToString());
                    

                    i++;
                    ordTbl.AcceptChanges();
                }

                foreach (DataRow row in ordTbl.Rows)
                {
                    if (row["PayMode"].ToString() == "C")
                    {
                        row["PayMode"] = "CHG";
                    }
                    else
                    {
                        row["PayMode"] = "FOC";
                    }

                    if (row["DF_PAL_Rs"] == DBNull.Value)
                    {
                        row["DF_PAL_Rs"] = row["DF"];
                        row["DF_PAL_D"] = row["DF_D"];
                    }
                    if (row["BANK BILL PAID COST"] == DBNull.Value)
                    {
                        row["BANK BILL PAID COST"] = "0";
                    }
                }


                if (ordTbl.Rows.Count > 0)
                {
                    ordTbl = ordTbl.AsEnumerable()
            .GroupBy(r => new { Col1 = r["BL_NO"], Col2 = r["ENTRY_NO"], Col3 = r["ITEM"], Col4 = r["MODEL"], Col5 = r["PayMode"] }).Select(g => g.First())
            .CopyToDataTable();



                    //var sortedDataTable =
                    //  (from row in ordTbl.AsEnumerable()
                    //   let correctDate = Convert.ToDateTime(row.Field<string>("Costing_Dt"))
                    //   orderby correctDate
                    //   select row).CopyToDataTable();

                    EnumerableRowCollection<DataRow> query = from row in ordTbl.AsEnumerable()
                                                             orderby DateTime.Parse(row.Field<string>("Costing_Dt")) descending
                                                             select row;
                    DataTable dataTable = query.AsDataView().ToTable();

                    ImportGrid.DataSource = dataTable;
                    //ImportGrid.DataSource = ordTbl;
                    ImportGrid.DataBind();

                }
                else
                {
                    DisplayMessage("No data found", 2);
                }
            }


        }
        protected void POrdGrid_DataBound(string item, string ordFrom, string ordNo)
        {
            DataTable LOrdTbl = CHNLSVC.General.SearchLatestOrders(item, ordFrom);
            DataTable dtNew = LOrdTbl.Clone();

            //for (int i = 0; i < LOrdTbl.Rows.Count; i++)
            //{
            //    DataRow dr = LOrdTbl.Rows[i];
            //    if (dr["ITH_OTH_DOCNO"].ToString() != ordNo)
            //        dr.Delete();
            //}
            int i = 0;
            foreach (DataRow row in LOrdTbl.Rows)
            {
                if (row["ITH_OTH_DOCNO"].ToString() != ordNo && i < 5)
                {
                    dtNew.ImportRow(row);
                    i++;
                }

            }
            //for(int )
            //if (LOrdTbl.Rows.Count > 5)
            //{
            //    DataRow lastRow = LOrdTbl.Rows[LOrdTbl.Rows.Count - 1];
            //    lastRow.Delete();
            //}
            GrdPreOrdDet.DataSource = dtNew;
            GrdPreOrdDet.DataBind();
        }
        protected void PBookGrid_DataBound(string SearchParams, string item)
        {
            DataTable PBTbl = CHNLSVC.General.SearchLatestPriceBooks(SearchParams, item, "A");
            GrdPrePrice.DataSource = PBTbl;
            GrdPrePrice.DataBind();
        }
        //protected void GPCalGrid_DataBound(string item, string fromDt, string toDt,decimal cost)
        //{
        //    DataTable GPTbl = CHNLSVC.General.SearchPriceForGPCal(item, "A", Convert.ToDateTime(fromDt), Convert.ToDateTime(toDt));

        //    DataTable GPTblNew = GPTbl.Clone();
        //    GPTblNew.Columns.Add(new DataColumn("Markup_Per", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("GP_Per", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("Expct_MU_Per", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("Expct_GP_Per", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("New_MU_Price", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("New_GP_Price", typeof(decimal)));

        //    int count = 0;
        //    foreach (DataRow row in GPTbl.Rows)
        //    {
        //        if (row["sapd_price_type"].ToString() != "0")
        //        {
        //            GPTblNew.ImportRow(row);
        //        }
        //        else
        //        {
        //            foreach (DataRow rowNew in GPTblNew.Rows)
        //            {
        //                if (row["sapd_pbk_lvl_cd"].ToString() == rowNew["sapd_pbk_lvl_cd"].ToString() && rowNew["sapd_price_type"].ToString()=="0")
        //                {
        //                    count++;
        //                }
        //            }
        //            if (count < 1)
        //            {
        //                GPTblNew.ImportRow(row);
        //            }
        //            count = 0;
        //        }

        //    }
        //    foreach (DataRow rowNew in GPTblNew.Rows)
        //    {
        //        decimal markupPer = ((((decimal)rowNew["sapd_itm_price"]) - cost) / cost) * 100;
        //        decimal GPPer = 0;
        //        if (rowNew["sapd_itm_price"].ToString() != "0")
        //        {
        //            GPPer = ((((decimal)rowNew["sapd_itm_price"]) - cost) / ((decimal)rowNew["sapd_itm_price"])) * 100;
        //        }
        //        else
        //        {
        //            GPPer = -100;
        //        }
        //        rowNew["Markup_Per"] = markupPer;
        //        rowNew["GP_Per"] = GPPer;
        //    }
        //    GrdGPCal.DataSource = GPTblNew;
        //    GrdGPCal.DataBind();
        //}

        //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)
        protected void btnCalAll_Click(object sender, System.EventArgs e)
        {
            //string cost = "0";
            //if (orderFrom.SelectedValue == "I")
            //{
            //    cost = (ImportGrid.SelectedRow.FindControl("ImpGridDP") as Label).Text;
            //}
            //else
            //{
            //    cost = (LocalGrid.SelectedRow.FindControl("LclGridACost") as Label).Text;
            //}


            //decimal costValue = Convert.ToDecimal(cost);

            //foreach (GridViewRow row in GrdGPCal.Rows)
            //{
            //    string price = (((row.FindControl("GrdGPCalPrice")) as Label).Text);

            //    decimal priceValue = Convert.ToDecimal(price);

            //    if (!string.IsNullOrEmpty(AllExpctMUPer.Text))
            //    {
            //        decimal expectMU = Convert.ToDecimal(AllExpctMUPer.Text);
            //        decimal newMUPrice = (expectMU * costValue / 100) + costValue;

            //        row.Cells[7].Text = expectMU.ToString("F");
            //        row.Cells[10].Text = newMUPrice.ToString("N2");

            //        GrdGPCal.Columns[7].Visible = true;
            //        GrdGPCal.Columns[6].Visible = false;
            //    }

            //    if (!string.IsNullOrEmpty(AllExpctGPPer.Text))
            //    {
            //        decimal expectGP = Convert.ToDecimal(AllExpctGPPer.Text);
            //        decimal newGPPrice = (costValue) / (1 - (expectGP / 100));

            //        row.Cells[9].Text = expectGP.ToString("F");
            //        row.Cells[11].Text = newGPPrice.ToString("N2");

            //        GrdGPCal.Columns[9].Visible = true;
            //        GrdGPCal.Columns[8].Visible = false;
            //    }
            //    if (string.IsNullOrEmpty(AllExpctMUPer.Text))
            //    {
            //        TextBox newtextBox = (row.Cells[2].FindControl("GrdGPCalExpctMUPer") as TextBox);

            //        if (!string.IsNullOrEmpty(newtextBox.Text))
            //        {
            //            GrdGPCalExpctPer_TextChanged(sender, e);
            //        }
            //        else
            //        {
            //        }
            //    }
            //    if (string.IsNullOrEmpty(AllExpctGPPer.Text))
            //    {
            //        TextBox newtextBox = (row.Cells[2].FindControl("GrdGPCalExpctGPPer") as TextBox);

            //        if (!string.IsNullOrEmpty(newtextBox.Text))
            //        {
            //            GrdGPCalExpctPer_TextChanged(sender, e);
            //        }
            //        else
            //        {
            //        }
            //    }
            //}

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)
        }
        protected void GrdGPCalExpctPer_TextChanged(object sender, EventArgs e)
        {
            //    string cost="0";
            //    if (orderFrom.SelectedValue == "L")
            //    {
            //        cost = (LocalGrid.SelectedRow.FindControl("LclGridACost") as Label).Text;
            //    }

            //    if (orderFrom.SelectedValue == "I")
            //    {
            //        cost = (ImportGrid.SelectedRow.FindControl("ImpGridDP") as Label).Text;
            //    }

            //    decimal costValue = Convert.ToDecimal(cost);

            //    foreach (GridViewRow row in GrdGPCal.Rows)
            //    {
            //        if (row.RowType == DataControlRowType.DataRow)
            //        {
            //            TextBox expctMuText = (row.Cells[2].FindControl("GrdGPCalExpctMUPer") as TextBox);
            //            TextBox expctGPText = (row.Cells[2].FindControl("GrdGPCalExpctGPPer") as TextBox);

            //            if (!string.IsNullOrEmpty(expctMuText.Text) && !string.IsNullOrEmpty(expctGPText.Text))
            //            {
            //                decimal expectMU = Convert.ToDecimal(expctMuText.Text);
            //                decimal newMUPrice = (expectMU * costValue / 100) + costValue;

            //                decimal expectGP = Convert.ToDecimal(expctGPText.Text);
            //                decimal newGPPrice = (costValue) / (1 - (expectGP / 100));

            //                row.Cells[10].Text = newMUPrice.ToString("N2");
            //                row.Cells[7].Text = expectMU.ToString("F");

            //                row.Cells[11].Text = newGPPrice.ToString("N2");
            //                row.Cells[9].Text = expectGP.ToString("F");

            //                if (row.RowIndex == (GrdGPCal.Rows.Count - 1))
            //                {
            //                    GrdGPCal.Columns[6].Visible = false;
            //                    GrdGPCal.Columns[7].Visible = true;

            //                    GrdGPCal.Columns[8].Visible = false;
            //                    GrdGPCal.Columns[9].Visible = true;
            //                }
            //            }

            //            else
            //            {
            //                if (!string.IsNullOrEmpty(expctMuText.Text))
            //                {
            //                    decimal expectMU = Convert.ToDecimal(expctMuText.Text);
            //                    decimal newMUPrice = (expectMU * costValue / 100) + costValue;

            //                    row.Cells[10].Text = newMUPrice.ToString("N2");
            //                    row.Cells[7].Text = expectMU.ToString("F");

            //                    if (row.RowIndex == (GrdGPCal.Rows.Count - 1))
            //                    {
            //                        GrdGPCal.Columns[6].Visible = false;
            //                        GrdGPCal.Columns[7].Visible = true;
            //                    }
            //                }

            //                if (!string.IsNullOrEmpty(expctGPText.Text))
            //                {
            //                    decimal expectGP = Convert.ToDecimal(expctGPText.Text);
            //                    decimal newGPPrice = (costValue) / (1 - (expectGP / 100));

            //                    row.Cells[11].Text = newGPPrice.ToString("N2");
            //                    row.Cells[9].Text = expectGP.ToString("F");

            //                    if (row.RowIndex == (GrdGPCal.Rows.Count - 1))
            //                    {
            //                        GrdGPCal.Columns[8].Visible = false;
            //                        GrdGPCal.Columns[9].Visible = true;
            //                    }
            //                }
            //            }

            //        }
            //    }
            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)
        }
        protected void LocalGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    //GrdGPCal.Columns[7].Visible = false;
            //    //GrdGPCal.Columns[6].Visible = true;
            //    //AllExpctMUPer.Text = string.Empty;
            //    //AllExpctGPPer.Text = string.Empty;

            //    //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)

            //    for (int i = 0; i < LocalGrid.Rows.Count; i++)
            //    {
            //        LocalGrid.Rows[i].BackColor = Color.FromName("0");
            //    }
            //    GridViewRow row = LocalGrid.SelectedRow;
            //    row.BackColor = Color.FromName("PALETURQUOISE");

            //    string item = (LocalGrid.SelectedRow.FindControl("LclGridItem") as Label).Text;
            //    string cost = (LocalGrid.SelectedRow.FindControl("LclGridACost") as Label).Text;
            //    string PONo = (LocalGrid.SelectedRow.FindControl("LclGridPONo") as Label).Text;

            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);

            //    itemLbl_DataBound(SearchParams, item);

            //    POrdGrid_DataBound(item, "L", PONo);

            //    PBookGrid_DataBound(SearchParams,item);

            //    string today = DateTime.Today.ToString("dd/MM/yyyy");

            //    GPCalGrid_DataBound(item, today, today, Convert.ToDecimal(cost));

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)
        }
        protected void LocalGrid_OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(LocalGrid, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void ImportGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GrdGPCal.Columns[7].Visible = false;
            //GrdGPCal.Columns[6].Visible = true;
            //AllExpctMUPer.Text = string.Empty;
            //AllExpctGPPer.Text = string.Empty;

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)

            for (int i = 0; i < ImportGrid.Rows.Count; i++)
            {
                ImportGrid.Rows[i].BackColor = Color.FromName("0");
            }
            GridViewRow row = ImportGrid.SelectedRow;
            row.BackColor = Color.FromName("PALETURQUOISE");

            string item = (ImportGrid.SelectedRow.FindControl("ImpGridItem") as Label).Text;
            string cost = (ImportGrid.SelectedRow.FindControl("ImpGridDP") as Label).Text;
            string BLNo = (ImportGrid.SelectedRow.FindControl("ImpGridBLNo") as Label).Text;

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);

            ImportsBLHeader oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), BLNo.Trim(), "ALL");

           if (oHeader != null)
           {
            
               List<ImportsBLItems> oImportsBLItems = CHNLSVC.Financial.GET_BL_ITMS_BY_SEQ(oHeader.Ib_seq_no);


               string _piNo = "";
               List<OrderPlanItem> _OrderPlaneItem = new List<OrderPlanItem>();
               foreach (ImportsBLItems _oImportsBLItems in oImportsBLItems)
               {
                   if (_piNo != _oImportsBLItems.Ibi_pi_no)
                   {
                       _piNo = _oImportsBLItems.Ibi_pi_no;
                       _OrderPlaneItem = CHNLSVC.Financial.GET_IMP_OPBY_PI(_oImportsBLItems.Ibi_pi_no);
                   }

                   if (_OrderPlaneItem != null)
                   {
                       var _filter = _OrderPlaneItem.Where(x => x.IOI_ITM_CD == item).ToList();
                       if (_filter != null)
                       { 
                           if(_filter.Count>0)
                           {
                               LabelProj.Text = _filter[0].IOI_ProjectName;
                           }
                       }
                   }
               }
           }
            itemLbl_DataBound(SearchParams, item);

            POrdGrid_DataBound(item, "I", BLNo);

            PBookGrid_DataBound(SearchParams, item);

            string today = DateTime.Today.ToString("dd/MM/yyyy");

            //GPCalGrid_DataBound(item, today, today, Convert.ToDecimal(cost));

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)
        }
        protected void ImportGrid_OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    GridView HeaderGrid = (GridView)sender;
                    GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell Cell_Header = new TableCell();

                    Cell_Header.Text = " ";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 5;
                    Cell_Header.CssClass = "column_right";
                    HeaderRow.Cells.Add(Cell_Header);

                    Cell_Header = new TableCell();
                    Cell_Header.Text = "Exchange Rate";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 3;
                    Cell_Header.CssClass = "column_right";
                    HeaderRow.Cells.Add(Cell_Header);

                    Cell_Header = new TableCell();
                    Cell_Header.Text = " ";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 2;
                    Cell_Header.CssClass = "column_right";
                    HeaderRow.Cells.Add(Cell_Header);

                    Cell_Header = new TableCell();
                    Cell_Header.Text = "Cost";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 4;
                    Cell_Header.CssClass = "column_right";
                    HeaderRow.Cells.Add(Cell_Header);

                    Cell_Header = new TableCell();
                    Cell_Header.Text = " ";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 3;
                    Cell_Header.CssClass = "column_right_x";
                    HeaderRow.Cells.Add(Cell_Header);

                    ImportGrid.Controls[0].Controls.AddAt(0, HeaderRow);

                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(ImportGrid, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtnGRN_Click(object sender, EventArgs e)
        {
            try
            {
                string item = "";
                string BLNo = "";

                if (orderFrom.SelectedValue == "L")
                {
                    Button btn = (Button)sender;

                    GridViewRow row = (GridViewRow)btn.NamingContainer;
                    item = (row.FindControl("LclGridItem") as Label).Text;
                    BLNo = (row.FindControl("LclGridPONo") as Label).Text;

                }
                else
                {
                    Button btn = (Button)sender;

                    GridViewRow row = (GridViewRow)btn.NamingContainer;
                    item = (row.FindControl("ImpGridItem") as Label).Text;
                    BLNo = (row.FindControl("ImpGridBLNo") as Label).Text;
                }
                DataTable _result = CHNLSVC.CommonSearch.SearchItemGRNnew(null, null, "GRN", BLNo, item);
                txtItem.Text = item;
                textBLNo.Text = BLNo;
                //string value =string.Format("N2",_result.Columns["UNIT COST"]);
                //string value =((decimal)_result.Columns["UNIT COST"]).ToString("N2");
                //_result.Columns["UNIT COST"];
                foreach (DataRow row in _result.Rows)
                {
                    string value = ((decimal)row["UNIT COST"]).ToString("N2");
                    row["UNIT COST"] = value;
                }

                _result.Columns.Add("COST", typeof(string));

                foreach (DataRow row in _result.Rows)
                {
                    row["COST"] = ((decimal)row["UNIT COST"]).ToString("N2");
                }

                _result.Columns.Remove("UNIT COST");
                this.grdResult.Columns[0].Visible = false;
                //UpdatePanel10.Visible = false;
                //search.Visible = false;

                grdResult.DataSource = _result;
                grdResult.DataBind();
                foreach (GridViewRow row in grdResult.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        grdResult.HeaderRow.Cells[i].CssClass = "column_style_right";

                        if (i == 7 || i == 6)
                        {
                            row.Cells[i].CssClass = "data_style_decimal";
                        }
                        else
                        {
                            row.Cells[i].CssClass = "data_style_text";
                        }
                    }

                }

                BindUCtrlDDLData(_result);
                lblvalue.Text = "ItemGRN";
                UserPopoup.Show();
                ViewState["SEARCH"] = _result;
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void ExpctMUPer_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(AllExpctMUPer.Text))
            //{
            //    foreach (GridViewRow row in GrdGPCal.Rows)
            //    {
            //        TextBox newtextBox = (row.Cells[2].FindControl("GrdGPCalExpctMUPer") as TextBox);
            //        newtextBox.Text = string.Empty;
            //    }
            //    GrdGPCal.Columns[6].Visible = true;
            //    GrdGPCal.Columns[7].Visible = false;
            //}

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)
        }
        protected void btnClearPerAll_Click(object sender, EventArgs e)
        {
            //AllExpctMUPer.Text = string.Empty;
            //AllExpctGPPer.Text = string.Empty;

            //foreach (GridViewRow row in GrdGPCal.Rows)
            //{
            //    TextBox ExpctMU = (row.Cells[2].FindControl("GrdGPCalExpctMUPer") as TextBox);
            //    ExpctMU.Text = string.Empty;

            //    TextBox ExpctGP = (row.Cells[2].FindControl("GrdGPCalExpctGPPer") as TextBox);
            //    ExpctGP.Text = string.Empty;
            //}
            //GrdGPCal.Columns[6].Visible = true;
            //GrdGPCal.Columns[7].Visible = false;

            //GrdGPCal.Columns[8].Visible = true;
            //GrdGPCal.Columns[9].Visible = false;

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)
        }
        protected void ExpctGPPer_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(AllExpctGPPer.Text))
            //{
            //    foreach (GridViewRow row in GrdGPCal.Rows)
            //    {
            //        TextBox newtextBox = (row.Cells[2].FindControl("GrdGPCalExpctGPPer") as TextBox);
            //        newtextBox.Text = string.Empty;
            //    }
            //    GrdGPCal.Columns[8].Visible = true;
            //    GrdGPCal.Columns[9].Visible = false;
            //}

            //Edited & commented  on 01/12/2016 --RANDIMA (for new I/f changes)
        }
        protected void chkBLDate_CheckChanged(object sender, System.EventArgs e)
        {

            if (chkBLDate.Checked == true)
            {
                lblDtvalue.Text = "PODate";
                chkCostDate.Enabled = false;
                chkGRNDate.Enabled = false;
            }
            else
            {
                lblDtvalue.Text = "";
                chkGRNDate.Enabled = true;

                if (orderFrom.Text == "I")
                    chkCostDate.Enabled = true;
                else
                    chkCostDate.Enabled = false;
            }
        }
        protected void chkCostingDate_CheckChanged(object sender, System.EventArgs e)
        {

            if (chkCostDate.Checked == true)
            {
                lblDtvalue.Text = "CostingDate";
                chkBLDate.Enabled = false;
                chkGRNDate.Enabled = false;
            }
            else
            {
                lblDtvalue.Text = "";
                chkBLDate.Enabled = true;
                chkGRNDate.Enabled = true;
            }
        }
        protected void chkGRNDate_CheckChanged(object sender, System.EventArgs e)
        {

            if (chkGRNDate.Checked == true)
            {
                lblDtvalue.Text = "GRNDate";
                chkCostDate.Enabled = false;
                chkBLDate.Enabled = false;
            }
            else
            {
                lblDtvalue.Text = "";
                chkBLDate.Enabled = true;
                if (orderFrom.Text == "I")
                    chkCostDate.Enabled = true;
                else
                    chkCostDate.Enabled = false;

            }
        }

        protected void txtfrcst_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            LinkButton ImpGridFrCst = (row.FindControl("ImpGridFrCst") as LinkButton);
            TextBox txtfrcst = (row.FindControl("txtfrcst") as TextBox);
            ImpGridFrCst.Text = txtfrcst.Text;
            txtfrcst.Visible = false;
            ImpGridFrCst.Visible = true;

        }

        protected void ImpGridFrCst_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            LinkButton ImpGridFrCst = (row.FindControl("ImpGridFrCst") as LinkButton);
            TextBox txtfrcst = (row.FindControl("txtfrcst") as TextBox);
            ImpGridFrCst.Visible = false;
            txtfrcst.Visible = true;
            txtfrcst.Focus();

        }
        protected void btnPreviewGP_Click(object sender, EventArgs e)
        {
            Dictionary<string, decimal> priValue = new Dictionary<string, decimal>();
            if (string.IsNullOrEmpty(txtGPItem.Text) && Session["itmList"] == null)
            {
                DisplayMessage("Please add one item or item list", 2);
                return;
            }
            //else if (string.IsNullOrEmpty(txtGPPriBk.Text) || string.IsNullOrEmpty(txtGPPriLvl.Text)) //Wimal - 09/May/2018
            //{
            //    DisplayMessage("Please select price book and price level", 2);
            //    return;
            //}
            else
            {
                if (string.IsNullOrEmpty(txtGPItem.Text))
                {
                    List<string> itmList = new List<string>();
                    itmList = (List<string>)Session["itmList"];
                    DataTable dt2 = CHNLSVC.General.CostInqirytblitemlist(itmList, Session["UserCompanyCode"].ToString(), chkGPAllDoc.Checked, txtGPPriBk.Text.ToString(), txtGPPriLvl.Text, txtRecCount.Text, txtStatus.Text, Session["UserID"].ToString());
                    dt2.Columns.Add("SER");
                    dt2.Columns.Add("SER1");
                    Session["GPDataTable"] = dt2;
                    grdCostGPView.DataSource = dt2;
                    grdCostGPView.DataBind();
                    mpCostGPView.Show();
                    if (string.IsNullOrEmpty(txtGPPriBk.Text) && string.IsNullOrEmpty(txtGPPriLvl.Text))
                    {
                        grdCostGPView.Columns[10].Visible = false;
                        grdCostGPView.Columns[11].Visible = false;
                        grdCostGPView.Columns[12].Visible = false;
                    }
                    else
                    {
                        grdCostGPView.Columns[10].Visible = true;
                        grdCostGPView.Columns[11].Visible = true;
                        grdCostGPView.Columns[12].Visible = true;
                    }
                }
                else
                {
                    if (!(String.IsNullOrEmpty(txtGPSerial.Text)))
                {                    
                    List<string> itmList = new List<string>();
                    itmList = (List<string>)Session["itmList"];
                    DataTable dt2 = CHNLSVC.General.CostInqirytblitemSerial(itmList, Session["UserCompanyCode"].ToString(), true, txtGPPriBk.Text.ToString(), txtGPPriLvl.Text, txtGPItem.Text.ToString(), txtRecCount.Text, txtStatus.Text, false, txtGPSerial.Text);
                   // dt2.Columns.Add("SER");
                    Session["GPDataTable"] = dt2;
                    grdCostGPView.DataSource = dt2;
                    grdCostGPView.DataBind();
                    mpCostGPView.Show();
                    if (string.IsNullOrEmpty(txtGPPriBk.Text) && string.IsNullOrEmpty(txtGPPriLvl.Text))
                    {
                       // grdCostGPView.Columns[10].Visible = false;
                        grdCostGPView.Columns[11].Visible = false;
                        grdCostGPView.Columns[12].Visible = false;
                    }
                    else
                    {
                        grdCostGPView.Columns[10].Visible = true;
                        grdCostGPView.Columns[11].Visible = true;
                        grdCostGPView.Columns[12].Visible = true;
                    }

                }else
                    {
                    List<string> itmList = new List<string>();
                    itmList = (List<string>)Session["itmList"];
                    DataTable dt2 = CHNLSVC.General.CostInqirytblitem(itmList, Session["UserCompanyCode"].ToString(), chkGPAllDoc.Checked, txtGPPriBk.Text.ToString(), txtGPPriLvl.Text, txtGPItem.Text.ToString(), txtRecCount.Text, txtStatus.Text, false);
                    dt2.Columns.Add("SER");
                    dt2.Columns.Add("SER1");
                    Session["GPDataTable"] = dt2;
                    grdCostGPView.DataSource = dt2;
                    grdCostGPView.DataBind();
                    mpCostGPView.Show();
                    if (string.IsNullOrEmpty(txtGPPriBk.Text) && string.IsNullOrEmpty(txtGPPriLvl.Text))
                    {
                        grdCostGPView.Columns[10].Visible = false;
                        grdCostGPView.Columns[11].Visible = false;
                        grdCostGPView.Columns[12].Visible = false;
                    }
                    else
                    {
                        grdCostGPView.Columns[10].Visible = true;
                        grdCostGPView.Columns[11].Visible = true;
                        grdCostGPView.Columns[12].Visible = true;
                    }
                }
                }
                

            }
        }
        //protected void btnPreviewGP_Click(object sender, EventArgs e)
        //{
        //    Dictionary<string, decimal> priValue = new Dictionary<string, decimal>();
        //    if (string.IsNullOrEmpty(txtGPItem.Text) && Session["itmList"] == null)
        //    {
        //        DisplayMessage("Please add one item or item list", 2);
        //        return;
        //    }
        //    //else if (string.IsNullOrEmpty(txtGPPriBk.Text) || string.IsNullOrEmpty(txtGPPriLvl.Text)) //Wimal - 09/May/2018
        //    //{
        //    //    DisplayMessage("Please select price book and price level", 2);
        //    //    return;
        //    //}
        //    else
        //    {
        //        if (string.IsNullOrEmpty(txtGPItem.Text))
        //        {
        //            List<string> itmList = new List<string>();
        //            itmList = (List<string>)Session["itmList"];
        //            DataTable dt2 = CHNLSVC.General.CostInqirytblitemlist(itmList, Session["UserCompanyCode"].ToString(), chkGPAllDoc.Checked, txtGPPriBk.Text.ToString(), txtGPPriLvl.Text, txtRecCount.Text, txtStatus.Text, Session["UserID"].ToString());

        //            Session["GPDataTable"] = dt2;
        //            grdCostGPView.DataSource = dt2;
        //            grdCostGPView.DataBind();
        //            mpCostGPView.Show();
        //            if (string.IsNullOrEmpty(txtGPPriBk.Text) && string.IsNullOrEmpty(txtGPPriLvl.Text))
        //            {
        //                grdCostGPView.Columns[10].Visible = false;
        //                grdCostGPView.Columns[11].Visible = false;
        //                grdCostGPView.Columns[12].Visible = false;
        //            }
        //            else
        //            {
        //                grdCostGPView.Columns[10].Visible = true;
        //                grdCostGPView.Columns[11].Visible = true;
        //                grdCostGPView.Columns[12].Visible = true;
        //            }
        //        }
        //        else
        //        {
        //            List<string> itmList = new List<string>();
        //            itmList = (List<string>)Session["itmList"];
        //            DataTable dt2 = CHNLSVC.General.CostInqirytblitem(itmList, Session["UserCompanyCode"].ToString(), chkGPAllDoc.Checked, txtGPPriBk.Text.ToString(), txtGPPriLvl.Text, txtGPItem.Text.ToString(), txtRecCount.Text, txtStatus.Text, false);

        //            Session["GPDataTable"] = dt2;
        //            grdCostGPView.DataSource = dt2;
        //            grdCostGPView.DataBind();
        //            mpCostGPView.Show();
        //            if (string.IsNullOrEmpty(txtGPPriBk.Text) && string.IsNullOrEmpty(txtGPPriLvl.Text))
        //            {
        //                grdCostGPView.Columns[10].Visible = false;
        //                grdCostGPView.Columns[11].Visible = false;
        //                grdCostGPView.Columns[12].Visible = false;
        //            }
        //            else
        //            {
        //                grdCostGPView.Columns[10].Visible = true;
        //                grdCostGPView.Columns[11].Visible = true;
        //                grdCostGPView.Columns[12].Visible = true;
        //            }
        //        }


        //    }
        //}

        protected void grdCostGPView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    GridView HeaderGrid = (GridView)sender;
                    GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell Cell_Header = new TableCell();

                    Cell_Header.Text = " ";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 2;
                    Cell_Header.CssClass = "column_right";
                    HeaderRow.Cells.Add(Cell_Header);

                    Cell_Header = new TableCell();
                    Cell_Header.Text = "Status";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 2;
                    Cell_Header.CssClass = "column_right";
                    HeaderRow.Cells.Add(Cell_Header);

                    Cell_Header = new TableCell();
                    Cell_Header.Text = "Latest Cost";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 2;
                    Cell_Header.CssClass = "column_right";
                    HeaderRow.Cells.Add(Cell_Header);

                    Cell_Header = new TableCell();
                    Cell_Header.Text = "Pick up Doc";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 4;
                    Cell_Header.CssClass = "column_right";
                    HeaderRow.Cells.Add(Cell_Header);

                    Cell_Header = new TableCell();
                    Cell_Header.Text = " ";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 10;
                    Cell_Header.CssClass = "column_right";
                    HeaderRow.Cells.Add(Cell_Header);


                    //Cell_Header = new TableCell();
                    //Cell_Header.Text = " ";
                    //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    //Cell_Header.ColumnSpan = 2;
                    //Cell_Header.CssClass = "column_right_x";
                    //HeaderRow.Cells.Add(Cell_Header);

                    grdCostGPView.Controls[0].Controls.AddAt(0, HeaderRow);

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtnGPItem_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "costGPViewItem";
                UserPopoup.Show();
                ViewState["SEARCH"] = _result;
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnGPSerial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGPItem.Text))
                Session["_SerialSearchType"] = "SER1_WOITEM";
            else
                Session["_SerialSearchType"] = "SER1_WITEM";

            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
            DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, null, null);
            int count = result.Rows.Count;
            for (int x = count - 1; x >= 500; x--)
            {
                DataRow dr = result.Rows[x];
                dr.Delete();
            }
            result.AcceptChanges();
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "itmSerialI";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            UserPopoup.Show();
            txtSearchbyword.Focus();
        }

        protected void lbtnGPCat1_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "ItmMainCat";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnGPCat2_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "ItmCat2";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnGPCat3_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "ItmCat3";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnGPBrand_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "ItmBrand";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;

        }

        protected void lbtnGPPriBk_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "PriceBook";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnGPCircular_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
            DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "Circular";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnGPPromoCd_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
            DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "Promotion";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnGPPriLvl_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "PriceLevel";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnGPItmUp_Click(object sender, EventArgs e)
        {
            excelUpload.Show();
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileupexcelupload.HasFile)
                {
                    string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                    if (Extension == ".xls" || Extension == ".XLS" || Extension == ".xlsx" || Extension == ".XLSX")
                    {

                    }
                    else
                    {
                        Label3.Visible = true;
                        Label3.Text = "Please select a valid excel (.xls or .xlsx) file";
                    }
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    //LoadData(FolderPath + FileName);
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fileupexcelupload.SaveAs(FilePath);
                    Session["FilePath"] = FilePath;
                    string value = string.Empty;
                    ExcelProcess(FilePath, 1, out value);
                    if (value == "1")
                    {
                        DisplayMessage("Excel uploading is successful", 1);
                    }
                }
                else
                {
                    DisplayMessage("Please select the correct upload file path", 2);
                    Label3.Visible = true;
                    Label3.Text = "Please select the correct upload file path";
                    excelUpload.Show();

                }
            }
            catch (Exception ex)
            {
                Label3.Visible = true;
                Label3.Text = ex.Message;
                excelUpload.Show();
                DisplayMessage(ex.Message, 1);
            }


        }

        private void ExcelProcess(string FilePath, int option, out string value)
        {
            DataTable[] GetExecelTbl = LoadData(FilePath);
            List<string> itmList = new List<string>();
            if (Session["itmList"] != null)
            {
                itmList = (List<string>)Session["itmList"];
            }
            value = "";
            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {

                    // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        try
                        {

                            string item = GetExecelTbl[0].Rows[i][0].ToString();
                            itmList.Add(item);
                            value = "1";
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                            Label3.Visible = true;
                            Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
                            excelUpload.Show();
                            value = "3";
                            return;
                        }
                    }
                    Session["itmList"] = itmList;
                }
            }
        }

        public DataTable[] LoadData(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();

            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
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


                return new DataTable[] { Tax };
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

        protected void btnGPPnlProc_Click(object sender, EventArgs e)
        {
            if (Session["GPDataTable"] != null)
            {
                DataTable dt = (DataTable)Session["GPDataTable"];
                foreach (DataRow row in dt.Rows)
                {
                    //row["CUR_PRI"] = curItmPri.ToString();
                    row["PRI_BK"] = txtGPPnlPB.Text;
                    row["PRI_LVL"] = txtGPPnlPL.Text;
                    decimal m_markupper = 0;
                    decimal m_gp_per = 0;
                    if (txtGPPnlExMU.Text.ToString() != "" && IsNumeric(txtGPPnlExMU.Text.ToString()))
                    {
                        m_markupper = Convert.ToDecimal(txtGPPnlExMU.Text.ToString());
                    }
                    if (txtGPPnlExGP.Text.ToString() != "" && IsNumeric(txtGPPnlExGP.Text.ToString()))
                    {
                        m_gp_per = Convert.ToDecimal(txtGPPnlExGP.Text.ToString());
                    }
                    decimal m_itm_price = Convert.ToDecimal(row["CUR_PRI"].ToString());
                    decimal m_cost = Convert.ToDecimal(row["DP_COST"].ToString());

                    decimal mu_val = m_itm_price * m_markupper / 100;
                    decimal gp_val = m_cost * m_gp_per / 100;
                    row["MK_PER"] = m_markupper;
                    row["GP_PER"] = m_gp_per;
                    row["CUR_NWMU"] = mu_val;
                    row["CUR_NWGP"] = gp_val;
                }
                Session["GPDataTable"] = dt;
                grdCostGPView.DataSource = dt;
                grdCostGPView.DataBind();
                mpCostGPView.Show();
            }
            else
            {
                DisplayMessage("No Items to check GP values", 2);
                return;
            }
        }

        protected void LbtnGPPnlPB_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "PriceBook2";
                UserPopoup.Show();
                ViewState["SEARCH"] = _result;
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void grdCGMUPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;

                GridViewRow row = (GridViewRow)txt.NamingContainer;
                decimal per_mk = Convert.ToDecimal((row.FindControl("grdCGMUPer") as TextBox).Text);
                decimal per_gp = Convert.ToDecimal((row.FindControl("grdCGGPPer") as TextBox).Text);
                decimal itm_price = Convert.ToDecimal((row.FindControl("grdCGCurPri") as Label).Text);
                decimal cost = Convert.ToDecimal((row.FindControl("grdCGLtstDP") as Label).Text);

                decimal mk_nw = per_mk * itm_price / 100;
                decimal gp_new = cost * per_gp / 100;

                (row.FindControl("grdCGMU") as Label).Text = mk_nw.ToString();
                (row.FindControl("grdCGGP") as Label).Text = gp_new.ToString();
                mpCostGPView.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnexportexel_Click(object sender, EventArgs e)
        {
            try
            {
                string out1 = "";
                DataTable dt = Session["GPDataTable"] as DataTable;
                dt.TableName = "dt";
                string path = CHNLSVC.MsgPortal.ExportExcel2007(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), dt, out out1);
                _copytoLocal(path);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xlsx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch (Exception ex)
            {

            }
        }
        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                string targetFileName = Server.MapPath("~\\Temp\\") + filenamenew + ".xlsx";
                System.IO.File.Copy(@"" + _filePath, "C:/Download_excel/" + filenamenew + ".xlsx", true);
                System.IO.File.Copy(@"" + _filePath, targetFileName, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + "This file does not exist." + "')", true);
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, "ITEM", "%" + txtItem.Text.ToString());
                if (_result.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + "Invalid Item." + "')", true);
                    txtItem.Text = "";
                    return;
                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void LbtnGPPnlPL_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "PriceLevel2";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnGPPnlPromoCd_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
            DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "Promotion2";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnGPPnlCir_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
            DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "Circular2";
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnclosegpvw_Click(object sender, EventArgs e)
        {

            mpCostGPView.Hide();

        }

        protected void grdCGVATPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;

                GridViewRow row = (GridViewRow)txt.NamingContainer;
                string tax = (row.FindControl("grdCGVATPer") as TextBox).Text.ToString();
                if (tax != "")
                {
                    if (IsNumeric(tax))
                    {
                        if (Convert.ToDecimal(tax) < 0)
                        {
                            DisplayMessage("Plese Enter Valid Tax", 2);
                        }
                    }
                    else
                    {
                        DisplayMessage("Plese Enter Valid Tax", 2);
                    }
                }
                mpCostGPView.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtGPPnlPB_TextChanged(object sender, EventArgs e)
        {
            mpCostGPView.Show();
        }

        protected void txtGPPnlPromoCd_TextChanged(object sender, EventArgs e)
        {
            mpCostGPView.Show();
        }

        protected void txtGPPnlExMU_TextChanged(object sender, EventArgs e)
        {
            mpCostGPView.Show();
        }

        protected void txtGPPnlPL_TextChanged(object sender, EventArgs e)
        {
            mpCostGPView.Show();
        }

        protected void txtGPPnlCir_TextChanged(object sender, EventArgs e)
        {
            mpCostGPView.Show();
        }

        protected void txtGPPnlExGP_TextChanged(object sender, EventArgs e)
        {
            mpCostGPView.Show();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mpCostGPView.Show();
        }

        protected void txtGPSerial_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtGPItem.Text))
            //    Session["_SerialSearchType"] = "SER1_WOITEM";
            //else
            //    Session["_SerialSearchType"] = "SER1_WITEM";
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
            //DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, "SERIAL 1", txtGPSerial.Text);
            //if (result.Rows.Count == 0)
            //{
            //    DisplayMessage("Plese Enter Valid Serial", 2);
            //    txtGPSerial.Text = "";
            //    return;
            //}

            //else if (result.Rows.Count == 1)
            //{
            //    if (txtGPSerial.Text != "")
            //    { txtGPItem.Text = result.Rows[0][3].ToString(); }
            //    else
            //    { txtGPItem.Text = ""; }

            //    //type = Convert.ToBoolean(_dtResults.Rows[0][0].ToString());
            //}

            //else if (result.Rows.Count == 2)
            //{
            //    DisplayMessage("Inventory Contains 2 items" + result.Rows[0][3].ToString() + " , " + result.Rows[1][3].ToString(), 2);
            //}

            //New SourceCode 
            DataTable _InitialStageSearch = null;
            string _serial = string.Empty;
            string _serialType = string.Empty;
            Int16 _isWholeWord = 1;
            _serialType = "SERIAL1";
            _serial = txtGPSerial.Text;

            _InitialStageSearch = CHNLSVC.Inventory.GetSerialItem(_serialType, Session["UserCompanyCode"].ToString(), _serial, _isWholeWord);
            var dtSerial = (from p in _InitialStageSearch.AsEnumerable()
                           select new { item = p.Field<String>("INS_ITM_CD"), ser = p.Field<String>("INS_SER_1") }).Distinct();
            string item="";
            foreach (var dtSer in dtSerial)
            {
                item = Convert.ToString(dtSer.item);
            }

            if (dtSerial.Count() == 0)
            {
                // Page.ClientScript.RegisterStartupScript(this.GetType(),"toastr_message", "toastr.error('There was an error', 'Error')", true);
                //   Page.ClientScript.RegisterStartupScript(this.GetType(), "showStickyWarningToast", "showStickyWarningToast('hello')", true);
                string msg = "There is no such serial available in the system for the given criteria";
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
                //  divscro.Visible = true;                
                return;
            }
            if (dtSerial.Count() == 1)
            {
                if (txtGPSerial.Text != "")
                { txtGPItem.Text = item; }
                else
                { txtGPItem.Text = ""; }
                return;
            }

            if (dtSerial.Count() == 2)
            {
                DisplayMessage("Inventory Contains 2 items" + item, 2);
                //-------
                //    GetItemAdvanceDetail(lblItem.Text.Trim());
                return;
            }
            //

        }
    }

}