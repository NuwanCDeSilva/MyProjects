using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FF.BusinessObjects;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;


namespace FastForward.SCMWeb
{
    /// <summary>
    /// This is a common class for common User Interface Operations.
    /// Created By : Miginda Geeganage.
    /// Created On : 05/03/2012
    /// Modified By :
    /// Modified On :
    /// </summary>
    public class CommonUIOperations : BasePage
    {
        #region  Load System Option
        /// <summary>
        /// Refer the _allSystemOption which allocated data to make tree structure
        /// </summary>
        List<SystemOption> _allSystemOptions = null;
        /// <summary>
        /// Load System Options to the tree view
        /// </summary>
        public void LoadSystemOptions(TreeView _treeView)
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



            foreach (var _parent in _parentSystemOption)
            {
                TreeNode _treeNode = new TreeNode();
                _treeNode = SetNodeProperty(_treeNode, _parent);
                CreateChild(_treeNode, (int)_parent.Ssp_optid);
                _treeView.Nodes.Add(_treeNode);

            }

            _treeView.ExpandAll();

        }
        /// <summary>
        /// Common procedure to create root to leaf in tree view
        /// </summary>
        /// <param name="_node"> Used for store rootnode/chiled node </param>
        /// <param name="_optionID">Used to store option id which check whether having child for the above node</param>
        public void CreateChild(TreeNode _node, Int32 _optionID)
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
        public TreeNode SetNodeProperty(TreeNode _node, SystemOption _list)
        {
            _node.ToolTip = _list.Ssp_url;
            _node.Text = _list.Ssp_title;
            string _obj = string.Empty;
            _obj = _list.Ssp_act + ";" + _list.Ssp_desc + ";" + _list.Ssp_isenabled.ToString() + ";" + _list.Ssp_ishide.ToString() + ";" + _list.Ssp_optid.ToString() + ";" + _list.Ssp_orgposition.ToString() + ";" + _list.Ssp_parentid.ToString() + ";" + _list.Ssp_title + ";" + _list.Ssp_url + ";";
            _node.ImageToolTip = _obj;
            return _node;
        }
        /// <summary>
        /// Common function to get the property which assign by table/user
        /// </summary>
        /// <param name="_treeValue">Used to store value string which split and assign for the systemoption object</param>
        /// <returns></returns>
        public SystemOption GetProperty(string _treeValue)
        {

            SystemOption _opt = new SystemOption();
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

            return _opt;

        }
        #endregion


        #region Common Search methods

        //Convert datatable in to formatted HTML table.
        public static string ConvertDataTableToHtml(DataTable targetTable)
        {
            string htmlString = string.Empty;

            //if ((targetTable != null) && (targetTable.Rows.Count > 0))
            //{       

            StringBuilder htmlBuilder = new StringBuilder();

            //Create Top Portion of HTML Document
            htmlBuilder.Append("<table border='1px' cellpadding='2' cellspacing='0' ");
            //htmlBuilder.Append("style='border: solid 1px Black; font-size: small;'>");
            htmlBuilder.Append("class='searchmodalstyle'>");

            //Create Header Row
            htmlBuilder.Append("<tr align='left' valign='top'>");

            foreach (DataColumn targetColumn in targetTable.Columns)
            {
                //htmlBuilder.Append("<td align='left' valign='top' style='background: #4b6c9e;color:White;'>");
                htmlBuilder.Append("<th align='left' valign='top' class='searchmodalrow'>");

                htmlBuilder.Append(targetColumn.ColumnName);
                htmlBuilder.Append("</th>");
            }

            htmlBuilder.Append("</tr>");

            int i = 0;

            //Create Data Rows
            foreach (DataRow myRow in targetTable.Rows)
            {
                //ondblclick ,onclick              
                htmlBuilder.Append("<tr align='left' valign='top' class='result' ");
                htmlBuilder.Append(" ondblclick ='TableRowClick(" + i + ")' id='tabRow" + i + "' >");

                foreach (DataColumn targetColumn in targetTable.Columns)
                {
                    htmlBuilder.Append("<td align='left' valign='top'>");
                    htmlBuilder.Append(myRow[targetColumn.ColumnName].ToString());
                    htmlBuilder.Append("</td>");
                }

                htmlBuilder.Append("</tr>");
                i++;
            }

            //Create Bottom Portion of HTML Document
            htmlBuilder.Append("</table>");

            //Create String to be Returned
            htmlString = htmlBuilder.ToString();
            //}

            return htmlString;
        }

