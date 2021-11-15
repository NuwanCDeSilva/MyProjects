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
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FF.BusinessObjects.General;

namespace FF.WindowsERPClient.Security
{
    //get_childMenus  NEW
    //get_Menu NEW
    //sp_get_LOC_onHierachy -UPDATE
    //sp_get_backdates  -new

    public partial class BackDate : Base
    {
        Base _base = new Base();
        DataTable dt;
        List<string> ADDED_LIST = new List<string>();
        List<string> selected_Module_list = new List<string>();

        DataTable select_PC_List = new DataTable();
        DataTable select_LOC_List = new DataTable();
        //----------------------------------------

        #region properties
        private Int16 _IsdayEnd;

        public Int16 _isdayEnd
        {
            get { return _IsdayEnd; }
            set { _IsdayEnd = value; }
        }

        #endregion

        public BackDate()
        {
            InitializeComponent();
            _isdayEnd = 0;
            treeView1.CheckBoxes = true;
            // panel_ViewHistory.Visible = false;

            TextBoxBDVFrom.CustomFormat = "dd'/'MM'/'yyyy hh':'mm tt";
            TextBoxBDVTo.CustomFormat = "dd'/'MM'/'yyyy hh':'mm tt";


            dt = _base.CHNLSVC.Security.GetUserSystemMenus("ALL", "ALL");

            TreeNode TN = new TreeNode();
            TN.Name = "m";
            TN.Text = "Modules";

            treeView1.Nodes.Add(TN);

            ADD_CHILD(ref TN, TN.Name.ToString());

            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            ucLoactionSearch1.Company = BaseCls.GlbUserComCode;
            ucLoactionSearch_Other.Company = BaseCls.GlbUserComCode;

            treeView1.ExpandAll();

        }
        private void ADD_CHILD(ref TreeNode parentNode, string parentNodeName)
        {
            //SHOWBACKDATEONLY
            //SHOWALL
            DataTable dt = CHNLSVC.Security.Get_childMenus("SHOWBACKDATEONLY", parentNodeName);
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

                string IS_A_BACKDATE_Module = (Convert.ToInt32(dr["SSM_ISALLOWBACKDT"] == DBNull.Value ? "0" : dr["SSM_ISALLOWBACKDT"]).ToString());
                if (IS_A_BACKDATE_Module == "1")
                {
                    //TN_CHILD.BackColor = Color.Green;
                    TN_CHILD.ForeColor = Color.Blue;
                }
                else
                {
                    //TN_CHILD.ForeColor = Color.SlateGray;
                    TN_CHILD.ForeColor = Color.SteelBlue;
                }
                parentNode.Nodes.Add(TN_CHILD);
                ADD_CHILD(ref TN_CHILD, TN_CHILD.Name.ToString());
                //string[]  _C=  dr["SSM_ANAL1"].ToString().Split('_');
                //if (_C.Length == level + 1)                                       
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                Boolean is_checked = e.Node.Checked;
                string menuName = e.Node.Name.ToString();
                if (is_checked == true)
                {
                    selected_Module_list.Add(menuName.ToString());

                    foreach (TreeNode child in e.Node.Nodes)
                    {
                        child.Checked = true;
                    }
                }
                else
                {
                    selected_Module_list.Remove(menuName.ToString());

                    foreach (TreeNode child in e.Node.Nodes)
                    {
                        child.Checked = false;
                    }
                }
                //dataGridView1.DataSource = null;
                //dataGridView1.DataSource = selected_Module_list;
                List<TreeNode> checked_NodesList = CheckedNodes(treeView1.Nodes);
                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = checked_NodesList;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnAllPc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnNonePc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClearPc_Click(object sender, EventArgs e)
        {
            // DataTable emptyDt = new DataTable();
            select_PC_List = new DataTable();
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = select_PC_List;
            //grvProfCents.DataSource = emptyDt;
        }

        private void btnAddPc_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucProfitCenterSearch1.Company;
                string chanel = ucProfitCenterSearch1.Channel;
                string subChanel = ucProfitCenterSearch1.SubChannel;
                string area = ucProfitCenterSearch1.Area;
                string region = ucProfitCenterSearch1.Regien;
                string zone = ucProfitCenterSearch1.Zone;
                string pc = ucProfitCenterSearch1.ProfitCenter;

                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                //-----------------------------------------------------
                foreach (DataRow drr in dt.Rows)
                {
                    string itmcd = drr["PROFIT_CENTER"].ToString();
                    // string descirption = drr["mi_shortdesc"].ToString();
                    var _duplicate = from _dup in select_PC_List.AsEnumerable()
                                     where _dup["PROFIT_CENTER"].ToString() == itmcd
                                     select _dup;
                    if (_duplicate.Count() != 0)
                    {
                        MessageBox.Show("Profit center(s) already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                        //DataRow DR2 = dataSource2.NewRow();
                        //DR2["mi_cd"] = itmcd;
                        //DR2["mi_shortdesc"] = descirption;
                        //dataSource2.Rows.Add(DR2);
                    }
                }
                //-----------------------------------------------------
                select_PC_List.Merge(dt);
                grvProfCents.DataSource = null;
                grvProfCents.AutoGenerateColumns = false;
                // grvProfCents.DataSource = dt;
                grvProfCents.DataSource = select_PC_List;
                this.btnAllPc_Click(sender, e);

                grvHistoryBackDates.DataSource = null;
                grvHistoryBackDates.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddLoc_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucLoactionSearch1.Company;
                string chanel = ucLoactionSearch1.Channel;
                string subChanel = ucLoactionSearch1.SubChannel;
                string area = ucLoactionSearch1.Area;
                string region = ucLoactionSearch1.Regien;
                string zone = ucLoactionSearch1.Zone;
                string pc = ucLoactionSearch1.ProfitCenter;

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
                        MessageBox.Show("Location(s) already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //-----------------------------------------------------           

                select_LOC_List.Merge(dt);
                GridAllLocations.DataSource = null;
                GridAllLocations.AutoGenerateColumns = false;
                GridAllLocations.DataSource = select_LOC_List;

                this.btnAllLoc_Click(sender, e);
                grvHistoryBackDates.DataSource = null;
                grvHistoryBackDates.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnAllLoc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GridAllLocations.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                GridAllLocations.EndEdit();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnNonLoc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GridAllLocations.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                GridAllLocations.EndEdit();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClearLoc_Click(object sender, EventArgs e)
        {
            //GridAllLocations.AutoGenerateColumns = false;
            //GridAllLocations.DataSource = null;
            select_LOC_List = new DataTable();
            GridAllLocations.DataSource = null;
            GridAllLocations.AutoGenerateColumns = false;
            GridAllLocations.DataSource = select_LOC_List;


            //select_PC_List = new DataTable();
            //grvProfCents.DataSource = null;
            //grvProfCents.AutoGenerateColumns = false;
            //grvProfCents.DataSource = select_PC_List;

        }

        private void btnOpenClose_Click(object sender, EventArgs e)
        {
            //if (btnOpenClose.Text == "V")
            //{
            //    btnOpenClose.Text = "/\\";
            //    panel_ViewHistory.Visible = true;
            //}
            //else
            //{
            //    btnOpenClose.Text = "V";
            //    panel_ViewHistory.Visible = false;
            //}
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

                //case CommonUIDefiniton.SearchUserControlType.Location:
                //    {
                //        paramsText.Append(txtViewCompany.Text + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.Company:
                //    {
                //        paramsText.Append(txtViewPc_Loc.Text + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                //    {
                //        paramsText.Append(txtViewCompany.Text.ToUpper().Trim() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                //    {
                //        paramsText.Append(txtViewCompany.Text.ToUpper().Trim() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.OPE:
                //    {
                //        paramsText.Append(ucLoactionSearch_Other.Company + seperator);
                //        break;
                //    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void ImgBtnSearchPcLoc_Click(object sender, EventArgs e)
        {
            //if (rdo_Pc.Checked == true)
            //{
            //    //load profit centers
            //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            //    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtViewPc_Loc;
            //    _CommonSearch.ShowDialog();
            //    txtViewPc_Loc.Select();
            //}
            //else if (rdo_Loc.Checked == true)
            //{
            //    //load locations
            //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            //    DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtViewPc_Loc;
            //    _CommonSearch.ShowDialog();
            //    txtViewPc_Loc.Select();
            //}
        }

        //private void button1_Click_1(object sender, EventArgs e)
        //{

        //}

        private void btnSearchCom_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            //DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtViewCompany;
            //_CommonSearch.ShowDialog();
            //txtViewCompany.Select();
        }

        private void ImgBtnViewBD_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------------
            /*
            if (rdo_Pc.Checked == false && rdo_Loc.Checked == false)
            {
                MessageBox.Show("Please select the option- PC or Location!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtViewCompany.Text == "")
            {
                MessageBox.Show("Please enter the company code!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtViewCompany.Focus();
                return;
            }
            if (txtViewPc_Loc.Text == "")
            {
                MessageBox.Show("Please enter the PC or Location code!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtViewPc_Loc.Focus();
                return;
            }
            string pc_or_loc = string.Empty;
            if (rdo_Pc.Checked == true)
            {
                pc_or_loc = "PC";
            }
            else if (rdo_Loc.Checked == true)
            {
                pc_or_loc = "LOC";
            }
            */
            //------------------------------------------------------------------------------------------
            try
            {
                #region
                string pc_or_loc = string.Empty;
                List<string> pc_list = new List<string>();
                List<string> loc_list = new List<string>();
                string COMPANY = "";
                if (tabControl1.SelectedIndex == 0)
                {
                    pc_or_loc = "PC";
                    pc_list = GetSelectedPCList(); //SELECTED PROFIT CENTER LIST
                    if (pc_list.Count < 1)
                    {
                        MessageBox.Show("Please select profit center(s)!");
                        return;
                    }
                    COMPANY = ucProfitCenterSearch1.Company;
                    //---------------------------------------------------
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
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    pc_or_loc = "LOC";
                    loc_list = GetSelectedLocationList(); //SELECTED LOCATION LIST
                    if (loc_list.Count < 1)
                    {
                        MessageBox.Show("Please select location(s)!");
                        return;
                    }
                    COMPANY = ucLoactionSearch1.Company;
                    //----------------------------------------------------
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
                        // dt = CHNLSVC.General.Get_backdates(txtViewCompany.Text.Trim().ToUpper(), txtViewPc_Loc.Text.Trim().ToUpper(), pc_or_loc, out backDatesList);
                        grvHistoryBackDates.DataSource = null;
                        grvHistoryBackDates.AutoGenerateColumns = false;
                        grvHistoryBackDates.DataSource = final_dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    MessageBox.Show("Select Profit Centers or Locations!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                #endregion

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            //grvHistoryBackDates
        }
        private void isAllowModules(out List<string> wrondModuleList)
        {

            List<string> wrondModuleList_return = new List<string>();
            try
            {
                List<TreeNode> checked_NodesList = CheckedNodes(treeView1.Nodes);

                foreach (TreeNode T_node in checked_NodesList)
                {
                    List<SystemMenus> list = new List<SystemMenus>();
                    CHNLSVC.Security.Get_Menu(T_node.Name.ToString(), out list);
                    if (tabControl1.SelectedIndex == 0)
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
                    if (tabControl1.SelectedIndex == 1)
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

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            wrondModuleList = wrondModuleList_return;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {   
                //**********************************************
                List<string> wrondModuleList = new List<string>();

                isAllowModules(out wrondModuleList);

                //updated by akila 2018/02/01
                List<TreeNode> _nodesList = CheckedNodes(treeView1.Nodes);
                foreach (TreeNode T_node in _nodesList)
                {
                    if (T_node.Name != "m_Trans_Finance_DayEnd")
                    {
                        //Akila 2017/04/03 Backdate validation
                        List<RefPrdMt> _perBlockList = CHNLSVC.Inventory.GET_REF_PRD_MT_DATA(new RefPrdMt()
                        {
                            Prd_stus = "CLOSE",
                            Prd_com_cd = ucProfitCenterSearch1.Company,
                            Prd_from = Convert.ToDateTime(TextBoxBDAFrom.Text),
                            Prd_to = Convert.ToDateTime(TextBoxBDATo.Text)
                        });
                        if (_perBlockList.Count > 0)
                        {
                            MessageBox.Show("Selected date does not allow to backdate. The period has closed", "Invalid Backdate", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                        }
                    }
                }                

                //BackDates _objBackDate = new BackDates();
                //if (wrondModuleList.Count > 0)
                //{
                //    bool _isAllow = false;
                //    foreach (string module in wrondModuleList)
                //    {
                //       _isAllow = CHNLSVC.General.IsAllowBackDateForModule(ucProfitCenterSearch1.Company, string.Empty, string.Empty, module, TextBoxBDAFrom.Value.ToShortDateString(), out _objBackDate);
                //       if (!_isAllow) { break; }
                //    }
                //    if (!_isAllow) { MessageBox.Show("Selected date does not allow to backdate. The period has closed", "Invalid Backdate", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                //}

                string wrongModule_concat = "";
                foreach (string module in wrondModuleList)
                {
                    wrongModule_concat = wrongModule_concat + "\n" + module;
                }
                if (wrondModuleList.Count > 0)
                {
                    if (tabControl1.SelectedIndex == 0)
                    {
                        MessageBox.Show("Following modules are not allowed for profit center backdates!\n" + wrongModule_concat, "Remove following modules", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (tabControl1.SelectedIndex == 1)
                    {
                        MessageBox.Show("Following modules are not allowed for location backdates!\n" + wrongModule_concat, "Remove following modules", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }
                //**********************Check Permission***********************
                if (tabControl1.SelectedIndex == 0)
                {
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 188))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to give back-dates for profit centers!\n( Advice: Reqired permission code :188)");
                    //    return;
                    //}
                    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "BDPC") == false)
                    {
                        MessageBox.Show("Sorry, You have no permission to give back-dates for profit centers!\n( Advice: Reqired permission code :BDPC)");
                        return;
                    }


                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 187))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to give back-dates for locations!\n( Advice: Reqired permission code :187)");
                    //    return;
                    //}
                    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "BDLOC") == false)
                    {
                        MessageBox.Show("Sorry, You have no permission to give back-dates for locations!\n( Advice: Reqired permission code :BDLOC)");
                        return;
                    }
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    if (rdoCompany.Checked == true)
                    {
                        //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 189))
                        //{
                        //    MessageBox.Show("Sorry, You have no permission to give back-dates for Company!\n( Advice: Reqired permission code :189)");
                        //    return;
                        //}
                        if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "BDCOM") == false)
                        {
                            MessageBox.Show("Sorry, You have no permission to give back-dates for Company!\n( Advice: Reqired permission code :BDCOM)");
                            return;
                        }
                    }
                    if (rdoChannel.Checked == true)
                    {
                        //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 191))
                        //{
                        //    MessageBox.Show("Sorry, You have no permission to give back-dates for Channels!\n( Advice: Reqired permission code :191)");
                        //    return;
                        //}
                        if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "BDCHNL") == false)
                        {
                            MessageBox.Show("Sorry, You have no permission to give back-dates for Channels!\n( Advice: Reqired permission code :BDCHNL)");
                            return;
                        }
                    }
                    if (rdoOPE.Checked == true)
                    {
                        //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 190))
                        //{
                        //    MessageBox.Show("Sorry, You have no permission to give back-dates for locations!\n( Advice: Reqired permission code :190)");

                        //    return;
                        //}
                        if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "BDCOPE") == false)
                        {
                            MessageBox.Show("Sorry, You have no permission to give back-dates for locations!\n( Advice: Reqired permission code :BDCOPE)");

                            return;
                        }
                    }
                }
                //*************************************************************

