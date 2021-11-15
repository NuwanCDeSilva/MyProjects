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
    public partial class ScanItem : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserCompanyName"] != null)
            {
                if (Session["UserCompanyName"].ToString() == "AAL")
                {
                    btnTracker.Visible = true;
                }
                else
                {
                    btnTracker.Visible = false;

                }
            }
            else
            {
                Response.Redirect("LoginPDA.aspx");
            }
        }

        protected void lbtnok_Click(object sender, EventArgs e)
        {
            try
            {
                divok.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnalert_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtninfo_Click(object sender, EventArgs e)
        {
            try
            {
                Divinfo.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void btncreatejob_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CurrentJobb"] = null;
                Session["CreateJobNumber"] = "CreateJobNumber";
              
                Response.Redirect("CreateJob.aspx");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void btncurrjob_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CreateJobNumber"] = null;
                Session["CurrentJobb"] = "CurrentJobb";
                Response.Redirect("CurrentJobSelect.aspx");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void btnTracker_Click(object sender, EventArgs e)
        {
            try
            {
                    Response.Redirect("SerialTracker.aspx", false);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message.ToString();
            }
        }
    }
}