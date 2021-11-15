using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System.Collections;
using System.Web.UI;

namespace FastForward.SCMWeb.View.Reports.Imports
{
    public class clsImports : Page
    {
        public FastForward.SCMWeb.View.Reports.Imports.Order_Status_Report _ordstatus = new FastForward.SCMWeb.View.Reports.Imports.Order_Status_Report();
        public FastForward.SCMWeb.View.Reports.Imports.Total_Imports _totImports = new FastForward.SCMWeb.View.Reports.Imports.Total_Imports();
        public FastForward.SCMWeb.View.Reports.Imports.LC_Details_Bankwise_Report _lcdtl = new FastForward.SCMWeb.View.Reports.Imports.LC_Details_Bankwise_Report();
        public FastForward.SCMWeb.View.Reports.Imports.rptShipmentStatus _shipmentStatus = new FastForward.SCMWeb.View.Reports.Imports.rptShipmentStatus();
        public FastForward.SCMWeb.View.Reports.Imports.Container_volume _contVolume = new FastForward.SCMWeb.View.Reports.Imports.Container_volume();
        public FastForward.SCMWeb.View.Reports.Imports.rptImpCostAnalys _impCstAnalysis = new FastForward.SCMWeb.View.Reports.Imports.rptImpCostAnalys();
        public FastForward.SCMWeb.View.Reports.Imports.rptCostInformation _CstInformationSummery = new FastForward.SCMWeb.View.Reports.Imports.rptCostInformation();
        public FastForward.SCMWeb.View.Reports.Imports.rptImportRegister _ImptRegister = new FastForward.SCMWeb.View.Reports.Imports.rptImportRegister();
        public FastForward.SCMWeb.View.Reports.Imports.rptSLPARegister _slpaRegister = new FastForward.SCMWeb.View.Reports.Imports.rptSLPARegister();
        public FastForward.SCMWeb.View.Reports.Imports.Shipment_Schedule _shipsched = new FastForward.SCMWeb.View.Reports.Imports.Shipment_Schedule();
        public FastForward.SCMWeb.View.Reports.Imports.Import_Schedule _ImpSched = new FastForward.SCMWeb.View.Reports.Imports.Import_Schedule();
        public FastForward.SCMWeb.View.Reports.Imports.Import_Schedule_New _ImpSched_New = new FastForward.SCMWeb.View.Reports.Imports.Import_Schedule_New();
        public FastForward.SCMWeb.View.Reports.Imports.costing_sheet_report _costsheet = new FastForward.SCMWeb.View.Reports.Imports.costing_sheet_report();

        public FastForward.SCMWeb.View.Reports.Imports.costing_sheet_report_ABE _costsheet_ABE = new FastForward.SCMWeb.View.Reports.Imports.costing_sheet_report_ABE();

        public FastForward.SCMWeb.View.Reports.Imports.CusDecEntryReq _cusDecEntry = new FastForward.SCMWeb.View.Reports.Imports.CusDecEntryReq();
        public FastForward.SCMWeb.View.Reports.Imports.CusDecEntryDetails _cusDecEntryDetails = new FastForward.SCMWeb.View.Reports.Imports.CusDecEntryDetails();
        public FastForward.SCMWeb.View.Reports.Imports.profitability_report _profitability = new FastForward.SCMWeb.View.Reports.Imports.profitability_report();
        public FastForward.SCMWeb.View.Reports.Imports.Shipping_Invoice_Report _shippingInvoice = new FastForward.SCMWeb.View.Reports.Imports.Shipping_Invoice_Report();
        public FastForward.SCMWeb.View.Reports.Imports.FinancialDocDtls _FinDocDtl = new FastForward.SCMWeb.View.Reports.Imports.FinancialDocDtls();
        public FastForward.SCMWeb.View.Reports.Imports.FinancialDocSummary _FinDocSummary = new FastForward.SCMWeb.View.Reports.Imports.FinancialDocSummary();

        public FastForward.SCMWeb.View.Reports.Imports.TRNoteRequest _TRNoteRequest = new FastForward.SCMWeb.View.Reports.Imports.TRNoteRequest();

        public FastForward.SCMWeb.View.Reports.Imports.CustomAssesment _customAssesment = new FastForward.SCMWeb.View.Reports.Imports.CustomAssesment();

        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();

        Services.Base bsObj;
        public clsImports()
        {
            bsObj = new Services.Base();

        }

