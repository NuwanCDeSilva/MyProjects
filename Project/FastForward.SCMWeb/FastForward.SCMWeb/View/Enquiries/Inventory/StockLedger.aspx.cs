using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Inventory
{
    public partial class StockLedger : Base
    {
        string _sortDirection;
        string SortDireaction;
        DataTable dataTable;
        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";
                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }
                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }
        List<MasterItemStatus> _statusList
        {
            get { if (Session["_statusList"] != null) { return (List<MasterItemStatus>)Session["_statusList"]; } else { return new List<MasterItemStatus>(); } }
            set { Session["_statusList"] = value; }
        }
        List<MasterUOM> _itemUomList
        {
            get { if (Session["_statusList"] != null) { return (List<MasterUOM>)Session["_itemUomList"]; } else { return new List<MasterUOM>(); } }
            set { Session["_itemUomList"] = value; }
        }
        string SerPopShow
        {
            get { if (Session["SerPopShow"] != null) { return (string)Session["SerPopShow"]; } else { return ""; } }
            set { Session["SerPopShow"] = value; }
        }
        DateTime minDate;
        public String _DWSublocs { get { return (String)ViewState["_DWSublocs"]; } set { ViewState["_DWSublocs"] = value; } }
        public String _SWSublocs { get { return (String)ViewState["_SWSublocs"]; } set { ViewState["_SWSublocs"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserCompanyCode"]==null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                _itemUomList = new List<MasterUOM>();
                _statusList = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                _itemUomList = CHNLSVC.General.GetItemUOM();
                grdDWlocations.DataSource = new int[] { };
                grdDWlocations.DataBind();

                grdDWSerialDetails.DataSource = new int[] { };
                grdDWSerialDetails.DataBind();

                grdSWlocations.DataSource = new int[] { };
                grdSWlocations.DataBind();

                grdSWSerialDetails.DataSource = new int[] { };
                grdSWSerialDetails.DataBind();

                StockLedger_Load();

                hdfTabIndex.Value =  "#DocumentWise";
                ViewState["sortOrder"] = "desc";
                DateTime _date = DateTime.Today;

                //TextBox1.Text = _date.AddMonths(-1).ToString("dd/MMM/yyyy");
                txtDWfrom.Text = _date.AddMonths(-1).ToString("dd/MMM/yyyy");
                txtSWfrom.Text = _date.AddMonths(-1).ToString("dd/MMM/yyyy");
                
                txtDWto.Text = _date.ToString("dd/MMM/yyyy");
                txtSWto.Text = _date.ToString("dd/MMM/yyyy");
            }

            else
            {
                txtDWfrom.Text = Request[txtDWfrom.UniqueID];
                txtSWfrom.Text = Request[txtSWfrom.UniqueID];
                txtDWto.Text = Request[txtDWto.UniqueID];
                txtSWto.Text = Request[txtSWto.UniqueID];
                if (SerPopShow == "Show")
                {
                    UserPopoup.Show();
                    SerPopShow = "Hide";
                }
                else
                {
                    UserPopoup.Hide();
                    SerPopShow = "Hide";
                }     
            }

            

        }
        protected void StockLedger_Load()
        {
            try
            {
                txtDWCompany.Text = Session["UserCompanyCode"].ToString();
                txtSWCompany.Text = Session["UserCompanyCode"].ToString();

                ValidateAndLoadCompany(txtDWCompany.Text, lblDWCompanyName);
                ValidateAndLoadCompany(txtSWCompany.Text, lblSWCompanyName);

                string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                //check company search permission
                if (!CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INV6"))
                {
                    lbtnDWCompany.Enabled = false;
                    txtDWCompany.Enabled = false;

                    lbtnSWCompany.Enabled = false;
                    txtSWCompany.Enabled = false;
                }
                else
                {
                    lbtnDWCompany.Enabled = true;
                    txtDWCompany.Enabled = true;

                    lbtnSWCompany.Enabled = true;
                    txtSWCompany.Enabled = true;
                }

                DateTime _date = DateTime.Now;

                //Load default location
                txtDWLocation.Text = Session["UserDefLoca"].ToString();
                txtSWLocation.Text = Session["UserDefLoca"].ToString();

                LoadSubLocation(txtDWCompany.Text, txtDWLocation.Text, grdDWlocations);
                LoadSubLocation(txtSWCompany.Text, txtSWLocation.Text, grdSWlocations);
                txtDWLocation_Leave(null, null);
                txtSWLocation_Leave(null, null);
               

                DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), txtSWLocation.Text);
                if (_loc != null && _loc.Rows.Count > 0)
                {
                    if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                        minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                    else
                        minDate = DateTime.MinValue;
                }
                setupinitialcheckboxValuesDW();
                setupcheckboxValuesDW();
                setupinitialcheckboxValuesSW();
                setupcheckboxValuesSW();
            }
            catch (Exception ex)
            {                                
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2); 
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void LoadSubLocation(string com, string loc, GridView grid)
        {
            try
            {
                //load all locs
                List<MasterLocation> _subLoc = CHNLSVC.Inventory.getAllLoc_WithSubLoc(com, loc);
                grid.DataSource = _subLoc;
                grid.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error occured while processing\n" + ex.Message,2);
                CHNLSVC.CloseChannel();
                return;
            }
        }
        protected void txtDWLocation_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDWLocation.Text != "")
                {
                    DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), txtDWLocation.Text);
                    if (_loc != null && _loc.Rows.Count > 0)
                    {
                        if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                            minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                        else
                            minDate = DateTime.MinValue;
                    }
                    if (!ValidateAndLoadLocation(txtDWCompany.Text, txtDWLocation.Text, lblDWLocationName))
                    {
                        lblDWLocationName.Text = "";
                    }
                    else
                        LoadSubLocation(txtDWCompany.Text, txtDWLocation.Text.ToUpper().Trim(), grdDWlocations);
                }
            }
            catch (Exception ex)
            {
                minDate = DateTime.MinValue;
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2); 
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtSWLocation_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSWLocation.Text != "")
                {
                    DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), txtSWLocation.Text);
                    if (_loc != null && _loc.Rows.Count > 0)
                    {
                        if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                            minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                        else
                            minDate = DateTime.MinValue;
                    }
                    if (!ValidateAndLoadLocation(txtSWCompany.Text, txtSWLocation.Text, lblSWLocationName))
                    {
                        lblSWLocationName.Text = "";
                    }
                    else
                        LoadSubLocation(txtSWCompany.Text, txtSWLocation.Text.ToUpper().Trim(), grdSWlocations);
                }
            }
            catch (Exception ex)
            {
                minDate = DateTime.MinValue;
                DisplayMessage("Error Occurred while processing...\n" + ex.Message,2); 
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected bool ValidateAndLoadLocation(string com, string loc, Label lbl)
        {
            try
            {
                MasterLocation location = CHNLSVC.General.GetLocationByLocCode(com.ToUpper(), loc.ToUpper());
                if (location != null)
                {
                    lbl.Text = location.Ml_loc_desc;
                    return true;
                }
                else
                {
                    lbl.Text = "";
                    return true;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n" + ex.Message,2); 
                CHNLSVC.CloseChannel();
                return true;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
        protected bool ValidateAndLoadCompany(string code, Label lbl)
        {
            try
            {
                MasterCompany company = CHNLSVC.General.GetCompByCode(code.ToUpper());
                if (company != null)
                {
                    lbl.Text = company.Mc_desc;
                    return true;
                }
                else
                {
                    lbl.Text = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error occured while processing\n" + ex.Message, 2);
                CHNLSVC.CloseChannel();
                return false;
            }
        }
        #region search        
        protected string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
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
                case CommonUIDefiniton.SearchUserControlType.UserLocationDW:
                    {
                        paramsText.Append(txtDWCompany.Text  + seperator + Session["UserID"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocationSW:
                    {
                        paramsText.Append(txtDWCompany.Text + seperator + Session["UserID"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementTypes:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtSWItemCode.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "0" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void lbtnDWItem_Click(object sender, EventArgs e)
        {  
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "dwitemcode";
                txtSearchbyword.Text = "";
                SerPopShow = "Show";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void lbtnDWCompany_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "dwcompanycode";
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                SerPopShow = "Show";
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2); 
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnDWLocation_Click(object sender, EventArgs e)
        {
            try
            {         

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationDW);
                DataTable _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "dwlocationcode";
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                SerPopShow = "Show";
                
                if (txtDWLocation.Text != "")
                {
                    DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), txtDWLocation.Text);
                    if (_loc != null && _loc.Rows.Count > 0)
                    {
                        //if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                        //    minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                        //else
                        minDate = DateTime.MinValue;
                    }
                    if (ValidateAndLoadLocation(txtDWCompany.Text, txtDWLocation.Text, lblDWLocationName))
                    {
                        LoadSubLocation(txtDWCompany.Text, txtDWLocation.Text, grdDWlocations);
                    }
                }
            }
            catch (Exception ex)
            {
                minDate = DateTime.MinValue;
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }   
     
        //doctype wise search
        protected void lbtnDocType_Click(object sender, EventArgs e)
        {
            try
            {
                lblvalue.Text = "doctype";                
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementTypes(SearchParams, null, null);
                grdResult.DataSource = null;

                if (_result.Rows.Count > 0)
                {
                    grdResult.DataSource = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    grdResult.DataSource = null;
                }
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();
                SerPopShow = "Show";
                if (grdResult.PageIndex > 0)
                { grdResult.SetPageIndex(0); }
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

        //serial wise
        protected void lbtnSWItem_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "switemcode";
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                SerPopShow = "Show";
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnSWCompany_Click(object sender, EventArgs e)
        {
            try
            {      

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "swcompanycode";
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                SerPopShow = "Show";
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2); 
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnSWLocation_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationSW);
                DataTable _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "swlocationcode";
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                SerPopShow = "Show";

                if (txtSWLocation.Text != "")
                {
                    DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), txtSWLocation.Text);
                    if (_loc != null && _loc.Rows.Count > 0)
                    {
                        if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                            minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                        else
                            minDate = DateTime.MinValue;
                    }
                    if (ValidateAndLoadLocation(txtSWCompany.Text, txtSWLocation.Text, lblSWLocationName))
                    {
                        LoadSubLocation(txtSWCompany.Text, txtSWLocation.Text, grdSWlocations);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        //serial code wise search
        protected void lbtnSerial_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSWItemCode.Text))
                {
                    DisplayMessage("Please enter item code !!!", 2); txtSWItemCode.Focus(); return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable _result = CHNLSVC.Inventory.SearchSerialsIntByItem(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "serial";                
                UserPopoup.Show();
                SerPopShow = "Show";
                txtSearchbyword.Text = "";
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
            if (lblvalue.Text == "dwitemcode")
            {                
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                SerPopShow = "Show";
                return;
            }

            else if (lblvalue.Text == "dwcompanycode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";
                SerPopShow = "Show";
            }
            else if (lblvalue.Text == "dwlocationcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationDW);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";
            }
            else if (lblvalue.Text == "switemcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";
            }
            else if (lblvalue.Text == "swcompanycode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";
            }
            else if (lblvalue.Text == "swlocationcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationSW);
                DataTable _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";

            }
          
            else if (lblvalue.Text == "serial")
            {
                if (string.IsNullOrEmpty(txtSWItemCode.Text))
                {
                    DisplayMessage("Please enter item code !!!", 2); txtSWItemCode.Focus(); return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable result = CHNLSVC.Inventory.SearchSerialsIntByItem(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";              
                txtSearchbyword.Focus();
            }
        }
        #endregion
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            string DESC = "";
            if (grdResult.SelectedRow.Cells.Count>2)
            {
                DESC = grdResult.SelectedRow.Cells[2].Text;
            }
            
            if (lblvalue.Text == "dwitemcode")
            {
                txtDWItemCode.Text = ID;
                lblDWItemName.Text = DESC;
                txtDWItemCode_TextChanged(null, null);
                lblvalue.Text = "";
                SerPopShow = "Hide";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "dwcompanycode")
            {
                txtDWCompany.Text = ID;
                lblDWCompanyName.Text = DESC;
                lblvalue.Text = "";
                SerPopShow = "Hide";
                UserPopoup.Hide();
                return;
            }

            if (lblvalue.Text == "dwlocationcode")
            {
                txtDWLocation.Text = ID;
                lblDWLocationName.Text = DESC;
                lblvalue.Text = "";
                SerPopShow = "Hide";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "switemcode")
            {
                txtSWItemCode.Text = ID;
                lblSWItemName.Text = DESC;
                lblvalue.Text = "";
                txtSWItemCode_TextChanged(null, null);
                SerPopShow = "Hide";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "swcompanycode")
            {
                txtSWCompany.Text = ID;
                lblSWCompanyName.Text = DESC;
                lblvalue.Text = "";
                SerPopShow = "Hide";
                UserPopoup.Hide();
                return;
            }

            if (lblvalue.Text == "swlocationcode")
            {
                txtSWLocation.Text = ID;
                lblSWLocationName.Text = DESC;
                lblvalue.Text = "";
                SerPopShow = "Hide";
                UserPopoup.Hide();
                return;
            }

            if (lblvalue.Text == "doctype")
            {
                txtDWDocType.Text = ID;
                SerPopShow = "Hide";
                UserPopoup.Hide();
                return;
            }
  
            if (lblvalue.Text == "serial")
            {
                txtSWserial.Text = ID;
                SerPopShow = "Hide";
                lblvalue.Text = "";
                UserPopoup.Hide();
                //return;
            }
        }

        private bool ValidateAndLoadItemCode(string code, string com, Label lbl, Label lbl1,Label lbl2)
        {
            try
            {
                MasterItem item = CHNLSVC.Inventory.GetItem(com.ToUpper(), code.ToUpper());
                if (item != null && item.Mi_cd != null)
                {
                    lbl.Text = "Brand- " + item.Mi_brand;
                    lbl1.Text = "Model- " + item.Mi_model;
                    lbl2.Text =  item.Mi_shortdesc;
                    return true;
                }
                else
                {
                    lbl.Text = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error occured while processing\n" + ex.Message, 4);
                return false;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;

            if (lblvalue.Text == "dwitemcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show(); SerPopShow = "Show";
                return;
            }

            else if (lblvalue.Text == "dwcompanycode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show(); SerPopShow = "Show";
                return;
            }
            else if (lblvalue.Text == "dwlocationcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationDW);
                DataTable _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show(); SerPopShow = "Show";
                return;
            }
            else if (lblvalue.Text == "switemcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show(); SerPopShow = "Show";
                return;
            }
            else if (lblvalue.Text == "swcompanycode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show(); SerPopShow = "Show";
                return;
            }
            else if (lblvalue.Text == "swlocationcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationSW);
                DataTable _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show(); SerPopShow = "Show";
                return;

            }


             else if (lblvalue.Text == "doctype")
             {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementTypes(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show(); SerPopShow = "Show";
                return;
             }
             
             else if (lblvalue.Text == "serial")
             {
                 if (string.IsNullOrEmpty(txtSWItemCode.Text))
                 {
                     DisplayMessage("Please enter item code !!!", 2); txtSWItemCode.Focus(); return;
                 }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable _result = CHNLSVC.Inventory.SearchSerialsIntByItem(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show(); SerPopShow = "Show";
                return;
             }
             
        }               
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "dwitemcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";
                return;
            }

            else if (lblvalue.Text == "dwcompanycode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";
            }
            else if (lblvalue.Text == "dwlocationcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationDW);
                DataTable _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";
            }
            else if (lblvalue.Text == "switemcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";
            }
            else if (lblvalue.Text == "swcompanycode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";
            }
            else if (lblvalue.Text == "swlocationcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationSW);
                DataTable _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show(); SerPopShow = "Show";

            }
             
             else if (lblvalue.Text == "doctype")
             {
                 string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                 DataTable _result = CHNLSVC.CommonSearch.GetMovementTypes(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                 grdResult.DataSource = _result;
                 grdResult.DataBind();
                 UserPopoup.Show(); SerPopShow = "Show";
             }
             
             else if (lblvalue.Text == "serial")
             {
                 if (string.IsNullOrEmpty(txtSWItemCode.Text))
                 {
                     DisplayMessage("Please enter item code !!!", 2); txtSWItemCode.Focus(); return;
                 }
                 string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                 DataTable _result = CHNLSVC.Inventory.SearchSerialsIntByItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                 grdResult.DataSource = _result;
                 grdResult.DataBind();
                 UserPopoup.Show(); SerPopShow = "Show";
             }
             
        }
        
        //view ledger details
        protected void lbtnViewLedger_Click(object sender, EventArgs e)
        {
            try
            {

                //grdDWSerialDetails.DataSource = new int[] { };
                //grdDWSerialDetails.DataBind();

                //    grdSWSerialDetails.DataSource = new int[] { };
                //grdSWSerialDetails.DataBind();

                string Tabvalue = hdfTabIndex.Value;

               // txtDWLocation_Leave(null, null);
                if (((txtDWfrom.Text) != null) && ((txtDWto.Text) != null))
                {
                    DateTime dtFrom = new DateTime(), dtTo = new DateTime();
                    DateTime dateTimePickerDWFrom = DateTime.TryParse(txtDWfrom.Text, out dtFrom) ? Convert.ToDateTime(txtDWfrom.Text) : dtFrom;
                    DateTime dateTimePickerDWTo = DateTime.TryParse(txtDWfrom.Text, out dtTo) ? Convert.ToDateTime(txtDWto.Text) : dtTo;

          
                    if (Tabvalue == "#DocumentWise")
                    {
                        DateTime _dtFrom = Convert.ToDateTime(txtDWfrom.Text);
                        DateTime _dtTo = Convert.ToDateTime(txtDWto.Text);
                        DateTime _maxDate = _dtFrom.AddMonths(6).AddDays(-1);
                        if (Session["UserCompanyCode"].ToString()=="ARL")
                        {
                            _maxDate = _dtFrom.AddMonths(12).AddDays(-1);
                            if (_dtTo > _maxDate)
                            {
                                DisplayMessage("Maximum date range allowed is 12 months period !", 2); return;
                            }
                        }
                        else
                        {
                            if (_dtTo > _maxDate)
                            {
                                DisplayMessage("Maximum date range allowed is 6 months period !", 2); return;
                            }
                        }
                        
                        //this.Cursor = Cursors.WaitCursor;
                        //string locs = "";

                        //foreach (GridViewRow dgvr in grdDWlocations.Rows)
                        //{
                        //    CheckBox chk = (CheckBox)dgvr.FindControl("chk_DWlocReq");
                        //    if (chk.Checked)
                        //    {
                        //        locs = locs + (dgvr.FindControl("lbl_DWCode") as Label).Text + ",";
                        //    }                           

                        //}

                        if (_DWSublocs == "")
                        {
                            DisplayMessage("Please select Sub Location from location grid", 2);
                            grdDWSerialDetails.DataSource = null;
                            return;
                        }
                         if (txtDWItemCode.Text == "")
                        {
                            DisplayMessage("Please select Item", 2);
                            grdDWSerialDetails.DataSource = null;
                            return;
                        }
                        else if (txtDWCompany.Text == "")
                        {
                            DisplayMessage("Please select company", 2);
                            grdDWSerialDetails.DataSource = null;
                            return;
                        }

                        else
                        {
                            if (Session["UserCompanyCode"].ToString() == "AST")
                            {
                                string _item = "";
                                //kapila 18/11/2013
                                if (txtDWItemCode.Text.Length == 16)
                                    _item = txtDWItemCode.Text.Substring(1, 7);
                                else if (txtDWItemCode.Text.Length == 15)
                                    _item = txtDWItemCode.Text.Substring(0, 7);
                                else if (txtDWItemCode.Text.Length == 8)
                                    _item = txtDWItemCode.Text.Substring(1, 7);
                                else if (txtDWItemCode.Text.Length == 20)
                                    _item = txtDWItemCode.Text.Substring(0, 12);
                                else
                                    _item = txtDWItemCode.Text;

                                txtDWItemCode.Text = _item;
                            }

                            if (chkStatus.Checked)
                            {
                                MasterItem _mstItem = CHNLSVC.General.GetItemMaster(txtDWItemCode.Text);
                                hdfValTp.Value = "0";
                                if (_mstItem != null)
                                {
                                    if (_itemUomList != null)
                                    {
                                        var _uom = _itemUomList.Where(c => c.Msu_cd == _mstItem.Mi_itm_uom).FirstOrDefault();
                                        if (_uom != null)
                                        {
                                            hdfValTp.Value = _uom.Msu_isdecimal.ToString();
                                        }
                                    }
                                }
                                DataTable dt = GetStockLedgerData(Session["UserID"].ToString(), "", _mstItem.Mi_brand, _mstItem.Mi_model, txtDWItemCode.Text.Trim(),
                                    0, _mstItem.Mi_cate_1, _mstItem.Mi_cate_2, _mstItem.Mi_cate_3, 0, dateTimePickerDWFrom.Date, 0, 1, txtDWCompany.Text.Trim(), _DWSublocs, dateTimePickerDWFrom.Date,
                                    dateTimePickerDWTo.Date, 1, txtDWDocType.Text.Trim());
                                if (!string.IsNullOrEmpty(txtDWDocType.Text))
                                {
                                    for (int x = dt.Rows.Count - 1; x >= 0; x--)
                                    {
                                        DataRow dr = dt.Rows[x];
                                        if (dr["DOC_TYPE"].ToString() == "OPERNING_BAL")
                                        {
                                            dr.Delete();
                                        }
                                    }
                                    dt.AcceptChanges();
                                }
                                else
                                {
                                    dt = MakeGridBalWithStatus(dt);
                                }
                                DataTable tem = dt.Clone();
                                tem.Columns.Add("id", typeof(int));
                                int i = 0;
                                foreach (DataRow dr in dt.Rows)
                                {

                                    DataRow drT = tem.NewRow();
                                    drT[0] = dr[0].ToString();
                                    drT[1] = dr[1].ToString();
                                    drT[2] = dr[2].ToString();
                                    drT[3] = dr[3].ToString();
                                    drT[4] = dr[4].ToString();
                                    drT[5] = dr[5].ToString();
                                    drT[6] = dr[6].ToString();
                                    drT[7] = dr[7].ToString();
                                    drT[8] = dr[8].ToString();
                                    if (dr["DOC_TYPE"].ToString().ToUpper() == "OPERNING_BAL")
                                    {
                                        drT[9] = dr[9].ToString();
                                    }
                                    else
                                    {
                                        int cou;
                                        if (dr[7].ToString() != "0")
                                            cou = Convert.ToInt32(dr[7]);
                                        else
                                            cou = -1 * Convert.ToInt32(dr[8]);
                                        var _result = (from _res in tem.AsEnumerable()
                                                       //group _res by new { column1 = _res[0], column2 = _res[10], Column3 = _res[9], Column4 = _res[2] } into _res1
                                                       where _res.Field<string>(10) == dr[10].ToString()
                                                       orderby _res.Field<int>(11) descending
                                                       select _res).FirstOrDefault();
                                        if (_result != null)
                                            drT[9] = (Convert.ToInt32(_result[9].ToString()) + cou).ToString();
                                        else
                                            drT[9] = cou;
                                    }
                                    drT[10] = dr[10].ToString();
                                    drT[11] = i++;
                                    tem.Rows.Add(drT);
                                }
                                if (tem.Rows.Count > 0)
                                {
                                    //DataView dv = tem.DefaultView;
                                    //dv.Sort = "doc_date,SEQ_NO,doc_no";
                                    //tem = dv.ToTable();
                                }
                              //  tem = LoadBal(tem);
                                grdDWSerialDetails.DataSource = tem;
                                Session["dataTable"] = tem;
                                grdDWSerialDetails.Columns[7].Visible = true;

                                if (!string.IsNullOrWhiteSpace(txtDWDocType.Text))
                                {
                                    grdDWSerialDetails.Columns[10].Visible = false;
                                }
                                else 
                                {
                                    grdDWSerialDetails.Columns[10].Visible = true;
                                }

                                grdDWSerialDetails.DataBind();
                                setupinitialcheckboxValuesDW();
                                setupcheckboxValuesDW();
                                
                            }
                            else
                            {
                                MasterItem _mstItem = CHNLSVC.General.GetItemMaster(txtDWItemCode.Text.ToUpper());
                                hdfValTp.Value = "0";
                                if (_mstItem != null)
                                {
                                    if (_itemUomList != null)
                                    {
                                        var _uom = _itemUomList.Where(c => c.Msu_cd == _mstItem.Mi_itm_uom).FirstOrDefault();
                                        if (_uom != null)
                                        {
                                            hdfValTp.Value = _uom.Msu_isdecimal.ToString();
                                        }
                                    }
                                }
                                DataTable dt = new DataTable ("tbl");
                                //DataTable dt = CHNLSVC.Inventory.StockBalanceSearch1(dateTimePickerDWFrom.Date, dateTimePickerDWTo.Date, txtDWItemCode.Text, _DWSublocs, txtDWCompany.Text, false, !string.IsNullOrEmpty(txtDWDocType.Text)?txtDWDocType.Text:null);
                                dt = GetStockLedgerData(Session["UserID"].ToString(), "", _mstItem.Mi_brand, _mstItem.Mi_model, txtDWItemCode.Text.Trim(),
                                    0, _mstItem.Mi_cate_1, _mstItem.Mi_cate_2, _mstItem.Mi_cate_3, 0, dateTimePickerDWFrom.Date, 0, 0, txtDWCompany.Text.Trim(), _DWSublocs, dateTimePickerDWFrom.Date,
                                    dateTimePickerDWTo.Date, 0, txtDWDocType.Text.Trim());
                                if (!string.IsNullOrEmpty(txtDWDocType.Text))
                                {
                                    for (int x = dt.Rows.Count - 1; x >= 0; x--)
                                    {
                                        DataRow dr = dt.Rows[x];
                                        if (dr["DOC_TYPE"].ToString() == "OPERNING_BAL")
                                        {
                                            dr.Delete();
                                        }
                                    }
                                    dt.AcceptChanges();
                                }
                               // dt = MakeGridBalWithoutStatus(dt);
                                dt =CHNLSVC.MsgPortal.MakeGridBalWithoutStatus(dt);
                                grdDWSerialDetails.DataSource = dt;
                                Session["dataTable"] = dt;
                                grdDWSerialDetails.Columns[7].Visible = false;

                                if (!string.IsNullOrWhiteSpace(txtDWDocType.Text))
                                {
                                    grdDWSerialDetails.Columns[10].Visible = false;
                                }
                                else
                                {
                                    grdDWSerialDetails.Columns[10].Visible = true;
                                }

                                grdDWSerialDetails.DataBind();
                                setupinitialcheckboxValuesDW();
                                setupcheckboxValuesDW();
                                
                            }
                            decimal inCount = 0;
                            decimal outCount = 0;

                            decimal tmpIn = 0, tmpOut = 0;

                            foreach (GridViewRow item in grdDWSerialDetails.Rows)
                            {
                                Label lbl_doctype = item.FindControl("lbl_doctype") as Label;
                                if (lbl_doctype.Text != "OPERNING_BAL")
                                {
                                    Label lbl_ins = item.FindControl("lbl_ins1") as Label;
                                    Label lbl_outs = item.FindControl("lbl_outs1") as Label;
                                    inCount = inCount + (Decimal.TryParse(lbl_ins.Text, out tmpIn) ? Convert.ToDecimal(lbl_ins.Text) : 0);
                                    outCount = outCount + (Decimal.TryParse(lbl_outs.Text, out tmpOut) ? Convert.ToDecimal(lbl_outs.Text) : 0);

                                    Label lbl_status = item.FindControl("lbl_status") as Label;
                                    if (lbl_status != null)
                                    {
                                        if (!string.IsNullOrEmpty(lbl_status.Text))
                                        {
                                            var v = _statusList.Where(c => c.Mis_cd == lbl_status.Text).FirstOrDefault();
                                            if (v != null)
                                            {
                                                lbl_status.Text = v.Mis_desc;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Label lbl_ins = item.FindControl("lbl_ins1") as Label; lbl_ins.Text="";
                                    Label lbl_outs = item.FindControl("lbl_outs1") as Label; lbl_outs.Text = "";
                                    Label lbl_status = item.FindControl("lbl_status") as Label;
                                    if (lbl_status != null)
                                    {
                                        if (!string.IsNullOrEmpty(lbl_status.Text))
                                        {
                                            var v = _statusList.Where(c => c.Mis_cd == lbl_status.Text).FirstOrDefault();
                                            if (v != null)
                                            {
                                                lbl_status.Text = v.Mis_desc;
                                            }
                                        }
                                    }
                                }
                            }

                            //for (int i = 0; i < grdDWSerialDetails.Rows.Count; i++)
                            //{
                            //    if ((grdDWSerialDetails.Rows[i].FindControl("lbl_doctype")as Label).Text != "OPERNING_BAL")
                            //    {
                            //        inCount = inCount + (Convert.ToInt32((grdDWSerialDetails.Rows[i].FindControl("lbl_ins") as Label).Text));
                            //        outCount = outCount + (Convert.ToInt32((grdDWSerialDetails.Rows[i].FindControl("lbl_outs") as Label).Text));
                            //    }
                            //}

                            txtinTotal.Text = inCount.ToString();
                            txtoutTotal.Text = outCount.ToString();
                            pnlSum.Visible = true;
                            if (!string.IsNullOrEmpty(txtDWDocType.Text))
                            {
                               pnlSum.Visible=false;
                            }
                            if (grdDWSerialDetails.Rows.Count <= 0)
                            {
                                DisplayMessage("No Data found.", 2);
                                return;
                            }
                        }
                    }

                    //Serial Wise Data Fetch

                    if (Tabvalue == "#SerialWise")
                    {

                        if (((txtSWfrom.Text) != null) && ((txtSWto.Text) != null))
                        {
                            DateTime dtFromSer = new DateTime(), dtToSer = new DateTime();
                            DateTime dateTimePickerSWFrom = DateTime.TryParse(txtSWfrom.Text, out dtFromSer) ? Convert.ToDateTime(txtSWfrom.Text) : dtFromSer;
                            DateTime dateTimePickerSWTo = DateTime.TryParse(txtSWto.Text, out dtToSer) ? Convert.ToDateTime(txtSWto.Text) : dtToSer;
                            DateTime _dtFrom = Convert.ToDateTime(txtSWfrom.Text);
                            DateTime _dtTo = Convert.ToDateTime(txtSWto.Text);
                            DateTime _maxDate = _dtFrom.AddMonths(6).AddDays(-1);
                            if (_dtTo > _maxDate)
                            {
                                DisplayMessage("Maximum date range allowed is 6 months period !", 2); return;
                            }                          
                            //foreach (GridViewRow row in grdSWlocations.Rows)
                            //{
                            //    CheckBox chk = (CheckBox)row.FindControl("chk_SWlocReq");
                            //    if (chk.Checked)
                            //    {
                            //        locs = locs + (row.FindControl("lbl_SWCode") as Label).Text + ",";
                                   
                            //    }
                            //}
                            if (_SWSublocs == "")
                            {
                                DisplayMessage("Please select Location from location grid", 2);
                                grdSWSerialDetails.DataSource = null;
                                return;
                            }
                            else if (txtSWItemCode.Text == "")
                            {
                                DisplayMessage("Please select Item", 2);
                                grdSWSerialDetails.DataSource = null;
                                return;
                            }
                            else if (txtSWCompany.Text == "")
                            {
                                DisplayMessage("Please select company", 2);
                                grdSWSerialDetails.DataSource = null;
                                return;
                            }
                            else
                            {
                                if (Session["UserCompanyCode"].ToString() == "AST")
                                {
                                    string _item = "";
                                    //kapila 18/11/2013
                                    if (txtSWItemCode.Text.Length == 16)
                                        _item = txtSWItemCode.Text.Substring(1, 7);
                                    else if (txtSWItemCode.Text.Length == 15)
                                        _item = txtSWItemCode.Text.Substring(0, 7);
                                    else if (txtSWItemCode.Text.Length == 8)
                                        _item = txtSWItemCode.Text.Substring(1, 7);
                                    else if (txtSWItemCode.Text.Length == 20)
                                        _item = txtSWItemCode.Text.Substring(0, 12);
                                    else
                                        _item = txtSWItemCode.Text;

                                    txtSWItemCode.Text = _item;
                                }

                                DataTable dt = CHNLSVC.MsgPortal.SerialBalanceSearch1(dateTimePickerSWFrom.Date, dateTimePickerSWTo.Date, txtSWItemCode.Text, _SWSublocs, txtSWCompany.Text, txtSWserial.Text);
                                if (dt.Rows.Count > 0)
                                {
                                    DataView dv = dt.DefaultView;
                                    dv.Sort = "IN_DATE,REFERENCE DESC";
                                    dt = dv.ToTable();
                                }
                                grdSWSerialDetails.DataSource = new int[] { };
                                if (dt.Rows.Count>0)
                                {
                                    grdSWSerialDetails.DataSource = dt;
                                }
                                Session["dataTable"] = dt;
                                grdSWSerialDetails.DataBind();
                                setupinitialcheckboxValuesSW();
                                setupcheckboxValuesSW();

                                if (grdSWSerialDetails.Rows.Count <= 0)
                                {
                                    DisplayMessage("No Data found.", 2);
                                    return;
                                }
                            }
                        }
                    }
                }
              
            }
            catch (Exception ex)
            {
                DisplayMessage("Error occurred while processing\n" + ex.Message,2);
                CHNLSVC.CloseChannel();
                return;
            }

            finally
            {               
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void lbtnSerPrice_Click(object sender, EventArgs e)
        {            
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

        protected void btnDWsublocation_Click(object sender, EventArgs e)
        {
            PopupDWSubLocations.Show();
        }
        protected void btnSWsublocation_Click(object sender, EventArgs e)
        {
            PopupSWSubLocations.Show();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
        }
        protected void chk_Req_CheckedChangedDW_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdDWlocations.Rows.Count == 0)
                {
                    return;
                }
                    
                var checkbox = (CheckBox)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {
                    if (checkbox.Checked == true)
                    {
                        string ID = (row.FindControl("lbl_DWCode") as Label).Text;
                        string DESC = (row.FindControl("lbl_DWDescription") as Label).Text;                      
                  
                        row.BackColor = System.Drawing.Color.LightCyan;
                    }
                    else
                    {
                        row.BackColor = System.Drawing.Color.Transparent;
                    }
                }

            }

            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void chk_Req_CheckedChangedSW_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdSWlocations.Rows.Count == 0)
                {
                    return;
                }

                var checkbox = (CheckBox)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {
                    if (checkbox.Checked == true)
                    {
                        string ID = (row.FindControl("lbl_SWCode") as Label).Text;
                        string DESC = (row.FindControl("lbl_SWDescription") as Label).Text;

                        row.BackColor = System.Drawing.Color.LightCyan;
                    }
                    else
                    {
                        row.BackColor = System.Drawing.Color.Transparent;
                    }
                }

            }

            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void btnBlItem_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow dgvr in grdDWlocations.Rows)
            {

                CheckBox chk = (CheckBox)dgvr.FindControl("chk_DWlocReq");
               
            }
        }

        //DW popup close
        protected void btnPopupDWSubLocationsClose_Click(object sender, EventArgs e)
        {
            ViewState["_DWSublocs"] = "";


            foreach (GridViewRow dgvr in grdDWlocations.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("chk_DWlocReq");
                if (chk.Checked)
                {
                    _DWSublocs = _DWSublocs + (dgvr.FindControl("lbl_DWCode") as Label).Text + ",";
                }

            }


        
        }

        protected void btnPopupSWSubLocationsClose_Click(object sender, EventArgs e)
        {
            ViewState["_SWSublocs"] = "";

            foreach (GridViewRow dgvr in grdSWlocations.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("chk_SWlocReq");
                if (chk.Checked)
                {
                    _SWSublocs = _SWSublocs + (dgvr.FindControl("lbl_SWCode") as Label).Text + ",";
                }

            }

        }


        protected void CheckSublocationBoxCheckDW_Click(object sender, EventArgs e)
        {
            try
            {
                //var checkbox = (CheckBox)sender;

                //if (checkbox.Checked == true)
                //{      
                //    setupcheckboxValuesDW();
                //}
                //else
                //{
                //    btnSubLocationsDW.Visible = true;

                //    foreach (GridViewRow dgvr in grdDWlocations.Rows)
                //    {
                //        ((CheckBox)dgvr.FindControl("chk_DWlocReq")).Checked = false;
                //    }
                //}
                setupcheckboxValuesDW();

            }

            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void CheckSublocationBoxCheckSW_Click(object sender, EventArgs e)
        {
            try
            {
                //var checkbox = (CheckBox)sender;

                //if (checkbox.Checked == true)
                //{
                //    setupcheckboxValuesSW();
                //}
                //else
                //{
                //    btnSubLocationsSW.Visible = true;

                //    foreach (GridViewRow dgvr in grdSWlocations.Rows)
                //    {
                //       ((CheckBox)dgvr.FindControl("chk_SWlocReq")).Checked = false;
                //    }
                //}
                setupcheckboxValuesSW();


            }

            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void setupinitialcheckboxValuesDW()
        {

            chkSublocationAll_DW.Checked = true;

            ViewState["_DWSublocs"] = "";

        }

        protected void setupcheckboxValuesDW() 
        {

            if (chkSublocationAll_DW.Checked == true)
            {
                btnSubLocationsDW.Visible = false;

                GridViewRow headerRow = grdDWlocations.HeaderRow;
                ((CheckBox)headerRow.FindControl("allDWlocchk")).Checked = true;

                foreach (GridViewRow dgvr in grdDWlocations.Rows)
                {
                    ((CheckBox)dgvr.FindControl("chk_DWlocReq")).Checked = true;
                }
            }
            else
            {
                btnSubLocationsDW.Visible = true;

                GridViewRow headerRow = grdDWlocations.HeaderRow;
                ((CheckBox)headerRow.FindControl("allDWlocchk")).Checked = false;

                foreach (GridViewRow dgvr in grdDWlocations.Rows)
                {
                    ((CheckBox)dgvr.FindControl("chk_DWlocReq")).Checked = false;
                }
            }
           // _DWSublocs = txtDWLocation.Text;
            foreach (GridViewRow dgvr in grdDWlocations.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("chk_DWlocReq");
                if (chk.Checked)
                {
                    _DWSublocs = _DWSublocs + (dgvr.FindControl("lbl_DWCode") as Label).Text + ",";
                }

            }

        }

        protected void setupinitialcheckboxValuesSW()
        {

           chkSublocationAll_SW.Checked = true;

            ViewState["_SWSublocs"] = "";
        }

        protected void setupcheckboxValuesSW()
        {
            if (chkSublocationAll_SW.Checked == true)
            {
                btnSubLocationsSW.Visible = false;

                GridViewRow headerRow = grdSWlocations.HeaderRow;
                ((CheckBox)headerRow.FindControl("allSWlocchk")).Checked = true;

                foreach (GridViewRow dgvr in grdSWlocations.Rows)
                {
                    ((CheckBox)dgvr.FindControl("chk_SWlocReq")).Checked = true;
                }


            }
            else
            {
                btnSubLocationsSW.Visible = true;

                GridViewRow headerRow = grdSWlocations.HeaderRow;
                ((CheckBox)headerRow.FindControl("allSWlocchk")).Checked = false;

                foreach (GridViewRow dgvr in grdSWlocations.Rows)
                {
                    ((CheckBox)dgvr.FindControl("chk_SWlocReq")).Checked = false;
                }
            }

            //_SWSublocs = txtSWLocation.Text;

            foreach (GridViewRow dgvr in grdSWlocations.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("chk_SWlocReq");
                if (chk.Checked)
                {
                    _SWSublocs = _SWSublocs + (dgvr.FindControl("lbl_SWCode") as Label).Text + ",";
                }

            }
        }

        protected void lbtnClearStkLedgr_Click(object sender, EventArgs e)
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

        protected void lbtnSelSubLoc_Click(object sender, EventArgs e)
        {
            var lbtnSelSubLoc = (LinkButton)sender;
            var row = (GridViewRow)lbtnSelSubLoc.NamingContainer;
            Label lbl_DWCode = row.FindControl("lbl_DWCode") as Label;
            Label lbl_DWDescription = row.FindControl("lbl_DWDescription") as Label;
            txtDWLocation.Text=lbl_DWCode.Text;
            txtDWLocation.ToolTip = lbl_DWDescription.Text;
            lblDWLocationName.Text = lbl_DWDescription.Text;
            
        }

        private DataTable LoadBal(DataTable dt)
        {
            if (dt!=null)
            {
                if (dt.Rows.Count>0)
                {
                    foreach (DataRow row  in dt.Rows)
                    {
                        decimal _in=0,_out=0,_bal=0;
                        _in = decimal.TryParse(row["IN_COU"].ToString(), out _in) ? Convert.ToDecimal(row["IN_COU"].ToString()) : _in;
                        _out = decimal.TryParse(row["OUT_COU"].ToString(), out _out) ? Convert.ToDecimal(row["OUT_COU"].ToString()) : _out;
                        _bal=_in-_out;
                        row["BALANCE"] = _bal.ToString();
                    }
                }
            }
            return dt;
        }

        protected void lbtnSelSerSubLoc_Click(object sender, EventArgs e)
        {
            var lbtnSelSubLoc = (LinkButton)sender;
            var row = (GridViewRow)lbtnSelSubLoc.NamingContainer;
            Label lbl_SWCode = row.FindControl("lbl_SWCode") as Label;
            Label lbl_SWDescription = row.FindControl("lbl_SWDescription") as Label;
            txtSWLocation.Text = lbl_SWCode.Text;
            txtSWLocation.ToolTip = lbl_SWDescription.Text;
            lblSWLocationName.Text = lbl_SWDescription.Text;
        }

        protected void txtDWItemCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDWItemCode.Text))
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, "Item", txtDWItemCode.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Item"].ToString()))
                    {
                        if (txtDWItemCode.Text.ToUpper() == row["Item"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            break;
                        }
                    }
                }
                if (b2)
                {
                    txtDWItemCode.ToolTip = toolTip;
                    ValidateAndLoadItemCode(txtDWItemCode.Text, Session["UserCompanyCode"].ToString(), lblBrand1, lblModel1, lblDWItemName);
                    txtDWLocation.Focus();
                    //lbtnViewLedger_Click(null, null);
                }
                else
                {
                    txtDWItemCode.ToolTip = "";
                    txtDWItemCode.Text = "";
                    txtDWItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid item code !!!')", true);
                    return;
                }

            }
            if (txtDWItemCode.Text.Trim() == "")
            {
                txtDWItemCode.Text = "";
                return;
            }
        }

        protected void txtSWItemCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSWItemCode.Text))
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, "Item", txtSWItemCode.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Item"].ToString()))
                    {
                        if (txtSWItemCode.Text.ToUpper() == row["Item"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            break;
                        }
                    }
                }
                if (b2)
                {
                    txtSWItemCode.ToolTip = toolTip;
                    ValidateAndLoadItemCode(txtSWItemCode.Text, Session["UserCompanyCode"].ToString(), lblBrand2, lblModel2, lblSWItemName);
                }
                else
                {
                    txtSWItemCode.ToolTip = "";
                    txtSWItemCode.Text = "";
                    txtSWItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid item code !!!')", true);
                    return;
                }

            }
            if (txtSWItemCode.Text.Trim() == "")
            {
                txtSWItemCode.Text = "";
                return;
            }
        }

        protected void btnClose_Click1(object sender, EventArgs e)
        {
            SerPopShow = "Hide";
            UserPopoup.Hide();
        }

        protected void grdDWSerialDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            _sortDirection = sortOrder;
            //Sort the data.
            dataTable = (DataTable)Session["dataTable"];
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                    grdDWSerialDetails.DataSource = dataTable;
                    grdDWSerialDetails.DataBind();
                    SortDireaction = _sortDirection;
                }
            }
        }

        protected void grdSWSerialDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            _sortDirection = sortOrder;
            //Sort the data.
            dataTable = (DataTable)Session["dataTable"];
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                    grdSWSerialDetails.DataSource = dataTable;
                    grdSWSerialDetails.DataBind();
                    SortDireaction = _sortDirection;
                }
            }
        }

        protected void txtDWLocation_TextChanged(object sender, EventArgs e)
        {
            _DWSublocs = "";
            if (!string.IsNullOrEmpty(txtDWLocation.Text))
            {
                lblDWLocationName.Text = "";
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationDW);
                DataTable _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(para, "Code", txtDWLocation.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtDWLocation.Text.ToUpper() == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            break;
                        }
                    }
                }
                if (b2)
                {
                    txtDWLocation.ToolTip = toolTip;
                    lblDWLocationName.Text = toolTip;
                    LoadSubLocation(txtDWCompany.Text, txtDWLocation.Text.ToUpper().Trim(), grdDWlocations);
                    CheckSublocationBoxCheckDW_Click(null, null);
                   // setupcheckboxValuesDW();
                }
                else
                {
                    txtDWLocation.ToolTip = "";
                    txtDWLocation.Text = "";
                    txtDWLocation.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You dont have permission for this location !')", true);
                    return;
                }

            }
            if (txtDWLocation.Text.Trim() == "")
            {
                txtDWLocation.Text = "";
                return;
            }
            
        }

        protected void txtSWLocation_TextChanged(object sender, EventArgs e)
        {
            _SWSublocs = "";
            if (!string.IsNullOrEmpty(txtSWLocation.Text))
            {
                bool b2 = false;
                lblSWLocationName.Text = "";
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocationSW);
                DataTable _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(para, "Code", txtSWLocation.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtSWLocation.Text.ToUpper() == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            break;
                        }
                    }
                }
                if (b2)
                {
                    txtSWLocation.ToolTip = toolTip;
                    LoadSubLocation(txtSWCompany.Text, txtSWLocation.Text.ToUpper().Trim(), grdSWlocations);
                    CheckSublocationBoxCheckSW_Click(null, null);
                }
                else
                {
                    txtSWLocation.ToolTip = "";
                    txtSWLocation.Text = "";
                    txtSWLocation.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You dont have permission for this location !')", true);
                    return;
                }

            }
            if (txtSWLocation.Text.Trim() == "")
            {
                txtSWLocation.Text = "";
                return;
            }
            
        }

        private DataTable MakeGridBalWithoutStatus(DataTable dt)
        {
            DataTable _dtNew = new DataTable();
            try
            {
                #region MakeDataTable
                _dtNew.Columns.Add("location", typeof(string));
                _dtNew.Columns.Add("other_loc", typeof(string));
                _dtNew.Columns.Add("doc_date", typeof(DateTime));
                _dtNew.Columns.Add("doc_no", typeof(string));
                _dtNew.Columns.Add("other_doc", typeof(string));
                _dtNew.Columns.Add("man_ref", typeof(string));
                _dtNew.Columns.Add("doc_type", typeof(string));
                _dtNew.Columns.Add("in_cou", typeof(Int32));
                _dtNew.Columns.Add("out_cou", typeof(Int32));
                _dtNew.Columns.Add("balance", typeof(Int32));
                _dtNew.Columns.Add("status", typeof(string));
                #endregion
                DataRow _dr;
                Int32 _openBal = 0;
                Int32 _in = 0, _out = 0, _bal = 0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int x = dt.Rows.Count - 1; x >= 0; x--)
                        {
                            DataRow row = dt.Rows[x];
                            if (row["DOC_TYPE"].ToString() == "OPERNING_BAL")
                            {
                                _dr = _dtNew.NewRow();
                                _dr["location"] = row["location"].ToString();
                                _dr["other_loc"] = row["other_loc"].ToString();
                                _dr["doc_date"] = Convert.ToDateTime(row["doc_date"].ToString());
                                _dr["doc_no"] = row["doc_no"].ToString();
                                _dr["other_doc"] = row["other_doc"].ToString();
                                _dr["man_ref"] = row["man_ref"].ToString();
                                _dr["doc_type"] = row["doc_type"].ToString();
                                _dr["in_cou"] = Convert.ToInt32(row["in_cou"].ToString());
                                _dr["out_cou"] = Convert.ToInt32(row["out_cou"].ToString());

                                _in = Int32.TryParse(row["BALANCE"].ToString(), out _in) ? Convert.ToInt32(row["BALANCE"].ToString()) : _in;
                               // _out = Int32.TryParse(row["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(row["OUT_COU"].ToString()) : _out;
                                _openBal = _in - _out;

                                _dr["balance"] = _in.ToString();
                                _dr["status"] = row["status"].ToString();
                                _dtNew.Rows.Add(_dr);
                                row.Delete();
                            }
                        }
                        dt.AcceptChanges();
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = dt.DefaultView;
                            dv.Sort = "seq_no";
                            dt = dv.ToTable();
                        }
                        _bal = _openBal;
                        foreach (DataRow item in dt.Rows)
                        {
                            _dr = _dtNew.NewRow();
                            _dr["location"] = item["location"].ToString();
                            _dr["other_loc"] = item["other_loc"].ToString();
                            _dr["doc_date"] = Convert.ToDateTime(item["doc_date"].ToString());
                            _dr["doc_no"] = item["doc_no"].ToString();
                            _dr["other_doc"] = item["other_doc"].ToString();
                            _dr["man_ref"] = item["man_ref"].ToString();
                            _dr["doc_type"] = item["doc_type"].ToString();
                            bool _ifAva = false;
                            foreach (DataRow rw in _dtNew.Rows)
                            {
                                if (rw["location"].ToString() == item["location"].ToString()
                                    && rw["other_loc"].ToString() == item["other_loc"].ToString()
                                    && Convert.ToDateTime(rw["doc_date"].ToString()) == Convert.ToDateTime(item["doc_date"].ToString())
                                    && rw["doc_no"].ToString() == item["doc_no"].ToString()
                                    && rw["other_doc"].ToString() == item["other_doc"].ToString()
                                    && rw["man_ref"].ToString() == item["man_ref"].ToString()
                                    && rw["doc_type"].ToString() == item["doc_type"].ToString()
                                    )
                                {
                                    rw["in_cou"] = Convert.ToInt32(rw["in_cou"].ToString()) + Convert.ToInt32(item["in_cou"].ToString());
                                    rw["out_cou"] = Convert.ToInt32(rw["out_cou"].ToString()) + Convert.ToInt32(item["out_cou"].ToString());
                                    _in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                                    _out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                                    _bal = _bal + _in - _out;

                                    item["BALANCE"] = _bal.ToString();
                                    rw["BALANCE"] = _bal.ToString();
                                    rw["status"] = item["status"].ToString();
                                    _ifAva = true;
                                }
                            }
                            if (!_ifAva)
                            {
                                _dr["in_cou"] = Convert.ToInt32(item["in_cou"].ToString());
                                _dr["out_cou"] = Convert.ToInt32(item["out_cou"].ToString());
                                _in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                                _out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                                _bal = _bal + _in - _out;

                                item["BALANCE"] = _bal.ToString();
                                _dr["BALANCE"] = _bal.ToString();
                                _dr["status"] = item["status"].ToString();
                                _dtNew.Rows.Add(_dr);
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error : Grid Balance", 4);
            }
            return _dtNew;
        }
        private DataTable MakeGridBalWithoutStatusOld(DataTable dt)
        {
            DataTable _dtNew = new DataTable();
            try
            {
                #region MakeDataTable
                _dtNew.Columns.Add("location", typeof(string));
                _dtNew.Columns.Add("other_loc", typeof(string));
                _dtNew.Columns.Add("doc_date", typeof(DateTime));
                _dtNew.Columns.Add("doc_no", typeof(string));
                _dtNew.Columns.Add("other_doc", typeof(string));
                _dtNew.Columns.Add("man_ref", typeof(string));
                _dtNew.Columns.Add("doc_type", typeof(string));
                _dtNew.Columns.Add("in_cou", typeof(Int32));
                _dtNew.Columns.Add("out_cou", typeof(Int32));
                _dtNew.Columns.Add("balance", typeof(Int32));
                _dtNew.Columns.Add("status", typeof(string));
                #endregion
                DataRow _dr;
                Int32 _openBal = 0;
                Int32 _in = 0, _out = 0, _bal = 0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int x = dt.Rows.Count - 1; x >= 0; x--)
                        {
                            DataRow row = dt.Rows[x];
                            if (row["DOC_TYPE"].ToString() == "OPERNING_BAL")
                            {
                                _dr = _dtNew.NewRow();
                                _dr["location"] = row["location"].ToString();
                                _dr["other_loc"] = row["other_loc"].ToString();
                                _dr["doc_date"] = Convert.ToDateTime(row["doc_date"].ToString());
                                _dr["doc_no"] = row["doc_no"].ToString();
                                _dr["other_doc"] = row["other_doc"].ToString();
                                _dr["man_ref"] = row["man_ref"].ToString();
                                _dr["doc_type"] = row["doc_type"].ToString();
                                _dr["in_cou"] = Convert.ToInt32(row["in_cou"].ToString());
                                _dr["out_cou"] = Convert.ToInt32(row["out_cou"].ToString());

                                _in = Int32.TryParse(row["IN_COU"].ToString(), out _in) ? Convert.ToInt32(row["IN_COU"].ToString()) : _in;
                                _out = Int32.TryParse(row["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(row["OUT_COU"].ToString()) : _out;
                                _openBal = _in - _out;

                                _dr["balance"] = _openBal.ToString();
                                _dr["status"] = row["status"].ToString();
                                _dtNew.Rows.Add(_dr);
                                row.Delete();
                            }
                        }
                        dt.AcceptChanges();
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = dt.DefaultView;
                            dv.Sort = "seq_no";
                            dt = dv.ToTable();
                        }
                        _bal = _openBal;
                        foreach (DataRow item in dt.Rows)
                        {
                            _dr = _dtNew.NewRow();
                            _dr["location"] = item["location"].ToString();
                            _dr["other_loc"] = item["other_loc"].ToString();
                            _dr["doc_date"] = Convert.ToDateTime(item["doc_date"].ToString());
                            _dr["doc_no"] = item["doc_no"].ToString();
                            _dr["other_doc"] = item["other_doc"].ToString();
                            _dr["man_ref"] = item["man_ref"].ToString();
                            _dr["doc_type"] = item["doc_type"].ToString();
                            bool _ifAva = false;
                            foreach (DataRow rw  in _dtNew.Rows)
                            {
                                if (rw["location"].ToString() == item["location"].ToString()
                                    && rw["other_loc"].ToString() == item["other_loc"].ToString()
                                    && Convert.ToDateTime(rw["doc_date"].ToString()) == Convert.ToDateTime(item["doc_date"].ToString())
                                    && rw["doc_no"].ToString() == item["doc_no"].ToString()
                                    && rw["other_doc"].ToString() == item["other_doc"].ToString()
                                    && rw["man_ref"].ToString() == item["man_ref"].ToString()
                                    && rw["doc_type"].ToString() == item["doc_type"].ToString()
                                    )
                                {
                                    rw["in_cou"] = Convert.ToInt32(rw["in_cou"].ToString()) + Convert.ToInt32(item["in_cou"].ToString());
                                    rw["out_cou"] = Convert.ToInt32(rw["out_cou"].ToString()) + Convert.ToInt32(item["out_cou"].ToString());
                                    _in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                                    _out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                                    _bal = _bal + _in - _out;

                                    item["BALANCE"] = _bal.ToString();
                                    rw["BALANCE"] = _bal.ToString();
                                    rw["status"] = item["status"].ToString();
                                    _ifAva = true;
                                }
                            }
                            if (!_ifAva)
                            {
                                _dr["in_cou"] = Convert.ToInt32(item["in_cou"].ToString());
                                _dr["out_cou"] = Convert.ToInt32(item["out_cou"].ToString());
                                _in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                                _out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                                _bal = _bal + _in - _out;

                                item["BALANCE"] = _bal.ToString();
                                _dr["BALANCE"] = _bal.ToString();
                                _dr["status"] = item["status"].ToString();
                                _dtNew.Rows.Add(_dr);
                            }
                            
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error : Grid Balance", 4); 
            }
            return _dtNew;
        }

        private DataTable MakeGridBalWithStatus(DataTable dt)
        {
            DataTable _dtNew = new DataTable();
            try
            {
                #region MakeDataTable
                _dtNew.Columns.Add("location", typeof(string));
                _dtNew.Columns.Add("other_loc", typeof(string));
                _dtNew.Columns.Add("doc_date", typeof(DateTime));
                _dtNew.Columns.Add("doc_no", typeof(string));
                _dtNew.Columns.Add("other_doc", typeof(string));
                _dtNew.Columns.Add("man_ref", typeof(string));
                _dtNew.Columns.Add("doc_type", typeof(string));
                _dtNew.Columns.Add("in_cou", typeof(Int32));
                _dtNew.Columns.Add("out_cou", typeof(Int32));
                _dtNew.Columns.Add("balance", typeof(Int32));
                _dtNew.Columns.Add("status", typeof(string));
                #endregion
                DataRow _dr;
                Int32 _openBal = 0;
                Int32 _in = 0, _out = 0, _bal = 0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<OpenBalTemp> balList = new List<OpenBalTemp>();

                        for (int x = dt.Rows.Count - 1; x >= 0; x--)
                        {
                            DataRow row = dt.Rows[x];
                            if (row["DOC_TYPE"].ToString() == "OPERNING_BAL")
                            {
                                _dr = _dtNew.NewRow();
                                _dr["location"] = row["location"].ToString();
                                _dr["other_loc"] = row["other_loc"].ToString();
                                _dr["doc_date"] = Convert.ToDateTime(row["doc_date"].ToString());
                                _dr["doc_no"] = row["doc_no"].ToString();
                                _dr["other_doc"] = row["other_doc"].ToString();
                                _dr["man_ref"] = row["man_ref"].ToString();
                                _dr["doc_type"] = row["doc_type"].ToString();
                                _dr["in_cou"] = Convert.ToInt32(row["in_cou"].ToString());
                                _dr["out_cou"] = Convert.ToInt32(row["out_cou"].ToString());

                                _in = Int32.TryParse(row["BALANCE"].ToString(), out _in) ? Convert.ToInt32(row["BALANCE"].ToString()) : _in;
                                //_out = Int32.TryParse(row["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(row["OUT_COU"].ToString()) : _out;
                                _openBal = _in - _out;

                                _dr["balance"] = _in.ToString();
                                _dr["status"] = row["status"].ToString();

                                OpenBalTemp tmp = new OpenBalTemp();
                                tmp.Status = _dr["status"].ToString();
                                tmp.OpenBal = Convert.ToInt32(_dr["BALANCE"].ToString());
                                balList.Add(tmp);

                                _dtNew.Rows.Add(_dr);
                                row.Delete();
                            }
                        }
                        dt.AcceptChanges();
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = dt.DefaultView;
                            dv.Sort = "seq_no";
                            dt = dv.ToTable();
                        }

                        foreach (DataRow item in dt.Rows)
                        {
                            _dr = _dtNew.NewRow();
                            _dr["location"] = item["location"].ToString();
                            _dr["other_loc"] = item["other_loc"].ToString();
                            _dr["doc_date"] = Convert.ToDateTime(item["doc_date"].ToString());
                            _dr["doc_no"] = item["doc_no"].ToString();
                            _dr["other_doc"] = item["other_doc"].ToString();
                            _dr["man_ref"] = item["man_ref"].ToString();
                            _dr["doc_type"] = item["doc_type"].ToString();
                            _dr["in_cou"] = Convert.ToInt32(item["in_cou"].ToString());
                            _dr["out_cou"] = Convert.ToInt32(item["out_cou"].ToString());

                            string _status = item["status"].ToString();
                            var sts = balList.Where(c => c.Status == _status).FirstOrDefault();
                            if (sts != null)
                            {
                                _in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                                _out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                                sts.OpenBal = sts.OpenBal + _in - _out;
                                _dr["BALANCE"] = sts.OpenBal.ToString();
                            }
                            else
                            {
                                OpenBalTemp tmp = new OpenBalTemp();
                                tmp.Status = item["status"].ToString();
                                tmp.OpenBal = Convert.ToInt32(item["BALANCE"].ToString());
                                balList.Add(tmp);
                            }
                            _dr["status"] = item["status"].ToString();
                            _dtNew.Rows.Add(_dr);
                        }
                    }
                }
                #region sum grid data
               /* DataTable _dtTemp = new DataTable();
                _dtTemp.Columns.Add("location", typeof(string));
                _dtTemp.Columns.Add("other_loc", typeof(string));
                _dtTemp.Columns.Add("doc_date", typeof(DateTime));
                _dtTemp.Columns.Add("doc_no", typeof(string));
                _dtTemp.Columns.Add("other_doc", typeof(string));
                _dtTemp.Columns.Add("man_ref", typeof(string));
                _dtTemp.Columns.Add("doc_type", typeof(string));
                _dtTemp.Columns.Add("in_cou", typeof(Int32));
                _dtTemp.Columns.Add("out_cou", typeof(Int32));
                _dtTemp.Columns.Add("balance", typeof(Int32));
                _dtTemp.Columns.Add("status", typeof(string));
                foreach (DataRow item in _dtNew.Rows)
                {
                    bool _ifAva = false;
                    foreach (DataRow rw in _dtNew.Rows)
                    {
                        if (rw["location"].ToString() == item["location"].ToString()
                            && rw["other_loc"].ToString() == item["other_loc"].ToString()
                            && Convert.ToDateTime(rw["doc_date"].ToString()) == Convert.ToDateTime(item["doc_date"].ToString())
                            && rw["doc_no"].ToString() == item["doc_no"].ToString()
                            && rw["other_doc"].ToString() == item["other_doc"].ToString()
                            && rw["man_ref"].ToString() == item["man_ref"].ToString()
                            && rw["doc_type"].ToString() == item["doc_type"].ToString()
                            && rw["status"].ToString() == item["status"].ToString()
                            && rw["DOC_TYPE"].ToString() != "OPERNING_BAL"
                            )
                        {
                            rw["in_cou"] = Convert.ToInt32(rw["in_cou"].ToString()) + Convert.ToInt32(item["in_cou"].ToString());
                            rw["out_cou"] = Convert.ToInt32(rw["out_cou"].ToString()) + Convert.ToInt32(item["out_cou"].ToString());
                            _in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                            _out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                            _bal = _bal + _in - _out;
                            rw["BALANCE"] = _bal.ToString();
                            _ifAva = true;
                        }
                    }
                    if (!_ifAva)
                    {
                        //_dr["in_cou"] = Convert.ToInt32(item["in_cou"].ToString());
                        //_dr["out_cou"] = Convert.ToInt32(item["out_cou"].ToString());
                        //_in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                        //_out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                        //_bal = _bal + _in - _out;

                        //item["BALANCE"] = _bal.ToString();
                        //_dr["BALANCE"] = _bal.ToString();
                        //_dr["status"] = item["status"].ToString();
                        //_dtNew.Rows.Add(_dr);
                    }
                }*/
                #endregion
            }
            catch (Exception ex)
            {
                DisplayMessage("Error : Grid Balance", 4); 
            }
           
            return _dtNew;
        }

        protected void txtDWDocType_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDWDocType.Text))
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementTypes(para, "Code", txtDWDocType.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtDWDocType.Text.ToUpper() == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            break;
                        }
                    }
                }
                if (b2)
                {
                    txtDWDocType.ToolTip = toolTip;
                }
                else
                {
                    txtDWDocType.ToolTip = "";
                    txtDWDocType.Text = "";
                    txtDWDocType.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid document type !!!')", true);
                    return;
                }

            }
            if (txtDWDocType.Text.Trim() == "")
            {
                txtDWDocType.Text = "";
                return;
            }
        }

        protected void txtSWserial_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSWserial.Text))
            {
                if (string.IsNullOrEmpty(txtSWItemCode.Text))
                {
                    DisplayMessage("Please enter item code !!!", 2); txtSWItemCode.Focus(); return;
                }
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable _result = CHNLSVC.Inventory.SearchSerialsIntByItem(para, "Serial 1", txtSWserial.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Serial 1"].ToString()))
                    {
                        if (txtSWserial.Text == row["Serial 1"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Serial 1"].ToString();
                            break;
                        }
                    }
                }
                if (b2)
                {
                    txtSWserial.ToolTip = toolTip;
                }
                else
                {
                    txtSWserial.ToolTip = "";
                    txtSWserial.Text = "";
                    txtSWserial.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid serial # !!!')", true);
                    return;
                }

            }
            if (txtSWserial.Text.Trim() == "")
            {
                txtSWserial.Text = "";
                return;
            }
        }

        private DataTable GetStockLedgerData(string _userr, string _chnl, string _brand, string _model, string _item, Int32 _itemSts, string _itmCat1, string _itmCat2, string _itmCat3,
            Int32 _withCost, DateTime _asAtDate, Int32 _withSer, Int32 _status, string _com, string _loc,
            DateTime _dtFrom, DateTime _dtTo, Int32 isStatus, string _doctype = null)
        {
            DataTable _dtOpenBal = new DataTable();
            DataTable _dtAllOpenBal = new DataTable();
          
            #region Create New Table
            DataTable _dtNew = new DataTable("tbl1");
            _dtNew.Columns.Add(new DataColumn("location"));
            _dtNew.Columns.Add(new DataColumn("other_loc"));
            _dtNew.Columns.Add(new DataColumn("doc_date"));
            _dtNew.Columns.Add(new DataColumn("doc_no"));
            _dtNew.Columns.Add(new DataColumn("other_doc"));
            _dtNew.Columns.Add(new DataColumn("man_ref"));
            _dtNew.Columns.Add(new DataColumn("doc_type"));
            _dtNew.Columns.Add(new DataColumn("in_cou", typeof(decimal)));
            _dtNew.Columns.Add(new DataColumn("out_cou", typeof(decimal)));
            _dtNew.Columns.Add(new DataColumn("balance", typeof(decimal)));
            _dtNew.Columns.Add(new DataColumn("status"));
            _dtNew.Columns.Add(new DataColumn("seq_no"));

            DataTable _dtNewData = new DataTable("tbl2");
            _dtNewData.Columns.Add(new DataColumn("location"));
            _dtNewData.Columns.Add(new DataColumn("other_loc"));
            _dtNewData.Columns.Add(new DataColumn("doc_date"));
            _dtNewData.Columns.Add(new DataColumn("doc_no"));
            _dtNewData.Columns.Add(new DataColumn("other_doc"));
            _dtNewData.Columns.Add(new DataColumn("man_ref"));
            _dtNewData.Columns.Add(new DataColumn("doc_type"));
            _dtNewData.Columns.Add(new DataColumn("in_cou", typeof(decimal)));
            _dtNewData.Columns.Add(new DataColumn("out_cou", typeof(decimal)));
            _dtNewData.Columns.Add(new DataColumn("balance", typeof(decimal)));
            _dtNewData.Columns.Add(new DataColumn("status"));
            _dtNewData.Columns.Add(new DataColumn("seq_no"));
            #endregion

            #region Get Open Bal
            string[] seperator = new string[] { "," };
            string[] searchParams = _loc.Split(seperator, StringSplitOptions.None);
            _asAtDate = _asAtDate.AddDays(-1);
            for (int i = 0; i < searchParams.Length; i++)
            {
                if (!string.IsNullOrEmpty(searchParams[i]))
                {
                    _dtOpenBal = CHNLSVC.MsgPortal.GetInventoryBalanceAsAt(_userr, _chnl, _brand, _model, _item, isStatus == 1 ? true : false, _itmCat1, _itmCat2, _itmCat3,
                        _withCost, _asAtDate, _withSer, _status, _com, searchParams[i]);
                   _dtAllOpenBal.Merge(_dtOpenBal);
                }
            }
            #endregion
            #region Genarate Open Bal
            List<MasterItemStatus> _statusList = CHNLSVC.General.GetAllStockTypes(_com);
            if (_dtAllOpenBal.Rows.Count > 0)
            {
                foreach (DataRow dr in _dtAllOpenBal.Rows)
                {
                    DataRow _newDr = _dtNewData.NewRow();
                    _newDr["LOCATION"] = dr["LOC_CODE"].ToString();
                    _newDr["OTHER_LOC"] = "";
                    _newDr["DOC_DATE"] = _dtFrom.ToString();
                    _newDr["DOC_NO"] = ""; 
                    _newDr["OTHER_DOC"] = "";
                    _newDr["MAN_REF"] = ""; 
                    _newDr["DOC_TYPE"] = "OPERNING_BAL";
                    _newDr["IN_COU"] = Convert.ToDecimal("0");
                    _newDr["OUT_COU"] = Convert.ToDecimal("0");
                    _newDr["BALANCE"] = Convert.ToDecimal(dr["QTY"].ToString());
                    _newDr["STATUS"] = "";//dr["ITEM_STATUS"].ToString(); 
                    var v = _statusList.Where(c => c.Mis_desc == dr["ITEM_STATUS"].ToString()).FirstOrDefault();
                    if (v!=null)
                    {
                        _newDr["STATUS"] = v.Mis_cd;
                    }
                    _newDr["SEQ_NO"] = "0";
                    if (isStatus == 1)
	                {
		              _dtNewData.Rows.Add(_newDr);  
	                }
                    if (isStatus == 0)
                    {
                        if (_dtNewData.Rows.Count == 0)
                        {
                            _dtNewData.Rows.Add(_newDr);
                        }
                        else
                        {
                            decimal _tmpQty = 0, _oldBal = 0, _curBal = 0, _newBal = 0;
                            _oldBal = decimal.TryParse(_dtNewData.Rows[0]["BALANCE"].ToString(), out _tmpQty) ? Convert.ToDecimal(_dtNewData.Rows[0]["BALANCE"].ToString()) : 0;
                            _curBal = decimal.TryParse(dr["QTY"].ToString(), out _tmpQty) ? Convert.ToDecimal(dr["QTY"].ToString()) : 0;
                            _newBal = _oldBal + _curBal;
                            _dtNewData.Rows[0]["BALANCE"] = _newBal;
                        }
                    }
                }
            }
            #endregion

            #region Get Trans Action Data
            DataTable _dtTrans = CHNLSVC.MsgPortal.StockBalanceSearchNew(_dtFrom, _dtTo, _item, _loc, _com, isStatus == 1 ? true : false, _doctype);
            for (int x = 0; x < _dtTrans.Rows.Count; x++)
            {
                DataRow dr = _dtTrans.Rows[x];
                if (dr["DOC_TYPE"].ToString() != "OPERNING_BAL")
                {
                    DataRow _newDr = _dtNewData.NewRow();
                    _newDr["location"] = dr["location"].ToString();
                    _newDr["other_loc"] = dr["other_loc"].ToString();
                    _newDr["doc_date"] = dr["doc_date"].ToString();
                    _newDr["doc_no"] = dr["doc_no"].ToString();
                    _newDr["other_doc"] = dr["other_doc"].ToString();
                    _newDr["man_ref"] = dr["man_ref"].ToString();
                    _newDr["doc_type"] = dr["doc_type"].ToString();
                    _newDr["in_cou"] = Convert.ToDecimal(dr["in_cou"].ToString());
                    _newDr["out_cou"] = Convert.ToDecimal(dr["out_cou"].ToString());
                    _newDr["balance"] = Convert.ToDecimal(dr["balance"].ToString());
                    _newDr["status"] = dr["status"].ToString();
                    _newDr["seq_no"] = dr["seq_no"].ToString();
                    _dtNewData.Rows.Add(_newDr);
                }
            }
            #endregion
            return _dtNewData;
        }

    }

    public class OpenBalTemp
    {
        public string Status { get; set; }
        public int OpenBal { get; set; }
    }
}