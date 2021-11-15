using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
using FF.BusinessObjects.ToursNew;
using System.Reflection;

namespace FastForward.SCMWeb.View.Reports.Warehouse
{
    public class csWarehouse
    {
        public Item_Pick_Plan _pickplan = new Item_Pick_Plan();
        public Item_Pick_Plan_sum _pickplansum = new Item_Pick_Plan_sum();
        public Stock_Verification stock_verify = new Stock_Verification();
        public Dispatch_Req_Report _dispatchReqSummary = new Dispatch_Req_Report();
        public Dispatch_Req_Detail_Report _dispatchReqDetails = new Dispatch_Req_Detail_Report();
        public Dispatch_Req_Detail_Report_AAL _dispatchReqDetailsaal = new Dispatch_Req_Detail_Report_AAL();
        public Item_Dispatch_Detail_Report _itemDispatchDetails = new Item_Dispatch_Detail_Report();
        public Approved_MRN_Summary _approvedMRN = new Approved_MRN_Summary();
        public Item_Pick_Plan_sum_AAL _pickplansum_AAL = new Item_Pick_Plan_sum_AAL();
        public Courier_Dailly_Summary _courierDaillySum = new Courier_Dailly_Summary();
        public ITEM_PICK_PLAN_AAL _PICK_PLAN_AAL = new ITEM_PICK_PLAN_AAL();

        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();

        Services.Base bsObj;
        public csWarehouse()
        {
            bsObj = new Services.Base();
        }

