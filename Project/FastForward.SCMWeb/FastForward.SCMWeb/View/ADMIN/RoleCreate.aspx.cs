using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Drawing;
using System.Text;
using FF.WindowsERPClient;
using FastForward.SCMWeb.UserControls;
using FastForward.SCMWeb.Services;

namespace FastForward.SCMWeb.View.ADMIN
{
    public partial class RoleCreate : BasePage
    {
        List<string> selected_Module_list = new List<string>();
        string Select_company = "";
        string _userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PopulateDropDowns();

                    Select_company = ddlCompany.SelectedValue.ToString();
                    Session["SelectCompany"] = Select_company;

                    txtRoleID.Attributes.Add("onKeyPress", "doClick('" + lbtnSearchRoleNew.ClientID + "',event)");
                    txtRoleIDGrant.Attributes.Add("onKeyPress", "doClick('" + lbtnSearchRoleGrant.ClientID + "',event)");
                    txtRoleIDview.Attributes.Add("onKeyPress", "doClick('" + lbtnRoleIDView.ClientID + "',event)");
                    txtRoleID_opt.Attributes.Add("onKeyPress", "doClick('" + lbtnSearchRoleIDopt.ClientID + "',event)");
                    txtRoleIDLoc.Attributes.Add("onKeyPress", "doClick('" + btnRoleLoc.ClientID + "',event)");
                    this.Menu1.Items[0].Selected = true;
                    this.Menu2.Items[0].Selected = true;
                    Session["TREELOADED"] = null;
                    FillEmptryGrids();
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
                grvRoleVeiwDet.DataSource = null;
                grvRoleVeiwDet.DataBind();

                grvUserRole.DataSource = null;
                grvUserRole.DataBind();

                grvViewRoleMenus.DataSource = null;
                grvViewRoleMenus.DataBind();

                grvGrpOpt.DataSource = null;
                grvGrpOpt.DataBind();

                grvLocs.DataSource = null;
                grvLocs.DataBind();

                gvRoleLoc.DataSource = null;
                gvRoleLoc.DataBind();

                grvParty.DataSource = null;
                grvParty.DataBind();

                grvRoleChnl.DataSource = null;
                grvRoleChnl.DataBind();

                grvPCs.DataSource = null;
                grvPCs.DataBind();

                grvRolePC.DataSource = null;
                grvRolePC.DataBind();

                grvParty2.DataSource = null;
                grvParty2.DataBind();

                grvRolePCChnl.DataSource = null;
                grvRolePCChnl.DataBind();
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
                List<MasterCompany> ListCom = CHNLSVC.General.GetALLMasterCompaniesData();
                ddlCompany.DataSource = ListCom;
                ddlCompany.DataTextField = "Mc_desc";
                ddlCompany.DataValueField = "Mc_cd";

                if (ListCom != null)
                {
                    ddlCompany.DataBind();
                }
                ddlCompany.Items.Insert(0, new ListItem("Select", ""));
                ddlCompany.SelectedIndex = 0;

                List<MasterCompany> ListComGrant = CHNLSVC.General.GetALLMasterCompaniesData();
                ddlCompayGrant.DataSource = ListComGrant;
                ddlCompayGrant.DataTextField = "Mc_desc";
                ddlCompayGrant.DataValueField = "Mc_cd";

                if (ListComGrant != null)
                {
                    ddlCompayGrant.DataBind();
                }
                ddlCompayGrant.Items.Insert(0, new ListItem("Select", ""));
                ddlCompayGrant.SelectedIndex = 0;

                List<MasterCompany> ListComVeiw = CHNLSVC.General.GetALLMasterCompaniesData();
                ddlComView.DataSource = ListComVeiw;
                ddlComView.DataTextField = "Mc_desc";
                ddlComView.DataValueField = "Mc_cd";

                if (ListComVeiw != null)
                {
                    ddlComView.DataBind();
                }
                ddlComView.Items.Insert(0, new ListItem("Select", ""));
                ddlComView.SelectedIndex = 0;

                List<MasterCompany> ListComRoleOpt = CHNLSVC.General.GetALLMasterCompaniesData();
                ddlCompanyRoleOpt.DataSource = ListComRoleOpt;
                ddlCompanyRoleOpt.DataTextField = "Mc_desc";
                ddlCompanyRoleOpt.DataValueField = "Mc_cd";

                if (ListComRoleOpt != null)
                {
                    ddlCompanyRoleOpt.DataBind();
                }
                ddlCompanyRoleOpt.Items.Insert(0, new ListItem("Select", ""));
                ddlCompanyRoleOpt.SelectedIndex = 0;

                List<MasterCompany> ListComLOC = CHNLSVC.General.GetALLMasterCompaniesData();
                ddlCompanyLoc.DataSource = ListComLOC;
                ddlCompanyLoc.DataTextField = "Mc_desc";
                ddlCompanyLoc.DataValueField = "Mc_cd";

                if (ListComLOC != null)
                {
                    ddlCompanyLoc.DataBind();
                }
                ddlCompanyLoc.Items.Insert(0, new ListItem("Select", ""));
                ddlCompanyLoc.SelectedIndex = 0;

                List<MasterCompany> ListComPC = CHNLSVC.General.GetALLMasterCompaniesData();
                ddlCompanyPC.DataSource = ListComPC;
                ddlCompanyPC.DataTextField = "Mc_desc";
                ddlCompanyPC.DataValueField = "Mc_cd";

                if (ListComPC != null)
                {
                    ddlCompanyPC.DataBind();
                }
                ddlCompanyPC.Items.Insert(0, new ListItem("Select", ""));
                ddlCompanyPC.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                    Session["TREELOADED"] =null; 
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                } 
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["SelectCompany"] = null;
                if (ddlCompany.SelectedIndex == 0)
                {
                    return;
                }

                GridLoad();

                chkNewRole.Checked = true;
                chkNewRole.Checked = false;
                Select_company = ddlCompany.SelectedValue.ToString();
                Session["SelectCompany"] = Select_company;

                ddlCompayGrant.SelectedValue = Select_company;
                ddlComView.SelectedValue = Select_company;
                ddlCompanyRoleOpt.SelectedValue = Select_company;
                ddlCompanyLoc.SelectedValue = Select_company;
                ddlCompanyPC.SelectedValue = Select_company;

