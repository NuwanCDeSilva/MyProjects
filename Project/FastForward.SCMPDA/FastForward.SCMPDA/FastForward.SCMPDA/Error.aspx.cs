using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMPDA
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string _mytype = Request.QueryString["ERROR"];

                if (_mytype == "1")
                {
                    lblalert.Text = "No loading bays found for the location of the company !!!";
                }
                else
                {
                    lblalert.Text = "You have not assigned any loading bays !!!";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        protected void btnchgloc_Click(object sender, EventArgs e)
        {
             Response.Redirect("ChangeLocation.aspx");
           
        }
    }
}