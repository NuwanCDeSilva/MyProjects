using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMPDA
{
    public partial class ReopenJobs : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CurrentJobSelect.aspx");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void BindGrid()
        {
            try
            {
                string docdirection = (string)Session["DOCDIRECTION"];
                string _doctype = Request.QueryString["DocType"].ToString();
                Session["DOCTYPE"] = _doctype;
                string warecom = (string)Session["WAREHOUSE_COMPDA"];
                string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                string loadingpoint = (string)Session["LOADING_POINT"];

                DataTable dtjobs = CHNLSVC.Inventory.LoadFinishedCurrentJobs(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _doctype, Convert.ToInt32(docdirection), warecom, wareloc, loadingpoint);
                if (dtjobs.Rows.Count > 0)
                {
                    grdjobs.DataSource = dtjobs;
                    grdjobs.DataBind();
                }
                else {
                    dtjobs = new DataTable();
                    grdjobs.DataSource = dtjobs;
                    grdjobs.DataBind();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtndicalertclose_Click(object sender, EventArgs e)
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
        protected void lbtndivinfoclose_Click(object sender, EventArgs e)
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
        protected void lbtnreopen_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmreopen.Value == "Yes")
                {

                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;
                    if (row != null)
                    {
                        string docnum = (row.FindControl("lbldoc") as Label).Text;
                        string seq = (row.FindControl("lblseq") as Label).Text;
                        if (docnum != "")
                        {
                            string warecom = (string)Session["WAREHOUSE_COMPDA"];
                            string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                            string loadingpoint = (string)Session["LOADING_POINT"];
                            DataTable dtdoccheck = CHNLSVC.Inventory.IsDocAvailable(Session["UserCompanyName"].ToString(), docnum, Session["UserDefLoca"].ToString(), warecom, wareloc, loadingpoint);
                            if (dtdoccheck.Rows.Count > 0 && dtdoccheck.Rows[0]["TUS_FIN_STUS"].ToString() == "1")
                            {
                                string error = string.Empty;
                                bool reopen = CHNLSVC.Inventory.reopenTempDocument(docnum, dtdoccheck.Rows[0]["TUH_DOC_TP"].ToString(), seq, out error);
                                if (reopen == false)
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = error;
                                    return;
                                }
                                else
                                {
                                    BindGrid();
                                    divok.Visible = true;
                                    lblok.Text = "Document reopen successfull: Document : " + docnum;
                                    return;
                                }
                            }
                            else
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Already opened document.";
                                return;
                            }
                        }
                        else
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Invalid document number.";
                            return;
                        }
                    }
                    else
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid document number.";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void grdjobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string _currentjob = (string)Session["DOCNO"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfdoc = e.Row.FindControl("HiddenField1") as HiddenField;

                if (hfdoc.Value == _currentjob)
                {
                    e.Row.BackColor = System.Drawing.Color.Silver;
                }
            }
        }
    }
}