using FastForward.SCMWeb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.ADMIN
{
    public partial class CommonSearchRoles : BasePage
    {
        string Select_company = string.Empty;
        protected string search_Word = string.Empty;
     
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    LoadGrid();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private void LoadGrid()
        {
            try
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(para, null, null);
                GridView1.DataSource = _result;

                if (_result.Rows.Count > 0)
                {
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
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
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string roleid = GridView1.SelectedRow.Cells[1].Text;
                txtnames.Text = roleid;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                LoadGrid();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string parasearch = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                DataTable _resultsearch = CHNLSVC.CommonSearch.Get_system_role(parasearch, DropDownList1.SelectedValue, "%" + TextBox1.Text.ToUpper());
                GridView1.DataSource = _resultsearch;

                if (_resultsearch.Rows.Count > 0)
                {
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

    }   
}