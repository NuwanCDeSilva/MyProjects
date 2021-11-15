using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Wharf;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Imports.Cusdec_Assessments
{
    public partial class CusdecAssessments : BasePage
    {
        protected List<ImpAstDet> _remAstDet
        {
            get
            {
                if (Session["_remAstDet"] != null)
                {
                    return (List<ImpAstDet>)Session["_remAstDet"];
                }
                else
                {
                    return new List<ImpAstDet>();
                }
            }
            set { Session["_remAstDet"] = value; }
        }

        public Boolean _isEntryUpdate
        {
            get { return Session["_isEntryUpdate"] != null ? (Boolean)Session["_isEntryUpdate"] : false; }
            set { Session["_isEntryUpdate"] = value; }
        }
        string _userid = string.Empty;
        List<string> assno = new List<string>();
        List<string> entryno = new List<string>();
        List<Int32> lstischecked = new List<Int32>();
        List<string> entrynolst = new List<string>();
        List<string> entrynolstamaend = new List<string>();
        List<Int32> canceldutysum = new List<Int32>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    _isEntryUpdate = false;
                    gvDutyentrydetails.DataSource = new int[] { };
                    gvDutyentrydetails.DataBind();

                    gvtotentry1.DataSource = new int[] { };
                    gvtotentry1.DataBind();

                    gvtotentry2.DataSource = new int[] { };
                    gvtotentry2.DataBind();

                    gvaod.DataSource = new int[] { };
                    gvaod.DataBind();

                    DateTime orddate = DateTime.Now;
                    dtpFromDate.Text = orddate.ToString("dd/MMM/yyyy");

                    DataTable dtconditions = new DataTable();
                    dtconditions.Columns.AddRange(new DataColumn[14] { new DataColumn("istd_seq_no"), new DataColumn("istd_line_no"), new DataColumn("istd_entry_no"), new DataColumn("istd_assess_no"),
                        new DataColumn("istd_cost_ele"), new DataColumn("istd_cost_ele_amt"), new DataColumn("istd_cost_claim_amt"), new DataColumn("istd_cost_unclaim_amt"), 
                        new DataColumn("istd_cost_stl_amt"), new DataColumn("istd_diff_amt"), new DataColumn("istd_assess_dt"), new DataColumn("cuds_cost_cat"), 
                        new DataColumn("cuds_cost_tp"),new DataColumn("istd_stus") });
                    ViewState["Conditions"] = dtconditions;
                    this.BindGrid();

                    DataTable dtentries = new DataTable();
                    dtentries.Columns.AddRange(new DataColumn[7] { new DataColumn("isth_seq_no"), new DataColumn("isth_doc_no"), new DataColumn("istd_entry_no"), new DataColumn("isth_entry_amt"), new DataColumn("isth_stl_amt"), new DataColumn("isth_diff_amt"), new DataColumn("cuh_ast_noties_no") });
                    ViewState["Entries"] = dtentries;
                    this.BindEntryGrid();

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16027))
                    {
                        lbrnapprove.Enabled = false;
                        lbrnapprove.CssClass = "buttoncolorleft";
                        lbrnapprove.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        lbrnapprove.Enabled = true;
                        lbrnapprove.CssClass = "buttonUndocolorLeft";
                        lbrnapprove.OnClientClick = "ConfirmApproveRequest();";
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16028))
                    {
                        lbtncancel.Enabled = false;
                        lbtncancel.CssClass = "buttoncolorleft";
                        lbtncancel.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        lbtncancel.Enabled = true;
                        lbtncancel.CssClass = "buttonUndocolorLeft";
                        lbtncancel.OnClientClick = "ConfirmCancelReq();";
                    }
                    hiddendv.Visible = false;
                    txtbondno.Focus();
                    entryno = new List<string>();
                    _remAstDet = new List<ImpAstDet>();


                    DateTime fdate = DateTime.Now;
                    txtFDate.Text = fdate.AddMonths(-1).ToString("dd/MMM/yyyy");
                    DateTime tdate = DateTime.Now;
                    txtTDate.Text = tdate.ToString("dd/MMM/yyyy");


                    txtNewFrom.Text = fdate.AddMonths(-1).ToString("dd/MMM/yyyy");
                    txtNewTo.Text = tdate.ToString("dd/MMM/yyyy");

                    Session["DPopup"] = "";

                }
                else
                {
                    //txtFDate.Text = Request[txtFDate.UniqueID];
                    //txtTDate.Text = Request[txtTDate.UniqueID];

                    //if (Session["DPopup"].ToString() == "true")
                    //{
                    //    UserDPopoup.Show();

                    //}
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void BindGrid()
        {
            try
            {
                gvtotentry1.DataSource = (DataTable)ViewState["Conditions"];
                gvtotentry1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void BindEntryGrid()
        {
            try
            {
                gvtotentry2.DataSource = (DataTable)ViewState["Entries"];
                gvtotentry2.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtSearchbyword2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword2.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void FilterData()
        {
            try
            {

                if (lblvalue.Text == "434")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecBondNo);
                    DataTable result = CHNLSVC.Financial.LoadBondNumbersNew(SearchParams, ddlSearchbykeyDate.SelectedItem.Text, txtSearchbywordDate.Text, Convert.ToDateTime(txtNewFrom.Text), Convert.ToDateTime(txtNewTo.Text), 1);
                    grdResultDate.DataSource = result;
                    grdResultDate.DataBind();
                    lblvalue.Text = "434";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    UserDPopoup.Show();
                    txtSearchbyword.Focus();
                    Session["DPopup"] = "true";
                }
                else if (lblvalue.Text == "437")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecSavedEntry);
                    DataTable result = CHNLSVC.Financial.LoadSavedEntryPopUp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "437";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "438")
                {

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessmentDocs);
                    DataTable result = CHNLSVC.Financial.LoadSettleDocs(SearchParams, ddlSearchbykey2.SelectedItem.Text, txtSearchbyword2.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    grdResult2.DataSource = result;
                    grdResult2.DataBind();
                    lblvalue.Text = "438";
                    ViewState["SEARCH"] = result;
                    grdResult2.PageIndex = 0;
                    ASPopup.Show();
                    txtSearchbyword2.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.CusDecBondNo:
                    {
                        int status = -1;

                        if (chkpending.Checked == true)
                        {
                            status = 1;
                        }
                        if (!chkDoDfs.Checked && !chkReBond.Checked)
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + status + seperator + "EX" + seperator + null);
                            break;
                        }
                        else if (!chkDoDfs.Checked && chkReBond.Checked)
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + status + seperator + "RE" + seperator + "AOD");
                            break;
                        }
                        else if (chkDoDfs.Checked && !chkReBond.Checked)
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + status + seperator + "RE" + seperator + "DO");
                            break;
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + status + seperator + null + seperator + null);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.CusDecSavedEntry:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AssessmentDocs:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (lblvalue.Text == "434")
                {
                    grdResult.PageIndex = e.NewPageIndex;
                    grdResult.DataSource = null;
                    grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "438")
                {
                    grdResult2.PageIndex = e.NewPageIndex;

                    grdResult2.DataSource = null;
                    grdResult2.DataSource = (DataTable)ViewState["SEARCH"];
                    grdResult2.DataBind();
                    grdResult2.PageIndex = 0;
                    ASPopup.Show();
                    txtSearchbyword2.Focus();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }
        public string INTFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0}", myNumber);

            
                return s;
            
        }
        private void CalCulateEntryTotAmt()
        {
            try
            {
                Decimal totamt = 0;

                foreach (GridViewRow gvrow in gvDutyentrydetails.Rows)
                {
                    Label lblamount = (Label)gvrow.FindControl("lblamount");
                    if (!string.IsNullOrEmpty(lblamount.Text.Trim()))
                    {
                        totamt += Math.Round(Convert.ToDecimal(lblamount.Text.Trim()), 2);
                    }
                }
                txtentrytotal.Text = DoFormat(totamt);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void CalCulateAssementTot()
        {
            try
            {
                Decimal asstot = 0;

                foreach (GridViewRow gvrow in gvDutyentrydetails.Rows)
                {
                    TextBox lblassamount = (TextBox)gvrow.FindControl("txtassamt");
                    if (!string.IsNullOrEmpty(lblassamount.Text.Trim()))
                    {
                        asstot += Math.Round(Convert.ToDecimal(lblassamount.Text.Trim()), 2);
                    }
                }
                txtasstot.Text = DoFormat(asstot);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "434")
                {
                    string bondno = grdResultDate.SelectedRow.Cells[1].Text;
                    txtbondno.Text = bondno;
                    txtrlatedbond.Text = grdResultDate.SelectedRow.Cells[4].Text;
                    DataTable dtbonddata = CHNLSVC.Financial.LoadCusDecDutySum(bondno);

                    //Lakshika - Hide zero duties
                    var zeroDuties = dtbonddata.AsEnumerable().Where(r => Convert.ToInt32(r["CUDS_COST_ELE_AMT"]) <= 0).ToList();
                    foreach (DataRow rowToDelete in zeroDuties)
                        rowToDelete.Delete();
                    dtbonddata.AcceptChanges();


                    if ((chkReBond.Checked) || (chkDoDfs.Checked))
                    {

                        var zeroDuties2 = dtbonddata.AsEnumerable().Where(r => r["CUDS_COST_ELE"].ToString() == "PAL").ToList();
                        var zeroDuties3 = dtbonddata.AsEnumerable().Where(r => r["CUDS_COST_ELE"].ToString() == "CE&S").ToList();

                        zeroDuties2.AddRange(zeroDuties3);
                        dtbonddata = zeroDuties2.CopyToDataTable();

                        dtbonddata.AcceptChanges();
                    }

                    //Lakshan
                    dtbonddata.Columns.Add(new DataColumn("rowNo", typeof(Int32)));
                    Int32 _row = 0;
                    int roundupVal = 0;
                    foreach (DataRow row in dtbonddata.Rows)
                    {
                        _row++;
                        row["rowNo"] = _row;
                        roundupVal = (int)(Convert.ToDecimal(row["CUDS_COST_ELE_AMT"]) + 0.5m);//(a + 0.5m);
                        row["CUDS_COST_ELE_AMT"] = roundupVal;
                    }
                    //

                    gvDutyentrydetails.DataSource = null;
                    gvDutyentrydetails.DataBind();

                    gvDutyentrydetails.DataSource = dtbonddata;
                    gvDutyentrydetails.DataBind();

                    CalCulateEntryTotAmt();
                    CalCulateAssementTot();

                    Decimal val1 = Convert.ToDecimal(txtasstot.Text.Trim());
                    Decimal val2 = Convert.ToDecimal(txtentrytotal.Text.Trim());
                    Decimal diff = val1 - val2;
                    txtcaldiff.Text = DoFormat(diff);

                    foreach (GridViewRow gvrow in gvDutyentrydetails.Rows)
                    {
                        Label entryamt = (Label)gvrow.FindControl("lblamount");
                        TextBox assamt = (TextBox)gvrow.FindControl("txtassamt");
                        entryamt.Text = DoFormat(Convert.ToDecimal(entryamt.Text.Trim()));
                        assamt.Text = INTFormat(Convert.ToDecimal(entryamt.Text.Trim())); //Corrected by Chamal 31-03-2016
                    }

                    DataTable dtaod = CHNLSVC.Financial.LoadAODDocs(bondno, Session["UserCompanyCode"].ToString(), "AOD");

                    gvaod.DataSource = null;
                    gvaod.DataBind();

                    gvaod.DataSource = dtaod;
                    gvaod.DataBind();

                    foreach (GridViewRow gvrow in gvaod.Rows)
                    {
                        Label lblentrynoaod = (Label)gvrow.FindControl("lblentrynoaod");
                        lblentrynoaod.Text = bondno;
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    if (gvDutyentrydetails.Rows.Count > 0)
                    {
                        TextBox txtassamt = (TextBox)gvDutyentrydetails.Rows[0].FindControl("txtassamt");
                        if (txtassamt != null)
                        {
                            txtassamt.Focus();
                        }
                    }
                    UserDPopoup.Hide();
                    Session["DPopup"] = "";
                }
                else if (lblvalue.Text == "437")
                {
                    string bondno = grdResult.SelectedRow.Cells[1].Text;
                    txtbondno.Text = bondno;
                    txtrlatedbond.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtassref.Text = grdResult.SelectedRow.Cells[2].Text;
                    Session["SOSEQNO"] = grdResult.SelectedRow.Cells[4].Text;
                    txtassnoticeno.Text = grdResult.SelectedRow.Cells[3].Text;

                    string loadeddoc = (string)Session["LOAD_AMEND_DOC"];

                    if (string.IsNullOrEmpty(loadeddoc))
                    {
                        Session["LOAD_AMEND_DOC"] = txtassref.Text;
                    }
                    else
                    {
                        if (loadeddoc != txtassref.Text.Trim())
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to select an entry to amend which is not in document " + txtassref.Text.Trim() + " !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                            return;
                        }
                    }


                    DataTable dtsavedduty = CHNLSVC.Financial.LoadSavedDutyDetails(bondno, Convert.ToInt32(grdResult.SelectedRow.Cells[4].Text));

                    gvDutyentrydetails.DataSource = null;
                    gvDutyentrydetails.DataBind();

                    //Lakshika - Hide zero duties
                    var zeroDuties = dtsavedduty.AsEnumerable().Where(r => Convert.ToInt32(r["CUDS_COST_ELE_AMT"]) <= 0).ToList();
                    foreach (DataRow rowToDelete in zeroDuties)
                        rowToDelete.Delete();
                    dtsavedduty.AcceptChanges();

                    //Lakshan
                    dtsavedduty.Columns.Add(new DataColumn("rowNo", typeof(Int32)));
                    Int32 _row = 0;
                    int roundupVal = 0; //Lakshika
                    foreach (DataRow row in dtsavedduty.Rows)
                    {
                        _row++;
                        row["rowNo"] = _row;

                        roundupVal = (int)(Convert.ToDecimal(row["CUDS_COST_ELE_AMT"]) + 0.5m);//(a + 0.5m); //Lakshika
                        row["CUDS_COST_ELE_AMT"] = roundupVal;

                        roundupVal = (int)(Convert.ToDecimal(row["CUDS_COST_STL_AMT"]) + 0.5m);//(a + 0.5m); //Lakshika
                        row["CUDS_COST_STL_AMT"] = roundupVal;

                        roundupVal = (int)(Convert.ToDecimal(row["CUDS_COST_CLAIM_AMT"]) + 0.5m);//(a + 0.5m); //Lakshika
                        row["CUDS_COST_CLAIM_AMT"] = roundupVal;

                        roundupVal = (int)(Convert.ToDecimal(row["CUDS_COST_UNCLAIM_AMT"]) + 0.5m);//(a + 0.5m); //Lakshika
                        row["CUDS_COST_UNCLAIM_AMT"] = roundupVal;
                    }

                    gvDutyentrydetails.DataSource = dtsavedduty;
                    gvDutyentrydetails.DataBind();

                    CalCulateEntryTotAmt();
                    CalCulateAssementTot();

                    Decimal val1 = Convert.ToDecimal(txtasstot.Text.Trim());
                    Decimal val2 = Convert.ToDecimal(txtentrytotal.Text.Trim());
                    Decimal diff = val1 - val2;
                    txtcaldiff.Text = DoFormat(diff);

                    foreach (GridViewRow gvrow in gvDutyentrydetails.Rows)
                    {
                        Label entryamt = (Label)gvrow.FindControl("lblamount");
                        TextBox assamt = (TextBox)gvrow.FindControl("txtassamt");
                        entryamt.Text = DoFormat(Convert.ToDecimal(entryamt.Text.Trim()));
                        assamt.Text = DoFormat(Convert.ToDecimal(assamt.Text.Trim()));
                    }
                    txtassnoticeno.ReadOnly = true;
                }
                else if (lblvalue.Text == "438")
                {
                    txtassref.Text = grdResult2.SelectedRow.Cells[1].Text;
                    txtassref_TextChanged(null, null);

                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
            }
            catch
            {

            }
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if (col.ColumnName != "Seq No")
                {
                    this.ddlSearchbykey.Items.Add(col.ColumnName);
                }
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        public void BindUCtrlDDLDataOriginal(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        public void BindUCtrlDDLDataCustom(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if ((col.ColumnName != "Date") && (col.ColumnName != "Status"))
                {
                    this.ddlSearchbykey.Items.Add(col.ColumnName);
                }
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        public void BindUCtrlDDLDataCustomForAssesmnt(DataTable _dataSource)
        {
            this.ddlSearchbykey2.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if ((col.ColumnName != "Date") && (col.ColumnName != "Status"))
                {
                    this.ddlSearchbykey2.Items.Add(col.ColumnName);
                }
            }
            this.ddlSearchbykey2.SelectedIndex = 0;
        }

        public void BindUCtrlDDLDataBondNo(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if (col.ColumnName != "Date")
                {
                    this.ddlSearchbykey.Items.Add(col.ColumnName);
                }
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        public void BindUCtrlDDLDataNew(DataTable _dataSource)
        {
            this.ddlSearchbykeyDate.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if (col.ColumnName != "Date")
                {
                    this.ddlSearchbykeyDate.Items.Add(col.ColumnName);
                }
            }
            this.ddlSearchbykeyDate.SelectedIndex = 0;
        }
        protected void lbtnbondload_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (chkpending.Checked == true)
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecSavedEntry);
                    DataTable result = CHNLSVC.Financial.LoadSavedEntryPopUp(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "437";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecBondNo);
                    DataTable result = CHNLSVC.Financial.LoadBondNumbersNew(SearchParams, null, null, Convert.ToDateTime(txtNewFrom.Text), Convert.ToDateTime(txtNewTo.Text), 1);
                    grdResultDate.DataSource = result;
                    grdResultDate.DataBind();
                    lblvalue.Text = "434";
                    BindUCtrlDDLDataNew(result);
                    ViewState["SEARCH"] = result;
                    Session["DPopup"] = "true";
                    grdResult.PageIndex = 0;
                    UserDPopoup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void txtassamt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalCulateAssementTot();

                foreach (GridViewRow gvrow in gvDutyentrydetails.Rows)
                {
                    TextBox assamt = (TextBox)gvrow.FindControl("txtassamt");
                    if (!string.IsNullOrEmpty(assamt.Text.Trim()))
                    {
                        assamt.Text = DoFormat(Convert.ToDecimal(assamt.Text.Trim()));
                    }
                }
                Decimal val1 = Convert.ToDecimal(txtasstot.Text.Trim());
                Decimal val2 = Convert.ToDecimal(txtentrytotal.Text.Trim());
                Decimal diff = val1 - val2;
                txtcaldiff.Text = DoFormat(diff);

                var lb = (TextBox)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblRowNo = (Label)row.FindControl("lblRowNo");
                Int32 _rowNo = Convert.ToInt32(lblRowNo.Text);
                if (_rowNo < gvDutyentrydetails.Rows.Count)
                {
                    TextBox assamt = (TextBox)gvDutyentrydetails.Rows[_rowNo].FindControl("txtassamt");
                    if (!string.IsNullOrEmpty(assamt.Text.Trim()))
                    {
                        assamt.Focus();
                    }
                }
                if (_rowNo == gvDutyentrydetails.Rows.Count)
                {
                    txtassnoticeno.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }

        }

        private void Clear()
        {
            try
            {
                ViewState["Conditions"] = null;
                _remAstDet = new List<ImpAstDet>();
                Session["ASS_NO"] = null;
                Session["ENTRY_NO"] = null;
                Session["SOSEQNO"] = null;
                Session["STUS"] = null;
                Session["LOAD_AMEND_DOC"] = null;

                assno.Clear();
                entryno.Clear();
                lstischecked.Clear();
                entrynolst.Clear();
                canceldutysum.Clear();

                txtassnoticeno.Text = string.Empty;
                DateTime orddate = DateTime.Now;
                dtpFromDate.Text = orddate.ToString("dd/MMM/yyyy");
                txtentrytotal.Text = string.Empty;
                txtasstot.Text = string.Empty;
                txtcaldiff.Text = string.Empty;
                txtfinaltotal.Text = string.Empty;
                txtfnalasstot.Text = string.Empty;
                txtfinalassdiff.Text = string.Empty;

                gvDutyentrydetails.DataSource = new int[] { };
                gvDutyentrydetails.DataBind();

                gvtotentry1.DataSource = new int[] { };
                gvtotentry1.DataBind();

                gvtotentry2.DataSource = new int[] { };
                gvtotentry2.DataBind();

                txtbondno.Text = string.Empty;
                txtrlatedbond.Text = string.Empty;
                txtassref.Text = string.Empty;
                chkpending.Checked = false;
                txtbondno.Enabled = true;

                DataTable dtconditions = new DataTable();
                dtconditions.Columns.AddRange(new DataColumn[14] { new DataColumn("istd_seq_no"), new DataColumn("istd_line_no"), new DataColumn("istd_entry_no"), 
                    new DataColumn("istd_assess_no"), new DataColumn("istd_cost_ele"), new DataColumn("istd_cost_ele_amt"), new DataColumn("istd_cost_claim_amt"), 
                    new DataColumn("istd_cost_unclaim_amt"), new DataColumn("istd_cost_stl_amt"), new DataColumn("istd_diff_amt"), new DataColumn("istd_assess_dt"), 
                    new DataColumn("cuds_cost_cat"), new DataColumn("cuds_cost_tp"),new DataColumn("istd_stus") });
                ViewState["Conditions"] = dtconditions;
                this.BindGrid();

                DataTable dtentries = new DataTable();
                dtentries.Columns.AddRange(new DataColumn[7] { new DataColumn("isth_seq_no"), new DataColumn("isth_doc_no"), new DataColumn("istd_entry_no"), new DataColumn("isth_entry_amt"), new DataColumn("isth_stl_amt"), new DataColumn("isth_diff_amt"), new DataColumn("cuh_ast_noties_no") });
                ViewState["Entries"] = dtentries;
                this.BindEntryGrid();

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16027))
                {
                    lbrnapprove.Enabled = false;
                    lbrnapprove.CssClass = "buttoncolorleft";
                    lbrnapprove.OnClientClick = "return Enable();";
                }
                else
                {
                    lbrnapprove.Enabled = true;
                    lbrnapprove.CssClass = "buttonUndocolorLeft";
                    lbrnapprove.OnClientClick = "ConfirmApproveRequest();";
                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16028))
                {
                    lbtncancel.Enabled = false;
                    lbtncancel.CssClass = "buttoncolorleft";
                    lbtncancel.OnClientClick = "return Enable();";
                }
                else
                {
                    lbtncancel.Enabled = true;
                    lbtncancel.CssClass = "buttonUndocolorLeft";
                    lbtncancel.OnClientClick = "ConfirmCancelReq();";
                }

                lbtnremoveentry.Enabled = true;
                lbtnremoveentry.CssClass = "buttonUndocolorLeft";
                lbtnremoveentry.OnClientClick = "ConfirmDelete();";

                lbtnaddentry.Enabled = true;
                lbtnaddentry.CssClass = "buttonUndocolorLeft";
                txtassnoticeno.ReadOnly = false;

                lbtnsave.Enabled = true;
                lbtnsave.CssClass = "buttonUndocolorLeft";
                lbtnsave.OnClientClick = "ConfirmSave();";

                gvaod.DataSource = new int[] { };
                gvaod.DataBind();
                chkReBond.Enabled = true;
                chkReBond.Checked = false;
                chkDoDfs.Enabled = true;
                chkDoDfs.Checked = false;

                txtFDate.Text = DateTime.Now.ToShortDateString();
                txtTDate.Text = DateTime.Now.ToShortDateString();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        private void ClearNew()
        {
            try
            {
                _remAstDet = new List<ImpAstDet>();
                ViewState["Conditions"] = null;
                Session["ASS_NO"] = null;
                Session["ENTRY_NO"] = null;
                Session["SOSEQNO"] = null;
                Session["STUS"] = null;
                Session["LOAD_AMEND_DOC"] = null;

                assno.Clear();
                entryno.Clear();
                lstischecked.Clear();
                entrynolst.Clear();
                canceldutysum.Clear();

                txtassnoticeno.Text = string.Empty;
                DateTime orddate = DateTime.Now;
                dtpFromDate.Text = orddate.ToString("dd/MMM/yyyy");
                txtentrytotal.Text = string.Empty;
                txtasstot.Text = string.Empty;
                txtcaldiff.Text = string.Empty;
                txtfinaltotal.Text = string.Empty;
                txtfnalasstot.Text = string.Empty;
                txtfinalassdiff.Text = string.Empty;

                gvDutyentrydetails.DataSource = new int[] { };
                gvDutyentrydetails.DataBind();

                gvtotentry1.DataSource = new int[] { };
                gvtotentry1.DataBind();

                gvtotentry2.DataSource = new int[] { };
                gvtotentry2.DataBind();

                //   txtbondno.Text = string.Empty;
                txtrlatedbond.Text = string.Empty;
                // txtassref.Text = string.Empty;
                chkpending.Checked = false;
                txtbondno.Enabled = true;

                DataTable dtconditions = new DataTable();
                dtconditions.Columns.AddRange(new DataColumn[14] { new DataColumn("istd_seq_no"), new DataColumn("istd_line_no"), new DataColumn("istd_entry_no"), new DataColumn("istd_assess_no"), 
                    new DataColumn("istd_cost_ele"), new DataColumn("istd_cost_ele_amt"), new DataColumn("istd_cost_claim_amt"), new DataColumn("istd_cost_unclaim_amt"), 
                    new DataColumn("istd_cost_stl_amt"), new DataColumn("istd_diff_amt"), new DataColumn("istd_assess_dt"), new DataColumn("cuds_cost_cat"), 
                    new DataColumn("cuds_cost_tp") ,new DataColumn("istd_stus")});
                ViewState["Conditions"] = dtconditions;
                this.BindGrid();

                DataTable dtentries = new DataTable();
                dtentries.Columns.AddRange(new DataColumn[7] { new DataColumn("isth_seq_no"), new DataColumn("isth_doc_no"), new DataColumn("istd_entry_no"), new DataColumn("isth_entry_amt"), new DataColumn("isth_stl_amt"), new DataColumn("isth_diff_amt"), new DataColumn("cuh_ast_noties_no") });
                ViewState["Entries"] = dtentries;
                this.BindEntryGrid();

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16027))
                {
                    lbrnapprove.Enabled = false;
                    lbrnapprove.CssClass = "buttoncolorleft";
                    lbrnapprove.OnClientClick = "return Enable();";
                }
                else
                {
                    lbrnapprove.Enabled = true;
                    lbrnapprove.CssClass = "buttonUndocolorLeft";
                    lbrnapprove.OnClientClick = "ConfirmApproveRequest();";
                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16028))
                {
                    lbtncancel.Enabled = false;
                    lbtncancel.CssClass = "buttoncolorleft";
                    lbtncancel.OnClientClick = "return Enable();";
                }
                else
                {
                    lbtncancel.Enabled = true;
                    lbtncancel.CssClass = "buttonUndocolorLeft";
                    lbtncancel.OnClientClick = "ConfirmCancelReq();";
                }

                lbtnremoveentry.Enabled = true;
                lbtnremoveentry.CssClass = "buttonUndocolorLeft";
                lbtnremoveentry.OnClientClick = "ConfirmDelete();";

                lbtnaddentry.Enabled = true;
                lbtnaddentry.CssClass = "buttonUndocolorLeft";
                txtassnoticeno.ReadOnly = false;

                lbtnsave.Enabled = true;
                lbtnsave.CssClass = "buttonUndocolorLeft";
                lbtnsave.OnClientClick = "ConfirmSave();";

                gvaod.DataSource = new int[] { };
                gvaod.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtnaddentry_Click(object sender, EventArgs e)
        {
            try
            {
                string _returnMsg = string.Empty;
                if (string.IsNullOrEmpty(txtbondno.Text.ToString()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the entry no!')", true);
                    txtbondno.Focus();
                    return;
                }
                if (!chkDoDfs.Checked)
                {
                    if (CHNLSVC.Financial.CheckEntryByPass(Session["UserCompanyCode"].ToString(), txtbondno.Text.ToString(), out _returnMsg) == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _returnMsg + "')", true);
                        txtbondno.Focus();
                        return;
                    }
                }

                if (gvDutyentrydetails.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Load entry duty details !!!')", true);
                    lbtnbondload.Focus();
                    return;
                }
                foreach (GridViewRow rw in gvtotentry2.Rows)
                {

                    Label lblbondno = (Label)rw.FindControl("lblbondno");
                    if (lblbondno.Text == txtbondno.Text && !_isEntryUpdate)
                    {
                        _isEntryUpdate = true;//lakshika
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document no is already added.!!!')", true);
                        lbtnbondload.Focus();
                        return;
                    }
                }
                CalCulateAssementTot();

                Decimal val1a = Convert.ToDecimal(txtasstot.Text.Trim());
                Decimal val2a = Convert.ToDecimal(txtentrytotal.Text.Trim());
                Decimal diff2 = val1a - val2a;
                txtcaldiff.Text = DoFormat(diff2);

                if (string.IsNullOrEmpty(txtassnoticeno.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter an assessment notice number !!!')", true);
                    txtassnoticeno.Focus();
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecSavedEntry);
                bool validNoticeNo = false;
                if (!_isEntryUpdate)
                {
                    //DataTable result = CHNLSVC.Financial.LoadSavedEntryPopUp(SearchParams, "Assessment Notice No", txtassnoticeno.Text.Trim());
                    //if (result != null)
                    //{
                    //    if (result.Rows.Count > 0)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Assessment notice no is already added !!!')", true);
                    //        txtassnoticeno.Focus();
                    //        return;
                    //    }
                    //}

                    //foreach (GridViewRow row in gvtotentry1.Rows)
                    //{
                    //    Label lblassno = row.FindControl("lblassno") as Label;
                    //    if (lblassno.Text == txtassnoticeno.Text.Trim())
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Assessment notice no is already added !!!')", true);
                    //        txtassnoticeno.Focus();
                    //        return;
                    //    }
                    //}
                }
                else
                {
                    //DataTable result = CHNLSVC.Financial.LoadSavedEntryPopUp(SearchParams, "Assessment Notice No", txtassnoticeno.Text.Trim());

                    //if (result != null)
                    //{
                    //    if (result.Rows.Count > 0)
                    //    {
                    //        foreach (DataRow dataRow in result.Rows)
                    //        {
                    //            if (dataRow["Document No"].ToString() == txtbondno.Text.Trim() && dataRow["Assessment Notice No"].ToString() == txtassnoticeno.Text.Trim())
                    //            {
                    //                validNoticeNo = true;
                    //                break;
                    //            }
                    //        }

                    //        if (!validNoticeNo)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Assessment notice no is already added !!!')", true);
                    //            txtassnoticeno.Focus();
                    //            return;
                    //        }
                    //    }
                    //}

                    //foreach (GridViewRow row in gvtotentry1.Rows)
                    //{
                    //    Label lblassno = row.FindControl("lblassno") as Label;
                    //    if (lblassno.Text == txtassnoticeno.Text.Trim() && !validNoticeNo)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Assessment notice no is already added !!!')", true);
                    //        txtassnoticeno.Focus();
                    //        return;
                    //    }
                    //}
                }

                foreach (GridViewRow gvrow in gvDutyentrydetails.Rows)
                {
                    TextBox assamt = (TextBox)gvrow.FindControl("txtassamt");

                    if (string.IsNullOrEmpty(assamt.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter assessment amount !!!')", true);
                        assamt.Focus();
                        return;
                    }

                    //if (assamt.Text == "0.00")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('0 assessment amount is not allowed !!!')", true);
                    //    assamt.Focus();
                    //    return;
                    //}
                }

                var list = new List<string>();

                list = (List<string>)Session["ASS_NO"];

                if (list != null)
                {
                    assno = list;
                }

                //if (assno.Contains(txtassnoticeno.Text.Trim()))
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Assessment notice no " + txtassnoticeno.Text +" has been already added !!!')", true);
                //    txtassnoticeno.Text = string.Empty;
                //    txtassnoticeno.Focus();
                //    return; 
                //}

                var list2 = new List<string>();

                list2 = (List<string>)Session["ENTRY_NO"];

                if (list2 != null)
                {
                    entryno = list2;
                }

                //if (entryno.Contains(txtbondno.Text.Trim()))
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Bond no " + txtbondno.Text + " has been already added !!!')", true);
                //    txtbondno.Text = string.Empty;
                //    txtrlatedbond.Text = string.Empty;
                //    txtentrytotal.Text = string.Empty;
                //    txtasstot.Text = string.Empty;
                //    txtcaldiff.Text = string.Empty;
                //    gvDutyentrydetails.DataSource = null;
                //    gvDutyentrydetails.DataBind();
                //    return;
                //}

                DataTable dt = (DataTable)ViewState["Conditions"];

                string seqno = string.Empty;

                if (Session["SOSEQNO"] != null)
                {
                    seqno = (string)Session["SOSEQNO"].ToString();
                }
                else
                {
                    seqno = string.Empty;
                }


                foreach (GridViewRow gvrow in gvDutyentrydetails.Rows)
                {
                    Int32 gridline = gvrow.RowIndex + 1;

                    Label duty = (Label)gvrow.FindControl("lblduty");
                    Label entryamt = (Label)gvrow.FindControl("lblamount");
                    Label claimamt = (Label)gvrow.FindControl("lbl_cost_claim_amt");
                    Label unclaimamt = (Label)gvrow.FindControl("lbl_cost_unclaim_amt");
                    TextBox assamt = (TextBox)gvrow.FindControl("txtassamt");
                    Label costp = (Label)gvrow.FindControl("lblcuds_cost_tp");
                    Label coscat = (Label)gvrow.FindControl("lblcuds_cost_cat");

                    Decimal diff = 0;

                    Decimal val1 = Convert.ToDecimal(assamt.Text.Trim());
                    Decimal val2 = Convert.ToDecimal(entryamt.Text.Trim());
                    diff = val1 - val2;
                    if (!validNoticeNo)
                    {
                        //Lakshika TEst -----------------
                        if (_isEntryUpdate)
                        {
                            var existingRows = dt.AsEnumerable().Where(r => Convert.ToString(r["ISTD_ASSESS_NO"]) == txtassnoticeno.Text.Trim()).ToList();
                            foreach (DataRow rowToDelete in existingRows)
                                rowToDelete.Delete();
                            dt.AcceptChanges();
                            //---------------------
                        }


                        dt.Rows.Add(seqno, gridline, txtbondno.Text.Trim(), txtassnoticeno.Text.Trim(), duty.Text.Trim(), entryamt.Text.Trim(),
                        claimamt.Text.Trim(), unclaimamt.Text.Trim(), assamt.Text.Trim(), diff, dtpFromDate.Text, coscat.Text.Trim(), costp.Text.Trim(), "A");
                        _isEntryUpdate = false;

                    }
                    else
                    {
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            if (dataRow["istd_entry_no"].ToString() == txtbondno.Text.Trim()
                                && dataRow["istd_assess_no"].ToString() == txtassnoticeno.Text.Trim()
                                && dataRow["istd_cost_ele"].ToString() == duty.Text.Trim())
                            {
                                dataRow["istd_seq_no"] = seqno;
                                dataRow["istd_line_no"] = gridline;
                                // dataRow["istd_entry_no"] = txtbondno.Text.Trim();
                                // dataRow["istd_assess_no"] = txtassnoticeno.Text.Trim();
                                //   dataRow["istd_cost_ele"] = duty.Text.Trim();
                                dataRow["istd_cost_ele_amt"] = entryamt.Text.Trim();
                                dataRow["istd_cost_claim_amt"] = claimamt.Text.Trim();
                                dataRow["istd_cost_unclaim_amt"] = unclaimamt.Text.Trim();
                                dataRow["istd_cost_stl_amt"] = assamt.Text.Trim();
                                dataRow["istd_diff_amt"] = diff;
                                dataRow["istd_assess_dt"] = dtpFromDate.Text;
                                dataRow["cuds_cost_cat"] = coscat.Text.Trim();
                                dataRow["cuds_cost_tp"] = costp.Text.Trim();
                                dataRow["istd_stus"] = "A";
                            }
                        }
                    }
                }

                gvtotentry1.DataSource = null;
                gvtotentry1.DataBind();

                gvtotentry1.DataSource = dt;
                gvtotentry1.DataBind();

                ViewState["Conditions"] = dt;


                DataTable dt1 = (DataTable)ViewState["Entries"];
                bool _found = false;
                if (dt1.Rows.Count > 0)//_isEntryUpdate 
                {
                    foreach (DataRow roww in dt1.Rows)
                    {
                        if (roww["istd_entry_no"].ToString() == txtbondno.Text)
                        {
                            roww["isth_entry_amt"] = Convert.ToDecimal(txtentrytotal.Text.Trim());
                            roww["isth_stl_amt"] = Convert.ToDecimal(txtasstot.Text.Trim());
                            roww["isth_diff_amt"] = Convert.ToDecimal(txtcaldiff.Text.Trim());
                            roww["cuh_ast_noties_no"] = txtassnoticeno.Text.Trim();
                            _found = true;
                        }
                        //else
                        //{
                        //    dt1.Rows.Add(seqno, txtassref.Text.Trim(), txtentrytotal.Text.Trim(), txtasstot.Text.Trim(), txtcaldiff.Text.Trim(), "A", 0, txtbondno.Text.Trim(), txtassnoticeno.Text.Trim(), 1);
                        //}
                    }
                    //if (txtassref.Text != null || txtassref.Text != "")
                    if (seqno != "")
                    {
                        dt1.Rows.Add(seqno, txtassref.Text.Trim(), txtentrytotal.Text.Trim(), txtasstot.Text.Trim(), txtcaldiff.Text.Trim(), "A", 0, txtbondno.Text.Trim(), txtassnoticeno.Text.Trim(), 1);
                        _found = true;
                    }
                    if (!_found)
                    {
                        dt1.Rows.Add(string.Empty, string.Empty, txtbondno.Text.Trim(), txtentrytotal.Text.Trim(), txtasstot.Text.Trim(), txtcaldiff.Text.Trim(), txtassnoticeno.Text.Trim());                        
                    }
                }
                else
                {
                    dt1.Rows.Add(string.Empty, string.Empty, txtbondno.Text.Trim(), txtentrytotal.Text.Trim(), txtasstot.Text.Trim(), txtcaldiff.Text.Trim(), txtassnoticeno.Text.Trim());
                }

                gvtotentry2.DataSource = null;
                gvtotentry2.DataBind();

                gvtotentry2.DataSource = dt1;
                gvtotentry2.DataBind();

                ViewState["Entries"] = dt1;


                assno.Add(txtassnoticeno.Text);
                Session["ASS_NO"] = assno;

                entryno.Add(txtbondno.Text);
                Session["ENTRY_NO"] = entryno;

                if (Session["SOSEQNO"] != null)
                {
                    string seqnoamend = (string)Session["SOSEQNO"].ToString();

                    foreach (DataRow ddr in dt1.Rows)
                    {
                        entrynolstamaend.Add(ddr["istd_entry_no"].ToString());
                    }

                    string filtervalues2 = String.Join(",", entrynolstamaend.Where(x => x != null).Select(x => x.ToString()));
                    DataTable dtsum = CHNLSVC.Financial.CalculateAmendSum(filtervalues2, Convert.ToInt32(seqnoamend));

                    DataTable dtall = CHNLSVC.Financial.CalculateTotEntrySum(Convert.ToInt32(seqnoamend));

                    Decimal entrytotamend = 0;
                    Decimal asstotamened = 0;
                    Decimal diffamend = 0;

                    Decimal asstotloaded = Convert.ToDecimal(txtasstot.Text.Trim());
                    Decimal diffamtloaded = Convert.ToDecimal(txtcaldiff.Text.Trim());

                    string needtocalgridval = string.Empty;

                    foreach (DataRow ddrsum in dtsum.Rows)
                    {
                        if (!string.IsNullOrEmpty(ddrsum["istd_cost_stl_amt"].ToString()))
                        {
                            asstotamened = Convert.ToDecimal(ddrsum["istd_cost_stl_amt"]);
                        }
                        else
                        {
                            needtocalgridval = "1";
                        }

                        if (!string.IsNullOrEmpty(ddrsum["istd_diff_amt"].ToString()))
                        {
                            diffamend = Convert.ToDecimal(ddrsum["istd_diff_amt"]);
                        }
                    }

                    if (needtocalgridval != "1")
                    {
                        foreach (DataRow ddrtot in dtall.Rows)
                        {
                            entrytotamend = Convert.ToDecimal(ddrtot["istd_cost_ele_amt"]);
                        }

                        Decimal finalamanedentrytot = entrytotamend;
                        Decimal finalamanedentryassment = asstotloaded + asstotamened;
                        Decimal finalamanedentrydiff = diffamtloaded + diffamend;

                        txtfinaltotal.Text = finalamanedentrytot.ToString();
                        txtfnalasstot.Text = finalamanedentryassment.ToString();
                        txtfinalassdiff.Text = finalamanedentrydiff.ToString();
                    }
                    else
                    {
                        CalCulateFinalEntryTotAmt();
                        CalCulateFinalAssTot();
                        CalCulateFinalDiff();
                    }
                }
                else
                {
                    CalCulateFinalEntryTotAmt();
                    CalCulateFinalAssTot();
                    CalCulateFinalDiff();
                }

                txtbondno.Text = string.Empty;
                txtrlatedbond.Text = string.Empty;
                txtassnoticeno.Text = string.Empty;
                DateTime orddate = DateTime.Now;
                dtpFromDate.Text = orddate.ToString("dd/MMM/yyyy");
                txtentrytotal.Text = string.Empty;
                txtasstot.Text = string.Empty;
                txtcaldiff.Text = string.Empty;
                gvDutyentrydetails.DataSource = null;
                gvDutyentrydetails.DataBind();
                txtbondno.Focus();
                if (chkDoDfs.Checked)
                {
                    chkDoDfs.Enabled = false;
                    chkReBond.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbuttonclear_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    Clear();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        private void CalCulateFinalEntryTotAmt()
        {
            try
            {
                Decimal totamt = 0;

                foreach (GridViewRow gvrow in gvtotentry1.Rows)
                {
                    Label lblamount = (Label)gvrow.FindControl("lblentryamt");
                    if (!string.IsNullOrEmpty(lblamount.Text.Trim()))
                    {
                        totamt += Math.Round(Convert.ToDecimal(lblamount.Text.Trim()), 2);
                    }
                }
                txtfinaltotal.Text = DoFormat(totamt);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void CalCulateFinalAssTot()
        {
            try
            {
                Decimal totamt = 0;

                foreach (GridViewRow gvrow in gvtotentry1.Rows)
                {
                    Label lblamount = (Label)gvrow.FindControl("lblassamt");
                    if (!string.IsNullOrEmpty(lblamount.Text.Trim()))
                    {
                        totamt += Math.Round(Convert.ToDecimal(lblamount.Text.Trim()), 2);
                    }
                }
                txtfnalasstot.Text = DoFormat(totamt);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void CalCulateFinalDiff()
        {
            try
            {
                Decimal totamt = 0;

                foreach (GridViewRow gvrow in gvtotentry1.Rows)
                {
                    Label lblamount = (Label)gvrow.FindControl("lbldiff");
                    if (!string.IsNullOrEmpty(lblamount.Text.Trim()))
                    {
                        totamt += Math.Round(Convert.ToDecimal(lblamount.Text.Trim()), 2);
                    }
                }
                txtfinalassdiff.Text = DoFormat(totamt);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnsave_Click(object sender, EventArgs e)
        {
            if (txtsaveconfirm.Value == "Yes")
            {
                try
                {
                    if (gvtotentry1.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Enter Duty Details !!!')", true);
                        return;
                    }

                    _userid = (string)Session["UserID"];
                    MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                    ImpAstHeader Header = new ImpAstHeader();
                    if (chkDoDfs.Checked)
                    {

                        mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                        mastAutoNo.Aut_cate_tp = "DFD";
                        mastAutoNo.Aut_direction = 1;
                        mastAutoNo.Aut_moduleid = "DFD";
                        mastAutoNo.Aut_start_char = "DFD";
                        mastAutoNo.Aut_year = DateTime.Now.Year;
                        Header.ISTH_TP = "1";
                    }
                    else
                    {
                        mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                        mastAutoNo.Aut_cate_tp = "AST";
                        mastAutoNo.Aut_direction = 0;
                        mastAutoNo.Aut_moduleid = "AST";
                        mastAutoNo.Aut_start_char = "AST";
                        mastAutoNo.Aut_year = DateTime.Now.Year;
                        Header.ISTH_TP = "0";
                    }
                    string seqnofororder = string.Empty;

                    if (Session["SOSEQNO"] != null)
                    {
                        seqnofororder = (string)Session["SOSEQNO"].ToString();
                    }


                    if (string.IsNullOrEmpty(seqnofororder))
                    {
                        seqnofororder = "0";
                    }

                    Header.ISTH_SEQ_NO = Convert.ToInt32(seqnofororder);
                    Header.ISTH_DOC_NO = txtassref.Text.Trim();
                    Header.ISTH_COM = Session["UserCompanyCode"].ToString();
                    Header.ISTH_DT = Convert.ToDateTime(DateTime.Now.Date);
                    Header.ISTH_ENTRY_AMT = Convert.ToDecimal(txtfinaltotal.Text.Trim());
                    Header.ISTH_STL_AMT = Convert.ToDecimal(txtfnalasstot.Text.Trim());
                    Header.ISTH_DIFF_AMT = Convert.ToDecimal(txtfinalassdiff.Text.Trim());
                    Header.ISTH_STUS = "A";
                    Header.ISTH_IS_STL = 0;
                    Header.ISTH_CRE_BY = _userid;
                    Header.ISTH_CRE_DT = Convert.ToDateTime(DateTime.Now.Date);
                    Header.ISTH_CRE_SESSION = Session["SessionID"].ToString();
                    Header.ISTH_MOD_BY = _userid;
                    Header.ISTH_MOD_DT = Convert.ToDateTime(DateTime.Now.Date);
                    Header.ISTH_MOD_SESSION = Session["SessionID"].ToString();

                    ImpCusdecHdr CusDecHeader = new ImpCusdecHdr();

                    ImpAstDet Details = new ImpAstDet();
                    DataTable dtCusDec = (DataTable)ViewState["Entries"];
                    ImpCusdecDutySum DutySum = new ImpCusdecDutySum();
                    DataTable dtSettlements = (DataTable)ViewState["Conditions"];
                    //Sahan
                    //Tuple<int, string> outopno = CHNLSVC.Financial.SaveSettleHeader(Header, mastAutoNo, CusDecHeader, dtCusDec, Session["UserCompanyCode"].ToString(), Details, dtSettlements, _userid, Session["SessionID"].ToString(), DutySum);

                    //Lakshan
                    Tuple<int, string> outopno = CHNLSVC.Financial.SaveSettleHeaderNew(Header, mastAutoNo, CusDecHeader, dtCusDec, Session["UserCompanyCode"].ToString(),
                        Details, dtSettlements, _userid, Session["SessionID"].ToString(), DutySum, _remAstDet);

                    string newseqno = string.Empty;
                    string outputopno = string.Empty;

                    if (string.IsNullOrEmpty(txtassref.Text.Trim()))
                    {
                        newseqno = outopno.Item1.ToString();
                        outputopno = outopno.Item2.ToString();
                    }
                    else
                    {
                        newseqno = seqnofororder;
                        outputopno = txtassref.Text.Trim();
                    }

                    if (Convert.ToInt32(newseqno) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully saved document " + outputopno + " !!!')", true);
                        txtassref.Text = outputopno;

                        if (outputopno.Contains("ABL-DFD"))
                        {
                            lbtnASTprint2_Click(null, null);
                        }
                        else
                        {
                            lbtnASTprint_Click(null, null);
                        }


                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
        }

        protected void lbtnassload_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fdate = DateTime.Now;
                txtFDate.Text = fdate.AddMonths(-1).ToString("dd/MMM/yyyy");
                DateTime tdate = DateTime.Now;
                txtTDate.Text = tdate.ToString("dd/MMM/yyyy");

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword2.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessmentDocs);
                DataTable result = CHNLSVC.Financial.LoadSettleDocs(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                result.Columns.Remove("ISTH_DT");
                grdResult2.DataSource = result;
                grdResult2.DataBind();
                lblvalue.Text = "438";
                //BindUCtrlDDLDataCustom(result);
                BindUCtrlDDLDataCustomForAssesmnt(result);
                ViewState["SEARCH"] = result;
                grdResult2.PageIndex = 0;
                //SIPopup.Show();
                ASPopup.Show();
                txtSearchbyword2.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbrnapprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtapprove.Value == "Yes")
                {
                    string ordstatus = (string)Session["STUS"];
                    _userid = (string)Session["UserID"];

                    Int32 appresult = 0;
                    DateTime? Mydate = null;

                    if (string.IsNullOrEmpty(txtassref.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a document !!!')", true);
                        lbtnassload.Focus();
                        return;
                    }

                    string seqnoapprove = (string)Session["SOSEQNO"].ToString();

                    appresult = CHNLSVC.Financial.UpdateAssessmentHeaderStatus("A", _userid, Convert.ToDateTime(DateTime.Now.Date), Session["SessionID"].ToString(), txtassref.Text.Trim(), Session["UserCompanyCode"].ToString(), Convert.ToInt32(seqnoapprove), string.Empty, Convert.ToDateTime(Mydate), string.Empty);

                    if (appresult > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully approved !!!')", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            if (txtcancel.Value == "Yes")
            {
                try
                {
                    string ordstatus = (string)Session["STUS"];
                    _userid = (string)Session["UserID"];

                    Int32 cancelesult = 0;
                    Int32 resetresults = 0;
                    DateTime? Mydatecancel = null;

                    if (string.IsNullOrEmpty(txtassref.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a document !!!')", true);
                        lbtnassload.Focus();
                        return;
                    }

                    string seqnocancel = (string)Session["SOSEQNO"].ToString();

                    cancelesult = CHNLSVC.Financial.UpdateAssessmentHeaderStatus("C", string.Empty, Convert.ToDateTime(Mydatecancel), string.Empty, txtassref.Text.Trim(), Session["UserCompanyCode"].ToString(), Convert.ToInt32(seqnocancel), _userid, Convert.ToDateTime(DateTime.Now), Session["SessionID"].ToString());

                    foreach (GridViewRow gvrow in gvtotentry2.Rows)
                    {
                        Label lblentry = (Label)gvrow.FindControl("lblbondno");
                        resetresults = CHNLSVC.Financial.ResetCusDecHeaderAndDutySum(Session["UserCompanyCode"].ToString(), lblentry.Text.Trim(), _userid, Convert.ToDateTime(DateTime.Now.Date), Session["SessionID"].ToString());
                        canceldutysum.Add(resetresults);
                    }

                    if ((cancelesult > 0) && (!canceldutysum.Contains(-1)))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully cancelled !!!')", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
        }

        protected void lbtnremoveentry_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "Yes")
                {
                    if (gvtotentry2.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No data found to remove !!!')", true);
                        gvtotentry2.Focus();
                        return;
                    }

                    foreach (GridViewRow gvrow in gvtotentry2.Rows)
                    {
                        CheckBox chkselect = (CheckBox)gvrow.FindControl("chkcancel");
                        if (chkselect.Checked == true)
                        {
                            lstischecked.Add(1);
                        }
                        else
                        {
                            lstischecked.Add(0);
                        }
                    }

                    if (!lstischecked.Contains(1))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select entry/entries from total entry details summary !!!')", true);
                        gvtotentry2.Focus();
                        return;
                    }

                    DataTable dt1 = (DataTable)ViewState["Entries"];
                    DataView dv = new DataView(dt1);

                    foreach (GridViewRow gvrow2 in gvtotentry2.Rows)
                    {
                        CheckBox chkselect2 = (CheckBox)gvrow2.FindControl("chkcancel");

                        if (chkselect2.Checked == true)
                        {
                            Label lblentry = (Label)gvrow2.FindControl("lblbondno");
                            entrynolst.Add(lblentry.Text);
                        }
                    }

                    string filtervalues = String.Join("','", entrynolst.Where(x => x != null).Select(x => x.ToString()));

                    dv.RowFilter = "istd_entry_no not in ('" + filtervalues + "')";

                    dt1 = dv.ToTable();

                    gvtotentry2.DataSource = dt1;
                    gvtotentry2.DataBind();

                    ViewState["Entries"] = dt1;


                    DataTable dt2 = (DataTable)ViewState["Conditions"];
                    DataView dv2 = new DataView(dt2);
                    dv2.RowFilter = "istd_entry_no not in ('" + filtervalues + "')";
                    dt2 = dv2.ToTable();

                    DataTable _dtRemoveData = (DataTable)ViewState["Conditions"];
                    DataView _dvRemoveData = new DataView(_dtRemoveData);
                    _dvRemoveData.RowFilter = "istd_entry_no in ('" + filtervalues + "')";
                    _dtRemoveData = _dvRemoveData.ToTable();
                    foreach (DataRow dr in _dtRemoveData.Rows)
                    {
                        ImpAstDet _det = new ImpAstDet();
                        _det.ISTD_SEQ_NO = Convert.ToInt32(dr["istd_seq_no"].ToString());
                        _det.ISTD_ENTRY_NO = dr["istd_entry_no"].ToString();
                        _det.ISTD_ASSESS_NO = dr["istd_assess_no"].ToString();
                        _det.ISTD_COST_ELE = dr["istd_cost_ele"].ToString();
                        _det.ISTD_STUS = "C";
                        _remAstDet.Add(_det);
                    }


                    gvtotentry1.DataSource = dt2;
                    gvtotentry1.DataBind();

                    ViewState["Conditions"] = dt2;

                    CalCulateFinalEntryTotAmt();
                    CalCulateFinalAssTot();
                    CalCulateFinalDiff();

                    Session["ASS_NO"] = null;
                    Session["ENTRY_NO"] = null;

                    assno.Clear();
                    entryno.Clear();

                    foreach (GridViewRow gvrow in gvaod.Rows)
                    {
                        Label lblentrynoaod = (Label)gvrow.FindControl("lblentrynoaod");

                        if (filtervalues == lblentrynoaod.Text.Trim())
                        {
                            gvaod.DataSource = null;
                            gvaod.DataBind();
                        }
                        //lblentrynoaod.Text = bondno;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtbondno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // ClearNew();
                //txtassref.Text = string.Empty;
                if (string.IsNullOrEmpty(txtbondno.Text))
                {
                    return;
                }
                if (!string.IsNullOrEmpty(txtbondno.Text))
                {
                    string _bondNo = "";
                    string _lateBond = "";
                    bool b = false;
                    if (chkpending.Checked == true)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecSavedEntry);
                        DataTable result = CHNLSVC.Financial.LoadSavedEntryPopUp(SearchParams, "Code", txtbondno.Text);
                        foreach (DataRow row in result.Rows)
                        {
                            if (row["Code"].ToString() == txtbondno.Text)
                            {
                                b = true;
                                _bondNo = txtbondno.Text;
                                _lateBond = txtbondno.Text;
                                break;
                            }
                        }
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecBondNo);
                        DataTable result = CHNLSVC.Financial.LoadBondNumbersNew(SearchParams, "Document No", txtbondno.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), 0);
                        foreach (DataRow row in result.Rows)
                        {
                            if (row["Document No"].ToString() == txtbondno.Text)
                            {
                                txtrlatedbond.Text = row["Other Ref"].ToString();
                                b = true;
                                break;
                            }
                        }
                    }
                    if (!b)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document # is invalid !!!')", true);
                        txtbondno.Text = "";
                        txtbondno.Focus();
                        return;
                    }
                    else
                    {
                        string bondno = txtbondno.Text;
                        txtrlatedbond.Text = _lateBond;
                        DataTable dtbonddata = CHNLSVC.Financial.LoadCusDecDutySum(bondno);

                        //Lakshika - Hide zero duties
                        var zeroDuties = dtbonddata.AsEnumerable().Where(r => Convert.ToInt32(r["CUDS_COST_ELE_AMT"]) <= 0).ToList();
                        foreach (DataRow rowToDelete in zeroDuties)
                            rowToDelete.Delete();
                        dtbonddata.AcceptChanges();

                        //Lakshan
                        dtbonddata.Columns.Add(new DataColumn("rowNo", typeof(Int32)));
                        Int32 _row = 0;
                        int roundupVal = 0; //Lakshika
                        foreach (DataRow row in dtbonddata.Rows)
                        {
                            _row++;
                            row["rowNo"] = _row;
                            roundupVal = (int)(Convert.ToDecimal(row["CUDS_COST_ELE_AMT"]) + 0.5m);//(a + 0.5m); //Lakshika
                            row["CUDS_COST_ELE_AMT"] = roundupVal;
                        }

                        gvDutyentrydetails.DataSource = null;
                        gvDutyentrydetails.DataBind();

                        gvDutyentrydetails.DataSource = dtbonddata;
                        gvDutyentrydetails.DataBind();

                        CalCulateEntryTotAmt();
                        CalCulateAssementTot();

                        Decimal val1 = Convert.ToDecimal(txtasstot.Text.Trim());
                        Decimal val2 = Convert.ToDecimal(txtentrytotal.Text.Trim());
                        Decimal diff = val1 - val2;
                        txtcaldiff.Text = DoFormat(diff);

                        foreach (GridViewRow gvrow in gvDutyentrydetails.Rows)
                        {
                            Label entryamt = (Label)gvrow.FindControl("lblamount");
                            TextBox assamt = (TextBox)gvrow.FindControl("txtassamt");
                            entryamt.Text = DoFormat(Convert.ToDecimal(entryamt.Text.Trim()));
                            assamt.Text = INTFormat(Convert.ToDecimal(entryamt.Text.Trim())); //Corrected by Chamal 31-03-2016
                        }

                        DataTable dtaod = CHNLSVC.Financial.LoadAODDocs(bondno, Session["UserCompanyCode"].ToString(), "AOD");

                        gvaod.DataSource = null;
                        gvaod.DataBind();

                        gvaod.DataSource = dtaod;
                        gvaod.DataBind();

                        foreach (GridViewRow gvrow in gvaod.Rows)
                        {
                            Label lblentrynoaod = (Label)gvrow.FindControl("lblentrynoaod");
                            lblentrynoaod.Text = bondno;
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        if (gvDutyentrydetails.Rows.Count > 0)
                        {
                            TextBox txtassamt = (TextBox)gvDutyentrydetails.Rows[0].FindControl("txtassamt");
                            if (txtassamt != null)
                            {
                                txtassamt.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void chkDoDfs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDoDfs.Checked)
            {
                txtbondno.Text = "";
                chkReBond.Checked = false;
            }
        }

        protected void chkReBond_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReBond.Checked)
            {
                chkDoDfs.Checked = false;
            }
        }

        protected void txtassnoticeno_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var r = (GridViewRow)lb.NamingContainer;
            Label lblbondno = r.FindControl("lblbondno") as Label;
            txtbondno.Text = lblbondno.Text;
            //  txtbondno_TextChanged(null, null);
            DataTable dtbonddata = CHNLSVC.Financial.LoadCusDecDutySum(lblbondno.Text);

            //Lakshika - Hide zero duties
            var zeroDuties = dtbonddata.AsEnumerable().Where(x => Convert.ToInt32(x["CUDS_COST_ELE_AMT"]) <= 0).ToList();
            foreach (DataRow rowToDelete in zeroDuties)
                rowToDelete.Delete();
            dtbonddata.AcceptChanges();

            //Lakshan
            dtbonddata.Columns.Add(new DataColumn("rowNo", typeof(Int32)));
            Int32 _row = 0;
            int roundupVal = 0;//Lakshika
            decimal decVal = 0.5m;
            foreach (DataRow row in dtbonddata.Rows)
            {
                _row++;
                row["rowNo"] = _row;
                roundupVal = (int)(Convert.ToDecimal(row["CUDS_COST_ELE_AMT"]));//(a + 0.5m); //Lakshika
                decVal = (decimal)(Convert.ToDecimal(roundupVal + 0.05m));
                row["CUDS_COST_ELE_AMT"] = (int)Convert.ToDecimal(roundupVal);
                string newValue = roundupVal.ToString("0.00");
                row["CUDS_COST_ELE_AMT"] = newValue;

                int stlAmount = (int)(Convert.ToDecimal(row["CUDS_COST_STL_AMT"]));
                row["CUDS_COST_STL_AMT"] = stlAmount.ToString("0.00");

            }

            //Lakshika
            // List<string> tempList = new List<string>();
            List<DutyDetail> tempList = new List<DutyDetail>();

            DataTable dt = ViewState["Conditions"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (lblbondno.Text == Convert.ToString(row["istd_entry_no"]))
                {
                    DutyDetail _obj = new DutyDetail();
                    _obj.Id = Convert.ToString(row["ISTD_COST_ELE"]);
                    _obj.Name = Convert.ToString(row["ISTD_COST_STL_AMT"]);
                    //tempList.Add(Convert.ToString(row["ISTD_COST_STL_AMT"]));
                    tempList.Add(_obj);
                }

            }

            int i = 0;
            foreach (DataRow row in dtbonddata.Rows)
            {
                var _filter = tempList.Find(x => x.Id == row["CUDS_COST_ELE"].ToString());
                if (_filter != null)
                {
                    row["CUDS_COST_STL_AMT"] = _filter.Name;
                }

                //string stlAmount = tempList[i].ToString();//Convert.ToString((row["CUDS_COST_STL_AMT"]));
                //row["CUDS_COST_STL_AMT"] = stlAmount;
                //i++;
            }

            gvDutyentrydetails.DataSource = null;
            gvDutyentrydetails.DataBind();

            gvDutyentrydetails.DataSource = dtbonddata;
            gvDutyentrydetails.DataBind();

            CalCulateEntryTotAmt();
            CalCulateAssementTot();
            Decimal val1 = Convert.ToDecimal(txtasstot.Text.Trim());
            Decimal val2 = Convert.ToDecimal(txtentrytotal.Text.Trim());
            Decimal diff = val1 - val2;
            txtcaldiff.Text = DoFormat(diff);
            //lbtnaddentry.Enabled = false;
            // lbtnaddentry.CssClass = "buttoncolorleft";
            // lbtnaddentry.OnClientClick = "return Enable();";

            List<ImpAstDet> _listImpAst = CHNLSVC.Inventory.GET_Entry_no_Ammend(txtassref.Text, lblbondno.Text);
            if (_listImpAst != null)
            {
                if (_listImpAst.Count > 0)
                {
                    _isEntryUpdate = true;
                    lbtnaddentry.Enabled = true;
                    lbtnaddentry.CssClass = "buttonUndocolorLeft";
                    txtassnoticeno.ReadOnly = false;

                    lbtnremoveentry.Enabled = true;
                    lbtnremoveentry.CssClass = "buttonUndocolorLeft";
                    lbtnremoveentry.OnClientClick = "ConfirmDelete();";
                }
            }

            foreach (GridViewRow row in gvDutyentrydetails.Rows)
            {
                TextBox txtassamt = row.FindControl("txtassamt") as TextBox;
                txtassamt.Enabled = false;
            }

            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessmentDocs);
            //DataTable result = CHNLSVC.Financial.LoadSettleDocs(SearchParams, "Doc No", txtassref.Text);
            DataTable dtsummary = CHNLSVC.Financial.LoadSettleDocSummary(Session["UserCompanyCode"].ToString(), txtassref.Text);
            if (dtsummary != null)
            {
                foreach (DataRow dr in dtsummary.Rows)
                {
                    if (dr["istd_entry_no"].ToString() == lblbondno.Text)
                    {
                        txtassnoticeno.Text = dr["cuh_ast_noties_no"].ToString();
                        break;
                    }
                }
            }
            chkpending.Checked = false;
            txtbondno.Enabled = true;
        }

        protected void chkpending_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpending.Checked)
            {
                foreach (GridViewRow row in gvDutyentrydetails.Rows)
                {
                    TextBox txtassamt = row.FindControl("txtassamt") as TextBox;
                    txtassamt.Enabled = true;
                }

                txtbondno.Enabled = false;
                lbtnaddentry.Enabled = true;
            }
            else
            {
                txtbondno.Enabled = true;
            }
        }

        protected void txtassref_TextChanged(object sender, EventArgs e)
        {
            ClearNew();
            txtbondno.Text = "";
            if (string.IsNullOrEmpty(txtassref.Text))
            {
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessmentDocs);
            // DataTable result = CHNLSVC.Financial.LoadSettleDocs(SearchParams, "Doc No", txtassref.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            DataTable result = CHNLSVC.Financial.ValidateAssesmentNo(SearchParams, "Doc No", txtassref.Text);

            bool valid = false;
            foreach (DataRow row in result.Rows)
            {
                if (row["Doc No"].ToString() == txtassref.Text)
                {
                    txtrlatedbond.Text = row["Doc No"].ToString();
                    valid = true;
                    break;
                }
            }
            if (!valid)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document # is invalid !!!')", true);
                txtassref.Text = "";
                txtassref.Focus();
                ClearNew();
                return;
            }

            DataTable dtsummary = CHNLSVC.Financial.LoadSettleDocSummary(Session["UserCompanyCode"].ToString(), txtassref.Text);
            // dtsummary=
            DataView dv = dtsummary.DefaultView;
            dv.Sort = "istd_line_no";
            dtsummary = dv.ToTable();
            gvDutyentrydetails.DataSource = null;
            gvDutyentrydetails.DataBind();

            foreach (DataRow ddr in dtsummary.Rows)
            {
                Session["STUS"] = ddr["ISTH_STUS"];
            }

            foreach (DataRow ddr in dtsummary.Rows)
            {
                Session["SOSEQNO"] = ddr["ISTH_SEQ_NO"];
            }

            string stus = (string)Session["STUS"];

            if (stus == "A")
            {
                lbrnapprove.Enabled = false;
                lbrnapprove.CssClass = "buttoncolorleft";
                lbrnapprove.OnClientClick = "return Enable();";
            }
            else
            {
                lbrnapprove.Enabled = true;
                lbrnapprove.CssClass = "buttonUndocolorLeft";
                lbrnapprove.OnClientClick = "ConfirmApproveRequest();";
            }

            if (stus == "C")
            {
                lbtncancel.Enabled = false;
                lbtncancel.CssClass = "buttoncolorleft";
                lbtncancel.OnClientClick = "return Enable();";

                lbtnsave.Enabled = false;
                lbtnsave.CssClass = "buttoncolorleft";
                lbtnsave.OnClientClick = "return Enable();";
            }
            else
            {
                lbtncancel.Enabled = true;
                lbtncancel.CssClass = "buttonUndocolorLeft";
                lbtncancel.OnClientClick = "ConfirmCancelReq();";

                lbtnsave.Enabled = true;
                lbtnsave.CssClass = "buttonUndocolorLeft";
                lbtnsave.OnClientClick = "ConfirmSave();";
            }

            lbtnremoveentry.Enabled = false;
            lbtnremoveentry.CssClass = "buttoncolorleft";
            lbtnremoveentry.OnClientClick = "return Enable();";

            lbtnaddentry.Enabled = false;
            lbtnaddentry.CssClass = "buttoncolorleft";
            lbtnaddentry.OnClientClick = "return Enable();";

            List<ImpAstHeader> _listAstHeader = CHNLSVC.Inventory.GET_IMP_AST_HDR(new ImpAstHeader()
            {
                ISTH_DOC_NO = txtassref.Text,
                ISTH_STUS = "A",
                ISTH_IS_STL = 0
            });

            if (_listAstHeader != null)
            {
                if (_listAstHeader.Count > 0)
                {
                    lbtnaddentry.Enabled = true;
                    lbtnaddentry.CssClass = "buttonUndocolorLeft";
                    txtassnoticeno.ReadOnly = false;
                }
            }

            gvtotentry2.DataSource = null;
            gvtotentry2.DataBind();

            gvtotentry2.DataSource = dtsummary;
            gvtotentry2.DataBind();

            ViewState["Entries"] = dtsummary;
            DataTable dtalldetails = CHNLSVC.Financial.LoadAllEntryDetails(txtassref.Text, Session["UserCompanyCode"].ToString());

            gvtotentry1.DataSource = null;
            gvtotentry1.DataBind();

            gvtotentry1.DataSource = dtalldetails;
            gvtotentry1.DataBind();
            ViewState["Conditions"] = dtalldetails;
            CalCulateFinalEntryTotAmt();
            CalCulateFinalAssTot();
            CalCulateFinalDiff();
        }





        #region Modal Popup 2

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "434")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecBondNo);
                DataTable result = CHNLSVC.Financial.LoadBondNumbersNew(SearchParams, null, null, Convert.ToDateTime(txtNewFrom.Text), Convert.ToDateTime(txtNewTo.Text), 1);
                grdResultDate.DataSource = result;
                grdResultDate.DataBind();
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
            }
        }
        protected void btnDCloseNew_Click(object sender, EventArgs e)
        {
            txtSearchbywordDate.Text = "";
            Session["DPopup"] = "";
            UserDPopoup.Hide();
        }
        protected void grdResultDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordDate.ClientID + "').value = '';", true);
            string Name = grdResultDate.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "434")
            {
                txtbondno.Text = Name;
                lblvalue.Text = "";
                Session["DPopup"] = "";
                UserDPopoup.Hide();
                return;
            }

        }
        protected void grdResultDate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (lblvalue.Text == "434")
            {
                try
                {
                    grdResultDate.PageIndex = e.NewPageIndex;
                    grdResultDate.DataSource = null;
                    grdResultDate.DataSource = (DataTable)ViewState["SEARCH"];
                    grdResultDate.DataBind();
                    grdResultDate.PageIndex = 0;
                    UserDPopoup.Show();
                    txtSearchbyword.Focus();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
                return;
            }

        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {

            FilterData();
        }
        protected void txtSearchbywordDate_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }
        #endregion

        public class DutyDetail
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                //clsInventory obj = new clsInventory();
                //InvReportPara _objRepoPara = new InvReportPara();
                //_objRepoPara = Session["InvReportPara"] as InvReportPara;
                //obj.Print_AOA_Warra(_objRepoPara);
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error')", true);
                return;
            }
        }
        protected void lbtnASTprint_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                string docno = txtassref.Text.ToString();
                if (docno == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Assessment No')", true);
                    return;
                }
                else
                {
                    docno = txtassref.Text.ToString();
                    string com = Session["UserCompanyCode"].ToString();
                    string loc = Session["UserDefLoca"].ToString();
                    string user = Session["UserID"].ToString();
                    Session["GlbReportName"] = "ASTReport.rpt";
                    BaseCls.GlbReportHeading = "AST Sheet";
                    clswharf obj = new clswharf();
                    obj.CusdecAssessment(docno, loc, com, user);
                    PrintPDF(targetFileName, obj._cusdec_assessment);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("CusdecAsses", "CusdecAsses", "Run Ok", Session["UserID"].ToString());

                }



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error')", true);
                CHNLSVC.MsgPortal.SaveReportErrorLog("AST Print", "CusdecAssessments", ex.Message, Session["UserID"].ToString());
                return;
            }
        }

        protected void lbtnASTprint2_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                string docno = txtassref.Text.ToString();
                if (docno == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Assessment No')", true);
                    return;
                }
                else
                {
                    docno = txtassref.Text.ToString();
                    string com = Session["UserCompanyCode"].ToString();
                    string loc = Session["UserDefLoca"].ToString();
                    string user = Session["UserID"].ToString();
                    Session["GlbReportName"] = "ASTReportII.rpt";
                    BaseCls.GlbReportHeading = "AST Sheet2";
                    clswharf obj = new clswharf();
                    obj.CusdecAssessment2(docno, loc, com, user);
                    PrintPDF(targetFileName, obj._cusdec_assessment2);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("CusdecAsses", "CusdecAsses", "Run Ok", Session["UserID"].ToString());

                }



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error')", true);
                CHNLSVC.MsgPortal.SaveReportErrorLog("AST PrintII", "CusdecAssessments", ex.Message, Session["UserID"].ToString());
                return;
            }
        }

        protected void lbtnASTAccountprint_Click(object sender, EventArgs e)
        {
            
        }
    }
}