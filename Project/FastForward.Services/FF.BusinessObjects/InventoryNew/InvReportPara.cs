using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class InvReportPara
    {
        public string _GlbReportCompAddr { get; set; }
        public string _GlbReportDoc { get; set; }
        public string _GlbReportPromotor { get; set; }
        public int _GlbReportIsCostPrmission { get; set; }
        public Int16 _GlbReportIsFast { get; set; }
        
        public int _GlbReportIsSummary { get; set; }
        public Int16 _GlbReportWithStatus { get; set; }
        public string _GlbReportDocType { get; set; }
        public string _GlbReportDirection { get; set; }
        public String _GlbReportDocSubType { get; set; }
        public string _GlbReportDoc2 { get; set; }
        public string _GlbReportName { get; set; }
        public String _GlbReportComp { get; set; }
        public String _GlbUserID { get; set; }
        public String _GlbReportChannel { get; set; }
        public String _GlbReportBrand { get; set; }
        public String _GlbReportModel { get; set; }
        public String _GlbReportItemCode { get; set; }
        public String _GlbReportItemStatus { get; set; }
        public String _GlbReportItemCat1 { get; set; }
        public String _GlbReportItemCat2 { get; set; }
        public String _GlbReportItemCat3 { get; set; }
        public String _GlbReportItemCat4 { get; set; }
        public String _GlbReportItemCat5 { get; set; }
        public Int16 _GlbReportWithCost { get; set; }
        public String _GlbReportMainSerial { get; set; }
        public Int16 _GlbReportWithSerial { get; set; }
        public String _GlbReportCompCode { get; set; }
        public String _GlbReportWithRCC { get; set; }
        public String _GlbReportWithJob { get; set; }
        public String _GlbReportWithGIT { get; set; }
        public String _GlbUserComCode { get; set; }
        public String _GlbReportJobStatus { get; set; }
        public String _GlbReportCompName { get; set; }
        public String _GlbReportCompanies { get; set; }
        public String _GlbReportHeading { get; set; }
        public DateTime _GlbReportAsAtDate { get; set; }
        public String _GlbReportProfit { get; set; }
        public DateTime _GlbReportFromDate { get; set; }
        public DateTime _GlbReportToDate { get; set; }
        public DateTime _GlbReportExpDate { get; set; }
        public string _GlbReportSupplier { get; set; }
        public string _GlbReportType { get; set; }
        public string _GlbReportDocCat { get; set; }
        public string _GlbReportGroupDOLoc { get; set; }
        public string _GlbReportAgent { get; set; }
        public string _GlbReportCountry { get; set; }
        public string _GlbReportPort { get; set; }
        public string _GlbReportGroupItemCode { get; set; }
        public string _GlbReportGroupItemStatus { get; set; }
        public Int32 _GlbReportnoofDays  { get; set; }
        public Int32 _GlbReportFromAge  { get; set; }
        public Int32 _GlbReportToAge { get; set; }
        public string _GlbReportCustomer { get; set; }
        public string _GlbReportExecutive { get; set; }        
        public Int32 _GlbReportIsFreeIssue { get; set; }
        public string _GlbDefSubChannel { get; set; }
        public string _GlbReportToBondNo { get; set; }
        public string _GlbReportGRNNo { get; set; }
        public string _GlbReportReqNo { get; set; }
        public Int32 _GlbReportOutstandingStatus { get; set; }
        public Int32 _GlbReportCheckRegDate { get; set; }
        public string _GlbReportBank { get; set; }
        public string _GlbReportLcNo { get; set; }
        public string _GlbReportBlNo { get; set; }
        public string _GlbReportOtherLoc { get; set; }
        public string _GlbReportColor { get; set; }
        public string _GlbReportRoute { get; set; }
        public Int32 _GlbReportwithBin { get; set; }
        public string _GlbReportBrandMgr { get; set; }
        public string _GlbReportItmClasif { get; set; }
        public string _GlbReportReqFrom { get; set; }
        public string _GlbReportReqTo { get; set; }
        public Int32 _GlbReportParaLine1 { get; set; }
        public string _GlbReportBondNo { get; set; }
        public decimal _GlbReportRate { get; set; }
        public Int32 _GlbReportExport { get; set; }
        public string _GlbReportPONo { get; set; }
        public string _GlbEntryType { get; set; }
        public string _GlbCusDecNo { get; set; }
        public decimal _GlbReportBuyRate { get; set; }
        public decimal _GlbReportForcastRate { get; set; }
        public decimal _GlbReportCostRate { get; set; }
        public decimal _GlbReportMarkup { get; set; }
        public decimal _GlbReportAssemblyCharge { get; set; }
        public decimal _GlbReportOverHead { get; set; }
        public string _GlbReportPriceBook { get; set; }
        public string _GlbReportPBLevel { get; set; }
        public string _GlbCompany { get; set; }
        public string _GlbLocation { get; set; }
        public string _GlbDispatchStatus { get; set; }
        public string _GlbDocNo { get; set; }
        public Int32 _GlbGITWithSerials { get; set; }
        public string _GlbReportPoweredBy { get; set; }
        public DateTime _GlbReportFromDate2 { get; set; }
        public DateTime _GlbReportToDate2 { get; set; }
        public Int32 _GlbReportIsReplItem { get; set; }
//---Report Grouping----

        public Int32 _GlbReportGroupPC { get; set; }
        public Int32 _GlbReportGroupDocTp { get; set; }
        public Int32 _GlbReportGroupCust { get; set; }
        public Int32 _GlbReportGroupExec { get; set; }
        public Int32 _GlbReportGroupItem { get; set; }
        public Int32 _GlbReportGroupBrand { get; set; }
        public Int32 _GlbReportGroupModel { get; set; }
        public Int32 _GlbReportGroupCat1 { get; set; }
        public Int32 _GlbReportGroupCat2 { get; set; }
        public Int32 _GlbReportGroupCat3 { get; set; }
        public Int32 _GlbReportGroupCat4 { get; set; }
        public Int32 _GlbReportGroupCat5 { get; set; }
        public Int32 _GlbReportGroupStockTp { get; set; }
        public Int32 _GlbReportGroupDocNo { get; set; }
        public Int32 _GlbReportGroupLastGroup { get; set; }
        public string _GlbReportGroupLastGroupCat { get; set; }
        public Int32 _GlbReportGroupPromotor { get; set; }
        public Int32 _GlbReportGroupDLoc { get; set; }
        public Int32 _GlbReportGroupJobNo { get; set; }
        public Int32 _GlbReportGroupCheque { get; set; }
        public Int32 _GlbReportGroupaccountcode { get; set; }
        public DateTime _GlbReportGroupfrmtime { get; set; }
        public DateTime _GlbReportGrouptotime { get; set; }
        public int _GlbReportExDteWise { get; set; }
        public int _GlbReportprintMark { get; set; }
        public Int32 _GlbReportGetTimeWise { get; set; }

        public string Entry01 { get; set; }//Added By Dulaj 2018/May/28
        public string Entry02 { get; set; }//Added By Dulaj 2018/May/28
        public int _GlbReportIsExpireDate { get; set; }//Added By Udesh 05/Oct/2018
        public int _GlbReportIsInsurancePolicyDate { get; set; }//Added By Udesh 05/Oct/2018
        public int _GlbReportIsFinanceDocDate { get; set; }//Added By Udesh 05/Oct/2018
    }
}

