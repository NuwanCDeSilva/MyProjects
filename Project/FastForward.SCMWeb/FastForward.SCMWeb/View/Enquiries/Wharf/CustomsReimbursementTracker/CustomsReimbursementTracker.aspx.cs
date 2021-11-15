using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Imports.Customs_Reimbursement_Tracker
{
    public partial class CustomsReimbursementTracker : BasePage
    {
        string _para = "";
        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        string _serType
        {
            get { if (Session["_serType"] != null) { return (string)Session["_serType"]; } else { return ""; } }
            set { Session["_serType"] = value; }
        }
        string _showPop
        {
            get { if (Session["_showPop"] != null) { return (string)Session["_showPop"]; } else { return ""; } }
            set { Session["_showPop"] = value; }
        }

        string _userid = string.Empty;
        string _currCode { set { Session["_currCode"] = value; } get { return (string)Session["_currCode"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    MasterCompany _mstComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                    if (_mstComp != null)
                    {
                        _currCode = _mstComp.Mc_cur_cd;
                    }
                    gventrydetails.DataSource = new int[] { };
                    gventrydetails.DataBind();
                    Label lblSetAmt = gventrydetails.HeaderRow.FindControl("lblSetAmt") as Label;
                    Label lblAssAmt = gventrydetails.HeaderRow.FindControl("lblAssAmt") as Label;
                    lblSetAmt.Text = "STL Amt (" + _currCode + ")";
                    lblAssAmt.Text = "AST Amt (" + _currCode + ")";

                    gvaodout.DataSource = new int[] { };
                    gvaodout.DataBind();

                    gvaodin.DataSource = new int[] { };
                    gvaodin.DataBind();

                    DateTime orddate = DateTime.Now;
                    dtpFromDate.Text = orddate.ToString("dd/MMM/yyyy");

                    DateTime orddate2 = DateTime.Now;
                    dtpToDate.Text = orddate2.ToString("dd/MMM/yyyy");

                    txtFDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                    txtTDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

                    _showPop = "Hide";
                }
                else
                {
                    if (_showPop == "Show")
                    {
                        UserPopoup.Show();
                    }
                    else
                    {
                        UserPopoup.Hide();
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

        private void Clear()
        {
            try
            {
                _showPop = "Hide";
                gventrydetails.DataSource = new int[] { };
                gventrydetails.DataBind();
                Label lblSetAmt = gventrydetails.HeaderRow.FindControl("lblSetAmt") as Label;
                Label lblAssAmt = gventrydetails.HeaderRow.FindControl("lblAssAmt") as Label;
                lblSetAmt.Text = "STL Amt (" + _currCode + ")";
                lblAssAmt.Text = "AST Amt (" + _currCode + ")";

                gvaodout.DataSource = new int[] { };
                gvaodout.DataBind();

                gvaodin.DataSource = new int[] { };
                gvaodin.DataBind();

                txtentryno.Text = string.Empty;
                txtastno.Text = string.Empty;
                txtstlno.Text = string.Empty;
                txtcustomref.Text = string.Empty;
                txtassesnoticno.Text = string.Empty;
                ddlstus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmclear.Value == "Yes")
                {
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.EntryNoSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AssessementNoSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SettlementNoSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomRefNoSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AssessmentNoticeNoSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "445")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNoSearch);
                    DataTable result = CHNLSVC.Financial.LoadEntryPopUp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "445";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "446")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessementNoSearch);
                    DataTable result = CHNLSVC.Financial.LoadAssementPopUp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "446";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "447")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SettlementNoSearch);
                    DataTable result = CHNLSVC.Financial.LoadSettlementPopUp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "447";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "448")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomRefNoSearch);
                    DataTable result = CHNLSVC.Financial.LoadCusRefPopUp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "448";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "449")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessmentNoticeNoSearch);
                    DataTable result = CHNLSVC.Financial.LoadAssNoticePopUp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "449";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
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
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                SIPopup.Show();
                txtSearchbyword.Focus();
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
                if (lblvalue.Text == "445")
                {
                    string entry = grdResult.SelectedRow.Cells[1].Text;

                    txtentryno.Text = entry;
                    txtentryno_TextChanged(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "446")
                {
                    string assno = grdResult.SelectedRow.Cells[1].Text;

                    txtastno.Text = assno;
                    txtastno_TextChanged(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "447")
                {
                    string settleno = grdResult.SelectedRow.Cells[1].Text;

                    txtstlno.Text = settleno;
                    txtstlno_TextChanged(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "448")
                {
                    string cusref = grdResult.SelectedRow.Cells[1].Text;

                    txtcustomref.Text = cusref;
                    txtcustomref_TextChanged(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "449")
                {
                    string assnotice = grdResult.SelectedRow.Cells[1].Text;

                    txtassesnoticno.Text = assnotice;
                    txtassesnoticno_TextChanged(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
            }
            catch
            {

            }
        }

        protected void gventrydetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string docNo = (gventrydetails.SelectedRow.FindControl("lblentryno") as Label).Text;

                DataTable dtalldetails1 = CHNLSVC.Financial.LoadAODDetailsWithItem(docNo, Session["UserCompanyCode"].ToString(), "AOD", 0);

                //GROUP ITEM

                var query = from r in dtalldetails1.AsEnumerable()
                            group r by r.Field<string>("aod_no") into groupedTable
                            select new
                            {
                                aod_no = groupedTable.Key,
                                from_location = groupedTable.Max(s => s.Field<string>("from_location")),
                                aod_date = groupedTable.Max(s => s.Field<DateTime>("aod_date")),
                                to_location = groupedTable.Max(s => s.Field<string>("to_location")),
                                Item = groupedTable.Max(s => s.Field<string>("Item")),
                                qty = groupedTable.Sum(s => s.Field<decimal>("qty")),
                                FROM_LOC_DESC = groupedTable.Max(s => s.Field<string>("FROM_LOC_DESC")),
                                TO_LOC_DESC = groupedTable.Max(s => s.Field<string>("TO_LOC_DESC")),
                            };




                dtalldetails1 = ConvertToDataTable(query);


                gvaodout.DataSource = null;
                gvaodout.DataBind();

                if (dtalldetails1.Rows.Count > 0)
                {
                    DataView dv = dtalldetails1.DefaultView;
                    dv.Sort = "aod_date DESC";
                    dtalldetails1 = dv.ToTable();
                }

                gvaodout.DataSource = dtalldetails1;
                gvaodout.DataBind();

                DataTable dtalldetails2 = CHNLSVC.Financial.LoadAODDetailsWithItem(docNo, Session["UserCompanyCode"].ToString(), "AOD", 1);

                gvaodin.DataSource = null;
                gvaodin.DataBind();
                var query2 = from r in dtalldetails2.AsEnumerable()
                            group r by r.Field<string>("aod_no") into groupedTable
                            select new
                            {
                                aod_no = groupedTable.Key,
                                from_location = groupedTable.Max(s => s.Field<string>("from_location")),
                                aod_date = groupedTable.Max(s => s.Field<DateTime>("aod_date")),
                                to_location = groupedTable.Max(s => s.Field<string>("to_location")),
                                Item = groupedTable.Max(s => s.Field<string>("Item")),
                                qty = groupedTable.Sum(s => s.Field<decimal>("qty")),
                                FROM_LOC_DESC = groupedTable.Max(s => s.Field<string>("FROM_LOC_DESC")),
                                TO_LOC_DESC = groupedTable.Max(s => s.Field<string>("TO_LOC_DESC")),
                                OTH_DOC = groupedTable.Max(s => s.Field<string>("OTH_DOC")),
                            };




                dtalldetails2 = ConvertToDataTable(query2);



                if (dtalldetails2.Rows.Count > 0)
                {
                    DataView dv = dtalldetails2.DefaultView;
                    dv.Sort = "aod_date DESC";
                    dtalldetails2 = dv.ToTable();
                }

                gvaodin.DataSource = dtalldetails2;
                gvaodin.DataBind();
                txtentryno.Text = txtentryno.Text.Trim().ToString();
                // cusdec entry item and balance 
                DataTable entryitembal = CHNLSVC.Inventory.GetEntryProgDetails(txtentryno.Text.Trim().ToString());
                //grdentrydetails
                grdentrydetails.DataSource = entryitembal;
                grdentrydetails.DataBind();
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
        public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();


            // column names
            PropertyInfo[] oProps = null;


            if (varlist == null) return dtReturn;


            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;


                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }


                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }


                DataRow dr = dtReturn.NewRow();


                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }


                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        protected void gventrydetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gventrydetails, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[0].Attributes["style"] = "cursor:pointer";
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblpick = e.Row.FindControl("lblassstus") as Label;
                    Label lblignore = e.Row.FindControl("lblignored") as Label;
                    Label lblbypassstus = e.Row.FindControl("lblbypassstus") as Label;
                    LinkButton lblbypass = e.Row.FindControl("lbtnbypass") as LinkButton;

                    //if ((lblpick.Text == "0") && (lblignore.Text != "1"))
                    if (lblbypassstus.Text == "Allowed")
                    {
                        lblbypass.Enabled = true;
                        lblbypass.CssClass = "buttonUndocolor";
                        lblbypass.OnClientClick = "ConfirmByPass();";
                    }
                    else
                    {
                        lblbypass.Enabled = false;
                        lblbypass.CssClass = "buttoncolor";
                        lblbypass.OnClientClick = "return Enable();";
                    }
                    //lblbypass.Enabled = true;
                    //lblbypass.CssClass = "buttonUndocolor";
                    //lblbypass.OnClientClick = "ConfirmByPass();";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnperiod_Click(object sender, EventArgs e)
        {
            try
            {
                grdentrydetails.DataSource = new int[] { };
                grdentrydetails.DataBind();
                string status = string.Empty;
                if (ddlstus.SelectedValue == "1")
                {
                    status = null;
                }
                else
                {
                    status = "0";
                }

                DateTime fromdate = Convert.ToDateTime(dtpFromDate.Text);
                DateTime todate = Convert.ToDateTime(dtpToDate.Text);

                if (fromdate > todate)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid date range !!!')", true);
                    return;
                }

                DataTable dtentry = CHNLSVC.Financial.LoadEntriesByDateNew(Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text), status);
                gventrydetails.DataSource = new int[] { };
                // gventrydetails.DataBind();

                if (dtentry.Rows.Count > 0)
                {
                    DataView dv = dtentry.DefaultView;
                    dv.Sort = "cuh_dt DESC";
                    dtentry = dv.ToTable();
                }
                for (int x = dtentry.Rows.Count - 1; x >= 0; x--)
                {
                    DataRow dr = dtentry.Rows[x];
                    if (x > 500)
                    {
                        dr.Delete();
                    }
                }
                dtentry.AcceptChanges();

                gventrydetails.DataSource = dtentry;
                gventrydetails.DataBind();

                Label lblSetAmt = gventrydetails.HeaderRow.FindControl("lblSetAmt") as Label;
                Label lblAssAmt = gventrydetails.HeaderRow.FindControl("lblAssAmt") as Label;
                lblSetAmt.Text = "STL Amt (" + _currCode + ")";
                lblAssAmt.Text = "AST Amt (" + _currCode + ")";

                //foreach (GridViewRow row in gventrydetails.Rows)
                //{
                //    Label entry = (Label)row.FindControl("lblentryno");
                //    DataTable dtasstot = CHNLSVC.Financial.CountAssessmentTot(entry.Text.Trim());
                //    string asstot = string.Empty;
                //    foreach (DataRow ddr in dtasstot.Rows)
                //    {
                //        asstot = ddr["Assessment_Amount"].ToString();
                //    }
                //    Label lblassamt = (Label)row.FindControl("lblassamt");
                //    lblassamt.Text = asstot;

                //    if (!string.IsNullOrEmpty(asstot))
                //    {
                //        Decimal formatasstot = Convert.ToDecimal(asstot);
                //        lblassamt.Text = formatasstot.ToString("N2");
                //    }

                //    DataTable dtstlot = CHNLSVC.Financial.CalSettleTotbyEntry(entry.Text.Trim());
                //    string stltot = string.Empty;
                //    foreach (DataRow ddr2 in dtstlot.Rows)
                //    {
                //        stltot = ddr2["Settle_Amount"].ToString();
                //    }
                //    Label lblsettleamt = (Label)row.FindControl("lblsettleamt");
                //    lblsettleamt.Text = stltot;

                //    if (!string.IsNullOrEmpty(stltot))
                //    {
                //        Decimal formatstltot = Convert.ToDecimal(stltot);
                //        lblsettleamt.Text = formatstltot.ToString("N2");
                //    }
                //}
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

        protected void lbtnbypass_Click(object sender, EventArgs e)
        {
            try
            {
                _userid = (string)Session["UserID"];

                if (hidbypass.Value == "Yes")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16040))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You do not have permission for this function.Permission Code :- 16040 !!!')", true);
                        return;
                    }

                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;

                    if (row != null)
                    {

                        string entry = (row.FindControl("lblentryno") as Label).Text;
                        ImpCusdecHdr CusDecHeader = new ImpCusdecHdr();
                        CusDecHeader.CUH_DOC_NO = entry;
                        CusDecHeader.CUH_AST_IGNORE_BY = _userid;
                        CusDecHeader.CUH_AST_IGNORE_DT = Convert.ToDateTime(DateTime.Now.Date);
                        Int32 results = CHNLSVC.Financial.ByPassCusDec(CusDecHeader);

                        if (results > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully saved !!!')", true);
                            Clear();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            return;
                        }
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

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            try
            {
                if ((string.IsNullOrEmpty(txtentryno.Text.Trim())) && (string.IsNullOrEmpty(txtastno.Text.Trim())) && (string.IsNullOrEmpty(txtstlno.Text.Trim())) && (string.IsNullOrEmpty(txtcustomref.Text.Trim())) && (string.IsNullOrEmpty(txtassesnoticno.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a search criteria !!!')", true);
                    return;
                }

                ImpCusdecHdr CusDecHeaders = new ImpCusdecHdr();
                CusDecHeaders.CUH_DOC_NO = txtentryno.Text.Trim();
                CusDecHeaders.CUH_AST_NO = txtastno.Text.Trim();
                CusDecHeaders.CUH_STL_NO = txtstlno.Text.Trim();
                CusDecHeaders.CUH_CUSDEC_ENTRY_NO = txtcustomref.Text.Trim();
                CusDecHeaders.CUH_AST_NOTIES_NO = txtassesnoticno.Text.Trim();

                DataTable dtentry = CHNLSVC.Financial.LoadCustomsReimbursements(CusDecHeaders);

                gventrydetails.DataSource = new int[] { };
                gventrydetails.DataBind();

                grdentrydetails.DataSource = new int[] { };
                grdentrydetails.DataBind();


                Label lblSetAmt = gventrydetails.HeaderRow.FindControl("lblSetAmt") as Label;
                Label lblAssAmt = gventrydetails.HeaderRow.FindControl("lblAssAmt") as Label;
                lblSetAmt.Text = "STL Amt (" + _currCode + ")";
                lblAssAmt.Text = "AST Amt (" + _currCode + ")";

                if (dtentry.Rows.Count == 0)
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter/select a valid number !!!')", true);
                    //Clear();
                    //return;
                }
                else
                {
                    if (dtentry.Rows.Count > 0)
                    {
                        DataView dv = dtentry.DefaultView;
                        dv.Sort = "cuh_dt DESC";
                        dtentry = dv.ToTable();
                    }

                    gventrydetails.DataSource = dtentry;
                    gventrydetails.DataBind();


                    foreach (GridViewRow row in gventrydetails.Rows)
                    {
                        Label entry = (Label)row.FindControl("lblentryno");

                        DataTable dtasstot = CHNLSVC.Financial.CountAssessmentTot(entry.Text.Trim());
                        string asstot = string.Empty;
                        foreach (DataRow ddr in dtasstot.Rows)
                        {
                            asstot = ddr["Assessment_Amount"].ToString();
                        }
                        Label lblassamt = (Label)row.FindControl("lblassamt");
                        lblassamt.Text = asstot;

                        if (!string.IsNullOrEmpty(asstot))
                        {
                            Decimal formatasstot = Convert.ToDecimal(asstot);
                            lblassamt.Text = formatasstot.ToString("N2");
                        }

                        DataTable dtstlot = CHNLSVC.Financial.CalSettleTotbyEntry(entry.Text.Trim());
                        string stltot = string.Empty;
                        foreach (DataRow ddr2 in dtstlot.Rows)
                        {
                            stltot = ddr2["Settle_Amount"].ToString();
                        }
                        Label lblsettleamt = (Label)row.FindControl("lblsettleamt");
                        lblsettleamt.Text = stltot;

                        if (!string.IsNullOrEmpty(stltot))
                        {
                            Decimal formatstltot = Convert.ToDecimal(stltot);
                            lblsettleamt.Text = formatstltot.ToString("N2");
                        }
                    }
                }
                lblSetAmt = gventrydetails.HeaderRow.FindControl("lblSetAmt") as Label;
                lblAssAmt = gventrydetails.HeaderRow.FindControl("lblAssAmt") as Label;
                lblSetAmt.Text = "STL Amt (" + _currCode + ")";
                lblAssAmt.Text = "AST Amt (" + _currCode + ")";

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

        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:N2}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }
        protected void txtentryno_TextChanged(object sender, EventArgs e)
        {
            txtastno.Text = string.Empty;
            txtstlno.Text = string.Empty;
            txtcustomref.Text = string.Empty;
            txtassesnoticno.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtentryno.Text))
            {
                LinkButton9_Click(null, null);
            }
        }

        protected void txtastno_TextChanged(object sender, EventArgs e)
        {
            txtentryno.Text = string.Empty;
            txtstlno.Text = string.Empty;
            txtcustomref.Text = string.Empty;
            txtassesnoticno.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtastno.Text))
            {
                LinkButton9_Click(null, null);
            }
        }

        protected void txtstlno_TextChanged(object sender, EventArgs e)
        {
            txtentryno.Text = string.Empty;
            txtastno.Text = string.Empty;
            txtcustomref.Text = string.Empty;
            txtassesnoticno.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtentryno.Text))
            {
                LinkButton9_Click(null, null);
            }
        }

        protected void txtcustomref_TextChanged(object sender, EventArgs e)
        {
            txtentryno.Text = string.Empty;
            txtastno.Text = string.Empty;
            txtstlno.Text = string.Empty;
            txtassesnoticno.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtcustomref.Text))
            {
                LinkButton9_Click(null, null);
            }
        }

        protected void txtassesnoticno_TextChanged(object sender, EventArgs e)
        {
            txtentryno.Text = string.Empty;
            txtastno.Text = string.Empty;
            txtstlno.Text = string.Empty;
            txtcustomref.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtassesnoticno.Text))
            {
                LinkButton9_Click(null, null);
            }
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

        public void BindEntry(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if ((col.ColumnName != "Date") && (col.ColumnName != "Amount") && (col.ColumnName != "Type"))
                {
                    this.ddlSearchbykey.Items.Add(col.ColumnName);
                }
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        public void BindEntry2(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if ((col.ColumnName != "Date") && (col.ColumnName != "Entry No") && (col.ColumnName != "Assessment No"))
                {
                    this.ddlSearchbykey.Items.Add(col.ColumnName);
                }
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        private void LoadSearchPopup(string serType)
        {
            try
            {
                dgvDateSearch.DataSource = new int[] { };
                if (_serData != null)
                {
                    dgvDateSearch.DataSource = _serData;
                    BindDdlData(_serData);
                }
                dgvDateSearch.DataBind();
                txtDateSearch.Text = "";
                txtDateSearch.Focus();
                _serType = serType;
                UserPopoup.Show();
                _showPop = "Show";
                if (dgvDateSearch.PageIndex > 0)
                { dgvDateSearch.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        public void BindDdlData(DataTable _dataSource)
        {
            this.ddlDateSearch.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlDateSearch.Items.Add(col.ColumnName);
            }

            this.ddlDateSearch.SelectedIndex = 0;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                txtFDate.Text = DateTime.Today.AddDays(-7).ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNoSearch);
                _serData = CHNLSVC.Financial.LoadEntryPopUpNew(_para, DateTime.Today.AddDays(-7), DateTime.Today, null, null);
                LoadSearchPopup("EntryNoSearch");
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                txtFDate.Text = DateTime.Today.AddDays(-7).ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessementNoSearch);
                _serData = CHNLSVC.Financial.LoadAssementPopUpNew(_para, DateTime.Today.AddDays(-7), DateTime.Today, null, null);
                LoadSearchPopup("AssessementNoSearch");
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            try
            {
                txtFDate.Text = DateTime.Today.AddDays(-7).ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SettlementNoSearch);
                _serData = CHNLSVC.Financial.LoadSettlementPopUpNew(_para, DateTime.Today.AddDays(-7), DateTime.Today, null, null);
                _serData.Columns.Add("AmountTemp", typeof(string));
                foreach (DataRow item in _serData.Rows)
                {
                    decimal d = Convert.ToDecimal(item["Amount"].ToString());
                    string s = d.ToString("N2");
                    item["AmountTemp"] = s;
                }
                _serData.Columns.Remove("Amount");
                _serData.Columns["AmountTemp"].ColumnName = "Amount";
                LoadSearchPopup("SettlementNoSearch");
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomRefNoSearch);
                DataTable result = CHNLSVC.Financial.LoadCusRefPopUp(SearchParams, null, null);
                if (result != null)
                {
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        if (i > 100)
                        {
                            DataRow dr = result.Rows[i];
                            dr.Delete();
                        }
                    }
                }
                result.AcceptChanges();
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "448";
                BindEntry2(result);
                ViewState["SEARCH"] = result;
                grdResult.PageIndex = 0;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessmentNoticeNoSearch);
                DataTable result = CHNLSVC.Financial.LoadAssNoticePopUp(SearchParams, null, null);
                //if (result.Rows.Count > 0)
                //{
                //    DataView dv = result.DefaultView;
                //    dv.Sort = "Date DESC";
                //    result = dv.ToTable();
                //}
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "449";
                BindEntry2(result);
                ViewState["SEARCH"] = result;
                grdResult.PageIndex = 0;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void grdResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lbtnClosePop_Click(object sender, EventArgs e)
        {
            _showPop = "Hide";
            UserPopoup.Hide();
        }

        protected void lbtnSerByDate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "EntryNoSearch")
                {
                    DateTime dtFrom = Convert.ToDateTime(txtFDate.Text);
                    DateTime dtTo = Convert.ToDateTime(txtTDate.Text);
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNoSearch);
                    _serData = CHNLSVC.Financial.LoadEntryPopUpNew(_para, dtFrom, dtTo, null, null);
                    LoadSearchPopup("EntryNoSearch");
                }
                else if (_serType == "AssessementNoSearch")
                {
                    DateTime dtFrom = Convert.ToDateTime(txtFDate.Text);
                    DateTime dtTo = Convert.ToDateTime(txtTDate.Text);
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessementNoSearch);
                    _serData = CHNLSVC.Financial.LoadAssementPopUpNew(_para, dtFrom, dtTo, null, null);
                    LoadSearchPopup("AssessementNoSearch");
                }
                else if (_serType == "SettlementNoSearch")
                {
                    DateTime dtFrom = Convert.ToDateTime(txtFDate.Text);
                    DateTime dtTo = Convert.ToDateTime(txtTDate.Text);
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SettlementNoSearch);
                    _serData = CHNLSVC.Financial.LoadSettlementPopUpNew(_para, dtFrom, dtTo, null, null);
                    _serData.Columns.Add("AmountTemp", typeof(string));
                    foreach (DataRow item in _serData.Rows)
                    {
                        decimal d = Convert.ToDecimal(item["Amount"].ToString());
                        string s = d.ToString("N2");
                        item["AmountTemp"] = s;
                    }
                    _serData.Columns.Remove("Amount");
                    _serData.Columns["AmountTemp"].ColumnName = "Amount";
                    LoadSearchPopup("SettlementNoSearch");
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtDateSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDateSearch.Text))
            {
                lbtnDateSearch_Click(null, null);
            }
        }

        protected void lbtnDateSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "EntryNoSearch")
                {
                    DateTime dtFrom = Convert.ToDateTime(txtFDate.Text);
                    DateTime dtTo = Convert.ToDateTime(txtTDate.Text);
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNoSearch);
                    _serData = CHNLSVC.Financial.LoadEntryPopUpNew(_para, dtFrom, dtTo, ddlDateSearch.SelectedItem.Text, txtDateSearch.Text);
                    LoadSearchPopup("EntryNoSearch");
                }
                else if (_serType == "AssessementNoSearch")
                {
                    DateTime dtFrom = Convert.ToDateTime(txtFDate.Text);
                    DateTime dtTo = Convert.ToDateTime(txtTDate.Text);
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssessementNoSearch);
                    _serData = CHNLSVC.Financial.LoadAssementPopUpNew(_para, dtFrom, dtTo, ddlDateSearch.SelectedItem.Text, txtDateSearch.Text);
                    LoadSearchPopup("AssessementNoSearch");
                }
                else if (_serType == "SettlementNoSearch")
                {
                    DateTime dtFrom = Convert.ToDateTime(txtFDate.Text);
                    DateTime dtTo = Convert.ToDateTime(txtTDate.Text);
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SettlementNoSearch);
                    _serData = CHNLSVC.Financial.LoadSettlementPopUpNew(_para, dtFrom, dtTo, ddlDateSearch.SelectedItem.Text, txtDateSearch.Text);
                    _serData.Columns.Add("AmountTemp", typeof(string));
                    foreach (DataRow item in _serData.Rows)
                    {
                        decimal d = Convert.ToDecimal(item["Amount"].ToString());
                        string s = d.ToString("N2");
                        item["AmountTemp"] = s;
                    }
                    _serData.Columns.Remove("Amount");
                    _serData.Columns["AmountTemp"].ColumnName = "Amount";
                    LoadSearchPopup("SettlementNoSearch");
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvDateSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvDateSearch.SelectedRow.Cells[1].Text;
                if (_serType == "EntryNoSearch")
                {
                    txtentryno.Text = code;
                    txtentryno_TextChanged(null, null);
                }
                else if (_serType == "AssessementNoSearch")
                {
                    txtastno.Text = code;
                    txtastno_TextChanged(null, null);
                }
                else if (_serType == "SettlementNoSearch")
                {
                    txtstlno.Text = code;
                    txtstlno_TextChanged(null, null);
                }
                UserPopoup.Hide();
                _showPop = "Hide";
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvDateSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvDateSearch.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvDateSearch.DataSource = _serData;
                }
                else
                {
                    dgvDateSearch.DataSource = new int[] { };
                }
                dgvDateSearch.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }
        protected void lbtngrnsearch_Click(object sender, EventArgs e)
        {
            try
            {  var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _itm = (row.FindControl("lbitem") as Label).Text;
                    string entry = txtentryno.Text.ToString();
                    string aodno = (row.FindControl("lblaodoutno") as Label).Text;
                    DataTable grnnumbers = CHNLSVC.Inventory.GetEntryProgGRN(entry, _itm,aodno);
                    int i = 0;
                    string grnno = "";
                    if (grnnumbers.Rows.Count>0)
                    {
                        foreach (var grn in grnnumbers.Rows)
                        {
                            if (grnnumbers.Rows[i]["inb_doc_no"].ToString().Contains("GRN"))
                            {
                                grnno = grnno + grnnumbers.Rows[i]["inb_doc_no"].ToString() + " ,";
                            }
                           
                            i++;
                        }
                    }

                   // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + grnno + "')", true);
                    lb.ToolTip = grnno;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "')", true);
            }

        }
    }
    }