        public static string MultiItemforCompeleteItem(DataTable targetTable)
        {
            string htmlString = string.Empty;

            //if ((targetTable != null) && (targetTable.Rows.Count > 0))
            //{       

            StringBuilder htmlBuilder = new StringBuilder();

            //Create Top Portion of HTML Document
            htmlBuilder.Append("<table border='1px' cellpadding='2' cellspacing='0' ");
            htmlBuilder.Append("style='border: solid 1px Black; font-size: small;'>");

            //Create Header Row
            htmlBuilder.Append("<tr align='left' valign='top'>");

            foreach (DataColumn targetColumn in targetTable.Columns)
            {
                htmlBuilder.Append("<td align='left' valign='top' style='background: #4b6c9e;color:White;'>");
                htmlBuilder.Append(targetColumn.ColumnName);
                htmlBuilder.Append("</td>");
            }

            htmlBuilder.Append("</tr>");

            int i = 0;
            int _counter = 1;

            //Create Data Rows
            foreach (DataRow myRow in targetTable.Rows)
            {
                //ondblclick ,onclick              
                htmlBuilder.Append("<tr align='left' valign='top' onmouseover='this.style.background=&#39;#eeff00&#39;' ");
                htmlBuilder.Append(" onmouseout='this.style.background=&#39;#ffffff&#39;' ondblclick ='SaleTableRowClick(" + i + ")' id='tabRow" + i + "' >");

                foreach (DataColumn targetColumn in targetTable.Columns)
                {
                    htmlBuilder.Append("<td align='left' valign='top'>");
                    htmlBuilder.Append(myRow[targetColumn.ColumnName].ToString());
                    htmlBuilder.Append("</td>");
                }

                htmlBuilder.Append("</tr>");
                i++;
                _counter++;
                if (_counter >= 11) break;
            }

            //Create Bottom Portion of HTML Document
            htmlBuilder.Append("</table>");

            //Create String to be Returned
            htmlString = htmlBuilder.ToString();
            //}

            return htmlString;
        }

        //Written By Prabhath on 25/10/2012
        public static string ConvertDataTableToHtml(DataTable targetTable, string _script,string _tableid)
        {
            string htmlString = string.Empty;

            //if ((targetTable != null) && (targetTable.Rows.Count > 0))
            //{       

            StringBuilder htmlBuilder = new StringBuilder();

            //Create Top Portion of HTML Document
            htmlBuilder.Append("<table id='" + _tableid + "' class='GridView' cellspacing='0' CellPadding='3' rules='all' border='0' style='width:100%;'> ");
        
            //Create Header Row
            htmlBuilder.Append("<tr>");

            foreach (DataColumn targetColumn in targetTable.Columns)
            {
                htmlBuilder.Append("<td style='background: #4b6c9e;color:White;height:16px;'>");
                htmlBuilder.Append(targetColumn.ColumnName);
                htmlBuilder.Append("</td>");
            }

            htmlBuilder.Append("</tr>");

            int i =1;
            int _counter = 1;

            //Create Data Rows
            foreach (DataRow myRow in targetTable.Rows)
            {
                //ondblclick ,onclick              
                htmlBuilder.Append("<tr onmouseover='this.style.background=&#39;#eeff00&#39;' ");
                htmlBuilder.Append(" onmouseout='this.style.background=&#39;#ffffff&#39;' ");
                if (!string.IsNullOrEmpty(_script))
                {
                    htmlBuilder.Append("onclick ='" + _script + "(" + i + "," + _tableid + ")' id='tabRow" + i + "'");
                }
                htmlBuilder.Append(">");

                foreach (DataColumn targetColumn in targetTable.Columns)
                {
                    htmlBuilder.Append("<td >");
                    htmlBuilder.Append(myRow[targetColumn.ColumnName].ToString());
                    htmlBuilder.Append("</td>");
                }

                htmlBuilder.Append("</tr>");
                i++;
                _counter++;
              
            }

            //Create Bottom Portion of HTML Document
            htmlBuilder.Append("</table>");

            //Create String to be Returned
            htmlString = htmlBuilder.ToString();
            //}

            return htmlString;
        }

        #endregion

    }
}