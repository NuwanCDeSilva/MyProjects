using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.Sales;
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

namespace FastForward.SCMWeb.View.Transaction.Pricing
{
    public partial class PriceDefinition : BasePage
    {
        string _userid = string.Empty;
        private string _Stype = "";
        private string _Ltype = "";
        private List<PriceDefinitionRef> _defDet = new List<PriceDefinitionRef>();
        private List<PriceDetailRef> _list = new List<PriceDetailRef>();
        private List<PriceCombinedItemRef> _comList = new List<PriceCombinedItemRef>();
        private List<PriceSerialRef> _serial = new List<PriceSerialRef>();
        private List<PriceProfitCenterPromotion> _appPcList = new List<PriceProfitCenterPromotion>();
        private Boolean _isRecal = false;
        Int32 _seqNo = 0;
        private int MainLine = 0;
        private int SubLine = 0;
        private Boolean _isRestrict = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    gvbook.DataSource = new int[] { };
                    gvbook.DataBind();
                    ClearNewPriceCreation();
                    dgvMDef.DataSource = new int[] { };
                    dgvMDef.DataBind();


                    grdPriceDet.DataSource = new int[] { };
                    grdPriceDet.DataBind();

                    dgvComDet.DataSource = new int[] { };
                    dgvComDet.DataBind();

                    PopulateDropDowns();

                    txtcomcode.Text = Session["UserCompanyCode"].ToString();
                    txtcomcodde2.Text = Session["UserCompanyCode"].ToString();

                    if (chkDefDis.Checked == true)
                    {
                        txtDisRate.ReadOnly = true;
                    }
                    else
                    {
                        txtDisRate.ReadOnly = false;
                    }

