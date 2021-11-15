using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.WebERPClient.UserControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using System.Globalization;
using System.Transactions;

namespace FF.WebERPClient.General_Modules
{
    public partial class ReprintDocuments : BasePage
    {
        static string _docType = "CS";
        static string _status = "ALL";
        static string _reqDocType = "CS";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtFromDateApp.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtToDateApp.Text = DateTime.Now.ToString("dd/MMM/yyyy");

                BindDocNumbersGridData(_docType, GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                BindRequestedDocDetailsGridData(GlbUserDefLoca, "ALL", Convert.ToDateTime(DateTime.Now.Date).Date, Convert.ToDateTime(DateTime.Now.Date).Date);
                BindAllDocDetailsGridData("", "ALL", Convert.ToDateTime(DateTime.Now.Date).Date, Convert.ToDateTime(DateTime.Now.Date).Date);

                string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _masterLocation, "DOCRP"))
                {
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            BindDocNumbersGridData(_docType, GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            BindRequestedDocDetailsGridData(GlbUserDefLoca, "ALL", Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
        }

        protected void btnViewApp_Click(object sender, EventArgs e)
        {
            BindAllDocDetailsGridData("", "ALL", Convert.ToDateTime(txtFromDateApp.Text), Convert.ToDateTime(txtToDateApp.Text));
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtDocNoApp.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select a Document!");
                return;
            }

            int X = CHNLSVC.General.UpdateReprintApproval(txtDocNoApp.Text, "R",GlbUserName);
            BindAllDocDetailsGridData("", _status, Convert.ToDateTime(txtFromDateApp.Text), Convert.ToDateTime(txtToDateApp.Text));

            txtDocNoApp.Text = "";
            txtDocDateApp.Text = "";
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Rejected!");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (txtDocNoApp.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select a Document!");
                return;
            }

            int X = CHNLSVC.General.UpdateReprintApproval(txtDocNoApp.Text, "A", GlbUserName);
            BindAllDocDetailsGridData("", _status, Convert.ToDateTime(txtFromDateApp.Text), Convert.ToDateTime(txtToDateApp.Text));

