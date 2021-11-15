using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FF.DataAccessLayer;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Data;
using System.DirectoryServices;
using System.Net;
using System.ServiceModel;
using System.Net.Mail;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;
using FF.BusinessObjects.General;
using FF.BusinessObjects.BITool;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.Account;
using FF.BusinessObjects.Enadoc;

namespace FF.BusinessLogicLayer
{
    /// <summary>
    /// This is a Business Logic Layer class for common search functionalty.
    /// Created By : Miginda Geeganage.
    /// Created On : 16/03/2012
    /// Modified By :
    /// Modified On :
    /// </summary>
    /// 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CommonSearchBLL : ICommonSearch
    {
        CommonSearchDAL _commonSearchDAL = null;
        FMS_InventoryDAL _fmsInentoryDAL = null;
        ReptCommonDAL _reptCommonDAL = null;
        STNCommonDAL _stnCommonDAL = null;
        public SecurityDAL _securityDAL = null;

        //kapila
        public DataTable Get_PendingDoc_ByRefNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_PendingDoc_ByRefNo(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable GetCancelRequest(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCancelRequest(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable GetQuotation4Inv(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetQuotation4Inv(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable searchGRNData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchGRNData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchAgreementData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchAgreementData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Sahan

        public DataTable SearchAllVehicles(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchAllVehicles(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetGenDiscSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetGenDiscSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCommonSearchData(string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCommonSearchData(_searchCatergory, _searchText);
        }

        public DataTable GetLocationSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetLocationSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        /// <summary>
        /// Created By : Mignda Geeganage On 20/03/2012
        /// </summary>       
        public DataTable GetUserLocationSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetUserLocationSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        /// <summary>
        /// Created By : Mignda Geeganage On 20/03/2012
        /// </summary>       
        public DataTable GetUserProfitCentreSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetUserProfitCentreSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Available Serial Search
        //Code by Prabhath on 19/03/2012
        public DataTable GetAvailableSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAvailableSerialSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }


        //Available Serial Search
        //Code by darshana on 09/08/2012
        public DataTable GetAvailableSerialWithOthSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAvailableSerialWithOthSerialSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Available None-Serial Search
        //Code by Chamal De Silva on 05/07/2012
        public DataTable GetAvailableNoneSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAvailableNoneSerialSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        //kapila 7/6/2012
        public DataTable GetIssuedSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetIssuedSerialSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable GetSearchDataBySerial(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchDataBySerial(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchSubLocationData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchSubLocationData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchPromoCommDefData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPromoCommDefData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable GetAvailableSerialSCM(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAvailableSerialSCM(_initialSearchParams, _searchCatergory, _searchText);
        }

        //kapila 7/6/2012
        public DataTable GetIssuedSerialSearchDataOth(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetIssuedSerialSearchDataOth(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Item Search from Item Master
        //Code by Prabhath on 19/03/2012
        public DataTable GetItemSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable GetItemSearchDataByCat(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSearchDataByCat(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetCustGradeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustGradeSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCustSatisSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustSatisSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCustQuestSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustQuestSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Code by Prabhath on 13/09/2012
        public DataTable GetItemforInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemforInvoiceSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable GetVItemforInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVItemforInvoiceSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchMasterTaxCodes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMasterTaxCodes(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetPreferLocSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPreferLocSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchMasterTaxRateCodes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMasterTaxRateCodes(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable searchPayCircularData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchPayCircularData(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Common Search for Invoice data.
        //Code by Miginda on 17/04/2012
        public DataTable GetInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }


        //Code by Miginda on 17/04/2012
        public DataTable GetAllInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllInvoiceSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }


        /// <summary>
        /// Get the Invoice types fo the particuler profit center
        /// Written by Prabhath on 26/04/2012
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns> Datatabe of selected data </returns>
        public DataTable GetInvoiceTypeData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceTypeData(_initialSearchParams, _searchCatergory, _searchText);
        }

        /// <summary>
        /// Get the price book for the aprticuler company/profit center and the selected invoice type
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns>Datatable of the selected data</returns>
        public DataTable GetPriceBookData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPriceBookData(_initialSearchParams, _searchCatergory, _searchText);
        }

        /// <summary>
        /// Get the price level for tha praticular 
        /// </summary>
        /// <param name="_initialSearchParams"></param>
        /// <param name="_searchCatergory"></param>
        /// <param name="_searchText"></param>
        /// <returns></returns>
        public DataTable GetPriceLevelData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPriceLevelData(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetCompanyItemStatusData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCompanyItemStatusData(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetPriceLevelItemStatusData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPriceLevelItemStatusData(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetCustomerGenaral(string _initialSearchParams, string _searchCatergory, string _searchText, string _returnCol, string _returnColDesc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustomerGenaral(_initialSearchParams, _searchCatergory, _searchText, _returnCol, _returnColDesc);
        }


        public DataTable GetCurrencyData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCurrencyData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetEmployeeData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetEmployeeData(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetSupplierData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSupplierData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetClaimSupplierData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetClaimSupplierData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetReceiptTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetReceiptTypes(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetBusinessCompany(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBusinessCompany(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetCreditNoteAll(string _initialSearchParams, string _searchCatergory, string _searchText, string _com, string _pc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCreditNoteAll(_initialSearchParams, _searchCatergory, _searchText, _com, _pc);
        }

        public DataTable searchDepositBankCode(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchDepositBankCode(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable searchFinAdjTypeData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchFinAdjTypeData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetBankAccounts(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBankAccounts(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetAllBankAccounts(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllBankAccounts(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPurchaseOrders(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPurchaseOrders(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPurchaseOrders(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPurchaseOrdersByDate(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }


        public DataTable GetReceipts(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetReceipts(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable GetManagerIssuReceipts(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetManagerIssuReceipts(_initialSearchParams, _searchCatergory, _searchText);
        }


        //chamal 15/08/2012
        public DataTable GetReceiptsByType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetReceiptsByType(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPriceLevelByBookData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPriceLevelByBookData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetItemSupplierSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSupplierSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetDepositChequeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDepositChequeSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAllPBLevelByBookData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllPBLevelByBookData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_CLS_ALW_LOC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_CLS_ALW_LOC_SearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetPriceBookByCompanyData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPriceBookByCompanyData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetDivision(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDivision(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetOutstandingInvoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetOutstandingInvoice(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetOutstandingInvoiceweb(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetOutstandingInvoiceweb(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetOutstandingInvoiceTBS(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetOutstandingInvoiceTBS(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetOutsidePartySearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetOutsidePartySearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetCountrySearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCountryData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetGroupSaleSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetGroupSaleSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCompanySearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCompanySearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        //ISuru 2017/03/15
        public DataTable GetManagersetupSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetManagersetupSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetChannelSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetChannelSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetJobRegNoSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetJobRegNoSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetRCC(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetRCC(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetAcInsSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAcInsSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetRCC_REQ(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetRCC_REQ(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Getloc_HIRC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            try
            {
                _securityDAL = new SecurityDAL();
                if (_securityDAL.Is_Report_DR("LocHeirarchySearch") == true) _commonSearchDAL.ConnectionOpen_DR();
                return _commonSearchDAL.Getloc_HIRC_SearchData(_initialSearchParams, _searchCatergory, _searchText);
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        //fazan added 19 Nov 2015
        public DataTable GetCompanySearchDetails(string _initialSearchParams, string _searchCategory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();

            try
            {
                return _commonSearchDAL.GetCompanySearchDetails(_initialSearchParams, _searchCategory, _searchText);
            }
            catch (Exception ex)
            {

                return null;
            }

            return null;

        }

        //fazan added 19 Nov 2015
        public DataTable GetBLDetails(string initialSearchParams, string bl_no)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable table = _commonSearchDAL.GetBLDetails(initialSearchParams, bl_no);

            return table;

        }
        //fazan added 21 Nov 2015
        public DataTable GetBLALL(bool status)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable dt = _commonSearchDAL.GetBLALL(status);
            return dt;
        }

        //fazan added 21 Nov 2015
        public DataTable GetSubSerial(string itemcode, string serialcode)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable dt = _commonSearchDAL.GetSubSerial(itemcode, serialcode);
            return dt;

        }
        //fazan added 21 Nov 2015
        public DataTable GetSerialWarrantyDetails(string serialcode)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable dt = _commonSearchDAL.GetSerialWarrantyDetails(serialcode);
            return dt;
        }

        //fazan added 23 nov 2015
        public DataTable GetSearchBLByDate(string company)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable data = _commonSearchDAL.GetSearchBLByDate(company);
            return data;

        }
        //fazan 2015 11 23
        public DataTable FilterDateRange(string company, string from, string to)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable data = _commonSearchDAL.FilterDateRange(company, from, to);
            return data;
        }

        //fazan 23 nov 2015
        public DataTable SearchAllBL(string company, string bl_no, string eta_frm, string eta_to, string clearance_frm, string clearance_to)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable data = _commonSearchDAL.SearchAllBL(company, bl_no, eta_frm, eta_to, clearance_frm, clearance_to);

            return data;
            //   data
        }

        //fazan 23 nov 2015

        public DataTable BL_Items(string doc_no, string _toBond)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable data = _commonSearchDAL.BL_Items(doc_no, _toBond);
            return data;
        }
        //fazan 24 nov 2015
        public DataTable LoadDstinctBL_No()
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable dt = _commonSearchDAL.LoadDstinctBL_No();
            return dt;
        }

        //fazan 24 nov 2015
        public Int32 UpdateBLDetails_shipping(string doc_clear, string enrty_no, DateTime hand_over, string actual_eta, string blno, string p_loc, Int32 _actLead, DateTime filerec, DateTime actualClrDt, string clrUsr)
        {
            int _effect = 0;
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            _effect = _commonSearchDAL.UpdateBLDetails_shipping(doc_clear, enrty_no, hand_over, actual_eta, blno, p_loc, _actLead,filerec,actualClrDt,clrUsr);
            _commonSearchDAL.ConnectionClose();
            return _effect;


        }

        //fazan 26 nov 2015
        public DataTable Container_Count(string blno)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable dt = _commonSearchDAL.Container_Count(blno);
            return dt;
        }

        //
        //fazan 27 nov 2015
        public DataTable GetGrnDt(string company, string doc_no, int line_no)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable dt = _commonSearchDAL.GetGrnDt(company, doc_no, line_no);
            return dt;
        }


        public DataTable GetInventoryTrackerSearchData(string _initialSearchParams)
        {
            _commonSearchDAL = new CommonSearchDAL();
            SCMCommonDAL _scmCommonDAL = new SCMCommonDAL();

            DataTable _scm = _scmCommonDAL.GetInventoryTrackerSearchData(_initialSearchParams);
            DataTable _scm2 = _commonSearchDAL.GetInventoryTrackerSearchData(_initialSearchParams);
            _scm.Merge(_scm2);

            return _scm;
        }

        public DataTable GetInventoryTrackerSearchData_new(string _initialSearchParams, Boolean allow_SCM, Boolean allow_SCM2)
        {
            _commonSearchDAL = new CommonSearchDAL();
            SCMCommonDAL _scmCommonDAL = new SCMCommonDAL();

            DataTable _scm = new DataTable();
            DataTable _scm2 = new DataTable();
            if (allow_SCM == true && allow_SCM2 == true)
            {
                _scm = _scmCommonDAL.GetInventoryTrackerSearchData(_initialSearchParams);
                _scm2 = _commonSearchDAL.GetInventoryTrackerSearchData(_initialSearchParams);
                _scm.Merge(_scm2);
                return _scm;
            }
            else if (allow_SCM == true && allow_SCM2 == false)
            {
                _scm = _scmCommonDAL.GetInventoryTrackerSearchData(_initialSearchParams);
                return _scm;
            }
            else if (allow_SCM == false && allow_SCM2 == true)
            {
                _scm2 = _commonSearchDAL.GetInventoryTrackerSearchData(_initialSearchParams);
                _scm = _scm2;
                return _scm;
            }
            else { return null; }

            //return _scm;
        }
        public DataTable GetCat_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCat_SearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCat_ItemSearchData(string _type, string _mcat, string _cat, string _brand, string _model, string _item, string _desn)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCat_ItemSearchData(_type, _mcat, _cat, _brand, _model, _item, _desn);
        }

        /// <summary>
        /// Created By :Shani Waththuhewa On 17/07/2012
        /// To get all Profit centers in the company
        /// </summary>   
        public DataTable GetPC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPC_SearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPriceItemSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {//Dim query = From q In (From p In dt.AsEnumerable() Select New With {.col1= p("ColumnName1"), .col2 = p("ColumnName2")}) Select q.col1, q.col2 Distinct
            _commonSearchDAL = new CommonSearchDAL();
            DataTable _t = _commonSearchDAL.GetPriceItemSearchData(_initialSearchParams, _searchCatergory, _searchText);
            string[] _str = { "Item", "Description", "Brand", "Model" };
            DataTable t = _t.DefaultView.ToTable("t", true, _str);


            return t;
        }

        //written by darshana 19/07/2012
        public DataTable GetInvoiceByAcc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceByAcc(_initialSearchParams, _searchCatergory, _searchText);
        }

        //23/07/2012
        //sachith
        public DataTable GetItemDocSearchData(string _initialSearchParams)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemDocSearchData(_initialSearchParams);
        }

        //23/07/2012
        //sachith
        public DataTable GetItemSerialSearchData(string _initialSearchParams)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSerialSearchData(_initialSearchParams);
        }

        public DataTable Get_SchemesCD_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_SchemesCD_SearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_PriceTypes_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_PriceTypes_SearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetWarrantySearchByWarrantyNoSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetWarrantySearchByWarrantyNoSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetWarrantySearchBySerialNoSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetWarrantySearchBySerialNoSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetGeneralRequestSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetGeneralRequestSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetJobSearchBySerialNoSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetJobSearchBySerialNoSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetInvoiceByInvType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceByInvType(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetInvoiceItem(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceItem(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetDCNItems(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDCNItems(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetInsPayReqAccSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInsPayReqAccSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInsDebitNoteAccSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInsDebitNoteAccSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetHpAccountSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHpAccountSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetHpAccountAdjSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHpAccountAdjSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetInsuCompany(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInsuCompany(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInsuPolicy(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInsuPolicy(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetBusinessCompanyBranch(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBusinessCompanyBranch(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Common Search for Invoice data according to customer.
        public DataTable GetInvoicebyCustomer(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoicebyCustomer(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetUserID(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetUserID(_initialSearchParams, _searchCatergory, _searchText);

        }
        public DataTable GetTown(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetTown(_initialSearchParams, _searchCatergory, _searchText);

        }



        public DataTable GetTown_new(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetTown_new(_initialSearchParams, _searchCatergory, _searchText);

        }
        public DataTable GetSerialNIC(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSerialNIC(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSerialNICAll(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSerialNICAll(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetOPE(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetOPE(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_PC_HIRC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            try
            {
                return _commonSearchDAL.Get_PC_HIRC_SearchData(_initialSearchParams, _searchCatergory, _searchText);
            }
            catch (Exception EX)
            {
                return null;
            }

        }

        public DataTable SearchAvailableItemSerial(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchAvailableItemSerial(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAllModels(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllModels(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchBusDesigData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBusDesigData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchBusDeptData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBusDeptData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable searchTownData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchTownData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable searchOfficeTownData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchOfficeTownData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable CustSatisfacSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.CustSatisfacSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable GatePassSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GatePassSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetAllInsTerms(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllInsTerms(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetDeliverdInvoiceItemSerials(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDeliverdInvoiceItemSerials(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPromotionSearch(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPromotionSearch(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPriceDefCircularSearch(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPriceDefCircularSearch(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetLoyaltyTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetLoyaltyTypes(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchAvlbleSerial4Invoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchAvlbleSerial4Invoice(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetProvinceData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetProvinceData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetDistrictByProvinceData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDistrictByProvinceData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetTownByDistrictData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetTownByDistrictData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetGradeByComData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetGradeByComData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchBankBranchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBankBranchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_employee_categories(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_employee_categories(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_employee_EPF(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_employee_EPF(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_employee_All(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_employee_All(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_sales_subtypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_sales_subtypes(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetQuotationDetailForInvoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetQuotationDetailForInvoice(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_LOC_HIRC_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_LOC_HIRC_SearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_DocNum_ByRefNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_DocNum_ByRefNo(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAllHpAccountSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllHpAccountSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetHpAcc4ActiveSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHpAcc4ActiveSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetAgrTypeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAgrTypeSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetAgrClaimTypeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAgrClaimTypeSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Search_int_hdr_Document(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_int_hdr_Document(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal 03-01-2013
        public DataTable Search_int_hdr_Infor(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_int_hdr_Infor(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        // Nadeeka 26-03-2015
        public DataTable Search_int_hdr_Infor_Quotation(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_int_hdr_Infor_Quotation(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //darshana 26-11-2013
        public DataTable GetReceiptsDate(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetReceiptsDate(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //darshana 26-11-2013
        public DataTable GetReceiptsDateTBS(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetReceiptsDateTBS(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //darshana 26-11-2013
        public DataTable GetPaymentDateTBS(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPaymentDateTBS(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        public DataTable GetHpAccountDateSearchData(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHpAccountDateSearchData(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }


        //Chamal 02-04-2013
        public DataTable Search_GIT_AODs(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_GIT_AODs(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Chamal 02-04-2013
        public DataTable Search_GIT_AODs_WithLoc(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_GIT_AODs_WithLoc(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Chamal 27-03-2013
        public DataTable Search_inr_ser_infor(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_inr_ser_infor(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetSchemaCategory(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSchemaCategory(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetSchemaType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSchemaType(_initialSearchParams, _searchCatergory, _searchText);
        }

        //kapila 21/5/2014
        public DataTable GetServiceAgentLocation(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceAgentLocation(_initialSearchParams, _searchCatergory, _searchText);
        }

        //kapila 26/5/2014
        public DataTable Getareasearchdata(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Getareasearchdata(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila 26/5/2014
        public DataTable Getzonesearchdata(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Getzonesearchdata(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila 26/5/2014
        public DataTable Getregionsearchdata(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Getregionsearchdata(_initialSearchParams, _searchCatergory, _searchText);
        }

        //kapila 23/11/2012
        public DataTable GetServiceAgent(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceAgent(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_hp_parameterTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_hp_parameterTypes(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GET_FixAsset_ref(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_FixAsset_ref(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetCustomerCommon(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustomerCommon(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSupplierCommon(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSupplierCommon(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable SearchAcServicesJobs(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchAcServicesJobs(_initialSearchParams, _searchCatergory, _searchText);

        }

        public DataTable GetSearchMRN(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchMRN(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetInvoiceNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceNo(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetDocNumforTP(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDocNumforTP(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetAvailableSerialNonSerialSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAvailableSerialNonSerialSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetAvailableSeriaSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAvailableSeriaSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetGRNItemSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetGRNItemSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetMainItemSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetMainItemSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }


        //Common Search for Invoice data according to customer. darshana
        public DataTable GetCashInvoicebyCustomer(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCashInvoicebyCustomer(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetDIN(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDIN(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Common Search for Invoice data according to customer. darshana
        public DataTable GetHPInvoicebyCustomer(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHPInvoicebyCustomer(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetHpInvoices(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHpInvoices(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchReceipt(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchReceipt(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Code by darshana on 09/02/2013
        public DataTable GetInvoiceForReversal(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceForReversal(_initialSearchParams, _searchCatergory, _searchText);
        }


        //Written By Prabhath on 13/02/2013
        public DataTable GetSearchInterTransferRequest(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchInterTransferRequest(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInvoiceforInterTransferSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceforInterTransferSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetCircularSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCircularSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCircularSearchDataByComp(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCircularSearchDataByComp(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCashComCircSearchDataByComp(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCashComCircSearchDataByComp(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSearchHpAdjuestment(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchHpAdjuestment(_initialSearchParams, _searchCatergory, _searchText);
        }

        //darshana 26-02-2013
        public DataTable GetSerInvSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSerInvSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_Search_cheque(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Search_cheque(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_Search_return_cheque(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Search_return_cheque(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSearchChqByDate(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchChqByDate(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchReceiptByAnal3(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchReceiptByAnal3(_initialSearchParams, _searchCatergory, _searchText);
        }
        //kapila
        public DataTable SearchSalesPromotor(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchSalesPromotor(_initialSearchParams, _searchCatergory, _searchText);
        }

        // NADEEKA
        public DataTable SearchPromotor(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPromotor(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetWarrantyClaimableInvoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetWarrantyClaimableInvoice(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetWarrantyClaimableSerial(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetWarrantyClaimableSerial(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetMovementDocSubTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetMovementDocSubTypes(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_Search_Registration_Det(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Search_Registration_Det(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetVehicalInsuranceRef(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVehicalInsuranceRef(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetCashCommissionCircularNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCashCommissionCircularNo(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_Search_Insuarance_Det(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Search_Insuarance_Det(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetVehicalInsuranceDebitNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVehicalInsuranceDebitNo(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetvehicalInsuranceRegistrationNUmber(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetvehicalInsuranceRegistrationNUmber(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_Hp_ActiveAccounts(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Hp_ActiveAccounts(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetRawPriceBook(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetRawPriceBook(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetRawPriceLevel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetRawPriceLevel(_initialSearchParams, _searchCatergory, _searchText);

        }

        public DataTable GetVehicalJobRegistrationNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVehicalJobRegistrationNo(_initialSearchParams, _searchCatergory, _searchText);

        }

        public DataTable GetSchemaTypeByCate(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSchemaTypeByCate(_initialSearchParams, _searchCatergory, _searchText);

        }

        //darshana 10-04-2013  GetAllScheme
        public DataTable GetAllScheme(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllScheme(_initialSearchParams, _searchCatergory, _searchText);

        }

        public DataTable Get_invoiceDet(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_invoiceDet(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_Party_Types(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Party_Types(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_Party_Codes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Party_Codes(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchTransactionType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchTransactionType(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetVoucheNos(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVoucheNos(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetItemBrands(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemBrands(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_Promotion_Codes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Promotion_Codes(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetCircularSearchByBookAndLevel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCircularSearchByBookAndLevel(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetCircularSearchForSerial(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCircularSearchForSerial(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCircularSearchForCancel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCircularSearchForCancel(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSerialPriceForCancel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSerialPriceForCancel(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSerialDetForCir(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSerialDetForCir(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAllProofDocs(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllProofDocs(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInvoiceForReversalOth(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceForReversalOth(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetHpInvoicesOth(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHpInvoicesOth(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInternalVoucherExpense(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInternalVoucherExpense(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_GPC(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_GPC(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal 03/05/2013
        public DataTable GetMovementTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Move_Types(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal 03/05/2013
        public DataTable GetInventoryDirections(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_InventoryDirections(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchAvailableGiftVoucher(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _fmsInentoryDAL = new FMS_InventoryDAL();
            return _fmsInentoryDAL.SearchAvailableGiftVoucher(_initialSearchParams, _searchCatergory, _searchText);
        }

        //darshana on 09-05-2013
        public DataTable GetComInvoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetComInvoice(_initialSearchParams, _searchCatergory, _searchText);
        }

        //darshana on 11-05-2013
        public DataTable GetItemByType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemByType(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetCreditNote(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCreditNote(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_system_role(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_system_role(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetCustomerId(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustomerId(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetVehicalRegistrationRef(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVehicalRegistrationRef(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchBuyBackItem(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBuyBackItem(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetSchemeComByCircular(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSchemeComByCircular(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchInvoice(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchInvoice(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        public DataTable Get_system_option_Groups(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_system_option_Groups(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchLoyaltyCard(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchLoyaltyCard(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchLoyaltyCardNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchLoyaltyCardNo(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchGsByCus(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchGsByCus(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetLoyaltyCardNos(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetLoyaltyCardNos(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetDocProInvoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDocProInvoice(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchEmployeeAssignToProfitCenter(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchEmployeeAssignToProfitCenter(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetDocProChassis(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDocProChassis(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetDocProEngine(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDocProEngine(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetVehicalRMVNotSendInvoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVehicalRMVNotSendInvoice(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetVehicalRMVNotSendEngine(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVehicalRMVNotSendEngine(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetVehicalRMVNotSendChassis(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVehicalRMVNotSendChassis(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetVehical_regTxn(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVehical_regTxn(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetSearchMRN_AllLoc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchMRN_AllLoc(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_All_Roles(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_All_Roles(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_All_SystemUsers(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_All_SystemUsers(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_All_SecUsersPerimssionTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_All_SecUsersPerimssionTypes(_initialSearchParams, _searchCatergory, _searchText);

        }

        public DataTable GetCustomerAll(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            //return _commonSearchDAL.GetCustomerAll(_initialSearchParams, _searchCatergory, _searchText);
            return null;
        }

        public DataTable GetQuotationAll(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetQuotationAll(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetQuotationByCust(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetQuotationByCust(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSupplierQuotation(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSupplierQuotation(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_Designations(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Designations(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_Departments(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Departments(_initialSearchParams, _searchCatergory, _searchText);

        }

        public DataTable GetPrefixData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPrefixData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable getSearchFAItemData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _stnCommonDAL = new STNCommonDAL();
            return _stnCommonDAL.getSearchFAItemData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable getSearchFAItemStatusData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _stnCommonDAL = new STNCommonDAL();
            return _stnCommonDAL.getSearchFAItemStatusData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetItemforInvoiceSearchDataByModel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemforInvoiceSearchDataByModel(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetSearchDataForPromotion(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchDataForPromotion(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSearchDataForPromoByComp(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchDataForPromoByComp(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetSearchWarrantyExtend(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchWarrantyExtend(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_AC_SevCharge_itmes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {

            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_AC_SevCharge_itmes(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetSearchEliteCommCircular(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchEliteCommCircular(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetSearchProdBonusCircular(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchProdBonusCircular(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPritHeirachySearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPritHeirachySearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetIncSchPersonSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetIncSchPersonSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetIncSchSaleTypeSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetIncSchSaleTypeSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetRCCDefSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetRCCDefSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Code by Chamal 09-Jul-2013
        public void Send_SMTPMail(string _recipientEmailAddress, string _subject, string _message)
        {
            _commonSearchDAL = new CommonSearchDAL();

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            MailAddress _senderEmailAddress = new MailAddress(_commonSearchDAL.GetMailAddress(), _commonSearchDAL.GetMailDispalyName());

            smtpClient.Host = _commonSearchDAL.GetMailHost();
            smtpClient.Port = 25;
            message.From = _senderEmailAddress;

            //string _email = _generalDAL.GetMailFooterMsg();

            message.To.Add(_recipientEmailAddress);
            message.Subject = _subject;
            //message.CC.Add(new MailAddress(_info.Mmi_superior_mail));
            //message.Bcc.Add(new MailAddress(""));
            message.IsBodyHtml = false;
            message.Body = _message;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            // Send SMTP mail
            smtpClient.Send(message);
        }

        public int StartTimeModule(string _modName, string _funcName, DateTime _stTime, string _loc, string _com, string _user, DateTime _funcDate)
        {
            _reptCommonDAL = new ReptCommonDAL();
            _reptCommonDAL.ConnectionOpen();
            int _val = _reptCommonDAL.StartTimeModule(_modName, _funcName, _stTime, _loc, _com, _user, _funcDate.Date);
            _reptCommonDAL.ConnectionClose();
            return _val;
        }

        public int EndTimeModule(int _seqNo, DateTime _edTime, TimeSpan _diffTime)
        {
            _reptCommonDAL = new ReptCommonDAL();
            _reptCommonDAL.ConnectionOpen();
            int _val = _reptCommonDAL.EndTimeModule(_seqNo, _edTime, _diffTime);
            _reptCommonDAL.ConnectionClose();
            return _val;
        }

        public string GetModuleWiseEmail(string _modName)
        {
            SecurityDAL _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            string _val = _securityDAL.GetModuleWiseEmail(_modName);
            _securityDAL.ConnectionClose();
            return _val;
        }
        public DataTable Search_Approve_Permissions(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_Approve_Permissions(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Search_ApprovePermission_Levels(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_ApprovePermission_Levels(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Search_PurchaseOrders(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_PurchaseOrders(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchPromotinalCircular(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPromotinalCircular(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_employee_sub_categories(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_employee_sub_categories(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetAdvancedReciept(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAdvancedReciept(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetCustomerCodeLoyalty(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustomerCodeLoyalty(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInventoryTrackerSearchData_new2(string _initialSearchParams, Boolean allow_SCM, Boolean allow_SCM2, out string err)
        {
            try
            {
                err = "";
                _commonSearchDAL = new CommonSearchDAL();
                SCMCommonDAL _scmCommonDAL = new SCMCommonDAL();

                DataTable _scm = new DataTable();
                DataTable _scm2 = new DataTable();
                if (allow_SCM == true && allow_SCM2 == true)
                {
                    // _scm = _scmCommonDAL.GetInventoryTrackerSearchData(_initialSearchParams);
                    _scm = _scmCommonDAL.GetInventoryTrackerSearchData_NEW2(_initialSearchParams);

                    //  _scm2 = _commonSearchDAL.GetInventoryTrackerSearchData(_initialSearchParams);
                    _scm2 = _commonSearchDAL.GetInventoryTrackerSearchData_NEW2(_initialSearchParams);
                    if (_scm2.Rows.Count > 0)
                        _scm.Merge(_scm2);
                    if (_scm.Rows.Count > 0)
                    {
                        return _scm;
                    }
                    else
                    {
                        return null;
                    }

                }
                else if (allow_SCM == true && allow_SCM2 == false)
                {
                    _scm = _scmCommonDAL.GetInventoryTrackerSearchData_NEW2(_initialSearchParams);
                    return _scm;
                }
                else if (allow_SCM == false && allow_SCM2 == true)
                {
                    _scm2 = _commonSearchDAL.GetInventoryTrackerSearchData_NEW2(_initialSearchParams);
                    _scm = _scm2;
                    return _scm;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return null;
            }


        }

        public DataTable GetCustomerCommonByNIC(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustomerCommonByNIC(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetInventoryTrackeChannel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInventoryTrackeChannel(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetApprovedRequestData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetApprovedRequestData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInvoiceforExchange(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceforExchange(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetHpInvoicesForCancel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHpInvoicesForCancel(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInvoiceReversal(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceReversal(_initialSearchParams, _searchCatergory, _searchText);
        }


        public DataTable GetExchangedReceiveDoc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetExchangedReceiveDoc(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetSupplierClaimDoc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSupplierClaimDoc(_initialSearchParams, _searchCatergory, _searchText);
        }



        public DataTable GetExchangedIssueDoc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetExchangedIssueDoc(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAuditJobNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAuditJobNo(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetReverseAccDet(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetReverseAccDet(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAuditCashVerification(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAuditCashVerification(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAuditStockVerification(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAuditStockVerification(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchServiceJob(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchServiceJob(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInventoryItem(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInventoryItem(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchGvCategory(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchGvCategory(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPromotionlDiscountHeader(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPromotionlDiscountHeader(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetRCCByStage(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetRCCByStage(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_PC_HIRC_SearchRawData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_PC_HIRC_SearchRawData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAccountChecklist(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAccountChecklist(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPBonusVoucherData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPBonusVoucherData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAccountChecklistPOD(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAccountChecklistPOD(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Search_inr_ser_Supinfor(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_inr_ser_Supinfor(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetDeductionRefData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDeductionRefData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetRefundRefData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetRefundRefData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSearchPBonusCircular(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSearchPBonusCircular(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetAdvancedRecieptForCus(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAdvancedRecieptForCus(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCategory3(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCategory3(_initialSearchParams, _searchCatergory, _searchText);
        }
        //darshana 24-05-2013  GetAllInactiveScheme
        public DataTable GetAllInactiveScheme(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllInactiveScheme(_initialSearchParams, _searchCatergory, _searchText);

        }
        //kapila

        public DataTable GetBusinessCompanyData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBusinessCompanyData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //darshana on 10-06-2014
        public DataTable GetDisVouTp(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDisVouTp(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetPromotionVoucherByCircular(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPromotionVoucherByCircular(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchAvlbleSerial4Item(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchAvlbleSerial4Item(_initialSearchParams, _searchCatergory, _searchText);
        }


        //Tharaka 10-07-2014
        public DataTable GetPromotionVoucherAll(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPromotionVoucherAll(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 18-07-2014
        public DataTable GetMasterItemModel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetMasterItemModel(_initialSearchParams, _searchCatergory, _searchText);
        }
        //shanuka 22-09-2014
        public DataTable Load_ItemSearch_details(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _fmsInentoryDAL = new FMS_InventoryDAL();
            return _fmsInentoryDAL.Load_ItemSearch_details(_initialSearchParams, _searchCatergory, _searchText);
        }


        //Tharaka 16-09-2014
        public DataTable GetChequeVouchers(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetChequeVouchers(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 30-09-2014
        public DataTable GetServiceJobDetails(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceJobDetails(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka on 01-10-2014
        public DataTable GetAllEmp(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllEmp(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Tharaka on 01-10-2014
        public DataTable GetDefectTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDefectTypes(_initialSearchParams, _searchCatergory, _searchText);
        }
        //shalika on 10/10/2014
        public DataTable Get_Utilization_Location(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Utilization_Location(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka on 13-10-2014
        public DataTable GetServiceJobs(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceJobs(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        public DataTable GetServiceJobsF3(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceJobsF3(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        //Nadeeka 24-JUN-2015
        public DataTable GetServiceJobsWarrClaim(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceJobsWarrClaim(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        //kapila
        public DataTable SearchMasterType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMasterType(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Tharaka on 23-10-2014
        public DataTable Get_Service_Estimates(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Service_Estimates(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka on 30-10-2014
        public DataTable Get_LOC_SCV_MRN(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_LOC_SCV_MRN(_initialSearchParams, _searchCatergory, _searchText);
        }
        //shalika on 08/11/2014
        public DataTable Get_Service_Def_Code(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Service_Def_Code(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal on 19/11/2014
        public DataTable SearchWarranty(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchWarranty(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2014-11-25
        public DataTable GET_CHEQUE_BOOKs(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_CHEQUE_BOOKs(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2014-11-26
        public DataTable SERCH_TECHCOMMTBYCHNNL(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SERCH_TECHCOMMTBYCHNNL(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2014-11-26
        public DataTable SEARCH_ESTIMATE_ITEMS(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_ESTIMATE_ITEMS(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2014-11-28
        public DataTable SEARCH_CONSUMABLE_ITEMS(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_CONSUMABLE_ITEMS(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2014-12-01
        public DataTable SEARCH_SCV_CONF_REQNO(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_SCV_CONF_REQNO(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2014-12-01
        public DataTable SEARCH_SCV_CONF_JOB(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_SCV_CONF_JOB(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2014-12-01
        public DataTable SEARCH_SCV_CONF_CUST(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_SCV_CONF_CUST(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka on 2014-12-04
        public DataTable Get_Service_Estimates_ByJob(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Service_Estimates_ByJob(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal 2014-12-08
        public DataTable GetServiceRequests(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceRequests(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Tharaka on 2014-12-18
        public DataTable SearchServiceInvoice(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchServiceInvoice(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        public DataTable SearchInv4InsReq(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchInv4InsReq(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        public DataTable GetSignOnSeq(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSignOnSeq(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetSignOnSeqByDate(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSignOnSeqByDate(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Tharaka on 2014-12-22
        public DataTable GetServiceDetailSearials(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceDetailSearials(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal on 2014-12-24
        public DataTable SearchScvTaskCateByLoc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchScvTaskCateByLoc(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal on 2014-12-24
        public DataTable SearchScvPriority(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchScvPriority(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchJobStage(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchJobStage(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Darshana 2014-12-24
        public DataTable GetPurchaseOrdersByDate(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPurchaseOrdersByDate(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Chamal 2014-12-28
        public DataTable SearchScvStageChg(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchScvStageChg(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-01-17
        public DataTable GetItemByTypeNew(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemByTypeNew(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka on 26-01-2015
        public DataTable GetServiceJobsWIP(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceJobsWIP(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Tharaka 2015-02-07
        public DataTable GetDOInvoiceNumber(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDOInvoiceNumber(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-02-12
        public DataTable GET_ITEM_COMP(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_ITEM_COMP(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-03-09
        public DataTable SEARCH_FAC_COM(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_FAC_COM(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-03-09
        public DataTable SEARCH_ENQUIRY(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_ENQUIRY(_initialSearchParams, _searchCatergory, _searchText);
        }

        //DARSHANA 2015-03-17
        public DataTable SEARCH_SCV_CONF_BY_JOB(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_SCV_CONF_BY_JOB(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-03-26
        public DataTable SEARCH_Invoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_Invoice(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-03-31
        public DataTable SEARCH_ChargeCode(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_ChargeCode(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-04-01
        public DataTable SEARCH_TransferCode(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_TransferCode(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-04-02
        public DataTable SEARCH_Miscellaneous(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_Miscellaneous(_initialSearchParams, _searchCatergory, _searchText);
        }


        //Tharaka on 2015-06-04
        public DataTable GetServiceJobsEnqeuiy(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable dtFinal = _commonSearchDAL.GetServiceJobsEnqeuiy(_initialSearchParams, "JOB", "testtest", _fromDate, _toDate);
            DataTable dtResult = _commonSearchDAL.GetServiceJobsEnqeuiy(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
            if (dtResult.Rows.Count > 0)
            {
                if (_initialSearchParams.Contains(":"))
                {
                    string[] arr = _initialSearchParams.Split(new string[] { ":" }, StringSplitOptions.None);
                    _initialSearchParams = arr[1];
                }
                string[] seperator = new string[] { "|" };
                string[] searchParams = _initialSearchParams.Split(seperator, StringSplitOptions.None);
                String UserPC = searchParams[3];
                DataTable dtPClist = _commonSearchDAL.GET_EQULPC_TO_PC(searchParams[0], UserPC);
                if (dtPClist != null && dtPClist.Rows.Count > 0)
                {
                    var customerNames = from PC in dtResult.AsEnumerable()
                                        join aliases in dtPClist.AsEnumerable() on PC.Field<string>("PC") equals aliases.Field<string>("PC")
                                        select PC;

                    if (customerNames.Count() > 0)
                    {
                        dtFinal = customerNames.CopyToDataTable();
                    }

                    dtFinal.TableName = "Final";
                }
            }

            return dtFinal;
        }

        //Tharaka 2015-06-06
        public DataTable SearchDrivers(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchDrivers(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-06-06
        public DataTable SearchVehicle(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchVehicle(_initialSearchParams, _searchCatergory, _searchText);
        }

        //PEMIL 2015-06-08
        public DataTable SearchDriverTBS(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchDriverTBS(_initialSearchParams, _searchCatergory, _searchText);
        }

        //PEMIL 2015-06-09
        public DataTable SearchEmployeeTBS(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchEmployeeTBS(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-06-10
        public DataTable SearchEnquiryWithStage(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchEnquiryWithStage(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-06-13
        public DataTable SearchLogHeader(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchLogHeader(_initialSearchParams, _searchCatergory, _searchText);
        }

        #region Transaction -Imports
        //Rukshan 2015-06-26
        public DataTable Get_All_PINo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_All_PINo(_initialSearchParams, _searchCatergory, _searchText);
        }
        //PEMIL 2015-09-23
        public DataTable SEARCH_PINO_PI(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_PINO_PI(_initialSearchParams, _searchCatergory, _searchText);
        }
        /*Rukshan*/
        public DataTable GetPaymentSubTerm(string _PCAD, string _PCD)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPaymentSubTerm(_PCAD, _PCD);
        }
        //Rukshan
        public DataTable SearchCostType(string _initialSearchParams)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCostType(_initialSearchParams);
        }
        //Rukshan
        public DataTable GetBankAccountFacility(string _initialSearchParams, string _SearchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBankAccountFacility(_initialSearchParams, _SearchText);
        }
        //Rukshan
        public DataTable SearchCompanyCurrancy(string _initialSearchParams)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCompanyCurrancy(_initialSearchParams);
        }
        public DataTable SearchBusEntity(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBusEntity(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchTraderTerms(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchTraderTerms(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchOrderPlanNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchOrderPlanNo(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchBank(string _initialSearchParams)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBank(_initialSearchParams);
        }

        public DataTable SearchModel(string _initialSearchParams)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchModel(_initialSearchParams);
        }

        public DataTable SearchItem(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchItem(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-07-04
        public DataTable SEARCH_FIN_HDR(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_FIN_HDR(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Rukshan'
        public DataTable SEARCH_FIN_ByID(string _initialSearchParams, string _docno)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_FIN_ByID(_initialSearchParams, _docno);
        }

        public DataTable SP_Search_Order(string P_ORDERPLAN_NO)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SP_Search_Order(P_ORDERPLAN_NO);
        }

        public DataTable SP_Search_OP_Itms(string P_ORDERPLAN_NO)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SP_Search_OP_Itms(P_ORDERPLAN_NO);

        }
        public DataTable SearchFacilityLimit(string _initialSearchParams)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFacilityLimit(_initialSearchParams);
        }

        //Tharaka 2015-07-09
        public DataTable GetAgents(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAgents(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-07-13
        public DataTable SearchBLHeader(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            DataTable searchResults = _commonSearchDAL.SearchBLHeader(_initialSearchParams, _searchCatergory, _searchText);
            DataTable searchResultsnew = new DataTable();
            searchResultsnew = searchResults.Clone();
            //Added By Dulaj 2018/Dec/28 Check Pending ToBond
            if (searchResults != null)
            {
                foreach (DataRow drtobond in searchResults.Rows)
                {

                    DataTable toBondSts = _commonSearchDAL.LoadCusdecDatabyDoc(drtobond["doc"].ToString());
                    if (toBondSts != null)
                    {
                        if (toBondSts.Rows.Count > 0)
                        {
                            if (toBondSts.Rows[0]["cuh_stus"].ToString().Equals("P"))
                            {
                                drtobond["REMARK"] = "N/A";
                            }
                            else
                            {
                                searchResultsnew.ImportRow(drtobond);
                            }
                        }
                    }
                }
            }
            //
            return searchResultsnew;
        }
        public DataTable SearchBLHeaderWithSeq(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBLHeaderWithSeq(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchPRNHeader(string p_itr_req_no)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPRNHeader(p_itr_req_no);
        }

        public DataTable SearchPRNREQNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPRNREQNo(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchPRNItems(Int32 p_itri_seq_no)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPRNItems(p_itri_seq_no);
        }

        public DataTable SearchQuotation(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchQuotation(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchOrderPlanNoByStatus(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchOrderPlanNoByStatus(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable LoadKit(string p_iok_op_no)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadKit(p_iok_op_no);
        }
        #endregion

        //Sahan
        public DataTable LoadSupplierCurrencies(string p_mscu_com, string p_mscu_sup_cd)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadSupplierCurrencies(p_mscu_com, p_mscu_sup_cd);
        }

        //Sahan
        public DataTable LoadAllPorts(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadAllPorts(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan
        public DataTable LoadSupplierPorts(string p_mspr_com, string p_mspr_cd)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadSupplierPorts(p_mspr_com, p_mspr_cd);
        }

        //Sahan
        public DataTable LoadSupplierItems(string p_mbii_com, string p_mbii_cd)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadSupplierItems(p_mbii_com, p_mbii_cd);
        }

        //Sahan
        public DataTable LoadAllTaxCat(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadAllTaxCat(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan
        public DataTable LoadAllTaxCodes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadAllTaxCodes(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan
        public DataTable LoadSupplierTaxCodes(string p_mbit_com, string p_mbit_cd)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadSupplierTaxCodes(p_mbit_com, p_mbit_cd);
        }


        //Sahan
        public DataTable GetSupplierProfileByGrup(string CustCD, string nic, string DL, string PPNo, string brNo, string mobNo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSupplierProfileByGrup(CustCD, nic, DL, PPNo, brNo, mobNo);
        }

        //Sahan
        public DataTable LoadCurrencyText(string p_mcr_cd)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadCurrencyText(p_mcr_cd);
        }

        //Sahan
        public DataTable LoadCountryText(string p_mcu_cd)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadCountryText(p_mcu_cd);
        }
        #region Item Master
        public DataTable GetItemSearchDataMaster(string _initialSearchParams, string _searchCatergory, string _searchText)
        {   // Nadeeka 09-07-2015
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSearchDataMaster(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Randima 2016-11-23
        public DataTable GetBLItems(string _initialSearchParams, string _searchCatergory, string _searchText, int _seqNo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBLItems(_initialSearchParams, _searchCatergory, _searchText, _seqNo);
        }

        //Randima 2016-11-24
        public DataTable GetPIItems(string _initialSearchParams, string _searchCatergory, string _searchText, string _piNo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPIItems(_initialSearchParams, _searchCatergory, _searchText, _piNo);
        }
        public DataTable GetCat_SearchDataMaster(string _initialSearchParams, string _searchCatergory, string _searchText)
        {   // Nadeeka 09-07-2015
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCat_SearchDataMaster(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetItemSearchUOMMaster(string _initialSearchParams, string _searchCatergory, string _searchText)
        {   // Nadeeka 09-07-2015
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSearchUOMMaster(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetItemSearchColorMaster(string _initialSearchParams, string _searchCatergory, string _searchText)
        {   // Nadeeka 09-07-2015
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSearchColorMaster(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetItemSearchTaxMaster(string _initialSearchParams, string _searchCatergory, string _searchText)
        { // Nadeeka 09-07-2015
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSearchTaxMaster(_initialSearchParams, _searchCatergory, _searchText);
        }
        #endregion

        //
        public DataTable SearchRequestType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchRequestType(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchExecutive(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchExecutive(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Search_INT_REQ(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_INT_REQ(_initialSearchParams, _searchCatergory, _searchText);

        }

        public DataTable Search_INT_REQ_RER(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_INT_REQ_RER(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Search_INR_SER(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_INR_SER(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Search_INT_RES(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_INT_RES(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Code by Rukshan 12-sep-2015 
        public DataTable Search_int_hdr_Temp_Infor(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_int_hdr_Temp_Infor(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        //Code by Rukshan 15-sep-2015 
        public DataTable GET_DeliverByOption(string _com)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_DeliverByOption(_com);
        }
        //Darshana 24-09-2015
        public DataTable GetModelMaster(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetModelMaster(_initialSearchParams, _searchCatergory, _searchText);
        }
        #region PDA // Sahan 24 Aug 2015

        //Sahan 24 Aug 2015
        public DataTable LoadUserLocation(string p_user, string p_com)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadUserLocation(p_user, p_com);
        }

        #endregion

        //Tharaka 2015-10-13
        public DataTable Search_Routes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_ROUTE(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Rukshan 2015-10-14
        public DataTable GetInventoryChannel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInventoryChannel(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan 19/Oct/2015
        public DataTable SearchPurchaseOrdersFast(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPurchaseOrdersFast(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan 03/Nov/2015
        public DataTable SearchPriceBookWeb(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPriceBookWeb(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-11-03
        public DataTable SEARCH_BIN(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_BIN(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan 04/Nov/2015
        public DataTable SearchPriceBookLevelsWeb(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPriceBookLevelsWeb(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 23/11/2015
        public DataTable GetContainerType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetContainerType(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 23/11/2015
        public DataTable GetContainerAgent(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetContainerAgent(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-11-24
        public DataTable SEARCH_DISPOSAL_JOB(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_DISPOSAL_JOB(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        //Lakshan 30/11/2015
        public DataTable GetAdminTeamByCompany(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAdminTeamByCompany(_initialSearchParams, _searchCatergory, _searchText);

        }

        //Lakshan 02/12/2015
        public DataTable SEARCH_LC_NO(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_LC_NO(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 02/12/2015
        public DataTable SEARCH_ENTRY_NO(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_ENTRY_NO(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 06-02-2016
        public DataTable SEARCH_TO_BOND_NO(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_TO_BOND_NO(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Rukshan 08/12/2015
        public DataTable GetDemurage_Para(string _com, string _doc, DateTime Fdate, DateTime Todate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDemurage_Para(_com, _doc, Fdate, Todate);
        }

        //09/Dec/2015 Rukshan
        public DataTable GetDemurrageCount(string doc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDemurrageCount(doc);
        }

        //Lakshan 14/Dec/2015
        public DataTable GetHsCodeDutyTypes(string _comp)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHsCodeDutyTypes(_comp);
        }
        //Lakshan 14/Dec/2015
        public DataTable GetHsClaimDutyTypes(string _comp)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHsClaimDutyTypes(_comp);
        }
        //Lakshan 15/Dec/2015
        public DataTable SearchOrderPlanNoNew(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchOrderPlanNoNew(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 15/Dec/2015
        public DataTable searchGRNDataNew(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchGRNDataNew(_initialSearchParams, _searchCatergory, _searchText);
        }

        //15-12-2015 Lakshan 
        public DataTable SearchGetHsCode(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchGetHsCode(_initialSearchParams, _searchCatergory, _searchText);
        }

        //15-12-2015 Lakshan 
        public DataTable GetHsCodeData(string code)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHsCodeData(code);
        }

        //15-12-2015 Lakshan 
        public DataTable GetHsCodeEntryType(string _country)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHsCodeEntryType(_country);
        }

        //15-12-2015 Lakshan 
        public DataTable GetHsDutyBU(string country, string hsCode)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHsDutyBU(country, hsCode);
        }

        //15-12-2015 Lakshan 
        public DataTable GetHsClaimBU(string country, string hsCode)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHsClaimBU(country, hsCode);
        }

        //15-12-2015 Lakshan 
        public DataTable SearchGetPort(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchGetPort(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-12-15
        public DataTable GetItemSearchDataWithBIN(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSearchDataWithBIN(_initialSearchParams, _searchCatergory, _searchText);
        }


        //17-12-2015 Lakshan 
        public DataTable SearchHsDefinitionDuty(HsCode hsCode)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchHsDefinitionDuty(hsCode);
        }
        //17-12-2015 Lakshan 
        public DataTable SearchHsDefinitionClaim(HsCodeClaim hsCodeClaim)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchHsDefinitionClaim(hsCodeClaim);
        }

        //Tharaka 2015-12-16
        public DataTable SearchSerialByLocBIN(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchSerialByLocBIN(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 2015-12-18
        public DataTable GetBusinessCompanyDataByCountry(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBusinessCompanyDataByCountry(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 2015-12-19
        public DataTable GetContainerNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetContainerNo(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 2015-12-19
        public DataTable GetBlNumber(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBlNumber(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2015-12-21
        public DataTable SEARCH_RECEIPT_UNALO(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_RECEIPT_UNALO(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Lakshan 2015-12-23
        public DataTable Search_Tax_Master_Data(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_Tax_Master_Data(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 2015-12-23
        public DataTable Search_Tax_CODES(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_Tax_CODES(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 2015-12-29
        public DataTable GetTaxClaimCode(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetTaxClaimCode(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 2015/12/30
        public DataTable GetBrandManager(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBrandManager(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Dilshan on 02/02/2018
        public DataTable GetUser(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetUser(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Code by Rukshan 05-Jan-2016 
        public DataTable GET_Vehicle(string _COM, string _VNO)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_Vehicle(_COM, _VNO);
        }
        //Code by Rukshan 05-Jan-2016 
        public DataTable GET_TRANSPORT_JOB(string _COM, string _VNO)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_Vehicle(_COM, _VNO);
        }

        //Code by Rukshan 07-Jan-2016 
        public DataTable GET_UOM_CAT(string _CAT)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_UOM_CAT(_CAT);
        }
        //Code by Rukshan 07-Jan-2016 
        public DataTable GET_ITM_REPL_REASON()
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_ITM_REPL_REASON();
        }

        public DataTable SearchOrderPlanNoByStatusNew(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchOrderPlanNoByStatusNew(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        /*Lakshan 08/01/2016 from Country search*/
        public DataTable GetAllCountryData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllCountryData(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan 12 Jan 2106
        public DataTable GetAvailableSeriaSearchDataWeb(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAvailableSeriaSearchDataWeb(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 14/01/2016
        public DataTable GET_DISTRICT_DATA(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_DISTRICT_DATA(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal 19-Jan-2016 
        public DataTable SearchGetProcedureCode(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchGetProcedureCode(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal 19-Jan-2016
        public DataTable SearchCusdecHeader(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCusdecHeader(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 18/01/2016
        public DataTable SearchWarrentyAmendSerial(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchWarrentyAmendSerial(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 21/01/2016
        public DataTable SearchWarrentyAmendWarrenty(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchWarrentyAmendWarrenty(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 21/01/2016
        public DataTable SearchWarrentyAmendInvoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchWarrentyAmendInvoice(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 21/01/2016
        public DataTable SearchWarrentyAmendDocNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchWarrentyAmendDocNo(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 21/01/2016
        public DataTable SearchWarrentyAmendData(string comp, string loc, string serial1, string warr, string inv, string doc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchWarrentyAmendData(comp, loc, serial1, warr, inv, doc);
        }
        //Rukshan 22-Jan-2016
        public DataTable SearchCusdec(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCusdec(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 22/01/2016
        public DataTable SearchWarrentyAmendRequestData(string comp, string loc, DateTime fromDate, DateTime toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchWarrentyAmendRequestData(comp, loc, fromDate, toDate);
        }
        //Chamal 24/01/2016
        public DataTable SearchCusdecReq(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCusdecReq(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Chamal 28/01/2016
        public DataTable GetHSHistory(string com, string country, string docType, string item)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetHSHistory(com, country, docType, item);
        }


        /*Lakshan 01/02/2016*/
        public DataTable GetSupplierCommonNew(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetSupplierCommonNew(_initialSearchParams, _searchCatergory, _searchText);

        }

        //Lakshan 2016/02/03
        public DataTable GetValid_ExchangeRates(string _com, string fromCur, string toCur, DateTime fromDt)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetValid_ExchangeRates(_com, fromCur, toCur, fromDt);
        }

        //Lakshan 2016/02/05
        public DataTable GetVahicleAllocationEnquiryData(string _com, string _pc, string _veh_tp, string _fleet, DateTime expect_dt, string _whole_day)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVahicleAllocationEnquiryData(_com, _pc, _veh_tp, _fleet, expect_dt, _whole_day);
        }
        //Lakshan 2016/02/05
        public DataTable GetVahiclesNotAllocated(string _pc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetVahiclesNotAllocated(_pc);
        }

        //nuwan 2016/02/08
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetails(string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCustomerDetails(spgeNum, pgeSize, searchFld, searchVal, company, type);
        }
        //nuwan 2016/02/09
        public List<MST_TOWN_SEARCH_HEAD> getTownDetails(string spgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getTownDetails(spgeNum, pgeSize, searchFld, searchVal);
        }
        //nuwan 2016/02/10
        public List<MST_PROFIT_CENTER_SEARCH_HEAD> getProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        //dilshan 98/09/2017
        public List<MST_PROFIT_CENTER_SEARCH_HEAD> getMgProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string mgr)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getMgProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company, mgr);
        }
        //nuwan 2016/02/10
        public List<MST_EMPLOYEE_SEARCH_HEAD> getEmployeeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getEmployeeDetails(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<EMP_SEARCH_HEAD_SCM> getEmployeeDetailsSCM(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getEmployeeDetailsSCM(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        //dilshan 28/09/2017
        public List<EMP_SEARCH_HEAD_SCM> getManagerDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getManagerDetails(pgeNum, pgeSize, searchFld, searchVal, company);
        }

        //dilshan 28/09/2017
        public List<hpr_sch_tp> getSchemeType(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getSchemeType(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        //dilshan 28/09/2017
        public List<hpr_disr_val_ref> getCircular(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string Cat)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCircular(pgeNum, pgeSize, searchFld, searchVal, company, Cat);
        }
        //dilshan 28/09/2017
        public List<BonusDefinition> getCircularcbd(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCircularcbd(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        //dilshan 28/09/2017
        public List<hpr_sch_det> getScheme(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getScheme(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<EMP_SEARCH_HEAD_SCM> getEmployeeCatSCM(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getEmployeeCatSCM(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<ref_comm_hdr> getCommissionCode(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCommissionCode(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<ACCCODESEARCH> getHandAccCodes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc, DateTime date)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getHandAccCodes(pgeNum, pgeSize, searchFld, searchVal, company, pc, date);
        }
        public List<REF_BONUS_HDR> getBonusCodeCode(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string schemeCode)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBonusCodeCode(pgeNum, pgeSize, searchFld, searchVal, company, schemeCode);
        }
        public List<Sar_Type> GetCommissionInvTp(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCommissionInvTp(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<Sar_Type> GetPromoCircula(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPromoCircula(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        //subodana 2016/02/12
        public List<mst_fleet_search_head> getFleetDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getFleetDetails(pgeNum, pgeSize, searchFld, searchVal);
        }

        public List<MST_SER_PROVIDER_SEARCH_HEAD> getServiceProviderDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getServiceProviderDetails(pgeNum, pgeSize, searchFld, searchVal);
        }


        /*Lakshan 11-Feb-2016*/
        public DataTable GetItemBinSearchData(string _initialSearchParams)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemBinSearchData(_initialSearchParams);
        }

        //Sahan 12 Feb 2016
        public DataTable LoadColours()
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.LoadColours();
        }

        //Lakshan 15 Feb 2016
        public DataTable SearchMstGradeTypes(GradeMaster obj)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMstGradeTypes(obj);
        }
        //nuwan 2016/02/15
        public List<MST_BANKACC_SEARCH_HEAD> getDepositedBanks(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getDepositedBanks(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        //Nuwan 2016/02/16
        public List<MST_BUSCOM_BANK_SEARCH_HEAD> getBusComBanks(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBusComBanks(pgeNum, pgeSize, searchFld, searchVal);
        }
        //NUwan 2016/02/16
        public List<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD> getBoscomBankBranchs(string bankcd, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBoscomBankBranchs(bankcd, pgeNum, pgeSize, searchFld, searchVal);
        }
        //Lakshan 2016-02-16
        public DataTable SearchOperation(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchOperation(_initialSearchParams, _searchCatergory, _searchText);
        }
        public List<MST_ADVAN_REF_SEARCH_HEAD> getAdvanceRerference(string company, string cusCd, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getAdvanceRerference(company, cusCd, pgeNum, pgeSize, searchFld, searchVal);
        }

        public List<MST_CREDIT_NOTE_REF_SEARCH_HEAD> getCredNoteRerference(string customer, string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCredNoteRerference(customer, company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_LOYALTYCARD_SEARCH_HEAD> getLoyaltyCard(string customer, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getLoyaltyCard(customer, pgeNum, pgeSize, searchFld, searchVal);
        }

        //Lakshan 24/Feb/2016
        public DataTable SearchRefLocCate1()
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchRefLocCate1();
        }
        //Lakshan 24/Feb/2016
        public DataTable SearchMstChnl(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMstChnl(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 24/Feb/2016
        public DataTable SearchRefLocCate3(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchRefLocCate3(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Nuwan 2016/02/24
        public List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> getTransportEnqiurySearch(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getTransportEnqiurySearch(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
        }
        //Nuwan 2016/02/25
        public List<MST_CHARG_CODE_SEARCH_HEAD> getChargCodeList(string company, string category, string pgeNum, string pgeSize, string searchFld, string searchVal, string pc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getChargCodeList(company, category, pgeNum, pgeSize, searchFld, searchVal, pc);
        }

        //Lakshan 26/Feb/2016
        public DataTable SearchDocPriceDefDocTp(PriceDefinitionRef _def, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchDocPriceDefDocTp(_def, _searchCatergory, _searchText);
        }
        //Subodana 2016/02/27
        public List<MST_COUNTRY_SEARCH> getCountry(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCountry(pgeNum, pgeSize, searchFld, searchVal);
        }
        //Lakshan 27/Feb/2016
        public DataTable SearchDocPriceDefPrBook(PriceDefinitionRef _def, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchDocPriceDefPrBook(_def, _searchCatergory, _searchText);
        }
        //Lakshan 27/Feb/2016
        public DataTable SearchDocPriceDefPrLVL(PriceDefinitionRef _def, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchDocPriceDefPrLVL(_def, _searchCatergory, _searchText);
        }

        /*Lakshan 2016-Mar-02*/
        public DataTable SearchMainItemsData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMainItemsData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Randima 19/Oct/2016
        public DataTable SearchMainItemsDataSplit(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMainItemsDataSplit(_initialSearchParams, _searchCatergory, _searchText);
        }
        /*Lakshan 2016-Mar-02*/
        public DataTable SearchMainItemsDataSer(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMainItemsSer(_initialSearchParams, _searchCatergory, _searchText);
        }
        public List<MST_CHARG_CODE_AIRTVL_SEARCH_HEAD> getChargCodeListArrival(string company, string category, string pgeNum, string pgeSize, string searchFld, string searchVal, string pc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getChargCodeListArrival(company, category, pgeNum, pgeSize, searchFld, searchVal, pc);
        }
        public List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> getChargCodeListMsclens(string company, string category, string pgeNum, string pgeSize, string searchFld, string searchVal, string pc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getChargCodeListMsclens(company, category, pgeNum, pgeSize, searchFld, searchVal, pc);
        }
        public List<MST_RECEIPT_TYPE_SEARCH_HEAD> getReceiptTypes(string company, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getReceiptTypes(company, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_DIVISION_SEARCH_HEAD> getDivisions(string company, string profCen, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getDivisions(company, profCen, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_OUTSTANDING_INVOICE_SEARCH_HEAD> getOutstandingInvoice(string company, string profCen, string cusCd, string othText, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getOutstandingInvoice(company, profCen, cusCd, othText, pgeNum, pgeSize, searchFld, searchVal);

        }
        public List<MST_OUTSTANDING_INVOICE_SEARCH_HEAD> getOutstandingInvoice2(string company, string profCen, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getOutstandingInvoice2(company, profCen, pgeNum, pgeSize, searchFld, searchVal);

        }
        public List<MST_PROF_CEN_SEARCH_HEAD> getAllProfitCenters(string company, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getAllProfitCenters(company, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_GIFTVOUCHER_ISSSUE_SEARCH_HEAD> getGVISUVouchers(string company, string type, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getGVISUVouchers(company, type, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_RECEIPT_SEARCH_HEAD> getReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getReceiptEntries(company, profCen, _fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_RECEIPT_SEARCH_HEAD> getUnallowReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getUnallowReceiptEntries(company, profCen, _fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal);
        }

        //Lakshan 2016 Mar 18
        public DataTable GetBlItmByDocNo(string _docNo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetBlItmByDocNo(_docNo);
        }
        public List<MST_CHANNEL_SEARCH_HEAD> getChannels(string company, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getChannels(company, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_SUBCHANNEL_SEARCH_HEAD> getSubChannels(string channel, string company, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getSubChannels(channel, company, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_SERVICE_CODE_SEARCH_HEAD> getServiceCodes(string company, string profcen, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getServiceCodes(company, profcen, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_COST_SHEET_SEARCH_HEAD> getCoseSheets(string company, string profcen, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCoseSheets(company, profcen, pgeNum, pgeSize, searchFld, searchVal);
        }
        //Lakshan 02 Mar 2016
        public DataTable SearchMainItemsComp(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMainItemsComp(_initialSearchParams, _searchCatergory, _searchText);
        }
        //SUBODANA
        public List<MST_CURRENCY_SEARCH_HEAD> GetAllCurrencyNew(string company, string profCen, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllCurrencyNew(company, profCen, pgeNum, pgeSize, searchFld, searchVal);

        }
        public List<MST_COMPANIES_SEARCH_HEAD> GetLogCompanies(string company, string type, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetLogCompanies(company, type, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_LOGSHEET_SEARCH_HEAD> GetLogSheets(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetLogSheets(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_ENQUIRY_SEARCH_HEAD> GetLogEnquiries(string company, string userDefPro, string type, string stage, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetLogEnquiries(company, userDefPro, type, stage, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_EMPLOYEE_SEARCH_HEAD> GetLogDrivers(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetLogDrivers(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<mst_fleet_search_head> GetLogVehicles(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetLogVehicles(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
        }
        //Rukshan 11/Apr/2016
        public DataTable Get_Sales_Ex(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Sales_Ex(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Nuwan  11/04/2016
        public List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> geTRANSEnqiurySearch(string company, string userDefPro, string type, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.geTRANSEnqiurySearch(company, userDefPro, type, pgeNum, pgeSize, searchFld, searchVal);
        }

        //Rukshan 11/Apr/2016
        public DataTable GetCustomerBYsalesExe(string _initialSearchParams, string _searchCatergory, string _searchText, string _returnCol, string _returnColDesc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustomerBYsalesExe(_initialSearchParams, _searchCatergory, _searchText, _returnCol, _returnColDesc);
        }
        //Nuwan 2016/04/30
        public List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> getAllEnquirySearch(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getAllEnquirySearch(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
        }
        //Lakshan 30 Apr 2016
        public DataTable SearchAdminTeam(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchAdminTeam(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 06 May 2016
        public DataTable SEARCH_INT_ADHOC_HDR(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_INT_ADHOC_HDR(_initialSearchParams, _searchCatergory, _searchText);
        }

        //subodana 2016-05-11

        public DataTable GetInvoiceTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Sar_Types(_initialSearchParams, _searchCatergory, _searchText);
        }
        // Isuru
        public DataTable Get_Rcpt_Types(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Rcpt_Types(_initialSearchParams, _searchCatergory, _searchText);
        }
        //subodana
        public DataTable GetInvoiceSubTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Sar_SubTypes(_initialSearchParams, _searchCatergory, _searchText);
        }
        //SUBODANA 2016-05-14
        public List<GEN_CUST_ENQ> GET_TOURS_ENQ(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal, String Status)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_TOURS_ENQ(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal, Status);
        }
        //nuwan 2016/05/16
        public List<MST_PROFIT_CENTER_SEARCH_HEAD> getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getUserProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company, userId);
        }

        //SUBODANA 2016-05-20
        public DataTable Get_BondNumber(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_BondNumber(_initialSearchParams, _searchCatergory, _searchText);
        }

        //SUBODANA 2016-05-20
        public DataTable Get_GrnNumber(string com, string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_GrnNumber(com, _initialSearchParams, _searchCatergory, _searchText);
        }
        //SUBODANA 2016-05-20
        public DataTable Get_ReqNumber(string com, string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_ReqNumber(com, _initialSearchParams, _searchCatergory, _searchText);
        }

        //SUBODANA 2016-05-20
        public DataTable Get_OtherLoc(string com, string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_OtherLoc(com, _initialSearchParams, _searchCatergory, _searchText);
        }
        //SUBODANA 2016-05-20
        public DataTable Get_Operationteam(string com, string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Operationteam(com, _initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SearchInrBinLoc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchInrBinLoc(_initialSearchParams, _searchCatergory, _searchText);
        }

        //SUBODANA 2016-05-25
        public DataTable Get_Color_All(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Color_All(_initialSearchParams, _searchCatergory, _searchText);
        }
        //SUBODANA 2016-05-25
        public DataTable SearchApprovedDocNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchApprovedDocNo(_initialSearchParams, _searchCatergory, _searchText);
        }
        //SUBODANA 2016-05-25
        public DataTable Get_ReqApprType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_ReqApprType(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Nuwan
        public List<MST_FAC_LOC_SEARCH_HEAD> getFacLocation(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getFacLocation(pgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        //SUBODANA 2016-05-26
        public DataTable SearchLoadingPlace(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchLoadingPlace(_initialSearchParams, _searchCatergory, _searchText);
        }

        //SUBODANA 2016-05-30
        public DataTable SearchStockDocNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchStockDocNo(_initialSearchParams, _searchCatergory, _searchText);
        }

        //SUBODANA 2016-05-30
        public DataTable SearchClompanyLocation(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchClompanyLocation(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Nuwan
        public List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> getOtherChargesForTransport(string service, string company, string pgeNum, string pgeSize, string searchFld, string searchVal, string userDefPro, string addedChgCd)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getOtherChargesForTransport(service, company, pgeNum, pgeSize, searchFld, searchVal, userDefPro, addedChgCd);
        }
        //Nuwan
        public List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> getMescChargesWitoutParent(string service, string company, string pgeNum, string pgeSize, string searchFld, string searchVal, string userDefPro, string addedChgCd)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getMescChargesWitoutParent(service, company, pgeNum, pgeSize, searchFld, searchVal, userDefPro, addedChgCd);
        }
        //Lakshan 31 May 2016
        public DataTable SearchExchangeRateHistry(MasterExchangeRate busObj)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchExchangeRateHistry(busObj);
        }

        //Lakshan 04 Jun 2016
        public DataTable SER_REF_COND_TP(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SER_REF_COND_TP(_initialSearchParams, _searchCatergory, _searchText);
        }

        //subodana 2016-06-09
        public DataTable SearchReversalInvoice(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchReversalInvoice(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        //subodana 2016-06-15
        public DataTable SearchFileNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFileNo(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 16 Jun 2016
        public DataTable SearchProfitCenterByUser(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchProfitCenterByUser(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 16 Jun 2016
        public DataTable SearchUserLocationByUser(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchUserLocationByUser(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Rukshan 2016-Jun-18
        public DataTable SearchCusdecHeaderForInvoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCusdecHeaderForInvoice(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 20 Jun 2016
        public DataTable SearchBLHeaderNew(string _initialSearchParams, string _searchCatergory, string _searchText, Int32 isDate, DateTime from, DateTime to)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBLHeaderNew(_initialSearchParams, _searchCatergory, _searchText, isDate, from, to);
        }

        //Randima 15 Jul 2016
        public DataTable SearchBLHeaderCosting(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime fromDate, DateTime toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBLHeaderCosting(_initialSearchParams, _searchCatergory, _searchText, fromDate, toDate);
        }


        //Randima 2016/07/25
        public DataTable SearchItemGRN(string _searchCatergory, string _searchText, string _tp, string _PONo, string item)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchItemGRN(_searchCatergory, _searchText, _tp, _PONo, item);
        }
        public DataTable SearchItemGRNnew(string _searchCatergory, string _searchText, string _tp, string _PONo, string item)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchItemGRNnew(_searchCatergory, _searchText, _tp, _PONo, item);
        }

        //Randima 18 Jul 2016
        public DataTable SearchPurOrdCosting(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime fromDate, DateTime toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPurOrdCosting(_initialSearchParams, _searchCatergory, _searchText, fromDate, toDate);
        }

        //Lakshan 21 Jun 2016
        public DataTable SearchCusdecReqNew(string _initialSearchParams, string _searchCatergory, string _searchText, Int32 isDate, DateTime from, DateTime to)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCusdecReqNew(_initialSearchParams, _searchCatergory, _searchText, isDate, from, to);
        }
        //Lakshan 21 Jun 2016
        public DataTable SearchCusdecHeaderNew(string _initialSearchParams, string _searchCatergory, string _searchText, Int32 isDate, DateTime from, DateTime to)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCusdecHeaderNew(_initialSearchParams, _searchCatergory, _searchText, isDate, from, to);
        }
        //subodana 2016-07-01
        public DataTable SearchRoutDetailsnew(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchRoutDetailsnew(_initialSearchParams, _searchCatergory, _searchText);
        }


        //subodana 2016-07-05
        public DataTable SearchResvationRequestNo(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchResvationRequestNo(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        //subodana 2016-07-07
        public DataTable SearchResvationApproveNo(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchResvationApproveNo(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        //Nuwan 2016/02/25
        public List<MST_CHARG_CODE_SEARCH_HEAD> getChargCodeListWithType(string company, string category, string pgeNum, string pgeSize, string searchFld, string searchVal, string pc, string type)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getChargCodeListWithType(company, category, pgeNum, pgeSize, searchFld, searchVal, pc, type);
        }
        //Lakshan 18 Jul 2016
        public DataTable SearchAodDocumentForItemAllocation(string _initialSearchParams, DateTime _dtFrom, DateTime _dtTo, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchAodDocumentForItemAllocation(_initialSearchParams, _dtFrom, _dtTo, _searchCatergory, _searchText);
        }

        //subodana 2016-08-01

        public DataTable GetChanalDetailsNew(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetChanalDetailsNew(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshika 2016-Aug-05
        public DataTable GET_CUSDEC_NO(string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_CUSDEC_NO(_searchText);
        }
        //subodana 2016-08-04
        public DataTable GetTransportMethods(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetTransportMethods(_initialSearchParams, _searchCatergory, _searchText);
        }
        //subodana 2016-08-04
        public DataTable GetTransportReference(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetTransportReference(_initialSearchParams, _searchCatergory, _searchText);
        }
        //subodana 2016-08-04
        public DataTable GetTransportParty(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetTransportParty(_initialSearchParams, _searchCatergory, _searchText);
        }
        //subodana 2016-08-08
        public DataTable GetAODTrackerDOC(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAODTrackerDOC(_initialSearchParams, _searchCatergory, _searchText);
        }

        //SUBODANA 2016-08-09
        public DataTable SearchSerialByLocBINNEW(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchSerialByLocBINNEW(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Rukshan 2016-08-11
        public DataTable SEARCHLOAD_PLS(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCHLOAD_PLS(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 2016 Aug 13
        public DataTable Search_int_hdr_Infor_New(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_int_hdr_Infor_New(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        //Lakshan 2016 Aug 19
        public DataTable SearchBankDetails(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBankDetails(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 19 Aug 2016
        public DataTable SearchChequeDetailsReturnChk(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _frmChqDte, DateTime _toChqDte)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchChequeDetailsReturnChk(_initialSearchParams, _searchCatergory, _searchText, _frmChqDte, _toChqDte);
        }
        //Lakshan 20 Aug 2016
        public DataTable GetChequeDetForReturn(RecieptHeader _recHdr, RecieptItem _recItm, DateTime _dtFrom, DateTime _dtTo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetChequeDetForReturn(_recHdr, _recItm, _dtFrom, _dtTo);
        }
        //Lakshan 25 Aug 2016
        public DataTable GetShipmentTrackerDatByClearenseDate(ImportsBLHeader _obj, DateTime _dtFrom, DateTime _dtTo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetShipmentTrackerDatByClearenseDate(_obj, _dtFrom, _dtTo);
        }
        //Lakshan 13 Sep 2016
        public DataTable Search_int_hdr_Infor_new(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_int_hdr_Infor_new(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        public DataTable SearchInvoiceWeb(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchInvoiceWeb(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        public List<MST_SRCH_ITM> getItemSearchDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getItemSearchDetails(pgeNum, pgeSize, searchFld, searchVal);

        }

        //Randima 25 Oct 2016
        public DataTable GetItmStusByCompany(string _com)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItmStusByCompany(_com);
        }
        public List<DEPO_AMT_SEARCH_HED> getDepositAmounts(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getDepositAmounts(pgeNum, pgeSize, searchFld, searchVal);
        }
        //LAkshan 10 Nov 2016
        public DataTable SER_REF_COND_TP_NEW(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SER_REF_COND_TP_NEW(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 23 Nov 2016
        public DataTable GetItemSearchDataForBatch(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSearchDataForBatch(_initialSearchParams, _searchCatergory, _searchText);
        }

        //RUKSHAN 29 Nov 2016
        public DataTable GetKitItemforInvoiceSearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetKitItemforInvoiceSearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Akila 2016/12/31
        public DataTable GetItemByLocation(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemByLocation(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Add by Akila 2016/12/17
        public DataTable GetSubReceiptTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.GetSubReceiptTypes(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Add by akila 2017/01/05
        public DataTable GetInvoiceWithoutReversal(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvoiceWithoutReversal(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 03 Jan 2017
        public DataTable SearchFleetRegistrationNO(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFleetRegistrationNO(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 10 Jan 2017
        public DataTable SearchProductionNoAodOut(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchProductionNoAodOut(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Add by akila 2017/01/21
        public DataTable GetServiceJobsByCustomer(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.GetServiceJobsByCustomer(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchCreditNotes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.SearchCreditNotes(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 25 Jan 2017
        public DataTable GetItemSearchDataForProductionPlan(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSearchDataForProductionPlan(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 26 Jan 2017
        public DataTable GetItemforKit(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemforKit(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 26 Jan 2017
        public DataTable SearchEmployeeByType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchEmployeeByType(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 26 Jan 2017
        public DataTable SearchFleetModel(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFleetModel(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 26 Jan 2017
        public DataTable SearchFleetMake(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFleetMake(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 26 Jan 2017
        public DataTable SearchFleetFuelType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFleetFuelType(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 26 Jan 2017
        public DataTable SearchFleetBattryType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFleetBattryType(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 26 Jan 2017
        public DataTable SearchFleetTaxClass(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFleetTaxClass(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 26 Jan 2017
        public DataTable SearchFleetVehClass(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFleetVehClass(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 26 Jan 2017
        public DataTable SearchFleetVehCarryerTp(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFleetVehCarryerTp(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 21 Feb 2017
        public DataTable SearchBLHeaderForSI(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBLHeaderForSI(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_LoadingBays(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_LoadingBays(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_LoadingBaysDoc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_LoadingBaysDoc(_initialSearchParams, _searchCatergory, _searchText);
        }
        public List<LoadingBay> Get_LoadingBayOtherDetails(string userid, string locid, string locbayid)
        {
            _commonSearchDAL = new CommonSearchDAL();
            List<LoadingBay> _loadBay = null;
            _loadBay = _commonSearchDAL.Get_LoadingBayOtherDetails(userid, locid, locbayid);
            if (_loadBay != null)
            {
                return new List<LoadingBay>(_loadBay);
            }
            else
                return new List<LoadingBay>();
        }

        public Int32 ExistsLoadingBaysUpdate(string useridU, string locationidU, string loadingBayidU, string userid, string locationid, string loadingBayId, int active, string modUser, DateTime modDate, string modSession, string comCode, string wCom, string wLoc)
        {
            int _effect = 0;
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            _effect = _commonSearchDAL.ExistsLoadingBaysUpdate(useridU, locationidU, loadingBayidU, userid, locationid, loadingBayId, active, modUser, modDate, modSession, comCode, wCom, wLoc);
            _commonSearchDAL.ConnectionClose();
            return _effect;
        }

        //Isuru 2017/03/21
        public DataTable GetCat_SearchData_FORBM(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCat_SearchData_FORBM(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable Get_UsersFrompda(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_UsersFrompda(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetUserLocationFrompda(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetUserLocationFrompda(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_RecordExistInBay(string userid, string locCode, string comCode, string wCom, string wLoc, string lBay)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_RecordExistInBay(userid, locCode, comCode, wCom, wLoc, lBay);
        }
        public DataTable getWarehouseData(string userid, string locCode, string comCode)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getWarehouseData(userid, locCode, comCode);
        }

        //Isuru 2017/03/23
        public DataTable GetInv_TypCre_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInv_TypCre_SearchData(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetInv_Typforupdate_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText, string _pc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInv_Typforupdate_SearchData(_initialSearchParams, _searchCatergory, _searchText, _pc);
        }
        //Udaya 30/03/2017
        public DataTable GetAllEngineNos(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllEngineNos(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Udaya 30/03/2017
        public DataTable GetAllChassiNos(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllChassiNos(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Udaya 30/03/2017
        public DataTable GetAllJobNos(string _initialSearchParams, string _searchCatergory, string _searchText, string _fromDate, string _todate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllJobNos(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _todate);
        }
        //Udaya 31/03/2017
        public DataTable GetAllAodNos(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllAodNos(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Udaya 31/03/2017
        public DataTable GetAllTechnician(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllTechnician(_initialSearchParams, _searchCatergory, _searchText);
        }
        //create by randima modify by lakshan 03 Apr 2017
        public DataTable SearchMainItemsDataSplitByItem(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMainItemsDataSplitByItem(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Akila 2017/04/18
        public DataTable SearchAllEmployee(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchAllEmployee(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Akila 2017/04/19
        public DataTable SearchExchangeRequest(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchExchangeRequest(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchForwardInvoice(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchForwardInvoice(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable SearchFixAssetRequest(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchFixAssetRequest(_initialSearchParams, _searchCatergory, _searchText);
        }

        //ISURU 2017/03/24
        public DataTable SearchPRNREQNoforall(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPRNREQNoforall(_initialSearchParams, _searchCatergory, _searchText);
        }


        //ISURU 2017/03/25
        public DataTable SearchPRNItemsfordesc(Int32 p_itri_seq_no)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchPRNItemsfordesc(p_itri_seq_no);
        }

        //ISURU 2017/04/25
        public DataTable SearchItemforchange(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchItemforchange(_initialSearchParams, _searchCatergory, _searchText);
        }

        public List<PLU_SEARCH_ITM> getItemSearchDetailsforplu(string pgeNum, string pgeSize, string searchFld, string searchVal, string customercode)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getItemSearchDetailsforplu(pgeNum, pgeSize, searchFld, searchVal, customercode);

        }

        //Isuru 2017/04/28
        public DataTable GetCustomerCodeDetails(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustomerCodeDetails(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Isuru 2017/04/28
        public DataTable GetItemCodeDetails(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemCodeDetails(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Udaya 29/04/2017
        public DataTable GetUserLocationByRoleAndCompany(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetUserLocationByRoleAndCompany(_initialSearchParams, _searchCatergory, _searchText);
        }

        //tharanga 04/may/2017
        public DataTable GetServiceCenterDetails(string _com_CD, string _svc_CD, string _Town_CD, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceCenterDetails(_com_CD, _svc_CD, _Town_CD, _searchText);

        }
        //Isuru 2017/05/03
        public List<mst_busentity_customer> GetCustomerDetailsforplu(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCustomerDetailsforplu(pgeNum, pgeSize, searchFld, searchVal, company);

        }

        public DataTable Get_All_Users(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_All_Users(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Udaya 09/05/2017
        public DataTable creditNoteVirtualItems(string _initialSearchParams, string _searchCatergory, string _searchText, string filter)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.creditNoteVirtualItems(_initialSearchParams, _searchCatergory, _searchText, filter);
        }
        //Udaya 12/05/2017
        public DataTable creditNoteInvoiceType(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.creditNoteInvoiceType(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Akila 2017/05/16
        public DataTable SearchServiceJobInStage(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchServiceJobInStage(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Isuru 2017/05/22
        public DataTable SearchChequeCodes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchChequeCodes(_initialSearchParams, _searchCatergory, _searchText);
        }


        //Isuru 2017/05/22
        public DataTable Searchaccountcode(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Searchaccountcode(_initialSearchParams, _searchCatergory, _searchText);
        }
        //tHARANGA 2017/06/01
        public DataTable GetItemSerNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemSerNo(_initialSearchParams, _searchCatergory, _searchText);
        }
        //subodana
        public List<BTU_SEARCH> getBTUCodes(string pgeNum, string pgeSize, string searchFld, string searchVal, string cat)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBTUCodes(pgeNum, pgeSize, searchFld, searchVal, cat);
        }
        // Tharanga 2017/06/03
        public DataTable GetsubJobNo(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetsubJobNo(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        // Tharanga 2017/06/03
        public DataTable GetJobNo(string _subJon, string _loc)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetJobNo(_subJon, _loc);
        }
        //Udaya 16.06.2017 - collect Credit note SRN 
        public DataTable SearchReversalInvoiceForCreditDebit(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchReversalInvoiceForCreditDebit(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Udaya 30.06.2017 - collect Debit note SRN 
        public DataTable SearchReversalInvoiceForDebit(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchReversalInvoiceForDebit(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Add by akila 2017/06/21 search adviso
        public DataTable SearchServiceAdvisor(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchServiceAdvisor(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Add by laksha 23 Jun 2017
        public DataTable GetServiceJobsWIPWEB(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetServiceJobsWIPWEB(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Add by akila 2017/06/28
        public DataTable SearchSupplierData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchSupplierData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Add by Lakshan 28 Jun 2017
        public DataTable GetQuotationAllWeb(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetQuotationAllWeb(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Add by lakshan 12Aug2017
        public DataTable GetItemforBoqMrn(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetItemforBoqMrn(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Add by lakshan 12Aug2017
        public DataTable SearchBoqProNoInMrn(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchBoqProNoInMrn(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Udaya 14.08.2017
        public DataTable SEARCH_PRN_REQNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_PRN_REQNo(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Add by Lakshan 20Aug2017
        public DataTable SearchMrnDocumentsWeb(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _dtFrom, DateTime _dtTo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchMrnDocumentsWeb(_initialSearchParams, _searchCatergory, _searchText, _dtFrom, _dtTo);
        }

        //Add by Lakshan 23Aug2017
        public DataTable GetInvTrackerDocData(string _initialSearchParams)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInvTrackerDocData(_initialSearchParams);
        }
        //add by tharanga 2017/08/29
        public DataTable Get_route_SearchData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_route_SearchData(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Add by Lakshan 11Sep2017
        public DataTable SearchSalesOrderWeb(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchSalesOrderWeb(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Add by Lakshan 20Aug2017
        public DataTable SearchTempPickItemData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _reptCommonDAL = new ReptCommonDAL();
            return _reptCommonDAL.SearchTempPickItemData(_initialSearchParams, _searchCatergory, _searchText);
        }
        public List<SHWRMMAN_SEARCH> getShowManager(string pgeNum, string pgeSize, string searchFld, string searchVal, string com)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getShowManager(pgeNum, pgeSize, searchFld, searchVal, com);
        }
        //Add by Lakshan 12Oct2017
        public DataTable GetInventoryTrackerSearchDataWEB(string _initialSearchParams, Boolean allow_SCM, Boolean allow_SCM2, out string err)
        {
            try
            {
                err = "";
                _commonSearchDAL = new CommonSearchDAL();
                SCMCommonDAL _scmCommonDAL = new SCMCommonDAL();

                DataTable _scm = new DataTable();
                DataTable _scm2 = new DataTable();
                if (allow_SCM == true && allow_SCM2 == true)
                {
                    // _scm = _scmCommonDAL.GetInventoryTrackerSearchData(_initialSearchParams);
                    _scm = _scmCommonDAL.GetInventoryTrackerSearchData_NEW2(_initialSearchParams);

                    //  _scm2 = _commonSearchDAL.GetInventoryTrackerSearchData(_initialSearchParams);
                    _scm2 = _commonSearchDAL.GetInventoryTrackerSearchDataWEB(_initialSearchParams);
                    if (_scm2.Rows.Count > 0)
                        _scm.Merge(_scm2);
                    if (_scm.Rows.Count > 0)
                    {
                        return _scm;
                    }
                    else
                    {
                        return null;
                    }

                }
                else if (allow_SCM == true && allow_SCM2 == false)
                {
                    _scm = _scmCommonDAL.GetInventoryTrackerSearchData_NEW2(_initialSearchParams);
                    return _scm;
                }
                else if (allow_SCM == false && allow_SCM2 == true)
                {
                    _scm2 = _commonSearchDAL.GetInventoryTrackerSearchDataWEB(_initialSearchParams);
                    _scm = _scm2;
                    return _scm;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return null;
            }
        }
        public DataTable SearchRevInvoice(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchRevInvoice(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }
        //Lakshan 19Oct2017
        public DataTable SearchSerialsByJobNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchSerialsByJobNo(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Lakshan 24Oct2017
        public DataTable SearchLocationByLocationCategory(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchLocationByLocationCategory(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Added By Udaya 06/Nov/2017
        public DataTable GetCourierNos(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCourierNos(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharindu 2017-11-13
        public DataTable Get_RootTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_RootTypes(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Akila 2017/11/27
        public DataTable SearchNationality(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchNationality(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Getbrandmng(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Getbrandmng(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetDocSubTypes_ADJ(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDocSubTypes_ADJ(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable get_quo_to_inv(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.get_quo_to_inv(_initialSearchParams, _searchCatergory, _searchText, _fromDate, _toDate);
        }

        //Tharindu 2017-12-07
        public DataTable Get_RefernceTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_RefernceTypes(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GET_PRIT_BY_CUST(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GET_PRIT_BY_CUST(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetCrCdCircular(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCrCdCircular(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetADJREQ_DET(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetADJREQ_DET(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Add by lakshan 31Jan2018
        public DataTable SerachSatProHdr(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _dtFrom, DateTime _dtTo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SerachSatProHdr(_initialSearchParams, _searchCatergory, _searchText, _dtFrom, _dtTo);
        }

        public DataTable GetCourierCompany(string refdoc, string loc, string com)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetCourierCompany(refdoc, loc, com);
        }

        //Add by lakshan 14Feb2018
        public DataTable SearchTempPickItemDataPartial(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _reptCommonDAL = new ReptCommonDAL();
            return _reptCommonDAL.SearchTempPickItemDataPartial(_initialSearchParams, _searchCatergory, _searchText);
        }


        //Akila 2018/01/25
        public DataTable SearchEvents(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchEvents(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Dulaj 2018-Mar-19
        public DataTable GetDocTypes(string _com, string _loc, string _docName)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetDocTypes(_com, _loc, _docName);
        }

        //Akila 2018/03/29
        public DataTable SearchDistrictDetails(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchDistrictDetails(_initialSearchParams, _searchCatergory, _searchText);
        }
        //tharanga 2018/04/09
        public DataTable Search_mid_account(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_mid_account(_initialSearchParams, _searchCatergory, _searchText);
        }

        //pasindu 2018/05/16

        public List<RENT_SCH_SEARCH> getRentSCHDetail(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getRentSCHDetail(pgeNum, pgeSize, searchFld, searchVal, company);
        }


        public List<PAYMENT_TYPES> getPaymentTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getPaymentTypes(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        //Pasindu 2018/05/21
        public List<PAYMENT_SUB_TYPES> getPaymentSubTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string p_type)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getPaymentSubTypes(pgeNum, pgeSize, searchFld, searchVal, company, p_type);
        }
        //tharanga 2018/05/17
        public DataTable Search_veh_reg_ref(string _initialSearchParams, string _searchCatergory, string _searchText)
        {

            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_veh_reg_ref(_initialSearchParams, _searchCatergory, _searchText);

        }
        public List<ref_cht_accgrp> getChartAccCodeCode(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getChartAccCodeCode(pgeNum, pgeSize, searchFld, searchVal, company);
        }

        //Pasindu 2018/06/01
        public List<REF_BONUS_SCHEME> getSchemeCode(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string p_circu_number)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getSchemeCode(pgeNum, pgeSize, searchFld, searchVal, company, p_circu_number);
        }
        //tharanga 2018/06/22
        public DataTable Get_tec_by_teamcd(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_tec_by_teamcd(_initialSearchParams, _searchCatergory, _searchText);
        }
        //tharanga 2018/07/13
        public DataTable Get_scv_location(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_scv_location(_initialSearchParams, _searchCatergory, _searchText);
        }
        //tharanga 2018/07/13
        public DataTable serch_pcbysvcloc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.serch_pcbysvcloc(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_scv_location_BY_LOCTIONTABLE(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_scv_location_BY_LOCTIONTABLE(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_scv_agent(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_scv_agent(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable searchDepositBankCode_cred_card(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchDepositBankCode_cred_card(_initialSearchParams, _searchCatergory, _searchText);
        }
        public List<SRCH_PAY_REQ> searchPaymentRequest(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string reqtp)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchPaymentRequest(pgeNum, pgeSize, searchFld, searchVal, company, reqtp);
        }
        public List<SRCH_ACC_DET> searchAccountnumbers(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchAccountnumbers(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<SRCH_TEMP_SRCH> searchFieldValue(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchFieldValue(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<COMMON_SEARCH> searchCommonDataValue(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId, string type)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchCommonDataValue(pgeNum, pgeSize, searchFld, searchVal, company, userId, type);
        }

        //Rangika 2019/06/08 
        public DataTable get_item_rest(string _com, string mic_cd)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.get_item_rest(_com, mic_cd);
        }
        public List<PAYMENT_TYPES> getAccPaymentTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getAccPaymentTypes(pgeNum, pgeSize, searchFld, searchVal, company);
        }

        public DataTable Search_Bond_Itm_Model(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_Bond_Itm_Model(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Search_Wharf_Employee(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Search_Wharf_Employee(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable SEARCH_TO_BOND_NO_Shp(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SEARCH_TO_BOND_NO_Shp(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable Get_All_Users_Loc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_All_Users_Loc(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Chamal 20-Dec-2018
        public List<string> GenerateZPLBarcodes(string _label, string _option, List<string> _source, int noofprint, out string _msg, out bool _success)
        {
            string _out_msg = string.Empty;
            string _itemcode = string.Empty;
            string _serialno = string.Empty;
            string _itemdesc = string.Empty;
            bool _out_success = false;
            int _qty = 0;
            List<string> _barList = new List<string>();

            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            string _barcodeTemplate = _commonSearchDAL.GetBarcodeString(_label);
            if (!string.IsNullOrEmpty(_barcodeTemplate))
            {
                if (_option == "INDIVIDUAL")
                {
                    int i = 0;
                    foreach (string word in _source)
                    {
                        if (i == 0)
                        { _itemcode = word; }
                        else if (i == 1)
                        { _serialno = word; }
                        else if (i == 2)
                        { _itemdesc = word; }
                        i++;
                    }
                    string _barcode = String.Copy(_barcodeTemplate);

                    _barcode = _barcode.Replace("@itemcode", _itemcode);
                    _barcode = _barcode.Replace("@serialno", _serialno);
                    _barcode = _barcode.Replace("@itemdesc", _itemdesc);

                    for (i = 0; i < noofprint; i++)
                    {
                        _barList.Add(_barcode);
                    }

                    if (_barList.Count > 0)
                    {
                        _out_success = true;
                    }
                    else
                    {
                        _out_msg = "Unknown Error";
                        _out_success = false;
                    }

                }
                else if (_option == "DOC")
                {
                    string _docno = string.Empty;
                    int _serialized = 0;
                    foreach (string word in _source)
                    {
                        _docno = word;
                    }
                    DataTable _doclist = _commonSearchDAL.GetDocumentSerials(_docno);
                    if (_doclist != null)
                    {
                        if (_doclist.Rows.Count > 0)
                        {
                            foreach (DataRow row in _doclist.Rows)
                            {
                                _itemcode = row["ITEMCODE"].ToString();
                                _qty = Convert.ToInt32(row["QTY"].ToString());
                                _serialno = row["SERNO"].ToString();
                                _itemdesc = row["MODEL"].ToString() + " | " + row["SHORTDESC"].ToString();
                                _serialized = Convert.ToInt32(row["SERIALIZED"].ToString());

                                if (!string.IsNullOrEmpty(_serialno) && _serialno.ToUpper().ToString() != "N/A")
                                {
                                    string _barcode = String.Copy(_barcodeTemplate);
                                    _barcode = _barcode.Replace("@itemcode", _itemcode);
                                    _barcode = _barcode.Replace("@serialno", _serialno);
                                    _barcode = _barcode.Replace("@itemdesc", _itemdesc);
                                    _barList.Add(_barcode);
                                }
                                //Add by Chamal 03-Jan-2019
                                if (!string.IsNullOrEmpty(_serialno) && _serialno.ToUpper().ToString() == "N/A")
                                {
                                    string _barcode = String.Copy(_barcodeTemplate);
                                    _barcode = _barcode.Replace("@itemcode", _itemcode);
                                    _barcode = _barcode.Replace("@serialno", "^GB");
                                    _barcode = _barcode.Replace("@itemdesc", _itemdesc);
                                    _barList.Add(_barcode);
                                }
                                if (string.IsNullOrEmpty(_serialno))
                                {
                                    if (_serialized == -1)
                                    {
                                        for (int i = 0; i < _qty; i++)
                                        {
                                            string _barcode = String.Copy(_barcodeTemplate);
                                            _barcode = _barcode.Replace("@itemcode", _itemcode);
                                            _barcode = _barcode.Replace("@serialno", "^GB");
                                            _barcode = _barcode.Replace("@itemdesc", _itemdesc);
                                            _barList.Add(_barcode);
                                        }
                                    }
                                }
                            }

                            if (_barList.Count > 0)
                            {
                                _out_success = true;
                            }
                            else
                            {
                                _out_msg = "Unknown Error";
                                _out_success = false;
                            }
                        }
                        else
                        {
                            _out_msg = "Invalid document number";
                        }
                    }
                    else
                    {
                        _out_msg = "Invalid document number";
                    }
                }
            }
            _commonSearchDAL.ConnectionClose();
            GC.Collect();
            _msg = _out_msg;
            _success = _out_success;
            return _barList;
        }

        public List<ACCOUNT_TAX> srchTaxTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string creditor)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.srchTaxTypes(pgeNum, pgeSize, searchFld, searchVal, company, creditor);
        }
        public List<PUR_SEARCH> searchPOPayRequest(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type, string creditor)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.searchPOPayRequest(pgeNum, pgeSize, searchFld, searchVal, company, creditor);
        }

        //Chamal 31-Dec-2018
        public DataTable GerBarcodePrinters(string _user)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GerBarcodePrinters(_user);
        }

        //Chamal 05-Jan-2019
        public DataTable GerBarcodeLabels(string _user)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GerBarcodeLabels(_user);
        }
        public DataTable SearchCusdecHeaderWithStus(string _initialSearchParams, string _searchCatergory, string _searchText, string stus)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCusdecHeaderWithStus(_initialSearchParams, _searchCatergory, _searchText, stus);
        }

        //Dilan 07-Jan-2019
        public Int32 UploadEnadocDoc(REF_ENADOC_DOCS endocDocRef)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            Int32 result = _commonSearchDAL.UploadEnadocDoc(endocDocRef);
            _commonSearchDAL.ConnectionClose();
            return result;
        }
        //Dilan 10/Jan/2019
        public DataTable GetEnadocDoc(DocSearchParam endocDocParam)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.GetEnadocDoc(endocDocParam);
        }
        //Dilan 11/Jan/2019
        public Int32 SetEnadocHash(string enadocHash, string userId)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.SetEnadocHash(enadocHash, userId);
        }

        //Dilan 11/Jan/2019
        public DataTable GetEnadocUserPermissions(string userId, string com)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.GetEnadocUserPermissions(userId, com);
        }
        public List<Mst_Sys_Para> Get_hp_collection_aloow_det(string _msp_pty_com, string _msp_pty_tp, string _msp_pty_cd, string _msp_dir_pty_cd, string _msp_rest_type)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_hp_collection_aloow_det(_msp_pty_com, _msp_pty_tp, _msp_pty_cd, _msp_dir_pty_cd, _msp_rest_type);
        }
        //Dulaj 02/Jan/2019
        public DataTable SearchCusdecHeaderHS(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.SearchCusdecHeaderHS(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Dulaj 02/Jan/2019
        public DataTable GerHsSearchDetails(string company, string entry, string hscode, DateTime fromDate, DateTime toDate, string description)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _securityDAL = new SecurityDAL();
            if (_securityDAL.Is_Report_DR("HSCODESEARCH") == true) _commonSearchDAL.ConnectionOpen_DR();
            return _commonSearchDAL.GerHsSearchDetails(company, entry, hscode, fromDate, toDate, description);
        }

        //Dilan 16/Jan/2019
        public Int32 SetAccessToken(string token,string refrshToken)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.SetAccessToken(token,refrshToken);
        }

        //Dilan 16/Jan/2019
        public DataTable GetAccessToken()
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.GetAccessToken();
        }

        //Dilan 22/Jan/2019
        public DataTable GetTagProfileByLibId(string libId)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.GetTagProfileByLibId(libId);
        }

        //Dilan 24/Jan/2019
        public DataTable GetViewAccessToken(string com, string userid, int libId)
        {
            _commonSearchDAL = new CommonSearchDAL();
            _commonSearchDAL.ConnectionOpen();
            return _commonSearchDAL.GetViewAccessToken(com, userid, libId);
        }
        public DataTable GetShipmentTrackerDatByClearenseDatePending(ImportsBLHeader _obj, DateTime _dtFrom, DateTime _dtTo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetShipmentTrackerDatByClearenseDatePending(_obj, _dtFrom, _dtTo);
        }
        public DataTable GetShipmentTrackerDatByClearenseDateActual(ImportsBLHeader _obj, DateTime _dtFrom, DateTime _dtTo)
        {
            _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetShipmentTrackerDatByClearenseDateActual(_obj, _dtFrom, _dtTo);
        }
    }
}
