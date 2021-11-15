using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory.WarrantyOperation
{
    public partial class DirectWarrantyPrinting : BasePage
    {
        #region PropertyData
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
        List<InventorySerialN> _intSerList
        {
            get { if (Session["_intSerList"] != null) { return (List<InventorySerialN>)Session["_intSerList"]; } else { return new List<InventorySerialN>(); } }
            set { Session["_intSerList"] = value; }
        }
        MasterItem _mstItm = new MasterItem();
        InventoryHeader _invHeader = new InventoryHeader();
        List<InventoryHeader> _invHdrList = new List<InventoryHeader>();
        List<InventorySerialN> _intSerListTmp = new List<InventorySerialN>();
        List<InventorySerialN> _intSerPrint
        {
            get { if (Session["_intSerPrint"] != null) { return (List<InventorySerialN>)Session["_intSerPrint"]; } else { return new List<InventorySerialN>(); } }
            set { Session["_intSerPrint"] = value; }
        }
        string _para = "";
        DataTable _result = new DataTable();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                Clear();
            }
            else
            {
                if (_showPop == "Show")
                {
                    serPop.Show();
                }
                else
                {
                    serPop.Hide();
                }
            }
        }
        private void Clear()
        {
            _serData = new DataTable();
            _serType = "";
            _showPop = "";
            _para = "";
            txtLocation.Text = Session["UserDefLoca"].ToString();
            _intSerList = new List<InventorySerialN>();
            lblDefPrinter.Text = lblDefPrinter.Text + GetDefaultPrinter();
            txtFrDate.Text = DateTime.Today.AddDays(-1).ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
            dgvPenDoc.DataSource = new int[] { };
            dgvPenDoc.DataBind();
            dgvDocDetails.DataSource = new int[] { };
            dgvDocDetails.DataBind();
            chkRePrint.Checked = false;
            pnlRePrint.Visible = false;
            bool b16053 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16053);
            if (b16053)
            {
                pnlRePrint.Visible = true;
            }
            _intSerPrint = new List<InventorySerialN>();
        }
        private string GetDefaultPrinter()
        {
            string _printer = "";
            try
            {
                PrinterSettings settings = new PrinterSettings();
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    settings.PrinterName = printer;
                    if (settings.IsDefaultPrinter)
                    {
                        _printer = printer;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error :" + ex.Message);
            }
            return _printer;
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
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportName"] = Session["UserCompanyCode"].ToString() == "SGL" ? "WarrPrint_SGL" : "WarrPrint_abl_new";
                string url = "";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = "";
                targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                Int32 noOfCopy = 0;
                Int32 RecNo = 0;
                string paperSize = "";
                if (radHarfPage.Checked)
                {
                    noOfCopy = 1; paperSize = "6Inch";
                }
                else if (radFullPage.Checked)
                {
                    noOfCopy = 2; paperSize = "6Inch";
                }
                else if (rad4Inch.Checked)
                {
                    noOfCopy = 1; paperSize = "4Inch";
                }
                else if (rad8Inch.Checked)
                {
                    noOfCopy = 2; paperSize = "4Inch";
                }
                else if (radMobile.Checked)
                {
                    noOfCopy = 1; paperSize = "Letter";
                }

                View.Reports.Inventory.clsInventory obj = new View.Reports.Inventory.clsInventory();

                bool _select = false;
                DataTable _warrPrint = new DataTable();
                foreach (GridViewRow item in dgvDocDetails.Rows)
                {
                    CheckBox chkSerSelect = item.FindControl("chkSerSelect") as CheckBox;
                    if (chkSerSelect.Checked)
                    {
                        _select = true;
                    }
                    Label lblIns_warr_no = item.FindControl("lblIns_warr_no") as Label;
                    Label lblIns_ser_1 = item.FindControl("lblIns_ser_1") as Label;
                    Label lblIns_itm_cd = item.FindControl("lblIns_itm_cd") as Label;
                    Label lblIns_ser_id = item.FindControl("lblIns_ser_id") as Label;
                    Label lblIns_doc_no = item.FindControl("lblIns_doc_no") as Label;
                    if (chkSerSelect.Checked)
                    {
                        int res = CHNLSVC.Inventory.UpdateDerectWarantyPrint(Convert.ToInt32(lblIns_ser_id.Text));
                        if (res < 0)
                        {
                            DispMsg("Data not updated !");
                            return;
                        }
                        else
                        {
                            CHNLSVC.Inventory.UpdateWarrantyAuthorization(new int_war_print_auth()
                            {
                                Iwa_ser_id = Convert.ToInt32(lblIns_ser_id.Text),
                                Iwa_mod_by = Session["UserID"].ToString(),
                                Iwa_mod_dt = DateTime.Now
                            });
                        }

                        RecNo = RecNo + 1;
                        for (int i = 1; i <= noOfCopy; i++)
                        {
                            DataTable _temp = CHNLSVC.Inventory.getWarrantyPrintDetails(lblIns_warr_no.Text, RecNo, lblIns_doc_no.Text);
                            _warrPrint.Merge(_temp);
                        }

                        DataTable val = CHNLSVC.Inventory.updateWarrentryIsPrint(lblIns_doc_no.Text);

                    }
                }

                _warrPrint.DefaultView.Sort = "irsm_warr_no";

                if (!_select)
                {
                    DispMsg("Please select the warranty details !"); return;
                }
                else
                {
                    if (!radMobile.Checked)
                    {
                        //obj.printWarrantyCard(lblIns_warr_no.Text, noOfCopy, paperSize);                            

                        if (paperSize == "4Inch" || paperSize == "8Inch")
                        {
                            //_warrprint_SGL
                            if (Session["UserCompanyCode"].ToString() == "SGL")
                            {
                                obj._warrprint_SGL.Database.Tables["WARR_PRINT"].SetDataSource(_warrPrint);
                                PrintPDF(targetFileName, obj._warrprint_SGL);
                            }
                            else
                            {
                                obj._warrprint.Database.Tables["WARR_PRINT"].SetDataSource(_warrPrint);
                                PrintPDF(targetFileName, obj._warrprint);
                            }

                        }
                        if (paperSize == "6Inch" || paperSize == "12Inch")
                        {
                            //obj._warrprint.Database.Tables["WARR_PRINT"].SetDataSource(_warrPrint);
                            //PrintPDF(targetFileName, obj._warrprint);
                            // add by tharanga 2017/08/18
                            obj._warrprintnew.Database.Tables["WARR_PRINT"].SetDataSource(_warrPrint);
                            PrintPDF(targetFileName, obj._warrprintnew);
                        }
                    }
                    else
                    {
                        obj._warrprintmobile.Database.Tables["WARR_PRINT"].SetDataSource(_warrPrint);
                        PrintPDF(targetFileName, obj._warrprintmobile);
                        //PrintMobile(lblIns_itm_cd.Text, lblIns_ser_1.Text);
                    }

                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                _intSerList = new List<InventorySerialN>();
                dgvDocDetails.DataSource = new int[] { };
                dgvDocDetails.DataBind();
                CHNLSVC.MsgPortal.SaveReportErrorLog("WarPrint", "DirectWarrantyPrinting", "Run Ok", Session["UserID"].ToString());

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while printig !!! " + ex.Message, "E");
                CHNLSVC.MsgPortal.SaveReportErrorLog("WarPrint", "DirectWarrantyPrinting", ex.Message, Session["UserID"].ToString());
            }

        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
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
                throw ex;
            }
        }

        private void PrintMobile(string item, string serial)
        {
            try
            {
                InvReportPara _invRepPara = new InvReportPara();
                Session["GlbReportDoc"] = "";
                Session["GlbReportType"] = "";
                Session["GlbReportName"] = "RepWarrantyCard_AOA.rpt";
                _invRepPara._GlbReportName = "RepWarrantyCard_AOA.rpt";
                _invRepPara._GlbReportDoc = Session["GlbReportDoc"].ToString();// Session["userSeqNo"].ToString();
                _invRepPara._GlbReportItemCode = item;//main itm LGMU-KP105                        
                _invRepPara._GlbReportMainSerial = serial;//PO2
                _invRepPara._GlbReportHeading = "Warranty Print";
                Session["InvReportPara"] = _invRepPara;

                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                PrintPDFWarr(targetFileName);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                // _invRepPara._GlbReportHeading = "Warranty Print";
                // Session["InvReportPara"] = _invRepPara;
                // clsInventory obj = new clsInventory();
                // obj.Print_AOA_Warra(_invRepPara);
                //// CVInventory.ReportSource = obj._warraPrint;
                // obj._warraPrint.PrintOptions.PrinterName = GetDefaultPrinter();
                // obj._warraPrint.PrintToPrinter(1, false, 0, 0);
                // string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while printing !!! " + ex.Message, "E");
            }
        }
        public void PrintPDFWarr(string targetFileName)
        {
            try
            {
                clsInventory obj = new clsInventory();
                InvReportPara _objRepoPara = new InvReportPara();
                _objRepoPara = Session["InvReportPara"] as InvReportPara;
                obj.Print_AOA_Warra(_objRepoPara);
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)obj._warraPrint;
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
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }
        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLocation.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _result = CHNLSVC.CommonSearch.SearchUserLocationByUser(_para, "Code", txtLocation.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtLocation.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtLocation.ToolTip = toolTip;
                    }
                    else
                    {
                        txtLocation.ToolTip = "";
                        txtLocation.Text = "";
                        txtLocation.Focus();
                        DispMsg("Please enter a valid location !!!");
                        return;
                    }
                }
                else
                {
                    txtLocation.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!! ", "E");
            }
        }

        protected void txtDocTp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDocTp.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                    _result = CHNLSVC.CommonSearch.GetMovementTypes(_para, "Code", txtDocTp.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtDocTp.Text.ToUpper() == row["Code"].ToString())
                            {
                                if (txtDocTp.Text.ToUpper() == "AOD" || txtDocTp.Text.ToUpper() == "DO")
                                {
                                    b2 = true;
                                    toolTip = row["Description"].ToString();
                                    break;
                                }
                            }
                        }
                    }
                    if (b2)
                    {
                        txtDocTp.ToolTip = toolTip;
                    }
                    else
                    {
                        txtDocTp.ToolTip = "";
                        txtDocTp.Text = "";
                        txtDocTp.Focus();
                        DispMsg("Please enter a valid document type !");
                        return;
                    }
                }
                else
                {
                    txtLocation.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!! ", "E");
            }
        }

        protected void lbtnSeLocation_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                _serData = CHNLSVC.CommonSearch.SearchUserLocationByUser(_para, null, null);
                LoadSearchPopup("Location");
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeDocTp_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                _serData = CHNLSVC.CommonSearch.GetMovementTypes(_para, null, null);
                LoadSearchPopup("MovementTypes");
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private void LoadSearchPopup(string serType)
        {
            try
            {
                dgvResult.DataSource = new int[] { };
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData;
                        BindUCtrlDDLData(_serData);
                    }
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = serType;
                serPop.Show();
                _showPop = "Show";
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            _showPop = "Hide";
            serPop.Hide();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            lbtnSearch_Click(null, null);
        }
        protected string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserID"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementTypes:
                    {
                        paramsText.Append("" + seperator);
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
                if (_serType == "Location")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _serData = CHNLSVC.CommonSearch.SearchUserLocationByUser(_para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text);
                }
                else if (_serType == "MovementTypes")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                    _serData = CHNLSVC.CommonSearch.GetMovementTypes(_para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text);
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResult.DataBind();
                serPop.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                serPop.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "Location")
                {
                    txtLocation.Text = code;
                    txtLocation_TextChanged(null, null);
                }
                else if (_serType == "MovementTypes")
                {
                    txtDocTp.Text = code;
                    txtDocTp_TextChanged(null, null);
                }
                serPop.Hide();
                _showPop = "Hide";
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnMainSer_Click(object sender, EventArgs e)
        {
            try
            {
                _intSerPrint = new List<InventorySerialN>();
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    DispMsg("Please select a location !!!"); txtLocation.Focus(); return;
                }
                DateTime tmpDt = DateTime.Today;
                DateTime dtFrom = DateTime.TryParse(txtFrDate.Text, out tmpDt) ? Convert.ToDateTime(txtFrDate.Text) : tmpDt;
                DateTime dtTo = DateTime.TryParse(txtToDate.Text, out tmpDt) ? Convert.ToDateTime(txtToDate.Text) : tmpDt;
                bool b16053 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16053);
                if (chkRePrint.Checked && b16053)
                {
                    _invHdrList = CHNLSVC.Inventory.GetIntHdrDatByDateRange(new InventoryHeader()
                    {
                        Ith_com = Session["UserCompanyCode"].ToString(),
                        Ith_loc = string.IsNullOrEmpty(txtLocation.Text) ? null : txtLocation.Text.ToUpper().Trim(),
                        Ith_entry_tp = string.IsNullOrEmpty(txtDocTp.Text) ? null : txtDocTp.Text.ToUpper().Trim(),
                        Ith_isprinted = true
                    }, dtFrom, dtTo, 1);
                }
                else
                {
                    _invHdrList = CHNLSVC.Inventory.GetIntHdrDatByDateRange(new InventoryHeader()
                    {
                        Ith_com = Session["UserCompanyCode"].ToString(),
                        Ith_loc = string.IsNullOrEmpty(txtLocation.Text) ? null : txtLocation.Text.ToUpper().Trim(),
                        Ith_entry_tp = string.IsNullOrEmpty(txtDocTp.Text) ? null : txtDocTp.Text.ToUpper().Trim(),
                        Ith_isprinted = false
                    }, dtFrom, dtTo, 1);
                }

                dgvPenDoc.DataSource = new int[] { };
                dgvDocDetails.DataSource = new int[] { };
                dgvDocDetails.DataBind();
                bool dataHave = false;
                List<InventoryHeader> newList = new List<InventoryHeader>();
                if (_invHdrList != null)
                {
                    if (_invHdrList.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(txtDocNo.Text))
                        {
                            foreach (InventoryHeader item in _invHdrList)
                            {
                                if (item.Ith_doc_no == txtDocNo.Text)
                                {
                                    newList.Add(item);
                                }
                            }
                        }
                        else
                        {
                            newList = new List<InventoryHeader>(_invHdrList);
                        }
                        dgvPenDoc.DataSource = newList;
                        dataHave = true;
                    }
                }
                dgvPenDoc.DataBind();
                if (!dataHave)
                {
                    DispMsg("No data found !!!");
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private void BindDocDetails(List<InventoryHeader> list)
        {
            foreach (var item in list)
            {
            }
        }

        protected void chkSerSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkSerSelectAll = dgvDocDetails.HeaderRow.FindControl("chkSerSelectAll") as CheckBox;
                bool select = false;
                if (chkSerSelectAll.Checked)
                {
                    select = true;
                }
                foreach (GridViewRow item in dgvDocDetails.Rows)
                {
                    CheckBox chkSerSelect = item.FindControl("chkSerSelect") as CheckBox;
                    chkSerSelect.Checked = select;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }

        }
        protected void lbtnSelectDoc_Click(object sender, EventArgs e)
        {
            try
            {
                // _intSerList = new List<InventorySerialN>();
                //dgvDocDetails.DataSource = new int[] { };
                //dgvDocDetails.DataBind();
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblIth_doc_no = row.FindControl("lblIth_doc_no") as Label;
                bool b16053 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16053);
                int _isPer = chkRePrint.Checked && b16053 ? 1 : 0;
                _intSerListTmp = new List<InventorySerialN>();
                List<InventorySerialN> _varTempSer = CHNLSVC.Inventory.GetDerWarrantyPrintSerial(lblIth_doc_no.Text, _isPer);
                List<int_war_print_auth> _intWarSerAvaList = CHNLSVC.Inventory.GET_INT_WAR_PRINT_AUTH(new int_war_print_auth() { Iwa_doc_no = lblIth_doc_no.Text, Iwa_act = 1 });
                if (chkRePrint.Checked)
                {
                    bool _added = false;
                    foreach (var item in _varTempSer)
                    {
                        _added = false;
                        foreach (var _intSerAva in _intWarSerAvaList)
                        {
                            if (_intSerAva.Iwa_ser_id == item.Ins_ser_id && !_added)
                            {
                                _intSerListTmp.Add(item);
                                _added = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    _intSerListTmp = _varTempSer;
                }
                foreach (var item in _intSerListTmp)
                {
                    _intSerPrint.Add(item);
                }

                bool intSerAvailble = false;
                if (_intSerListTmp != null)
                {

                    if (_intSerListTmp.Count > 0)
                    {
                        _intSerListTmp = _intSerListTmp.Where(c => c.Ins_warr_no != "N/A" && !string.IsNullOrEmpty(c.Ins_warr_no)).ToList();
                        if (_intSerListTmp != null)
                        {
                            if (_intSerListTmp.Count > 0)
                            {
                                intSerAvailble = true;
                            }
                        }

                    }

                }
                if (!intSerAvailble)
                {
                    DispMsg("No warranty # available !!!"); return;
                }
                if (_intSerListTmp != null)
                {
                    foreach (var item in _intSerListTmp)
                    {
                        bool allreadyAdded = false;
                        var v = _intSerList.Where(c => c.Ins_warr_no == item.Ins_warr_no && c.Ins_itm_cd == item.Ins_itm_cd).ToList();
                        if (v != null)
                        {
                            if (v.Count > 0)
                            {
                                allreadyAdded = true;
                            }
                        }
                        if (!allreadyAdded)
                        {
                            _intSerList.Add(item);
                        }
                    }
                }
                BindDocDetails();
                if (dgvDocDetails.Rows.Count > 0)
                {
                    CheckBox chkSerSelectAll = dgvDocDetails.HeaderRow.FindControl("chkSerSelectAll") as CheckBox;
                    chkSerSelectAll.Checked = true;
                    chkSerSelectAll_CheckedChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!! " + ex.Message, "E");
            }
        }

        private void BindDocDetails()
        {
            dgvDocDetails.DataSource = new Int32[] { };
            List<InventorySerialN> newList = new List<InventorySerialN>();
            if (_intSerList != null)
            {
                if (_intSerList.Count > 0)
                {
                    foreach (var item in _intSerList)
                    {
                        _mstItm = CHNLSVC.General.GetItemMaster(item.Ins_itm_cd);
                        if (_mstItm != null)
                        {
                            item.tmpItmDesc = _mstItm.Mi_shortdesc;
                            item.tmpItmModel = _mstItm.Mi_model;
                            item.tmpItmTp = _mstItm.Mi_brand;

                        }
                        if (!string.IsNullOrEmpty(txtSer.Text) && !string.IsNullOrEmpty(txtWrnCrd.Text))
                        {
                            if (item.Ins_ser_1 == txtSer.Text && item.Ins_warr_no == txtWrnCrd.Text)
                            {
                                newList.Add(item);
                            }
                        }
                        else if (!string.IsNullOrEmpty(txtSer.Text))
                        {
                            if (item.Ins_ser_1 == txtSer.Text)
                            {
                                newList.Add(item);
                            }
                        }
                        else if (!string.IsNullOrEmpty(txtWrnCrd.Text))
                        {
                            if (item.Ins_warr_no == txtWrnCrd.Text)
                            {
                                newList.Add(item);
                            }
                        }
                        else
                        {
                            newList = new List<InventorySerialN>(_intSerList);
                        }
                    }
                    dgvDocDetails.DataSource = newList;
                }
            }
            dgvDocDetails.DataBind();
        }

        protected void chkRePrint_CheckedChanged(object sender, EventArgs e)
        {
            _intSerList = new List<InventorySerialN>();
            BindDocDetails();
        }

        protected void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            lbtnMainSer_Click(sender, e);
        }

        protected void txtSer_TextChanged(object sender, EventArgs e)
        {
            BindDocDetails();
        }
    }


}