            txtDocNoApp.Text = "";
            txtDocDateApp.Text = "";
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Approved!");

        }

        protected void btnCloseApp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnSaveApp_Click(object sender, EventArgs e)
        {

        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Msg = "";
            if (txtReqDocNo.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select a Document!");
                return;
            }
            GlbDocNosList = txtReqDocNo.Text;
            GlbMainPage = "~/General_Modules/ReprintDocuments.aspx";

            int X = CHNLSVC.General.UpdatePrintStatus(txtReqDocNo.Text);
            BindRequestedDocDetailsGridData(GlbUserDefLoca, "ALL", Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));

            if (_reqDocType == "CS" || _reqDocType == "CSREV" || _reqDocType == "HP" || _reqDocType == "HPREV" || _reqDocType == "CR" || _reqDocType == "CRREV")
            {
                if (GlbUserComCode == "SGL")
                {
                    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\InvoicePrintPre.rpt";
                    GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoicePrintPre.rpt";
                }
                else
                {
                    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\InvoicePrint.rpt";
                    GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoicePrint.rpt";
                }

                Msg = "window.open('../Test/PdfPrint.aspx',  '_blank');";
                
            }

            if (_reqDocType == "ADVAN")
            {
                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ReceiptPrint.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrint.rpt";
                Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");

                Msg = "window.open('../../Reports_Module/Sales_Rep/ReceiptEntryPrint.aspx','_blank');";
            }

            if (_reqDocType == "HPREC")
            {
                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HPReceiptPrint.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/HPReceiptPrint.rpt";
                Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");

                Msg = "window.open('../../Reports_Module/Sales_Rep/HpReceiptPrintPrint.aspx','_blank');";
            }

            if (_reqDocType == "HPAGR")
            {
                GlbReportName = "HP Agreement";
                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HP_Agreement.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/HP_Agreement.rpt";
                int Z = CHNLSVC.Financial.Print_HP_Agreement(txtReqDocNo.Text);
                Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");
                
                //Msg = "<script>window.open('../../Reports_Module/Sales_Rep/HP_AgreementPrint.aspx','_blank');</script>";
            }

            if (_reqDocType == "OUT" || _reqDocType == "IN")
            {

                if (GlbUserComCode == "SGL")
                {
                    GlbReportPath = "~\\Reports_Module\\Inv_Rep\\Outward_DocPre.rpt";
                    GlbReportMapPath = "~/Reports_Module/Inv_Rep/Outward_DocPre.rpt";
                }
                else
                {
                    GlbReportPath = "~\\Reports_Module\\Inv_Rep\\Outward_Doc.rpt";
                    GlbReportMapPath = "~/Reports_Module/Inv_Rep/Outward_Doc.rpt";
                }
                Msg = "window.open('../../Reports_Module/Inv_Rep/OutWard_DocPrint.aspx','_blank');";
                
            }

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, true);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDocNo.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select a Document!");
                return;
            }

            Reprint_Docs _reprint = new Reprint_Docs();

            _reprint.Drp_tp = _docType;
            _reprint.Drp_doc_no = txtDocNo.Text;
            _reprint.Drp_doc_dt = Convert.ToDateTime(txtDocDate.Text);
            _reprint.Drp_loc = GlbUserDefLoca;
            _reprint.Drp_req_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _reprint.Drp_is_add_pending = 0;
            _reprint.Drp_stus = "P";
            _reprint.Drp_app_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _reprint.Drp_can_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _reprint.Drp_rej_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _reprint.Drp_stus_change_by = "";
            _reprint.Drp_printed = 0;
            _reprint.Drp_print_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _reprint.Drp_reason = txtReason.Text;

            int X = CHNLSVC.General.SaveReprintDocRequest(_reprint);
            BindRequestedDocDetailsGridData(GlbUserDefLoca, "ALL", Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));

            //string Msg = "<script>alert('Successfully Saved!');window.location = 'ReprintDocuments.aspx';</script>";
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            txtDocNo.Text = "";
            txtDocDate.Text = "";
            txtReason.Text = "";
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved!");
        }

        private void BindDocNumbersGridData(string _type, string _loc, string _pc, DateTime _from, DateTime _to)
        {
            txtDocDate.Text = "";
            txtDocNo.Text = "";

            DataTable _tbl = CHNLSVC.General.GetReprintDocs(_type, _loc, _pc, Convert.ToDateTime(_from), Convert.ToDateTime(_to));

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { docno = dr["docno"], docdate = dr["docdate"] } into item
                   select new
                   {
                       docno = item.Key.docno,
                       docdate = item.Key.docdate,

                   };

                gvDocs.DataSource = _tblItems;
            }
            else
            {
                gvDocs.DataSource = CHNLSVC.General.GetReprintDocs(_type, _loc, _pc, Convert.ToDateTime(_from), Convert.ToDateTime(_to));
            }
            gvDocs.DataBind();

        }

        private void BindRequestedDocDetailsGridData(string _loc, string _stus, DateTime _from, DateTime _to)
        {

            DataTable _tbl = CHNLSVC.General.GetRequestedReprintDocs(_loc, _stus, Convert.ToDateTime(_from), Convert.ToDateTime(_to));

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { DRP_DOC_NO = dr["DRP_DOC_NO"], DRP_DOC_DT = dr["DRP_DOC_DT"], DRP_TP = dr["DRP_TP"], DRP_REQ_DT = dr["DRP_REQ_DT"], DRP_STUS = dr["DRP_STUS"], DRP_PRINTED = dr["DRP_PRINTED"] } into item
                   select new
                   {
                       DRP_DOC_NO = item.Key.DRP_DOC_NO,
                       DRP_DOC_DT = item.Key.DRP_DOC_DT,
                       DRP_TP = item.Key.DRP_TP,
                       DRP_REQ_DT = item.Key.DRP_REQ_DT,
                       DRP_STUS = item.Key.DRP_STUS,
                       DRP_PRINTED = item.Key.DRP_PRINTED,

                   };

                gvReqDocs.DataSource = _tblItems;
            }
            else
            {
                gvReqDocs.DataSource = CHNLSVC.General.GetRequestedReprintDocs(_loc, _stus, Convert.ToDateTime(_from), Convert.ToDateTime(_to));
            }
            gvReqDocs.DataBind();

        }

        private void BindAllDocDetailsGridData(string _loc, string _status, DateTime _from, DateTime _to)
        {

            DataTable _tbl = CHNLSVC.General.GetRequestedReprintDocs("", _status, Convert.ToDateTime(_from), Convert.ToDateTime(_to));

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { DRP_LOC = dr["DRP_LOC"], DRP_DOC_NO = dr["DRP_DOC_NO"], DRP_DOC_DT = dr["DRP_DOC_DT"], DRP_TP = dr["DRP_TP"], DRP_REQ_DT = dr["DRP_REQ_DT"], DRP_STUS = dr["DRP_STUS"], DRP_PRINTED = dr["DRP_PRINTED"] } into item
                   select new
                   {
                       DRP_LOC = item.Key.DRP_LOC,
                       DRP_DOC_NO = item.Key.DRP_DOC_NO,
                       DRP_DOC_DT = item.Key.DRP_DOC_DT,
                       DRP_TP = item.Key.DRP_TP,
                       DRP_REQ_DT = item.Key.DRP_REQ_DT,
                       DRP_STUS = item.Key.DRP_STUS,
                       DRP_PRINTED = item.Key.DRP_PRINTED,

                   };

                gvAllDocs.DataSource = _tblItems;
            }
            else
            {
                gvAllDocs.DataSource = CHNLSVC.General.GetRequestedReprintDocs("", _status, Convert.ToDateTime(_from), Convert.ToDateTime(_to));
            }
            gvAllDocs.DataBind();

        }

        static int _count = 0;
        protected void OnPendingRequestBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                            gvDocs,
                            String.Concat("Select$", e.Row.RowIndex),
                            true);

                _count += 1;
            }
        }

        protected void OnRequestedBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                            gvReqDocs,
                            String.Concat("Select$", e.Row.RowIndex),
                            true);

                _count += 1;
            }
        }

        protected void OnAllBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                            gvAllDocs,
                            String.Concat("Select$", e.Row.RowIndex),
                            true);

                _count += 1;
            }
        }

        protected void BindSelectedDocNo(Object sender, EventArgs e)
        {
            txtDocNo.Text = gvDocs.SelectedDataKey[0].ToString();
            txtDocDate.Text = Convert.ToDateTime(gvDocs.SelectedDataKey[1].ToString()).Date.ToString("dd/MM/yyyy");

        }

        protected void BindSelectedReqDocNo(Object sender, EventArgs e)
        {
            txtReqDocNo.Text = gvReqDocs.SelectedDataKey[0].ToString();
            txtReqDocDate.Text = Convert.ToDateTime(gvReqDocs.SelectedDataKey[1].ToString()).Date.ToString("dd/MM/yyyy");
            _reqDocType = gvReqDocs.SelectedDataKey[2].ToString();

            if (gvReqDocs.SelectedDataKey[4].ToString() == "A" && Convert.ToInt32(gvReqDocs.SelectedDataKey[5]) == 0)
            {
                btnPrint.Enabled = true;
            }
            else
            {
                btnPrint.Enabled = false;
            }
        }

        protected void BindAllReqDocNo(Object sender, EventArgs e)
        {
            txtDocNoApp.Text = gvAllDocs.SelectedDataKey[1].ToString();
            txtDocDateApp.Text = Convert.ToDateTime(gvAllDocs.SelectedDataKey[2].ToString()).Date.ToString("dd/MM/yyyy");

            if (gvAllDocs.SelectedDataKey[5].ToString() == "P")
            {
                btnReject.Enabled = true;
                btnApprove.Enabled = true;
            }
            else
            {
                btnReject.Enabled = false;
                btnApprove.Enabled = false;
            }
        }

        protected void opt1_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("CS", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "CS";
        }
        protected void opt2_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("CSREV", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "CSREV";
        }
        protected void opt3_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("CR", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "CR";
        }
        protected void opt4_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("CRREV", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "CRREV";
        }
        protected void opt5_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("DIR", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "DIR";
        }
        protected void opt6_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("HP", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "HP";
        }
        protected void opt7_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("HPREV", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "HPREV";
        }
        protected void opt8_OnCheckedChanged(object sender, System.EventArgs e)
        {
        }
        protected void opt9_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("OUT", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "OUT";
        }
        protected void opt10_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("IN", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "IN";
        }
        protected void opt11_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("ADVREC", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "ADVREC";
        }
        protected void opt12_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("HPREC", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "HPREC";
        }
        protected void opt13_OnCheckedChanged(object sender, System.EventArgs e)
        {
            BindDocNumbersGridData("HPAGR", GlbUserDefLoca, GlbUserDefProf, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            _docType = "HPAGR";
        }
        protected void opt14_OnCheckedChanged(object sender, System.EventArgs e)
        {
        }
        protected void opt15_OnCheckedChanged(object sender, System.EventArgs e)
        {
        }

        protected void optAll_OnCheckedChanged(object sender, System.EventArgs e)
        {
            _status = "ALL";
            txtDocDateApp.Text = "";
            txtDocNoApp.Text = "";
            BindAllDocDetailsGridData("", _status, Convert.ToDateTime(txtFromDateApp.Text), Convert.ToDateTime(txtToDateApp.Text));
        }
        protected void optPending_OnCheckedChanged(object sender, System.EventArgs e)
        {
            _status = "P";
            txtDocDateApp.Text = "";
            txtDocNoApp.Text = "";
            BindAllDocDetailsGridData("", _status, Convert.ToDateTime(txtFromDateApp.Text), Convert.ToDateTime(txtToDateApp.Text));
        }
        protected void optApp_OnCheckedChanged(object sender, System.EventArgs e)
        {
            _status = "A";
            txtDocDateApp.Text = "";
            txtDocNoApp.Text = "";
            BindAllDocDetailsGridData("", _status, Convert.ToDateTime(txtFromDateApp.Text), Convert.ToDateTime(txtToDateApp.Text));
        }
        protected void optRej_OnCheckedChanged(object sender, System.EventArgs e)
        {
            _status = "R";
            txtDocDateApp.Text = "";
            txtDocNoApp.Text = "";
            BindAllDocDetailsGridData("", _status, Convert.ToDateTime(txtFromDateApp.Text), Convert.ToDateTime(txtToDateApp.Text));
        }
    }
}