        public void CusDecEntryRequest(InvReportPara _objRepPara)
        {
            // kapila 
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("location", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("supplier", typeof(string));
            param.Columns.Add("agent", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["supplier"] = _objRepPara._GlbReportSupplier;
            dr["agent"] = _objRepPara._GlbReportAgent;
            dr["location"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;

            param.Rows.Add(dr);

            glbTable.Clear();

            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.CusDecEntryRequest(_objRepPara._GlbUserID, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportType, _objRepPara._GlbReportParaLine1);
            glbTable.Merge(tmp_Table);

            _cusDecEntry.Database.Tables["CusDecEntryReq"].SetDataSource(glbTable);
            _cusDecEntry.Database.Tables["param"].SetDataSource(param);
        }

        //kapila
        public void Container_Volume(InvReportPara _objRepPara)
        {
            // kapila 
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("location", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("country", typeof(string));
            param.Columns.Add("agent", typeof(string));
            param.Columns.Add("port", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["location"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;

            dr["country"] = _objRepPara._GlbReportCountry == "" ? "ALL" : _objRepPara._GlbReportCountry;
            dr["agent"] = _objRepPara._GlbReportAgent == "" ? "ALL" : _objRepPara._GlbReportAgent;
            dr["port"] = _objRepPara._GlbReportPort == "" ? "ALL" : _objRepPara._GlbReportPort;
            param.Rows.Add(dr);

            glbTable.Clear();

            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.PrintContainerVolReport(_objRepPara._GlbReportCompCode, _objRepPara._GlbReportProfit, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCountry, _objRepPara._GlbReportAgent, _objRepPara._GlbReportPort);
            glbTable.Merge(tmp_Table);

            _contVolume.Database.Tables["Container_Vol"].SetDataSource(glbTable);
            _contVolume.Database.Tables["param"].SetDataSource(param);
        }

        public void Total_Imports(InvReportPara _objRepPara)
        {
            // kapila 
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("location", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("supplier", typeof(string));
            param.Columns.Add("agent", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["supplier"] = _objRepPara._GlbReportSupplier;
            dr["agent"] = _objRepPara._GlbReportAgent;
            dr["location"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;

            param.Rows.Add(dr);

            glbTable.Clear();

            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.PrintTotalImportsReport(_objRepPara._GlbReportCompCode, _objRepPara._GlbReportProfit, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportAgent);
            glbTable.Merge(tmp_Table);

            _totImports.Database.Tables["Total_Impt"].SetDataSource(glbTable);
            _totImports.Database.Tables["param"].SetDataSource(param);
        }

        public void OrderStatus(InvReportPara _objRepPara)
        {
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("bank", typeof(string));
            param.Columns.Add("lc_no", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("item", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("supplier", typeof(string));
            //param.Columns.Add("portoforigin", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading +(_objRepPara._GlbReportDoc=="Y" ? " (BL Not Done)" : "");
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit;
            dr["bank"] = _objRepPara._GlbReportBank == "" ? "ALL" : _objRepPara._GlbReportBank;
            dr["lc_no"] = _objRepPara._GlbReportLcNo == "" ? "ALL" : _objRepPara._GlbReportLcNo;
            dr["company"] = _objRepPara._GlbReportComp;
            dr["item"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["supplier"] = _objRepPara._GlbReportSupplier == "" ? "ALL" : _objRepPara._GlbReportSupplier;
            //dr["portoforigin"] = _objRepPara._GlbReportPort;
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.getOrderStatusDetails(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbUserID, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportDoc);

            _ordstatus.Database.Tables["ORDER_STATUS"].SetDataSource(GLOB_DataTable);
            _ordstatus.Database.Tables["param"].SetDataSource(param);
        }

        public void ProfitabilityReport(InvReportPara _objRepPara)
        {
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("Company", typeof(string));
            param.Columns.Add("buyrt", typeof(decimal));
            param.Columns.Add("fcastrt", typeof(decimal));
            param.Columns.Add("costrt", typeof(decimal));
            param.Columns.Add("markup", typeof(decimal));
            param.Columns.Add("asschrge", typeof(decimal));
            param.Columns.Add("sino", typeof(string));
            param.Columns.Add("entryno", typeof(string));
            param.Columns.Add("overhead", typeof(decimal));
            param.Columns.Add("pricebk", typeof(string));
            param.Columns.Add("pblvl", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["Company"] = _objRepPara._GlbReportComp;
            dr["buyrt"] = _objRepPara._GlbReportBuyRate;
            dr["fcastrt"] = _objRepPara._GlbReportForcastRate;
            dr["costrt"] = _objRepPara._GlbReportCostRate;
            dr["markup"] = _objRepPara._GlbReportMarkup;
            dr["asschrge"] = _objRepPara._GlbReportAssemblyCharge;
            dr["sino"] = _objRepPara._GlbReportBlNo;
            dr["entryno"] = _objRepPara._GlbReportBondNo;
            dr["overhead"] = _objRepPara._GlbReportOverHead;
            dr["pricebk"] = _objRepPara._GlbReportPriceBook;
            dr["pblvl"] = _objRepPara._GlbReportPBLevel;
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();

            DataTable _pblist = new DataTable();
            _pblist = bsObj.CHNLSVC.MsgPortal.get_SelectedPB(_objRepPara._GlbReportCompCode, _objRepPara._GlbUserID);

            if (_pblist.Rows.Count > 0)
            {
                foreach (DataRow drow in _pblist.Rows)
                {
                    DataTable _temp = bsObj.CHNLSVC.MsgPortal.getProfitabilityDetails(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportBuyRate, _objRepPara._GlbReportForcastRate, _objRepPara._GlbReportCostRate,
                        _objRepPara._GlbReportMarkup, _objRepPara._GlbReportAssemblyCharge, _objRepPara._GlbReportBlNo, _objRepPara._GlbReportBondNo, _objRepPara._GlbReportOverHead, drow["rpt_pb"].ToString().Trim(), drow["rpt_pblvl"].ToString().Trim(), _objRepPara._GlbUserID);
                    GLOB_DataTable.Merge(_temp);
                }
            }
            else
            {
                GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.getProfitabilityDetails(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportBuyRate, _objRepPara._GlbReportForcastRate, _objRepPara._GlbReportCostRate,
                    _objRepPara._GlbReportMarkup, _objRepPara._GlbReportAssemblyCharge, _objRepPara._GlbReportBlNo, _objRepPara._GlbReportBondNo, _objRepPara._GlbReportOverHead, _objRepPara._GlbReportPriceBook, _objRepPara._GlbReportPBLevel, _objRepPara._GlbUserID);
            }

            _profitability.Database.Tables["PROFITABILITY_DTL"].SetDataSource(GLOB_DataTable);
            _profitability.Database.Tables["param"].SetDataSource(param);
        }

        public void ImportSchedule(InvReportPara _objRepPara)
        {
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("bank", typeof(string));
            param.Columns.Add("lc_no", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("item", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("supplier", typeof(string));
            param.Columns.Add("admin", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit;
            dr["bank"] = _objRepPara._GlbReportBank == "" ? "ALL" : _objRepPara._GlbReportBank;
            dr["lc_no"] = _objRepPara._GlbReportLcNo == "" ? "ALL" : _objRepPara._GlbReportLcNo;
            dr["company"] = _objRepPara._GlbReportComp;
            dr["item"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["supplier"] = _objRepPara._GlbReportSupplier == "" ? "ALL" : _objRepPara._GlbReportSupplier;
            dr["admin"] = _objRepPara._GlbReportDoc == "" ? "ALL" : _objRepPara._GlbReportDoc;
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.ImportScheduleReport(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportDoc, _objRepPara._GlbUserID);
            DataTable _GRNDtl = bsObj.CHNLSVC.MsgPortal.ImportScheduleGRNDtl(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportDoc, _objRepPara._GlbUserID);

            _ImpSched.Database.Tables["IMPORT_SCHEDULE"].SetDataSource(GLOB_DataTable);
            _ImpSched.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _ImpSched.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "grn_dtl")
                    {
                        ReportDocument subRepDoc = _ImpSched.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["IMPORT_SCHEDULE_GRN"].SetDataSource(_GRNDtl);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    _cs.Dispose();
                }
            }
        }
        public void ShipmentSchedule(InvReportPara _objRepPara)
        {
            // Sanjeewa 
            DataTable glbBOC = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("bank", typeof(string));
            param.Columns.Add("lc_no", typeof(string));
            param.Columns.Add("company", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit;
            dr["bank"] = _objRepPara._GlbReportBank == "" ? "ALL" : _objRepPara._GlbReportBank;
            dr["lc_no"] = _objRepPara._GlbReportLcNo == "" ? "ALL" : _objRepPara._GlbReportLcNo;
            dr["company"] = _objRepPara._GlbReportCompanies;
            param.Rows.Add(dr);

            glbBOC.Clear();
            glbBOC = bsObj.CHNLSVC.MsgPortal.BOCCustomersPrint(_objRepPara._GlbUserComCode, _objRepPara._GlbReportProfit, "", _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbUserID);

            _ordstatus.Database.Tables["BOC_CUS_RESERVE"].SetDataSource(glbBOC);
            _ordstatus.Database.Tables["param"].SetDataSource(param);
        }

        public void LCDetails(InvReportPara _objRepPara)
        {
            // Sanjeewa             
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("bank", typeof(string));
            param.Columns.Add("lc_no", typeof(string));
            param.Columns.Add("type", typeof(string));
            param.Columns.Add("company", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit ;
            dr["bank"] = _objRepPara._GlbReportBank == "" ? "ALL" : _objRepPara._GlbReportBank;
            dr["lc_no"] = _objRepPara._GlbReportLcNo == "" ? "ALL" : _objRepPara._GlbReportLcNo;
            dr["type"] = _objRepPara._GlbReportColor == "ALL" ? "ALL" : _objRepPara._GlbReportDoc;
            dr["company"] = _objRepPara._GlbReportCompanies;
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.getLCDetailsBankwise(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportBank, _objRepPara._GlbReportLcNo, _objRepPara._GlbReportDoc, _objRepPara._GlbReportColor, _objRepPara._GlbUserID);

            _lcdtl.Database.Tables["LC_DETAIL_BANK"].SetDataSource(GLOB_DataTable);
            _lcdtl.Database.Tables["param"].SetDataSource(param);
        }

        public void CostSheetDetails(InvReportPara _objRepPara)
        {
            // Sanjeewa             
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("bank", typeof(string));
            param.Columns.Add("lc_no", typeof(string));
            param.Columns.Add("company", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading + " [" + _objRepPara._GlbReportType + "]";
            dr["period"] = "";
            dr["profitcenter"] = _objRepPara._GlbReportProfit;
            dr["bank"] = "";
            dr["lc_no"] = _objRepPara._GlbReportBlNo;
            dr["company"] = _objRepPara._GlbReportCompanies;
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.getCostingSheetDetails(_objRepPara._GlbReportBlNo, _objRepPara._GlbReportType);
            DataTable _costsheetsum = bsObj.CHNLSVC.MsgPortal.getCostingSheetSumDetails(_objRepPara._GlbReportBlNo, _objRepPara._GlbReportType);
            DataTable _costsheetCont = bsObj.CHNLSVC.MsgPortal.getContainerDetailsBySI(_objRepPara._GlbReportBlNo, _objRepPara._GlbUserID);

            DataTable _costSheetParams = bsObj.CHNLSVC.MsgPortal.getCostingSheetParms(_objRepPara._GlbReportBlNo);

            if (_costSheetParams.Rows.Count>0)
            {
                string insuranceVal = _costSheetParams.Rows[0]["Full Insurance amt"].ToString();
                string shippinggurantee = _costSheetParams.Rows[0]["Shipping Guarantee"].ToString();
                string cleardDate = _costSheetParams.Rows[0]["CleardDate"].ToString();
                if(!(insuranceVal.Equals(string.Empty)))
                {
                    double insuranceValWithoutVat = Convert.ToDouble(insuranceVal);
                    insuranceValWithoutVat = insuranceValWithoutVat / 1.15;
                    _costSheetParams.Rows[0]["Full Insurance amt"] = insuranceValWithoutVat.ToString();
                }
                if ((shippinggurantee.Equals(string.Empty)))
                {
                    _costSheetParams.Rows[0]["Shipping Guarantee"] = "No";
                }
                if (!(cleardDate.Equals(string.Empty)))
                {
                    DateTime cldate = Convert.ToDateTime(cleardDate);
                    if(cldate<DateTime.Now.AddYears(-100))
                    {
                        _costSheetParams.Rows[0]["CleardDate"] =DBNull.Value;
                    }
                }                
            }

            _costsheet.Database.Tables["COST_SHEET"].SetDataSource(GLOB_DataTable);
            _costsheet.Database.Tables["param"].SetDataSource(param);
            //COST_SHEET_PARM
            _costsheet.Database.Tables["COST_SHEET_PARM"].SetDataSource(_costSheetParams);

            foreach (object repOp in _costsheet.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "costsheet_sum")
                    {
                        ReportDocument subRepDoc = _costsheet.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["COST_SHEET_SUM"].SetDataSource(_costsheetsum);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "costsheet_cont")
                    {
                        ReportDocument subRepDoc = _costsheet.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["COST_SHEET_CONT"].SetDataSource(_costsheetCont);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    _cs.Dispose();
                }
            }
        }
        
        //THarindu 2018-01-25
        public void CostSheetDetails_ABE(InvReportPara _objRepPara)
        {
            // Sanjeewa             
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("bank", typeof(string));
            param.Columns.Add("lc_no", typeof(string));
            param.Columns.Add("company", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading + " [" + _objRepPara._GlbReportType + "]";
            dr["period"] = "";
            dr["profitcenter"] = _objRepPara._GlbReportProfit;
            dr["bank"] = "";
            dr["lc_no"] = _objRepPara._GlbReportBlNo;
            dr["company"] = _objRepPara._GlbReportCompanies;
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.getCostingSheetDetails(_objRepPara._GlbReportBlNo, _objRepPara._GlbReportType);
            DataTable _costsheetsum = bsObj.CHNLSVC.MsgPortal.getCostingSheetSumDetails(_objRepPara._GlbReportBlNo, _objRepPara._GlbReportType);
            DataTable _costsheetCont = bsObj.CHNLSVC.MsgPortal.getContainerDetailsBySI(_objRepPara._GlbReportBlNo, _objRepPara._GlbUserID);

            _costsheet_ABE.Database.Tables["COST_SHEET"].SetDataSource(GLOB_DataTable);
            _costsheet_ABE.Database.Tables["param"].SetDataSource(param);

            //Added By Dulaj 2018-May-31 For Additional Coulumns for Cost Sheet Report
            //DataTable _costsheetCont = bsObj.CHNLSVC.MsgPortal.getContainerDetailsBySI(_objRepPara._GlbReportBlNo, _objRepPara._GlbUserID);

            foreach (object repOp in _costsheet.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "costsheet_sum")
                    {
                        ReportDocument subRepDoc = _costsheet_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["COST_SHEET_SUM"].SetDataSource(_costsheetsum);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "costsheet_cont")
                    {
                        ReportDocument subRepDoc = _costsheet_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["COST_SHEET_CONT"].SetDataSource(_costsheetCont);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    _cs.Dispose();
                }
            }
        }

        public void ShipmentScheduleDetails(InvReportPara _objRepPara)
        {
            // Sanjeewa             
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("bank", typeof(string));
            param.Columns.Add("lc_no", typeof(string));
            param.Columns.Add("company", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit;
            dr["bank"] = _objRepPara._GlbReportBank == "" ? "ALL" : _objRepPara._GlbReportBank;
            dr["lc_no"] = _objRepPara._GlbReportLcNo == "" ? "ALL" : _objRepPara._GlbReportLcNo;
            dr["company"] = _objRepPara._GlbReportCompanies;
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.getShipmentScheduleDetails(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportLcNo, _objRepPara._GlbUserID);
            DataTable dtContainer = bsObj.CHNLSVC.MsgPortal.getShipmentScheduleContainerDetails(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportLcNo, _objRepPara._GlbUserID);

            foreach (object repOp in _shipsched.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Container")
                    {
                        ReportDocument subRepDoc = _shipsched.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SHIPMENT_CONTAINER"].SetDataSource(dtContainer);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }

            _shipsched.Database.Tables["SHIPMENT_SCHEDULE"].SetDataSource(GLOB_DataTable);
            _shipsched.Database.Tables["param"].SetDataSource(param);
        }
        public void shipmentStatus(InvReportPara _objRepPara)
        {
            // Wimal  
            DataTable glbTable = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable shipmentSatus = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportCompCode;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            param.Rows.Add(dr);

           shipmentSatus.Clear();
           shipmentSatus = bsObj.CHNLSVC.Financial.GetSipmentDetails(_objRepPara._GlbUserComCode, _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportBlNo, "NULL", _objRepPara._GlbReportBlNo);

            _shipmentStatus.Database.Tables["MST_COM"].SetDataSource(MST_COM);            
            _shipmentStatus.Database.Tables["shipmentDetails"].SetDataSource(shipmentSatus);
            _shipmentStatus.Database.Tables["param"].SetDataSource(param);
        }

        public void importCostAnalysis(InvReportPara _objRepPara)
        {
            // Wimal  @ 03/12/2015

            DataTable impCstAnal = new DataTable();
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbUserComCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("com_desc", typeof(string));
            param.Columns.Add("variance", typeof(string));
            param.Columns.Add("companies", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbUserComCode;
            dr["com_desc"] = "N/A";
            dr["variance"] = "N/A";
            dr["companies"] = _objRepPara._GlbReportCompanies;
            param.Rows.Add(dr);


            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.Financial.getImpCstAnalDetails(_objRepPara._GlbUserComCode, _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportBlNo, _objRepPara._GlbReportBondNo);
            glbTable.Merge(tmp_Table);

            _impCstAnalysis.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _impCstAnalysis.Database.Tables["impCostingAnalys"].SetDataSource(glbTable);
            _impCstAnalysis.Database.Tables["param"].SetDataSource(param);
        }

        public void importCostInformation(InvReportPara _objRepPara)
        {
            // Wimal  @ 09/12/2015

            DataTable impCstInfor = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            DataTable MST_COM = new DataTable();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("bExRate", typeof(string));

            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbUserComCode);

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbUserComCode;
            dr["company_name"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["bExRate"] = _objRepPara._GlbReportRate;
            param.Rows.Add(dr);

            impCstInfor.Clear();
            impCstInfor = bsObj.CHNLSVC.Financial.getImpCstinfomation(_objRepPara._GlbUserComCode, _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date);

            _CstInformationSummery.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _CstInformationSummery.Database.Tables["costInformation"].SetDataSource(impCstInfor);
            _CstInformationSummery.Database.Tables["param"].SetDataSource(param);
        }

        public void importRegister(InvReportPara _objRepPara)
        {
            // Wimal  @ 09/12/2015
            //Kelum : Modification : 2016-June-07

            DataTable MST_COM = new DataTable();
            DataTable impRegister = new DataTable();
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();            
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbUserComCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("filefrom", typeof(string));
            param.Columns.Add("fileto", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportComp;
            dr["company_name"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["filefrom"] = _objRepPara._GlbReportDoc;
            dr["fileto"] = _objRepPara._GlbReportDoc2;
            param.Rows.Add(dr);

            glbTable.Clear();
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(_objRepPara._GlbUserComCode, _objRepPara._GlbUserID);

            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.Financial.getImportRegister(_objRepPara._GlbUserComCode, _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportBlNo, _objRepPara._GlbReportBondNo,_objRepPara._GlbReportDoc, _objRepPara._GlbReportDoc2);
            glbTable.Merge(tmp_Table);

            _ImptRegister.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _ImptRegister.Database.Tables["ImportRegister"].SetDataSource(glbTable);
            _ImptRegister.Database.Tables["param"].SetDataSource(param);
        }

        public void slpaRegister(InvReportPara _objRepPara)
        {
            // Wimal  @ 09/12/2015-- Modified by Sanjeewa 2016-06-15
            DataTable MST_COM = new DataTable();
            DataTable glbTable = new DataTable();
            DataTable slpaRegister = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbUserComCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("filefrom", typeof(string));
            param.Columns.Add("fileto", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportComp;
            dr["company_name"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["filefrom"] = _objRepPara._GlbReportDoc;
            dr["fileto"] = _objRepPara._GlbReportDoc2;
            param.Rows.Add(dr);

            glbTable.Clear();
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(_objRepPara._GlbUserComCode, Session["UserID"].ToString());

            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.Financial.getSLPARegister(_objRepPara._GlbReportDoc, _objRepPara._GlbReportDoc2, _objRepPara._GlbUserComCode, _objRepPara._GlbUserID);
            glbTable.Merge(tmp_Table);

            _slpaRegister.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _slpaRegister.Database.Tables["SLPA_REG"].SetDataSource(glbTable);
            _slpaRegister.Database.Tables["param"].SetDataSource(param);
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        //Lakhsika 2016/Aug/04
        public void EntryDetails(InvReportPara _objRepPara)
        {
            DataTable param = new DataTable();
            DataTable CusDecEntryDetails = new DataTable();
            DataRow dr;

            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("entrytype", typeof(string));
            param.Columns.Add("cusdecNo", typeof(string));
            param.Columns.Add("user", typeof(string));

            dr = param.NewRow();

            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["entrytype"] = _objRepPara._GlbEntryType == "" ? "" : _objRepPara._GlbEntryType.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cusdecNo"] = _objRepPara._GlbCusDecNo == "" ? "ALL" : _objRepPara._GlbCusDecNo.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["user"] = _objRepPara._GlbUserID == "" ? "" : _objRepPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');


            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            //GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.GetEntryReportDetails(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbUserID, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportDoc);
            CusDecEntryDetails = bsObj.CHNLSVC.MsgPortal.GetEntryReportDetails(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbEntryType, _objRepPara._GlbCusDecNo);
          
            
            DataTable realdata = new DataTable();
            DataRow redr;
            realdata.Columns.Add("CUH_CUSDEC_ENTRY_DT", typeof(DateTime));
            realdata.Columns.Add("CUH_CUSDEC_ENTRY_NO", typeof(string));
            realdata.Columns.Add("CUI_HS_CD", typeof(string));
            realdata.Columns.Add("CUI_MODEL", typeof(string));
            realdata.Columns.Add("CUI_ITM_DESC", typeof(string));
            realdata.Columns.Add("CUI_ORGIN_CNTY", typeof(string));
            realdata.Columns.Add("CUH_SUN_BOND_NO", typeof(string));
            realdata.Columns.Add("CUI_QTY", typeof(decimal));
            realdata.Columns.Add("CUI_GROSS_MASS", typeof(decimal));
            realdata.Columns.Add("CUI_NET_MASS", typeof(decimal));
            realdata.Columns.Add("CUI_UNIT_AMT", typeof(decimal));
            realdata.Columns.Add("ROWNUM", typeof(Int32));
            realdata.Columns.Add("cui_oth_doc_line", typeof(Int32));
            realdata.Columns.Add("CUI_ANAL_3", typeof(string));
            realdata.Columns.Add("CUI_OTH_DOC_NO", typeof(string));
            realdata.Columns.Add("CUI_UNIT_RT", typeof(string));
            
             int i = 0;
             foreach (var cushedernew2 in CusDecEntryDetails.Rows)
             {
                 string entryno = CusDecEntryDetails.Rows[i]["cui_oth_doc_no"].ToString();
                 DataTable cushedernew = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryno, Session["UserCompanyCode"].ToString());
                 if (cushedernew.Rows.Count>0)
                 {
                     redr = realdata.NewRow();
                     redr["CUH_CUSDEC_ENTRY_DT"] = Convert.ToDateTime(cushedernew.Rows[0]["cuh_cusdec_entry_dt"].ToString());
                     redr["CUH_CUSDEC_ENTRY_NO"] = cushedernew.Rows[0]["cuh_cusdec_entry_no"].ToString();
                     redr["CUI_HS_CD"] = CusDecEntryDetails.Rows[i]["CUI_HS_CD"].ToString();
                     redr["CUI_MODEL"] = CusDecEntryDetails.Rows[i]["CUI_MODEL"].ToString();
                     redr["CUI_ITM_DESC"] = CusDecEntryDetails.Rows[i]["CUI_ITM_DESC"].ToString();
                     redr["CUH_SUN_BOND_NO"] = CusDecEntryDetails.Rows[i]["CUH_SUN_BOND_NO"].ToString();
                     redr["CUI_QTY"] = Convert.ToDecimal(CusDecEntryDetails.Rows[i]["CUI_QTY"].ToString());
                     redr["CUI_GROSS_MASS"] = Convert.ToDecimal(CusDecEntryDetails.Rows[i]["CUI_GROSS_MASS"].ToString());
                     redr["CUI_NET_MASS"] = Convert.ToDecimal(CusDecEntryDetails.Rows[i]["CUI_NET_MASS"].ToString());
                     redr["CUI_UNIT_AMT"] = Convert.ToDecimal(CusDecEntryDetails.Rows[i]["CUI_UNIT_AMT"].ToString());
                     redr["ROWNUM"] = Convert.ToInt32(CusDecEntryDetails.Rows[i]["ROWNUM"].ToString());
                     redr["CUI_ORGIN_CNTY"] = CusDecEntryDetails.Rows[i]["CUI_ORGIN_CNTY"].ToString();
                     redr["cui_oth_doc_line"] = Convert.ToInt32(CusDecEntryDetails.Rows[i]["cui_oth_doc_line"].ToString());
                     redr["CUI_ANAL_3"] = CusDecEntryDetails.Rows[i]["CUI_ANAL_3"].ToString();
                     redr["CUI_OTH_DOC_NO"] = CusDecEntryDetails.Rows[i]["CUI_OTH_DOC_NO"].ToString();
                 }
                 else
                 {
                     redr = realdata.NewRow();
                     redr["CUH_CUSDEC_ENTRY_DT"] = Convert.ToDateTime(DateTime.Now.Date);
                     redr["CUH_CUSDEC_ENTRY_NO"] = "No data";
                     redr["CUI_HS_CD"] = CusDecEntryDetails.Rows[i]["CUI_HS_CD"].ToString();
                     redr["CUI_MODEL"] = CusDecEntryDetails.Rows[i]["CUI_MODEL"].ToString();
                     redr["CUI_ITM_DESC"] = CusDecEntryDetails.Rows[i]["CUI_ITM_DESC"].ToString();
                     redr["CUH_SUN_BOND_NO"] = CusDecEntryDetails.Rows[i]["CUH_SUN_BOND_NO"].ToString();
                     redr["CUI_QTY"] = Convert.ToDecimal(CusDecEntryDetails.Rows[i]["CUI_QTY"].ToString());
                     redr["CUI_GROSS_MASS"] = Convert.ToDecimal(CusDecEntryDetails.Rows[i]["CUI_GROSS_MASS"].ToString());
                     redr["CUI_NET_MASS"] = Convert.ToDecimal(CusDecEntryDetails.Rows[i]["CUI_UNIT_AMT"].ToString());
                     redr["ROWNUM"] = Convert.ToInt32(CusDecEntryDetails.Rows[i]["ROWNUM"].ToString());
                     redr["CUI_ORGIN_CNTY"] = CusDecEntryDetails.Rows[i]["CUI_ORGIN_CNTY"].ToString();
                     redr["cui_oth_doc_line"] = Convert.ToInt32(CusDecEntryDetails.Rows[i]["cui_oth_doc_line"].ToString());
                     redr["CUI_ANAL_3"] = CusDecEntryDetails.Rows[i]["CUI_ANAL_3"].ToString();
                     redr["CUI_OTH_DOC_NO"] = CusDecEntryDetails.Rows[i]["CUI_OTH_DOC_NO"].ToString();
                 }

                 realdata.Rows.Add(redr);
                 i++;
             }

             _cusDecEntryDetails.Database.Tables["CUS_DEC_ENTRY_DETAILS"].SetDataSource(realdata);
            _cusDecEntryDetails.Database.Tables["param"].SetDataSource(param);
        }

        //Lakshika 2016/Aug/23
        public void GetShippingDetailReport(InvReportPara _objRepoPara)
        {
            DataTable param = new DataTable();
            DataRow dr;

            GLOB_DataTable = new DataTable();
            DataTable shippingDetails = new DataTable();
            DataTable costDetails = new DataTable();
            shippingDetails = bsObj.CHNLSVC.MsgPortal.GetShippingDetailReport(_objRepoPara._GlbCompany, _objRepoPara._GlbDocNo); // get inv dtls



            param.Columns.Add("doc_no", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("comName", typeof(string));
            param.Columns.Add("IF_TP", typeof(string));
            param.Columns.Add("IF_SUB_TP", typeof(string));
            param.Columns.Add("IF_CRDT_PD", typeof(string));
            dr = param.NewRow();

            dr["doc_no"] = _objRepoPara._GlbDocNo == "" ? "" : _objRepoPara._GlbDocNo.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["com"] = _objRepoPara._GlbCompany == "" ? "" : _objRepoPara._GlbCompany.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["user"] = _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            DataTable comDetails = bsObj.CHNLSVC.General.GetCompanyInforDT(Session["UserCompanyCode"].ToString());
            dr["comName"] = (from DataRow drw in comDetails.Rows select (drw["MC_DESC"] == null || drw["MC_DESC"].ToString() == "") ? string.Empty : (string)drw["MC_DESC"]).FirstOrDefault();
            dr["IF_TP"] = (from DataRow drw in shippingDetails.Rows where drw["IF_TP"].ToString() != "" select (drw["IF_TP"] == null || drw["IF_TP"].ToString() == "") ? string.Empty : (string)drw["IF_TP"]).FirstOrDefault();
            dr["IF_SUB_TP"] = (from DataRow drw in shippingDetails.Rows where drw["IF_SUB_TP"].ToString() != "" select (drw["IF_SUB_TP"] == null || drw["IF_SUB_TP"].ToString() == "") ? string.Empty : (string)drw["IF_SUB_TP"]).FirstOrDefault();
            dr["IF_CRDT_PD"] = (from DataRow drw in shippingDetails.Rows where drw["IF_CRDT_PD"].ToString() != "" select Convert.ToDecimal(drw["IF_CRDT_PD"])).FirstOrDefault();
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();

            costDetails = bsObj.CHNLSVC.MsgPortal.GetCostDetailsShipInvReport(_objRepoPara._GlbDocNo.ToString()); // Get Cost Details

            //DataTable realdata = new DataTable();
            //DataRow redr;
            //realdata.Columns.Add("IBI_ITM_CD", typeof(string));
            //realdata.Columns.Add("MI_SHORTDESC", typeof(string));
            //realdata.Columns.Add("MI_MODEL", typeof(string));
            //realdata.Columns.Add("IBI_QTY", typeof(double));
            //realdata.Columns.Add("IBI_UNIT_RT", typeof(double));
            //realdata.Columns.Add("IBI_ANAL_5", typeof(string));
            //realdata.Columns.Add("IB_CUR_CD", typeof(string));
            //realdata.Columns.Add("IB_BL_REF_NO", typeof(string));
            //realdata.Columns.Add("IB_SI_SEQ_NO", typeof(string));
            //realdata.Columns.Add("IBI_FIN_NO", typeof(string));
            //realdata.Columns.Add("MBI_DESC", typeof(string));
            //realdata.Columns.Add("IF_DOC_DT", typeof(string));
            //realdata.Columns.Add("IB_ETD", typeof(string));
            //realdata.Columns.Add("IBI_CRE_DT", typeof(string));

            //int i = 0;
            //foreach (var dis in shippingDetails.Rows)
            //{
            //    redr = realdata.NewRow();

            //    redr["IBI_ITM_CD"] = shippingDetails.Rows[i]["IBI_ITM_CD"].ToString();
            //    redr["MI_SHORTDESC"] = shippingDetails.Rows[i]["MI_SHORTDESC"].ToString();
            //    redr["MI_MODEL"] = shippingDetails.Rows[i]["MI_MODEL"].ToString();
            //    redr["IBI_QTY"] = Convert.ToDouble(shippingDetails.Rows[i]["IBI_QTY"]);
            //    redr["IBI_UNIT_RT"] = Convert.ToDouble(shippingDetails.Rows[i]["IBI_UNIT_RT"]);
            //    redr["IBI_ANAL_5"] = shippingDetails.Rows[i]["IBI_ANAL_5"].ToString();
            //    redr["IB_CUR_CD"] = shippingDetails.Rows[i]["IB_CUR_CD"].ToString();
            //    redr["IB_BL_REF_NO"] = shippingDetails.Rows[i]["IB_BL_REF_NO"].ToString();
            //    redr["IB_SI_SEQ_NO"] = shippingDetails.Rows[i]["IB_SI_SEQ_NO"].ToString();
            //    redr["IBI_FIN_NO"] = shippingDetails.Rows[i]["IBI_FIN_NO"].ToString();
            //    redr["MBI_DESC"] = shippingDetails.Rows[i]["MBI_DESC"].ToString();
            //    redr["IF_DOC_DT"] = Convert.ToDateTime(shippingDetails.Rows[i]["IF_DOC_DT"]).ToShortDateString();
            //    redr["IB_ETD"] = Convert.ToDateTime(shippingDetails.Rows[i]["IB_ETD"]).ToShortDateString();
            //    redr["IBI_CRE_DT"] = Convert.ToDateTime(shippingDetails.Rows[i]["IBI_CRE_DT"]).ToShortDateString();
               
              
            //    realdata.Rows.Add(redr);
            //    i++;
            //}

            _shippingInvoice.Database.Tables["SHIPPINGINVOICERPT"].SetDataSource(shippingDetails);
            _shippingInvoice.Database.Tables["param"].SetDataSource(param);
            _shippingInvoice.Database.Tables["CHARGE_DETAIL"].SetDataSource(costDetails);


        }

        //Tharindu 2018-01-27
        public void ImportSchedule_New(InvReportPara _objRepPara)
        {
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("bank", typeof(string));
            param.Columns.Add("lc_no", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("item", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("supplier", typeof(string));
            param.Columns.Add("admin", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit;
            dr["bank"] = _objRepPara._GlbReportBank == "" ? "ALL" : _objRepPara._GlbReportBank;
            dr["lc_no"] = _objRepPara._GlbReportLcNo == "" ? "ALL" : _objRepPara._GlbReportLcNo;
            dr["company"] = _objRepPara._GlbReportComp;
            dr["item"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["supplier"] = _objRepPara._GlbReportSupplier == "" ? "ALL" : _objRepPara._GlbReportSupplier;
            dr["admin"] = _objRepPara._GlbReportDoc == "" ? "ALL" : _objRepPara._GlbReportDoc;
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.ImportScheduleReport_New(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportDoc, _objRepPara._GlbUserID);
            DataTable _GRNDtl = bsObj.CHNLSVC.MsgPortal.ImportScheduleGRNDtl_New(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportDoc, _objRepPara._GlbUserID);

            _ImpSched_New.Database.Tables["IMPORT_SCHEDULE"].SetDataSource(GLOB_DataTable);
            _ImpSched_New.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _ImpSched_New.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "grn_dtl")
                    {
                        ReportDocument subRepDoc = _ImpSched_New.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["IMPORT_SCHEDULE_GRN"].SetDataSource(_GRNDtl);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    _cs.Dispose();
                }
            }
        }

        public void FinancialDocumentDetails(InvReportPara _objRepPara)
        {
            //Wimal 24/03/2018
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("bank", typeof(string));
            param.Columns.Add("lc", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("account", typeof(string));
            param.Columns.Add("pi", typeof(string));

            //param.Columns.Add("item", typeof(string));
            //param.Columns.Add("brand", typeof(string));
            //param.Columns.Add("model", typeof(string));
            //param.Columns.Add("cat1", typeof(string));
            //param.Columns.Add("cat2", typeof(string));
            //param.Columns.Add("cat3", typeof(string));
            //param.Columns.Add("cat4", typeof(string));
            //param.Columns.Add("cat5", typeof(string));
            //param.Columns.Add("supplier", typeof(string));
            //param.Columns.Add("admin", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");          
            dr["bank"] = _objRepPara._GlbReportBank == "" ? "ALL" : _objRepPara._GlbReportBank;
            dr["lc"] = _objRepPara._GlbReportLcNo == "" ? "ALL" : _objRepPara._GlbReportLcNo;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["account"] = "ALL";
            dr["pi"] = _objRepPara._GlbReportOtherLoc == "" ? "ALL" : _objRepPara._GlbReportOtherLoc;
           
            
            //dr["item"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["supplier"] = _objRepPara._GlbReportSupplier == "" ? "ALL" : _objRepPara._GlbReportSupplier;
            //dr["admin"] = _objRepPara._GlbReportDoc == "" ? "ALL" : _objRepPara._GlbReportDoc;
            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.Get_FinancialDocDetails(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportDoc, _objRepPara._GlbUserID, _objRepPara._GlbReportLcNo, _objRepPara._GlbReportOtherLoc, _objRepPara._GlbReportBank, "", _objRepPara._GlbReportIsSummary, _objRepPara._GlbReportIsExpireDate, _objRepPara._GlbReportIsInsurancePolicyDate, _objRepPara._GlbReportIsFinanceDocDate);
            //DataTable _GRNDtl = bsObj.CHNLSVC.MsgPortal.ImportScheduleGRNDtl_New(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportDoc, _objRepPara._GlbUserID);


            //Added by Udesh 08/Oct/2018
            if (_objRepPara._GlbReportIsSummary == 0)
            {
                #region Detail Report
                _FinDocDtl.Database.Tables["FinDocDtl"].SetDataSource(GLOB_DataTable);
                _FinDocDtl.Database.Tables["param"].SetDataSource(param); 
                #endregion
            }
            else
            {
                #region Summary Report
                _FinDocSummary.Database.Tables["FinDocSummary"].SetDataSource(GLOB_DataTable);
                _FinDocSummary.Database.Tables["param"].SetDataSource(param);
                #endregion
            }

           // foreach (object repOp in _ImpSched_New.ReportDefinition.ReportObjects)
            //{
            //    string _s = repOp.GetType().ToString();
            //    if (_s.ToLower().Contains("subreport"))
            //    {
            //        SubreportObject _cs = (SubreportObject)repOp;
            //        if (_cs.SubreportName == "grn_dtl")
            //        {
            //            ReportDocument subRepDoc = _ImpSched_New.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["IMPORT_SCHEDULE_GRN"].SetDataSource(_GRNDtl);
            //            subRepDoc.Close();
            //            subRepDoc.Dispose();
            //        }

            //        _cs.Dispose();
            //    }
            //}
        }

        //dilshan on 22/03/2018
        public void TRNoteRequest(InvReportPara _objRepPara)
        {
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("location", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("supplier", typeof(string));
            param.Columns.Add("agent", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["supplier"] = _objRepPara._GlbReportSupplier;
            dr["agent"] = _objRepPara._GlbReportAgent;
            dr["location"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;

            param.Rows.Add(dr);

            glbTable.Clear();

            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.TRNoteRequest(_objRepPara._GlbUserID, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportType, _objRepPara._GlbReportParaLine1, _objRepPara._GlbDocNo, _objRepPara.Entry01,_objRepPara.Entry02);
            glbTable.Merge(tmp_Table);

            _TRNoteRequest.Database.Tables["TRNoteRequest"].SetDataSource(glbTable);
            _TRNoteRequest.Database.Tables["param"].SetDataSource(param);
        }
        public void CustomAssesment(InvReportPara _objRepPara,string typebond)
        {
            //Dulaj 2018/Oct/29
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("User", typeof(string));
            param.Columns.Add("Company", typeof(string));
            param.Columns.Add("FromDate", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));         

            dr = param.NewRow();
            dr["User"] = _objRepPara._GlbUserID;
            dr["Company"] = _objRepPara._GlbReportCompName;
        //    dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["FromDate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            param.Rows.Add(dr);

            glbTable.Clear();

            DataTable tmp_Table = new DataTable();
            //tmp_Table = bsObj.CHNLSVC.MsgPortal.Get_Custom_Assmnt_Det(_objRepPara._GlbReportCompCode, _objRepPara._GlbReportProfit, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportAgent);
            tmp_Table = bsObj.CHNLSVC.MsgPortal.Get_Custom_Assmnt_Det(_objRepPara._GlbReportCompCode, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, typebond);
            glbTable.Merge(tmp_Table);

            _customAssesment.Database.Tables["CustomAssesment"].SetDataSource(glbTable);
            _customAssesment.Database.Tables["CustomAssesmentDet"].SetDataSource(param);
        }
    }
}