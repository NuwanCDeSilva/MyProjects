using FF.BusinessObjects;
using FF.BusinessObjects.BITool;
using FF.BusinessObjects.ReptObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace FF.Interfaces
{
    [ServiceContract]
    public interface IDashboard
    {
        
        [OperationContract]
        MasterLocation GetLocationByLocCode(string _CompCode, string _LocCode);

        //Lakshika 2016-07-25
        [OperationContract]
        List<BMT_REF_HEAD> getBIToolProperties(string _searchValue, string pgeNum, string pgeSize, string searchFld, string propertyType, string module);

        //Nuwan 2016.07.26
        [OperationContract]
        List<BRAND_MNGR_SEARCH_HEAD> getBrandManagers(string pgeNum,string pgeSize,string searchFld,string searchVal,string companies);

        //Lakshika 2016-07-26
        [OperationContract]
        List<BMT_REF_HEAD> LoadBIToolDetailsByName(string _columnName);
        
        //Nuwan 2016.07.26
        [OperationContract]
        List<BRAND_SEARCH_HEAD> getBrands(string pgeNum, string pgeSize, string searchFld, string searchVal, string brandMngr);
      
        //Lakshika 2016-07-28
        [OperationContract]
        string getBMSalesDetails(DateTime _FromDate, DateTime _ToDate, DateTime _FromDateOne, DateTime _ToDateOne, DateTime _FromDateTwo, DateTime _ToDateTwo, string allOne, string allTwo, string _invdate, string _com, string _user, DataTable _col, DataTable _row, DataTable _val, Dictionary<string, string[]> dataValues, out DataTable _result, out DataTable _header, int _foc, int _intercom, int intcurdate, int _inthpsales, int _intcrecardsale, int _servicecharge, int _intitem, string hpinttype, out string _err, Boolean listing);
        
        //Sanjeewa 2016-12-01
        [OperationContract]
        DataTable getCreatedSalesDetails(DateTime _FromDate, DateTime _ToDate, string _invdate, string _com, string _user, Dictionary<string, string[]> dataValues);
        
        [OperationContract]
        DataTable getActiveAccountDetails(DateTime _FromDate, DateTime _ToDate, string _invdate, string _com, string _user, Dictionary<string, string[]> dataValues);
        
        //Nuwan 2016.7.29
        [OperationContract]
        List<TARGET_ALIGNMENT> getTargetAlignmentData(string selCompany, string type, string reptype, string Brand, string BrandMngr, string dtRange, string allbrnd, string allbrndmngr,string sesCompany,string defby,out string error);


        //Dulanga 2017.4.28
        [OperationContract]
        List<CHANL_WISE_SALES> getDeliverdSaleDetails(string selectedcompany, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, string filterby, string Category);

        //Dulanga 2017.5.09
        [OperationContract]
        DataTable GetCompareSales(DateTime fdate, DateTime todate, DateTime pfdate, DateTime ptodate,
          DateTime pmfdate, DateTime pmtodate, string com, string chanel, string pc);

        //Dulanga 2017.5.24
        [OperationContract]
        DataTable GetMobBMTSales(string p_com, string p_code, string p_defby, string p_start, string p_end);

        //Dulanga 2017.6.2
        [OperationContract]
        DataTable GetMobBMTTargertPeriod(string p_type, string p_code);

        //Dulanga 2017.6.2
        [OperationContract]
        DataTable GetSpecificationByModel(string model);

        //Dulanga 2017.6.2
        [OperationContract]
        List<SpecDetail> GetVideoByModel(string model);

        //DataTable GetColorByModel(string model)
        [OperationContract]
        DataTable GetColorByModel(string model);

        //dulanga 2017.6.29
        [OperationContract]
        DataTable GetInquaryPcSales(string p_pc, string p_cat, string p_com, DateTime p_stdt, DateTime p_enddt, string p_all);

        //Dulanga 2017.5.26
        [OperationContract]
        DataTable GetMobBMTTargert(string p_pc, string p_code, string p_start, string p_end);

        //Dulanga 2017.5.26
        [OperationContract]
        DataTable GetMobSales(string p_com, string p_pc, string p_start, string p_end, string p_direct,string type);

        //Nuwan 2016.7.29
        [OperationContract]
        List<SARFORPD_SEARCH_HEAD> getTargetDates(string pgeNum, string pgeSize, string searchFld, string searchVal, string defby, string catdefon);
        //Nuwan 2016.7.29
        [OperationContract]
        SAR_FOR_PD getTargetDateValues(string code, string defby, string catdefon,string calendcode);

        //Randima 2016-08-01
        [OperationContract]
        List<LOC_HIRCH_SEARCH_HEAD> getLocHierarchy(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string company, string type);
        //Dilshan 2017-12-04
        [OperationContract]
        List<LOC_HIRCH_SEARCH_HEAD> getLocHierarchy_new1(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string company, string type);
        //Dilshan 2017-11-29
        [OperationContract]
        List<LOC_HIRCH_SEARCH_HEAD> getLocHierarchy_new(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string company, string type);
        
        //Dilshan 
        [OperationContract]
        List<LOC_HIRCH_SEARCH_HEAD> getLocHierarchyAll(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string company, string type);

        //Randima 2016-08-02
        [OperationContract]
        List<MAIN_CAT_SEARCH> getMainCategory(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Randima 2016-08-02
        [OperationContract]
        List<ITM_STUS_SEARCH_HEAD> getItmOthStatus(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Randima 2016-08-03
        [OperationContract]
        DataTable getAgeSlotForCom(string company);

        //Randima 2016-08-03
        [OperationContract]
        List<ITM_CUR_AGE_DET> getAsAtAgeItmDetails(string _locHircCd, string _locHircDesc, string _cat1, string _brnd, DateTime _frmDt, DateTime _toDt, string _com, string type, string model, string itemCd, List<BRND_NEW_STUS> statusLst, string itmStustyp, string brandMngr,string userid, out string error);

        //Randima 2016-08-03
        [OperationContract]
        List<BRND_NEW_STUS> getStatusForTyp(string _stus_typ);

        //Randima 2016-08-05
        [OperationContract]
        List<ITM_CUR_AGE_DET> getCurAgeItmDetails(string _locHircCd, string _locHircDesc, string _cat1, string _brnd, string _com, string type, string model, string itemCd, List<BRND_NEW_STUS> stusForTyp, string brandMngr,string userid,bool cate1,bool cate2,bool cate3,bool stus,out string error);

        //Randima 2016-08-11
        [OperationContract]
        List<INVOICE_TYPE_SEARCH> getInvoiceTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        
        //Nuwan 2016.08.10
        [OperationContract]
        List<InventoryHeader> test();

        //Randima 2016-08-15
        [OperationContract]
        List<DELI_SALE> getFastMoveItem_SalesDetails(DateTime _fdate, DateTime _tDate, DateTime _asatDt, string invType, string _brnd, string _cat1, string _com, string _freeIss, string qty, string _PCHircCd, string _PCHircDesc);

        //Lakshika 2016-08-30
        [OperationContract]
        List<ITEM_SEARCH> getItems(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Dilshan 2017-12-06
        [OperationContract]
        List<MST_TOWN_SEARCH> getTown(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Dilshan 2017-12-06
        [OperationContract]
        List<MST_PROMOTOR_SEARCH> getPromotor(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Dilshan 2017-12-06
        [OperationContract]
        List<MST_ADMINT_SEARCH> getAdmint(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Dilshan 2018/06/25***************
        [OperationContract]
        List<MST_TEAML_SEARCH> getTeaml(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        [OperationContract]
        List<MST_MNGR_SEARCH> getMangr(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        [OperationContract]
        List<MST_ITMSTS_SEARCH> getItmsts(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        [OperationContract]
        List<MST_LOYALTY_SEARCH> getLtltyp(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        [OperationContract]
        List<MST_LOYALTY_SEARCH> getLtlno(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //**********************************
        //Dilshan 2017-12-06
        [OperationContract]
        List<MST_DIST_SEARCH> getDistrict(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Dilshan 2017-12-06
        [OperationContract]
        List<MST_DIST_SEARCH> getProvince(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Dilshan 2017-12-06
        [OperationContract]
        List<BANK_SEARCH> getBank(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Lakshika 2016-08-30
        [OperationContract]
        List<MAIN_CAT2_SEARCH> getCategory2(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Lakshika 2016-08-30
        [OperationContract]
        List<MAIN_CAT3_SEARCH> getCategory3(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Lakshika 2016-08-30
        [OperationContract]
        List<ITEM_BRAND_SEARCH> getItemBrands(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Lakshika 2016-08-30
        [OperationContract]
        List<ITEM_MODEL_SEARCH> getItemModel(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Lakshika 2016-08-31
        [OperationContract]
        List<BM_SALE_DETAILS> getBMTSaleReportDetails(DateTime _frmDate, DateTime _toDate, string _saleType, string _itemCode, string _cat1, string _cat2, string _cat3, string _brand, string _itemModel, string _groupBy, string _com);

        //Lakshika 2016-08-31
        [OperationContract]
        List<BM_SALE_DETAILS> getSaleReportActiveDetails(DateTime _frmDate, DateTime _toDate, string _saleType, string _itemCode, string _cat1, string _cat2, string _cat3, string _brand, string _itemModel, string _groupBy, string _com);

        //Randima 2016-09-05
        [OperationContract]
        List<MAIN_CAT3_SEARCH> getCategory4(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Randima 2016-09-05
        [OperationContract]
        List<MAIN_CAT3_SEARCH> getCategory5(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Randima 2016-09-05
        [OperationContract]
        List<LOC_HIRCH_SEARCH_HEAD> getAllCompanies(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Randima 2016-09-06
        [OperationContract]
        List<CIRCULAR_NO_SEARCH> getCircualrNo(string pgeNum, string pgeSize, string cir_no, DateTime _frmDt, DateTime _toDt);

        //Randima 2016-09-06
        [OperationContract]
        List<MAIN_CAT_SEARCH> getSchemeType(string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Randima 2016-09-06
        [OperationContract]
        List<MAIN_CAT_SEARCH> getSchemeCode(string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Dilshan
        [OperationContract]
        List<MAIN_CAT_SEARCH> getSchemeTerm(string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Dilshan
        [OperationContract]
        List<MAIN_CAT_SEARCH> getPtypeCode(string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Randima 2016-09-06
        [OperationContract]
        List<MAIN_CAT_SEARCH> getAllPriceBooks(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Randima 2016-09-06
        [OperationContract]
        List<MAIN_CAT_SEARCH> getAllPriceLevels(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Randima 2016-09-06
        [OperationContract]
        List<CUSTOMER_SEARCH> getCustomers(string pgeNum, string pgeSize, string code, string name, string add, string company);

        //Randima 2016-09-06
        [OperationContract]
        List<EXECUTIVE_SEARCH> getAllExecutives(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Randima 2016-09-06
        [OperationContract]
        List<MAIN_CAT_SEARCH> getInvoiceSubTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string mainTyp, string company);

        //subodana 2016-10-25
         [OperationContract]
        List<Sim_Pc> GetPcInfoData(string code, string val);

        //Sanjaya De Silva 20-Dec-2016
        [OperationContract]
        DataTable GetTotMonthSalesDataTable(string _com, string _pc, int _year, int _month, string _exCode, string _custCode);

        //Sanjaya De Silva 22-Dec-2016
        [OperationContract]
        DataTable GetMonthSalesDataTable(string _com, string _pc, int _year, int _month, string _exCode, string _custCode);

        //Lakshan 16 Jan 2017
        [OperationContract]
        DataTable GetSalesForCastingWeeklyForcastAchivement(DateTime _FromDate, DateTime _ToDate, string _invdate, string _com, string _user, Dictionary<string, string[]> dataValues);

        //Sanjaya De Silva 24-JAN-2017
        [OperationContract]
        DataTable GetLocationsByCompDataTable(string _com, string _cate);

        //subodana 2017-jan/28
        [OperationContract]
        List<Deliver_forcst_data> ForecastAnalist(string com, string codes, string brand, string mcat, string scat, string modle, string item, DateTime date,string type,string isforcast);

        // Hushani Dinithi 2017-01-30
        [OperationContract]
        DataTable GetSalesDealersByCompDataTable(string _com, string _chnl);

        // Hushani 2017-02-02
        [OperationContract]
        DataTable GetModelDataTable(string _cat1, string _cat2, string _brand);

        // Hushani 2017-02-02
        [OperationContract]
        DataTable GetShowroomTownByCompanyDataTable(string _com);
        //Lakshan 26 Jan 2017
        [OperationContract]
        List<RptWarehousStockBalance> GetDailyWarehouseBalanseData(string _comp, MasterItem _mstItm, DateTime date, string _tp, string bmanager, string brand, string mcat, out string error);
        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable GetDailyWarehouseBalanseDataAsDataTable(List<RptWarehousStockBalance> _list);
        //Nuwan 2017.02.14
        [OperationContract]
        List<ITEM_MODEL_SEARCH> getInventryAgeItems(string company, string brndMngr, string brnd, string cate1, string pagenum, string pagesize, string field, string search);
        //Nuwan 2017.02.17
        [OperationContract]
        DataTable getAgeSlotForComALL(string company);
        //Nuwan 2017.02.20
        [OperationContract]
        List<ITEM_SEARCH> getInventryAgeSrchItm(string company, string brndMngr, string brnd, string cate1, string model, string pagenum, string pagesize, string field, string search);

        // Hushani 2017-02-03
        [OperationContract]
        DataTable SearchShowroomTownDataTable(string _com, string cate3, string _searchCatergory, string _searchText);
        //Nuwan 2017.02.24
        [OperationContract]
        List<MST_CHNL_SEARH_HEAD> getAllChannel(string company, string pagenum, string pagesize, string field, string search);

        //Nuwan 2017.02.24
        [OperationContract]
        List<CHANL_WISE_SALES> getChannelWiseSales(List<string> selectedcompany, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, string filterby, string Category, out string error);
        //SUBODANA 2017-03-06
        [OperationContract]
        DataTable RatioDetailsReports(string com, DateTime fdate, DateTime tdate, string item, string model, string cat, string brand);
        //Nuwan 2017.03.14
        [OperationContract]
        List<CUSTOMER_SALES> getCustomerInvDetails(string selectedcompany, string Channel, string Subchnl, string Area, string Region, string Zone, string pc, DateTime SalesFrom, DateTime SalesTo, string Brand, string MainCat, string txtModel, string txtItem, decimal CheckAmount, string filterby, string cat2, string cat3, Int32 visit, string age, string salary, string customer, string invtype, string schemetype, string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype, string user, string dist, string prov);

        // Hushani 2017-04-17
        [OperationContract]
        DataTable GetShowroomByCompanyCAT3DataTable(string _com, string _act, string _ref, string _cat2, string _cat3);

        // Hushani 2017-04-26
        [OperationContract]
        DataTable GetAllDistric();

        //subodana 2017-05-12
        [OperationContract]
        List<DELI_SALE_NEW> GetTotalDeliverSales(List<DeliverSale> deldalelist);

        //Isuru 2017/05/12
        [OperationContract]
        DataTable getexchangereversaldetails(List<string> selectedcompany, string Channel, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, string Category, string type, string pc, string itemcode, string userID, out string _err, out string _path);

        [OperationContract]
        List<BarChartData> getPortAgentDetails(DateTime frmdt, DateTime toDt, List<string> company);

        [OperationContract]
        DataTable getAllRelatedPorts(DateTime frmdt, DateTime toDt, List<string> company);
        [OperationContract]
        DataTable getAllRelatedAgents(DateTime frmdt, DateTime toDt, List<string> company);
        [OperationContract]
        List<BarChartData> getFromPortData(DateTime fromdate, DateTime todate, List<string> selectedcompany, string port = null, string agent = null);

        [OperationContract]
        List<BarChartDataPort> getPortTotal(DateTime frmdt, DateTime toDt, List<string> company);
        [OperationContract]
        List<BarChartDataAgent> getAgentTotal(DateTime frmdt, DateTime toDt, List<string> company);
        [OperationContract]
        DataTable getShipmentContainers(DateTime fromdate, DateTime todate, List<string> company, string port, string agent);
        [OperationContract]
        DataTable getShipmentContainersByAgent(DateTime fromdate, DateTime todate, List<string> company, string port, string agent);
        [OperationContract]
        List<CHANL_WISE_SALES> getChannelWiseDeliverySales(string selectedcompany, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, string filterby, string Category, out string error);
        [OperationContract]
        List<CHANL_WISE_SALES> getChnWiseDeliverySalesWithPre(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, DateTime preFrmDt, DateTime preToDt, string filterby, string Category, out string error);
        [OperationContract]
        List<CHANL_WISE_SALES> getDelSalesWithCate(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, string filterby, string Category, out string error);
        [OperationContract]
        List<CHANL_WISE_SALES> getDelSalesWithCateWithPre(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, DateTime preFrmDt, DateTime preToDt, string filterby, string Category, out string error);
        [OperationContract]
        List<CHANL_WISE_SALES> getSpecialCriSales(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, string filterby, string Category, out string error);
        [OperationContract]
        List<CHANL_WISE_SALES> getSpecialCriSalesCate(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, string filterby, string Category, out string error);
        [OperationContract]
        List<CHANL_WISE_SALES> getSpecialCriSalesCatePY(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, DateTime preFrmDt, DateTime preToDt, string filterby, string Category, out string error);
        [OperationContract]
        List<CHANL_WISE_SALES> getSpecialCriSalesPyWithCate(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, DateTime preFrmDt, DateTime preToDt, string filterby, string Category, out string error);
        [OperationContract]
        List<BI_TRGT_SALE> getTargetAlignmentDataNew(string defby, string catedifon, string calccd, string pdcd, DateTime frmdt, DateTime todt, out string error);
        [OperationContract]
        DataTable getCostAnalysisData(string company, DateTime fromdt, DateTime todt, string type, out string error);
        [OperationContract]
        DataTable getCostAnalysisDataSummery(string company, DateTime fromdt, DateTime todt, string type, out string error);
        [OperationContract]
        List<MST_SEGMANT> getSegmantList(string pgeNum, string pgeSize, string searchFld, string searchVal);
        [OperationContract]
        List<BOND_BALANCE> getBondBalanceDetails(string userid, string brand, string cate1, string cate2, string cate3, string brandmanager, bool firsttimerun, out string error);
        [OperationContract]
        string getLastRunTimeAlt(string code);

        //Dilshan 
        [OperationContract]
        List<SalesInventoryAge> getsalesinventoryage(string selectedcompany, DateTime fromdate, DateTime todate, string user, string status, out string error);
        //Dilshan 
        [OperationContract]
        List<GPAnalysis> getdeliveredsales(string selectedcompany, DateTime fromdate, DateTime todate, string user, string itemModel, string itemCode, string brand, string mainCat, string Category2, string filterby);
        //Dilshan 
        [OperationContract]
        List<ITService> getitservicedetails(string selectedcompany, DateTime fromdate, DateTime todate, string user, string itemModel, string itemCode, string brand, string mainCat, string Category2, string filterby);
        
        //Nuwan 2017.12.26
        [OperationContract]
        List<MST_CHNL_SEARH_HEAD> getSearchChannelList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Nuwan 2017.12.26
        [OperationContract]
        List<MST_HIC_SEARH_HEAD> getSearchSubChannelList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl);
        //Nuwan 2017.12.27
        [OperationContract]
        List<MST_HIC_SEARH_HEAD> getSearchAreaList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl, string schnl);
        //Nuwan 2017.12.27
        [OperationContract]
        List<MST_HIC_SEARH_HEAD> getSearchRegionList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl, string schnl, string area);
        //Nuwan 2017.12.27
        [OperationContract]
        List<MST_HIC_SEARH_HEAD> getSearchZoneList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl, string schnl, string area, string region);
        //Nuwan 2017.12.27
        [OperationContract]
        List<MST_HIC_SEARH_HEAD> getSearchPCList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl, string schnl, string area, string region, string zone);
        //Nuwan 2018.01.18
        [OperationContract]
        DataTable getInventoryTurnOverDetails(DateTime fromdt, DateTime todt, string stus, string company, Int32 col, out string error);
        //Nuwan 2018.1.19
        [OperationContract]
        DataTable getInventoryMonthEndBalance(DateTime fromdt, DateTime todt, string stus, string company, Int32 col, out string error);
        //Nuwan 2018.1.19
        [OperationContract]
        DataTable getInventoryAvgCost(DateTime fromdt, DateTime todt, string stus, string company, Int32 mntcnt, Int32 cnt, out string error);
        //Nuwan 2018.01.21
        [OperationContract]
        DataTable getInventoryTurnOverDetailsAll(string comlst, string statusLst, DateTime fdt, DateTime tdt, bool withprevyr, string BrandMngr, string userid, out string error);
        //nuwan 2018.02.05
        [OperationContract]
        List<BOND_BALANCE_AAL> getBondBalanceDetailsAAL(string userid, out string error);
        //Nuwan 2018.04.03
        [OperationContract]
        List<MST_HIC_SEARH_HEAD> getRelatedPclist(string pgeNum, string pgeSize, string searchFld, string searchVal, string channel, string company, string userId);
        //Nuwan 2018.04.04
        [OperationContract]
        List<GLB_PROFITABILITY> getProfitabilityData(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, List<SELECTED_PC> pc, string cate, string com, string user, out string error);
        //Nuwan 2018.04.05
        [OperationContract]
        List<GLB_PROFITABILITY> getProfitabilityDetails(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, List<SELECTED_PC> pc, string cate, string com, string user, Int32 headid, Int32 groupid, out string error);
        //Dulaj 2018 Mar 27

        [OperationContract]
        List<CUSTOMER_SALES> getCustomerDetails(string selectedcompany, string Channel, string Subchnl, string Area, string Region, string Zone, string pc, DateTime SalesFrom, DateTime SalesTo, string Brand, string MainCat, string txtModel, string txtItem, decimal CheckAmount, string filterby, string cat2, string cat3, Int32 visit, string age, string salary, string customer, string invtype, string schemetype, string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype, string user, string dist, string prov, string CheckMobile);
        [OperationContract]
        List<CUSTOMER_SALES> getInvDetails(string selectedcompany, string Channel, string Subchnl, string Area, string Region, string Zone, string pc, DateTime SalesFrom, DateTime SalesTo, string Brand, string MainCat, string txtModel, string txtItem, decimal CheckAmount, string filterby, string cat2, string cat3, Int32 visit, string age, string salary, string customer, string invtype, string schemetype, string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype, string user, string dist, string prov);
        [OperationContract]
        List<USR_DEF_TEMP> getUsrTemplate(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type);
        //Nuwan 2018.05.03 
        [OperationContract]
        List<GLB_PROFITABILITY> getProfitabilityPcDetails(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, List<SELECTED_PC> pc, string cate, string com, string user, Int32 headid, Int32 groupid, Int32 eleid, out string error);

        // Hushani 2018.06.08
        [OperationContract]

        DataTable GetHeroIntColors(string _type, string _cat1, string _cat2, string _brand);
        //dilshan 13/06/2018
        [OperationContract]
        List<BMT_REF_HEAD> getBIToolInventory(string _searchValue, string pgeNum, string pgeSize, string searchFld, string propertyType);
        [OperationContract]
        List<BMT_REF_HEAD> getBIToolImport(string _searchValue, string pgeNum, string pgeSize, string searchFld, string propertyType);
        //dilshan 2018/06/14
        [OperationContract]
        DataTable getInventorySerialAge(string _cat1, string _brnd, string locHircCd, string locHircDesc, string status, string type, string model, string itemCd, string company, string inclution, bool cate1, bool cate2, bool cate3, string brandMngr, bool stus, string column, List<BRND_NEW_STUS> stusForTyp, string agefrom, string ageto, string agefromto);
        [OperationContract]
        DataTable getInventorySerialAgeHeading(string column, string type);
        [OperationContract]
        DataTable getImportSerialAgeHeading(string column, string type);
        [OperationContract]
        DataTable getInventorySerialAgeHeadingCol(string column, string type);
        [OperationContract]
        DataTable getImportSerialAgeHeadingCol(string column, string type);
        [OperationContract]
        DataTable getInventorySerialAgeAsAt(string _cat1, string _brnd, string locHircCd, string locHircDesc, string status, string type, string model, string itemCd, string company, string inclution, bool cate1, bool cate2, bool cate3, string brandMngr, bool stus, string column, List<BRND_NEW_STUS> stusForTyp, DateTime asAtDtStrng, string agefrom, string ageto, string agefromto, DateTime asAtDtStrngfrom, DateTime asAtDtStrngto, string userId);
        [OperationContract]
        DataTable getImportSerialAgeAsAt(string _cat1, string _brnd, string locHircCd, string locHircDesc, string status, string type, string model, string itemCd, string company, string inclution, bool cate1, bool cate2, bool cate3, string brandMngr, bool stus, string column, List<BRND_NEW_STUS> stusForTyp, string asAtDtStrng, string asAtDtfrom, string asAtDtto, string grnno, string tobondno, string serialno, string userId);
        //Nuwan 2018.07.04
        [OperationContract]
        List<GV_SEARCH_HEAD> GetItemByType(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type);
        //Nuwan 2018.07.04
        [OperationContract]
        Int16 UpdateAutoNumber(MasterAutoNumber _masterAutoNumber, Int32 number);

        //Pasindu 2018/07/11
        [OperationContract]
        List<INVOICE_MAIN_TYPE> getInvoiceTypesMainTp();

        //Pasindu 2018/07/12
        [OperationContract]
        List<MAIN_CAT2_SEARCH> getCategorySubSearch(string pgeNum, string pgeSize, string searchFld, string searchVal, string cate);

        //Pasindu 2018/07/16
        [OperationContract]
        List<SEARCH_FAST_MOVING_DET> getFMIDetails(string p_company, DateTime p_fromdate, DateTime p_todate, DateTime p_prev_fromdate, DateTime p_prev_todate, DateTime p_fromasat, string p_chanel, string p_subchanel, string p_region, string p_brandmanager, string p_maincat, string p_subcat, string p_brand, string p_invoicetype);

        //Pasindu 2018/07/18
        [OperationContract]
        List<PROVINCES_LIST> getProvinceList();

        //Pasindu 2018/07/22
        [OperationContract]
        List<SEARCH_SLOW_MOVING_DET> getSMIDetails(string p_company, DateTime p_fromdate, DateTime p_todate, string p_chanel, string p_subchanel, string p_region, string p_brandmanager, string p_maincat, string p_subcat, string p_brand, string p_invoicetype);

        //Pasindu 2018/07/25
        [OperationContract]
        List<SEARCH_FAST_MOVING_DET> getFMIDetailsArrivalOP(string company, DateTime p_asat);

        ////Pasindu 2018/07/25
        //[OperationContract]
        //DataTable ProfitAnalyser(string Chanel, string Item, string InvType, string PriceBook, string PriceLevel, string Schema, string Company);
        //SUBODANA 2018-07-30
        [OperationContract]
        List<ref_bud_ele_form> GetAccDynamicFields(string _company);
        //SUBODANA 2018-07-30
        [OperationContract]
        Decimal LatestBIPrice(string Com, string Item, string PriceBook, string PriceLevel);
        //SUBODANA 2018-07-30
        [OperationContract]
        decimal LatestBICost(string Com, string Item);
        //SUBODANA 2018-08-09
        [OperationContract]
        Decimal GetInsTPVal(string com, string ele, DateTime _date, string _item, string _brand, string _maincat, string _cat, string btu, string model);
        [OperationContract]
        List<AGING_DET> getAgingDetails(string company);

        [OperationContract]
        DataTable getHPaccCompAsAt(string _schmcode, string _schmterm, string locHircCd, string locHircDesc, string company, Int32 curmonth, Int32 curyear, Int32 premonth, Int32 preyear);
        //dilshan 2017-07-12
        [OperationContract]
        List<HP_ACC_COMP> getHPaccCompAsAt_NEW(string _schmcode, string _schmterm, string locHircCd, string locHircDesc, string company, Int32 curmonth, Int32 curyear, Int32 premonth, Int32 preyear);
        //Dilshan 13/07/2018
        [OperationContract]
        List<BMT_REF_HEAD> getHPBIToolProperties(string _searchValue, string pgeNum, string pgeSize, string searchFld, string propertyType);
        //Dilshan 16/07/2018
        [OperationContract]
        string getHPBMSalesDetails(DateTime _FromDate, DateTime _ToDate, string allOne, string allTwo, string allThree, string _invdate, string _com, string _user, DataTable _col, DataTable _row, DataTable _val, DataTable _col1, Dictionary<string, string[]> dataValues, out DataTable _result, out DataTable _header, int _foc, int _intercom, int intcurdate, int _inthpsales, int _intcrecardsale, int _servicecharge, int _intitem, string hpinttype, int _arrsAmt, int _collPeriod, int _term, DataTable dtNewColumn, List<HPFilter> _lst, out string _err);
        //Sanjeewa 2018-08-16
        [OperationContract]
        //string getHPBMSalesDetails1(DateTime _FromDate, DateTime _ToDate, string allOne, string allTwo, string allThree, string _invdate, string _com, string _user, DataTable _col, DataTable _row, DataTable _val, DataTable _col1, Dictionary<string, string[]> dataValues, out DataTable _result, out DataTable _header, int _foc, int _intercom, int intcurdate, int _inthpsales, int _intcrecardsale, int _servicecharge, int _intitem, string hpinttype, int _arrsAmt, int _collPeriod, int _term, DataTable dtNewColumn, List<HPFilter> _lst, out string _err);
        string getHPBMSalesDetails1(DateTime _FromDate, DateTime _ToDate, string allOne, string allTwo, string allThree, string _invdate, string _com, string _user, DataTable _col, DataTable _row, DataTable _val, DataTable _col1, Dictionary<string, string[]> dataValues, out DataTable _result, out DataTable _header, int _foc, int _intercom, int intcurdate, int _inthpsales, int _intcrecardsale, int _servicecharge, int _intitem, string hpinttype, int _arrsAmt, int _collPeriod, int _term, DataTable dtNewColumn, List<HPFilter> _lst, out string _err, bool listing);

        //Dilshan 16/07/2018
        [OperationContract]
        List<BMT_REF_HEAD> LoadHPBIToolDetailsByName(string _columnName);
        //Dilshan 19/07/2018
        [OperationContract]
        DataTable getHPDebtorsArrAsAt(string locHircCd, string locHircDesc, string company, Int32 curmonth, Int32 curyear, string userId);
        //Dilshan 19/07/2018
        [OperationContract]
        DataTable getHPDebtorsArrAsAtSum(string locHircCd, string locHircDesc, string company, Int32 curmonth, Int32 curyear, string userId);
        //Nuwan 2018.08.16
        [OperationContract]
        DataTable getStockBalanceDetails(string com, string brand, string mainCat, DateTime asatDt, string chnl, string schnl, string BrandMngr,
            bool currdt, string grp1, string grp2, string grp3, string grp4, string grp5, string grp6, string grp7, string itmcd, string model, string location, string area,
            string region, string zone, string item, string userid, out string error);
        //Nuwan 2016.08.21
        [OperationContract]
        bool downloadPNLDetails(string com, string userid, string grp, string cate, List<SELECTED_PC> pc, DateTime frmdt, DateTime todt, string accgrp, out string error);

        //subodana 2018-08-23
        [OperationContract]
        ProductEvl ProductEvaluation(string company, string Scheem, string Item, string Type, string finratec,
             string cashcommrt, string creditcommrt, string SerIncoRt, string intrsthprt, string DiriyaRt,
             string dpcommrt, string inscommrt, string Pricebook, string Pricelevel, out string err, out List<CashFlow> clist);
        //Nuwan 2018/08/23
        [OperationContract]
        List<BMT_HPTERM_HEAD> getHPTerem(string searchVal, string pgeNum, string pgeSize, string searchFld);
        //Nuwan 2018.08.23
        [OperationContract]
        List<HPSCHCATE_SEARCH> getHPSchCate(string _searchValue, string _pageNum, string _pageSize, string _serachField);
        //Nuwan 2019.01.01
        [OperationContract]
        string getBudgetDetails(DateTime _strFromdate, DateTime _strToDate,DateTime _strFromdateOne,DateTime _strToDateOne,string allOne, string company, string userId, DataTable dtColumn, DataTable dtRow, DataTable dtValue, Dictionary<string, string[]> dataValues, string group, out DataTable dtReport, out DataTable _header, out string outParam, out string error);
        //Nuwan 2019/jan/03
        [OperationContract]
        List<HPSCHCATE_SEARCH> getBudgetElements(string searchVal, string pgeNum, string pgeSize, string searchFld, string type);
        //Nuwan 2018/Jan/04
        [OperationContract]
        List<LOC_SEARCH> searchLocation(string searchVal, string pgeNum, string pgeSize, string searchFld, string selectCom);

        //tharanga 2018/12/28
        [OperationContract]
        List<GLB_PROFITABILITY> getBudgetReport(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, List<SELECTED_PC> pc, string cate, string com, string user, Int32 headid, Int32 groupid, string report_type, out string error);
        //tharanga 2019101/07
        [OperationContract]
        List<GLB_PROFITABILITY> getBudgetReport_cat_wise(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, List<SELECTED_PC> pc, string cate, string com, string user, Int32 headid, string CategoryWise, string report_type, out string error);

        // hushani 2019/01/31
        [OperationContract]
        DataTable GetAutoShowroomForEMS(string _com, string _ope_cd, string _cat2, string _cat3);

        //Nuwan 2019/01/31
        [OperationContract]
        List<INVENTORY_SHIPMENT> getInventoryShipmentdet(Int32 pagenum, Int32 pagesize, string companylst, string runtp, DateTime frmdt, DateTime todt, string BrandMngr, string Brand, string MainCat, string txtModel, string txtItem, out List<ITEM_BI_AGE> ageitem, out List<SI_BAL_DET> sibal, out string error);
        //NUwan 2019/02/05
        [OperationContract]
        List<INVENTORY_SHIPMENT_SI> loadBLItemDetails(Int32 pagenum, Int32 pagesize, string checkcom,string blno, out string error);
        //Added by Udesh 18-Jan-2019
        [OperationContract]
        List<ProfitCenterPerformanceFilter> getProfitCentersByShowroomMgr(string com, string channel, string subChannel, string area, string region, string zone, string pc_code, string opteam, string town, string district, string province, string userId);
        
        //Added by Udesh 21-Jan-2019
        [OperationContract]
        List<SALES_MNGR_SEARCH_HEAD> getSalesManagers(string pgeNum, string pgeSize, string searchFld, string searchVal, string companies);

        //Added by Udesh 21-Jan-2019
        [OperationContract]
        bool SaveCommaSeparatedValues(string commaValue, string valueType, string reportType, string userID, out string error);

        //Added by Udesh 11-Jan-2019
        [OperationContract]
        void RemoveCommaSeparatedValues(string reportType, string userID);
        
        //Added by Udesh 21-Jan-2019
        [OperationContract]
        List<BMT_REF_HEAD> getPcPerfomanceProperties(string _searchValue, string _pageNum, string _pageSize, string _serachType, string _propertyType);
        
        //Added by Udesh 21-Jan-2019
        [OperationContract]
        DataTable getPcPerformanceReport(string com, string pc, DateTime fromDate, DateTime toDate, bool isMainProfitCenter, string userId);

        //Added by Udesh 21-Jan-2019
        [OperationContract]
        DataTable getProfitCenterHeader(string column, string type);
        //Nuwan 2019/feb/07
        [OperationContract]
        List<ITEM_SALE_SUMM>  getBiSalesItemInv(string checkcom,string itemcode,DateTime fromdt,DateTime todt, out string error);
    } 

}