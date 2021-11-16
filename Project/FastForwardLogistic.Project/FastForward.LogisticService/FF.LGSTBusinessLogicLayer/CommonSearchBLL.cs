using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF.Interfaces;
using System.ServiceModel;
using FF.DataAccessLayer;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.DataAccessLayer.BaseDAL;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Security;

namespace FF.BusinessLogicLayer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CommonSearchBLL : ICommonSearch
    {
       /// <summary>
       /// Isuru 2017/05/26
       /// </summary>
       /// <param name="pgeNum"></param>
       /// <param name="pgeSize"></param>
       /// <param name="searchFld"></param>
       /// <param name="searchVal"></param>
       /// <param name="company"></param>
       /// <returns></returns>
        public List<cus_details> getcustomerdet(string pgeNum, string pgeSize, string searchFld, string searchVal,
            string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getcustomerdet(pgeNum, pgeSize, searchFld, searchVal, company);

        }

        /// <summary>
        /// Isuru 2017/05/30
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<cus_details> getCustomerExecutive(string pgeNum, string pgeSize, string searchFld, string searchVal,
            string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCustomerExecutive(pgeNum, pgeSize, searchFld, searchVal, company);

        }

        /// <summary>
        /// nuwan 2017.06.02
        /// get customer search details
        /// </summary>
        /// <param name="spgeNum">page number</param>
        /// <param name="pgeSize">page size</param>
        /// <param name="searchFld">search field</param>
        /// <param name="searchVal">search value</param>
        /// <param name="company">company</param>
        /// <param name="type">type</param>
        /// <returns>list</returns>
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetails(string spgeNum, string pgeSize, string searchFld,
            string searchVal, string company, string type = null)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCustomerDetails(spgeNum, pgeSize, searchFld, searchVal, company, type);
        }
        //added by Pasindu
        public List<MST_USERROLEID_SEARCH> getUserRoleID(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getUserRoleID(pgeNum, pgeSize, searchFld, searchVal, company);
        }

        //added by dilshan
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsByType(string spgeNum, string pgeSize, string searchFld,
            string searchVal, string company, string type = null)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCustomerDetailsByType(spgeNum, pgeSize, searchFld, searchVal, company, type);
        }
        //added by dilshan
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsByType_New(string spgeNum, string pgeSize, string searchFld,
            string searchVal, string company, string type = null)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCustomerDetailsByType_New(spgeNum, pgeSize, searchFld, searchVal, company, type);
        }
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsByJobNo(string jobno, string spgeNum, string pgeSize, string searchFld,
            string searchVal, string company, string type = null)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCustomerDetailsByJobNo(jobno, spgeNum, pgeSize, searchFld, searchVal, company, type);
        }
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsWithtype(string spgeNum, string pgeSize, string searchFld,
            string searchVal, string company, string type, string ctype)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCustomerDetailsWithtype(spgeNum, pgeSize, searchFld, searchVal, company, type, ctype);
        }
        public List<CustomerSearchObject> getCustomer(string telephone, string email)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCustomer(telephone, email);
        }
        public List<JOB_NUM_SEARCH> getJobNumber(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getJobNumber(_fromDate, _toDate, spgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        //dilshan
        public List<JOB_NUM_SEARCH> GetAllJobNumber(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetAllJobNumber(_fromDate, _toDate, spgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        public List<BL_NUM_SEARCH> getBLNumber(string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBLNumber(spgeNum, pgeSize, searchFld, searchVal, company, bltype);
        }
        public List<UOM_SEARCH> getUOM(string spgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getUOM(spgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<FIELD_SEARCH> getAllSearch(string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getAllSearch(spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<FIELD_SEARCH> getAllSearchByJobNo(string jobno, string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getAllSearchByJobNo(jobno, spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<Pay_type> GetPayTypes()
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetPayTypes();
        }
        public List<SEARCH_PORT> getPorts(string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getPorts(spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<SEARCH_PORT> getPortsRef(string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getPortsRef(spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<SEARCH_VESSEL> getVessels(string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getVessels(spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<SEARCH_VESSEL> getVesselsRef(string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getVesselsRef(spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<Pay_type> GetInwordTypes()
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetInwordTypes();
        }
        // Changed and commented below by Chathura on 13-sep-2017
        public List<MST_PC_SEARCH> getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId, string userDefChnl)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getUserProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company, userId, userDefChnl);
        }

        //public List<MST_PC_SEARCH> getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId)
        //{
        //    CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
        //    return _commonSearchDAL.getUserProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company, userId);
        //}

        public List<MST_COM_SEARCH> getUserCompanySet(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getUserCompanySet(pgeNum, pgeSize, searchFld, searchVal, company, userId);
        }


        public List<MST_EMPLOYEE_SEARCH_HEAD> getEmployeeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getEmployeeDetails(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<MST_EMPLOYEE_SEARCH_HEAD> getEmployeeDetailsEx(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getEmployeeDetailsEx(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<SEARCH_MESURE_TP> Get_Mesure_Tp(string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.Get_Mesure_Tp(spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<PETTYCASH_REQHDR_SRCHHED> ptyCshReqSearch(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc, string type)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.ptyCshReqSearch(_fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal, company, pc, type);
        }
        public List<CONS_SEARCH_HEAD> getConsigneeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getConsigneeDetails(pgeNum, pgeSize, searchFld, searchVal, company, type);
        }
        public List<JOB_NUM_SEARCH> getPettyCashJobSearch(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getPettyCashJobSearch(pgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        public List<MST_COST_ELEMENT_SEARCH> getCostElemts(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCostElemts(pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_COST_ELEMENT_SEARCH> getCostElemtsRef(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCostElemtsRef(pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_CUR_SEARCH> getCurrency(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCurrency(pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<FTW_VEHICLE_NO_SEARCH> getTelVehLcDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getTelVehLcDetails(pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<SEARCH_INVOICE> getInvoiceNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company ,string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getInvoiceNo(pgeNum, pgeSize, searchFld, searchVal, company,pc);
        }
        public List<SEARCH_INVOICE> getInvoiceNoCrd(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getInvoiceNoCrd(pgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        public List<SEARCH_INVOICE> getDebitNoteNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company , string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getDebitNoteNo(pgeNum, pgeSize, searchFld, searchVal, company,pc);
        }
        public List<SEARCH_INVOICE_BAL> getInvoiceNoByCus(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string cus, string pc,string othpc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getInvoiceNoByCus(pgeNum, pgeSize, searchFld, searchVal, company, cus, pc, othpc);
        }
        public List<PETTYCASH_SETTLE_SEARCH> getSettlementList(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getSettlementList(_fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        //Subodana 2017/07/13
        public List<MST_COUNTRY_SEARCH> getCountry(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCountry(pgeNum, pgeSize, searchFld, searchVal);
        }
        //Subodana 2017/07/13
        public List<MST_TOWN_SEARCH_HEAD> getTownDetails(string spgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getTownDetails(spgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_TOWN_SEARCH_HEAD> getTownwithCountry(string spgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getTownwithCountry(spgeNum, pgeSize, searchFld, searchVal, searchVal1);
        }
        public List<MST_DISTRICT_SEARCH> getDistrictwithCountry(string spgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getDistrictwithCountry(spgeNum, pgeSize, searchFld, searchVal, searchVal1);
        }
        public List<MST_PROVINCE_SEARCH> getProvincewithCountry(string spgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getProvincewithCountry(spgeNum, pgeSize, searchFld, searchVal, searchVal1);
        }
        public List<FIELD_SEARCH> getJobPouchSearch(string spgeNum, string pgeSize, string searchFld, string searchVal, string com)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getJobPouchSearch(spgeNum, pgeSize, searchFld, searchVal, com);
        }
        public List<FIELD_SEARCH2> getJobPouchSearch2(string spgeNum, string pgeSize, string searchFld, string searchVal, string com)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getJobPouchSearch2(spgeNum, pgeSize, searchFld, searchVal, com);
        }
        public List<MST_BANKACC_SEARCH_HEAD> getDepositedBanks(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getDepositedBanks(pgeNum, pgeSize, searchFld, searchVal, company);
        }
        public List<MST_BUSCOM_BANK_SEARCH_HEAD> getBusComBanks(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBusComBanks(pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD> getBoscomBankBranchs(string bankcd, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBoscomBankBranchs(bankcd, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_RECEIPT_TYPE_SEARCH_HEAD> getReceiptTypes(string company, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getReceiptTypes(company, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_RECEIPT_SEARCH_HEAD> getUnallowReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate, string customer, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getUnallowReceiptEntries(company, profCen, _fromDate, _toDate, customer,pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<MST_RECEIPT_SEARCH_HEAD> getReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getReceiptEntries(company, profCen, _fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<TYPE_OF_SHIPMENT> getShipmentTypes(string company, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getShipmentTypes(company, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<Job_No_Search> getJobNoForPouch(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pouch)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getJobNoForPouch(pgeNum, pgeSize, searchFld, searchVal, company, pouch);
        }
        // Added by Chathura on 13-Sep-2017
        public List<MST_MS_SEARCH> getModeOfShipment(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getModeOfShipment(pgeNum, pgeSize, searchFld, searchVal, company, userId);
        }
        // Added by Chathura on 20-Sep-2017
        public List<MST_DIVISION_SEARCH> getDivisions(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getDivisions(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
        }
        public List<BL_NUM_SEARCH> getBLNumberDf(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBLNumberDf(_fromDate, _toDate, spgeNum, pgeSize, searchFld, searchVal, company, bltype);
        }
        public List<BL_NUM_SEARCH_MBL> getBLNumberDfMBL(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBLNumberDfMBL(_fromDate, _toDate, spgeNum, pgeSize, searchFld, searchVal, company, bltype);
        }
        public List<FIELD_SEARCH_DF> getAllSearchDf(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getAllSearchDf(_fromDate, _toDate, spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<FIELD_SEARCH_DF_HBL> getAllSearchDfHBL(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getAllSearchDfHBL(_fromDate, _toDate, spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<FIELD_SEARCH_DF_HBL> getAllSearchDfHBLJob(DateTime _fromDate, DateTime _toDate, string jobno, string spgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getAllSearchDfHBLJob(_fromDate, _toDate, jobno, spgeNum, pgeSize, searchFld, searchVal, column);
        }
        public List<BL_NUM_SEARCH_DBL> getBLNumberDDf(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getBLNumberDDf(_fromDate, _toDate, spgeNum, pgeSize, searchFld, searchVal, company, bltype,pc);
        }

        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsJobFiltered(string spgeNum, string pgeSize, string searchFld,
            string searchVal, string company, string type, string ctype, string jobno)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCustomerDetailsJobFiltered(spgeNum, pgeSize, searchFld, searchVal, company, type, ctype, jobno);
        }

        public List<JOB_NUM_SEARCH> getJobPouchSearchDateFiltered(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string com)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getJobPouchSearchDateFiltered(_fromDate, _toDate, spgeNum, pgeSize, searchFld, searchVal, com);
        }

        public List<JOB_NUM_SEARCH> GetJobNumberInClose(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.GetJobNumberInClose(_fromDate, _toDate, spgeNum, pgeSize, searchFld, searchVal, company, pc);
        }

        public List<SEARCH_INVOICE> getInvoiceNoDateFiltered(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getInvoiceNoDateFiltered(_fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal, company, pc);
        }

        public List<SEARCH_INVOICE> getInvoiceNoDf(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getInvoiceNoDf(_fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        public List<SEARCH_INVOICE> getInvoiceNoDfApp(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getInvoiceNoDfApp(_fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        public List<SEARCH_INVOICE> getInvoiceNoDfNew(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc, string hbl)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getInvoiceNoDfNew(_fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal, company, pc, hbl);
        }
        public List<SEARCH_INVOICE> getInvoiceNoDfNonPC(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getInvoiceNoDfNonPC(_fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        public List<SEARCH_INVOICE> getInvoiceNoDfNonPCApp(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getInvoiceNoDfNonPCApp(_fromDate, _toDate, pgeNum, pgeSize, searchFld, searchVal, company, pc);
        }
        public List<MST_COST_ELEMENT_SEARCH> getRevenueElements(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getRevenueElements(pgeNum, pgeSize, searchFld, searchVal);
        }

        public List<PAY_TP_SEARCH> getPaymentTypes(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getPaymentTypes(pgeNum, pgeSize, searchFld, searchVal);
        }

        public List<USER_SEARCH> getUserDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getUserDetails(pgeNum, pgeSize, searchFld, searchVal,company);
        }
        
        public List<MST_USERS> getUserList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            List<MST_USERS> getUserListte = _commonSearchDAL.getUserList(pgeNum, pgeSize, searchFld, searchVal);

            for (int i = 0; i < getUserListte.Count; i++) {
                 SystemUser _systemaduser = new SystemUser();
                SecurityBLL secu = new SecurityBLL();
                secu.CheckValidADUser(getUserListte[i].Se_usr_id, out _systemaduser);
                getUserListte[i].Ad_title = _systemaduser.Ad_title;
                getUserListte[i].Ad_full_name = _systemaduser.Ad_full_name;
                getUserListte[i].Ad_department = _systemaduser.Ad_department;
            }

                return _commonSearchDAL.getUserList(pgeNum, pgeSize, searchFld, searchVal);
        }

        public List<MST_DEPARTMENT> getDeptList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getDeptList(pgeNum, pgeSize, searchFld, searchVal);
        }

        public List<MST_DESIGNATION> getDesignationtList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getDesignationtList(pgeNum, pgeSize, searchFld, searchVal);
        }

        public List<MST_COM_SEARCH> getCompanySet(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            CommonSearchDAL _commonSearchDAL = new CommonSearchDAL();
            return _commonSearchDAL.getCompanySet(pgeNum, pgeSize, searchFld, searchVal);
        }
    }
}