        public void ItemPickPlanReport(InvReportPara _objRepoPara)
        {// Sanjeewa 25-05-2016
            DataTable param = new DataTable();
            DataRow dr;
            tmp_user_pc = new DataTable();
            GLOB_DataTable = new DataTable();
            DataTable _intBatch = new DataTable();
            DataTable _intBatchGlb = new DataTable();
            int timeWise = _objRepoPara._GlbReportGetTimeWise;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(_objRepoPara._GlbReportCompCode, _objRepoPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.PickPlanReport(timeWise == 1 ? Convert.ToDateTime(_objRepoPara._GlbReportFromDate) : Convert.ToDateTime(_objRepoPara._GlbReportFromDate).Date,
                        timeWise == 1 ? Convert.ToDateTime(_objRepoPara._GlbReportToDate) : Convert.ToDateTime(_objRepoPara._GlbReportToDate).Date, _objRepoPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepoPara._GlbUserID, _objRepoPara._GlbReportDoc, _objRepoPara._GlbReportRoute, _objRepoPara._GlbReportReqNo, _objRepoPara._GlbReportGetTimeWise);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_LOC", typeof(string));
            param.Columns.Add("PARA_REQ_LOC", typeof(string));
            param.Columns.Add("PARA_DOCTYPE", typeof(string));
            param.Columns.Add("PARA_ITCODE", typeof(string));
            param.Columns.Add("PARA_BRAND", typeof(string));
            param.Columns.Add("PARA_MODEL", typeof(string));
            param.Columns.Add("PARA_CAT1", typeof(string));
            param.Columns.Add("PARA_CAT2", typeof(string));
            param.Columns.Add("PARA_CAT3", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));
            param.Columns.Add("PARA_CLR", typeof(string));
            param.Columns.Add("GRP_REQ", typeof(int));
            param.Columns.Add("GRP_MODEL", typeof(int));
            param.Columns.Add("PARA_WITHBIN", typeof(int));
            param.Columns.Add("ROUTE", typeof(string));
            param.Columns.Add("ROUTE_ALL", typeof(int));
            param.Columns.Add("PARA_DOCNO", typeof(string));

            dr = param.NewRow();
            dr["PARA_USER"] = _objRepoPara._GlbUserID;
            if (timeWise == 1)//yyyy-MM-dd hh:mm
            {
                dr["PARA_PERIOD"] = "FROM " + _objRepoPara._GlbReportFromDate.ToString("yyyy-MM-dd HH:mm") + " TO " + _objRepoPara._GlbReportToDate.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                dr["PARA_PERIOD"] = "FROM " + _objRepoPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepoPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            }
            dr["PARA_LOC"] = _objRepoPara._GlbReportProfit == "" ? "ALL" : _objRepoPara._GlbReportProfit;
            dr["PARA_REQ_LOC"] = _objRepoPara._GlbReportOtherLoc == "" ? "ALL" : _objRepoPara._GlbReportOtherLoc;
            dr["PARA_DOCTYPE"] = _objRepoPara._GlbReportDocType == "" ? "ALL" : _objRepoPara._GlbReportDocType;
            dr["PARA_ITCODE"] = _objRepoPara._GlbReportItemCode == "" ? "ALL" : _objRepoPara._GlbReportItemCode;
            dr["PARA_BRAND"] = _objRepoPara._GlbReportBrand == "" ? "ALL" : _objRepoPara._GlbReportBrand;
            dr["PARA_MODEL"] = _objRepoPara._GlbReportModel == "" ? "ALL" : _objRepoPara._GlbReportModel;
            dr["PARA_CAT1"] = _objRepoPara._GlbReportItemCat1 == "" ? "ALL" : _objRepoPara._GlbReportItemCat1;
            dr["PARA_CAT2"] = _objRepoPara._GlbReportItemCat2 == "" ? "ALL" : _objRepoPara._GlbReportItemCat2;
            dr["PARA_CAT3"] = _objRepoPara._GlbReportItemCat3 == "" ? "ALL" : _objRepoPara._GlbReportItemCat3;
            dr["PARA_HEADING"] = _objRepoPara._GlbReportHeading;
            dr["PARA_CLR"] = _objRepoPara._GlbReportColor == "" ? "ALL" : _objRepoPara._GlbReportColor;
            dr["GRP_REQ"] = _objRepoPara._GlbReportnoofDays;
            dr["GRP_MODEL"] = _objRepoPara._GlbReportFromAge;
            dr["PARA_WITHBIN"] = _objRepoPara._GlbReportwithBin;
            dr["ROUTE"] = _objRepoPara._GlbReportRoute == "" ? "ALL" : _objRepoPara._GlbReportRoute;
            dr["ROUTE_ALL"] = _objRepoPara._GlbReportCheckRegDate;
            dr["PARA_DOCNO"] = _objRepoPara._GlbReportDoc == "" ? "ALL" : _objRepoPara._GlbReportDoc;

            param.Rows.Add(dr);

            if (_objRepoPara._GlbReportwithBin == 1)
            {
                _pickplan.Database.Tables["PICK_PLAN"].SetDataSource(GLOB_DataTable);
                _pickplan.Database.Tables["REP_PARA"].SetDataSource(param);
            }
            else
            {
                _pickplansum.Database.Tables["PICK_PLAN"].SetDataSource(GLOB_DataTable);
                _pickplansum.Database.Tables["REP_PARA"].SetDataSource(param);
            }
            //For AAL
            _PICK_PLAN_AAL.Database.Tables["PICK_PLAN"].SetDataSource(GLOB_DataTable);  //_pickplansum_AAL
            _PICK_PLAN_AAL.Database.Tables["REP_PARA"].SetDataSource(param);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    _intBatch = bsObj.CHNLSVC.Inventory.get_PickPlan_InrBatchData(_objRepoPara._GlbReportCompCode, drow["tpl_pc"].ToString());
                    _intBatchGlb.Merge(_intBatch);
                }
            }
            foreach (object repOp in _PICK_PLAN_AAL.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "INRBATCHDATA")
                    {
                        ReportDocument subRepDoc = _PICK_PLAN_AAL.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["INRBATCH"].SetDataSource(_intBatchGlb);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                }
            }
        }

        public void StockVerification(string com, string docno)
        {
            DataTable data = bsObj.CHNLSVC.CustService.StockVerification(com, docno);
            int i = 0;
            int j = 0;
            int count = 1;
            string loc = "";
            string itemcode = "";
            int scanqty = data.Rows.Count;
            int sysqty = 0;
            int vari = 0;
            string scanserial = "";
            string sysserial = "";
            DataTable data2 = null;
            DataTable StockTable = new DataTable();
            DataRow dr;
            StockTable.Columns.Add("ItemCode", typeof(string));
            StockTable.Columns.Add("Description", typeof(string));
            StockTable.Columns.Add("SysQty", typeof(string));
            StockTable.Columns.Add("ScanQty", typeof(string));
            StockTable.Columns.Add("Variance", typeof(string));
            StockTable.Columns.Add("SysSerial", typeof(string));
            StockTable.Columns.Add("Serial", typeof(string));
            StockTable.Columns.Add("Location", typeof(string));
            StockTable.Columns.Add("UserName", typeof(string));
            if (data != null)
            {
                loc = data.Rows[0].Field<string>(3);
                itemcode = data.Rows[0].Field<string>(4);
                data2 = bsObj.CHNLSVC.CustService.GetSysSerials(com, loc, itemcode);
                sysqty = data2.Rows.Count;
                vari = sysqty - scanqty;
                foreach (DataRow dtRow in data.Rows)
                {
                    dr = StockTable.NewRow();

                    if (itemcode != data.Rows[i].Field<string>(4))
                    {
                        StockTable.Rows[i - 1][3] = count - 1;
                        itemcode = data.Rows[i].Field<string>(4);
                        scanserial = data.Rows[i].Field<string>(6);
                        dr["ItemCode"] = itemcode;
                        dr["Description"] = data.Rows[i].Field<string>(0);
                        dr["ScanQty"] = scanqty;
                        dr["Serial"] = scanserial;
                        if (data2 != null)
                        {
                            j = 0;
                            dr["SysQty"] = scanqty;
                            dr["Variance"] = vari;
                            foreach (DataRow dtRow2 in data2.Rows)
                            {
                                sysserial = data2.Rows[j].Field<string>(0);
                                if (scanserial == sysserial)
                                {
                                    dr["SysSerial"] = sysserial;

                                    data2.Rows.Remove(dtRow2);

                                }


                                j++;
                            }
                            count = 1;
                        }

                    }
                    else
                    {
                        scanserial = data.Rows[i].Field<string>(6);
                        dr["ItemCode"] = itemcode;
                        dr["Description"] = data.Rows[i].Field<string>(0);
                        dr["ScanQty"] = i + 1;
                        dr["Serial"] = scanserial;
                        if (data2 != null)
                        {
                            dr["SysQty"] = sysqty;
                            dr["Variance"] = sysqty - scanqty;
                            foreach (DataRow dtRow2 in data2.Rows)
                            {
                                sysserial = data2.Rows[j].Field<string>(0);
                                if (scanserial == sysserial)
                                {
                                    dr["SysSerial"] = sysserial;

                                    data2.Rows.Remove(dtRow2);

                                }


                                j++;
                            }

                        }
                    }


                    StockTable.Rows.Add(dr);
                    i++;
                    count++;
                }

            }
            foreach (DataRow dtRow2 in data2.Rows)
            {
                dr = StockTable.NewRow();
                dr["ItemCode"] = itemcode;
                dr["Description"] = data.Rows[0].Field<string>(0);
                dr["ScanQty"] = scanqty;
                dr["Serial"] = "";
                dr["SysQty"] = scanqty;
                dr["Variance"] = sysqty - scanqty;
                dr["SysSerial"] = sysserial;
                StockTable.Rows.Add(dr);
            }
            stock_verify.Database.Tables["StockVerify"].SetDataSource(StockTable);
        }

        public void StockVerificationnew(string com, string loc, string itemcode, string docno)
        {
            DataTable systemqty = bsObj.CHNLSVC.CustService.GetSystemQTY(com, loc, itemcode);
            DataTable systemitem = bsObj.CHNLSVC.CustService.GetSysSerials(com, loc, itemcode);
            DataTable scanitems = bsObj.CHNLSVC.CustService.StockVerification(com, docno);
            DataTable scanitemqty = bsObj.CHNLSVC.CustService.GetScanQTY(com, docno);

            int i = 0;
            int j = 0;
            string systemqtycode = "";
            decimal systemqtyqty = 0;
            string systemitemcode = "";
            string scanitemcodenew = "";
            decimal scanitemqtynew = 0;
            string scanitemqtycode = "";
            //string systemitemqty = "";
            DataTable StockTable = new DataTable();
            StockTable.Columns.Add("ITEMCODE", typeof(string));
            StockTable.Columns.Add("DESCRIPTION", typeof(string));
            StockTable.Columns.Add("SYSSERIAL", typeof(string));
            StockTable.Columns.Add("SERIAL", typeof(string));
            StockTable.Columns.Add("VARIANCES", typeof(string));
            StockTable.Columns.Add("SERIQTY", typeof(Int32));
            StockTable.Columns.Add("SYSQTY", typeof(Int32));

            if (systemitem != null)
            {
                foreach (DataRow systemqtyRow in systemqty.Rows)
                {
                    systemqtycode = systemqty.Rows[i].Field<string>(0);
                    systemqtyqty = systemqty.Rows[i].Field<decimal>(1);
                    foreach (DataRow systemitemRow in systemitem.Rows)
                    {
                        systemitemcode = systemitem.Rows[j].Field<string>(0);
                        //systemitemqty = systemitem.Rows[i].Field<string>(5);

                        if (systemqtycode == systemitemcode)
                        {
                            systemitem.Rows[j][5] = systemqtyqty;
                            systemitem.Rows[j][6] = "0";
                            systemitem.Rows[j][4] = "";
                            systemitem.Rows[j][3] = "";
                        }

                        j++;
                    }
                    j = 0;
                    i++;
                }
            }

            i = 0;
            j = 0;
            if (scanitems != null)
            {
                foreach (DataRow scanitemqtyRow in scanitemqty.Rows)
                {
                    scanitemqtycode = scanitemqty.Rows[i].Field<string>(0);
                    scanitemqtynew = scanitemqty.Rows[i].Field<decimal>(1);
                    foreach (DataRow scanitemsmRow in scanitems.Rows)
                    {
                        scanitemcodenew = scanitems.Rows[j].Field<string>(0);
                        //systemitemqty = systemitem.Rows[i].Field<string>(5);

                        if (scanitemqtycode == scanitemcodenew)
                        {
                            scanitems.Rows[j][4] = scanitemqtynew;
                            scanitems.Rows[j][3] = "0";
                        }

                        j++;
                    }
                    j = 0;
                    i++;
                }
            }

            //scan item
            systemitemcode = "";
            string scanitemcode = "";
            string systemserial = "";
            string scanserial = "";
            decimal scanserialqtynew = 0;
            i = 0;
            j = 0;

            if (scanitems != null)
            {
                foreach (DataRow scanitemsRow in scanitems.Rows)
                {
                    scanitemcode = scanitems.Rows[i].Field<string>(0);
                    scanserial = scanitems.Rows[i].Field<string>(1);


                    Int32 qt = scanitems.Rows[i].Field<Int32>(4);

                    scanserialqtynew = Convert.ToDecimal(qt);



                    foreach (DataRow systemitemRow in systemitem.Rows)
                    {
                        systemitemcode = systemitem.Rows[j].Field<string>(0);
                        systemserial = systemitem.Rows[j].Field<string>(2);

                        if (systemitemcode == scanitemcode && scanserial == systemserial)
                        {
                            systemitem.Rows[j][3] = systemserial.ToString();
                            systemitem.Rows[j][5] = scanserialqtynew.ToString();

                            // scanitems.Rows.Remove(scanitemsRow);
                            scanitemsRow.Delete();
                            //scanitems.AcceptChanges();
                        }
                        if (systemitemcode == scanitemcode && scanserial != systemserial)
                        {
                            DataRow dr;
                            dr = StockTable.NewRow();
                            dr["ITEMCODE"] = systemitemcode;
                            dr["DESCRIPTION"] = systemitem.Rows[j].Field<string>(1);
                            dr["SYSSERIAL"] = "";
                            dr["SERIAL"] = scanserial;
                            dr["VARIANCES"] = "";
                            dr["SERIQTY"] = scanserialqtynew;

                            Int32 qt2 = systemitem.Rows[j].Field<Int32>(5);

                            dr["SYSQTY"] = Convert.ToDecimal(qt2);
                            StockTable.Rows.Add(dr);
                            //  scanitems.Rows.Remove(scanitemsRow);
                            scanitemsRow.Delete();
                            // scanitems.AcceptChanges();
                        }



                        j++;
                    }
                    j = 0;
                    i++;
                }
            }
            scanitems.AcceptChanges();
            systemitem.Merge(StockTable);
            systemitem.Merge(scanitems);



            stock_verify.Database.Tables["StockVerify"].SetDataSource(systemitem);
        }

        public DataTable DispatchRequestReport(InvReportPara _objRepoPara)
        {//Lakshika 2016/Aug/11
            DataTable param = new DataTable();
            DataRow dr;

            GLOB_DataTable = new DataTable();
            DataTable DidpatchReqDetails = new DataTable();
            DataTable dtReportFooter = new DataTable();

            //dtReportFooter = bsObj.CHNLSVC.Inventory.GetReportParam(_objRepoPara._GlbCompany, _objRepoPara._GlbUserID);
            string powered_by = "";
            //foreach (DataRow row in dtReportFooter.Rows)
            //{
            //    powered_by = row["MC_IT_POWERED"].ToString();

            //}

            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("issue_loc", typeof(string));
            param.Columns.Add("delevery_loc", typeof(string));
            param.Columns.Add("routedet", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("status", typeof(string));
            param.Columns.Add("poweredby", typeof(string));
            dr = param.NewRow();

            string fdate = _objRepoPara._GlbReportFromDate.Date.ToShortDateString();
            string fdt = Convert.ToDateTime(fdate).ToShortDateString();

            string tdate = _objRepoPara._GlbReportToDate.Date.ToShortDateString();
            string tdt = Convert.ToDateTime(tdate).ToShortDateString();

            dr["fromdate"] = fdate;
            dr["todate"] = tdate;
            dr["com"] = _objRepoPara._GlbReportCompName == "" ? "" : _objRepoPara._GlbReportCompName.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["delevery_loc"] = _objRepoPara._GlbLocation == "" ? "" : _objRepoPara._GlbLocation.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["issue_loc"] = _objRepoPara._GlbReportOtherLoc == "" ? "ALL" : _objRepoPara._GlbReportOtherLoc.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["user"] = _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["routedet"] = _objRepoPara._GlbReportRoute == "" ? "" : _objRepoPara._GlbReportRoute.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["status"] = _objRepoPara._GlbDispatchStatus == "" ? "" : _objRepoPara._GlbDispatchStatus.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["poweredby"] = powered_by == "" ? "" : powered_by.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            param.Rows.Add(dr);

            GLOB_DataTable.Clear();
            //GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.GetEntryReportDetails(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbUserID, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportDoc);
            DidpatchReqDetails = bsObj.CHNLSVC.MsgPortal.DispatchReqSummaryReport(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbCompany, _objRepoPara._GlbLocation, _objRepoPara._GlbReportOtherLoc, _objRepoPara._GlbUserID, _objRepoPara._GlbReportRoute, _objRepoPara._GlbDispatchStatus, _objRepoPara._GlbReportGroupfrmtime, _objRepoPara._GlbReportGrouptotime, _objRepoPara._GlbReportprintMark, _objRepoPara._GlbReportExDteWise);


            DataTable realdata = new DataTable("DispatchSummary");
            DataRow redr;
            realdata.Columns.Add("ITR_EXP_DT", typeof(string));
            realdata.Columns.Add("ITR_REQ_NO", typeof(string));
            realdata.Columns.Add("ITR_REF", typeof(string));
            realdata.Columns.Add("ITR_REC_TO", typeof(string));
            realdata.Columns.Add("ITR_ISSUE_FROM", typeof(string));
            realdata.Columns.Add("ROWNUM", typeof(Int32));
            realdata.Columns.Add("ML_LOC_DESC", typeof(string));

            int i = 0;
            foreach (var dis in DidpatchReqDetails.Rows)
            {

                redr = realdata.NewRow();

                string date = DidpatchReqDetails.Rows[i]["ITR_EXP_DT"].ToString();
                string dt = Convert.ToDateTime(date).ToShortDateString();

                redr["ITR_EXP_DT"] = dt;
                redr["ITR_REQ_NO"] = DidpatchReqDetails.Rows[i]["ITR_REQ_NO"].ToString();
                redr["ITR_REF"] = DidpatchReqDetails.Rows[i]["ITR_REF"].ToString();
                redr["ITR_REC_TO"] = DidpatchReqDetails.Rows[i]["ITR_REC_TO"].ToString();
                redr["ITR_ISSUE_FROM"] = DidpatchReqDetails.Rows[i]["ITR_ISSUE_FROM"].ToString();
                redr["ML_LOC_DESC"] = DidpatchReqDetails.Rows[i]["ML_LOC_DESC"].ToString();
                redr["ROWNUM"] = Convert.ToInt32(DidpatchReqDetails.Rows[i]["ROWNUM"].ToString());

                realdata.Rows.Add(redr);
                i++;
            }


            // param.Rows.Add(realdata);

            if (_objRepoPara._GlbDispatchStatus == "A")
            {
                dr["status"] = "Pending";
            }
            else if (_objRepoPara._GlbDispatchStatus == "F")
            {
                dr["status"] = "Approved";
            }
            _dispatchReqSummary.Database.Tables["DISPATCH_REQ_SUMMARY"].SetDataSource(realdata);
            _dispatchReqSummary.Database.Tables["param"].SetDataSource(param);

            return realdata;
        }

        public DataTable DispatchRequestDtlReport(InvReportPara _objRepoPara)
        {//Lakshika 2016/Aug/11
            DataTable param = new DataTable();
            DataRow dr;
            //tmp_user_pc = new DataTable();
            GLOB_DataTable = new DataTable();
            DataTable DidpatchReqDetails = new DataTable();

            DataTable dtReportFooter = new DataTable();

            //dtReportFooter = bsObj.CHNLSVC.Inventory.GetReportParam(_objRepoPara._GlbCompany, _objRepoPara._GlbUserID);
            string powered_by = "";
            //foreach (DataRow row in dtReportFooter.Rows)
            //{
            //    powered_by = row["MC_IT_POWERED"].ToString();

            //}

            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("issue_loc", typeof(string));
            param.Columns.Add("delevery_loc", typeof(string));
            param.Columns.Add("routedet", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("status", typeof(string));
            param.Columns.Add("poweredby", typeof(string));

            dr = param.NewRow();

            string fdate = _objRepoPara._GlbReportFromDate.Date.ToShortDateString();
            string fdt = Convert.ToDateTime(fdate).ToShortDateString();

            string tdate = _objRepoPara._GlbReportToDate.Date.ToShortDateString();
            string tdt = Convert.ToDateTime(tdate).ToShortDateString();

            dr["fromdate"] = fdt;
            dr["todate"] = tdt;
            dr["com"] = _objRepoPara._GlbReportCompName == "" ? "" : _objRepoPara._GlbReportCompName.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["delevery_loc"] = _objRepoPara._GlbLocation == "" ? "" : _objRepoPara._GlbLocation.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["issue_loc"] = _objRepoPara._GlbReportOtherLoc == "" ? "ALL" : _objRepoPara._GlbReportOtherLoc.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["user"] = _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["routedet"] = _objRepoPara._GlbReportRoute == "" ? "" : _objRepoPara._GlbReportRoute.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["status"] = _objRepoPara._GlbDispatchStatus == "" ? "" : _objRepoPara._GlbDispatchStatus.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["poweredby"] = powered_by == "" ? "" : powered_by.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            param.Rows.Add(dr);
            string status = "";
            GLOB_DataTable.Clear();
            //GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.GetEntryReportDetails(_objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportCompCode, _objRepPara._GlbUserID, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportDoc);
            DidpatchReqDetails = bsObj.CHNLSVC.MsgPortal.DispatchRequestDtlReport(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbCompany, _objRepoPara._GlbLocation, _objRepoPara._GlbReportOtherLoc, _objRepoPara._GlbUserID, _objRepoPara._GlbReportRoute, _objRepoPara._GlbDispatchStatus, _objRepoPara._GlbReportGroupfrmtime, _objRepoPara._GlbReportGrouptotime, _objRepoPara._GlbReportprintMark, _objRepoPara._GlbReportExDteWise, out status);


            DataTable realdata = new DataTable("DispatchDetails");
            DataRow redr;
            //Header Details
            realdata.Columns.Add("ITR_EXP_DT", typeof(string));
            realdata.Columns.Add("ITR_REQ_NO", typeof(string));
            realdata.Columns.Add("ITR_REF", typeof(string));
            realdata.Columns.Add("ITR_REC_TO", typeof(string));
            realdata.Columns.Add("ITR_ISSUE_FROM", typeof(string));
            //Item Details
            realdata.Columns.Add("MI_CD", typeof(string));
            realdata.Columns.Add("MI_SHORTDESC", typeof(string));
            realdata.Columns.Add("MI_MODEL", typeof(string));
            realdata.Columns.Add("ITRI_ITM_STUS", typeof(string));
            realdata.Columns.Add("ITRI_QTY", typeof(Int32));

            realdata.Columns.Add("ITR_NOTE", typeof(string));
            realdata.Columns.Add("ML_LOC_DESC", typeof(string));
            realdata.Columns.Add("ML_ADD1", typeof(string));
            realdata.Columns.Add("ML_ADD2", typeof(string));

            realdata.Columns.Add("ITRI_APP_QTY", typeof(Int32));
            realdata.Columns.Add("ITRI_BQTY", typeof(Int32));

            realdata.Columns.Add("ROWNUM", typeof(Int32));

            int i = 0;
            foreach (var dis in DidpatchReqDetails.Rows)
            {

                redr = realdata.NewRow();
                string date = DidpatchReqDetails.Rows[i]["ITR_EXP_DT"].ToString();
                string dt = Convert.ToDateTime(date).ToShortDateString();
                redr["ITR_EXP_DT"] = dt;// DidpatchReqDetails.Rows[i]["ITR_DT"].ToString();
                redr["ITR_REQ_NO"] = DidpatchReqDetails.Rows[i]["ITR_REQ_NO"].ToString();
                redr["ITR_REF"] = DidpatchReqDetails.Rows[i]["ITR_REF"].ToString();
                redr["ITR_REC_TO"] = DidpatchReqDetails.Rows[i]["ITR_REC_TO"].ToString();
                redr["ITR_ISSUE_FROM"] = DidpatchReqDetails.Rows[i]["ITR_ISSUE_FROM"].ToString();

                redr["MI_CD"] = DidpatchReqDetails.Rows[i]["MI_CD"].ToString();
                redr["MI_SHORTDESC"] = DidpatchReqDetails.Rows[i]["MI_SHORTDESC"].ToString();
                redr["MI_MODEL"] = DidpatchReqDetails.Rows[i]["MI_MODEL"].ToString();
                redr["ITRI_ITM_STUS"] = DidpatchReqDetails.Rows[i]["ITRI_ITM_STUS"].ToString();
                redr["ITRI_QTY"] = Convert.ToInt32(DidpatchReqDetails.Rows[i]["ITRI_QTY"]);

                redr["ITR_NOTE"] = DidpatchReqDetails.Rows[i]["ITR_NOTE"].ToString();
                redr["ML_LOC_DESC"] = DidpatchReqDetails.Rows[i]["ML_LOC_DESC"].ToString();
                redr["ML_ADD1"] = DidpatchReqDetails.Rows[i]["ML_ADD1"].ToString();
                redr["ML_ADD2"] = DidpatchReqDetails.Rows[i]["ML_ADD2"].ToString();

                redr["ITRI_APP_QTY"] = Convert.ToInt32(DidpatchReqDetails.Rows[i]["ITRI_APP_QTY"]);
                redr["ITRI_BQTY"] = Convert.ToInt32(DidpatchReqDetails.Rows[i]["ITRI_BQTY"]);

                redr["ROWNUM"] = Convert.ToInt32(DidpatchReqDetails.Rows[i]["ROWNUM"].ToString());



                realdata.Rows.Add(redr);
                i++;
            }


            // param.Rows.Add(realdata);
            if (_objRepoPara._GlbDispatchStatus == "A")
            {
                dr["status"] = "Pending";
            }
            else if (_objRepoPara._GlbDispatchStatus == "F")
            {
                dr["status"] = "Approved";
            }

           // Add by Tharindu 2018-02-14


            if (_objRepoPara._GlbReportName == "Dispatch_Req_Detail_Report.rpt")
            {
                _dispatchReqDetails.Database.Tables["DISPATCH_REQ_DETAILS"].SetDataSource(realdata);
                _dispatchReqDetails.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _dispatchReqDetailsaal.Database.Tables["DISPATCH_REQ_DETAILS"].SetDataSource(realdata);
                _dispatchReqDetailsaal.Database.Tables["param"].SetDataSource(param);

                #region  CommentedDuetotheNoneedfromUser-BINqty
            //    DataTable _intBatch = new DataTable();
            //    DataTable _intBatchGlb = new DataTable();

            //    var distinctWH = realdata.AsEnumerable()
            //            .Select(row => new
            //            {
            //                WHcode = row.Field<string>("ITR_ISSUE_FROM")
            //            })
            //            .Distinct();
            //    DataTable _intBatch1 = LINQResultToDataTable(distinctWH);

            //    foreach (DataRow drow in _intBatch1.Rows)
            //    {
            //        _intBatch = bsObj.CHNLSVC.Inventory.get_PickPlan_InrBatchData(_objRepoPara._GlbReportCompCode, drow["WHcode"].ToString());
            //        _intBatchGlb.Merge(_intBatch);
            //    }


            //    foreach (object repOp in _dispatchReqDetailsaal.ReportDefinition.ReportObjects)
            //    {
            //        string _s = repOp.GetType().ToString();
            //        if (_s.ToLower().Contains("subreport"))
            //        {
            //            SubreportObject _cs = (SubreportObject)repOp;
            //            if (_cs.SubreportName == "rpt_Sub_BinQty")
            //            {
            //                ReportDocument subRepDoc = _dispatchReqDetailsaal.Subreports[_cs.SubreportName];

            //                subRepDoc.Database.Tables["INRBATCH"].SetDataSource(_intBatchGlb);
            //                subRepDoc.Close();
            //                subRepDoc.Dispose();
            //            }
            //        }
                //    }
                #endregion
            } 
              
            return realdata;     
        
        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        //Lakshika 2016/Aug/18
        public void ItemDispatchDetailReport(InvReportPara _objRepoPara)
        {
            DataTable param = new DataTable();
            DataRow dr;
            //tmp_user_pc = new DataTable();
            GLOB_DataTable = new DataTable();
            DataTable DidpatchReqDetails = new DataTable();

            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("powered_by", typeof(string));

            dr = param.NewRow();

            string fdate = _objRepoPara._GlbReportFromDate.Date.ToShortDateString();
            string fdt = Convert.ToDateTime(fdate).ToShortDateString();

            string tdate = _objRepoPara._GlbReportToDate.Date.ToShortDateString();
            string tdt = Convert.ToDateTime(tdate).ToShortDateString();

            dr["fromdate"] = fdt;
            dr["todate"] = tdt;
            dr["user"] = _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["com"] = _objRepoPara._GlbReportCompName;
            dr["powered_by"] = _objRepoPara._GlbReportPoweredBy;

            param.Rows.Add(dr);

            GLOB_DataTable.Clear();

            DidpatchReqDetails = bsObj.CHNLSVC.MsgPortal.ItemDispatchDtlReport(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbUserID);

            DataTable realdata = new DataTable();
            DataRow redr;
            //Header Details

            realdata.Columns.Add("MI_CATE_1", typeof(string));
            realdata.Columns.Add("MI_CD", typeof(string));
            realdata.Columns.Add("MI_SHORTDESC", typeof(string));

            realdata.Columns.Add("RETAIL_SR_QTY", typeof(Int32));
            realdata.Columns.Add("ELITE_SR_QTY", typeof(Int32));
            realdata.Columns.Add("DEALER_QTY", typeof(Int32));
            realdata.Columns.Add("DF_SR_QTY", typeof(Int32));
            realdata.Columns.Add("DF_DEL_QTY", typeof(Int32));
            realdata.Columns.Add("AZ_SR_QTY", typeof(Int32));
            //Balance
            realdata.Columns.Add("BAL_BOND_QTY", typeof(Int32));
            realdata.Columns.Add("BAL_DPS32_QTY", typeof(Int32));
            realdata.Columns.Add("BAL_AZKK_QTY", typeof(Int32));
            realdata.Columns.Add("BAL_AEL_QTY", typeof(Int32));

            realdata.Columns.Add("BAL_DPS45_QTY", typeof(Int32));
            realdata.Columns.Add("BAL_RDPS32_QTY", typeof(Int32));
            realdata.Columns.Add("BAL_RDPS45_QTY", typeof(Int32));
            realdata.Columns.Add("BAL_DPS51_QTY", typeof(Int32));
            realdata.Columns.Add("BAL_RDPS51_QTY", typeof(Int32));
            realdata.Columns.Add("BAL_RWHGL_QTY", typeof(Int32));
            realdata.Columns.Add("BAL_VWHGL_QTY", typeof(Int32));


            //  realdata.Columns.Add("ROWNUM", typeof(Int32));

            int i = 0;
            foreach (var dis in DidpatchReqDetails.Rows)
            {

                redr = realdata.NewRow();

                redr["MI_CATE_1"] = DidpatchReqDetails.Rows[i]["MI_CATE_1"].ToString();
                redr["MI_CD"] = DidpatchReqDetails.Rows[i]["MI_CD"].ToString();
                redr["MI_SHORTDESC"] = DidpatchReqDetails.Rows[i]["MI_SHORTDESC"].ToString();

                redr["RETAIL_SR_QTY"] = DidpatchReqDetails.Rows[i]["RETAIL_SR_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["RETAIL_SR_QTY"].ToString());
                redr["ELITE_SR_QTY"] = DidpatchReqDetails.Rows[i]["ELITE_SR_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["ELITE_SR_QTY"].ToString());
                redr["DEALER_QTY"] = DidpatchReqDetails.Rows[i]["DEALER_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["DEALER_QTY"].ToString());
                redr["DF_SR_QTY"] = DidpatchReqDetails.Rows[i]["DF_SR_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["DF_SR_QTY"].ToString());
                redr["DF_DEL_QTY"] = DidpatchReqDetails.Rows[i]["DF_DEL_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["DF_DEL_QTY"].ToString());
                redr["AZ_SR_QTY"] = DidpatchReqDetails.Rows[i]["AZ_SR_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["AZ_SR_QTY"].ToString());
                //Balance
                redr["BAL_BOND_QTY"] = DidpatchReqDetails.Rows[i]["BAL_BOND_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_BOND_QTY"].ToString());
                redr["BAL_DPS32_QTY"] = DidpatchReqDetails.Rows[i]["BAL_DPS32_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_DPS32_QTY"].ToString());
                redr["BAL_AZKK_QTY"] = DidpatchReqDetails.Rows[i]["BAL_AZKK_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_AZKK_QTY"].ToString());
                redr["BAL_AEL_QTY"] = DidpatchReqDetails.Rows[i]["BAL_AEL_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_AEL_QTY"].ToString());


                redr["BAL_DPS45_QTY"] = DidpatchReqDetails.Rows[i]["BAL_DPS45_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_DPS45_QTY"].ToString());
                redr["BAL_RDPS32_QTY"] = DidpatchReqDetails.Rows[i]["BAL_RDPS32_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_RDPS32_QTY"].ToString());
                redr["BAL_RDPS45_QTY"] = DidpatchReqDetails.Rows[i]["BAL_RDPS45_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_RDPS45_QTY"].ToString());
                redr["BAL_DPS51_QTY"] = DidpatchReqDetails.Rows[i]["BAL_DPS51_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_DPS51_QTY"].ToString());
                redr["BAL_RDPS51_QTY"] = DidpatchReqDetails.Rows[i]["BAL_RDPS51_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_RDPS51_QTY"].ToString());
                redr["BAL_RWHGL_QTY"] = DidpatchReqDetails.Rows[i]["BAL_RWHGL_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_RWHGL_QTY"].ToString());
                redr["BAL_VWHGL_QTY"] = DidpatchReqDetails.Rows[i]["BAL_VWHGL_QTY"].ToString() == "" ? 0 : Convert.ToInt32(DidpatchReqDetails.Rows[i]["BAL_VWHGL_QTY"].ToString());

                realdata.Rows.Add(redr);
                i++;
            }

            _itemDispatchDetails.Database.Tables["ITEM_DISPATCH_DETAILS"].SetDataSource(realdata);
            _itemDispatchDetails.Database.Tables["param"].SetDataSource(param);

        }
        public void ApprovedMRN(InvReportPara _objRepoPara)
        {
            DataTable param = new DataTable();
            DataTable result = new DataTable();
            DataRow dr;

            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("powered_by", typeof(string));

            dr = param.NewRow();

            dr["fromdate"] = _objRepoPara._GlbReportFromDate.ToShortDateString();
            dr["todate"] = _objRepoPara._GlbReportToDate.ToShortDateString();
            dr["user"] = _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["com"] = _objRepoPara._GlbReportCompName;
            dr["powered_by"] = _objRepoPara._GlbReportPoweredBy;

            param.Rows.Add(dr);
            result = bsObj.CHNLSVC.MsgPortal.MRN_Approved_Report(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbReportCompCode, _objRepoPara._GlbLocation, _objRepoPara._GlbUserID, _objRepoPara._GlbReportType, _objRepoPara._GlbReportRoute, _objRepoPara._GlbReportCheckRegDate);
            _approvedMRN.Database.Tables["MRNAApproved"].SetDataSource(result);
            _approvedMRN.Database.Tables["MRNAPara"].SetDataSource(param);
        }
        public void CourierDaillySummary(InvReportPara _objRepoPara, string _courierCom, string _FromNo, string _ToNo)
        {
            DataTable param = new DataTable();
            DataTable result = new DataTable();
            GLOB_DataTable = new DataTable();
            DataRow dr;

            param.Columns.Add("fromDate", typeof(string));
            param.Columns.Add("toDate", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("pwdBy", typeof(string));
            param.Columns.Add("loc", typeof(string));
            param.Columns.Add("slipNo", typeof(string));
            param.Columns.Add("courierCom", typeof(string));

            dr = param.NewRow();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(_objRepoPara._GlbReportCompCode, _objRepoPara._GlbUserID);
            dr["fromDate"] = _objRepoPara._GlbReportFromDate.ToShortDateString();
            dr["toDate"] = _objRepoPara._GlbReportToDate.ToShortDateString();
            dr["user"] = _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["com"] = _objRepoPara._GlbReportCompName;
            dr["pwdBy"] = _objRepoPara._GlbReportPoweredBy;
            dr["slipNo"] = _FromNo+" - "+_ToNo;
            dr["courierCom"] = _courierCom;
            dr["loc"] = string.IsNullOrEmpty(_objRepoPara._GlbLocation) ? "ALL" : _objRepoPara._GlbLocation.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            param.Rows.Add(dr);
            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    result = bsObj.CHNLSVC.MsgPortal.CourierDaillySummary(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepoPara._GlbUserID, _FromNo, _ToNo, _courierCom);
                    GLOB_DataTable.Merge(result);
                }
            }
            
            _courierDaillySum.Database.Tables["CourierDaillySum"].SetDataSource(GLOB_DataTable);
            //_courierDaillySum.Database.Tables["CouPara"].SetDataSource(param);

            _courierDaillySum.SetParameterValue("fromDate", _objRepoPara._GlbReportFromDate.ToShortDateString());
            _courierDaillySum.SetParameterValue("toDate", _objRepoPara._GlbReportToDate.ToShortDateString());
            _courierDaillySum.SetParameterValue("user", _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ','));
            _courierDaillySum.SetParameterValue("com", _objRepoPara._GlbReportCompName);
            _courierDaillySum.SetParameterValue("pwdBy", string.Empty);
            if (string.IsNullOrEmpty(_FromNo) && string.IsNullOrEmpty(_ToNo))
            {
                _courierDaillySum.SetParameterValue("slipNo", " : ALL");
            }
            else
            {
                _courierDaillySum.SetParameterValue("slipNo", "From : "+_FromNo + " - To : " + _ToNo);
            }
            _courierDaillySum.SetParameterValue("courierCom", _courierCom);
            _courierDaillySum.SetParameterValue("loc", string.IsNullOrEmpty(_objRepoPara._GlbLocation) ? "ALL" : _objRepoPara._GlbLocation.Replace('$', ' ').Replace('^', ' ').Replace('|', ','));
        }
    }
}