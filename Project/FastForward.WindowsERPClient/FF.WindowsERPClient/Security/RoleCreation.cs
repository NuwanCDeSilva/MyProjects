using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Diagnostics;

namespace FF.WindowsERPClient.Security
{
    #region new/updated SP s
    //pkg_search.sp_search_system_role = NEW
    //sp_updatesystemrole_new  = NEW
    //get_menusForRole  =NEW
    //sp_getsystem_MENU_usersbyrole =NEW
    //SP_GET_ROLE_byDATA =NEW
    //------------------------------------  
    //sp_get_role_by_company= UPDATE
    //sp_get_role_by_code =UPDATE
    //SP_GET_ROLE_byDATA =NEW
    //sp_getSystemroleMenus =NEW
    //sp_save_SysRole_Menu =NEW
    //sp_updatecurrent_sys_roleMenus =NEW
    //---------------------------          
    //sp_save_sys_role_optins =NEW
    //get_Active_options_OfRole= new
    //pkg_search.sp_search_system_optionGroups= NEW
    //sp_get_sysOptForGrop =new
    //sp_update_sys_roleOPTIONS =new

    #endregion


    //    //pkg_search.sp_search_system_role = NEW
    //    //sp_updatesystemrole_new  = NEW
    //    //get_menusForRole  =NEW
    //    //sp_getsystem_MENU_usersbyrole =NEW
    //    //SP_GET_ROLE_byDATA =NEW
    //    //------------------------------------  
    //    //sp_get_role_by_company= UPDATE
    //    //sp_get_role_by_code =UPDATE
    //    //SP_GET_ROLE_byDATA =NEW
    //    //sp_getSystemroleMenus =NEW

    //    //sp_save_SysRole_Menu =NEW
    //    //sp_updatecurrent_sys_roleMenus =NEW

    //    //---------------------------    
    //    //sp_getSystemroleMenus  =update
    //    //sp_updatecurrent_sys_roleMenus= update
    //    //sp_save_SysRole_Menu =update

    //    //------------------------------

    ////get_menusForRole =UPDATE
    ////sp_getSystemroleMenus  =update
    ////sp_updatecurrent_sys_roleMenus= update
    ////sp_save_SysRole_Menu =update

    ////sp_save_sys_role_optins =NEW
    ////get_Active_options_OfRole= new
    ////pkg_search.sp_search_system_optionGroups= NEW


    //----------Role LOC/PC assign-----------------------------------------
    //sp_save_sec_role_loc =new
    //sp_save_sec_role_locchnl =new
    //sp_save_sec_role_pc =new
    //sp_save_sec_role_pcchnl=new
    //sp_updat_secRoleLoc =NEW
    //sp_update_secRolePC =NEW
    //sp_update_secRoleLocChnl =new
    //sp_update_secRolePcChnl =NEW

    //sp_get_SEC_ROLE_LOC =NEW
    //sp_get_SEC_ROLE_LOC_CHANNEL =new
    //sp_get_SEC_ROLE_PC =new
    //sp_get_SEC_ROLE_PC_CHANNEL =NEW
    //----------------------------------------------------
    public partial class RoleCreation : Base
    {
        //  Boolean isChangePrevi = false;

        Base _base = new Base();
        List<string> selected_Module_list = new List<string>();
        string Select_company = "";
        DataTable dt_tree;

        public RoleCreation()
        {
            InitializeComponent();
            //  isChangePrevi = false;

            List<MasterCompany> ListCom = CHNLSVC.General.GetALLMasterCompaniesData();
            ddlCompany.DataSource = ListCom;
            ddlCompany.DisplayMember = "Mc_desc";
            ddlCompany.ValueMember = "Mc_cd";
            ddlCompany.SelectedIndex = -1;

            List<MasterCompany> ListComGrant = CHNLSVC.General.GetALLMasterCompaniesData();
            ddlCompayGrant.DataSource = ListComGrant;
            ddlCompayGrant.DisplayMember = "Mc_desc";
            ddlCompayGrant.ValueMember = "Mc_cd";
            ddlCompayGrant.SelectedIndex = -1;

            List<MasterCompany> ListComVeiw = CHNLSVC.General.GetALLMasterCompaniesData();
            ddlComView.DataSource = ListComVeiw;
            ddlComView.DisplayMember = "Mc_desc";
            ddlComView.ValueMember = "Mc_cd";
            ddlComView.SelectedIndex = -1;

            //ddlCompanyRoleOpt
            List<MasterCompany> ListComRoleOpt = CHNLSVC.General.GetALLMasterCompaniesData();
            ddlCompanyRoleOpt.DataSource = ListComRoleOpt;
            ddlCompanyRoleOpt.DisplayMember = "Mc_desc";
            ddlCompanyRoleOpt.ValueMember = "Mc_cd";
            ddlCompanyRoleOpt.SelectedIndex = -1;

            List<MasterCompany> ListComLOC = CHNLSVC.General.GetALLMasterCompaniesData();
            ddlCompanyLoc.DataSource = ListComLOC;
            ddlCompanyLoc.DisplayMember = "Mc_desc";
            ddlCompanyLoc.ValueMember = "Mc_cd";
            ddlCompanyLoc.SelectedIndex = -1;


            List<MasterCompany> ListComPC = CHNLSVC.General.GetALLMasterCompaniesData();
            ddlCompanyPC.DataSource = ListComPC;
            ddlCompanyPC.DisplayMember = "Mc_desc";
            ddlCompanyPC.ValueMember = "Mc_cd";
            ddlCompanyPC.SelectedIndex = -1;

            #region
            treeView1.CheckBoxes = true;
            DataTable dt_tree = _base.CHNLSVC.Security.GetUserSystemMenus("ALL", "ALL");

            TreeNode TN = new TreeNode();
            TN.Name = "m";
            TN.Text = "Modules";

            treeView1.Nodes.Add(TN);

            ADD_CHILD(ref TN, TN.Name.ToString());

            treeView1.ExpandAll();

            #endregion
        }