                txtRoleID.Text = string.Empty;
                txtRoleName.Text = string.Empty;
                txtRoleDesc.Text = string.Empty;
                chkIsActiveRole.Checked = false;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void GridLoad()
        {
            try
            {
                List<SystemRole> roles_list = CHNLSVC.Security.GetRoleByCompany(ddlCompany.SelectedValue.ToString(), 1);
                grvRoleVeiwDet.DataSource = null;
                grvRoleVeiwDet.AutoGenerateColumns = false;
                grvRoleVeiwDet.DataSource = roles_list;

                if (roles_list != null)
                {
                    grvRoleVeiwDet.DataBind();
                }
                else
                {
                    grvRoleVeiwDet.DataSource = null;
                    grvRoleVeiwDet.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void BuildTree()
        {
            try
            {
                DataTable dt_tree = CHNLSVC.Security.GetUserSystemMenus("WEB", "WEB");
                TreeNode TN = new TreeNode();
                TN.Value = "m";
                TN.Text = "Modules";
                treeView1.Nodes.Add(TN);
                ADD_CHILD(ref TN, TN.Value.ToString());
                treeView1.CollapseAll();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void ADD_CHILD(ref TreeNode parentNode, string parentNodeName)
        {
            try
            {
                DataTable dt = CHNLSVC.Security.Get_childMenus("SHOWWEBALL", parentNodeName);
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
        protected void Button17_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedroleid = (string)Session["SELECTEDROLEID"];

                string sessionrolecreation = (string)Session["ROLECREATION_TAB"];
                string sessiongrantprivil = (string)Session["GRANTPRVIL_TAB"];
                string sessionvwroleusers = (string)Session["VIEWROLEUSERS_TAB"];
                string sessiongrantroleopt = (string)Session["GRANTROLEOPT_TAB"];
                string sessionassignloc = (string)Session["ASSIGNLOC_TAB"];
                string sessionassignpc = (string)Session["ASSIGNLOC_PC"];

                if (!string.IsNullOrEmpty(sessionrolecreation))
                {
                    txtRoleID.Text = selectedroleid;
                    txtRoleID_TextChanged();
                }
                else if (!string.IsNullOrEmpty(sessiongrantprivil))
                {
                    txtRoleIDGrant.Text = selectedroleid;
                    txtRoleIDGrant_TextChanged();
                }
                else if (!string.IsNullOrEmpty(sessionvwroleusers))
                {
                    txtRoleIDview.Text = selectedroleid;
                    txtRoleIDview_TextChanged();
                }
                else if (!string.IsNullOrEmpty(sessiongrantroleopt))
                {
                    txtRoleID_opt.Text = selectedroleid;
                    txtRoleID_opt_TextChanged();
                }
                else if (!string.IsNullOrEmpty(sessionassignloc))
                {
                    txtRoleIDLoc.Text = selectedroleid;
                    txtRoleIDLoc_TextChanged1();
                }
                else if (!string.IsNullOrEmpty(sessionassignpc))
                {
                    txtRoleIDPC.Text = selectedroleid;
                    txtRoleIDPC_TextChanged1();
                }

                Session["ROLECREATION_TAB"] = null;
                Session["GRANTPRVIL_TAB"] = null;
                Session["VIEWROLEUSERS_TAB"] = null;
                Session["GRANTROLEOPT_TAB"] = null;
                Session["ASSIGNLOC_TAB"] = null;
                Session["ASSIGNLOC_PC"] = null;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void txtRoleIDLoc_TextChanged1()
        {
            string error = string.Empty;
            DataTable dtroleloc;
            DataTable dtrolechanl;
            try
            {
                if (txtRoleIDLoc.Text.Trim() != "")
                {
                    Int32 roleID = Convert.ToInt32(txtRoleIDLoc.Text.Trim());
                    gvRoleLoc.DataSource = null;
                    gvRoleLoc.AutoGenerateColumns = false;
                    dtroleloc = CHNLSVC.Security.Get_Sec_role_loc(ddlCompanyLoc.SelectedValue.ToString(), roleID, string.Empty);
                    gvRoleLoc.DataSource = dtroleloc;

                    if (dtroleloc.Rows.Count > 0)
                    {
                        gvRoleLoc.DataBind();
                    }

                    grvRoleChnl.DataSource = null;
                    grvRoleChnl.AutoGenerateColumns = false;
                    dtrolechanl = CHNLSVC.Security.Get_Sec_role_locChannel(ddlCompanyLoc.SelectedValue.ToString(), roleID, string.Empty);
                    grvRoleChnl.DataSource = dtrolechanl;

                    if (dtrolechanl.Rows.Count > 0)
                    {
                        grvRoleChnl.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                error = "Error Occurred while processing";
                if (!string.IsNullOrEmpty(error))
                {
                    Response.Redirect("~/Error.aspx?Error=" + error + "");
                }
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRoleID_opt_TextChanged()
        {
            string error = string.Empty;
            try
            {
                string ROLE_ID = txtRoleID_opt.Text.Trim() == "" ? "-1" : txtRoleID_opt.Text.Trim();
                if (txtRoleID_opt.Text.Trim() == "")
                {
                    return;
                }

                SystemRole ROLE = CHNLSVC.Security.GetRoleByCode(ddlCompanyRoleOpt.SelectedValue.ToString(), Convert.ToInt32(ROLE_ID));
                if (ROLE != null)
                {
                    txtRoleName_opt.Text = ROLE.RoleName;
                    txtRoleDesc_opt.Text = ROLE.Description;
                    chkActRole_opt.Checked = ROLE.IsActive == 1 ? true : false;
                }
                else
                {
                    error = "Invalid role ID";
                    Response.Redirect("~/Error.aspx?Error=" + error + "");
                    return;
                }
                this.btnGetOptForRole_Click(null, null);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(error))
                {
                    Response.Redirect("~/Error.aspx?Error=" + error + "");
                }
                else
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
        }

        private void txtRoleIDview_TextChanged()
        {
            try
            {
                txtViewRoleDesc.Text = "";
                grvViewRoleMenus.DataSource = null;
                grvViewRoleMenus.AutoGenerateColumns = false;
                grvViewRoleMenus.DataBind();

                grvUserRole.DataSource = null;
                grvUserRole.AutoGenerateColumns = false;
                grvUserRole.DataBind();

                if (txtRoleIDview.Text.Trim() != "" && ddlComView.SelectedIndex != -1)
                {
                    this.ImgBtnViewBD_Click(null, null);
                }
                SystemRole sysrl = CHNLSVC.Security.GetRoleByCode(ddlComView.SelectedValue.ToString(), Convert.ToInt32(txtRoleIDview.Text.Trim()));

                if (sysrl != null)
                {
                    txtViewRoleDesc.Text = sysrl.Description;
                }
            }
            catch (Exception ex)
            {
                txtViewRoleDesc.Text = "";
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void loadRole_grantedMenus()
        {
            try
            {
                Int32 roleID = Convert.ToInt32(txtRoleIDGrant.Text.Trim());
                string select_com = ddlCompayGrant.SelectedValue.ToString();
                CallRecursive(treeView1);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void CallRecursive(TreeView treeView)
        {
            try
            {
                TreeNodeCollection nodes = treeView.Nodes;
                foreach (TreeNode n in nodes)
                {
                    PrintRecursive(n);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void PrintRecursive(TreeNode treeNode)
        {
            try
            {
                Int32 roleID = Convert.ToInt32(txtRoleIDGrant.Text.Trim());
                string module_name = treeNode.Value.ToString();
                List<SystemMenus> menus = new List<SystemMenus>();
                CHNLSVC.Security.Get_Menu(module_name, out menus);

                if (menus != null)
                {
                    if (menus.Count > 0)
                    {
                        SystemMenus Menu = menus[0];
                        DataTable dt = CHNLSVC.Security.Get_SystemOptionsData_ByRoleID(ddlCompayGrant.SelectedValue.ToString(), roleID);
                        if (dt != null)
                        {
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (drow["SSRM_OPTID"].ToString() == Menu.Ssm_id.ToString())
                                {
                                    treeNode.Checked = true;
                                    treeNode.Text = "<FONT COLOR='blue'>" + treeNode.Text + "</FONT>";
                                }
                            }
                        }
                    }
                }
                foreach (TreeNode tn in treeNode.ChildNodes)
                {
                    PrintRecursive(tn);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void txtRoleIDGrant_TextChanged()
        {
            //BuildTree();
            string error = string.Empty;
            try
            {
                string ROLE_ID = txtRoleIDGrant.Text.Trim() == "" ? "-1" : txtRoleIDGrant.Text.Trim();
                if (txtRoleIDGrant.Text.Trim() == "")
                {
                    return;
                }

                SystemRole ROLE = CHNLSVC.Security.GetRoleByCode(ddlCompayGrant.SelectedValue.ToString(), Convert.ToInt32(ROLE_ID));
                if (ROLE != null)
                {
                    txtRoleNameGrant.Text = ROLE.RoleName;
                    txtRoleDescGrant.Text = ROLE.Description;
                    txtRoleIDGrant.Text = ROLE.RoleId.ToString();
                    chkActRoleGrant.Checked = ROLE.IsActive == 1 ? true : false;
                    UncheckAllNodes(treeView1.Nodes);
                    loadRole_grantedMenus();
                }
                else
                {
                    error = "Invalid role ID";
                    Response.Redirect("~/Error.aspx?Error=" + error + "");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(error))
                {
                    Response.Redirect("~/Error.aspx?Error=" + error + "");
                }
                else
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
        }

        private void txtRoleID_TextChanged()
        {
            try
            {
                string ROLE_ID = txtRoleID.Text.Trim() == "" ? "-1" : txtRoleID.Text.Trim();

                if (txtRoleID.Text.Trim() != "")
                {
                    chkNewRole.Checked = false;
                }

                SystemRole ROLE = CHNLSVC.Security.GetRoleByCode(ddlCompany.SelectedValue.ToString(), Convert.ToInt32(ROLE_ID));
                if (ROLE == null)
                {
                    ROLE = new SystemRole();
                }
                else
                {
                    txtRoleName.Text = ROLE.RoleName;
                    txtRoleDesc.Text = ROLE.Description;
                    txtRoleID.Text = ROLE.RoleId.ToString();
                    chkIsActiveRole.Checked = Convert.ToBoolean(ROLE.IsActive);
                }

                List<SystemRole> roles = new List<SystemRole>();
                roles.Add(ROLE);

                grvRoleVeiwDet.DataSource = null;
                grvRoleVeiwDet.AutoGenerateColumns = false;
                grvRoleVeiwDet.DataSource = roles;
                grvRoleVeiwDet.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void btnSearchRoleNew_Click(object sender, EventArgs e)
        {
            try
            {
                divfailrole.Visible = false;
                if (ddlCompany.SelectedIndex == 0)
                {
                    divfailrole.Visible = true;
                    lblfailrole.Text = "Please select company first !!!";
                    return;
                }

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(para, null, null);
                dvResultUser.DataSource = _result;

                if (_result.Rows.Count > 0)
                {
                    dvResultUser.DataBind();
                }
                else
                {
                    dvResultUser.DataSource = null;
                    dvResultUser.DataBind();
                }
               
                UserPopoup.Show();

                Session["ROLECREATION_TAB"] = "Yes";
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void chkNewRole_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkNewRole.Checked == true)
                {
                    txtRoleID.Text = "";
                    txtRoleName.Text = "";
                    txtRoleDesc.Text = "";
                    txtRoleName.Enabled = true;
                    lbtnRoleSave.Visible = false;
                    lbtnNewRole.Visible = true;
                    grvRoleVeiwDet.DataSource = null;
                    grvRoleVeiwDet.AutoGenerateColumns = false;
                    grvRoleVeiwDet.DataBind();
                    lbtnSearchRoleNew.Visible = false;
                    txtRoleID.Enabled = false;
                }
                else
                {
                    txtRoleID.Text = "";
                    txtRoleName.Text = "";
                    txtRoleDesc.Text = "";
                    txtRoleName.Enabled = false;
                    lbtnNewRole.Visible = false;
                    lbtnRoleSave.Visible = true;
                    lbtnSearchRoleNew.Visible = true;
                    txtRoleID.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnNewRole_Click(object sender, EventArgs e)
        {
            divfailrole.Visible = false;
            divscrole.Visible = false;
            _userid = (string)Session["UserID"];
            if (confirmnewrole.Value == "Yes")
            {
                try
                {
                    if (ddlCompany.SelectedIndex == 0)
                    {
                        divfailrole.Visible = true;
                        lblfailrole.Text = "Please select a Company !!!";
                        return;
                    }

                    if (txtRoleName.Enabled == true)
                    {
                        if (string.IsNullOrEmpty(txtRoleName.Text))
                        {
                            divfailrole.Visible = true;
                            lblfailrole.Text = "Please enter a new role name !!!";
                            return;
                        }
                    }

                    if (txtRoleDesc.Text == "")
                    {
                        divfailrole.Visible = true;
                        lblfailrole.Text = "Please enter description !!!";
                        return;
                    }

                    SystemRole _userRole = new SystemRole();
                    _userRole.CompanyCode = ddlCompany.SelectedValue.ToString();
                    _userRole.Description = txtRoleDesc.Text;
                    _userRole.IsActive = (chkIsActiveRole.Checked == true) ? 1 : 0;
                    _userRole.CreatedBy = _userid;
                    _userRole.CreatedDate = CHNLSVC.Security.GetServerDateTime();
                    _userRole.ModifiedBy = _userid;
                    _userRole.ModifyedDate = CHNLSVC.Security.GetServerDateTime();
                    _userRole.SessionId = Session["SessionID"].ToString();

                    _userRole.RoleName = txtRoleName.Text.Trim();

                    SystemRole role = CHNLSVC.Security.GetSystemRole_ByRoleData(_userRole);
                    if (role != null)
                    {
                        divfailrole.Visible = true;
                        lblfailrole.Text = "Role name already exists. Please try with a different role name !!!";
                        return;
                    }
                    else
                    {
                        Int32 NEW_RoleID = CHNLSVC.Security.UpdateSystemUserRole_NEW(_userRole);
                        chkNewRole.Checked = false;
                        txtRoleID.Text = NEW_RoleID.ToString();

                        SystemRole ROLENEW = CHNLSVC.Security.GetRoleByCode(ddlCompany.SelectedValue.ToString(), Convert.ToInt32(txtRoleID.Text.Trim()));
                        List<SystemRole> rolesnew = new List<SystemRole>();
                        rolesnew.Add(ROLENEW);
                        grvRoleVeiwDet.DataSource = null;
                        grvRoleVeiwDet.AutoGenerateColumns = false;
                        grvRoleVeiwDet.DataSource = rolesnew;

                        if (rolesnew != null)
                        {
                            grvRoleVeiwDet.DataBind();
                        }

                        if (NEW_RoleID > 0)
                        {
                            Session["SELECTEDROLEID"] = txtRoleID.Text.Trim();
                            Session["ROLECREATION_TAB"] = "Yes";

                            Button17_Click(null, null);
                            divscrole.Visible = true;
                            divfailrole.Visible = false;
                            lblscrole.Text = "Successfully created !!!";

                            Session["SELECTEDROLEID"] = null;
                            Session["ROLECREATION_TAB"] = null;

                            ddlCompayGrant.SelectedIndex = 0;
                            txtRoleIDGrant.Text = string.Empty;
                            txtRoleNameGrant.Text = string.Empty;
                            txtRoleDescGrant.Text = string.Empty;
                            chkActRoleGrant.Checked = false;

                            ddlComView.SelectedIndex = 0;
                            txtRoleIDview.Text = string.Empty;
                            txtViewRoleDesc.Text = string.Empty;

                            ddlCompanyRoleOpt.SelectedIndex = 0;
                            txtRoleID_opt.Text = string.Empty;
                            txtRoleName_opt.Text = string.Empty;
                            txtRoleDesc_opt.Text = string.Empty;
                            chkActRole_opt.Checked = false;

                            ddlCompanyLoc.SelectedIndex = 0;
                            txtRoleIDLoc.Text = string.Empty;

                            ddlCompanyPC.SelectedIndex = 0;
                            txtRoleIDPC.Text = string.Empty;
                        }
                        else
                        {
                            divfailrole.Visible = true;
                            lblfailrole.Text = "Not created !!!";
                        }
                        lbtnSearchRoleNew.Visible = true;
                        lbtnNewRole.Visible = false;
                        lbtnRoleSave.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }

        }

        protected void btnRoleSave_Click(object sender, EventArgs e)
        {
            _userid = (string)Session["UserID"];
            if (confirmrolesave.Value == "Yes")
            {
                divfailrole.Visible = false;
                divscrole.Visible = false;
                try
                {
                    if (ddlCompany.SelectedIndex == 0)
                    {
                        divfailrole.Visible = true;
                        lblfailrole.Text = "Please select a Company !!!";
                        return;
                    }

                    if (txtRoleName.Visible)
                    {
                        if (string.IsNullOrEmpty(txtRoleName.Text))
                        {
                            divfailrole.Visible = true;
                            lblfailrole.Text = "Please enter role name !!!";
                            return;
                        }
                    }
                    if (txtRoleDesc.Text == "")
                    {
                        divfailrole.Visible = true;
                        lblfailrole.Text = "Please enter description !!!";
                        return;
                    }


                    SystemRole _userRole = new SystemRole();
                    _userRole.CompanyCode = ddlCompany.SelectedValue.ToString();
                    _userRole.Description = txtRoleDesc.Text;
                    _userRole.IsActive = (chkIsActiveRole.Checked == true) ? 1 : 0;
                    _userRole.CreatedBy = _userid;
                    _userRole.CreatedDate = CHNLSVC.Security.GetServerDateTime();
                    _userRole.ModifiedBy = _userid;
                    _userRole.ModifyedDate = CHNLSVC.Security.GetServerDateTime();
                    _userRole.SessionId = Session["SessionID"].ToString();
                    _userRole.RoleId = Convert.ToInt32(txtRoleID.Text.Trim());
                    _userRole.RoleName = txtRoleName.Text.Trim();

                    SystemRole role = CHNLSVC.Security.GetSystemRole_ByRoleData(_userRole);
                    if (role != null)
                    {
                        _userRole.RoleId = role.RoleId;
                        _userRole.RoleName = role.RoleName;
                    }
                    else
                    {
                        divfailrole.Visible = true;
                        lblfailrole.Text = "No such role exists to update !!!";
                        return;
                    }

                    Int32 Updated_roleID = CHNLSVC.Security.UpdateSystemUserRole_NEW(_userRole);

                    if (Updated_roleID > 0)
                    {
                        divscrole.Visible = true;
                        divfailrole.Visible = false;
                        lblscrole.Text = "Successfully updated !!!";

                        #region
                        SystemRole ROLE = CHNLSVC.Security.GetRoleByCode(ddlCompany.SelectedValue.ToString(), Updated_roleID);
                        txtRoleName.Text = ROLE.RoleName;
                        txtRoleDesc.Text = ROLE.Description;
                        chkIsActiveRole.Checked = Convert.ToBoolean(ROLE.IsActive);

                        List<SystemRole> roles = new List<SystemRole>();
                        roles.Add(ROLE);

                        grvRoleVeiwDet.DataSource = null;
                        grvRoleVeiwDet.AutoGenerateColumns = false;
                        grvRoleVeiwDet.DataSource = roles;
                        grvRoleVeiwDet.DataBind();
                        #endregion

                        ddlCompayGrant.SelectedIndex = 0;
                        txtRoleIDGrant.Text = string.Empty;
                        txtRoleNameGrant.Text = string.Empty;
                        txtRoleDescGrant.Text = string.Empty;
                        chkActRoleGrant.Checked = false;

                        ddlComView.SelectedIndex = 0;
                        txtRoleIDview.Text = string.Empty;
                        txtViewRoleDesc.Text = string.Empty;

                        ddlCompanyRoleOpt.SelectedIndex = 0;
                        txtRoleID_opt.Text = string.Empty;
                        txtRoleName_opt.Text = string.Empty;
                        txtRoleDesc_opt.Text = string.Empty;
                        chkActRole_opt.Checked = false;

                        ddlCompanyLoc.SelectedIndex = 0;
                        txtRoleIDLoc.Text = string.Empty;

                        ddlCompanyPC.SelectedIndex = 0;
                        txtRoleIDPC.Text = string.Empty;

                    }
                    else
                    {
                        divfailrole.Visible = true;
                        lblfailrole.Text = "Not updated !!!";
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
        }

        private void CheckChildren(TreeNode rootNode, bool isChecked)
        {
            try
            {
                foreach (TreeNode node in rootNode.ChildNodes)
                {
                    CheckChildren(node, isChecked);
                    node.Checked = isChecked;
                    node.Text = "<FONT COLOR='blue'>" + node.Text + "</FONT>";
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        public void UncheckAllNodes(TreeNodeCollection nodes)
        {
            try
            {
                foreach (TreeNode node in nodes)
                {
                    node.Checked = false;
                    CheckChildren(node, false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void ddlCompayGrant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UncheckAllNodes(treeView1.Nodes);
                txtRoleIDGrant.Text = "";
                txtRoleNameGrant.Text = "";
                txtRoleDescGrant.Text = "";
                chkActRoleGrant.Checked = false;

                Session["SelectCompany"] = null;
                if (ddlCompayGrant.SelectedIndex == 0)
                {
                    return;
                }
                Select_company = ddlCompayGrant.SelectedValue.ToString();
                Session["SelectCompany"] = Select_company;

                ddlCompany.SelectedValue = Select_company;
                ddlComView.SelectedValue = Select_company;
                ddlCompanyRoleOpt.SelectedValue = Select_company;
                ddlCompanyLoc.SelectedValue = Select_company;
                ddlCompanyPC.SelectedValue = Select_company;

                txtRoleIDGrant.Text = string.Empty;
                txtRoleNameGrant.Text = string.Empty;
                txtRoleDescGrant.Text = string.Empty;
                chkActRoleGrant.Checked = false;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnSearchRoleGrant_Click(object sender, EventArgs e)
        {
            divfailgrant.Visible = false;
            try
            {
                if (ddlCompayGrant.SelectedIndex == 0)
                {
                    divfailgrant.Visible = true;
                    lblfailgrant.Text = "Please select company first !!!";
                    return;
                }

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(para, null, null);
                dvResultUser.DataSource = _result;

                if (_result.Rows.Count > 0)
                {
                    dvResultUser.DataBind();
                }
                else
                {
                    dvResultUser.DataSource = null;
                    dvResultUser.DataBind();
                }
                UserPopoup.Show();

                Session["GRANTPRVIL_TAB"] = "Yes";
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        public void GetSelected_AllNodes(TreeNodeCollection nodes)
        {
            try
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
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void GetCheckedChildren(TreeNode rootNode)
        {
            try
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
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnGrant_Click(object sender, EventArgs e)
        {
            divfailgrant.Visible = false;
            divscgrant.Visible = false;

            if (ddlCompayGrant.SelectedIndex == 0)
            {
                divfailgrant.Visible = true;
                lblfailgrant.Text = "Please select a Company !!!";
                return;
            }

            if (string.IsNullOrEmpty(txtRoleIDGrant.Text.Trim()))
            {
                divfailgrant.Visible = true;
                lblfailgrant.Text = "Please select a role !!!";
                return;
            }

            if (txtconformmessageValue.Value == "Yes")
            {
                string error = string.Empty;
                try
                {
                    SystemRoleOption _systemRoleOption = new SystemRoleOption();

                    SystemRole _systemRole = new SystemRole();
                    _systemRole.CompanyCode = ddlCompayGrant.SelectedValue.ToString();
                    _systemRole.RoleId = Convert.ToInt32(txtRoleIDGrant.Text.Trim());
                    _systemRoleOption.SystemRole = _systemRole;
                    _systemRoleOption.CreatedBy = Session["UserID"].ToString(); ;
                    _systemRoleOption.IsActive = 1;

                    SystemRole role = CHNLSVC.Security.GetSystemRole_ByRoleData(_systemRole);
                    if (role == null)
                    {
                        error = "Invalid Role";
                        Response.Redirect("~/Error.aspx?Error=" + error + "");
                        return;
                    }

                    List<SystemOption> _systemOptionList = null;
                    SystemOption _sysOption = null;
                    GetSelected_AllNodes(treeView1.Nodes);
                    if (selected_Module_list != null)
                    {
                        _systemOptionList = new List<SystemOption>();
                        foreach (string module_name in selected_Module_list)
                        {
                            List<SystemMenus> menus = new List<SystemMenus>();
                            CHNLSVC.Security.Get_Menu(module_name, out menus);
                            _sysOption = new SystemOption();
                            _sysOption.Ssp_optid = menus[0].Ssm_id;
                            _systemOptionList.Add(_sysOption);
                        }
                    }
                    _systemRoleOption.SystemOptionList = _systemOptionList;

                    int result = CHNLSVC.Security.SaveSelectedSystemOptionsRolePrivillages_NEW(_systemRoleOption);

                    txtRoleIDGrant_TextChanged();

                    System.Threading.Thread.Sleep(1000);
                    divscgrant.Visible = true;
                    divfailgrant.Visible = false;
                    lblscgrant.Text = "Role Privillages Sucessfully updated !!!";

                    ddlCompany.SelectedIndex = 0;
                    txtRoleID.Text = string.Empty;
                    txtRoleName.Text = string.Empty;
                    txtRoleDesc.Text = string.Empty;
                    chkIsActiveRole.Checked = false;

                    ddlComView.SelectedIndex = 0;
                    txtRoleIDview.Text = string.Empty;
                    txtViewRoleDesc.Text = string.Empty;

                    ddlCompanyRoleOpt.SelectedIndex = 0;
                    txtRoleID_opt.Text = string.Empty;
                    txtRoleName_opt.Text = string.Empty;
                    txtRoleDesc_opt.Text = string.Empty;
                    chkActRole_opt.Checked = false;

                    ddlCompanyLoc.SelectedIndex = 0;
                    txtRoleIDLoc.Text = string.Empty;

                    ddlCompanyPC.SelectedIndex = 0;
                    txtRoleIDPC.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        Response.Redirect("~/Error.aspx?Error=" + error + "");
                    }
                    else
                    {
                        Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                    }
                }
            }
            else
            {
                return;
            }
        }

        private List<string> get_selected_Locations()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in grvLocs.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[1].Text.ToString());
                }
            }
            return list;
        }

        protected void btnClearViewGrid_Click(object sender, EventArgs e)
        {
            if (txtconfirmcleargrids.Value == "Yes")
            {
                try
                {
                    grvViewRoleMenus.DataSource = null;
                    grvViewRoleMenus.AutoGenerateColumns = false;
                    grvViewRoleMenus.DataBind();

                    grvUserRole.DataSource = null;
                    grvUserRole.AutoGenerateColumns = false;
                    grvUserRole.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                } 
            }
        }

        protected void ddlComView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grvViewRoleMenus.DataSource = null;
                grvViewRoleMenus.AutoGenerateColumns = false;
                grvViewRoleMenus.DataBind();

                grvUserRole.DataSource = null;
                grvUserRole.AutoGenerateColumns = false;
                grvUserRole.DataBind();
                txtRoleIDview.Text = "";

                Session["SelectCompany"] = null;
                if (ddlComView.SelectedIndex == 0)
                {
                    return;
                }
                Select_company = ddlComView.SelectedValue.ToString();
                Session["SelectCompany"] = Select_company;

                ddlCompany.SelectedValue = Select_company;
                ddlCompayGrant.SelectedValue = Select_company;
                ddlCompanyRoleOpt.SelectedValue = Select_company;
                ddlCompanyLoc.SelectedValue = Select_company;
                ddlCompanyPC.SelectedValue = Select_company;

                txtRoleIDview.Text = string.Empty;
                txtViewRoleDesc.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnRoleIDView_Click(object sender, EventArgs e)
        {
            try
            {
                dvfailroleusr.Visible = false;
                if (ddlComView.SelectedIndex == 0)
                {
                    dvfailroleusr.Visible = true;
                    lblfailvwroleusers.Text = "Please select company first !!!";
                    return;
                }

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(para, null, null);
                dvResultUser.DataSource = _result;

                if (_result.Rows.Count > 0)
                {
                    dvResultUser.DataBind();
                }
                else
                {
                    dvResultUser.DataSource = null;
                    dvResultUser.DataBind();
                }
                UserPopoup.Show();

                Session["VIEWROLEUSERS_TAB"] = "Yes";
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private DataTable UserInRoleGridLoad()
        {
            DataTable dtuserinrole;
            dtuserinrole = CHNLSVC.Security.Get_UsersForRole(ddlComView.SelectedValue.ToString(), Convert.ToInt32(txtRoleIDview.Text.Trim()));
            grvUserRole.DataSource = dtuserinrole;

            if (dtuserinrole.Rows.Count > 0)
            {
                grvUserRole.DataBind();
            }
            else
            {
                grvUserRole.DataSource = null;
                grvUserRole.DataBind();
            }
            return dtuserinrole;
        }

        private DataTable MenusForUserGridLoad()
        {
            DataTable dtvwrolemenus;
            dtvwrolemenus = CHNLSVC.Security.Get_MenusForRole(ddlComView.SelectedValue.ToString(), Convert.ToInt32(txtRoleIDview.Text.Trim()));
            grvViewRoleMenus.DataSource = dtvwrolemenus;

            if (dtvwrolemenus.Rows.Count > 0)
            {
                grvViewRoleMenus.DataBind();
            }
            else
            {
                grvViewRoleMenus.DataSource=null;
                grvViewRoleMenus.DataBind();
            }

            return dtvwrolemenus;
        }
        protected void ImgBtnViewBD_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlComView.SelectedIndex == 0)
                {
                    dvfailroleusr.Visible = true;
                    lblfailvwroleusers.Text = "Please select a Company !!!";
                    return;
                }

                if (string.IsNullOrEmpty(txtRoleIDview.Text.Trim()))
                {
                    dvfailroleusr.Visible = true;
                    lblfailvwroleusers.Text = "Please select a role !!!";
                    return;
                }

                LoadUsersAndMenus();

                ddlCompany.SelectedIndex = 0;
                txtRoleID.Text = string.Empty;
                txtRoleName.Text = string.Empty;
                txtRoleDesc.Text = string.Empty;
                chkIsActiveRole.Checked = false;

                ddlCompayGrant.SelectedIndex = 0;
                txtRoleIDGrant.Text = string.Empty;
                txtRoleNameGrant.Text = string.Empty;
                txtRoleDescGrant.Text = string.Empty;
                chkActRoleGrant.Checked = false;

                ddlCompanyRoleOpt.SelectedIndex = 0;
                txtRoleID_opt.Text = string.Empty;
                txtRoleName_opt.Text = string.Empty;
                txtRoleDesc_opt.Text = string.Empty;
                chkActRole_opt.Checked = false;

                ddlCompanyLoc.SelectedIndex = 0;
                txtRoleIDLoc.Text = string.Empty;

                ddlCompanyPC.SelectedIndex = 0;
                txtRoleIDPC.Text = string.Empty;


            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void LoadUsersAndMenus()
        {
            try
            {
                DataTable dtuserinrole;
                DataTable dtvwrolemenus;

                grvUserRole.DataSource = null;
                grvUserRole.AutoGenerateColumns = false;

                dtuserinrole = UserInRoleGridLoad();

                grvViewRoleMenus.DataSource = null;
                grvViewRoleMenus.AutoGenerateColumns = false;

                dtvwrolemenus = MenusForUserGridLoad();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void ddlCompanyRoleOpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtRoleID_opt.Text = "";
                txtRoleID_opt.Text = "";
                txtRoleName_opt.Text = "";
                txtRoleDesc_opt.Text = "";
                chkActRole_opt.Checked = false;
                GrantRleOptionsGridLoad();

                Session["SelectCompany"] = null;
                if (ddlCompanyRoleOpt.SelectedIndex == 0)
                {
                    return;
                }
                Select_company = ddlCompanyRoleOpt.SelectedValue.ToString();
                Session["SelectCompany"] = Select_company;

                ddlCompany.SelectedValue = Select_company;
                ddlCompayGrant.SelectedValue = Select_company;
                ddlComView.SelectedValue = Select_company;
                ddlCompanyLoc.SelectedValue = Select_company;
                ddlCompanyPC.SelectedValue = Select_company;

                txtRoleID_opt.Text = string.Empty;
                txtRoleName_opt.Text = string.Empty;
                txtRoleDesc_opt.Text = string.Empty;
                chkActRole_opt.Checked = false;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnSearchRoleIDopt_Click(object sender, EventArgs e)
        {
            try
            {
                dvfailgrntroleopt.Visible = false;
                if (ddlCompanyRoleOpt.SelectedIndex == 0)
                {
                    dvfailgrntroleopt.Visible = true;
                    lblfailgrntroleopt.Text = "Please select company first !!!";
                    return;
                }
                Session["GRANTROLEOPT_TAB"] = "Yes";

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(para, null, null);
                dvResultUser.DataSource = _result;

                if (_result.Rows.Count > 0)
                {
                    dvResultUser.DataBind();
                }
                else
                {
                    dvResultUser.DataSource = null;
                    dvResultUser.DataBind();
                }
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void GrantRleOptionsGridLoad()
        {
            try
            {
                DataTable dtsec = new DataTable();
                grvGrpOpt.DataSource = null;
                grvGrpOpt.AutoGenerateColumns = false;
                dtsec = CHNLSVC.Security.Get_SystemOptionsForGroup("");
                grvGrpOpt.DataSource = dtsec;

                if (dtsec.Rows.Count > 0)
                {
                    grvGrpOpt.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void btnGetOptForRole_Click(object sender, EventArgs e)
        {
            try
            {
                dvfailgrntroleopt.Visible = false;
                dvscgrantroleopt.Visible = false;

                if (ddlCompanyRoleOpt.SelectedIndex == 0)
                {
                    dvfailgrntroleopt.Visible = true;
                    lblfailgrntroleopt.Text = "Please select a Company !!!";
                    return;
                }

                if (txtRoleID_opt.Text.Trim() == "")
                {
                    dvfailgrntroleopt.Visible = true;
                    lblfailgrntroleopt.Text = "Select role details !!!";
                    return;
                }

                GrantRleOptionsGridLoad();
                DataTable dt = CHNLSVC.Security.Get_Active_System_OptionsFor_Role(ddlCompanyRoleOpt.SelectedValue.ToString(), Convert.ToInt32(txtRoleID_opt.Text.Trim()));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow tb_row in dt.Rows)
                        {
                            foreach (GridViewRow row in grvGrpOpt.Rows)
                            {
                                Int32 sysOpt = Convert.ToInt32(tb_row["SSRM_OPTID"].ToString());
                                Int32 grdSysOpt = Convert.ToInt32(row.Cells[1].Text.ToString());
                                if (sysOpt == grdSysOpt)
                                {
                                    if (row.RowType == DataControlRowType.DataRow)
                                    {
                                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                                        chkRow.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private List<Int32> get_selected_SystemOptions()
        {
            List<Int32> list = new List<Int32>();

            foreach (GridViewRow r in grvGrpOpt.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(Convert.ToInt32(r.Cells[1].Text.ToString()));
                }
            }
            return list;
        }
        protected void btnSaveRoleOpt_Click(object sender, EventArgs e)
        {
            try
            {
                dvscgrantroleopt.Visible = false;
                dvfailgrntroleopt.Visible = false;
                if (txtconformmessageValue2.Value == "Yes")
                {
                    if (ddlCompanyRoleOpt.SelectedIndex == 0)
                    {
                        dvfailgrntroleopt.Visible = true;
                        lblfailgrntroleopt.Text = "Please select a Company !!!";
                        return;
                    }

                    if (txtRoleID_opt.Text.Trim() == "")
                    {
                        dvfailgrntroleopt.Visible = true;
                        lblfailgrntroleopt.Text = "Select role details !!!";
                        return;
                    }

                    SystemRoleOption _systemRoleOption = new SystemRoleOption();

                    SystemRole _systemRole = new SystemRole();
                    _systemRole.CompanyCode = ddlCompanyRoleOpt.SelectedValue.ToString();
                    _systemRole.RoleId = Convert.ToInt32(txtRoleID_opt.Text.Trim());
                    _systemRoleOption.SystemRole = _systemRole;
                    _systemRoleOption.CreatedBy = Session["UserID"].ToString(); ;
                    _systemRoleOption.IsActive = 1;

                    SystemRole role = CHNLSVC.Security.GetSystemRole_ByRoleData(_systemRole);
                    if (role == null)
                    {
                        dvfailgrntroleopt.Visible = true;
                        lblfailgrntroleopt.Text = "Invalid Role !!!";
                        return;
                    }

                    List<SystemOption> _systemOptionList = null;
                    SystemOption _sysOption = null;

                    List<Int32> list_options = get_selected_SystemOptions();
                    _systemOptionList = new List<SystemOption>();
                    foreach (Int32 op_id in list_options)
                    {
                        _sysOption = new SystemOption();
                        _sysOption.Ssp_optid = op_id;
                        _systemOptionList.Add(_sysOption);
                    }
                    _systemRoleOption.SystemOptionList = _systemOptionList;

                    //System.Threading.Thread.Sleep(1000);
                    
                    Int32 eff = CHNLSVC.Security.Save_System_Options_For_Role(_systemRoleOption, "");

                    if (eff == 1)
                    {
                        dvscgrantroleopt.Visible = true;
                        dvfailgrntroleopt.Visible = false;
                        lblscgrantroleopt.Text = "Successfully saved !!!";

                        ddlCompany.SelectedIndex = 0;
                        txtRoleID.Text = string.Empty;
                        txtRoleName.Text = string.Empty;
                        txtRoleDesc.Text = string.Empty;
                        chkIsActiveRole.Checked = false;

                        ddlCompayGrant.SelectedIndex = 0;
                        txtRoleIDGrant.Text = string.Empty;
                        txtRoleNameGrant.Text = string.Empty;
                        txtRoleDescGrant.Text = string.Empty;
                        chkActRoleGrant.Checked = false;

                        ddlComView.SelectedIndex = 0;
                        txtRoleIDview.Text = string.Empty;
                        txtViewRoleDesc.Text = string.Empty;

                        ddlCompanyLoc.SelectedIndex = 0;
                        txtRoleIDLoc.Text = string.Empty;

                        ddlCompanyPC.SelectedIndex = 0;
                        txtRoleIDPC.Text = string.Empty;
                    }
                    else
                    {
                        dvfailgrntroleopt.Visible = true;
                        lblfailgrntroleopt.Text = "Not saved !!!";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void ddlCompanyLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                if (ddlCompanyLoc.SelectedIndex != -1)
                {
                    Select_company = ddlCompanyLoc.SelectedValue.ToString();
                    this.btnAddPartys_Click(null, null);
                }

                Session["SelectCompany"] = null;
                if (ddlCompanyLoc.SelectedIndex == 0)
                {
                    return;
                }
                Select_company = ddlCompanyLoc.SelectedValue.ToString();
                Session["SelectCompany"] = Select_company;

                ddlCompany.SelectedValue = Select_company;
                ddlCompayGrant.SelectedValue = Select_company;
                ddlComView.SelectedValue = Select_company;
                ddlCompanyRoleOpt.SelectedValue = Select_company;
                ddlCompanyPC.SelectedValue = Select_company;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnRoleLoc_Click(object sender, EventArgs e)
        {
            try
            {
                dvfaileassloc.Visible = false;
                if (ddlCompanyLoc.SelectedIndex == 0)
                {
                    dvfaileassloc.Visible = true;
                    lblassfailloc.Text = "Please select company first !!!";
                    return;
                }

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(para, null, null);
                dvResultUser.DataSource = _result;

                if (_result.Rows.Count > 0)
                {
                    dvResultUser.DataBind();
                }
                else
                {
                    dvResultUser.DataSource = null;
                    dvResultUser.DataBind();
                }
                UserPopoup.Show();

                Session["ASSIGNLOC_TAB"] = "Yes";
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnAddLocs_Click(object sender, EventArgs e)
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

                grvLocs.DataSource = null;
                grvLocs.AutoGenerateColumns = false;
                grvLocs.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    grvLocs.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnAddLoc_Click(object sender, EventArgs e)
        {
            try
            {
                dvscassloc.Visible = false;
                dvfaileassloc.Visible = false;
                List<string> loc_list = get_selected_Locations();

                if (ddlCompanyLoc.SelectedIndex == 0)
                {
                    dvfaileassloc.Visible = true;
                    lblassfailloc.Text = "Please select a Company !!!";
                    return;
                }

                if (txtRoleIDLoc.Text.Trim() == "")
                {
                    dvfaileassloc.Visible = true;
                    lblassfailloc.Text = "Please select a Role ID !!!";
                    return;
                }
                if ((loc_list == null) ||  (loc_list.Count == 0))
                {
                    dvfaileassloc.Visible = true;
                    lblassfailloc.Text = "Please select location(s) !!!";
                    return;
                }

                List<SecRoleLocation> _RoleLocList = new List<SecRoleLocation>();
                foreach (string secloc in loc_list)
                {
                    SecRoleLocation secRloc = new SecRoleLocation();
                    secRloc.Ssrl_act = true;
                    secRloc.Ssrl_com = ucLoactionSearch.Company;
                    secRloc.Ssrl_cre_by = Session["UserID"].ToString();
                    secRloc.Ssrl_cre_dt = DateTime.Now.Date;
                    secRloc.Ssrl_loc = secloc;
                    secRloc.Ssrl_mod_by = Session["UserID"].ToString();
                    secRloc.Ssrl_mod_dt = DateTime.Now.Date; ;
                    secRloc.Ssrl_readonly = true;
                    secRloc.Ssrl_roleid = Convert.ToInt32(txtRoleIDLoc.Text);

                    _RoleLocList.Add(secRloc);
                }
                Int32 eff = CHNLSVC.Security.Save_sec_role_loc(_RoleLocList);
                if (eff > 0)
                {
                    dvscassloc.Visible = true;
                    dvfaileassloc.Visible = false;
                    lblscassloc.Text = "Successfully added !!!";
                    txtRoleIDLoc_TextChanged1();
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "setTimeout('window.scrollTo(0,0)', 0);", true);

                    ddlCompany.SelectedIndex = 0;
                    txtRoleID.Text = string.Empty;
                    txtRoleName.Text = string.Empty;
                    txtRoleDesc.Text = string.Empty;
                    chkIsActiveRole.Checked = false;

                    ddlCompayGrant.SelectedIndex = 0;
                    txtRoleIDGrant.Text = string.Empty;
                    txtRoleNameGrant.Text = string.Empty;
                    txtRoleDescGrant.Text = string.Empty;
                    chkActRoleGrant.Checked = false;

                    ddlComView.SelectedIndex = 0;
                    txtRoleIDview.Text = string.Empty;
                    txtViewRoleDesc.Text = string.Empty;

                    ddlCompanyRoleOpt.SelectedIndex = 0;
                    txtRoleID_opt.Text = string.Empty;
                    txtRoleName_opt.Text = string.Empty;
                    txtRoleDesc_opt.Text = string.Empty;
                    chkActRole_opt.Checked = false;

                    ddlCompanyPC.SelectedIndex = 0;
                    txtRoleIDPC.Text = string.Empty;
                }
                else
                {
                    dvfaileassloc.Visible = true;
                    lblassfailloc.Text = "Not added !!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                string error = "Error Occurred while processing...\n";
                Response.Redirect("~/Error.aspx?Error=" + error + "");
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<string> get_selected_LocUpdate()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in gvRoleLoc.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[3].Text.ToString());
                }
            }
            return list;
        }
        protected void lbtnDelRoleLoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdeletion.Value == "Yes")
                {
                    dvscassloc.Visible = false;
                    dvfaileassloc.Visible = false;

                    List<string> update_roleLocList = get_selected_LocUpdate();
                    List<SecRoleLocation> _RoleLocList = new List<SecRoleLocation>();

                    if ((update_roleLocList == null) || (update_roleLocList.Count == 0))
                    {
                        dvfaileassloc.Visible = true;
                        lblassfailloc.Text = "Please select items to delete !!!";
                        return;
                    }

                    foreach (string secloc in update_roleLocList)
                    {
                        SecRoleLocation secRloc = new SecRoleLocation();
                        secRloc.Ssrl_act = false;
                        secRloc.Ssrl_com = ucLoactionSearch.Company;
                        secRloc.Ssrl_cre_by = Session["UserID"].ToString();
                        secRloc.Ssrl_cre_dt = DateTime.Now.Date;
                        secRloc.Ssrl_loc = secloc;
                        secRloc.Ssrl_mod_by = Session["UserID"].ToString();
                        secRloc.Ssrl_mod_dt = DateTime.Now.Date;
                        secRloc.Ssrl_readonly = true;
                        secRloc.Ssrl_roleid = Convert.ToInt32(txtRoleIDLoc.Text);

                        _RoleLocList.Add(secRloc);
                    }
                    Int32 eff = CHNLSVC.Security.Update_sec_role_loc(_RoleLocList);
                    if (eff > 0)
                    {
                        dvscassloc.Visible = true;
                        dvfaileassloc.Visible = false;
                        lblscassloc.Text = "Successfully updated !!!";
                        txtRoleIDLoc_TextChanged1();

                        ddlCompany.SelectedIndex = 0;
                        txtRoleID.Text = string.Empty;
                        txtRoleName.Text = string.Empty;
                        txtRoleDesc.Text = string.Empty;
                        chkIsActiveRole.Checked = false;

                        ddlCompayGrant.SelectedIndex = 0;
                        txtRoleIDGrant.Text = string.Empty;
                        txtRoleNameGrant.Text = string.Empty;
                        txtRoleDescGrant.Text = string.Empty;
                        chkActRoleGrant.Checked = false;

                        ddlComView.SelectedIndex = 0;
                        txtRoleIDview.Text = string.Empty;
                        txtViewRoleDesc.Text = string.Empty;

                        ddlCompanyRoleOpt.SelectedIndex = 0;
                        txtRoleID_opt.Text = string.Empty;
                        txtRoleName_opt.Text = string.Empty;
                        txtRoleDesc_opt.Text = string.Empty;
                        chkActRole_opt.Checked = false;

                        ddlCompanyPC.SelectedIndex = 0;
                        txtRoleIDPC.Text = string.Empty;
                    }
                    else
                    {
                        dvfaileassloc.Visible = true;
                        lblassfailloc.Text = "Not updated !!!";
                        return;
                    } 
                }
            }
            catch (Exception ex)
            {
                string error = "Error Occurred while processing...\n";
                Response.Redirect("~/Error.aspx?Error=" + error + "");
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserRole:
                    {
                        paramsText.Append(Select_company + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SysOptGroups:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(Select_company + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void btnAddPartys_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable _result = new DataTable();

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(para, null, null);

                grvParty.DataSource = null;
                grvParty.AutoGenerateColumns = false;
                grvParty.DataSource = _result;

                if (_result == null)
                {
                    return;
                }

                if (_result.Rows.Count > 0)
                {
                    grvParty.DataBind();
                }

                if (_result == null)
                {
                    return;
                }
                if (_result.Rows.Count == 0)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private List<string> get_selected_LocChannels()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in grvParty.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[1].Text.ToString());
                }
            }
            return list;
        }
        protected void lbtnAddLocChnl_Click(object sender, EventArgs e)
        {
            try
            {
                dvscassloc.Visible = false;
                dvfaileassloc.Visible = false;
                List<string> locChanel_list = get_selected_LocChannels();

                if (ddlCompanyLoc.SelectedIndex == 0)
                {
                    dvfaileassloc.Visible = true;
                    lblassfailloc.Text = "Please select a Company !!!";
                    return;
                }

                if (txtRoleIDLoc.Text.Trim() == "")
                {
                    dvfaileassloc.Visible = true;
                    lblassfailloc.Text = "Please select a role ID !!!";
                    return;
                }
                if ((locChanel_list == null) || (locChanel_list.Count == 0))
                {
                    dvfaileassloc.Visible = true;
                    lblassfailloc.Text = "'Please select channel(s) !!!";
                    return;
                }

                List<SecRoleLocChanel> _secLocChnlList = new List<SecRoleLocChanel>();
                foreach (string locCh in locChanel_list)
                {
                    SecRoleLocChanel locChanel = new SecRoleLocChanel();
                    locChanel.Ssrl_cre_by = Session["UserID"].ToString();
                    locChanel.Ssrlc_act = true;
                    locChanel.Ssrlc_chnnl = locCh;
                    locChanel.Ssrlc_com = ddlCompanyLoc.SelectedValue.ToString();
                    locChanel.Ssrlc_cre_dt = DateTime.Now.Date;
                    locChanel.Ssrlc_mod_by = Session["UserID"].ToString(); ;
                    locChanel.Ssrlc_mod_dt = DateTime.Now.Date;
                    locChanel.Ssrlc_readonly = true;
                    locChanel.Ssrlc_roleid = Convert.ToInt32(txtRoleIDLoc.Text);

                    _secLocChnlList.Add(locChanel);
                }
                Int32 eff = CHNLSVC.Security.Save_sec_role_LocChanel(_secLocChnlList);
                if (eff > 0)
                {
                    dvscassloc.Visible = true;
                    dvfaileassloc.Visible = false;
                    lblscassloc.Text = "Sucessfully added !!!";
                    txtRoleIDLoc_TextChanged1();

                    ddlCompany.SelectedIndex = 0;
                    txtRoleID.Text = string.Empty;
                    txtRoleName.Text = string.Empty;
                    txtRoleDesc.Text = string.Empty;
                    chkIsActiveRole.Checked = false;

                    ddlCompayGrant.SelectedIndex = 0;
                    txtRoleIDGrant.Text = string.Empty;
                    txtRoleNameGrant.Text = string.Empty;
                    txtRoleDescGrant.Text = string.Empty;
                    chkActRoleGrant.Checked = false;

                    ddlComView.SelectedIndex = 0;
                    txtRoleIDview.Text = string.Empty;
                    txtViewRoleDesc.Text = string.Empty;

                    ddlCompanyRoleOpt.SelectedIndex = 0;
                    txtRoleID_opt.Text = string.Empty;
                    txtRoleName_opt.Text = string.Empty;
                    txtRoleDesc_opt.Text = string.Empty;
                    chkActRole_opt.Checked = false;

                    ddlCompanyPC.SelectedIndex = 0;
                    txtRoleIDPC.Text = string.Empty;
                }
                else
                {
                    dvfaileassloc.Visible = true;
                    lblassfailloc.Text = "Not added !!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                dvfaileassloc.Visible = true;
                lblassfailloc.Text = "Error Occurred while processing !!!";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<string> get_selected_LocChannel_Update()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in grvRoleChnl.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[3].Text.ToString());
                }
            }
            return list;
        }
        protected void lbtnDelLocChnl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdeletion.Value == "Yes")
                {
                    dvfaileassloc.Visible = false;
                    dvscassloc.Visible = false;
                    List<string> locChnl_list = get_selected_LocChannel_Update();
                    List<SecRoleLocChanel> _secLocChnlList = new List<SecRoleLocChanel>();

                    if ((locChnl_list == null) || (locChnl_list.Count == 0))
                    {
                        dvfaileassloc.Visible = true;
                        lblassfailloc.Text = "Please select items to delete !!!";
                        return;
                    }

                    foreach (string locCh in locChnl_list)
                    {
                        SecRoleLocChanel locChanel = new SecRoleLocChanel();
                        locChanel.Ssrl_cre_by = Session["UserID"].ToString();
                        locChanel.Ssrlc_act = false;
                        locChanel.Ssrlc_chnnl = locCh;
                        locChanel.Ssrlc_com = ddlCompanyLoc.SelectedValue.ToString();
                        locChanel.Ssrlc_cre_dt = DateTime.Now.Date;
                        locChanel.Ssrlc_mod_by = Session["UserID"].ToString(); ;
                        locChanel.Ssrlc_mod_dt = DateTime.Now.Date;
                        locChanel.Ssrlc_readonly = true;
                        locChanel.Ssrlc_roleid = Convert.ToInt32(txtRoleIDLoc.Text);

                        _secLocChnlList.Add(locChanel);
                    }
                    Int32 eff = CHNLSVC.Security.Update_sec_role_locChannel(_secLocChnlList);

                    if (eff > 0)
                    {
                        dvscassloc.Visible = true;
                        dvfaileassloc.Visible = false;
                        lblscassloc.Text = "Sucessfully updated !!!";
                        txtRoleIDLoc_TextChanged1();

                        ddlCompany.SelectedIndex = 0;
                        txtRoleID.Text = string.Empty;
                        txtRoleName.Text = string.Empty;
                        txtRoleDesc.Text = string.Empty;
                        chkIsActiveRole.Checked = false;

                        ddlCompayGrant.SelectedIndex = 0;
                        txtRoleIDGrant.Text = string.Empty;
                        txtRoleNameGrant.Text = string.Empty;
                        txtRoleDescGrant.Text = string.Empty;
                        chkActRoleGrant.Checked = false;

                        ddlComView.SelectedIndex = 0;
                        txtRoleIDview.Text = string.Empty;
                        txtViewRoleDesc.Text = string.Empty;

                        ddlCompanyRoleOpt.SelectedIndex = 0;
                        txtRoleID_opt.Text = string.Empty;
                        txtRoleName_opt.Text = string.Empty;
                        txtRoleDesc_opt.Text = string.Empty;
                        chkActRole_opt.Checked = false;

                        ddlCompanyPC.SelectedIndex = 0;
                        txtRoleIDPC.Text = string.Empty;
                    }
                    else
                    {
                        dvfaileassloc.Visible = true;
                        lblassfailloc.Text = "Not updated !!!";
                        return;
                    } 
                }
            }
            catch (Exception ex)
            {
                dvfaileassloc.Visible = true;
                lblassfailloc.Text = "Error Occurred while processing !!!";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void ddlCompanyPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ucProfitCenterSearch.Company = Session["UserCompanyCode"].ToString();
                if (ddlCompanyPC.SelectedIndex != -1)
                {
                    Select_company = ddlCompanyPC.SelectedValue.ToString();
                    ucProfitCenterSearch.Company = Select_company;
                    this.btnAddPartys2_Click(null, null);
                }

                Session["SelectCompany"] = null;
                if (ddlCompanyPC.SelectedIndex == 0)
                {
                    return;
                }
                Select_company = ddlCompanyPC.SelectedValue.ToString();
                Session["SelectCompany"] = Select_company;

                ddlCompany.SelectedValue = Select_company;
                ddlCompayGrant.SelectedValue = Select_company;
                ddlComView.SelectedValue = Select_company;
                ddlCompanyRoleOpt.SelectedValue = Select_company;
                ddlCompanyLoc.SelectedValue = Select_company;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void imgbutton4_Click(object sender, EventArgs e)
        {
            try
            {
                dvfailpc.Visible = false;
                if (ddlCompanyPC.SelectedIndex == 0)
                {
                    dvfailpc.Visible = true;
                    lblfailpc.Text = "Please select company first !!!";
                    return;
                }

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(para, null, null);
                dvResultUser.DataSource = _result;

                if (_result.Rows.Count > 0)
                {
                    dvResultUser.DataBind();
                }
                else
                {
                    dvResultUser.DataSource = null;
                    dvResultUser.DataBind();
                }
                UserPopoup.Show();

                Session["ASSIGNLOC_PC"] = "Yes";
            }
            catch (Exception ex)
            {
                dvfailpc.Visible = true;
                lblfailpc.Text = "Error Occurred while processing !!!";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

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
                grvPCs.DataSource = null;
                grvPCs.AutoGenerateColumns = false;
                grvPCs.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    grvPCs.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private List<string> get_selected_RolePCs()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in grvPCs.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[1].Text.ToString());
                }
            }
            return list;
        }

        private void txtRoleIDPC_TextChanged1()
        {
            try
            {
                dvfailpc.Visible = false;
                DataTable dtrolepc;
                DataTable dtpcchannel;

                if (txtRoleIDPC.Text.Trim() != "")
                {
                    Int32 roleID = Convert.ToInt32(txtRoleIDPC.Text.Trim());
                    grvRolePC.DataSource = null;
                    grvRolePC.AutoGenerateColumns = false;
                    dtrolepc = CHNLSVC.Security.Get_Sec_role_pc(ddlCompanyPC.SelectedValue.ToString(), roleID, string.Empty);
                    grvRolePC.DataSource = dtrolepc;

                    if (dtrolepc.Rows.Count > 0)
                    {
                        grvRolePC.DataBind();
                    }

                    grvRolePCChnl.DataSource = null;
                    grvRolePCChnl.AutoGenerateColumns = false;
                    dtpcchannel = CHNLSVC.Security.Get_Sec_role_pcChannel(ddlCompanyPC.SelectedValue.ToString(), roleID, string.Empty);
                    grvRolePCChnl.DataSource = dtpcchannel;

                    if (dtpcchannel.Rows.Count > 0)
                    {
                        grvRolePCChnl.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                dvfailpc.Visible = true;
                lblfailpc.Text = "Error Occurred while processing !!!";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnAddRolePC_Click(object sender, EventArgs e)
        {
            try
            {
                dvsucpc.Visible = false;
                dvfailpc.Visible = false;
                List<string> rolePC_list = get_selected_RolePCs();

                if (ddlCompanyPC.SelectedIndex == 0)
                {
                    dvfailpc.Visible = true;
                    lblfailpc.Text = "Please select a Company !!!";
                    return;
                }

                if (txtRoleIDPC.Text.Trim() == "")
                {
                    dvfailpc.Visible = true;
                    lblfailpc.Text = "Please select a role ID !!!";
                    return;
                }
                if ((rolePC_list == null) || (rolePC_list.Count == 0))
                {
                    dvfailpc.Visible = true;
                    lblfailpc.Text = "Please select Pc(s) !!!";
                    return;
                }

                List<SecRolePC> _secRolePCList = new List<SecRolePC>();
                foreach (string PC in rolePC_list)
                {
                    SecRolePC rolePC = new SecRolePC();
                    rolePC.Ssrp_act = true;
                    rolePC.Ssrp_com = ddlCompanyPC.SelectedValue.ToString();
                    rolePC.Ssrp_cre_by = Session["UserID"].ToString();
                    rolePC.Ssrp_cre_dt = DateTime.Now.Date;
                    rolePC.Ssrp_mod_by = Session["UserID"].ToString();
                    rolePC.Ssrp_mod_dt = DateTime.Now.Date;
                    rolePC.Ssrp_pc = PC;
                    rolePC.Ssrp_readonly = true;
                    rolePC.Ssrp_roleid = Convert.ToInt32(txtRoleIDPC.Text);

                    _secRolePCList.Add(rolePC);
                }
                Int32 eff = CHNLSVC.Security.Save_sec_role_pc(_secRolePCList);
                if (eff > 0)
                {
                    dvsucpc.Visible = true;
                    dvfailpc.Visible = false;
                    lblsucpc.Text = "Successfully added !!!";
                    txtRoleIDPC_TextChanged1();

                    ddlCompany.SelectedIndex = 0;
                    txtRoleID.Text = string.Empty;
                    txtRoleName.Text = string.Empty;
                    txtRoleDesc.Text = string.Empty;
                    chkIsActiveRole.Checked = false;

                    ddlCompayGrant.SelectedIndex = 0;
                    txtRoleIDGrant.Text = string.Empty;
                    txtRoleNameGrant.Text = string.Empty;
                    txtRoleDescGrant.Text = string.Empty;
                    chkActRoleGrant.Checked = false;

                    ddlComView.SelectedIndex = 0;
                    txtRoleIDview.Text = string.Empty;
                    txtViewRoleDesc.Text = string.Empty;

                    ddlCompanyRoleOpt.SelectedIndex = 0;
                    txtRoleID_opt.Text = string.Empty;
                    txtRoleName_opt.Text = string.Empty;
                    txtRoleDesc_opt.Text = string.Empty;
                    chkActRole_opt.Checked = false;

                    ddlCompanyLoc.SelectedIndex = 0;
                    txtRoleIDLoc.Text = string.Empty;
                }
                else
                {
                    dvfailpc.Visible = true;
                    lblfailpc.Text = "Not added !!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                dvfailpc.Visible = true;
                lblfailpc.Text = "Error Occurred while processing !!!";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<string> get_selected_PC_Update()
        {
            List<string> list = new List<string>();
            foreach (GridViewRow r in grvRolePC.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[3].Text.ToString());
                }
            }
            return list;
        }
        protected void btnDelRolePc_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdeletion.Value == "Yes")
                {
                    dvsucpc.Visible = false;
                    dvfailpc.Visible = false;
                    List<string> rolePC_list = get_selected_PC_Update();
                    List<SecRolePC> _secRolePCList = new List<SecRolePC>();

                    if ((rolePC_list == null) || (rolePC_list.Count == 0))
                    {
                        dvfailpc.Visible = true;
                        lblfailpc.Text = "Please select items to delete !!!";
                        return;
                    }

                    foreach (string PC in rolePC_list)
                    {
                        SecRolePC rolePC = new SecRolePC();
                        rolePC.Ssrp_act = false;
                        rolePC.Ssrp_com = ddlCompanyPC.SelectedValue.ToString();
                        rolePC.Ssrp_cre_by = Session["UserID"].ToString();
                        rolePC.Ssrp_cre_dt = DateTime.Now.Date;
                        rolePC.Ssrp_mod_by = Session["UserID"].ToString();
                        rolePC.Ssrp_mod_dt = DateTime.Now.Date;
                        rolePC.Ssrp_pc = PC;
                        rolePC.Ssrp_readonly = true;
                        rolePC.Ssrp_roleid = Convert.ToInt32(txtRoleIDPC.Text);

                        _secRolePCList.Add(rolePC);
                    }
                    Int32 eff = CHNLSVC.Security.Update_sec_role_PC(_secRolePCList);
                    if (eff > 0)
                    {
                        dvsucpc.Visible = true;
                        dvfailpc.Visible = false;
                        lblsucpc.Text = "Sucessfully updated !!!";
                        txtRoleIDPC_TextChanged1();

                        ddlCompany.SelectedIndex = 0;
                        txtRoleID.Text = string.Empty;
                        txtRoleName.Text = string.Empty;
                        txtRoleDesc.Text = string.Empty;
                        chkIsActiveRole.Checked = false;

                        ddlCompayGrant.SelectedIndex = 0;
                        txtRoleIDGrant.Text = string.Empty;
                        txtRoleNameGrant.Text = string.Empty;
                        txtRoleDescGrant.Text = string.Empty;
                        chkActRoleGrant.Checked = false;

                        ddlComView.SelectedIndex = 0;
                        txtRoleIDview.Text = string.Empty;
                        txtViewRoleDesc.Text = string.Empty;

                        ddlCompanyRoleOpt.SelectedIndex = 0;
                        txtRoleID_opt.Text = string.Empty;
                        txtRoleName_opt.Text = string.Empty;
                        txtRoleDesc_opt.Text = string.Empty;
                        chkActRole_opt.Checked = false;

                        ddlCompanyLoc.SelectedIndex = 0;
                        txtRoleIDLoc.Text = string.Empty;
                    }
                    else
                    {
                        dvfailpc.Visible = true;
                        lblfailpc.Text = "Not updated !!!";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private List<string> get_selected_RolePcChannels()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in grvParty2.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[1].Text.ToString());
                }
            }
            return list;
        }
        protected void btnAddPCchanel_Click(object sender, EventArgs e)
        {
            try
            {
                dvsucpc.Visible = false;
                dvfailpc.Visible = false;
                List<string> rolePcChnl_list = get_selected_RolePcChannels();
                if (txtRoleIDPC.Text.Trim() == "")
                {
                    dvfailpc.Visible = true;
                    lblfailpc.Text = "Please select a role ID !!!";
                    return;
                }
                if ((rolePcChnl_list == null) || (rolePcChnl_list.Count == 0))
                {
                    dvfailpc.Visible = true;
                    lblfailpc.Text = "Please select channel(s) !!!";
                    return;
                }

                List<SecRolePcChannel> _secPcChnlList = new List<SecRolePcChannel>();
                foreach (string pcChnl in rolePcChnl_list)
                {
                    SecRolePcChannel rolePcChn = new SecRolePcChannel();
                    rolePcChn.Ssrpc_act = true;
                    rolePcChn.Ssrpc_chnnl = pcChnl;
                    rolePcChn.Ssrpc_com = ddlCompanyPC.SelectedValue.ToString();
                    rolePcChn.Ssrpc_cre_by = Session["UserID"].ToString();
                    rolePcChn.Ssrpc_cre_dt = DateTime.Now.Date;
                    rolePcChn.Ssrpc_mod_by = Session["UserID"].ToString();
                    rolePcChn.Ssrpc_mod_dt = DateTime.Now.Date;
                    rolePcChn.Ssrpc_readonly = true;
                    rolePcChn.Ssrpc_roleid = Convert.ToInt32(txtRoleIDPC.Text);

                    _secPcChnlList.Add(rolePcChn);
                }

                Int32 eff = CHNLSVC.Security.Save_sec_role_pcchnl(_secPcChnlList);
                if (eff > 0)
                {
                    dvsucpc.Visible = true;
                    dvfailpc.Visible = false;
                    lblsucpc.Text = "Sucessfully added !!!";
                    txtRoleIDPC_TextChanged1();

                    ddlCompany.SelectedIndex = 0;
                    txtRoleID.Text = string.Empty;
                    txtRoleName.Text = string.Empty;
                    txtRoleDesc.Text = string.Empty;
                    chkIsActiveRole.Checked = false;

                    ddlCompayGrant.SelectedIndex = 0;
                    txtRoleIDGrant.Text = string.Empty;
                    txtRoleNameGrant.Text = string.Empty;
                    txtRoleDescGrant.Text = string.Empty;
                    chkActRoleGrant.Checked = false;

                    ddlComView.SelectedIndex = 0;
                    txtRoleIDview.Text = string.Empty;
                    txtViewRoleDesc.Text = string.Empty;

                    ddlCompanyRoleOpt.SelectedIndex = 0;
                    txtRoleID_opt.Text = string.Empty;
                    txtRoleName_opt.Text = string.Empty;
                    txtRoleDesc_opt.Text = string.Empty;
                    chkActRole_opt.Checked = false;

                    ddlCompanyLoc.SelectedIndex = 0;
                    txtRoleIDLoc.Text = string.Empty;
                }
                else
                {
                    dvfailpc.Visible = true;
                    lblfailpc.Text = "Not added !!!";
                    return;
                }
               
            }
            catch (Exception ex)
            {
                dvfailpc.Visible = true;
                lblfailpc.Text = "Error Occurred while processing !!!";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<string> get_selected_PcChannels_Update()
        {
            List<string> list = new List<string>();
            foreach (GridViewRow r in grvRolePCChnl.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[3].Text.ToString());
                }
            }
            return list;
        }
        protected void btnDelPCChnl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdeletion.Value == "Yes")
                {
                    dvsucpc.Visible = false;
                    dvfailpc.Visible = false;
                    List<string> rolePcChnl_list = get_selected_PcChannels_Update();
                    List<SecRolePcChannel> _secPcChnlList = new List<SecRolePcChannel>();

                    if ((rolePcChnl_list == null) || (rolePcChnl_list.Count == 0))
                    {
                        dvfailpc.Visible = true;
                        lblfailpc.Text = "Please select items to delete !!!";
                        return;
                    }

                    foreach (string pcChnl in rolePcChnl_list)
                    {
                        SecRolePcChannel rolePcChn = new SecRolePcChannel();
                        rolePcChn.Ssrpc_act = false;
                        rolePcChn.Ssrpc_chnnl = pcChnl;
                        rolePcChn.Ssrpc_com = ddlCompanyPC.SelectedValue.ToString();
                        rolePcChn.Ssrpc_cre_by = Session["UserID"].ToString();
                        rolePcChn.Ssrpc_cre_dt = DateTime.Now.Date;
                        rolePcChn.Ssrpc_mod_by = Session["UserID"].ToString();
                        rolePcChn.Ssrpc_mod_dt = DateTime.Now.Date;
                        rolePcChn.Ssrpc_readonly = true;
                        rolePcChn.Ssrpc_roleid = Convert.ToInt32(txtRoleIDPC.Text);

                        _secPcChnlList.Add(rolePcChn);
                    }

                    Int32 eff = CHNLSVC.Security.Update_secRolePcChannel(_secPcChnlList);
                    if (eff > 0)
                    {
                        dvsucpc.Visible = true;
                        dvfailpc.Visible = false;
                        lblsucpc.Text = "Sucessfully updated !!!";
                        txtRoleIDPC_TextChanged1();

                        ddlCompany.SelectedIndex = 0;
                        txtRoleID.Text = string.Empty;
                        txtRoleName.Text = string.Empty;
                        txtRoleDesc.Text = string.Empty;
                        chkIsActiveRole.Checked = false;

                        ddlCompayGrant.SelectedIndex = 0;
                        txtRoleIDGrant.Text = string.Empty;
                        txtRoleNameGrant.Text = string.Empty;
                        txtRoleDescGrant.Text = string.Empty;
                        chkActRoleGrant.Checked = false;

                        ddlComView.SelectedIndex = 0;
                        txtRoleIDview.Text = string.Empty;
                        txtViewRoleDesc.Text = string.Empty;

                        ddlCompanyRoleOpt.SelectedIndex = 0;
                        txtRoleID_opt.Text = string.Empty;
                        txtRoleName_opt.Text = string.Empty;
                        txtRoleDesc_opt.Text = string.Empty;
                        chkActRole_opt.Checked = false;

                        ddlCompanyLoc.SelectedIndex = 0;
                        txtRoleIDLoc.Text = string.Empty;
                    }
                    else
                    {
                        dvfailpc.Visible = true;
                        lblfailpc.Text = "Not updated !!!";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnAddPartys2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(para, null, null);

                grvParty2.DataSource = null;
                grvParty2.AutoGenerateColumns = false;
                grvParty2.DataSource = _result;

                if (_result == null)
                {
                    return;
                }
                if (_result.Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    grvParty2.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    Session["SELECTEDROLEID"] = txtSearchbyword.Text.Trim();
                    PopupSearch();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void PopupSearch()
        {
            try
            {
                txtSearchbyword.Attributes.Add("onkeydown", "(event.keyCode==13);");
                dvResultUser.DataSource = null;
                dvResultUser.DataBind();

                string SELECTED_ROLE_ID = (string)Session["SELECTEDROLEID"];

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(para, cmbSearchbykey.SelectedValue, "%" + SELECTED_ROLE_ID);
                dvResultUser.DataSource = _result;

                if (_result.Rows.Count > 0)
                {
                    dvResultUser.DataBind();
                }
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dvResultUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dvResultUser.PageIndex = e.NewPageIndex;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(para, null, null);
                dvResultUser.DataSource = _result;

                if (_result.Rows.Count > 0)
                {
                    dvResultUser.DataBind();
                }
                else
                {
                    dvResultUser.DataSource = null;
                    dvResultUser.DataBind();
                }
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dvResultUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["SELECTEDROLEID"] = null;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string RoleID = dvResultUser.SelectedRow.Cells[1].Text;
                Session["SELECTEDROLEID"] = RoleID;
                Button17_Click(null, null);
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
                PopupSearch();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            divscrole.Visible = false;
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            divfailrole.Visible = false;
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            divscgrant.Visible = false;
        }

        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            divfailgrant.Visible = false;
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            dvscvwroleusers.Visible = false;
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            dvfailroleusr.Visible = false;
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            dvscgrantroleopt.Visible = false;
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            dvfailgrntroleopt.Visible = false;
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            dvscassloc.Visible = false;
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            dvfaileassloc.Visible = false;
        }

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            dvsucpc.Visible = false;
        }

        protected void LinkButton14_Click(object sender, EventArgs e)
        {
            dvfailpc.Visible = false;
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);
        }

        protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
        {
            MultiView2.ActiveViewIndex = Int32.Parse(e.Item.Value);
        }

        protected void lbltree_Click(object sender, EventArgs e)
        {
            try
            {
                string treeloaded = (string)Session["TREELOADED"];

                if (string.IsNullOrEmpty(treeloaded))
                {
                    BuildTree();
                    Session["TREELOADED"] = "Yes"; 
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

    }
}