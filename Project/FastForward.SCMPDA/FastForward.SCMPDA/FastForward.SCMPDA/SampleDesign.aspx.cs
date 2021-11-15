using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMPDA.Services;
using FF.BusinessObjects;

namespace FastForward.SCMPDA
{
    public partial class SampleDesign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(Convert.ToString(Session["UserID"]))) && (!string.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"]))) && (!string.IsNullOrEmpty(Convert.ToString(Session["UserCompanyName"]))) && (!string.IsNullOrEmpty(Convert.ToString(Session["UserIP"]))) && (!string.IsNullOrEmpty(Convert.ToString(Session["UserComputer"]))))
                {
                    if (!IsPostBack)
                    {
                        string user = (string)Session["UserID"];
                    }
                }
                else
                {
                    Response.Redirect("LoginPDA.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}