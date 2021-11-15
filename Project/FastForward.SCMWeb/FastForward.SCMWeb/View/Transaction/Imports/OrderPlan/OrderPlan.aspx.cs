using FF.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.General;


namespace FastForward.SCMWeb.View.Transaction.Order_Plan
{
    public partial class OrderPlan : BasePage
    {
        #region add by lakshan 24Nov2017
        OrderPlanExcelUploader _ordPlnUpItm = new OrderPlanExcelUploader();
        List<OrderPlanExcelUploader> _ordPlnUpItmList
        {
            get { if (Session["_ordPlnUpItmList"] != null) { return (List<OrderPlanExcelUploader>)Session["_ordPlnUpItmList"]; } else { return new List<OrderPlanExcelUploader>(); } }
            set { Session["_ordPlnUpItmList"] = value; }
        }
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
        protected List<ImportsBLContainer> oImportsBLContainers { get { return (List<ImportsBLContainer>)Session["oImportsBLContainers"]; } set { Session["oImportsBLContainers"] = value; } }
        protected Int32 _tempLine { get { return (Int32)Session["_tempLine"]; } set { Session["_tempLine"] = value; } }
        protected Int32 _maxLine { get { return (Int32)Session["_maxLine"]; } set { Session["_maxLine"] = value; } }
        private int _MinYear;
        private int _MaxYear;
        DataTable dtorditems = new DataTable();
        string _userid = string.Empty;
        Decimal exchangerate;
        Int32 SuccessOPItemsSave;
        Int32 SuccessOPItemsEdit;
        List<int> successItems = new List<int>();
        List<int> successItemsEdit = new List<int>();
        DataTable dtports = new DataTable();
        DataTable uniqueCols = new DataTable();
        string ioiline = string.Empty;
        string refline = string.Empty;
        string fline = string.Empty;
        string colouritem = string.Empty;
        string qtyitem = string.Empty;
        string priceitem = string.Empty;
        string sequenceitem = string.Empty;
        Int32 yearitem;
        Int32 monthitem;
      //  List<ImportsBLContainer> orderplancontanierForExcel = new List<ImportsBLContainer>();
        public int MinYear
        {
            get { return _MinYear; }
            set { _MinYear = value; }
        }
        public int MaxYear
        {
            get { return _MaxYear; }
            set { _MaxYear = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    _ordPlnUpItm = new OrderPlanExcelUploader();
                    _ordPlnUpItmList = new List<OrderPlanExcelUploader>();
                    if (string.IsNullOrEmpty(Session["UserSBU"].ToString()))
                    {
                        lblSbuMsg1.Text = "SBU (Strategic Business) is not allocate for your login ID.";
                        lblSbuMsg2.Text = "There is not setup default SBU (Sttre Buds Unit) for your login ID.";
                        SbuPopup.Show();
                    }
                    txtmanualref.Focus();
                    grdorderdetails.DataSource = new int[] { };
                    grdorderdetails.DataBind();
                    dgvContainers.DataSource = new int[] { };
                    dgvContainers.DataBind();
                    grdkititems.DataSource = null;
                    grdkititems.DataBind();
                    loadContainerType();
                    loadContainerTypeForDetail();
                    PopulateDropDowns();
                    _tempLine = 0;
                    DateTime orddate = DateTime.Now;
                    txtdate.Text = orddate.ToString("dd/MMM/yyyy");

                    ViewState["ItemsTable"] = null;

                    DataTable dtitems = new DataTable();
                    dtitems.Columns.AddRange(new DataColumn[19] { new DataColumn("Item",typeof(string)), new DataColumn("Description",typeof(string)), 
                        new DataColumn("Brand",typeof(string)), new DataColumn("Model",typeof(string)), new DataColumn("Colour",typeof(string)), new DataColumn("PartNo",typeof(string)), 
                        new DataColumn("UOM",typeof(string)), new DataColumn("Type",typeof(string)), 
                        new DataColumn("Ord Qty",typeof(decimal)), new DataColumn("Unit Rate",typeof(decimal)), new DataColumn("Value",typeof(decimal)), 
                        new DataColumn("Ioi Line",typeof(Int32)), new DataColumn("KitItemCode",typeof(string)), new DataColumn("Year",typeof(Int32)), new DataColumn("Month",typeof(Int32)), 
                        new DataColumn("Tag",typeof(string)), new DataColumn("TagName",typeof(string)), new DataColumn("ProName",typeof(string)), new DataColumn("Ioi Line_temp",typeof(int)) });
                    ViewState["ItemsTable"] = dtitems;
                    this.BindGrid();

                    DateTime todate = DateTime.Now.AddMonths(-1);
                    txtFDate.Text = todate.ToString("dd/MMM/yyyy");

                    DateTime date = DateTime.Now;
                    txtTDate.Text = date.ToString("dd/MMM/yyyy");

                    lbltotordqty.Text = Convert.ToDecimal(0).ToString("#,##0.00");
                    lbltotordval.Text = Convert.ToDecimal(0).ToString("N5");

                    txtddlYear.Text = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                    Session["oImportsBLContainers"] = null;
                    oImportsBLContainers = new List<ImportsBLContainer>();
                    BindContainers();
                }

                //if (!string.IsNullOrEmpty(txtordno.Text.Trim()))
                //{
                //    if (chkteplate.Checked != true)
                //    {
                //        lbtnadditems.Visible = false;
                //    }
                //}
                //else
                //{
                //    lbtnadditems.Visible = true;
                //}

                string sessioop = (string)Session["OP_SEARCH"];

                if (!string.IsNullOrEmpty(sessioop))
                {
                    UserDPopoup.Show();
                }
                else
                {
                    UserDPopoup.Hide();
                }
                #region add by lakshan 24Nov2017
                if (IsPostBack)
                {
                    if (_showExcelPop)
                    {
                        popupExcel.Show();
                    }
                    else
                    {
                        popupExcel.Hide();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private List<string> finalColorList()
        {

            string[] allColors = Enum.GetNames(typeof(System.Drawing.KnownColor));
            string[] systemEnvironmentColors = new string[(typeof(System.Drawing.SystemColors)).GetProperties().Length];

            int index = 0;

            foreach (MemberInfo member in (typeof(System.Drawing.SystemColors)).GetProperties())
            {
                systemEnvironmentColors[index++] = member.Name;
            }

            List<string> finalColorList = new List<string>();

            foreach (string color in allColors)
            {
                if (Array.IndexOf(systemEnvironmentColors, color) < 0)
                {
                    finalColorList.Add(color);
                }
            }

            return finalColorList;

        }
        private void PopulateDropDowns()
        {
            try
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                    ddlMonth.Items.Add(new ListItem(monthName, month.ToString().PadLeft(1, '0')));
                }

                ddlMonth.Items.Insert(0, new ListItem("Select", ""));
                ddlMonth.SelectedIndex = 0;

                ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
                ddlitemtype.Items.Insert(1, new ListItem("Main", "M"));
                ddlitemtype.Items.Insert(2, new ListItem("Component", "C"));
                ddlitemtype.Items.Insert(3, new ListItem("Accessories", "A"));

                ddlmodeofshipment.Items.Insert(0, new ListItem("Select", "0"));
                ddlmodeofshipment.Items.Insert(1, new ListItem("Sea", "S"));
                ddlmodeofshipment.Items.Insert(2, new ListItem("Air", "A"));

                string SearchParamsTT = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
                DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParamsTT, null, null);
                ddltradeterm.DataSource = result;
                ddltradeterm.DataTextField = "CODE";
                ddltradeterm.DataValueField = "CODE";
                if (result.Rows.Count > 0)
                {
                    ddltradeterm.DataBind();
                }
                ddltradeterm.Items.Insert(0, new ListItem("Select", "0"));


                DataTable dtcolour = CHNLSVC.CommonSearch.LoadColours();
                ddlMultiColor.DataSource = dtcolour;
                ddlMultiColor.DataTextField = "clr_desc";
                ddlMultiColor.DataValueField = "clr_cd";
                if (dtcolour.Rows.Count > 0)
                {
                    ddlMultiColor.DataBind();
                }
                ddlMultiColor.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void Clear()
        {
            try
            {
                lbtnrate.Text = string.Empty;
                txtdate.Text = string.Empty;
                txtordno.Text = string.Empty;
                txtmanualref.Text = string.Empty;
                txtsupplier.Text = string.Empty;
                txtdescription.Text = string.Empty;
                ddltradeterm.SelectedIndex = 0;

                ddlportoforigin.DataSource = null;
                ddlportoforigin.DataBind();
                if (ddlportoforigin.Items.Count > 0)
                {
                    ddlportoforigin.SelectedIndex = 0;
                }
                txteta.Text = string.Empty;
                lbltotordqty.Text = string.Empty;
                lblcurrency.Text = string.Empty;
                txtitem.Text = string.Empty;
                lblitmdescription.Text = string.Empty;
                lblbrandit.Text = string.Empty;
                txtmodel.Text = string.Empty;
                ddlMultiColor.SelectedIndex = 0;
                lbluomit.Text = string.Empty;
                ddlitemtype.SelectedIndex = 0;
                txtqty.Text = string.Empty;
                txtunitrate.Text = string.Empty;
                lbltotordval.Text = string.Empty;
                lblcif.Text = string.Empty;
                lblportoforigin.Text = string.Empty;
                lblleaddays.Text = string.Empty;
                ddlitemtype.SelectedIndex = 0;
                ddlmodeofshipment.SelectedIndex = 0;
                ddlMonth.SelectedIndex = 0;
                txtddlYear.Text = string.Empty;
                grdorderdetails.DataSource = null;
                grdorderdetails.DataBind();
                divalert.Visible = false;
                chkteplate.Checked = false;
                lbtnadditems.Visible = true;
                Divinfo.Visible = false;
                DateTime orddateclr = DateTime.Now;
                txtdate.Text = orddateclr.ToString("dd/MMM/yyyy");
                lblsupplier.Text = string.Empty;
                chkteplate.Checked = false;
                lbtnsupplier.Visible = true;

                SessionClear();
                Session["Se_ref"] = false;
                ViewState["ItemsTable"] = null;
                ViewState["KITItemsTable"] = null;

                DataTable dtitems = new DataTable();
                dtitems.Columns.AddRange(new DataColumn[18] { new DataColumn("Item",typeof(string)), new DataColumn("Description",typeof(string)), 
                        new DataColumn("Brand",typeof(string)), new DataColumn("Model",typeof(string)), new DataColumn("Colour",typeof(string)), 
                        new DataColumn("UOM",typeof(string)), new DataColumn("Type",typeof(string)), 
                        new DataColumn("Ord Qty",typeof(decimal)), new DataColumn("Unit Rate",typeof(decimal)), new DataColumn("Value",typeof(decimal)), 
                        new DataColumn("Ioi Line",typeof(Int32)), new DataColumn("KitItemCode",typeof(string)), new DataColumn("Year",typeof(Int32)), new DataColumn("Month",typeof(Int32)), 
                        new DataColumn("Tag",typeof(string)), new DataColumn("TagName",typeof(string)), new DataColumn("ProName",typeof(string)), new DataColumn("Ioi Line_temp",typeof(int)) });
                ViewState["ItemsTable"] = dtitems;
                this.BindGrid();

                lblmodeship.Text = string.Empty;
                ddlMultiColor.SelectedIndex = 0;

                txtddlYear.Text = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                Session["UNITINVALID"] = null;
                Session["UPDATEQTY"] = null;
                Session["OP_SEARCH"] = null;
                UserDPopoup.Hide();
                Session["oImportsBLContainers"] = null;
                oImportsBLContainers = new List<ImportsBLContainer>();
                BindContainers();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            try
            {
                string a = ddlMonth.SelectedValue;
                string b = txtddlYear.Text;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtndivokclose_Click(object sender, EventArgs e)
        {
            divok.Visible = false;
        }
        protected void lbtndicalertclose_Click(object sender, EventArgs e)
        {
            divalert.Visible = false;
        }
        protected void lbtndivinfoclose_Click(object sender, EventArgs e)
        {
            Divinfo.Visible = false;
        }
        protected void lbtnClear_Click1(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    Clear();
                    divalert.Visible = false;
                    Divinfo.Visible = false;
                    divok.Visible = false;
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
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
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void dvResultUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                UserPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void dvResultUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdkititems.DataSource = null;
                grdkititems.DataBind();

                Divinfo.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                if (lblvalue.Text == "401")
                {
                    txtsupplier.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadSupplierDetails();
                }
                if (lblvalue.Text == "403")
                {
                    txtordno.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadOrderHeader();
                    LoadOrderHeaderItems();
                    ExcRate();
                    oImportsBLContainers = CHNLSVC.Financial.GETORDER_CONTENER(txtordno.Text);
                    BindContainers();
                    //if (!string.IsNullOrEmpty(txtordno.Text.Trim()))
                    //{
                    //    lbtnadditems.Visible = false;
                    //}
                    //else
                    //{
                    //    lbtnadditems.Visible = true;
                    //}

                    foreach (GridViewRow myrowitem in grdorderdetails.Rows)
                    {
                        Decimal ordqty = Convert.ToDecimal(myrowitem.Cells[10].Text);
                        myrowitem.Cells[10].Text = DoFormat(ordqty);

                        Decimal unitrate = Convert.ToDecimal(myrowitem.Cells[11].Text);
                        myrowitem.Cells[11].Text = DoFormat(unitrate);

                        Decimal value = Convert.ToDecimal(myrowitem.Cells[12].Text);
                        myrowitem.Cells[12].Text = DoFormat(value);
                    }
                }
                if (lblvalue.Text == "406")
                {
                    txtmodel.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "407")
                {
                    txtitem.Text = grdResult.SelectedRow.Cells[2].Text;
                    DataTable dt = CHNLSVC.Financial.GetItemDetails(txtitem.Text);

                    if (dt.Rows[0]["MI_ITM_TP"].ToString() == "K")
                    {
                        if (ViewState["KITItemsTable"] != null)
                        {
                            DataTable dtcopy = ViewState["KITItemsTable"] as DataTable;
                            dtcopy.Merge(dt);

                            ViewState["KITItemsTable"] = dtcopy;
                            Session["KITITEM"] = txtitem.Text.Trim();
                            Divinfo.Visible = true;
                            lblinfo.Text = "This is a KIT Item";
                        }
                        else
                        {
                            Session["KITITEM"] = txtitem.Text.Trim();
                            Divinfo.Visible = true;
                            lblinfo.Text = "This is a KIT Item";

                            DataTable dtKITitems = new DataTable();
                            dtKITitems = dt.Copy();

                            ViewState["KITItemsTable"] = dtKITitems;
                        }
                        txtunitrate.Enabled = false;
                        txtunitrate.Text = string.Empty;
                    }
                    else
                    {
                        txtunitrate.Enabled = true;
                    }

                    txtmodel.Text = dt.Rows[0]["MI_MODEL"].ToString();
                    lblitmdescription.Text = dt.Rows[0]["MI_SHORTDESC"].ToString();
                    lblbrandit.Text = dt.Rows[0]["MI_BRAND"].ToString();
                    lbluomit.Text = dt.Rows[0]["MI_ITM_UOM"].ToString();

                    if (ddlMultiColor.Items.FindByValue(dt.Rows[0]["MI_COLOR_EXT"].ToString()) != null)
                    {
                        ddlMultiColor.SelectedValue = dt.Rows[0]["MI_COLOR_EXT"].ToString();
                    }
                    else
                    {
                        ddlMultiColor.SelectedItem.Text = "N/A";
                    }

                    if (ddlitemtype.Items.FindByValue(dt.Rows[0]["MI_ITM_TP"].ToString()) != null)
                    {
                        ddlitemtype.SelectedValue = dt.Rows[0]["MI_ITM_TP"].ToString();
                    }
                    else
                    {
                        ddlitemtype.SelectedIndex = 0;
                    }
                }
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S" + seperator + "F" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TradeTerms:
                    {
                        paramsText.Append("TOT" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OrderPlanNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportModel:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(txtsupplier.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportItem:
                    {
                        paramsText.Append(txtmodel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat1:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat1.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
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

        public void BindUCtrlDDLDataSupplier(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 1;
        }
        public void BindUCtrlDDLDataOP(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if ((col.ColumnName != "DATE") && (col.ColumnName != "Status"))
                {
                    this.ddlSearchbykeyD.Items.Add(col.ColumnName);
                }
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }
        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "407")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchItemforchange(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.ToUpper());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "407";
                    ViewState["SEARCH"] = result;
                    UserPopup.Show();
                    txtSearchbyword.Focus();
                }
                //Supplier
                else if (lblvalue.Text == "401")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                    DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "401";
                    ViewState["SEARCH"] = result;
                    UserPopup.Show();
                    txtSearchbyword.Focus();
                }
                //Order Plan No
                else if (lblvalue.Text == "403")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
                    DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNoByStatus(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "403";
                    ViewState["SEARCH"] = result;
                    UserPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);
                    string searchParameter = ddlSearchbykey.Text;
                    dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";

                    result = dv.ToTable();

                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    UserPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnsupplier_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "401";
                BindUCtrlDDLDataSupplier(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                txtSearchbyword.Focus();
                txteta.Text = string.Empty;
                lblleaddays.Text = string.Empty;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private bool ValidateUnitRate()
        {
            if (string.IsNullOrEmpty(txtunitrate.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "Please select unit rate !!!";
                txtunitrate.Focus();
                return false;
            }
            return true;
        }
        private bool ValidateItemsEntry()
        {
            if (string.IsNullOrEmpty(txtddlYear.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "Please enter the Year !!!";
                txtddlYear.Focus();
                return false;
            }

            if (ddlMonth.SelectedIndex == 0)
            {
                divalert.Visible = true;
                lblalert.Text = "Please select month !!!";
                ddlMonth.Focus();
                return false;
            }

            DateTime selectedyear = Convert.ToDateTime(txtdate.Text.Trim());
            Int32 selectedyearValue = selectedyear.Year;
            Int32 selectedMonthValue = selectedyear.Month;

            //comment by Rukshan--request chandima mail(03-jan-2017)
            //if (Convert.ToInt32(txtddlYear.Text.Trim()) < selectedyearValue)
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Year can not be a past year !!!";
            //    txtddlYear.Focus();
            //    return false;
            //}

            //if ((selectedyearValue == Convert.ToInt32(txtddlYear.Text.Trim())) && (Convert.ToInt32(ddlMonth.SelectedValue) < selectedMonthValue))
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Month can not be a past month !!!";
            //    ddlMonth.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtmodel.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "Please select model !!!";
                txtmodel.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(lblitmdescription.Text.Trim()))
            {
                //divalert.Visible = true;
                //lblalert.Text = "Item description can not be empty !!!";
                //lblitmdescription.Focus();
                //return false;
                txtitem.Text = "N/A";
            }

            //if (string.IsNullOrEmpty(lblbrandit.Text.Trim()))
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Brand can not be empty !!!";
            //    lblbrandit.Focus();
            //    return false;
            //}

            if (ddlitemtype.SelectedIndex == 0)
            {
                divalert.Visible = true;
                lblalert.Text = "Please select item type !!!";
                ddlitemtype.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtqty.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "Please select quantity !!!";
                txtqty.Focus();
                return false;
            }

            string kititem = (string)Session["KITITEM"];
            if (string.IsNullOrEmpty(kititem))
            {
                if (string.IsNullOrEmpty(txtunitrate.Text.Trim()))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select unit rate !!!";
                    txtunitrate.Focus();
                    return false;
                }
            }

            //if (string.IsNullOrEmpty(lbluomit.Text.Trim()))
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "UOM can not be empty !!!";
            //    lbluomit.Focus();
            //    return false;
            //}

            return true;
        }
        private bool ValidateAddEntry()
        {
            if (string.IsNullOrEmpty(txtmanualref.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "Please enter manual ref # !!!";
                txtmanualref.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtsupplier.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "Please select supplier !!!";
                txtsupplier.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtdate.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "Please select order date !!!";
                txtdate.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(txtdescription.Text.Trim()))
            //{
            //    txtdescription.Text = "&nbsp;";
            //    txtdescription.ForeColor = Color.White;
            //}
            //else
            //{
            //    if (txtdescription.Text == "&nbsp;")
            //    {
            //        txtdescription.ForeColor = Color.White;
            //    }
            //    else
            //    {
            //        txtdescription.ForeColor = Color.Black;
            //    }
            //}

            if (ddltradeterm.SelectedIndex == 0)
            {
                divalert.Visible = true;
                lblalert.Text = "Please select trade term !!!";
                ddltradeterm.Focus();
                return false;
            }

            if (ddlmodeofshipment.SelectedIndex == 0)
            {
                divalert.Visible = true;
                lblalert.Text = "Please select mode of shipment !!!";
                ddlmodeofshipment.Focus();
                return false;
            }

            if (ddlportoforigin.SelectedIndex == 0)
            {
                divalert.Visible = true;
                lblalert.Text = "Please select port of origin !!!";
                ddlportoforigin.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txteta.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "ETA should be availabe in order to proceed !!!";
                txteta.Focus();
                return false;
            }

            string suppliercurrency = (string)Session["SUPPLER_CURRENCY"];
            if (string.IsNullOrEmpty(suppliercurrency))
            {
                divalert.Visible = true;
                lblalert.Text = "Unable to continue the process,the supplier is not assigned with a currency !!!";
                txtunitrate.Focus();
                return false;
            }

            if (grdorderdetails.Rows.Count == 0)
            {
                divalert.Visible = true;
                lblalert.Text = "Please add order items !!!";
                txtitem.Focus();
                return false;
            }

            return true;
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
        protected void lbtnadditems_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtsupplier.Text.ToString()))
                {
                    string msg = "Please Select Supplier";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtqty.Text))
                {
                    string msg = "Please enter qty !!!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    return;
                }
                decimal _qty = 0;
                if (!decimal.TryParse(txtqty.Text, out _qty))
                {
                    string msg = "Please enter valid qty !!!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    return;
                }
                if (Convert.ToDecimal(txtqty.Text) <= 0)
                {
                    string msg = "Please enter valid qty !!!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    return;
                }
                if (DropDownListContainer.SelectedIndex != 0)
                {
                    //DropDownListContainer.SelectedItem.ToString();
                    if(!(string.IsNullOrEmpty(TextBoxContainerQty.Text)))
                    {
                        if(Convert.ToDecimal(TextBoxContainerQty.Text)<1)
                        {
                            TextBoxContainerQty.Text = "0";
                            string msg = "Please enter valid container qty !!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                            return;
                        }
                    }else
                    {
                        TextBoxContainerQty.Text = "0";
                        string msg = "Please enter valid container qty !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                        return;                      
                    }
                }
                if (!(string.IsNullOrEmpty(TextBoxContainerQty.Text.Trim())))
                {
                    
                }
                divalert.Visible = false;
                string _item = txtitem.Text.ToString();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + _item);
                if (_result.Rows.Count > 0)
                {
                    bool isvaliditems = ValidateItemsEntry();
                    if (isvaliditems == false)
                    {
                        return;
                    }

                    InsertToGrid();

                    string sessionval = (string)Session["UNITINVALID"];
                    string sessioqty = (string)Session["UPDATEQTY"];

                    if (!string.IsNullOrEmpty(sessionval))
                    {
                        return;
                    }

                    if (!string.IsNullOrEmpty(sessioqty))
                    {
                        return;
                    }

                    DataTable dtqty = ViewState["KITItemsTable"] as DataTable;

                    if (dtqty != null)
                    {
                        if ((dtqty.Rows.Count > 0) && (txtunitrate.Enabled == false))
                        {
                            Int32 rowlinemodify = dtqty.Rows.Count - 1;
                            dtqty.Rows[rowlinemodify].SetField("Qty", txtqty.Text.Trim());
                        }
                    }

                    txtmodel.Text = string.Empty;
                    txtitem.Text = string.Empty;
                    ddlitemtype.SelectedIndex = 0;
                    txtqty.Text = string.Empty;
                    txtunitrate.Text = string.Empty;

                    foreach (GridViewRow myrowitem in grdorderdetails.Rows)
                    {
                        //Decimal ordqty = Convert.ToDecimal(myrowitem.Cells[10].Text);
                        //myrowitem.Cells[10].Text = ordqty.ToString();

                        //Decimal unitrate = Convert.ToDecimal(myrowitem.Cells[11].Text);
                        //myrowitem.Cells[11].Text = DoFormat(unitrate);

                        //Decimal value = Convert.ToDecimal(myrowitem.Cells[12].Text);
                        //myrowitem.Cells[12].Text = DoFormat(value);
                    }
                    lblitmdescription.Text = string.Empty;
                    lblbrandit.Text = string.Empty;
                    lbluomit.Text = string.Empty;
                    DropDownListContainer.SelectedIndex = 0;
                    TextBoxContainerQty.Text = string.Empty;
                    Session["UNITINVALID"] = null;
                    Session["UPDATEQTY"] = null;
                }
                else
                {
                    string msg = "Please allocate this item : " + _item + " for supplier";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                }


            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void ddltradeterm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddltradeterm.Text == "C&F")
                {
                    lblcif.Text = "Cost and Freight";
                }
                else if (ddltradeterm.Text == "CIF")
                {
                    lblcif.Text = "Cost, Insurance and Freight";
                }
                else if (ddltradeterm.Text == "FOB")
                {
                    lblcif.Text = "Free On Board";
                }
                else
                {
                    lblcif.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void ddlportoforigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetEta();

                if (!string.IsNullOrEmpty(lblleaddays.Text.Trim()))
                {
                    DateTime ETA = Convert.ToDateTime(txtdate.Text).AddDays(Convert.ToInt32(lblleaddays.Text));
                    txteta.Text = ETA.ToString("dd/MMM/yyyy");
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void GetEta()
        {
            try
            {
                DataTable dtETA = CHNLSVC.Financial.GetSupplierETA(Session["UserCompanyCode"].ToString(), txtsupplier.Text, ddlportoforigin.SelectedValue);
                if (dtETA.Rows.Count > 0)
                {
                    foreach (DataRow item in dtETA.Rows)
                    {
                        lblleaddays.Text = item[0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private decimal GetExchangeRate()
        {
            try
            {
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), lblcurrency.Text, DateTime.Now, _pc.Mpc_def_exrate, string.Empty);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                }
                return _exchangRate;

            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
                return 0;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtconfirmplaceord.Value == "Yes")
            {
                   if(dgvContainers.Rows.Count<1)
                   {
                       popupContainer.Show();
                   }
                   else
                   { saveOrderPlan(); }
            }
        }
        private void saveOrderPlan()
        {
            try
            {
                    divalert.Visible = false;
                    Divinfo.Visible = false;

                    string opstatus = (string)Session["OPSTATUS"];

                    if (chkteplate.Checked == false)
                    {
                        if (opstatus == "A")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "You are not allowed to amend Approved orders !!!";
                            return;
                        }

                        if (opstatus == "F")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "You are not allowed to amend Finished orders !!!";
                            return;
                        }

                        if (opstatus == "C")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "You are not allowed to amend Cancelled orders !!!";
                            return;
                        }
                    }

                    bool isvalid = ValidateAddEntry();
                    if (isvalid == false)
                    {
                        return;
                    }

                    if (grdorderdetails.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please select items for the order !!!";
                        txtddlYear.Focus();
                        return;
                    }

                    if (!string.IsNullOrEmpty(lblcurrency.Text.Trim()))
                    {
                        exchangerate = GetExchangeRate();
                    }

                    if (string.IsNullOrEmpty(txtordno.Text.Trim()))
                    {
                        SaveOPHeader();
                    }
                    else
                    {
                        if (chkteplate.Checked == true)
                        {
                            SaveOPHeader();
                        }
                        else
                        {
                            _userid = (string)Session["UserID"];
                            Int32? IO_SEND = null;
                            DateTime? IO_SEND_DT = null;

                            string amaendtime = (string)Session["AMENDSEQ"];
                            Int32 Amanndvalue = Convert.ToInt32(amaendtime);
                            string sequencenum = (string)Session["OPSEQNO"];
                            string SBU_Character = Session["UserSBU"].ToString();

                            OrderPlanHeader OrderPlanHeader = new OrderPlanHeader();

                            OrderPlanHeader.IO_OP_NO = txtordno.Text.Trim();
                            OrderPlanHeader.IO_REF_NO = txtmanualref.Text.Trim();
                            OrderPlanHeader.IO_OP_DT = Convert.ToDateTime(txtdate.Text.Trim());
                            OrderPlanHeader.IO_COM = Session["UserCompanyCode"].ToString();
                            OrderPlanHeader.IO_SBU = SBU_Character;
                            OrderPlanHeader.IO_SUPP = txtsupplier.Text.Trim();
                            OrderPlanHeader.IO_TP = "S";
                            OrderPlanHeader.IO_RMK = txtdescription.Text.Trim();
                            OrderPlanHeader.IO_CUR = lblcurrency.Text;
                            OrderPlanHeader.IO_EX_RT = Convert.ToDecimal(exchangerate);
                            OrderPlanHeader.IO_TOP_CAT = "TOT";
                            OrderPlanHeader.IO_TOP = ddltradeterm.SelectedValue;
                            OrderPlanHeader.IO_TOS = ddlmodeofshipment.SelectedValue;
                            OrderPlanHeader.IO_FRM_PORT = ddlportoforigin.SelectedValue;
                            OrderPlanHeader.IO_TO_PORT = "CMB";
                            OrderPlanHeader.IO_ETA_DT = Convert.ToDateTime(txteta.Text.Trim());
                            OrderPlanHeader.IO_IS_KIT = 0;
                            OrderPlanHeader.IO_STUS = "S";
                            OrderPlanHeader.IO_SEND = Convert.ToInt32(IO_SEND);
                            OrderPlanHeader.IO_SEND_DT = Convert.ToDateTime(IO_SEND_DT);
                            OrderPlanHeader.IO_AMD_SEQ = Convert.ToInt32(Amanndvalue + 1);
                            OrderPlanHeader.IO_TOT_QTY = Convert.ToDecimal(lbltotordqty.Text);
                            OrderPlanHeader.IO_TOT_AMT = Convert.ToDecimal(lbltotordval.Text);
                            OrderPlanHeader.IO_ANAL_1 = string.Empty;
                            OrderPlanHeader.IO_ANAL_2 = string.Empty;
                            OrderPlanHeader.IO_ANAL_3 = string.Empty;
                            OrderPlanHeader.IO_ANAL_4 = string.Empty;
                            OrderPlanHeader.IO_ANAL_5 = string.Empty;
                            OrderPlanHeader.IO_MOD_BY = _userid;
                            OrderPlanHeader.IO_MOD_DT = DateTime.Now.Date;
                            OrderPlanHeader.IO_SESSION_ID = Session["SessionID"].ToString();
                            OrderPlanHeader.IO_SEQ_NO = Convert.ToInt32(sequencenum);

                            System.Threading.Thread.Sleep(1000);

                            Int32 outopno = CHNLSVC.Financial.UpdateOrderHeader(OrderPlanHeader);

                            UpdateOPItems();

                            if ((outopno == 1) && (!successItemsEdit.Contains(-1)))
                            {
                                divalert.Visible = false;
                                divok.Visible = true;
                                lblok.Text = "Successfully saved";
                                Clear();

                                //if (!string.IsNullOrEmpty(txtordno.Text.Trim()))
                                //{
                                //    lbtnadditems.Visible = false;
                                //}
                                //else
                                //{
                                //    lbtnadditems.Visible = true;
                                //}
                            }
                            else
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Error Occurred while processing";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
        }
        private void ExcRate()
        {
            //MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            //MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), lblcurrency.Text.ToString(), DateTime.Now, _pc.Mpc_def_exrate, string.Empty);
            //if (_exc1 != null)
            //{

            //    lbtnrate.Text = _exc1.Mer_bnkbuy_rt.ToString();
            //}
            DataTable ERateTbl = CHNLSVC.Financial.GetExchangeRate(Session["UserCompanyCode"].ToString(), lblcurrency.Text, "LKR");
            if (ERateTbl != null)
            {
                if (ERateTbl.Rows.Count > 0)
                {
                    lbtnrate.Text = ERateTbl.Rows[0][5].ToString();

                }
                else
                {
                    divalert.Visible = true;
                    lblalert.Text = "Exchange rate not setup";
                }
            }
            else
            {
                divalert.Visible = true;
                lblalert.Text = "Currency not setup";
            }
        }
        private void SessionClear()
        {
            try
            {
                Session["SUPPLER_CURRENCY"] = null;
                Session["AMENDSEQ"] = null;
                Session["OPSEQNO"] = null;
                Session["KITITEM"] = null;
                Session["OPNO"] = null;
                Session["OPSTATUS"] = null;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void SaveOPHeader()
        {
            try
            {
                _userid = (string)Session["UserID"];
                Int32? IO_SEND = null;
                DateTime? IO_SEND_DT = null;
                string iskititem = (string)Session["KITITEM"];
                string kituses = string.Empty;
                if (!string.IsNullOrEmpty(iskititem))
                {
                    kituses = "1";
                }
                else
                {
                    kituses = "0";
                }

                string SBU_Character = Session["UserSBU"].ToString();
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = null;
                mastAutoNo.Aut_modify_dt = null;
                mastAutoNo.Aut_moduleid = "OP";
                mastAutoNo.Aut_start_char = "ORP";
                mastAutoNo.Aut_year = DateTime.Now.Year;

                OrderPlanHeader OrderPlanHeader = new OrderPlanHeader();
                OrderPlanHeader.IO_OP_NO = "";
                OrderPlanHeader.IO_REF_NO = txtmanualref.Text.Trim();
                OrderPlanHeader.IO_OP_DT = Convert.ToDateTime(txtdate.Text.Trim());
                OrderPlanHeader.IO_COM = Session["UserCompanyCode"].ToString();
                OrderPlanHeader.IO_SBU = SBU_Character;
                OrderPlanHeader.IO_SUPP = txtsupplier.Text.Trim();
                OrderPlanHeader.IO_TP = "S";
                OrderPlanHeader.IO_RMK = txtdescription.Text.Trim();
                OrderPlanHeader.IO_CUR = lblcurrency.Text.Trim();
                OrderPlanHeader.IO_EX_RT = Convert.ToDecimal(exchangerate);
                OrderPlanHeader.IO_TOP_CAT = "TOT";
                OrderPlanHeader.IO_TOP = ddltradeterm.SelectedValue;
                OrderPlanHeader.IO_TOS = ddlmodeofshipment.SelectedValue;
                OrderPlanHeader.IO_FRM_PORT = ddlportoforigin.SelectedValue;
                OrderPlanHeader.IO_TO_PORT = "CMB";
                OrderPlanHeader.IO_ETA_DT = Convert.ToDateTime(txteta.Text.Trim());
                OrderPlanHeader.IO_IS_KIT = Convert.ToInt32(kituses);
                OrderPlanHeader.IO_STUS = "S";
                OrderPlanHeader.IO_SEND = Convert.ToInt32(IO_SEND);
                OrderPlanHeader.IO_SEND_DT = Convert.ToDateTime(IO_SEND_DT);
                OrderPlanHeader.IO_AMD_SEQ = 0;
                OrderPlanHeader.IO_TOT_QTY = Convert.ToDecimal(lbltotordqty.Text);
                OrderPlanHeader.IO_TOT_AMT = Convert.ToDecimal(lbltotordval.Text);
                OrderPlanHeader.IO_ANAL_1 = string.Empty;
                OrderPlanHeader.IO_ANAL_2 = string.Empty;
                OrderPlanHeader.IO_ANAL_3 = string.Empty;
                OrderPlanHeader.IO_ANAL_4 = string.Empty;
                OrderPlanHeader.IO_ANAL_5 = string.Empty;
                OrderPlanHeader.IO_CRE_BY = _userid;
                OrderPlanHeader.IO_CRE_DT = DateTime.Now.Date;
                OrderPlanHeader.IO_MOD_BY = _userid;
                OrderPlanHeader.IO_MOD_DT = Convert.ToDateTime(IO_SEND_DT);
                OrderPlanHeader.IO_SESSION_ID = Session["SessionID"].ToString();

                System.Threading.Thread.Sleep(1000);

                Tuple<int, string> outopno = CHNLSVC.Financial.PlaceOrder(OrderPlanHeader, mastAutoNo, oImportsBLContainers);

                string newseqno = outopno.Item1.ToString();
                string outputopno = outopno.Item2.ToString();

                Session["OPSEQNO"] = newseqno;
                Session["OPNO"] = outputopno;

                SaveOPItems();

                if (!string.IsNullOrEmpty(iskititem))
                {
                    SaveKitItems();
                }

                if ((Convert.ToInt32(newseqno) > 0) && (!successItems.Contains(-1)))
                {
                    string OPNOMSG = (string)Session["OPNO"];

                    divalert.Visible = false;
                    divok.Visible = true;
                    lblok.Text = "Successfully created order " + OPNOMSG;
                    Clear();
                }
                else
                {
                    divalert.Visible = true;
                    lblalert.Text = "Error Occurred while processing";
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }


        private void SaveOPItems()
        {
            try
            {
                string opseqno = (string)(Session["OPSEQNO"]);
                string OPNO = (string)Session["OPNO"];
                _userid = (string)Session["UserID"];
                Int32? KIT_LINE = null;
                DateTime? IO_SEND_DT = null;
                string iskititem = (string)Session["KITITEM"];
                OrderPlanItem OrderPlanItems = new OrderPlanItem();

                foreach (GridViewRow ddritem in grdorderdetails.Rows)
                {
                    string itemtype = string.Empty;
                    Int32 gridline = ddritem.RowIndex + 1;
                    string kititemcode = ddritem.Cells[13].Text;
                    string Monthname = ddritem.Cells[9].Text;
                    var ProName = ddritem.FindControl("lblProName") as Label;
                    var containertype = ddritem.FindControl("ContainerTypelbl") as Label;
                    //string ProName = (ddritem.FindControl("txtpName") as Label).Text;
                    Int32 month = 0;

                    if (Monthname == "January")
                    {
                        month = 1;
                    }
                    else if (Monthname == "February")
                    {
                        month = 2;
                    }
                    else if (Monthname == "March")
                    {
                        month = 3;
                    }
                    else if (Monthname == "April")
                    {
                        month = 4;
                    }
                    else if (Monthname == "May")
                    {
                        month = 5;
                    }
                    else if (Monthname == "June")
                    {
                        month = 6;
                    }
                    else if (Monthname == "July")
                    {
                        month = 7;
                    }
                    else if (Monthname == "August")
                    {
                        month = 8;
                    }
                    else if (Monthname == "September")
                    {
                        month = 9;
                    }
                    else if (Monthname == "October")
                    {
                        month = 10;
                    }
                    else if (Monthname == "November")
                    {
                        month = 11;
                    }
                    else if (Monthname == "December")
                    {
                        month = 12;
                    }

                    if ((string.IsNullOrEmpty(kititemcode)) || (kititemcode == "&nbsp;"))
                    {
                        kititemcode = string.Empty;
                    }
                    else
                    {
                        kititemcode = ddritem.Cells[13].Text;
                    }

                    if (ddritem.Cells[7].Text == "Main")
                    {
                        itemtype = "M";
                    }
                    else if (ddritem.Cells[7].Text == "Component")
                    {
                        itemtype = "C";
                    }
                    else if (ddritem.Cells[7].Text == "Accessories")
                    {
                        itemtype = "A";
                    }

                    OrderPlanItems.IOI_SEQ_NO = Convert.ToInt32(opseqno);
                    OrderPlanItems.IOI_OP_NO = OPNO;
                    OrderPlanItems.IOI_LINE = gridline;
                    OrderPlanItems.IOI_REF_LINE = gridline;
                    OrderPlanItems.IOI_F_LINE = gridline;
                    OrderPlanItems.IOI_STUS = 1;
                    OrderPlanItems.IOI_YY = Convert.ToInt32(ddritem.Cells[8].Text.Trim());
                    OrderPlanItems.IOI_MM = Convert.ToInt32(month);
                    OrderPlanItems.IOI_ITM_CD = ddritem.Cells[1].Text.Trim();
                    OrderPlanItems.IOI_ITM_STUS = "GOD";
                    OrderPlanItems.IOI_MODEL = ddritem.Cells[3].Text.Trim();
                    OrderPlanItems.IOI_BRAND = ddritem.Cells[4].Text.Trim();
                    MasterItem ioiitem = CHNLSVC.Sales.getMasterItemDetails(Session["UserCompanyCode"].ToString(),OrderPlanItems.IOI_ITM_CD,1);
                    OrderPlanItems.IOI_DESC = ioiitem.Mi_shortdesc;
                    OrderPlanItems.IOI_ITM_TP = itemtype;
                    OrderPlanItems.IOI_COLOR = ddritem.Cells[5].Text.Trim();
                    OrderPlanItems.IOI_MFC = string.Empty;
                    OrderPlanItems.IOI_QTY = Convert.ToDecimal(ddritem.Cells[10].Text.Trim());
                    OrderPlanItems.IOI_BAL_QTY = Convert.ToDecimal(ddritem.Cells[10].Text.Trim());
                    OrderPlanItems.IOI_UOM = ddritem.Cells[6].Text.Trim();
                    OrderPlanItems.IOI_UNIT_RT = Convert.ToDecimal(ddritem.Cells[11].Text.Trim());
                    OrderPlanItems.IOI_PI_QTY = 0;
                    OrderPlanItems.IOI_KIT_LINE = Convert.ToInt32(KIT_LINE);
                    OrderPlanItems.IOI_KIT_ITM_CD = kititemcode;
                    OrderPlanItems.IOI_CRE_BY = _userid;
                    OrderPlanItems.IOI_CRE_DT = DateTime.Now.Date;
                    OrderPlanItems.IOI_MOD_BY = _userid;
                    OrderPlanItems.IOI_MOD_DT = Convert.ToDateTime(IO_SEND_DT);
                    OrderPlanItems.IOI_SESSION_ID = Session["SessionID"].ToString();
                    OrderPlanItems.IOI_TAG = ddritem.Cells[16].Text.Trim();
                    OrderPlanItems.IOI_ProjectName = ProName.Text;
                    //Added By Dulaj 2018/Sep/27
                    OrderPlanItems.IOI_CONT_TYPE = containertype.Text;
                    OrderPlanItems.IOI_CONT_QTY = Convert.ToDecimal(ddritem.Cells[21].Text);                  
                    
                    //
                    Label lblTotCBM = ddritem.FindControl("lblTotCBM") as Label;
                    decimal _tmp = 0;
                    OrderPlanItems.IOI_ITM_TOT_CBM = decimal.TryParse(lblTotCBM.Text, out _tmp) ? Convert.ToDecimal(lblTotCBM.Text) : 0;
                    SuccessOPItemsSave = CHNLSVC.Financial.SaveOPItems(OrderPlanItems);

                    successItems.Add(SuccessOPItemsSave);
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnsearchord_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
                DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNoByStatusNew(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "403";
                BindUCtrlDDLDataOP(result);
                ViewState["SEARCH"] = result;

                //txtFDate.Text = Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1).ToShortDateString();
                //txtTDate.Text = Convert.ToDateTime(txtdate.Text).Date.ToShortDateString();

                UserDPopoup.Show();
                txtSearchbyword.Focus();
                Session["Se_ref"] = false;

                //ViewState["SEARCH"] = null;
                //txtSearchbyword.Text = string.Empty;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
                //DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNoByStatus(SearchParams, null, null);
                //grdResult.DataSource = result;
                //grdResult.DataBind();
                //lblvalue.Text = "403";
                //BindUCtrlDDLData(result);
                //ViewState["SEARCH"] = result;
                //UserPopup.Show();
                Session["OP_SEARCH"] = "YES";
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnsearchordref_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
                DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNoByStatusNew(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "403";
                ViewState["SEARCH"] = result;
                result.Columns["REFERENCE NO"].SetOrdinal(0);
                BindUCtrlDDLDataOP(result);

                UserDPopoup.Show();
                txtSearchbyword.Focus();
                Session["OP_SEARCH"] = "YES";
                Session["Se_ref"] = true;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void LoadOrderHeader()
        {
            try
            {
                Session["OPSTATUS"] = null;
                DataTable dtHeaders = CHNLSVC.CommonSearch.SP_Search_Order(txtordno.Text.Trim());
                if (dtHeaders.Rows.Count > 0)
                {
                    foreach (DataRow item in dtHeaders.Rows)
                    {
                        Session["OPSEQNO"] = item[0].ToString();
                        txtmanualref.Text = item[2].ToString();
                        txtsupplier.Text = item[6].ToString();
                        Session["OPSTATUS"] = item[18].ToString();
                        if (item[18].ToString() == "C")
                        {
                            lblstatus.Text = "CANCELLED";
                        }
                        if (item[18].ToString() == "P")
                        {
                            lblstatus.Text = "PENDING";
                        }
                        if (item[18].ToString() == "A")
                        {
                            lblstatus.Text = "APPROVED";
                        }
                        if (item[18].ToString() == "F")
                        {
                            lblstatus.Text = "FINISHED";
                        }
                        DateTime oreddate = Convert.ToDateTime(item[3].ToString());
                        string oreddatetext = oreddate.ToString("dd/MMM/yyyy");
                        txtdate.Text = oreddatetext;

                        txtdescription.Text = item[8].ToString();

                        if (txtdescription.Text == "&nbsp;")
                        {
                            txtdescription.ForeColor = Color.White;
                        }
                        else
                        {
                            txtdescription.ForeColor = Color.Black;
                        }

                        if (!string.IsNullOrEmpty(item[12].ToString()))
                        {
                            ddltradeterm.SelectedValue = item[12].ToString();
                        }

                        if (!string.IsNullOrEmpty(item[13].ToString()))
                        {
                            ddlmodeofshipment.SelectedValue = item[13].ToString();
                        }


                        DateTime ETA = Convert.ToDateTime(item[16].ToString());
                        string etatext = ETA.ToString("dd/MMM/yyyy");
                        txteta.Text = etatext;

                        LoadSupplierDetails();

                        if (dtports.Rows.Count > 0)
                        {
                            ddlportoforigin.Text = item[14].ToString();
                        }

                        GetEta();

                        this.ddltradeterm_SelectedIndexChanged(null, null);
                        this.ddlmodeofshipment_SelectedIndexChanged(null, null);
                        string amaendseq = item[21].ToString();
                        Session["AMENDSEQ"] = amaendseq;
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void LoadSupplierDetails()
        {
            try
            {
                Session["SUPPLER_CURRENCY"] = null;
                List<MasterBusinessEntity> supplierlistedit = CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), txtsupplier.Text, string.Empty, string.Empty, "S");

                if (supplierlistedit != null || supplierlistedit.Count > 1)
                {
                    foreach (var _nicCust in supplierlistedit)
                    {
                        lblcurrency.Text = _nicCust.Mbe_cur_cd;
                        Session["SUPPLER_CURRENCY"] = _nicCust.Mbe_cur_cd;
                        lblsupplier.Text = _nicCust.Mbe_name;
                    }
                }
                ExcRate();
                ddlportoforigin.DataSource = null;
                ddlportoforigin.DataBind();

                dtports = CHNLSVC.Financial.GetSupplierPorts(Session["UserCompanyCode"].ToString(), txtsupplier.Text);
                ddlportoforigin.DataSource = dtports;
                ddlportoforigin.DataTextField = "MP_NAME";
                ddlportoforigin.DataValueField = "MSPR_FRM_PORT";
                ddlportoforigin.DataBind();
                ddlportoforigin.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void LoadOrderHeaderItems()
        {
            try
            {
                DataTable sortedDT = null; ;
                DataTable dtHeaderItems = CHNLSVC.CommonSearch.SP_Search_OP_Itms(txtordno.Text.Trim());
                dtHeaderItems.Columns["Kit Item Code"].ColumnName = "KitItemCode";
                //dtHeaderItems.Columns[25].Caption = "ContainerType";
                _maxLine = Convert.ToInt32(dtHeaderItems.Compute("max(Line1)", string.Empty));
                #region add by lakshan 20 Feb 2017
                if (!dtHeaderItems.Columns.Contains("IOI_ITM_TOT_CBM"))
                {
                    dtHeaderItems.Columns.Add("IOI_ITM_TOT_CBM", typeof(System.Decimal));
                }
                #endregion
                //Kit Item Code
                if (dtHeaderItems.Rows.Count > 0)
                {
                    grdorderdetails.DataSource = null;
                    grdorderdetails.DataBind();

                    DataView dv = dtHeaderItems.DefaultView;
                    dv.Sort = "Item";
                    sortedDT = dv.ToTable();

                    grdorderdetails.DataSource = sortedDT;
                    grdorderdetails.DataBind();
                }
                CalculateTotQty();
                CalculateTotValue();

                ViewState["ItemsTable"] = null;
                this.BindGrid();

                ViewState["ItemsTable"] = sortedDT;
                this.BindGrid();
                ddlMultiColor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnmodelfind_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportModel);
                DataTable result = CHNLSVC.CommonSearch.SearchModel(SearchParams);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "406";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnitmfind_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItemforchange(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                result.Columns["MODEL"].SetOrdinal(0);
                lblvalue.Text = "407";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void BindGrid()
        {
            try
            {
                DataTable upitems = (DataTable)ViewState["ItemsTable"];
                grdorderdetails.DataSource = upitems;
                grdorderdetails.DataBind();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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
        protected void InsertToGrid()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["ItemsTable"];
                #region add by lakshan 20 Feb 2017
                if (!dt.Columns.Contains("IOI_ITM_TOT_CBM"))
                {
                    dt.Columns.Add("IOI_ITM_TOT_CBM", typeof(System.Decimal));
                }
                if (!dt.Columns.Contains("temporder"))
                {
                    dt.Columns.Add("temporder", typeof(System.String));
                }
                if (!dt.Columns.Contains("Trade Agreement"))
                {
                    dt.Columns.Add("Trade Agreement", typeof(System.String));
                }
                if (!dt.Columns.Contains("ContainerType"))
                {
                    dt.Columns.Add("ContainerType", typeof(System.String));
                }
                if (!dt.Columns.Contains("Container Qty"))
                {
                    dt.Columns.Add("Container Qty", typeof(System.Decimal));
                }
                if (!dt.Columns.Contains("Container Des"))
                {
                    dt.Columns.Add("Container Des", typeof(System.String));
                }  
                #endregion
                if (dt.Rows.Count > 0)
                {
                    DataColumnCollection columns = dt.Columns;

                    if (columns.Contains("Ioi Line_temp"))
                    {

                    }
                    else
                    {
                        dt.Columns.Add("Ioi Line_temp", typeof(System.String));
                    }

                }


                Decimal ordqty = 0;
                Decimal unitrate = 0;
                Decimal value = 0;

                if (((string)Session["KITITEM"] == txtitem.Text.Trim()) && (string.IsNullOrEmpty(txtunitrate.Text.Trim())))
                {
                    DataTable dtKit = CHNLSVC.Financial.GetKitItem(txtitem.Text);
                    int count = dtKit.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Item"] = dtKit.Rows[i]["MI_CD"].ToString();
                        dr["Description"] = dtKit.Rows[i]["MI_SHORTDESC"].ToString();
                        dr["Brand"] = dtKit.Rows[i]["MI_BRAND"].ToString();
                        dr["Model"] = dtKit.Rows[i]["MI_MODEL"].ToString();
                        dr["Colour"] = dtKit.Rows[i]["MI_COLOR_EXT"].ToString();
                        dr["PartNo"] = ddlMultiColor.SelectedValue.ToString();
                        dr["UOM"] = dtKit.Rows[i]["MI_ITM_UOM"].ToString();
                        dr["Type"] = ddlitemtype.SelectedItem.Text;
                        dr["Ord Qty"] = Convert.ToDecimal(dtKit.Rows[i]["MIKC_NO_OF_UNIT"].ToString()) * Convert.ToDecimal(txtqty.Text.Trim());
                        dr["Unit Rate"] = dtKit.Rows[i]["MIKC_COST"].ToString();
                        dr["Value"] = (Convert.ToDecimal(dr["Ord Qty"].ToString()) * Convert.ToDecimal(dr["Unit Rate"].ToString()));
                        dr["Ioi Line"] = "0";
                        dr["KitItemCode"] = txtitem.Text;
                        dr["Year"] = txtddlYear.Text.Trim();
                        dr["Month"] = ddlMonth.SelectedItem.Value;
                        if (ddlTag.SelectedValue == "0")
                        {
                            dr["Tag"] = "N";
                            dr["TagName"] = "Normal";
                        }
                        else
                        {
                            dr["Tag"] = ddlTag.SelectedValue;
                            dr["TagName"] = ddlTag.SelectedItem.Text;
                        }
                        dr["ProName"] = "";
                        dr["temporder"] = "";
                        #region add by lakshan 20 Feb 2017
                        MasterItem _mstTmp = CHNLSVC.General.GetItemMaster(dr["Item"].ToString());
                        if (_mstTmp != null)
                        {
                            decimal _ordQty = 0, _tmp = 0;
                            _ordQty = decimal.TryParse(dr["ORD Qty"].ToString(), out _tmp) ? Convert.ToDecimal(dr["ORD Qty"].ToString()) : 0;
                            decimal _itmCbm = (_mstTmp.Mi_dim_height * _mstTmp.Mi_dim_length * _mstTmp.Mi_dim_width * _ordQty);
                            dr["IOI_ITM_TOT_CBM"] = _itmCbm;
                        }
                        dr["ContainerType"] = "-";
                        dr["Container Qty"] = 0;
                        // dr["Container Des"] = "-";

                        if (DropDownListContainer.SelectedIndex != 0)
                        {
                            dr["ContainerType"] = DropDownListContainer.SelectedValue.ToString();
                        }
                        //if (DropDownListContainer.SelectedIndex != 0)
                        //{
                        //    dr["Container Des"] = DropDownListContainer.SelectedItem.ToString();
                        //}
                        if (!(string.IsNullOrEmpty(TextBoxContainerQty.Text.Trim())))
                        {
                            dr["Container Qty"] = Convert.ToDecimal(TextBoxContainerQty.Text);
                        }
                        #endregion
                        dt.Rows.Add(dr);
                    }

                    DataView dv = dt.DefaultView;
                    dv.Sort = "Item";
                    DataTable sortedDT = dv.ToTable();
                    ViewState["ItemsTable"] = sortedDT;
                    grdorderdetails.DataSource = sortedDT;
                    grdorderdetails.DataBind();

                    CalculateTotQty();
                    CalculateTotValue();
                }
                else
                {
                    string desc = lblitmdescription.Text.Trim();
                    string brand = lblbrandit.Text.Trim();
                    string uom = lbluomit.Text.Trim();

                    bool isvalidurt = ValidateUnitRate();
                    if (isvalidurt == false)
                    {
                        return;
                    }

                    ordqty = Convert.ToDecimal(txtqty.Text.Trim());
                    unitrate = Convert.ToDecimal(txtunitrate.Text.Trim());
                    value = Math.Round((ordqty * unitrate), 5);

                    if (string.IsNullOrEmpty(desc))
                    {
                        desc = "N/A";
                    }

                    if (string.IsNullOrEmpty(brand))
                    {
                        brand = "N/A";
                    }

                    if (string.IsNullOrEmpty(uom))
                    {
                        uom = "N/A";
                    }

                    foreach (GridViewRow ddr in grdorderdetails.Rows)
                    {
                        if (ddr.Cells[1].Text == txtitem.Text.Trim())
                        {
                            if (Convert.ToDecimal(ddr.Cells[11].Text.Trim()) != unitrate)
                            {
                                //divalert.Visible = true;
                                //lblalert.Text = "You are not allowed to add this item with a different unit rate !!!";
                                //txtmanualref.Focus();
                                //Session["UNITINVALID"] = "YES";
                                //Session["UPDATEQTY"] = null;
                                //return;
                            }
                            else
                            {
                                lblMssg.Text = "Do you want to continue ?";
                                UserPopoup.Show();
                                Session["UNITINVALID"] = null;
                                Session["UPDATEQTY"] = "YES";
                                return;
                            }
                        }
                    }
                    string Tag = string.Empty;
                    string TagName = string.Empty;
                    if (ddlTag.SelectedValue == "0")
                    {
                        Tag = "N";
                        TagName = "Normal";
                    }
                    else
                    {
                        Tag = ddlTag.SelectedValue;
                        TagName = ddlTag.SelectedItem.Text;
                    }

                    //dt.Rows.Add(txtitem.Text , desc, 
                    //    brand, txtmodel.Text.Trim(), ddlMultiColor.SelectedValue.ToString(), 
                    //    uom, ddlitemtype.SelectedItem.Text, 
                    //    ordqty, unitrate, value,
                    //    0,0,Convert.ToInt32(txtddlYear.Text.Trim()), Convert.ToInt32(ddlMonth.SelectedItem.Value.ToString()),
                    //    Tag, TagName)
                    Int32 d = 0;

                    DataRow dr = dt.NewRow();
                    dr["Item"] = txtitem.Text;
                    dr["Description"] = desc;
                    dr["Brand"] = brand;
                    dr["Model"] = txtmodel.Text.Trim();
                    dr["Colour"] = ddlMultiColor.SelectedValue.ToString();
                    dr["PartNo"] = ddlMultiColor.SelectedValue.ToString();
                    dr["UOM"] = uom;
                    dr["Type"] = ddlitemtype.SelectedItem.Text;
                    dr["Ord Qty"] = ordqty;
                    dr["Unit Rate"] = unitrate;
                    dr["Value"] = value;
                    dr["Ioi Line"] = "0";
                    dr["KitItemCode"] = "";
                    dr["Year"] = Convert.ToInt32(txtddlYear.Text.Trim());
                    dr["Month"] = ddlMonth.SelectedItem.Value.ToString();
                    dr["Trade Agreement"] = "-";
                    dr["ContainerType"] = "-";
                    dr["Container Qty"] = 0;
                   // dr["Container Des"] = "-";
                    
                    if (DropDownListContainer.SelectedIndex!=0)
                    {
                        dr["ContainerType"] = DropDownListContainer.SelectedValue.ToString();
                    }
                    //if (DropDownListContainer.SelectedIndex != 0)
                    //{
                    //    dr["Container Des"] = DropDownListContainer.SelectedItem.ToString();
                    //}
                    if (!(string.IsNullOrEmpty(TextBoxContainerQty.Text.Trim())))
                    {
                        dr["Container Qty"] = Convert.ToDecimal(TextBoxContainerQty.Text);
                    }
                    if (ddlTag.SelectedValue == "0")
                    {
                        dr["Tag"] = "N";
                        dr["TagName"] = "Normal";
                    }
                    else
                    {
                        dr["Tag"] = ddlTag.SelectedValue.ToString();
                        dr["TagName"] = ddlTag.SelectedItem.ToString();
                    }
                    _tempLine = 0;
                    dr["ProName"] = "";
                    dr["Ioi Line_temp"] = _tempLine;
                    #region add by lakshan 20 Feb 2017
                    MasterItem _mstTmp = CHNLSVC.General.GetItemMaster(dr["Item"].ToString());
                    if (_mstTmp != null)
                    {
                        decimal _ordQty = 0, _tmp = 0;
                        _ordQty = decimal.TryParse(dr["ORD Qty"].ToString(), out _tmp) ? Convert.ToDecimal(dr["ORD Qty"].ToString()) : 0;
                        decimal _itmCbm = (_mstTmp.Mi_dim_height * _mstTmp.Mi_dim_length * _mstTmp.Mi_dim_width * _ordQty);
                        dr["IOI_ITM_TOT_CBM"] = _itmCbm;
                    }
                    #endregion
                    dt.Rows.Add(dr);


                    //dt.Rows.Add(txtitem.Text, desc, 
                    //    brand, txtmodel.Text.Trim(), ddlMultiColor.SelectedValue.ToString(),
                    //   uom, ddlitemtype.SelectedItem.Text, 
                    //    ordqty, unitrate, value,
                    //    0, "", Convert.ToInt32(txtddlYear.Text.Trim()), Convert.ToInt32(ddlMonth.SelectedItem.Value.ToString()),
                    //   d, d);

                    DataView dv = dt.DefaultView;
                    dv.Sort = "Ioi Line_temp";
                    DataTable sortedDT = dv.ToTable();
                    ViewState["ItemsTable"] = sortedDT;

                    grdorderdetails.DataSource = sortedDT;
                    grdorderdetails.DataBind();

                    CalculateTotQty();
                    CalculateTotValue();
                    _tempLine++;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void CalculateTotQty()
        {
            try
            {
                decimal totordqty = 0;

                foreach (GridViewRow ddr in grdorderdetails.Rows)
                {
                    if (!string.IsNullOrEmpty(ddr.Cells[10].Text))
                    {
                        totordqty += Math.Round(Convert.ToDecimal(ddr.Cells[10].Text), 2);
                    }
                }

                lbltotordqty.Text = Convert.ToDecimal(totordqty).ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void CalculateTotValue()
        {
            try
            {
                decimal totordvalue = 0;

                foreach (GridViewRow ddr in grdorderdetails.Rows)
                {
                    if (!string.IsNullOrEmpty(ddr.Cells[12].Text))
                    {
                        totordvalue += Math.Round((Convert.ToDecimal(ddr.Cells[11].Text) * Convert.ToDecimal(ddr.Cells[10].Text)), 5);
                    }
                }
                lbltotordval.Text = Convert.ToDecimal(totordvalue).ToString("N5");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                divalert.Visible = false;
                int Myindex = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["ItemsTable"] as DataTable;

                string kitcode = dt.Rows[Myindex]["KitItemCode"].ToString();
                int ioiline = Int32.Parse(dt.Rows[Myindex]["Ioi Line"].ToString());
                int qty = Int32.Parse(dt.Rows[Myindex]["Ord Qty"].ToString());
                string OPitem = dt.Rows[Myindex]["Item"].ToString();
                string model = dt.Rows[Myindex]["Model"].ToString();
                string brand = dt.Rows[Myindex]["Brand"].ToString();
                string desc = dt.Rows[Myindex]["Description"].ToString();
                string itmtype = dt.Rows[Myindex]["Type"].ToString();
                string uom = dt.Rows[Myindex]["UOM"].ToString();

                if (itmtype == "Main")
                {
                    itmtype = "M";
                }
                else if (itmtype == "Component")
                {
                    itmtype = "C";
                }
                else if (itmtype == "Accessories")
                {
                    itmtype = "A";
                }

                string sequenceno = (string)Session["OPSEQNO"];
                OrderPlanItem OrderPlanItemsdup = new OrderPlanItem();

                DataTable dtitemlines = CHNLSVC.Financial.GetItemLines(txtordno.Text.Trim(), Convert.ToInt32(sequenceno), ioiline);
                foreach (DataRow item in dtitemlines.Rows)
                {
                    OrderPlanItemsdup.IOI_F_LINE = Int32.Parse(item[4].ToString());
                    OrderPlanItemsdup.IOI_REF_LINE = Int32.Parse(item[3].ToString());
                    OrderPlanItemsdup.IOI_COLOR = item[6].ToString();
                    OrderPlanItemsdup.IOI_BAL_QTY = Int32.Parse(item[7].ToString());
                    OrderPlanItemsdup.IOI_QTY = Int32.Parse(item[7].ToString());
                    OrderPlanItemsdup.IOI_PI_QTY = qty;
                    OrderPlanItemsdup.IOI_UNIT_RT = Int32.Parse(item[8].ToString());
                    OrderPlanItemsdup.IOI_YY = Convert.ToInt32(item[9].ToString());
                    OrderPlanItemsdup.IOI_MM = Convert.ToInt32(item[10].ToString());
                }

                OrderPlanItemsdup.IOI_OP_NO = txtordno.Text.Trim();
                if (sequenceno != null)
                {
                    OrderPlanItemsdup.IOI_SEQ_NO = Int32.Parse(sequenceno);
                }
                OrderPlanItemsdup.IOI_LINE = ioiline;
                OrderPlanItemsdup.IOI_STUS = 0;
                OrderPlanItemsdup.IOI_ITM_CD = OPitem;
                OrderPlanItemsdup.IOI_ITM_STUS = "GOD";
                OrderPlanItemsdup.IOI_KIT_LINE = 0;
                OrderPlanItemsdup.IOI_KIT_ITM_CD = kitcode;
                OrderPlanItemsdup.IOI_MOD_BY = _userid;
                OrderPlanItemsdup.IOI_MOD_DT = DateTime.Now.Date;
                OrderPlanItemsdup.IOI_SESSION_ID = Session["SessionID"].ToString();

                OrderPlanItemsdup.IOI_MODEL = model;
                OrderPlanItemsdup.IOI_BRAND = brand;
                OrderPlanItemsdup.IOI_DESC = desc;
                OrderPlanItemsdup.IOI_ITM_TP = itmtype;
                OrderPlanItemsdup.IOI_UOM = uom;

                if (txtdeleteItem.Value == "Yes")
                {

                    if (!string.IsNullOrEmpty(kitcode))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Cant delete items in kit !!!";
                        return;
                    }
                    else
                    {
                        dt.Rows[Myindex].Delete();
                        dt.AcceptChanges();
                        ViewState["ItemsTable"] = dt;
                        BindGrid();
                        CalculateTotQty();
                        CalculateTotValue();
                        int success = 1;
                        if (sequenceno != null)
                        {
                             success = CHNLSVC.Financial.UpdateOPItems(OrderPlanItemsdup);
                        }
                        if (success < 1)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Not deleted";
                            return;
                        }
                        else
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Item Deleted";
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void grdorderdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string item = e.Row.Cells[1].Text;
                    foreach (LinkButton button in e.Row.Cells[16].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete item " + item + "?')){ return false; };";
                        }
                    }

                    TableCell statusCell = e.Row.Cells[7];
                    if (statusCell.Text == "M")
                    {
                        statusCell.Text = "Main";
                    }
                    if (statusCell.Text == "C")
                    {
                        statusCell.Text = "Component";
                    }
                    if (statusCell.Text == "A")
                    {
                        statusCell.Text = "Accessories";
                    }

                    TableCell monthcell = e.Row.Cells[9];
                    if (monthcell.Text == "1")
                    {
                        monthcell.Text = "January";
                    }
                    else if (monthcell.Text == "2")
                    {
                        monthcell.Text = "February";
                    }
                    else if (monthcell.Text == "3")
                    {
                        monthcell.Text = "March";
                    }
                    else if (monthcell.Text == "4")
                    {
                        monthcell.Text = "April";
                    }
                    else if (monthcell.Text == "5")
                    {
                        monthcell.Text = "May";
                    }
                    else if (monthcell.Text == "6")
                    {
                        monthcell.Text = "June";
                    }
                    else if (monthcell.Text == "7")
                    {
                        monthcell.Text = "July";
                    }
                    else if (monthcell.Text == "8")
                    {
                        monthcell.Text = "August";
                    }
                    else if (monthcell.Text == "9")
                    {
                        monthcell.Text = "September";
                    }
                    else if (monthcell.Text == "10")
                    {
                        monthcell.Text = "October";
                    }
                    else if (monthcell.Text == "11")
                    {
                        monthcell.Text = "November";
                    }
                    else if (monthcell.Text == "12")
                    {
                        monthcell.Text = "December";
                    }
                }

            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void grdorderdetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdorderdetails.EditIndex = e.NewEditIndex;
                this.BindGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void OnUpdate(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                
                TextBox txtQt = (row.Cells[10].Controls[0] as TextBox);
                string OrdQty = (row.Cells[10].Controls[0] as TextBox).Text;
                string UnitRate = (row.Cells[11].Controls[0] as TextBox).Text;
                string kititem = row.Cells[13].Text;
                string year = (row.Cells[8].Controls[0] as TextBox).Text;
                string month = (row.Cells[9].Controls[0] as TextBox).Text;
                string ProName = (row.FindControl("txtpName") as TextBox).Text;
                string containerQty = (row.Cells[21].Controls[0] as TextBox).Text;
                string containertype = (row.FindControl("ContainerTypelbl") as Label).Text;
                if (containertype.Equals("-"))
                {
                    divalert.Visible = true;
                    lblalert.Text = "You cannot edit container qty without container type";
                    return;
                }
                //string containertype = (row.Cells[18].Controls[0] as TextBox).Text;
                //if ((!string.IsNullOrEmpty(kititem)) && (kititem != "&nbsp;"))
                //{
                //    divalert.Visible = true;
                //    lblalert.Text = "Cant modify items in kit !!!";
                //    return;
                //}

                if (string.IsNullOrEmpty(OrdQty))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter quantity !!!";
                    return;
                }
                decimal _qty = 0;
                if (!decimal.TryParse(txtQt.Text, out _qty))
                {
                    txtQt.Text = "";
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid quantity !!!";
                    return;
                }
                if (Convert.ToDecimal(txtQt.Text) <= 0)
                {
                    txtQt.Text = "";
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid quantity !!!";
                    return;
                }

                if (string.IsNullOrEmpty(UnitRate))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter unit rate !!!";
                    return;
                }

                if (string.IsNullOrEmpty(year))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter year !!!";
                    return;
                }

                if (string.IsNullOrEmpty(month))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter month !!!";
                    return;
                }

                if (!IsNumeric(OrdQty, NumberStyles.Float))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid number for qty !!!";
                    return;
                }

                if (!IsNumeric(UnitRate, NumberStyles.Float))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid number for unit rate !!!";
                    return;
                }

                if (!IsNumeric(year, NumberStyles.Integer))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid number for year !!!";
                    return;
                }

                if (month == "January")
                {
                    month = "1";
                }
                else if (month == "February")
                {
                    month = "2";
                }
                else if (month == "March")
                {
                    month = "3";
                }
                else if (month == "April")
                {
                    month = "4";
                }
                else if (month == "May")
                {
                    month = "5";
                }
                else if (month == "June")
                {
                    month = "6";
                }
                else if (month == "July")
                {
                    month = "7";
                }
                else if (month == "August")
                {
                    month = "8";
                }
                else if (month == "September")
                {
                    month = "9";
                }
                else if (month == "October")
                {
                    month = "10";
                }
                else if (month == "November")
                {
                    month = "11";
                }
                else if (month == "December")
                {
                    month = "12";
                }

                if (!IsNumeric(month, NumberStyles.Integer))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid number for month !!!";
                    return;
                }

                if (Convert.ToInt32(year) < 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid year !!!";
                    return;
                }

                if (Convert.ToInt32(month) < 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid month !!!";
                    return;
                }

                if (Convert.ToInt32(month) > 12)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid month !!!";
                    return;
                }

                if (Convert.ToInt32(OrdQty) < 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Minus qty is not allowed !!!";
                    return;
                }

                if (Convert.ToDecimal(UnitRate) < 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Minus unit rate is not allowed !!!";
                    return;
                }
                //Added By Dulaj 2018/Oct/04
                if (!IsNumeric(containerQty, NumberStyles.Integer))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid number for containerQty !!!";
                    return;
                }
                if (Convert.ToDecimal(containerQty) < 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Minus container qty not allowed !!!";
                    return;
                }
                
                DataTable dt = ViewState["ItemsTable"] as DataTable;


                dt.Rows[row.RowIndex]["Ord Qty"] = OrdQty;
                dt.Rows[row.RowIndex]["Unit Rate"] = UnitRate;

                Decimal ordqty = Convert.ToDecimal(OrdQty);
                Decimal unitrate = Convert.ToDecimal(UnitRate);
                Decimal value = Math.Round((ordqty * unitrate), 5);
                dt.Rows[row.RowIndex]["Value"] = value.ToString();
                dt.Rows[row.RowIndex]["Year"] = year;
                dt.Rows[row.RowIndex]["Month"] = month;
                dt.Rows[row.RowIndex]["ProName"] = ProName;
                dt.Rows[row.RowIndex]["Container Qty"] = containerQty;
                ViewState["ItemsTable"] = dt;
                grdorderdetails.EditIndex = -1;
                this.BindGrid();

                CalculateTotQty();
                CalculateTotValue();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void btnConfYes_Click(object sender, EventArgs e)
        {
            popupContainer.Hide();
            //hdfConf.Value = "1";
            saveOrderPlan();
          //  hdfConfItem.Value = txtItem.Text.ToUpper().Trim();
           // hdfConfStatus.Value = cmbStatus.SelectedValue.ToString();
            
        }
        protected void btnConfClose_Click(object sender, EventArgs e)
        {
          //  lblConfText.Text = "";
            popupContainer.Hide();
        }
        protected void btnConfNo_Click(object sender, EventArgs e)
        {
            popupContainer.Hide();
        }
        protected void OnCancel(object sender, EventArgs e)
        {
            try
            {
                grdorderdetails.EditIndex = -1;
                this.BindGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnkititem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtordno.Text.Trim()))
                {
                    grdkititems.DataSource = null;
                    grdkititems.DataBind();

                    grdkititems.DataSource = (DataTable)ViewState["KITItemsTable"];
                    grdkititems.DataBind();
                    mpkit.Show();
                    txtSearchbyword.Focus();
                }
                else
                {
                    LoadKit();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void UpdateOPItems()
        {
            try
            {
                _userid = (string)Session["UserID"];
                string sequenceno = (string)Session["OPSEQNO"];
                Int32? KIT_LINE = null;
                string lineno = "0";
                if (refline == "") refline = "0";
                if (fline == "") fline = "0";
               

                OrderPlanItem OrderPlanItems = new OrderPlanItem();

                foreach (GridViewRow ddritem in grdorderdetails.Rows)
                {
                    string itemtype = string.Empty;


                    Label project = ddritem.FindControl("lblProNamenew") as Label;
                    string prj = "";
                    if (project != null)
                    {
                        prj = project.Text;
                    }

                    var containertype = ddritem.FindControl("ContainerTypelbl") as Label;
                    if ((string.IsNullOrEmpty(ddritem.Cells[14].Text)) || (ddritem.Cells[14].Text == "&nbsp;"))
                    {
                        ddritem.Cells[14].Text = "99999";
                    }

                    DataTable dtitemlines = CHNLSVC.Financial.GetItemLines(txtordno.Text.Trim(), Convert.ToInt32(sequenceno), Convert.ToInt32(ddritem.Cells[14].Text));

                    if (dtitemlines.Rows.Count == 0)
                    {
                        //continue;
                        lineno = _maxLine.ToString(); ;
                        OrderPlanItem OrderPlanItemsdup = new OrderPlanItem();


                        string itemtypedup = string.Empty;
                        Int32 gridlinedup = ddritem.RowIndex + 1;

                        if (ddritem.Cells[7].Text == "Main")
                        {
                            itemtype = "M";
                        }
                        else if (ddritem.Cells[7].Text == "Component")
                        {
                            itemtype = "C";
                        }
                        else if (ddritem.Cells[7].Text == "Accessories")
                        {
                            itemtype = "A";
                        }

                        string Monthname = ddritem.Cells[9].Text;
                        Int32 month = 0;

                        if (Monthname == "January")
                        {
                            month = 1;
                        }
                        else if (Monthname == "February")
                        {
                            month = 2;
                        }
                        else if (Monthname == "March")
                        {
                            month = 3;
                        }
                        else if (Monthname == "April")
                        {
                            month = 4;
                        }
                        else if (Monthname == "May")
                        {
                            month = 5;
                        }
                        else if (Monthname == "June")
                        {
                            month = 6;
                        }
                        else if (Monthname == "July")
                        {
                            month = 7;
                        }
                        else if (Monthname == "August")
                        {
                            month = 8;
                        }
                        else if (Monthname == "September")
                        {
                            month = 9;
                        }
                        else if (Monthname == "October")
                        {
                            month = 10;
                        }
                        else if (Monthname == "November")
                        {
                            month = 11;
                        }
                        else if (Monthname == "December")
                        {
                            month = 12;
                        }




                        OrderPlanItemsdup.IOI_SEQ_NO = Convert.ToInt32(sequenceno);
                        OrderPlanItemsdup.IOI_OP_NO = txtordno.Text.Trim().Trim();
                        OrderPlanItemsdup.IOI_LINE = Convert.ToInt32(lineno) + 1;
                        OrderPlanItemsdup.IOI_REF_LINE = Convert.ToInt32(lineno) + 1;
                        OrderPlanItemsdup.IOI_F_LINE = Convert.ToInt32(lineno) + 1;
                        OrderPlanItemsdup.IOI_STUS = 1;
                        OrderPlanItemsdup.IOI_YY = Convert.ToInt32(ddritem.Cells[8].Text.Trim());
                        OrderPlanItemsdup.IOI_MM = Convert.ToInt32(month);
                        OrderPlanItemsdup.IOI_ITM_CD = ddritem.Cells[1].Text.Trim();
                        OrderPlanItemsdup.IOI_ITM_STUS = "GOD";
                        OrderPlanItemsdup.IOI_MODEL = ddritem.Cells[3].Text.Trim();
                        OrderPlanItemsdup.IOI_BRAND = ddritem.Cells[4].Text.Trim();
                        OrderPlanItemsdup.IOI_DESC = ddritem.Cells[2].Text.Trim();
                        OrderPlanItemsdup.IOI_ITM_TP = itemtype;
                        OrderPlanItemsdup.IOI_COLOR = ddritem.Cells[5].Text.Trim();
                        OrderPlanItemsdup.IOI_MFC = string.Empty;
                        OrderPlanItemsdup.IOI_QTY = Convert.ToDecimal(ddritem.Cells[10].Text.Trim());
                        OrderPlanItemsdup.IOI_BAL_QTY = Convert.ToDecimal(ddritem.Cells[10].Text.Trim());
                        OrderPlanItemsdup.IOI_UOM = ddritem.Cells[6].Text.Trim();
                        OrderPlanItemsdup.IOI_UNIT_RT = Convert.ToDecimal(ddritem.Cells[11].Text.Trim());
                        OrderPlanItemsdup.IOI_PI_QTY = Convert.ToDecimal(ddritem.Cells[10].Text.Trim());
                        OrderPlanItemsdup.IOI_KIT_LINE = 0;
                        OrderPlanItemsdup.IOI_KIT_ITM_CD = "";
                        OrderPlanItemsdup.IOI_CRE_BY = _userid;
                        OrderPlanItemsdup.IOI_CRE_DT = DateTime.Now.Date;
                        OrderPlanItemsdup.IOI_MOD_BY = _userid;
                        OrderPlanItemsdup.IOI_MOD_DT = DateTime.Now.Date; ;
                        OrderPlanItemsdup.IOI_SESSION_ID = Session["SessionID"].ToString();
                        OrderPlanItemsdup.IOI_TAG = ddritem.Cells[16].Text.Trim();
                        OrderPlanItemsdup.IOI_ProjectName = prj;
                        //Added By Dulaj 2018/Sep/27
                        OrderPlanItemsdup.IOI_CONT_TYPE = containertype.Text;
                        OrderPlanItemsdup.IOI_CONT_QTY = Convert.ToDecimal(ddritem.Cells[21].Text);  
                        SuccessOPItemsEdit = CHNLSVC.Financial.SaveOPItems(OrderPlanItemsdup);
                        successItemsEdit.Add(SuccessOPItemsEdit);
                        _maxLine++;
                    }

                    if (dtitemlines.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtitemlines.Rows)
                        {
                            ioiline = item[2].ToString();
                            fline = item[4].ToString();
                            refline = item[3].ToString();
                            colouritem = item[6].ToString();
                            qtyitem = item[7].ToString();
                            priceitem = item[8].ToString();
                            sequenceitem = item[0].ToString();
                            yearitem = Convert.ToInt32(item[9].ToString());
                            monthitem = Convert.ToInt32(item[10].ToString());

                            if (ddritem.Cells[7].Text == "Main")
                            {
                                itemtype = "M";
                            }
                            else if (ddritem.Cells[7].Text == "Component")
                            {
                                itemtype = "C";
                            }
                            else if (ddritem.Cells[7].Text == "Accessories")
                            {
                                itemtype = "A";
                            }

                            string kititemcode = ddritem.Cells[13].Text;

                            if ((string.IsNullOrEmpty(kititemcode)) || (kititemcode == "&nbsp;"))
                            {
                                kititemcode = string.Empty;
                            }
                            else
                            {
                                kititemcode = ddritem.Cells[13].Text;
                            }

                            DataTable dtlinenum = CHNLSVC.Financial.GetItemRowCount(txtordno.Text.Trim(), Convert.ToInt32(sequenceno));

                            if (dtlinenum.Rows.Count > 0)
                            {
                                foreach (DataRow itemsub in dtlinenum.Rows)
                                {
                                    lineno = itemsub[0].ToString();
                                }
                            }

                            string Monthname = ddritem.Cells[9].Text;
                            Int32 month = 0;

                            if (Monthname == "January")
                            {
                                month = 1;
                            }
                            else if (Monthname == "February")
                            {
                                month = 2;
                            }
                            else if (Monthname == "March")
                            {
                                month = 3;
                            }
                            else if (Monthname == "April")
                            {
                                month = 4;
                            }
                            else if (Monthname == "May")
                            {
                                month = 5;
                            }
                            else if (Monthname == "June")
                            {
                                month = 6;
                            }
                            else if (Monthname == "July")
                            {
                                month = 7;
                            }
                            else if (Monthname == "August")
                            {
                                month = 8;
                            }
                            else if (Monthname == "September")
                            {
                                month = 9;
                            }
                            else if (Monthname == "October")
                            {
                                month = 10;
                            }
                            else if (Monthname == "November")
                            {
                                month = 11;
                            }
                            else if (Monthname == "December")
                            {
                                month = 12;
                            }

                            qtyitem = DoFormat(Convert.ToDecimal(qtyitem));
                            priceitem = DoFormat(Convert.ToDecimal(priceitem));
                            if ((colouritem != ddritem.Cells[5].Text) || (qtyitem != ddritem.Cells[10].Text) || (priceitem != ddritem.Cells[11].Text) || (yearitem != Convert.ToInt32(ddritem.Cells[8].Text)) || (monthitem != Convert.ToInt32(month)))
                            {
                                string opseqno = (string)Session["OPSEQNO"];
                                _userid = (string)Session["UserID"];
                                Int32? KIT_LINEdup = null;
                                DateTime? IO_SEND_DT_dup = null;
                                string iskititem = (string)Session["KITITEM"];

                                OrderPlanItem OrderPlanItemsdup = new OrderPlanItem();


                                string itemtypedup = string.Empty;
                                Int32 gridlinedup = ddritem.RowIndex + 1;

                                if (ddritem.Cells[7].Text == "Main")
                                {
                                    itemtype = "M";
                                }
                                else if (ddritem.Cells[7].Text == "Component")
                                {
                                    itemtype = "C";
                                }
                                else if (ddritem.Cells[7].Text == "Accessories")
                                {
                                    itemtype = "A";
                                }

                                OrderPlanItemsdup.IOI_SEQ_NO = Convert.ToInt32(opseqno);
                                OrderPlanItemsdup.IOI_OP_NO = txtordno.Text.Trim().Trim();
                                OrderPlanItemsdup.IOI_LINE = Convert.ToInt32(lineno);
                                OrderPlanItemsdup.IOI_REF_LINE = Convert.ToInt32(refline);
                                OrderPlanItemsdup.IOI_F_LINE = Convert.ToInt32(fline);
                                OrderPlanItemsdup.IOI_STUS = 1;
                                OrderPlanItemsdup.IOI_YY = Convert.ToInt32(ddritem.Cells[8].Text.Trim());
                                OrderPlanItemsdup.IOI_MM = Convert.ToInt32(month);
                                OrderPlanItemsdup.IOI_ITM_CD = ddritem.Cells[1].Text.Trim();
                                OrderPlanItemsdup.IOI_ITM_STUS = "GOD";
                                OrderPlanItemsdup.IOI_MODEL = ddritem.Cells[3].Text.Trim();
                                OrderPlanItemsdup.IOI_BRAND = ddritem.Cells[4].Text.Trim();
                                OrderPlanItemsdup.IOI_DESC = ddritem.Cells[2].Text.Trim();
                                OrderPlanItemsdup.IOI_ITM_TP = itemtype;
                                OrderPlanItemsdup.IOI_COLOR = ddritem.Cells[5].Text.Trim();
                                OrderPlanItemsdup.IOI_MFC = string.Empty;
                                OrderPlanItemsdup.IOI_QTY = Convert.ToDecimal(ddritem.Cells[10].Text.Trim());
                                OrderPlanItemsdup.IOI_BAL_QTY = Convert.ToDecimal(ddritem.Cells[10].Text.Trim());
                                OrderPlanItemsdup.IOI_UOM = ddritem.Cells[6].Text.Trim();
                                OrderPlanItemsdup.IOI_UNIT_RT = Convert.ToDecimal(ddritem.Cells[11].Text.Trim());
                                OrderPlanItemsdup.IOI_PI_QTY = Convert.ToDecimal(ddritem.Cells[10].Text.Trim());
                                OrderPlanItemsdup.IOI_KIT_LINE = Convert.ToInt32(KIT_LINEdup);
                                OrderPlanItemsdup.IOI_KIT_ITM_CD = kititemcode;
                                OrderPlanItemsdup.IOI_CRE_BY = _userid;
                                OrderPlanItemsdup.IOI_CRE_DT = DateTime.Now.Date;
                                OrderPlanItemsdup.IOI_MOD_BY = string.Empty;
                                OrderPlanItemsdup.IOI_MOD_DT = Convert.ToDateTime(IO_SEND_DT_dup);
                                OrderPlanItemsdup.IOI_SESSION_ID = Session["SessionID"].ToString();
                                OrderPlanItemsdup.IOI_ProjectName = prj;
                                OrderPlanItemsdup.IOI_TAG = ddritem.Cells[16].Text.Trim();
                                //Dulaj 2018/Oct/09
                                 //Added By Dulaj 2018/Sep/27
                                OrderPlanItemsdup.IOI_CONT_TYPE = containertype.Text;
                                OrderPlanItemsdup.IOI_CONT_QTY = Convert.ToDecimal(ddritem.Cells[21].Text); 
                                SuccessOPItemsEdit = CHNLSVC.Financial.SaveOPItems(OrderPlanItemsdup);
                                successItemsEdit.Add(SuccessOPItemsEdit);
                                _maxLine++;

                                OrderPlanItems.IOI_OP_NO = txtordno.Text.Trim();
                                OrderPlanItems.IOI_LINE = Convert.ToInt32(ioiline);
                                OrderPlanItems.IOI_REF_LINE = Convert.ToInt32(refline);
                                OrderPlanItems.IOI_F_LINE = Convert.ToInt32(fline);
                                OrderPlanItems.IOI_STUS = 0;
                                OrderPlanItems.IOI_YY = Convert.ToInt32(ddritem.Cells[8].Text.Trim());
                                OrderPlanItems.IOI_MM = Convert.ToInt32(month);
                                OrderPlanItems.IOI_ITM_CD = ddritem.Cells[1].Text.Trim();
                                OrderPlanItems.IOI_ITM_STUS = "GOD";
                                OrderPlanItems.IOI_MODEL = ddritem.Cells[3].Text.Trim();
                                OrderPlanItems.IOI_BRAND = ddritem.Cells[4].Text.Trim();
                                OrderPlanItems.IOI_DESC = ddritem.Cells[2].Text.Trim();
                                OrderPlanItems.IOI_ITM_TP = itemtype;
                                OrderPlanItems.IOI_COLOR = colouritem;
                                OrderPlanItems.IOI_MFC = string.Empty;
                                OrderPlanItems.IOI_QTY = Convert.ToDecimal(qtyitem);
                                OrderPlanItems.IOI_BAL_QTY = Convert.ToDecimal(qtyitem);
                                OrderPlanItems.IOI_UOM = ddritem.Cells[6].Text.Trim();
                                OrderPlanItems.IOI_UNIT_RT = Convert.ToDecimal(priceitem);
                                OrderPlanItems.IOI_PI_QTY = Convert.ToDecimal(ddritem.Cells[10].Text.Trim());
                                OrderPlanItems.IOI_KIT_LINE = Convert.ToInt32(KIT_LINE);
                                OrderPlanItems.IOI_KIT_ITM_CD = kititemcode;
                                OrderPlanItems.IOI_MOD_BY = _userid;
                                OrderPlanItems.IOI_MOD_DT = DateTime.Now.Date;
                                OrderPlanItems.IOI_SESSION_ID = Session["SessionID"].ToString();
                                OrderPlanItems.IOI_SEQ_NO = Convert.ToInt32(sequenceno);
                                OrderPlanItems.IOI_ProjectName = prj;
                                OrderPlanItems.IOI_TAG = ddritem.Cells[16].Text.Trim();                                
                                //Added By Dulaj 2018/Sep/27
                                OrderPlanItems.IOI_CONT_TYPE = containertype.Text;
                                OrderPlanItems.IOI_CONT_QTY = Convert.ToDecimal(ddritem.Cells[21].Text); 
                                SuccessOPItemsEdit = CHNLSVC.Financial.UpdateOPItems(OrderPlanItems);

                                successItemsEdit.Add(SuccessOPItemsEdit);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void chkteplate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkteplate.Checked == true)
                {
                    lbtnsupplier.Visible = false;
                    lbtnadditems.Visible = true;
                }
                else
                {
                    lbtnsupplier.Visible = true;
                    lbtnadditems.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnapprove_Click(object sender, EventArgs e)
        {
            if (txtapprove.Value == "Yes")
            {
                try
                {
                    if (lblstatus.Text == "CANCELLED")
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Already cancelled order plane";
                        txtordno.Focus();
                        return;
                    }
                    if (lblstatus.Text == "FINISHED")
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Already finished order plane";
                        txtordno.Focus();
                        return;
                    }
                    divalert.Visible = false;
                    if (string.IsNullOrEmpty(txtordno.Text.Trim()))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please select an order !!!";
                        txtordno.Focus();
                        return;
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16003))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Sorry, You have no permission to approve this order.( Advice: Required permission code : 16003) !!!";
                        return;
                    }
                    _userid = (string)Session["UserID"];

                    OrderPlanHeader OrderPlanHeader = new OrderPlanHeader();

                    OrderPlanHeader.IO_OP_NO = txtordno.Text.Trim();
                    OrderPlanHeader.IO_STUS = "A";
                    OrderPlanHeader.IO_MOD_BY = _userid;
                    OrderPlanHeader.IO_MOD_DT = DateTime.Now.Date;

                    Int32 outputresult = CHNLSVC.Financial.UpdateOPStatus(OrderPlanHeader);

                    if (outputresult == 1)
                    {
                        divalert.Visible = false;
                        divok.Visible = true;
                        lblok.Text = "Successfully approved";
                        Clear();
                    }
                    else
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Error Occurred while processing";
                    }
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
            }
        }
        private void SaveKitItems()
        {
            try
            {
                string opseqnokit = (string)Session["OPSEQNO"];
                _userid = (string)Session["UserID"];
                string OPNOKIT = (string)Session["OPNO"];

                OrderPlanKitItem OrderPlanKitItems = new OrderPlanKitItem();

                DataTable dtKITS = ViewState["KITItemsTable"] as DataTable;

                foreach (DataRow ddritemKIT in dtKITS.Rows)
                {
                    Int32 rowline = dtKITS.Rows.IndexOf(ddritemKIT) + 1;

                    OrderPlanKitItems.IOK_SEQ_NO = Convert.ToInt32(opseqnokit);
                    OrderPlanKitItems.IOK_OP_NO = OPNOKIT;
                    OrderPlanKitItems.IOK_LINE = rowline;
                    OrderPlanKitItems.IOK_REF_LINE = rowline;
                    OrderPlanKitItems.IOK_F_LINE = rowline;
                    OrderPlanKitItems.IOK_STUS = 1;
                    OrderPlanKitItems.IOK_ITM_CD = ddritemKIT["MI_CD"].ToString().Trim();
                    OrderPlanKitItems.IOK_ITM_STUS = "GOD";
                    OrderPlanKitItems.IOK_QTY = Convert.ToDecimal(0);
                    OrderPlanKitItems.IOK_BAL_QTY = Convert.ToDecimal(ddritemKIT["Qty"].ToString());
                    OrderPlanKitItems.IOK_UOM = ddritemKIT["MI_ITM_UOM"].ToString();
                    OrderPlanKitItems.IOK_UNIT_RT = Convert.ToDecimal(ddritemKIT["MI_ITMTOT_COST"].ToString());
                    OrderPlanKitItems.IOK_PI_QTY = 0;
                    OrderPlanKitItems.IOK_CRE_BY = _userid;
                    OrderPlanKitItems.IOK_CRE_DT = DateTime.Now.Date;

                    Int32 SuccessKITSAve = CHNLSVC.Financial.SaveKitItems(OrderPlanKitItems);
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            if (txtcancel.Value == "Yes")
            {
                try
                {
                    if (lblstatus.Text == "CANCELLED")
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Already cancelled order plane";
                        txtordno.Focus();
                        return;
                    }
                    if (lblstatus.Text == "FINISHED")
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Already finised order plane";
                        txtordno.Focus();
                        return;
                    }
                    divalert.Visible = false;
                    if (string.IsNullOrEmpty(txtordno.Text.Trim()))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please select an order !!!";
                        txtordno.Focus();
                        return;
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16004))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Sorry, You have no permission to cancel this order.( Advice: Required permission code : 16004) !!!";
                        return;
                    }

                    _userid = (string)Session["UserID"];

                    OrderPlanHeader OrderPlanHeader = new OrderPlanHeader();

                    OrderPlanHeader.IO_OP_NO = txtordno.Text.Trim();
                    OrderPlanHeader.IO_STUS = "C";
                    OrderPlanHeader.IO_MOD_BY = _userid;
                    OrderPlanHeader.IO_MOD_DT = DateTime.Now.Date;

                    Int32 outputresult = CHNLSVC.Financial.UpdateOPStatus(OrderPlanHeader);

                    if (outputresult == 1)
                    {
                        divalert.Visible = false;
                        divok.Visible = true;
                        lblok.Text = "Successfully cancelled";
                        Clear();
                    }
                    else
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Error Occurred while processing";
                    }
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
            }
        }

        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                //Bind Data to GridView

                DataTable dtcopy = new DataTable();
                DataTable dtoriginal = new DataTable();
                DataTable dtvwstatetbl = ViewState["ItemsTable"] as DataTable;

                if ((ViewState["ItemsTable"] != null) && (dtvwstatetbl.Rows.Count != 0))
                {
                    dtcopy = ViewState["ItemsTable"] as DataTable;
                    dtcopy.Merge(dt, true, MissingSchemaAction.Ignore);

                    DataView dv = dtcopy.DefaultView;
                    dv.Sort = "Item";
                    DataTable sortedDT = dv.ToTable();
                    ViewState["ItemsTable"] = sortedDT;

                    grdorderdetails.DataSource = sortedDT;
                    grdorderdetails.DataBind();
                }
                else
                {
                    dtoriginal = dt.Copy();
                    DataView dv = dtoriginal.DefaultView;
                    dv.Sort = "Item";
                    DataTable sortedDT = dv.ToTable();
                    ViewState["ItemsTable"] = sortedDT;

                    grdorderdetails.DataSource = sortedDT;
                    grdorderdetails.DataBind();
                }



                CalculateTotQty();
                CalculateTotValue();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnuploadexcel_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;
                if (fileupexcelupload.HasFile)
                {
                    string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                    if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please select a valid excel (.xls) file";
                        return;
                    }
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fileupexcelupload.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
                }
                else
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select an excel file";
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnupload_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;
                mpexcel.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private List<string> get_selected_Items()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in grdorderdetails.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[1].Text.ToString());
                }
            }
            return list;
        }

        protected void lbtnPDelete_Click(object sender, EventArgs e)
        {
            divalert.Visible = false;
            string iskititemcontain = string.Empty;

            if (txtdelselected.Value == "Yes")
            {
                try
                {
                    if (grdorderdetails.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Item list is empty !!!";
                        return;
                    }

                    List<string> selectedlist = get_selected_Items();

                    if ((selectedlist == null) || (selectedlist.Count == 0))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please select items to delete !!!";
                        return;
                    }

                    iskititemcontain = (string)Session["KITITEM"];

                    if (!string.IsNullOrEmpty(iskititemcontain))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "You cant delete items in kit !!!";
                        return;
                    }


                    DataTable dtTemp = new DataTable();
                    foreach (GridViewRow dgvr in grdorderdetails.Rows)
                    {
                        Int32 gridline = dgvr.RowIndex;
                        CheckBox chk = (CheckBox)dgvr.FindControl("chkRow");
                        if (chk != null & chk.Checked)
                        {
                            dtTemp = ViewState["ItemsTable"] as DataTable;
                            dtTemp.Rows[gridline].Delete();
                            ViewState["ItemsTable"] = dtTemp;
                        }
                    }
                    dtTemp.AcceptChanges();
                    BindGrid();
                    CalculateTotQty();
                    CalculateTotValue();
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
            }
        }

        protected void txtitem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtitem.Text))
                {
                    txtitem.Text = string.Empty;
                    lblalert.Text = "Please Enter Item Code.";
                    divalert.Visible = true;
                    return;
                }

                DataTable dt = CHNLSVC.Financial.GetItemDetails(txtitem.Text);

                if (dt.Rows.Count == 0)
                {
                    txtitem.Text = string.Empty;
                    lblalert.Text = "Invalid Item.";
                    divalert.Visible = true;
                    return;
                }

                if (dt.Rows[0]["MI_ITM_TP"].ToString() == "K")
                {
                    if (ViewState["KITItemsTable"] != null)
                    {
                        DataTable dtcopy = ViewState["KITItemsTable"] as DataTable;
                        dtcopy.Merge(dt);

                        ViewState["KITItemsTable"] = dtcopy;
                        Session["KITITEM"] = txtitem.Text.Trim();
                        Divinfo.Visible = true;
                        lblinfo.Text = "This is a KIT Item";
                    }
                    else
                    {
                        Session["KITITEM"] = txtitem.Text.Trim();
                        Divinfo.Visible = true;
                        lblinfo.Text = "This is a KIT Item";

                        DataTable dtKITitems = new DataTable();
                        dtKITitems = dt.Copy();

                        ViewState["KITItemsTable"] = dtKITitems;
                    }
                    txtunitrate.Enabled = false;
                    txtunitrate.Text = string.Empty;
                }
                else
                {
                    txtunitrate.Enabled = true;
                }

                txtmodel.Text = dt.Rows[0]["MI_MODEL"].ToString();
                lblitmdescription.Text = dt.Rows[0]["MI_SHORTDESC"].ToString();
                lblbrandit.Text = dt.Rows[0]["MI_BRAND"].ToString();
                lbluomit.Text = dt.Rows[0]["MI_ITM_UOM"].ToString();

                if (ddlMultiColor.Items.FindByValue(dt.Rows[0]["MI_COLOR_EXT"].ToString()) != null)
                {
                    ddlMultiColor.SelectedValue = dt.Rows[0]["MI_COLOR_EXT"].ToString();
                }
                else
                {
                    ddlMultiColor.SelectedItem.Text = "N/A";
                }

                if (ddlitemtype.Items.FindByValue(dt.Rows[0]["MI_ITM_TP"].ToString()) != null)
                {
                    ddlitemtype.SelectedValue = dt.Rows[0]["MI_ITM_TP"].ToString();
                }
                else
                {
                    ddlitemtype.SelectedIndex = 0;
                }

                ClientScriptManager cs = Page.ClientScript;
                Type cstype = this.GetType();
                cs.RegisterStartupScript(cstype, "SetFocus", "<Script>document.getElementById('" + ddlitemtype.ClientID + "').focus();</Script>");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void LoadKit()
        {
            try
            {
                string opno = txtordno.Text.Trim();

                DataTable dtkit = CHNLSVC.CommonSearch.LoadKit(opno);

                if (dtkit.Rows.Count > 0)
                {
                    string[] kitarray = dtkit.AsEnumerable().Select(r => r.Field<string>("Code")).ToArray();
                    string concatedString = kitarray.Aggregate((a, b) => Convert.ToString(a) + "," + Convert.ToString(b));
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Loaded Kit Items - " + concatedString + "')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No kit items for this order')", true);
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void txtsupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadSupplierDetails();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        static private string SoundEx(string word)
        {
            // The length of the returned code.
            int length = 4;

            // Value to return.
            string value = "";

            // The size of the word to process.
            int size = word.Length;

            // The word must be at least two characters in length.
            if (size > 1)
            {
                // Convert the word to uppercase characters.
                word = word.ToUpper(System.Globalization.CultureInfo.InvariantCulture);

                // Convert the word to a character array.
                char[] chars = word.ToCharArray();

                // Buffer to hold the character codes.
                StringBuilder buffer = new StringBuilder();
                buffer.Length = 0;

                // The current and previous character codes.
                int prevCode = 0;
                int currCode = 0;

                // Add the first character to the buffer.
                buffer.Append(chars[0]);

                // Loop through all the characters and convert them to the proper character code.
                for (int i = 1; i < size; i++)
                {
                    switch (chars[i])
                    {
                        case 'A':
                        case 'E':
                        case 'I':
                        case 'O':
                        case 'U':
                        case 'H':
                        case 'W':
                        case 'Y':
                            currCode = 0;
                            break;
                        case 'B':
                        case 'F':
                        case 'P':
                        case 'V':
                            currCode = 1;
                            break;
                        case 'C':
                        case 'G':
                        case 'J':
                        case 'K':
                        case 'Q':
                        case 'S':
                        case 'X':
                        case 'Z':
                            currCode = 2;
                            break;
                        case 'D':
                        case 'T':
                            currCode = 3;
                            break;
                        case 'L':
                            currCode = 4;
                            break;
                        case 'M':
                        case 'N':
                            currCode = 5;
                            break;
                        case 'R':
                            currCode = 6;
                            break;
                    }

                    // Check if the current code is the same as the previous code.
                    if (currCode != prevCode)
                    {
                        // Check to see if the current code is 0 (a vowel); do not process vowels.
                        if (currCode != 0)
                            buffer.Append(currCode);
                    }
                    // Set the previous character code.
                    prevCode = currCode;

                    // If the buffer size meets the length limit, exit the loop.
                    if (buffer.Length == length)
                        break;
                }
                // Pad the buffer, if required.
                size = buffer.Length;
                if (size < length)
                    buffer.Append('0', (length - size));

                // Set the value to return.
                value = buffer.ToString();
            }
            // Return the value.
            return value;
        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
            DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNoByStatusNew(SearchParams, ddlSearchbykeyD.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text));

            grdResultD.DataSource = result;
            grdResultD.DataBind();
            lblvalue.Text = "403";
            BindUCtrlDDLDataOP(result);
            ViewState["SEARCH"] = result;

            //txtFDate.Text = Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1).ToShortDateString();
            //txtTDate.Text = Convert.ToDateTime(txtdate.Text).Date.ToShortDateString();

            UserDPopoup.Show();
            txtSearchbyword.Focus();
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
            DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNoByStatusNew(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            grdResultD.DataSource = result;
            grdResultD.DataBind();
            lblvalue.Text = "403";
            BindUCtrlDDLDataOP(result);
            ViewState["SEARCH"] = result;

            //txtFDate.Text = Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1).ToShortDateString();
            //txtTDate.Text = Convert.ToDateTime(txtdate.Text).Date.ToShortDateString();

            UserDPopoup.Show();
            txtSearchbyword.Focus();
        }

        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "403")
            {
                txtordno.Text = grdResultD.SelectedRow.Cells[1].Text;
                LoadOrderHeader();
                bool _ischechref = (bool)Session["Se_ref"];
                if (_ischechref)
                {
                    if (!Chkitem.Checked)
                    {
                        LoadOrderHeaderItems();
                    }
                    else
                    {
                        txtordno.Text = string.Empty;
                    }
                }
                else
                {
                    LoadOrderHeaderItems();
                }

                //if (!string.IsNullOrEmpty(txtordno.Text.Trim()))
                //{
                //    if (chkteplate.Checked != true)
                //    {
                //        lbtnadditems.Visible = false;
                //    }
                //}
                //else
                //{
                //    lbtnadditems.Visible = true;
                //}
                oImportsBLContainers = CHNLSVC.Financial.GETORDER_CONTENER(txtordno.Text);
                BindContainers();
                foreach (GridViewRow myrowitem in grdorderdetails.Rows)
                {
                    //Decimal ordqty = Convert.ToDecimal(myrowitem.Cells[10].Text);
                    //myrowitem.Cells[10].Text = DoFormat(ordqty);

                    //Decimal unitrate = Convert.ToDecimal(myrowitem.Cells[11].Text);
                    //myrowitem.Cells[11].Text = DoFormat(unitrate);

                    //Decimal value = Convert.ToDecimal(myrowitem.Cells[12].Text);
                    //myrowitem.Cells[12].Text = DoFormat(value);
                }
                Session["OP_SEARCH"] = null;
                UserDPopoup.Hide();
            }
        }

        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
            DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNoByStatusNew(SearchParams, ddlSearchbykeyD.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

            grdResultD.DataSource = result;
            grdResultD.DataBind();
            lblvalue.Text = "403";
            BindUCtrlDDLDataOP(result);
            ViewState["SEARCH"] = result;

            //txtFDate.Text = Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1).ToShortDateString();
            //txtTDate.Text = Convert.ToDateTime(txtdate.Text).Date.ToShortDateString();

            UserDPopoup.Show();
            txtSearchbyword.Focus();
        }

        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
            DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNoByStatusNew(SearchParams, ddlSearchbykeyD.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

            grdResultD.DataSource = result;
            grdResultD.DataBind();
            lblvalue.Text = "403";
            BindUCtrlDDLDataOP(result);
            ViewState["SEARCH"] = result;

            //txtFDate.Text = Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1).ToShortDateString();
            //txtTDate.Text = Convert.ToDateTime(txtdate.Text).Date.ToShortDateString();

            UserDPopoup.Show();
            txtSearchbyword.Focus();
        }

        protected void ddlmodeofshipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlmodeofshipment.SelectedValue == "S")
            {
                lblmodeship.Text = "By sea";
            }
            else if (ddlmodeofshipment.SelectedValue == "A")
            {
                lblmodeship.Text = "By air";
            }
        }

        protected void txtordno_TextChanged(object sender, EventArgs e)
        {
            txtordno.Text = txtordno.Text;
            oImportsBLContainers = CHNLSVC.Financial.GETORDER_CONTENER(txtordno.Text);
            BindContainers();
            LoadOrderHeader();
            LoadOrderHeaderItems();
            Session["Se_ref"] = false;
            //if (!string.IsNullOrEmpty(txtordno.Text.Trim()))
            //{
            //    if (chkteplate.Checked != true)
            //    {
            //        lbtnadditems.Visible = false;
            //    }
            //}
            //else
            //{
            //    lbtnadditems.Visible = true;
            //}

            foreach (GridViewRow myrowitem in grdorderdetails.Rows)
            {
                Decimal ordqty = Convert.ToDecimal(myrowitem.Cells[10].Text);
                myrowitem.Cells[10].Text = DoFormat(ordqty);

                Decimal unitrate = Convert.ToDecimal(myrowitem.Cells[11].Text);
                myrowitem.Cells[11].Text = DoFormat(unitrate);

                Decimal value = Convert.ToDecimal(myrowitem.Cells[12].Text);
                myrowitem.Cells[12].Text = DoFormat(value);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dts = (DataTable)ViewState["ItemsTable"];
                foreach (DataRow _row in dts.Rows)
                {
                    if (_row["Item"].ToString() == txtitem.Text)
                    {
                        _row["Ord Qty"] = Convert.ToDecimal(txtqty.Text) + Convert.ToDecimal(_row["Ord Qty"].ToString());
                        _row["Value"] = Convert.ToDecimal(_row["Ord Qty"]) * Convert.ToDecimal(_row["Unit Rate"]);
                    }
                }
                DataView dv = dts.DefaultView;
                dv.Sort = "Item";
                DataTable sortedDT = dv.ToTable();

                grdorderdetails.DataSource = sortedDT;
                grdorderdetails.DataBind();

                ViewState["ItemsTable"] = sortedDT;
                UserPopoup.Hide();

                CalculateTotQty();
                CalculateTotValue();


                DataTable dtqty = ViewState["KITItemsTable"] as DataTable;

                if (dtqty != null)
                {
                    if ((dtqty.Rows.Count > 0) && (txtunitrate.Enabled == false))
                    {
                        Int32 rowlinemodify = dtqty.Rows.Count - 1;
                        dtqty.Rows[rowlinemodify].SetField("Qty", txtqty.Text.Trim());
                    }
                }

                txtmodel.Text = string.Empty;
                txtitem.Text = string.Empty;
                ddlitemtype.SelectedIndex = 0;
                txtqty.Text = string.Empty;
                txtunitrate.Text = string.Empty;

                foreach (GridViewRow myrowitem in grdorderdetails.Rows)
                {
                    Decimal ordqty = Convert.ToDecimal(myrowitem.Cells[10].Text);
                    myrowitem.Cells[10].Text = DoFormat(ordqty);

                    Decimal unitrate = Convert.ToDecimal(myrowitem.Cells[11].Text);
                    myrowitem.Cells[11].Text = DoFormat(unitrate);

                    Decimal value = Convert.ToDecimal(myrowitem.Cells[12].Text);
                    myrowitem.Cells[12].Text = DoFormat(value);
                }
                lblitmdescription.Text = string.Empty;
                lblbrandit.Text = string.Empty;
                lbluomit.Text = string.Empty;
                Session["UNITINVALID"] = null;
                Session["UPDATEQTY"] = null;

            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            UserPopoup.Hide();
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            Session["OP_SEARCH"] = null;
            UserDPopoup.Hide();
        }

        protected void txtmanualref_TextChanged(object sender, EventArgs e)
        {


        }
        private void loadContainerType()
        {
            List<MST_CONTAINER_TP> oMST_CONTAINER_TPs = CHNLSVC.Financial.GET_CONTAINER_TYPES();
            if (oMST_CONTAINER_TPs != null)
            {
                List<ComboBoxObject> oItems = new List<ComboBoxObject>();
                ComboBoxObject oItem1 = new ComboBoxObject();
                oItem1.Text = "-- Select --";
                oItem1.Value = "0";
                oItems.Add(oItem1);
                foreach (MST_CONTAINER_TP item in oMST_CONTAINER_TPs)
                {
                    ComboBoxObject oItem2 = new ComboBoxObject();
                    oItem2.Text = item.Mct_desc;
                    oItem2.Value = item.Mct_tp;
                    oItems.Add(oItem2);
                }

                ddlContainersType.DataSource = oItems;
                ddlContainersType.DataTextField = "Text";
                ddlContainersType.DataValueField = "Value";
                ddlContainersType.DataBind();
            }
        }
        //Added By Dulaj 2018-Sep-@7
        private void loadContainerTypeForDetail()
        {
            List<MST_CONTAINER_TP> oMST_CONTAINER_TPs = CHNLSVC.Financial.GET_CONTAINER_TYPES();
            if (oMST_CONTAINER_TPs != null)
            {
                List<ComboBoxObject> oItems = new List<ComboBoxObject>();
                ComboBoxObject oItem1 = new ComboBoxObject();
                oItem1.Text = "-- Select --";
                oItem1.Value = "0";
                oItems.Add(oItem1);
                foreach (MST_CONTAINER_TP item in oMST_CONTAINER_TPs)
                {
                    ComboBoxObject oItem2 = new ComboBoxObject();
                    oItem2.Text = item.Mct_desc;
                    oItem2.Value = item.Mct_tp;
                    oItems.Add(oItem2);
                }

                DropDownListContainer.DataSource = oItems;
                DropDownListContainer.DataTextField = "Text";
                DropDownListContainer.DataValueField = "Value";
                DropDownListContainer.DataBind();
            }
        }
        protected void DropDownListContainer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnSbu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ADMIN/Home.aspx");
        }

        //protected void txtdescription_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtdescription.Text == "&nbsp;")
        //    {
        //        txtdescription.Text = string.Empty;
        //    }
        //    txtdescription.ForeColor = Color.Black;
        //}

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

        protected void btnContainerAdd_Click(object sender, EventArgs e)
        {
            if (ViewState["ItemsTable"] == null)
            {
                DisplayMessage("Please add the order items !", 2);
                return;
            }
            else
            {
                DataTable _dtTemp = (DataTable)ViewState["ItemsTable"];
                if (_dtTemp.Rows.Count < 1)
                {
                    DisplayMessage("Please add the order items !", 2);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtContainerNo.Text))
            {
                DisplayMessage("Please enter a Container number", 2);
                return;
            }
            decimal _tmp = 0, _cor = 0;
            if (!string.IsNullOrEmpty(txtContainerNo.Text))
            {
                _cor = decimal.TryParse(txtContainerNo.Text, out _tmp) ? Convert.ToDecimal(txtContainerNo.Text) : 0;
                if (_cor <= 0)
                {
                    DisplayMessage("Please enter valid no of units ", 2);
                    return;
                }
            }
            if (ddlContainersType.SelectedIndex == 0)
            {
                DisplayMessage("Please select a Container type", 2);
                return;
            }

            if (Session["oImportsBLContainers"] == null)
            {
                oImportsBLContainers = new List<ImportsBLContainer>();
            }
            else
            {
                oImportsBLContainers = (List<ImportsBLContainer>)Session["oImportsBLContainers"]; ;
            }
            #region validate containe no of unit 24 feb 2017
            int _NoOfConReq = 0;
            MST_CONTAINER_TP _mstConTp = CHNLSVC.Financial.GET_MST_CONTAINER_TP_DATA(new MST_CONTAINER_TP() { Mct_tp = ddlContainersType.SelectedValue }).FirstOrDefault();
            List<ImportsBLContainer> _blConList = new List<ImportsBLContainer>();
            if (Session["oImportsBLContainers"] == null)
            {
                _blConList = new List<ImportsBLContainer>();
            }
            else
            {
                _blConList = (List<ImportsBLContainer>)Session["oImportsBLContainers"];
            }
            decimal _alreadyAddContainerSpace = 0;
            if (_blConList.Count > 0)
            {
                MST_CONTAINER_TP _conTp = new MST_CONTAINER_TP();
                var _dList = _blConList.Where(c => c.Ibc_act == 1).ToList();
                foreach (var item in _dList)
                {
                    _conTp = CHNLSVC.Financial.GET_MST_CONTAINER_TP_DATA(new MST_CONTAINER_TP() { Mct_tp = item.Ibc_tp }).FirstOrDefault();
                    _alreadyAddContainerSpace = _alreadyAddContainerSpace + (_conTp.Mct_height * _conTp.Mct_length * _conTp.Mct_width * item.Ibc_unit);
                }
            }
            if (_mstConTp != null)
            {
                decimal _tmp1 = 0; decimal _containerCap = 0; decimal _itmsCap = 0; decimal t1 = new decimal(0.5);
                _containerCap = _mstConTp.Mct_height * _mstConTp.Mct_length * _mstConTp.Mct_width;
                _itmsCap = GetItemsContainerSize();
                _itmsCap = _itmsCap - _alreadyAddContainerSpace;
                if (_containerCap > 0)
                {
                    _tmp1 = _itmsCap / _containerCap;
                }
                _NoOfConReq = (int)(_tmp1);
                decimal tmp3 = _tmp1 % 1;
                if (_tmp1 % 1 != 0)
                {
                    _NoOfConReq = (int)(_tmp1 + 1);
                }
                // txtContainerNo.Text = _NoOfConReq.ToString();
            }
            if (_NoOfConReq < _cor)
            {
                DisplayMessage("Number of required units of container value is exceeded !", 2);
                // return;
            }
            #endregion
            ImportsBLContainer oNewItem = new ImportsBLContainer();
            oNewItem.Ibc_unit = Convert.ToInt32(txtContainerNo.Text.Trim());
            oNewItem.Ibc_tp = ddlContainersType.SelectedValue.ToString();
            oNewItem.Ibc_desc = ddlContainersType.SelectedItem.ToString();
            oNewItem.Ibc_act = 1;

            if (oImportsBLContainers.FindAll(x => x.Ibc_tp == ddlContainersType.SelectedValue.ToString() && x.Ibc_act == 1).Count > 0)
            {
                DisplayMessage("Container is already added.", 2);
                return;
            }
            else
            {
                oImportsBLContainers.Add(oNewItem);
                Session["oImportsBLContainers"] = oImportsBLContainers;
                BindContainers();

                txtContainerNo.Text = "";
                ddlContainersType.SelectedIndex = 0;
            }
        }

        private void BindContainers()
        {
            dgvContainers.DataSource = new int[] { };
            dgvContainers.DataBind();
            if (Session["oImportsBLContainers"] != null)
            {
                oImportsBLContainers = (List<ImportsBLContainer>)Session["oImportsBLContainers"];
                dgvContainers.DataSource = oImportsBLContainers.Where(c => c.Ibc_act == 1).ToList();
            }
            else
            {
                dgvContainers.DataSource = new int[] { };
            }

            dgvContainers.DataBind();

            for (int i = 0; i < dgvContainers.Rows.Count; i++)
            {
                GridViewRow row = dgvContainers.Rows[i];
                Label lblIbc_act = (Label)row.FindControl("lblIbc_act");
                if (lblIbc_act.Text == "0")
                {
                    row.Visible = false;
                }
            }
        }

        protected void ddlContainersType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlContainersType.SelectedIndex > 0)
                {
                    //DataTable _dtTemp = (DataTable)ViewState["ItemsTable"];
                    //if (_dtTemp.Rows.Count < 1)
                    //{
                    //    DisplayMessage("Please add the order items !", 2);
                    //    return;
                    //}

                    MST_CONTAINER_TP _mstConTp = CHNLSVC.Financial.GET_MST_CONTAINER_TP_DATA(new MST_CONTAINER_TP() { Mct_tp = ddlContainersType.SelectedValue }).FirstOrDefault();
                    List<ImportsBLContainer> _blConList = new List<ImportsBLContainer>();
                    if (Session["oImportsBLContainers"] == null)
                    {
                        _blConList = new List<ImportsBLContainer>();
                    }
                    else
                    {
                        _blConList = (List<ImportsBLContainer>)Session["oImportsBLContainers"];
                    }
                    decimal _alreadyAddContainerSpace = 0;
                    if (_blConList.Count > 0)
                    {
                        MST_CONTAINER_TP _conTp = new MST_CONTAINER_TP();
                        var _dList = _blConList.Where(c => c.Ibc_act == 1).ToList();
                        foreach (var item in _dList)
                        {
                            _conTp = CHNLSVC.Financial.GET_MST_CONTAINER_TP_DATA(new MST_CONTAINER_TP() { Mct_tp = item.Ibc_tp }).FirstOrDefault();
                            _alreadyAddContainerSpace = _alreadyAddContainerSpace + (_conTp.Mct_height * _conTp.Mct_length * _conTp.Mct_width * item.Ibc_unit);
                        }
                    }
                    if (_mstConTp != null)
                    {
                        decimal _tmp1 = 0; decimal _containerCap = 0; decimal _itmsCap = 0; int _NoOfConReq = 0; decimal t1 = new decimal(0.5);
                        _containerCap = _mstConTp.Mct_height * _mstConTp.Mct_length * _mstConTp.Mct_width;
                        _itmsCap = GetItemsContainerSize();
                        _itmsCap = _itmsCap - _alreadyAddContainerSpace;
                        if (_containerCap > 0)
                        {
                            _tmp1 = _itmsCap / _containerCap;
                        }
                        _NoOfConReq = (int)(_tmp1);
                        decimal tmp3 = _tmp1 % 1;
                        if (_tmp1 % 1 != 0)
                        {
                            _NoOfConReq = (int)(_tmp1 + 1);
                        }
                        txtContainerNo.Text = _NoOfConReq.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private decimal GetItemsContainerSize()
        {
            decimal _totValue = 0;
            DataTable _dt = (DataTable)ViewState["ItemsTable"];
            List<MasterItem> _mstList = new List<MasterItem>();
            MasterItem _mstItm = new MasterItem();
            decimal _ordQty = 0, _tmp = 0;
            foreach (DataRow _dr in _dt.Rows)
            {
                _ordQty = decimal.TryParse(_dr["ORD Qty"].ToString(), out _tmp) ? Convert.ToDecimal(_dr["ORD Qty"].ToString()) : 0;
                _mstItm = new MasterItem() { Mi_cd = _dr["Item"].ToString(), Tmp_order_qty = _ordQty };
                _mstList.Add(_mstItm);
            }
            _totValue = CHNLSVC.Financial.OrderPlanItemQuentity(_mstList);
            return _totValue;
        }

        protected void btnContaiDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                List<ImportsBLContainer> _blConList = new List<ImportsBLContainer>();
                if (Session["oImportsBLContainers"] == null)
                {
                    _blConList = new List<ImportsBLContainer>();
                }
                else
                {
                    _blConList = (List<ImportsBLContainer>)Session["oImportsBLContainers"];
                }
                Label lblIbc_tp = row.FindControl("lblIbc_tp") as Label;
                if (_blConList.Count > 0)
                {
                    var v = _blConList.Where(c => c.Ibc_tp == lblIbc_tp.Text).FirstOrDefault();
                    if (v != null)
                    {
                        v.Ibc_act = 0;
                    }
                    Session["oImportsBLContainers"] = _blConList;
                    BindContainers();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnExcelUploadClose_Click(object sender, EventArgs e)
        {
            _showExcelPop = false;
            popupExcel.Hide();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnExcClose_Click(object sender, EventArgs e)
        {

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

        protected void btnExcelDataUpload_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            _showExcelPop = true;
            popupExcel.Show();
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
        private void UploadData()
        {
            try
            {
              //   orderplancontanierForExcel = new List<ImportsBLContainer>();
                _ordPlnUpItmList = new List<OrderPlanExcelUploader>();
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
                #region Excel hdr data read
                List<string> _mths = new List<string>();
                _mths.Add(string.IsNullOrEmpty(_dtExData.Rows[0][12].ToString()) ? "" : _dtExData.Rows[0][12].ToString().Trim());
                _mths.Add(string.IsNullOrEmpty(_dtExData.Rows[0][13].ToString()) ? "" : _dtExData.Rows[0][13].ToString().Trim());
                _mths.Add(string.IsNullOrEmpty(_dtExData.Rows[0][14].ToString()) ? "" : _dtExData.Rows[0][14].ToString().Trim());

                List<string> _chnls = new List<string>();
                _chnls.Add(string.IsNullOrEmpty(_dtExData.Rows[0][22].ToString()) ? "" : _dtExData.Rows[0][22].ToString().Trim().ToUpper());
                _chnls.Add(string.IsNullOrEmpty(_dtExData.Rows[0][23].ToString()) ? "" : _dtExData.Rows[0][23].ToString().Trim().ToUpper());
                _chnls.Add(string.IsNullOrEmpty(_dtExData.Rows[0][24].ToString()) ? "" : _dtExData.Rows[0][24].ToString().Trim().ToUpper());
                _chnls.Add(string.IsNullOrEmpty(_dtExData.Rows[0][25].ToString()) ? "" : _dtExData.Rows[0][25].ToString().Trim().ToUpper());
                _chnls.Add(string.IsNullOrEmpty(_dtExData.Rows[0][26].ToString()) ? "" : _dtExData.Rows[0][25].ToString().Trim().ToUpper());
                _chnls.Add(string.IsNullOrEmpty(_dtExData.Rows[0][27].ToString()) ? "" : _dtExData.Rows[0][27].ToString().Trim().ToUpper());
                #region validate chnl
                for (int i = 0; i < _chnls.Count; i++)
                {
                    if (!string.IsNullOrEmpty(_chnls[i]))
                    {
                        string _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                        DataTable _dt = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_para, "CODE", _chnls[i]);
                        bool _dtAva = false;
                        if (_dt != null)
                        {
                            foreach (DataRow _row in _dt.Rows)
                            {
                                if (_row["CODE"].ToString() == _chnls[i])
                                {
                                    _dtAva = true;
                                    break;
                                }
                            }
                        }
                        if (!_dtAva)
                        {
                            lblExcelUploadError.Visible = true;
                            lblExcelUploadError.Text = "Invalid Channel code : " + _chnls[i];
                            _showExcelPop = true;
                            popupExcel.Show();
                            return;
                        }
                    }
                }

                #endregion
                List<string> _grds = new List<string>();
                //_grds.Add(string.IsNullOrEmpty(_dtExData.Rows[0][20].ToString()) ? "" : _dtExData.Rows[0][22].ToString().Trim());
                //_grds.Add(string.IsNullOrEmpty(_dtExData.Rows[0][21].ToString()) ? "" : _dtExData.Rows[0][23].ToString().Trim());
                //_grds.Add(string.IsNullOrEmpty(_dtExData.Rows[0][22].ToString()) ? "" : _dtExData.Rows[0][24].ToString().Trim());
                //_grds.Add(string.IsNullOrEmpty(_dtExData.Rows[0][23].ToString()) ? "" : _dtExData.Rows[0][25].ToString().Trim());
                #region read buffer lvl data
                Int32 _bufLvlCount = _dtExData.Columns.Count - 32;
                Int32 _bufColIndex = 31;
                //MSR-EA-S01-4
                while (_bufLvlCount > 0)
                {
                    _bufLvlCount--;
                    _bufColIndex++;
                    #region validate buff lvl
                    string _hdrName = string.IsNullOrEmpty(_dtExData.Rows[0][_bufColIndex].ToString()) ? "" : _dtExData.Rows[0][_bufColIndex].ToString().Trim();
                    string[] seperator = new string[] { "-" };
                    string[] _strList = _hdrName.Split(seperator, StringSplitOptions.None);

                    bool _isCorrChnl = false;
                    bool _isCorrGrd = false;
                    bool _isCorrSes = false;
                    if (_strList.Length > 3)
                    {
                        #region valiate chanel
                        if (!string.IsNullOrEmpty(_strList[0].ToString()))
                        {
                            string _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                            DataTable _dt = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_para, "CODE", _strList[0].ToString());

                            if (_dt != null)
                            {
                                foreach (DataRow _row in _dt.Rows)
                                {
                                    if (_row["CODE"].ToString() == _strList[0].ToString())
                                    {
                                        _isCorrChnl = true;
                                        break;
                                    }
                                }
                            }
                            if (!_isCorrChnl)
                            {
                                lblExcelUploadError.Visible = true;
                                lblExcelUploadError.Text = "Invalid Channel code : " + _strList[0].ToString();
                                _showExcelPop = true;
                                popupExcel.Show();
                                return;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_strList[1].ToString()))
                                {
                                    #region validate Grade code
                                    GradeMaster _grdData = CHNLSVC.General.GET_MST_GRADE_DATA(Session["UserCompanyCode"].ToString(), _strList[0].ToString(), _strList[1].ToString());
                                    if (_grdData == null)
                                    {
                                        lblExcelUploadError.Visible = true;
                                        lblExcelUploadError.Text = "Invalid grade : " + _strList[1].ToString();
                                        _showExcelPop = true;
                                        popupExcel.Show();
                                        return;
                                    }
                                    else
                                    {
                                        _isCorrGrd = true;
                                        if (!string.IsNullOrEmpty(_strList[1].ToString()))
                                        {
                                            DataTable _dtSession = CHNLSVC.General.getSeason(Session["UserCompanyCode"].ToString());
                                            if (_dtSession != null)
                                            {
                                                foreach (DataRow _row in _dtSession.Rows)
                                                {
                                                    if (_row["msa_cd"].ToString() == _strList[2].ToString())
                                                    {
                                                        _isCorrSes = true;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                    }


                    #endregion
                    if (_isCorrChnl && _isCorrGrd && _isCorrSes)
                    {
                        _grds.Add(_hdrName);
                    }
                }
                #endregion

                #endregion
                for (int i = 1; i < _dtExData.Rows.Count; i++)
                {
                    #region column null check
                    if (string.IsNullOrEmpty(_dtExData.Rows[i][0].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][1].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][2].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][3].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][4].ToString())
                        //&& string.IsNullOrEmpty(_dtExData.Rows[i][5].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][6].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][7].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][8].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][9].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][10].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][11].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][12].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][13].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][14].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][15].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][21].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][22].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][23].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][24].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][25].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][26].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][27].ToString())
                        )
                    {
                        i = _dtExData.Rows.Count;
                    }
                    #endregion
                    #region itm
                    _ordPlnUpItm = new OrderPlanExcelUploader();
                    _ordPlnUpItm.Itm_cd = string.IsNullOrEmpty(_dtExData.Rows[i][0].ToString()) ? "" : _dtExData.Rows[i][0].ToString().Trim().ToUpper();
                    _ordPlnUpItm.Model = string.IsNullOrEmpty(_dtExData.Rows[i][1].ToString()) ? "" : _dtExData.Rows[i][1].ToString().Trim().ToUpper();
                    _ordPlnUpItm.Model_desc = string.IsNullOrEmpty(_dtExData.Rows[i][2].ToString()) ? "" : _dtExData.Rows[i][2].ToString().Trim().ToUpper();
                    _ordPlnUpItm.Brand = string.IsNullOrEmpty(_dtExData.Rows[i][3].ToString()) ? "" : _dtExData.Rows[i][3].ToString().Trim().ToUpper();
                    _ordPlnUpItm.Uom = string.IsNullOrEmpty(_dtExData.Rows[i][4].ToString()) ? "" : _dtExData.Rows[i][4].ToString().Trim().ToUpper();
                    _ordPlnUpItm.Supplier = string.IsNullOrEmpty(_dtExData.Rows[i][6].ToString()) ? "" : _dtExData.Rows[i][6].ToString().Trim().ToUpper();
                    _ordPlnUpItm.Tmp_unit_price = string.IsNullOrEmpty(_dtExData.Rows[i][7].ToString()) ? "" : _dtExData.Rows[i][7].ToString().Trim();
                    _ordPlnUpItm.Currency = string.IsNullOrEmpty(_dtExData.Rows[i][8].ToString()) ? "" : _dtExData.Rows[i][8].ToString().Trim().ToUpper();
                    _ordPlnUpItm.Traid_term = string.IsNullOrEmpty(_dtExData.Rows[i][9].ToString()) ? "" : _dtExData.Rows[i][9].ToString().Trim().ToUpper();
                    _ordPlnUpItm.Tmp_mode_of_shipment = string.IsNullOrEmpty(_dtExData.Rows[i][10].ToString()) ? "" : _dtExData.Rows[i][10].ToString().Trim().ToUpper();
                    _ordPlnUpItm.Tmp_port_of_origin = string.IsNullOrEmpty(_dtExData.Rows[i][11].ToString()) ? "" : _dtExData.Rows[i][11].ToString().Trim().ToUpper();
                    #region mth data add
                    _ordPlnUpItm.MonthData = new OrderPlanExcelUploader.OrderPlanExcelMonthData();
                    _ordPlnUpItm.MonthData.Mth_cd = _mths[0];
                    _ordPlnUpItm.MonthData.Tmp_mth_qty = string.IsNullOrEmpty(_dtExData.Rows[i][12].ToString()) ? "" : _dtExData.Rows[i][12].ToString().Trim().ToUpper();
                    _ordPlnUpItm.MonthDataList.Add(_ordPlnUpItm.MonthData);
                    _ordPlnUpItm.MonthData = new OrderPlanExcelUploader.OrderPlanExcelMonthData();
                    _ordPlnUpItm.MonthData.Mth_cd = _mths[1];
                    _ordPlnUpItm.MonthData.Tmp_mth_qty = string.IsNullOrEmpty(_dtExData.Rows[i][13].ToString()) ? "" : _dtExData.Rows[i][13].ToString().Trim().ToUpper();
                    _ordPlnUpItm.MonthDataList.Add(_ordPlnUpItm.MonthData);
                    _ordPlnUpItm.MonthData = new OrderPlanExcelUploader.OrderPlanExcelMonthData();
                    _ordPlnUpItm.MonthData.Mth_cd = _mths[2];
                    _ordPlnUpItm.MonthData.Tmp_mth_qty = string.IsNullOrEmpty(_dtExData.Rows[i][14].ToString()) ? "" : _dtExData.Rows[i][14].ToString().Trim().ToUpper();
                    _ordPlnUpItm.MonthDataList.Add(_ordPlnUpItm.MonthData);
                    #endregion
                    _ordPlnUpItm.Main_cat = string.IsNullOrEmpty(_dtExData.Rows[i][15].ToString()) ? "" : _dtExData.Rows[i][15].ToString().Trim().ToUpper().ToUpper();
                    _ordPlnUpItm.Cat_1 = string.IsNullOrEmpty(_dtExData.Rows[i][16].ToString()) ? "" : _dtExData.Rows[i][16].ToString().Trim().ToUpper().ToUpper();
                    //_ordPlnUpItm.L = string.IsNullOrEmpty(_dtExData.Rows[i][17].ToString()) ? "" : _dtExData.Rows[i][17].ToString().Trim().ToUpper().ToUpper();
                    #region chnl data
                    _ordPlnUpItm.ChnlData = new OrderPlanExcelUploader.OrderPlanExcelChnlData();
                    _ordPlnUpItm.ChnlData.All_ch_cd = _chnls[0];
                    _ordPlnUpItm.ChnlData.Tmp_all_ch_qty = string.IsNullOrEmpty(_dtExData.Rows[i][21].ToString()) ? "" : _dtExData.Rows[i][21].ToString().Trim();
                    _ordPlnUpItm.ChnlDataList.Add(_ordPlnUpItm.ChnlData);
                    _ordPlnUpItm.ChnlData = new OrderPlanExcelUploader.OrderPlanExcelChnlData();
                    _ordPlnUpItm.ChnlData.All_ch_cd = _chnls[1];
                    _ordPlnUpItm.ChnlData.Tmp_all_ch_qty = string.IsNullOrEmpty(_dtExData.Rows[i][22].ToString()) ? "" : _dtExData.Rows[i][22].ToString().Trim();
                    _ordPlnUpItm.ChnlDataList.Add(_ordPlnUpItm.ChnlData);
                    _ordPlnUpItm.ChnlData = new OrderPlanExcelUploader.OrderPlanExcelChnlData();
                    _ordPlnUpItm.ChnlData.All_ch_cd = _chnls[2];
                    _ordPlnUpItm.ChnlData.Tmp_all_ch_qty = string.IsNullOrEmpty(_dtExData.Rows[i][23].ToString()) ? "" : _dtExData.Rows[i][23].ToString().Trim();
                    _ordPlnUpItm.ChnlDataList.Add(_ordPlnUpItm.ChnlData);
                    _ordPlnUpItm.ChnlData = new OrderPlanExcelUploader.OrderPlanExcelChnlData();
                    _ordPlnUpItm.ChnlData.All_ch_cd = _chnls[3];
                    _ordPlnUpItm.ChnlData.Tmp_all_ch_qty = string.IsNullOrEmpty(_dtExData.Rows[i][24].ToString()) ? "" : _dtExData.Rows[i][24].ToString().Trim();
                    _ordPlnUpItm.ChnlDataList.Add(_ordPlnUpItm.ChnlData);
                    _ordPlnUpItm.ChnlData = new OrderPlanExcelUploader.OrderPlanExcelChnlData();
                    _ordPlnUpItm.ChnlData.All_ch_cd = _chnls[4];
                    _ordPlnUpItm.ChnlData.Tmp_all_ch_qty = string.IsNullOrEmpty(_dtExData.Rows[i][25].ToString()) ? "" : _dtExData.Rows[i][25].ToString().Trim();
                    _ordPlnUpItm.ChnlDataList.Add(_ordPlnUpItm.ChnlData);
                    _ordPlnUpItm.ChnlData = new OrderPlanExcelUploader.OrderPlanExcelChnlData();
                    _ordPlnUpItm.ChnlData.All_ch_cd = _chnls[5];
                    _ordPlnUpItm.ChnlData.Tmp_all_ch_qty = string.IsNullOrEmpty(_dtExData.Rows[i][26].ToString()) ? "" : _dtExData.Rows[i][26].ToString().Trim();
                    _ordPlnUpItm.ChnlDataList.Add(_ordPlnUpItm.ChnlData);
                    #endregion
                    _ordPlnUpItm.Price_tp = string.IsNullOrEmpty(_dtExData.Rows[i][28].ToString()) ? "" : _dtExData.Rows[i][28].ToString().Trim();
                    _ordPlnUpItm.Proj_name = string.IsNullOrEmpty(_dtExData.Rows[i][29].ToString()) ? "" : _dtExData.Rows[i][29].ToString().Trim();
                    _ordPlnUpItm.Ref_no = string.IsNullOrEmpty(_dtExData.Rows[i][30].ToString()) ? "" : _dtExData.Rows[i][30].ToString().Trim();

                    #region container details


                   // Added By Dulaj 2018/May/15
                    
                    //ImportsBLContainer _importsBLContainer = new ImportsBLContainer();
                    //_importsBLContainer.Ibc_tp = string.IsNullOrEmpty(_dtExData.Rows[i][17].ToString()) ? "" : _dtExData.Rows[i][17].ToString().Trim();
                    //orderplancontanierForExcel.Add(_importsBLContainer);
                    //Session["ContainerParams"] = orderplancontanierForExcel;

                    _ordPlnUpItm.LcL = string.IsNullOrEmpty(_dtExData.Rows[i][17].ToString()) ? "" : _dtExData.Rows[i][17].ToString().Trim();
                    _ordPlnUpItm.Ft20 = string.IsNullOrEmpty(_dtExData.Rows[i][18].ToString()) ? "" : _dtExData.Rows[i][18].ToString().Trim();
                    _ordPlnUpItm.Ft40 = string.IsNullOrEmpty(_dtExData.Rows[i][19].ToString()) ? "" : _dtExData.Rows[i][19].ToString().Trim();
                    _ordPlnUpItm.Hq40 = string.IsNullOrEmpty(_dtExData.Rows[i][20].ToString()) ? "" : _dtExData.Rows[i][20].ToString().Trim();
                    _ordPlnUpItm.Af = string.IsNullOrEmpty(_dtExData.Rows[i][21].ToString()) ? "" : _dtExData.Rows[i][21].ToString().Trim();

                    _ordPlnUpItm.TradeAgreement = -2;
                    //TradeAgreement
                    string tradeAgreeMent = string.IsNullOrEmpty(_dtExData.Rows[i][31].ToString()) ? "" : _dtExData.Rows[i][31].ToString().Trim();
                    if(tradeAgreeMent.Equals("YES"))
                    {
                        _ordPlnUpItm.TradeAgreement = 1;
                    }
                    if (tradeAgreeMent.Equals("NO"))
                    {
                        _ordPlnUpItm.TradeAgreement = 0;
                    }
                    if (tradeAgreeMent.Equals(""))
                    {
                        _ordPlnUpItm.TradeAgreement = -1;
                    }
                    #endregion



                    #region buff LVL
                    _bufLvlCount = _dtExData.Columns.Count - 32;
                    _bufColIndex = 32;
                    //MSR-EA-S01-4
                    Int32 _listBFLIndex = 0;
                    while (_bufLvlCount > 0)
                    {
                        string _hdrName = string.IsNullOrEmpty(_dtExData.Rows[0][_bufColIndex].ToString()) ? "" : _dtExData.Rows[0][_bufColIndex].ToString().Trim();
                        decimal _buffQty = ToDecimal(_dtExData.Rows[i][_bufColIndex].ToString());
                        if (!string.IsNullOrEmpty(_hdrName) && _buffQty != 0)
                        {
                            string[] seperator = new string[] { "-" };
                            string[] _strList = _hdrName.Split(seperator, StringSplitOptions.None);
                            if (_strList.Length > 3)
                            {
                                _ordPlnUpItm.BuffLvl = new OrderPlanExcelUploader.OrderPlanExcelBuffLvl();
                                _ordPlnUpItm.BuffLvl.Mbc_chnl = _strList[0].ToString();
                                _ordPlnUpItm.BuffLvl.Mbc_grade = _strList[1].ToString();
                                _ordPlnUpItm.BuffLvl.Mbc_season = _strList[2].ToString();
                                _ordPlnUpItm.BuffLvl.Buff_qty = _buffQty;
                                _ordPlnUpItm.BuffLvlDataList.Add(_ordPlnUpItm.BuffLvl);
                            }
                        }
                        _listBFLIndex++;
                        _bufLvlCount--;
                        _bufColIndex++;
                    }
                    #endregion
                    _ordPlnUpItm.Line = 2;
                    if (_ordPlnUpItmList.Count > 0)
                    {
                        _ordPlnUpItm.Line = _ordPlnUpItmList.Max(c => c.Line) + 1;
                    }
                    _ordPlnUpItmList.Add(_ordPlnUpItm);
                    #endregion
                }
                popupExcel.Hide();
                popOpExcSave.Show();
                //ProcessExcelData();
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
        private void ProcessExcelData()
        {
            List<OrderPlanExcelUploader> _errList = new List<OrderPlanExcelUploader>();
            OrderPlanExcelUploader _err = new OrderPlanExcelUploader();

            List<MasterUOM> _uomList = CHNLSVC.General.GetItemUOM();
            List<MasterCurrency> _currList = CHNLSVC.General.GetAllCurrency("");
            MasterBusinessEntity _busEntity = new MasterBusinessEntity();
            foreach (var item in _ordPlnUpItmList)
            {
                _err = new OrderPlanExcelUploader();
                _err.Line = item.Line;
                _err.Itm_cd = item.Itm_cd;
                _err.Model = item.Model;
                _err.Tmp_err_text = "";
                _err.Tmp_err = "";
                bool _isItmBase = false;
                string _erMsg = "";
                item.IO_CRE_BY = Session["UserCompanyCode"].ToString();
                #region validate doc #
                //Added By Dulaj 2018/may/28
                if (item.TradeAgreement==-2)
                {
                    _erMsg = "Please enter valid TradeAgreement ";
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                //
                if (string.IsNullOrEmpty(item.Itm_cd) && string.IsNullOrEmpty(item.Model))
                {
                    _erMsg = "Please enter item code or model # ";
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (string.IsNullOrEmpty(item.Itm_cd) && !string.IsNullOrEmpty(item.Model))
                {
                    List<MasterItem> _itmlist = CHNLSVC.Inventory.GetAllItemByModel(item.Model);
                    if (_itmlist != null)
                    {
                        if (_itmlist.Count == 1)
                        {
                            item.Itm_cd = _itmlist[0].Mi_cd;
                        }
                        else
                        {
                            _erMsg = "Multiple Items found !";
                            //_err.Tmp_err_text = item.Supplier;
                            _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Model : _err.Tmp_err_text;
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(item.Itm_cd))
                {
                    #region Item Base
                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(item.Itm_cd);
                    if (_mstItm == null)
                    {
                        _erMsg = "Invalid item code ! ";
                        //    _err.Tmp_err_text = item.Itm_cd;
                        _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Itm_cd : _err.Tmp_err_text;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                    else
                    {
                        if (_mstItm.Mi_is_ser1 == -1)
                        {
                            item.Is_dec_itm = true;
                        }
                        _isItmBase = true;
                        item.Is_itm_base = true;
                        if (!string.IsNullOrEmpty(item.Model))
                        {
                            if (item.Model != _mstItm.Mi_model)
                            {
                                _erMsg = "Invalid model code ! ";
                                //    _err.Tmp_err_text = _mstItm.Mi_model;
                                _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? _mstItm.Mi_model : _err.Tmp_err_text;
                                _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                            }
                            if (item.Model_desc != _mstItm.Mi_shortdesc && !string.IsNullOrEmpty(item.Model_desc))
                            {
                                _erMsg = "Invalid model description ! ";
                                // _err.Tmp_err_text = item.Model_desc;
                                _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Itm_cd : _err.Tmp_err_text;
                                _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                            }
                        }
                        item.Model = _mstItm.Mi_model;
                        //MasterItemModel _mstItmMod = CHNLSVC.General.GetItemModelNew(item.Model).FirstOrDefault();
                        //if (_mstItmMod != null)
                        //{
                        //    if (!string.IsNullOrEmpty(item.Model_desc))
                        //    {
                        //        if (item.Model_desc != _mstItmMod.Mm_desc)
                        //        {
                        //            _erMsg = "Invalid model description ! ";
                        //            _err.Tmp_err_text = item.Model_desc;
                        //            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        //        }
                        //    }
                        //}
                        if (item.Brand != _mstItm.Mi_brand)
                        {
                            if (!string.IsNullOrEmpty(item.Brand))
                            {
                                _erMsg = "Invalid brand ! ";
                                //  _err.Tmp_err_text = item.Brand;
                                _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Brand : _err.Tmp_err_text;
                                _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                            }
                        }
                        item.Brand = _mstItm.Mi_brand;
                        item.Uom = _mstItm.Mi_itm_uom;
                        item.Main_cat = _mstItm.Mi_cate_1;
                        item.Cat_1 = _mstItm.Mi_cate_2;
                        item.Cat_2 = _mstItm.Mi_cate_3;
                        item.Cat_3 = _mstItm.Mi_cate_4;
                        item.Itm_desc = _mstItm.Mi_shortdesc;
                        item.Itm_tp = _mstItm.Mi_itm_tp;
                        item.Itm_clr = _mstItm.Mi_color_ext;


                    }
                    #endregion
                }
                if (!_isItmBase)
                {
                    #region model base
                    List<MasterItemModel> _mstItmModList = CHNLSVC.General.GetItemModelNew(item.Model);
                    MasterItemModel _mstItmMod = null;
                    if (_mstItmModList != null)
                    {
                        if (_mstItmModList.Count > 0)
                        {
                            _mstItmMod = _mstItmModList[0];
                        }
                    }
                    if (_mstItmMod != null)
                    {
                        _erMsg = "Model is available please add the item code ! ";
                        //  _err.Tmp_err_text = item.Model;
                        _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Model : _err.Tmp_err_text;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                    else
                    {
                        item.Is_new_model = true;
                        if (string.IsNullOrEmpty(item.Model_desc))
                        {
                            _erMsg = "Please enter model description ! ";
                            //  _err.Tmp_err_text = item.Model;
                            _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Model : _err.Tmp_err_text;
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                        if (string.IsNullOrEmpty(item.Brand))
                        {
                            _erMsg = "Please enter brand ! ";
                            // _err.Tmp_err_text = item.Model;
                            _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Model : _err.Tmp_err_text;
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                        if (!string.IsNullOrEmpty(item.Brand))
                        {
                            MasterItemBrand _itmBrand = CHNLSVC.General.GET_ITM_BRAND_DATA(item.Brand);
                            if (_itmBrand == null)
                            {
                                _erMsg = "Invalid brand code  ! ";
                                // _err.Tmp_err_text = item.Brand;
                                _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Brand : _err.Tmp_err_text;
                                _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                            }
                        }
                        if (string.IsNullOrEmpty(item.Uom))
                        {
                            _erMsg = "Please enter unit of measure ! ";
                            //_err.Tmp_err_text = item.Model;
                            _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Model : _err.Tmp_err_text;
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                        if (!string.IsNullOrEmpty(item.Uom))
                        {
                            var _vUom = _uomList.Where(c => c.Msu_cd == item.Uom && c.Msu_act).FirstOrDefault();
                            if (_vUom == null)
                            {
                                _erMsg = "Invalid unit of measure ! ";
                                // _err.Tmp_err_text = item.Uom;
                                _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Uom : _err.Tmp_err_text;
                                _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                            }
                        }
                        if (string.IsNullOrEmpty(item.Main_cat))
                        {
                            _erMsg = "Please enter main category ! ";
                            //  _err.Tmp_err_text = item.Main_cat;
                            _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Main_cat : _err.Tmp_err_text;
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                        if (!string.IsNullOrEmpty(item.Main_cat))
                        {
                            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, "CODE", item.Main_cat);
                            bool _dtAva = false;
                            foreach (DataRow _row in _result.Rows)
                            {
                                if (_row["CODE"].ToString() == item.Main_cat)
                                {
                                    _dtAva = true;
                                    break;
                                }
                            }
                            if (!_dtAva)
                            {
                                _erMsg = "Invalid main category ! ";
                                // _err.Tmp_err_text = item.Main_cat;
                                _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Main_cat : _err.Tmp_err_text;
                                _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                            }
                        }

                        string cat02txt = "333:" + item.Main_cat + "||masterCat2||CAT_Sub1|";
                       // string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                        DataTable _cat02 = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(cat02txt, null, null);
                        int count = _cat02.AsEnumerable()
                         .Count(row => row.Field<string>("Code") == item.Cat_1);
                        if (count == 0)
                        {
                            _erMsg = " Subcategory is not valid !";
                             _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Cat_1 : _err.Tmp_err_text;
                                _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                            
                        }
                    }
                    #endregion
                }
                if (string.IsNullOrEmpty(item.Supplier))
                {
                    _erMsg = "Please enter supplier code ! ";
                    //_err.Tmp_err_text = item.Supplier;
                    _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Supplier : _err.Tmp_err_text;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (!string.IsNullOrEmpty(item.Supplier))
                {
                    _busEntity = CHNLSVC.Inventory.GET_MST_BUSENTITY_DATA(Session["UserCompanyCode"].ToString(), item.Supplier, "S").FirstOrDefault();
                    if (_busEntity == null)
                    {
                        _erMsg = "Invalid supplier code ! ";
                        //_err.Tmp_err_text = item.Supplier;
                        _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Supplier : _err.Tmp_err_text;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                }
                IList<string> containerTypes = new List<string>();
                if (!(string.IsNullOrEmpty(item.LcL)))
                {
                    containerTypes.Add("LCL");
                }
                if (!(string.IsNullOrEmpty(item.Ft20)))
                {
                    containerTypes.Add("Ft20");
                }
                if (!(string.IsNullOrEmpty(item.Ft40)))
                {
                    containerTypes.Add("Ft40");
                }
                if (!(string.IsNullOrEmpty(item.Hq40)))
                {
                    containerTypes.Add("Hq40");
                }
                if (!(string.IsNullOrEmpty(item.Af)))
                {
                    containerTypes.Add("Af");
                }
                if (containerTypes.Count>1)
                {
                    string types = string.Join(",", containerTypes.ToArray());
                    _erMsg = "More than one container types! ";
                    _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? types : _err.Tmp_err_text;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (item.Is_itm_base)
                {
                    BusEntityItem _busEntItm = CHNLSVC.General.GetBuninessEntityItemBySupplierItm(Session["UserCompanyCode"].ToString(), item.Supplier, item.Itm_cd);
                    if (_busEntItm == null)
                    {
                        _erMsg = "Item is not allocated for selected supplier! ";
                        //_err.Tmp_err_text = item.Supplier;
                        _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? "Supplier : " + item.Supplier + " Item : " + item.Itm_cd : _err.Tmp_err_text;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                }
                if (string.IsNullOrEmpty(item.Price_tp))
                {
                    _erMsg = "Please enter price type ! ";
                    // _err.Tmp_err_text = item.Price_tp;
                    _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? "Null" : _err.Tmp_err_text;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (!string.IsNullOrEmpty(item.Price_tp))
                {
                    if (item.Price_tp != "N")
                    {
                        if (item.Price_tp != "S")
                        {
                            _erMsg = "Invalid price type ! ";
                            _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Price_tp : _err.Tmp_err_text;
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                    }
                }
                if (string.IsNullOrEmpty(item.Tmp_unit_price))
                {
                    _erMsg = "Please enter unit price ! ";
                    _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Tmp_unit_price : _err.Tmp_err_text;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (!string.IsNullOrEmpty(item.Tmp_unit_price))
                {
                    item.Unit_price = ToDecimal(item.Tmp_unit_price);
                    if (item.Unit_price < 0)
                    {
                        _erMsg = "Please enter vlaid unit price ! ";
                        _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Tmp_unit_price : _err.Tmp_err_text;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                }
                if (string.IsNullOrEmpty(item.Currency))
                {
                    _erMsg = "Please enter currency code ! ";
                    _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Currency : _err.Tmp_err_text;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (!string.IsNullOrEmpty(item.Currency))
                {
                    var _vCur = _currList.Where(c => c.Mcr_cd == item.Currency).FirstOrDefault();
                    if (_vCur == null)
                    {
                        _erMsg = "Invalid currency code ! ";
                        _err.Tmp_err_text = item.Currency;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                    DataTable ERateTbl = CHNLSVC.Financial.GetExchangeRate(Session["UserCompanyCode"].ToString(), item.Currency, "LKR");
                    if (ERateTbl != null)
                    {
                        if (ERateTbl.Rows.Count > 0)
                        {
                            item.CurrencyRate = Convert.ToDecimal(ERateTbl.Rows[0][5].ToString());
                        }
                        else
                        {
                            _erMsg = "Exchange rate not setup";
                            _err.Tmp_err_text = item.Currency;
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                    }
                    else
                    {
                        _erMsg = "Currency not setup";
                        _err.Tmp_err_text = item.Currency;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                }
                if (string.IsNullOrEmpty(item.Traid_term))
                {
                    _erMsg = "Please enter traid term ! ";
                    _err.Tmp_err_text = item.Traid_term;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (!string.IsNullOrEmpty(item.Traid_term))
                {
                    string SearchParamsTT = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
                    DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParamsTT, "CODE", item.Traid_term);
                    bool _dtAva = false;
                    foreach (DataRow _row in result.Rows)
                    {
                        if (_row["CODE"].ToString() == item.Traid_term)
                        {
                            _dtAva = true;
                            break;
                        }
                    }
                    if (!_dtAva)
                    {
                        _erMsg = "Invalid traid term ! ";
                        _err.Tmp_err_text = item.Traid_term;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                }
                if (string.IsNullOrEmpty(item.Tmp_mode_of_shipment))
                {
                    _erMsg = "Please enter mode of shipment ! ";
                    _err.Tmp_err_text = item.Tmp_mode_of_shipment;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (!string.IsNullOrEmpty(item.Tmp_mode_of_shipment))
                {
                    if (item.Tmp_mode_of_shipment == "AIR")
                    {
                        item.Mode_of_shipment = "A";
                    }
                    else if (item.Tmp_mode_of_shipment == "SEA")
                    {
                        item.Mode_of_shipment = "S";
                    }
                    else
                    {
                        _erMsg = "Invalid mode of shipment ! ";
                        _err.Tmp_err_text = item.Tmp_mode_of_shipment;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                }
                if (string.IsNullOrEmpty(item.Tmp_port_of_origin))
                {
                    _erMsg = "Please enter port of origin ! ";
                    _err.Tmp_err_text = item.Tmp_port_of_origin;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (!string.IsNullOrEmpty(item.Tmp_port_of_origin))
                {
                    DataTable _dt = CHNLSVC.Financial.GetSupplierPorts(Session["UserCompanyCode"].ToString(), item.Supplier);
                    bool _dtAva = false;
                    foreach (DataRow _row in _dt.Rows)
                    {
                        if (_row["MP_NAME"].ToString().ToUpper() == item.Tmp_port_of_origin.ToUpper())
                        {
                            _dtAva = true;
                            item.Port_of_origin = _row["MSPR_FRM_PORT"].ToString();
                            break;
                        }
                    }
                    if (!_dtAva)
                    {
                        _erMsg = "Invalid port of origin ! ";
                        _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? item.Tmp_port_of_origin : _err.Tmp_err_text;
                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                    }
                }
                #region mth data fill
                foreach (var _mth in item.MonthDataList)
                {
                    if (!string.IsNullOrEmpty(_mth.Mth_cd))
                    {
                        string[] seperator = new string[] { "-" };
                        string[] _strList = _mth.Mth_cd.Split(seperator, StringSplitOptions.None);
                        if (_strList != null)
                        {
                            if (_strList.Length == 2)
                            {
                                if (_strList[0] != null)
                                {
                                    if (!string.IsNullOrEmpty(_strList[0].ToString()))
                                    {
                                        _mth.Year = ToInteger(_strList[0].ToString());
                                        Int32 _maxYear = DateTime.Today.AddYears(2).Year;
                                        Int32 _minYear = DateTime.Today.AddYears(-2).Year;
                                        if (_mth.Year < _minYear)
                                        {
                                            _erMsg = "Please check the order year ! ";
                                            _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? _mth.Year.ToString() : _err.Tmp_err_text;
                                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                                        }
                                        if (_mth.Year > _maxYear)
                                        {
                                            _erMsg = "Please check the order year ! ";
                                            _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? _mth.Year.ToString() : _err.Tmp_err_text;
                                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                                        }
                                    }
                                }
                                if (_strList[1] != null)
                                {
                                    if (!string.IsNullOrEmpty(_strList[1].ToString()))
                                    {
                                        string _m = _strList[1].ToString().ToUpper();
                                        #region mth setup
                                        _mth.Mth =
                                            _m == "JAN" ? 1 :
                                            _m == "FEB" ? 2 :
                                            _m == "MAR" ? 3 :
                                            _m == "APR" ? 4 :
                                            _m == "MAY" ? 5 :
                                            _m == "JUN" ? 6 :
                                            _m == "JUL" ? 7 :
                                            _m == "AUG" ? 8 :
                                            _m == "SEP" ? 9 :
                                            _m == "OCT" ? 10 :
                                            _m == "NOV" ? 11 :
                                            _m == "DEC" ? 12 :
                                            0;
                                        #endregion
                                    }
                                }
                            }
                        }
                        if (_mth.Year != 0 && _mth.Mth != 0)
                        {
                            if (item.MonthDataList != null)
                            {
                                var _mthAva = item.MonthDataList.Where(c => c.Year == _mth.Year && c.Mth == _mth.Mth).ToList();
                                if (_mthAva != null)
                                {
                                    if (_mthAva.Count > 1)
                                    {
                                        _erMsg = "Please check the order year and month ! ";
                                        _err.Tmp_err_text = "";
                                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                                    }
                                }
                            }
                            _mth.Is_ok = true;
                        }
                        else
                        {
                            _erMsg = "Please check the order year and month ! ";
                            _err.Tmp_err_text = "";
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                        if (_mth.Is_ok)
                        {
                            _mth.Mth_qty = ToDecimal(_mth.Tmp_mth_qty);
                            if (_isItmBase)
                            {
                                if (!item.Is_dec_itm)
                                {
                                    if ((_mth.Mth_qty % 1) > 0)
                                    {
                                        _erMsg = "Please check the order quantity ! ";
                                        _err.Tmp_err_text = "";
                                        _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                                    }
                                }
                            }
                        }
                        if (_mth.Mth_qty > 0)
                        {
                            item.Is_corr_qty = true;
                        }
                        if (_mth.Mth_qty < 0)
                        {
                            _erMsg = "Please check the order quantity ! ";
                            _err.Tmp_err_text = _mth.Mth_qty.ToString();
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                    }
                }
                var vMthAva = item.MonthDataList.Where(c => c.Is_ok && c.Mth_qty > 0).FirstOrDefault();
                if (vMthAva == null)
                {
                    _erMsg = "Please check the order year and month ! ";
                    _err.Tmp_err_text = item.Tmp_port_of_origin;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                item.MonthDataList = item.MonthDataList.Where(c => c.Is_ok && c.Mth_qty > 0).ToList();
                #endregion
                #region chnl
                foreach (var _chn in item.ChnlDataList)
                {
                    if (!string.IsNullOrEmpty(_chn.All_ch_cd))
                    {
                        _chn.All_ch_qty = ToDecimal(_chn.Tmp_all_ch_qty);
                        if (_chn.All_ch_qty != 0)
                        {
                            _chn.Is_ok = true;
                        }
                    }
                }
                var vChnAva = item.ChnlDataList.Where(c => c.Is_ok).FirstOrDefault();
                if (vChnAva == null)
                {
                    //_erMsg = "Please check the channel allocation ! ";
                    //_err.Tmp_err_text = "";
                    //_err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                #endregion
                if (string.IsNullOrEmpty(item.Tmp_unallocate_Qty))
                {
                    //_erMsg = "Please enter unallocation quentity ! ";
                    //_err.Tmp_err_text = item.Tmp_unallocate_Qty;
                    //_err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                item.Unallocate_Qty = ToDecimal(item.Tmp_unallocate_Qty);
                foreach (var _buf in item.BuffLvlDataList)
                {
                    if (!string.IsNullOrEmpty(_buf.Tmp_buff_qty))
                    {
                        _erMsg = "Please enter buffer level quentity ! ";
                    }
                    else
                    {
                        //_buf.Buff_qty = ToDecimal(_buf.Tmp_buff_qty);
                        if (_buf.Buff_qty < 0)
                        {
                            _erMsg = "Please check the buffer level quentity ! ";
                            _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? _buf.Buff_qty.ToString() : _err.Tmp_err_text;
                            _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                        }
                        if (_isItmBase)
                        {
                            if (!item.Is_dec_itm)
                            {
                                if ((_buf.Buff_qty % 1) > 0)
                                {
                                    _erMsg = "Please check the buffer level quentity ! ";
                                    _err.Tmp_err_text = string.IsNullOrEmpty(_err.Tmp_err_text) ? _buf.Buff_qty.ToString() : _err.Tmp_err_text;
                                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                                }
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(item.Ref_no))
                {
                    _erMsg = "Please enter reference ! ";
                    _err.Tmp_err_text = item.Ref_no;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                var _opTotQty = item.MonthDataList.Sum(c => c.Mth_qty);
                var _opTotAllQty = item.ChnlDataList.Sum(c => c.All_ch_qty);
                if (_opTotQty < (_opTotAllQty + item.Unallocate_Qty))
                {
                    _erMsg = "Allocation quantity exceed ! ";
                    _err.Tmp_err_text = item.Ref_no;
                    _err.Tmp_err = string.IsNullOrEmpty(_err.Tmp_err) ? _erMsg : _err.Tmp_err;
                }
                if (!string.IsNullOrEmpty(_err.Tmp_err))
                {
                    _errList.Add(_err);
                }
                #endregion
                #region add default data
                item.IO_COM = Session["UserCompanyCode"].ToString();
                item.IO_SBU = Session["UserSBU"].ToString();
                item.IO_TP = "S";
                item.IO_REM = "";
                item.IO_CRE_BY = Session["UserID"].ToString();
                item.IO_MOD_BY = Session["UserID"].ToString();
                item.IO_SESSION_ID = Session["SessionID"].ToString();
                item.Itm_tp = string.IsNullOrEmpty(item.Itm_tp) ? "M" : item.Itm_tp;
                item.Itm_clr = string.IsNullOrEmpty(item.Itm_clr) ? "N/A" : item.Itm_clr;
                item.Def_loc = Session["UserDefLoca"].ToString();
                #endregion
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
                //popupExcel.Hide();
                //popOpExcSave.Show();
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = null;
                mastAutoNo.Aut_modify_dt = null;
                mastAutoNo.Aut_moduleid = "OP";
                mastAutoNo.Aut_start_char = "ORP";
                mastAutoNo.Aut_year = DateTime.Now.Year;
                string _errr = "", _docNo = "";
                _ordPlnUpItmList = _ordPlnUpItmList.Where(c => c.Is_corr_qty).ToList();
                List<OrderPlanHeader> _opHdrdocs = new List<OrderPlanHeader>();
                //
                //Added By Dulaj 2018/Sep/27
                foreach(OrderPlanExcelUploader opExcel in _ordPlnUpItmList)
                {
                    if(!(string.IsNullOrEmpty(opExcel.LcL)))
                    {
                        opExcel.ContainerType = "LCL";
                        opExcel.ContainerQty = Convert.ToDecimal(opExcel.LcL);
                    }
                    if (!(string.IsNullOrEmpty(opExcel.Af)))
                    {
                        opExcel.ContainerType = "AIR";
                        opExcel.ContainerQty = Convert.ToDecimal(opExcel.Af);
                    }
                    if (!(string.IsNullOrEmpty(opExcel.Ft20)))
                    {
                        opExcel.ContainerType = "20FT";
                        opExcel.ContainerQty = Convert.ToDecimal(opExcel.Ft20);
                    }
                    if (!(string.IsNullOrEmpty(opExcel.Ft40)))
                    {
                        opExcel.ContainerType = "40FT";
                        opExcel.ContainerQty = Convert.ToDecimal(opExcel.Ft40);
                    }
                    if (!(string.IsNullOrEmpty(opExcel.Hq40)))
                    {
                        opExcel.ContainerType = "40HC";
                        opExcel.ContainerQty = Convert.ToDecimal(opExcel.Hq40);
                    }
                }
              //  orderplancontanierForExcel = Session["ContainerParams"] as List<ImportsBLContainer>;
                Int32 _res = CHNLSVC.Financial.CreateOrderPlansusingExcel(_ordPlnUpItmList, mastAutoNo, out _opHdrdocs, out  _docNo, out  _errr);
                if (_res > 0)
                {
                    bool _shsavePop = false;
                    if (_opHdrdocs != null)
                    {
                        if (_opHdrdocs.Count > 5)
                        {
                            _shsavePop = true;
                        }
                    }
                    if (!_shsavePop)
                    {
                        DispMsg("Successfully saved : " + _docNo, "S");
                    }
                    else
                    {
                        dgvDocNo.DataSource = _opHdrdocs;
                        dgvDocNo.DataBind();
                        popDocNoShow.Show();
                    }
                }
                else
                {
                    DispMsg(_errr, "E");
                }
            }
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
        protected void btnCancelProcess_Click(object sender, EventArgs e)
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


    }
}

