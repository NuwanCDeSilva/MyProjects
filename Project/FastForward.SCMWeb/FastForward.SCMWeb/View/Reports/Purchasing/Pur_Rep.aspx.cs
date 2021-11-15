using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.UserControls;
using FastForward.SCMWeb.View.Reports.Inventory;
using FastForward.SCMWeb.View.Reports.Sales;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
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

namespace FastForward.SCMWeb.View.Reports.Purchasing
{
    public partial class Pur_Rep : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["DATAADMIN"] = null;
                Session["DATACOMPANY"] = null;
                BindYear();
                LoadEmptyGrid();
                BindCompany(Session["UserID"].ToString());
                ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                txtFromDate.Text = (DateTime.Now.Date.AddMonths(-1)).ToString("dd/MMM/yyyy");
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtAsAt.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtExDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                hdfCurrentDate.Value = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                locationPanel.Enabled = !chkAllCompany.Checked;
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
                txtAgeFrom.Text = "0";
                txtAgeTo.Text = "9999";
                dgvAdminTeam.Columns[1].Visible = false;
            }
            else
            {

            }
        }
        private void displayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
        }
        string _opt = "";
        protected void lbtnView_Click(object sender, EventArgs e)
        {

            try
            {
                if (rad29.Checked)
                {

                }
                else
                {
                    if (rad27.Checked == true || rad28.Checked == true)
                        if ((dgvLocation.Rows.Count == 0) && (ucLoactionSearch.txtLocation.Text == ""))
                        {
                            displayMessage("Please add location deatils");
                            return;
                        }
                }
                if ((ucLoactionSearch.Company == ""))
                {
                    displayMessage("Please enter company");
                    return;
                }

                Boolean _isSelected = false;
                Session["GlbReportName"] = string.Empty;
                GlbReportName = string.Empty;

                lbtnView.Enabled = false;

                //kapila 23/12/2015 - to get the company name in the report header
                string com_cds = "";
                string com_desc = "";
                string com_add = "";
                Int32 _cnt = 0;
                foreach (GridViewRow row in dgvCompany.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkCompanyCode");
                    if (chk.Checked)
                    {
                        if (com_cds != "")
                        {
                            com_cds = com_cds + ",";
                        }
                        Label lblCode = (Label)row.FindControl("lblCode");

                        com_cds = com_cds + lblCode.Text;
                        Session["GlbReportCompCode"] = lblCode.Text;
                        _cnt = _cnt + 1;
                    }
                }

                MasterCompany _newCom = new MasterCompany();
                _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                com_desc = _newCom.Mc_desc;
                com_add = _newCom.Mc_add1 + " " + _newCom.Mc_add2;

                //BaseCls.GlbReportCompName = com_desc;
                //BaseCls.GlbReportCompanies = com_cds;

                ////set all common parameters
                //BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                //BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                //BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                ////BaseCls.GlbReportSupplier = txtSupplier.Text.ToString();

                //BaseCls.GlbReportCompCode = Session["UserCompanyCode"].ToString();
                //BaseCls.GlbReportComp = Session["UserCompanyCode"].ToString();
                //BaseCls.GlbReportProfit = Session["UserDefLoca"].ToString();
                Session["GlbReportType"] = "";

                string vItemCat1 = "";
                string vItemCat2 = "";
                string vItemCat3 = "";
                string vItemCat4 = "";
                string vItemCat5 = "";
                string vItemcode = "";
                string vBrand = "";
                string vModel = "";

                foreach (ListItem Item in listCat1.Items)
                {
                    vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listCat2.Items)
                {
                    vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listCat3.Items)
                {
                    vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listCat4.Items)
                {
                    vItemCat4 = vItemCat4 == "" ? "^" + Item.Text + "$" : vItemCat4 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listCat5.Items)
                {
                    vItemCat5 = vItemCat5 == "" ? "^" + Item.Text + "$" : vItemCat5 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listItemCode.Items)
                {
                    vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listBrand.Items)
                {
                    vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listModel.Items)
                {
                    vModel = vModel == "" ? "^" + Item.Text + "$" : vModel + "|" + "^" + Item.Text + "$";
                }




                //BaseCls.GlbReportItemStatus = txtItemStat.Text;
                //BaseCls.GlbReportDoc2 = chkWithGit.Checked == true ? "Y" : "N";
                //BaseCls.GlbReportJobStatus = chkWithJob.Checked == true ? "Y" : "N";
                //BaseCls.GlbReportWithRCC = chkWithServiceWIP.Checked == true ? "Y" : "N";
                //BaseCls.GlbReportWithCost = chkWithCostWIP.Checked == true ? Convert.ToInt16(1) : Convert.ToInt16(0);

                //BaseCls.GlbReportDocType = txtDocType.Text;
                //BaseCls.GlbReportDirection = txtDirection.Text;
                //BaseCls.GlbReportDocSubType = txtDocSubType.Text;

                int x = 0;
                foreach (ListItem Item in listGroup.Items)
                {
                    x++;
                    if (Item.Text == "INV")
                    {
                        if (listGroup.Items.Count > x)
                        {
                            displayMessage("Document Number group should be the last group.");
                            lbtnView.Enabled = true;
                            return;
                        }
                    }
                }


                Session["GlbReportBrand"] = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                Session["GlbReportModel"] = vModel == "" ? txtModel.Text == "" ? txtModel.Text : "^" + txtModel.Text + "$" : vModel;
                Session["GlbReportItemCode"] = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                Session["GlbReportItemCat1"] = vItemCat1 == "" ? txtCat1.Text == "" ? txtCat1.Text : "^" + txtCat1.Text + "$" : vItemCat1;
                Session["GlbReportItemCat2"] = vItemCat2 == "" ? txtCat2.Text == "" ? txtCat2.Text : "^" + txtCat2.Text + "$" : vItemCat2;
                Session["GlbReportItemCat3"] = vItemCat3 == "" ? txtCat3.Text == "" ? txtCat3.Text : "^" + txtCat3.Text + "$" : vItemCat3;
                Session["GlbReportItemCat4"] = vItemCat4 == "" ? txtCat4.Text == "" ? txtCat4.Text : "^" + txtCat4.Text + "$" : vItemCat4;
                Session["GlbReportItemCat5"] = vItemCat5 == "" ? txtCat5.Text == "" ? txtCat5.Text : "^" + txtCat5.Text + "$" : vItemCat5;
                Session["GlbReportItemStatus"] = txtItemStat.Text;
                Session["GlbReportDoc2"] = "";
                Session["GlbReportJobStatus"] = "";
                Session["GlbReportWithRCC"] = "";
                Session["GlbReportWithCost"] = 0;
                Session["GlbReportDocType"] = txtDocType.Text;
                //Session["GlbReportDocSubType"] = txtDocSubType.Text;

                Session["GlbReportFromDate"] = Convert.ToDateTime(txtFromDate.Text).Date;
                Session["GlbReportToDate"] = Convert.ToDateTime(txtToDate.Text).Date;
                Session["GlbReportAsAtDate"] = Convert.ToDateTime(txtAsAt.Text).Date;
                Session["GlbReportSupplier"] = txtSupplier.Text.ToString();

                //Session["GlbReportCompCode"] = Session["UserCompanyCode"].ToString();
                Session["GlbReportComp"] = Session["UserCompanyCode"].ToString();
                Session["GlbReportProfit"] = Session["UserDefLoca"].ToString();

                Session["GlbReportCompName"] = com_desc;
                Session["GlbReportCompanies"] = com_cds;

                Session["GlbReportDirection"] = string.Empty;
                if (txtDirection.Text == "IN")
                { BaseCls.GlbReportDirection = "1"; Session["GlbReportDirection"] = "1"; }

                if (txtDirection.Text == "OUT")
                { BaseCls.GlbReportDirection = "0"; Session["GlbReportDirection"] = "0"; }

                InvReportPara _invRepPara = new InvReportPara();
                _invRepPara._GlbReportBrand = Session["GlbReportBrand"].ToString();
                _invRepPara._GlbReportModel = Session["GlbReportModel"].ToString();
                _invRepPara._GlbReportItemCode = Session["GlbReportItemCode"].ToString();
                _invRepPara._GlbReportItemCat1 = Session["GlbReportItemCat1"].ToString();
                _invRepPara._GlbReportItemCat2 = Session["GlbReportItemCat2"].ToString();
                _invRepPara._GlbReportItemCat3 = Session["GlbReportItemCat3"].ToString();
                _invRepPara._GlbReportItemCat4 = Session["GlbReportItemCat4"].ToString();
                _invRepPara._GlbReportItemCat5 = Session["GlbReportItemCat5"].ToString();
                _invRepPara._GlbReportItemStatus = Session["GlbReportItemStatus"].ToString();
                _invRepPara._GlbReportDoc2 = Session["GlbReportDoc2"].ToString();
                _invRepPara._GlbReportJobStatus = Session["GlbReportJobStatus"].ToString();
                _invRepPara._GlbReportWithRCC = Session["GlbReportWithRCC"].ToString();
                _invRepPara._GlbReportWithCost = Convert.ToInt16(Session["GlbReportWithCost"]);
                _invRepPara._GlbReportDocType = Session["GlbReportDocType"].ToString();
                _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
                //_invRepPara._GlbReportDocSubType = Session["GlbReportDocSubType"].ToString();
                _invRepPara._GlbReportCompName = Session["GlbReportCompName"].ToString();
                _invRepPara._GlbReportCompanies = Session["GlbReportCompanies"].ToString();

                _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                _invRepPara._GlbReportSupplier = txtSupplier.Text.ToString();
                _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
           //     _invRepPara._GlbReportProfit = ucProfitCenterSearch.TxtProfCenter.Text == "" ? "All Profit centers based on user rights" : ucProfitCenterSearch.TxtProfCenter.Text;
                _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
                _invRepPara._GlbUserID = Session["UserID"].ToString();
                _invRepPara._GlbReportWithJob = "N";
                _invRepPara._GlbReportCheckRegDate = 0;
                _invRepPara._GlbReportCompCode = Session["GlbReportCompCode"].ToString();
                _invRepPara._GlbReportComp = com_desc;
                _invRepPara._GlbReportCompAddr = com_add;

                int i = 1;
                int j = listGroup.Items.Count;
                _invRepPara._GlbReportGroupPC = 0;
                _invRepPara._GlbReportGroupDLoc = 0;
                _invRepPara._GlbReportGroupDocTp = 0;
                _invRepPara._GlbReportGroupCust = 0;
                _invRepPara._GlbReportGroupExec = 0;
                _invRepPara._GlbReportGroupItem = 0;
                _invRepPara._GlbReportGroupBrand = 0;
                _invRepPara._GlbReportGroupModel = 0;
                _invRepPara._GlbReportGroupCat1 = 0;
                _invRepPara._GlbReportGroupCat2 = 0;
                _invRepPara._GlbReportGroupCat3 = 0;
                _invRepPara._GlbReportGroupCat4 = 0;
                _invRepPara._GlbReportGroupCat5 = 0;
                _invRepPara._GlbReportGroupLastGroup = 0;
                _invRepPara._GlbReportGroupDocNo = 0;
                _invRepPara._GlbReportGroupStockTp = 0;
                _invRepPara._GlbReportGroupPromotor = 0;
                _invRepPara._GlbReportGroupJobNo = 0;
                _invRepPara._GlbReportGroupLastGroupCat = "";

                foreach (ListItem Item in listGroup.Items)
                {
                    if (Item.Text == "PC")
                    {
                        _invRepPara._GlbReportGroupPC = i;
                    }
                    if (Item.Text == "DLOC")
                    {
                        _invRepPara._GlbReportGroupDLoc = i;
                    }
                    if (Item.Text == "DTP")
                    {
                        _invRepPara._GlbReportGroupDocTp = i;
                    }
                    if (Item.Text == "CUST")
                    {
                        _invRepPara._GlbReportGroupCust = i;
                    }
                    if (Item.Text == "EXEC")
                    {
                        _invRepPara._GlbReportGroupExec = i;
                    }
                    if (Item.Text == "ITM")
                    {
                        _invRepPara._GlbReportGroupItem = i;
                    }
                    if (Item.Text == "BRND")
                    {
                        _invRepPara._GlbReportGroupBrand = i;
                    }
                    if (Item.Text == "MDL")
                    {
                        _invRepPara._GlbReportGroupModel = i;
                    }
                    if (Item.Text == "CAT1")
                    {
                        _invRepPara._GlbReportGroupCat1 = i;
                    }
                    if (Item.Text == "CAT2")
                    {
                        _invRepPara._GlbReportGroupCat2 = i;
                    }
                    if (Item.Text == "CAT3")
                    {
                        _invRepPara._GlbReportGroupCat3 = i;
                    }
                    if (Item.Text == "CAT4")
                    {
                        _invRepPara._GlbReportGroupCat4 = i;
                    }
                    if (Item.Text == "CAT5")
                    {
                        _invRepPara._GlbReportGroupCat5 = i;
                    }
                    if (Item.Text == "DOC")
                    {
                        _invRepPara._GlbReportGroupDocNo = i;
                    }
                    if (Item.Text == "STK")
                    {
                        _invRepPara._GlbReportGroupStockTp = i;
                    }
                    if (Item.Text == "PRM")
                    {
                        _invRepPara._GlbReportGroupPromotor = i;
                    }

                    _invRepPara._GlbReportGroupLastGroup = j;
                    if (j == i)
                    {
                        _invRepPara._GlbReportGroupLastGroupCat = Item.Text;
                    }
                    i++;
                }
                if (j == 0)
                {
                    _invRepPara._GlbReportGroupPC = 1;
                    _invRepPara._GlbReportGroupItem = 2;
                    _invRepPara._GlbReportGroupLastGroup = 2;
                    _invRepPara._GlbReportGroupLastGroupCat = "ITM";
                }

                _opt = "";


                string _export = "N";
                clsInventory obj = new clsInventory();
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = "";
                if (rbpdf.Checked)
                {
                    targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                }
                if (rbexel.Checked)
                {
                    targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".xls";
                }
                if (rbexeldata.Checked)
                {
                    targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".xls";
                }


                //Lakshika 2016-10-04
                if (rad26.Checked == true)
                {
                    _opt = "rad26";
                    _isSelected = true;
                    Session["docNo"] = txtDocNo.Text.Trim();
                    Session["ItemCode"] = txtItemCode.Text.Trim();
                    Session["GlbReportName"] = "Dispatch_Req_Report.rpt";
                    Session["GlbReportType"] = "";
                    Session["InvReportPara"] = _invRepPara;

                    _invRepPara._GlbCompany = Session["GlbReportCompanies"].ToString();//Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbLocation = "";
                    _invRepPara._GlbReportSupplier = txtSupplier.Text;
                    _invRepPara._GlbReportChannel = ucLoactionSearch.Channel;
                    
                    update_Location_List_RPTDB(); //save locations tempory

                    obj.PurchaseReturnReport(_invRepPara);
                    PrintPDF(targetFileName, obj._purchaseReturns);

                }
                //Item Wise Stationery Requirements Report - Udaya 16.08.2017
                if (rad27.Checked == true)
                {
                    //Session["Company"] = ucLoactionSearch.Company;
                    //if (Session["Company"] == null | Session["Company"].ToString() == "")
                    //{
                    //    displayMessage("Please select Companay");
                    //}
                    _opt = "rad27";
                    _isSelected = true;
                    Session["GlbReportName"] = "ItemWiseStationeryRequirements.rpt";
                    Session["UserCompanyCode"] = _invRepPara._GlbReportCompCode;
                    _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                    _invRepPara._GlbCompany = _newCom.Mc_desc;
                    _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    update_Location_List_RPTDB(); //save locations tempory

                    DataTable _result = obj.Requirment_Details(_invRepPara, "I");
                    if (_result.Rows.Count > 0)
                    {
                        PrintPDF(targetFileName, obj._ItemWiseStationeryRequirements);
                    }
                    else
                    {
                        displayMessage("No records to view...!!!");
                        _export = "N/A";
                    }
                }

                //Dept Wise Stationery Requirements Report - Udaya 19.08.2017
                if (rad28.Checked == true)
                {
                    _opt = "rad28";
                    _isSelected = true;
                    Session["GlbReportName"] = "ItemWiseStationeryRequirements.rpt";
                    Session["UserCompanyCode"] = _invRepPara._GlbReportCompCode;
                    _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                    _invRepPara._GlbCompany = _newCom.Mc_desc;
                    _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    update_Location_List_RPTDB(); //save locations tempory

                    DataTable _result = obj.Requirment_Details(_invRepPara, "D");
                    if (_result.Rows.Count > 0)
                    {
                        PrintPDF(targetFileName, obj._ItemWiseStationeryRequirements);
                    }
                    else
                    {
                        displayMessage("No records to view...!!!");
                        _export = "N/A";
                    }
                }
                //Supplier Prices Report - Udaya 21.08.2017
                if (rad29.Checked == true)
                {
                    _opt = "rad29";
                    _isSelected = true;
                    Session["GlbReportName"] = "SupplierPrices.rpt";
                    Session["UserCompanyCode"] = _invRepPara._GlbReportCompCode;
                    _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                    _invRepPara._GlbCompany = _newCom.Mc_desc;
                    _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    update_Location_List_RPTDB(); //save locations tempory

                    DataTable _result = obj.Supplier_Pricess_Details(_invRepPara);
                    if (_result.Rows.Count > 0)
                    {
                        PrintPDF(targetFileName, obj._SupplierPrices);
                    }
                    else
                    {
                        displayMessage("No records to view...!!!");
                        _export = "N/A";
                    }
                }
                if (_export == "N")
                {
                    String url = "";
                    if (rbpdf.Checked)
                    {
                        url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    }
                    if (rbexel.Checked)
                    {
                        url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xls','_blank');</script>";
                    }
                    if (rbexeldata.Checked)
                    {
                        url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xls','_blank');</script>";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }

                lbtnView.Enabled = true;
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "inventory", "Run Ok", Session["UserID"].ToString());

               
            }

            catch (Exception err)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "inventory", err.Message, Session["UserID"].ToString());
                CHNLSVC.CloseChannel();
                Response.Redirect("~/Error.aspx?Error=" + err.Message + "");
                lbtnView.Enabled = true;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);

            string path = @"C:\Download_excel";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (file.Exists)
            {


                System.IO.File.Copy(@"\\" + _filePath, "C:/Download_excel/" + filenamenew + ".xlsx", true);
            }
            else
            {
                DisplayMessage("This file does not exist.", 1);
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
                if (rbpdf.Checked)
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                }
                if (rbexel.Checked)
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.Excel;
                }
                if (rbexeldata.Checked)
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.Excel;
                }
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

        private void update_Location_List_RPTDB()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, null, null);

            foreach (GridViewRow Item in dgvLocation.Rows)
            {
                Label lblLocation = (Label)Item.FindControl("lblLocation");
                CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                string loc = lblLocation.Text.ToUpper();

                if (chkLocation.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, null, loc);

                    _isPCFound = true;
                    if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
                    {
                        BaseCls.GlbReportProfit = loc;
                    }
                    else
                    {
                        //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
                        BaseCls.GlbReportProfit = "All Locations Based on User Rights";
                    }
                }
            }

            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = ucLoactionSearch.ProfitCenter.ToUpper();
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, ucLoactionSearch.ProfitCenter.ToUpper(), null);
            }
        }
        //private void update_LOC_List_RPTDB()
        //{
        //    string _tmpPC = "";
        //    BaseCls.GlbReportProfit = "";

        //    Boolean _isPCFound = false;
        //    Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), "ABL", null, null);

        //    foreach (GridViewRow Item in dgvLocation.Rows)
        //    {
        //        Label lblLocation = (Label)Item.FindControl("lblLocation");
        //        CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
        //        string pc = lblLocation.Text;

        //        if (chkLocation.Checked == true)
        //        {
        //            Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, pc, null);

        //            _isPCFound = true;
        //            if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
        //            {
        //                BaseCls.GlbReportProfit = pc;
        //            }
        //            else
        //            {
        //                //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
        //                BaseCls.GlbReportProfit = "All Locations Based on User Rights";
        //            }
        //        }
        //    }

        //    if (_isPCFound == false)
        //    {
        //        BaseCls.GlbReportProfit = "";
        //        Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, ucLoactionSearch.ProfitCenter.ToUpper(), null);
        //    }
        //}
        private void LoadEmptyGrid()
        {
            dgvCompany.DataSource = null;
            dgvCompany.DataBind();
            dgvAdminTeam.DataSource = null;
            dgvAdminTeam.DataBind();
        }
        private void BindAdminTeam()
        {
            Session["DATAADMIN"] = null;
            DataTable dt = new DataTable();
            dgvAdminTeam.DataSource = null;
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdminTeam);
            dt = CHNLSVC.CommonSearch.GetAdminTeamByCompany(para, null, null);
            if (dt.Rows.Count > 0)
            {
                System.Data.DataColumn newColumn = new System.Data.DataColumn("flag", typeof(System.Int32));
                newColumn.DefaultValue = "0";
                dt.Columns.Add(newColumn);
                Session["DATAADMIN"] = dt;
                dgvAdminTeam.DataSource = (DataTable)Session["DATAADMIN"];
            }
            dgvAdminTeam.DataBind();
        }
        private void BindCompany(string userName)
        {
            dgvCompany.DataSource = null;
            if (!string.IsNullOrEmpty(userName))
            {
                string intime = DateTime.Now.ToString();
                List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(userName);

                if (_systemUserCompanyList != null)
                {
                    dgvCompany.DataSource = _systemUserCompanyList.OrderBy(c => c.SEC_COM_CD).ToList();
                }
            }
            dgvCompany.DataBind();
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdminTeam:
                    {
                        string com_cds = "";
                        foreach (GridViewRow row in dgvCompany.Rows)
                        {
                            CheckBox chk = (CheckBox)row.FindControl("chkCompanyCode");
                            if (chk != null && chk.Checked)
                            {
                                if (com_cds != "")
                                {
                                    com_cds = com_cds + ",";
                                }
                                Label lblCode = (Label)row.FindControl("lblCode");
                                string com_cd = lblCode.Text;
                                com_cds = com_cds + com_cd;
                            }
                        }
                        paramsText.Append(com_cds);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        string com_cds = "";
                        foreach (GridViewRow row in dgvCompany.Rows)
                        {
                            CheckBox chk = (CheckBox)row.FindControl("chkCompanyCode");
                            if (chk != null && chk.Checked)
                            {
                                if (com_cds != "")
                                {
                                    com_cds = com_cds + ",";
                                }
                                string com_cd = row.Cells[1].Text;
                                com_cds = com_cds + com_cd;
                            }
                        }
                        paramsText.Append(com_cds);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtCat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub3:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text.Trim() + seperator + txtCat3.Text.Trim() + seperator + txtCat4.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub4:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text.Trim() + seperator + txtCat3.Text.Trim() + seperator + txtCat4.Text.Trim() + seperator + txtCat5.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementTypes:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append(txtDocType.Text.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InventoryDirection:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BrandManager:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void chkCompanyCode_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtCompany = (DataTable)Session["DATACOMPANY"];
            GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
            Label compCode = (Label)Row.FindControl("lblCode");
            CheckBox chkSelect = (CheckBox)Row.FindControl("chkCompanyCode");
            bool select = chkSelect.Checked;
            string cellvalue = compCode.Text;

            Session["DATACOMPANY"] = dtCompany;
            BindAdminTeam();
            if (chkAllCompany.Checked)
            {
                ucLoactionSearch.ClearText();
            }
            else
            {
                foreach (GridViewRow hiderowbtn in this.dgvCompany.Rows)
                {
                    CheckBox chkCompanyCode = (CheckBox)hiderowbtn.FindControl("chkCompanyCode");

                    if (chkCompanyCode.Checked == true)
                    {
                        chkCompanyCode.Checked = false;
                    }
                }
                if (select)
                {
                    foreach (GridViewRow hiderowbtn in this.dgvCompany.Rows)
                    {
                        CheckBox chkCompanyCode = (CheckBox)hiderowbtn.FindControl("chkCompanyCode");
                        Label code = (Label)hiderowbtn.FindControl("lblCode");
                        if (code.Text == cellvalue)
                        {
                            chkCompanyCode.Checked = true;
                        }
                    }
                }
                CheckBox chk = (CheckBox)sender;
                if (chk != null && chk.Checked)
                {
                    ucLoactionSearch.Company = cellvalue;
                }
                else
                {
                    ucLoactionSearch.ClearText();
                }
            }
        }

        protected void dgvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvCompany.PageIndex = e.NewPageIndex;
                DataTable dt = (DataTable)Session["DATACOMPANY"];
                if (dt.Rows.Count > 0)
                {
                    dgvCompany.DataSource = dt;
                    dgvCompany.DataBind();
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

        protected void dgvAdminTeam_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvAdminTeam.PageIndex = e.NewPageIndex;
                DataTable dt = (DataTable)Session["DATAADMIN"];
                if (dt.Rows.Count > 0)
                {
                    dgvAdminTeam.DataSource = dt;
                    dgvAdminTeam.DataBind();
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
        public void BindYear()
        {
            ddlYear.DataSource = null;
            int sysYear = 2012;
            int yearCount = DateTime.Today.Year - 2011;
            int[] yearList = new int[yearCount];
            for (int x = 0; x < yearCount; x++)
            {
                yearList[x] = sysYear;
                sysYear++;
            }
            ddlYear.DataSource = yearList;
            ddlYear.DataBind();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(DateTime.Today.Year.ToString()));
            ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(DateTime.Today.Month.ToString()));
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

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                Session["CAT_Main"] = null;
                Session["CAT_Sub1"] = null;
                Session["CAT_Sub2"] = null;
                Session["CAT_Sub3"] = null;
                Session["CAT_Sub4"] = null;
                Session["Item"] = null;
                Session["Brand"] = null;
                Session["Model"] = null;
                Session["MovementTypes"] = null;
                Session["DocSubType"] = null;
                Session["InventoryDirection"] = null;
                Session["ItemStatus"] = null;
                Session["BrandManager"] = null;
                Session["Supplier"] = null;

                if (lblSearchType.Text == "CAT_Main")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    dt = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Main"] = dt;
                }
                if (lblSearchType.Text == "CAT_Sub1")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    dt = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub1"] = dt;
                }
                if (lblSearchType.Text == "CAT_Sub2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    dt = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub2"] = dt;
                }
                if (lblSearchType.Text == "CAT_Sub3")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                    dt = CHNLSVC.General.GetItemSubCat4(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub3"] = dt;
                }
                if (lblSearchType.Text == "CAT_Sub4")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                    dt = CHNLSVC.General.GetItemSubCat5(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub4"] = dt;
                }
                if (lblSearchType.Text == "Item")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    dt = CHNLSVC.CommonSearch.GetItemSearchDataMaster(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Item"] = dt;
                }
                if (lblSearchType.Text == "Brand")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    dt = CHNLSVC.CommonSearch.GetItemBrands(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Brand"] = dt;
                }
                if (lblSearchType.Text == "Model")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    dt = CHNLSVC.CommonSearch.GetAllModels(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Model"] = dt;
                }
                if (lblSearchType.Text == "MovementTypes")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                    dt = CHNLSVC.CommonSearch.GetMovementTypes(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["MovementTypes"] = dt;
                }
                if (lblSearchType.Text == "DocSubType")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                    dt = CHNLSVC.CommonSearch.GetMovementDocSubTypes(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["DocSubType"] = dt;
                }
                if (lblSearchType.Text == "InventoryDirection")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryDirection);
                    dt = CHNLSVC.CommonSearch.GetInventoryDirections(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["InventoryDirection"] = dt;
                }
                if (lblSearchType.Text == "ItemStatus")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                    dt = CHNLSVC.CommonSearch.GetCompanyItemStatusData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ItemStatus"] = dt;
                }
                if (lblSearchType.Text == "BrandManager")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BrandManager);
                    dt = CHNLSVC.CommonSearch.GetBrandManager(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BrandManager"] = dt;
                }
                if (lblSearchType.Text == "Supplier")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    dt = CHNLSVC.CommonSearch.GetSupplierData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Supplier"] = dt;
                }

                dgvResultItem.DataSource = null;
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                if (dt.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = dt;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResultItem.DataBind();
                ItemPopup.Show();
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

        protected void dgvResultItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResultItem.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "CAT_Main")
                {
                    _result = (DataTable)Session["CAT_Main"];
                }
                if (lblSearchType.Text == "CAT_Sub1")
                {
                    _result = (DataTable)Session["CAT_Sub1"];
                }
                if (lblSearchType.Text == "CAT_Sub2")
                {
                    _result = (DataTable)Session["CAT_Sub2"];
                }
                if (lblSearchType.Text == "CAT_Sub3")
                {
                    _result = (DataTable)Session["CAT_Sub3"];
                }
                if (lblSearchType.Text == "CAT_Sub4")
                {
                    _result = (DataTable)Session["CAT_Sub4"];
                }
                if (lblSearchType.Text == "Item")
                {
                    _result = (DataTable)Session["Item"];
                }
                if (lblSearchType.Text == "Brand")
                {
                    _result = (DataTable)Session["Brand"];
                }
                if (lblSearchType.Text == "Model")
                {
                    _result = (DataTable)Session["Model"];
                }
                if (lblSearchType.Text == "MovementTypes")
                {
                    _result = (DataTable)Session["MovementTypes"];
                }
                if (lblSearchType.Text == "DocSubType")
                {
                    _result = (DataTable)Session["DocSubType"];
                }
                if (lblSearchType.Text == "InventoryDirection")
                {
                    _result = (DataTable)Session["InventoryDirection"];
                }
                if (lblSearchType.Text == "ItemStatus")
                {
                    _result = (DataTable)Session["ItemStatus"];
                }
                if (lblSearchType.Text == "BrandManager")
                {
                    _result = (DataTable)Session["BrandManager"];
                }
                if (lblSearchType.Text == "Supplier")
                {
                    _result = (DataTable)Session["Supplier"];
                }

                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;

                }
                else
                {
                    dgvResultItem.DataSource = null;
                } dgvResultItem.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                ItemPopup.Show();

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


        protected void dgvResultItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript",
                     "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvResultItem.SelectedRow.Cells[1].Text;
                if (lblSearchType.Text == "CAT_Main")
                {
                    txtCat1.Text = code;
                    chkAllCat1.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub1")
                {
                    txtCat2.Text = code;
                    chkAllCat2.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub2")
                {
                    txtCat3.Text = code;
                    chkAllCat3.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub3")
                {
                    txtCat4.Text = code;
                    chkAllCat4.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub4")
                {
                    txtCat5.Text = code;
                    chkAllCat5.Checked = false;
                }
                else if (lblSearchType.Text == "Item")
                {
                    txtItemCode.Text = code;
                    chkAllItemCode.Checked = false;
                }
                else if (lblSearchType.Text == "Brand")
                {
                    txtBrand.Text = code;
                    chkAllBrand.Checked = false;
                }
                else if (lblSearchType.Text == "Model")
                {
                    txtModel.Text = code;
                    chkAllModel.Checked = false;
                }
                if (lblSearchType.Text == "MovementTypes")
                {
                    txtDocType.Text = code;
                    chkAllDocType.Checked = false;
                }
                if (lblSearchType.Text == "DocSubType")
                {
                    txtDocSubType.Text = code;
                    chkAllDocSubType.Checked = false;
                }
                if (lblSearchType.Text == "InventoryDirection")
                {
                    txtDirection.Text = code;
                    chkAllDirNo.Checked = false;
                }
                if (lblSearchType.Text == "ItemStatus")
                {
                    txtItemStat.Text = dgvResultItem.SelectedRow.Cells[2].Text; ;
                    chkAllStat.Checked = false;
                }
                if (lblSearchType.Text == "BrandManager")
                {
                    txtBrandMan.Text = code;
                    chkAllBrandMan.Checked = false;
                }
                if (lblSearchType.Text == "Supplier")
                {
                    txtSupplier.Text = code;
                    chkAllSupplier.Checked = false;
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


        #region start
        protected void lbtnSeCat1_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Main";
                Session["CAT_Main"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Main"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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



        protected void lbtnSeCat2_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub1";
                Session["CAT_Sub1"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub1"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeCat3_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub2";
                Session["CAT_Sub2"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub2"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeCat4_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub3";
                Session["CAT_Sub3"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _result = CHNLSVC.General.GetItemSubCat4(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub3"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeCat5_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub4";
                Session["CAT_Sub4"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                DataTable _result = CHNLSVC.General.GetItemSubCat5(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub4"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeItemCode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Item";
                Session["Item"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Item"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtSeBrand_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Brand";
                Session["Brand"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Brand"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeModel_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Model";
                Session["Model"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Model"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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



        protected void chkAllCat1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat1.Checked)
            {
                txtCat1.Enabled = false;
                txtCat1.Text = "";
                listCat1.Items.Clear();
            }
            else
            {
                txtCat1.Enabled = true;
            }
        }
        protected void chkAllCat2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat2.Checked)
            {
                txtCat2.Enabled = false;
                txtCat2.Text = "";
                listCat2.Items.Clear();
            }
            else
            {
                txtCat2.Enabled = true;
            }
        }

        protected void chkAllCat3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat3.Checked)
            {
                txtCat3.Enabled = false;
                txtCat3.Text = "";
                listCat3.Items.Clear();
            }
            else
            {
                txtCat3.Enabled = true;
            }
        }

        protected void chkAllCat4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat4.Checked)
            {
                txtCat4.Enabled = false;
                txtCat4.Text = "";
                listCat4.Items.Clear();
            }
            else
            {
                txtCat4.Enabled = true;
            }
        }

        protected void chkAllCat5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat5.Checked)
            {
                txtCat5.Enabled = false;
                txtCat5.Text = "";
                listCat5.Items.Clear();
            }
            else
            {
                txtCat5.Enabled = true;
            }
        }

        protected void chkAllItemCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllItemCode.Checked)
            {
                txtItemCode.Enabled = false;
                txtItemCode.Text = "";
                listItemCode.Items.Clear();
            }
            else
            {
                txtItemCode.Enabled = true;
            }
        }

        protected void chkAllBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBrand.Checked)
            {
                txtBrand.Enabled = false;
                txtBrand.Text = "";
                listBrand.Items.Clear();
            }
            else
            {
                txtBrand.Enabled = true;
            }
        }

        protected void chkAllModel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllModel.Checked)
            {
                txtModel.Enabled = false;
                txtModel.Text = "";
                listModel.Items.Clear();
            }
            else
            {
                txtModel.Enabled = true;
            }
        }


        protected void chkAllOrPlNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllOrPlNo.Checked)
            //{
            //    txtOrderPlanNo.Text = "";
            //}
        }

        protected void chkAllBankNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllBankNo.Checked)
            //{
            //    txtBankNo.Text = "";
            //}
        }

        protected void chkAllPLNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllPLNo.Checked)
            //{
            //    txtPlNo.Text = "";
            //}
        }

        protected void chkAllLcNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllLcNo.Checked)
            //{
            //    txtLcNo.Text = "";
            //}
        }

        protected void chkAllBlNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllBlNo.Checked)
            //{
            //    txtBlNo.Text = "";
            //}
        }

        protected void chkAllGrnNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllGrnNo.Checked)
            //{
            //    txtGrnNo.Text = "";
            //}
        }

        protected void chkAllEntryNo_CheckedChanged(object sender, EventArgs e)
        {
            //    if (chkAllEntryNo.Checked)
            //    {
            //        txtEntryNo.Text = "";
            //    }
        }
        protected void chkAllBrandMan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBrandMan.Checked)
            {
                txtBrandMan.Text = "";
            }
        }
        protected void lbtnAddCat1_Click(object sender, EventArgs e)
        {
            if (txtCat1.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat1.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat1.Items)
                {
                    if (list.Text == txtCat1.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat1.Text);
                        listCat1.Items.Add(listItem);
                        chkAllCat1.Checked = false;
                        txtCat1.Text = "";
                    }
                    else
                    {
                        txtCat1.Text = "";
                        txtCat1.Focus();
                        displayMessage("Please select a valid item for category 1");
                    }
                }
                else
                {
                    txtCat1.Focus();
                    displayMessage(txtCat1.Text + " is already added");
                }
            }
            else
            {
                txtCat1.Focus();
                displayMessage("Please select category 1");
            }
        }

        protected void lbtnAddCat2_Click(object sender, EventArgs e)
        {
            if (txtCat2.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat2.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat2.Items)
                {
                    if (list.Text == txtCat2.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat2.Text);
                        listCat2.Items.Add(listItem);
                        chkAllCat2.Checked = false;
                        txtCat2.Text = "";
                    }
                    else
                    {
                        txtCat2.Text = "";
                        txtCat2.Focus(); displayMessage("Please select a valid item for category 2");
                    }
                }
                else
                {
                    txtCat2.Focus(); displayMessage(txtCat2.Text + " is already added");

                }
            }
            else
            {
                txtCat2.Focus(); displayMessage("Please select category 2");

            }
        }

        protected void lbtnAddCat3_Click(object sender, EventArgs e)
        {
            if (txtCat3.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat3.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat3.Items)
                {
                    if (list.Text == txtCat3.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat3.Text);
                        listCat3.Items.Add(listItem);
                        chkAllCat3.Checked = false;
                        txtCat3.Text = "";
                    }
                    else
                    {
                        txtCat3.Text = "";
                        txtCat3.Focus(); displayMessage("Please select a valid item for category 3");

                    }
                }
                else
                {
                    txtCat3.Focus(); displayMessage(txtCat3.Text + " is already added");

                }
            }
            else
            {
                txtCat3.Focus(); displayMessage("Please select category 3");

            }
        }

        protected void lbtnAddCat4_Click(object sender, EventArgs e)
        {
            if (txtCat4.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _result = CHNLSVC.General.GetItemSubCat4(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat4.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat4.Items)
                {
                    if (list.Text == txtCat4.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat4.Text);
                        listCat4.Items.Add(listItem);
                        chkAllCat4.Checked = false;
                        txtCat4.Text = "";
                    }
                    else
                    {
                        txtCat4.Text = "";
                        txtCat4.Focus(); displayMessage("Please select a valid item for category 4");

                    }
                }
                else
                {
                    txtCat4.Text = "";
                    txtCat4.Focus(); displayMessage(txtCat4.Text + " is already added");

                }
            }
            else
            {
                txtCat4.Focus(); displayMessage("Please select category 4");

            }
        }

        protected void lbtnAddCat5_Click(object sender, EventArgs e)
        {
            if (txtCat5.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                DataTable _result = CHNLSVC.General.GetItemSubCat5(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat5.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat5.Items)
                {
                    if (list.Text == txtCat5.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat5.Text);
                        listCat5.Items.Add(listItem);
                        chkAllCat5.Checked = false;
                        txtCat5.Text = "";
                    }
                    else
                    {
                        txtCat5.Text = "";
                        txtCat5.Focus(); displayMessage("Please select a valid item for category 5");

                    }
                }
                else
                {
                    txtCat5.Focus(); displayMessage(txtCat5.Text + " is already added");

                }
            }
            else
            {
                txtCat5.Focus(); displayMessage("Please select category 5");

            }
        }

        protected void lbtnAddItemCode_Click(object sender, EventArgs e)
        {
            if (txtItemCode.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(para, "Item", txtItemCode.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Item"].ToString()))
                    {
                        if (txtItemCode.Text == row["Item"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listItemCode.Items)
                {
                    if (list.Text == txtItemCode.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtItemCode.Text);
                        listItemCode.Items.Add(listItem);
                        chkAllItemCode.Checked = false;
                        txtItemCode.Text = "";
                    }
                    else
                    {
                        txtItemCode.Text = "";
                        txtItemCode.Focus();
                        displayMessage("Please select a valid item code");
                    }
                }
                else
                {
                    displayMessage(txtItemCode.Text + " is already added");
                    txtItemCode.Focus();
                }
            }
            else
            {
                displayMessage("Please select item code ");
                txtItemCode.Focus();
            }
        }

        protected void lbtnAddBrand_Click(object sender, EventArgs e)
        {
            if (txtBrand.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtBrand.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listBrand.Items)
                {
                    if (list.Text == txtBrand.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtBrand.Text);
                        listBrand.Items.Add(listItem);
                        chkAllBrand.Checked = false;
                        txtBrand.Text = "";
                    }
                    else
                    {
                        txtBrand.Text = "";
                        txtBrand.Focus(); displayMessage("Please select a valid brand");

                    }
                }
                else
                {
                    txtBrand.Focus(); displayMessage(txtBrand.Text + " is already added");

                }
            }
            else
            {
                txtBrand.Focus(); displayMessage("Please select item brand");

            }
        }

        protected void lbtnAddModel_Click(object sender, EventArgs e)
        {
            if (txtModel.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, "Code", txtModel.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtModel.Text.ToUpper() == row["Code"].ToString())
                        {
                            b2 = true;
                            break;
                        }
                    }
                }

                foreach (ListItem list in listModel.Items)
                {
                    if (list.Text == txtModel.Text)
                    {
                        b1 = true;
                        break;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtModel.Text);
                        listModel.Items.Add(listItem);
                        chkAllModel.Checked = false;
                        txtModel.Text = "";
                    }
                    else
                    {
                        txtModel.Text = "";
                        txtModel.Focus(); displayMessage("Please select a valid model");

                    }
                }
                else
                {
                    txtModel.Focus(); displayMessage(txtModel.Text + " is already added");

                }
            }
            else
            {
                txtModel.Focus(); displayMessage("Please select item model");

            }
        }
        #endregion

        protected void lbtnSeDocType_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "MovementTypes";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementTypes(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeDocSubType_Click(object sender, EventArgs e)
        {

            try
            {
                lblSearchType.Text = "DocSubType";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeDocNo_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSeDerection_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "InventoryDirection";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryDirection);
                DataTable _result = CHNLSVC.CommonSearch.GetInventoryDirections(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeEntry_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "InventoryDirection";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryDirection);
                DataTable _result = CHNLSVC.CommonSearch.GetInventoryDirections(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeItemSta_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ItemStatus";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeBrandMan_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BrandManager";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BrandManager);
                DataTable _result = CHNLSVC.CommonSearch.GetBrandManager(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void chkAllStat_CheckedChanged(object sender, EventArgs e)
        {
            txtItemStat.Enabled = !chkAllStat.Checked;
            txtItemStat.Text = "";
        }

        protected void chkAllDocType_CheckedChanged(object sender, EventArgs e)
        {
            txtDocType.Text = "";
        }

        protected void chkAllDocSubType_CheckedChanged(object sender, EventArgs e)
        {
            txtDocSubType.Text = "";
        }

        protected void chkAllDocNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDocNo.Text = "";
        }

        protected void chkAllDirNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDirection.Text = "";
        }

        protected void chkAllEntType_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void chkAllRecType_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void chkAllBatchNo_CheckedChanged(object sender, EventArgs e)
        {
            txtBatchNo.Text = "";
        }

        protected void dgvAdminTeam_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                if (chkAllCompany.Checked)
                {
                    e.Row.Cells[1].Visible = true;
                }
                else
                {
                    e.Row.Cells[1].Visible = false;
                }
            }
        }

        protected void chkAllCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16031))
            {
                chkAllCompany.Checked = false;
                displayMessage("You don't have the permission.\nPermission Code :- 16031");
                //return;
            }
            else
            {
                if (dgvAdminTeam.Rows.Count > 0)
                {
                    BindAdminTeam();
                }
                if (!chkAllCompany.Checked)
                {
                    dgvAdminTeam.Columns[1].Visible = false;
                    foreach (GridViewRow hiderowbtn in this.dgvCompany.Rows)
                    {
                        CheckBox chkCompanyCode = (CheckBox)hiderowbtn.FindControl("chkCompanyCode");

                        if (chkCompanyCode.Checked == true)
                        {
                            chkCompanyCode.Checked = false;
                        }
                    }
                    BindAdminTeam();
                    ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                    locationPanel.Enabled = true;
                }
                else
                {
                    dgvAdminTeam.Columns[1].Visible = true;
                    foreach (GridViewRow hiderowbtn in this.dgvCompany.Rows)
                    {
                        CheckBox chkCompanyCode = (CheckBox)hiderowbtn.FindControl("chkCompanyCode");

                        if (chkCompanyCode.Checked == true)
                        {
                            chkCompanyCode.Checked = false;
                        }
                    }
                    BindAdminTeam();
                    ucLoactionSearch.ClearText();
                    locationPanel.Enabled = false;
                }
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
            }
        }
        protected void chkAllAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16032))
                {
                    chkAllAdmin.Checked = false;
                    displayMessage("You don't have the permission.\nPermission Code :- 16032");
                    return;
                }
                else
                {
                    if (!chkAllAdmin.Checked)
                    {
                        // dgvAdminTeam.Columns[1].Visible = false;
                        foreach (GridViewRow hiderowbtn in this.dgvAdminTeam.Rows)
                        {
                            CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkAdminTeam");

                            if (chkAdminTeam.Checked == true)
                            {
                                chkAdminTeam.Checked = false;
                            }
                            Label lblAdminCode = (Label)hiderowbtn.FindControl("lblAdminCode");
                            //lblAdminCode.Visible = false;
                        }
                        //ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                        //locationPanel.Enabled = true;
                    }
                    else
                    {
                        // dgvAdminTeam.Columns[1].Visible = true;
                        foreach (GridViewRow hiderowbtn in this.dgvAdminTeam.Rows)
                        {
                            CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkAdminTeam");

                            if (chkAdminTeam.Checked == true)
                            {
                                chkAdminTeam.Checked = false;
                            }
                        }
                        //ucLoactionSearch.ClearText();
                        //locationPanel.Enabled = false;
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

        protected void lbtnAddLocation_Click(object sender, EventArgs e)
        {
            try
            {
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
                if (chkAllCompany.Checked)
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10045))
                    {
                        string com_cds = "";
                        foreach (GridViewRow row in dgvCompany.Rows)
                        {
                            CheckBox chk = (CheckBox)row.FindControl("chkCompanyCode");
                            if (chk != null && chk.Checked)
                            {
                                if (com_cds != "")
                                {
                                    com_cds = com_cds + ",";
                                }
                                Label lblCode = (Label)row.FindControl("lblCode");
                                string com_cd = lblCode.Text;
                                com_cds = com_cds + com_cd;
                            }
                        }
                        DataTable dt = CHNLSVC.General.GetLocationByComs(com_cds);
                        dgvLocation.DataSource = dt;
                        dgvLocation.DataBind();
                        //foreach (DataRow drow in dt.Rows)
                        //{
                        //    chklstbox.Items.Add(drow["PROFIT_CENTER"].ToString());
                        //}
                    }
                }
                else
                {
                    if (ucLoactionSearch.Company == "")
                    {
                        displayMessage("Please select the company ");
                        return;
                    }
                    string opval = "";
                    foreach (GridViewRow Item in dgvAdminTeam.Rows)
                    {
                        Label lblAdminCode = (Label)Item.FindControl("lblAdminCode");
                        CheckBox chkAdminTeam = (CheckBox)Item.FindControl("chkAdminTeam");
                        // string pc = opcode.Text.ToUpper();

                        if (chkAdminTeam.Checked == true)
                        {
                            opval = opval + lblAdminCode.Text;
                        }
                    }
                    string adminteam = opval;

                    string com = ucLoactionSearch.Company;
                    string chanel = ucLoactionSearch.Channel;
                    string subChanel = ucLoactionSearch.SubChannel;
                    string area = ucLoactionSearch.Area;
                    string region = ucLoactionSearch.Regien;
                    string zone = ucLoactionSearch.Zone;
                    string pc = ucLoactionSearch.ProfitCenter;


                    string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();

                    //if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "REPI"))
                    //Add by Chamal 30-Aug-2013
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10045))
                    {
                       // DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);
                        DataTable dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                        dgvLocation.DataSource = dt;
                        dgvLocation.DataBind();
                        //foreach (DataRow drow in dt.Rows)
                        //{
                        //    chklstbox.Items.Add(drow["PROFIT_CENTER"].ToString());
                        //}
                    }
                    else
                    {
                       // DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep(Session["UserID"].ToString(), com, chanel, subChanel, area, region, zone, pc);
                        DataTable dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                        dgvLocation.DataSource = dt;
                        dgvLocation.DataBind();
                        //foreach (DataRow drow in dt.Rows)
                        //{
                        //    chklstbox.Items.Add(drow["PROFIT_CENTER"].ToString());
                        //}
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

        protected void lbtnLocationClear_Click(object sender, EventArgs e)
        {
            dgvLocation.DataSource = null;
            dgvLocation.DataBind();
        }

        protected void lbtnLocationAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvLocation.Rows)
                {
                    // Label lblLocation = (Label)row.FindControl("lblLocation");
                    CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");
                    chkLocation.Checked = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnLocationNone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvLocation.Rows)
                {
                    // Label lblLocation = (Label)row.FindControl("lblLocation");
                    CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");
                    chkLocation.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnClearGroup_Click(object sender, EventArgs e)
        {
            listGroup.Items.Clear();
        }
        protected void btnCat1_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT1";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 1 is already added");
                return;
            }
        }

        protected void btnCat2_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT2";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 2 is already added");
                return;
            }
        }

        protected void btnCat3_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT3";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 3 is already added");
                return;
            }
        }

        protected void btnCat4_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT4";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 4 is already added");
                return;
            }
        }

        protected void btnCat5_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT5";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 5 is already added");
                return;
            }
        }

        protected void btnItemCode_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "ITEM";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Item is already added");
                return;
            }
        }

        protected void btnBrand_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "BRND";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Brand is already added");
                return;
            }
        }

        protected void btnModel_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "MDL";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Model is already added");
                return;
            }
        }
        protected void btnDocSubType_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "DSUB";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Document sub type is already added");
                return;
            }
        }
        protected void btnDocType_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "DOC";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Document type is already added");
                return;
            }
        }
        protected void btnBrandMan_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "BMAN";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Brand manager is already added");
                return;
            }
        }
        protected void btnItemStats_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "ITMS";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Item status is already added");
                return;
            }
        }
        protected void btnLocation_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "LOC";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Location is already added");
                return;
            }
        }
        protected void chkAdminTeam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
                Label compCode = (Label)Row.FindControl("lblAdminCode");
                CheckBox chkSelect = (CheckBox)Row.FindControl("chkAdminTeam");
                bool select = chkSelect.Checked;
                string cellvalue = compCode.Text;
                if (chkAllAdmin.Checked)
                {
                    //ucLoactionSearch.ClearText();
                }
                else
                {
                    foreach (GridViewRow hiderowbtn in this.dgvAdminTeam.Rows)
                    {
                        CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkAdminTeam");
                        if (chkAdminTeam.Checked == true)
                        {
                            chkAdminTeam.Checked = false;
                        }
                    }
                    if (select)
                    {
                        foreach (GridViewRow hiderowbtn in this.dgvAdminTeam.Rows)
                        {
                            CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkAdminTeam");
                            Label code = (Label)hiderowbtn.FindControl("lblAdminCode");
                            if (code.Text == cellvalue)
                            {
                                chkAdminTeam.Checked = true;
                            }
                        }
                    }
                    CheckBox chk = (CheckBox)sender;
                    if (chk != null && chk.Checked)
                    {
                        //ucLoactionSearch.Company = cellvalue;
                    }
                    else
                    {
                        //ucLoactionSearch.ClearText();
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

        protected void lbtnSeSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Supplier";
                Session["Supplier"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Supplier"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplier.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtSupplier.Text == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Name"].ToString();
                                return;
                            }
                        }
                    }
                    txtSupplier.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid supplier !!!')", true);
                        txtSupplier.Text = "";
                        txtSupplier.Focus();
                        return;
                    }
                }
                else
                {
                    txtSupplier.ToolTip = "";
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void chkAllSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSupplier.Checked)
            {
                txtSupplier.Text = "";
            }
        }

        protected void btnSupplier_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "SUPP";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Supplier is already added");
                return;
            }
        }
        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(txtToDate.Text.ToString()), Convert.ToDateTime(txtFromDate.Text.ToString())) < 0)
            {
                displayMessage("From date cannot exceed to date");

                txtToDate.Text = "";
            }
        }

        protected void rad01_CheckedChanged(object sender, EventArgs e)
        {

        }
        
        protected void rad26_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }

        protected void btnDownloadfile_Click(object sender, EventArgs e)
        {
            DownloadFileinSever();
        }

        private void DownloadFileinSever()
        {
            // string name = "C:/Users/subodanam/Desktop/subo1.xlsx";
            string filenamenew = Session["UserID"].ToString();

            string name = "C:/Download_excel/" + filenamenew + ".xlsx";
            string filename = filenamenew + ".xlsx";
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.AddHeader("Content-Type", "application/Excel");
                Response.ContentType = "application/vnd.xls";
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.End();
            }
            else
            {
                DisplayMessage("This file does not exist.", 1);
            }
        }

        protected void rad27_CheckedChanged(object sender, EventArgs e)
        {
            Default();
        }

        protected void rad28_CheckedChanged(object sender, EventArgs e)
        {
            Default();
        }

        protected void rad29_CheckedChanged(object sender, EventArgs e)
        {
            Default();
        }
        private void Default()
        {
            if (ucLoactionSearch.txtCompany.Text == "")
            {
                ucLoactionSearch.txtCompany.Text = Session["UserCompanyCode"].ToString();
            }
            if (ucLoactionSearch.txtLocation.Text == "")
            {
                ucLoactionSearch.txtLocation.Text = Session["UserDefLoca"].ToString();
            }

            foreach (GridViewRow _row in dgvCompany.Rows)
            {
                CheckBox ItemCode = (_row.FindControl("chkCompanyCode") as CheckBox);
                Label com = (_row.FindControl("lblCode") as Label);
                if (com.Text == Session["UserCompanyCode"].ToString())
                {
                    ItemCode.Checked = true;
                }
            }
        }
    }
}