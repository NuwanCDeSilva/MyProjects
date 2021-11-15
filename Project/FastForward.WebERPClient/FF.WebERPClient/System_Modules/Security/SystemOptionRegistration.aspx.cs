using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FF.WebERPClient.UserControls;

namespace FF.WebERPClient.System_Modules.Security
{
    public partial class SystemOptionRegistration : BasePage
    {
        string txtMessage = string.Empty;
        static Int32 _newOptID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSystemOptions(treeView);
            }
        }

        #region PageValidation

        #endregion

        #region  Load System Option
        /// <summary>
        /// Refer the _allSystemOption which allocated data to make tree structure
        /// </summary>
        List<SystemOption> _allSystemOptions = null;
        /// <summary>
        /// Load System Options to the tree view
        /// </summary>
        private void LoadSystemOptions(TreeView _treeView)
        {

            _allSystemOptions = CHNLSVC.Security.GetAllSystemOptions();
            var _parentSystemOption = from _List in _allSystemOptions
                                      where _List.Ssp_parentid == 0
                                      orderby _List.Ssp_orgposition
                                      select new SystemOption
                                      {
                                          Ssp_act = _List.Ssp_act,
                                          Ssp_desc = _List.Ssp_desc,
                                          Ssp_isenabled = _List.Ssp_isenabled,
                                          Ssp_ishide = _List.Ssp_ishide,
                                          Ssp_optid = _List.Ssp_optid,
                                          Ssp_orgposition = _List.Ssp_orgposition,
                                          Ssp_parentid = _List.Ssp_parentid,
                                          Ssp_title = _List.Ssp_title,
                                          Ssp_url = _List.Ssp_url
                                      };

            TreeNode _treeNodeMaster = new TreeNode();
            _treeNodeMaster.Text = "System Options";
            _treeNodeMaster.ImageToolTip = "1;System Options;1;0;0;0;0;System Options;0";

            foreach (var _parent in _parentSystemOption)
            {
                TreeNode _treeNode = new TreeNode();
                _treeNode = SetNodeProperty(_treeNode, _parent);
                CreateChild(_treeNode, (int)_parent.Ssp_optid);
                _treeNodeMaster.ChildNodes.Add(_treeNode);
            }
            _treeView.Nodes.Add(_treeNodeMaster);
            _treeView.ExpandAll();

        }
        /// <summary>
        /// Common procedure to create root to leaf in tree view
        /// </summary>
        /// <param name="_node"> Used for store rootnode/chiled node </param>
        /// <param name="_optionID">Used to store option id which check whether having child for the above node</param>
        private void CreateChild(TreeNode _node, Int32 _optionID)
        {
            var _childSystemOption = from _List in _allSystemOptions
                                     where _List.Ssp_parentid != -1 && _List.Ssp_parentid == _optionID
                                     orderby _List.Ssp_orgposition
                                     select new SystemOption
                                     {
                                         Ssp_act = _List.Ssp_act,
                                         Ssp_desc = _List.Ssp_desc,
                                         Ssp_isenabled = _List.Ssp_isenabled,
                                         Ssp_ishide = _List.Ssp_ishide,
                                         Ssp_optid = _List.Ssp_optid,
                                         Ssp_orgposition = _List.Ssp_orgposition,
                                         Ssp_parentid = _List.Ssp_parentid,
                                         Ssp_title = _List.Ssp_title,
                                         Ssp_url = _List.Ssp_url
                                     };

            foreach (var _child in _childSystemOption)
            {
                TreeNode _treeChild = new TreeNode();
                _treeChild = SetNodeProperty(_treeChild, _child);
                _node.ChildNodes.Add(_treeChild);
                CreateChild(_treeChild, (int)_child.Ssp_optid);
            }


        }
        /// <summary>
        /// Common function to set the node property/store user define property
        /// </summary>
        /// <param name="_node">Used to store node which propery assign</param>
        /// <param name="_list">System option list to set the property</param>
        /// <returns></returns>
        private TreeNode SetNodeProperty(TreeNode _node, SystemOption _list)
        {
            try
            {
                _node.ToolTip = _list.Ssp_url;
                _node.Text = _list.Ssp_title;
                string _obj = string.Empty;
                _obj = _list.Ssp_act + ";" + _list.Ssp_desc + ";" + _list.Ssp_isenabled.ToString() + ";" + _list.Ssp_ishide.ToString() + ";" + _list.Ssp_optid.ToString() + ";" + _list.Ssp_orgposition.ToString() + ";" + _list.Ssp_parentid.ToString() + ";" + _list.Ssp_title + ";" + _list.Ssp_url + ";";
                _node.ImageToolTip = _obj;

            }

            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
            }


            return _node;
        }
        /// <summary>
        /// Common function to get the property which assign by table/user
        /// </summary>
        /// <param name="_treeValue">Used to store value string which split and assign for the systemoption object</param>
        /// <returns></returns>
        private SystemOption GetProperty(string _treeValue)
        {
            SystemOption _opt = new SystemOption();
            try
            {
                object[] _obj = _treeValue.Split(';');
                _opt.Ssp_act = Convert.ToInt32(_obj[0]);
                _opt.Ssp_desc = _obj[1].ToString();
                _opt.Ssp_isenabled = Convert.ToInt32(_obj[2]);
                _opt.Ssp_ishide = Convert.ToInt32(_obj[3]);
                _opt.Ssp_optid = Convert.ToInt32(_obj[4]);
                _opt.Ssp_orgposition = Convert.ToInt32(_obj[5]);
                _opt.Ssp_parentid = Convert.ToInt32(_obj[6]);
                _opt.Ssp_title = _obj[7].ToString();
                _opt.Ssp_url = _obj[8].ToString();
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
            }

            return _opt;

        }
        /// <summary>
        /// Set the screen property to the system option object and return a System Option BO
        /// </summary>
        /// <param name="_active">Used to allocate status</param>
        /// <param name="_desc">Used to allocate description</param>
        /// <param name="_isEnable">Used to allocate enable/disable</param>
        /// <param name="_isHide">Used to allocate hide/show</param>
        /// <param name="_optID">Used to allocate optional ID</param>
        /// <param name="_orgPosition">Used to allocate organize position</param>
        /// <param name="_parentID">Used to allocate parent ID</param>
        /// <param name="_title">Used to allocate title</param>
        /// <param name="_url">Used to allocate url</param>
        /// <returns></returns>
        private SystemOption GetProperty(Int32? _active, string _desc, Int32? _isEnable, Int32? _isHide, Int32? _optID, Int32? _orgPosition, Int32? _parentID, string _title, string _url)
        {
            SystemOption _opt = new SystemOption();
            try
            {
                _opt.Ssp_act = Convert.ToInt32(_active);
                _opt.Ssp_desc = _desc;
                _opt.Ssp_isenabled = Convert.ToInt32(_isEnable);
                _opt.Ssp_ishide = Convert.ToInt32(_isHide);
                _opt.Ssp_optid = Convert.ToInt32(_optID);
                _opt.Ssp_orgposition = Convert.ToInt32(_orgPosition);
                _opt.Ssp_parentid = Convert.ToInt32(_parentID);
                _opt.Ssp_title = _title;
                _opt.Ssp_url = _url;
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
            }
            return _opt;
        }

        #endregion

        #region Bind Properties to the screen
        private void BindNodePropertyToScreen(SystemOption _opt)
        {
            try
            {
                chkIsActive.Checked = _opt.Ssp_act == 0 ? false : true;
                txtDescription.Text = _opt.Ssp_desc;
                chkIsEnable.Checked = _opt.Ssp_isenabled == 0 ? false : true;
                chkIsHide.Checked = _opt.Ssp_ishide == 0 ? false : true;
                txtTitle.Text = _opt.Ssp_title;
                txtUrl.Text = _opt.Ssp_url;
                txtParentID.Text = _opt.Ssp_parentid.ToString();
                txtOrdinalPosition.Text = _opt.Ssp_orgposition.ToString();
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
            }

        }
        #endregion

        #region Select a Node
        protected void treeView_Click(object sender, EventArgs e)
        {

            TreeNode _node = treeView.SelectedNode;
            if (_node.ValuePath != "System Options")
            {
                SystemOption _opt = GetProperty(_node.ImageToolTip);
                BindNodePropertyToScreen(_opt);
            }
            else
            {
                ClearTextBox();
            }
        }
        #endregion

        #region Add System Option

        protected void btnAddNew_Click(Object sender, EventArgs e)
        {
            ClearTextBox();
            chkIsActive.Checked = true;
            chkIsEnable.Checked = true;
            chkIsHide.Checked = false;
            btnAddNew.Enabled = false;
            btnCancel.Enabled = true;

        }

        #endregion

        #region Update
        protected void btnUpdate_Click(Object sender, EventArgs e)
        {
            //check title for empty
            if (txtTitle.Text == string.Empty || txtTitle.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the title!");
                return;
            }

            //check Description for empty
            if (txtDescription.Text == string.Empty || txtDescription.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the description!");
                return;
            }

            //check Orgniaze position for empty
            if (txtOrdinalPosition.Text == string.Empty || txtOrdinalPosition.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the organize position!");
                return;
            }

            if (btnAddNew.Enabled == false)
            {

                if (treeView.SelectedNode == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the menu item");
                    return;
                }

                if (txtTitle.Text == null || txtTitle.Text == "") MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the system option Title");

                if (treeView.SelectedNode.ToolTip.ToString() != "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected Node is having a assigned page. You can not add a sub menu");
                    return;
                }

                string PreviousNode = treeView.SelectedNode.ValuePath.ToString();
                PreviousNode = PreviousNode + "/" + txtTitle.Text;
                if (treeView.FindNode(PreviousNode) != null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Menu item name is duplicating.Please refer a new menu name");
                    return;

                }

                if (_newOptID == 0)
                    _newOptID = Convert.ToInt32(CHNLSVC.Security.GetMaxOptionID());
                else
                    _newOptID += 1;

                TreeNode treeNode = new TreeNode();
                try
                {

                    SystemOption _optParent = new SystemOption();
                    _optParent = GetProperty(treeView.SelectedNode.ImageToolTip);
                    SystemOption _optChild = new SystemOption();
                    _optChild = GetProperty(_optParent.Ssp_act, txtDescription.Text, _optParent.Ssp_isenabled, _optParent.Ssp_ishide, _newOptID, Convert.ToInt32(txtOrdinalPosition.Text), _optParent.Ssp_optid, txtTitle.Text, txtUrl.Text);
                    treeNode = SetNodeProperty(treeNode, _optChild);
                    treeNode.Text = txtTitle.Text;
                    string name = treeView.SelectedValue.ToString();
                    treeView.FindNode(treeView.SelectedNode.ValuePath.ToString()).ChildNodes.Add(treeNode);
                    CHNLSVC.Security.UpdateSystemOption(_optChild, GlbUserName, GlbUserSessionID);
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully added!");
                }
                catch (Exception ex)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);

                }

                finally
                {
                    treeNode.Expand();
                    ClearTextBox();
                    btnAddNew.Enabled = true;
                    btnCancel.Enabled = false;
                }

            }
            else
            {


                //cheking given node is duplicating
                TreeNode _node = treeView.SelectedNode;
                //Check the seleced node child availability
                if (_node.ChildNodes.Count > 0 && txtUrl.Text.Trim() != "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected node already having sub menus. Therefore you can not add url for the selected node.");
                    return;
                }
                //get the selected node properties
                SystemOption _opt = GetProperty(_node.ImageToolTip);


                //check for dupicate title in current treeview
                TreeNode _nodeExist = treeView.FindNode(Server.HtmlEncode(txtTitle.Text));
                if (_nodeExist != null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected title already available. Please select a different title.");
                    return;
                }

                Int16? effect = null;
                //check for duplicate Organize position
                effect = CHNLSVC.Security.CheckSystemOptionOganizePosition(Convert.ToInt16(_opt.Ssp_optid), Convert.ToInt16(txtOrdinalPosition.Text));
                if (effect.HasValue)
                {
                    if (effect == 1)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected organize position already available. Please select a different position");
                        return;
                    }
                }

                //set the node property with the parentID with other details
                SystemOption _optUser = new SystemOption
                {
                    Ssp_act = chkIsActive.Checked ? 1 : 0,
                    Ssp_desc = txtDescription.Text,
                    Ssp_isenabled = chkIsEnable.Checked ? 1 : 0,
                    Ssp_ishide = chkIsHide.Checked ? 1 : 0,
                    Ssp_optid = _opt.Ssp_optid,
                    Ssp_orgposition = Convert.ToInt32(txtOrdinalPosition.Text),
                    Ssp_parentid = Convert.ToInt32(_opt.Ssp_parentid),
                    Ssp_title = txtTitle.Text,
                    Ssp_url = txtUrl.Text == string.Empty || txtUrl.Text.Trim() == "" ? "" : txtUrl.Text.Trim()
                };
                try
                {
                    _node = SetNodeProperty(_node, _optUser);
                    CHNLSVC.Security.UpdateSystemOption(_optUser, GlbUserName,GlbUserSessionID);
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully updated!");
                }
                catch (Exception ex)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
                }

                finally
                {
                    ClearTextBox();
                }
            }
            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        #endregion

        #region Canceling
        protected void btnCancel_Click(Object sender, EventArgs e)
        {
            btnAddNew.Enabled = true;
            btnCancel.Enabled = false;
            ClearTextBox();
        }

        #endregion

        #region Clear TextBox
        private void ClearTextBox()
        {
            txtDescription.Text = "";
            txtMessage = "";
            txtOrdinalPosition.Text = "";
            txtParentID.Text = "";
            txtTitle.Text = "";
            txtUrl.Text = "";

        }
        #endregion
    }
}

