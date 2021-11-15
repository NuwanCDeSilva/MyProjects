using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;

namespace FF.AbansTours.UserControls
{
    public partial class uc_MainMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
                if (Session["UserID"] != null)
                {
                    LoadSystemOptions(MainMenu, Session["UserID"].ToString());
                }
            //}
        }

        #region  Load System Option
        /// <summary>
        /// Refer the _allSystemOption which allocated data to make tree structure
        /// </summary>
        List<SystemOption> _allSystemOptions = null;
        /// <summary>
        /// Load System Options to the tree view
        /// </summary>
        private void LoadSystemOptions(Menu _treeView, string user)
        {
            BasePage basepage = new BasePage();


            //lock (MainMenu)
            //{

            //_treeView = HttpContext.Current.Cache["rootNode"] as Menu;
            //if (_treeView == null || _treeView.Items.Count == 0)
            //{
            //    _treeView = MainMenu;
            //    Clear();
            
            if (user.ToString().ToUpper() == "ADMIN" )
            _allSystemOptions = basepage.CHNLSVC.Security.GetAllSystemOptions();
            else
            _allSystemOptions = basepage.CHNLSVC.Security.GetUserSystemOptions(user);
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



            foreach (var _parent in _parentSystemOption)
            {
                MenuItem _treeNode = new MenuItem();
                _treeNode = SetNodeProperty(_treeNode, _parent);
                CreateChild(_treeNode, (int)_parent.Ssp_optid);
                _treeView.Items.Add(_treeNode);
            }
            //_treeView.Items.Add(_treeView);
            //_treeView.ExpandAll();

            //HttpContext.Current.Cache.Insert("rootNode", MainMenu, null,
            //   DateTime.Now.AddSeconds(2000), TimeSpan.Zero);

            //}
            //else
            //{
            //    MainMenu = HttpContext.Current.Cache["rootNode"] as Menu;
            //}
            //}

            
        }
        /// <summary>
        /// Common procedure to create root to leaf in tree view
        /// </summary>
        /// <param name="_node"> Used for store rootnode/chiled node </param>
        /// <param name="_optionID">Used to store option id which check whether having child for the above node</param>
        private void CreateChild(MenuItem _node, Int32 _optionID)
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
                MenuItem _treeChild = new MenuItem();
                _treeChild = SetNodeProperty(_treeChild, _child);
                _node.ChildItems.Add(_treeChild);
                CreateChild(_treeChild, (int)_child.Ssp_optid);

            }


        }
        /// <summary>
        /// Common function to set the node property/store user define property
        /// </summary>
        /// <param name="_node">Used to store node which propery assign</param>
        /// <param name="_list">System option list to set the property</param>
        /// <returns></returns>
        private MenuItem SetNodeProperty(MenuItem _node, SystemOption _list)
        {
            try
            {

                if (_list.Ssp_ishide != 1)
                {

                    //_node.ToolTip = _list.Ssp_url;
                    _node.ToolTip = _list.Ssp_title;
                    _node.NavigateUrl = _list.Ssp_url;
                    _node.Text = _list.Ssp_title;
                    _node.Enabled = _list.Ssp_isenabled == 1 ? true : false;

                    //string _obj = string.Empty;
                    //_obj = _list.Ssp_act + ";" + _list.Ssp_desc + ";" + _list.Ssp_isenabled.ToString() + ";" + _list.Ssp_ishide.ToString() + ";" + _list.Ssp_optid.ToString() + ";" + _list.Ssp_orgposition.ToString() + ";" + _list.Ssp_parentid.ToString() + ";" + _list.Ssp_title + ";" + _list.Ssp_url + ";";
                    //_node.ImageToolTip = _obj;
                }


            }

            catch (Exception ex)
            {
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
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
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
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
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
            }
            return _opt;
        }

        #endregion

        #region clear sitemap

        protected void Clear()
        {
            lock (this)
            {
                HttpContext.Current.Cache.Remove("rootNode");
                MainMenu.Items.Clear();
            }
        }

        #endregion

    }
}