        public void CheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = true;
                node.ForeColor = Color.RoyalBlue;
                CheckChildren(node, true);
            }
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
            foreach (TreeNode node in rootNode.Nodes)
            {
                CheckChildren(node, isChecked);
                node.Checked = isChecked;
                node.ForeColor = Color.RoyalBlue;
            }
        }


        private void PrintRecursive(TreeNode treeNode)
        {
            // Print the node.
            //System.Diagnostics.Debug.WriteLine(treeNode.Text);
            // MessageBox.Show(treeNode.Text);

            Int32 roleID = Convert.ToInt32(txtRoleIDGrant.Text.Trim());
            //-------------------------------------
            string module_name = treeNode.Name.ToString();
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
                                treeNode.ForeColor = Color.Blue;
                            }
                            //else
                            //{
                            //    treeNode.Checked = false;
                            //}
                        }
                    }
                }
            }

            //-------------------------------------
            // Print each node recursively.
            foreach (TreeNode tn in treeNode.Nodes)
            {
                PrintRecursive(tn);
            }
        }

        // Call the procedure using the TreeView.
        private void CallRecursive(TreeView treeView)
        {
            // Print each node recursively.
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                PrintRecursive(n);
            }
        }

        private void loadRole_grantedMenus()
        {
            // //Boolean is_checked = e.Node.Checked;
            //// string menuName = e.Node.Name.ToString();
            // if (is_checked == true)
            // {
            //     selected_Module_list.Add(menuName.ToString());

            //     foreach (TreeNode child in e.Node.Nodes)
            //     {
            //         child.Checked = true;
            //     }
            // }
            // else
            // {
            //     selected_Module_list.Remove(menuName.ToString());

            //     foreach (TreeNode child in e.Node.Nodes)
            //     {
            //         child.Checked = false;
            //     }
            // }

            Int32 roleID = Convert.ToInt32(txtRoleIDGrant.Text.Trim());
            string select_com = ddlCompayGrant.SelectedValue.ToString();

            CallRecursive(treeView1);


        }
        private void ADD_CHILD(ref TreeNode parentNode, string parentNodeName)
        {
            //SHOWBACKDATEONLY
            //SHOWALL
            DataTable dt = CHNLSVC.Security.Get_childMenus("SHOWALL", parentNodeName);
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode TN_CHILD = new TreeNode();
                TreeNode TN = new TreeNode();
                TN_CHILD.Name = dr["SSM_NAME"].ToString();
                TN_CHILD.Text = dr["SSM_DISP_NAME"].ToString(); //dr["SSM_NAME"].ToString();//
                //TN.Tag = Convert.ToInt32( dr["SSM_ISALLOWBACKDT"]);
                //TN.Tag = (Object)(Convert.ToInt32(dr["SSM_ISALLOWBACKDT"] == DBNull.Value ? 0 : dr["SSM_ISALLOWBACKDT"]).ToString());
                // TN.ToolTipText = Convert.ToInt32(dr["SSM_ISALLOWBACKDT"] == DBNull.Value ? 0 : dr["SSM_ISALLOWBACKDT"]).ToString();
                if ((Convert.ToInt32(dr["SSM_ISALLOWBACKDT"] == DBNull.Value ? "0" : dr["SSM_ISALLOWBACKDT"]).ToString()) == "1")
                {
                    TN.ToolTipText = "1";

                }
                else
                {
                    TN.ToolTipText = "0";
                }

                //string IS_A_BACKDATE_Module = (Convert.ToInt32(dr["SSM_ISALLOWBACKDT"] == DBNull.Value ? "0" : dr["SSM_ISALLOWBACKDT"]).ToString());
                /*
                if (IS_A_BACKDATE_Module == "1")
                {
                    //TN_CHILD.BackColor = Color.Green;
                    TN_CHILD.ForeColor = Color.Blue;
                }
                else
                {
                    //TN_CHILD.ForeColor = Color.SlateGray;
                    TN_CHILD.ForeColor = Color.SteelBlue;
                }*/
                parentNode.Nodes.Add(TN_CHILD);
                ADD_CHILD(ref TN_CHILD, TN_CHILD.Name.ToString());
                //string[]  _C=  dr["SSM_ANAL1"].ToString().Split('_');
                //if (_C.Length == level + 1)                                       
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            RoleCreation formnew = new RoleCreation();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();


            //CHNLSVC.General.GetALLMasterCompaniesData()
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");


            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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
                        // paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        paramsText.Append(Select_company + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnSearchRole_Click(object sender, EventArgs e)
        {
            //chkNewRole.Checked = false; 

            if (ddlCompany.SelectedIndex == -1)
            {
                MessageBox.Show("Please select company first", "Select company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Select_company = ddlCompany.SelectedValue.ToString();

            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
            DataTable _result = CHNLSVC.CommonSearch.Get_system_role(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRoleID; //txtBox;
            _CommonSearch.ShowDialog();
            // txtVeiwPcAcRestr.Focus();
        }

        private void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCompany.SelectedIndex == -1)
            {
                return;
            }

            List<SystemRole> roles_list = CHNLSVC.Security.GetRoleByCompany(ddlCompany.SelectedValue.ToString(), 1);
            // List<SystemRole> roles_list2 = CHNLSVC.Security.GetRoleByCompany(ddlCompany.SelectedValue.ToString(), 0);
            // roles_list.AddRange(roles_list2);
            grvRoleVeiwDet.DataSource = null;
            grvRoleVeiwDet.AutoGenerateColumns = false;
            grvRoleVeiwDet.DataSource = roles_list;

            //if (ddlCompany.SelectedIndex != -1)
            //{
            //    Select_company = ddlCompany.SelectedValue.ToString();
            //}                    
            // txtRoleID.Text = "";
            //txtRoleName.Text = "";
            chkNewRole.Checked = true;
            chkNewRole.Checked = false;

        }

        private void txtRoleID_TextChanged(object sender, EventArgs e)
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
                //ddlRoleName.DisplayMember = "RoleName";
                txtRoleID.Text = ROLE.RoleId.ToString();
                chkIsActiveRole.Checked = Convert.ToBoolean(ROLE.IsActive);
            }

            List<SystemRole> roles = new List<SystemRole>();
            roles.Add(ROLE);

            grvRoleVeiwDet.DataSource = null;
            grvRoleVeiwDet.AutoGenerateColumns = false;
            grvRoleVeiwDet.DataSource = roles;

        }

        private void btnRoleSave_Click(object sender, EventArgs e)
        {

            try
            {
                //UI validation.
                //if (ddlCompany.SelectedValue.Equals("-1"))
                if (ddlCompany.SelectedIndex == -1)
                {
                    //throw new UIValidationException("Please select a Company.");
                    MessageBox.Show("Please select a Company", "Select Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Insert mode.
                if (txtRoleName.Visible)
                {
                    if (string.IsNullOrEmpty(txtRoleName.Text))
                    {
                        //throw new UIValidationException("Please enter role name.");
                        MessageBox.Show("Please enter role name", "Select role name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


                //Update mode.
                //else if (ddlRoles.Visible)
                //{
                //    if (ddlRoles.SelectedValue.Equals("-1"))
                //        throw new UIValidationException("Please select a Role.");
                //}
                //if (txtRoleID.Text=="")
                //{
                //    MessageBox.Show("Please select a Role ID.", "Select Role ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                //if (string.IsNullOrEmpty(txtRoleDescription.Text))
                //    throw new UIValidationException("Please enter description.");
                if (txtRoleDesc.Text == "")
                {
                    MessageBox.Show("Please enter description", "Role description", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Fill the relavant object.
                SystemRole _userRole = new SystemRole();
                _userRole.CompanyCode = ddlCompany.SelectedValue.ToString();
                _userRole.Description = txtRoleDesc.Text;
                _userRole.IsActive = (chkIsActiveRole.Checked == true) ? 1 : 0;
                _userRole.CreatedBy = BaseCls.GlbUserID;
                _userRole.CreatedDate = CHNLSVC.Security.GetServerDateTime();
                _userRole.ModifiedBy = BaseCls.GlbUserID;
                _userRole.ModifyedDate = CHNLSVC.Security.GetServerDateTime();
                _userRole.SessionId = BaseCls.GlbUserSessionID;
                _userRole.RoleId = Convert.ToInt32(txtRoleID.Text.Trim());
                _userRole.RoleName = txtRoleName.Text.Trim();

                SystemRole role = CHNLSVC.Security.GetSystemRole_ByRoleData(_userRole);
                if (role != null)
                {
                    if (MessageBox.Show("Do you want to update role details?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _userRole.RoleId = role.RoleId;
                        _userRole.RoleName = role.RoleName;
                    }
                }
                else
                {
                    MessageBox.Show("No suh role exists to update!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Cal the service method.
                Int32 Updated_roleID = CHNLSVC.Security.UpdateSystemUserRole_NEW(_userRole);

                //-----------------------------------------------------------------------------------------------------------------------
                //Rebind the gridview and ddl controls.                
                #region
                SystemRole ROLE = CHNLSVC.Security.GetRoleByCode(ddlCompany.SelectedValue.ToString(), Updated_roleID);
                txtRoleName.Text = ROLE.RoleName;
                txtRoleDesc.Text = ROLE.Description;
                //ddlRoleName.DisplayMember = "RoleName";
                //ddlRoleName.ValueMember = "RoleId";
                chkIsActiveRole.Checked = Convert.ToBoolean(ROLE.IsActive);

                List<SystemRole> roles = new List<SystemRole>();
                roles.Add(ROLE);

                grvRoleVeiwDet.DataSource = null;
                grvRoleVeiwDet.AutoGenerateColumns = false;
                grvRoleVeiwDet.DataSource = roles;
                #endregion

                if (Updated_roleID > 0)
                {
                    MessageBox.Show("Sucessfully updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //catch (UIValidationException ex)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            //}
            catch (Exception ex)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewRole_Click(object sender, EventArgs e)
        {
            if (ddlCompany.SelectedIndex == -1)
            {
                //throw new UIValidationException("Please select a Company.");
                MessageBox.Show("Please select Company", "Select Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtRoleName.Enabled == true)
            {
                if (string.IsNullOrEmpty(txtRoleName.Text))
                {
                    //throw new UIValidationException("Please enter role name.");
                    MessageBox.Show("Please enter new role name", "Select role name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (txtRoleDesc.Text == "")
            {
                MessageBox.Show("Please enter description", "Role description", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SystemRole _userRole = new SystemRole();
            _userRole.CompanyCode = ddlCompany.SelectedValue.ToString();
            _userRole.Description = txtRoleDesc.Text;
            _userRole.IsActive = (chkIsActiveRole.Checked == true) ? 1 : 0;
            _userRole.CreatedBy = BaseCls.GlbUserID;
            _userRole.CreatedDate = CHNLSVC.Security.GetServerDateTime();
            _userRole.ModifiedBy = BaseCls.GlbUserID;
            _userRole.ModifyedDate = CHNLSVC.Security.GetServerDateTime();
            _userRole.SessionId = BaseCls.GlbUserSessionID;

            _userRole.RoleName = txtRoleName.Text.Trim();

            SystemRole role = CHNLSVC.Security.GetSystemRole_ByRoleData(_userRole);
            if (role != null)
            {
                MessageBox.Show("Role name already exists. Please try with a different role name!", "Role existing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //Cal the service method.
                if (MessageBox.Show("Are you sure to create?", "Confirm Create", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                Int32 NEW_RoleID = CHNLSVC.Security.UpdateSystemUserRole_NEW(_userRole);
                chkNewRole.Checked = false;
                txtRoleID.Text = NEW_RoleID.ToString();


                #region
                //SystemRole ROLE = CHNLSVC.Security.GetRoleByCode(ddlCompany.SelectedValue.ToString(), rowsAffected);
                //txtRoleName.Text = ROLE.RoleName;
                //txtRoleDesc.Text = ROLE.Description;                
                //chkIsActiveRole.Checked = Convert.ToBoolean(ROLE.IsActive);

                //List<SystemRole> roles = new List<SystemRole>();
                //roles.Add(ROLE);

                //grvRoleVeiwDet.DataSource = null;
                //grvRoleVeiwDet.AutoGenerateColumns = false;
                //grvRoleVeiwDet.DataSource = roles;
                #endregion

                if (NEW_RoleID > 0)
                {
                    MessageBox.Show("Sucessfully created!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not created.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkNewRole_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNewRole.Checked == true)
            {
                txtRoleID.Text = "";
                txtRoleName.Text = "";
                txtRoleDesc.Text = "";
                txtRoleName.Enabled = true;
                btnRoleSave.Enabled = false;
                btnNewRole.Enabled = true;

                grvRoleVeiwDet.DataSource = null;
                grvRoleVeiwDet.AutoGenerateColumns = false;

            }
            else
            {
                txtRoleID.Text = "";
                txtRoleName.Text = "";
                txtRoleDesc.Text = "";
                txtRoleName.Enabled = false;
                btnNewRole.Enabled = false;
                btnRoleSave.Enabled = true;

            }
        }

        private void btnCancelRoleCre_Click(object sender, EventArgs e)
        {
            //chkNewRole.Checked = false;

            txtRoleID.Text = "";
            txtRoleName.Text = "";
            txtRoleDesc.Text = "";
            txtRoleName.Enabled = true;
            btnRoleSave.Enabled = false;
            btnNewRole.Enabled = true;

            grvRoleVeiwDet.DataSource = null;
            grvRoleVeiwDet.AutoGenerateColumns = false;
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //-------------------------------------------------------------------
            //if( isChangePrevi == true)
            //{
            //    return;
            //}
            //-------------------------------------------------------------------
            /* 
            Boolean is_checked = e.Node.Checked;
            string menuName = e.Node.Name.ToString();
            if (is_checked == true)
            {
               // selected_Module_list.Add(menuName.ToString());

                foreach (TreeNode child in e.Node.Nodes)
                {
                    child.Checked = true;
                }
            }
            else
            {
               // selected_Module_list.Remove(menuName.ToString());
               
                
                foreach (TreeNode child in e.Node.Nodes)
                {
                    child.Checked = false;    
                    
                }

                //if (e.Node.Parent != null)
                //{
                //    e.Node.Parent.Checked = false;
                //}
               
            }
           
            //List<TreeNode> checked_NodesList = CheckedNodes(treeView1.Nodes);
            //dataGridView1.DataSource = null;
            //dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.DataSource = checked_NodesList;
            
           */
        }
        private List<TreeNode> CheckedNodes(System.Windows.Forms.TreeNodeCollection theNodes)
        {
            List<TreeNode> checkedNodes = new List<TreeNode>();
            if (theNodes != null)
            {
                foreach (System.Windows.Forms.TreeNode aNode in theNodes)
                {
                    if (aNode.Checked)
                    {
                        checkedNodes.Add(aNode);
                    }
                    //aResult.AddRange(CheckedNodes(aNode.Nodes));
                    checkedNodes.AddRange(CheckedNodes(aNode.Nodes));
                }
            }
            return checkedNodes;
        }

        private void btnSearchRoleGrant_Click(object sender, EventArgs e)
        {
            if (ddlCompayGrant.SelectedIndex == -1)
            {
                MessageBox.Show("Please select company first", "Select company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Select_company = ddlCompayGrant.SelectedValue.ToString();

            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
            DataTable _result = CHNLSVC.CommonSearch.Get_system_role(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRoleIDGrant; //txtBox;
            _CommonSearch.ShowDialog();
        }

        private void txtRoleIDGrant_TextChanged(object sender, EventArgs e)
        {
            string ROLE_ID = txtRoleIDGrant.Text.Trim() == "" ? "-1" : txtRoleIDGrant.Text.Trim();
            if (txtRoleIDGrant.Text.Trim() == "")
            {
                return;
            }
            //isChangePrevi = true;

            SystemRole ROLE = CHNLSVC.Security.GetRoleByCode(ddlCompayGrant.SelectedValue.ToString(), Convert.ToInt32(ROLE_ID));
            if (ROLE != null)
            {
                txtRoleNameGrant.Text = ROLE.RoleName;
                txtRoleDescGrant.Text = ROLE.Description;
                txtRoleIDGrant.Text = ROLE.RoleId.ToString();
                chkActRoleGrant.Checked = ROLE.IsActive == 1 ? true : false;
                //chekOrUncheck(treeView1, false);
                UncheckAllNodes(treeView1.Nodes);
                loadRole_grantedMenus();
            }
            else
            {
                MessageBox.Show("Invalid role ID", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void ddlCompayGrant_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlCompayGrant.SelectedIndex!=-1)
            //{
            //    Select_company = ddlCompayGrant.SelectedValue.ToString();
            //}           

            UncheckAllNodes(treeView1.Nodes);
            txtRoleIDGrant.Text = "";
            txtRoleNameGrant.Text = "";
            txtRoleDescGrant.Text = "";
            chkActRoleGrant.Checked = false;
        }

        private void btnGrant_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ddlCompanyPrivillages.SelectedValue.Equals("-1"))
                //    throw new UIValidationException("Please select a Company.");
                if (ddlCompayGrant.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select company", "Select Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //if (ddlRolePrivillages.SelectedValue.Equals("-1"))
                //    throw new UIValidationException("Please select a Role.");
                if (txtRoleIDGrant.Text.Trim() == "")
                {
                    MessageBox.Show("Please select Role ID", "Select role ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SystemRoleOption _systemRoleOption = new SystemRoleOption();

                // _commonUIOperations = new CommonUIOperations();


                //Fill SystemRole object.
                SystemRole _systemRole = new SystemRole();
                _systemRole.CompanyCode = ddlCompayGrant.SelectedValue.ToString();
                _systemRole.RoleId = Convert.ToInt32(txtRoleIDGrant.Text.Trim());
                _systemRoleOption.SystemRole = _systemRole;
                _systemRoleOption.CreatedBy = BaseCls.GlbUserID; ;
                _systemRoleOption.IsActive = 1;

                SystemRole role = CHNLSVC.Security.GetSystemRole_ByRoleData(_systemRole);
                if (role == null)
                {
                    MessageBox.Show("Invalid Role", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<SystemOption> _systemOptionList = null;
                SystemOption _sysOption = null;
                GetSelected_AllNodes(treeView1.Nodes);
                if (selected_Module_list.Count > 0)
                {
                    _systemOptionList = new List<SystemOption>();
                    //foreach (TreeNode node in tViewGrantSystemOption.CheckedNodes)
                    //{
                    //    _sysOption = new SystemOption();
                    //    _sysOption.Ssp_optid = _commonUIOperations.GetProperty(node.ImageToolTip).Ssp_optid;
                    //    _systemOptionList.Add(_sysOption);
                    //}
                    foreach (string module_name in selected_Module_list)
                    {
                        List<SystemMenus> menus = new List<SystemMenus>();
                        CHNLSVC.Security.Get_Menu(module_name, out menus);
                        _sysOption = new SystemOption();
                        _sysOption.Ssp_optid = menus[0].Ssm_id;//_commonUIOperations.GetProperty(node.ImageToolTip).Ssp_optid;
                        //-----------------------------------------------------
                        _systemOptionList.Add(_sysOption);
                        //DataTable dtt= CHNLSVC.Security.Get_childMenus("SHOWALL", module_name);

                        //if (dtt.Rows.Count == 0)
                        //{
                        //    _systemOptionList.Add(_sysOption);
                        //}
                        //------------------------------------------------------
                        // _systemOptionList.Add(_sysOption);                      

                    }
                }
                //Add SystemOption List.
                _systemRoleOption.SystemOptionList = _systemOptionList;

                //Call the service method.
                if (MessageBox.Show("Are you sure to save?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //  isChangePrevi = false;
                this.Cursor = Cursors.WaitCursor;

                int result = CHNLSVC.Security.SaveSelectedSystemOptionsRolePrivillages_NEW(_systemRoleOption);

                this.txtRoleIDGrant_TextChanged(null, null);

                this.Cursor = Cursors.Default;

                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Role Privillages Sucessfully updated.");
                MessageBox.Show("Role Privillages Sucessfully updated", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
                MessageBox.Show(ex.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImgBtnViewBD_Click(object sender, EventArgs e)
        {
            if (ddlComView.SelectedIndex == -1 || txtRoleIDview.Text.Trim() == "")
            {
                return;
            }
            grvViewRoleMenus.DataSource = null;
            grvViewRoleMenus.AutoGenerateColumns = false;
            grvViewRoleMenus.DataSource = CHNLSVC.Security.Get_MenusForRole(ddlComView.SelectedValue.ToString(), Convert.ToInt32(txtRoleIDview.Text.Trim()));


            grvUserRole.DataSource = null;
            grvUserRole.AutoGenerateColumns = false;
            grvUserRole.DataSource = CHNLSVC.Security.Get_UsersForRole(ddlComView.SelectedValue.ToString(), Convert.ToInt32(txtRoleIDview.Text.Trim()));
        }

        private void btnRoleIDView_Click(object sender, EventArgs e)
        {
            if (ddlComView.SelectedIndex == -1)
            {
                MessageBox.Show("Please select company first", "Select company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Select_company = ddlComView.SelectedValue.ToString();

            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
            DataTable _result = CHNLSVC.CommonSearch.Get_system_role(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRoleIDview; //txtBox;
            _CommonSearch.ShowDialog();
        }

        private void btnClearViewGrid_Click(object sender, EventArgs e)
        {
            grvViewRoleMenus.DataSource = null;
            grvViewRoleMenus.AutoGenerateColumns = false;

            grvUserRole.DataSource = null;
            grvUserRole.AutoGenerateColumns = false;
        }

        private void txtRoleIDview_TextChanged(object sender, EventArgs e)
        {
            txtViewRoleDesc.Text = "";

            grvViewRoleMenus.DataSource = null;
            grvViewRoleMenus.AutoGenerateColumns = false;

            grvUserRole.DataSource = null;
            grvUserRole.AutoGenerateColumns = false;

            if (txtRoleIDview.Text.Trim() != "" && ddlComView.SelectedIndex != -1)
            {
                this.ImgBtnViewBD_Click(null, null);
            }
            SystemRole sysrl = CHNLSVC.Security.GetRoleByCode(ddlComView.SelectedValue.ToString(), Convert.ToInt32(txtRoleIDview.Text.Trim()));
            try
            {
                if (sysrl != null)
                {
                    txtViewRoleDesc.Text = sysrl.Description;
                }
            }
            catch (Exception ex)
            {
                txtViewRoleDesc.Text = "";
            }
        }

        private void txtRoleIDview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnRoleIDView_Click(null, null);
            }
        }

        private void txtRoleIDGrant_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnSearchRoleGrant_Click(null, null);
            }
        }

        private void txtRoleID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnSearchRole_Click(null, null);
            }
        }

        private void ddlComView_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvViewRoleMenus.DataSource = null;
            grvViewRoleMenus.AutoGenerateColumns = false;

            grvUserRole.DataSource = null;
            grvUserRole.AutoGenerateColumns = false;
            txtRoleIDview.Text = "";
        }

        public void GetSelected_AllNodes(TreeNodeCollection nodes)
        {
            selected_Module_list = new List<string>();

            foreach (TreeNode node in nodes)
            {
                //node.Checked = false;
                if (node.Checked == true)
                {
                    if (node.Name != "m")
                    {
                        selected_Module_list.Add(node.Name);
                    }

                }
                //else
                //{
                //    node.Parent.Checked = false;
                //}
                GetCheckedChildren(node);
            }
        }

        private void GetCheckedChildren(TreeNode rootNode)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                GetCheckedChildren(node);
                //node.Checked = isChecked;
                if (node.Checked == true)
                {
                    selected_Module_list.Add(node.Name);
                }
                //else
                //{
                //    node.Parent.Checked = false;
                //}
                //node.ForeColor = Color.RoyalBlue;
            }
        }

        private void txtRoleID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnSearchRole_Click(null, null);
            }
        }

        private void txtRoleIDGrant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnSearchRoleGrant_Click(null, null);
            }
        }

        private void txtRoleIDview_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnRoleIDView_Click(null, null);
            }
        }

        private void btnGetOpt_Click(object sender, EventArgs e)
        {
            grvGrpOpt.DataSource = null;
            grvGrpOpt.AutoGenerateColumns = false;
            grvGrpOpt.DataSource = CHNLSVC.Security.Get_SystemOptionsForGroup(txtOptGrupID.Text.Trim());
        }

        private void btnSearchRoleIDopt_Click(object sender, EventArgs e)
        {
            if (ddlCompanyRoleOpt.SelectedIndex == -1)
            {
                MessageBox.Show("Please select company first", "Select company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Select_company = ddlCompanyRoleOpt.SelectedValue.ToString();

            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
            DataTable _result = CHNLSVC.CommonSearch.Get_system_role(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRoleID_opt; //txtBox;
            _CommonSearch.ShowDialog();
        }

        private void txtRoleID_opt_TextChanged(object sender, EventArgs e)
        {
            string ROLE_ID = txtRoleID_opt.Text.Trim() == "" ? "-1" : txtRoleID_opt.Text.Trim();
            if (txtRoleID_opt.Text.Trim() == "")
            {
                return;
            }
            //isChangePrevi = true;

            SystemRole ROLE = CHNLSVC.Security.GetRoleByCode(ddlCompanyRoleOpt.SelectedValue.ToString(), Convert.ToInt32(ROLE_ID));
            if (ROLE != null)
            {
                txtRoleName_opt.Text = ROLE.RoleName;
                txtRoleDesc_opt.Text = ROLE.Description;
                //txtRoleID_opt.Text = ROLE.RoleId.ToString();
                chkActRole_opt.Checked = ROLE.IsActive == 1 ? true : false;
            }
            else
            {
                MessageBox.Show("Invalid role ID", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.btnGetOptForRole_Click(null, null);

        }

        private List<Int32> get_selected_SystemOptions()
        {
            grvGrpOpt.EndEdit();
            List<Int32> list = new List<Int32>();
            foreach (DataGridViewRow dgvr in grvGrpOpt.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(Convert.ToInt32(dgvr.Cells[1].Value.ToString()));
                }
            }
            return list;
        }
        private void btnSaveRoleOpt_Click(object sender, EventArgs e)
        {
            //Fill SystemRole object.
            if (ddlCompanyRoleOpt.SelectedIndex == -1 || txtRoleID_opt.Text.Trim() == "")
            {
                MessageBox.Show("Select role details.", "Role details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SystemRoleOption _systemRoleOption = new SystemRoleOption();

            SystemRole _systemRole = new SystemRole();
            _systemRole.CompanyCode = ddlCompanyRoleOpt.SelectedValue.ToString();
            _systemRole.RoleId = Convert.ToInt32(txtRoleID_opt.Text.Trim());
            _systemRoleOption.SystemRole = _systemRole;
            _systemRoleOption.CreatedBy = BaseCls.GlbUserID; ;
            _systemRoleOption.IsActive = 1;

            SystemRole role = CHNLSVC.Security.GetSystemRole_ByRoleData(_systemRole);
            if (role == null)
            {
                MessageBox.Show("Invalid Role", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<SystemOption> _systemOptionList = null;
            SystemOption _sysOption = null;



            //GetSelected_AllNodes(treeView1.Nodes);

            List<Int32> list_options = get_selected_SystemOptions();
            //if (list_options.Count  <1)
            //{
            //    MessageBox.Show("Are you sure to remove all options?","Select Options",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //    return;

            //}           
            _systemOptionList = new List<SystemOption>();
            foreach (Int32 op_id in list_options)
            {
                // List<SystemMenus> menus = new List<SystemMenus>();
                // CHNLSVC.Security.Get_Menu(module_name, out menus);
                _sysOption = new SystemOption();
                _sysOption.Ssp_optid = op_id;//_commonUIOperations.GetProperty(node.ImageToolTip).Ssp_optid;
                //-----------------------------------------------------
                _systemOptionList.Add(_sysOption);
                //DataTable dtt= CHNLSVC.Security.Get_childMenus("SHOWALL", module_name);

                //if (dtt.Rows.Count == 0)
                //{
                //    _systemOptionList.Add(_sysOption);
                //}
                //------------------------------------------------------
                // _systemOptionList.Add(_sysOption);                    
            }
            //Add SystemOption List.
            _systemRoleOption.SystemOptionList = _systemOptionList;


            if (MessageBox.Show("Are you sure to save?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            Int32 eff = CHNLSVC.Security.Save_System_Options_For_Role(_systemRoleOption, txtOptGrupID.Text.Trim());
            this.Cursor = Cursors.Default;

            if (eff == 1)
            {
                MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Not Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOptGrupID_TextChanged(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------
            try
            {
                if (txtOptGrupID.Text != "")
                {
                    DataTable dt = CHNLSVC.Security.Get_OptionGroupDetail(txtOptGrupID.Text.Trim());
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string desc = dt.Rows[0]["SOTG_DESC"].ToString();
                            txtOptGrupDesc.Text = desc;
                        }
                    }
                }
            }
            catch (Exception EX)
            {

            }
            //-----------------------------------------------------------------
            //TODO: LOAD GRID.
            this.btnGetOpt_Click(null, null);
            if (txtRoleID_opt.Text != "" && ddlCompanyRoleOpt.SelectedIndex != -1)
            {
                this.btnGetOptForRole_Click(null, null);
            }


        }

        private void btnGetOptForRole_Click(object sender, EventArgs e)
        {
            if (ddlCompanyRoleOpt.SelectedIndex == -1 || txtRoleID_opt.Text.Trim() == "")
            {
                MessageBox.Show("Select role details.", "Role details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (txtOptGrupID.Text.Trim()=="")
            //{
            //    MessageBox.Show("Select Option Group", "Option Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            this.btnGetOpt_Click(null, null);


            //Get_Active_System_OptionsFor_Role
            DataTable dt = CHNLSVC.Security.Get_Active_System_OptionsFor_Role(ddlCompanyRoleOpt.SelectedValue.ToString(), Convert.ToInt32(txtRoleID_opt.Text.Trim()));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow tb_row in dt.Rows)
                    {
                        foreach (DataGridViewRow row in grvGrpOpt.Rows)
                        {
                            Int32 sysOpt = Convert.ToInt32(tb_row["SSRM_OPTID"].ToString());
                            Int32 grdSysOpt = Convert.ToInt32(row.Cells["ssp_optid"].Value.ToString());
                            if (sysOpt == grdSysOpt)
                            {
                                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                                chk.Value = true;
                                row.Selected = true;
                            }
                            //else
                            //{
                            //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                            //    chk.Value = false;
                            //}

                        }
                    }

                    grvGrpOpt.EndEdit();
                }
            }
        }

        private void checkBox_allOpt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_allOpt.Checked == true)
            {
                this.btnAll_Schemes_Click(null, null);
            }
            else
            {
                this.btnNon_Schemes_Click(null, null);
            }
        }

        private void btnAll_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvGrpOpt.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvGrpOpt.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_Schemes_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (DataGridViewRow row in grvGrpOpt.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvGrpOpt.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnOptGrpSearch_Click(object sender, EventArgs e)
        {
            //Select_company = ddlCompanyRoleOpt.SelectedValue.ToString();

            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SysOptGroups);
            DataTable _result = CHNLSVC.CommonSearch.Get_system_option_Groups(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtOptGrupID; //txtBox;
            _CommonSearch.ShowDialog();
        }

        private void ddlCompanyRoleOpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoleID_opt.Text = "";
            txtRoleID_opt.Text = "";
            txtRoleName_opt.Text = "";
            txtRoleDesc_opt.Text = "";
            chkActRole_opt.Checked = false;

            this.btnGetOpt_Click(null, null);
        }

        private void btnRoleLoc_Click(object sender, EventArgs e)
        {
            if (ddlCompanyLoc.SelectedIndex == -1)
            {
                MessageBox.Show("Please select company first", "Select company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Select_company = ddlCompanyLoc.SelectedValue.ToString();

            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
            DataTable _result = CHNLSVC.CommonSearch.Get_system_role(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRoleIDLoc; //txtBox;
            _CommonSearch.ShowDialog();
        }

        private void btnAddPC_Click(object sender, EventArgs e)
        {
            string com = ucProfitCenterSearch1.Company;
            string chanel = ucProfitCenterSearch1.Channel;
            string subChanel = ucProfitCenterSearch1.SubChannel;
            string area = ucProfitCenterSearch1.Area;
            string region = ucProfitCenterSearch1.Regien;
            string zone = ucProfitCenterSearch1.Zone;
            string pc = ucProfitCenterSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
            grvPCs.DataSource = null;
            grvPCs.AutoGenerateColumns = false;
            grvPCs.DataSource = dt;
        }

        private void btnAddLocs_Click(object sender, EventArgs e)
        {
            string com = ucLoactionSearch1.Company;
            string chanel = ucLoactionSearch1.Channel;
            string subChanel = ucLoactionSearch1.SubChannel;
            string area = ucLoactionSearch1.Area;
            string region = ucLoactionSearch1.Regien;
            string zone = ucLoactionSearch1.Zone;
            string pc = ucLoactionSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Inventory.GetLOC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
            // select_LOC_List.Merge(dt);
            grvLocs.DataSource = null;
            grvLocs.AutoGenerateColumns = false;
            grvLocs.DataSource = dt;
        }

        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();

            //if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            //{                             
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //}
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = _result;

            if (_result == null)
            {
                //MessageBox.Show("No data found!", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_result.Rows.Count == 0)
            {
                // MessageBox.Show("No data found!","Data not found",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
        }

        private void ddlCompanyLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucLoactionSearch1.Company = BaseCls.GlbUserComCode;
            if (ddlCompanyLoc.SelectedIndex != -1)
            {
                Select_company = ddlCompanyLoc.SelectedValue.ToString();
                // ucLoactionSearch1.Company = Select_company;
                this.btnAddPartys_Click(null, null);
            }

        }

        private void btnAddPartys2_Click(object sender, EventArgs e)
        {
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();

            //if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            //{                             
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //}
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

            grvParty2.DataSource = null;
            grvParty2.AutoGenerateColumns = false;
            grvParty2.DataSource = _result;

            if (_result == null)
            {
                //MessageBox.Show("No data found!", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_result.Rows.Count == 0)
            {
                // MessageBox.Show("No data found!","Data not found",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
        }

        private void btnAddLoc_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> loc_list = get_selected_Locations();
                if (txtRoleIDLoc.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a Role ID.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (loc_list.Count == 0)
                {
                    MessageBox.Show("Please select Location(s)", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Save_sec_role_loc
                List<SecRoleLocation> _RoleLocList = new List<SecRoleLocation>();
                foreach (string secloc in loc_list)
                {
                    SecRoleLocation secRloc = new SecRoleLocation();
                    secRloc.Ssrl_act = true;
                    secRloc.Ssrl_com = ucLoactionSearch1.Company;
                    secRloc.Ssrl_cre_by = BaseCls.GlbUserID;
                    secRloc.Ssrl_cre_dt = DateTime.Now.Date;
                    secRloc.Ssrl_loc = secloc;
                    secRloc.Ssrl_mod_by = BaseCls.GlbUserID;
                    secRloc.Ssrl_mod_dt = DateTime.Now.Date; ;
                    secRloc.Ssrl_readonly = true;
                    secRloc.Ssrl_roleid = Convert.ToInt32(txtRoleIDLoc.Text);

                    _RoleLocList.Add(secRloc);
                }
                Int32 eff = CHNLSVC.Security.Save_sec_role_loc(_RoleLocList);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully added!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtRoleIDLoc_TextChanged(null, null);
                    return;
                }
                else
                {
                    MessageBox.Show("Not added!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private List<string> get_selected_Locations()
        {
            grvLocs.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvLocs.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_LocChannels()
        {
            grvParty.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvParty.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private void btnAddLocChnl_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> locChanel_list = get_selected_LocChannels();

                if (txtRoleIDLoc.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a Role ID.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (locChanel_list.Count == 0)
                {
                    MessageBox.Show("Please select Channel(s)", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<SecRoleLocChanel> _secLocChnlList = new List<SecRoleLocChanel>();
                foreach (string locCh in locChanel_list)
                {
                    SecRoleLocChanel locChanel = new SecRoleLocChanel();
                    locChanel.Ssrl_cre_by = BaseCls.GlbUserID;
                    locChanel.Ssrlc_act = true;
                    locChanel.Ssrlc_chnnl = locCh;
                    locChanel.Ssrlc_com = ddlCompanyLoc.SelectedValue.ToString();
                    locChanel.Ssrlc_cre_dt = DateTime.Now.Date;
                    locChanel.Ssrlc_mod_by = BaseCls.GlbUserID; ;
                    locChanel.Ssrlc_mod_dt = DateTime.Now.Date;
                    locChanel.Ssrlc_readonly = true;
                    locChanel.Ssrlc_roleid = Convert.ToInt32(txtRoleIDLoc.Text);

                    _secLocChnlList.Add(locChanel);
                }
                Int32 eff = CHNLSVC.Security.Save_sec_role_LocChanel(_secLocChnlList);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully added!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtRoleIDLoc_TextChanged(null, null);
                    return;
                }
                else
                {
                    MessageBox.Show("Not added!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<string> get_selected_RolePCs()
        {
            grvPCs.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvPCs.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }

        private List<string> get_selected_RolePcChannels()
        {
            grvParty2.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvParty2.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private void btnAddRolePC_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> rolePC_list = get_selected_RolePCs();

                if (txtRoleIDPC.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a Role ID.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (rolePC_list.Count == 0)
                {
                    MessageBox.Show("Please select PC(s)", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<SecRolePC> _secRolePCList = new List<SecRolePC>();
                foreach (string PC in rolePC_list)
                {
                    SecRolePC rolePC = new SecRolePC();
                    rolePC.Ssrp_act = true;
                    rolePC.Ssrp_com = ddlCompanyPC.SelectedValue.ToString();
                    rolePC.Ssrp_cre_by = BaseCls.GlbUserID;
                    rolePC.Ssrp_cre_dt = DateTime.Now.Date;
                    rolePC.Ssrp_mod_by = BaseCls.GlbUserID;
                    rolePC.Ssrp_mod_dt = DateTime.Now.Date;
                    rolePC.Ssrp_pc = PC;
                    rolePC.Ssrp_readonly = true;
                    rolePC.Ssrp_roleid = Convert.ToInt32(txtRoleIDPC.Text);

                    _secRolePCList.Add(rolePC);
                }
                Int32 eff = CHNLSVC.Security.Save_sec_role_pc(_secRolePCList);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully added!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtRoleIDPC_TextChanged(null, null);
                    return;
                }
                else
                {
                    MessageBox.Show("Not added!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddPCchanel_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> rolePcChnl_list = get_selected_RolePcChannels();
                if (txtRoleIDPC.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a Role ID.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (rolePcChnl_list.Count == 0)
                {
                    MessageBox.Show("Please select channel(s)", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<SecRolePcChannel> _secPcChnlList = new List<SecRolePcChannel>();
                foreach (string pcChnl in rolePcChnl_list)
                {
                    SecRolePcChannel rolePcChn = new SecRolePcChannel();
                    rolePcChn.Ssrpc_act = true;
                    rolePcChn.Ssrpc_chnnl = pcChnl;
                    rolePcChn.Ssrpc_com = ddlCompanyPC.SelectedValue.ToString();
                    rolePcChn.Ssrpc_cre_by = BaseCls.GlbUserID;
                    rolePcChn.Ssrpc_cre_dt = DateTime.Now.Date;
                    rolePcChn.Ssrpc_mod_by = BaseCls.GlbUserID;
                    rolePcChn.Ssrpc_mod_dt = DateTime.Now.Date;
                    rolePcChn.Ssrpc_readonly = true;
                    rolePcChn.Ssrpc_roleid = Convert.ToInt32(txtRoleIDPC.Text);

                    _secPcChnlList.Add(rolePcChn);
                }

                Int32 eff = CHNLSVC.Security.Save_sec_role_pcchnl(_secPcChnlList);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully added!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtRoleIDPC_TextChanged(null, null);
                    return;
                }
                else
                {
                    MessageBox.Show("Not added!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCompanyPC.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select company first", "Select company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Select_company = ddlCompanyPC.SelectedValue.ToString();

                TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRoleIDPC; //txtBox;
                _CommonSearch.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void ddlCompanyPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            if (ddlCompanyPC.SelectedIndex != -1)
            {
                Select_company = ddlCompanyPC.SelectedValue.ToString();
                ucProfitCenterSearch1.Company = Select_company;
                this.btnAddPartys2_Click(null, null);
            }
        }

        private void check_allLoc_CheckedChanged(object sender, EventArgs e)
        {
            if (check_allLoc.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow row in grvLocs.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvLocs.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                //this.btnNonPc_Click(null, null);
                try
                {
                    foreach (DataGridViewRow row in grvLocs.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvLocs.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void chkAllRLoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllRLoc.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow row in gvRoleLoc.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    gvRoleLoc.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in gvRoleLoc.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    gvRoleLoc.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void chkAllParty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllParty.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow row in grvParty.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvParty.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in grvParty.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvParty.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void chkAllRChnl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllRChnl.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow row in grvRoleChnl.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvRoleChnl.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in grvRoleChnl.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvRoleChnl.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void chkAllRPC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllRPC.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow row in grvPCs.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvPCs.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in grvPCs.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvPCs.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void chkAllPC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPC.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow row in grvRolePC.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvRolePC.EndEdit();
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in grvRolePC.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvRolePC.EndEdit();
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void chkAllParty2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllParty2.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow row in grvParty2.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvParty2.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in grvParty2.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvParty2.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void chkAllPcChnl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPcChnl.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow row in grvRolePCChnl.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvRolePCChnl.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in grvRolePCChnl.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvRolePCChnl.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
        }
        //----------------------------------------------------------------------------------
        private List<string> get_selected_LocUpdate()
        {
            gvRoleLoc.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in gvRoleLoc.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells["SSRL_LOC"].Value.ToString());
                }
            }
            return list;
        }
        private void btnDelRoleLoc_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> update_roleLocList = get_selected_LocUpdate();
                List<SecRoleLocation> _RoleLocList = new List<SecRoleLocation>();
                foreach (string secloc in update_roleLocList)
                {
                    SecRoleLocation secRloc = new SecRoleLocation();
                    secRloc.Ssrl_act = false;
                    secRloc.Ssrl_com = ucLoactionSearch1.Company;
                    secRloc.Ssrl_cre_by = BaseCls.GlbUserID;
                    secRloc.Ssrl_cre_dt = DateTime.Now.Date;
                    secRloc.Ssrl_loc = secloc;
                    secRloc.Ssrl_mod_by = BaseCls.GlbUserID;
                    secRloc.Ssrl_mod_dt = DateTime.Now.Date;
                    secRloc.Ssrl_readonly = true;
                    secRloc.Ssrl_roleid = Convert.ToInt32(txtRoleIDLoc.Text);

                    _RoleLocList.Add(secRloc);
                }
                Int32 eff = CHNLSVC.Security.Update_sec_role_loc(_RoleLocList);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtRoleIDLoc_TextChanged(null, null);
                    return;
                }
                else
                {
                    MessageBox.Show("Not updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private List<string> get_selected_LocChannel_Update()
        {
            grvRoleChnl.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvRoleChnl.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells["SSRLC_CHNNL"].Value.ToString());
                }
            }
            return list;
        }
        private void btnDelLocChnl_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> locChnl_list = get_selected_LocChannel_Update();
                List<SecRoleLocChanel> _secLocChnlList = new List<SecRoleLocChanel>();
                foreach (string locCh in locChnl_list)
                {
                    SecRoleLocChanel locChanel = new SecRoleLocChanel();
                    locChanel.Ssrl_cre_by = BaseCls.GlbUserID;
                    locChanel.Ssrlc_act = false;
                    locChanel.Ssrlc_chnnl = locCh;
                    locChanel.Ssrlc_com = ddlCompanyLoc.SelectedValue.ToString();
                    locChanel.Ssrlc_cre_dt = DateTime.Now.Date;
                    locChanel.Ssrlc_mod_by = BaseCls.GlbUserID; ;
                    locChanel.Ssrlc_mod_dt = DateTime.Now.Date;
                    locChanel.Ssrlc_readonly = true;
                    locChanel.Ssrlc_roleid = Convert.ToInt32(txtRoleIDLoc.Text);

                    _secLocChnlList.Add(locChanel);
                }
                Int32 eff = CHNLSVC.Security.Update_sec_role_locChannel(_secLocChnlList);

                if (eff > 0)
                {
                    MessageBox.Show("Successfully updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtRoleIDLoc_TextChanged(null, null);
                    return;
                }
                else
                {
                    MessageBox.Show("Not updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRoleIDLoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtRoleIDLoc.Text.Trim() != "")
                {
                    Int32 roleID = Convert.ToInt32(txtRoleIDLoc.Text.Trim());
                    gvRoleLoc.DataSource = null;
                    gvRoleLoc.AutoGenerateColumns = false;
                    gvRoleLoc.DataSource = CHNLSVC.Security.Get_Sec_role_loc(ddlCompanyLoc.SelectedValue.ToString(), roleID, string.Empty);

                    grvRoleChnl.DataSource = null;
                    grvRoleChnl.AutoGenerateColumns = false;
                    grvRoleChnl.DataSource = CHNLSVC.Security.Get_Sec_role_locChannel(ddlCompanyLoc.SelectedValue.ToString(), roleID, string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        //--------
        private List<string> get_selected_PC_Update()
        {
            grvRolePC.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvRolePC.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells["SSRP_PC"].Value.ToString());
                }
            }
            return list;
        }
        private void btnDelRolePc_Click(object sender, EventArgs e)
        {
            //txtRoleIDPC_TextChanged
            List<string> rolePC_list = get_selected_PC_Update();
            List<SecRolePC> _secRolePCList = new List<SecRolePC>();
            foreach (string PC in rolePC_list)
            {
                SecRolePC rolePC = new SecRolePC();
                rolePC.Ssrp_act = false;
                rolePC.Ssrp_com = ddlCompanyPC.SelectedValue.ToString();
                rolePC.Ssrp_cre_by = BaseCls.GlbUserID;
                rolePC.Ssrp_cre_dt = DateTime.Now.Date;
                rolePC.Ssrp_mod_by = BaseCls.GlbUserID;
                rolePC.Ssrp_mod_dt = DateTime.Now.Date;
                rolePC.Ssrp_pc = PC;
                rolePC.Ssrp_readonly = true;
                rolePC.Ssrp_roleid = Convert.ToInt32(txtRoleIDPC.Text);

                _secRolePCList.Add(rolePC);
            }
            Int32 eff = CHNLSVC.Security.Update_sec_role_PC(_secRolePCList);
            if (eff > 0)
            {
                MessageBox.Show("Successfully updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtRoleIDPC_TextChanged(null, null);
                return;
            }
            else
            {
                MessageBox.Show("Not updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtRoleIDPC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtRoleIDPC.Text.Trim() != "")
                {
                    Int32 roleID = Convert.ToInt32(txtRoleIDPC.Text.Trim());
                    grvRolePC.DataSource = null;
                    grvRolePC.AutoGenerateColumns = false;
                    grvRolePC.DataSource = CHNLSVC.Security.Get_Sec_role_pc(ddlCompanyPC.SelectedValue.ToString(), roleID, string.Empty);

                    grvRolePCChnl.DataSource = null;
                    grvRolePCChnl.AutoGenerateColumns = false;
                    grvRolePCChnl.DataSource = CHNLSVC.Security.Get_Sec_role_pcChannel(ddlCompanyPC.SelectedValue.ToString(), roleID, string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<string> get_selected_PcChannels_Update()
        {
            grvRolePCChnl.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvRolePCChnl.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells["SSRPC_CHNNL"].Value.ToString());
                }
            }
            return list;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            List<string> rolePcChnl_list = get_selected_PcChannels_Update();
            List<SecRolePcChannel> _secPcChnlList = new List<SecRolePcChannel>();
            foreach (string pcChnl in rolePcChnl_list)
            {
                SecRolePcChannel rolePcChn = new SecRolePcChannel();
                rolePcChn.Ssrpc_act = false;
                rolePcChn.Ssrpc_chnnl = pcChnl;
                rolePcChn.Ssrpc_com = ddlCompanyPC.SelectedValue.ToString();
                rolePcChn.Ssrpc_cre_by = BaseCls.GlbUserID;
                rolePcChn.Ssrpc_cre_dt = DateTime.Now.Date;
                rolePcChn.Ssrpc_mod_by = BaseCls.GlbUserID;
                rolePcChn.Ssrpc_mod_dt = DateTime.Now.Date;
                rolePcChn.Ssrpc_readonly = true;
                rolePcChn.Ssrpc_roleid = Convert.ToInt32(txtRoleIDPC.Text);

                _secPcChnlList.Add(rolePcChn);
            }

            Int32 eff = CHNLSVC.Security.Update_secRolePcChannel(_secPcChnlList);
            if (eff > 0)
            {
                MessageBox.Show("Successfully updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtRoleIDPC_TextChanged(null, null);
                return;
            }
            else
            {
                MessageBox.Show("Not updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