                //added on 21-03-2013----------------------
                TextBoxBDAFrom.Checked = true;
                TextBoxBDATo.Checked = true;

                TextBoxBDVFrom.Checked = true;
                TextBoxBDVTo.Checked = true;
                if (Convert.ToDateTime(TextBoxBDAFrom.Value).Date > Convert.ToDateTime(TextBoxBDATo.Value).Date)
                {
                    MessageBox.Show("'Allowed From' Date cannot be greater than 'Allowed To' Date", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToDateTime(TextBoxBDVFrom.Value) > Convert.ToDateTime(TextBoxBDVTo.Value))
                // if (TextBoxBDVFrom.Value < TextBoxBDVTo.Value)
                {
                    MessageBox.Show("'Valid From' cannot be greater than 'Valid To'", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //-----------------------------------------

                #region get selected modules list
                //List<string> Final_selected_Module_list = new List<string>();
                //List<SystemMenus> Final_modules = new List<SystemMenus>();

                //foreach (string moduleName in selected_Module_list)
                //{
                //    var _duplicate = from _dup in Final_selected_Module_list
                //                     where _dup == moduleName
                //                     select _dup;
                //    if (_duplicate.Count() == 0)
                //    {
                //        Final_selected_Module_list.Add(moduleName);
                //        SystemMenus module = new SystemMenus();
                //        module.Ssm_name = moduleName;
                //      //  module.Ssm_disp_name= 
                //        Final_modules.Add(module);
                //    }               
                //}          

                #endregion
                //-------------------------------------------------------------------------------------------
                #region Get checked modules list
                List<TreeNode> checked_NodesList = CheckedNodes(treeView1.Nodes);
                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = checked_NodesList;

                #endregion
                //------------------------------------------------------------------------------
                #region Get PC_List or Loc_List

                List<string> pc_list = null;
                // if(rdo_Pc.Checked==true )
                if (tabControl1.SelectedTab == tabPage1)//profit centers
                {
                    pc_list = GetSelectedPCList(); //SELECTED PROFIT CENTER LIST
                    if (pc_list.Count < 1)
                    {
                        MessageBox.Show("Please select profit center(s)!");
                        return;
                    }
                }
                //if(rdo_Loc.Checked==true)
                List<string> loc_list = null;
                if (tabControl1.SelectedTab == tabPage2)//locations
                {
                    loc_list = GetSelectedLocationList(); //SELECTED LOCATION LIST
                    if (loc_list.Count < 1)
                    {
                        MessageBox.Show("Please select location(s)!");
                        return;
                    }
                }
                if (tabControl1.SelectedTab == tabPage3)//Other
                {
                    if (rdoCompany.Checked == false && rdoOPE.Checked == false && rdoChannel.Checked == false)
                    {
                        MessageBox.Show("Please select one option!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //-----------------------------------
                    if (rdoCompany.Checked == true)
                    {
                        if (ucLoactionSearch_Other.Company == "")
                        {
                            MessageBox.Show("Please enter Company!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    //----------------------------------
                    if (rdoOPE.Checked == true)
                    {
                        if (txtOPE_code.Text == "")
                        {
                            MessageBox.Show("Please enter OPE code!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    //----------------------------------
                    if (rdoChannel.Checked == true)
                    {
                        if (ucLoactionSearch_Other.Channel == "")
                        {
                            MessageBox.Show("Please enter Channel!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (ucLoactionSearch_Other.Company == "")
                        {
                            MessageBox.Show("Please enter Company and Channel!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                #endregion
                //------------------------------------------------------------------------------
                #region Validations
                if (checked_NodesList.Count < 1)
                {
                    MessageBox.Show("Please select module(s)!", "No modules selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //     MessageBox.Show("Number of modules to be back dated: " + checked_NodesList.Count.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (MessageBox.Show("Are you sure to save?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                #endregion
                //------------------------------------------------------------------------------
                List<BackDates> backDateList = new List<BackDates>();
                MasterLocation _loc;
                MasterProfitCenter _pc;
                #region Process

                if (tabControl1.SelectedTab == tabPage3)//Other
                {
                    foreach (TreeNode T_node in checked_NodesList)
                    {
                        DateTime from = Convert.ToDateTime(TextBoxBDVFrom.Value);
                        DateTime to = Convert.ToDateTime(TextBoxBDVTo.Value);

                        BackDates _backdate = new BackDates();
                        _backdate.Gad_from_dt = from;
                        _backdate.Gad_to_dt = to;
                        _backdate.Gad_act_from_dt = Convert.ToDateTime(TextBoxBDAFrom.Text);
                        _backdate.Gad_act_to_dt = Convert.ToDateTime(TextBoxBDATo.Text);

                        _backdate.Gad_cre_by = BaseCls.GlbUserID;
                        _backdate.Gad_cre_dt = DateTime.Now;
                        _backdate.Gad_session_id = BaseCls.GlbUserSessionID;
                        _backdate.Gad_act = true;

                        _backdate.Gad_com = ucLoactionSearch_Other.Company;
                        if (rdoCompany.Checked == true)
                        {
                            _backdate.Gad_com = ucLoactionSearch_Other.Company;
                            _backdate.Gad_chnl = null;
                            _backdate.Gad_ope = null;
                            _backdate.Gad_loc = null;
                            _backdate.Gad_module = T_node.Name.ToString();
                            _backdate.Gad_rmk = "COM";
                            backDateList.Add(_backdate);
                        }
                        //----------------------------------
                        if (rdoOPE.Checked == true)
                        {
                            _backdate.Gad_com = ucLoactionSearch_Other.Company;
                            _backdate.Gad_ope = txtOPE_code.Text.Trim();
                            _backdate.Gad_chnl = null;
                            _backdate.Gad_loc = null;
                            _backdate.Gad_module = T_node.Name.ToString();
                            _backdate.Gad_rmk = "OPE";
                            backDateList.Add(_backdate);
                        }
                        //----------------------------------
                        if (rdoChannel.Checked == true)
                        {
                            _backdate.Gad_com = ucLoactionSearch_Other.Company;
                            _backdate.Gad_chnl = ucLoactionSearch_Other.Channel;
                            _backdate.Gad_ope = null;
                            _backdate.Gad_loc = null;
                            _backdate.Gad_module = T_node.Name.ToString();
                            _backdate.Gad_rmk = "CHNL";
                            backDateList.Add(_backdate);
                        }
                    }//foreach nodes
                }

                else if (tabControl1.SelectedTab == tabPage1)//PC
                {

                    foreach (string pc in pc_list)
                    {
                        foreach (TreeNode T_node in checked_NodesList)
                        {
                            DateTime from = Convert.ToDateTime(TextBoxBDVFrom.Value); //new DateTime(Convert.ToDateTime(TextBoxBDVFrom.Text).Year, Convert.ToDateTime(TextBoxBDVFrom.Text).Month, Convert.ToDateTime(TextBoxBDVFrom.Text).Day, Convert.ToInt32(TextBoxFrHH.Text), Convert.ToInt32(TextBoxFrMM.Text), 00);
                            DateTime to = Convert.ToDateTime(TextBoxBDVTo.Value);//new DateTime(Convert.ToDateTime(TextBoxBDVTo.Text).Year, Convert.ToDateTime(TextBoxBDVTo.Text).Month, Convert.ToDateTime(TextBoxBDVTo.Text).Day, Convert.ToInt32(TextBoxToHH.Text), Convert.ToInt32(TextBoxToMM.Text), 00);

                            BackDates _backdate = new BackDates();
                            _backdate.Gad_from_dt = from;
                            _backdate.Gad_to_dt = to;
                            _backdate.Gad_act_from_dt = Convert.ToDateTime(TextBoxBDAFrom.Value).Date;
                            _backdate.Gad_act_to_dt = Convert.ToDateTime(TextBoxBDATo.Value).Date;

                            _backdate.Gad_cre_by = BaseCls.GlbUserID;
                            _backdate.Gad_cre_dt = DateTime.Now;
                            _backdate.Gad_session_id = BaseCls.GlbUserSessionID;
                            _backdate.Gad_act = true;

                            try
                            {
                                _pc = CHNLSVC.General.GetPCByPCCode(ucProfitCenterSearch1.Company, pc);
                                _backdate.Gad_com = _pc.Mpc_com;
                                _backdate.Gad_ope = _pc.Mpc_ope_cd;
                                _backdate.Gad_chnl = _pc.Mpc_chnl;
                                _backdate.Gad_loc = _pc.Mpc_cd;
                                _backdate.Gad_module = T_node.Name.ToString();
                                _backdate.Gad_rmk = "PC";
                            }
                            catch (Exception ex)
                            {

                                _backdate.Gad_com = "";
                                _backdate.Gad_ope = "";
                                _backdate.Gad_chnl = "";
                                _backdate.Gad_loc = "";
                                _backdate.Gad_module = "";
                                _backdate.Gad_rmk = "PC";
                            }


                            backDateList.Add(_backdate);
                        }//foreach nodes
                    }//foreach pc

                    //************ADD ON 23-05-2013******************************************************************************************************
                    List<string> Finalized_pcList = IsFinalizedModule();
                    if (Finalized_pcList.Count > 0)
                    {
                        string fin_pcList = "";
                        foreach (string prof in Finalized_pcList)
                        {
                            fin_pcList = fin_pcList + "\n" + prof;
                        }

                        MessageBox.Show("Following Profit centers are finalized for the given back date.\n" + fin_pcList, "Finalized Profit centers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //MessageBox.Show("Following Profit centers","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    //******************************************************************************************************************
                }
                else if (tabControl1.SelectedTab == tabPage2)
                {
                    foreach (string loc in loc_list)
                    {
                        foreach (TreeNode T_node in checked_NodesList)
                        {
                            DateTime from = Convert.ToDateTime(TextBoxBDVFrom.Value); //new DateTime(Convert.ToDateTime(TextBoxBDVFrom.Text).Year, Convert.ToDateTime(TextBoxBDVFrom.Text).Month, Convert.ToDateTime(TextBoxBDVFrom.Text).Day, Convert.ToInt32(TextBoxFrHH.Text), Convert.ToInt32(TextBoxFrMM.Text), 00);
                            DateTime to = Convert.ToDateTime(TextBoxBDVTo.Value);//new DateTime(Convert.ToDateTime(TextBoxBDVTo.Text).Year, Convert.ToDateTime(TextBoxBDVTo.Text).Month, Convert.ToDateTime(TextBoxBDVTo.Text).Day, Convert.ToInt32(TextBoxToHH.Text), Convert.ToInt32(TextBoxToMM.Text), 00);

                            BackDates _backdate = new BackDates();
                            _backdate.Gad_from_dt = from;
                            _backdate.Gad_to_dt = to;
                            _backdate.Gad_act_from_dt = Convert.ToDateTime(TextBoxBDAFrom.Text);
                            _backdate.Gad_act_to_dt = Convert.ToDateTime(TextBoxBDATo.Text);

                            _backdate.Gad_cre_by = BaseCls.GlbUserID;
                            _backdate.Gad_cre_dt = DateTime.Now;
                            _backdate.Gad_session_id = BaseCls.GlbUserSessionID;
                            _backdate.Gad_act = true;

                            _loc = CHNLSVC.General.GetLocationByLocCode(ucLoactionSearch1.Company, loc);
                            _backdate.Gad_com = _loc.Ml_com_cd;
                            _backdate.Gad_ope = _loc.Ml_ope_cd;
                            _backdate.Gad_chnl = _loc.Ml_cate_2;
                            _backdate.Gad_loc = _loc.Ml_loc_cd;
                            _backdate.Gad_module = T_node.Name.ToString();
                            _backdate.Gad_rmk = "LOC";

                            backDateList.Add(_backdate);
                        }//foreach nodes
                    }//foreach loc
                }

                grvHistoryBackDates.DataSource = null;
                grvHistoryBackDates.Refresh();

                List<SystemMenus> list_menus = new List<SystemMenus>();
                DataTable DT = CHNLSVC.Security.Get_Menu(string.Empty, out list_menus);
                List<BackDates> finalBackDateList = new List<BackDates>();
                foreach (BackDates bd in backDateList)
                {
                    var _duplicate = from _dup in list_menus
                                     where _dup.Ssm_isallowbackdt == true && _dup.Ssm_name == bd.Gad_module
                                     select _dup;
                    if (_duplicate.Count() > 0)
                    {
                        finalBackDateList.Add(bd);
                    }
                }

                grvHistoryBackDates.DataSource = finalBackDateList;

                //*******************************************************************************************************************************
                #region check constraints
                //check valuvation
                //check financal or both status
                Int32 result = 0;
                foreach (BackDates _backdate in finalBackDateList)
                {
                    int _isSucessful = 0;
                    string _err = string.Empty;  
                    string temOpe = (_backdate.Gad_ope == null) ? "%" : _backdate.Gad_ope;

                    //if (!(_backdate.Gad_com == "ABL" | _backdate.Gad_com == "LRP" | _backdate.Gad_com == "SGL" | _backdate.Gad_com == "SGD" | _backdate.Gad_com == "AAL" | _backdate.Gad_com == "FIO" | _backdate.Gad_com == "JAD" | _backdate.Gad_com == "JAC" | _backdate.Gad_com == "PPL" | _backdate.Gad_com == "PDF" | _backdate.Gad_com == "ABE")) //Sanjeewa 2016-11-07
                    //{
                    //    _isSucessful = CHNLSVC.General.CheckSCMPeriodIsOpen(_backdate, out _err);
                    //    if (_isSucessful != 1)
                    //    {
                    //        MessageBox.Show(_err, "Back Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //}

                    _isSucessful = CHNLSVC.General.CheckBackDate(_backdate.Gad_com, temOpe);
                    bool dayEnd = false;
                    // if (_backdate.Gad_module == "DAYENDPROCESS")
                    if (_backdate.Gad_module == "m_Trans_Finance_DayEnd")
                    {
                        dayEnd = true;
                    }
                    if (_isSucessful <= 0)
                    {

                        //Check Currect Date transactions are allow or not :: Chamal 23-Dec-2013
                        if (chkNotAllowToday.Checked == true)
                        {
                            _backdate.Gad_alw_curr_trans = true;
                        }
                        else
                        {
                            _backdate.Gad_alw_curr_trans = false;
                        }

                        //*******************************************************************************
                        List<SystemMenus> list_menus2 = new List<SystemMenus>();
                        DataTable DT2 = new DataTable();
                        try
                        {
                            DT2 = CHNLSVC.Security.Get_Menu(_backdate.Gad_module, out list_menus2);
                        }
                        catch (Exception EX)
                        {
                            _isdayEnd = 0;
                        }
                        if (list_menus2[0].Ssm_needdayend == true)
                        {
                            _isdayEnd = 1;
                        }
                        //******************************************************************************
                        string _Err = string.Empty;
                        if (_isdayEnd > 0)
                        {
                            //save to log table
                            Day_End_Log _log = new Day_End_Log();
                            _log.Upd_com = _backdate.Gad_com;
                            _log.Upd_pc = _backdate.Gad_loc;
                            _log.Upd_log_by = BaseCls.GlbUserID;
                            _log.Upd_log_dt = CHNLSVC.Security.GetServerDateTime().Date;
                            _log.Upd_cre_by = BaseCls.GlbUserID;
                            _log.Upd_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                            _log.Upd_ov_wrt = true;

                            //overwrite dayend table
                            DayEnd _dayend = new DayEnd();
                            _dayend.Upd_com = _backdate.Gad_com;
                            _dayend.Upd_pc = _backdate.Gad_loc;
                            _dayend.Upd_cre_by = BaseCls.GlbUserID;
                            _dayend.Upd_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                            _dayend.Upd_ov_wrt = true;

                            if (_backdate.Gad_loc == null)
                            {
                                //save back date
                                result = CHNLSVC.General.SaveBackDate(_backdate, _isdayEnd, _log, _dayend, true, false, dayEnd, out _Err);
                                // Response.Redirect("../Security/BackDate.aspx");
                            }
                            else
                            {
                                //save back date
                                result = CHNLSVC.General.SaveBackDate(_backdate, _isdayEnd, _log, _dayend, false, false, dayEnd, out _Err);
                                // string Msgg = "<script>alert('Records Insert Sucessfully');window.location ='BackDate.aspx'</script>";
                                // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);                           
                            }
                        }
                        else
                        {
                            result = CHNLSVC.General.SaveBackDate(_backdate, _isdayEnd, null, null, false, false, dayEnd, out _Err);
                            // string Msgg = "<script>alert('Records Insert Sucessfully');window.location ='BackDate.aspx'</script>";
                            // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);

                        }
                        if (result == -1)
                        {
                            MessageBox.Show(_Err.ToString() , "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not allowed to save back date");
                        MessageBox.Show("Not allowed to save back date for: " + _backdate.Gad_loc, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (result > 0)
                {
                    MessageBox.Show("Modules are backdated sucessfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    redrawTree();
                }
                else
                {
                    MessageBox.Show("Not saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //this.btnClear_Click(null, null);
                if (tabControl1.SelectedTab == tabPage1 || tabControl1.SelectedTab == tabPage2)
                {
                    this.ImgBtnViewBD_Click(null, null);
                }
                ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
                ucProfitCenterSearch1.Channel = "";
                ucProfitCenterSearch1.SubChannel = "";
                ucProfitCenterSearch1.Area = "";
                ucProfitCenterSearch1.Regien = "";
                ucProfitCenterSearch1.Zone = "";
                ucProfitCenterSearch1.ProfitCenter = "";

                ucLoactionSearch1.Company = BaseCls.GlbUserComCode;
                ucLoactionSearch1.Channel = "";
                ucLoactionSearch1.SubChannel = "";
                ucLoactionSearch1.Area = "";
                ucLoactionSearch1.Regien = "";
                ucLoactionSearch1.Zone = "";
                ucLoactionSearch1.ProfitCenter = "";

                ucLoactionSearch_Other.Company = BaseCls.GlbUserComCode;
                ucLoactionSearch_Other.Channel = "";
                ucLoactionSearch_Other.SubChannel = "";
                ucLoactionSearch_Other.Area = "";
                ucLoactionSearch_Other.Regien = "";
                ucLoactionSearch_Other.Zone = "";
                ucLoactionSearch_Other.ProfitCenter = "";

                //*********************commented******************************************
                //foreach (DataGridViewRow grv in grvHistoryBackDates.Rows)
                //{
                //    try
                //    {
                //        string modID = grv.Cells["GAD_MODULE"].Value.ToString();
                //        DataTable MENU = CHNLSVC.Security.Get_Menu(modID, out list_menus);
                //        grv.Cells["ssm_disp_name"].Value = MENU.Rows[0]["SSM_DISP_NAME"].ToString();
                //    }
                //    catch (Exception ex)
                //    {
                //    }
                //}
                //***************************************************************************
                #endregion check constraints
                #endregion

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private List<string> GetSelectedPCList()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvProfCents.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }

            }
            return list;
        }
        private List<string> GetSelectedLocationList()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in GridAllLocations.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }

            }
            return list;
        }
        List<TreeNode> CheckedNodes(System.Windows.Forms.TreeNodeCollection theNodes)
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
                ucProfitCenterSearch1.Channel = "";
                ucProfitCenterSearch1.SubChannel = "";
                ucProfitCenterSearch1.Area = "";
                ucProfitCenterSearch1.Regien = "";
                ucProfitCenterSearch1.Zone = "";
                ucProfitCenterSearch1.ProfitCenter = "";

                ucLoactionSearch1.Company = BaseCls.GlbUserComCode;
                ucLoactionSearch1.Channel = "";
                ucLoactionSearch1.SubChannel = "";
                ucLoactionSearch1.Area = "";
                ucLoactionSearch1.Regien = "";
                ucLoactionSearch1.Zone = "";
                ucLoactionSearch1.ProfitCenter = "";

                ucLoactionSearch_Other.Company = BaseCls.GlbUserComCode;
                ucLoactionSearch_Other.Channel = "";
                ucLoactionSearch_Other.SubChannel = "";
                ucLoactionSearch_Other.Area = "";
                ucLoactionSearch_Other.Regien = "";
                ucLoactionSearch_Other.Zone = "";
                ucLoactionSearch_Other.ProfitCenter = "";

                this.btnClearPc_Click(null, null);
                this.btnClearLoc_Click(null, null);

                grvHistoryBackDates.DataSource = null;
                grvHistoryBackDates.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                BackDate formnew = new BackDate();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtViewCompany_DoubleClick(object sender, EventArgs e)
        {
            this.btnSearchCom_Click(sender, e);
        }

        private void txtViewPc_Loc_DoubleClick(object sender, EventArgs e)
        {
            this.ImgBtnSearchPcLoc_Click(sender, e);
        }

        private void txtViewCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImgBtnSearchPcLoc_Click(sender, e);
            }
        }

        private void txtViewCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    txtViewPc_Loc.Focus();
            //}
        }

        private void txtViewPc_Loc_Enter(object sender, EventArgs e)
        {
            //if (txtViewCompany.Text == "")
            //{
            //    MessageBox.Show("Please enter Company", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtViewCompany.Focus();
            //}
        }

        private void rdoChannel_Enter(object sender, EventArgs e)
        {


        }

        private void rdoChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoChannel.Checked == true)
            {
                if (ucLoactionSearch_Other.Channel == "")
                {
                    MessageBox.Show("Please enter Channel first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ucLoactionSearch_Other.Company == "")
                {
                    MessageBox.Show("Please enter Company and Channel!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void rdoCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCompany.Checked == true)
            {
                if (ucLoactionSearch_Other.Company == "")
                {
                    MessageBox.Show("Please enter Company first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void rdoOPE_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOPE.Checked == true)
            {
                if (txtOPE_code.Text == "")
                {
                    MessageBox.Show("Please enter OPE code!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void ImgBtnOPE_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucLoactionSearch_Other.Company == "")
                {
                    MessageBox.Show("Please enter Company!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
                DataTable _result = CHNLSVC.CommonSearch.GetOPE(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOPE_code;
                _CommonSearch.ShowDialog();
                txtOPE_code.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            //MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
            //DataTable dataSource = CHNLSVC.CommonSearch.GetOPE(MasterCommonSearchUCtrl.SearchParams, null, null);
            //MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            //MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //MasterCommonSearchUCtrl.ReturnResultControl = TextBoxPAT.ClientID;
            //MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                redrawTree();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void redrawTree()
        {
            treeView1.Nodes.Clear();
            TreeNode TN = new TreeNode();
            TN.Name = "m";
            TN.Text = "Modules";

            treeView1.Nodes.Add(TN);
            ADD_CHILD(ref TN, TN.Name.ToString());

            treeView1.ExpandAll();
        }

        private void TextBoxBDAFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxBDATo.Focus();
            }

        }

        private void TextBoxBDATo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxBDVFrom.Focus();
            }
        }

        private void TextBoxBDVFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxBDVTo.Focus();
            }
        }
        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    string FROM_V = Convert.ToDateTime(TextBoxBDVFrom.Value).ToLocalTime().ToString();
        //    string TO_V   = Convert.ToDateTime(TextBoxBDVTo.Value).ToString();
        //    MessageBox.Show("FROM_V:" + FROM_V + "\nTO_V: " + TextBoxBDVTo);
        //}

        #region
        //private void check_child_nodes( TreeNode parentNode)
        //{ 
        //    parentNode.Checked=true;
        //    if (parentNode.GetNodeCount(true) > 0)
        //    {
        //        foreach (TreeNode child in parentNode.Nodes)
        //        {
        //            child.Checked = true;
        //            check_child_nodes(child);
        //        }
        //    }           

        //}
        //private void un_check_child_nodes( TreeNode parentNode)
        //{
        //    parentNode.Checked = false;
        //    if (parentNode.GetNodeCount(true) > 0)
        //    {
        //        foreach (TreeNode child in parentNode.Nodes)
        //        {
        //            child.Checked = false;
        //            check_child_nodes(child);
        //        }
        //    }          

        //}
        #endregion


        private List<string> IsFinalizedModule()
        {
            List<string> Fin_pc_list = new List<string>();
            List<string> pc_list = null;
            // if(rdo_Pc.Checked==true )
            if (tabControl1.SelectedTab == tabPage1)//profit centers
            {
                pc_list = GetSelectedPCList(); //SELECTED PROFIT CENTER LIST
                if (pc_list.Count < 1)
                {
                    // MessageBox.Show("Please select profit center(s)!");
                    return Fin_pc_list;
                }
            }
            else
            {
                return Fin_pc_list;
            }

            foreach (string PC in pc_list)
            {
                DateTime FRMdt = TextBoxBDAFrom.Value;
                //CALL PROC
                Boolean IS_Finalized = CHNLSVC.Financial.Is_PC_Finalized(ucProfitCenterSearch1.Company, PC, FRMdt);
                if (IS_Finalized == true)
                {
                    Fin_pc_list.Add(PC);
                }
            }
            return Fin_pc_list;
            //---------------------------------------------------------------------------------------
            //List<TreeNode> checked_NodesList = CheckedNodes(treeView1.Nodes);

            //foreach (TreeNode T_node in checked_NodesList)
            //{
            //    List<SystemMenus> list = new List<SystemMenus>();
            //    CHNLSVC.Security.Get_Menu(T_node.Name.ToString(), out list);
            //    if (tabControl1.SelectedIndex == 0)
            //    {
            //        if (list.Count > 0)
            //        {
            //            //if (list[0].Ssm_menu_tp != "F")
            //            if (list[0].Ssm_menu_tp == "F")
            //            {
            //                //if (list[0].Ssm_menu_tp != "" && list[0].Ssm_menu_tp != "m")
            //                //{
            //                //    //wrondModuleList_return.Add(list[0].Ssm_disp_name.ToString());
            //                //}
            //                //TODO: TEST
            //                foreach (string PC in pc_list)
            //                {
            //                   DateTime FRMdt= TextBoxBDAFrom.Value;
            //                   //CALL PROC
            //                   Boolean IS_Finalized= CHNLSVC.Financial.Is_PC_Finalized(ucProfitCenterSearch1.Company, PC, FRMdt);
            //                   if (IS_Finalized==true)
            //                   {
            //                        Fin_pc_list.Add(PC);
            //                   }
            //                }
            //            }

            //        }

            //    }

            //}
            return Fin_pc_list;
        }
    }
}
