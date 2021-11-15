using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.ADMIN
{
    public partial class Home : BasePage
    {

        bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (Session["UserID"].ToString() != "LAKSHAN")
                {
                    setLogOnInfor();
                }
            }
        }

        private void setLogOnInfor()
        {
            dgvResult.DataSource = new int[] { };
            DataTable _dt = CHNLSVC.Security.GetUserLastLogTrans(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 0);

            //if (_dt.Columns.Contains("Log On"))
            //{
            //    _dt.Columns["Log On"].ColumnName = "Log_On";
            //}
            //if (_dt.Columns.Contains("Log Off"))
            //{
            //    _dt.Columns["Log Off"].ColumnName = "Log_Off";
            //}
            //if (_dt.Columns.Contains("Login Company"))
            //{
            //    _dt.Columns["Login Company"].ColumnName = "Login_Company";
            //}
            //if (_dt.Columns.Contains("Login IP"))
            //{
            //    _dt.Columns["Login IP"].ColumnName = "Login_IP";
            //}
            //if (_dt.Columns.Contains("Login PC"))
            //{
            //    _dt.Columns["Login PC"].ColumnName = "Login_PC";
            //}
            //if (_dt.Columns.Contains("Login Domain"))
            //{
            //    _dt.Columns["Login Domain"].ColumnName = "Login_Domain";
            //}

            dgvResult.DataSource = _dt;
            dgvResult.DataBind();
            PopupSearch.Show();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            _serPopShow = false;
            PopupSearch.Hide();
        }


    
    }
}