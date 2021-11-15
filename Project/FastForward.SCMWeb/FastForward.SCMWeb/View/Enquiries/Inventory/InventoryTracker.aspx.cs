using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.WindowsERPClient;
using FastForward.SCMWeb.UserControls;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System.Drawing;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.General;

namespace FastForward.SCMWeb.View.Enquiries.Inventory
{
    public partial class InventoryTracker : BasePage
    {
        private string selectLocation = "";
        string Select_company = "";
        string _userid = string.Empty;
        private List<int> RoleId = new List<int>();
        string _sortDirection;
        string SortDireaction;
        DataTable dataTable;
        public string _pkgTblTp
        {
            get { if (ViewState["_pkgTblTp"] == null) return null; return ViewState["_pkgTblTp"].ToString(); }
            set { ViewState["_pkgTblTp"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    _pkgTblTp = "";
                    //if (lblSearchType.Text == "ItemRes")
                    //{
                    //    ItmResDet.Visible = true;
                    //}
                    //else
                    //{
                    //    ItmResDet.Visible = false;
                    //}

                    Select_company = Session["UserCompanyCode"].ToString();
                    _userid = Session["UserID"].ToString();
                    Session["SelectCompany"] = Select_company;
                    dataTable = new DataTable();
                    Session["dataTable"] = dataTable;
                    ViewState["sortOrder"] = "desc";
                    PopulateDropDowns();
                    FillEmptryGrids();
                    txtbldate2.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    txtbltodate2.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

                    string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString()))
                        ? Session["UserDefProf"].ToString()
                        : Session["UserDefLoca"].ToString();
                    if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation,
                        "INVAC"))
                    {
                        lBtnCompany.Enabled = true;
                        ucLoactionSearch.Visible = true;
                        panel_advanceSearch.Enabled = true;

                        chkAdvancedSearch.Visible = true;
                        chkAllLocation.Enabled = true;
                        hdfUserLevel.Value = "1";
                        txtCompany.Enabled = true;

                        chkShowCostValue.Visible = true;
                        lblShowCostValue.Visible = true;
                    }
                    else
                    {
                        chkShowCostValue.Visible = false;
                        lblShowCostValue.Visible = false;
                        chkShowCostValue.Checked = false;
                        chkShowCostValue_CheckedChanged(null, null);

                        txtCompany.Enabled = false;
                        lBtnCompany.Enabled = false;
                        chkAllLocation.Enabled = false;
                        hdfUserLevel.Value = "0";

                        ucLoactionSearch.Visible = false;
                        panel_advanceSearch.Visible = false;
                        chkAdvancedSearch.Visible = false;
                        txtLocation.Enabled = false;
                        //--------------------------------------------------------**
                    }
                    if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INVWC"))
                    {
                        chkShowCostValue.Visible = true;
                        chkShowCostValue.Checked = true;
                        lblShowCostValue.Visible = true;
                        chkAdvancedSearch_CheckedChanged(null, null);
                    }
                    else
                    {
                        chkShowCostValue.Visible = false;
                        chkShowCostValue.Checked = false;
                        lblShowCostValue.Visible = false;
                        chkAdvancedSearch_CheckedChanged(null, null);
                    }
                    if (chkAllLocation.Enabled == false)
                    {
                        if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10038))
                        {
                            chkAllLocation.Enabled = true;
                            chkAllLocation.Text = Session["UserDefLoca"].ToString();
                            txtChannel.Text = "";
                            chkAllLocation.Text = "";

                            txtChannel.Enabled = false;
                            txtChannelLocation.Enabled = false;

                            lBtnChannel.Enabled = false;
                            lBtnChanLocation.Enabled = false;
                        }
                    }
                    txtCompany.Text = Session["UserCompanyCode"].ToString();
                    txtLocation.Text = Session["UserDefLoca"].ToString();
                    ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                    ucLoactionSearch.ParrentFormName = "InventoryTracker";

                    FillGrdAllAllocation();
                    DataTable _role = CHNLSVC.Security.GetCompanyUserRole(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString());
                    Session["DATARoleId"] = null;
                    divDanger.Visible = false;
                    if (_role == null || _role.Rows.Count <= 0)
                    {
                        lblAlertDanger.Text = "User role not found, Can not Proceed";
                        divDanger.Visible = true;
                        return;
                    }
                    foreach (DataRow dr in _role.Rows)
                    {
                        RoleId.Add(Convert.ToInt32(dr["serm_role_id"]));
                    }
                    Session["DATARoleId"] = RoleId;
                    DataTable _userChannel = CHNLSVC.Inventory.GetAllChannelForInventoryTracker(Session["UserCompanyCode"].ToString(), RoleId);
                    if (_userChannel.Rows.Count > 0)
                    {
                        panel_chanel.Visible = true;
                    }
                    else
                    {
                        panel_chanel.Visible = false;
                    }
                    txtLocation.Enabled = true;
                    txtChannel.Text = "";
                    txtChannelLocation.Text = "";

                    txtChannel.Enabled = false;
                    txtChannelLocation.Enabled = false;

                    lBtnChannel.Enabled = false;
                    lBtnChanLocation.Enabled = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "CheckAllLocation();", true);
                    Session["SELECTLOCATION"] = null;
                    Session["UcLoSeLocation"] = null;

                    /*  Product Details    */

                    grdWarrentyDetailsGL.DataSource = new int[] { };
                    grdWarrentyDetailsGL.DataBind();

                    grdWarrentyDetailsSP.DataSource = new int[] { };
                    grdWarrentyDetailsSP.DataBind();

                    txtCompanySWTD.Text = Session["UserCompanyCode"].ToString();
                    txtCompanySWTD_TextChanged(null, null);
                    grdStatusWiseTaxDet.DataSource = new int[] { };
                    grdStatusWiseTaxDet.DataBind();

                    grdServiceShedule.DataSource = new int[] { };
                    grdServiceShedule.DataBind();

                    grdComponentItem.DataSource = new int[] { };
                    grdComponentItem.DataBind();

                    grdSimmilarItem.DataSource = new int[] { };
                    grdSimmilarItem.DataBind();

                    grdTaxClaimable.DataSource = new int[] { };
                    grdTaxClaimable.DataBind();

                    grdDocWiseBal.DataSource = new int[] { };
                    grdDocWiseBal.DataBind();

                    grdStockAllo.DataSource = new int[] { };
                    grdStockAllo.DataBind();
                    
                    hdfTabIndex.Value = "#InventoryTracker";
                    txtDWfrom.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    txtDWto.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                }
                else
                {
                    string s = (string)Session["ShowUcDocument"];
                    if (s == "Y")
                    {
                        //  string s = Session["ShowUcDocument"].ToString();
                        PopupDocument.Show();
                    }
                    //if (lblSearchType.Text == "ItemRes")
                    //{
                    //    ItmResDet.Visible = true;
                    //}
                    //else
                    //{
                    //    ItmResDet.Visible = false;
                    //}
                    //lblItemRes.Text = " " ;
                    //lblModelRes.Text = " " ;
                    //lblQtyRes.Text = " ";

                    txtDWfrom.Text = Request[txtDWfrom.UniqueID];
                    txtDWto.Text = Request[txtDWto.UniqueID];

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        private void FillEmptryGrids()
        {
            try
            {
                grvAllLocation.DataSource = null;
                grvAllLocation.DataBind();

                dgvItemDetails.DataSource = null;
                dgvItemDetails.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        private void PopulateDropDowns()
        {
            try
            {
                Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
                List<KeyValuePair<string, string>> myList = status_list.ToList();

                myList.Sort((firstPair, nextPair) =>
                {
                    return firstPair.Value.CompareTo(nextPair.Value);
                }
                );
                ddlItemStatus.DataSource = myList;
                ddlItemStatus.DataTextField = "Value";
                ddlItemStatus.DataValueField = "Key";

                if (status_list != null)
                {
                    ddlItemStatus.DataBind();
                }
                ddlItemStatus.Items.Insert(0, new ListItem("Any", "%"));
                ddlItemStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void FillGrdAllAllocation()
        {
            try
            {
                List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(Session["UserCompanyCode"].ToString(), txtLocation.Text.ToUpper());
                if (loc_list == null)
                {
                    MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtLocation.Text.ToUpper());
                    loc_list = new List<MasterLocation>();
                    loc_list.Add(loc_);
                }
                else if (loc_list.Count < 1)
                {
                    MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtLocation.Text.ToUpper());
                    loc_list.Add(loc_);
                }
                grvAllLocation.DataSource = null;
                grvAllLocation.AutoGenerateColumns = false;
                grvAllLocation.DataSource = loc_list;
                grvAllLocation.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "CheckAllLocation();", true);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void chkShowCostValue_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvItemDetails.Rows)
            {
                Label lblCost = (Label)row.FindControl("lblCostVal");
                if (chkShowCostValue.Checked)
                {
                    lblCost.Visible = true;
                }
                if (!chkShowCostValue.Checked)
                {
                    lblCost.Visible = false;
                }
            }
        }

        protected void chkAdvancedSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdvancedSearch.Checked)
            {
                //txtLocation.Text = ucLoactionSearch.ProfitCenter;
                //AllLocationBind();
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            Select_company = (string)Session["SelectCompany"];
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Channel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item2:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Tobond_bl:
                    {
                        int cusdec = 0;
                        string _tp = null;
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + cusdec + seperator + txtitemcode2.Text + seperator + _tp);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Tobond_bl_2:
                    {
                        int cusdec = 0;
                        string _tp = null;
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + 1 + seperator + txtItemCode.Text.Trim() + seperator + _tp);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WHAREHOUSE:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtMainCategory.Text.ToUpper() + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub3:
                    {
                        paramsText.Append(txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.Trim().ToUpper() + seperator + txtItemRange.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub4:
                    {
                        paramsText.Append(txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.Trim().ToUpper() + seperator + txtItemRange.Text.Trim().ToUpper() + seperator + txtCat4.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(txtCompany.Text.Trim().ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }

                //load empty grid
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append("-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "Loc" + seperator + "-999" + seperator + "-999" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvTrcChnl:
                    {
                        string roleid = "";
                        RoleId = (List<int>)Session["DATARoleId"];
                        foreach (int role in RoleId)
                        {
                            roleid = roleid + "," + role;
                        }

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + roleid + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append(txtCompany.Text + seperator + txtChannel.Text + seperator + txtChannelLocation.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + txtChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                //    {
                //    //    paramsText.Append(txtCompany.Text + seperator + txtChannel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                //    //    break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Inventory_Tracker: // added by darshana to get packing item details
                    {
                        string s = "";
                        string status = null;
                        if (ddlItemStatus.SelectedItem == null)
                            status = "";
                        else
                        {
                            //---------------------
                            ////ComboboxItem combo_reqtp = (ComboboxItem)DDLStatus.SelectedItem;
                            //ListItem combo_reqtp = (ListItem)ddlItemStatus.SelectedItem;
                            //status = combo_reqtp.Value.ToString();
                            status = ddlItemStatus.SelectedItem.Value;
                            //---------------------
                        }
                        if (Session["UserCompanyCode"].ToString() == "AST")
                        {
                            string _item = "";
                            //kapila 18/11/2013
                            if (txtItemCode.Text.Length == 16)
                                _item = txtItemCode.Text.Substring(1, 7);
                            else if (txtItemCode.Text.Length == 15)
                                _item = txtItemCode.Text.Substring(0, 7);
                            else if (txtItemCode.Text.Length == 8)
                                _item = txtItemCode.Text.Substring(1, 7);
                            else if (txtItemCode.Text.Length == 20)
                                _item = txtItemCode.Text.Substring(0, 12);
                            else
                                _item = txtItemCode.Text;

                            txtItemCode.Text = _item;
                        }
                        if (chkChannel.Checked)
                        {
                            if (txtChannelLocation.Text == "")
                            {
                                paramsText.Append(txtItemCode.Text.Trim().ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator +
                                    ucLoactionSearch.Company.ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() + seperator +
                                    txtChannel.Text.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator + ucLoactionSearch.Area.ToUpper() + seperator +
                                    ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() + seperator + "No_Loc" + seperator + "" + seperator +
                                    txtItemRange.Text.ToUpper() + seperator + txtBrand.Text.ToUpper() + seperator + chkShowCostValue.Checked + seperator
                                    + txtCat4.Text.ToUpper() + seperator + txtCat5.Text.ToUpper() + seperator + txtBinCode.Text.ToUpper() + seperator
                                    );
                                break;
                            }
                            else
                            {
                                paramsText.Append(txtItemCode.Text.Trim().ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator +
                                    ucLoactionSearch.Company.ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() +
                                    seperator + ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator +
                                    ucLoactionSearch.Area.ToUpper() + seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() +
                                    seperator + "Loc" + seperator + txtChannelLocation.Text.ToUpper() + seperator + txtItemRange.Text.ToUpper() + seperator +
                                    txtBrand.Text.ToUpper() + seperator + chkShowCostValue.Checked + seperator + txtCat4.Text.ToUpper() + seperator + txtCat5.Text.ToUpper() + seperator + txtBinCode.Text.ToUpper() + seperator);
                                break;
                            }
                        }
                        if (selectLocation == "")
                        {
                            if (txtLocation.Text == "")
                            {
                                paramsText.Append(txtItemCode.Text.ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator +
                                    ucLoactionSearch.Company.ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() +
                                    seperator + ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator +
                                    ucLoactionSearch.Area.ToUpper() + seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() +
                                    seperator + "No_Loc" + seperator + ucLoactionSearch.ProfitCenter.ToUpper() + seperator + txtItemRange.Text.ToUpper() +
                                    seperator + txtBrand.Text.ToUpper() + seperator + chkShowCostValue.Checked + seperator + txtCat4.Text.ToUpper() + seperator + txtCat5.Text.ToUpper() + seperator + txtBinCode.Text.ToUpper() + seperator);
                                break;
                            }
                            else
                            {
                                paramsText.Append(txtItemCode.Text.Trim().ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator +
                                    ucLoactionSearch.Company.ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() +
                                    seperator + ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator +
                                    ucLoactionSearch.Area.ToUpper() + seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() +
                                    seperator + "Loc" + seperator + txtLocation.Text.ToUpper() + seperator + txtItemRange.Text.ToUpper() + seperator + txtBrand.Text.ToUpper()
                                    + seperator + chkShowCostValue.Checked + seperator + txtCat4.Text.ToUpper() + seperator + txtCat5.Text.ToUpper() + seperator + txtBinCode.Text.ToUpper() + seperator);
                                break;
                            }
                        }
                        if (selectLocation == "%")
                        {
                            //All locations in the Company

                            //paramsText.Append(txtItemCode.Text.ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch.Company.ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() + seperator + ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator + ucLoactionSearch.Area.ToUpper() + seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() + seperator + "Loc" + seperator + ucLoactionSearch.ProfitCenter.ToUpper() + seperator + txtItemRange.Text.ToUpper() + seperator);
                            paramsText.Append(txtItemCode.Text.Trim().ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator +
                                txtCompany.Text.Trim().ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() +
                                seperator + ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator +
                                ucLoactionSearch.Area.ToUpper() + seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() + seperator +
                                "No_Loc" + seperator + selectLocation.ToUpper() + seperator + txtItemRange.Text.ToUpper() + seperator + txtBrand.Text.ToUpper() + seperator
                                + chkShowCostValue.Checked + seperator + txtCat4.Text.ToUpper() + seperator + txtCat5.Text.ToUpper() + seperator + txtBinCode.Text.ToUpper() + seperator);
                            break;
                        }
                        //------------------------------------------------------------------------------------------------
                        if (!txtCompany.Enabled && ucLoactionSearch.ProfitCenter != string.Empty)
                        {
                            //selectLocation
                            //paramsText.Append(txtItemCode.Text.Trim().ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch.Company.ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() + seperator + ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator + ucLoactionSearch.Area.ToUpper() + seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() + seperator + "Loc" + seperator + ucLoactionSearch.ProfitCenter.ToUpper() + seperator + txtItemRange.Text.ToUpper() + seperator);
                            paramsText.Append(txtItemCode.Text.Trim().ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch.Company.ToUpper() + seperator + txtMainCategory.Text.ToUpper() +
                                seperator + txtSubCategory.Text.ToUpper() + seperator + ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() +
                                seperator + ucLoactionSearch.Area.ToUpper() + seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() +
                                seperator + "Loc" + seperator + selectLocation.ToUpper() + seperator + txtItemRange.Text.ToUpper() + seperator + txtBrand.Text.ToUpper() +
                                seperator + chkShowCostValue.Checked + seperator + txtCat4.Text.ToUpper() + seperator + txtCat5.Text.ToUpper() + seperator + txtBinCode.Text.ToUpper() + seperator);
                        }
                        //else if (!TextBoxLoc.Enabled)
                        //{
                        //    paramsText.Append(txtItemCode.Text.ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch.Company.ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() + seperator + ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator + ucLoactionSearch.Area.ToUpper() + seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() + seperator + "No_Loc" + seperator + "" + seperator + txtItemRange.Text.ToUpper() + seperator);

                        //}
                        else
                        {
                            // paramsText.Append(txtItemCode.Text + seperator + txtModel.Text.ToUpper() + seperator + status + seperator + txtCompany.Text.ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() + seperator + ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator + ucLoactionSearch.Area.ToUpper() + seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() + seperator + "Loc" + seperator + TextBoxLoc.Text.ToUpper() + seperator + txtItemRange.Text.ToUpper() + seperator);

                            paramsText.Append(txtItemCode.Text.Trim().ToUpper() + seperator + txtModel.Text.ToUpper() + seperator + status + seperator +
                                ucLoactionSearch.Company.ToUpper() + seperator + txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() + seperator +
                                ucLoactionSearch.Channel.ToUpper() + seperator + ucLoactionSearch.SubChannel.ToUpper() + seperator + ucLoactionSearch.Area.ToUpper() +
                                seperator + ucLoactionSearch.Regien.ToUpper() + seperator + ucLoactionSearch.Zone.ToUpper() + seperator + "Loc" + seperator +
                                txtLocation.Text.ToUpper() + seperator + txtItemRange.Text.ToUpper() + seperator + txtBrand.Text.ToUpper() + seperator + chkShowCostValue.Checked + seperator + txtCat4.Text.ToUpper() + seperator + txtCat5.Text.ToUpper() + seperator + txtBinCode.Text.ToUpper() + seperator);
                        }
                        break;
                    }
                //added by kelum : 2016-June-01
                case CommonUIDefiniton.SearchUserControlType.POorBLNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "A,F" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + 1 + seperator + "A,F" + seperator + "GRN" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EntryNo:
                    {
                        paramsText.Append("TO,AIR,LR" + seperator + "A,F" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocWiseBalancePOBL:
                    {
                        paramsText.Append(1 + seperator + "A,F" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocWiseBalanceDocGRN:
                    {
                        paramsText.Append("1" + seperator + "A,F" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocWiseBalanceEntry:
                    {
                        paramsText.Append("" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchBINCodes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtLocation.Text.ToUpper() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }


        protected void gdvResDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvResDet.PageIndex = e.NewPageIndex;
            DataTable _result = null;
            _result = (DataTable)Session["ItemRes"];
            gdvResDet.DataSource = _result;
            gdvResDet.DataBind();
            gdvResDet.PageIndex = 0;
            ItmResPopup.Show();

        }
        protected void dgvResultItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResultItem.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "Item")
                {
                    _result = (DataTable)Session["Item"];
                }
                if (lblSearchType.Text == "Item2")
                {
                    _result = (DataTable)Session["Item2"];
                }
                if (lblSearchType.Text == "Item3")
                {
                    _result = (DataTable)Session["Item3"];
                }
                if (lblSearchType.Text == "Channel2")
                {
                    _result = (DataTable)Session["Channel2"];
                }
                if (lblSearchType.Text == "locationall")
                {
                    _result = (DataTable)Session["locationall"];
                }
                //added by kelum : set prdcut details item code : 2016-May-24
                if (lblSearchType.Text == "ProdcutDetailsItem")
                {
                    _result = (DataTable)Session["ProdcutDetailsItem"];
                }

                //added by kelum : set balances PO or BL number : 2016-june-02

                if (lblSearchType.Text == "BalancesPOorBLNo")
                {
                    _result = (DataTable)Session["BalancesPOorBLNo"];
                }
                //
                //added by kelum : set balances Doc GRN number : 2016-june-02

                if (lblSearchType.Text == "BalancesDocGrnNo")
                {
                    _result = (DataTable)Session["BalancesDocGrnNo"];
                }

                //added by kelum : set balances entry number : 2016-june-02

                if (lblSearchType.Text == "BalancesEntryNo")
                {
                    _result = (DataTable)Session["BalancesEntryNo"];
                }

                if (lblSearchType.Text == "Model")
                {
                    _result = (DataTable)Session["Model"];
                }
                if (lblSearchType.Text == "Model2")
                {
                    _result = (DataTable)Session["Model2"];
                }
                if (lblSearchType.Text == "Brand")
                {
                    _result = (DataTable)Session["Brand"];
                }
                if (lblSearchType.Text == "Brand2")
                {
                    _result = (DataTable)Session["Brand2"];
                }
                if (lblSearchType.Text == "CAT_Main")
                {
                    _result = (DataTable)Session["CAT_Main"];
                }
                if (lblSearchType.Text == "CAT_Main2")
                {
                    _result = (DataTable)Session["CAT_Main2"];
                }
                if (lblSearchType.Text == "CAT_Sub1")
                {
                    _result = (DataTable)Session["CAT_Sub1"];
                }
                if (lblSearchType.Text == "CAT_Sub12")
                {
                    _result = (DataTable)Session["CAT_Sub12"];
                }
                if (lblSearchType.Text == "CAT_Sub2")
                {
                    _result = (DataTable)Session["CAT_Sub2"];
                }
                if (lblSearchType.Text == "CAT_Sub22")
                {
                    _result = (DataTable)Session["CAT_Sub22"];
                }
                if (lblSearchType.Text == "Tobond_bl")
                {
                    _result = (DataTable)Session["Tobond_bl"];
                }
                if (lblSearchType.Text == "tobond")
                {
                    _result = (DataTable)Session["tobond"];
                }
                if (lblSearchType.Text == "CAT_Sub3")
                {
                    _result = (DataTable)Session["CAT_Sub3"];
                }
                if (lblSearchType.Text == "CAT_Sub4")
                {
                    _result = (DataTable)Session["CAT_Sub4"];
                }
                if (lblSearchType.Text == "Company")
                {
                    _result = (DataTable)Session["Company"];
                }
                if (lblSearchType.Text == "UserLocation")
                {
                    _result = (DataTable)Session["UserLocation"];
                }
                if (lblSearchType.Text == "Loc_HIRC_Location")
                {
                    _result = (DataTable)Session["Loc_HIRC_Location"];
                }
                if (lblSearchType.Text == "Bin")
                {
                    _result = (DataTable)Session["Bin"];
                }
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;

                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                ItemPopup.Show();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void ImageSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _result = new DataTable();
                Session["Item"] = null;
                Session["Item2"] = null;
                Session["Model"] = null;
                Session["Model2"] = null;
                Session["Brand"] = null;
                Session["Brand2"] = null;
                Session["CAT_Main"] = null;
                Session["CAT_Main2"] = null;
                Session["CAT_Sub1"] = null;
                Session["CAT_Sub12"] = null;
                Session["CAT_Sub2"] = null;
                Session["CAT_Sub22"] = null;
                Session["Tobond_bl"] = null;
                Session["tobond"] = null;
                Session["CAT_Sub3"] = null;
                Session["CAT_Sub4"] = null;
                Session["Company"] = null;
                Session["InvTrcChnl"] = null;
                Session["Loc_HIRC_Location"] = null;
                Session["UserLocation"] = null;
                Session["Channel"] = null;

                // Added by Kelum
                Session["ProdcutDetailsItem"] = null;
                Session["ProdcutDetailsCompanyCode"] = null;
                //
                Session["Bin"] = null;

                if (lblSearchType.Text == "Item")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Item"] = _result;
                }
                //dilshan on 19/10/2017
                if (lblSearchType.Text == "Bin")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes);
                    _result = CHNLSVC.CommonSearch.SEARCH_BIN(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Bin"] = _result;
                }
                if (lblSearchType.Text == "Item2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item2);
                    _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Item2"] = _result;
                }
                if (lblSearchType.Text == "Item3")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item2);
                    _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Item3"] = _result;
                }
                if (lblSearchType.Text == "Channel2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                    _result = CHNLSVC.CommonSearch.GetChanalDetailsNew(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Channel2"] = _result;
                }
                if (lblSearchType.Text == "locationall")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["locationall"] = _result;
                }
                //added by kelum : set prdcut details item code : 2016-May-24
                if (lblSearchType.Text == "ProdcutDetailsItem")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ProdcutDetailsItem"] = _result;
                }
                //added by kelum : set prdcut details company code : 2016-May-24
                if (lblSearchType.Text == "ProdcutDetailsCompanyCode")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, null, null);
                    Session["ProdcutDetailsCompanyCode"] = _result;
                }
                //added by kelum : set balances by PO or BL number : 2016-June-02
                if (lblSearchType.Text == "BalancesPOorBLNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POorBLNo);
                    _result = CHNLSVC.General.Get_PoBlNumber(para, null, null);
                    Session["BalancesPOorBLNo"] = _result;
                }
                //added by kelum : set balances by doc GRN : 2016-June-02
                if (lblSearchType.Text == "BalancesDocGrnNo")
                {
                    DateTime? _docdtfrom = null;
                    DateTime? _docdtto = null;

                    if ((!string.IsNullOrEmpty(txtDWfrom.Text.Trim())) & (!string.IsNullOrEmpty(txtDWto.Text.Trim())))
                    {
                        _docdtfrom = Convert.ToDateTime(txtDWfrom.Text.Trim()).Date;
                        _docdtto = Convert.ToDateTime(txtDWto.Text.Trim()).Date;
                    }

                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                    _result = CHNLSVC.General.Get_DocGrnNumber(para, _docdtfrom, _docdtto, null, null);
                    Session["BalancesDocGrnNo"] = _result;
                }
                //added by kelum : set balances by entry number : 2016-June-02
                if (lblSearchType.Text == "BalancesEntryNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    _result = CHNLSVC.General.Get_EntryNumber(para, null, null);
                    Session["BalancesEntryNo"] = _result;
                }


                else if (lblSearchType.Text == "Model")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    _result = CHNLSVC.CommonSearch.GetAllModels(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Model"] = _result;
                }
                else if (lblSearchType.Text == "Model2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    _result = CHNLSVC.CommonSearch.GetAllModels(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Model2"] = _result;
                }
                else if (lblSearchType.Text == "Brand")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    _result = CHNLSVC.CommonSearch.GetItemBrands(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Brand"] = _result;
                }
                else if (lblSearchType.Text == "Brand2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    _result = CHNLSVC.CommonSearch.GetItemBrands(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Brand2"] = _result;
                }
                else if (lblSearchType.Text == "CAT_Main")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Main"] = _result;
                }
                else if (lblSearchType.Text == "CAT_Main2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Main2"] = _result;
                }
                else if (lblSearchType.Text == "CAT_Sub1")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    string valuetest = txtSearchbyword.Text;
                    valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                    _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                    Session["CAT_Sub1"] = _result;
                }
                else if (lblSearchType.Text == "CAT_Sub12")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    string valuetest = txtSearchbyword.Text;
                    valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                    _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                    Session["CAT_Sub12"] = _result;
                }
                else if (lblSearchType.Text == "CAT_Sub2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    string valuetest = txtSearchbyword.Text;
                    valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                    _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                    Session["CAT_Sub2"] = _result;
                }
                else if (lblSearchType.Text == "CAT_Sub22")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    string valuetest = txtSearchbyword.Text;
                    valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                    _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                    Session["CAT_Sub22"] = _result;
                }
                else if (lblSearchType.Text == "Tobond_bl")
                {

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                    _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                    if (_result.Rows.Count > 0)
                    {
                        DataView dv = _result.DefaultView;
                        dv.Sort = "ib_doc_rec_dt ASC";
                        _result = dv.ToTable();
                    }

                    _result.Columns.Remove("ib_doc_rec_dt");
                    Session["Tobond_bl"] = _result;
                }
                else if (lblSearchType.Text == "tobond")
                {

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                    _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                    if (_result.Rows.Count > 0)
                    {
                        DataView dv = _result.DefaultView;
                        dv.Sort = "ib_doc_rec_dt ASC";
                        _result = dv.ToTable();
                    }

                    _result.Columns.Remove("ib_doc_rec_dt");
                    Session["tobond"] = _result;
                }
                if (lblSearchType.Text == "CAT_Sub3")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                    _result = CHNLSVC.General.GetItemSubCat4(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub3"] = _result;
                }
                if (lblSearchType.Text == "CAT_Sub4")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                    _result = CHNLSVC.General.GetItemSubCat5(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub4"] = _result;
                }
                else if (lblSearchType.Text == "Company")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Company"] = _result;
                }
                else if (lblSearchType.Text == "InvTrcChnl")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvTrcChnl);
                    _result = CHNLSVC.CommonSearch.GetInventoryTrackeChannel(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["InvTrcChnl"] = _result;
                }
                else if (lblSearchType.Text == "Loc_HIRC_Location")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Loc_HIRC_Location"] = _result;
                }
                else if (lblSearchType.Text == "UserLocation")
                {
                    string para = "";
                    //_CommonSearch.ReturnIndex = 0;

                    _result = new DataTable();
                    //----------------------------------------------------------------------------
                    Boolean allow_WHAREHOUSE = CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INVAC");
                    DataTable _result2 = null;
                    if (allow_WHAREHOUSE == true)
                    {
                        para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                        _result2 = CHNLSVC.Inventory.GetLocationByType(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        _result.Merge(_result2);
                    }

                    para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    _result.Merge(CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text)); dgvResultItem.DataSource = _result;
                    Session["UserLocation"] = _result;

                }
                else if (lblSearchType.Text == "Channel")
                {
                    string para = "";
                    //_CommonSearch.ReturnIndex = 0;

                    _result = new DataTable();
                    //----------------------------------------------------------------------------
                    Boolean allow_WHAREHOUSE = CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INVAC");
                    DataTable _result2 = null;
                    if (allow_WHAREHOUSE == true)
                    {
                        para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                        _result2 = CHNLSVC.Inventory.GetLocationByType(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        _result.Merge(_result2);
                    }

                    para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    _result.Merge(CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text)); dgvResultItem.DataSource = _result;
                    Session["Channel"] = _result;
                }
                dgvResultItem.DataSource = null;
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResultItem.DataBind();
                ItemPopup.Show();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void dgvResultItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["SELECTEDITEM"] = null;
                Session["SELECTEDMODEL"] = null;
                Session["SELECTEDBRAND"] = null;
                Session["SELECTEDCAT_MAIN"] = null;
                Session["SELECTEDCAT_SUB1"] = null;
                Session["SELECTEDCAT_SUB2"] = null;
                Session["SELECTEDCOMPANY"] = null;
                Session["SELECTEDUSERLOCATION"] = null;
                Session["SELECTEDCHANNELLOCATION"] = null;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript",
                    "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvResultItem.SelectedRow.Cells[1].Text;

                //added by kelum : set prdcut details item code : 2016-May-20
                if (lblSearchType.Text == "ProdcutDetailsItem")
                {
                    txtPDitemcode.Text = code;
                    txtPDitemcode_TextChanged(null, null);
                }

                //added by kelum : set prdcut details CompanyCode : 2016-May-20
                if (lblSearchType.Text == "ProdcutDetailsCompanyCode")
                {
                    txtCompanySWTD.Text = code;
                    txtCompanySWTD_TextChanged(null, null);

                    LoadTaxbyCompany();
                }

                //added by kelum : set blanaces PO or BL number : 2016-June-02
                if (lblSearchType.Text == "BalancesPOorBLNo")
                {
                    txtPOorBLNumber.Text = code;
                    txtPOorBLNumber.Focus();
                }
                //added by kelum : set blanaces DOC GRN number : 2016-June-02
                if (lblSearchType.Text == "BalancesDocGrnNo")
                {
                    txtDocGRNumber.Text = code;
                    txtDocGRNumber.Focus();
                }

                //added by kelum : set blanaces Entry number : 2016-June-02
                if (lblSearchType.Text == "BalancesEntryNo")
                {
                    txtEntryNumber.Text = code;
                    txtEntryNumber.Focus();
                }

                if (lblSearchType.Text == "Item")
                {
                    Session["SELECTEDITEMCODE"] = code;
                    txtItemCode.Text = code;
                    txtItemCode.Focus();
                }
                if (lblSearchType.Text == "Item2")
                {
                    txtitemcode2.Text = code;
                    txtitemcode2.Focus();
                }
                if (lblSearchType.Text == "Item3")
                {
                    txtItem3.Text = code;
                    txtItem3.Focus();
                }
                if (lblSearchType.Text == "Channel2")
                {
                    txtchannelAlloc.Text = code;
                    txtchannelAlloc.Focus();
                }
                if (lblSearchType.Text == "locationall")
                {
                    txtLocAlloc.Text = code;
                    txtLocAlloc.Focus();
                }
                if (lblSearchType.Text == "Model")
                {
                    Session["SELECTEDMODEL"] = code;
                    txtModel.Text = code;
                    txtModel.Focus();
                }
                if (lblSearchType.Text == "Model2")
                {
                    txtmodel2.Text = code;
                    txtmodel2.Focus();
                }
                if (lblSearchType.Text == "Brand")
                {
                    Session["SELECTEDBRAND"] = code;
                    txtBrand.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "Brand2")
                {
                    txtbrand2.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "CAT_Main")
                {
                    Session["SELECTEDCAT_MAIN"] = code;
                    txtMainCategory.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "CAT_Main2")
                {
                    txtcat12.Text = code;
                }
                if (lblSearchType.Text == "CAT_Sub1")
                {
                    Session["SELECTEDCAT_SUB1"] = code;
                    txtSubCategory.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "CAT_Sub12")
                {
                    txtcat22.Text = code;
                }
                if (lblSearchType.Text == "CAT_Sub2")
                {
                    txtItemRange.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "CAT_Sub22")
                {
                    txtcat32.Text = code;
                }
                if (lblSearchType.Text == "Tobond_bl")
                {
                    txtblno2.Text = dgvResultItem.SelectedRow.Cells[2].Text;
                }
                if (lblSearchType.Text == "tobond")
                {
                    txttobond2.Text = dgvResultItem.SelectedRow.Cells[3].Text;
                }
                else if (lblSearchType.Text == "CAT_Sub3")
                {
                    txtCat4.Text = code;
                }
                else if (lblSearchType.Text == "CAT_Sub4")
                {
                    txtCat5.Text = code;
                }
                if (lblSearchType.Text == "Company")
                {
                    Session["SELECTEDCOMPANY"] = code;
                    txtCompany.Text = code;
                    txtItemCode.Text = "";
                    txtModel.Text = "";
                    txtBrand.Text = "";
                    ddlItemStatus.SelectedIndex = 0;
                    txtMainCategory.Text = "";
                    txtSubCategory.Text = "";
                    txtItemRange.Text = "";
                    txtLocation.Text = "";

                    txtChannel.Text = "";
                    txtChannelLocation.Text = "";

                    txtChannel.Enabled = false;
                    txtChannelLocation.Enabled = false;

                    lBtnChannel.Enabled = false;
                    lBtnChanLocation.Enabled = false;
                    txtLocation.Text = "";
                    chkAdvancedSearch.Checked = false;
                    ucLoactionSearch.Company = code;
                    ucLoactionSearch.Channel = "";
                    ucLoactionSearch.SubChannel = "";
                    ucLoactionSearch.Area = "";
                    ucLoactionSearch.Regien = "";
                    ucLoactionSearch.Zone = "";
                    ucLoactionSearch.ProfitCenter = "";
                    dgvItemDetails.DataSource = null;
                    dgvItemDetails.DataBind();
                    grvAllLocation.DataSource = null;
                    grvAllLocation.DataBind();
                }
                if (lblSearchType.Text == "UserLocation")
                {
                    Session["SELECTEDUSERLOCATION"] = code;
                    txtLocation.Text = code;
                    AllLocationBind();
                }
                if (lblSearchType.Text == "InvTrcChnl")
                {
                    Session["SELECTEDCHANNEL"] = code;
                    txtChannel.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "Loc_HIRC_Location")
                {
                    Session["SELECTEDCHANNELLOCATION"] = code;
                    txtChannelLocation.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "Bin")
                {
                    //Session["Bin"] = code;
                    txtBinCode.Text = code;
                    //btnLoad_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            //    if (lblSearchType.Text == "Item")
            //    {
            //        string code = (string)Session["SELECTEDITEMCODE"];
            //        if (!string.IsNullOrEmpty(code))
            //        {
            //            txtItemCode.Text = code;
            //        }
            //    }
            //    else if (lblSearchType.Text == "Model")
            //    {
            //        string code = (string)Session["SELECTEDMODEL"];
            //        if (!string.IsNullOrEmpty(code))
            //        {
            //            txtModel.Text = code;
            //        }
            //    }
            //    else if (lblSearchType.Text == "Brand")
            //    {
            //        string code = (string)Session["SELECTEDBRAND"];
            //        if (!string.IsNullOrEmpty(code))
            //        {
            //            txtBrand.Text = code;
            //        }
            //    }
            //    else if (lblSearchType.Text == "CAT_Main")
            //    {
            //        string code = (string)Session["SELECTEDCAT_MAIN"];
            //        if (!string.IsNullOrEmpty(code))
            //        {
            //            txtMainCategory.Text = code;
            //        }
            //    }
            //    else if (lblSearchType.Text == "CAT_Sub1")
            //    {
            //        string code = (string)Session["SELECTEDCAT_SUB1"];
            //        if (!string.IsNullOrEmpty(code))
            //        {
            //            txtSubCategory.Text = code;
            //        }
            //    }
            //    else if (lblSearchType.Text == "CAT_Sub2")
            //    {
            //        string code = (string)Session["SELECTEDCAT_SUB2"];
            //        if (!string.IsNullOrEmpty(code))
            //        {
            //            txtItemRange.Text = code;
            //        }
            //    }
            //    else if (lblSearchType.Text == "Company")
            //    {
            //        string code = (string)Session["SELECTEDCOMPANY"];
            //        if (!string.IsNullOrEmpty(code))
            //        {
            //            txtCompany.Text = code;
            //        }
            //    }
            //    else if (lblSearchType.Text == "UserLocation")
            //    {
            //        string code = (string)Session["SELECTEDUSERLOCATION"];
            //        if (!string.IsNullOrEmpty(code))
            //        {
            //            txtLocation.Text = code;
            //           //AllAlocationBind();
            //        }
            //    }
            //    else if (lblSearchType.Text == "Channel")
            //    {
            //        string code = (string)Session["SELECTEDCHANNEL"];
            //        if (!string.IsNullOrEmpty(code))
            //        {
            //            txtLocation.Text = code;

            //        }
            //    }
            //    Session["SELECTEDITEM"] = null;
            //    Session["SELECTEDMODEL"] = null;
            //    Session["SELECTEDBRAND"] = null;
            //    Session["SELECTEDCAT_MAIN"] = null;
            //    Session["SELECTEDCAT_SUB1"] = null;
            //    Session["SELECTEDCAT_SUB2"] = null;
            //    Session["SELECTEDCOMPANY"] = null;
            //   // Session["SELECTEDUSERLOCATION"] = null;
            //    Session["SELECTEDITEMCODE"] = null;
            //    Session["SELECTEDCHANNEL"] = null;

        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemCode.Text))
                {
                    //string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    //DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, "ITEM", txtItemCode.Text.Trim().ToUpper());
                    //bool _valiItm = false; 
                    //foreach (DataRow item in _result.Rows)
                    //{
                    //    if (item["ITEM"].ToString()==txtItemCode.Text.Trim().ToUpper())
                    //    {
                    //        _valiItm = true;
                    //        break;
                    //    }
                    //}
                    //if (!_valiItm)
                    //{
                    //    txtItemCode.Text = "";
                    //    DispMsg("Please select the valid item code ! ");
                    //}
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
            //GetItemSearchData
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

        protected void lBtnItemCode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Item";
                Session["Item"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Item"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                Label2.Text = "Item";
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        //by dilshan on 19/10/2017
        protected void lBtnBinCode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Bin";
                Session["Bin"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes);
                //DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_BIN(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Bin"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                Label2.Text = "Bin";
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void lBtnModel_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Model";
                Session["Model"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Model"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "Model";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lBtnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Brand";
                Session["Brand"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Brand"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "Brand";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lBtnMainCategory_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Main";
                Session["CAT_Main"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Main"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "CAT_Main";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lBtnSubCategory_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub1";
                Session["CAT_Sub1"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub1"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "CAT_Sub1";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lBtnItemRange_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub2";
                Session["CAT_Sub2"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub2"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "CAT_Sub2";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lBtnCompany_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Company";
                Session["Company"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Company"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "Company";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lBtnLocation_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "UserLocation";
                Session["UserLocation"] = null;
                string para = "";
                //_CommonSearch.ReturnIndex = 0;

                DataTable _result = new DataTable();
                //----------------------------------------------------------------------------
                Boolean allow_WHAREHOUSE = CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INVAC");
                DataTable _result2 = null;
                if (allow_WHAREHOUSE == true)
                {
                    para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                    _result2 = CHNLSVC.Inventory.GetLocationByType(para, null, null);
                    _result.Merge(_result2);
                }

                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result.Merge(CHNLSVC.CommonSearch.GetUserLocationSearchData(para, null, null));
                //----------------------------------------------------------------------------
                DataTable _newDt = new DataTable();
                _newDt.Columns.Add("Code");
                _newDt.Columns.Add("Description");
                foreach (DataRow dr in _result.Rows)
                {
                    DataRow _newDataRow=_newDt.NewRow();
                    _newDataRow["Code"] = dr["Code"].ToString();
                    _newDataRow["Description"] = dr["Description"].ToString();
                    String author = dr["Code"].ToString();
                    bool contains = _newDt.AsEnumerable().Any(row => author == row.Field<String>("Code"));
                    if (!contains)
                    {
                        _newDt.Rows.Add(_newDataRow);
                    }
                }
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _newDt;
                    Session["UserLocation"] = _newDt;
                    BindUCtrlDDLData(_newDt);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "UserLocation";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }

            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void AllLocationBind()
        {
            try
            {
                if (txtLocation.Text.Trim() == "")
                {
                    grvAllLocation.DataSource = null;
                    grvAllLocation.DataBind();
                    return;
                }
                try
                {
                    divDanger.Visible = false;
                    List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(Session["UserCompanyCode"].ToString(), txtLocation.Text.ToUpper());
                    divDanger.Visible = false;
                    if (loc_list == null)
                    {
                        lblAlertDanger.Text = "Invalid Location Code";
                        divDanger.Visible = true;
                        return;
                    }
                    else if (loc_list.Count < 1)
                    {
                        MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtLocation.Text.ToUpper());
                        loc_list.Add(loc_);
                    }

                    grvAllLocation.DataSource = null;
                    if (loc_list.Count > 0)
                    {
                        grvAllLocation.DataSource = loc_list;
                    }

                    grvAllLocation.DataBind();

                    foreach (GridViewRow row in grvAllLocation.Rows)
                    {
                        CheckBox chkcheck = (CheckBox)row.FindControl("chkLocationRow");
                        chkcheck.Checked = true;
                    }
                }
                catch (Exception ex)
                {
                }
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "CheckAllLocation();", true);
            }
            catch (Exception ex)
            {
                lblAlertDanger.Text = "Error Occurred while processing...\n" + ex.Message;
                divDanger.Visible = true;
            }
        }
        protected void chkChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChannel.Checked)
            {
                txtLocation.Enabled = false;
                txtLocation.Text = "";

                txtChannel.Enabled = true;
                txtChannelLocation.Enabled = true;

                lBtnChannel.Enabled = true;
                lBtnChanLocation.Enabled = true;
                grvAllLocation.DataSource = null;
                grvAllLocation.DataBind();
            }
            else
            {
                txtLocation.Enabled = true;
                txtCompany.Text = Session["UserCompanyCode"].ToString();
                ucLoactionSearch.Company = txtCompany.Text;
                txtLocation.Text = Session["UserDefLoca"].ToString();
                txtChannel.Text = "";
                txtChannelLocation.Text = "";

                txtChannel.Enabled = false;
                txtChannelLocation.Enabled = false;

                lBtnChannel.Enabled = false;
                lBtnChanLocation.Enabled = false;
                FillGrdAllAllocation();
            }


        }

        protected void lbtnView_Click(object sender, EventArgs e)
        {
            //Modified by Kelum : 2016-May-20 : View buttton specify for tab value

            string Tabvalue = hdfTabIndex.Value;

            if (Tabvalue == "#InventoryTracker")
            {
                DataLoad();
            }

            else if (Tabvalue == "#ProductDetails")
            {
                LoadProductDetails();
            }

            else if (Tabvalue == "#DocumentWiseBal")
            {
                LoadBalancesbyPurchaseOrder();
            }
            else if (Tabvalue == "#ItemAllocation")
            {
                LoadAllocationDetails();
            }
        }

        private void DataLoad()
        {
            try
            {
                string err = "";
                dgvItemDetails.DataSource = null;
                dgvItemDetails.DataBind();
                divDanger.Visible = false;
                if (grvAllLocation.Rows.Count <= 0 && txtLocation.Text == "" && txtChannel.Text == "" &&
                    //panel_advanceSearch.Enabled == false &&
                    chkAllLocation.Checked == false)
                {
                    lblAlertDanger.Text = "Please enter the search criteria for search";
                    divDanger.Visible = true;
                    return;
                }

                // panel_serialView.Visible = false;

                //  GridAllLocations.EndEdit();
                // lblNote.Text = "";
                txtTotalQty.Text = "";
                Boolean allow_SCM = true; //CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "SCMI");
                Boolean allow_SCM2 = true; //CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "SCM2I");

                if (txtLocation.Text.Trim() == Session["UserDefLoca"].ToString())
                {
                    // allow_SCM = true;
                    allow_SCM2 = true;
                }
                if (allow_SCM == true && allow_SCM2 == true)
                {
                    //lblNote.Text = "Enquired from both SCM and SCM2 tables.";
                    //  lblNote.Text = "Enquired from Allowed locations and/or Channels.";
                }
                else if (allow_SCM == true && allow_SCM2 == false)
                {
                    // lblNote.Text = "Enquired from SCM tables only."; //
                    //   lblNote.Text = "Enquired from allowed locations and warehouses only";
                }
                else if (allow_SCM == false && allow_SCM2 == true)
                {
                    //lblNote.Text = "Enquired from SCM2 tables only";//Enquired from allowed locations only
                    //  lblNote.Text = "Enquired from allowed locations only.";
                }
                else
                {
                    //lblNote.Text = "Permission not granted for either SCM or SCM2.";
                    // lblNote.Text = "Permission not granted.";
                }
                dgvItemDetails.DataSource = null;
                dgvItemDetails.DataBind();

                DataTable tbl_all = new DataTable();
                DataTable tbl = new DataTable();
                GridViewDataBind(tbl_all);
                string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                //if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INV2"))
                if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INVAC"))
                {
                    divInfo.Visible = false;
                    if (chkAdvancedSearch.Checked)
                    {
                        if (hdfAlertInfo.Value == "0")
                        {
                            lblDilogResult.Text = "Do you want to search item details in advance?";
                            PopupDilogResult.Show();
                            return;
                        }
                        else if (hdfAlertInfo.Value == "1")
                        {
                            hdfAlertInfo.Value = "0";
                            selectLocation = "";
                            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                            tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchDataWEB(para, allow_SCM, allow_SCM2, out err);
                            if (tbl != null)
                            {
                                if (tbl.Rows.Count > 0)
                                {
                                    tbl_all.Merge(tbl);
                                }
                            }
                            GridViewDataBind(tbl_all);
                            divDanger.Visible = false;
                            if (txtTotalQty.Text == "0.00")
                            {
                                lblAlertDanger.Text = "No balance available in this location ! ";
                                divDanger.Visible = true;

                            }
                            else { lblGrandTotal.Visible = true; txtTotalQty.Visible = true; }
                            return;
                        }
                        else
                        {
                            hdfAlertInfo.Value = "0";
                            chkAdvancedSearch.Checked = false;
                            return;
                        }
                    }
                }
                selectLocation = (string)Session["SELECTLOCATION"];
                if (selectLocation == "%")
                {
                    if (txtItemCode.Text.Trim() == "")
                    {
                        if (hdfAlertInfo.Value == "0")
                        {
                            lblDilogResult.Text = "Do you want to search all items in all locations?(It might take a long time)";
                            PopupDilogResult.Show();
                            return;
                        }
                        if (hdfAlertInfo.Value == "2")
                        {
                            hdfAlertInfo.Value = "0";
                            return;
                        }
                        hdfAlertInfo.Value = "0";
                    }
                    try
                    {
                        divDanger.Visible = false;
                        string para =
                            SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                        tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchDataWEB(para, allow_SCM, allow_SCM2, out err);
                        if (tbl != null)
                        {
                            if (tbl.Rows.Count > 0)
                            {
                                tbl_all.Merge(tbl);
                            }
                        }
                        GridViewDataBind(tbl_all);
                        divSuccess.Visible = false;
                        if (txtTotalQty.Text == "0.00")
                        {
                            lblAlertDanger.Text = "No balance available in this location ! ";
                            divDanger.Visible = true;
                            return;
                        }
                        ALLOW_WHEARHOUSE_QTY();
                        Session["SELECTLOCATION"] = "";
                        return;
                    }
                    catch (Exception ex)
                    {
                        lblAlertDanger.Text =
                                "Error occuered! \nAdvice: This might cause because of huge data amount. Please enter a single item code and try. " + err;
                        divDanger.Visible = true;
                        return;
                    }
                }
                //ADDED 2013/08/27 CHANNEL SEARCH
                if (chkChannel.Checked && txtChannel.Text != "")
                {
                    if (txtItemCode.Text.Trim() == "")
                    {
                        if (hdfAlertInfo.Value == "0")
                        {
                            lblDilogResult.Text = "Do you want to search all items in the selected channel?(It might take a long time)";
                            PopupDilogResult.Show();
                            return;
                        }
                        if (hdfAlertInfo.Value == "2")
                        {
                            hdfAlertInfo.Value = "0";
                            return;
                        }
                    }
                    RoleId = (List<int>)Session["DATAROLEID"];
                    DataTable _dt = CHNLSVC.Inventory.GetInventoryTrackerChannel(Session["UserCompanyCode"].ToString(), txtChannel.Text, RoleId);
                    divDanger.Visible = false;
                    if (_dt == null || _dt.Rows.Count <= 0)
                    {
                        lblAlertDanger.Text =
                               "Entered Channel is not allow to user or Invalid Channel Code ";
                        divDanger.Visible = true;
                        return;
                    }
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                    tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchDataWEB(para, allow_SCM, allow_SCM2, out err);
                    if (tbl != null)
                    {
                        if (tbl.Rows.Count > 0)
                        {
                            tbl_all.Merge(tbl);
                        }
                    }
                    GridViewDataBind(tbl_all);
                    if (txtTotalQty.Text == "0.00")
                    {
                        //MessageBox.Show("No balance available in this location !");
                        lblAlertDanger.Text = "No balance available in this location ! ";
                        divDanger.Visible = true;
                        return;
                    }
                    else
                    {
                        lblGrandTotal.Visible = true;
                        txtTotalQty.Visible = true;
                    }
                    if (Convert.ToInt32(_dt.Rows[0]["SSRT_WO_QTY"]) == 1)
                    {
                        foreach (GridViewRow row in dgvItemDetails.Rows)
                        {
                            row.Cells[6].Text = string.Empty;
                            row.Cells[6].BackColor = Color.Green;
                        }
                        lblGrandTotal.Visible = true;
                        txtTotalQty.Visible = true;
                    }

                    // ALLOW_WHEARHOUSE_QTY();

                    return;
                }

                //else
                //{
                //selectLocation = "%";
                try
                {
                    divDanger.Visible = false;
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                    tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchDataWEB(para, allow_SCM, allow_SCM2, out err);
                    if (tbl != null)
                    {
                        if (tbl.Rows.Count > 0)
                        {
                            tbl_all.Merge(tbl);
                        }
                    }
                    GridViewDataBind(tbl_all);
                    divDanger.Visible = false;
                    if (txtTotalQty.Text == "0.00")
                    {
                        //MessageBox.Show("No balance available in this location !");
                        lblAlertDanger.Text = "No balance available in this location ! ";
                        divDanger.Visible = true;
                        return;
                    }
                    ALLOW_WHEARHOUSE_QTY();
                    return;
                }
                catch (Exception EX)
                {
                    lblAlertDanger.Text =
                               "Error occuered! \nAdvice: This might cause because of huge data amount. Please enter a single item code and try. " + err;
                    divDanger.Visible = true;
                    return;
                }

                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                ////DataTable dataSource = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData(_CommonSearch.SearchParams);
                //DataTable dataSource = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2);

                //------------------------------------------------**-----------------------------------------------------------------------

                //GridViewitemDetails.Refresh();
                //if (GridAllLocations.DataSource == null)
                //{
                //    this.TextBoxLoc_Leave(sender, e);
                //}
                //else if (GridAllLocations.Rows.Count == 1)
                //{
                //    this.TextBoxLoc_Leave(sender, e);
                //}
                if (dgvItemDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow row in dgvItemDetails.Rows)
                    {
                        CheckBox chk = row.Cells[0].Controls[0] as CheckBox;
                        if (chk != null && chk.Checked)
                        {
                            divDanger.Visible = false;
                            if (row.Cells[1].Text == null)
                            {
                                lblAlertDanger.Text = "Enter Location";
                                divDanger.Visible = true;
                                return;
                            }
                            string loc_ = row.Cells[1].Text;
                            selectLocation = loc_;
                            Session["SELECTLOCATION"] = loc_;
                            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                            tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchDataWEB(para, allow_SCM, allow_SCM2, out err);
                            if (tbl != null)
                            {
                                if (tbl.Rows.Count > 0)
                                {
                                    tbl_all.Merge(tbl);
                                }
                            }
                        }
                    }
                    GridViewDataBind(tbl_all);
                    divDanger.Visible = false;
                    if (txtTotalQty.Text == "0.00")
                    {
                        lblAlertDanger.Text = "No balance available in this location ! ";
                        divDanger.Visible = true;
                        return;
                    }
                }

                ALLOW_WHEARHOUSE_QTY();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void DataLoad2()
        {
            try
            {
                string err = "";
                divDanger.Visible = false;
                if (grvAllLocation.Rows.Count <= 0 && txtLocation.Text == "" && txtChannel.Text == "" &&
                    //panel_advanceSearch.Visible == false && 
                    chkAllLocation.Checked == false)
                {
                    lblAlertDanger.Text = "Please enter the search criteria for search";
                    divDanger.Visible = true;
                    return;
                }

                // panel_serialView.Visible = false;

                //  GridAllLocations.EndEdit();
                // lblNote.Text = "";
                txtTotalQty.Text = "";
                Boolean allow_SCM = true; //CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "SCMI");
                Boolean allow_SCM2 = true; //CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "SCM2I");

                if (txtLocation.Text.Trim() == Session["UserDefLoca"].ToString())
                {
                    // allow_SCM = true;
                    allow_SCM2 = true;
                }
                if (allow_SCM == true && allow_SCM2 == true)
                {
                    //lblNote.Text = "Enquired from both SCM and SCM2 tables.";
                    //  lblNote.Text = "Enquired from Allowed locations and/or Channels.";
                }
                else if (allow_SCM == true && allow_SCM2 == false)
                {
                    // lblNote.Text = "Enquired from SCM tables only."; //
                    //   lblNote.Text = "Enquired from allowed locations and warehouses only";
                }
                else if (allow_SCM == false && allow_SCM2 == true)
                {
                    //lblNote.Text = "Enquired from SCM2 tables only";//Enquired from allowed locations only
                    //  lblNote.Text = "Enquired from allowed locations only.";
                }
                else
                {
                    //lblNote.Text = "Permission not granted for either SCM or SCM2.";
                    // lblNote.Text = "Permission not granted.";
                }
                //   dgvItemDetails.DataSource = null;
                // dgvItemDetails.DataBind();

                DataTable tbl_all = new DataTable();
                DataTable tbl = new DataTable();
                GridViewDataBind2(tbl_all);
                string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                //if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INV2"))
                if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INVAC"))
                {
                    divInfo.Visible = false;
                    if (chkAdvancedSearch.Checked)
                    {
                        if (hdfAlertInfo.Value == "0")
                        {
                            lblDilogResult.Text = "Do you want to search item details in advance?";
                            PopupDilogResult.Show();
                            return;
                        }
                        else if (hdfAlertInfo.Value == "1")
                        {

                            hdfAlertInfo.Value = "0";
                            selectLocation = "";
                            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                            tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchDataWEB(para, allow_SCM, allow_SCM2, out err);
                            if (tbl != null)
                            {
                                if (tbl.Rows.Count > 0)
                                {
                                    tbl_all.Merge(tbl);
                                }
                            }
                            GridViewDataBind(tbl_all);
                            divDanger.Visible = false;
                            if (txtTotalQty.Text == "0.00")
                            {
                                lblAlertDanger.Text = "No balance available in this location ! ";
                                divDanger.Visible = true;
                                return;
                            }
                        }
                        else
                        {
                            chkAdvancedSearch.Checked = false;
                            return;
                        }
                    }
                }
                if (selectLocation == "%")
                {
                    if (txtItemCode.Text.Trim() == "")
                    {
                        if (hdfAlertInfo.Value == "0")
                        {
                            lblDilogResult.Text = "Do you want to search all items in all locations?(It might take a long time)";
                            PopupDilogResult.Show();
                            return;
                        }
                        if (hdfAlertInfo.Value == "2")
                        {
                            hdfAlertInfo.Value = "0";
                            return;
                        }
                    }
                    //else
                    //{
                    //selectLocation = "%";
                    try
                    {
                        divDanger.Visible = false;
                        string para =
                            SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                        tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchDataWEB(para, allow_SCM, allow_SCM2, out err);
                        if (tbl != null)
                        {
                            if (tbl.Rows.Count > 0)
                            {
                                tbl_all.Merge(tbl);
                            }
                        }
                        GridViewDataBind(tbl_all);
                        divDanger.Visible = false;
                        if (txtTotalQty.Text == "0.00")
                        {
                            lblAlertDanger.Text = "No balance available in this location ! ";
                            divDanger.Visible = true;
                            return;
                        }
                        ALLOW_WHEARHOUSE_QTY();
                        return;
                    }
                    catch (Exception ex)
                    {
                        lblAlertDanger.Text =
                            "Error occuered! \nAdvice: This might cause because of huge data amount. " +
                            "Please enter a single item code and try. ";
                        divDanger.Visible = true;
                        return;
                    }
                }
                //ADDED 2013/08/27 CHANNEL SEARCH
                if (chkChannel.Checked && txtChannel.Text != "")
                {
                    if (txtItemCode.Text.Trim() == "")
                    {
                        divInfo.Visible = false;
                        if (hdfAlertInfo.Value == "0")
                        {
                            divInfo.Visible = false;
                            divInfo.Visible = true;
                            lblAlertInfo.Text =
                                "Do you want to search all items in the selected channel?(It might take a long time)";
                            return;
                        }
                        if (hdfAlertInfo.Value == "2")
                        {
                            return;
                        }
                    }

                    DataTable _dt = CHNLSVC.Inventory.GetInventoryTrackerChannel(Session["UserCompanyCode"].ToString(), txtChannel.Text, RoleId);
                    divDanger.Visible = false;
                    if (_dt == null || _dt.Rows.Count <= 0)
                    {
                        lblAlertDanger.Text =
                               "Entered Channel is not allow to user or Invalid Channel Code ";
                        divDanger.Visible = true;
                        return;
                    }
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                    tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchDataWEB(para, allow_SCM, allow_SCM2, out err);
                    if (tbl != null)
                    {
                        if (tbl.Rows.Count > 0)
                        {
                            tbl_all.Merge(tbl);
                        }
                    }
                    GridViewDataBind(tbl_all);
                    if (txtTotalQty.Text == "0.00")
                    {
                        //MessageBox.Show("No balance available in this location !");
                        lblAlertDanger.Text = "No balance available in this location ! ";
                        divDanger.Visible = true;
                        return;
                    }
                    if (Convert.ToInt32(_dt.Rows[0]["SSRT_WO_QTY"]) == 1)
                    {
                        foreach (GridViewRow row in dgvItemDetails.Rows)
                        {
                            row.Cells[6].Text = "";
                            row.Cells[6].ForeColor = Color.Green;

                            lblGrandTotal.Visible = true;
                            txtTotalQty.Visible = true;
                        }
                    }

                    // ALLOW_WHEARHOUSE_QTY();

                    return;
                }

                //else
                //{
                //selectLocation = "%";
                try
                {
                    divDanger.Visible = false;
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                    tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchDataWEB(para, allow_SCM, allow_SCM2, out err);
                    if (tbl != null)
                    {
                        if (tbl.Rows.Count > 0)
                        {
                            tbl_all.Merge(tbl);
                        }
                    }
                    GridViewDataBind(tbl_all);
                    divDanger.Visible = false;
                    if (txtTotalQty.Text == "0.00")
                    {
                        //MessageBox.Show("No balance available in this location !");
                        lblAlertDanger.Text = "No balance available in this location ! ";
                        divDanger.Visible = true;
                        return;
                    }
                    ALLOW_WHEARHOUSE_QTY();
                    return;
                }
                catch (Exception EX)
                {
                    lblAlertDanger.Text =
                               "Error occuered! \nAdvice: This might cause because of huge data amount.\nPlease enter a single item code and try. ";
                    divDanger.Visible = true;
                    return;
                }

                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                ////DataTable dataSource = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData(_CommonSearch.SearchParams);
                //DataTable dataSource = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2);

                //------------------------------------------------**-----------------------------------------------------------------------

                //GridViewItemDetails.Refresh();
                //if (GridAllLocations.DataSource == null)
                //{
                //    this.TextBoxLoc_Leave(sender, e);
                //}
                //else if (GridAllLocations.Rows.Count == 1)
                //{
                //    this.TextBoxLoc_Leave(sender, e);
                //}
                if (dgvItemDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow dgvr in dgvItemDetails.Rows)
                    {
                        //DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                        //if (Convert.ToBoolean(chk.Value) == true)
                        //{
                        //    if (dgvr.Cells["Loc_code"].Value == null)
                        //    {
                        //        this.TextBoxLoc_Leave(sender, e);
                        //        if (dgvr.Cells["Loc_code"].Value == null)
                        //        {
                        //            MessageBox.Show("Enter Location");
                        //            return;
                        //        }
                        //    }
                        //    string loc_ = dgvr.Cells["Loc_code"].Value.ToString();
                        //    selectLocation = loc_;

                        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                        //    tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2);
                        //    if (tbl != null)
                        //    {
                        //        if (tbl.Rows.Count > 0)
                        //        {
                        //            tbl_all.Merge(tbl);
                        //        }
                        //    }
                        //}
                    }
                    GridViewDataBind(tbl_all);
                    divSuccess.Visible = false;
                    if (txtTotalQty.Text == "0.00")
                    {
                        lblAlertDanger.Text = "No balance available in this location ! ";
                        divDanger.Visible = true;
                        return;
                    }
                }

                ALLOW_WHEARHOUSE_QTY();
                //#region
                ////---13-03-2013----------------------**----ALLOW WHEARHOUSE QTY-------**-----------------------------------------------------------------
                //Boolean allow_WAREHOUSE_QTY = CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "SCMIWOQ");

                //if (allow_WAREHOUSE_QTY == false)
                //{
                //    #region LOOP THE GRID
                //    DataTable wh_houses = null;
                //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                //    wh_houses = CHNLSVC.Inventory.GetLocationByType(_CommonSearch.SearchParams, null, null);

                //    foreach (DataGridViewRow row in GridViewItemDetails.Rows)
                //    {
                //        var _duplicate = from _dup in wh_houses.AsEnumerable()
                //                         where _dup.Field<string>("CODE") == row.Cells["Location"].Value.ToString()
                //                         select _dup;

                //        if (_duplicate.Count() > 0)
                //        {
                //            row.Cells["Avail_Stock"].Value = DBNull.Value;
                //            row.Cells["Avail_Stock"].Style.ForeColor = Color.Green;
                //        }
                //    }
                //    #endregion
                //}
                ////-------------------------**-------------------------------**-----------------------------------------------------------------
                //#endregion
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void ALLOW_WHEARHOUSE_QTY()
        {
            try
            {
                Boolean allow_WAREHOUSE_QTY = CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INVAC");

                if (allow_WAREHOUSE_QTY == false)
                {
                    #region LOOP THE GRID

                    DataTable wh_houses = null;
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                    wh_houses = CHNLSVC.Inventory.GetLocationByType(para, null, null);

                    foreach (GridViewRow row in dgvItemDetails.Rows)
                    {
                        var _duplicate = from _dup in wh_houses.AsEnumerable()
                                         where _dup.Field<string>("CODE") == row.Cells[1].Text
                                         //"Location"
                                         select _dup;

                        if (_duplicate.Count() > 0)
                        {
                            row.Cells[1].Text = ""; //"Avail_Stock"
                            row.Cells[1].BackColor = Color.Green; //"Avail_Stock"
                        }
                    }
                    lblGrandTotal.Visible = true;
                    //Change after chamal sujjest
                    txtTotalQty.Visible = true;

                    #endregion LOOP THE GRID
                }
                else
                {
                    lblGrandTotal.Visible = true;
                    txtTotalQty.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "Error Occurred while processing...");
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void GridViewDataBind(DataTable dataSource)
        {
            Session["DATAITEMS"] = null;
            dgvItemDetails.DataSource = null;
            if (dataSource != null)
            {
                if (dataSource.Rows.Count > 0)
                {
                    DataView dv = new DataView(dataSource);
                    dv.Sort = "COMPANY,LOC, ITEM_CODE ASC";
                    dataSource = dv.ToTable();
                }
                dataTable = dataSource;
                Session["dataTable"] = dataTable;
            }
            dgvItemDetails.DataSource = dataSource;
            dgvItemDetails.DataBind();
            ShowPkgQuntity();
            foreach (GridViewRow row in dgvItemDetails.Rows)
            {
                Label lblCost = (Label)row.FindControl("lblCostVal");
                if (chkShowCostValue.Checked)
                {
                    lblCost.Visible = true;
                }
                if (!chkShowCostValue.Checked)
                {
                    lblCost.Visible = false;
                }
            }

            txtTotalQty.Text = GetTotalQty(dataSource).ToString("N");
            Session["DATAITEMS"] = dataSource;
        }
        private void GridViewDataBind2(DataTable dataSource)
        {
            //dgvItemDetails.DataSource = null;
            chkShowCostValue_CheckedChanged(null, null);
            dgvItemDetails.DataSource = dataSource;
            dgvItemDetails.DataBind();
            txtTotalQty.Text = GetTotalQty(dataSource).ToString("N");
            ShowPkgQuntity();
        }
        private int GetTotalQty(DataTable dataSource)
        {
            int total = 0;
            try
            {
                for (int i = 0; i < dataSource.Rows.Count; i++)
                {
                    total = total + Convert.ToInt32(dataSource.Rows[i]["FREE_QTY"]);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "Error Occurred while processing...");
            }

            return total;
        }


        private void ViewItemCondtion()
        {
            // Cursor = Cursors.WaitCursor;
            List<string> LocationList = new List<string>();
            if (chkAdvancedSearch.Checked)
            {
                string com = ucLoactionSearch.Company;
                string chanel = ucLoactionSearch.Channel;
                string subChanel = ucLoactionSearch.SubChannel;
                string area = ucLoactionSearch.Area;
                string region = ucLoactionSearch.Regien;
                string zone = ucLoactionSearch.Zone;
                string pc = ucLoactionSearch.ProfitCenter;

                LocationList.Clear();
                string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();

                DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);

                foreach (DataRow drow in dt.Rows)
                {
                    LocationList.Add(drow["PROFIT_CENTER"].ToString());
                }

            }
            else
            {
                // LocationList.Add(txtIClocationDef.Text);
            }

            string ItemCondition = string.Empty;
            if (ddlItemStatus.SelectedIndex > 0)
            {
                ItemCondition = ddlItemStatus.SelectedValue;
            }

            DataTable dtDetails = new DataTable();
            dtDetails = CHNLSVC.General.Get_ItemCondition_Inquiary(Session["UserID"].ToString(), txtCompany.Text,
                txtMainCategory.Text//txtICMainCategori.Text
                , txtSubCategory.Text//txtICSubCate.Text
                , txtBrand.Text//txtICBrand.Text
                , txtItemCode.Text.Trim()//txtICItemCode.Text
                , txtModel.Text//txtICModel.Text
                , ItemCondition, LocationList);

            if (dtDetails.Rows.Count > 0)
            {
                dgvItemDetails.DataSource = null;
                dgvItemDetails.DataSource = dtDetails;
                dgvItemDetails.DataBind();
                ShowPkgQuntity();
            }
            else
            {
                dgvItemDetails.DataSource = null;
                //MessageBox.Show("No stock Available", "itemCondition - Enquiries", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //  Cursor = Cursors.Default;
        }

        protected void lBtnAlertDanger_Click(object sender, EventArgs e)
        {
            divDanger.Visible = false;
        }

        protected void lBtnAlertSuccess_Click(object sender, EventArgs e)
        {
            divSuccess.Visible = false;
        }

        protected void lbtnLocationAll_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnLocationClear_Click(object sender, EventArgs e)
        {
            grvAllLocation.DataSource = null;
            grvAllLocation.DataBind();
        }

        protected void lBtnChannel_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "InvTrcChnl";
                divDanger.Visible = false;
                if (txtCompany.Text == "")
                {
                    divDanger.Visible = true;
                    lblAlertDanger.Text = "Please enter company code !!!";
                    return;
                }
                lblSearchType.Text = "InvTrcChnl";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvTrcChnl);
                DataTable _result = CHNLSVC.CommonSearch.GetInventoryTrackeChannel(para, null, null);
                dgvResultItem.DataSource = null;
                txtSearchbyword.Text = "";
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["InvTrcChnl"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "InvTrcChnl";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lBtnChanLocation_Click(object sender, EventArgs e)
        {
            Session["Loc_HIRC_Location"] = null;
            try
            {
                divDanger.Visible = false;
                if (txtCompany.Text == "")
                {
                    divDanger.Visible = true;
                    lblAlertDanger.Text = "Please enter company code !!!";
                    return;
                }
                if (txtChannel.Text == "")
                {
                    divDanger.Visible = true;
                    lblAlertDanger.Text = "Please enter channel code !!!";
                    return;
                }
                lblSearchType.Text = "Loc_HIRC_Location";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Loc_HIRC_Location"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "Loc_HIRC_Location";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //Label Item = e.Row.FindControl("ITEM_CODE") as Label;
            //Label model = e.Row.FindControl("MODEL") as Label;
            //Label resQty = e.Row.FindControl("RES_QTY") as Label;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label LabelCoachName = e.Row.FindControl("ITEM_DESC") as Label;
            //    LabelCoachName.ToolTip = LabelCoachName.Text;
            //}
           

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{ 
            //    Label loc = (e.Row.DataItem as DataRowView)[1]
            //    loc.ToolTip = (e.Row.DataItem as DataRowView)[15].ToString();
            //}
        }

        protected void lBtnAlertInfoCancel_Click(object sender, EventArgs e)
        {
            hdfAlertInfo.Value = "2";
            lbtnView_Click(null, null);
        }

        protected void lBtnAlertInfoOk_Click(object sender, EventArgs e)
        {
            hdfAlertInfo.Value = "1";
            lbtnView_Click(null, null);
        }
        protected void btnGridImgSelect_Click(object sender, EventArgs e)
        {

            //PopupDocument.Show();
        }

        protected void btnItmRes_Click(object sender, EventArgs e)
        {
            object sum;
            try
            {
                bool b10144 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10144);
                if (!b10144)
                {
                    DispMsg("Sorry, You have no permission! ( Advice: Required permission code : 10144)"); return;
                }

                //lblSearchType.Text = "ItemRes";

                GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
                Label lblCOMPANY = row.FindControl("lblCOMPANY") as Label;
                Label lblLocation = row.FindControl("lblLocation") as Label;
                Label lblITEM_CODE = row.FindControl("lblITEM_CODE") as Label;
                Label lblITEM_STATUS = row.FindControl("lblITEM_STATUS") as Label;
                Label lblMODEL_CODE = row.FindControl("lblMODEL_CODE") as Label;
                Label lblRES_QTY = row.FindControl("lblRES_QTY") as Label;

                //DataTable _result = CHNLSVC.Inventory.GetItemReservationDet(lblCOMPANY.Text, lblLocation.Text, lblITEM_CODE.Text, lblITEM_STATUS.Text);
                DataTable _result = CHNLSVC.Inventory.Collect_ItemReservationDtl(lblCOMPANY.Text, lblLocation.Text, lblITEM_CODE.Text, lblITEM_STATUS.Text);
                Session["ItemRes"] = _result;

                if (_result.Rows.Count > 0)
                {
                    gdvResDet.DataSource = _result;
                    gdvResDet.DataBind();

                    lblItemRes.Text = lblITEM_CODE.Text;
                    lblModelRes.Text = lblMODEL_CODE.Text;

                    sum = _result.Compute("Sum(Qty)", "");
                    lblQtyRes.Text = sum.ToString();//lblRES_QTY.Text;
                    BindUCtrlDDLData(_result);
                    ItmResPopup.Show();
                    //ItmResDet.Visible = true;
                    //lblSearchType.Text = "";
                }
                else
                {
                    DisplayMessage("No reservation data", 2);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }


        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
                dgvItemDetails_RowDataBound(null, null);
                // clear();
            }
        }

        protected void dgvItemDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {


        }

        protected void dgvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                dgvItemDetails.PageIndex = e.NewPageIndex;
                dgvItemDetails.DataSource = null;
                dgvItemDetails.DataSource = (DataTable)Session["dataTable"]; ;
                dgvItemDetails.DataBind();
                ShowPkgQuntity();
                foreach (GridViewRow row in dgvItemDetails.Rows)
                {
                    Label lblCost = (Label)row.FindControl("lblCostVal");
                    if (chkShowCostValue.Checked)
                    {
                        lblCost.Visible = true;
                    }
                    if (!chkShowCostValue.Checked)
                    {
                        lblCost.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvItemDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes.Add("onmouseover","this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FF9955';");
                //e.Row.Attributes.Add("style", "cursor:pointer;");
                //e.Row.Attributes.Add("onmouseout","this.style.backgroundColor=this.originalstyle;");


                //e.Row.ToolTip = "Click to select row";
                //   e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(dgvItemDetails, "Select$" + e.Row.RowIndex);
            }
        }

        protected void lBtnDilogResultYes_Click(object sender, EventArgs e)
        {
            hdfAlertInfo.Value = "1";
            PopupDilogResult.Hide();
            lbtnView_Click(null, null);
        }

        protected void lBtnDilogResultNo_Click(object sender, EventArgs e)
        {
            hdfAlertInfo.Value = "2";
            PopupDilogResult.Hide();
            lbtnView_Click(null, null);
        }

        protected void dgvItemDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblItemRes.Text = "";
            lblModelRes.Text = "";
            lblQtyRes.Text = "";
            if (dgvItemDetails.SelectedIndex > -1)
            {
                Label lblLocation = (Label)dgvItemDetails.SelectedRow.FindControl("lblLocation");
                string companyCode = ((Label)dgvItemDetails.SelectedRow.FindControl("lblCOMPANY")).Text;
                string itemCode = ((Label)dgvItemDetails.SelectedRow.FindControl("lblITEM_CODE")).Text;
                string location =  ((Label)dgvItemDetails.SelectedRow.FindControl("lblCOMPANY")).Text;
                string itemStatus = ((Label)dgvItemDetails.SelectedRow.FindControl("lblITEM_STATUS")).Text;
                string itemModel = ((Label)dgvItemDetails.SelectedRow.FindControl("lblMODEL_CODE")).Text;
                string itemResQty = ((Label)dgvItemDetails.SelectedRow.FindControl("lblRES_QTY")).Text;
                location = lblLocation.Text;

                

                //if (lblSearchType.Text == "ItemRes")
                //{
                //    DataTable _result = CHNLSVC.Inventory.GetItemReservationDet(companyCode, location, itemCode, itemStatus);

                //    Session["ItemRes"] = _result;

                    


                //    if (_result.Rows.Count > 0)
                //    {
                //        gdvResDet.DataSource = _result;
                //        gdvResDet.DataBind();

                //        lblItemRes.Text = itemCode;
                //        lblModelRes.Text = itemModel;
                //        lblQtyRes.Text = itemResQty;

                //        BindUCtrlDDLData(_result);

                        
                //        ItmResPopup.Show();
                //        //ItmResDet.Visible = true;


                //        //lblSearchType.Text = "";
                //    }
                //    else
                //    {
                //        DisplayMessage("No reservation data", 2);
                //    }
                //}
                //else{

                //    lblSearchType.Text = "";
                
                //MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itemCode); // Nadeeka 06-08-2015
                //divDanger.Visible = false;
                //if (_item != null)
                //{
                //    if (_item.Mi_is_ser1 == -1)
                //    {
                //        lblAlertDanger.Text = "This item is a decimal allowed item, serials are not available for such items";
                //        divDanger.Visible = true;
                //        return;
                //    }
                //}

                    Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();

                    foreach (KeyValuePair<string, string> pair in status_list)
                    {
                        //Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
                        if (pair.Value == itemStatus)
                        {
                            itemStatus = pair.Key;
                        }
                    }
                    // panel_serialView.Visible = true;
                    //***************************************************
                    string status = string.Empty;
                    if (ddlItemStatus.SelectedItem == null)
                    {
                        status = "";
                    }
                    else
                    {
                        status = ddlItemStatus.SelectedItem.Value;
                        //ComboboxItem combo_reqtp = (ComboboxItem) DDLStatus.SelectedItem;
                        //status = combo_reqtp.Value.ToString();
                    }

                    ucItemSerialView1.ITEM_CODE = itemCode;
                    //GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[2].Text;
                    ucItemSerialView1.ITEM_STATUS = itemStatus; //status;
                    ucItemSerialView1.COMPANY = companyCode;
                    //GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[0].Text;
                    ucItemSerialView1.CHANNEL = "";
                    ucItemSerialView1.SUB_CHANNEL = "";
                    ucItemSerialView1.AREA = "";
                    ucItemSerialView1.REAGION = "";
                    ucItemSerialView1.ZONE = "";
                    ucItemSerialView1.TYPE = "Loc";
                    ucItemSerialView1.LOC = location;
                    //GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[1].Text;

                    ucItemSerialView1.IsVisible = true;
                    ucItemSerialView1.ShowCost = chkShowCostValue.Checked;

                    ucItemSerialView1.Display();
                    ucItemSerialView1.ITEM_CODE = txtItemCode.Text.Trim().ToUpper();
                    Session["ItemCode"] = ucItemSerialView1.ITEM_CODE;
                    Session["ShowUcDocument"] = "Y";

                    MasterItem mst_item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itemCode);
                    if (mst_item != null)
                    {
                        if (mst_item.Mi_is_ser1 == -1)
                        {
                            displayMessage("This item is a decimal allowed item, serials are not available for such items  !");
                        }
                    }
                    PopupDocument.Show();
                
            }
        }
        private void displayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

        }
        protected void lbtPopDocClose_OnClick(object sender, EventArgs e)
        {
            Session["ShowUcDocument"] = "N";
            Session["DATADOC"] = null;
            Session["DATASERIAL"] = null;
            ucItemSerialView1.ClearDisplay();
            PopupDocument.Hide();
        }
        protected void chkAllLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllLocation.Checked)
            {
                txtLocation.Text = "";
                selectLocation = "%";
                Session["SELECTLOCATION"] = "%";
                grvAllLocation.DataSource = null;
                grvAllLocation.DataBind();
            }
            else
            {
                txtLocation.Text = Session["UserDefLoca"].ToString();
                selectLocation = txtLocation.Text;
                Session["SELECTLOCATION"] = txtLocation.Text;
                divDanger.Visible = false;
                try
                {
                    AllAlocationBind();
                }
                catch (Exception ex)
                {
                    lblAlertDanger.Text = "Error Occurred while processing...\n" + ex.Message;
                    divDanger.Visible = true;
                    return;
                }
            }
        }
        private void AllAlocationBind()
        {
            if (txtLocation.Text.Trim() == "")
            {
                grvAllLocation.DataSource = null;
                grvAllLocation.DataBind();
                return;
            }
            try
            {
                List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(Session["UserCompanyCode"].ToString(), txtLocation.Text.ToUpper());
                divDanger.Visible = false;
                if (loc_list == null)
                {
                    lblAlertDanger.Text = "Invalid Location Code";
                    divDanger.Visible = true;
                    return;
                }
                else if (loc_list.Count < 1)
                {
                    MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtLocation.Text.ToUpper());
                    loc_list.Add(loc_);
                }

                grvAllLocation.DataSource = null;
                grvAllLocation.DataSource = loc_list;
                grvAllLocation.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "CheckAllLocation();", true);

            }
            catch (Exception ex)
            {
            }
        }
        protected void btnTemp_Click(object sender, EventArgs e)
        {
            hdfTabIndex.Value = "#ExcessStockEnquiry";
        }
        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
                CheckBox chkTempSelect = (CheckBox)Row.FindControl("chkLocationRow");
                bool select = chkTempSelect.Checked;
                bool allSelected = false;
                if (select)
                {
                    allSelected = true;
                    foreach (GridViewRow hiderowbtn in this.grvAllLocation.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)hiderowbtn.FindControl("chkLocationRow");
                        if (chkSelect.Checked == false)
                        {
                            allSelected = false;
                        }
                    }
                }
                else
                {
                    allSelected = false;
                }
                CheckBox chk = (CheckBox)grvAllLocation.HeaderRow.FindControl("chkboxSelectAll");
                chk.Checked = allSelected;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }

        }
        protected void lbtnSeCat4_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub3";
                Session["CAT_Sub3"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _result = CHNLSVC.General.GetItemSubCat4(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub3"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "CAT_Sub3";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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
        protected void lbtnSeCat5_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub4";
                Session["CAT_Sub4"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                DataTable _result = CHNLSVC.General.GetItemSubCat5(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub4"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "CAT_Sub4";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void dgvItemDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            // SetSortDirection("desc");
            _sortDirection = sortOrder;
            //Sort the data.
            dataTable = (DataTable)Session["dataTable"];
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                    dgvItemDetails.DataSource = dataTable;
                    dgvItemDetails.DataBind();
                    SortDireaction = _sortDirection;
                }
            }
            chkShowCostValue_CheckedChanged(null, null);
        }

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

        #region Inventry tracker >> product details : Kelum : 2016-May-19

        protected void lbtnPDitemcode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ProdcutDetailsItem";
                Session["ProdcutDetailsItem"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["ProdcutDetailsItem"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "ProdcutDetailsItem";

                if (dgvResultItem.PageIndex > 0)
                {
                    dgvResultItem.SetPageIndex(0);
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

        protected void lbtnProDetails_Click(object sender, EventArgs e)
        {

        }

        private void LoadProductDetails()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPDitemcode.Text))
                {
                    ClearProductDetails();
                    return;
                }

                MasterItem mst_item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtPDitemcode.Text.Trim().ToUpper());
                if (mst_item == null)
                {
                    displayMessage("Invalid Item Code…!!!");
                    ClearProductDetails();
                    txtPDitemcode.Focus();
                    return;
                }
                else
                {
                    txtPDitemcode.Text = mst_item.Mi_cd;

                    txtItemCodePDI.Text = mst_item.Mi_cd;
                    txtItemTypePDI.Text = mst_item.Mi_itm_tp;
                    txtDescriptionPDI.Text = mst_item.Mi_shortdesc;
                    txtBrandPDI.Text = mst_item.Mi_brand;
                    txtModelPDI.Text = mst_item.Mi_model;
                    txtExtClrPDI.Text = mst_item.Mi_color_ext;
                    //  txtCategoryPDI.Text = mst_item.Mi_cate_1;
                    txtIntClrPDI.Text = mst_item.Mi_color_int;
                    // txtTaxRatePDI = mst_item.Mi_          
                    chkHasHPinsPDI.Checked = mst_item.Mi_insu_allow;
                    txtUOMPDI.Text = mst_item.Mi_itm_uom;
                    chkStusActivePDI.Checked = mst_item.Mi_act;
                    txtCate1PDI.Text = mst_item.Mi_cate_1;
                    txtCate2PDI.Text = mst_item.Mi_cate_2;
                    txtCate3PDI.Text = mst_item.Mi_cate_3;
                    txtPartNoPd.Text = mst_item.Mi_part_no;
                    txtPackingCodePDI.Text = mst_item.Mi_packing_cd;


                    //add sachith
                    if (mst_item.Mi_is_ser1 == 1)
                        chkSerializedPDI.Checked = true;
                    else
                        chkSerializedPDI.Checked = false;
                    chkHasWarrtPDI.Checked = mst_item.Mi_warr;
                    chkHasInsPDI.Checked = mst_item.Mi_need_insu;


                    DataTable _cat1 = CHNLSVC.General.GetMainCategoryDetail(txtCate1PDI.Text);
                    if (_cat1.Rows.Count > 0)
                    {
                        lblCate1PDI.Text = _cat1.Rows[0]["ric1_desc"].ToString();
                        if (string.IsNullOrEmpty(_cat1.Rows[0]["ric1_age"].ToString()))
                        {
                            lblCate1AgePDI.Text = "Product aging policy is not setup";
                        }
                        else
                        {
                            lblCate1AgePDI.Text = "Product aging policy activate after " + _cat1.Rows[0]["ric1_age"].ToString() + " days";
                        }
                    }

                    DataTable _cat2 = CHNLSVC.Sales.GetItemSubCate2(txtCate1PDI.Text, txtCate2PDI.Text);
                    if (_cat2.Rows.Count > 0)
                    {
                        lblCate2PDI.Text = _cat2.Rows[0]["ric2_desc"].ToString();
                    }

                    DataTable _cat3 = CHNLSVC.Sales.GetItemSubCate3(txtCate1PDI.Text, txtCate2PDI.Text, txtCate3PDI.Text);
                    if (_cat3.Rows.Count > 0)
                    {
                        lblCate3PDI.Text = _cat3.Rows[0]["ric2_desc"].ToString();
                    }
                    Decimal TAX_RT = CHNLSVC.Sales.GET_Item_vat_Rate(Session["UserCompanyCode"].ToString(), mst_item.Mi_cd, "VAT");
                    txtTaxRatePDI.Text = TAX_RT.ToString();

                    //--------------------------------------------

                    //GetItemStatusWiseWarrantyPeriods
                    DataTable warr_dt = CHNLSVC.Sales.GetItemStatusWiseWarrantyPeriods(mst_item.Mi_cd, string.Empty);
                    grdWarrentyDetailsGL.DataSource = null;
                    //tmpRmk,MWP_RMK
                    if (warr_dt != null)
                    {
                        if (warr_dt.Rows.Count > 0)
                        {
                            warr_dt.Columns.Add("tmpRmk");
                            foreach (DataRow rw in warr_dt.Rows)
                            {
                                string rem = rw["MWP_RMK"].ToString();
                                if (!string.IsNullOrEmpty(rem))
                                {
                                    if (rem.Length > 60)
                                    {
                                        rw["tmpRmk"] = rem.Substring(0, 60);
                                    }
                                    else
                                    {
                                        rw["tmpRmk"] = rem;
                                    }
                                }
                            }
                        }
                    }
                    grdWarrentyDetailsGL.DataSource = warr_dt;
                    grdWarrentyDetailsGL.DataBind();
                    BinsStatusWarrantyDetails();


                    DataTable _SpWara = CHNLSVC.Sales.GetAllPCWara(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtPDitemcode.Text.Trim());
                    grdWarrentyDetailsSP.DataSource = null;
                    grdWarrentyDetailsSP.DataSource = _SpWara;
                    grdWarrentyDetailsSP.DataBind();
                    BinsStatusWarrentyDetailsSP();

                    //Status Wise Tax details
                    txtCompanySWTD.Text = Session["UserCompanyCode"].ToString();
                    LoadTaxbyCompany();
                    //

                    DataTable _service = CHNLSVC.Inventory.GetItemServiceSchedule(txtPDitemcode.Text.Trim());
                    grdServiceShedule.DataSource = null;
                    grdServiceShedule.DataSource = _service;
                    grdServiceShedule.DataBind();
                    BinsStatusServiceShedul();

                    DataTable _component = CHNLSVC.Inventory.GetItemComponentTableNew(txtPDitemcode.Text.Trim());

                    string _tp = "";
                    string _table = "";
                    if (_component != null)
                    {
                        if (_component.Rows.Count > 0)
                        {
                            _table = _component.Rows[0]["tblName"].ToString();
                            if (!string.IsNullOrEmpty(_table))
                            {
                                if (_table == "mst_itm_component")
                                {
                                    if (_component.Rows[0]["CostMethode"].ToString() == "1")
                                    {
                                        _tp = " (%)";
                                    }
                                    else
                                    {
                                        _tp = " (AMT)";
                                    }
                                }
                                if (_table == "MST_ITM_KIT_COMPONENT")
                                {
                                    if (_component.Rows[0]["CostMethode"].ToString() == "PER")
                                    {
                                        _tp = " (%)";
                                    }
                                    else
                                    {
                                        _tp = " (AMT)";
                                    }
                                }
                            }
                        }
                    }
                    grdComponentItem.DataSource = null;
                    grdComponentItem.DataSource = _component;
                    grdComponentItem.DataBind();
                    Label lblCostTp = grdComponentItem.HeaderRow.FindControl("lblCostTp") as Label;
                    lblCostTp.Text = lblCostTp.Text + _tp;
                    List<MasterItemSimilar> _similar = new List<MasterItemSimilar>();
                    _similar = CHNLSVC.Inventory.GetSimilarItems("I", txtPDitemcode.Text.Trim(), Session["UserCompanyCode"].ToString(), DateTime.Now.Date, null, null, null, null);

                    if (_similar != null)
                    {
                        grdSimmilarItem.DataSource = null;
                        grdSimmilarItem.DataSource = _similar;
                        grdSimmilarItem.DataBind();
                    }

                    List<MasterItemTaxClaim> _taxclaimable  = new List<MasterItemTaxClaim>();
                    //_taxclaimable = CHNLSVC.Inventory.GetSimilarItems("I", txtPDitemcode.Text.Trim(), Session["UserCompanyCode"].ToString(), DateTime.Now.Date, null, null, null, null);
                    _taxclaimable = CHNLSVC.General.getitemTaxClaim(txtPDitemcode.Text.Trim());
                    if (_taxclaimable != null)
                    {
                        grdTaxClaimable.DataSource = null;
                        grdTaxClaimable.DataSource = _taxclaimable;
                        grdTaxClaimable.DataBind();
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

        private void ClearProductDetails()
        {

            txtPDitemcode.Text = "";

            txtItemCodePDI.Text = "";
            txtItemTypePDI.Text = "";
            txtDescriptionPDI.Text = "";
            txtBrandPDI.Text = "";
            txtModelPDI.Text = "";
            txtExtClrPDI.Text = "";
            //  txtCategoryPDI.Text = "";
            txtIntClrPDI.Text = "";
            txtTaxRatePDI.Text = "";
            chkHasHPinsPDI.Checked = false;
            txtUOMPDI.Text = "";
            chkStusActivePDI.Checked = false;
            txtCate1PDI.Text = "";
            lblCate1PDI.Text = "";
            lblCate1AgePDI.Text = "";
            txtCate2PDI.Text = "";
            lblCate2PDI.Text = "";
            txtCate3PDI.Text = "";
            txtPartNoPd.Text = "";
            lblCate3PDI.Text = "";
            txtPackingCodePDI.Text = "";

            chkSerializedPDI.Checked = false;
            chkHasWarrtPDI.Checked = false;
            chkHasInsPDI.Checked = false;


            grdWarrentyDetailsGL.DataSource = null;
            grdWarrentyDetailsGL.DataSource = null;
            grdWarrentyDetailsSP.DataSource = null;
            grdStatusWiseTaxDet.DataSource = null;
            grdServiceShedule.DataSource = null;
            grdComponentItem.DataSource = null;
            grdSimmilarItem.DataSource = null;
            grdTaxClaimable.DataSource = null;
        }

        protected void lbtnCompanySWTD_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ProdcutDetailsCompanyCode";
                Session["ProdcutDetailsCompanyCode"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["ProdcutDetailsCompanyCode"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "ProdcutDetailsCompanyCode";

                if (dgvResultItem.PageIndex > 0)
                {
                    dgvResultItem.SetPageIndex(0);
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

        protected void txtCompanySWTD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCompanySWTD.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, "CODE", txtCompanySWTD.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["CODE"].ToString()))
                        {
                            if (txtCompanySWTD.Text.ToUpper() == row["CODE"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtCompanySWTD.ToolTip = toolTip;
                    }
                    else
                    {
                        txtCompanySWTD.ToolTip = "";
                        txtCompanySWTD.Text = "";
                        txtCompanySWTD.Focus();
                        DispMsg("Please select a valid company !!!");
                        return;
                    }
                }
                else
                {
                    txtCompanySWTD.ToolTip = "";
                    LoadTaxbyCompany();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }

        }
        private void DispMsg(string msgText, string msgType = "")
        {
            //msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

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
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }
        private void LoadTaxbyCompany()
        {
            try
            {
                if (txtPDitemcode.Text == "")
                {
                    return;
                }

                if (!(string.IsNullOrEmpty(txtCompanySWTD.Text)))
                {
                    MasterCompany _masterComp = CHNLSVC.General.GetCompByCode(txtCompanySWTD.Text);

                    if (_masterComp.MC_TAX_CALC_MTD == "1")
                    {
                        string stuc_code = "";
                        string _comp = txtCompanySWTD.Text.Trim().ToUpper();

                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(_comp, txtPDitemcode.Text.Trim().ToUpper());
                        stuc_code = _mstItem.Mi_anal1;

                        if (!(string.IsNullOrEmpty(stuc_code)))
                        {

                            List<mst_itm_tax_structure_det> _lstitemTaxDet = new List<mst_itm_tax_structure_det>();
                            _lstitemTaxDet = CHNLSVC.General.getItemTaxStructure(stuc_code);

                            if (_lstitemTaxDet != null)
                            {

                                var filteredCollection = (from pro in _lstitemTaxDet
                                                          where pro.Its_com == _comp
                                                          select new
                                                          {
                                                              MICT_STUS = pro.Its_stus,
                                                              MICT_TAX_CD = pro.Its_tax_cd,
                                                              MICT_TAX_RATE = pro.Its_tax_rate
                                                          }).ToList();



                                if (filteredCollection.Count > 0)
                                {
                                    grdStatusWiseTaxDet.DataSource = null;
                                    grdStatusWiseTaxDet.DataSource = filteredCollection.ToDataTable();
                                    grdStatusWiseTaxDet.DataBind();
                                    BinsStatusWarrentyDetailsTaxDet();
                                }

                                //else
                                //{
                                //    grdStatusWiseTaxDet.DataSource = null;
                                //    grdStatusWiseTaxDet.DataBind();
                                //    DisplayMessage("No Data Found for this company code...\n", 2);
                                //    txtCompanySWTD.Focus();
                                //}                           

                            }



                        }
                    }

                    else
                    {
                        DataTable _warrtax = CHNLSVC.Inventory.GetItemTaxData(txtCompanySWTD.Text, txtPDitemcode.Text.ToUpper());
                        if (_warrtax.Rows.Count > 0)
                        {
                            grdStatusWiseTaxDet.DataSource = null;
                            grdStatusWiseTaxDet.DataSource = _warrtax;
                            grdStatusWiseTaxDet.DataBind();
                            BinsStatusWarrentyDetailsTaxDet();
                        }

                        //else
                        //{
                        //    grdStatusWiseTaxDet.DataSource = null;
                        //    grdStatusWiseTaxDet.DataBind();
                        //    DisplayMessage("No Data Found for this company code...\n", 2);
                        //    txtCompanySWTD.Focus();
                        //}

                        return;
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

        protected void txtPDitemcode_TextChanged(object sender, EventArgs e)
        {
            LoadProductDetails();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchbyword.Text != "")
            {
                if (Label2.Text == "Item")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Item"] = _result;
                    return;
                }
                if (Label2.Text == "Bin")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_BIN(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Bin"] = _result;
                    return;
                }
                if (Label2.Text == "Item2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item2);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Item2"] = _result;
                    return;
                }
                if (Label2.Text == "Item3")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item2);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Item3"] = _result;
                    return;
                }
                if (Label2.Text == "Channel2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                    DataTable _result = CHNLSVC.CommonSearch.GetChanalDetailsNew(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Channel2"] = _result;
                    return;
                }
                if (Label2.Text == "locationall")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["locationall"] = result;
                    return;
                }
                if (Label2.Text == "Model")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Model"] = _result;
                    return;
                }
                if (Label2.Text == "Brand")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Brand"] = _result;
                    return;
                }
                if (Label2.Text == "CAT_Main")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["CAT_Main"] = _result;
                    return;
                }
                if (Label2.Text == "CAT_Sub1")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    string valuetest = txtSearchbyword.Text;
                    valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["CAT_Sub1"] = _result;
                    return;
                }
                if (Label2.Text == "CAT_Sub2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    string valuetest = txtSearchbyword.Text;
                    valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["CAT_Sub2"] = _result;
                    return;
                }
                if (Label2.Text == "CAT_Sub3")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                    DataTable _result = CHNLSVC.General.GetItemSubCat4(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["CAT_Sub3"] = _result;
                    return;
                }
                if (Label2.Text == "CAT_Sub4")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                    DataTable _result = CHNLSVC.General.GetItemSubCat5(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["CAT_Sub4"] = _result;
                    return;
                }
                if (Label2.Text == "Company")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Company"] = _result;
                    return;
                }
                if (Label2.Text == "InvTrcChnl")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvTrcChnl);
                    DataTable _result = CHNLSVC.CommonSearch.GetInventoryTrackeChannel(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["InvTrcChnl"] = _result;
                    return;
                }
                if (Label2.Text == "Loc_HIRC_Location")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Loc_HIRC_Location"] = _result;
                    return;
                }
                if (Label2.Text == "UserLocation")
                {
                    string para = "";
                    //_CommonSearch.ReturnIndex = 0;

                    DataTable _result = new DataTable();
                    //----------------------------------------------------------------------------
                    Boolean allow_WHAREHOUSE = CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INVAC");
                    DataTable _result2 = null;
                    DataTable _dtGetLoc = new DataTable();
                    if (allow_WHAREHOUSE == true)
                    {
                        para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                        _result2 = CHNLSVC.Inventory.GetLocationByType(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        _dtGetLoc.Merge(_result2);
                    }

                    para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    _dtGetLoc.Merge(CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text)); dgvResultItem.DataSource = _result;

                    DataTable _newDt = new DataTable();
                    _newDt.Columns.Add("Code");
                    _newDt.Columns.Add("Description");
                    foreach (DataRow dr in _dtGetLoc.Rows)
                    {
                        DataRow _newDataRow = _newDt.NewRow();
                        _newDataRow["Code"] = dr["Code"].ToString();
                        _newDataRow["Description"] = dr["Description"].ToString();
                        String author = dr["Code"].ToString();
                        bool contains = _newDt.AsEnumerable().Any(row => author == row.Field<String>("Code"));
                        if (!contains)
                        {
                            _newDt.Rows.Add(_newDataRow);
                        }
                    }

                    _result = _newDt;
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["UserLocation"] = _result;
                    return;

                }
                if (Label2.Text == "Channel")
                {
                    string para = "";
                    //_CommonSearch.ReturnIndex = 0;

                    DataTable _result = new DataTable();
                    //----------------------------------------------------------------------------
                    Boolean allow_WHAREHOUSE = CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INVAC");
                    DataTable _result2 = null;
                    if (allow_WHAREHOUSE == true)
                    {
                        para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                        _result2 = CHNLSVC.Inventory.GetLocationByType(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        _result.Merge(_result2);
                    }

                    para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    _result.Merge(CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text)); dgvResultItem.DataSource = _result;
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["Channel"] = _result;
                    return;
                }


                if (Label2.Text == "ProdcutDetailsItem")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["ProdcutDetailsItem"] = _result;
                    return;

                }

                else if (Label2.Text == "ProdcutDetailsCompanyCode")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["ProdcutDetailsCompanyCode"] = _result;
                    return;
                }

                else if (Label2.Text == "BalancesPOorBLNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POorBLNo);
                    DataTable _result = CHNLSVC.General.Get_PoBlNumber(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["BalancesPOorBLNo"] = _result;
                    return;
                }

                else if (Label2.Text == "BalancesDocGrnNo")
                {
                    DateTime? _docdtfrom = null;
                    DateTime? _docdtto = null;

                    if ((!string.IsNullOrEmpty(txtDWfrom.Text.Trim())) & (!string.IsNullOrEmpty(txtDWto.Text.Trim())))
                    {
                        _docdtfrom = Convert.ToDateTime(txtDWfrom.Text.Trim()).Date;
                        _docdtto = Convert.ToDateTime(txtDWto.Text.Trim()).Date;
                    }

                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                    DataTable _result = CHNLSVC.General.Get_DocGrnNumber(para, _docdtfrom, _docdtto, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["BalancesDocGrnNo"] = _result;
                    return;
                }

                else if (Label2.Text == "BalancesEntryNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.General.Get_EntryNumber(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    dgvResultItem.DataSource = _result;
                    dgvResultItem.DataBind();
                    ItemPopup.Show();
                    Session["BalancesEntryNo"] = _result;
                    return;
                }

            }

        }

        #endregion

        #region Inventry tracker >> Balances by Purchase Document : Kelum : 2016-June-01
        protected void lbtnPOBLNumber_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BalancesPOorBLNo";
                Session["BalancesPOorBLNo"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POorBLNo);
                DataTable _result = CHNLSVC.General.Get_PoBlNumber(SearchParams, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["BalancesPOorBLNo"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "BalancesPOorBLNo";

                if (dgvResultItem.PageIndex > 0)
                {
                    dgvResultItem.SetPageIndex(0);
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

        protected void lbtnDocGrnNumber_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BalancesDocGrnNo";
                Session["BalancesDocGrnNo"] = null;

                DateTime? _docdtfrom = null;
                DateTime? _docdtto = null;

                if ((!string.IsNullOrEmpty(txtDWfrom.Text.Trim())) & (!string.IsNullOrEmpty(txtDWto.Text.Trim())))
                {
                    _docdtfrom = Convert.ToDateTime(txtDWfrom.Text.Trim()).Date;
                    _docdtto = Convert.ToDateTime(txtDWto.Text.Trim()).Date;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = CHNLSVC.General.Get_DocGrnNumber2(SearchParams, _docdtfrom, _docdtto, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["BalancesDocGrnNo"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "BalancesDocGrnNo";

                if (dgvResultItem.PageIndex > 0)
                {
                    dgvResultItem.SetPageIndex(0);
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

        protected void lbtnEntryNumber_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BalancesEntryNo";
                Session["BalancesEntryNo"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                DataTable _result = CHNLSVC.General.Get_EntryNumber(SearchParams, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["BalancesEntryNo"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "BalancesEntryNo";

                if (dgvResultItem.PageIndex > 0)
                {
                    dgvResultItem.SetPageIndex(0);
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

        private void LoadBalancesbyPurchaseOrder()
        {
            try
            {
                string PO_or_BL_Number = txtPOorBLNumber.Text.Trim().ToUpper();
                string Doc_GRN_Number = txtDocGRNumber.Text.Trim().ToUpper();
                string Entry_Number = txtEntryNumber.Text.Trim().ToUpper();

                DataTable _balanceresult = new DataTable();

                if ((string.IsNullOrEmpty(PO_or_BL_Number)) & (string.IsNullOrEmpty(Doc_GRN_Number)) & (string.IsNullOrEmpty(Entry_Number)))
                {
                    DisplayMessage("Please enter PO/BL # or GRN # or Entry #", 2);
                    txtPOorBLNumber.Focus();
                    return;
                }

                //if all textboxes not null : PO_BL#
                if (!(string.IsNullOrEmpty(PO_or_BL_Number)) & !(string.IsNullOrEmpty(Doc_GRN_Number)) & !(string.IsNullOrEmpty(Entry_Number)))
                {
                    _balanceresult = LoadBalancesbyPOBLNumber(PO_or_BL_Number);

                    if (_balanceresult != null)
                    {
                        BindBalancesGridData(_balanceresult);
                    }
                    else
                    {
                        _balanceresult = LoadBalancesbyGRNNumber(Doc_GRN_Number);

                        if (_balanceresult != null)
                        {
                            BindBalancesGridData(_balanceresult);
                        }

                        else
                        {
                            _balanceresult = LoadBalancesbyEntryNumber(Entry_Number);

                            if (_balanceresult != null)
                            {
                                BindBalancesGridData(_balanceresult);
                            }
                            else
                            {
                                DisplayMessage("No data found for this search", 2);
                                txtPOorBLNumber.Focus();
                                return;
                            }
                        }

                    }
                }

                //if first two textboxes not null : PO_BL#
                if (!(string.IsNullOrEmpty(PO_or_BL_Number)) & !(string.IsNullOrEmpty(Doc_GRN_Number)) & (string.IsNullOrEmpty(Entry_Number)))
                {

                    _balanceresult = LoadBalancesbyPOBLNumber(PO_or_BL_Number);

                    if (_balanceresult != null)
                    {
                        BindBalancesGridData(_balanceresult);
                    }
                    else
                    {
                        _balanceresult = LoadBalancesbyGRNNumber(Doc_GRN_Number);

                        if (_balanceresult != null)
                        {
                            BindBalancesGridData(_balanceresult);
                        }
                        else
                        {
                            DisplayMessage("No data found for this search", 2);
                            txtPOorBLNumber.Focus();
                            return;
                        }
                    }

                }

                //if last two textboxes not null : GRN #
                if ((string.IsNullOrEmpty(PO_or_BL_Number)) & !(string.IsNullOrEmpty(Doc_GRN_Number)) & !(string.IsNullOrEmpty(Entry_Number)))
                {
                    _balanceresult = LoadBalancesbyGRNNumber(Doc_GRN_Number);

                    if (_balanceresult != null)
                    {
                        BindBalancesGridData(_balanceresult);
                    }
                    else
                    {
                        _balanceresult = LoadBalancesbyEntryNumber(Entry_Number);

                        if (_balanceresult != null)
                        {
                            BindBalancesGridData(_balanceresult);
                        }

                        else
                        {
                            DisplayMessage("No data found for this search", 2);
                            txtPOorBLNumber.Focus();
                            return;
                        }

                    }


                }

                //if first and last textboxes not null : PO_BL#
                if (!(string.IsNullOrEmpty(PO_or_BL_Number)) & (string.IsNullOrEmpty(Doc_GRN_Number)) & !(string.IsNullOrEmpty(Entry_Number)))
                {
                    _balanceresult = LoadBalancesbyPOBLNumber(PO_or_BL_Number);

                    if (_balanceresult != null)
                    {
                        BindBalancesGridData(_balanceresult);
                    }
                    else
                    {
                        _balanceresult = LoadBalancesbyEntryNumber(Entry_Number);

                        if (_balanceresult != null)
                        {
                            BindBalancesGridData(_balanceresult);
                        }
                        else
                        {
                            DisplayMessage("No data found for this search", 2);
                            txtPOorBLNumber.Focus();
                            return;
                        }
                    }
                }

                // POBL #
                if (!(string.IsNullOrEmpty(PO_or_BL_Number)) & (string.IsNullOrEmpty(Doc_GRN_Number)) & (string.IsNullOrEmpty(Entry_Number)))
                {
                    _balanceresult = LoadBalancesbyPOBLNumber(PO_or_BL_Number);

                    if (_balanceresult != null)
                    {
                        BindBalancesGridData(_balanceresult);
                    }
                    else
                    {
                        DisplayMessage("No data found for this search", 2);
                        txtPOorBLNumber.Focus();
                        return;
                    }
                }

                // GRN #
                if ((string.IsNullOrEmpty(PO_or_BL_Number)) & !(string.IsNullOrEmpty(Doc_GRN_Number)) & (string.IsNullOrEmpty(Entry_Number)))
                {
                    _balanceresult = LoadBalancesbyGRNNumber(Doc_GRN_Number);

                    if (_balanceresult != null)
                    {
                        BindBalancesGridData(_balanceresult);
                    }
                    else
                    {
                        DisplayMessage("No data found for this search", 2);
                        txtPOorBLNumber.Focus();
                        return;
                    }
                }

                // Entry #
                if ((string.IsNullOrEmpty(PO_or_BL_Number)) & (string.IsNullOrEmpty(Doc_GRN_Number)) & !(string.IsNullOrEmpty(Entry_Number)))
                {
                    _balanceresult = LoadBalancesbyEntryNumber(Entry_Number);

                    if (_balanceresult != null)
                    {
                        BindBalancesGridData(_balanceresult);
                    }
                    else
                    {
                        DisplayMessage("No data found for this search", 2);
                        txtPOorBLNumber.Focus();
                        return;
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

        private DataTable LoadBalancesbyPOBLNumber(string _poblnumber)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocWiseBalancePOBL);
                DataTable _balanceresult = CHNLSVC.General.LoadDocumentWiseBalance(SearchParams, _poblnumber, null, null);

                if (_balanceresult.Rows.Count > 0)
                {
                    return _balanceresult;
                }

                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2);
                CHNLSVC.CloseChannel();
                return null;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private DataTable LoadBalancesbyGRNNumber(string _grnnumber)
        {
            try
            {
                string PO_or_BL_Number = txtPOorBLNumber.Text.Trim().ToUpper();

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocWiseBalanceDocGRN);
                DataTable _balanceresult = CHNLSVC.General.LoadDocumentWiseBalance(SearchParams, null, _grnnumber, null);

                if (_balanceresult.Rows.Count > 0)
                {
                    return _balanceresult;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2);
                CHNLSVC.CloseChannel();
                return null;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private DataTable LoadBalancesbyEntryNumber(string _entrynumber)
        {
            try
            {
                string PO_or_BL_Number = txtPOorBLNumber.Text.Trim().ToUpper();

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocWiseBalanceEntry);
                DataTable _balanceresult = CHNLSVC.General.LoadDocumentWiseBalance(SearchParams, null, null, _entrynumber);

                if (_balanceresult.Rows.Count > 0)
                {
                    return _balanceresult;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2);
                CHNLSVC.CloseChannel();
                return null;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void BindBalancesGridData(DataTable _result)
        {
            try
            {
                grdDocWiseBal.DataSource = null;
                grdDocWiseBal.DataSource = _result;
                grdDocWiseBal.DataBind();
                return;
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
        #endregion


        #region Add by Lakshan
        private void BinsStatusWarrantyDetails()
        {
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            foreach (GridViewRow rw in grdWarrentyDetailsGL.Rows)
            {
                Label lblStatus = rw.FindControl("lblStatus") as Label;
                Label lbl_Status_WDGL = rw.FindControl("lbl_Status_WDGL") as Label;
                if (status_list != null)
                {
                    lblStatus.Text = status_list.Where(c => c.Key == lbl_Status_WDGL.Text).FirstOrDefault().Value;
                }
            }
        }
        private void BinsStatusWarrentyDetailsSP()
        {
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            foreach (GridViewRow rw in grdWarrentyDetailsSP.Rows)
            {
                Label lblStatus = rw.FindControl("lblStatus") as Label;
                Label lbl_Status_WDSP = rw.FindControl("lbl_Status_WDSP") as Label;
                if (status_list != null)
                {
                    lblStatus.Text = status_list.Where(c => c.Key == lbl_Status_WDSP.Text).FirstOrDefault().Value;
                }
            }
        }
        private void BinsStatusWarrentyDetailsTaxDet()
        {
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            foreach (GridViewRow rw in grdStatusWiseTaxDet.Rows)
            {
                Label lblStatus = rw.FindControl("lblStatus") as Label;
                Label lbl_Status_SWTD = rw.FindControl("lbl_Status_SWTD") as Label;
                if (status_list != null)
                {
                    lblStatus.Text = status_list.Where(c => c.Key == lbl_Status_SWTD.Text).FirstOrDefault().Value;
                }
            }
        }
        private void BinsStatusServiceShedul()
        {
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            foreach (GridViewRow rw in grdServiceShedule.Rows)
            {
                Label lblStatus = rw.FindControl("lblStatus") as Label;
                Label lbl_Status_SS = rw.FindControl("lbl_Status_SS") as Label;
                if (status_list != null)
                {
                    lblStatus.Text = status_list.Where(c => c.Key == lbl_Status_SS.Text).FirstOrDefault().Value;
                }
            }
        }
        #endregion

        protected void btnsearchblall_Click(object sender, EventArgs e)
        {
            try
            {

                grdbldata.DataSource = new int[] { };
                grdbldata.DataBind();

                string itemcode = txtitemcode2.Text.ToString();
                string model = txtmodel2.Text.ToString();
                string brand = txtbrand2.Text.ToString();
                string blno = txtblno2.Text.ToString();
                string bondno = txttobond2.Text.ToString();
                DateTime fdate = Convert.ToDateTime(txtbldate2.Text.ToString());
                DateTime tdate = Convert.ToDateTime(txtbltodate2.Text.ToString());
                string com = Session["UserCompanyCode"].ToString();

                List<BLtracker> bldataall = CHNLSVC.Inventory.GetBlTrackerData(com, fdate, tdate, blno, itemcode, bondno, model);

                grdbldata.DataSource = bldataall;
                grdbldata.DataBind();


            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnitencode2_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Item2";
                Session["Item2"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Item2"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                Label2.Text = "Item2";
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnmodelsearch2_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Model2";
                Session["Model2"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Model2"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "Model2";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnbrandsearch2_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Brand2";
                Session["Brand2"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Brand2"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "Brand2";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtncat12search_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Main2";
                Session["CAT_Main2"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Main2"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "CAT_Main2";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtncat22search_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub12";
                Session["CAT_Sub12"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub12"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "CAT_Sub12";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtncat32search_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub22";
                Session["CAT_Sub22"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub22"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "CAT_Sub22";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnblsearch2_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, null, null);

                _result.Columns.Remove("ib_doc_rec_dt");
                Session["Tobond_bl"] = _result;
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                _result.Columns.Remove("BL qty");
                _result.Columns.Remove("request qty");
                _result.Columns.Remove("File #");
                _result.Columns.Remove("LC #");
                BindUCtrlDDLData(_result);
                lblSearchType.Text = "Tobond_bl";

                ItemPopup.Show();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtntobondsearch2_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, null, null);

                _result.Columns.Remove("ib_doc_rec_dt");
                Session["tobond"] = _result;
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                _result.Columns.Remove("BL qty");
                _result.Columns.Remove("request qty");
                _result.Columns.Remove("File #");
                _result.Columns.Remove("LC #");
                BindUCtrlDDLData(_result);
                lblSearchType.Text = "tobond";

                ItemPopup.Show();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnselectitm_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
                Label ib_doc_no = row.FindControl("ib_doc_no") as Label;
                Label ibi_itm_cd = row.FindControl("ibi_itm_cd") as Label;
                Label ib_ref_no = row.FindControl("ib_ref_no") as Label;
                txtitemcode2.Text = ibi_itm_cd.Text.ToString();
                txttobond2.Text = ib_ref_no.Text.ToString();
                string sino = ib_doc_no.Text.ToString();
                DataTable dt = CHNLSVC.Inventory.SP_GETENTRYREQDATA(Session["UserCompanyCode"].ToString(),sino);
                if (txtitemcode2.Text.ToString() != "")
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "itri_itm_cd='" + txtitemcode2.Text.ToString()+"'";
                    dt = dv.ToTable();
                }
                int k = 0;
                DataTable RESDATA = CHNLSVC.Inventory.SP_GETENTRYRESDATA(sino, ib_ref_no.Text.ToString(), ibi_itm_cd.Text.ToString());
                if (RESDATA.Rows.Count >0)
                {
                    foreach (var resdatalist in RESDATA.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itr_req_no"] = RESDATA.Rows[k]["ird_res_no"].ToString();
                        dr["itri_itm_cd"] = RESDATA.Rows[k]["ird_itm_cd"].ToString();
                        dr["itri_qty"] = RESDATA.Rows[k]["ird_res_qty"].ToString();
                        dr["itr_dt"] = RESDATA.Rows[k]["resdt"].ToString();
                        dt.Rows.Add(dr);
                        k++;
                    }
                }

                grdreqentry.DataSource = dt;
                grdreqentry.DataBind();
            }catch(Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnItmBond_Click(object sender, EventArgs e)
        {
            try
            {
                bool b10144 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10144);
                if (!b10144)
                {
                    DispMsg("Sorry, You have no permission! ( Advice: Required permission code : 10144)"); return;
                }

                //lblSearchType.Text = "ItemRes";

                GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
                Label lblCOMPANY = row.FindControl("lblCOMPANY") as Label;
                Label lblLocation = row.FindControl("lblLocation") as Label;
                Label lblITEM_CODE = row.FindControl("lblITEM_CODE") as Label;
                Label lblITEM_STATUS = row.FindControl("lblITEM_STATUS") as Label;
                Label lblMODEL_CODE = row.FindControl("lblMODEL_CODE") as Label;
                Label lblRES_QTY = row.FindControl("lblRES_QTY") as Label;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl_2);
                DataTable _result = CHNLSVC.Inventory.GET_EnquiryBond(Session["UserCompanyCode"].ToString(), Session["UserSBU"].ToString(), "TO", null, 1, lblITEM_CODE.Text, null, lblLocation.Text);
                if (_result.Rows.Count > 0)
                {
                    _result.Columns.Remove("ib_doc_rec_dt");
                    ViewState["SEARCH"] = _result;
                    grdResultBond.DataSource = _result;
                    grdResultBond.DataBind();
                    UserPopoupBond.Show();
                }
                else
                {
                    string _Msg = "No data found";
                    DisplayMessage(_Msg, 2);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }


        private void LoadAllocationDetails()
        {
            try
            {
                

                List<InventoryAllocateDetails> _Allocation = new List<InventoryAllocateDetails>();
                InventoryAllocateDetails _allobj = new InventoryAllocateDetails();
                _allobj.isa_chnl = txtchannelAlloc.Text.Trim().ToUpper();
                _allobj.isa_itm_cd = txtItem3.Text.Trim().ToUpper();
                _allobj.isa_loc = txtLocAlloc.Text.Trim().ToUpper();
                _Allocation = CHNLSVC.Inventory.GET_STOCK_ALOC_DATA_TRCK(_allobj);
                grdStockAllo.DataSource = _Allocation.Where(x=>x.Isa_aloc_bqty >0).ToList();
                grdStockAllo.DataBind();
               

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2);
              
           
            }
           

        }

        protected void lbtnItem_Click(object sender, EventArgs e)
        {

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = result;
                dgvResultItem.DataBind();
                BindUCtrlDDLData(result);
                Label2.Text = "Item3";
                lblSearchType.Text = "Item3";
                Session["Item3"] = result;
                ItemPopup.Show();
                return;
        }

        protected void txtItem3_TextChanged(object sender, EventArgs e)
        {
            MasterItem _Mstitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem3.Text);
            if (_Mstitem != null)
            {
                if (_Mstitem == null && string.IsNullOrEmpty(_Mstitem.Mi_cd))
                {
                    DisplayMessage("Please check the item code", 2);
                    txtItem3.Text = string.Empty;
                }
            }
            else
            {
                DisplayMessage("Please check the item code", 2);
                txtItem3.Text = string.Empty;
            }
        }
        protected void txtchannelAlloc_TextChanged(object sender, EventArgs e)
        {
            DataTable result = CHNLSVC.Inventory.GetChannelDetail(Session["UserCompanyCode"].ToString(), txtchannelAlloc.Text);
            if (result.Rows.Count == 0)
            {
                // ErrorMsg("Please enter a valid channel..!");
                DisplayMessage("Please enter a valid channel..!",2);
                txtChannel.Text = "";
                return;
            }
        }

        protected void lbtnchannel_Click(object sender, EventArgs e)
        {

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
            DataTable _result = CHNLSVC.CommonSearch.GetChanalDetailsNew(SearchParams, null, null);
            dgvResultItem.DataSource = _result;
            dgvResultItem.DataBind();
            BindUCtrlDDLData(_result);
            Label2.Text = "Channel2";
            lblSearchType.Text = "Channel2";
            Session["Channel2"] = _result;
            ItemPopup.Show();
        }

        protected void txtLocAlloc_TextChanged(object sender, EventArgs e)
        {
            txtLocAlloc.Text = txtLocAlloc.Text.ToUpper().Trim();
            DataTable result = new DataTable();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtLocAlloc.Text);
            if (result.Rows.Count == 0)
            {
                txtLocAlloc.Text = string.Empty;
                DisplayMessage("Please enter valid location", 2);
                txtLocAlloc.Text = string.Empty;
            }
        }

        protected void lbtnloc_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            dgvResultItem.DataSource = result;
            dgvResultItem.DataBind();
            Label2.Text = "locationall";
            lblSearchType.Text = "locationall";
            BindUCtrlDDLData(result);
            Session["locationall"] = result;
            txtSearchbyword.Focus();
            ItemPopup.Show();

        }
        private void ShowPkgQuntity()
        {
            if (Session["UserCompanyCode"].ToString() == "ARL")
            {
                if (dgvItemDetails.Rows.Count > 0)
                {
                    Label lblHdrPkgQty = dgvItemDetails.HeaderRow.FindControl("lblHdrPkgQty") as Label;
                    decimal _tmpDesimal = 0, Qty = 0, pkgQty = 0, _modPkgQty = 0, _pickQty = 0; ;
                    List<UnitConvert> _unitConvList = new List<UnitConvert>();
                    foreach (GridViewRow _row in dgvItemDetails.Rows)
                    {
                        LinkButton lbtnPkgQtyShow = _row.FindControl("lbtnPkgQtyShow") as LinkButton;
                        Label lblPkgQty = _row.FindControl("lblPkgQty") as Label;
                        Label lblMODEL_CODE = _row.FindControl("lblMODEL_CODE") as Label;
                        Label lblITEM_CODE = _row.FindControl("lblITEM_CODE") as Label;
                        string cell_1_Value = _row.Cells[6].Text;
                        Qty = decimal.TryParse(cell_1_Value, out _tmpDesimal) ? Convert.ToDecimal(cell_1_Value) : 0;
                        if (!string.IsNullOrEmpty(lblMODEL_CODE.Text))
                        {
                            lblPkgQty.Visible = true;
                            lbtnPkgQtyShow.Visible = true;
                            _unitConvList = new List<UnitConvert>();
                            List<UnitConvert> _unitConvert = CHNLSVC.Inventory.GET_UNIT_CONVERTER_DATA(new UnitConvert()
                            {
                                mmu_model = lblMODEL_CODE.Text.Trim(),
                                mmu_com = Session["UserCompanyCode"].ToString()
                            });
                            if (_unitConvert.Count > 0)
                            {
                                pkgQty = (Qty / _unitConvert[0].mmu_qty);
                                _row.Cells[12].Text = _unitConvert[0].mmu_model_uom;
                                lblPkgQty.Text = pkgQty.ToString("N2");
                            }
                            else
                            {
                                lblPkgQty.Text = Qty.ToString("N2");
                            }
                        }
                    }
                    lblHdrPkgQty.Visible = true;
                }
            }
        }

        protected void lbtnPkgQtyShow_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _tmpDesimal = 0, Qty = 0;
                GridViewRow _row = ((GridViewRow)((LinkButton)sender).NamingContainer);
                List<UnitConvert> _unitConvList = new List<UnitConvert>();
                Label lblMODEL_CODE = _row.FindControl("lblMODEL_CODE") as Label;
                Label lblITEM_CODE = _row.FindControl("lblITEM_CODE") as Label;
                string cell_1_Value = _row.Cells[6].Text;
                Qty = decimal.TryParse(cell_1_Value, out _tmpDesimal) ? Convert.ToDecimal(cell_1_Value) : 0;
                if (!string.IsNullOrEmpty(lblMODEL_CODE.Text))
                {
                    _unitConvList = new List<UnitConvert>();
                    List<UnitConvert> _unitConvert = CHNLSVC.Inventory.GET_UNIT_CONVERTER_DATA(new UnitConvert()
                    {
                        mmu_model = lblMODEL_CODE.Text.Trim(),
                        mmu_com = Session["UserCompanyCode"].ToString()
                    });
                    if (_unitConvert.Count > 0)
                    {
                        _unitConvert = _unitConvert.OrderBy(c => c.mmu_con_uom).ToList();
                        DataTable _dt = new DataTable();
                        _dt.Columns.Add("Item");
                        _dt.Columns.Add("Model");
                        _dt.Columns.Add("PkgTp");
                        _dt.Columns.Add("Quantity");
                        foreach (var item in _unitConvert)
                        {
                            DataRow _dr = _dt.NewRow();
                            _dr["Item"] = lblITEM_CODE.Text;
                            _dr["Model"] = lblMODEL_CODE.Text;
                            _dr["PkgTp"] = item.mmu_con_uom;
                            _dr["Quantity"] = (Qty/item.mmu_qty).ToString("N2");
                            _dt.Rows.Add(_dr);
                        }
                        dgvModelDetails.DataSource = _dt;
                        dgvModelDetails.DataBind();
                        popModelTp.Show();
                    }
                    else
                    {
                        DispMsg("Item model measuring types not found ! ");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnModelPopClose_Click(object sender, EventArgs e)
        {

        }
    }
}