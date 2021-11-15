using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Finance
{
    public partial class CreditCardReconcilation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtEDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

                gvpclist.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                gvpclist.DataSource = _result;
                gvpclist.DataBind();

            }
            else
            {

            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
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
                mastAutoNo.Aut_moduleid = "BNKST";
                mastAutoNo.Aut_start_char = "BNKST";
                mastAutoNo.Aut_year = DateTime.Now.Year;

                //ADJ HDR
                SAT_ADJ_CRCD _CRDATA = new SAT_ADJ_CRCD();
                _CRDATA.staj_seq = 0;
                _CRDATA.staj_dt = DateTime.Now.Date;
                _CRDATA.staj_com = Session["UserCompanyCode"].ToString();
                _CRDATA.staj_pc = Session["UserDefProf"].ToString();
                _CRDATA.staj_direct = 1;
                _CRDATA.staj_tp = "BNKST";
                _CRDATA.staj_sub_tp = "COM";
                _CRDATA.staj_amt = Convert.ToDecimal(txtcommissions.Text.ToString());
                _CRDATA.staj_acc_dbt = txtdraccount.Text.ToString();
                _CRDATA.staj_acc_crd = txtcraccount.Text.ToString();
                _CRDATA.staj_rmk = txtremark.Text.ToString();
                _CRDATA.staj_ref = "";
                _CRDATA.staj_seq = 0;
                _CRDATA.staj_cre_by = Session["UserID"].ToString();
                _CRDATA.staj_cre_dt = DateTime.Now.Date;

                //get reciept no
                List<RecieptHeader> reciept = new List<RecieptHeader>();

                foreach (GridViewRow row in gvreciptlist.Rows)
                {
                    CheckBox chkselect2 = (CheckBox)row.FindControl("chkselect2");
                    Label lbrecno = (Label)row.FindControl("lbrecno");
                    Label mid = (Label)row.FindControl("lbmid");
                    if (chkselect2.Checked)
                    {
                        RecieptHeader obrec = new RecieptHeader();
                        obrec.Sar_receipt_no = lbrecno.Text.ToString();
                        obrec.Sar_direct_deposit_branch = mid.Text.ToString();
                        reciept.Add(obrec);
                    }
                   
                }
               
                //save
                effect = CHNLSVC.Financial.SaveCredCardRec(_CRDATA, mastAutoNo, reciept, out _docnum);
                if (effect==1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + " Successfull Saved :" + _docnum + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + " Error:" + _docnum + "');", true);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {

            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void chkselectall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvpclist.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                    if (chkselectall.Checked)
                    {
                        chkselect.Checked = true;

                    }
                    else
                    {
                        chkselect.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }
        protected void lbtnaddmid_Click(object sender, EventArgs e)
        {
            try
            {
                string pctxt = "";
                foreach (GridViewRow row in gvpclist.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                    if (chkselect != null && chkselect.Checked)
                    {
                        if (pctxt != "")
                        {
                            pctxt = pctxt + ",";
                        }
                        Label lbpccd = (Label)row.FindControl("lbpccd");
                        string com_cd = lbpccd.Text;
                        pctxt = pctxt + com_cd;

                    }
                }


                DataTable _tblmid = CHNLSVC.Financial.GetMIDNO(Session["UserCompanyCode"].ToString(), pctxt);
                if (_tblmid != null)
                {

                    dropMID.DataSource = _tblmid;
                    dropMID.DataTextField = "mstm_mid";
                    dropMID.DataValueField = "mstm_mid";
                    dropMID.DataBind();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        protected void lbtnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable reclist = new DataTable();
                if (chkmidall.Checked==false)
                {

                     reclist = CHNLSVC.Financial.GetMIDRECIEPT(Session["UserCompanyCode"].ToString(), dropMID.Text.ToString(), Convert.ToDateTime(txtSDate.Text.ToString()), Convert.ToDateTime(txtEDate.Text.ToString()));
                }
                else
                {
                    string mids = "";
                    reclist = null;
                    foreach (ListItem item in dropMID.Items)
                    {
                        mids =mids+ item.Text.ToString() +",";
                    }
                    reclist = CHNLSVC.Financial.GetMIDRECIEPTAll(Session["UserCompanyCode"].ToString(), mids, Convert.ToDateTime(txtSDate.Text.ToString()), Convert.ToDateTime(txtEDate.Text.ToString()));
                }
             
                if (reclist != null)
                {
                    gvreciptlist.DataSource = reclist;
                    gvreciptlist.DataBind();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        protected void chkallreciept_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvreciptlist.Rows)
                {
                    CheckBox chkselect2 = (CheckBox)row.FindControl("chkselect2");
                    if (chkallreciept.Checked)
                    {
                        chkselect2.Checked = true;

                    }
                    else
                    {
                        chkselect2.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        protected void lbncalc_Click(object sender, EventArgs e)
        {
            try
            {
                decimal grossamm = 0;
                decimal commi = 0;
                decimal netamm = 0;
                foreach (GridViewRow row in gvreciptlist.Rows)
                {
                    CheckBox chkselect2 = (CheckBox)row.FindControl("chkselect2");
                    Label lbtrammount = (Label)row.FindControl("lbtrammount");
                    Label lbcomm = (Label)row.FindControl("lbcomm");

                    if (chkselect2.Checked)
                    {
                        grossamm = grossamm + Convert.ToDecimal(lbtrammount.Text.ToString());
                        commi = commi + Convert.ToDecimal(lbcomm.Text.ToString());
                    }

                }
                netamm = grossamm - commi;
                lbgrossamount.Text = grossamm.ToString();
                txtcommissions.Text = commi.ToString();
                lbnetpayment.Text = netamm.ToString();


            }
            catch (Exception ex)
            {

            }
        }

        protected void dropMID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string pctxt = "";
                foreach (GridViewRow row in gvpclist.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                    if (chkselect != null && chkselect.Checked)
                    {
                        if (pctxt != "")
                        {
                            pctxt = pctxt + ",";
                        }
                        Label lbpccd = (Label)row.FindControl("lbpccd");
                        string com_cd = lbpccd.Text;
                        pctxt = pctxt + com_cd;

                    }
                }
                DataTable middetails = CHNLSVC.Financial.GetMIDDETAILS(Session["UserCompanyCode"].ToString(), pctxt, dropMID.Text.ToString());
                if (middetails.Rows.Count>0)
                {
                    lbbank.Text = middetails.Rows[0]["mstm_bank"].ToString();
                }

            }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        protected void lbexport_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                string out1="";
                 DataTable exceldata = new DataTable();
                 DataRow dr;
                 exceldata.Columns.Add("Receipt No", typeof(string));
                 exceldata.Columns.Add("Date", typeof(DateTime));
                 exceldata.Columns.Add("Description", typeof(string));
                 exceldata.Columns.Add("Auth", typeof(string));
                 exceldata.Columns.Add("Ammount", typeof(decimal));
                 exceldata.Columns.Add("Commission", typeof(decimal));
                foreach (GridViewRow row in gvreciptlist.Rows)
                {
                    CheckBox chkselect2 = (CheckBox)row.FindControl("chkselect2");
                    Label lbrecno = (Label)row.FindControl("lbrecno");
                    Label lbtrdate = (Label)row.FindControl("lbtrdate");
                    Label lbdescrip = (Label)row.FindControl("lbdescrip");
                    Label lbauth = (Label)row.FindControl("lbauth");
                    Label lbtrammount = (Label)row.FindControl("lbtrammount");
                    Label lbcomm = (Label)row.FindControl("lbcomm");
                    if (chkselect2.Checked)
                    {
                       
                        dr = exceldata.NewRow();
                        dr["Receipt No"] = lbrecno.Text.ToString();
                        dr["Date"] = Convert.ToDateTime(lbtrdate.Text.ToString());
                        dr["Description"] = lbdescrip.Text.ToString();
                        dr["Auth"] = lbauth.Text.ToString();
                        dr["Ammount"] = Convert.ToDecimal(lbtrammount.Text.ToString());
                        dr["Commission"] = Convert.ToDecimal(lbcomm.Text.ToString()); ;
                        exceldata.Rows.Add(dr);

                    }

                   
                }
                exceldata.TableName = "dt";
                string path = CHNLSVC.MsgPortal.ExportExcel2007(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), exceldata, out out1);
                _copytoLocal(path);
                 url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xlsx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                //ExportToExcel(exceldata, @"C:\SUN\CRCD.xls");
                ////url = "<script>window.open('C:/SUN/" + "CRCD" + ".xls','_blank');</script>";
                ////ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ////ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                //Process p = new Process();
                //p.StartInfo = new ProcessStartInfo("C:/SUN/CRCD.xls");
                //p.Start();
            }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
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
                        workSheet.SaveAs(ExcelFilePath);
                        excelApp.Quit();
                        // MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                        + ex.Message);
                    }
                }
                else // no filepath is given
                {
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }

    }
}