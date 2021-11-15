using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FF.BusinessObjects;
using System.Data;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;
using FF.BusinessObjects.General;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.Account;
using FF.BusinessObjects.Enadoc;

namespace FF.Interfaces
{
    /// <summary>
    /// This is a Interface class for common search functionalty.
    /// Created By : Miginda Geeganage.
    /// Created On : 16/03/2012
    /// Modified By :
    /// Modified On :
    /// </summary>
    [ServiceContract]
    public interface ICommonSearch
    {
        //kapila
        [OperationContract]
        DataTable Get_PendingDoc_ByRefNo(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila
        [OperationContract]
        DataTable GetAvailableSerialSCM(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila
        [OperationContract]
        DataTable SearchPromoCommDefData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchLoyaltyCardNo(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila
        [OperationContract]
        DataTable GetCancelRequest(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetQuotation4Inv(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila
        [OperationContract]
        DataTable getSearchFAItemStatusData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable getSearchFAItemData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable searchGRNData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchAgreementData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan
        [OperationContract]
        DataTable SearchAllVehicles(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Created By : Mignda Geeganage On 12/03/2012
        /// </summary> 
        [OperationContract]
        DataTable GetCommonSearchData(string _searchCatergory, string _searchText);
        /// <summary>
        /// Created By : Mignda Geeganage On 12/03/2012
        /// </summary> 
        [OperationContract]
        DataTable GetLocationSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Created By : Mignda Geeganage On 20/03/2012
        /// </summary>       
        [OperationContract]
        DataTable GetUserLocationSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Created By : Mignda Geeganage On 20/03/2012
        /// </summary>       
        [OperationContract]
        DataTable GetUserProfitCentreSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Available Serial Search
        //Code by Prabhath on 19/03/2012
        [OperationContract]
        DataTable GetAvailableSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Available Serial Search
        //Code by Prabhath on 09/08/2012
        [OperationContract]
        DataTable GetAvailableSerialWithOthSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Available None-Serial Search
        //Code by Chamal De Silva on 05/07/2012
        [OperationContract]
        DataTable GetAvailableNoneSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila 6/7/2012
        [OperationContract]
        DataTable GetIssuedSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetSearchDataBySerial(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchSubLocationData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetIssuedSerialSearchDataOth(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Available Serial Search
        //Code by Prabhath on 19/03/2012
        [OperationContract]
        DataTable GetItemSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila
        [OperationContract]
        DataTable GetItemSearchDataByCat(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetCustGradeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetCustSatisSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetCustQuestSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila 26/5/2014
        [OperationContract]
        DataTable Getareasearchdata(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila 26/5/2014
        [OperationContract]
        DataTable Getzonesearchdata(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila 26/5/2014
        [OperationContract]
        DataTable Getregionsearchdata(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetGenDiscSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Code by Prabhath on 13/09/2012
        [OperationContract]
        DataTable GetItemforInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila
        [OperationContract]
        DataTable GetVItemforInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchMasterTaxCodes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetPreferLocSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila
        [OperationContract]
        DataTable SearchMasterTaxRateCodes(string _initialSearchParams, string _searchCatergory, string _searchText);


        //kapila 21/5/2014
        [OperationContract]
        DataTable GetServiceAgentLocation(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchBusDesigData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable searchDepositBankCode(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetItemSupplierSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila
        [OperationContract]
        DataTable GetDepositChequeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable searchPayCircularData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchBusDeptData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable searchTownData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable CustSatisfacSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GatePassSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable searchOfficeTownData(string _initialSearchParams, string _searchCatergory, string _searchText);
        /// <summary>
        /// Code by Miginda on 17/04/2012
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);


        /// <summary>
        /// Code by Darshana 26-11-2012
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);


        /// <summary>
        /// Get the Invoice types fo the particuler profit center
        /// Written by Prabhath on 26/04/2012
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns> Datatabe of selected data </returns>
        /// 
        [OperationContract]
        DataTable GetInvoiceTypeData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Get Price Books for the particuler pcenter/comapny/invoice type
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns>Datatable of selected data</returns>
        [OperationContract]
        DataTable GetPriceBookData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Get Price Books for the particuler comapny 
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns>Datatable of selected data</returns>
        [OperationContract]
        DataTable GetPriceBookByCompanyData(string _initialSearchParams, string _searchCatergory, string _searchText);


        /// <summary>
        /// Get Price Levels for the particuler pcenter/comapny/invoice type/Price Book
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetPriceLevelData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Get company allow item status for the particuler company
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCompanyItemStatusData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Get price level item status for the particuler company/price book/price level
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetPriceLevelItemStatusData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Get customer details according to the requirements
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <param name="_returnColumn"> Hard Code value for differentiate the out-come, Values ('CODE','MOBILE') </param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCustomerGenaral(string _initialSearchParams, string _searchCatergory, string _searchText, string _returnCol, string _returnColDesc);

        /// <summary>
        /// Get Currency details
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCurrencyData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Get Employee details as per the employee category
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetEmployeeData(string _initialSearchParams, string _searchCatergory, string _searchText);


        /// <summary>
        /// add by darshana
        /// Get Supplier Details
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetSupplierData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetClaimSupplierData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// add by darshana
        /// Get save purchase orders
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetPurchaseOrders(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetPurchaseOrdersByDate(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        /// <summary>
        /// add by darshana
        /// Get save purchase orders
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetReceiptTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// add by darshana
        /// Get Bank Details
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetBusinessCompany(string _initialSearchParams, string _searchCatergory, string _searchText);


        /// <summary>
        /// add by darshana
        /// Get Bank Account
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetBankAccounts(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// add by darshana
        /// Get Supplier Details
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>

        [OperationContract]
        DataTable GetCreditNoteAll(string _initialSearchParams, string _searchCatergory, string _searchText, string _com, string _pc);

        [OperationContract]
        DataTable GetPriceLevelByBookData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetDivision(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetReceipts(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetManagerIssuReceipts(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila 20/4/2015
        [OperationContract]
        DataTable GetAllBankAccounts(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 15/08/2012
        [OperationContract]
        DataTable GetReceiptsByType(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetOutstandingInvoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetOutstandingInvoiceTBS(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetOutsidePartySearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetCountrySearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetGroupSaleSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetCompanySearchData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //ISuru 2017/03/15
        [OperationContract]
        DataTable GetManagersetupSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //fazan
        [OperationContract]
        DataTable GetCompanySearchDetails(string _initialSearchParams, string _searchCategory, string _searchText);

        //fazan added 19 Nov 2015
        [OperationContract]
        DataTable GetBLDetails(string initialSearchParams, string bl_no);

        //fazan added 21 Nov 2015
        [OperationContract]
        DataTable GetBLALL(bool status);

        [OperationContract]

        //fazan added 21 Nov 2015
        DataTable GetSubSerial(string itemcode, string serialcode);

        //fazan added 21 Nov 2015
        [OperationContract]
        DataTable GetSerialWarrantyDetails(string serialcode);


        //fazan added 23 nov 2015
        [OperationContract]
        DataTable GetSearchBLByDate(string company);

        [OperationContract]
        DataTable FilterDateRange(string company, string from, string to);


        //fazan 23 nov 2015
        [OperationContract]
        DataTable SearchAllBL(string company, string bl_no, string eta_frm, string eta_to, string clearance_frm, string clearance_to);

        //fazan 23 nov 2015
        [OperationContract]
        DataTable BL_Items(string doc_no, string _toBond);

        //fazan 24 nov 2015
        [OperationContract]
        DataTable LoadDstinctBL_No();

        // fazan 24-nov-2015
        [OperationContract]
        Int32 UpdateBLDetails_shipping(string doc_clear, string enrty_no, DateTime hand_over, string actual_eta, string blno, string p_loc, Int32 _actLead, DateTime filerec, DateTime actualClrDt, string clrUsr);

        [OperationContract]
        DataTable Container_Count(string blno);

        [OperationContract]
        DataTable GetGrnDt(string company, string doc_no, int line_no);




        [OperationContract]
        DataTable GetChannelSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila 19/5/2017
        [OperationContract]
        DataTable GetJobRegNoSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable Getloc_HIRC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetCat_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetCat_ItemSearchData(string _type, string _mcat, string _cat, string _brand, string _model, string _item, string _desn);

        [OperationContract]
        DataTable GetInventoryTrackerSearchData(string _initialSearchParams);

        //kapila
        [OperationContract]
        DataTable searchFinAdjTypeData(string _initialSearchParams, string _searchCatergory, string _searchText);

        /// <summary>
        /// Created By :Shani Waththuhewa On 17/07/2012
        /// To get all Profit centers in the company
        /// </summary>   
        [OperationContract]
        DataTable GetPC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 17/07/2012
        [OperationContract]
        DataTable GetPriceItemSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by darshana on 19/07/2012
        [OperationContract]
        DataTable GetInvoiceByAcc(string _initialSearchParams, string _searchCatergory, string _searchText);

        //23/07/2012
        //sachith
        [OperationContract]
        DataTable GetItemDocSearchData(string _initialSearchParams);

        //23/07/2012
        //sachith
        [OperationContract]
        DataTable GetItemSerialSearchData(string _initialSearchParams);

        //Shani  25/07/2012
        [OperationContract]
        DataTable Get_SchemesCD_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani  26/07/2012
        [OperationContract]
        DataTable Get_PriceTypes_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Prabhath  26/07/2012
        [OperationContract]
        DataTable GetWarrantySearchByWarrantyNoSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Prabhath  26/07/2012
        [OperationContract]
        DataTable GetWarrantySearchBySerialNoSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Prabhath  26/07/2012
        [OperationContract]
        DataTable GetGeneralRequestSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        // Nadeeka 11/02/2015
        [OperationContract]
        DataTable GetJobSearchBySerialNoSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Prabhath  26/07/2012
        [OperationContract]
        DataTable GetInvoiceByInvType(string _initialSearchParams, string _searchCatergory, string _searchText);


        /// <summary>
        /// add by darshana
        /// Get invoice items
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetInvoiceItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetDCNItems(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prahath on 14/08/2012
        [OperationContract]
        DataTable GetHpAccountSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetInsPayReqAccSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetHpAccountAdjSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Wrritn by darshana 15/08/2012
        [OperationContract]
        DataTable GetInsuCompany(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Wrritn by darshana 15/08/2012
        [OperationContract]
        DataTable GetInsuPolicy(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Wrritn by Prabhath 15/08/2012
        [OperationContract]
        DataTable GetBusinessCompanyBranch(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana 16/08/2012
        [OperationContract]
        DataTable GetInvoicebyCustomer(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Shani 24/08/2012
        [OperationContract]
        DataTable GetUserID(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Shani 03/09/2012
        [OperationContract]
        DataTable GetTown(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetTown_new(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith 04/09/2012
        [OperationContract]
        DataTable GetOPE(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Shani 05/09/2012
        [OperationContract]
        DataTable Get_PC_HIRC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetProvinceData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetDistrictByProvinceData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //dilshan
        [OperationContract]
        DataTable GetTownByDistrictData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //dilshan
        [OperationContract]
        DataTable GetGradeByComData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Prabhath on 11/09/2012
        [OperationContract]
        DataTable SearchAvailableItemSerial(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith 2012/09/12
        [OperationContract]
        DataTable GetAllModels(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith 2012/09/12
        [OperationContract]
        DataTable GetAllInsTerms(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana 2012-09-12
        [OperationContract]
        DataTable GetDeliverdInvoiceItemSerials(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Prabhath on 18/09/2012
        [OperationContract]
        DataTable SearchAvlbleSerial4Invoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Prabhath on 06/10/2012
        [OperationContract]
        DataTable SearchBankBranchData(string _initialSearchParams, string _searchCatergory, string _searchText);


        //sachith 
        //2012/10/10
        [OperationContract]
        DataTable GetPromotionSearch(string _initialSearchParams, string _searchCatergory, string _searchText);

        // Nadeeka 10-09-2015
        [OperationContract]
        DataTable GetPriceDefCircularSearch(string _initialSearchParams, string _searchCatergory, string _searchText);


        //sachith 
        //2012/10/10
        [OperationContract]
        DataTable GetLoyaltyTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/10/17 Shani
        [OperationContract]
        DataTable Get_employee_categories(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/10/17 Shani
        [OperationContract]
        DataTable Get_employee_EPF(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable Get_employee_All(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/10/18 Shani
        [OperationContract]
        DataTable Get_sales_subtypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 05/11/2012
        [OperationContract]
        DataTable GetQuotationDetailForInvoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila 8/11/2012
        [OperationContract]
        DataTable Get_LOC_HIRC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/10/17 Shani
        [OperationContract]
        DataTable Get_DocNum_ByRefNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Darshana on 13/11/2012
        [OperationContract]
        DataTable GetAllHpAccountSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetHpAcc4ActiveSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetAgrClaimTypeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetAgrTypeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/11/17 Shani
        [OperationContract]
        DataTable Search_int_hdr_Document(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 03-01-2013
        [OperationContract]
        DataTable Search_int_hdr_Infor(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        // Nadeeka 26-03-2015
        [OperationContract]
        DataTable Search_int_hdr_Infor_Quotation(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //DARSHANA 26-11-2013
        [OperationContract]
        DataTable GetReceiptsDate(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //DARSHANA 26-11-2013
        [OperationContract]
        DataTable GetReceiptsDateTBS(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //DARSHANA 26-11-2013
        [OperationContract]
        DataTable GetPaymentDateTBS(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //kapila
        [OperationContract]
        DataTable GetHpAccountDateSearchData(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);
        //Chamal 02-04-2013
        [OperationContract]
        DataTable Search_GIT_AODs(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Chamal 02-04-2013
        [OperationContract]
        DataTable Search_GIT_AODs_WithLoc(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Chamal 27-03-2013
        [OperationContract]
        DataTable Search_inr_ser_infor(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/10/26 sachith
        [OperationContract]
        DataTable GetSchemaCategory(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/10/26 sachith
        [OperationContract]
        DataTable GetSchemaType(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila 23/11/2012
        [OperationContract]
        DataTable GetServiceAgent(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/11/26Shani
        [OperationContract]
        DataTable Get_hp_parameterTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/11/26 Shani
        [OperationContract]
        DataTable GET_FixAsset_ref(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2012/12/28 Darshana
        [OperationContract]
        DataTable GetCustomerCommon(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2014/12/15 Nadeeka
        [OperationContract]
        DataTable GetSupplierCommon(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2013/01/12 Shani
        [OperationContract]
        DataTable SearchAcServicesJobs(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on 2013/01/12
        [OperationContract]
        DataTable GetSearchMRN(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2013/01/12 Shani
        [OperationContract]
        DataTable GetInvoiceNo(string _initialSearchParams, string _searchCatergory, string _searchText);
        //2013/01/19 sachith
        [OperationContract]
        DataTable GetAvailableSerialNonSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);
        // Nadeeka 14-02-2014
        [OperationContract]
        DataTable GetAvailableSeriaSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2013/01/22 sachith
        [OperationContract]
        DataTable GetGRNItemSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2015-03-09 Nadeeka
        [OperationContract]
        DataTable GetMainItemSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);


        //written by darshana 22/01/2013
        [OperationContract]
        DataTable GetCashInvoicebyCustomer(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2013/01/23 sachith
        [OperationContract]
        DataTable GetDIN(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2013/01/29 shani
        [OperationContract]
        DataTable GetInventoryTrackerSearchData_new(string _initialSearchParams, Boolean allow_SCM, Boolean allow_SCM2);

        //written by darshana 01/02/2013
        [OperationContract]
        DataTable GetHPInvoicebyCustomer(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana 01/02/2013
        [OperationContract]
        DataTable GetHpInvoices(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by shani 07/02/2013
        [OperationContract]
        DataTable SearchReceipt(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract] // darshana on 09-02-2013
        DataTable GetInvoiceForReversal(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 13/02/2013
        [OperationContract]
        DataTable GetSearchInterTransferRequest(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on 14/02/2013
        [OperationContract]
        DataTable GetInvoiceforInterTransferSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on 19/02/2013
        [OperationContract]
        DataTable GetCircularSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetCircularSearchDataByComp(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetCashComCircSearchDataByComp(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetRCC(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetAcInsSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);


        [OperationContract]
        DataTable GetRCC_REQ(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana on 23-02-2013
        [OperationContract]
        DataTable GetSearchHpAdjuestment(string _initialSearchParams, string _searchCatergory, string _searchText);

        //darshana 26-02-2013
        [OperationContract]
        DataTable GetSerInvSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani 26-02-2013
        [OperationContract]
        DataTable Get_Search_cheque(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable Get_Search_return_cheque(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetSearchChqByDate(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchReceiptByAnal3(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchSalesPromotor(string _initialSearchParams, string _searchCatergory, string _searchText);

        // NADEEKA
        [OperationContract]
        DataTable SearchPromotor(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetInsDebitNoteAccSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on  06/03/2013
        [OperationContract]
        DataTable GetWarrantyClaimableInvoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on  06/03/2013
        [OperationContract]
        DataTable GetWarrantyClaimableSerial(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Chamal on 09/03/2013
        [OperationContract]
        DataTable GetMovementDocSubTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana on 11-03-2013
        [OperationContract]
        DataTable Get_Search_Registration_Det(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By sachith on 13/03/2013
        [OperationContract]
        DataTable GetVehicalInsuranceRef(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By sachith on 13/03/2013
        [OperationContract]
        DataTable GetCashCommissionCircularNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana on 14-03-2013
        [OperationContract]
        DataTable Get_Search_Insuarance_Det(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith on 15-03-2013
        [OperationContract]
        DataTable GetVehicalInsuranceDebitNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith on 19-03-2013
        [OperationContract]
        DataTable GetvehicalInsuranceRegistrationNUmber(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetSerialNIC(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetSerialNICAll(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Shani 20-03-2013
        [OperationContract]
        DataTable Get_Hp_ActiveAccounts(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on 22/03/2013
        [OperationContract]
        DataTable GetRawPriceBook(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on 22/03/2013
        [OperationContract]
        DataTable GetRawPriceLevel(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on 05/04/2013
        [OperationContract]
        DataTable GetVehicalJobRegistrationNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana on 05/04/2013
        [OperationContract]
        DataTable GetSchemaTypeByCate(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by darshana on 10-04-2013
        [OperationContract]
        DataTable GetAllScheme(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani 10-04-2013
        [OperationContract]
        DataTable Get_invoiceDet(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila 12/4/2013
        [OperationContract]
        DataTable Get_Party_Types(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on 12/04/2013
        [OperationContract]
        DataTable SearchTransactionType(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila 12/4/2013
        [OperationContract]
        DataTable Get_Party_Codes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //sachith 16/4/2013
        [OperationContract]
        DataTable GetVoucheNos(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana 16-04-2013
        [OperationContract]
        DataTable GetItemBrands(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana 16-04-2013
        [OperationContract]
        DataTable Get_Promotion_Codes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //by darshana 16-04-2013
        [OperationContract]
        DataTable GetCircularSearchByBookAndLevel(string _initialSearchParams, string _searchCatergory, string _searchText);

        //by darshana on 19-04-2013
        [OperationContract]
        DataTable GetCircularSearchForSerial(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetCircularSearchForCancel(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetSerialPriceForCancel(string _initialSearchParams, string _searchCatergory, string _searchText);

        //by darshana on 19-04-2013
        [OperationContract]
        DataTable GetSerialDetForCir(string _initialSearchParams, string _searchCatergory, string _searchText);

        //by darshana on 19-04-2013
        [OperationContract]
        DataTable GetAllProofDocs(string _initialSearchParams, string _searchCatergory, string _searchText);

        //by darshana on 22-04-2013
        [OperationContract]
        DataTable GetInvoiceForReversalOth(string _initialSearchParams, string _searchCatergory, string _searchText);

        //by darshana on 23/04/2013
        [OperationContract]
        DataTable GetHpInvoicesOth(string _initialSearchParams, string _searchCatergory, string _searchText);

        //by sachith on 23-04-2013
        [OperationContract]
        DataTable GetInternalVoucherExpense(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani 23-04-2013
        [OperationContract]
        DataTable Get_GPC(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 03/05/2013
        [OperationContract]
        DataTable GetMovementTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 03/05/2013
        [OperationContract]
        DataTable GetInventoryDirections(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Prabhath on 08/05/2013
        [OperationContract]
        DataTable SearchAvailableGiftVoucher(string _initialSearchParams, string _searchCatergory, string _searchText);

        //darshana on 09-05-2013
        [OperationContract]
        DataTable GetComInvoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        //darshana on 11-05-2013
        [OperationContract]
        DataTable GetItemByType(string _initialSearchParams, string _searchCatergory, string _searchText);

        //sachith on 15-05-2013
        [OperationContract]
        DataTable GetCreditNote(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 15-05-2013
        [OperationContract]
        DataTable Get_system_role(string _initialSearchParams, string _searchCatergory, string _searchText);

        //sachith on 16-05-2013
        [OperationContract]
        DataTable GetCustomerId(string _initialSearchParams, string _searchCatergory, string _searchText);

        //sachith on 17-05-2013
        [OperationContract]
        DataTable GetVehicalRegistrationRef(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 17/05/2013
        [OperationContract]
        DataTable SearchBuyBackItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana on 18-05-2013
        [OperationContract]
        DataTable GetSchemeComByCircular(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Written by Prabhath on 20/05/2013
        [OperationContract]
        DataTable SearchInvoice(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Shani on 22-05-2013
        [OperationContract]
        DataTable Get_system_option_Groups(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 03/06/2013
        [OperationContract]
        DataTable SearchLoyaltyCard(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by darshana on 06-06-2013
        [OperationContract]
        DataTable SearchGsByCus(string _initialSearchParams, string _searchCatergory, string _searchText);
        //written by sachith on 10-06-2013
        [OperationContract]
        DataTable GetLoyaltyCardNos(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith on 11-06-2013
        [OperationContract]
        DataTable GetDocProInvoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Added by Prabhath on 11/06/2013 -><- as per the new requirement of the employee (confirmed Chamal Table - MST_PC_EMP)
        [OperationContract]
        DataTable SearchEmployeeAssignToProfitCenter(string _initialSearchParams, string _searchCatergory, string _searchText);
        //written by sachith on 11-06-2013
        [OperationContract]
        DataTable GetDocProEngine(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith on 11-06-2013
        [OperationContract]
        DataTable GetDocProChassis(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith on 11-06-2013
        [OperationContract]
        DataTable GetVehicalRMVNotSendInvoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith on 11-06-2013
        [OperationContract]
        DataTable GetVehicalRMVNotSendEngine(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by sachith on 11-06-2013
        [OperationContract]
        DataTable GetVehicalRMVNotSendChassis(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 13-06-2013
        [OperationContract]
        DataTable GetVehical_regTxn(string _initialSearchParams, string _searchCatergory, string _searchText);

        //sachith on 17-06-2013
        [OperationContract]
        DataTable GetSearchMRN_AllLoc(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 17-06-2013
        [OperationContract]
        DataTable Get_All_Roles(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 17-06-2013
        [OperationContract]
        DataTable Get_All_SystemUsers(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 17-06-2013
        [OperationContract]
        DataTable Get_All_SecUsersPerimssionTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //darshana on 18-06-2013
        [OperationContract]
        DataTable GetQuotationAll(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetQuotationByCust(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetSupplierQuotation(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 19-06-2013
        [OperationContract]
        DataTable Get_Designations(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 19-06-2013
        [OperationContract]
        DataTable Get_Departments(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetPrefixData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //darshana on 28-06-2013
        [OperationContract]
        DataTable GetItemforInvoiceSearchDataByModel(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 29/06/2013
        [OperationContract]
        DataTable GetSearchDataForPromotion(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetSearchDataForPromoByComp(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Written by sachith on 01/07/2013
        [OperationContract]
        DataTable GetSearchWarrantyExtend(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 02-07-2013
        [OperationContract]
        DataTable Get_AC_SevCharge_itmes(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Sachith on 04-07-2013

        [OperationContract]
        DataTable GetSearchEliteCommCircular(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetSearchProdBonusCircular(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetPritHeirachySearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetIncSchPersonSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetIncSchSaleTypeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetRCCDefSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 09-Jul-2013
        [OperationContract]
        void Send_SMTPMail(string _recipientEmailAddress, string _subject, string _message);

        //Chamal 09-Jul-2013
        [OperationContract]
        int StartTimeModule(string _modName, string _funcName, DateTime _stTime, string _loc, string _com, string _user, DateTime _funcDate);

        //Chamal 09-Jul-2013
        [OperationContract]
        int EndTimeModule(int _seqNo, DateTime _edTime, TimeSpan _diffTime);

        //Chamal 09-Jul-2013
        [OperationContract]
        string GetModuleWiseEmail(string _modName);

        //Shani on 12-07-2013
        [OperationContract]
        DataTable Search_Approve_Permissions(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 12-07-2013
        [OperationContract]
        DataTable Search_ApprovePermission_Levels(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shani on 16-07-2013
        [OperationContract]
        DataTable Search_PurchaseOrders(string _initialSearchParams, string _searchCatergory, string _searchText);
        //sachith 17/07/2013
        [OperationContract]
        DataTable SearchPromotinalCircular(string _initialSearchParams, string _searchCatergory, string _searchText);
        //sachith 17/07/2013
        [OperationContract]
        DataTable Get_employee_sub_categories(string _initialSearchParams, string _searchCatergory, string _searchText);
        //sachith /07/2013
        [OperationContract]
        DataTable GetAdvancedReciept(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2013/01/29 shani
        [OperationContract]
        DataTable GetInventoryTrackerSearchData_new2(string _initialSearchParams, Boolean allow_SCM, Boolean allow_SCM2, out string err);

        //2013/08/16 sachith
        [OperationContract]
        DataTable GetCustomerCodeLoyalty(string _initialSearchParams, string _searchCatergory, string _searchText);

        //DARSHANA 2013-08-21
        [OperationContract]
        DataTable GetCustomerCommonByNIC(string _initialSearchParams, string _searchCatergory, string _searchText);
        //sachith 2013-08-27
        [OperationContract]
        DataTable GetInventoryTrackeChannel(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 10/09/2013
        [OperationContract]
        DataTable GetApprovedRequestData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 10/02/2013
        [OperationContract]
        DataTable GetInvoiceforExchange(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Written by sachith on 10/02/2013
        [OperationContract]
        DataTable GetInvoiceReversal(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by darshana on 02/10/2013
        [OperationContract]
        DataTable GetHpInvoicesForCancel(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 07/10/2013
        [OperationContract]
        DataTable GetExchangedReceiveDoc(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Nadeeka
        [OperationContract]
        DataTable GetSupplierClaimDoc(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Written by Prabhath on 07/10/2013
        [OperationContract]
        DataTable GetExchangedIssueDoc(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 07/10/2013
        [OperationContract]
        DataTable GetAuditJobNo(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Written by Darshana on 25-10-2013
        [OperationContract]
        DataTable GetReverseAccDet(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Written by sachith on 29-10-2013
        [OperationContract]
        DataTable GetAuditCashVerification(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Written by sachith on 29-10-2013
        [OperationContract]
        DataTable GetAuditStockVerification(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 08/11/2013
        [OperationContract]
        DataTable SearchServiceJob(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Written by sachith on 12/11/2013
        [OperationContract]
        DataTable GetInventoryItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 20/11/2013
        [OperationContract]
        DataTable SearchGvCategory(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Written by sachith on 19/12/2013
        [OperationContract]
        DataTable GetPromotionlDiscountHeader(string _initialSearchParams, string _searchCatergory, string _searchText);



        //Written by Prabhath on 20/12/2013
        [OperationContract]
        DataTable GetRCCByStage(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 21/12/2013
        [OperationContract]
        DataTable Get_PC_HIRC_SearchRawData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by sachith on 20/01/2014
        [OperationContract]
        DataTable GetAccountChecklist(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetPBonusVoucherData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Written by sachith on 21/01/2014
        [OperationContract]
        DataTable GetAccountChecklistPOD(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]
        DataTable Search_inr_ser_Supinfor(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetDeductionRefData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetRefundRefData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetSearchPBonusCircular(string _initialSearchParams, string _searchCatergory, string _searchText);
        //darshana on 21-02-2014
        [OperationContract]
        DataTable GetAdvancedRecieptForCus(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Prabhath on 14/03/2014
        [OperationContract]
        DataTable GetCategory3(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by darshana on 24-05-2014
        [OperationContract]
        DataTable GetAllInactiveScheme(string _initialSearchParams, string _searchCatergory, string _searchText);
        //kapila 
        [OperationContract]
        DataTable GetBusinessCompanyData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Darshana on 10-06-2014
        [OperationContract]
        DataTable GetDisVouTp(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Chamal on 19-06-2014
        [OperationContract]
        DataTable GetPromotionVoucherByCircular(string _initialSearchParams, string _searchCatergory, string _searchText);
        //written by darshana on 11-07-2014
        [OperationContract]
        DataTable SearchAvlbleSerial4Item(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Tharaka on 10-07-2014
        [OperationContract]
        DataTable GetPromotionVoucherAll(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Tharaka on 18-07-2014
        [OperationContract]
        DataTable GetMasterItemModel(string _initialSearchParams, string _searchCatergory, string _searchText);
        //written by shanuka on 22-09-2014
        [OperationContract]
        DataTable Load_ItemSearch_details(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Tharaka on 16-09-2014
        [OperationContract]
        DataTable GetChequeVouchers(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by Tharaka on 30-09-2014
        [OperationContract]
        DataTable GetServiceJobDetails(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka on 01-10-2014
        [OperationContract]
        DataTable GetAllEmp(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka on 01-10-2014
        [OperationContract]
        DataTable GetDefectTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Shalika on 10/10/2014
        [OperationContract]
        DataTable Get_Utilization_Location(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchMasterType(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka on 13-10-2014
        [OperationContract]
        DataTable GetServiceJobs(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //kapila
        [OperationContract]
        DataTable GetServiceJobsF3(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Nadeeka 24-06-2015
        [OperationContract]
        DataTable GetServiceJobsWarrClaim(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Tharaka on 23-10-2014
        [OperationContract]
        DataTable Get_Service_Estimates(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka on 30-10-2014
        [OperationContract]
        DataTable Get_LOC_SCV_MRN(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Shalika on 08/11/2014
        [OperationContract]
        DataTable Get_Service_Def_Code(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal on 19/11/2014
        [OperationContract]
        DataTable SearchWarranty(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2014-11-25
        [OperationContract]
        DataTable GET_CHEQUE_BOOKs(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2014-11-26
        [OperationContract]
        DataTable SERCH_TECHCOMMTBYCHNNL(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2014-11-26
        [OperationContract]
        DataTable SEARCH_ESTIMATE_ITEMS(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2014-11-28
        [OperationContract]
        DataTable SEARCH_CONSUMABLE_ITEMS(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2014-12-01
        [OperationContract]
        DataTable SEARCH_SCV_CONF_REQNO(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2014-12-01
        [OperationContract]
        DataTable SEARCH_SCV_CONF_JOB(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2014-12-01
        [OperationContract]
        DataTable SEARCH_SCV_CONF_CUST(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka on 2014-12-04
        [OperationContract]
        DataTable Get_Service_Estimates_ByJob(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 2014-12-08
        [OperationContract]
        DataTable GetServiceRequests(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Tharaka on 2014-12-18
        [OperationContract]
        DataTable SearchServiceInvoice(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //kapila
        [OperationContract]
        DataTable SearchInv4InsReq(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Tharaka on 2014-12-22
        [OperationContract]
        DataTable GetServiceDetailSearials(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetSignOnSeq(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetSignOnSeqByDate(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable GetAllPBLevelByBookData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable Get_CLS_ALW_LOC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Chamal on 2014-12-24
        [OperationContract]
        DataTable SearchScvTaskCateByLoc(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal on 2014-12-24
        [OperationContract]
        DataTable SearchScvPriority(string _initialSearchParams, string _searchCatergory, string _searchText);

        //kapila
        [OperationContract]
        DataTable SearchJobStage(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal on 2014-12-24
        [OperationContract]
        DataTable SearchScvStageChg(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-01-17
        [OperationContract]
        DataTable GetItemByTypeNew(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka on 26-01-2015
        [OperationContract]
        DataTable GetServiceJobsWIP(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Tharaka 2015-02-07
        [OperationContract]
        DataTable GetDOInvoiceNumber(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-02-12
        [OperationContract]
        DataTable GET_ITEM_COMP(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-03-09
        [OperationContract]
        DataTable SEARCH_FAC_COM(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-03-09
        [OperationContract]
        DataTable SEARCH_ENQUIRY(string _initialSearchParams, string _searchCatergory, string _searchText);

        //darshana 2015-03-17
        [OperationContract]
        DataTable SEARCH_SCV_CONF_BY_JOB(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-03-26
        [OperationContract]
        DataTable SEARCH_Invoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-03-31
        [OperationContract]
        DataTable SEARCH_ChargeCode(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-04-01
        [OperationContract]
        DataTable SEARCH_TransferCode(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-04-02
        [OperationContract]
        DataTable SEARCH_Miscellaneous(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka on 2015-06-04
        [OperationContract]
        DataTable GetServiceJobsEnqeuiy(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Tharaka 2015-06-06
        [OperationContract]
        DataTable SearchDrivers(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-06-06
        [OperationContract]
        DataTable SearchVehicle(string _initialSearchParams, string _searchCatergory, string _searchText);

        //PEMIL 2015-06-08
        [OperationContract]
        DataTable SearchDriverTBS(string _initialSearchParams, string _searchCatergory, string _searchText);

        //PEMIL 2015-06-09
        [OperationContract]
        DataTable SearchEmployeeTBS(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-06-10
        [OperationContract]
        DataTable SearchEnquiryWithStage(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-06-13
        [OperationContract]
        DataTable SearchLogHeader(string _initialSearchParams, string _searchCatergory, string _searchText);

        #region Transaction -Imports
        //Rukshan 2015-06-26
        [OperationContract]
        DataTable Get_All_PINo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //PEMIL 2015-09-23
        [OperationContract]
        DataTable SEARCH_PINO_PI(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan
        [OperationContract]
        DataTable SearchBusEntity(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan
        [OperationContract]
        DataTable SearchTraderTerms(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchOrderPlanNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchBank(string _initialSearchParams);

        //Rukshan
        [OperationContract]
        DataTable SearchCostType(string _initialSearchParams);
        //Rukshan
        [OperationContract]
        DataTable SearchCompanyCurrancy(string _initialSearchParams);

        [OperationContract]
        DataTable SearchModel(string _initialSearchParams);

        [OperationContract]
        DataTable SearchItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-07-04
        [OperationContract]
        DataTable SEARCH_FIN_HDR(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SP_Search_Order(string P_ORDERPLAN_NO);

        [OperationContract]
        DataTable SP_Search_OP_Itms(string P_ORDERPLAN_NO);

        //Rukshan
        [OperationContract]
        DataTable SEARCH_FIN_ByID(string _initialSearchParams, string _docno);
        //Rukshan
        [OperationContract]
        DataTable GetPaymentSubTerm(string _PCAD, string _PCD);
        //Rukshan
        [OperationContract]
        DataTable GetBankAccountFacility(string _initialSearchParams, string _SearchText);

        //Rukshan
        [OperationContract]
        DataTable SearchFacilityLimit(string _initialSearchParams);

        //Tharaka 2015-07-09
        [OperationContract]
        DataTable GetAgents(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-07-13
        [OperationContract]
        DataTable SearchBLHeader(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchPRNHeader(string p_itr_req_no);

        [OperationContract]
        DataTable SearchPRNREQNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchPRNItems(Int32 p_itri_seq_no);

        [OperationContract]
        DataTable SearchQuotation(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchOrderPlanNoByStatus(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable LoadKit(string p_iok_op_no);
        #endregion

        //Sahan
        [OperationContract]
        DataTable LoadSupplierCurrencies(string p_mscu_com, string p_mscu_sup_cd);

        //Sahan
        [OperationContract]
        DataTable LoadAllPorts(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan
        [OperationContract]
        DataTable LoadSupplierPorts(string p_mspr_com, string p_mspr_cd);

        //Sahan

        [OperationContract]
        DataTable LoadSupplierItems(string p_mbii_com, string p_mbii_cd);

        //Sahan
        [OperationContract]
        DataTable LoadAllTaxCat(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan
        [OperationContract]
        DataTable LoadAllTaxCodes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan

        [OperationContract]
        DataTable LoadSupplierTaxCodes(string p_mbit_com, string p_mbit_cd);

        // Sahan
        [OperationContract]
        DataTable GetSupplierProfileByGrup(string CustCD, string nic, string DL, string PPNo, string brNo, string mobNo);

        // Sahan
        [OperationContract]
        DataTable LoadCurrencyText(string p_mcr_cd);

        // Sahan
        [OperationContract]
        DataTable LoadCountryText(string p_mcu_cd);
        #region Item Master
        // Nadeeka 09-07-2015
        [OperationContract]
        DataTable GetItemSearchDataMaster(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]
        DataTable GetCat_SearchDataMaster(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]
        DataTable GetItemSearchUOMMaster(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]
        DataTable GetItemSearchColorMaster(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]
        DataTable GetItemSearchTaxMaster(string _initialSearchParams, string _searchCatergory, string _searchText);
        #endregion Item Master
        //Randima 2016-11-23
        [OperationContract]
        DataTable GetBLItems(string _initialSearchParams, string _searchCatergory, string _searchText, int _seqNo);
        //Randima 2016-11-24
        [OperationContract]
        DataTable GetPIItems(string _initialSearchParams, string _searchCatergory, string _searchText, string _piNo);

        [OperationContract]
        DataTable SearchRequestType(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchExecutive(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable Search_INT_REQ(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable Search_INT_REQ_RER(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable Search_INR_SER(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable Search_INT_RES(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        //Code by Rukshan 12-sep-2015 
        DataTable Search_int_hdr_Temp_Infor(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        [OperationContract]
        //Code by Rukshan 12-sep-2015 
        DataTable GET_DeliverByOption(string _com);
        #region PDA // Sahan 24 Aug 2015

        //Sahan 24/Aug/2015

        [OperationContract]
        DataTable LoadUserLocation(string p_user, string p_com);

        //Darshana 24-Sep-2015
        [OperationContract]
        DataTable GetModelMaster(string _initialSearchParams, string _searchCatergory, string _searchText);

        #endregion

        //Tharaka 2015-10-13
        [OperationContract]
        DataTable Search_Routes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 2015-10-14
        [OperationContract]
        DataTable GetInventoryChannel(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 19/Oct/2015
        [OperationContract]
        DataTable SearchPurchaseOrdersFast(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 03/Nov/2015
        [OperationContract]
        DataTable SearchPriceBookWeb(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-11-03
        [OperationContract]
        DataTable SEARCH_BIN(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 04/Nov/2015
        [OperationContract]
        DataTable SearchPriceBookLevelsWeb(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 23/11/2015
        [OperationContract]
        DataTable GetContainerType(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 23/11/2015
        [OperationContract]
        DataTable GetContainerAgent(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-11-24
        [OperationContract]
        DataTable SEARCH_DISPOSAL_JOB(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Lakshan 30/11/2015
        [OperationContract]
        DataTable GetAdminTeamByCompany(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 02/12/2015
        [OperationContract]
        DataTable SEARCH_LC_NO(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 02/12/2015
        [OperationContract]
        DataTable SEARCH_ENTRY_NO(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 08/12/2015
        [OperationContract]
        DataTable GetDemurage_Para(string _com, string _doc, DateTime Fdate, DateTime Todate);

        //09/Dec/2015 Rukshan
        [OperationContract]
        DataTable GetDemurrageCount(string doc);

        //14/Dec/2015 Lakshan
        [OperationContract]
        DataTable GetHsCodeDutyTypes(string _Com);

        //14/Dec/2015 Lakshan
        [OperationContract]
        DataTable GetHsClaimDutyTypes(string _Com);

        //15/Dec/2015 Lakshan
        [OperationContract]
        DataTable SearchOrderPlanNoNew(string _initialSearchParams, string _searchCatergory, string _searchText);

        //15/Dec/2015 Lakshan
        [OperationContract]
        DataTable searchGRNDataNew(string _initialSearchParams, string _searchCatergory, string _searchText);

        //15/Dec/2015 Lakshan
        [OperationContract]
        DataTable SearchGetHsCode(string _initialSearchParams, string _searchCatergory, string _searchText);

        //15/Dec/2015 Lakshan
        [OperationContract]
        DataTable GetHsCodeData(string _country);

        //15/Dec/2015 Lakshan
        [OperationContract]
        DataTable GetHsCodeEntryType(string _country);

        //15/Dec/2015 Lakshan
        [OperationContract]
        DataTable GetHsDutyBU(string country, string hsCode);

        //15/Dec/2015 Lakshan
        [OperationContract]
        DataTable GetHsClaimBU(string country, string hsCode);

        //16/Dec/2015 Lakshan
        [OperationContract]
        DataTable SearchGetPort(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-12-15
        [OperationContract]
        DataTable GetItemSearchDataWithBIN(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2015-12-17
        [OperationContract]
        DataTable SearchHsDefinitionDuty(HsCode hsCode);

        //Lakshan 2015-12-17
        [OperationContract]
        DataTable SearchHsDefinitionClaim(HsCodeClaim hsCodeClaim);

        //Tharaka 2015-12-16
        [OperationContract]
        DataTable SearchSerialByLocBIN(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2015-12-18
        [OperationContract]
        DataTable GetBusinessCompanyDataByCountry(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2015-12-19
        [OperationContract]
        DataTable GetContainerNo(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Lakshan 2015-12-19
        [OperationContract]
        DataTable GetBlNumber(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2015-12-21
        [OperationContract]
        DataTable SEARCH_RECEIPT_UNALO(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Lakshan 2015-12-19
        [OperationContract]
        DataTable Search_Tax_Master_Data(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2015-12-23
        [OperationContract]
        DataTable Search_Tax_CODES(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2015-12-23
        [OperationContract]
        DataTable GetTaxClaimCode(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2015/12/30
        [OperationContract]
        DataTable GetBrandManager(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Dilshan on 02/02/2018
        [OperationContract]
        DataTable GetUser(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Code by Rukshan 05-Jan-2016 
        [OperationContract]
        DataTable GET_Vehicle(string _COM, string _VNO);

        //Code by Rukshan 05-Jan-2016 
        [OperationContract]
        DataTable GET_TRANSPORT_JOB(string _COM, string _VNO);

        //Code by Rukshan 07-Jan-2016 
        [OperationContract]
        DataTable GET_UOM_CAT(string _CAT);

        //Code by Rukshan 07-Jan-2016 
        [OperationContract]
        DataTable GET_ITM_REPL_REASON();

        [OperationContract]
        DataTable SearchOrderPlanNoByStatusNew(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        /*Lakshan 08/01/2016*/
        [OperationContract]
        DataTable GetAllCountryData(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetAvailableSeriaSearchDataWeb(string _initialSearchParams, string _searchCatergory, string _searchText);

        /*Lakshan 14/01/2016*/
        [OperationContract]
        DataTable GET_DISTRICT_DATA(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 19-01-2016
        [OperationContract]
        DataTable SearchGetProcedureCode(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 19-01-2016
        [OperationContract]

        DataTable SearchCusdecHeader(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 18/01/2016
        [OperationContract]
        DataTable SearchWarrentyAmendSerial(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 21/01/2016
        [OperationContract]
        DataTable SearchWarrentyAmendWarrenty(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 21/01/2016
        [OperationContract]
        DataTable SearchWarrentyAmendInvoice(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Lakshan 21/01/2016
        [OperationContract]
        DataTable SearchWarrentyAmendDocNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 21/01/2016
        [OperationContract]
        DataTable SearchWarrentyAmendData(string comp, string loc, string serial1, string warr, string inv, string doc);

        //Rukshan22/01/2016
        [OperationContract]
        DataTable SearchCusdec(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 22/01/2016
        [OperationContract]
        DataTable SearchWarrentyAmendRequestData(string comp, string loc, DateTime fromDate, DateTime toDate);

        //Chamal 24/01/2016
        [OperationContract]
        DataTable SearchCusdecReq(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 28-01-2016
        [OperationContract]
        DataTable GetHSHistory(string com, string country, string docType, string item);


        //lakshan 01-02-2016
        [OperationContract]

        DataTable GetSupplierCommonNew(string _initialSearchParams, string _searchCatergory, string _searchText);

        //lakshan 03-02-2016
        [OperationContract]
        DataTable GetValid_ExchangeRates(string _com, string fromCur, string toCur, DateTime fromDt);

        //lakshan 05-02-2016
        [OperationContract]
        DataTable GetVahicleAllocationEnquiryData(string _com, string _pc, string _veh_tp, string _fleet, DateTime expect_dt, string _whole_day);

        //lakshan 05-02-2016
        [OperationContract]
        DataTable GetVahiclesNotAllocated(string _pc);


        //lakshan 06-02-2016
        [OperationContract]
        DataTable SEARCH_TO_BOND_NO(string _initialSearchParams, string _searchCatergory, string _searchText);

        //nuwan 2016/02/08
        [OperationContract]
        List<MST_CUS_SEARCH_HEAD> getCustomerDetails(string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null);

        //nuwan 2016/02/09
        [OperationContract]
        List<MST_TOWN_SEARCH_HEAD> getTownDetails(string spgeNum, string pgeSize, string searchFld, string searchVal);

        //NUWAN 2016/02/10
        [OperationContract]
        List<MST_PROFIT_CENTER_SEARCH_HEAD> getProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //dilshan 28/09/2017
        [OperationContract]
        List<MST_PROFIT_CENTER_SEARCH_HEAD> getMgProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string mgr);

        //NUWAN 2016/02/10
        [OperationContract]
        List<MST_EMPLOYEE_SEARCH_HEAD> getEmployeeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        [OperationContract]
        List<EMP_SEARCH_HEAD_SCM> getEmployeeDetailsSCM(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //dilshan 28/09/2017
        [OperationContract]
        List<EMP_SEARCH_HEAD_SCM> getManagerDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //dilshan 28/09/2017
        [OperationContract]
        List<hpr_sch_tp> getSchemeType(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //dilshan 28/09/2017
        [OperationContract]
        List<hpr_disr_val_ref> getCircular(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string Cat);
        //dilshan 28/09/2017
        [OperationContract]
        List<BonusDefinition> getCircularcbd(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //dilshan 28/09/2017
        [OperationContract]
        List<hpr_sch_det> getScheme(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Lakshan 2016/02/11
        [OperationContract]
        DataTable GetItemBinSearchData(string _initialSearchParams);

        //Sahan 12 Feb 2016
        [OperationContract]
        DataTable LoadColours();

        //Subodana 2016/02/12
        [OperationContract]
        List<mst_fleet_search_head> getFleetDetails(string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/02/15
        [OperationContract]
        List<MST_BANKACC_SEARCH_HEAD> getDepositedBanks(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Subodana 2016/02/15
        [OperationContract]
        List<MST_SER_PROVIDER_SEARCH_HEAD> getServiceProviderDetails(string pgeNum, string pgeSize, string searchFld, string searchVal);


        //Lakshan 12 Feb 2016
        [OperationContract]
        DataTable SearchMstGradeTypes(GradeMaster obj);

        //Nuwan 2016/02/16
        [OperationContract]
        List<MST_BUSCOM_BANK_SEARCH_HEAD> getBusComBanks(string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/02/16
        [OperationContract]
        List<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD> getBoscomBankBranchs(string bankcd, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //LAkshan 2016/02/16
        [OperationContract]
        DataTable SearchOperation(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Nuwan 2016/02/17
        [OperationContract]
        List<MST_ADVAN_REF_SEARCH_HEAD> getAdvanceRerference(string company, string cusCd, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/02/17
        [OperationContract]
        List<MST_CREDIT_NOTE_REF_SEARCH_HEAD> getCredNoteRerference(string customer, string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/02/19
        [OperationContract]
        List<MST_LOYALTYCARD_SEARCH_HEAD> getLoyaltyCard(string customer, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Lakshan 2016/02/24
        [OperationContract]
        DataTable SearchRefLocCate1();

        //Lakshan 2016/02/24
        [OperationContract]
        DataTable SearchMstChnl(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2016/02/24
        [OperationContract]
        DataTable SearchRefLocCate3(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Nuwan 2016/02/24
        [OperationContract]
        List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> getTransportEnqiurySearch(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/02/25
        [OperationContract]
        List<MST_CHARG_CODE_SEARCH_HEAD> getChargCodeList(string company, string category, string pgeNum, string pgeSize, string searchFld, string searchVal, string pc);

        //Subodana 2016/02/26
        [OperationContract]
        List<MST_COUNTRY_SEARCH> getCountry(string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Lakshan 2016/02/26
        [OperationContract]
        DataTable SearchDocPriceDefDocTp(PriceDefinitionRef _def, string _searchCatergory, string _searchText);

        //Lakshan 2016/02/27
        [OperationContract]
        DataTable SearchDocPriceDefPrBook(PriceDefinitionRef _def, string _searchCatergory, string _searchText);

        //Lakshan 2016/02/27
        [OperationContract]
        DataTable SearchDocPriceDefPrLVL(PriceDefinitionRef _def, string _searchCatergory, string _searchText);

        //Lakshan 2016-Mar-02
        [OperationContract]
        DataTable SearchMainItemsData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Randima 19/Oct/2016
        [OperationContract]
        DataTable SearchMainItemsDataSplit(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2016-Mar-02
        [OperationContract]
        DataTable SearchMainItemsDataSer(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Nuwan 2016/03/03
        [OperationContract]
        List<MST_CHARG_CODE_AIRTVL_SEARCH_HEAD> getChargCodeListArrival(string company, string category, string pgeNum, string pgeSize, string searchFld, string searchVal, string pc);

        //Nuwan 2016/03/03
        [OperationContract]
        List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> getChargCodeListMsclens(string company, string category, string pgeNum, string pgeSize, string searchFld, string searchVal, string pc);

        //Nuwan 2016/03/08
        [OperationContract]
        List<MST_RECEIPT_TYPE_SEARCH_HEAD> getReceiptTypes(string company, string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Nuwan 2016/03/08
        [OperationContract]
        List<MST_DIVISION_SEARCH_HEAD> getDivisions(string company, string profCen, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/03/09
        [OperationContract]
        List<MST_OUTSTANDING_INVOICE_SEARCH_HEAD> getOutstandingInvoice(string company, string profCen, string cusCd, string othText, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/03/09
        [OperationContract]
        List<MST_PROF_CEN_SEARCH_HEAD> getAllProfitCenters(string company, string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Nuwan 2016/03/11
        [OperationContract]
        List<MST_GIFTVOUCHER_ISSSUE_SEARCH_HEAD> getGVISUVouchers(string company, string type, string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Nuwan 2016/03/11
        [OperationContract]
        List<MST_RECEIPT_SEARCH_HEAD> getReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Nuwan 2016/03/11
        [OperationContract]
        List<MST_RECEIPT_SEARCH_HEAD> getUnallowReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal);


        //Subodana 2016/03/18
        [OperationContract]
        List<MST_OUTSTANDING_INVOICE_SEARCH_HEAD> getOutstandingInvoice2(string company, string profCen, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Lakshan 2016/03/18
        [OperationContract]
        DataTable GetBlItmByDocNo(string _docNo);

        //Nuwan 2016/03/23
        [OperationContract]
        List<MST_CHANNEL_SEARCH_HEAD> getChannels(string company, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/03/23
        [OperationContract]
        List<MST_SUBCHANNEL_SEARCH_HEAD> getSubChannels(string channel, string company, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/03/24
        [OperationContract]
        List<MST_SERVICE_CODE_SEARCH_HEAD> getServiceCodes(string company, string profcen, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/03/24
        [OperationContract]
        List<MST_COST_SHEET_SEARCH_HEAD> getCoseSheets(string company, string profcen, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Lakshan 2016/03/30
        [OperationContract]
        DataTable SearchMainItemsComp(string _initialSearchParams, string _searchCatergory, string _searchText);

        //SUBODANA 2016-04-04
        [OperationContract]
        List<MST_CURRENCY_SEARCH_HEAD> GetAllCurrencyNew(string company, string profcen, string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Nuwan 2016/04/08
        [OperationContract]
        List<MST_COMPANIES_SEARCH_HEAD> GetLogCompanies(string company, string type, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Nuwan 2016/04/08
        [OperationContract]
        List<MST_LOGSHEET_SEARCH_HEAD> GetLogSheets(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Nuwan 2016/04/08
        [OperationContract]
        List<MST_ENQUIRY_SEARCH_HEAD> GetLogEnquiries(string company, string userDefPro, string type, string stage, string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Nuwan 2016/04/08
        [OperationContract]
        List<MST_EMPLOYEE_SEARCH_HEAD> GetLogDrivers(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Nuwan 2016/04/08
        [OperationContract]
        List<mst_fleet_search_head> GetLogVehicles(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Rukshan 11/Apr/2016
        [OperationContract]
        DataTable Get_Sales_Ex(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Nuwan 
        [OperationContract]
        List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> geTRANSEnqiurySearch(string company, string userDefPro, string type, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Rukshan 11/Apr/2016
        [OperationContract]
        DataTable GetCustomerBYsalesExe(string _initialSearchParams, string _searchCatergory, string _searchText, string _returnCol, string _returnColDesc);
        //Nuwan 2016/04/30
        [OperationContract]
        List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> getAllEnquirySearch(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Lakshan 30 Apr 2016
        [OperationContract]
        DataTable SearchAdminTeam(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 06 May 2016
        [OperationContract]
        DataTable SEARCH_INT_ADHOC_HDR(string _initialSearchParams, string _searchCatergory, string _searchText);

        //subodana 2016-05-11
        [OperationContract]
        DataTable GetInvoiceTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Isuru 2017-03-13
        [OperationContract]
        DataTable Get_Rcpt_Types(string _initialSearchParams, string _searchCatergory, string _searchText);

        //subodana 2016-05-11
        [OperationContract]
        DataTable GetInvoiceSubTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //subodana 2016-05-12
        [OperationContract]
        List<GEN_CUST_ENQ> GET_TOURS_ENQ(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal, String Status);
        //NUWAN 2016/05/16
        [OperationContract]
        List<MST_PROFIT_CENTER_SEARCH_HEAD> getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId);

        //SUBODANA 2016-05-20
        [OperationContract]
        DataTable Get_BondNumber(string _initialSearchParams, string _searchCatergory, string _searchText);

        //SUBODANA 2016-05-20
        [OperationContract]
        DataTable Get_GrnNumber(string com, string _initialSearchParams, string _searchCatergory, string _searchText);
        //SUBODANA 2016-05-20
        [OperationContract]
        DataTable Get_ReqNumber(string com, string _initialSearchParams, string _searchCatergory, string _searchText);


        //SUBODANA 2016-05-20
        [OperationContract]
        DataTable Get_OtherLoc(string com, string _initialSearchParams, string _searchCatergory, string _searchText);
        //SUBODANA 2016-05-20
        [OperationContract]
        DataTable Get_Operationteam(string com, string _initialSearchParams, string _searchCatergory, string _searchText);

        //SUBODANA 216-05-25
        [OperationContract]
        DataTable Get_Color_All(string _initialSearchParams, string _searchCatergory, string _searchText);

        //SUBODANA 216-05-25
        [OperationContract]
        DataTable SearchApprovedDocNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //SUBODANA 216-05-25
        [OperationContract]
        DataTable Get_ReqApprType(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 25 May 2016
        [OperationContract]
        DataTable SearchInrBinLoc(string _initialSearchParams, string _searchCatergory, string _searchText);
        //NUWAN 2016/05/16
        [OperationContract]
        List<MST_FAC_LOC_SEARCH_HEAD> getFacLocation(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);

        //SUBODANA 216-05-26
        [OperationContract]
        DataTable SearchLoadingPlace(string _initialSearchParams, string _searchCatergory, string _searchText);

        //SUBODANA 216-05-30
        [OperationContract]
        DataTable SearchStockDocNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //SUBODANA 216-05-30
        [OperationContract]
        DataTable SearchClompanyLocation(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Nuwan 2016.05/28
        [OperationContract]
        List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> getOtherChargesForTransport(string service, string company, string pgeNum, string pgeSize, string searchFld, string searchVal, string userDefPro, string addedChgCd);
        //Nuwan 2016.05/28
        [OperationContract]
        List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> getMescChargesWitoutParent(string service, string company, string pgeNum, string pgeSize, string searchFld, string searchVal, string userDefPro, string addedChgCd);

        //Lakshan 31 May 2016
        [OperationContract]
        DataTable SearchExchangeRateHistry(MasterExchangeRate busObj);

        //Lakshan 04 Jun 2016
        [OperationContract]
        DataTable SER_REF_COND_TP(string _initialSearchParams, string _searchCatergory, string _searchText);

        //subodana 2016-06-09
        [OperationContract]
        DataTable SearchReversalInvoice(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //subodana 2016-06-15
        [OperationContract]
        DataTable SearchFileNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 16 Jun 2016
        [OperationContract]
        DataTable SearchProfitCenterByUser(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 16 Jun 2016
        [OperationContract]
        DataTable SearchUserLocationByUser(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 2016-Jun-18
        [OperationContract]
        DataTable SearchCusdecHeaderForInvoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 20 Jun 2016
        [OperationContract]
        DataTable SearchBLHeaderNew(string _initialSearchParams, string _searchCatergory, string _searchText, Int32 isDate, DateTime from, DateTime to);

        //Lakshan 21 Jun 2016
        [OperationContract]
        DataTable SearchCusdecReqNew(string _initialSearchParams, string _searchCatergory, string _searchText, Int32 isDate, DateTime from, DateTime to);
        //Lakshan 21 Jun 2016
        [OperationContract]
        DataTable SearchCusdecHeaderNew(string _initialSearchParams, string _searchCatergory, string _searchText, Int32 isDate, DateTime from, DateTime to);
        //SUBODANA 1 JuLY 2016
        [OperationContract]
        DataTable SearchRoutDetailsnew(string _initialSearchParams, string _searchCatergory, string _searchText);

        //SUBODANA 5 JuLY 2016
        [OperationContract]
        DataTable SearchResvationRequestNo(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //SUBODANA 7 JuLY 2016
        [OperationContract]
        DataTable SearchResvationApproveNo(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Nuwan 2016/07/12
        [OperationContract]
        List<MST_CHARG_CODE_SEARCH_HEAD> getChargCodeListWithType(string company, string category, string pgeNum, string pgeSize, string searchFld, string searchVal, string pc, string type);

        //Randima 2016/07/15
        [OperationContract]
        DataTable SearchBLHeaderCosting(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime fromDate, DateTime toDate);

        //Lakshan 2016/07/16
        [OperationContract]
        DataTable SearchAodDocumentForItemAllocation(string _initialSearchParams, DateTime _dtFrom, DateTime _dtTo, string _searchCatergory, string _searchText);

        //Randima 2016/07/18
        [OperationContract]
        DataTable SearchPurOrdCosting(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime fromDate, DateTime toDate);

        //Randima 2016-07-25
        [OperationContract]
        DataTable SearchItemGRN(string _searchCatergory, string _searchText, string _tp, string _PONo, string item);

        //subodana 2016-08-01
        [OperationContract]
        DataTable GetChanalDetailsNew(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshika 2016/Aug/05
        [OperationContract]
        DataTable GET_CUSDEC_NO(string _searchText);

        //subodana 2016-08-04
        [OperationContract]
        DataTable GetTransportMethods(string _initialSearchParams, string _searchCatergory, string _searchText);

        //subodana 2016-08-04
        [OperationContract]
        DataTable GetTransportParty(string _initialSearchParams, string _searchCatergory, string _searchText);

        //subodana 2016-08-04
        [OperationContract]
        DataTable GetTransportReference(string _initialSearchParams, string _searchCatergory, string _searchText);

        //subodana 2016-08-08
        [OperationContract]
        DataTable GetAODTrackerDOC(string _initialSearchParams, string _searchCatergory, string _searchText);

        //subodana 2016-08-09
        [OperationContract]
        DataTable SearchSerialByLocBINNEW(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 2016-08-11
        [OperationContract]
        DataTable SEARCHLOAD_PLS(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2016 Aug 13
        [OperationContract]
        DataTable Search_int_hdr_Infor_New(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Lakshan 2016 Aug 19
        [OperationContract]
        DataTable SearchBankDetails(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 2016 Aug 19
        [OperationContract]
        DataTable SearchChequeDetailsReturnChk(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _frmChqDte, DateTime _toChqDte);
        //Lakshan 2016 Aug 19
        [OperationContract]
        DataTable GetChequeDetForReturn(RecieptHeader _recHdr, RecieptItem _recItm, DateTime _dtFrom, DateTime _dtTo);

        //Lakshan 2016 Aug 25
        [OperationContract]
        DataTable GetShipmentTrackerDatByClearenseDate(ImportsBLHeader _obj, DateTime _dtFrom, DateTime _dtTo);
        //subodana 2016-09-12
        [OperationContract]
        DataTable GetDocNumforTP(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 13 Sep 2016
        [OperationContract]
        DataTable Search_int_hdr_Infor_new(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Written by Rukshan on 25/sep/2013
        [OperationContract]
        DataTable SearchInvoiceWeb(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);
        //Nuwan 2016.10.17
        [OperationContract]
        List<MST_SRCH_ITM> getItemSearchDetails(string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Randima 2016-10-25
        [OperationContract]
        DataTable GetItmStusByCompany(string _com);
        //nuwan 2016/11/07
        [OperationContract]
        List<DEPO_AMT_SEARCH_HED> getDepositAmounts(string pgeNum, string pgeSize, string searchFld, string searchVal);

        //Lakshan 01 Nov 2016
        [OperationContract]
        DataTable SER_REF_COND_TP_NEW(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 26 Nov 2016
        [OperationContract]
        DataTable GetItemSearchDataForBatch(string _initialSearchParams, string _searchCatergory, string _searchText);

        //RUKSHAN 29 Nov 2016
        [OperationContract]
        DataTable GetKitItemforInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Akila 2016/12/17
        [OperationContract]
        DataTable GetSubReceiptTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Akila 2016/12/31
        [OperationContract]
        DataTable GetItemByLocation(string _initialSearchParams, string _searchCatergory, string _searchText);

        //add by akila 2017/01/05
        [OperationContract]
        DataTable GetInvoiceWithoutReversal(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 03 Jan 2017
        [OperationContract]
        DataTable SearchFleetRegistrationNO(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 10 Jan 2017
        [OperationContract]
        DataTable SearchProductionNoAodOut(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Add by Akila 2017/01/21
        [OperationContract]
        DataTable GetServiceJobsByCustomer(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchCreditNotes(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 25 Jan 2017
        [OperationContract]
        DataTable GetItemSearchDataForProductionPlan(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable GetItemforKit(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable SearchEmployeeByType(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable SearchFleetModel(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable SearchFleetMake(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable SearchFleetFuelType(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable SearchFleetBattryType(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable SearchFleetTaxClass(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable SearchFleetVehClass(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable SearchFleetVehCarryerTp(string _initialSearchParams, string _searchCatergory, string _searchText);
        //subodana 2017-02-13
        [OperationContract]
        List<EMP_SEARCH_HEAD_SCM> getEmployeeCatSCM(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //subodana 2017-02-17
        [OperationContract]
        List<ref_comm_hdr> getCommissionCode(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Lakshan 21 Feb 2017
        [OperationContract]
        DataTable SearchBLHeaderForSI(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchItemGRNnew(string _searchCatergory, string _searchText, string _tp, string _PONo, string item);
        //SUBODANA 2017/03/16
        [OperationContract]
        List<Sar_Type> GetCommissionInvTp(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Udaya 21/03/2017
        //[OperationContract]
        //DataTable Get_UsersByLocations(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]
        DataTable Get_LoadingBays(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable Get_LoadingBaysDoc(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        List<LoadingBay> Get_LoadingBayOtherDetails(string userid, string locid, string locbayid);
        [OperationContract]
        Int32 ExistsLoadingBaysUpdate(string useridU, string locationidU, string loadingBayidU, string userid, string locationid, string loadingBayId, int active, string modUser, DateTime modDate, string modSession, string comCode, string wCom, string wLoc);

        //Isuru 2017/03/21
        [OperationContract]
        DataTable GetCat_SearchData_FORBM(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]

        DataTable Get_UsersFrompda(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetUserLocationFrompda(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]

        DataTable Get_RecordExistInBay(string userid, string locCode, string comCode, string wCom, string wLoc, string lBay);
        [OperationContract]
        DataTable getWarehouseData(string userid, string locid, string comCode);

        //Isuru 2017/03/23
        [OperationContract]
        DataTable GetInv_TypCre_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Isuru 2017/03/27
        [OperationContract]
        DataTable GetInv_Typforupdate_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText, string _pc);

        //Udaya 30/03/2017
        [OperationContract]
        DataTable GetAllEngineNos(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Udaya 30/03/2017
        [OperationContract]
        DataTable GetAllChassiNos(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Udaya 30/03/2017
        [OperationContract]
        DataTable GetAllJobNos(string _initialSearchParams, string _searchCatergory, string _searchText, string _fromDate, string _todate);
        //Udaya 31/03/2017
        [OperationContract]
        DataTable GetAllAodNos(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Udaya 31/03/2017
        [OperationContract]
        DataTable GetAllTechnician(string _initialSearchParams, string _searchCatergory, string _searchText);
        //create by randima modify by lakshan 03 Apr 2017
        [OperationContract]
        DataTable SearchMainItemsDataSplitByItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Akila
        [OperationContract]
        DataTable SearchAllEmployee(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Akila 2017/04/19
        [OperationContract]
        DataTable SearchExchangeRequest(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchForwardInvoice(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchFixAssetRequest(string _initialSearchParams, string _searchCatergory, string _searchText);

        //ISURU 2017/03/24
        [OperationContract]
        DataTable SearchPRNREQNoforall(string _initialSearchParams, string _searchCatergory, string _searchText);

        //ISURU 2017/03/25
        [OperationContract]
        DataTable SearchPRNItemsfordesc(Int32 p_itri_seq_no);

        //ISURU 2017/04/25
        [OperationContract]
        DataTable SearchItemforchange(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Isuru 2017/04/27
        [OperationContract]
        List<PLU_SEARCH_ITM> getItemSearchDetailsforplu(string pgeNum, string pgeSize, string searchFld, string searchVal, string customercode);


        //Isuru 2017/04/28
        [OperationContract]
        DataTable GetCustomerCodeDetails(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Isuru 2017/04/28
        [OperationContract]
        DataTable GetItemCodeDetails(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Udaya 29/04/2017
        [OperationContract]
        DataTable GetUserLocationByRoleAndCompany(string _initialSearchParams, string _searchCatergory, string _searchText);

        //written by tharanga 04/04/2017
        [OperationContract]
        DataTable GetServiceCenterDetails(string _com_CD, string _svc_CD, string _Town_CD, string _searchText);

        //Isuru 2017/05/03
        [OperationContract]
        List<mst_busentity_customer> GetCustomerDetailsforplu(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Isuru 2017/05/09
        [OperationContract]
        DataTable Get_All_Users(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Udaya 09/05/2017
        [OperationContract]
        DataTable creditNoteVirtualItems(string _initialSearchParams, string _searchCatergory, string _searchText, string filter);
        //Udaya 12/05/2017
        [OperationContract]
        DataTable creditNoteInvoiceType(string _initialSearchParams, string _searchCatergory, string _searchText);
        //subodana 2017-05-16
        [OperationContract]
        List<REF_BONUS_HDR> getBonusCodeCode(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string schemeCode);

        //Akila 2017/05/16
        [OperationContract]
        DataTable SearchServiceJobInStage(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2017/05/22
        [OperationContract]
        DataTable SearchChequeCodes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //2017/05/22
        [OperationContract]
        DataTable Searchaccountcode(string _initialSearchParams, string _searchCatergory, string _searchText);
        //2017/06/2 subodana
        [OperationContract]
        List<BTU_SEARCH> getBTUCodes(string pgeNum, string pgeSize, string searchFld, string searchVal, string cat);

        // tHARANGA 2017/06/01
        [OperationContract]
        DataTable GetItemSerNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        // tHARANGA 2017/06/01
        [OperationContract]
        DataTable GetsubJobNo(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);
        // tHARANGA 2017/06/01
        [OperationContract]
        DataTable GetJobNo(string _subjob, string _loc);
        //Udaya 16.06.2017 - collect Credit note SRN 
        [OperationContract]
        DataTable SearchReversalInvoiceForCreditDebit(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);
        //Udaya 30.06.2017 - collect Debit note SRN 
        [OperationContract]
        DataTable SearchReversalInvoiceForDebit(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);
        //Add by akila 2017/06/21 search advisor
        [OperationContract]
        DataTable SearchServiceAdvisor(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Add by lakshan 23 Jun 2017
        [OperationContract]
        DataTable GetServiceJobsWIPWEB(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Add by akila 2017/06/28
        [OperationContract]
        DataTable SearchSupplierData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Add by Lakshan 28 Jun 2017
        [OperationContract]
        DataTable GetQuotationAllWeb(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Add by lakshan 12Aug2017
        [OperationContract]
        DataTable GetItemforBoqMrn(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Add by lakshan 12Aug2017
        [OperationContract]
        DataTable SearchBoqProNoInMrn(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Udaya 14.08.2017
        [OperationContract]
        DataTable SEARCH_PRN_REQNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Add by Lakshan 20Aug2017
        [OperationContract]
        DataTable SearchMrnDocumentsWeb(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _dtFrom, DateTime _dtTo);

        //Add by Lakshan 23Aug2017
        [OperationContract]
        DataTable GetInvTrackerDocData(string _initialSearchParams);

        //Tharanga 2017/08/29
        [OperationContract]
        DataTable Get_route_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Add by Lakshan 11Sep2017
        [OperationContract]
        DataTable SearchSalesOrderWeb(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Add by Lakshan 20Aug2017
        [OperationContract]
        DataTable SearchTempPickItemData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //subodana 2017/09/11
        [OperationContract]
        List<ACCCODESEARCH> getHandAccCodes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc, DateTime date);
        //subodana 2017/09/28
        [OperationContract]
        List<SHWRMMAN_SEARCH> getShowManager(string pgeNum, string pgeSize, string searchFld, string searchVal, string com);
        //Add by Lakshan 12Oct2017
        [OperationContract]
        DataTable GetInventoryTrackerSearchDataWEB(string _initialSearchParams, Boolean allow_SCM, Boolean allow_SCM2, out string err);
        //Added By Udaya 14/Oct/2017
        [OperationContract]
        DataTable SearchRevInvoice(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Lakshan 19Oct2017
        [OperationContract]
        DataTable SearchSerialsByJobNo(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 24Oct2017
        [OperationContract]
        DataTable SearchLocationByLocationCategory(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]
        DataTable GetCourierNos(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharindu 2017-11-13
        [OperationContract]
        DataTable Get_RootTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Akila 2017/11/27
        [OperationContract]
        DataTable SearchNationality(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Tharanaga 2017/11/21
        [OperationContract]
        DataTable Getbrandmng(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Tharanaga 2017/11/28
        [OperationContract]
        DataTable GetDocSubTypes_ADJ(string _initialSearchParams, string _searchCatergory, string _searchText);
        //subodana 2017/12/11
        [OperationContract]
        DataTable GetOutstandingInvoiceweb(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Tharanaga 2017/12/19
        [OperationContract]
        DataTable get_quo_to_inv(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Tharindu 2017-12-07
        [OperationContract]
        DataTable Get_RefernceTypes(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Tharanaga 2017/12/22
        [OperationContract]
        DataTable GET_PRIT_BY_CUST(string _initialSearchParams, string _searchCatergory, string _searchText);
        //subodana 2018/01/16
        [OperationContract]
        DataTable GetCrCdCircular(string _initialSearchParams, string _searchCatergory, string _searchText);
        //tharanga 2018/01/27
        [OperationContract]
        DataTable GetADJREQ_DET(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Add by lakshan 31Jan2018
        [OperationContract]
        DataTable SerachSatProHdr(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _dtFrom, DateTime _dtTo);

        //Tharindu 2018-02-17
        [OperationContract]
        DataTable GetCourierCompany(string refdoc, string loc, string com);

        //Add by lakshan 14Feb2018
        [OperationContract]
        DataTable SearchTempPickItemDataPartial(string _initialSearchParams, string _searchCatergory, string _searchText);

        //akila 2018/01/25
        [OperationContract]
        DataTable SearchEvents(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Dulaj 2018-Mar-19
        [OperationContract]
        DataTable GetDocTypes(string _com, string _loc, string _docName);

        //Akila 2018/03/29
        [OperationContract]
        DataTable SearchDistrictDetails(string _initialSearchParams, string _searchCatergory, string _searchText);
        //subodana 
        [OperationContract]
        DataTable SearchBLHeaderWithSeq(string _initialSearchParams, string _searchCatergory, string _searchText);

        //tharanga 2018/04/09
        [OperationContract]
        DataTable Search_mid_account(string _initialSearchParams, string _searchCatergory, string _searchText);
        //subodana 2018-05-02
        [OperationContract]
        List<Sar_Type> GetPromoCircula(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //pasindu 2018-05-16
        [OperationContract]
        List<RENT_SCH_SEARCH> getRentSCHDetail(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Pasindu 2018-05-16
        [OperationContract]
        List<PAYMENT_TYPES> getPaymentTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Pasindu 2018-05-16
        [OperationContract]
        List<PAYMENT_SUB_TYPES> getPaymentSubTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string p_type);


        //tharanga 2018/05/17
        [OperationContract]
        DataTable Search_veh_reg_ref(string _initialSearchParams, string _searchCatergory, string _searchText);
        //sube 2018/06/06
        [OperationContract]
        List<ref_cht_accgrp> getChartAccCodeCode(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Pasindu 2018/06/01
        [OperationContract]
        List<REF_BONUS_SCHEME> getSchemeCode(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string p_circu_number);
        //tharanga 2018/06/22
        [OperationContract]
        DataTable Get_tec_by_teamcd(string _initialSearchParams, string _searchCatergory, string _searchText);
        //tharanga 2018/07/13
        [OperationContract]
        DataTable Get_scv_location(string _initialSearchParams, string _searchCatergory, string _searchText);
        //tharanga 2018/07/13
        [OperationContract]
        DataTable serch_pcbysvcloc(string _initialSearchParams, string _searchCatergory, string _searchText);
        //tharanga 2018/07/13
        [OperationContract]
        DataTable Get_scv_location_BY_LOCTIONTABLE(string _initialSearchParams, string _searchCatergory, string _searchText);
        //tharanga 2018/07/13
        [OperationContract]
        DataTable Get_scv_agent(string _initialSearchParams, string _searchCatergory, string _searchText);
        //tharanga 2018/09/05
        [OperationContract]
        DataTable searchDepositBankCode_cred_card(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Nuwan 2018.08.10
        [OperationContract]
        List<SRCH_PAY_REQ> searchPaymentRequest(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string reqtp);
        //Nuwan 2018.09.07
        [OperationContract]
        List<SRCH_ACC_DET> searchAccountnumbers(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Nuwan 2018.09.07
        [OperationContract]
        List<SRCH_TEMP_SRCH> searchFieldValue(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Nuwan 2018.09.12
        [OperationContract]
        List<COMMON_SEARCH> searchCommonDataValue(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId, string type);
        //Rangika 2018/06/08
        [OperationContract]
        DataTable get_item_rest(string _com, string mic_cd);
        //Pasindu 2018-05-16
        [OperationContract]
        List<PAYMENT_TYPES> getAccPaymentTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Dulaj 2018/Dec/06
        [OperationContract]
        DataTable Search_Bond_Itm_Model(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Dulaj 2018/Dec/06
        [OperationContract]
        DataTable Search_Wharf_Employee(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Dulaj 2018/Dec/11
        [OperationContract]
        DataTable SEARCH_TO_BOND_NO_Shp(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Dulaj 2018/Nov/30
        [OperationContract]
        DataTable Get_All_Users_Loc(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Chamal 20-Dec-2018
        [OperationContract]
        List<string> GenerateZPLBarcodes(string _label, string _option, List<string> _source, int noofprint, out string _msg, out bool _success);

        //Nuwan 2018/dec/19
        [OperationContract]
        List<ACCOUNT_TAX> srchTaxTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string creditor);
        //Nuwan 2018/Dec/26
        [OperationContract]
        List<PUR_SEARCH> searchPOPayRequest(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type, string creditor);
        //Chamal 31-Dec-2018
        [OperationContract]
        DataTable GerBarcodePrinters(string _user);
        //Chamal 05-Jan-2019
        [OperationContract]
        DataTable GerBarcodeLabels(string _user);

        //Dulaj 2018/Dec/18
        [OperationContract]
        DataTable SearchCusdecHeaderWithStus(string _initialSearchParams, string _searchCatergory, string _searchText, string stus);

        //Dilan 05-Jan-2019
        [OperationContract]
        Int32 UploadEnadocDoc(REF_ENADOC_DOCS endoc_doc);
        //Dilan 07-Jan-2019
        [OperationContract]
        DataTable GetEnadocDoc(DocSearchParam endoc_doc_param);

        //Dilan 10-Jan-2019
        [OperationContract]
        int SetEnadocHash(string enadocHash, string userId);

        //Dilan 11-Jan-2019
        [OperationContract]
        DataTable GetEnadocUserPermissions(string userId, string com);
        //tharanga 2019/01/14
        [OperationContract]
        List<Mst_Sys_Para> Get_hp_collection_aloow_det(string _msp_pty_com, string _msp_pty_tp, string _msp_pty_cd, string _msp_dir_pty_cd, string _msp_rest_type);
        //Dulaj 2019//01/02
        [OperationContract]
        DataTable SearchCusdecHeaderHS(string _initialSearchParams, string _searchCatergory, string _searchText);
        [OperationContract]//txtEntrymodal.Text, txtHSCodepopup.Text, fromDate,toDate,description
        DataTable GerHsSearchDetails(string company, string entry, string hscode, DateTime fromDate, DateTime toDate, string description);
        //Dilan 2019//01/16
        [OperationContract]
        int SetAccessToken(string token,string refrshToken);

        //Dilan 2019//01/16
        [OperationContract]
        DataTable GetAccessToken();

        //Dilan 2019//01/22
        [OperationContract]
        DataTable GetTagProfileByLibId(string libId);

        //Dilan 2019//01/24
        [OperationContract]
        DataTable GetViewAccessToken(string com, string userid, int libId);
        //Dulaj 2019/Jan/29
        [OperationContract]
        DataTable GetShipmentTrackerDatByClearenseDatePending(ImportsBLHeader _obj, DateTime _dtFrom, DateTime _dtTo);
        //Dulaj 2019/Jan/29
        [OperationContract]
        DataTable GetShipmentTrackerDatByClearenseDateActual(ImportsBLHeader _obj, DateTime _dtFrom, DateTime _dtTo);
    }
}