                    chkSetDefault.Checked = true;
                    if (chkSetDefault.Checked == true)
                    {
                        txtPrefix.ReadOnly = true;
                    }
                    else
                    {
                        txtPrefix.ReadOnly = false;
                    }
                }
                else
                {
                    //lbtnPToDate.Text = Request[lbtnPToDate.UniqueID];
                    //txtPFromDate.Text = Request[txtPFromDate.UniqueID];
                    //if (chkPEndDt.Checked == true)
                    //{
                    //    CalendarExtender1.Enabled = true;
                    //    lbtnPToDate.Enabled = true;
                    //}
                    //else
                    //{
                    //    CalendarExtender1.Enabled = false;
                    //    lbtnPToDate.Enabled = false;
                    //}
                }
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
        protected void lbtnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmplaceord.Value == "Yes")
                {
                    if (string.IsNullOrEmpty(txtcomcode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the company code !!!')", true);
                        lbtncomcode.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtprbuk.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the price book name / code !!!')", true);
                        lbtnpb.Focus();
                        return;
                    }

                    String test_string = txtprbuk.Text.Trim().ToUpper();
                    if (!System.Text.RegularExpressions.Regex.IsMatch(test_string, "^[a-zA-Z0-9\x20]+$"))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Price book name / code should not contain special characters !!!')", true);
                        txtprbuk.Focus();
                        return;
                    }

                    PriceBookRef book = new PriceBookRef();
                    Int32 _effect = 0;
                    _userid = (string)Session["UserID"];

                    Int32 isactive = 1;

                    if (chkactive.Checked == true)
                    {
                        isactive = 1;
                    }
                    else
                    {
                        isactive = 0;
                    }

                    book.Sapb_com = Session["UserCompanyCode"].ToString();
                    book.Sapb_pb = txtprbuk.Text.Trim().ToUpper();
                    book.Sapb_desc = txtpb2.Text.Trim();
                    book.Sapb_hierachy_lvl = 0;
                    book.Sapb_act = Convert.ToBoolean(isactive);
                    book.Sapb_cre_by = _userid;
                    book.Sapb_cre_when = CHNLSVC.Security.GetServerDateTime();
                    book.Sapb_mod_by = _userid;
                    book.Sapb_mod_when = CHNLSVC.Security.GetServerDateTime();

                    _effect = CHNLSVC.Sales.UpdatePriceBook(book);

                    if (_effect != -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Price book saved successfully !!!')", true);
                        ClearPriceDefinition();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
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
        private void DisplayMessage(String Msg, Int32 option)
        {
            string Msgbody = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msgbody + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msgbody + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msgbody + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msgbody + "');", true);
            }
        }
        private void ClearPriceDefinition()
        {
            try
            {
                txtprbuk.Text = string.Empty;
                txtpb2.Text = string.Empty;
                chkactive.Checked = false;
                txtprbuk.ReadOnly = false;
                txtprbuk.ReadOnly = false;

                gvbook.DataSource = new int[] { };
                gvbook.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void ClearPriceLevels()
        {
            try
            {
                txtpb3.Text = string.Empty;
                txtpb4.Text = string.Empty;
                txtpricelevel.Text = string.Empty;
                chlallchecked.Checked = false;
                chkactivlvl.Checked = false;
                txtpricelevel3.Text = string.Empty;
                chkforsales.Checked = false;
                ddlstatus.SelectedIndex = 0;
                chkcheckstus.Checked = false;
                txtcurr.Text = string.Empty;
                chkvat.Checked = false;
                chkwarr.Checked = false;
                txtperiod.Text = string.Empty;
                chkser.Checked = false;
                chktotsales.Checked = false;
                chktransfer.Checked = false;
                chkaging.Checked = false;
                chkbatchwise.Checked = false;
                chklvlactive.Checked = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmclear.Value == "Yes")
                {
                    ClearPriceDefinition();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnsearchpb_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtpricebook = CHNLSVC.Sales.SearchPriceBooks(Session["UserCompanyCode"].ToString());
                gvbook.DataSource = dtpricebook;
                gvbook.DataBind();
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

        protected void gvbook_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvbook, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void gvbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvbook.Rows.Count > 0)
                {
                    string comcode = (gvbook.SelectedRow.FindControl("lblcompany") as Label).Text;
                    string pb = (gvbook.SelectedRow.FindControl("lblcode") as Label).Text;
                    string pbdesc = (gvbook.SelectedRow.FindControl("lbldesc") as Label).Text;
                    string active = (gvbook.SelectedRow.FindControl("lblact") as Label).Text;

                    txtcomcode.Text = comcode;
                    txtprbuk.Text = pb;
                    txtpb2.Text = pbdesc;

                    if (active == "1")
                    {
                        chkactive.Checked = true;
                    }
                    else
                    {
                        chkactive.Checked = false;
                    }

                    txtprbuk.ReadOnly = true;
                }
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

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "10")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
                    DataTable result = CHNLSVC.CommonSearch.SearchPriceBookWeb(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "10";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "69")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "69";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "69a")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "69a";
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

                else if (lblvalue.Text == "10a")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
                    DataTable result = CHNLSVC.CommonSearch.SearchPriceBookWeb(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "10a";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "11")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
                    DataTable result = CHNLSVC.CommonSearch.SearchPriceBookLevelsWeb(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "11";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "58")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "58";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "58a")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "58a";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "61")
                {
                    _Ltype = "Maintain";
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "61";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "61a")
                {
                    _Ltype = "NewCreate";
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "61a";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "75")
                {
                    _Ltype = "Maintain";

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "75";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (lblvalue.Text == "77")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                    DataTable result = CHNLSVC.General.GetSalesTypes(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "77";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "78")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                    DataTable result = CHNLSVC.CommonSearch.GetPromotionSearch(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "78";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "79")
                {
                    string SearchParams = string.Empty;
                    if (chkAppPen.Checked)
                    {
                        SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular);
                    }
                    else
                    {
                        SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                    }
                    DataTable _resultTemp = CHNLSVC.CommonSearch.GetPromotionSearch(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            
                    DataTable _result = _resultTemp.Clone();
                    if (chkAppPen.Checked)
                    {
                        var filter = _resultTemp.Select("STATUS = 'PENDING'");
                        if (filter.Length > 0)
                        {
                            _result = _resultTemp.Select("STATUS = 'PENDING'").CopyToDataTable();
                        }
                        else
                        {
                            grdResult.DataSource = _result;
                            grdResult.DataBind();
                            lblvalue.Text = "79";
                            ViewState["SEARCH"] = _result;
                            SIPopup.Show();
                            txtSearchbyword.Focus();
                            return;
                        }
                    }
                    else
                    {
                        _result.Merge(_resultTemp);
                    }
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "79";
                    ViewState["SEARCH"] = _result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if ((lblvalue.Text == "70") || (lblvalue.Text == "83"))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if ((lblvalue.Text == "71")|| (lblvalue.Text == "84"))
                {
                    _Ltype = "Maintain";

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if ((lblvalue.Text == "75a")|| (lblvalue.Text == "75a"))
                {
                    _Ltype = "Maintain";

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
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
                else if (lblvalue.Text == "82")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "82";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "83")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text); 
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "83";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "Customer")
                {

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                    DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text); 
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(result);
                    lblvalue.Text = "Customer";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
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
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        string pb = string.Empty;
                        if (lblvalue.Text == "10")
                        {
                            pb = txtcomcode.Text.Trim();
                        }
                        else
                        {
                            pb = txtcomcodde2.Text.Trim();
                        }

                        paramsText.Append(pb + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        Int32 AllCheck = 0;
                        Int32 ActiveCheck = 0;
                        string company = Session["UserCompanyCode"].ToString();
                        string pricebook = txtpb3.Text.Trim();

                        if ((chlallchecked.Checked == true) && (chkactivlvl.Checked == true))
                        {
                            AllCheck = 1;
                            ActiveCheck = 1;
                        }

                        else if ((chlallchecked.Checked == false) && (chkactivlvl.Checked == true))
                        {
                            AllCheck = 0;
                            ActiveCheck = 1;
                            company = null;
                        }

                        else if ((chlallchecked.Checked == true) && (chkactivlvl.Checked == false))
                        {
                            AllCheck = 1;
                            ActiveCheck = 0;
                            pricebook = null;
                        }

                        paramsText.Append(AllCheck + seperator + ActiveCheck + seperator + company + seperator + pricebook);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        if (_Stype == "PromoVou")
                        {
                            //paramsText.Append(cmbRdmAllowCompany.SelectedValue + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(string.Empty + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular:
                    {
                        paramsText.Append(string.Empty + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append(string.Empty + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {
                        if (_Ltype == "Maintain")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtMBook.Text.Trim() + seperator);
                        }
                        else if (_Ltype == "NewCreate")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtPPriceBook.Text.Trim() + seperator);
                        }
                        else if (string.IsNullOrEmpty(_Ltype))
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtMBook.Text.Trim() + seperator);
                        }
                        //else if (_Stype == "PromoVou")
                        //{
                        //    paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtPB_pv.Text.Trim() + seperator);
                        //}
                        //else if (_Stype == "PromoVouRedeem")
                        //{
                        //    paramsText.Append(cmbRdmAllowCompany.SelectedValue + seperator + txtRdmComPB.Text.Trim() + seperator);
                        //}

                        //else if (_Stype == "DefMaintain")
                        //{
                        //    paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtDpb.Text.Trim() + seperator);
                        //}
                        //else if (_Stype == "PBDEF1")
                        //{
                        //    paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtNpb.Text.Trim() + seperator);
                        //}
                        //else if (_Stype == "PBDEF2")
                        //{
                        //    paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtBasepb.Text.Trim() + seperator);
                        //}
                        //else
                        //{
                        //    paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtBook.Text.Trim() + seperator);
                        //}
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        if (_Ltype == "Similar")
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtSChannel.Text + seperator + txtSSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        else if (_Ltype == "Maintain")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtmChannel.Text + seperator + txtmSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        else
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        if (_Ltype == "Similar")
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtSChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else if (_Ltype == "Maintain")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtmChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else if (_Ltype == "PromoVou")
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtChnnl_pv.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }

                        else if (_Ltype == "DefMaintain")
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtDchannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
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
                if (lblvalue.Text == "10")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtprbuk.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtpb2.Text = grdResult.SelectedRow.Cells[2].Text;
                    string active = grdResult.SelectedRow.Cells[3].Text;
                    txtcomcode.Text = Session["UserCompanyCode"].ToString();

                    if (active == "Active")
                    {
                        chkactive.Checked = true;
                    }
                    else
                    {
                        chkactive.Checked = false;
                    }
                    txtprbuk.ReadOnly = true;
                }
                else if (lblvalue.Text == "69")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtcomcode.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                else if (lblvalue.Text == "78")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtPPromoCd.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadDetailsByPromoCd();
                }
                else if (lblvalue.Text == "79")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtPcir.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                else if (lblvalue.Text == "69a")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtcomcodde2.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                else if (lblvalue.Text == "14")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtcurr.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                else if (lblvalue.Text == "10a")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtpb3.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtpb4.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                else if (lblvalue.Text == "11")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                    txtpricelevel.Text = grdResult.SelectedRow.Cells[1].Text;

                    DataTable dtpricelevel = CHNLSVC.Sales.SearchPriceBookLevels(Session["UserCompanyCode"].ToString(), txtpricelevel.Text.Trim());

                    foreach (DataRow ddr in dtpricelevel.Rows)
                    {
                        txtcomcodde2.Text = ddr["sapl_com_cd"].ToString();
                        txtpb3.Text = ddr["sapl_pb"].ToString();
                        txtpb4.Text = ddr["sapb_desc"].ToString();
                        txtpricelevel3.Text = ddr["sapl_pb_lvl_desc"].ToString();

                        if (ddr["sapl_is_sales"].ToString() == "1")
                        {
                            chkforsales.Checked = true;
                        }
                        else
                        {
                            chkforsales.Checked = false;
                        }

                        ddlstatus.SelectedValue = ddr["sapl_itm_stuts"].ToString();

                        if (ddr["sapl_chk_st_tp"].ToString() == "1")
                        {
                            chkcheckstus.Checked = true;
                        }
                        else
                        {
                            chkcheckstus.Checked = false;
                        }

                        txtcurr.Text = ddr["sapl_currency_cd"].ToString();

                        if (ddr["sapl_vat_calc"].ToString() == "1")
                        {
                            chkvat.Checked = true;
                        }
                        else
                        {
                            chkvat.Checked = false;
                        }

                        if (ddr["sapl_set_warr"].ToString() == "1")
                        {
                            chkwarr.Checked = true;
                        }
                        else
                        {
                            chkwarr.Checked = false;
                        }

                        txtperiod.Text = ddr["sapl_warr_period"].ToString();

                        if (ddr["sapl_is_serialized"].ToString() == "1")
                        {
                            chkser.Checked = true;
                        }
                        else
                        {
                            chkser.Checked = false;
                        }

                        if (ddr["sapl_base_on_tot_inv_qty"].ToString() == "1")
                        {
                            chktotsales.Checked = true;
                        }
                        else
                        {
                            chktotsales.Checked = false;
                        }

                        if (ddr["sapl_is_transfer"].ToString() == "1")
                        {
                            chktransfer.Checked = true;
                        }
                        else
                        {
                            chktransfer.Checked = false;
                        }

                        if (ddr["sapl_isage"].ToString() == "1")
                        {
                            chkaging.Checked = true;
                        }
                        else
                        {
                            chkaging.Checked = false;
                        }

                        if (ddr["sapl_isbatch_wise"].ToString() == "1")
                        {
                            chkbatchwise.Checked = true;
                        }
                        else
                        {
                            chkbatchwise.Checked = false;
                        }

                        if (ddr["sapl_act"].ToString() == "1")
                        {
                            chklvlactive.Checked = true;
                        }
                        else
                        {
                            chklvlactive.Checked = false;
                        }

                    }
                }
                else if (lblvalue.Text == "58")
                {
                    txtMBook.Text = grdResult.SelectedRow.Cells[1].Text;
                    if (string.IsNullOrEmpty(txtMBook.Text)) return;
                    DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtMBook.Text.Trim());
                    if (_tbl == null || _tbl.Rows.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid price book !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtMBook.Text = string.Empty;
                        lbtnpbsearchpc.Focus();
                    }
                }
                else if (lblvalue.Text == "58a")
                {
                    txtPPriceBook.Text = grdResult.SelectedRow.Cells[1].Text;
                    if (string.IsNullOrEmpty(txtMBook.Text)) return;
                    DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtPPriceBook.Text.Trim());
                    if (_tbl == null || _tbl.Rows.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid price book !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtMBook.Text = string.Empty;
                        btnSearchPPriceBook.Focus();
                    }
                }
                else if (lblvalue.Text == "61")
                {
                    txtMLevel.Text = grdResult.SelectedRow.Cells[1].Text;
                    if (string.IsNullOrEmpty(txtMLevel.Text)) return;
                    if (string.IsNullOrEmpty(txtMBook.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the price book !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtMLevel.Text = string.Empty;
                        lbtnpbsearchpc.Focus();
                        return;
                    }
                    PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtMBook.Text.Trim(), txtMLevel.Text.Trim());
                    if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid price level !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtMLevel.Text = string.Empty;
                        lbtnprlvlpc.Focus();
                        return;
                    }
                }
                else if (lblvalue.Text == "61a")
                {
                    txtPPriceLevel.Text = grdResult.SelectedRow.Cells[1].Text;
                    if (string.IsNullOrEmpty(txtPPriceLevel.Text)) return;
                    if (string.IsNullOrEmpty(txtPPriceBook.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the price book !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtPPriceLevel.Text = string.Empty;
                        btnSearchPPriceLevel.Focus();
                        return;
                    }
                    PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtPPriceBook.Text.Trim(), txtPPriceLevel.Text.Trim());
                    if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid price level !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtPPriceLevel.Text = string.Empty;
                        btnSearchPPriceLevel.Focus();
                        return;
                    }
                    txtPPriceLevel_TextChanged(null, null);
                }

                else if (lblvalue.Text == "75")
                {
                    txtMPc.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                else if (lblvalue.Text == "77")
                {
                    txtMInvType.Text = grdResult.SelectedRow.Cells[1].Text;

                    if (string.IsNullOrEmpty(txtMInvType.Text)) return;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                    DataTable _result = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("srtp_cd") == txtMInvType.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid invoice type !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtMInvType.Text = string.Empty;
                        lbtninv.Focus();
                        return;
                    }
                }

                else if (lblvalue.Text == "70")
                {
                    txtmChannel.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                else if (lblvalue.Text == "71")
                {
                    txtmSubChannel.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                else if (lblvalue.Text == "75a")
                {
                    txtMAppPc.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                else if (lblvalue.Text == "81")
                {
                    txtpItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    MasterItem _itemdetail = new MasterItem();
                    if (!string.IsNullOrEmpty(txtpItem.Text))
                    {
                        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtpItem.Text.ToUpper().Trim());
                    }
                    if (_itemdetail != null)
                    {
                        if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                        {
                            txtpModel.Text = _itemdetail.Mi_model;

                        }
                    }
                }
                else if (lblvalue.Text == "82")
                {
                    txtCItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    mAddCombine.Show();

                }
                else if (lblvalue.Text == "83")
                {
                    txtMChan.Text = grdResult.SelectedRow.Cells[1].Text;
                    mpAddCostItems.Show();
                    SIPopup.Hide() ;

                }
                else if (lblvalue.Text == "84")
                {
                    txtMSubChan.Text = grdResult.SelectedRow.Cells[1].Text;
                    mpAddCostItems.Show();
                    SIPopup.Hide();
                }
                else if (lblvalue.Text == "85")
                {
                    txtMPCenter.Text = grdResult.SelectedRow.Cells[1].Text;
                    mpAddCostItems.Show();
                    SIPopup.Hide();
                }
                else if (lblvalue.Text == "Customer")
                {
                    txtPCus.Text = grdResult.SelectedRow.Cells[1].Text;
                    SIPopup.Hide();
                }
                ViewState["SEARCH"] = null;
            }
            catch
            {

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

        protected void lbtnpb_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                lblvalue.Text = "10";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
                DataTable result = CHNLSVC.CommonSearch.SearchPriceBookWeb(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "10";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtncomcode_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "69";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtncomcode2_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "69a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

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
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnsave2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmplaceord.Value == "Yes")
                {
                    if (string.IsNullOrEmpty(txtcomcodde2.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the company code !!!')", true);
                        lbtncomcode2.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtpb3.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the price book name/code !!!')", true);
                        lbtnfindpb2.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtpricelevel.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter or select the price book level name/code !!!')", true);
                        lbtnpriclvl.Focus();
                        return;
                    }

                    String test_string = txtpricelevel.Text.Trim();
                    if (!System.Text.RegularExpressions.Regex.IsMatch(test_string, "^[a-zA-Z0-9\x20]+$"))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Price book level name/code should not contain special characters !!!')", true);
                        txtpricelevel.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtpricelevel3.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the price book level description !!!')", true);
                        txtpricelevel3.Focus();
                        return;
                    }

                    if (ddlstatus.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select item status !!!')", true);
                        ddlstatus.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtcurr.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the currency code !!!')", true);
                        LinkButton1.Focus();
                        return;
                    }

                    PriceBookLevelRef booklevel = new PriceBookLevelRef();
                    Int32 _effect = 0;
                    _userid = (string)Session["UserID"];

                    if (string.IsNullOrEmpty(txtperiod.Text))
                    {
                        txtperiod.Text = "0";
                    }

                    booklevel.Sapl_pb_lvl_cd = txtpricelevel.Text.Trim();
                    booklevel.Sapl_erp_ref = txtpricelevel.Text.Trim();
                    booklevel.Sapl_pb_lvl_desc = txtpricelevel3.Text.Trim();
                    booklevel.Sapl_act = true;
                    booklevel.Sapl_cre_by = _userid;
                    booklevel.Sapl_cre_when = CHNLSVC.Security.GetServerDateTime();
                    booklevel.Sapl_mod_by = _userid;
                    booklevel.Sapl_mod_when = CHNLSVC.Security.GetServerDateTime();
                    booklevel.Sapl_com_cd = Session["UserCompanyCode"].ToString();
                    booklevel.Sapl_is_def = false;
                    booklevel.Sapl_pb = txtpb3.Text.Trim();
                    booklevel.Sapl_itm_stuts = ddlstatus.SelectedValue;
                    booklevel.Sapl_warr_period = Convert.ToInt32(txtperiod.Text.Trim());
                    booklevel.Sapl_is_serialized = chkser.Checked ? true : false;
                    booklevel.Sapl_currency_cd = txtcurr.Text.Trim();
                    booklevel.Sapl_is_print = false;
                    booklevel.Sapl_set_warr = chkwarr.Checked ? true : false;
                    booklevel.Sapl_vat_calc = chkvat.Checked ? true : false;
                    booklevel.Sapl_chk_st_tp = chkcheckstus.Checked ? true : false;
                    booklevel.Sapl_credit_period = 0;
                    booklevel.Sapl_is_valid = true;
                    booklevel.Sapl_is_transfer = chktransfer.Checked ? true : false;
                    booklevel.Sapl_is_sales = chkforsales.Checked ? true : false;
                    booklevel.Sapl_grn_com = Session["UserCompanyCode"].ToString();
                    booklevel.Sapl_is_without_p = false;
                    booklevel.Sapl_base_on_tot_inv_qty = chktotsales.Checked ? true : false;
                    booklevel.Sapl_is_for_po = false;
                    booklevel.Sapl_isage = chkaging.Checked ? true : false;
                    booklevel.Sapl_isbatch_wise = chkbatchwise.Checked ? true : false;
                    booklevel.Sapl_act = chklvlactive.Checked ? true : false;
                    booklevel.Sapl_model_base = chkModelBase.Checked ? true : false;

                    _effect = CHNLSVC.Sales.UpdatePriceBookLevel(booklevel);

                    if (_effect != -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Price level saved successfully !!!')", true);
                        ClearPriceLevels();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
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

        private void PopulateDropDowns()
        {
            try
            {
                DataTable _CompanyItemStatus = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
                if (_CompanyItemStatus.Rows.Count > 0)
                {
                    ddlstatus.DataSource = _CompanyItemStatus;
                    ddlstatus.DataTextField = "MIS_DESC";
                    ddlstatus.DataValueField = "MIC_CD";
                    ddlstatus.DataBind();
                }
                ddlstatus.Items.Insert(0, new ListItem("Select", "0"));
                ddlstatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnfindpb2_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                lblvalue.Text = "10a";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
                DataTable result = CHNLSVC.CommonSearch.SearchPriceBookWeb(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "10a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtnpriclvl_Click(object sender, EventArgs e)
        {
            try
            {
                if ((chlallchecked.Checked == false) && (chkactivlvl.Checked == false))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select ALL to search all price books in the company or ACTIVE to search levels of selected price book !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    chlallchecked.Focus();
                    return;
                }

                if (chkactivlvl.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtpb3.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter price book name !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtpb3.Focus();
                        return;
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
                DataTable result = CHNLSVC.CommonSearch.SearchPriceBookLevelsWeb(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "11";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtnclear2_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                ClearPriceLevels();
            }
        }

        protected void lbtnpbsearchpc_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "58";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtnprlvlpc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMBook.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the price book !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtMBook.Text = string.Empty;
                    lbtnpbsearchpc.Focus();
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                _Stype = "Maintain";

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "61";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtnpc_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                _Ltype = "Maintain";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "75";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtninv_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable result = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "77";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtnfindchannel_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "70";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtnsubchannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtmChannel.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select channel !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtmChannel.Text = string.Empty;
                    lbtnsubchannel.Focus();
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                _Ltype = "Maintain";

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "71";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void lbtnpcfind_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                _Ltype = "Maintain";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "75a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void btnload_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMBook.Text) && string.IsNullOrEmpty(txtMLevel.Text) && string.IsNullOrEmpty(txtMPc.Text) && string.IsNullOrEmpty(txtMInvType.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter one of above selection categories !!!')", true);
                    txtMBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMBook.Text) && !string.IsNullOrEmpty(txtMLevel.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the price book !!!')", true);
                    txtMBook.Focus();
                    return;
                }

                _defDet = new List<PriceDefinitionRef>();

                _defDet = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(Session["UserCompanyCode"].ToString(), txtMBook.Text, txtMLevel.Text, txtMInvType.Text, txtMPc.Text);

                dgvMDef.AutoGenerateColumns = false;
                dgvMDef.DataSource = new List<PriceDefinitionRef>();
                dgvMDef.DataSource = _defDet;
                dgvMDef.DataBind();

                btnMSave.Enabled = false;
                btnMSave.OnClientClick = "return Enable();";
                btnMSave.CssClass = "buttoncolor";
                Session["DEFTEP"] = _defDet;
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

        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(Session["UserCompanyCode"].ToString(), txtmChannel.Text, txtmSubChannel.Text, null, null, null, txtMAppPc.Text);
                foreach (DataRow drow in dt.Rows)
                {
                    chklstbox.Items.Add(drow["PROFIT_CENTER"].ToString());
                }

                //txtmChannel.Text = "";
                //txtmSubChannel.Text = "";
                //txtMAppPc.Text = "";
                txtMAppPc.Focus();
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem li in chklstbox.Items)
                {
                    li.Selected = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem li in chklstbox.Items)
                {
                    li.Selected = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                chklstbox.Items.Clear();
                txtmChannel.Text = string.Empty;
                txtmSubChannel.Text = string.Empty;
                txtMAppPc.Text = string.Empty;
                txtmChannel.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnapply_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMBook.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter applicable price book !!!')", true);
                    txtMBook.Text = "";
                    txtMBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMLevel.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter applicable price level !!!')", true);
                    txtMLevel.Text = "";
                    txtMLevel.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMInvType.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter applicable invoice type !!!')", true);
                    txtMInvType.Text = "";
                    txtMInvType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDisRate.Text.Trim()))
                {
                    txtDisRate.Text = "0";
                }

                if (chkDefDis.Checked == true)
                {
                    //if (string.IsNullOrEmpty(txtDisRate.Text))
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Defalut discount rate is missing !!!')", true);
                    //    txtDisRate.Text = "";
                    //    txtDisRate.Focus();
                    //    return;
                    //}

                    //if (Convert.ToDecimal(txtDisRate.Text) > 100 || Convert.ToDecimal(txtDisRate.Text) < 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Rate should be between 0 to 100 !!!')", true);
                    //    txtDisRate.Text = "";
                    //    txtDisRate.Focus();
                    //    return;
                    //}
                }

                if (chkSetDefault.Checked == false)
                {
                    if (string.IsNullOrEmpty(txtPrefix.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter invoice prefix !!!')", true);
                        txtPrefix.Text = "";
                        txtPrefix.Focus();
                        return;
                    }
                }

                Boolean _isValidpc = false;

                foreach (ListItem Item in chklstbox.Items)
                {
                    string _item = Item.Text;

                    if (Item.Selected == true)
                    {
                        _isValidpc = true;
                        goto L3;
                    }
                }
            L3:

                if (_isValidpc == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No any applicable profit center is selected !!!')", true);
                    return;
                }

                PriceDefinitionRef _tmpList = new PriceDefinitionRef();
                string _invPreTp = "";
                foreach (ListItem mAppPcList in chklstbox.Items)
                {
                    string itm = mAppPcList.Text;

                    if (mAppPcList.Selected == true)
                    {
                        _tmpList = new PriceDefinitionRef();
                        _invPreTp = "";
                        _tmpList.Sadd_chk_credit_bal = chkChkCredit.Checked;
                        _tmpList.Sadd_com = Session["UserCompanyCode"].ToString();
                        _tmpList.Sadd_cre_by = Session["UserID"].ToString();
                        _tmpList.Sadd_cre_when = DateTime.Today.Date;
                        _tmpList.Sadd_def = false;
                        _tmpList.Sadd_def_stus = null;
                       // _tmpList.Sadd_disc_rt = Convert.ToDecimal(txtDisRate.Text);
                        _tmpList.Sadd_disc_rt = 0;
                        _tmpList.Sadd_doc_tp = txtMInvType.Text;
                        _tmpList.Sadd_is_bank_ex_rt = true;
                        _tmpList.Sadd_is_disc = chkDefDis.Checked;
                        _tmpList.Sadd_mod_by = Session["UserID"].ToString();
                        _tmpList.Sadd_mod_when = DateTime.Today.Date;
                        _tmpList.Sadd_p_lvl = txtMLevel.Text;
                        _tmpList.Sadd_pb = txtMBook.Text;
                        _tmpList.Sadd_pc = itm;

                        DataTable _type = new DataTable();
                        _type = CHNLSVC.Sales.GetDefInvPrefix(Session["UserCompanyCode"].ToString(), txtMInvType.Text.Trim());

                        if (_type.Rows.Count > 1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Multyple prefixes are loading !!!')", true);
                            return;
                        }

                        if (_type.Rows.Count > 0)
                        {
                            _invPreTp = Convert.ToString(_type.Rows[0]["SDP_PFIX"]);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Default company prefixes are not define for this company !!!')", true);
                            return;
                        }

                        if (chkSetDefault.Checked == true)
                        {
                            if (txtMInvType.Text.Trim() == "HS")
                            {
                                _tmpList.Sadd_prefix = _invPreTp;
                            }
                            else
                            {
                                _tmpList.Sadd_prefix = itm + "-" + _invPreTp;
                            }
                        }
                        else
                        {
                            _tmpList.Sadd_prefix = txtPrefix.Text.Trim();
                        }

                        var list = new List<PriceDefinitionRef>();
                        list = (List<PriceDefinitionRef>)Session["DEFTEP"];

                        if (list != null)
                        {
                            _defDet = list;
                        }

                        var _filter = _defDet.Where(x => x.Sadd_com == Session["UserCompanyCode"].ToString() && x.Sadd_pc == itm && x.Sadd_doc_tp == txtMInvType.Text && x.Sadd_pb == txtMBook.Text && x.Sadd_p_lvl == txtMLevel.Text).ToList();

                        if (_filter.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Records are duplicating !!!')", true);
                            chkDefDis.Checked = false;
                            txtDisRate.Text = string.Empty;
                            chkChkCredit.Checked = false;
                            txtPrefix.Text = string.Empty;
                            chkSetDefault.Checked = false;
                            return;
                        }
                        else
                        {
                            _defDet.Add(_tmpList);
                        }
                    }
                }


                //var query = (from PriceDefinitionRef t in _defDet
                //             group t by new {t.Sadd_com,t.Sadd_pc,t.Sadd_doc_tp, t.Sadd_pb, t.Sadd_p_lvl,t.Sadd_is_disc,t.Sadd_disc_rt,t.Sadd_chk_credit_bal,t.Sadd_prefix}
                //                 into grp
                //                 select new
                //                 {
                //                     grp.Key.Sadd_com,
                //                     grp.Key.Sadd_pc,
                //                     grp.Key.Sadd_doc_tp,
                //                     grp.Key.Sadd_pb,
                //                     grp.Key.Sadd_p_lvl,
                //                     grp.Key.Sadd_is_disc,
                //                     grp.Key.Sadd_disc_rt,
                //                     grp.Key.Sadd_chk_credit_bal,
                //                     grp.Key.Sadd_prefix
                //                 }).ToList();

                dgvMDef.AutoGenerateColumns = false;
                dgvMDef.DataSource = new List<PriceDefinitionRef>();
                dgvMDef.DataSource = _defDet;
                dgvMDef.DataBind();

                Session["DEFTEP"] = _defDet;

                txtMBook.Text = "";
                txtMLevel.Text = "";
                txtMPc.Text = "";
                txtMInvType.Text = "";
                txtmChannel.Text = "";
                txtmSubChannel.Text = "";
                txtMAppPc.Text = "";
                chklstbox.Items.Clear();
                chkDefDis.Checked = false;
                txtDisRate.Text = string.Empty;
                txtDisRate.Enabled = true;
                chkChkCredit.Checked = false;
                txtPrefix.Text = "";
                chkSetDefault.Checked = false;
                txtPrefix.ReadOnly = false;
                txtDisRate.ReadOnly = false;

                btnMSave.Enabled = true;
                btnMSave.CssClass = "buttonUndocolor";
                btnMSave.OnClientClick = "ConfirmPlaceOrder();";
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

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtApprovalconformmessageValue.Value == "No")
                {
                    return;
                }
                DataTable dt = CHNLSVC.Sales.GetPromoCodesByCir(txtPcir.Text.ToUpper(), txtPPromoCd.Text, txtPPriceBook.Text.ToUpper(), txtPPriceLevel.Text.ToUpper());
                if (dt.Rows.Count > 0)
                {
                    Session["d"] = "";
                    string error = "";
                    List<string> _errList = new List<string>();
                    List<string> _promoCodes = new List<string>();
                    if (chkDetList.Items.Count <= 0)
                    {
                        DisplayMessage("Please add promotions from list", 2);
                        return;
                    }
                    foreach (ListItem item in chkDetList.Items)
                    {
                        if (item.Selected)
                            _promoCodes.Add(item.Text);
                    }
                    if (_promoCodes.Count <= 0)
                    {
                        DisplayMessage("Please select promotions from list", 2);                  
                        return;
                    }
                    if (lblPAStatus.Text != "Approval Pending")
                    {
                        DisplayMessage("Please valied cricular from list", 2);    
                        return;
                    }

                   // if (MessageBox.Show("Do you want to approve this promotions", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                   //     return;

                    int result = CHNLSVC.Sales.ProcessPromotionApprove(_promoCodes, out  error, out  _errList, Session["UserID"].ToString(), Session["SessionID"].ToString());
                    if (result > 0)
                    {
                        DisplayMessage("Approved Successfully", 3);
                        ClearNewPriceCreation();
                    }
                    else
                    {
                        DisplayMessage(error, 4);
                    }
                       
            

                }
                else
                {
                    DisplayMessage("Please select valid promotion circular ", 2);
                  
                    return;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
           
            }

        private void Clear_Maintaince()
        {
            _defDet = new List<PriceDefinitionRef>();
            _Stype = "";
            _Ltype = "";

            chkSetDefault.Checked = true;
            chkDefDis.Checked = false;
            txtDisRate.Enabled = true;
            txtDisRate.Text = "0";
            chkChkCredit.Checked = false;
            txtmChannel.Text = "";
            txtmSubChannel.Text = "";
            txtMPc.Text = "";
            chklstbox.Items.Clear();
            btnMSave.Enabled = true;

            txtMBook.Text = "";
            txtMLevel.Text = "";
            txtMInvType.Text = "";
            txtMPc.Text = "";

            dgvMDef.AutoGenerateColumns = false;
            dgvMDef.DataSource = new List<PriceDefinitionRef>();
            dgvMDef.DataBind();

            Session["DEFTEP"] = null;
            txtPrefix.Text = string.Empty;
            txtDisRate.ReadOnly = false;
            txtDisRate.Text = string.Empty;
            txtPrefix.Text = string.Empty;
            txtPrefix.ReadOnly = false;
            chkSetDefault.Checked = true;

            btnMSave.Enabled = true;
            btnMSave.CssClass = "buttonUndocolor";
            btnMSave.OnClientClick = "ConfirmPlaceOrder();";
        }
        private void SavePrice()
        {
            try
            {
                _list = ViewState["PriceItem"] as List<PriceDetailRef>;
               _comList = ViewState["PriceItemCombine"] as List<PriceCombinedItemRef>;
               _appPcList = ViewState["_appPcList"] as List<PriceProfitCenterPromotion>;
                Int32 row_aff = 0;
                if (_list == null)
                {
                    _list =new List<PriceDetailRef>();
                }
                if (_comList == null)
                {
                    _comList = new List<PriceCombinedItemRef>();
                }
                    string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtPPriceBook.Text))
                {
                    DisplayMessage("Please select Price book.", 2);
                    txtPPriceBook.Text = "";
                    txtPPriceBook.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPPriceLevel.Text))
                {
                    DisplayMessage("Please select Price level.", 2);
                    txtPPriceLevel.Text = "";
                    txtPPriceLevel.Focus();
                    return;
                }

                if (Convert.ToDateTime(txtPToDate.Text).Date < Convert.ToDateTime(txtPFromDate.Text).Date)
                {
                    DisplayMessage("To date cannot be less than from date.", 2);                 
                    return;
                }
                if (string.IsNullOrEmpty(txtPcir.Text))
                {
                    DisplayMessage("Please enter circular #.", 2);
                    txtPcir.Text = "";
                    txtPcir.Focus();
                    return;
                }
                List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(ddlPType.SelectedItem.Text);
                foreach (PriceTypeRef _tmp in _type)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        if (chkWithOutCombine.Checked == true)
                        {
                            if (dgvComDet.Rows.Count <= 0)
                            {
                                //if (MessageBox.Show("Confirm to continue without combine / free items ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                //{
                                //    return;
                                //}
                            }
                            else
                            {
                                DisplayMessage("Cannot continue, you select without combine but combine items are available.", 2);

                                return;
                            }

                        }
                        else
                        {
                            if (dgvComDet.Rows.Count <= 0)
                            {
                                DisplayMessage("No combine / free items are define.", 2);
                                return;
                            }
                        }
                    
                        
                    }
                    //check permission
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11048))
                    {
                        //chek for pb and plevel
                        if (!string.IsNullOrEmpty(txtPPriceLevel.Text))
                        {
                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, txtPPriceBook.Text.ToUpper().Trim(), txtPPriceLevel.Text.Trim(), 2);
                            if (_resList == null || _resList.Count <= 0)
                            {
                                string _Msg = "User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + txtPPriceBook.Text + " and Price Level - " + txtPPriceLevel.Text;
                                DisplayMessage(_Msg, 1);
                                txtPPriceLevel.Text = "";
                                txtPPriceBook.Text = "";
                                txtPPriceBook.Focus();
                                return;
                            }
                        }
                    }
                    List<PriceDetailRef> _saveDetail = new List<PriceDetailRef>();
                    List<PriceCombinedItemRef> _saveComDet = new List<PriceCombinedItemRef>();
                    foreach (PriceDetailRef _tmpPriceDet in _list)
                    {
                        PriceDetailRef _tmpPriceList = new PriceDetailRef();
                        _tmpPriceList = _tmpPriceDet;
                        _tmpPriceList.Sapd_customer_cd = txtPCus.Text;
                        _tmpPriceList.Sapd_from_date = Convert.ToDateTime(txtPFromDate.Text).Date;
                        _tmpPriceList.Sapd_to_date = Convert.ToDateTime(txtPToDate.Text).Date;
                        _tmpPriceList.Sapd_circular_no = txtPcir.Text.ToUpper();
                        _tmpPriceList.Sapd_pb_tp_cd = txtPPriceBook.Text.ToUpper().Trim();
                        _tmpPriceList.Sapd_pbk_lvl_cd = txtPPriceLevel.Text.ToUpper().Trim();
                        _tmpPriceList.Sapd_price_type = Convert.ToInt16(ddlPType.SelectedValue);
                        _tmpPriceList.Sapd_erp_ref =txtPPriceLevel.Text;
                        if (chkWithOutCombine.Checked == true)
                        {
                            _tmpPriceList.Sapd_usr_ip = "IGNORE COMBINE";
                        }
                        else
                        {

                        }
                        if (_tmpPriceList.Sapd_price_stus != "S")
                        {
                            _tmpPriceList.Sapd_price_stus = "P";
                        }
                        
                        _saveDetail.Add(_tmpPriceList);
                    }

                    foreach (PriceCombinedItemRef _tmpComList in _comList)
                    {
                        _tmpComList.Sapc_increse = chkPMulti.Checked;
                        _saveComDet.Add(_tmpComList);
                    }
                    MasterAutoNumber masterAuto = new MasterAutoNumber();

                    if (ddlPType.SelectedItem.Text != "NORMAL")
                    {
                        masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                        masterAuto.Aut_cate_tp = "COM";
                        masterAuto.Aut_direction = null;
                        masterAuto.Aut_modify_dt = null;
                        masterAuto.Aut_moduleid = "PRICE";
                        masterAuto.Aut_number = 5;//what is Aut_number
                        masterAuto.Aut_start_char = "PRO";
                        masterAuto.Aut_year = null;
                    }
                    else
                    {
                        masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                        masterAuto.Aut_cate_tp = "COM";
                        masterAuto.Aut_direction = null;
                        masterAuto.Aut_modify_dt = null;
                        masterAuto.Aut_moduleid = "PRICE";
                        masterAuto.Aut_number = 5;//what is Aut_number
                        masterAuto.Aut_start_char = "NOR";
                        masterAuto.Aut_year = null;
                    }

                    List<PriceProfitCenterPromotion> _savePcAllocList = new List<PriceProfitCenterPromotion>();

                    //_savePcAllocList = _appPcList;
                    foreach (GridViewRow row in grdAppPC.Rows)
                    {
                        Int32 _activeva =0;
                        //string _pc = row["col_a_Pc"].Value.ToString();
                        //string _promo = row.Cells["col_a_Promo"].Value.ToString();
                        //Int16 _active = Convert.ToInt16(row.Cells["col_a_Act"].Value);
                        //Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);
                        string _pc = ((Label)row.FindControl("col_a_Pc")).Text;
                        string _promo = ((Label)row.FindControl("col_a_Promo")).Text;
                        CheckBox _active = ((CheckBox)row.FindControl("col_a_Act"));
                        if (_active.Checked == true)
                        {
                            _activeva = 1;
                        }
                        Int32 _pbSeq = Convert.ToInt32(((Label)row.FindControl("col_a_pbSeq")).Text);

                        if (_activeva == 1)
                        {
                            foreach (PriceProfitCenterPromotion _tmp1 in _appPcList)
                            {
                                if (_tmp1.Srpr_com == Session["UserCompanyCode"].ToString() && _tmp1.Srpr_pbseq == _pbSeq && _tmp1.Srpr_pc == _pc && _tmp1.Srpr_promo_cd == _promo)
                                {
                                    //PriceProfitCenterPromotion _tmpList = new PriceProfitCenterPromotion();
                                    //_tmpList.Srpr_act = _active;
                                    //_tmpList.Srpr_com = Session["UserCompanyCode"].ToString();
                                    //_tmpList.Srpr_cre_by = Session["UserID"].ToString();
                                    //_tmpList.Srpr_mod_by = Session["UserID"].ToString();
                                    //_tmpList.Srpr_pbseq = _pbSeq;
                                    //_tmpList.Srpr_pc = _pc;
                                    //_tmpList.Srpr_promo_cd = _promo;
                                    _tmp1.Srpr_act = _activeva;
                                    _savePcAllocList.Add(_tmp1);
                                }

                            }
                        }
                    }
                        

                    
                    PriceDetailRestriction _priceRes = new PriceDetailRestriction();
                    _isRestrict = Convert.ToBoolean(Session["_isRestrict"].ToString());
                    // _priceRes = null;
                    if (_isRestrict == true)
                    {
                        _priceRes.Spr_com = Session["UserCompanyCode"].ToString();
                        _priceRes.Spr_msg = txtRMessage.Text;
                        _priceRes.Spr_need_cus = chkNeedCus.Checked;
                        _priceRes.Spr_need_nic = chkNeedNIC.Checked;
                        _priceRes.Spr_need_pp = false;
                        _priceRes.Spr_need_dl = false;
                        _priceRes.Spr_usr = Session["UserID"].ToString();
                        _priceRes.Spr_when = DateTime.Now;
                        _priceRes.Spr_promo = "";
                    }
                    else
                    {
                        _priceRes = null;
                    }

                    string _err = "";
                    row_aff = (Int32)CHNLSVC.Sales.SavePriceDetails(_saveDetail, _comList, masterAuto, _savePcAllocList, _serial, _priceRes, out _err, Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(),null,0,"");

                    if (row_aff == 1)
                    {
                        DisplayMessage("Price definition created successfully.", 3);
                        ClearNewPriceCreation();

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_err))
                        {
                            DisplayMessage(_err, 4);
                           
                        }
                        else
                        {
                            DisplayMessage("Faild to update.", 4);
                           
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnMSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmplaceord.Value == "Yes")
                {

                    Int32 row_aff = 0;
                    string _msg = string.Empty;

                    var list = (List<PriceDefinitionRef>)Session["DEFTEP"];
                    _defDet = list;

                    if (_defDet == null || _defDet.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot find defined details !!!')", true);
                        return;
                    }

                    row_aff = (Int32)CHNLSVC.Sales.SavePcPriceDefinition(_defDet);

                    if (row_aff == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Price level permission activated !!!')", true);
                        Clear_Maintaince();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _msg + "')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Failed to update !!!')", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
          
        }

        protected void btnPCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmplaceord.Value == "Yes")
                {
                    SavePrice();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }

        }

        protected void chkSetDefault_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSetDefault.Checked == true)
                {
                    txtPrefix.ReadOnly = true;
                }
                else
                {
                    txtPrefix.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void chkDefDis_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDefDis.Checked == true)
                {
                    txtDisRate.ReadOnly = true;
                }
                else
                {
                    txtDisRate.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtngrdInvoiceDetailsDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "Yes")
                {
                    if (dgvMDef.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Load price details before deleting !!!')", true);
                        btnload.Focus();
                        return;
                    }

                    string _msg = string.Empty;
                    var list = new List<PriceDefinitionRef>();
                    list = (List<PriceDefinitionRef>)Session["DEFTEP"];

                    if (list != null)
                    {
                        _defDet = list;
                    }

                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;

                    if (row != null)
                    {

                        string _pc = (row.FindControl("lblpc") as Label).Text;
                        string _invtp = (row.FindControl("lblinvtype") as Label).Text;
                        string _lvl = (row.FindControl("lbllevel") as Label).Text;
                        string _com = (row.FindControl("lblcompany") as Label).Text;
                        string _pb = (row.FindControl("lblprbook") as Label).Text;

                        List<PriceDefinitionRef> _temp = new List<PriceDefinitionRef>();
                        _temp = _defDet;

                        _temp.RemoveAll(x => x.Sadd_com == _com && x.Sadd_pc == _pc && x.Sadd_doc_tp == _invtp && x.Sadd_pb == _pb && x.Sadd_p_lvl == _lvl);
                        _defDet = _temp;

                        dgvMDef.AutoGenerateColumns = false;
                        dgvMDef.DataSource = new List<PriceDefinitionRef>();
                        dgvMDef.DataSource = _defDet;
                        dgvMDef.DataBind();

                        Int32 row_aff = (Int32)CHNLSVC.Sales.RemovePriceAccess(_pc, _invtp, _lvl, _com, _pb, Session["UserID"].ToString());

                        if (row_aff == 1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Price level permission removed successfully !!!')", true);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_msg))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _msg + "')", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            }
                        }
                    }
                }
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

        protected void txtPPriceBook_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPPriceBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(Session["UserCompanyCode"].ToString(), txtPPriceBook.Text.ToUpper().Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid price book !!!')", true);
                    txtPPriceBook.Text = string.Empty;
                    txtPPriceBook.Focus();
                    return;
                }

                //ADDED BY SACHITH
                //ON 2014/02/06
                //CHECK USER PB,PLEVEL
                else
                {
                    /*
                     * Check user has 11048 permission
                     * if have get user pb and plevel acoording to date 
                     * if no data found provide error 
                     * 
                     */
                    //check permission
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11048))
                    {
                        //chek for pb and plevel
                        if (!string.IsNullOrEmpty(txtPPriceLevel.Text))
                        {
                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, txtPPriceBook.Text.Trim(), txtPPriceLevel.Text.Trim(), 2);
                            if (_resList == null || _resList.Count <= 0)
                            {
                                //MessageBox.Show("User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + txtBook.Text + " and Price Level - " + txtLevel.Text, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('User - " + Session["UserID"].ToString() + " ' not allowed to use price book - ' " + txtPPriceBook.Text + " ' and price level - ' " + txtPPriceLevel.Text + "')", true);
                                txtPPriceLevel.Text = string.Empty;
                                txtPPriceBook.Text = string.Empty;
                                txtPPriceBook.Focus();
                                return;
                            }
                        }
                        //chek for pb only
                        if (string.IsNullOrEmpty(txtPPriceLevel.Text))
                        {
                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, txtPPriceBook.Text.Trim(), "", 1);
                            if (_resList == null || _resList.Count <= 0)
                            {
                                //MessageBox.Show("User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + txtBook.Text, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('User - ' " + Session["UserID"].ToString() + " ' not allowed to use price book - ' " + txtPPriceBook.Text + ")", true);
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('User - " + Session["UserID"].ToString() + " does not allowed to use price book " + txtPPriceBook.Text + "')", true);
                                txtPPriceBook.Text = string.Empty;
                                txtPPriceBook.Focus();
                                return;
                            }
                        }
                    }
                }
                txtPPriceLevel.Focus();
            }
            catch (Exception err)
            {
                // Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing' " + err.Message + ")", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtPPriceLevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPPriceLevel.Text)) return;
                if (string.IsNullOrEmpty(txtPPriceBook.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the price book.')", true);
                    txtPPriceLevel.Text = "";
                    txtPPriceBook.Focus();
                    return;
                }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), txtPPriceBook.Text.ToUpper().Trim(), txtPPriceLevel.Text.ToUpper().Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid price level.')", true);
                    txtPPriceLevel.Text = "";
                    txtPPriceLevel.Focus();
                    return;
                }

                //ADDED BY SACHITH
                //ON 2014/02/06
                //CHECK USER PB,PLEVEL
                else
                {
                    /*
                     * Check user has 11048 permission
                     * if have get user pb and plevel acoording to date 
                     * if no data found provide error 
                     * 
                     */
                    //check permission
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11048))
                    {
                        //chek for pb and plevel
                        if (!string.IsNullOrEmpty(txtPPriceBook.Text))
                        {
                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, txtPPriceBook.Text.Trim(), txtPPriceLevel.Text.Trim(), 2);
                            if (_resList == null || _resList.Count <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('User - " + Session["UserID"].ToString() + " not allowed to use price book - " + txtPPriceBook.Text + " and price level - " + txtPPriceLevel.Text + "')", true);
                                //MessageBox.Show("User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + txtBook.Text + " and Price Level - " + txtLevel.Text, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPPriceLevel.Text = "";
                                txtPPriceBook.Text = "";
                                txtPPriceBook.Focus();
                                return;
                            }
                        }
                        //chek for plevel only
                        else
                        {
                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, "", txtPPriceLevel.Text.Trim(), 3);
                            if (_resList == null || _resList.Count <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('User - " + Session["UserID"].ToString() + " not allowed to use price level -  " + txtPPriceLevel.Text + "')", true);
                                //MessageBox.Show("User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + txtBook.Text, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPPriceLevel.Text = "";
                                txtPPriceLevel.Focus();
                                return;
                            }
                        }
                    }

                    if (_tbl.Sapl_isbatch_wise == true)
                    {
                        //  btnBatch.Enabled = true;
                    }
                    else
                    {
                        //  btnBatch.Enabled = false;
                    }

                    if (_tbl.Sapl_model_base == true)
                    {
                        txtpItem.Enabled = false;
                        txtpModel.Enabled = true;
                    }
                    else
                    {
                        txtpItem.Enabled = true;
                        txtpModel.Enabled = false;
                    }
                }
                txtPFromDate.Focus();
            }
            catch (Exception err)
            {
                //   Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing' " + err.Message + ")", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnSearchPPriceLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPPriceBook.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the price book !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtPPriceBook.Text = string.Empty;
                    btnSearchPPriceLevel.Focus();
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                _Ltype = "NewCreate";

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "61a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void chkPEndDt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPEndDt.Checked == true)
                {
                    CalendarExtender1.Enabled = true;
                    lbtnPToDate.Enabled = true;
                }
                else
                {
                    CalendarExtender1.Enabled = false;
                    lbtnPToDate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnSearchPPriceBook_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "58a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void btnPClear_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                //try
                //{
                //    Response.Redirect(Request.RawUrl, false);
                //}
                //catch (Exception ex)
                //{
                //    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                //}
                ClearNewPriceCreation();
            }
        }

        protected void ClearNewPriceCreation()
        {
            Session["P_1"] = false;
            btnuppdateAp.Enabled = false;
            pnlmulti.Visible = false;
            Session["_isRestrict"] = "false";
            Session["_isRecal"] = "false";
            _list = new List<PriceDetailRef>();
            _comList = new List<PriceCombinedItemRef>();
            ViewState["PriceItem"] = null;
            ViewState["PriceItemCombine"] = null;
            ViewState["_list"] = null;
            txtPPriceBook.Text = string.Empty;
            txtPPriceLevel.Text = string.Empty;
            DateTime Date = DateTime.Now;
            txtPFromDate.Text = Date.ToString("dd/MMM/yyyy"); //Convert.ToDateTime(DateTime.Now).Date.ToShortDateString();
            txtPToDate.Text = "31/Dec/2999";
            txtPcir.Text = string.Empty;
            txtPPromoCd.Text = string.Empty;
            chkDetList.Items.Clear();
            ddlPType.Enabled = true;
            BindPriceCategory();
            BindPriceType();
            grdPriceDet.DataSource = new int[] { };
            grdPriceDet.DataBind();
            grdAppPC.DataSource = new int[] { };
            grdAppPC.DataBind();
            dgvComDet.DataSource = new int[] { };
            dgvComDet.DataBind();
            _seqNo = 0;
            txtpItem.Text = "";
            txtpModel.Text = "";
            txtpQtyFrom.Text = "";
           // txtPQtyTo.Text = "9999";
            txtPWaraRmk.Text = "";
            chkpInactive.Checked = false;
            txtpTimes.Text = "9999";
            //btnAddCom.Enabled = false;
            btnAppPC.Enabled = false;
            MainLine = 0;
            SubLine = 0;
            chkPEndDt.Text = "";
            txtPCus.Text = string.Empty;
            txtPWaraRmk.Text = "N/A";
            lblPAStatus.Text = string.Empty;
            chkPMulti.Checked = false;
            chkAppPen.Checked = false;
            ddlPCat.SelectedIndex = -1;
            ddlPType.SelectedIndex = -1;
            chkWithOutCombine.Checked = false;
            chkComPromo.Checked = false;
            chkNeedCus.Checked = false;
            chkNeedNIC.Checked = false;
            chkNeedMob.Checked = false;
            txtRMessage.Text = string.Empty;
            txtMChan.Text = string.Empty;
            txtMSubChan.Text = string.Empty;
            txtMPCenter.Text = string.Empty;
            chkMList.Items.Clear();
            txtCMainItem.Text = string.Empty;
            txtCMainPrice.Text = string.Empty;
            txtCItem.Text = string.Empty;
            txtCQty.Text = string.Empty;
            txtCPrice.Text = string.Empty;
            txtCMainLine.Text = string.Empty;
            txtCPBSeq.Text = string.Empty;
            pnlcombin.Visible = true;
            pnlpro.Visible = true;

            btnPCreate.Enabled = true;
            btnPCreate.OnClientClick = "ConfirmPlaceOrder();";
            btnPCreate.CssClass = "buttonUndocolor";

        }


        protected void btnHiddenClear_Click(object sender, EventArgs e)
        {
            ClearNewPriceCreation();
        }

        protected void lbtnPPromoCd_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable result = CHNLSVC.CommonSearch.GetPromotionSearch(SearchParams, null, null);
              
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "78";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
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

        protected void LoadDetailsByPromoCd()
        {
            if (!string.IsNullOrEmpty(txtPPromoCd.Text))
            {
                //btnMainSave.Enabled = true;


                //dgvPriceDet.AutoGenerateColumns = false;
                //dgvPriceDet.DataSource = new List<PriceDetailRef>();

                //dgvPromo.AutoGenerateColumns = false;
                //dgvPromo.DataSource = new List<PriceCombinedItemRef>();

                //dgvAppPC.AutoGenerateColumns = false;
                //dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();

                _list = new List<PriceDetailRef>();
                // _appPcList = new List<PriceProfitCenterPromotion>();

                //  List<PriceCombinedItemRef> _tmpComList = new List<PriceCombinedItemRef>();
                //  _comList = new List<PriceCombinedItemRef>();

                _list = CHNLSVC.Sales.GetPriceByPromoCD(txtPPromoCd.Text.Trim());

                if (_list == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid promotion code !!!')", true);
                    return;
                }


                foreach (PriceDetailRef _tmp in _list)
                {
                    txtPPriceBook.Text = _tmp.Sapd_pb_tp_cd;
                    txtPPriceLevel.Text = _tmp.Sapd_pbk_lvl_cd;
                    txtPFromDate.Text = _tmp.Sapd_from_date.Date.ToShortDateString();
                    txtPToDate.Text = _tmp.Sapd_to_date.Date.ToShortDateString();
                    txtPPromoCd.Text = _tmp.Sapd_promo_cd;
                    ddlPType.SelectedIndex = _tmp.Sapd_price_type;
                    //   ddlPType.SelectedValue = _tmp.Sapd_price_type;
                    txtPcir.Text = _tmp.Sapd_circular_no;
                    txtPCus.Text = _tmp.Sapd_customer_cd;

                    //_tmpComList = new List<PriceCombinedItemRef>();
                    //_tmpComList = CHNLSVC.Sales.GetPriceCombinedItemLine(_tmp.Sapd_pb_seq, _tmp.Sapd_seq_no, _tmp.Sapd_itm_cd, null);
                    //_comList.AddRange(_tmpComList);



                    //List<PriceProfitCenterPromotion> _tmpAppPC = new List<PriceProfitCenterPromotion>();
                    //_tmpAppPC = CHNLSVC.Sales.GetAllocPromoPc(Session["UserCompanyCode"].ToString(), txtPromoCode.Text.Trim(), _tmp.Sapd_pb_seq);
                    //_appPcList.AddRange(_tmpAppPC);

                }

                //dgvPriceDet.AutoGenerateColumns = false;
                //dgvPriceDet.DataSource = new List<PriceDetailRef>();
                //dgvPriceDet.DataSource = _list;

                grdPriceDet.DataSource = null;
                grdPriceDet.DataSource = new List<PriceDetailRef>();
                grdPriceDet.DataSource = _list;
                grdPriceDet.DataBind();
                ViewState["PriceItem"] = _list;
                //dgvPromo.AutoGenerateColumns = false;
                //dgvPromo.DataSource = new List<PriceCombinedItemRef>();
                //dgvPromo.DataSource = _comList;

                //dgvAppPC.AutoGenerateColumns = false;
                //dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                //dgvAppPC.DataSource = _appPcList;

                //if (_appPcList != null)
                //{
                //    foreach (PriceProfitCenterPromotion _chk in _appPcList)
                //    {
                //        foreach (DataGridViewRow row in dgvAppPC.Rows)
                //        {
                //            string _pc = row.Cells["col_a_Pc"].Value.ToString();
                //            string _promo = row.Cells["col_a_Promo"].Value.ToString();
                //            Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);

                //            if (_pc == _chk.Srpr_pc && _promo == _chk.Srpr_promo_cd && _pbSeq == _chk.Srpr_pbseq)
                //            {
                //                DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                //                //if (Convert.ToBoolean(chk.Value) == false)
                //                if (_chk.Srpr_act == 1)
                //                {
                //                    chk.Value = true;
                //                    goto L1;
                //                }
                //                else
                //                {
                //                    chk.Value = false;
                //                    goto L1;
                //                }
                //            }

                //        }
                //    L1: Int16 x = 1;
                //    }
                //}
                //_isRecal = true;
                //btnAppPCUpdate.Enabled = true;
                //btnMainSave.Enabled = false;
                //btnSaveAs.Enabled = true;
            }
        }

        protected void txtPPromoCd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadDetailsByPromoCd();
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void BindPriceCategory()
        {
            ddlPCat.Items.Clear();
            List<PriceCategoryRef> _list = CHNLSVC.Sales.GetAllPriceCategory(string.Empty);
            _list.Add(new PriceCategoryRef { Sarpc_cd = "" });

            ddlPCat.DataSource = _list;
            ddlPCat.DataTextField = "Sarpc_cd";
            ddlPCat.DataValueField = "Sarpc_cd";
            ddlPCat.DataBind();

        }


        protected void BindPriceType()
        {
            ddlPType.Items.Clear();
            List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            _list.Add(new PriceTypeRef { Sarpt_cd = "", Sarpt_indi = -1 });
            ddlPType.DataSource = _list;
            ddlPType.DataTextField = "Sarpt_cd";
            ddlPType.DataValueField = "Sarpt_indi";
            ddlPType.DataBind();

        }

        protected void lbtnPCir_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = string.Empty;

                if (chkAppPen.Checked)
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular);
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                }
                DataTable _resultTemp = CHNLSVC.CommonSearch.GetPromotionSearch(SearchParams, null, null);
                DataTable _result = _resultTemp.Clone();
                if (chkAppPen.Checked)
                {
                    _result = _resultTemp.Select("STATUS = 'PENDING'").CopyToDataTable();
                }
                else
                {
                    _result.Merge(_resultTemp);
                }
               
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "79";
                BindUCtrlDDLData(_result);
                ViewState["SEARCH"] = _result;
                SIPopup.Show();
                txtSearchbyword.Focus();

                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 0;
                //if (chkAppPendings.Checked)
                //{
                //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular);
                //}
                //else
                //{
                //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                //}

                //DataTable _resultTemp = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);

                //DataTable _result = _resultTemp.Clone();
                //if (chkAppPendings.Checked)
                //{
                //    _result = _resultTemp.Select("STATUS = 'P'").CopyToDataTable();
                //}
                //else
                //{
                //    _result.Merge(_resultTemp);
                //}

                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.dvResult.Columns["STATUS"].Visible = false;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.obj_TragetTextBox = txtCircular;
                //_CommonSearch.ShowDialog();
                //txtCircular.Select();
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnLoadDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPcir.Text))
                {
                    //MessageBox.Show("Please enter circluar #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter circluar #.');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtPcir.Text = "";
                    _isRecal = false;
                    btnPCreate.Enabled = true;
                    //btnAmend.Enabled = false;
                    //btnCancel.Enabled = false;
                    txtPcir.Focus();
                    return;
                }

                chkDetList.Items.Clear();
                DataTable dt = CHNLSVC.Sales.GetPromoCodesByCir(txtPcir.Text.ToUpper(), txtPPromoCd.Text, txtPPriceBook.Text.ToUpper(), txtPPriceLevel.Text.ToUpper());

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        chkDetList.Items.Add(drow["sapd_promo_cd"].ToString());
                    }

                    _isRecal = true;
                    //btnCancel.Enabled = true;
                    ddlPType.Enabled = false;
                    chkPEndDt.Text = "";
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid circluar #.');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtPcir.Text = "";
                    _isRecal = false;
                    // btnMainSave.Enabled = true;
                    //  btnAmend.Enabled = false;
                    // btnCancel.Enabled = false;
                    //  txtCircular.Focus();
                    return;
                }

            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        protected void txtpItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MasterItem _itemdetail = new MasterItem();

                if (string.IsNullOrEmpty(txtPPriceBook.Text))
                {
                    txtpItem.Text = "";
                    txtpModel.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select valid price book.');", true);
                    txtPPriceBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPPriceLevel.Text))
                {
                    txtpItem.Text = "";
                    txtpModel.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select valid price level.');", true);
                    txtPPriceLevel.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtpItem.Text))
                {
                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtpItem.Text.ToUpper().Trim());
                }
                if (_itemdetail != null)
                {
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        txtpModel.Text = _itemdetail.Mi_model;
                        txtpQtyFrom.Focus();
                    }
                }
                else
                {
                    txtpItem.Text = "";
                    txtpModel.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Invalid item code.');", true);
                }
            }
            catch (Exception ex)
            {

                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnAddPrice_Click(object sender, EventArgs e)
        {
            try
            {
                string _priceType = "A";
                PriceDetailRef _newPriceDet = new PriceDetailRef();
                _list = ViewState["PriceItem"] as List<PriceDetailRef>;

                if (ViewState["PriceItem"] == null)
                {
                    _list = new List<PriceDetailRef>();
                    
                }

                if (string.IsNullOrEmpty(txtpItem.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Enter valid item.');", true);
                    txtpItem.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtpQtyFrom.Text))
                {
                    DisplayMessage("Please enter valid from qty.", 2);
                    //MessageBox.Show("Please enter valid from qty.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtpQtyFrom.Text = "";
                    txtpQtyFrom.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPQtyTo.Text))
                {
                    DisplayMessage("Please enter valid to qty.", 2);
                    txtPQtyTo.Text = "";
                    txtPQtyTo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtpPrice.Text))
                {
                    DisplayMessage("Please enter valid to item price.", 2);
                    txtpPrice.Text = "";
                    txtpPrice.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPWaraRmk.Text))
                {
                    txtPWaraRmk.Text = "N/A";
                }
                if (string.IsNullOrEmpty(txtpTimes.Text))
                {
                    DisplayMessage("Please enter no of valid times.", 2);                  
                    txtpTimes.Text = "";
                    txtpTimes.Focus();
                    return;
                }
                if (chkpInactive.Checked == true)
                {
                    if (Convert.ToDecimal(txtpPrice.Text) > 0)
                    {
                        DisplayMessage("Inactive price should be zero.", 2);

                        txtpPrice.Text = "";
                        txtpPrice.Focus();
                        return;
                    }
                    _priceType = "S";
                }
                if (chkpInactive.Checked == true)
                {
                    _priceType = "S";
                }


                

                _seqNo = _seqNo + 1;
                _newPriceDet.Sapd_warr_remarks = txtPWaraRmk.Text.Trim();
                _newPriceDet.Sapd_upload_dt = DateTime.Now;
                _newPriceDet.Sapd_update_dt = DateTime.Now;
                _newPriceDet.Sapd_to_date = Convert.ToDateTime(txtPToDate.Text).Date;
                _newPriceDet.Sapd_session_id = Session["SessionID"].ToString();
                _newPriceDet.Sapd_seq_no = _seqNo;
                _newPriceDet.Sapd_qty_to = Convert.ToDecimal(txtPQtyTo.Text);
                _newPriceDet.Sapd_qty_from = Convert.ToDecimal(txtpQtyFrom.Text);
                _newPriceDet.Sapd_price_type = Convert.ToInt16(ddlPType.SelectedValue);
                _newPriceDet.Sapd_price_stus = _priceType;
                _newPriceDet.Sapd_pbk_lvl_cd = txtPPriceLevel.Text.ToUpper().Trim();
                _newPriceDet.Sapd_pb_tp_cd = txtPPriceBook.Text.ToUpper().Trim();
                _newPriceDet.Sapd_pb_seq = _seqNo;
                _newPriceDet.Sapd_no_of_use_times = 0;
                _newPriceDet.Sapd_no_of_times = Convert.ToInt16(txtpTimes.Text);
                _newPriceDet.Sapd_model = txtpModel.Text;
                _newPriceDet.Sapd_mod_when = DateTime.Now;
                _newPriceDet.Sapd_mod_by = Session["UserID"].ToString();
                _newPriceDet.Sapd_margin = 0;
                _newPriceDet.Sapd_lst_cost = 0;
                _newPriceDet.Sapd_itm_price = Convert.ToDecimal(txtpPrice.Text);
                _newPriceDet.Sapd_itm_cd = txtpItem.Text.ToUpper().Trim();
                _newPriceDet.Sapd_is_cancel = chkpInactive.Checked;
                _newPriceDet.Sapd_is_allow_individual = false;
                _newPriceDet.Sapd_from_date = Convert.ToDateTime(txtPFromDate.Text).Date;
                _newPriceDet.Sapd_erp_ref = txtPPriceLevel.Text.Trim();
                _newPriceDet.Sapd_dp_ex_cost = 0;
                _newPriceDet.Sapd_day_attempt = 0;
                _newPriceDet.Sapd_customer_cd = txtPCus.Text.Trim();
                _newPriceDet.Sapd_cre_when = DateTime.Now;
                _newPriceDet.Sapd_cre_by = Session["UserID"].ToString();
                _newPriceDet.Sapd_circular_no = txtPcir.Text.ToUpper().Trim();
                _newPriceDet.Sapd_cancel_dt = DateTime.MinValue;
                _newPriceDet.Sapd_avg_cost = 0;
                _newPriceDet.Sapd_apply_on = "0";
                //if (chkEndDate.Checked && btnAmend.Enabled)
                //    _newPriceDet.Sapd_ser_upload = 6;

                List<mst_itm_com_reorder> _ltsCost = new List<mst_itm_com_reorder>();
                _ltsCost = CHNLSVC.General.GetReOrder(txtpItem.Text.ToUpper().Trim());
                if (_ltsCost == null)
                {
                    _ltsCost = new List<mst_itm_com_reorder>();
                }
                foreach(mst_itm_com_reorder _temp in _ltsCost)
                {
                    if (_temp.Icr_com_code == GlbUserComCode)
                    {
                        _newPriceDet.Sapd_lst_cost = _temp.Icr_max_cost;
                        _newPriceDet.Sapd_avg_cost = _temp.Icr_avg_cost;

                        if (_newPriceDet.Sapd_avg_cost > 0)
                        {
                            _newPriceDet.Sapd_margin = (_newPriceDet.Sapd_itm_price - _newPriceDet.Sapd_avg_cost) / _newPriceDet.Sapd_avg_cost * 100;
                        }
                        else
                        {
                            _newPriceDet.Sapd_margin = 100;
                        }
                    }
                }

                _list.Add(_newPriceDet);

                grdPriceDet.DataSource = null;
                grdPriceDet.DataSource = new List<PriceDetailRef>();
                grdPriceDet.DataSource = _list;
                grdPriceDet.DataBind();

                ViewState["PriceItem"] = _list;

                txtpItem.Text = "";
                txtpModel.Text = "";
                txtpQtyFrom.Text = "";
                txtPQtyTo.Text = "9999";
                txtPWaraRmk.Text = "N/A";
                chkpInactive.Checked = false;
                txtpTimes.Text = "9999";
                txtpPrice.Text = "";

            }
            catch (Exception ex)
            {

                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        protected void ddlPType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(ddlPType.SelectedItem.ToString());
                foreach (PriceTypeRef _tmp in _list)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        //btnAddCom.Enabled = true;
                        chkPMulti.Checked = false;
                        chkPMulti.Visible = true;
                        //chkCombine.Enabled = true;
                        //chkCombine.Checked = false;
                        //chkWithOutCombine.Visible = true;
                    }
                    else
                    {
                        //btnAddCom.Enabled = false;
                        chkPMulti.Checked = false;
                        chkPMulti.Visible = false;
                        //  chkCombine.Checked = false;
                        //   chkCombine.Enabled = false;
                        //  chkWithOutCombine.Visible = false;
                    }

                    if (_tmp.Sarpt_indi == 0)
                    {
                        btnAppPC.Enabled = false;
                    }
                    else
                    {
                        btnAppPC.Enabled = true;
                    }

                }
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (ListItem Item in chkDetList.Items)
                {
                    Item.Selected = true;
                }
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lblUnSelect_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (ListItem Item in chkDetList.Items)
                {
                    Item.Selected = false;
                }
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnAppPC_Click(object sender, EventArgs e)
        {
            mpAddCostItems.Show();
        }

        protected void btnAddRest_Click(object sender, EventArgs e)
        {
            PopupRes.Show();
        }

        protected void btnAddCom_Click(object sender, EventArgs e)
        {
            // mAddCombine.Show();
        }

        protected void lbtnAddCombine_Click(object sender, EventArgs e)
        {
            try
            {
                string _mainItm = string.Empty;
                Int32 _mainLine = 0;
                decimal _mainPrice = 0;
                Int32 _pbSeq = 0;

                List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(ddlPType.SelectedItem.ToString());
                foreach (PriceTypeRef _tmp in _list)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        mAddCombine.Show();
                        if (grdPriceDet.Rows.Count == 0) return;

                        var lb = (LinkButton)sender;
                        var row = (GridViewRow)lb.NamingContainer;
                        if (row != null)
                        {

                            _mainLine = 1;//Convert.ToInt32((row.FindControl("col_seq") as Label).Text);
                            _mainItm = (row.FindControl("sapd_itm_cd") as Label).Text;
                            _pbSeq = 1;// (row.FindControl("col_Status") as Label).Text;
                            _mainPrice = Convert.ToDecimal((row.FindControl("sapd_itm_price") as Label).Text);


                            txtCMainItem.Text = _mainItm.ToString();
                            txtCMainPrice.Text = _mainPrice.ToString();
                            txtCMainLine.Text = _mainLine.ToString();
                            txtCPBSeq.Text = _pbSeq.ToString();
                            //lblDelStatus.Text = _status.ToString();

                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Not allow to add combine items for selected price type.');", true);
                        return;
                    }
                }
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void lbtnCItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "82";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnAddComDet_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _subSeq = 0;
                _comList = ViewState["PriceItemCombine"] as List<PriceCombinedItemRef>;

                if (ViewState["PriceItemCombine"] == null)
                {
                    _comList = new List<PriceCombinedItemRef>();
                }

                if (string.IsNullOrEmpty(txtCMainItem.Text))
                {
                    //MessageBox.Show("Please select main item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select main item.');", true);
                    mAddCombine.Show();
                    return;
                }

                //if (string.IsNullOrEmpty(lblSubModel.Text))
                //{
                //    MessageBox.Show("Please select main item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                if (string.IsNullOrEmpty(txtCMainLine.Text))
                {
                    //MessageBox.Show("Please select main item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select main item.');", true);
                    mAddCombine.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtCItem.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select sub item.');", true);
                    txtCItem.Text = "";
                    txtCItem.Focus();
                    mAddCombine.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtCQty.Text))
                {
                    //MessageBox.Show("Please enter item qty.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter item qty.');", true);
                    txtCQty.Text = "";
                    txtCQty.Focus();
                    mAddCombine.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtCPrice.Text))
                {
                    //MessageBox.Show("Please enter combine item price.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter combine price.');", true);
                    txtCPrice.Text = "";
                    txtCPrice.Focus();
                    mAddCombine.Show();
                    return;
                }

                if (_comList.Count > 0)
                {
                    var _tmp = (from _lst in _comList
                                where _lst.Sapc_main_itm_cd == txtCMainItem.Text.Trim() && _lst.Sapc_main_line == Convert.ToInt16(txtCMainLine.Text.Trim())
                                select (int?)_lst.Sapc_itm_line).Max();

                    _subSeq = Convert.ToInt32(_tmp);
                }

                if (MainLine <= 0 && SubLine <= 0)
                {
                    _subSeq = _subSeq + 1;

                    PriceCombinedItemRef _comItmRef = new PriceCombinedItemRef();
                    _comItmRef.Sapc_increse = false;
                    _comItmRef.Sapc_itm_cd = txtCItem.Text.Trim();
                    _comItmRef.Sapc_itm_line = _subSeq;
                    _comItmRef.Sapc_main_itm_cd = txtCMainItem.Text.Trim();
                    _comItmRef.Sapc_main_line = Convert.ToInt32(txtCMainLine.Text);
                    _comItmRef.Sapc_main_ser = null;
                    _comItmRef.Sapc_pb_seq = Convert.ToInt32(txtCPBSeq.Text);
                    _comItmRef.Sapc_price = Convert.ToDecimal(txtCPrice.Text);
                    _comItmRef.Sapc_qty = Convert.ToDecimal(txtCQty.Text);
                    _comItmRef.Sapc_sub_ser = null;
                    _comItmRef.Sapc_tot_com = false;
                    //if (chkEndDate.Checked && btnAmend.Enabled)
                    //{
                    //    _comItmRef.Sapc_sub_ser = "6";
                    //}
                    _comList.Add(_comItmRef);
                }
                //else
                {
                    //edit 
                    (from _res in _comList
                     where _res.Sapc_main_line == MainLine && _res.Sapc_itm_line == SubLine
                     select _res).ToList<PriceCombinedItemRef>().ForEach(x =>
                     {
                         x.Sapc_qty = Convert.ToDecimal(txtCQty.Text);
                         x.Sapc_itm_cd = txtCItem.Text;
                         x.Sapc_price = Convert.ToDecimal(txtCPrice.Text);
                         x.Sapc_sub_ser = "5";
                     });
                }

                //dgvPromo.AutoGenerateColumns = false;
                //BindingSource _source = new BindingSource();
                //_source.DataSource = _comList;
                //dgvPromo.DataSource = new List<PriceCombinedItemRef>();
                //dgvPromo.DataSource = _source;

                dgvComDet.DataSource = null;
                dgvComDet.DataSource = new List<PriceCombinedItemRef>();
                dgvComDet.DataSource = _comList;
                dgvComDet.DataBind();

                ViewState["PriceItemCombine"] = _comList;

                txtCItem.Text = "";
                txtCQty.Text = "";
                txtCPrice.Text = "";
                //lblSubModel.Text = "";
                //txtSubItem.Focus();
                mAddCombine.Show();
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnResClear_Click(object sender, EventArgs e)
        {
            txtRMessage.Text = "";
            chkNeedCus.Checked = false;
            chkNeedMob.Checked = false;
            chkNeedNIC.Checked = false;
            
        }

        protected void lbtnView_Click(object sender, EventArgs e)
        {
            try
            {
               // btnPCreate.Enabled = true;
                btnPCreate.Enabled = true;
                btnPCreate.OnClientClick = "ConfirmPlaceOrder();";
                btnPCreate.CssClass = "buttonUndocolor";

                Boolean _isValidItm = false;
                List<string> _item = new List<string>();

                grdPriceDet.AutoGenerateColumns = false;
                grdPriceDet.DataSource = new List<PriceDetailRef>();
                grdPriceDet.DataBind();

                dgvComDet.AutoGenerateColumns = false;
                dgvComDet.DataSource = new List<PriceCombinedItemRef>();
                dgvComDet.DataBind();

                grdAppPC.AutoGenerateColumns = false;
                grdAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                grdAppPC.DataBind();
                foreach (ListItem Item in chkDetList.Items)
                {


                    if (Item.Selected == true)
                    {
                        _item.Add(Item.Text);
                        _isValidItm = true;

                    }
                }
                Int16 I = 0;

                if (_isValidItm == false)
                {
                    DisplayMessage("Please select promotion code to get details.", 2);                
                    return;
                }
                _list = new List<PriceDetailRef>();
                _appPcList = new List<PriceProfitCenterPromotion>();
                List<PriceCombinedItemRef> _tmpComList = new List<PriceCombinedItemRef>();
                foreach (string st in _item)
                {
                    _list.AddRange(CHNLSVC.Sales.GetPricebyCirandPromo(txtPcir.Text.ToUpper(), st));
                }
                foreach (PriceDetailRef _tmp in _list)
                {
                    txtPPriceBook.Text = _tmp.Sapd_pb_tp_cd;
                    txtPPriceLevel.Text = _tmp.Sapd_pbk_lvl_cd;
                    txtPFromDate.Text = _tmp.Sapd_from_date.Date.ToString("dd/MMM/yyyy");
                    txtPToDate.Text = _tmp.Sapd_to_date.Date.ToString("dd/MMM/yyyy");
                    txtPPromoCd.Text = _tmp.Sapd_promo_cd;
                    ddlPType.SelectedValue = _tmp.Sapd_price_type.ToString();
                    txtPCus.Text = _tmp.Sapd_customer_cd;
                    if (_tmp.Sapd_usr_ip == "IGNORE COMBINE")
                    {
                        chkWithOutCombine.Checked = true;
                    }
                    else
                    {
                        chkWithOutCombine.Checked = false;
                    }
                    //cmbPriceType.Enabled = false;

                    _tmpComList = new List<PriceCombinedItemRef>();
                    _tmpComList = CHNLSVC.Sales.GetPriceCombinedItemLine(_tmp.Sapd_pb_seq, _tmp.Sapd_seq_no, _tmp.Sapd_itm_cd, null);
                    _comList.AddRange(_tmpComList);


                    List<PriceProfitCenterPromotion> _tmpAppPC = new List<PriceProfitCenterPromotion>();
                    _tmpAppPC.AddRange(CHNLSVC.Sales.GetAllocPromoPc(Session["UserCompanyCode"].ToString(), _tmp.Sapd_promo_cd, _tmp.Sapd_pb_seq));
                    _appPcList.AddRange(_tmpAppPC);

                    if (_tmp.Sapd_price_stus == "A")
                    {
                        lblPAStatus.Text = "Active";
                    }
                    else if (_tmp.Sapd_price_stus == "P")
                    {
                        lblPAStatus.Text = "Approval Pending";
                    }
                    else if (_tmp.Sapd_price_stus == "S")
                    {
                        lblPAStatus.Text = "Suspended";
                    }
                    else if (_tmp.Sapd_price_stus == "C")
                    {
                        lblPAStatus.Text = "Cancel";
                    }
                    grdPriceDet.AutoGenerateColumns = false;
                    grdPriceDet.DataSource = new List<PriceDetailRef>();
                    grdPriceDet.DataSource = _list;
                    grdPriceDet.DataBind();
                    ViewState["PriceItem"] = _list;
                    dgvComDet.AutoGenerateColumns = false;
                    dgvComDet.DataSource = new List<PriceCombinedItemRef>();
                    dgvComDet.DataSource = _comList;
                    dgvComDet.DataBind();
                    ViewState["PriceItemCombine"] = _comList;
                    grdAppPC.AutoGenerateColumns = false;
                    grdAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                    grdAppPC.DataSource = _appPcList;
                    grdAppPC.DataBind();
                    ViewState["_appPcList"] = _appPcList;
                    if (_appPcList != null)
                    {
                       
                        foreach (PriceProfitCenterPromotion _chk in _appPcList)
                        {
                            foreach (GridViewRow row in grdAppPC.Rows)
                            {

                                string _pc = ((Label)row.FindControl("col_a_Pc")).Text;
                                string _promo = ((Label)row.FindControl("col_a_Promo")).Text;                               
                                Int32 _pbSeq = Convert.ToInt32(((Label)row.FindControl("col_a_pbSeq")).Text);


                                if (_pc == _chk.Srpr_pc && _promo == _chk.Srpr_promo_cd && _pbSeq == _chk.Srpr_pbseq)
                                {
                                    Int16 chk = Convert.ToInt16(((CheckBox)row.FindControl("col_a_Act")).Checked);
                                   // DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                                    //if (Convert.ToBoolean(chk.Value) == false)
                                    if (_chk.Srpr_act == 1)
                                    {
                                        //chk.Value = true;
                                        chk = 1;
                                        goto L1;
                                    }
                                    else
                                    {
                                        //chk.Value = false;
                                        chk = 0;
                                        goto L1;
                                    }
                                }

                            }
                        L1: Int16 x = 1;
                        }
                    }

                    //give used message
                    foreach (PriceDetailRef _ref in _list)
                    {
                        if (_ref.Sapd_no_of_use_times > 0)
                        {
                            string _Msg = "Promotion Code-" + _ref.Sapd_promo_cd + " has used " + _ref.Sapd_no_of_use_times;
                            DisplayMessage(_Msg, 2);                               
                        }
                    }

                    PriceDetailRestriction _tmpRes = new PriceDetailRestriction();
                    foreach (PriceDetailRef _tmp1 in _list)
                    {
                        _tmpRes = CHNLSVC.Sales.GetPromotionRestriction(Session["UserCompanyCode"].ToString(), _tmp1.Sapd_promo_cd);
                    }

                    chkNeedCus.Checked = false;
                    chkNeedNIC.Checked = false;
                    chkNeedMob.Checked = false;
                    txtRMessage.Text = "";
                    //chkPP.Checked = false;
                    //chkDL.Checked = false;
                    if (_tmpRes != null)
                    {
                        chkNeedCus.Checked = _tmpRes.Spr_need_cus;
                        chkNeedNIC.Checked = _tmpRes.Spr_need_nic;
                        chkNeedMob.Checked = _tmpRes.Spr_need_mob;
                        txtRMessage.Text = _tmpRes.Spr_msg;
                        //chkPP.Checked = _tmpRes.Spr_need_pp;
                        //chkDL.Checked = _tmpRes.Spr_need_dl;
                    }

                    _isRecal = true;
                    Session["_isRecal"] = "true";
                   // lbtncancel.Enabled = true;
                    //lbtncancel.Enabled = true;
                    //lbtncancel.OnClientClick = "UpdateConfirm();";
                    //lbtncancel.CssClass = "buttonUndocolor";

                    //btnPApprove.Enabled = true;
                    btnuppdateAp.Enabled = true;
                    btnuppdateAp.OnClientClick = "UpdateConfirm();";
                    btnuppdateAp.CssClass = "buttonUndocolor";

                    //btnPCreate.Enabled = false;
                    btnPCreate.Enabled = false;
                    btnPCreate.OnClientClick = "return Enable();";
                    btnPCreate.CssClass = "buttoncolor";

                    //lbtnsaveas.Enabled = true;

                    //lbtnsaveas.Enabled = true;
                    //lbtnsaveas.OnClientClick = "UpdateConfirm();";
                    //lbtnsaveas.CssClass = "buttonUndocolor";
                    ddlPType_SelectedIndexChanged(null, null);
                }

            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 2);
            }
        }


        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            lblalert.Visible = false;
            lblsuccess.Visible = false;

            if (string.IsNullOrEmpty(txtPcir.Text))
            {
                DisplayMessage("Please enter circular #.",2);
                txtPcir.Text = "";
                txtPcir.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlPCat.SelectedItem.Text))
            {
                DisplayMessage("Please enter circular #.", 2);         
                return;
            }

            if (string.IsNullOrEmpty(ddlPType.SelectedItem.Text))
            {
                DisplayMessage("Please select price type.", 2);                        
                return;
            }

            if (chkWithOutCombine.Checked == true)
            {
                if (chkComPromo.Checked == true)
                {
                    DisplayMessage("You cannot select both combine and without combine.", 2);            
                  
                    return;
                }
            }

            if (chkComPromo.Checked == true)
            {
                List<PriceTypeRef> _Typelist = CHNLSVC.Sales.GetAllPriceType(ddlPType.SelectedItem.Text);
                foreach (PriceTypeRef _tmp in _Typelist)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        // btnCombineItems.Enabled = true;
                    }
                    else
                    {
                        DisplayMessage("Please check selected price type.", 2);      
                     
                        return;
                    }
                }

            }
            else
            {
                List<PriceTypeRef> _Typelist = CHNLSVC.Sales.GetAllPriceType(ddlPType.SelectedItem.Text);
                foreach (PriceTypeRef _tmp in _Typelist)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        if (chkWithOutCombine.Checked == true)
                        {
                            lblMssg.Text = "Confirm to continue without combine / free items ?";
                            PopupConfBox.Show();
                            //if (MessageBox.Show("Confirm to continue without combine / free items ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                            //{
                            //    return;
                            //}

                        }
                        else
                        {
                            DisplayMessage("Please check the price type and upload sheet.", 2);    
                          
                            return;
                        }
                    }

                }
            }


            DisplayMessage("Please unselect blank cell/cells which you have selected in excel sheet", 1);
            lblalert.Text = "";
            lblsuccess.Text = "";
            lblsuccess.Visible = false;
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
            excelUpload.Show();
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
                //Import_To_Grid(FilePath, Extension);
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
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
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


        protected void btnprocess_Click(object sender, EventArgs e)
        {

            _list = new List<PriceDetailRef>();
            _comList = new List<PriceCombinedItemRef>();

            Int32 _currentRow = 0;

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            DataTable[] GetExecelTbl = LoadData(Session["FilePath"].ToString());
            if (ddlPType.SelectedItem.Text != "NORMAL")
            {
                masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                masterAuto.Aut_cate_tp = "COM";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "PRICE";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "PRO";
                masterAuto.Aut_year = null;
            }
            else
            {
                masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                masterAuto.Aut_cate_tp = "COM";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "PRICE";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "NOR";
                masterAuto.Aut_year = null;
            }
            if (ddlPCat.SelectedItem.Text == "NORMAL")
            {
                #region
                if (chkComPromo.Checked == true)
                {
                    #region
                    Int32 _mainLine = 0;
                    Int32 _subLine = 0;
                    Int32 _pbSeq = 0;

                    _list = new List<PriceDetailRef>();
                    _comList = new List<PriceCombinedItemRef>();


                    if (GetExecelTbl != null)
                    {
                        if (GetExecelTbl[0].Rows.Count > 0)
                        {
                            // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                            for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                            {
                                try
                                {
                                    string _combineItem = "";
                                    Int32 _combineQty = 0;
                                    decimal _combinePrice = 0;
                                    string _mainItemForcombine = "";

                                    PriceDetailRef _one = new PriceDetailRef();
                                    PriceCombinedItemRef _comDet = new PriceCombinedItemRef();

                                    _currentRow = _currentRow + 1;
                                    string _book = Convert.ToString(GetExecelTbl[0].Rows[i][0].ToString());
                                    string _level = Convert.ToString(GetExecelTbl[0].Rows[i][1].ToString());
                                    string _mainItem = Convert.ToString(GetExecelTbl[0].Rows[i][2].ToString());
                                    _mainItemForcombine = Convert.ToString(GetExecelTbl[0].Rows[i][8].ToString());

                                    bool _isValidItem = CHNLSVC.Inventory.IsValidItem(Session["UserCompanyCode"].ToString(), _mainItem);
                                    bool _isValidBook = CHNLSVC.Sales.IsValidBook(Session["UserCompanyCode"].ToString(), _book);
                                    bool _isValidLevel = CHNLSVC.Sales.IsValidLevel(Session["UserCompanyCode"].ToString(), _book, _level);


                                    if (_isValidItem == false)
                                    {
                                        string _msg = "Item code in the document is invalid. please check the row - " + _currentRow.ToString();
                                      
                                        lblalert.Visible = true;
                                        lblalert.Text = _msg;
                                        excelUpload.Show();
                                        return;
                                    }

                                    if (_isValidBook == false)
                                    {
                                        string _msg = "Price book in the document is invalid. please check the row - " + _currentRow.ToString();

                                        lblalert.Visible = true;
                                        lblalert.Text = _msg;
                                        excelUpload.Show();
                                        return;
                                    }

                                    if (_isValidLevel == false)
                                    {
                                        string _msg = "Price level in the document is invalid. please check the row - " + _currentRow.ToString();

                                        lblalert.Visible = true;
                                        lblalert.Text = _msg;
                                        excelUpload.Show();
                                        return;
                                    }
                                    if (!string.IsNullOrEmpty(_mainItemForcombine))
                                    {


                                        bool _isValidMainItem = CHNLSVC.Inventory.IsValidItem(Session["UserCompanyCode"].ToString(), _mainItemForcombine);

                                        if (_isValidMainItem == false)
                                        {
                                            string _msg = "Main item code for combine item in the document is invalid. please check the row " + _currentRow.ToString();

                                            lblalert.Visible = true;
                                            lblalert.Text = _msg;
                                            excelUpload.Show();
                                            return;
                                        }

                                        _combineItem = Convert.ToString(GetExecelTbl[0].Rows[i][2].ToString()); ;
                                        _combineQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                                        _combinePrice = Convert.ToDecimal(GetExecelTbl[0].Rows[i][7].ToString());
                                        _subLine = _subLine + 1;

                                        //if (_combinePrice <=0)
                                        //{
                                        //    MessageBox.Show("Price can not be 0 or less\nOn Row" + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return;
                                        //}

                                        _comDet.Sapc_increse = chkPMulti.Checked;
                                        _comDet.Sapc_itm_cd = _combineItem;
                                        _comDet.Sapc_itm_line = _subLine;
                                        _comDet.Sapc_main_itm_cd = _mainItemForcombine;
                                        _comDet.Sapc_main_line = _mainLine;
                                        _comDet.Sapc_main_ser = null;
                                        _comDet.Sapc_pb_seq = _pbSeq;
                                        _comDet.Sapc_price = _combinePrice;
                                        _comDet.Sapc_qty = _combineQty;
                                        _comDet.Sapc_sub_ser = null;
                                        _comDet.Sapc_tot_com = true;
                                        _comList.Add(_comDet);

                                    }
                                    else
                                    {
                                        _mainLine = 1;
                                        _subLine = 0;
                                        _pbSeq = _pbSeq + 1;

                                        if (string.IsNullOrEmpty(GetExecelTbl[0].Rows[i][3].ToString()))
                                        {
                                            string _msg = "Invalid 'from date'. please check the row - " + _currentRow.ToString();

                                            lblalert.Visible = true;
                                            lblalert.Text = _msg;
                                            excelUpload.Show();

                                            return;
                                        }

                                        DateTime _fromDate = Convert.ToDateTime(GetExecelTbl[0].Rows[i][3].ToString()).Date;
                                        DateTime _toDate;
                                        if (GetExecelTbl[0].Rows[i][4].ToString() == "") _toDate = Convert.ToDateTime("31/Dec/2999");//.MaxDate;
                                        else _toDate = Convert.ToDateTime(GetExecelTbl[0].Rows[i][4].ToString()).Date;
                                        Int32 _fromQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());

                                        Int32 _toQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][6].ToString());
                                        Decimal _UPrice = Convert.ToDecimal(GetExecelTbl[0].Rows[i][7].ToString());
                                        string _warrantyRemarks = Convert.ToString(GetExecelTbl[0].Rows[i][13].ToString());
                                        Int32 _noOfTimes = Convert.ToInt32(GetExecelTbl[0].Rows[i][9].ToString());
                                        string _cusCode = Convert.ToString(GetExecelTbl[0].Rows[i][11].ToString());

                                        MasterItem _oneItem = new MasterItem();
                                        _oneItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _mainItem);


                                        if (!string.IsNullOrEmpty(_cusCode))
                                        {
                                            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

                                            _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), _cusCode, string.Empty, string.Empty, "C");

                                            if (_masterBusinessCompany.Mbe_cd == null)
                                            {
                                                string _msg = "Customer code is invalid. - " + _currentRow.ToString();

                                                lblalert.Visible = true;
                                                lblalert.Text = _msg;
                                                excelUpload.Show();
                                                return;
                                            }

                                        }
                                        if (_UPrice <= 0)
                                        {
                                            string _msg = "Price can not be 0 or less\nOn Row" + _currentRow.ToString();

                                            lblalert.Visible = true;
                                            lblalert.Text = _msg;
                                            excelUpload.Show();
                                            return;
                                        }

                                        _one.Sapd_warr_remarks = _warrantyRemarks;
                                        _one.Sapd_upload_dt = DateTime.Now;
                                        _one.Sapd_update_dt = DateTime.Now;
                                        _one.Sapd_to_date = _toDate;
                                        _one.Sapd_session_id = Session["SessionID"].ToString();
                                        _one.Sapd_seq_no = _mainLine;
                                        _one.Sapd_qty_to = _toQty;
                                        _one.Sapd_qty_from = _fromQty;
                                        _one.Sapd_price_type = Convert.ToInt16(ddlPType.SelectedValue);
                                        _one.Sapd_price_stus = "P";
                                        _one.Sapd_pbk_lvl_cd = _level;
                                        _one.Sapd_pb_tp_cd = _book;
                                        _one.Sapd_pb_seq = _pbSeq;
                                        _one.Sapd_no_of_use_times = 0;
                                        _one.Sapd_no_of_times = _noOfTimes;
                                        _one.Sapd_model = _oneItem.Mi_model;
                                        _one.Sapd_mod_when = DateTime.Now;
                                        _one.Sapd_mod_by = Session["UserID"].ToString();
                                        _one.Sapd_margin = 0;
                                        _one.Sapd_lst_cost = 0;
                                        _one.Sapd_itm_price = _UPrice;
                                        _one.Sapd_itm_cd = _mainItem;
                                        _one.Sapd_is_cancel = false;
                                        _one.Sapd_is_allow_individual = false;
                                        _one.Sapd_from_date = _fromDate;
                                        _one.Sapd_erp_ref = _level;
                                        _one.Sapd_dp_ex_cost = 0;
                                        _one.Sapd_day_attempt = 0;
                                        _one.Sapd_customer_cd = _cusCode;
                                        _one.Sapd_cre_when = DateTime.Now;
                                        _one.Sapd_cre_by = Session["UserID"].ToString();
                                        _one.Sapd_circular_no = txtPcir.Text.ToUpper().Trim();
                                        _one.Sapd_cancel_dt = DateTime.MinValue;
                                        _one.Sapd_avg_cost = 0;
                                        _one.Sapd_apply_on = "0";
                                        _list.Add(_one);
                                    }



                                }
                                catch (Exception ex)
                                {
                                    DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                    lblalert.Visible = true;
                                    lblalert.Text = "Excel Data Invalid Please check Excel File and Upload";
                                    excelUpload.Show();
                                    return;
                                }

                            }
                            if (_comList == null || _comList.Count <= 0)
                            {
                                lblalert.Text = "Cannot find combine items.Please check upload excel.";
                                excelUpload.Show();
                                return;
                            }


                        }
                    }
                #endregion
                }
                else
                {
                    #region
                    Int32 _mainLine = 0;
                    Int32 _subLine = 0;
                    Int32 _pbSeq = 0;

                    _list = new List<PriceDetailRef>();
                    _comList = new List<PriceCombinedItemRef>();
                    if (GetExecelTbl != null)
                    {
                        if (GetExecelTbl[0].Rows.Count > 0)
                        {
                            // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                            for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                            {
                                try
                                {
                                    PriceDetailRef _one = new PriceDetailRef();
                                    //PriceCombinedItemRef _comDet = new PriceCombinedItemRef();

                                    _currentRow = _currentRow + 1;
                                    string _book = GetExecelTbl[0].Rows[i][0].ToString();//Convert.ToString(_row["F1"]);
                                    string _level = GetExecelTbl[0].Rows[i][1].ToString();// Convert.ToString(_row["B"]);
                                    string _mainItem = GetExecelTbl[0].Rows[i][2].ToString();//Convert.ToString(_row["C"]);



                                    //_mainItemForcombine = Convert.ToString(_row["I"]);

                                    bool _isValidItem = CHNLSVC.Inventory.IsValidItem(Session["UserCompanyCode"].ToString(), _mainItem);
                                    bool _isValidBook = CHNLSVC.Sales.IsValidBook(Session["UserCompanyCode"].ToString(), _book);
                                    bool _isValidLevel = CHNLSVC.Sales.IsValidLevel(Session["UserCompanyCode"].ToString(), _book, _level);

                                    if (_isValidItem == false)
                                    {
                                         lblalert.Visible = true;
                                        lblalert.Text = " Price code in the document is invalid. please check the row - " + _currentRow.ToString();
                                        excelUpload.Show();
                                        return;
                                    }

                                    if (_isValidBook == false)
                                    {
                                      
                                        lblalert.Visible = true;
                                        lblalert.Text = " Price book in the document is invalid. please check the row - " + _currentRow.ToString();
                                        excelUpload.Show();
                                        return;
                                    }

                                    if (_isValidLevel == false)
                                    {
                                        lblalert.Visible = true;
                                        lblalert.Text =" Price level in the document is invalid. please check the row - " + _currentRow.ToString();
                                        excelUpload.Show();
                                        return;
                                    }
                                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11048))
                                    {
                                        //chek for pb and plevel
                                        if (!string.IsNullOrEmpty(_level))
                                        {
                                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, _book, _level, 2);
                                            if (_resList == null || _resList.Count <= 0)
                                            {
                                                lblalert.Visible = true;
                                                lblalert.Text = "User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + _book + " and Price Level - " + _level;
                                                excelUpload.Show();
                                                return;
                                            }
                                        }
                                        //chek for pb only
                                        if (string.IsNullOrEmpty(_level))
                                        {
                                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, _book, "", 1);
                                            if (_resList == null || _resList.Count <= 0)
                                            {
                                                lblalert.Visible = true;
                                                lblalert.Text = "User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + _book ;
                                                excelUpload.Show();
                                                return;
                                            }
                                        }
                                    }
                                    _mainLine = 1;
                                    _subLine = 0;
                                    _pbSeq = _pbSeq + 1;
                                   // _row["D"].ToString()
                                    if (string.IsNullOrEmpty(GetExecelTbl[0].Rows[i][3].ToString()))
                                    {
                                        lblalert.Visible = true;
                                        lblalert.Text = "Invalid 'from date'. please check the row - " + _currentRow.ToString();
                                        excelUpload.Show();
                                        return;
                                    }

                                    DateTime _fromDate = Convert.ToDateTime(GetExecelTbl[0].Rows[i][3].ToString()).Date;
                                    DateTime _toDate;
                                    if (GetExecelTbl[0].Rows[i][4].ToString() == "") _toDate = Convert.ToDateTime("31/Dec/2999");
                                    else _toDate = Convert.ToDateTime(GetExecelTbl[0].Rows[i][4].ToString()).Date;
                                    Int32 _fromQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                                    Int32 _toQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][6].ToString());
                                    Decimal _UPrice = Convert.ToDecimal(GetExecelTbl[0].Rows[i][7].ToString());
                                    string _warrantyRemarks = Convert.ToString(GetExecelTbl[0].Rows[i][13].ToString());
                                    Int32 _noOfTimes = Convert.ToInt32(GetExecelTbl[0].Rows[i][9].ToString());
                                    string _cusCode = Convert.ToString(GetExecelTbl[0].Rows[i][12].ToString());

                                    MasterItem _oneItem = new MasterItem();
                                    _oneItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _mainItem);


                                    if (!string.IsNullOrEmpty(_cusCode))
                                    {
                                        MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

                                        _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), _cusCode, string.Empty, string.Empty, "C");

                                        if (_masterBusinessCompany.Mbe_cd == null)
                                        {
                                            lblalert.Visible = true;
                                            lblalert.Text = "Customer code is invalid. - " + _currentRow.ToString();
                                            excelUpload.Show();
                                           return;
                                        }

                                    }
                                    if (_UPrice <= 0)
                                    {
                                        lblalert.Visible = true;
                                        lblalert.Text = "Price can not be 0 or less\nOn Row" + _currentRow.ToString();
                                        excelUpload.Show();
                                        return;
                                    }

                                    _one.Sapd_warr_remarks = _warrantyRemarks;
                                    _one.Sapd_upload_dt = DateTime.Now;
                                    _one.Sapd_update_dt = DateTime.Now;
                                    _one.Sapd_to_date = _toDate;
                                    _one.Sapd_session_id = Session["SessionID"].ToString();
                                    _one.Sapd_seq_no = _mainLine;
                                    _one.Sapd_qty_to = _toQty;
                                    _one.Sapd_qty_from = _fromQty;
                                    _one.Sapd_price_type = Convert.ToInt16(ddlPType.SelectedValue);
                                    _one.Sapd_price_stus = "P";
                                    _one.Sapd_pbk_lvl_cd = _level;
                                    _one.Sapd_pb_tp_cd = _book;
                                    _one.Sapd_pb_seq = _pbSeq;
                                    _one.Sapd_no_of_use_times = 0;
                                    _one.Sapd_no_of_times = _noOfTimes;
                                    _one.Sapd_model = _oneItem.Mi_model;
                                    _one.Sapd_mod_when = DateTime.Now;
                                    _one.Sapd_mod_by = Session["UserID"].ToString();
                                    _one.Sapd_margin = 0;
                                    _one.Sapd_lst_cost = 0;
                                    _one.Sapd_itm_price = _UPrice;
                                    _one.Sapd_itm_cd = _mainItem;
                                    _one.Sapd_is_cancel = false;
                                    _one.Sapd_is_allow_individual = false;
                                    _one.Sapd_from_date = _fromDate;
                                    _one.Sapd_erp_ref = _level;
                                    _one.Sapd_dp_ex_cost = 0;
                                    _one.Sapd_day_attempt = 0;
                                    _one.Sapd_customer_cd = _cusCode;
                                    _one.Sapd_cre_when = DateTime.Now;
                                    _one.Sapd_cre_by = Session["UserID"].ToString();
                                    _one.Sapd_circular_no = txtPcir.Text.ToUpper().Trim();
                                    _one.Sapd_cancel_dt = DateTime.MinValue;
                                    _one.Sapd_avg_cost = 0;
                                    _one.Sapd_apply_on = "0";
                                    if (chkWithOutCombine.Checked == true)
                                    {
                                        _one.Sapd_usr_ip = "IGNORE COMBINE";
                                    }

                                    _list.Add(_one);
                                }
                                catch (Exception ex)
                                {
                                    DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                    lblalert.Visible = true;
                                    lblalert.Text = "Excel Data Invalid Please check Excel File and Upload";
                                    excelUpload.Show();
                                    return;
                                }
                            }
                        }

                    }
                    #endregion
                }
                #endregion
            }
            else if (ddlPCat.SelectedItem.Text == "SERIALIZED")
            {
                if (chkComPromo.Checked == true)
                {
                    Int32 _mainLine = 0;
                    Int32 _subLine = 0;
                    Int32 _pbSeq = 0;

                    _serial = new List<PriceSerialRef>();
                    _comList = new List<PriceCombinedItemRef>();

                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        string _combineItem = "";
                        Int32 _combineQty = 0;
                        decimal _combinePrice = 0;
                        string _combineSerial = "";
                        string _mainItemForcombine = "";
                        string _mainSerialForcombine = "";

                        PriceSerialRef _one = new PriceSerialRef();
                        PriceCombinedItemRef _comDet = new PriceCombinedItemRef();

                        _currentRow = _currentRow + 1;
                        string _book = Convert.ToString(GetExecelTbl[0].Rows[i][0].ToString());
                        string _level = Convert.ToString(GetExecelTbl[0].Rows[i][1].ToString());
                        string _mainItem = Convert.ToString(GetExecelTbl[0].Rows[i][2].ToString());
                        string _mainSerial = Convert.ToString(GetExecelTbl[0].Rows[i][10].ToString());

                        _mainItemForcombine = Convert.ToString(GetExecelTbl[0].Rows[i][8].ToString());
                        _mainSerialForcombine = Convert.ToString(GetExecelTbl[0].Rows[i][11].ToString());

                        bool _isValidItem = CHNLSVC.Inventory.IsValidItem(Session["UserCompanyCode"].ToString(), _mainItem);
                        bool _isValidBook = CHNLSVC.Sales.IsValidBook(Session["UserCompanyCode"].ToString(), _book);
                        bool _isValidLevel = CHNLSVC.Sales.IsValidLevel(Session["UserCompanyCode"].ToString(), _book, _level);

                        if (_isValidItem == false)
                        {
                            string _msg = "Item code in the document is invalid. please check the row - " + _currentRow.ToString();
                            lblalert.Visible = true;
                            lblalert.Text = _msg;
                            excelUpload.Show();
                            return;
                        }

                        if (_isValidBook == false)
                        {
                            string _msg = "Price book in the document is invalid. please check the row - " + _currentRow.ToString();
                            lblalert.Visible = true;
                            lblalert.Text = _msg;
                            excelUpload.Show();
                            return;
                          
                        }

                        if (_isValidLevel == false)
                        {
                            string _msg = "Price level in the document is invalid. please check the row - " + _currentRow.ToString();
                            lblalert.Visible = true;
                            lblalert.Text = _msg;
                            excelUpload.Show();
                            return;
                           
                        }

                        if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11048))
                        {
                            //chek for pb and plevel
                            if (!string.IsNullOrEmpty(_level))
                            {
                                List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, _book, _level, 2);
                                if (_resList == null || _resList.Count <= 0)
                                {
                                    string _msg = "User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + _book + " and Price Level - " + _level;
                                    lblalert.Visible = true;
                                    lblalert.Text = _msg;
                                    excelUpload.Show();
                                    return;
                                    
                                }
                            }
                            //chek for pb only
                            if (string.IsNullOrEmpty(_level))
                            {
                                List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, _book, "", 1);
                                if (_resList == null || _resList.Count <= 0)
                                {
                                    string _msg = "User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + _book ;
                                    lblalert.Visible = true;
                                    lblalert.Text = _msg;
                                    excelUpload.Show();
                                    return;
                                }
                            }
                        }


                        if (!string.IsNullOrEmpty(_mainItemForcombine) || !string.IsNullOrEmpty(_mainSerialForcombine))
                        {


                            bool _isValidMainItem = CHNLSVC.Inventory.IsValidItem(Session["UserCompanyCode"].ToString(), _mainItemForcombine);

                            if (_isValidMainItem == false)
                            {
                                string _msg = "Main item code for combine item in the document is invalid. please check the row - " + _currentRow.ToString();
                                lblalert.Visible = true;
                                lblalert.Text = _msg;
                                excelUpload.Show();
                                return;
                                
                            }

                            _combineItem = Convert.ToString(GetExecelTbl[0].Rows[i][2].ToString()); ;
                            _combineQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                            _combinePrice = Convert.ToDecimal(GetExecelTbl[0].Rows[i][7].ToString());
                            _combineSerial = Convert.ToString(GetExecelTbl[0].Rows[i][10].ToString());

                            if (_combinePrice <= 0)
                            {
                                string _msg = "Price can not be 0 or less\nOn Row" + _currentRow.ToString();
                                lblalert.Visible = true;
                                lblalert.Text = _msg;
                                excelUpload.Show();
                                return;
                            }

                            _subLine = _subLine + 1;

                            _comDet.Sapc_increse = chkPMulti.Checked;
                            _comDet.Sapc_itm_cd = _combineItem;
                            _comDet.Sapc_itm_line = _subLine;
                            _comDet.Sapc_main_itm_cd = _mainItemForcombine;
                            _comDet.Sapc_main_line = _mainLine;
                            _comDet.Sapc_main_ser = _mainSerialForcombine;
                            _comDet.Sapc_pb_seq = _pbSeq;
                            _comDet.Sapc_price = _combinePrice;
                            _comDet.Sapc_qty = _combineQty;
                            _comDet.Sapc_sub_ser = _combineSerial;
                            _comDet.Sapc_tot_com = true;
                            _comList.Add(_comDet);

                        }
                        else
                        {
                            _mainLine = 1;
                            _subLine = 0;
                            _pbSeq = _pbSeq + 1;

                            if (string.IsNullOrEmpty(GetExecelTbl[0].Rows[i][3].ToString().ToString()))
                            {
                                string _msg = "Invalid 'from date'. please check the row - " + _currentRow.ToString();
                                lblalert.Visible = true;
                                lblalert.Text = _msg;
                                excelUpload.Show();
                                return;
                            }

                            DateTime _fromDate = Convert.ToDateTime(GetExecelTbl[0].Rows[i][3].ToString()).Date;
                            DateTime _toDate;
                            if (GetExecelTbl[0].Rows[i][4].ToString() == "") _toDate = Convert.ToDateTime("31/Dec/2999");
                            else _toDate = Convert.ToDateTime(GetExecelTbl[0].Rows[i][4].ToString()).Date;
                            Int32 _fromQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                            Int32 _toQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][6].ToString());
                            Decimal _UPrice = Convert.ToDecimal(GetExecelTbl[0].Rows[i][7].ToString());
                            //string _warrantyRemarks = Convert.ToString(_row["K"]);
                            Int32 _noOfTimes = Convert.ToInt32(GetExecelTbl[0].Rows[i][9].ToString());
                            string _cusCode = Convert.ToString(GetExecelTbl[0].Rows[i][12].ToString());
                            string _waraRemark = Convert.ToString(GetExecelTbl[0].Rows[i][13].ToString());

                            MasterItem _oneItem = new MasterItem();
                            _oneItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _mainItem);


                            if (!string.IsNullOrEmpty(_cusCode))
                            {
                                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

                                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), _cusCode, string.Empty, string.Empty, "C");

                                if (_masterBusinessCompany.Mbe_cd == null)
                                {
                                    string _msg = "Customer code is invalid. - " + _currentRow.ToString();
                                    lblalert.Visible = true;
                                    lblalert.Text = _msg;
                                    excelUpload.Show();
                                    return;
                                }

                            }
                            if (_UPrice <= 0)
                            {
                                string _msg = "Price can not be 0 or less\nOn Row " + _currentRow.ToString();
                                lblalert.Visible = true;
                                lblalert.Text = _msg;
                                excelUpload.Show();
                                return;
                            }

                            _one.Sars_circular_no = txtPcir.Text.ToUpper().Trim();
                            _one.Sars_cre_by = Session["UserID"].ToString();
                            _one.Sars_cre_when = DateTime.Now;
                            _one.Sars_customer_cd = _cusCode;
                            _one.Sars_day_attempt = 1;
                            _one.Sars_hp_allowed = 1;
                            _one.Sars_is_cancel = false;
                            _one.Sars_is_fix_qty = true;
                            _one.Sars_itm_cd = _mainItem;
                            _one.Sars_itm_price = _UPrice;
                            _one.Sars_mod_by = Session["UserID"].ToString();
                            _one.Sars_mod_when = DateTime.Now;
                            _one.Sars_pb_seq = _pbSeq;
                            _one.Sars_pbook = _book;
                            _one.Sars_price_lvl = _level;
                            _one.Sars_price_type = Convert.ToInt16(ddlPType.SelectedValue);
                            _one.Sars_promo_cd = "N/A";
                            _one.Sars_ser_no = _mainSerial;
                            _one.Sars_update_dt = Convert.ToDateTime(DateTime.Now).Date;
                            _one.Sars_val_frm = _fromDate;
                            _one.Sars_val_to = _toDate;
                            _one.Sars_warr_remarks = _waraRemark;
                            _serial.Add(_one);

                        }

                    }

                    if (_comList == null || _comList.Count <= 0)
                    {
                        string _msg = "Cannot find combine items.Please check upload excel";
                        lblalert.Visible = true;
                        lblalert.Text = _msg;
                        excelUpload.Show();
                        return;
                    }
                }
                else
                {
                    Int32 _mainLine = 0;
                    Int32 _subLine = 0;
                    Int32 _pbSeq = 0;

                    _serial = new List<PriceSerialRef>();

                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {

                        PriceSerialRef _one = new PriceSerialRef();
                        _currentRow = _currentRow + 1;
                        string _book = Convert.ToString(GetExecelTbl[0].Rows[i][0].ToString());
                        string _level = Convert.ToString(GetExecelTbl[0].Rows[i][1].ToString());
                        string _mainItem = Convert.ToString(GetExecelTbl[0].Rows[i][2].ToString());
                        string _mainSerial = Convert.ToString(GetExecelTbl[0].Rows[i][10].ToString());

                        bool _isValidItem = CHNLSVC.Inventory.IsValidItem(Session["UserCompanyCode"].ToString(), _mainItem);
                        bool _isValidBook = CHNLSVC.Sales.IsValidBook(Session["UserCompanyCode"].ToString(), _book);
                        bool _isValidLevel = CHNLSVC.Sales.IsValidLevel(Session["UserCompanyCode"].ToString(), _book, _level);

                        if (_isValidItem == false)
                        {
                            string _msg = "Item code in the document is invalid. please check the row - " + _currentRow.ToString();
                            lblalert.Visible = true;
                            lblalert.Text = _msg;
                            excelUpload.Show();
                            return;
                        }

                        if (_isValidBook == false)
                        {
                            string _msg = "Price book in the document is invalid. please check the row - " + _currentRow.ToString();
                            lblalert.Visible = true;
                            lblalert.Text = _msg;
                            excelUpload.Show();
                            return;
                        }

                        if (_isValidLevel == false)
                        {
                            string _msg = "Price level in the document is invalid. please check the row - " + _currentRow.ToString();
                            lblalert.Visible = true;
                            lblalert.Text = _msg;
                            excelUpload.Show();
                            return;
                        }

                        if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11048))
                        {
                            //chek for pb and plevel
                            if (!string.IsNullOrEmpty(_level))
                            {
                                List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, _book, _level, 2);
                                if (_resList == null || _resList.Count <= 0)
                                {
                                    string _msg = "User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + _book + " and Price Level - " + _level;
                                    lblalert.Visible = true;
                                    lblalert.Text = _msg;
                                    excelUpload.Show();
                                    return;
                                }
                            }
                            //chek for pb only
                            if (string.IsNullOrEmpty(_level))
                            {
                                List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), DateTime.Now, _book, "", 1);
                                if (_resList == null || _resList.Count <= 0)
                                {
                                    string _msg = "User - " + Session["UserID"].ToString() + " not allowed to user Price Book - " + _book;
                                    lblalert.Visible = true;
                                    lblalert.Text = _msg;
                                    excelUpload.Show(); return;
                                }
                            }
                        }


                        _mainLine = 1;
                        _subLine = 0;
                        _pbSeq = _pbSeq + 1;

                        if (string.IsNullOrEmpty(GetExecelTbl[0].Rows[i][3].ToString()))
                        {
                            string _msg = "Invalid 'from date'. please check the row - " + _currentRow.ToString();
                            lblalert.Visible = true;
                            lblalert.Text = _msg;
                            excelUpload.Show();
                            return;
                        }

                        DateTime _fromDate = Convert.ToDateTime(GetExecelTbl[0].Rows[i][3].ToString()).Date;
                        DateTime _toDate;
                        if (GetExecelTbl[0].Rows[i][4].ToString() == "") _toDate = Convert.ToDateTime("31/Dec/2999");
                        else _toDate = Convert.ToDateTime(GetExecelTbl[0].Rows[i][4].ToString()).Date;
                        Int32 _fromQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                        Int32 _toQty = Convert.ToInt32(GetExecelTbl[0].Rows[i][6].ToString());
                        Decimal _UPrice = Convert.ToDecimal(GetExecelTbl[0].Rows[i][7].ToString());
                        //string _warrantyRemarks = Convert.ToString(_row["K"]);
                        Int32 _noOfTimes = Convert.ToInt32(GetExecelTbl[0].Rows[i][9].ToString());
                        string _cusCode = Convert.ToString(GetExecelTbl[0].Rows[i][12].ToString());
                        string _waraRemark = Convert.ToString(GetExecelTbl[0].Rows[i][13].ToString());

                        MasterItem _oneItem = new MasterItem();
                        _oneItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _mainItem);


                        if (!string.IsNullOrEmpty(_cusCode))
                        {
                            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

                            _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), _cusCode, string.Empty, string.Empty, "C");

                            if (_masterBusinessCompany.Mbe_cd == null)
                            {
                                string _msg = "Customer code is invalid. - " + _currentRow.ToString();
                                lblalert.Visible = true;
                                lblalert.Text = _msg;
                                excelUpload.Show();
                                return;
                            }

                        }
                        if (_UPrice <= 0)
                        {
                            string _msg = "Price can not be 0 or less\nOn Row " + _currentRow.ToString();
                            lblalert.Visible = true;
                            lblalert.Text = _msg;
                            excelUpload.Show();
                            return;
                        }

                        _one.Sars_circular_no = txtPcir.Text.ToUpper().Trim();
                        _one.Sars_cre_by = Session["UserID"].ToString();
                        _one.Sars_cre_when = DateTime.Now;
                        _one.Sars_customer_cd = _cusCode;
                        _one.Sars_day_attempt = 1;
                        _one.Sars_hp_allowed = 1;
                        _one.Sars_is_cancel = false;
                        _one.Sars_is_fix_qty = true;
                        _one.Sars_itm_cd = _mainItem;
                        _one.Sars_itm_price = _UPrice;
                        _one.Sars_mod_by = Session["UserID"].ToString();
                        _one.Sars_mod_when = DateTime.Now;
                        _one.Sars_pb_seq = _pbSeq;
                        _one.Sars_pbook = _book;
                        _one.Sars_price_lvl = _level;
                        _one.Sars_price_type = Convert.ToInt16(ddlPType.SelectedValue);
                        _one.Sars_promo_cd = "N/A";
                        _one.Sars_ser_no = _mainSerial;
                        _one.Sars_update_dt = Convert.ToDateTime(DateTime.Now).Date;
                        _one.Sars_val_frm = _fromDate;
                        _one.Sars_val_to = _toDate;
                        _one.Sars_warr_remarks = _waraRemark;
                        _serial.Add(_one);



                    }


                }
            }

            List<PriceProfitCenterPromotion> _savePcAllocList = new List<PriceProfitCenterPromotion>();

            //_savePcAllocList = _appPcList;
            foreach (GridViewRow row in grdAppPC.Rows)
            {
                //string _pc = row["col_a_Pc"].Value.ToString();
                //string _promo = row.Cells["col_a_Promo"].Value.ToString();
                //Int16 _active = Convert.ToInt16(row.Cells["col_a_Act"].Value);
                //Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);
                string _pc = ((Label)row.FindControl("col_invItem")).Text;
                string _promo = ((Label)row.FindControl("col_a_Promo")).Text;
                Int16 _active = Convert.ToInt16(((Label)row.FindControl("col_a_Act")).Text);
                Int32 _pbSeq = Convert.ToInt32(((Label)row.FindControl("col_a_pbSeq")).Text);

                if (_active == 1)
                {
                    foreach (PriceProfitCenterPromotion _tmp in _appPcList)
                    {
                        if (_tmp.Srpr_com == Session["UserCompanyCode"].ToString() && _tmp.Srpr_pbseq == _pbSeq && _tmp.Srpr_pc == _pc && _tmp.Srpr_promo_cd == _promo)
                        {
                            //PriceProfitCenterPromotion _tmpList = new PriceProfitCenterPromotion();
                            //_tmpList.Srpr_act = _active;
                            //_tmpList.Srpr_com = Session["UserCompanyCode"].ToString();
                            //_tmpList.Srpr_cre_by = Session["UserID"].ToString();
                            //_tmpList.Srpr_mod_by = Session["UserID"].ToString();
                            //_tmpList.Srpr_pbseq = _pbSeq;
                            //_tmpList.Srpr_pc = _pc;
                            //_tmpList.Srpr_promo_cd = _promo;
                            _tmp.Srpr_act = _active;
                            _savePcAllocList.Add(_tmp);
                        }

                    }
                }

            }

            PriceDetailRestriction _priceRes = new PriceDetailRestriction();
            //_priceRes = null;

            if (_isRestrict == true)
            {
                _priceRes.Spr_com = Session["UserCompanyCode"].ToString();
                _priceRes.Spr_msg = txtRMessage.Text;//txtMessage.Text 1;
                _priceRes.Spr_need_cus = chkNeedCus.Checked;// chkCustomer.Checked 1;
                _priceRes.Spr_need_nic = chkNeedNIC.Checked; // 1 chkNIC.Checked;
                _priceRes.Spr_need_pp = false;
                _priceRes.Spr_need_dl = false;
                _priceRes.Spr_usr = Session["UserID"].ToString();
                _priceRes.Spr_when = DateTime.Now;
                _priceRes.Spr_promo = "";
            }
            else
            {
                _priceRes = null;
            }
            int row_aff = 0;
            string _err = "";
            row_aff = (Int32)CHNLSVC.Sales.SavePriceDetails(_list, _comList, masterAuto, _savePcAllocList, _serial, _priceRes, out _err, Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(),null,0,"");

            if (row_aff == 1)
            {
                lblsuccess.Visible = true;
                lblsuccess.Text = "Price definition created successfully.";
                lblalert.Visible = false;
                divUpcompleted.Visible = false;
                excelUpload.Show();
                
                //Clear_Data();

            }
            else
            {
                if (!string.IsNullOrEmpty(_err))
                {
                    lblalert.Visible = true;
                    lblalert.Text = _err;
                    excelUpload.Show();
                  
                }
                else
                {
                    lblalert.Visible = true;
                    lblalert.Text = "Failed to update.";
                    excelUpload.Show();
                    
                }
            }


        }

        protected void btnok_Click(object sender, EventArgs e)
        {
           
        }
        protected void btnno_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }

        protected void lbtnAddPC_Click(object sender, EventArgs e)
        {

            try
            {
                ////lstPC.Clear();
                //DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(Session["UserCompanyCode"].ToString(), txtMChan.Text, txtMSchan.Text, null, null, null, txtMProfit.Text);
                //foreach (DataRow drow in dt.Rows)
                //{
                //    chkPCList.Items.Add(drow["PROFIT_CENTER"].ToString());
                //}

                //txtMChan.Text = "";
                //txtMSchan.Text = "";
                //txtMProfit.Text = "";
                //txtMProfit.Focus();
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnfindMchannel_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "83";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
                Session["P_1"] = true;
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

        protected void lbtnsubMchannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMChan.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select channel !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtmChannel.Text = string.Empty;
                    lbtnsubchannel.Focus();
                    mpAddCostItems.Show();
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                _Ltype = "Maintain";

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "84";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
                Session["P_1"] = true;
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


        protected void lbtnpcMfind_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                _Ltype = "Maintain";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "85";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                mpAddCostItems.Show();
                txtSearchbyword.Focus();
                Session["P_1"] = true;
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
        protected void brtnaddM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(Session["UserCompanyCode"].ToString(), txtMChan.Text, txtMSubChan.Text, null, null, null, txtMPCenter.Text);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        chkMList.Items.Add(drow["PROFIT_CENTER"].ToString());
                    }
                }
                else
                {
                    DisplayMessage("Selected data not found", 2);
                }
                //txtmChannel.Text = "";
                //txtmSubChannel.Text = "";
                //txtMAppPc.Text = "";
                txtMPCenter.Focus();
                mpAddCostItems.Show();
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

        protected void btnselectall_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem li in chkMList.Items)
                {
                    li.Selected = true;
                }
                mpAddCostItems.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void btnunselect_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem li in chkMList.Items)
                {
                    li.Selected = false;
                }
                mpAddCostItems.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                chkMList.Items.Clear();
                txtMChan.Text = string.Empty;
                txtMSubChan.Text = string.Empty;
                txtMPCenter.Text = string.Empty;
                txtMChan.Focus();
                mpAddCostItems.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void btnAppPcApply_Click(object sender, EventArgs e)
        {
            try
            {
               _appPcList= ViewState["_appPcList"] as List<PriceProfitCenterPromotion>;
                _isRecal = Convert.ToBoolean(Session["_isRecal"].ToString());
                Boolean _isPCFound = false;
                foreach (ListItem Item in chkMList.Items)
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
                    string _msg = "No any applicable profit centers are selected.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "')", true);
                    mpAddCostItems.Show();
                    return;
                }

                if (_isRecal == true)
                {
                    Boolean _isPromoFound = false;
                    foreach (ListItem Item in chkDetList.Items)
                    {
                        string promoCD = Item.Text;

                        if (Item.Selected == true)
                        {
                            _isPromoFound = true;
                            goto L2;
                        }
                    }
                L2:

                    if (_isPromoFound == false)
                    {
                        string _msg = "No any applicable promotions are selected.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "')", true);
                        mpAddCostItems.Show();
                        return;
                    }

                    Int32 _listPbSeq = 0;
                    foreach (ListItem promoList in chkDetList.Items)
                    {
                        string promo = promoList.Text;

                        if (promoList.Selected == true)
                        {
                            List<PriceDetailRef> _tmpdetbyPromo = new List<PriceDetailRef>();

                            _tmpdetbyPromo = CHNLSVC.Sales.GetPriceByPromoCD(promo);
                            _listPbSeq = 0;
                            foreach (PriceDetailRef _tmpList in _tmpdetbyPromo)
                            {
                                _listPbSeq = _tmpList.Sapd_pb_seq;
                            }
                            foreach (ListItem pcList in chkMList.Items)
                            {
                                string pc = pcList.Text;

                                if (pcList.Selected == true)
                                {

                                    PriceProfitCenterPromotion _tmpAppPc = new PriceProfitCenterPromotion();


                                    _tmpAppPc.Srpr_act = 1;
                                    _tmpAppPc.Srpr_com = Session["UserCompanyCode"].ToString();
                                    _tmpAppPc.Srpr_cre_by = Session["UserID"].ToString();
                                    _tmpAppPc.Srpr_mod_by = Session["UserID"].ToString();
                                    _tmpAppPc.Srpr_pbseq = _listPbSeq;
                                    _tmpAppPc.Srpr_pc = pc;
                                    _tmpAppPc.Srpr_promo_cd = promo;
                                    _appPcList.Add(_tmpAppPc);
                                }
                            }
                        }
                    }

                   // lstPC.Clear();

                    grdAppPC.AutoGenerateColumns = false;
                    grdAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                    grdAppPC.DataSource = _appPcList;
                    grdAppPC.DataBind();
                    ViewState["_appPcList"] = _appPcList;
                    if (_appPcList != null)
                    {
                        //foreach (GridView row in grdAppPC.Rows)
                        //{
                        //    DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                        //    chk.Value = true;
                        //}

                        for (int i = 0; i < grdAppPC.Rows.Count; i++)
                        {
                            CheckBox chk = ((CheckBox)grdAppPC.Rows[i].FindControl("col_a_Act"));
                            chk.Checked = true;
                        }
                        /*
                        foreach (PriceProfitCenterPromotion _chk in _appPcList)
                        {
                            foreach (DataGridViewRow row in dgvAppPC.Rows)
                            {
                                string _pc = row.Cells["col_a_Pc"].Value.ToString();
                                string _promo = row.Cells["col_a_Promo"].Value.ToString();
                                Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);

                                if (_pc == _chk.Srpr_pc && _promo == _chk.Srpr_promo_cd && _pbSeq == _chk.Srpr_pbseq)
                                {
                                    DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                                    //if (Convert.ToBoolean(chk.Value) == false)
                                    if (_chk.Srpr_act == 1)
                                    {
                                        chk.Value = true;
                                        goto L3;
                                    }
                                    else
                                    {
                                        chk.Value = false;
                                        goto L3;
                                    }
                                }

                            }
                        L3: Int16 x = 1;
                        }
                        */
                    }
                }
                else
                {
                    if (_appPcList == null)
                    {
                        _appPcList = new List<PriceProfitCenterPromotion>();
                    }
                    foreach (ListItem pcList in chkMList.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Selected == true)
                        {
                            var _selec = _appPcList.Where(x => x.Srpr_pc == pc).ToList();
                            if (_selec.Count > 0)
                            {
                                string _msg = "All ready add Profit Center";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "')", true);
                                mpAddCostItems.Show();
                                return;
                            }

                            PriceProfitCenterPromotion _tmpAppPc = new PriceProfitCenterPromotion();
                            _tmpAppPc.Srpr_act = 1;
                            _tmpAppPc.Srpr_com = Session["UserCompanyCode"].ToString();
                            _tmpAppPc.Srpr_cre_by = Session["UserID"].ToString();
                            _tmpAppPc.Srpr_mod_by = Session["UserID"].ToString();
                            _tmpAppPc.Srpr_pbseq = 0;
                            _tmpAppPc.Srpr_pc = pc;
                            _tmpAppPc.Srpr_promo_cd = "N/A";
                            _appPcList.Add(_tmpAppPc);

                        }
                    }

                   // lstPC.Clear();

                    grdAppPC.AutoGenerateColumns = false;
                    grdAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                    grdAppPC.DataSource = _appPcList;
                    grdAppPC.DataBind();
                    ViewState["_appPcList"] = _appPcList;
                    //foreach (DataGridViewRow row in dgvAppPC.Rows)
                    //{

                    //    {
                    //        DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                    //        //if (Convert.ToBoolean(chk.Value) == false)
                    //        chk.Value = true;

                    //    }
                    //}
                    for (int i = 0; i < grdAppPC.Rows.Count; i++)
                    {
                        CheckBox chk = ((CheckBox)grdAppPC.Rows[i].FindControl("col_a_Act"));
                        chk.Checked = true;
                    }
                }

                mpAddCostItems.Show();
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            
        }


        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
            DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, "Code", txtPCus.Text);
            if (result.Rows.Count == 0)
            {
                string _Msg = "Invalid customer";
                DisplayMessage(_Msg, 2);
                txtPCus.Text = string.Empty;
            }
        }

        protected void lbtnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                lblvalue.Text = "Customer";
                ViewState["SEARCH"] = result;
                SIPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnRemove_Click(object sender, EventArgs e)
        {
            if (txtconfirmdelete.Value == "No")
            {
                return;
            }
            if (grdPriceDet.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
              _list=  ViewState["PriceItem"]  as List<PriceDetailRef>;

              if (_list != null)
              {
                  string _item = (row.FindControl("sapd_itm_cd") as Label).Text;
                  var _filter = _list.Find(x => x.Sapd_itm_cd == _item);
                  _list.Remove(_filter);
                  grdPriceDet.DataSource = _list;
                  grdPriceDet.DataBind();
                  ViewState["PriceItem"] = _list;
              }
            }
        }

        protected void ddlPType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(ddlPType.SelectedItem.ToString());
                foreach (PriceTypeRef _tmp in _list)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        //btnAddCom.Enabled = true;
                        chkPMulti.Checked = false;
                        chkPMulti.Visible = true;
                        pnlmulti.Visible = true;
                        chkComPromo.Enabled = true;
                        chkComPromo.Checked = false;
                        pnlpro.Visible = true;
                        chkWithOutCombine.Visible = true;
                        pnlcombin.Visible = true;
                    }
                    else
                    {
                        //btnAddCom.Enabled = false;
                        chkPMulti.Checked = false;
                        chkPMulti.Visible = false;
                        pnlmulti.Visible = false;
                        chkComPromo.Checked = false;
                        chkComPromo.Enabled = false;
                        pnlpro.Visible = false;
                        chkWithOutCombine.Visible = false;
                        pnlcombin.Visible = false;
                    }

                    if (_tmp.Sarpt_indi == 0)
                    {
                        btnAppPC.Enabled = false;
                    }
                    else
                    {
                        btnAppPC.Enabled = true;
                    }

                }
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);

            }
            
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            bool _pro = Convert.ToBoolean(Session["P_1"]);
            if (_pro == true)
            {
                mpAddCostItems.Show();
            }
        }

        protected void btncleargrid_Click(object sender, EventArgs e)
        {
            grdAppPC.AutoGenerateColumns = false;
            grdAppPC.DataSource = new List<PriceProfitCenterPromotion>();
            grdAppPC.DataBind();
            _appPcList = new List<PriceProfitCenterPromotion>();
            chkMList.Items.Clear();

            ViewState["_appPcList"] = _appPcList;
            txtMChan.Text = "";
            txtMSubChan.Text = "";
            txtMPCenter.Text = "";
            txtMChan.Focus();
        }

        protected void btnokAp_Click(object sender, EventArgs e)
        {
            mpAddCostItems.Hide();
        }

        protected void btnuppdateAp_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                List<PriceProfitCenterPromotion> _savePcAllocList = new List<PriceProfitCenterPromotion>();
                _appPcList = ViewState["_appPcList"] as List<PriceProfitCenterPromotion>;
                if (_appPcList.Count == 0)
                {
                    DisplayMessage("No Profit Center found update.", 2);
                    return;
                }
                foreach (GridViewRow row in grdAppPC.Rows)
                {
                    string _pc = ((Label)row.FindControl("col_a_Pc")).Text;
                    string _promo = ((Label)row.FindControl("col_a_Promo")).Text;
                    Int16 _active = Convert.ToInt16(((CheckBox)row.FindControl("col_a_Act")).Checked);
                    Int32 _pbSeq = Convert.ToInt32(((Label)row.FindControl("col_a_pbSeq")).Text);
                 

                    //string _pc = row.Cells["col_a_Pc"].Value.ToString();
                    //string _promo = row.Cells["col_a_Promo"].Value.ToString();
                    //Int16 _active = Convert.ToInt16(row.Cells["col_a_Act"].Value);
                    //Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);

                    foreach (PriceProfitCenterPromotion _tmp in _appPcList)
                    {
                        if (_tmp.Srpr_com == Session["UserCompanyCode"].ToString() && _tmp.Srpr_pbseq == _pbSeq && _tmp.Srpr_pc == _pc && _tmp.Srpr_promo_cd == _promo)
                        {
                            PriceProfitCenterPromotion _tmpList = new PriceProfitCenterPromotion();
                            _tmpList.Srpr_act = _active;
                            _tmpList.Srpr_com = Session["UserCompanyCode"].ToString();
                            _tmpList.Srpr_cre_by = Session["UserID"].ToString();
                            _tmpList.Srpr_mod_by = Session["UserID"].ToString();
                            _tmpList.Srpr_pbseq = _pbSeq;
                            _tmpList.Srpr_pc = _pc;
                            _tmpList.Srpr_promo_cd = _promo;
                            _savePcAllocList.Add(_tmpList);
                        }

                    }

                }
                DataTable _dt = _savePcAllocList.ToDataTable<PriceProfitCenterPromotion>();
                string _error;
                _dt.TableName = "aaa";
                row_aff = CHNLSVC.Sales.SaveAppPromoPcDataTable(_dt, out _error);

                if (row_aff == 1)
                {
                    DisplayMessage("Profit center allocated successfully.", 3);

                    ClearNewPriceCreation();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_error))
                    {
                        DisplayMessage(_error, 2);
                      
                    }
                    else
                    {
                        DisplayMessage("Faild to update.", 2);
                        
                    }
                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                DisplayMessage(_Msg, 4);
            }
            
        }

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmcancel.Value == "No")
                {
                    return;
                }
                //get all promotions
                //check for used items
                //cancel all
                string error = "";
                List<string> _errList = new List<string>();
                List<string> _promoCodes = new List<string>();
                if (chkDetList.Items.Count <= 0)
                {
                    DisplayMessage("Please add/select promotions from list", 2);                   
                    return;
                }
                foreach (ListItem item in chkDetList.Items)
                {
                    if (item.Selected)
                        _promoCodes.Add(item.Text);
                }

                int result = CHNLSVC.Sales.ProcessPromotionCancel(_promoCodes, out  error, out  _errList, Session["UserID"].ToString(), Session["SessionID"].ToString());
                // Nadeeka
                int resultsub = CHNLSVC.Sales.ProcessPromotionCancelSubPb(_promoCodes, out  error, out  _errList, Session["UserID"].ToString(), Session["SessionID"].ToString());

                if (result > 0)
                {
                    DisplayMessage("Successfully Cancelled", 3);
                    ClearNewPriceCreation();
                }
                else
                {
                    if (error != "")
                    {
                        DisplayMessage(error, 4); 
                       // MessageBox.Show("Error occurred while processing\n" + error, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (_errList != null && _errList.Count > 0)
                    {
                        string _errPromo = "";
                        foreach (string st in _errList)
                        {
                            _errPromo = _errPromo + st + ",";
                        }
                        DisplayMessage("Can not Cancel, following promotion has used items" + _errPromo, 2); 
                       // MessageBox.Show("Can not Cancel, following promotion has used items\n" + _errPromo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }




            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4); 
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
          
        }

        protected void lbtnsaveas_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtSavelconformmessageValue.Value == "No")
                {
                    return;
                }
                _list = ViewState["PriceItem"] as List<PriceDetailRef>;
                _comList = ViewState["PriceItemCombine"] as List<PriceCombinedItemRef>;
                _appPcList = ViewState["_appPcList"] as List<PriceProfitCenterPromotion>;

                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtPPriceBook.Text))
                {
                    DisplayMessage("Please select Price book.", 2);
                    txtPPriceBook.Text = "";
                    txtPPriceBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPPriceLevel.Text))
                {
                    DisplayMessage("Please select Price level.", 2);
                    txtPPriceLevel.Text = "";
                    txtPPriceLevel.Focus();
                    return;
                }

                if (Convert.ToDateTime(txtPToDate.Text).Date < Convert.ToDateTime(txtPFromDate.Text))
                {
                    DisplayMessage("To date cannot be less than from date.", 2);          
                    return;
                }

                if (string.IsNullOrEmpty(txtPcir.Text))
                {
                    DisplayMessage("Please enter circular #", 2);
                    txtPcir.Text = "";
                    txtPcir.Focus();
                    return;
                }
                DataTable _circular = CHNLSVC.Sales.GetCircularNo(txtPcir.Text.ToUpper());
                if (_circular.Rows.Count > 0)
                {
                    DisplayMessage("Circular exists,Please enter another name", 2);
                   
                    return;
                }



                List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(ddlPType.Text);
                foreach (PriceTypeRef _tmp in _type)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        if (dgvComDet.Rows.Count <= 0)
                        {
                            DisplayMessage("No combine / free items are define.e", 2);                          
                            return;
                        }
                    }

                }

                List<PriceDetailRef> _saveDetail = new List<PriceDetailRef>();
                List<PriceCombinedItemRef> _saveComDet = new List<PriceCombinedItemRef>();

                foreach (PriceDetailRef _tmpPriceDet in _list)
                {
                    PriceDetailRef _tmpPriceList = new PriceDetailRef();
                    _tmpPriceList = _tmpPriceDet;
                    _tmpPriceList.Sapd_customer_cd = txtPCus.Text;
                    _tmpPriceList.Sapd_from_date = Convert.ToDateTime(txtPFromDate.Text).Date;
                    _tmpPriceList.Sapd_to_date = Convert.ToDateTime(txtPToDate.Text).Date;
                    _tmpPriceList.Sapd_circular_no = txtPcir.Text.ToUpper();
                    _tmpPriceList.Sapd_pb_tp_cd = txtPPriceBook.Text.ToUpper().Trim();
                    _tmpPriceList.Sapd_pbk_lvl_cd = txtPPriceLevel.Text.ToUpper().Trim();
                    _tmpPriceList.Sapd_price_type = Convert.ToInt16(ddlPType.SelectedValue);
                    _tmpPriceList.Sapd_erp_ref = txtPPriceLevel.Text.ToUpper().Trim();
                    _tmpPriceList.Sapd_no_of_use_times = 0;
                    _tmpPriceList.Sapd_price_stus = "P";
                    _saveDetail.Add(_tmpPriceList);
                }

                foreach (PriceCombinedItemRef _tmpComList in _comList)
                {
                    _tmpComList.Sapc_increse = chkPMulti.Checked;
                    _saveComDet.Add(_tmpComList);
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();

                if (ddlPType.Text != "NORMAL")
                {
                    masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PRICE";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "PRO";
                    masterAuto.Aut_year = null;
                }
                else
                {
                    masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PRICE";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "NOR";
                    masterAuto.Aut_year = null;
                }

                List<PriceProfitCenterPromotion> _savePcAllocList = new List<PriceProfitCenterPromotion>();

                //_savePcAllocList = _appPcList;
                foreach (GridViewRow row in grdAppPC.Rows)
                {
                    string _pc = ((Label)row.FindControl("col_a_Pc")).Text;
                    string _promo = ((Label)row.FindControl("col_a_Promo")).Text;
                    Int16 _active = Convert.ToInt16(((Label)row.FindControl("col_a_Act")).Text);
                    Int32 _pbSeq = Convert.ToInt32(((Label)row.FindControl("col_a_pbSeq")).Text);



                    if (_active == 1)
                    {
                        foreach (PriceProfitCenterPromotion _tmp in _appPcList)
                        {
                            if (_tmp.Srpr_com == Session["UserCompanyCode"].ToString() && _tmp.Srpr_pbseq == _pbSeq && _tmp.Srpr_pc == _pc && _tmp.Srpr_promo_cd == _promo)
                            {
                                //PriceProfitCenterPromotion _tmpList = new PriceProfitCenterPromotion();
                                //_tmpList.Srpr_act = _active;
                                //_tmpList.Srpr_com = Session["UserCompanyCode"].ToString();
                                //_tmpList.Srpr_cre_by = Session["UserID"].ToString();
                                //_tmpList.Srpr_mod_by = Session["UserID"].ToString();
                                //_tmpList.Srpr_pbseq = _pbSeq;
                                //_tmpList.Srpr_pc = _pc;
                                //_tmpList.Srpr_promo_cd = _promo;
                                _tmp.Srpr_act = _active;
                                _savePcAllocList.Add(_tmp);
                            }

                        }
                    }

                }

                //save list



                //update list

                PriceDetailRestriction _priceRes = new PriceDetailRestriction();
                //_priceRes = null;

                if (!string.IsNullOrEmpty(txtRMessage.Text))
                {
                    _priceRes.Spr_com = Session["UserCompanyCode"].ToString();
                    _priceRes.Spr_msg = txtRMessage.Text;
                    _priceRes.Spr_need_cus = chkNeedCus.Checked;
                    _priceRes.Spr_need_nic = chkNeedNIC.Checked;
                    _priceRes.Spr_need_pp = false;
                    _priceRes.Spr_need_dl = false;
                    _priceRes.Spr_usr = Session["UserID"].ToString();
                    _priceRes.Spr_when = DateTime.Now;
                    _priceRes.Spr_promo = "";
                }
                else
                {
                    _priceRes = null;
                }

                string _err = "";


                row_aff = (Int32)CHNLSVC.Sales.SavePriceDetailsSaveAs(_saveDetail, _comList, masterAuto, _savePcAllocList, _serial, _priceRes, out _err, Session["SessionID"].ToString(), Session["UserID"].ToString());

                if (row_aff == 1)
                {
                    DisplayMessage("Price definition created successfully.", 3);

                    ClearNewPriceCreation();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_err))
                    {
                        DisplayMessage(_err, 3);
                        
                    }
                    else
                    {
                        DisplayMessage("aild to update.", 3);
                       
                    }
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 3);
            }
           
        }

        protected void lbtnamend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmAmend.Value == "No")
                {
                    return;
                }
                _list = ViewState["PriceItem"] as List<PriceDetailRef>;
                _comList = ViewState["PriceItemCombine"] as List<PriceCombinedItemRef>;
                _appPcList = ViewState["_appPcList"] as List<PriceProfitCenterPromotion>;


                Int32 row_aff = 0;
                string _promoList = "";
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtPPriceBook.Text))
                {
                    DisplayMessage("Please select a price book.", 2);
                    txtPPriceBook.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPPriceLevel.Text))
                {
                    DisplayMessage("Please select a price level.", 2);
                    txtPPriceLevel.Focus();
                    return;
                }
                _isRecal = Convert.ToBoolean(Session["_isRecal"].ToString());
                if (_isRecal == false)
                {
                    DisplayMessage("Please recall exsisting circular / promotion to extend.", 2);              
                    return;
                }

                if (Convert.ToDateTime(txtPToDate.Text).Date < DateTime.Now.Date)
                {
                    DisplayMessage("To date cannot be less than today.", 2);                   
                    return;
                }

                Boolean _isPromoFound = false;
                foreach (ListItem Item in chkDetList.Items)
                {
                    //string promoCD = Item.Text;

                    if (Item.Selected == true)
                    {
                        _isPromoFound = true;
                        goto L2;
                    }
                }
            L2:

                if (_isPromoFound == false)
                {
                    DisplayMessage("No any applicable promotions are selected.", 2);                  
                    return;
                }


                List<string> lstPromoI = new List<string>();


                foreach (ListItem Item in chkDetList.Items)
                {
                    //string promoCD = Item.Text;

                    if (Item.Selected == true)
                    {
                        _promoList = _promoList + "," + Item.Text;
                        lstPromoI.Add(Item.Text);
                    }
                }

                if (_promoList != null)
                {
                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    if (ddlPType.SelectedItem.Text != "NORMAL")
                    {
                        masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                        masterAuto.Aut_cate_tp = "COM";
                        masterAuto.Aut_direction = null;
                        masterAuto.Aut_modify_dt = null;
                        masterAuto.Aut_moduleid = "PRICE";
                        masterAuto.Aut_number = 5;//what is Aut_number
                        masterAuto.Aut_start_char = "PRO";
                        masterAuto.Aut_year = null;
                    }
                    else
                    {
                        masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                        masterAuto.Aut_cate_tp = "COM";
                        masterAuto.Aut_direction = null;
                        masterAuto.Aut_modify_dt = null;
                        masterAuto.Aut_moduleid = "PRICE";
                        masterAuto.Aut_number = 5;//what is Aut_number
                        masterAuto.Aut_start_char = "NOR";
                        masterAuto.Aut_year = null;
                    }

                    foreach (PriceDetailRef item in _list)
                    {
                        item.Sapd_price_stus = "P";
                    }


                    string _err = "";
                    row_aff = (Int32)CHNLSVC.Sales.AmendPromotion(_promoList, Session["UserID"].ToString(), Convert.ToDateTime(txtPToDate.Text).Date, Session["SessionID"].ToString(), _list, _comList, masterAuto, out _err, txtPPriceBook.Text.ToUpper(), txtPPriceLevel.Text.ToUpper(), txtPcir.Text.ToUpper(), lstPromoI);

                    if (row_aff > 0)
                    {
                        DisplayMessage("Promotion amended.", 3);
                       
                          ClearNewPriceCreation();

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_err))
                        {
                            DisplayMessage("_err", 4);
                        }
                        else
                        {
                            DisplayMessage("Failed to update.", 4);                   
                        }
                    }
                }
                else
                {
                    DisplayMessage("No any applicable promotions are selected.", 2);                   
                    return;
                }

            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 3);         
            }
           
        }


        protected void lbtnpCRemove_Click(object sender, EventArgs e)
        {
            if (txtconfirmdelete.Value == "No")
            {
                return;
            }
            if (grdAppPC.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                _appPcList = ViewState["_appPcList"] as List<PriceProfitCenterPromotion>;

                if (_appPcList != null)
                {
                    string _pc = (row.FindControl("col_a_Pc") as Label).Text;
                    var _filter = _appPcList.Find(x => x.Srpr_pc == _pc);
                    _appPcList.Remove(_filter);
                    grdAppPC.DataSource = _appPcList;
                    grdAppPC.DataBind();
                    ViewState["_appPcList"] = _appPcList;
                    mpAddCostItems.Show();
                }
            }
        }

        protected void lbtnClearProfitCenter_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnClose3_Click(object sender, EventArgs e)
        {

            lblalert.Text = "";
            lblsuccess.Text = "";
            lblsuccess.Visible = false;
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
            excelUpload.Hide();
        }

    }
}