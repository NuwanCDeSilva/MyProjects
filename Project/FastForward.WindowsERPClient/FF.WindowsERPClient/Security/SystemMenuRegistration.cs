using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using FF.BusinessObjects;
using System.Windows.Forms;

namespace FF.WindowsERPClient.Security
{
    public partial class SystemMenuRegistration : Base
    {
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();

        #region Initialize Component
        public SystemMenuRegistration()
        {
            try
            {
                InitializeComponent();
                //treeView1.CheckBoxes = true;
                NewMenu();
                ExsitingMenu();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Load New Items
        private void NewMenu()
        {
            dtNew = new DataTable();
            DataColumn dataColumn = new DataColumn("SelectAll", System.Type.GetType("System.Boolean"));
            dtNew.Columns.Add(dataColumn);
            dataColumn = new DataColumn("SSM_NAME", System.Type.GetType("System.String"));
            dtNew.Columns.Add(dataColumn);
            dataColumn = new DataColumn("SSM_DISP_NAME", System.Type.GetType("System.String"));
            dtNew.Columns.Add(dataColumn);

            MainMenu _mainMenu = new MainMenu();
            dt = CHNLSVC.Security.GetUserSystemMenus("ALL", "ALL");
            if (dt.Rows.Count > 0) menuActive(_mainMenu.mnuMain);
            dvNewItems.DataSource = dtNew;
        }
        #endregion

        #region Load Exsiting Menu Items
        private void ExsitingMenu()
        {
            treeView1.Nodes.Clear();   
            TreeNode _treeNode = new TreeNode();
            _treeNode.Name = "m";
            _treeNode.Text = "Main";
            treeView1.Nodes.Add(_treeNode);
            GetSubMenu(ref _treeNode, _treeNode.Name.ToString());
            ShoudAutoExpand(_treeNode);
        }

        private void GetSubMenu(ref TreeNode parentNode, string parentNodeName)
        {
            DataTable dt = CHNLSVC.Security.Get_childMenus("SHOWALL", parentNodeName);
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode _treeNode = new TreeNode();
                _treeNode.Name = dr["SSM_NAME"].ToString();
                if (Convert.ToInt16(dr["SSM_ACT"].ToString()) == 1)
                {
                    _treeNode.Text = dr["SSM_DISP_NAME"].ToString() + " [Active]";
                }
                else
                {
                    _treeNode.Text = dr["SSM_DISP_NAME"].ToString() + " [Inactive]";
                }
                parentNode.Nodes.Add(_treeNode);
                GetSubMenu(ref _treeNode, _treeNode.Name.ToString());
            }
        }

        private void ShoudAutoExpand(TreeNode tn)
        {
            if (tn.Level == 0)
                tn.Expand();
        }

        #endregion

        #region Add New Menus
        private void AddNewMenu(string _menuName, string _menuDispName)
        {

            DataRow dataRow = dtNew.NewRow();
            dataRow["SelectAll"] = false;
            dataRow["SSM_NAME"] = _menuName;
            dataRow["SSM_DISP_NAME"] = _menuDispName.Replace("&", "");
            dtNew.Rows.Add(dataRow);
        }
        #endregion

        #region Load User Privilage Menus
        private void menuActive(MenuStrip menus)
        {
            foreach (ToolStripMenuItem menu in menus.Items)
            {
                if (menu.Name.ToString() == "windowsMenu") continue;
                if (menu.Name.ToString() == "helpMenu") continue;

                if (checkMenu(menu.Name.ToString()) == "F")
                {
                    AddNewMenu(menu.Name.ToString(), menu.Text.ToString());
                }
                activateItems(menu);
            }
        }

        private string checkMenu(string _menuItem)
        {
            string _name = "F";
            var _exist = dt.AsEnumerable().Where(x => x.Field<string>("SSM_NAME") == _menuItem).Select(y => y.Field<string>("SSM_DISP_NAME")).ToList();
            if (_exist != null) if (_exist.Count > 0) _name = Convert.ToString(_exist[0]);
            return _name;
        }

        private void activateItems(ToolStripMenuItem item)
        {
            for (int i = 0; i < item.DropDown.Items.Count; i++)
            {
                ToolStripItem subItem = item.DropDown.Items[i];
                //Console.WriteLine(subItem.Name.ToString());
                if (checkMenu(subItem.Name.ToString()) == "F")
                {
                    if ("System.Windows.Forms.ToolStripSeparator" == subItem.GetType().ToString()) continue;
                    AddNewMenu(subItem.Name.ToString(), subItem.Text.ToString());
                }

                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem _subItem = subItem as ToolStripMenuItem;
                    if (_subItem == null) continue;
                    activateItems(_subItem);
                }
            }
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvNewItems.Rows.Count > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    bool _found = false;
                    for (int i = 0; i < dvNewItems.RowCount; i++)
                    {
                        if (dvNewItems.Rows[i].Cells["SelectAll"].Value.ToString().ToUpper() == "TRUE")
                        {
                            SystemMenus _menuItem = new SystemMenus();
                            _menuItem.Ssm_name = dvNewItems.Rows[i].Cells["SSM_NAME"].Value.ToString();
                            _menuItem.Ssm_disp_name = dvNewItems.Rows[i].Cells["SSM_DISP_NAME"].Value.ToString();
                            //_menuItem.Ssm_menu_tp =string.Empty; 
                            _menuItem.Ssm_tp = "M";
                            _menuItem.Ssm_isallowbackdt = false;
                            //_menuItem.Ssm_needdayend= false;
                            _menuItem.Ssm_act = true;
                            _menuItem.Ssm_anal1 = _menuItem.Ssm_name.Replace("_" + _menuItem.Ssm_name.Split('_').Last(), "");
                            _menuItem.Ssm_cre_by = BaseCls.GlbUserID;
                            CHNLSVC.Security.SaveSystemMenu(_menuItem);
                            _found = true;
                        }
                    }
                    if (_found == true)
                    {
                        MessageBox.Show("New menu(s) updated successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NewMenu();
                        ExsitingMenu();
                    }
                    else
                    {
                        MessageBox.Show("Selected items not found!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region Select/Deselect
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                if (dvNewItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dvNewItems.RowCount; i++)
                    {
                        dvNewItems.Rows[i].Cells["SelectAll"].Value = true;
                    }
                    //chkSelectAll.Text = "Deselect All";
                }
            }
            else
            {
                if (dvNewItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dvNewItems.RowCount; i++)
                    {
                        dvNewItems.Rows[i].Cells["SelectAll"].Value = false;
                    }
                    //chkSelectAll.Text = "Select All";
                }
            }
        }
        #endregion

        #region Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                NewMenu();
                ExsitingMenu();
                chkSelectAll.Checked = false;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void dvNewItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dvNewItems.IsCurrentCellDirty)
            {
                dvNewItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
