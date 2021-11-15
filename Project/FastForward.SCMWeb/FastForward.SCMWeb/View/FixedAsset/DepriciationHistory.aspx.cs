using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Fixed_Asset
{
    public partial class DepriciationHistory : BasePage
    {
        DataTable _tData
        {
             get { if (Session["_tData"] != null) { return (DataTable)Session["_tData"]; } else { return new DataTable(); } }
            set { Session["_tData"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            FillEmptryGrids();
            if (!IsPostBack)
            {
                fromdate.Text = (DateTime.Now.Date.AddMonths(-1)).ToString("dd/MMM/yyyy");
                txtTo.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
            }
        }

        protected void Location_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Location.Text))
            {
                displayMessage_warning("Please select a Location"); return;
            }

            if (!string.IsNullOrEmpty(Location.Text))
            {
                bool b = false;
                string desc = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = new DataTable();
                _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "CODE", Location.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (Location.Text == row["Code"].ToString())
                        {
                            desc = row["Description"].ToString();
                            b = true; break;
                        }
                    }
                }
                if (b)
                {
                }
                else
                {
                    Location.Text = "";
                    Location.Focus();
                    displayMessage_warning("Please select a valid Location"); return;
                }
            }

        }

        protected void LinkButtonsearch_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSeLocation_Click(object sender, EventArgs e)
        {
            ViewState["UserLocation"] = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            dvResult.DataSource = _result;
            dvResult.DataBind();
            ViewState["UserLocation"] = _result;
            Label8.Text = "UserLocation";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            UserPopoup.Show();
        }

        protected void itemid_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(itemiddeta.Text))
            {

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, "ITEM", itemiddeta.Text.Trim().ToUpper());

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == itemiddeta.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    displayMessage_warning("Please select a valid Item");
                    itemiddeta.Text = string.Empty;
                    itemiddeta.Focus();
                    return;
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
            dvResult.DataSource = _result;
            dvResult.DataBind();
            BindUCtrlDDLData(_result);
            ViewState["MasterItem"] = _result;
            Label8.Text = "MasterItem";
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            UserPopoup.Show();
        }

        protected void serial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(serialdata.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    string _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                    DataTable _result = CHNLSVC.Inventory.SearchSerialsInr(_Para, "Serial #", serialdata.Text.Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Serial #"].ToString()))
                        {
                            if (serialdata.Text == row["Serial #"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Item Code"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                       
                    }
                    else
                    {
                        serialdata.ToolTip = "";
                        serialdata.Text = "";
                        serialdata.Focus();
                        displayMessage_warning("Please select a valid serial # !!!");
                        return;
                    }
                }
                else
                {
                    serialdata.ToolTip = "";
                }
            }
            catch (Exception)
            {
                displayMessage_warning("Error Occurred while processing !!!");
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            DataTable _serData = new DataTable();
            dvResult.DataSource = new int[] { };
            string _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
            _serData = CHNLSVC.Inventory.SearchSerialsInr(_Para, null, null);
            if (itemiddeta.Text.ToString() != "")
            {
                DataView dv1 = _serData.DefaultView;
                String expression = "'%" + itemiddeta.Text.ToString() + "%'";
                dv1.RowFilter = "Convert([ITEM CODE], System.String) LIKE " + expression;
                _serData = dv1.ToTable();
            }
            if (_serData.Rows.Count > 0)
            {
                dvResult.DataSource = _serData;
                BindUCtrlDDLData(_serData);
            }
            _tData = _serData;
            dvResult.DataBind();
            Label8.Text = "Serial";
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            UserPopoup.Show();
            if (dvResult.PageIndex > 0)
            { dvResult.SetPageIndex(0); }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable availlocationTable = new DataTable();
            int val = 0;
            Session["dataTable"] = "";
            String location = Location.Text.ToString();
            String itemid = itemiddeta.Text.ToString();
            String serial = serialdata.Text.ToString();
            String fromdt = fromdate.Text.ToString();
            String todt = txtTo.Text.ToString();
            String category01 = txtMainCat.Text;
            String category02 = txtCat1.Text;
            availablelocation.Text = "";

            if (alllocation.Checked == false)
            {
                if (location == "")
                {
                    displayMessage("Please select a Company");
                    return;
                }
            }

            if (serial == "" && itemid == "" &&category01.Equals(string.Empty))
            {
                val = 1;
            }
            if (val == 0)
            {

                if (alltimeduration.Checked == true)
                {
                    fromdt = "01/May/1900";
                    todt = "01/May/2060";
                    DataTable _serData = new DataTable();
                    _serData = CHNLSVC.General.GetItemDetails(location, itemid, serial, fromdt, todt, category01, category02, Session["UserCompanyCode"].ToString());
                    if (_serData.Rows.Count > 0)
                    {
                        grdPriceDetail.DataSource = _serData;
                        grdPriceDetail.DataBind();
                        Session["dataTable"] = _serData;
                        if (serial != "")
                        {
                            availlocationTable = CHNLSVC.General.GetItemAvaialableLocation(serial);
                            if (availlocationTable.Rows.Count > 0)
                            {
                                foreach (DataRow rw in availlocationTable.Rows)
                                {
                                    availablelocation.Text = rw["ins_loc"].ToString();
                                }
                            }
                            else
                            {

                                availablelocation.Text = "Not available";
                            }
                        }

                    }
                    else {
                        displayMessage_warning("No data found for given criteria");
                    }
                }
                else
                {
                    if (fromdt == "" || todt == "")
                    {
                        displayMessage("Please select valid date range");
                        return;
                    }
                    else
                    {
                        DataTable _serData = new DataTable();
                        _serData = CHNLSVC.General.GetItemDetails(location, itemid, serial, fromdt, todt, category01, category02, Session["UserCompanyCode"].ToString());
                        if (_serData.Rows.Count > 0)
                        {
                            grdPriceDetail.DataSource = _serData;
                            grdPriceDetail.DataBind();
                            Session["dataTable"] = _serData;
                            if (serial != "")
                            {
                                availlocationTable = CHNLSVC.General.GetItemAvaialableLocation(serial);
                                if (availlocationTable.Rows.Count > 0)
                                {
                                    foreach (DataRow rw in availlocationTable.Rows)
                                    {
                                        availablelocation.Text = rw["ins_loc"].ToString();
                                    }
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                displayMessage("Please select Item or Serial");
                return;
            }

        }

        protected void dvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Label8.Text == "UserLocation")
            {
                Location.Text = dvResult.SelectedRow.Cells[1].Text;
            }
            if (Label8.Text == "MasterItem")
            {
                itemiddeta.Text = dvResult.SelectedRow.Cells[1].Text;
            }
            if (Label8.Text == "Serial")
            {
                serialdata.Text = dvResult.SelectedRow.Cells[1].Text.ToString();
                itemiddeta.Text = dvResult.SelectedRow.Cells[2].Text.ToString();
            }
            if (Label8.Text == "masterCat1")
            {
                txtMainCat.Text = dvResult.SelectedRow.Cells[1].Text.ToString();
            }
            if (Label8.Text == "masterCat2")
            {
                txtCat1.Text = dvResult.SelectedRow.Cells[1].Text.ToString();                
            }
        }

        protected void dvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResult.PageIndex = e.NewPageIndex;
            dvResult.DataSource = null;
            if (Label8.Text == "UserLocation")
            {
                dvResult.DataSource = (DataTable)ViewState["UserLocation"];
                dvResult.DataBind();
                UserPopoup.Show();
            }
            if (Label8.Text == "MasterItem")
            {
                dvResult.DataSource = (DataTable)ViewState["MasterItem"];
                dvResult.DataBind();
                UserPopoup.Show();
            }
            if (Label8.Text == "Serial")
            {
                dvResult.PageIndex = e.NewPageIndex;
                if (_tData.Rows.Count > 0)
                {
                    dvResult.DataSource = _tData;
                }
                else
                {
                    dvResult.DataSource = new int[] { };
                }
                dvResult.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }
            if (Label8.Text == "masterCat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                dvResult.DataSource = _result;
                dvResult.DataBind();
             //   dvResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (Label8.Text == "masterCat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                dvResult.DataSource = _result;
                dvResult.DataBind();
             //   grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
        }

        protected void ImgSearch_Click(object sender, EventArgs e)
        {
            if (Label8.Text == "UserLocation")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                ViewState["UserLocation"] = _result;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }

            if (Label8.Text == "MasterItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                ViewState["MasterItem"] = _result;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            if (Label8.Text == "Serial")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable _serData = CHNLSVC.Inventory.SearchSerialsInr(SearchParams, cmbSearchbykey.SelectedValue, txtSearchbyword.Text);
                if (itemiddeta.Text.ToString() != "")
                {
                    DataView dv1 = _serData.DefaultView;
                    String expression = "'%" + itemiddeta.Text.ToString() + "%'";
                    dv1.RowFilter = "Convert([ITEM CODE], System.String) LIKE " + expression;
                    _serData = dv1.ToTable();
                }
                dvResult.DataSource = _serData;
                BindUCtrlDDLData(_serData);
                dvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }
            if (Label8.Text == "masterCat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
                ViewState["SEARCH"] = _result;
                return;
            }
            if (Label8.Text == "masterCat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
                ViewState["SEARCH"] = _result;
                return;
            }

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.masterItem:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "CS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "0" + seperator);
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
                default:
                    break;

            }

            return paramsText.ToString();

        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }

        private void displayMessage_warning(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

        }

        protected void grdBlDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void grdBlDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdBlDetails_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void grdPriceDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPriceDetail.PageIndex = e.NewPageIndex;
            grdPriceDetail.DataSource = new int[] { };
            grdPriceDetail.DataSource = (DataTable)Session["dataTable"];
            grdPriceDetail.DataBind();
        }


        private void FillEmptryGrids()
        {
            try
            {
                grdPriceDetail.DataSource = null;
                grdPriceDetail.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void displayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

        }

        protected void fromdate_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
           
        }
        protected void lbtnSrch_mainCat_Click(object sender, EventArgs e)
        {

            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                Label8.Text = "masterCat1";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
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
        public bool validateinputString(string input)
        {
            Match match = Regex.Match(input, @"([~!@$%^&*]+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return false;
            }
            return true;
        }
        protected void lbtnSrch_cat1_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                Label8.Text = "masterCat2";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
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

    }
}