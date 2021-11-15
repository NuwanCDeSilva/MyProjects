using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Inventory;

namespace FastForward.SCMWeb.View.Transaction.Finance
{
    public partial class CreditCardAcknow : BasePage
    {
        private List<SAT_ADJ_CRCD> _CreditNotes = new List<SAT_ADJ_CRCD>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdInvoices.DataSource = _CreditNotes;
                grdInvoices.DataBind();
                txtSDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                if (Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {

            }
        }

        protected void btncrddoc_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CrcdCirc);
                DataTable result = CHNLSVC.CommonSearch.GetCrCdCircular(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "401";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                txtSearchbyword.Focus();
                txtcrddoc.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message+ "');", true);
            }
        }

        protected void btnbnkcd_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable _result = CHNLSVC.CommonSearch.SearchBank(SearchParams);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "21";
            BindUCtrlDDLData(_result);
            UserPopup.Show();
            ViewState["SEARCH"] = _result;
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {

                //if(txtdraccount.Text=="" || txtcraccount.Text=="")
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter CR/DR Account" + "');", true);
                //    return;
                //}

                //if (txtmid.Text=="")
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter MID" + "');", true);
                //    return;
                //}
                //bool _issun = CHNLSVC.Financial.IsSunAcc(txtdraccount.Text, Session["UserCompanyCode"].ToString());
                //if (_issun==false)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid DR Account" + "');", true);
                //    return;
                //}
                //_issun = CHNLSVC.Financial.IsSunAcc(txtcraccount.Text, Session["UserCompanyCode"].ToString());
                //if (_issun == false)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid CR Account" + "');", true);
                //    return;
                //}

                string _docnum = "";
                int effect = 0;
                //Auto Number
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = 1;
                mastAutoNo.Aut_moduleid = Session["UserDefProf"].ToString();
                mastAutoNo.Aut_start_char = "CRD";
                mastAutoNo.Aut_year = DateTime.Now.Year;

                //ADJ HDR
                //SAT_ADJ_CRCD _CRDATA = new SAT_ADJ_CRCD();
                //_CRDATA.staj_seq = CHNLSVC.Inventory.GetSerialID();
                //_CRDATA.staj_dt = Convert.ToDateTime(txtSDate.Text).Date;
                //_CRDATA.staj_com = Session["UserCompanyCode"].ToString();
                //_CRDATA.staj_pc = Session["UserDefProf"].ToString();
                //_CRDATA.staj_direct = 1;
                //_CRDATA.staj_tp = "CRDRCVACKW";
                //_CRDATA.staj_sub_tp = "COM";
                //_CRDATA.staj_amt = Convert.ToDecimal(txtamount.Text.ToString());
                //_CRDATA.staj_acc_dbt = txtdraccount.Text.ToString();
                //_CRDATA.staj_acc_crd = txtcraccount.Text.ToString();
                //_CRDATA.staj_rmk = txtremark.Text.ToString();
                //_CRDATA.staj_ref = txtcrddoc.Text.ToString();
                //_CRDATA.staj_seq = CHNLSVC.Inventory.GetSerialID();
                //_CRDATA.staj_cre_by = Session["UserID"].ToString();
                //_CRDATA.staj_cre_dt = DateTime.Now.Date;
                //_CRDATA.staj_bankcd = txtbankcd.Text.ToString();
                //_CRDATA.staj_midno = txtmid.Text.ToString();
                //_CRDATA.staj_is_upload = 0;

                //_CRDATA.staj_state_date = Convert.ToDateTime(txtSDate.Text);
                _CreditNotes = Session["StoreCN"] as List<SAT_ADJ_CRCD>;
                //save
                effect = CHNLSVC.Financial.SaveCredCardAchknow_new(_CreditNotes, mastAutoNo, out _docnum);
                if (effect == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + " Successfull Saved :" + _docnum + "');", true);
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + " Error:" + _docnum + "');", true);
                }

                //bool istoday = CHNLSVC.Financial.IsTodayStatemant(_CRDATA.staj_com, _CRDATA.staj_pc, _CRDATA.staj_state_date, _CRDATA.staj_bankcd);

                //if (istoday)
                //{
                //    lblMssg.Text = "Record is found for " + _CRDATA.staj_pc + " profit center, " + _CRDATA.staj_bankcd + " bank and "+ _CRDATA.staj_state_date +" statement date. Please confirmed to proceed.";
                //    PopupConfBox.Show();
                //}
                //else
                //{
                //    //save
                //    effect = CHNLSVC.Financial.SaveCredCardAchknow(_CRDATA, mastAutoNo, out _docnum);
                //    if (effect == 1)
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + " Successfull Saved :" + _docnum + "');", true);
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + " Error:" + _docnum + "');", true);
                //    }
                //}             
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                return;
            }
        }


        protected void btnupload_Click(object sender, EventArgs e)
        {
            try
            {

            }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                return;
            }
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportName"] = "CreditCardPayReceipt.rpt";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                
                //TO DO
            //    DateTime _fromdate = Convert.ToDateTime(txtSDate.Text);
            //    DateTime _todate = Convert.ToDateTime(txtSDate.Text);

                DateTime _fromdate = Convert.ToDateTime(txtSDate.Text);
                DateTime _todate = Convert.ToDateTime(txtSDate.Text);
                string bank = txtbankcd.Text;
                string mid = txtmid.Text;
                string reciptno = txtcrddoc.Text;

                clsInventory obj = new clsInventory();
               // obj.CreditCardPayPrint(Session["UserCompanyCode"].ToString(), "RAMP", _fromdate, _todate, bank, mid, reciptno, Session["UserID"].ToString());

                obj.CreditCardPayPrint(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _fromdate, _todate, bank,mid, reciptno, Session["UserID"].ToString() );
                PrintPDF(targetFileName, obj._creditcardPaydoc);

                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
            }
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
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
                throw ex;
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.CrcdCirc:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TradeTerms:
                    {
                        paramsText.Append("TOT" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("BANK" + seperator);
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
                if (lblvalue.Text == "401")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CrcdCirc);
                    DataTable result = CHNLSVC.CommonSearch.GetCrCdCircular(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "401";
                    ViewState["SEARCH"] = result;
                    UserPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
            }
        }
        private void FilterData1()
        {
           DataTable _result = (DataTable)ViewState["SEARCH"];
            DataView dv = new DataView(_result);
            string searchParameter = ddlSearchbykey.Text;
            dv.RowFilter = "" + ddlSearchbykey.Text + " LIKE '%" + txtSearchbyword.Text + "%'";
            // dv.RowFilter = "REFERENCESNO = '" + txtSearchbyword.Text + "' ";
            if (dv.Count > 0)
            {
                _result = dv.ToTable();
            }

            grdResult.DataSource = _result;
            grdResult.DataBind();
            UserPopup.Show();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (lblvalue.Text == "21")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.SearchBank(SearchParams);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopup.Show();
                return;
            }
            else if (lblvalue.Text == "401")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CrcdCirc);
                DataTable _result = CHNLSVC.CommonSearch.GetCrCdCircular(SearchParams,null,null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopup.Show();
                return;
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PID = grdResult.SelectedRow.Cells[1].Text;
          
            if (lblvalue.Text == "21")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                txtbankcd.Text = PID;
                txtbankcd.ToolTip = Name;
                //bankDetails();
                lblvalue.Text = "";
            }
            if (lblvalue.Text == "401")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                txtcrddoc.Text = PID;
                Loaddata(PID);
                lblvalue.Text = "";
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "21")
            {
                FilterData1();
            }
            if (lblvalue.Text == "401")
            {
                FilterData();
            }
        }

        private void Loaddata(string _doc)
        {
            List<SAT_ADJ_CRCD> _adjlist = CHNLSVC.Financial.GetAdjDetByCirc(Session["UserCompanyCode"].ToString(), _doc, "CRDRCVACKW");
            if (_adjlist != null && _adjlist.Count>0)
            {
                //txtSDate.Text = _adjlist.First().staj_state_date.ToString("dd/MMM/yyyy");
                //txtamount.Text = _adjlist.First().staj_amt.ToString();
                //txtcraccount.Text = _adjlist.First().staj_acc_crd;
                //txtdraccount.Text = _adjlist.First().staj_acc_dbt;
                //txtremark.Text = _adjlist.First().staj_rmk;
                //txtmid.Text = _adjlist.First().staj_midno;
                //txtbankcd.Text = _adjlist.First().staj_bankcd;
                grdInvoices.DataSource = _adjlist;
                grdInvoices.DataBind();
                //ViewState["StoreCN"] = _adjlist;
                //_adjlist = null;
            }
            else
            {

            }
        }

        protected void txtSDate_TextChanged(object sender, EventArgs e)
        {
            if(Convert.ToDateTime(txtSDate.Text).Date>DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" +"Cannot Exceed Current Date" +"');", true);
                txtSDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                return;
            }
        }

        protected void txtbankcd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.SearchBank(SearchParams);
                DataTable tblFiltered = _result.AsEnumerable()
                                             .Where(r => r.Field<string>("CODE") == txtbankcd.Text)
                                             .CopyToDataTable();
                if (tblFiltered == null || tblFiltered.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Select Valid Bank" + "');", true);
                    txtbankcd.Text = "";
                    return;
                }
            }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Select Valid Bank" + "');", true);
                txtbankcd.Text = "";
                return;
            }
           
        }

        protected void txtcraccount_TextChanged(object sender, EventArgs e)
        {

           bool _issun = CHNLSVC.Financial.IsSunAcc(txtcraccount.Text, Session["UserCompanyCode"].ToString());
            if (_issun == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid CR Account" + "');", true);
                txtcraccount.Text = "";
                return;
            }
        }

        protected void txtdraccount_TextChanged(object sender, EventArgs e)
        {
            bool _issun = CHNLSVC.Financial.IsSunAcc(txtdraccount.Text, Session["UserCompanyCode"].ToString());
            if (_issun == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid DR Account" + "');", true);
                txtdraccount.Text = "";
                return;
            }
        }

        protected void btnalertYes_Click(object sender, EventArgs e)
        {
            try
            {
                string _docnum = "";
                int effect = 0;
                //Auto Number
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = 1;
                mastAutoNo.Aut_moduleid = Session["UserDefProf"].ToString();
                mastAutoNo.Aut_start_char = "CRD";
                mastAutoNo.Aut_year = DateTime.Now.Year;

                //ADJ HDR
                SAT_ADJ_CRCD _CRDATA = new SAT_ADJ_CRCD();
                _CRDATA.staj_seq = CHNLSVC.Inventory.GetSerialID();
                _CRDATA.staj_dt = Convert.ToDateTime(txtSDate.Text).Date;
                _CRDATA.staj_com = Session["UserCompanyCode"].ToString();
                _CRDATA.staj_pc = Session["UserDefProf"].ToString();
                _CRDATA.staj_direct = 1;
                _CRDATA.staj_tp = "CRDRCVACKW";
                _CRDATA.staj_sub_tp = "COM";
                _CRDATA.staj_amt = Convert.ToDecimal(txtamount.Text.ToString());
                _CRDATA.staj_acc_dbt = txtdraccount.Text.ToString();
                _CRDATA.staj_acc_crd = txtcraccount.Text.ToString();
                _CRDATA.staj_rmk = txtremark.Text.ToString();
                _CRDATA.staj_ref = txtcrddoc.Text.ToString();
                _CRDATA.staj_seq = CHNLSVC.Inventory.GetSerialID();
                _CRDATA.staj_cre_by = Session["UserID"].ToString();
                _CRDATA.staj_cre_dt = DateTime.Now.Date;
                _CRDATA.staj_bankcd = txtbankcd.Text.ToString();
                _CRDATA.staj_midno = txtmid.Text.ToString();
                _CRDATA.staj_is_upload = 0;
                _CRDATA.staj_state_date = Convert.ToDateTime(txtSDate.Text);

                effect = CHNLSVC.Financial.SaveCredCardAchknow(_CRDATA, mastAutoNo, out _docnum);
                if (effect == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + " Successfull Saved :" + _docnum + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + " Error:" + _docnum + "');", true);
                    return;
                }
            }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                return;
            }
        }

        protected void btnalertNo_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }

        //DILSHAN ON 26/03/2018
        protected void lbtnDetaltecost_Click(object sender, EventArgs e)
        {
            try
            {
                List<SAT_ADJ_CRCD> crdNote = new List<SAT_ADJ_CRCD>();
                //if (txtDeleteconformmessageValue.Value == "Yes")
                {
                    crdNote = (List<SAT_ADJ_CRCD>)Session["StoreCN"];
                    if (crdNote.Count > 0)
                    {
                        GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                        Int32 rowIndex = dr.RowIndex;
                        Label lblItem = grdInvoices.Rows[rowIndex].FindControl("col_staj_bankcd") as Label;
                        //Session["VSItem"] = lblItem.Text;
                        var rmvItm = crdNote.Find(r => r.staj_bankcd == lblItem.Text);//Single
                        crdNote.Remove(rmvItm);
                        grdInvoices.DataSource = crdNote;
                        grdInvoices.DataBind();
                    }
                }
               // else
              //  {
              //      return;
               // }
                //gridTotCalculate();
            }
            catch (Exception ex)
            {
                //DisplayMessage(ex.Message, 4);
            }
        }
        protected void grdInvoices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Add to grid
        protected void lbtnadditems_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtdraccount.Text=="" || txtcraccount.Text=="")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter CR/DR Account" + "');", true);
                    return;
                }

                if (txtmid.Text=="")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter MID" + "');", true);
                    return;
                }
                bool _issun = CHNLSVC.Financial.IsSunAcc(txtdraccount.Text, Session["UserCompanyCode"].ToString());
                if (_issun==false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid DR Account" + "');", true);
                    return;
                }
                _issun = CHNLSVC.Financial.IsSunAcc(txtcraccount.Text, Session["UserCompanyCode"].ToString());
                if (_issun == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid CR Account" + "');", true);
                    return;
                }

                _CreditNotes = Session["StoreCN"] as List<SAT_ADJ_CRCD>;
                if (Session["StoreCN"] == null || Session["StoreCN"].ToString() == "")
                {
                    _CreditNotes = new List<SAT_ADJ_CRCD>();
                }

                SAT_ADJ_CRCD _CRDATA = new SAT_ADJ_CRCD();
                _CRDATA.staj_seq = CHNLSVC.Inventory.GetSerialID();
                _CRDATA.staj_dt = Convert.ToDateTime(txtSDate.Text).Date;
                _CRDATA.staj_com = Session["UserCompanyCode"].ToString();
                _CRDATA.staj_pc = Session["UserDefProf"].ToString();
                _CRDATA.staj_direct = 1;
                _CRDATA.staj_tp = "CRDRCVACKW";
                _CRDATA.staj_sub_tp = "COM";
                _CRDATA.staj_amt = Convert.ToDecimal(txtamount.Text.ToString());
                _CRDATA.staj_acc_dbt = txtdraccount.Text.ToString();
                _CRDATA.staj_acc_crd = txtcraccount.Text.ToString();
                _CRDATA.staj_rmk = txtremark.Text.ToString();
                _CRDATA.staj_ref = txtcrddoc.Text.ToString();
                _CRDATA.staj_seq = CHNLSVC.Inventory.GetSerialID();
                _CRDATA.staj_cre_by = Session["UserID"].ToString();
                _CRDATA.staj_cre_dt = DateTime.Now.Date;
                _CRDATA.staj_bankcd = txtbankcd.Text.ToString();
                _CRDATA.staj_midno = txtmid.Text.ToString();
                _CRDATA.staj_is_upload = 0;
                _CRDATA.staj_state_date = Convert.ToDateTime(txtSDate.Text);

                _CreditNotes.Add(_CRDATA);

                grdInvoices.DataSource = _CreditNotes;
                grdInvoices.DataBind();
                Session["StoreCN"] = _CreditNotes;
                //_CreditNotes = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                return;
            }
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                return;
            }
        }

        public void clear()
        {
            txtmid.Text = null;
            txtbankcd.Text = null;
            txtcrddoc.Text = null;
            txtremark.Text = null;
            txtcraccount.Text = null;
            txtdraccount.Text = null;
            txtamount.Text = null;
            Session["StoreCN"] = null;
            grdInvoices.DataSource = null;
            grdInvoices.DataBind();
        }
    }
}