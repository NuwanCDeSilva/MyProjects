using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using FF.BusinessObjects.General;

namespace FastForward.SCMWeb.View.Additional
{
    public partial class BackDate : BasePage
    {
        protected List<string> ADDED_LIST { get { return (List<string>)Session["ADDED_LIST"]; } set { Session["ADDED_LIST"] = value; } }
        protected List<string> selected_Module_list { get { return (List<string>)Session["selected_Module_list"]; } set { Session["selected_Module_list"] = value; } }
        protected DataTable select_PC_List { get { return (DataTable)Session["select_PC_List"]; } set { Session["select_PC_List"] = value; } }
        protected DataTable select_LOC_List { get { return (DataTable)Session["select_LOC_List"]; } set { Session["select_LOC_List"] = value; } }
        protected DataTable dt { get { return (DataTable)Session["dt"]; } set { Session["dt"] = value; } }
        protected Int16 _IsdayEnd { get { return (Int16)Session["_IsdayEnd"]; } set { Session["_IsdayEnd"] = value; } }

        private Int32 testValue = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clearAll();
            }
        }

        protected void lbtnprintord_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void clearAll()
        {
            Session["ISShow"] = null;
            TreeView1.Nodes.Clear();
            DataTable dt_tree = CHNLSVC.Security.GetUserSystemMenus("ALL", "ALL");
            TreeNode TN = new TreeNode();
            TN.Value = "m";
            TN.Text = "Modules";
            TreeView1.Nodes.Add(TN);
            ADD_CHILD(ref TN, TN.Value.ToString());
            //TreeView1.CollapseAll();
            TreeView1.ExpandAll();

            grvProfCents.DataSource = new int[] { };
            grvProfCents.DataBind();

            GridAllLocations.DataSource = new int[] { };
            GridAllLocations.DataBind();

            grvHistoryBackDates.DataSource = new int[] { };
            grvHistoryBackDates.DataBind();

            TextBoxBDAFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            TextBoxBDATo.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            TextBoxBDVFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt");
            TextBoxBDVTo.Text = DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt");

            ADDED_LIST = new List<string>();
            selected_Module_list = new List<string>();
            select_PC_List = new DataTable();
            select_LOC_List = new DataTable();
            dt = new DataTable();

            ucProfitCenterSearch.ClearAllTextBoxs(true);
            ucLoactionSearch.ClearText(true);

            hdfSeletedTab.Value = "0";
            _IsdayEnd = 0;
            UncheckAllNodes(TreeView1.Nodes);
        }

        private void ADD_CHILD(ref TreeNode parentNode, string parentNodeName)
        {
            try
            {
                DataTable dt = CHNLSVC.Security.Get_childMenus("SHOWBACKDATEONLY", parentNodeName);
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode TN_CHILD = new TreeNode();
                    TreeNode TN = new TreeNode();
                    TN_CHILD.Value = dr["SSM_NAME"].ToString();
                    TN_CHILD.Text = dr["SSM_DISP_NAME"].ToString();
                    if ((Convert.ToInt32(dr["SSM_ISALLOWBACKDT"] == DBNull.Value ? "0" : dr["SSM_ISALLOWBACKDT"]).ToString()) == "1")
                    {
                        TN.ToolTip = "1";
                    }
                    else
                    {
                        TN.ToolTip = "0";
                    }
                    parentNode.ChildNodes.Add(TN_CHILD);
                    ADD_CHILD(ref TN_CHILD, TN_CHILD.Value.ToString());

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
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
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error occurred while processing. Please contact IT department');", true);
            }
            else if (option == 5)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Can not proceed. Please contact the IT department');", true);
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

        #region Add Profit Centers
        protected void btnAddPC_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucProfitCenterSearch.Company;
                string chanel = ucProfitCenterSearch.Channel;
                string subChanel = ucProfitCenterSearch.SubChannel;
                string area = ucProfitCenterSearch.Area;
                string region = ucProfitCenterSearch.Regien;
                string zone = ucProfitCenterSearch.Zone;
                string pc = ucProfitCenterSearch.ProfitCenter;

                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                if (dt.Rows.Count == 0)
                {
                    DisplayMessage("Cannot find any profit centers according to the given values");
                    return;
                }
                foreach (DataRow drr in dt.Rows)
                {
                    string itmcd = drr["PROFIT_CENTER"].ToString();
                    var _duplicate = from _dup in select_PC_List.AsEnumerable()
                                     where _dup["PROFIT_CENTER"].ToString() == itmcd
                                     select _dup;
                    if (_duplicate.Count() != 0)
                    {
                        DisplayMessage("Profit center(s) already added.");
                        grvProfCents.DataSource = select_PC_List;
                        grvProfCents.DataBind();
                        return;
                    }
                }
                //-----------------------------------------------------
                select_PC_List.Merge(dt);
                grvProfCents.DataSource = new int[] { };
                grvProfCents.DataBind();

                grvProfCents.AutoGenerateColumns = false;
                grvProfCents.DataSource = select_PC_List;
                grvProfCents.DataBind();

                btnAllPc_Click(null, null);

                grvHistoryBackDates.DataSource = null;
                grvHistoryBackDates.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnAllPc_Click(object sender, EventArgs e)
        {
            if (grvProfCents.Rows.Count > 0)
            {
                for (int i = 0; i < grvProfCents.Rows.Count; i++)
                {
                    GridViewRow dr = grvProfCents.Rows[i];
                    CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                    chkSelectPC.Checked = true;
                }
            }
        }

        protected void btnNonePc_Click(object sender, EventArgs e)
        {
            if (grvProfCents.Rows.Count > 0)
            {
                for (int i = 0; i < grvProfCents.Rows.Count; i++)
                {
                    GridViewRow dr = grvProfCents.Rows[i];
                    CheckBox chkSelectPC = dr.FindControl("chkSelectPC") as CheckBox;
                    chkSelectPC.Checked = false;
                }
            }
        }

        protected void btnClearPc_Click(object sender, EventArgs e)
        {
            select_PC_List = new DataTable();
            grvProfCents.DataSource = new Int32[] { };
            grvProfCents.DataBind();
        }

        protected void btnRemoveProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblPROFIT_CENTER = dr.FindControl("lblPROFIT_CENTER") as Label;
                select_PC_List.Rows.Remove(select_PC_List.Select("PROFIT_CENTER = '" + lblPROFIT_CENTER.Text.Trim() + "'")[0]);
                grvProfCents.DataSource = select_PC_List;
                grvProfCents.DataBind();
                btnAllPc_Click(null, null);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        #endregion

        #region Add Locations
        protected void btnAddLoc_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucLoactionSearch.Company;
                string chanel = ucLoactionSearch.Channel;
                string subChanel = ucLoactionSearch.SubChannel;
                string area = ucLoactionSearch.Area;
                string region = ucLoactionSearch.Regien;
                string zone = ucLoactionSearch.Zone;
                string pc = ucLoactionSearch.ProfitCenter;

                DataTable dt = CHNLSVC.Inventory.GetLOC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                //-----------------------------------------------------
                foreach (DataRow drr in dt.Rows)
                {
                    string itmcd = drr["LOCATION"].ToString();
                    // string descirption = drr["mi_shortdesc"].ToString();
                    var _duplicate = from _dup in select_LOC_List.AsEnumerable()
                                     where _dup["LOCATION"].ToString() == itmcd
                                     select _dup;
                    if (_duplicate.Count() != 0)
                    {
                        DisplayMessage("Location(s) already added.");
                        return;
                    }
                }
                //-----------------------------------------------------           

                select_LOC_List.Merge(dt);

                GridAllLocations.AutoGenerateColumns = false;
                GridAllLocations.DataSource = select_LOC_List;
                GridAllLocations.DataBind();

                btnAllLoc_Click(null, null);
                grvHistoryBackDates.AutoGenerateColumns = false;
                grvHistoryBackDates.DataSource = new int[] { };
                grvHistoryBackDates.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnRemoveLocation_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblLOCATION = dr.FindControl("lblLOCATION") as Label;
                select_LOC_List.Rows.Remove(select_LOC_List.Select("LOCATION = '" + lblLOCATION.Text.Trim() + "'")[0]);
                GridAllLocations.DataSource = select_LOC_List;
                GridAllLocations.DataBind();
                btnAllLoc_Click(null, null);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnAllLoc_Click(object sender, EventArgs e)
        {
            if (GridAllLocations.Rows.Count > 0)
            {
                for (int i = 0; i < GridAllLocations.Rows.Count; i++)
                {
                    GridViewRow dr = GridAllLocations.Rows[i];
                    CheckBox chkSelectLoc = dr.FindControl("chkSelectLoc") as CheckBox;
                    chkSelectLoc.Checked = true;
                }
            }
        }

        protected void btnNonLoc_Click(object sender, EventArgs e)
        {
            if (GridAllLocations.Rows.Count > 0)
            {
                for (int i = 0; i < GridAllLocations.Rows.Count; i++)
                {
                    GridViewRow dr = GridAllLocations.Rows[i];
                    CheckBox chkSelectLoc = dr.FindControl("chkSelectLoc") as CheckBox;
                    chkSelectLoc.Checked = false;
                }
            }

        }

        protected void btnClearLoc_Click(object sender, EventArgs e)
        {
            select_LOC_List = new DataTable();
            GridAllLocations.DataSource = new int[] { };
            GridAllLocations.DataBind();
        }

        #endregion

        #region Search
        protected void btnCloseSearchMP_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            mpSearch.Hide();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (lblvalue.Text == "Loc_HIRC_Company")
                {
                    TextBoxCompany_Other.Text = grdResult.SelectedRow.Cells[1].Text;
                    TextBoxCompany_Other_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Loc_HIRC_Channel")
                {
                    TextBoxChannel_Channal.Text = grdResult.SelectedRow.Cells[1].Text;
                    TextBoxChannel_Channal_TextChanged(null, null);
                }
                else if (lblvalue.Text == "OPE")
                {
                    txtOPE_code.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtOPE_code_TextChanged(null, null);
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
                if (lblvalue.Text == "Loc_HIRC_Company")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                    DataTable result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Loc_HIRC_Company";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Loc_HIRC_Channel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    DataTable result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Loc_HIRC_Channel";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "OPE")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
                    DataTable result = CHNLSVC.CommonSearch.GetOPE(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "OPE";
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(TextBoxCompany_Other.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
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

        protected void btnComSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                DataTable result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Loc_HIRC_Company";
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

        protected void TextBoxCompany_Other_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
            if (!string.IsNullOrEmpty(TextBoxCompany_Other.Text))
            {
                DataTable result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, "CODE", TextBoxCompany_Other.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {
                    TextBoxCompany_Other_desc.Text = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("Please enter valid company code");
                    TextBoxCompany_Other.Text = string.Empty;
                    TextBoxCompany_Other_desc.Text = string.Empty;
                }
            }
            else
            {
                TextBoxCompany_Other.Text = string.Empty;
                TextBoxCompany_Other_desc.Text = string.Empty;
            }
        }

        protected void TextBoxChannel_Channal_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxCompany_Other.Text.Trim() == "")
            {
                DisplayMessage("Enter Company Code");
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
            DataTable result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, "CODE", TextBoxChannel_Channal.Text.Trim());
            if (result != null && result.Rows.Count > 0)
            {
                TextBoxChannel_Channal_desc.Text = result.Rows[0][1].ToString();
            }
            else
            {
                DisplayMessage("Please enter valid channel code");
                TextBoxChannel_Channal.Text = string.Empty;
                TextBoxChannel_Channal_desc.Text = string.Empty;
            }
        }

        protected void btnChannalSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxCompany_Other.Text.Trim() == "")
                {
                    DisplayMessage("Enter Company Code");
                    return;
                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Loc_HIRC_Channel";
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

        protected void txtOPE_code_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnOprationAdmi_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxCompany_Other.Text.Trim() == "")
                {
                    DisplayMessage("Enter Company Code");
                    return;
                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
                DataTable result = CHNLSVC.CommonSearch.GetOPE(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "OPE";
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 SelectedIndex = Convert.ToInt32(hdfSeletedTab.Value);
                List<string> wrondModuleList = new List<string>();
                GetSelected_AllNodes(TreeView1.Nodes);
                isAllowModules(out wrondModuleList);
                string wrongModule_concat = "";
                foreach (string module in wrondModuleList)
                {
                    wrongModule_concat = wrongModule_concat + "\n" + module;
                }
                if (wrondModuleList.Count > 0)
                {
                    DisplayMessage("Following modules are not allowed for profit center backdates!\n" + wrongModule_concat);
                    if (SelectedIndex == 0)
                    {
                        DisplayMessage("Following modules are not allowed for profit center backdates!\n" + wrongModule_concat);
                    }
                    if (SelectedIndex == 1)
                    {
                        DisplayMessage("Following modules are not allowed for location backdates!\n" + wrongModule_concat);
                    }
                    return;
                }
                //**********************Check Permission***********************
                if (SelectedIndex == 0)
                {
                    if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "BDPC") == false)
                    {
                        DisplayMessage("Sorry, You have no permission to give back-dates for profit centers!\n( Advice: Required permission code :BDPC)");
                        return;
                    }
                }
                else if (SelectedIndex == 1)
                {
                    if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "BDLOC") == false)
                    {
                        DisplayMessage("Sorry, You have no permission to give back-dates for locations!\n( Advice: Required permission code :BDLOC)");
                        return;
                    }
                }
                else if (SelectedIndex == 2)
                {
                    if (rbtOptions.SelectedValue == "rdoCompany")
                    {
                        if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "BDCOM") == false)
                        {
                            DisplayMessage("Sorry, You have no permission to give back-dates for Company!\n( Advice: Required permission code :BDCOM)");
                            return;
                        }
                    }
                    if (rbtOptions.SelectedValue == "rdoChannel")
                    {
                        if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "BDCHNL") == false)
                        {
                            DisplayMessage("Sorry, You have no permission to give back-dates for Channels!\n( Advice: Required permission code :BDCHNL)");
                            return;
                        }
                    }
                    if (rbtOptions.SelectedValue == "rdoOPE")
                    {
                        if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "BDCOPE") == false)
                        {
                            DisplayMessage("Sorry, You have no permission to give back-dates for locations!\n( Advice: Required permission code :BDCOPE)");
                            return;
                        }
                    }
                }
                //*************************************************************

                //added on 21-03-2013----------------------
                //TextBoxBDAFrom.Checked = true;
                //TextBoxBDATo.Checked = true;

                //TextBoxBDVFrom.Checked = true;
                //TextBoxBDVTo.Checked = true;

                if (!CheckValidDateTime(TextBoxBDAFrom.Text))
                {
                    DisplayMessage("Select a valid date and time for 'Back Date Valid From'");
                    TextBoxBDAFrom.Focus();
                    return;
                }
                if (!CheckValidDateTime(TextBoxBDATo.Text))
                {
                    DisplayMessage("Select a valid date and time for 'Back Date Valid To'");
                    TextBoxBDATo.Focus();
                    return;
                }

                if (Convert.ToDateTime(TextBoxBDAFrom.Text.ToString()).Date > Convert.ToDateTime(TextBoxBDATo.Text.ToUpper()).Date)
                {
                    DisplayMessage("'Allowed From' Date cannot be greater than 'Allowed To' Date");
                    return;
                }
                if (Convert.ToDateTime(TextBoxBDVFrom.Text.ToUpper()) >= Convert.ToDateTime(TextBoxBDVTo.Text.ToUpper()))
                {
                    DisplayMessage("'Valid From' cannot be greater than 'Valid To'");
                    return;
                }
               
                #region Get PC_List or Loc_List

                List<string> pc_list = null;
                // if(rdo_Pc.Checked==true )
                if (SelectedIndex == 0)//profit centers
                {
                    pc_list = GetSelectedPCList(); //SELECTED PROFIT CENTER LIST
                    if (pc_list.Count < 1)
                    {
                        DisplayMessage("Please select profit center(s)!");
                        return;
                    }
                }
                //if(rdo_Loc.Checked==true)
                List<string> loc_list = null;
                if (SelectedIndex == 1)//locations
                {
                    loc_list = GetSelectedLocationList(); //SELECTED LOCATION LIST
                    if (loc_list.Count < 1)
                    {
                        DisplayMessage("Please select location(s)!");
                        return;
                    }
                }
                if (SelectedIndex == 2)//Other
                {
                    if (rbtOptions.SelectedItem == null)
                    {
                        DisplayMessage("Please select one option!");
                        return;
                    }
                    //-----------------------------------
                    if (rbtOptions.SelectedValue == "rdoCompany")
                    {
                        if (string.IsNullOrEmpty(TextBoxCompany_Other.Text))
                        {
                            DisplayMessage("Please enter Company!");
                            return;
                        }
                    }
                    //----------------------------------
                    if (rbtOptions.SelectedValue == "rdoOPE")
                    {
                        if (txtOPE_code.Text == "")
                        {
                            DisplayMessage("Please enter OPE code!");
                            return;
                        }
                    }
                    //----------------------------------
                    if (rbtOptions.SelectedValue == "rdoChannel")
                    {
                        if (string.IsNullOrEmpty(TextBoxChannel_Channal.Text))
                        {
                            DisplayMessage("Please enter Channel!");
                            return;
                        }
                        if (string.IsNullOrEmpty(TextBoxCompany_Other.Text))
                        {
                            DisplayMessage("Please enter Company and Channel!");
                            return;
                        }
                    }
                }
                #endregion

                if (selected_Module_list.Count < 1)
                {
                    DisplayMessage("Please select module(s)!");
                    return;
                }

                List<BackDates> backDateList = new List<BackDates>();
                MasterLocation _loc;
                MasterProfitCenter _pc;

                string company = string.Empty;
                if (SelectedIndex == 0)
                {
                    company = ucProfitCenterSearch.Company;
                }
                else
                {
                    company = ucLoactionSearch.Company;
                }

                bool isAllowTxn = false;
                if (chkNotAllowToday.Checked)
                {
                    isAllowTxn = true;
                }
                else
                {
                    isAllowTxn = false;
                }
                string err = string.Empty;
                #region add validation to chk period close or not add by lakshan 24 Mar 2017
                List<RefPrdMt> _perBlockList = CHNLSVC.Inventory.GET_REF_PRD_MT_DATA(new RefPrdMt()
                { 
                    Prd_stus = "CLOSE",
                    Prd_com_cd  = company ,
                    Prd_from = Convert.ToDateTime(TextBoxBDAFrom.Text),
                    Prd_to = Convert.ToDateTime(TextBoxBDATo.Text)
                });
                if (_perBlockList.Count > 0)
                {
                    DisplayMessage("Selected period is closed !"); return;
                }
                #endregion
                Int32 result = CHNLSVC.General.BackDateProcess(company, Session["UserID"].ToString(), Session["SessionID"].ToString(), selected_Module_list, pc_list, loc_list, SelectedIndex, isAllowTxn, Convert.ToDateTime(TextBoxBDVFrom.Text), Convert.ToDateTime(TextBoxBDVTo.Text), Convert.ToDateTime(TextBoxBDAFrom.Text).Date, Convert.ToDateTime(TextBoxBDATo.Text).Date, TextBoxCompany_Other.Text.Trim(), TextBoxChannel_Channal.Text.Trim(), txtOPE_code.Text.Trim(), Convert.ToInt32(rbtOptions.SelectedIndex), out err);
                if (result > 0)
                {
                    DisplayMessage("Module(s) are backdated successfully!");
                    ImgBtnViewBD_Click(null, null);
                    ucLoactionSearch.ClearText();
                    ucProfitCenterSearch.ClearAllTextBoxs();
                }
                else
                {
                    DisplayMessage(err, 5);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void isAllowModules(out List<string> wrondModuleList)
        {
            Int32 SelectedIndex = Convert.ToInt32(hdfSeletedTab.Value);
            List<string> wrondModuleList_return = new List<string>();
            try
            {
                //List<TreeNode> checked_NodesList = CheckedNodes(treeView1.Nodes);
                foreach (string oSelectedItem in selected_Module_list)
                {
                    List<SystemMenus> list = new List<SystemMenus>();
                    CHNLSVC.Security.Get_Menu(oSelectedItem, out list);
                    if (SelectedIndex == 0)
                    {
                        if (list.Count > 0)
                        {
                            if (list[0].Ssm_menu_tp != "F")
                            {
                                if (list[0].Ssm_menu_tp != "" && list[0].Ssm_menu_tp != "m")
                                {
                                    wrondModuleList_return.Add(list[0].Ssm_disp_name.ToString());
                                }
                            }
                        }

                    }
                    if (SelectedIndex == 1)
                    {
                        if (list.Count > 0)
                        {
                            if (list[0].Ssm_menu_tp != "I")
                            {
                                if (list[0].Ssm_menu_tp != "" && list[0].Ssm_menu_tp != "m")
                                {
                                    wrondModuleList_return.Add(list[0].Ssm_disp_name.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            wrondModuleList = wrondModuleList_return;
        }

        //List<TreeNode> CheckedNodes(TreeView theNodes)
        //{
        //    List<TreeNode> checkedNodes = new List<TreeNode>();
        //    if (theNodes != null)
        //    {
        //        foreach (TreeNodeCollection aNode in theNodes.Nodes)
        //        {
        //            if (aNode.Checked)
        //            {
        //                checkedNodes.Add(aNode);
        //            }
        //            checkedNodes.AddRange(CheckedNodes(aNode.Nodes));
        //        }
        //    }
        //    return checkedNodes;
        //}

        public void GetSelected_AllNodes(TreeNodeCollection nodes)
        {
            selected_Module_list = new List<string>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked == true)
                {
                    if (node.Value != "m")
                    {
                        selected_Module_list.Add(node.Value);
                    }
                }
                GetCheckedChildren(node);
            }
        }

        private void GetCheckedChildren(TreeNode rootNode)
        {
            foreach (TreeNode node in rootNode.ChildNodes)
            {
                GetCheckedChildren(node);
                if (node.Checked == true)
                {
                    selected_Module_list.Add(node.Value);
                }
            }
        }

        private List<string> GetSelectedPCList()
        {
            List<string> list = new List<string>();
            foreach (GridViewRow dgvr in grvProfCents.Rows)
            {
                CheckBox chkSelectPC = dgvr.FindControl("chkSelectPC") as CheckBox;
                Label lblPROFIT_CENTER = dgvr.FindControl("lblPROFIT_CENTER") as Label;
                if (chkSelectPC.Checked)
                {
                    list.Add(lblPROFIT_CENTER.Text.Trim());
                }

            }
            return list;
        }

        private List<string> GetSelectedLocationList()
        {
            List<string> list = new List<string>();
            foreach (GridViewRow dgvr in GridAllLocations.Rows)
            {
                CheckBox chkSelectLoc = dgvr.FindControl("chkSelectLoc") as CheckBox;
                Label lblLOCATION = dgvr.FindControl("lblLOCATION") as Label;
                if (chkSelectLoc.Checked)
                {
                    list.Add(lblLOCATION.Text.Trim());
                }

            }
            return list;
        }

        private List<string> IsFinalizedModule()
        {
            Int32 SelectedIndex = Convert.ToInt32(hdfSeletedTab.Value);
            List<string> Fin_pc_list = new List<string>();
            List<string> pc_list = null;
            if (SelectedIndex == 0)//profit centers
            {
                pc_list = GetSelectedPCList(); //SELECTED PROFIT CENTER LIST
                if (pc_list.Count < 1)
                {
                    return Fin_pc_list;
                }
            }
            else
            {
                return Fin_pc_list;
            }

            foreach (string PC in pc_list)
            {
                DateTime FRMdt = Convert.ToDateTime(TextBoxBDAFrom.Text);
                Boolean IS_Finalized = CHNLSVC.Financial.Is_PC_Finalized(ucProfitCenterSearch.Company, PC, FRMdt);
                if (IS_Finalized == true)
                {
                    Fin_pc_list.Add(PC);
                }
            }
            return Fin_pc_list;
        }

        protected void ImgBtnViewBD_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                Int32 SelectedIndex = Convert.ToInt32(hdfSeletedTab.Value);
                string pc_or_loc = string.Empty;
                List<string> pc_list = new List<string>();
                List<string> loc_list = new List<string>();
                string COMPANY = "";
                if (SelectedIndex == 0)
                {
                    pc_or_loc = "PC";
                    pc_list = GetSelectedPCList(); //SELECTED PROFIT CENTER LIST
                    if (pc_list.Count < 1)
                    {
                        DisplayMessage("Please select profit center(s)!");
                        return;
                    }
                    COMPANY = ucProfitCenterSearch.Company;
                    try
                    {
                        List<BackDates> backDatesList = new List<BackDates>();
                        DataTable final_dt = new DataTable();
                        foreach (string pc in pc_list)
                        {
                            DataTable dt = new DataTable();
                            dt = CHNLSVC.General.Get_backdates(COMPANY, pc, pc_or_loc, out backDatesList);
                            final_dt.Merge(dt);
                        }
                        grvHistoryBackDates.DataSource = null;
                        grvHistoryBackDates.AutoGenerateColumns = false;
                        grvHistoryBackDates.DataSource = final_dt;
                        grvHistoryBackDates.DataBind();
                        for (int i = 0; i < grvHistoryBackDates.Rows.Count; i++)
                        {
                            GridViewRow dr = grvHistoryBackDates.Rows[i];
                            Label lblJan = dr.FindControl("lblJan") as Label;
                            if (lblJan.Text == "1")
                            {
                                lblJan.Text = "Yes";
                            }
                            else
                            {
                                lblJan.Text = "No";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(ex.Message, 4);
                        return;
                    }
                }
                else if (SelectedIndex == 1)
                {
                    pc_or_loc = "LOC";
                    loc_list = GetSelectedLocationList(); //SELECTED LOCATION LIST
                    if (loc_list.Count < 1)
                    {
                        DisplayMessage("Please select location(s)!");
                        return;
                    }
                    COMPANY = ucLoactionSearch.Company;
                    try
                    {
                        List<BackDates> backDatesList = new List<BackDates>();
                        DataTable final_dt = new DataTable();
                        foreach (string loc in loc_list)
                        {
                            DataTable dt = new DataTable();
                            dt = CHNLSVC.General.Get_backdates(COMPANY, loc, pc_or_loc, out backDatesList);
                            final_dt.Merge(dt);
                        }
                        grvHistoryBackDates.DataSource = null;
                        grvHistoryBackDates.AutoGenerateColumns = false;
                        grvHistoryBackDates.DataSource = final_dt;
                        grvHistoryBackDates.DataBind();

                        for (int i = 0; i < grvHistoryBackDates.Rows.Count; i++)
                        {
                            GridViewRow dr = grvHistoryBackDates.Rows[i];
                            Label lblJan = dr.FindControl("lblJan") as Label;
                            if (lblJan.Text == "1")
                            {
                                lblJan.Text = "Yes";
                            }
                            else
                            {
                                lblJan.Text = "No";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(ex.Message, 4);
                        return;
                    }

                }
                else if (SelectedIndex == 2)
                {
                    DisplayMessage("Search option is not allowed for advance tab. Select Profit Centers or Locations!");
                    return;
                }
                #endregion
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        private bool CheckValidDateTime(string dateTimeValue)
        {
            DateTime newValue;
            bool result = DateTime.TryParse(dateTimeValue, out newValue);
            return result;
        }

        public void UncheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                CheckChildren(node, false);
            }
        }

        private void CheckChildren(TreeNode rootNode, bool isChecked)
        {

            foreach (TreeNode node in rootNode.ChildNodes)
            {
                CheckChildren(node, isChecked);
                node.Checked = isChecked;
                node.Text = "<FONT COLOR='blue'>" + node.Text + "</FONT>";
            }
        }
    }
}