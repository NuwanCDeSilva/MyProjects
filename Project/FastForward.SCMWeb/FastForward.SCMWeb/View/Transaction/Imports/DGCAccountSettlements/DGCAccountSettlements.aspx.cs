using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Wharf;
using FF.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Imports.DGC_Account_Settlements
{
    public partial class DGCAccountSettlements : BasePage
    {
        List<Int32> lstischecked = new List<Int32>();
        List<Int32> lstischeckedAccounts = new List<Int32>();
        string _userid = string.Empty;
        DataTable uniqueColsEntry = new DataTable();
        List<string> entrylist = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    gvpendingreq.DataSource = new int[] { };
                    gvpendingreq.DataBind();

                    gvDGCAcc.DataSource = new int[] { };
                    gvDGCAcc.DataBind();

                    gvrequestheader.DataSource = new int[] { };
                    gvrequestheader.DataBind();

                    gvreqdetails.DataSource = new int[] { };
                    gvreqdetails.DataBind();

                    gvaod.DataSource = new int[] { };
                    gvaod.DataBind();

                    DateTime orddate = DateTime.Now;
                    dtpFromDate.Text = orddate.ToString("dd/MMM/yyyy");
                    txtfdate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    txttdate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    PopulateInitialData();

                    DataTable dtentries = new DataTable();
                    dtentries.Columns.AddRange(new DataColumn[5] { new DataColumn("isdt_line"), new DataColumn("isth_seq_no"), new DataColumn("isth_doc_no"), new DataColumn("isth_dt"), new DataColumn("isth_stl_amt") });
                    ViewState["SettleEntries"] = dtentries;
                    this.BindGrid();
                    BackDatefucntion();
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16030))
                    //{
                    //    LinkButton1.Enabled = false;
                    //    LinkButton1.CssClass = "buttoncolor";
                    //    LinkButton1.OnClientClick = "return Enable();";
                    //}
                    //else
                    //{
                    //    LinkButton1.Enabled = true;
                    //    LinkButton1.CssClass = "buttonUndocolor";
                    //    LinkButton1.OnClientClick = "ConfirmCancelReq();";
                    //}
                    lbtnassmentdate.Visible = false;



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
        // Back date

        protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, TextBox _dtpcontrol, Label _lblcontrol, string _date, out bool _allowCurrentDate)
        {
            BackDates _bdt = new BackDates();
            _dtpcontrol.Enabled = false;
            _allowCurrentDate = false;

            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
            if (_isAllow == true)
            {
                _dtpcontrol.Enabled = true;
                if (_bdt != null)
                {
                    _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
                    Information.Visible = true;
                    lbtnassmentdate.Visible = true;

                    DateTime Selecteddate = Convert.ToDateTime(dtpFromDate.Text.Trim());
                    DateTime appfromdate = Convert.ToDateTime(_bdt.Gad_act_from_dt);
                    DateTime apptodate = Convert.ToDateTime(_bdt.Gad_act_to_dt);

                    if (_bdt.Gad_alw_curr_trans == true)
                    {
                        if (Selecteddate >= appfromdate && Selecteddate <= apptodate)
                        {
                            Session["WRONGDATERANGE"] = "0";
                        }
                        else
                        {
                            Session["WRONGDATERANGE"] = "1";
                        }
                    }
                    else
                    {
                        if (dtpFromDate.Text == DateTime.Now.Date.ToString())
                        {
                            Session["ALLOWCURDATE"] = "1";
                        }
                        else
                        {
                            Session["ALLOWCURDATE"] = "0";
                        }

                        if (Selecteddate >= appfromdate && Selecteddate <= apptodate)
                        {
                            Session["WRONGDATERANGE"] = "0";
                        }
                        else
                        {
                            Session["WRONGDATERANGE"] = "1";
                        }
                    }
                }
                else
                {
                    lbtnassmentdate.Visible = false;
                }
            }

            if (_bdt == null)
            {
                _allowCurrentDate = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentDate = true;
                }
            }

            CheckSessionIsExpired();
            return _isAllow;
        }
        public void CheckSessionIsExpired()
        {
            if (Session["UserID"].ToString() != "ADMIN")
            {
                string _expMsg = "Current Session is expired or has been closed by administrator!";
                if (CHNLSVC.Security.IsSessionExpired(Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), out _expMsg) == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _expMsg + "');", true);
                    BaseCls.GlbIsExit = true;
                    GC.Collect();
                    return;
                }
            }
        }
        private void BackDatefucntion()
        {
            bool _allowCurrentTrans = false;
            Session["GlbModuleName"] = "m_Trans_Finance_DGC_A_S";
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty, Session["UserDefProf"].ToString(), Session["GlbModuleName"].ToString(), dtpFromDate, lblBackDateInfor, "", out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(dtpFromDate.Text).Date != DateTime.Now.Date)
                    {
                        dtpFromDate.Enabled = true;
                        string _Msg = "Back date not allow for selected date!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        dtpFromDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpFromDate.Enabled = true;
                    string _Msg = "Back date not allow for selected date!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    dtpFromDate.Focus();
                    return;
                }
            }
        }





        protected void BindGrid()
        {
            try
            {
                gvrequestheader.DataSource = (DataTable)ViewState["SettleEntries"];
                gvrequestheader.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        private void PopulateInitialData()
        {
            try
            {
                DataTable dtdgcacc = CHNLSVC.Financial.LoadDGCAccounts();

                string _currCode = "";
                MasterCompany _mstComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                if (_mstComp != null)
                {
                    _currCode = _mstComp.Mc_cur_cd;
                }
                gvDGCAcc.DataSource = null;
                gvDGCAcc.DataBind();

                gvDGCAcc.DataSource = dtdgcacc;
                gvDGCAcc.DataBind();

                ImpAstHeader header = new ImpAstHeader();
                header.ISTH_COM = Session["UserCompanyCode"].ToString();
                header.ISTH_IS_STL = 0;
                DataTable dtasshdr = CHNLSVC.Financial.LoadPendingAssesmentHeaders(header);

                gvpendingreq.DataSource = null;
                gvpendingreq.DataBind();

                gvpendingreq.DataSource = dtasshdr;
                gvpendingreq.DataBind();
                //com curr


                Label lblCurrCostCode = gvpendingreq.HeaderRow.FindControl("lblCurrCostCode") as Label;
                lblCurrCostCode.Text = "Amount (" + _currCode + ")";

                Label UtilityAmount = gvDGCAcc.HeaderRow.FindControl("UtilityAmount") as Label;
                UtilityAmount.Text = "Utility Amount (" + _currCode + ")";

                Label Balance = gvDGCAcc.HeaderRow.FindControl("Balance") as Label;
                Balance.Text = "Balance (" + _currCode + ")";

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

        protected void lbtnaddentry_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvpendingreq.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please load the pending requests !!!')", true);
                    return;
                }

                foreach (GridViewRow gvrow in gvpendingreq.Rows)
                {
                    CheckBox chkselect = (CheckBox)gvrow.FindControl("chkselect");
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
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the pending request (s) !!!')", true);
                    gvpendingreq.Focus();
                    return;
                }

                foreach (GridViewRow gvrow1 in gvDGCAcc.Rows)
                {
                    CheckBox chkselectacc = (CheckBox)gvrow1.FindControl("chlselectacc");
                    if (chkselectacc.Checked == true)
                    {
                        lstischeckedAccounts.Add(1);
                    }
                    else
                    {
                        lstischeckedAccounts.Add(0);
                    }
                }

                if (!lstischeckedAccounts.Contains(1))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a DGC Account !!!')", true);
                    gvDGCAcc.Focus();
                    return;
                }

                Int32 count = 0;
                foreach (GridViewRow rowCOunt in this.gvDGCAcc.Rows)
                {
                    CheckBox chkId = (rowCOunt.FindControl("chlselectacc") as CheckBox);
                    if (chkId.Checked)
                    {
                        count++;
                    }
                }

                if (count > 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to select multiple DGC accounts !!!')", true);
                    gvDGCAcc.Focus();
                    return;
                }

                DataTable dt = (DataTable)ViewState["SettleEntries"];

                foreach (GridViewRow gvrow in gvpendingreq.Rows)
                {
                    Int32 gridline = gvrow.RowIndex + 1;
                    CheckBox chkselect = (CheckBox)gvrow.FindControl("chkselect");
                    Label seqno = (Label)gvrow.FindControl("lblseqno");
                    Label assno = (Label)gvrow.FindControl("lblassreqno1");
                    Label assdate = (Label)gvrow.FindControl("lbldate1");
                    Label assamt = (Label)gvrow.FindControl("lblamount1");

                    if (chkselect.Checked == true)
                    {
                        dt.Rows.Add(gridline, seqno.Text, assno.Text.Trim(), assdate.Text.Trim(), assamt.Text.Trim());
                    }
                }

                uniqueColsEntry = RemoveDuplicateRows(dt, "isth_doc_no");

                gvrequestheader.DataSource = null;
                gvrequestheader.DataBind();

                gvrequestheader.DataSource = uniqueColsEntry;
                gvrequestheader.DataBind();

                //com curr
                string _currCode = "";
                MasterCompany _mstComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                if (_mstComp != null)
                {
                    _currCode = _mstComp.Mc_cur_cd;
                }

                Label Total = gvrequestheader.HeaderRow.FindControl("Total") as Label;
                Total.Text = "Total (" + _currCode + ")";

                Currcom1.Text = "(" + _currCode + ")";
                Currcom2.Text = "(" + _currCode + ")";

                ViewState["SettleEntries"] = uniqueColsEntry;
                CalCulateAssementTot();

                foreach (GridViewRow rowfinalbalance in this.gvDGCAcc.Rows)
                {
                    CheckBox chkId = (rowfinalbalance.FindControl("chlselectacc") as CheckBox);
                    Label lblbalance = (Label)rowfinalbalance.FindControl("lblbalancedgc");
                    if (chkId.Checked)
                    {
                        Decimal totbalance = Convert.ToDecimal(lblbalance.Text.Trim());
                        txfinalbalance.Text = DoFormat(totbalance);
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

        private void CalCulateAssementTot()
        {
            try
            {
                Decimal asstot = 0;

                foreach (GridViewRow gvrow in gvrequestheader.Rows)
                {
                    Label lblassamount = (Label)gvrow.FindControl("lbltot");
                    if (!string.IsNullOrEmpty(lblassamount.Text.Trim()))
                    {
                        asstot += Math.Round(Convert.ToDecimal(lblassamount.Text.Trim()), 2);
                    }
                }
                decimal amm = Convert.ToDecimal(DoFormat(asstot));
                txtsettleamount.Text = string.Format("{0:n2}", amm);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName1)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if ((hTable.Contains(drow[colName1])))
                {
                    duplicateList.Add(drow);
                }
                else
                {
                    hTable.Add(drow[colName1], string.Empty);
                }
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
            {
                dTable.Rows.Remove(dRow);
            }
            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        private void Clear()
        {
            try
            {
                ViewState["SettleEntries"] = null;
                ViewState["SettleEntryCancel"] = null;

                lstischecked.Clear();
                lstischeckedAccounts.Clear();

                Session["SOSEQNO"] = null;

                gvrequestheader.DataSource = new int[] { };
                gvrequestheader.DataBind();

                gvreqdetails.DataSource = new int[] { };
                gvreqdetails.DataBind();

                gvaod.DataSource = new int[] { };
                gvaod.DataBind();

                DateTime orddate = DateTime.Now;
                dtpFromDate.Text = orddate.ToString("dd/MMM/yyyy");

                PopulateInitialData();
                chkdealer.Checked = false;
                txtsettlemntno.Text = string.Empty;
                txtsettleamount.Text = string.Empty;
                txfinalbalance.Text = string.Empty;

                DataTable dtentries = new DataTable();
                dtentries.Columns.AddRange(new DataColumn[5] { new DataColumn("isdt_line"), new DataColumn("isth_seq_no"), new DataColumn("isth_doc_no"), new DataColumn("isth_dt"), new DataColumn("isth_stl_amt") });
                ViewState["SettleEntries"] = dtentries;
                this.BindGrid();

                lbtnsave.Enabled = true;
                lbtnsave.CssClass = "buttonUndocolor";
                lbtnsave.OnClientClick = "ConfirmSave();";

                lbtnaddentry.Enabled = true;
                lbtnaddentry.CssClass = "buttonUndocolor";

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16030))
                {
                    LinkButton1.Enabled = false;
                    LinkButton1.CssClass = "buttoncolor";
                    LinkButton1.OnClientClick = "return Enable();";
                }
                else
                {
                    LinkButton1.Enabled = true;
                    LinkButton1.CssClass = "buttonUndocolor";
                    LinkButton1.OnClientClick = "ConfirmCancelReq();";
                }
                entrylist.Clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbuttonclear_Click(object sender, EventArgs e)
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
        }

        protected void gvrequestheader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string docNo = (gvrequestheader.SelectedRow.FindControl("lblassreqno2") as Label).Text;
                DataTable dtalldetails = CHNLSVC.Financial.LoadAllEntryDetails(docNo, Session["UserCompanyCode"].ToString());
                gvreqdetails.DataSource = null;
                gvreqdetails.DataBind();

                gvreqdetails.DataSource = dtalldetails;
                gvreqdetails.DataBind();

                //com curr
                string _currCode = "";
                MasterCompany _mstComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                if (_mstComp != null)
                {
                    _currCode = _mstComp.Mc_cur_cd;
                }

                Label Amount2 = gvreqdetails.HeaderRow.FindControl("Amount2") as Label;
                Amount2.Text = "Amount (" + _currCode + ")";
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

        protected void lbtnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtsaveconfirm.Value == "Yes")
                {
                    if (gvrequestheader.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the assessment details !!!')", true);
                        return;
                    }

                    DateTime settledate = Convert.ToDateTime(dtpFromDate.Text.Trim());
                    DateTime todaydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    if (settledate < todaydate)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Date cannot be back dated !!!')", true);
                        lbtnassmentdate.Focus();
                        return;
                    }

                    if (settledate > todaydate)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Date cannot be a future date !!!')", true);
                        lbtnassmentdate.Focus();
                        return;
                    }

                    _userid = (string)Session["UserID"];

                    MasterAutoNumber mastAutoNo = new MasterAutoNumber();


                    if (chkdealer.Checked == true)
                    {
                        mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                        mastAutoNo.Aut_cate_tp = "SDF";
                        mastAutoNo.Aut_direction = 0;
                        mastAutoNo.Aut_moduleid = "SDF";
                        mastAutoNo.Aut_start_char = "SDF";
                        mastAutoNo.Aut_year = DateTime.Now.Year;
                    }
                    else
                    {
                        mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                        mastAutoNo.Aut_cate_tp = "STL";
                        mastAutoNo.Aut_direction = 0;
                        mastAutoNo.Aut_moduleid = "STL";
                        mastAutoNo.Aut_start_char = "STL";
                        mastAutoNo.Aut_year = DateTime.Now.Year;
                    }

                    ImportsSettleHeader SettleHeader = new ImportsSettleHeader();

                    string seqnofororder = string.Empty;

                    if (Session["SOSEQNO"] != null)
                    {
                        seqnofororder = (string)Session["SOSEQNO"].ToString();
                    }

                    if (string.IsNullOrEmpty(seqnofororder))
                    {
                        seqnofororder = "0";
                    }

                    Decimal usedamt = 0;
                    string accno = string.Empty;
                    foreach (GridViewRow rowacc in this.gvDGCAcc.Rows)
                    {
                        CheckBox chkselectacc = (rowacc.FindControl("chlselectacc") as CheckBox);
                        Label lblutilamt = (rowacc.FindControl("lblutilamt") as Label);
                        Label lblaccno = (rowacc.FindControl("lblaccno") as Label);

                        if (chkselectacc.Checked)
                        {
                            usedamt = Convert.ToDecimal(lblutilamt.Text.Trim());
                            accno = lblaccno.Text;
                        }
                    }

                    SettleHeader.ISHD_SEQ_NO = Convert.ToInt32(seqnofororder);
                    SettleHeader.ISHD_DOC_NO = txtsettlemntno.Text.Trim();
                    SettleHeader.ISHD_COM = Session["UserCompanyCode"].ToString();
                    SettleHeader.ISHD_STL_DT = Convert.ToDateTime(DateTime.Now.Date);
                    SettleHeader.ISHD_STL_AMT = Convert.ToDecimal(txtsettleamount.Text);
                    SettleHeader.ISHD_USED_AMT = Convert.ToDecimal(usedamt);
                    SettleHeader.ISHD_STUS = "A";
                    SettleHeader.ISHD_CRE_BY = _userid;
                    SettleHeader.ISHD_CRE_DT = Convert.ToDateTime(DateTime.Now.Date);
                    SettleHeader.ISHD_CRE_SESSION = Session["SessionID"].ToString();

                    ImportsSettleDetails SettleDetails = new ImportsSettleDetails();
                    ImpAstHeader AssHeader = new ImpAstHeader();
                    DGCAccounts Accounts = new DGCAccounts();
                    DataTable dtAssessements = (DataTable)ViewState["SettleEntries"];
                    ImpCusdecHdr CusDecHeader = new ImpCusdecHdr();

                    foreach (GridViewRow myrow in gvrequestheader.Rows)
                    {
                        Label lblentry = (Label)myrow.FindControl("lblassreqno2");
                        entrylist.Add(lblentry.Text);
                    }


                    string filtervalues2 = String.Join(",", entrylist.Where(x => x != null).Select(x => x.ToString()));

                    DataTable dtCusDec = CHNLSVC.Financial.LoadSelectedEntryNos(filtervalues2);

                    Tuple<int, string> outopno = CHNLSVC.Financial.SaveSettleMentsHeader(SettleHeader, mastAutoNo, SettleDetails, AssHeader, Accounts, dtAssessements, dtAssessements, Session["UserCompanyCode"].ToString(), accno, Convert.ToDecimal(txtsettleamount.Text.Trim()), CusDecHeader, dtCusDec, _userid, Session["SessionID"].ToString());

                    string newseqno = string.Empty;
                    string outputopno = string.Empty;

                    if (string.IsNullOrEmpty(txtsettlemntno.Text.Trim()))
                    {
                        newseqno = outopno.Item1.ToString();
                        outputopno = outopno.Item2.ToString();
                    }
                    else
                    {
                        newseqno = seqnofororder;
                        outputopno = txtsettlemntno.Text.Trim();
                    }

                    if (Convert.ToInt32(newseqno) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully saved document " + outputopno + " !!!')", true);
                        Clear();
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (txtcancel.Value == "Yes")
            {
                try
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16030))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Sorry, You have no permission for settlement cancellation! - ( Advice: Required permission code : 16012)')", true);
                        return;
                    }


                    string ordstatus = (string)Session["STUS"];
                    _userid = (string)Session["UserID"];

                    Int32 cancelesult = 0;

                    if (string.IsNullOrEmpty(txtsettlemntno.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a settlement number !!!')", true);
                        lbtnsettlemnt.Focus();
                        return;
                    }

                    ImportsSettleHeader SettleHeader = new ImportsSettleHeader();
                    ImpAstHeader AssHeader = new ImpAstHeader();

                    DataTable dtAssessements = (DataTable)ViewState["SettleEntryCancel"];

                    SettleHeader.ISHD_STUS = "C";
                    SettleHeader.ISHD_CNCL_BY = _userid;
                    SettleHeader.ISHD_CNCL_DT = Convert.ToDateTime(DateTime.Now.Date);
                    SettleHeader.ISHD_CNCL_SESSION = Session["SessionID"].ToString();
                    SettleHeader.ISHD_DOC_NO = txtsettlemntno.Text.Trim();
                    SettleHeader.ISHD_COM = Session["UserCompanyCode"].ToString();

                    cancelesult = CHNLSVC.Financial.CancelSettlement(SettleHeader, AssHeader, dtAssessements, Session["UserCompanyCode"].ToString());

                    if (cancelesult > 0)
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

        public void BindUCtrlDDLDataOriginal(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SettlementDocs:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void lbtnsettlemnt_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SettlementDocs);
                DataTable result = CHNLSVC.Financial.LoadSavedSettlementsPopUp(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "439";
                BindUCtrlDDLDataOriginal(result);
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

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "439")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SettlementDocs);
                    DataTable result = CHNLSVC.Financial.LoadSavedSettlementsPopUp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "439";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "")
                {

                }

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
                if (lblvalue.Text == "439")
                {
                    string bondno = grdResult.SelectedRow.Cells[1].Text;
                    txtsettlemntno.Text = bondno;
                    DataTable dtsavedsettlements = CHNLSVC.Financial.LoadSavedSettlemets(bondno, Session["UserCompanyCode"].ToString());
                    gvrequestheader.DataSource = null;
                    gvrequestheader.DataBind();
                    lbtnassmentdate.Visible = false;
                    gvrequestheader.DataSource = dtsavedsettlements;
                    gvrequestheader.DataBind();

                    ViewState["SettleEntryCancel"] = dtsavedsettlements;

                    foreach (DataRow ddr in dtsavedsettlements.Rows)
                    {
                        Session["STUS"] = ddr["ishd_stus"];
                    }

                    string stus = (string)Session["STUS"];

                    lbtnsave.Enabled = false;
                    lbtnsave.CssClass = "buttoncolor";
                    lbtnsave.OnClientClick = "return Enable();";

                    lbtnaddentry.Enabled = false;
                    lbtnaddentry.CssClass = "buttoncolor";
                    lbtnaddentry.OnClientClick = "return Enable();";

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16030))
                    {
                        LinkButton1.Enabled = false;
                        LinkButton1.CssClass = "buttoncolor";
                        LinkButton1.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        LinkButton1.Enabled = true;
                        LinkButton1.CssClass = "buttonUndocolor";
                        LinkButton1.OnClientClick = "ConfirmCancelReq();";
                    }

                    if (stus == "C")
                    {
                        LinkButton1.Enabled = false;
                        LinkButton1.CssClass = "buttoncolor";
                        LinkButton1.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        LinkButton1.Enabled = true;
                        LinkButton1.CssClass = "buttonUndocolor";
                        LinkButton1.OnClientClick = "ConfirmCancelReq();";
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "")
                {

                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
            }
            catch
            {

            }
        }

        protected void gvreqdetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Label docNo = (gvreqdetails.SelectedRow.FindControl("lblbondno") as Label);
                DataTable dtaod = CHNLSVC.Financial.LoadAODDocs(docNo.Text.ToString(), Session["UserCompanyCode"].ToString(), "AOD");

                gvaod.DataSource = null;
                gvaod.DataBind();

                gvaod.DataSource = dtaod;
                gvaod.DataBind();
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

        protected void lbtnprint_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                if (radprint.Checked)
                {
                  
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    string docno = txtsettlemntno.Text.ToString();
                    if (docno == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Assessment No')", true);
                        return;
                    }
                    else
                    {
                        docno = txtsettlemntno.Text.ToString();
                        string com = Session["UserCompanyCode"].ToString();
                        string loc = Session["UserDefLoca"].ToString();
                        string user = Session["UserID"].ToString();
                        Session["GlbReportName"] = "ASTAccountRep.rpt";
                        BaseCls.GlbReportHeading = "AST Sheet32";
                        clswharf obj = new clswharf();
                        obj.CusdecAssessment3(docno, loc, com, user);
                        if (chkexcel.Checked)
                        {
                            targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".xls";
                        }

                        PrintPDF(targetFileName, obj._cusdec_assessment3);

                        if (chkexcel.Checked == false)
                        {
                            url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                        }
                        if (chkexcel.Checked)
                        {
                            url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xls','_blank');</script>";
                        }



                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        CHNLSVC.MsgPortal.SaveReportErrorLog("CusdecAssesdgc", "CusdecAsses", "Run Ok", Session["UserID"].ToString());

                    }

                }
                else
                {
                    //string out1="";
                    DateTime fdate = Convert.ToDateTime(txtfdate.Text.ToString());
                    DateTime tdate = Convert.ToDateTime(txttdate.Text.ToString());

                    //DataTable exceldata = CHNLSVC.Financial.GenAssExel(fdate, tdate);
                    //exceldata.Columns.Add("istd_cost_claim_amt", typeof(decimal));
                    //exceldata.Columns.Add("istd_cost_unclaim_amt", typeof(decimal));
                    //exceldata.Columns.Add("istd_diff_amt", typeof(decimal));
                    //if (exceldata.Rows.Count>0)
                    //{
                    //    int i = 0;
                    //    foreach (var exceldata_nw in exceldata.Rows)
                    //    {
                    //        decimal clam = 0;
                    //        decimal unclaim = 0;
                    //        decimal diff = 0;
                    //        DataTable assdata = CHNLSVC.CustService.GetCusdecAssessmentAccountData(exceldata.Rows[i]["STL_NO"].ToString(), Session["UserCompanyCode"].ToString());
                    //        int j = 0;
                    //        foreach (var assdata_nw in assdata.Rows)
                    //        {
                    //            if (assdata.Rows[j]["ISTD_ENTRY_NO"].ToString() == exceldata.Rows[i]["ENTRY_NO"].ToString())
                    //            {
                    //                clam = clam + Convert.ToDecimal(assdata.Rows[j]["ISTD_COST_CLAIM_AMT"].ToString());
                    //                unclaim = unclaim + Convert.ToDecimal(assdata.Rows[j]["ISTD_COST_UNCLAIM_AMT"].ToString());
                    //                diff = diff + Convert.ToDecimal(assdata.Rows[j]["ISTD_DIFF_AMT"].ToString());
                    //            }
                    //            j++;
                    //        }
                    //        exceldata.Rows[i]["istd_cost_claim_amt"] = clam;
                    //        exceldata.Rows[i]["istd_cost_unclaim_amt"] = unclaim;
                    //        exceldata.Rows[i]["istd_diff_amt"] = diff;
                    //        i++;
                    //    }


                       
                    //}
                    //string path = CHNLSVC.MsgPortal.ExportExcel2007(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), exceldata, out out1);

                    ////Service Function

                    string path = CHNLSVC.Financial.DGCDAtaExcel(fdate, tdate, Session["UserCompanyCode"].ToString(), Session["UserID"].ToString());

                    _copytoLocal(path);
                  //  Process p = new Process();
                  //  p.StartInfo = new ProcessStartInfo(path);
                   // p.Start();
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xlsx','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    // string targetFileName = "";
                   // targetFileName= Server.MapPath("~\\Temp\\") + "ASSMNT" + ".xls";
                   // ExportToExcel(exceldata, targetFileName);
                    //url = "<script>window.open('C:/SUN/" + "CRCD" + ".xls','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    //string url = "<script>window.open('/Temp/" + "ASSMNT" + ".xls','_blank');</script>";
                  // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    //Process p = new Process();
                    //p.StartInfo = new ProcessStartInfo("/Temp/ASSMNT.xls");
                    //p.Start();

                }



                


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('"+ex.Message+"')", true);
                CHNLSVC.MsgPortal.SaveReportErrorLog("DGC Account Settlements Print", "DGCAccountSettlements", ex.Message, Session["UserID"].ToString());
                return;
            }
        }
        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                string targetFileName = Server.MapPath("~\\Temp\\") + filenamenew + ".xlsx";
                System.IO.File.Copy(@"" + _filePath, "C:/Download_excel/" + filenamenew + ".xlsx", true);
                System.IO.File.Copy(@"" + _filePath, targetFileName, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + "This file does not exist." + "')", true);
            }
        }
        private void ExportToExcel(DataTable Tbl, string ExcelFilePath = null)
        {
            string name = "C:/SUN" + "/ACCERR.txt";
              FileInfo info = new FileInfo(name);
              if (info.Exists || !info.Exists)
              {
                  using (StreamWriter writer = info.CreateText())
                  {
                      try
                      {


                          if (Tbl == null || Tbl.Columns.Count == 0)
                              throw new Exception("ExportToExcel: Null or empty input table!\n");

                          // load excel, and create a new workbook
                          Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                          excelApp.Workbooks.Add();

                          // single worksheet
                          Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                          // column headings
                          for (int i = 0; i < Tbl.Columns.Count; i++)
                          {
                              workSheet.Cells[1, (i + 1)] = Tbl.Columns[i].ColumnName;
                          }

                          // rows
                          for (int i = 0; i < Tbl.Rows.Count; i++)
                          {
                              // to do: format datetime values before printing
                              for (int j = 0; j < Tbl.Columns.Count; j++)
                              {
                                  workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                              }
                          }

                          // check fielpath
                          if (ExcelFilePath != null && ExcelFilePath != "")
                          {
                              try
                              {
                                  //DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                                  //diskOpts.DiskFileName = ExcelFilePath;
                                  if (File.Exists(ExcelFilePath))
                                  {
                                      File.Delete(ExcelFilePath);
                                  }
                                  workSheet.SaveAs(ExcelFilePath);
                                 // workSheet.ExportAsFixedFormat = diskOpts;
                                  excelApp.Quit();
                                  // MessageBox.Show("Excel file saved!");
                              }
                              catch (Exception ex)
                              {
                                  //throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                  //+ ex.Message);
                                  writer.WriteLine(ex.Message);
                              }
                          }
                          else // no filepath is given
                          {
                              excelApp.Visible = true;
                          }
                      }
                      catch (Exception ex)
                      {
                          //throw new Exception("ExportToExcel: \n" + ex.Message);
                          writer.WriteLine(ex.Message);

                      }
                  }
              }
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

                if (chkexcel.Checked)
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.Excel;
                }
                else
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                }
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                /*
                 * 
                 *  if (rbpdf.Checked)
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                }
                if (rbexel.Checked)
                {
                    
                }
                 */




                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error')", true);
                return;
            }
        }

        protected void chkdealer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdealer.Checked == true)
                {
                    DataTable dtdgcacc = CHNLSVC.Financial.LoadDGCAccounts();

                    string _currCode = "";
                    MasterCompany _mstComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                    if (_mstComp != null)
                    {
                        _currCode = _mstComp.Mc_cur_cd;
                    }
                    gvDGCAcc.DataSource = null;
                    gvDGCAcc.DataBind();

                    gvDGCAcc.DataSource = dtdgcacc;
                    gvDGCAcc.DataBind();

                    ImpAstHeader header = new ImpAstHeader();
                    header.ISTH_COM = Session["UserCompanyCode"].ToString();
                    header.ISTH_IS_STL = 0;
                    DataTable dtasshdr = CHNLSVC.Financial.LoadPendingAssesmentHeaders(header);
                    DataView dv = new DataView(dtasshdr);
                    dv.RowFilter = " isth_tp=1";
                    DataTable sortedDT = dv.ToTable();
                    gvpendingreq.DataSource = null;
                    gvpendingreq.DataBind();

                    gvpendingreq.DataSource = sortedDT;
                    gvpendingreq.DataBind();
                    //com curr


                    Label lblCurrCostCode = gvpendingreq.HeaderRow.FindControl("lblCurrCostCode") as Label;
                    lblCurrCostCode.Text = "Amount (" + _currCode + ")";

                    Label UtilityAmount = gvDGCAcc.HeaderRow.FindControl("UtilityAmount") as Label;
                    UtilityAmount.Text = "Utility Amount (" + _currCode + ")";

                    Label Balance = gvDGCAcc.HeaderRow.FindControl("Balance") as Label;
                    Balance.Text = "Balance (" + _currCode + ")";
                }
                else
                {
                    DataTable dtdgcacc = CHNLSVC.Financial.LoadDGCAccounts();

                    string _currCode = "";
                    MasterCompany _mstComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                    if (_mstComp != null)
                    {
                        _currCode = _mstComp.Mc_cur_cd;
                    }
                    gvDGCAcc.DataSource = null;
                    gvDGCAcc.DataBind();

                    gvDGCAcc.DataSource = dtdgcacc;
                    gvDGCAcc.DataBind();

                    ImpAstHeader header = new ImpAstHeader();
                    header.ISTH_COM = Session["UserCompanyCode"].ToString();
                    header.ISTH_IS_STL = 0;
                    DataTable dtasshdr = CHNLSVC.Financial.LoadPendingAssesmentHeaders(header);
                    gvpendingreq.DataSource = null;
                    gvpendingreq.DataBind();

                    gvpendingreq.DataSource = dtasshdr;
                    gvpendingreq.DataBind();
                    //com curr
                    Label lblCurrCostCode = gvpendingreq.HeaderRow.FindControl("lblCurrCostCode") as Label;
                    lblCurrCostCode.Text = "Amount (" + _currCode + ")";

                    Label UtilityAmount = gvDGCAcc.HeaderRow.FindControl("UtilityAmount") as Label;
                    UtilityAmount.Text = "Utility Amount (" + _currCode + ")";

                    Label Balance = gvDGCAcc.HeaderRow.FindControl("Balance") as Label;
                    Balance.Text = "Balance (" + _currCode + ")";
                }


            }
            catch (Exception ex)
            {

            }
        }

    }
}