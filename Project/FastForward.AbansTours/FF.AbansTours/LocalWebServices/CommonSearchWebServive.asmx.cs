using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using FF.AbansTours;
using FF.BusinessObjects;

namespace FF.AbansTours.LocalWebServices
{
    /// <summary>
    /// This is a web service class for common search functionalty, called by front end using javascript.
    /// Created By : Miginda Geeganage.
    /// Created On : 16/03/2012
    /// Modified By :
    /// Modified On :
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class CommonSearchWebServive : System.Web.Services.WebService
    {
        private ChannelOperator chnlOpt = null;
        private BasePage _basePage = null;

        [WebMethod]
        public string TestMethod()
        {
            return "This is a Webservice test Method.";
        }

        [WebMethod]
        public string GetCommonSearchData()
        {
            string htmlText = string.Empty;
            chnlOpt = new ChannelOperator();

            CommonUIOperations s = new CommonUIOperations();
            return htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCommonSearchData(string.Empty, string.Empty));
        }

        [WebMethod]
        public string GetLocationSearchData(string _companyCode, string _locationCode, string _locationDesc)
        {
            string htmlText = string.Empty;
            chnlOpt = new ChannelOperator();

            CommonUIOperations s = new CommonUIOperations();
            return htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetLocationSearchData(_companyCode, _locationCode, _locationDesc));
        }

        /// <summary>
        /// This is common service method for search functionalty.
        /// Have to call relavant DAL method according to the SearchUserControlType type.
        /// </summary>
        /// <param name="_initialParams">This is initial parameters send by front end through javascript.</param>
        /// <param name="_searchCatergory">Search Catergory</param>
        /// <param name="_searchText">Search Text</param>
        /// <returns>HTML formatted string</returns>
        [WebMethod]
        public string GetCommonSearchDetails(string _initialParams, string _searchCatergory, string _searchText)
        {
            string htmlText = string.Empty;
            chnlOpt = new ChannelOperator();

            //Split and get the SearchUserControlType.
            string[] param = _initialParams.Split(new string[] { ":" }, StringSplitOptions.None);
            CommonUIDefiniton.SearchUserControlType searchType = (CommonUIDefiniton.SearchUserControlType)Convert.ToInt32(param[0].ToString());

            //Get the remaining SP parameters.
            string searchParams = param[1];

            switch (searchType)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetLocationSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCompanySearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetItemSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetAvailableSerialSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableNoneSerial: // Add Chamal 06/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetAvailableNoneSerialSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.IssuedSerial:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetIssuedSerialSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetUserLocationSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetUserProfitCentreSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetInvoiceSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Receipt: //Add Chamal 17-08-2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetReceiptsByType(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ReceiptType: // Add Chamal 27/06/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetReceiptTypes(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemStatus: // Add Shani 27/06/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCompanyItemStatusData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier: // Add Shani 27/06/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetSupplierData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.OutsideParty: //kapila 2/7/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetOutsidePartySearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Country: //kapila 2/7/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCountrySearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Group_Sale: //kapila 9/7/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetGroupSaleSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Channel: //kapila 10/7/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetChannelSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Getloc_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Getloc_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Getloc_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Getloc_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Getloc_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Getloc_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Getloc_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCat_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCat_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCat_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.InvoiceType: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetInvoiceTypeData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Customer: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCustomerGenaral(searchParams, _searchCatergory, _searchText, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD)));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.NIC: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCustomerGenaral(searchParams, _searchCatergory, _searchText, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC)));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCustomerGenaral(searchParams, _searchCatergory, _searchText, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB)));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesOrder: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetInvoiceSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetInvoiceSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPriceBookData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPriceBookByCompanyData(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.PriceLevel: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPriceLevelData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPriceLevelByBookData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPriceLevelItemStatusData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetEmployeeData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCurrencyData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetBusinessCompany(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceItem: //Edited By Prabhath on 17/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPriceItemSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WarraWarrantyNo: //Edited By Prabhath on 27/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetWarrantySearchByWarrantyNoSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WarraSerialNo: //Edited By Prabhath on 27/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetWarrantySearchBySerialNoSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GeneralRequest: //Edited By Prabhath on 27/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetGeneralRequestSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashInvoice: //Edited By Prabhath on 27/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetInvoiceByInvType(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HireInvoice: //Edited By Prabhath on 27/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetInvoiceByInvType(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters: //Edited By Prabhath on 27/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceType: //Edited By Shani on 28/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_PriceTypes_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Scheme: //Edited By Shani on 28/07/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_SchemesCD_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount: //Edited By Prabhath on 14/08/2012
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetHpAccountSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserID:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetUserID(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WHAREHOUSE:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.Inventory.GetLocationByType(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetTown(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetOPE(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SearchAvlbleSerial4Invoice(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.General.GetSalesTypes(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_SubType:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_sales_subtypes(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetAllModels(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Insurance_Term:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetAllInsTerms(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        //Call the relavant DAL method and convert the datasource in to formatted HTML text.
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetItemforInvoiceSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                //--------------------------------------------------------------------------------
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                //---------------------------------------------------------------------------------
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCat_SearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPromotionSearch(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPromotionSearch(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loyalty_Type:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetLoyaltyTypes(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeCate:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_employee_categories(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeEPF:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_employee_EPF(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.QuotationForInvoice:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetQuotationDetailForInvoice(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_DocNum_ByRefNo(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.INV_DocNo:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Search_int_hdr_Document(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SearchBankBranchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Schema_category:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetSchemaCategory(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Schema_Type:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetSchemaType(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                //kapila 23/11/2012
                case CommonUIDefiniton.SearchUserControlType.ServiceAgent:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetServiceAgent(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpParaTp:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.Get_hp_parameterTypes(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllSalesInvoice:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetAllInvoiceSearchData(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FixAssetRefNo:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GET_FixAsset_ref(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiveSeach:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SEARCH_Invoice(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetCustomerCommon(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBank:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.searchDepositBankCode(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetAdvancedRecieptForCus(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearch:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SEARCH_ENQUIRY(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ToursFacCompany:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SEARCH_FAC_COM(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ChargeCodeTours:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SEARCH_ChargeCode(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TransferCodes:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SEARCH_TransferCode(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Miscellaneous:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SEARCH_Miscellaneous(searchParams, _searchCatergory, _searchText));
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetBankAccounts(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceByCus:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetInvoicebyCustomer(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutstandingInv:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetOutstandingInvoice(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptDate:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetReceiptsDateTBS(searchParams, _searchCatergory, _searchText, Convert.ToDateTime(DateTime.Now).AddMonths(-1), Convert.ToDateTime(DateTime.Now)));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PaymentDate:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPaymentDateTBS(searchParams, _searchCatergory, _searchText, Convert.ToDateTime(DateTime.Now).AddMonths(-1), Convert.ToDateTime(DateTime.Now)));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town_new:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetTown_new(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Drivers:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SearchDrivers(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Vehicles:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SearchVehicle(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DriverTBS:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SearchDriverTBS(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeTBS:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SearchEmployeeTBS(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearchWithStage:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SearchEnquiryWithStage(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LogSheetHEader:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SearchLogHeader(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllVehicles:
                    {
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.SearchAllVehicles(searchParams, _searchCatergory, _searchText));
                        break;
                    }
                default:
                    break;
            }

            return htmlText;
        }

        [WebMethod]
        public string GetCommonSearchDetailsDate(string _initialParams, string _searchCatergory, string _searchText, string fromDate, string toDate)
        {
            string htmlText = string.Empty;
            chnlOpt = new ChannelOperator();

            //Split and get the SearchUserControlType.
            string[] param = _initialParams.Split(new string[] { ":" }, StringSplitOptions.None);
            CommonUIDefiniton.SearchUserControlType searchType = (CommonUIDefiniton.SearchUserControlType)Convert.ToInt32(param[0].ToString());

            //Get the remaining SP parameters.
            string searchParams = param[1];

            switch (searchType)
            {
                case CommonUIDefiniton.SearchUserControlType.ReceiptDate:
                    {
                        DateTime fDate = Convert.ToDateTime(Convert.ToDateTime(fromDate).ToString("dd-MM-yyyy"));
                        DateTime tDate = Convert.ToDateTime(Convert.ToDateTime(toDate).ToString("dd-MM-yyyy"));
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetReceiptsDateTBS(searchParams, _searchCatergory, _searchText, fDate, tDate));
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PaymentDate:
                    {
                        DateTime fDate = Convert.ToDateTime(Convert.ToDateTime(fromDate).ToString("dd-MM-yyyy"));
                        DateTime tDate = Convert.ToDateTime(Convert.ToDateTime(toDate).ToString("dd-MM-yyyy"));
                        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetPaymentDateTBS(searchParams, _searchCatergory, _searchText, fDate, tDate));
                        break;
                    }
                default:
                    break;
            }

            return htmlText;
        }

        //Code By - Prabhath on 26/03/2012
        [WebMethod]
        public BusinessObjects.MasterItem GetItem(string _item)
        {
            chnlOpt = new ChannelOperator();
            return chnlOpt.Inventory.GetItem("ABL", _item);
        }

        //Code By - Prabhath on 26/03/2012
        [WebMethod]
        public string GetItemDescription(string _item)
        {
            chnlOpt = new ChannelOperator();
            MasterItem _objItem = chnlOpt.Inventory.GetItem("ABL", _item);
            string _description = "";
            if (_objItem == null) _description = ""; else _description = _objItem.Mi_longdesc;
            return _description;
        }

        /// <summary>
        /// Created By : Miginda Geeganage On 26-03-2012
        /// </summary>
        [WebMethod(Description = "GetAllItemDetailsByItemCode", EnableSession = true)]
        public MasterItem GetAllItemDetailsByItemCode(string _itemCode)
        {
            chnlOpt = new ChannelOperator();
            _basePage = new BasePage();
            return chnlOpt.Inventory.GetItem(_basePage.GlbUserComCode, _itemCode);
        }

        /// <summary>
        /// Created By : Miginda Geeganage On 17-04-2012
        /// </summary>
        [WebMethod(Description = "GetAllInvoiceDetailsByInvoiceNo", EnableSession = true)]
        public InvoiceHeader GetAllInvoiceDetailsByInvoiceNo(string _invoiceNo)
        {
            chnlOpt = new ChannelOperator();
            _basePage = new BasePage();
            return chnlOpt.Sales.GetInvoiceHeaderDetails(_invoiceNo);
        }

        //Code By - Prabhath on 26/03/2012
        [WebMethod]
        public Boolean IsItemSerialized_1(string _item)
        {
            chnlOpt = new ChannelOperator();
            return chnlOpt.Inventory.IsItemSerialized_1(_item);
        }

        //Code By - Prabhath on 26/03/2012
        [WebMethod]
        public Boolean IsItemSerialized_2(string _item)
        {
            chnlOpt = new ChannelOperator();
            return chnlOpt.Inventory.IsItemSerialized_2(_item);
        }

        //Code By - Prabhath on 26/03/2012
        [WebMethod]
        public Boolean IsItemSerialized_3(string _item)
        {
            chnlOpt = new ChannelOperator();
            return chnlOpt.Inventory.IsItemSerialized_3(_item);
        }

        //Code By - Prabhath on 26/03/2012
        [WebMethod]
        public Boolean IsItemHaveSubSerial(string _item)
        {
            chnlOpt = new ChannelOperator();
            return chnlOpt.Inventory.IsItemHaveSubItem(_item);
        }

        //Code By - Prabhath on 27/03/2012
        [WebMethod]
        public Boolean IsUOMDecimalAllow(string _item)
        {
            chnlOpt = new ChannelOperator();
            return chnlOpt.Inventory.IsUOMDecimalAllow(_item);
        }

        //Code By Chamal 04/08/2012
        [WebMethod]
        public List<SystemUserCompany> GetUserCompany(string _user)
        {
            chnlOpt = new ChannelOperator();
            return chnlOpt.Security.GetUserCompany(_user);
        }

        //kapila 12/6/2012
        [WebMethod(Description = "GetAvailableWarrBySerial", EnableSession = true)]
        public InventorySerialRefN GetAvailableWarrBySerial(string _Item, string _Serial)
        {
            chnlOpt = new ChannelOperator();
            _basePage = new BasePage();
            return chnlOpt.Inventory.GetAvailableWarranty(_basePage.GlbUserComCode, _basePage.GlbUserDefLoca, _Item, _Serial);
        }

        // Code By : Chamal De Silva On 07-07-2012
        [WebMethod]
        public ReptPickSerials GetAvailableSerIDInformation(string inforpara)
        {
            chnlOpt = new ChannelOperator();
            _basePage = new BasePage();

            //string _itemcode = "", _ser1 = "", _ser2 = "", _serid="";
            //foreach (object value in values)
            //{
            //    Dictionary<string, object> dicValues = new Dictionary<string, object>();
            //    dicValues = (Dictionary<string, object>)value;
            //    _itemcode = dicValues["ItemCode"].ToString();
            //    _ser1 = dicValues["Ser1"].ToString();
            //    _ser2 = dicValues["Ser2"].ToString();
            //    _serid = dicValues["SerID"].ToString();

            //    //strOutput += "PlayerID = " + PlayerID + ", PlayerName=" + PlayerName + ",PlayerPPD= " + PlayerPPD + "<br>";

            //}

            string[] _x = inforpara.Split('|');

            return chnlOpt.Inventory.GetAvailableSerIDInformation(_x[0], _x[1], _x[2], _x[3], _x[4], _x[5]);
        }

        [WebMethod]
        public string GetLoc_HIRC_SearchDesc(int i, string _code)
        {
            if (i > 41 || i < 35)
            {
                return null;
            }
            chnlOpt = new ChannelOperator();
            CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new BasePage();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            DataTable dt = chnlOpt.CommonSearch.Getloc_HIRC_SearchData(paramsText.ToString(), "CODE", _code);
            if (dt.Rows.Count <= 0 || dt == null || dt.Rows.Count > 1)
                return null;
            else
                return dt.Rows[0][1].ToString();
        }

        //Written By Shani on 05/09/2012
        [WebMethod]
        public string Get_pc_HIRC_SearchDesc(int i, string _code)
        {
            chnlOpt = new ChannelOperator();
            CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new BasePage();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            try
            {
                DataTable dt = chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(paramsText.ToString(), "CODE", _code);
                if (dt == null)
                {
                    return "";
                }
                if (dt.Rows.Count <= 0 || dt == null || dt.Rows.Count > 1)
                    return "";
                else
                    return dt.Rows[0][1].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Written By Prabhath on 7/08/2012
        [WebMethod(EnableSession = true)]
        public MasterProfitCenter CheckProfitCenter(string _profitcenter)
        {
            chnlOpt = new ChannelOperator(); _basePage = new BasePage();
            return chnlOpt.Sales.GetProfitCenter(_basePage.GlbUserComCode, _profitcenter);
        }

        //Written By Prabhath on 4/09/2012
        private static BasePage _page = new BasePage();

        [System.Web.Services.WebMethod(EnableSession = true)]
        public List<string> PopulatePayMode(string invoiceType)
        {
            _page = new BasePage();
            List<PaymentType> _paymentTypeRef = _page.CHNLSVC.Sales.GetPossiblePaymentTypes(_page.GlbUserDefProf, invoiceType, DateTime.Now.Date);
            List<string> payTypes = new List<string>();
            payTypes.Add("");
            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
            {
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    payTypes.Add(pt.Stp_pay_tp);
                }
            }

            return payTypes;
        }

        //Written By Prabhath on 4/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public decimal GetReceiptAmount(string _receiptno)
        {
            _page = new BasePage();
            RecieptHeader _hdr = _page.CHNLSVC.Sales.GetReceiptHeader(_page.GlbUserComCode, _page.GlbUserDefProf, _receiptno);
            decimal _amt = 0;
            if (_hdr != null)
            {
                _amt = _hdr.Sar_tot_settle_amt - _hdr.Sar_used_amt;
            }

            return _amt;
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public MasterBusinessEntity GetCustomerInformationByCustomer(string _customer)
        {
            _page = new BasePage();
            return _page.CHNLSVC.Sales.GetBusinessCompanyDetail(_page.GlbUserComCode, _customer, string.Empty, string.Empty, "C");
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public MasterBusinessEntity GetCustomerInformationByNIC(string _nic)
        {
            _page = new BasePage();
            return _page.CHNLSVC.Sales.GetBusinessCompanyDetail(_page.GlbUserComCode, string.Empty, _nic, string.Empty, "C");
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public MasterBusinessEntity GetCustomerInformationByMobile(string _mobile)
        {
            _page = new BasePage();
            return _page.CHNLSVC.Sales.GetBusinessCompanyDetail(_page.GlbUserComCode, string.Empty, string.Empty, _mobile, "C");
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public CustomerAccountRef GetCustomerAccountDetail(string _customer)
        {
            _page = new BasePage();
            return _page.CHNLSVC.Sales.GetCustomerAccount(_page.GlbUserComCode, _customer);
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public bool GetPriceDefinitionByBookAndLevel(string _pricebook, string _invoicetype)
        {
            _page = new BasePage();
            bool _isValid = false;
            List<PriceDefinitionRef> _list = _page.CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(_page.GlbUserComCode, _pricebook.Trim(), string.Empty, _invoicetype.Trim(), _page.GlbUserDefProf);
            if (_list.Count == 0)
                _isValid = false;
            else
                _isValid = true;

            return _isValid;
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetDefaultDeliveryLocation()
        {
            _page = new BasePage();
            return _page.CHNLSVC.Sales.GetProfitCenter(_page.GlbUserComCode, _page.GlbUserDefProf).Mpc_def_loc;
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public bool GetBlockedItem(string _item)
        {
            bool _isBlocked = false;
            _page = new BasePage();
            MasterItemBlock _one = new MasterItemBlock();
            _one = _page.CHNLSVC.Inventory.GetBlockedItem(_page.GlbUserComCode, _page.GlbUserDefProf, _item);
            if (_one == null) _isBlocked = false; else _isBlocked = true;
            return _isBlocked;
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public bool IsValidItenStatusforPricing(string _list)
        {
            string[] _lst = _list.Split('|');
            return _page.CHNLSVC.Sales.CheckItemStatus(_page.GlbUserComCode, _lst[0], _lst[1], _lst[2]);
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public Int32 CheckSerialAvailability(string _serial)
        {
            Int32 _count = -1;
            _page = new BasePage();
            _count = _page.CHNLSVC.Inventory.CheckAvailableSerial(_page.GlbUserComCode, _page.GlbUserDefLoca, string.Empty, string.Empty, string.Empty, string.Empty, _serial, string.Empty);
            return _count;
        }

        //Written By Shanuka Perera on 5/05/2014
        [System.Web.Services.WebMethod(EnableSession = true)]
        public int ExitLoginSession()
        {
            _page = new BasePage();
            return _page.CHNLSVC.Security.ExitLoginSession(_page.GlbUserName, _page.GlbUserComDesc, "-1");
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetAvailableSerialShortDetail(string _serial)
        {
            _page = new BasePage();
            string _str = string.Empty;
            DataTable _tbl = new DataTable();
            _tbl = _page.CHNLSVC.Inventory.GetAvailableSerialShortDetail(_page.GlbUserComCode, _page.GlbUserDefLoca, string.Empty, string.Empty, string.Empty, string.Empty, _serial, string.Empty);
            foreach (DataRow r in _tbl.Rows)
            {
                _str = r[2].ToString() + "|" + r[3].ToString();
            }

            return _str;
        }

        //Written By Prabhath on 5/09/2012
        [System.Web.Services.WebMethod]
        public string BindMultiItem(string _item)
        {
            _page = new BasePage();
            return CommonUIOperations.MultiItemforCompeleteItem(_page.CHNLSVC.Inventory.GetCompeleteItemfromAssambleItem(_item));
        }

        ////Written By Prabhath on 27/09/2012
        //[System.Web.Services.WebMethod]
        //public bool IsValidGroupSaleCode(string _groupsalecode)
        //{
        //    bool _valid = false;
        //    _page = new BasePage();
        //    GroupSaleHeader _groupSale = _page.CHNLSVC.Sales.GetGroupSaleHeaderDetails(_groupsalecode);
        //    if (_groupSale != null)
        //        if (!string.IsNullOrEmpty(_groupSale.Hgr_com))
        //            _valid = true;

        //    return _valid;
        //}

        //Written By Prabhath on 27/09/2012
        [System.Web.Services.WebMethod]
        public GroupSaleHeader IsValidGroupSaleCode(string _groupsalecode)
        {
            _page = new BasePage();
            GroupSaleHeader _groupSale = _page.CHNLSVC.Sales.GetGroupSaleHeaderDetails(_groupsalecode);
            return _groupSale;
        }

        //Written By Prabhath on 26/10/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMonitorCustomer(string _parameter, CommonUIDefiniton.CustomerMonitorSearchType _searchtype)
        {
            string _str = null;
            _page = new BasePage();
            _str = _page.CHNLSVC.Sales.GetMonitorCustomer(_page.GlbUserComCode, _page.GlbUserDefProf, _parameter, _searchtype.ToString());
            return _str;
        }

        //Written By Prabhath on 26/10/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMonitorByCustomerDocument(string _customer, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetMonitorByCustomerDocument(_page.GlbUserComCode, _page.GlbUserDefProf, _customer), "SelectDocument", _tableid);
            return _str;
        }

        //Written By Prabhath on 31/10/2012
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetGuarantorDetail(string _account, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetHpGuarantor("G", _account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 31/10/2012
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAccountSummary(string _account, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetAccountSummary(_account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 31/10/2012
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAccountScheduleHistory(string _account, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetAccountScheduleHistory(_account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 31/10/2012
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetInvoiceWithSerial(string _invoice, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetInvoiceWithSerial(_invoice), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 31/10/2012
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetRevertAccountDetail(string _account, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetRevertAccountDetail(_account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 31/10/2012
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetRevertReleaseAccountDetail(string _account, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetRevertReleaseAccountDetail(_account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 31/10/2012
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetExchangeDetail(string _account, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetExchangeDetail(_account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 13/11/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCustomerDocumentOutstand(string _customer, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();

            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetCustomerDocumentWithSettlement(_page.GlbUserComCode, _page.GlbUserDefProf, _customer, 1), "SelectDocumentforReceipt", _tableid);
            return _str;
        }

        //Written By Prabhath on 13/11/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCustomerDocumentSettlement(string _customer, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();

            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetCustomerDocumentWithSettlement(_page.GlbUserComCode, _page.GlbUserDefProf, _customer, 0), "SelectDocumentforReceipt", _tableid);
            return _str;
        }

        //Written By Prabhath on 13/11/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCustomerPaymentSummary(string _customer, string _tableid)
        {
            string _str = string.Empty;
            _page = new BasePage();

            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetCustomerPaymentSummary(_page.GlbUserComCode, _page.GlbUserDefProf, _customer), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 13/11/2012
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetInvoiceReceipt(string _document, string _tableid)
        {
            string _str = string.Empty;
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetInvoiceReceipt(_document), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 13/11/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHireSaleAccountBalance(string _customer, string _tableid)
        {
            _page = new BasePage();
            string _str = string.Empty;
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetHireSaleAccountBalance(_page.GlbUserComCode, _page.GlbUserDefProf, _customer, DateTime.Now.Date), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 19/11/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCustomerAccountSchedule(string _account, string _tableid)
        {
            _page = new BasePage();
            string _str = string.Empty;
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetCustomerAccountSchedule(_account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 19/11/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCustomerAccountBalance(string _account, string _tableid)
        {
            _page = new BasePage();
            string _str = string.Empty;
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetCustomerAccountBalance(_account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 19/11/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAccountTransfer(string _account, string _tableid)
        {
            _page = new BasePage();
            string _str = string.Empty;
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetAccountTransfer(_account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 19/11/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAccountCustomerTrasnfer(string _account, string _tableid)
        {
            _page = new BasePage();
            string _str = string.Empty;
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetAccountCustomerTrasnfer(_account), "", _tableid);
            return _str;
        }

        //Written By Prabhath on 19/11/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAccountDiriyaDetail(string _account, string _tableid)
        {
            _page = new BasePage();
            string _str = string.Empty;
            _str = CommonUIOperations.ConvertDataTableToHtml(_page.CHNLSVC.Sales.GetAccountDiriyaDetail(_account), "", _tableid);
            return _str;
        }

        #region HP collection

        //Code By - Shani on 30/11/2012
        [WebMethod]
        public Decimal WS_GetAccountBal(string _account)
        {
            _page = new BasePage();
            Decimal _str = 0;
            _str = _page.CHNLSVC.Sales.Get_AccountBalance(DateTime.Now.Date, _account);
            return _str;
        }

        #endregion HP collection
    }
}