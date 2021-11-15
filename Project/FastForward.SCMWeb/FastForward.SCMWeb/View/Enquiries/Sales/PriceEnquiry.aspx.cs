using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Sales
{
    public partial class PriceEnquiry : BasePage
    {
        #region Variables
        MasterProfitCenter _masterProfitCenter = null;
        List<PriceDefinitionRef> _priceDefinitionRef = null;
        MasterItem _itemdetail = null;
        private bool IsSuperUser = false;

        private int PageSize = 50;
        private int CurrentPageSize = 0;
       // private Boolean _isStrucBaseTax = false;
        protected bool _isStrucBaseTax { get { Session["_isStrucBaseTax "] = (Session["_isStrucBaseTax "] == null) ? false : (bool)Session["_isStrucBaseTax "]; ; return (bool)Session["_isStrucBaseTax "]; } set { Session["_isStrucBaseTax "] = value; } }
       
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    pageClear();
                }
            }
            catch
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void pageClear()
        {
            txtPc.Text = Session["UserDefProf"].ToString();
            rdoAsAt.Checked = true;
            hdfTabIndex.Value = "#PriceDetail";
            Session["Tab"] = "1";
            grdPriceDetail.DataSource = new int[] { };
            grdPriceDetail.DataBind();
            grdPriceSerial.DataSource = new int[] { };
            grdPriceSerial.DataBind();
            grdScheme.DataSource = new int[] { };
            grdScheme.DataBind();
            grdPromotionDiscount.DataSource = new int[] { };
            grdPromotionDiscount.DataBind();
            grdPayType.DataSource = new int[] { };
            grdPayType.DataBind();
            Session["IsSuperUser"] = "";
            UserPermissionforSuperUser();
            DateTime Date = DateTime.Now;
            txtFromDt.Text = Date.ToString("dd/MMM/yyyy");
            txtToDt.Text = "31/Dec/2999";
            txtAsAtDt.Text = Date.ToString("dd/MMM/yyyy");

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;


           
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

        #region Rooting for Common Search

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
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

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtPriceBook.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Scheme:
                    {
                        paramsText.Append(txtSchemeCD.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceType:
                    {
                        paramsText.Append("%" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circualr:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RawPriceBook:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RawPriceLevel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtPriceBook.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void DisplayMessage(String Msg, Int32 option)
        {
            string _Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
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

        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Pc")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtPc.Text = ID;
                txtPc.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if ((lblvalue.Text == "book_user_false") || (lblvalue.Text == "book_user_true"))
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtPriceBook.Text = ID;
                txtPriceBook.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if ((lblvalue.Text == "level_user_false") || (lblvalue.Text == "level_user_true"))
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtLevel.Text = ID;
                txtLevel.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if ((lblvalue.Text == "Circular"))
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtCircular.Text = ID;
                txtCircular.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Promotion")
            {

                string Name = grdResult.SelectedRow.Cells[1].Text;
                txtPromotion.Text = ID;
                txtPromotion.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;             
            }
            if (lblvalue.Text == "Item")
            {
                txtItem.Text = ID;
                LoadItemDetail(txtItem.Text.Trim());
                ClearGridView();
                if (chkWithInv.Checked)
                {
                    LoadBalance();
                }
                   
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Cat1")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtCate1.Text = ID;
                txtCate1.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;      
            }
            if (lblvalue.Text == "Cat2")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtCate2.Text = ID;
                txtCate2.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Cat3")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtCate3.Text = ID;
                txtCate3.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Type")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                string Des = grdResult.SelectedRow.Cells[3].Text;
                txtType.Text = Name;
                txtType.ToolTip = Des;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Customer")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtCustomer.Text = ID;
                txtCustomer.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "SchemeCD")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtSchemeCD.Text = ID;
                txtSchemeCD.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Pc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "book_user_false")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "book_user_true")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceBook);
                DataTable _result = CHNLSVC.CommonSearch.GetRawPriceBook(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "level_user_false"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "level_user_true"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceLevel);
                DataTable _result = CHNLSVC.CommonSearch.GetRawPriceLevel(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Circular"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Promotion")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cat3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Type")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceType);
                DataTable _result = CHNLSVC.CommonSearch.Get_PriceTypes_SearchData(SearchParams, null, null);
                _result.Columns.Remove("Seq");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }

            if (lblvalue.Text == "SchemeCD")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
                DataTable _result = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Pc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "book_user_false")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "book_user_true")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceBook);
                DataTable _result = CHNLSVC.CommonSearch.GetRawPriceBook(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "level_user_false")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "level_user_true")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceLevel);
                DataTable _result = CHNLSVC.CommonSearch.GetRawPriceLevel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Circular"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Promotion")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
            }
            if (lblvalue.Text == "Cat3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Type")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceType);
                DataTable _result = CHNLSVC.CommonSearch.Get_PriceTypes_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                _result.Columns.Remove("Seq");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "SchemeCD")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
                DataTable _result = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Pc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "book_user_false")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "book_user_true")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceBook);
                DataTable _result = CHNLSVC.CommonSearch.GetRawPriceBook(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "level_user_false")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "level_user_true")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceLevel);
                DataTable _result = CHNLSVC.CommonSearch.GetRawPriceLevel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Circular"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Promotion")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
            }
            if (lblvalue.Text == "Cat3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Type")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceType);
                DataTable _result = CHNLSVC.CommonSearch.Get_PriceTypes_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                _result.Columns.Remove("Seq");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "SchemeCD")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
                DataTable _result = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
           
        }
        #endregion

        #region Rooting for Load Item Detail
        private bool LoadItemDetail(string _item)
        {
            bool _isValid = false;
            try
            {
                //lblItemDescription.Text = "Description : ";
                //lblItemModel.Text = "Model : ";
                //lblItemBrand.Text = "Brand : ";
                //lblItemSubStatus.Text = "Serial Status : ";
                //lblVatRate.Text = "Imported VAT Rt. : ";
                _itemdetail = new MasterItem();

                if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    DisplayMessage("Please check the item code", 2);
                    txtItem.Text = "";
                    txtItem.Focus();
                    _isValid = false;
                    return _isValid;
                }
                if (_itemdetail != null)
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        _isValid = true;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "None";

                       // lblItemDescription.Text = "Description : "; //+ _description; 
                        lbtnItemDescription.Text = _description; //Edit by Chamal 22/Oct/2013
                       // lblItemModel.Text = "Model : ";// +_model;
                        lbtnItemModel.Text = _model; //Edit by Chamal 22/Oct/2013
                        //lblItemBrand.Text = "Brand : " + _brand;
                        lbtnBrand.Text = _brand;
                       // lblItemSubStatus.Text = "Serial Status : " + _serialstatus;
                        lblItemSubStatus.Text =  _serialstatus;
                        Decimal VAT_RATE = CHNLSVC.Sales.GET_Item_vat_Rate(Session["UserCompanyCode"].ToString(), _itemdetail.Mi_cd, "VAT");
                       // lblVatRate.Text = "Imported VAT Rt. : " + VAT_RATE.ToString() + "%";
                        lblVatRate.Text =  VAT_RATE.ToString() + "%";
                    }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
                return _isValid;
            }
            return _isValid;
        }
        #endregion
        private void UserPermissionforSuperUser()
        {
            string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, CommonUIDefiniton.UserPermissionType.PRICENQ.ToString()))
            {
                IsSuperUser = true;
                Session["IsSuperUser"] = true;
               // btnSearch_Pc.Enabled = true;
            }
            else
            {
                IsSuperUser = false;
                Session["IsSuperUser"] = false;
              //  btnSearch_Pc.Enabled = false;
            }
        }
        protected void lbtnPc_Click(object sender, EventArgs e)
        {
            try
            {
                IsSuperUser = Convert.ToBoolean(Session["IsSuperUser"]);
                if (IsSuperUser == false) 
                { 
                    txtPc.Text=string.Empty; 
                    return; 
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);               
            }
        }

        protected void txtPc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IsSuperUser = Convert.ToBoolean(Session["IsSuperUser"].ToString());
                if (IsSuperUser == false)
                {
                    txtPc.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtPc.Text)) return;
                MasterProfitCenter _pc = CHNLSVC.General.GetPCByPCCode(Session["UserCompanyCode"].ToString(), txtPc.Text.Trim());
                if (_pc == null || string.IsNullOrEmpty(_pc.Mpc_com))
                {
                    DisplayMessage("Please select the valid profit center", 2);                                 
                    txtPc.Text=string.Empty;
                    txtPc.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);               
            }
        }

   
        protected void lbtnSearch_Book_Click_Click(object sender, EventArgs e)
        {
            try
            {
                IsSuperUser = Convert.ToBoolean(Session["IsSuperUser"]);
                if (IsSuperUser == false)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "book_user_false";
                    UserPopoup.Show();
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetRawPriceBook(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "book_user_true";
                    UserPopoup.Show();
                }
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);          
            }
        }

  
        protected void txtPriceBook_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtPriceBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please check the price book", 2);
                    txtPriceBook.Text = string.Empty;
                    txtPriceBook.Focus();
                }
                txtLevel.Text = string.Empty;
                ClearGridView();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void lbtnGetDetail_Click(object sender, EventArgs e)
        {
            string Tabvalue = hdfTabIndex.Value;
            IsSuperUser = Convert.ToBoolean(Session["IsSuperUser"]);

            if (chkScheme.Checked)
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    DisplayMessage("You need to select the item if you need to search for schemes!", 2);
                    return;
                }
            }



            DateTime _fromdate = new DateTime();
            DateTime _todate = new DateTime();
            bool _isHistory = false;
            bool _isAsAtHistory = false;
            string _itemStatus = string.Empty;
            _itemStatus = ddlStatus.Text == "GOOD" ? "GOD" : "GDLP";

            if (rdoDateRange.Checked)
            {
                _fromdate = Convert.ToDateTime(txtFromDt.Text);
                _todate = Convert.ToDateTime(txtToDt.Text);
                _isHistory = true;
            }
            else if (rdoAsAt.Checked)
            {
                _fromdate = Convert.ToDateTime(txtAsAtDt.Text);
                _todate = Convert.ToDateTime(txtAsAtDt.Text);
                _isHistory = false;
                if (chkWithHistory.Checked)
                    _isAsAtHistory = true;
                else
                    _isAsAtHistory = false;
            }
            else
            {
                _todate = DateTime.Now;
                   _fromdate = DateTime.Now;
            }
            #region Get Prices
            if (Tabvalue == "#PriceDetail")
            {
                string _Taxstructure = null;
                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                if (_isStrucBaseTax == true)       //kapila 21/9/2015
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), null, string.Empty, string.Empty, _mstItem.Mi_anal1);
                    _Taxstructure = _mstItem.Mi_anal1;
                }
                else
                    _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), null, string.Empty, string.Empty);



                //  DataTable _tax = CHNLSVC.Inventory.GetItemTaxData(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                if (_taxs.Count <= 0)
                {
                    DisplayMessage("Tax definitions not setup.Please contact inventory dept.", 2);                     
                    txtItem.Focus();
                    return;
                }
                //Session["UserID"].ToString()
                List<PriceDetailRef> _ref = CHNLSVC.Sales.GetPriceEnquiryDetailNew(txtPc.Text.Trim(), CurrentPageSize, CurrentPageSize + PageSize, Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtCustomer.Text.Trim(),
                    txtItem.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text.Trim(), chkAllStatus.Checked ? string.Empty : _itemStatus, 
                    txtType.Text.Trim(), txtCircular.Text.Trim(), _fromdate.Date, _todate.Date, _isHistory, _isAsAtHistory,
                    chkAllStatus.Checked, IsSuperUser, txtPromotion.Text.Trim(), _Taxstructure);
                if (_ref == null || _ref.Count <= 0)
                    DisplayMessage("There is no price found for the selected criteria!", 2);      
                  


                // Nadeeka 17-10-2015 
                foreach (PriceDetailRef j in _ref)
                {
                    string expression;
                    decimal _tax = 0;
                    //_tax = Convert.ToDecimal("12.5");
                    _tax = Convert.ToDecimal("11");
                    expression = "sapl_is_sales = 0";
                    DataRow[] foundRows;

                    DataTable _extPrice = CHNLSVC.Sales.GetPriceLevelTable(Session["UserCompanyCode"].ToString(), j.Sapd_pb_tp_cd, j.Sapd_pbk_lvl_cd);
                    foundRows = _extPrice.Select(expression);

                    if (foundRows.Length > 0)
                    {
                        j.Sapd_with_tax = j.Sapd_itm_price + (j.Sapd_itm_price * _tax / 100);
                    }

                }


                //BindingSource _source1 = new BindingSource();
                //DataTable _tbl = _ref.ToDataTable();
                //_source1.DataSource = _tbl;
                grdPriceDetail.DataSource = _ref;
                grdPriceDetail.DataBind();
                ViewState["grdPriceDetail"] = _ref;
            }
            #endregion
            #region Get Scheme
            if (Tabvalue == "#SchemeDetail")
            {
                if (chkScheme.Checked)
                {
                    if (txtItem.Text == "")
                    {
                        DisplayMessage("Please enter item code", 2); 
                        txtItem.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPc.Text))
                    {
                        DisplayMessage("Please select the profit center", 2);                       
                        txtPc.Text=string.Empty;
                        txtPc.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPriceBook.Text))
                    {
                        DisplayMessage("Please select the price book", 2);
                        txtPriceBook.Text = string.Empty;
                        txtPriceBook.Focus(); 
                        return; 
                    }
                    if (string.IsNullOrEmpty(txtLevel.Text)) {
                        DisplayMessage("Please select the price level", 2);
                        txtLevel.Text = string.Empty;
                        txtLevel.Focus(); 
                        return;
                    }


                    try { Convert.ToDateTime(txtAsAtDt.Text); }
                    catch (Exception ex)
                    {
                        DisplayMessage("Please enter an as at date", 2);
                        return;
                    }

                    string entered_schemeCD = null;
                    if (txtSchemeCD.Text.Trim() != "")
                        entered_schemeCD = txtSchemeCD.Text.Trim();

                    string entered_PriceBook = null;
                    if (txtPriceBook.Text.Trim() != "")
                        entered_PriceBook = txtPriceBook.Text.Trim();

                    string entered_Level = null;
                    if (txtLevel.Text.Trim() != "")
                        entered_Level = txtLevel.Text.Trim();

                    DataTable pcHerachyTB = new DataTable();
                    List<HpSchemeDefinition> Final_schemaList = new List<HpSchemeDefinition>();

                    pcHerachyTB = CHNLSVC.Sales.Get_hpHierachy(txtPc.Text.Trim());

                    if (pcHerachyTB.Rows.Count > 0)
                    {
                        MasterItem mstItm = new MasterItem();
                        mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                        string _item = mstItm.Mi_cd;
                        string _brand = mstItm.Mi_brand;
                        string _mainCat = mstItm.Mi_cate_1;
                        string _subCat = mstItm.Mi_cate_2;
                        foreach (DataRow pcH in pcHerachyTB.Rows)
                        {
                            string party_tp = Convert.ToString(pcH["MPI_CD"]);
                            string party_cd = Convert.ToString(pcH["MPI_VAL"]);
                            //List<HpSchemeDefinition> schemsList = new List<HpSchemeDefinition>();
                            //schemsList = CHNLSVC.Sales.get_HP_Schemes(Convert.ToDateTime(txtAsAtDt.Text), txtItem.Text.Trim(), party_tp, party_cd, mstItm.Mi_brand, mstItm.Mi_cate_1, mstItm.Mi_cate_2, entered_schemeCD, entered_PriceBook, entered_Level);
                            //Final_schemaList.AddRange(schemsList);


                            //get details from item
                            List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, _item, null, null, null, null, null);
                            if (_def != null)
                            {
                                Final_schemaList.AddRange(_def);
                            }

                            //get details according to main category
                            List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, null, _brand, _mainCat, null, null, null);
                            if (_def1 != null)
                            {
                                Final_schemaList.AddRange(_def1);
                            }

                            //get details according to sub category
                            List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, null, _brand, null, _subCat, null, null);
                            if (_def2 != null)
                            {
                                Final_schemaList.AddRange(_def2);
                            }

                            //get details according to price book and level
                            List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, null, null, null, null, null, null);
                            if (_def3 != null)
                            {
                                Final_schemaList.AddRange(_def3);
                            }
                        }
                    }

                    if (txtPriceBook.Text.Trim() != "")
                        Final_schemaList.RemoveAll(x => x.Hpc_pb != txtPriceBook.Text.Trim());

                    if (txtLevel.Text.Trim() != "")
                        Final_schemaList.RemoveAll(x => x.Hpc_pb_lvl != txtLevel.Text.Trim());

                    if (txtCircular.Text.Trim() != "")
                        Final_schemaList.RemoveAll(x => x.Hpc_cir_no != txtCircular.Text.Trim());


                    var _record = (from _lst in Final_schemaList
                                   where _lst.Hpc_is_alw == false
                                   select _lst).ToList().Distinct();

                    foreach (HpSchemeDefinition j in _record)
                    {
                        Final_schemaList.RemoveAll(item => item.Hpc_sch_cd == j.Hpc_sch_cd);
                    }
                    if (Final_schemaList == null || Final_schemaList.Count <= 0)
                        DisplayMessage("There is no scheme found for the selected criteria!", 2);
                      

                   // BindingSource _source = new BindingSource();
                   // _source.DataSource = Final_schemaList;
                    grdScheme.DataSource = Final_schemaList;
                    grdScheme.DataBind();
                    ViewState["grdScheme"] = Final_schemaList;
                }
            }
            #endregion
            #region Get Serialized Prices
            if (Tabvalue == "#SerializedPriceDetail")
            {
                List<PriceSerialRef> _ref1 = CHNLSVC.Sales.GetPriceEnquirySerialDetail(txtPc.Text.Trim(), CurrentPageSize, CurrentPageSize + PageSize, Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text.Trim(), chkAllStatus.Checked ? string.Empty : _itemStatus, txtType.Text.Trim(), txtCircular.Text.Trim(), _fromdate.Date, _todate.Date, _isHistory, _isAsAtHistory, true, IsSuperUser);
                if (_ref1 == null || _ref1.Count <= 0)
                    DisplayMessage("There is no serialized price found for the selected criteria!", 2);    
                 
              //  BindingSource _source2 = new BindingSource();
              //  DataTable _tbl1 = _ref1.ToDataTable();
              //  _source2.DataSource = _tbl1;

                //List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                //oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                //if (_ref1 != null)
                //{
                //    foreach (PriceSerialRef itemSer in _ref1)
                //    {
                //        if (oItemStaus != null && oItemStaus.Count > 0)
                //        {
                //            itemSer.Sapl_itm_stuts_Des = oItemStaus.Find(x => x.Mis_cd == itemSer.Sapl_itm_stuts).Mis_desc;
                //        }
                //    }
                //}
                DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                if (_status.Rows.Count > 0)
                {
                    if (_ref1 != null)
                    {
                        foreach (PriceSerialRef itemSer in _ref1)
                        {
                            foreach (DataRow _row in _status.Rows)
                            {
                                if (itemSer.Sapl_itm_stuts == _row[0].ToString())
                                {
                                    itemSer.Sapl_itm_stuts_Des = _row[1].ToString();
                                }
                            }

                        }
                    }
                }
                grdPriceSerial.DataSource = _ref1;
                grdPriceSerial.DataBind();
                ViewState["grdPriceSerial"] = _ref1;
            }
            #endregion
            #region Get PromoDiscount
            if (Tabvalue == "#PromoDiscount")
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text)) {
                    DisplayMessage("Please select the price book.", 2);                   
                    return; 
                }
              
                if (string.IsNullOrEmpty(txtLevel.Text))
                {
                   DisplayMessage("Please select the price level.", 2);                   
                    return; 
                }
                if (string.IsNullOrEmpty(txtItem.Text)) 
                {
                    DisplayMessage("Please select the item.", 2);                       
                    return; 
                }
                if (IsSuperUser == false) txtPc.Text = Session["UserDefProf"].ToString();
                if (string.IsNullOrEmpty(txtPc.Text))
                {
                    DisplayMessage("Please select the profit center", 2);                     
                    return; 
                }
                Int32 _isPromotionBase = chkIsPromotionBase.Checked ? 1 : 0;
                Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                try
                {

                    List<CashPromotionDiscountDetail> _table = new List<CashPromotionDiscountDetail>();

                    if (!chkCancelDis.Checked)
                        _table = CHNLSVC.Sales.GetPromotionalDiscount(Convert.ToDateTime(txtAsAtDt.Text).Date, _timeno, Convert.ToDateTime(txtAsAtDt.Text.Trim()).DayOfWeek.ToString().ToUpper(), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtItem.Text.Trim(), Session["UserCompanyCode"].ToString(), txtPc.Text.Trim(), _isPromotionBase, IsSuperUser ? 1 : 0);
                    else if (chkCancelDis.Checked && IsSuperUser)
                        _table = CHNLSVC.Sales.GetPromotionalDiscountCacnel(Convert.ToDateTime(txtAsAtDt.Text).Date, _timeno, Convert.ToDateTime(txtAsAtDt.Text.Trim()).DayOfWeek.ToString().ToUpper(), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtItem.Text.Trim(), Session["UserCompanyCode"].ToString(), txtPc.Text.Trim(), _isPromotionBase, IsSuperUser ? 1 : 0);

                    if (_table == null || _table.Count <= 0)
                        DisplayMessage("There is no promotion discount found for the selected criteria!", 2);    
                        
                    _table.Where(X => X.Spdd_alw_cc_pro).ToList().ForEach(X => X.Spdd_cre_by = "Yes");
                    _table.Where(X => X.Spdd_alw_cc_pro == false).ToList().ForEach(X => X.Spdd_cre_by = "No");
                    _table.Where(x => x.Spdd_stus == 1).ToList().ForEach(x => x.Spdd_mod_by = "Active");
                    _table.Where(x => x.Spdd_stus == 0).ToList().ForEach(x => x.Spdd_mod_by = "Inactive");
                    _table.Where(x => x.Spdi_act).ToList().ForEach(x => x.Spdi_mod_by = "Active");
                    _table.Where(x => x.Spdi_act == false).ToList().ForEach(x => x.Spdi_mod_by = "Inactive");
                    grdPromotionDiscount.DataSource = _table;
                    grdPromotionDiscount.DataBind();
                    ViewState["grdPromotionDiscount"] = _table;
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message, 4);      
                }
                return;
            }

            #endregion
            #region Get Pay Types
            if (Tabvalue == "#PaymentType")
            {
                //if (string.IsNullOrEmpty(txtItem.Text))
                //    if (string.IsNullOrEmpty(txtPriceBook.Text))
                //    {
                //        DisplayMessage("Please enter price book", 2);
                //        return;
                //    }

                //if (!IsSuperUser && string.IsNullOrEmpty(txtItem.Text))
                //{
                //    DisplayMessage("Please enter price book", 2);
                //    return;
                //}
                DataTable _tbl = CHNLSVC.Sales.TrPayTpDefEnquiry(Session["UserCompanyCode"].ToString(), txtCircular.Text.Trim(), txtPromotion.Text.Trim(), null, rdoAsAt.Checked ? "Y" : "N", _fromdate.Date, _todate.Date, Convert.ToDateTime(txtAsAtDt.Text).Date, null, txtPriceBook.Text, txtLevel.Text, txtCate1.Text, txtCate2.Text, txtPc.Text, txtItem.Text);
                if (_tbl == null || _tbl.Rows.Count <= 0)
                    DisplayMessage("There is no pay type definition for the selected criteria!", 2);
                    
               // BindingSource _source1 = new BindingSource();
               // dgvPayType.AutoGenerateColumns = false;
               // _source1.DataSource = _tbl;
                grdPayType.DataSource = _tbl;
                grdPayType.DataBind();
                ViewState["grdPayType"] = _tbl;
            }
            #endregion

        }

        private void ClearGridView()
        {
           // grdCombine.DataSource = new int[] { }; 
            //grdCombine.data
            grdPriceSerial.DataSource = new int[] { };
            grdPriceSerial.DataBind();
            grdPriceDetail.DataSource = new int[] { };
            grdPriceDetail.DataBind();
            grdScheme.DataSource = new int[] { };
            grdScheme.DataBind();
        }

        protected void txtLevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLevel.Text)) return;
                if (string.IsNullOrEmpty(txtPriceBook.Text)) {
                    DisplayMessage("Please select the price book.", 2);                          
                    txtPriceBook.Focus(); return;
                }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtPriceBook.Text.Trim(), txtLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    DisplayMessage("Please check the price level.", 2);                       
                    txtLevel.Text=string.Empty; 
                    txtLevel.Focus(); 
                    return; 
                }
                ClearGridView();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);      
            }
        }

        protected void lbtnSearch_Level_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text))
                {
                    DisplayMessage("Please select the price book", 2);      
                    txtPriceBook.Text=string.Empty;
                    txtPriceBook.Focus();
                    return;
                }
                if (IsSuperUser == false)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "level_user_false";
                    UserPopoup.Show();
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceLevel);
                    DataTable _result = CHNLSVC.CommonSearch.GetRawPriceLevel(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "level_user_true";
                    UserPopoup.Show();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);      
            }
        }

        protected void txtCircular_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCircular.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchData(SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CIRCULAR") == txtCircular.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Please select the valid circular no", 2);                        
                    txtCircular.Text=string.Empty;
                    txtCircular.Focus();
                    return;
                }
                ClearGridView();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);  
            }
        }

        protected void lbtnSearch_Circular_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Circular";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);  
            }
        }

        protected void txtPromotion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPromotion.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, "Promotion", txtPromotion.Text);
              //  var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Promotion") == txtPromotion.Text.Trim()).ToList();
                if (_result == null || _result.Rows.Count <= 0)
                {
                    DisplayMessage("Please select the valid promotion", 2);
                    txtCircular.Text = string.Empty;
                    txtCircular.Focus();
                    return;
                }
                ClearGridView();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch_Promotion_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Promotion";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Item";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }
        }
        private void LoadBalance()
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text)) return;
                grdBalance.DataSource = null;
                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), string.Empty);
                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0) 
                {
                    DisplayMessage("No stock balance available", 4);
                   
                    return; 
                }
                //foreach (DataRow _row in _inventoryLocation.Rows)
                //{
                //    List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                //    oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                //    if (oItemStaus != null && oItemStaus.Count > 0)
                //    {
                //        _row["inl_itm_stus"] = oItemStaus.Find(x => x.Mis_cd == _row["inl_itm_stus"].ToString()).Mis_desc;

                //    }
                //}
               
                grdBalance.DataSource = _inventoryLocation;
                grdBalance.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text)) return;

                LoadItemDetail(txtItem.Text.Trim());
                ClearGridView();
                if (chkWithInv.Checked)
                    LoadBalance();
                lbtnGetDetail_Click(null, null);
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCate1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text))
                {
                    DisplayMessage("Please enter price book", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtLevel.Text))
                {
                    DisplayMessage("Please enter level", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtCate1.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCate1.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Please select the valid main category code", 2);                 
                    txtCate1.Text = string.Empty;
                    txtCate1.Focus();
                    return;
                }
                txtCate2.Text = string.Empty;
                txtCate3.Text = string.Empty; 
                ClearGridView();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch_Cat1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text))
                {
                    DisplayMessage("Please enter price book", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtLevel.Text))
                {
                    DisplayMessage("Please enter level", 2);
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Cat1";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCate2_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrEmpty(txtCate2.Text)) return;
                if (string.IsNullOrEmpty(txtCate1.Text))
                {
                    DisplayMessage("Please select the main category first", 2);                 
                    txtCate2.Text = string.Empty;
                    txtCate1.Focus();
                    return; 
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCate2.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Please select the valid category code", 2);
                    txtCate2.Text = string.Empty;
                    txtCate2.Focus();
                    return;
                }
                txtCate3.Text=string.Empty;
                ClearGridView();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch_Cat2_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Cat2";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCate3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCate3.Text)) return;
                if (string.IsNullOrEmpty(txtCate2.Text)) 
                {
                    DisplayMessage("Please select the main category first", 2);                            
                    txtCate3.Text=string.Empty; 
                    txtCate2.Focus(); 
                    return; 
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCate2.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Please select the valid sub category code", 2);                  
                    txtCate2.Text=string.Empty;
                    txtCate2.Focus();
                    return;
                }
                ClearGridView();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch_Cat3_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Cat3";
                UserPopoup.Show();
                txtCate3.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtType.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceType);
                DataTable _result = CHNLSVC.CommonSearch.Get_PriceTypes_SearchData(SearchParams, null, null);
                var _isExist = _result.AsEnumerable().Where(x => x.Field<string>("TypeCode") == txtType.Text.Trim());
                if (_isExist == null || _isExist.Count() <= 0)
                {
                    DisplayMessage("Please select the valid type", 2);                 
                    txtType.Text=string.Empty; 
                    txtType.Focus();
                    return; 
                }
                ClearGridView();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }

        protected void lbtnSearch_Type_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceType);
                DataTable _result = CHNLSVC.CommonSearch.Get_PriceTypes_SearchData(SearchParams, null, null);
                _result.Columns.Remove("Seq");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Type";
                UserPopoup.Show();
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
                if (string.IsNullOrEmpty(txtCustomer.Text)) return;
                MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text, string.Empty, string.Empty, "C");
                if (_masterBusinessCompany == null || string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    DisplayMessage("Please select the valid customer", 2);                 
                    txtCustomer.Text=string.Empty;
                    txtCustomer.Focus(); 
                    return; 
                }
                ClearGridView();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch_Customer_Click(object sender, EventArgs e)
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
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void rdoDateRange_CheckedChanged(object sender, EventArgs e)
        {
            ClearGridView();
        }

        protected void rdoAsAt_CheckedChanged(object sender, EventArgs e)
        {
            ClearGridView();
        }

        protected void chkWithHistory_CheckedChanged(object sender, EventArgs e)
        {
            ClearGridView();
        }

        protected void chkScheme_CheckedChanged(object sender, EventArgs e)
        {
            ClearGridView();
        }

        protected void lbtnSearch_Scheme_Click(object sender, EventArgs e)
        {

            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
                DataTable _result = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "SchemeCD";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtSchemeCD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSchemeCD.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetSchemes("CODE", txtSchemeCD.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please select a valid scheme code", 4);
                    txtSchemeCD.Text = string.Empty;
                    txtSchemeCD.Focus();
                    return;
                }
                ClearGridView();
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }

        }

        protected void chkDiscountCal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               // if (pnlBalance.Visible) pnlBalance.Visible = false;
               // else
               // {
                if (chkDiscountCal.Checked == true)
                {
                    PopupBalance.Show();
                    LoadBalance();
                }
             
               // }
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }
        }
        protected void chkWithInv_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkWithInv.Checked == true) 
                {
                   
                    txtDisRate.Text=string.Empty;
                    txtQty.Text=string.Empty;
                    radNormal.Checked = true;
                    lblDisVal.Text = "0.00";
                    btnprocess.Text = "Value";
                    PopupDiscountCalculator.Show();
                }

                
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }
        }
        protected void lbtnview_Click(object sender, EventArgs e)
        {
            if (grdPriceDetail.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _item = string.Empty;
                Int32 _pbseq = -1;
                Int32 _pblineseq = -1;
                Int32 _type = -1;
                string _IsComStr = string.Empty;

                string _book = string.Empty;
                string _level = string.Empty;
                string _customer = string.Empty;

                _item = (row.FindControl("pr_item") as Label).Text;
                _pbseq =  Convert.ToInt32((row.FindControl("pr_pbseq") as Label).Text);
                _pblineseq = Convert.ToInt32((row.FindControl("pr_pblineseq") as Label).Text);
                _type = Convert.ToInt32((row.FindControl("pr_typeseq") as Label).Text);
                _IsComStr = (row.FindControl("pr_iscom") as Label).Text;
                _book = (row.FindControl("pr_book") as Label).Text;
                _level = (row.FindControl("pr_level") as Label).Text;
                _customer = (row.FindControl("pr_activeFor") as Label).Text;

                //com_serial.Visible = false;

                DateTime _effectDate = new DateTime();
                if (rdoDateRange.Checked)
                {
                    _effectDate = Convert.ToDateTime(txtFromDt.Text);
                }
                if (rdoAsAt.Checked)
                {
                    _effectDate = Convert.ToDateTime(txtAsAtDt.Text);
                }

                Int32 _isCom = 0;

                if (!string.IsNullOrEmpty(_IsComStr))
                {
                   string _isComs =(row.FindControl("pr_iscom") as Label).Text;
                   if (_isComs == "True")
                   {
                       _isCom = 1;
                   }
                   else
                   {
                       _isCom = 0;
                   }
                   // _isCom = Convert.ToInt32((row.FindControl("pr_iscom") as Label).Text);
                   
                }
                
                 


                if (_type != 0)
                {
                   
                    List<PriceCombinedItemRef> _lst = CHNLSVC.Sales.GetPriceCombinedItemLine(_pbseq, _pblineseq, _item, string.Empty);
                    if (_lst == null || _lst.Count <= 0)
                    { 
                       // grdCombine.DataSource = new int; 
                        
                        return; 
                    }
                    grdCombine.DataSource = _lst;
                    grdCombine.DataBind();
                    PopupCombine.Show();
                }
            }
        }

        protected void lbtnstatus_Click(object sender, EventArgs e)
        {
            if (grdPriceDetail.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _item = string.Empty;
                Int32 _type = -1;
                string _book = string.Empty;
                string _level = string.Empty;
                decimal _price = 0;
                _item = (row.FindControl("pr_item") as Label).Text;
                _type =  Convert.ToInt32((row.FindControl("pr_typeseq") as Label).Text);
                _book = (row.FindControl("pr_book") as Label).Text;
                _level = (row.FindControl("pr_level") as Label).Text;
                _price = Convert.ToDecimal((row.FindControl("pr_vatExPrice") as Label).Text);



                DataTable _PriceStatus = CHNLSVC.Sales.GetPriceStatus(_item, _price, Session["UserCompanyCode"].ToString(), _book, _level);
                //gvStatus.AutoGenerateColumns = false;
               // grdStatus.Rows.Clear();

                if (_PriceStatus.Rows.Count > 0)
                {
                    foreach (DataRow _row in _PriceStatus.Rows)
                    {
                        //gvStatus.Rows.Add();
                        //gvStatus["Column22", gvStatus.Rows.Count - 1].Value = _row["MICT_STUS"].ToString();
                        //gvStatus["Column23", gvStatus.Rows.Count - 1].Value = _row["MICT_TAXRATE_CD"].ToString();
                        //gvStatus["Column24", gvStatus.Rows.Count - 1].Value = _row["MICT_TAX_RATE"].ToString();
                        //gvStatus["Column25", gvStatus.Rows.Count - 1].Value = _row["SAPD_WITH_TAX"].ToString();

                        if (Convert.ToInt16(_row["sapl_set_warr"]) == 1)
                        {
                            //gvStatus["Column27", gvStatus.Rows.Count - 1].Value = _row["sapl_warr_period"].ToString();
                        }
                        else
                        {
                           // _row["sapl_warr_period"] = "General Item Warranty";
                          //  gvStatus["Column27", gvStatus.Rows.Count - 1].Value = "General Item Warranty";
                        }

                        List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                        oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            _row["MICT_STUS"] = oItemStaus.Find(x => x.Mis_cd == _row["MICT_STUS"].ToString()).Mis_desc;
                            
                        }
                    }
                }

                //gvStatus.AutoGenerateColumns = false;
               // grdStatus.DataSource = CHNLSVC.Sales.GetPriceStatus(_item, _price, Session["UserCompanyCode"].ToString(), _book, _level);
                //pnlStatus.Visible = true;
               // pnlPc.Visible = false;
               // pnlCombine.Visible = false;
                grdStatus.DataSource = _PriceStatus;
                grdStatus.DataBind();
                PopupStatus.Show();
            }
        }

        protected void lbtnpc_Click(object sender, EventArgs e)
        {
            if (grdPriceDetail.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                IsSuperUser = Convert.ToBoolean(Session["IsSuperUser"]);
                if (!IsSuperUser)
                {
                    DisplayMessage("You don't have permission to see the price allocation.", 2);                 
                    return;
                }
                string _item = string.Empty;
                Int32 _type = -1;
                string _book = string.Empty;
                string _level = string.Empty;
                string _promotion = string.Empty;

                _item = (row.FindControl("pr_item") as Label).Text;
                _type =  Convert.ToInt32((row.FindControl("pr_typeseq") as Label).Text);
                _book = (row.FindControl("pr_book") as Label).Text;
                _level = (row.FindControl("pr_level") as Label).Text;
                _promotion = (row.FindControl("pr_promotioncd") as Label).Text;

         
                grdPc.AutoGenerateColumns = false;
                DataTable _Pc = CHNLSVC.Sales.GetProfitCenterDetail(Session["UserCompanyCode"].ToString(), _type, _book, _level, _promotion);
                grdPc.DataSource = _Pc;
                grdPc.DataBind();
                ViewState["grdPc"] = _Pc;
                Popuppc.Show();
                //pnlPc.Visible = true;
                //pnlStatus.Visible = false;
                //pnlCombine.Visible = false;
            }
        }

        protected void grdPc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPc.PageIndex = e.NewPageIndex;
            DataTable _pc = ViewState["grdPc"] as DataTable;
            grdPc.DataSource = _pc;
            grdPc.DataBind();
            grdPc.PageIndex = 0;
            Popuppc.Show();
        }
        public static Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }
        protected void btnprocess_Click(object sender, EventArgs e)
        {
           // btnprocess.Text = string.Empty;
            lblDisVal.Text = "0.00";
            if (string.IsNullOrEmpty(txtDisRate.Text))
            {            // Summer1021
                DisplayMessage("Please select the discount rate.", 2);
                PopupDiscountCalculator.Show();     
                return;
            }

            if (IsNumeric(txtDisRate.Text) == false)
            {
                DisplayMessage("Please select the valid discount rate", 2);
                PopupDiscountCalculator.Show();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                DisplayMessage("Please select the qty", 2);
                PopupDiscountCalculator.Show();
                return;
            }

            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessage("Please select the valid qty", 2);
                PopupDiscountCalculator.Show();
                return;
            }

            if (grdPriceDetail.Rows.Count <= 0)
            {
                DisplayMessage("Please select the item from the price detail table", 2);
                PopupDiscountCalculator.Show();
                return;
            }
            if (lblcr.Text == "")
            {
                DisplayMessage("Please select the item from the price detail table", 2);
                PopupDiscountCalculator.Show();
                return;
            }
            string _invoiceamount = string.Empty;
            string _discountamount = string.Empty;
            string _item = Session["_item"].ToString();// Convert.ToString(grvPriceDetail.SelectedRows[0].Cells["pr_item"].Value);
            string _status = "GOD";//Convert.ToString(grvPriceDetail.SelectedRows[0].Cells["pr_itemstatus"].Value);
            decimal _unitprice = Convert.ToDecimal(Session["_unitprice"].ToString()); //Convert.ToDecimal(grvPriceDetail.SelectedRows[0].Cells["pr_vatExPrice"].Value);
            decimal _qty = Convert.ToDecimal(txtQty.Text);
            decimal _disAmt = 0;
            decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
            bool _taxInvoice = radTaxInvoice.Checked ? true : radNormal.Checked ? false : true;

            CalculateItem(_item, _status, _unitprice, _qty, _disAmt, _disRate, _taxInvoice, out _invoiceamount, out _discountamount);

            btnprocess.Text = _invoiceamount;
            lblDisVal.Text = _discountamount;
            PopupDiscountCalculator.Show();
        }
        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction, bool _isVAT)
        {
            try
            {

                bool _isVATInvoice = false;
                if (_isVAT) _isVATInvoice = true;
                else _isVATInvoice = false;

                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, string.Empty);
                var _Tax = from _itm in _taxs
                           select _itm;
                foreach (MasterItemTax _one in _Tax)
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
            }
            catch (Exception ex)
            {
            
                DisplayMessage(ex.Message, 4);
               // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            return _pbUnitPrice;
        }
        private void CalculateItem(string _item, string _status, decimal _unitprice, decimal _qty, decimal _disAmount, decimal _disRate, bool _isTaxInvoice, out string _invoiceamount, out string _discountamount)
        {
            decimal _unitAmount = _unitprice * _qty;

            decimal _vatPortion = TaxCalculation(_item, _status, _qty, null, _unitprice, _disAmount, _disRate, true, _isTaxInvoice);
            decimal _taxAmount = _vatPortion;

            decimal _totalAmount = _qty * _unitprice;
            decimal _disAmt = 0;

            if (!string.IsNullOrEmpty(txtDisRate.Text))
            {
                bool _isVATInvoice = false;
                if (_isTaxInvoice) _isVATInvoice = true;
                else _isVATInvoice = false;

                if (_isVATInvoice)
                    _disAmt = _totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100);
                else
                    _disAmt = (_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100);
            }
            _totalAmount = _totalAmount + _taxAmount - _disAmt;

            _invoiceamount = FormatToCurrency(Convert.ToString(_totalAmount));
            _discountamount = FormatToCurrency(Convert.ToString(_disAmt));
        }


        protected void pr_circluer_Click(object sender, EventArgs e)
        {
             if (grdPriceDetail.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            foreach (GridViewRow _row in grdPriceDetail.Rows)
            {
                _row.BackColor = System.Drawing.Color.White;
            }
            if (row != null)
            {
               string _item= (row.FindControl("pr_item") as Label).Text;
               string _unitprice = (row.FindControl("pr_vatExPrice") as Label).Text;
               Session["_item"] = _item;
               lblcr.Text = _item;
               Session["_unitprice"] = _unitprice;
               //row.BackColor = System.Drawing.Color.LightCyan;
               row.BackColor = System.Drawing.Color.LightCyan;
            }
        }

        protected void lbtnSerPrice_Click(object sender, EventArgs e)
        {
            lblMssg.Text = "Do you need to search again?";
            PopupConfBox.Show();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            lbtnGetDetail_Click(sender, null);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnview2_Click(object sender, EventArgs e)
        {
            if (grdPriceDetail.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _item = string.Empty;
                Int32 _pbseq = -1;
                Int32 _pblineseq = -1;
                Int32 _type = -1;
                string _IsComStr = string.Empty;

                string _book = string.Empty;
                string _level = string.Empty;
                string _customer = string.Empty;

                _item = (row.FindControl("prs_item") as Label).Text;
                _pbseq = Convert.ToInt32((row.FindControl("prs_pbseq") as Label).Text);
                _pblineseq = 1;
                _type = Convert.ToInt32((row.FindControl("prs_prtype") as Label).Text);
                _IsComStr = (row.FindControl("prs_iscom") as Label).Text;
                _book = (row.FindControl("prs_book") as Label).Text;
                _level = (row.FindControl("prs_level") as Label).Text;
                _customer = (row.FindControl("prs_customer") as Label).Text;

                //com_serial.Visible = false;

                DateTime _effectDate = new DateTime();
                if (rdoDateRange.Checked)
                {
                    _effectDate = Convert.ToDateTime(txtFromDt.Text);
                }
                if (rdoAsAt.Checked)
                {
                    _effectDate = Convert.ToDateTime(txtAsAtDt.Text);
                }

                Int32 _isCom = 0;

                if (!string.IsNullOrEmpty(_IsComStr))
                {
                 
                    string _isComs = (row.FindControl("prs_iscom") as Label).Text;
                    if (_isComs == "True")
                    {
                        _isCom = 1;
                    }
                    else
                    {
                        _isCom = 0;
                    }
                    // _isCom = Convert.ToInt32((row.FindControl("pr_iscom") as Label).Text);

                }




                if (_type != 0)
                {

                    List<PriceCombinedItemRef> _lst = CHNLSVC.Sales.GetPriceCombinedItemLine(_pbseq, _pblineseq, _item, string.Empty);
                    if (_lst == null || _lst.Count <= 0)
                    {
                        // grdCombine.DataSource = new int; 

                        return;
                    }
                    grdCombine.DataSource = _lst;
                    grdCombine.DataBind();
                    PopupCombine.Show();
                }
            }
        }


        protected void txtFromDt_TextChanged(object sender, EventArgs e)
        {
            // DateTime.ParseExact(txtDeliverDate.Text, "dd MM yyyy", null);// will throw an exception if it fails

            if (Regex.IsMatch(txtFromDt.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
            {

                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Date.');", true);
               
            }
            else
            {
                txtFromDt.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                string _Msg = "Please enter valid date.";
                DisplayMessage(_Msg, 2);
            }
        }
        protected void txtToDt_TextChanged(object sender, EventArgs e)
        {
            // DateTime.ParseExact(txtDeliverDate.Text, "dd MM yyyy", null);// will throw an exception if it fails

            if (Regex.IsMatch(txtToDt.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
            {

                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Date.');", true);

            }
            else
            {
                txtToDt.Text = "31/Dec/2999";
                string _Msg = "Please enter valid date.";
                DisplayMessage(_Msg, 2);
            }
        }
        protected void txtAsAtDt_TextChanged(object sender, EventArgs e)
        {
            // DateTime.ParseExact(txtDeliverDate.Text, "dd MM yyyy", null);// will throw an exception if it fails

            if (Regex.IsMatch(txtAsAtDt.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
            {

                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Date.');", true);

            }
            else
            {
                txtAsAtDt.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                string _Msg = "Please enter valid date.";
                DisplayMessage(_Msg, 2);
            }
        }


        protected void grdPriceDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPriceDetail.PageIndex = e.NewPageIndex;
            List<PriceDetailRef> _pc = ViewState["grdPriceDetail"] as List<PriceDetailRef>;
            grdPriceDetail.DataSource = _pc;
            grdPriceDetail.DataBind();
            grdPriceDetail.PageIndex = 0;
           
        }
        protected void grdPriceSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
             
            grdPriceSerial.PageIndex = e.NewPageIndex;
            List<PriceSerialRef> _ref1 = ViewState["grdPriceSerial"] as List<PriceSerialRef>;
            grdPriceSerial.DataSource = _ref1;
            grdPriceSerial.DataBind();
            grdPriceSerial.PageIndex = 0;

        }
        protected void grdScheme_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            grdScheme.PageIndex = e.NewPageIndex;
            List<HpSchemeDefinition> Final_schemaList = ViewState["grdScheme"] as List<HpSchemeDefinition>;
            grdScheme.DataSource = Final_schemaList;
            grdScheme.DataBind();
            grdScheme.PageIndex = 0;

        }
        protected void grdPromotionDiscount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdPromotionDiscount.PageIndex = e.NewPageIndex;
            List<CashPromotionDiscountDetail> _table = ViewState["grdPromotionDiscount"] as List<CashPromotionDiscountDetail>;
            grdPromotionDiscount.DataSource = _table;
            grdPromotionDiscount.DataBind();
            grdPromotionDiscount.PageIndex = 0;

        }
        protected void grdPayType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdPayType.PageIndex = e.NewPageIndex;
            DataTable _table = ViewState["grdPayType"] as DataTable;
            grdPayType.DataSource = _table;
            grdPayType.DataBind();
            grdPayType.PageIndex = 0;

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
        }

        protected void radTaxInvoice_CheckedChanged(object sender, EventArgs e)
        {
            txtDisRate.Text = string.Empty;
            lblDisVal.Text = "0.00";
            txtQty.Text = string.Empty;
            btnprocess.Text = "Value";
            PopupDiscountCalculator.Show();
        }

        protected void radNormal_CheckedChanged(object sender, EventArgs e)
        {
            txtDisRate.Text = string.Empty;
            lblDisVal.Text = "0.00";
            txtQty.Text = string.Empty;
            btnprocess.Text = "Value";
            PopupDiscountCalculator.Show();
        }
    }
}