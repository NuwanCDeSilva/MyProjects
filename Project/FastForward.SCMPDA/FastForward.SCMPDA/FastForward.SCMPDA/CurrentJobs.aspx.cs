using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMPDA.Services;
using FF.BusinessObjects;
using System.Data;

namespace FastForward.SCMPDA
{
    public partial class CurrentJobs : BasePage
    {
        string _userid = string.Empty;
        string _warehcom = string.Empty;
        string _warehcomloc = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                    //GetSelectedRecord();
                    LoadUserLoadingBays();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            } 
        }

        private void LoadUserLoadingBays()
        {
            try
            {
                _userid = (string)Session["UserID"];
                _warehcom = (string)Session["WAREHOUSE_COMPDA"];
                _warehcomloc = (string)Session["WAREHOUSE_LOCPDA"];

                DataTable dtuserloadingpoints = CHNLSVC.Inventory.LoadUserLoadingBays(_userid, Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), _warehcom, _warehcomloc);

                string defaultpoint = string.Empty;

                foreach (DataRow ddr in dtuserloadingpoints.Rows)
                {
                    if (ddr["selb_def_lbcd"].ToString() == "1")
                    {
                        defaultpoint = ddr["selb_lb_cd"].ToString();
                    }
                }

                if (dtuserloadingpoints.Rows.Count > 0)
                {
                    ddlbay.DataSource = dtuserloadingpoints;
                    ddlbay.DataTextField = "mws_res_name";
                    ddlbay.DataValueField = "selb_lb_cd";
                    ddlbay.DataBind();
                }

                ddlbay.SelectedValue = defaultpoint;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void GetSelectedRecord()
        {
            try
            {
                string _currentjob = (string)Session["DOCNO"];

                foreach (GridViewRow ddr in grdjobs.Rows)
                {
                    Int32 gridline = ddr.RowIndex;
                    RadioButton rb = (RadioButton)grdjobs.Rows[gridline]
                                    .Cells[0].FindControl("RadioButton1");

                    HiddenField hf = (HiddenField)grdjobs.Rows[gridline]
                                           .Cells[0].FindControl("HiddenField1");

                    if (_currentjob == hf.Value)
                    {
                        if (rb != null)
                        {
                            rb.Checked = true;
                        }
                    }
                }
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

                if (_doctype == "STJO")
                {
                    docdirection = "0";
                }
                DataTable dtjobs = CHNLSVC.Inventory.LoadCurrentJobs(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _doctype, Convert.ToInt32(docdirection), warecom, wareloc, loadingpoint);
                if (dtjobs.Rows.Count > 0)
                {
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

        private string SetSelectedRecord(string jobno)
        {
            //foreach (GridViewRow ddr in grdjobs.Rows)
            //{
            //    Int32 gridline = ddr.RowIndex;
            //    RadioButton rb = (RadioButton)grdjobs.Rows[gridline]
            //                    .Cells[0].FindControl("RadioButton1");

            //    HiddenField hf = (HiddenField)grdjobs.Rows[gridline]
            //                           .Cells[0].FindControl("HiddenField1");

            //    HiddenField hfseq = (HiddenField)grdjobs.Rows[gridline]
            //                           .Cells[0].FindControl("HiddenField2");

            //    if (rb != null)
            //    {
            //        if (rb.Checked == true)
            //        {
            //            jobno = hf.Value;
            //            Session["SEQNO"] = hfseq.Value;
            //        }
            //    }
            //}

            string jobnosession = (string)Session["SELECTED_JOB"];
            jobno = jobnosession;
            return jobno;
        }
        protected void lbtndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "Yes")
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
                                string finishStus = (string)Session["FINISHED"];
                                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocAvailable(Session["UserCompanyName"].ToString(), docnum, Session["UserDefLoca"].ToString(), warecom, wareloc, loadingpoint);
                                if (dtdoccheck.Rows.Count > 0 && dtdoccheck.Rows[0]["TUS_FIN_STUS"].ToString() == "0")
                                {
                                    string error = string.Empty;
                                    bool deleteDoc = CHNLSVC.Inventory.deleteTempDocument(docnum, seq, out error);
                                    if (deleteDoc == false)
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = error;
                                        return;
                                    }
                                    else
                                    {
                                        BindGrid();
                                        divok.Visible = true;
                                        lblok.Text = "Document delete successfull: Document : " + docnum;
                                        return;
                                    }
                                }
                                else
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Cannot delete finished document.";
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

        private void DivsHide()
        {
            try
            {
                divalert.Visible = false;
                Divinfo.Visible = false;
                divok.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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

        private void SetScrollTop()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "scrolltop", "scrollTop();", true);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void btnchangeloc_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                string _selectedjob = string.Empty;
                string passvalue = SetSelectedRecord(_selectedjob);
                string docdirection = (string)Session["DOCDIRECTION"];

                if (!string.IsNullOrEmpty(passvalue))
                {
                    if (docdirection == "1")
                    {
                        Response.Redirect("CreateJobNumber.aspx?JobNo=" + passvalue, false);
                    }
                    else
                    {
                        if (Session["UserCompanyName"].ToString() == "AAL")
                        {
                            Response.Redirect("CreateOutJobNew.aspx?JobNo=" + passvalue, false);

                        }
                        else
                        {
                            Response.Redirect("CreateOutJob.aspx?JobNo=" + passvalue, false);

                        }
                    }
                }
                else
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select a job !!!";
                    grdjobs.Focus();
                    SetScrollTop();
                    return;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void btnselect_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                string _selectedjob = string.Empty;
                string passvalue = SetSelectedRecord(_selectedjob);

                if (string.IsNullOrEmpty(passvalue))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select a job !!!";
                    grdjobs.Focus();
                    SetScrollTop();
                    return;
                }
                else
                {
                    _warehcom = (string)Session["WAREHOUSE_COMPDA"];
                    _warehcomloc = (string)Session["WAREHOUSE_LOCPDA"];

                    ReptPickHeader _Header = new ReptPickHeader();

                    _Header.Tuh_load_bay = ddlbay.SelectedValue;
                    _Header.Tuh_usr_com = Session["UserCompanyName"].ToString();
                    _Header.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _Header.Tuh_wh_com = _warehcom;
                    _Header.Tuh_wh_loc = _warehcomloc;
                    _Header.Tuh_doc_no = passvalue;

                    Int32 val = CHNLSVC.Inventory.UpdateLoadingBay(_Header);

                    if (Convert.ToInt32(val) == -1)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Error in processing !!!";
                        SetScrollTop();
                        return;
                    }
                    else
                    {
                        divok.Visible = true;
                        lblok.Text = "Loading Point Changed !!!";
                        SetScrollTop();
                        Session["LOADING_POINT"] = ddlbay.SelectedValue;
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void grdjobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string docNo = (grdjobs.SelectedRow.FindControl("HiddenField1") as HiddenField).Value;
                string seqno = (grdjobs.SelectedRow.FindControl("HiddenField2") as HiddenField).Value;
                string jobno = docNo;
                Session["SEQNO"] = seqno;
                Session["SELECTED_JOB"] = jobno;
                DivsHide();
                string _selectedjob = string.Empty;
                string passvalue = SetSelectedRecord(_selectedjob);
                string docdirection = (string)Session["DOCDIRECTION"];
                DataTable DocumentHdr = CHNLSVC.Inventory.getTempPickHdrDoc(seqno);
                if (DocumentHdr.Rows.Count > 0)
                {
                    if (DocumentHdr.Rows[0]["TUH_ISDIRECT"].ToString() == "1")
                    {
                        Session["CurrentJobb"] = null;
                        Session["CreateJobNumber"] = "CreateJobNumber";
                    }
                    else {
                        Session["CurrentJobb"] = "CurrentJobb";
                        Session["CreateJobNumber"] = null;
                    }
                }
                if (!string.IsNullOrEmpty(passvalue))
                {
                    if (docdirection == "1")
                    {
                        Response.Redirect("CreateJobNumber.aspx?JobNo=" + passvalue, false);
                    }
                    else
                    {
                        if (Session["UserCompanyName"].ToString() == "AAL")
                        {
                            Response.Redirect("CreateOutJobNew.aspx?JobNo=" + passvalue, false);
                        }
                        else
                        {
                            Response.Redirect("CreateOutJob.aspx?JobNo=" + passvalue, false);
                        }
                    }
                }
                else
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select a job !!!";
                    grdjobs.Focus();
                    SetScrollTop();
                    return;
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