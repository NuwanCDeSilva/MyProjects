using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects;
using System.Data;
using System.Web;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Security;

namespace FF.Interfaces
{
    [ServiceContract]
    public interface ICommonSearch
    {
        //Isuru 2017/05/26
        [OperationContract]
        List<cus_details> getcustomerdet(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Isuru 2017/05/30
        [OperationContract]
        List<cus_details> getCustomerExecutive(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        
        //nuwan 2017-06-02
        [OperationContract]
        List<MST_CUS_SEARCH_HEAD> getCustomerDetails(string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null);
        
        //Dilshan 26/01/2017
        [OperationContract]
        List<MST_CUS_SEARCH_HEAD> getCustomerDetailsByType(string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null);
        //Dilshan 26/01/2017
        [OperationContract]
        List<MST_CUS_SEARCH_HEAD> getCustomerDetailsByType_New(string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null);
        
        // Added by Chathura on 2-oct-2017
        [OperationContract]
        List<MST_CUS_SEARCH_HEAD> getCustomerDetailsByJobNo(string jobno, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null);
        //subodana 2017/06/22
        [OperationContract]
        List<JOB_NUM_SEARCH> getJobNumber(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);
        //Dilshan
        [OperationContract]
        List<JOB_NUM_SEARCH> GetAllJobNumber(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);//Sanjaya 2017-06-23
        [OperationContract]
        List<CustomerSearchObject> getCustomer(string telephone, string email);
        //subodana 2017/06/28
        [OperationContract]
        List<BL_NUM_SEARCH> getBLNumber(string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype);

        //subodana 2017/06/28
        [OperationContract]
        List<UOM_SEARCH> getUOM(string spgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //subodana 2017/06/28
        [OperationContract]
        List<FIELD_SEARCH> getAllSearch(string spgeNum, string pgeSize, string searchFld, string searchVal, string column);
        // Added by Chathura
        [OperationContract]
        List<FIELD_SEARCH> getAllSearchByJobNo(string jobno, string spgeNum, string pgeSize, string searchFld, string searchVal, string column);
        //subodana 2017/06/29
        [OperationContract]
        List<Pay_type> GetPayTypes();
        //subodana 2017/06/29
        [OperationContract]
        List<SEARCH_PORT> getPorts(string spgeNum, string pgeSize, string searchFld, string searchVal, string column);
        //Dilshan 2017/09/11
        [OperationContract]
        List<SEARCH_PORT> getPortsRef(string spgeNum, string pgeSize, string searchFld, string searchVal, string column);
        //subodana 2017/06/29
        [OperationContract]
        List<SEARCH_VESSEL> getVessels(string spgeNum, string pgeSize, string searchFld, string searchVal, string column);
        //Dilshan 2017/09/11
        [OperationContract]
        List<SEARCH_VESSEL> getVesselsRef(string spgeNum, string pgeSize, string searchFld, string searchVal, string column);
        //subodana 2017/06/29
        [OperationContract]
        List<Pay_type> GetInwordTypes();
        //subodana 2017/06/30
        [OperationContract]
        List<SEARCH_MESURE_TP> Get_Mesure_Tp(string spgeNum, string pgeSize, string searchFld, string searchVal, string column);
        // Commented by Chathura on 15-sep-2017
        //Nuwan 2017.06.30
        //[OperationContract]
        //List<MST_PC_SEARCH> getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId);
        //Dilshan 31/08/2017
        [OperationContract]
        List<MST_COM_SEARCH> getUserCompanySet(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId);
        
        //Nuwan 2017.07.03
        [OperationContract]
        List<MST_EMPLOYEE_SEARCH_HEAD> getEmployeeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //dilshan 26/05/2018
        [OperationContract]
        List<MST_EMPLOYEE_SEARCH_HEAD> getEmployeeDetailsEx(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //Nuwan 2017.07.03
        [OperationContract]
        List<PETTYCASH_REQHDR_SRCHHED> ptyCshReqSearch(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company,string pc,string type);

        //Nuwan 2017.07.03
        [OperationContract]
        List<CONS_SEARCH_HEAD> getConsigneeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type);
    
        //Nuwan 2017.07.05
        [OperationContract]
        List<JOB_NUM_SEARCH> getPettyCashJobSearch(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);

        //Nuwan2017.07.05
        [OperationContract]
        List<MST_COST_ELEMENT_SEARCH> getCostElemts(string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Dilshan 2017/09/11
        [OperationContract]
        List<MST_COST_ELEMENT_SEARCH> getCostElemtsRef(string pgeNum, string pgeSize, string searchFld, string searchVal);
        //Nuwan 2017.07.05
        [OperationContract]
        List<MST_CUR_SEARCH> getCurrency(string pgeNum,string  pgeSize,string searchFld,string searchVal);

        //Nuwan 2017.07.05
        [OperationContract]
        List<FTW_VEHICLE_NO_SEARCH> getTelVehLcDetails(string pgeNum, string pgeSize, string searchFld, string searchVal);
        //subodana2017-07-06
        [OperationContract]
        List<SEARCH_INVOICE> getInvoiceNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);
        // Added by Chathura
        [OperationContract]
        List<SEARCH_INVOICE> getInvoiceNoCrd(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);
        //subodana2017-07-06
        [OperationContract]
        List<PETTYCASH_SETTLE_SEARCH> getSettlementList(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);
        //Subodana 2017/07/13
        [OperationContract]
        List<MST_COUNTRY_SEARCH> getCountry(string pgeNum, string pgeSize, string searchFld, string searchVal);
        //subodana 2017/07/13
        [OperationContract]
        List<MST_TOWN_SEARCH_HEAD> getTownDetails(string spgeNum, string pgeSize, string searchFld, string searchVal);
        //Dilshan 2017/09/13
        [OperationContract]
        List<MST_TOWN_SEARCH_HEAD> getTownwithCountry(string spgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1);
        //Dilshan 2018/05/12
        [OperationContract]
        List<MST_DISTRICT_SEARCH> getDistrictwithCountry(string spgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1);
        //Dilshan 2018/05/12
        [OperationContract]
        List<MST_PROVINCE_SEARCH> getProvincewithCountry(string spgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1);
        //subodana 2017/07/13
        [OperationContract]
        List<FIELD_SEARCH> getJobPouchSearch(string spgeNum, string pgeSize, string searchFld, string searchVal, string com);
        //subodana 2017/07/19
        [OperationContract]
        List<MST_BANKACC_SEARCH_HEAD> getDepositedBanks(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //subodana 2017/07/19
        [OperationContract]
        List<MST_BUSCOM_BANK_SEARCH_HEAD> getBusComBanks(string pgeNum, string pgeSize, string searchFld, string searchVal);
        //subodana 2017/07/19
        [OperationContract]
        List<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD> getBoscomBankBranchs(string bankcd, string pgeNum, string pgeSize, string searchFld, string searchVal);
        [OperationContract]
        List<MST_CUS_SEARCH_HEAD> getCustomerDetailsWithtype(string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string type, string ctype);
        [OperationContract]
        List<SEARCH_INVOICE_BAL> getInvoiceNoByCus(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string cus, string pc,string othpc);
        [OperationContract]
        List<MST_RECEIPT_TYPE_SEARCH_HEAD> getReceiptTypes(string company, string pgeNum, string pgeSize, string searchFld, string searchVal);
        [OperationContract]
        List<MST_RECEIPT_SEARCH_HEAD> getUnallowReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate,string customer, string pgeNum, string pgeSize, string searchFld, string searchVal);
        [OperationContract]
        List<MST_RECEIPT_SEARCH_HEAD> getReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal);
        [OperationContract]
        List<SEARCH_INVOICE> getDebitNoteNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);
        [OperationContract]
        List<TYPE_OF_SHIPMENT> getShipmentTypes(string company, string pgeNum, string pgeSize, string searchFld, string searchVal);
        [OperationContract]
        List<Job_No_Search> getJobNoForPouch(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pouch);
        [OperationContract]
        List<FIELD_SEARCH2> getJobPouchSearch2(string spgeNum, string pgeSize, string searchFld, string searchVal, string com);
        // Added by Chathura on 13-Sep-2017
        [OperationContract]
        List<MST_PC_SEARCH> getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId, string userDefChnl); 
        // Added by Chathura on 13-Sep-2017
        [OperationContract]
        List<MST_MS_SEARCH> getModeOfShipment(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId);
        // Added by Chathura on 20-sep-2017
        [OperationContract]
        List<MST_DIVISION_SEARCH> getDivisions(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal);
        // Added by Chathura on 7-oct-2017
        [OperationContract]
        List<BL_NUM_SEARCH> getBLNumberDf(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype);
        [OperationContract]
        List<BL_NUM_SEARCH_MBL> getBLNumberDfMBL(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype);

        // Added by Chathura on 7-oct-2017
        [OperationContract]
        List<FIELD_SEARCH_DF> getAllSearchDf(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string column);
        // Added by Chathura on 9-oct-2017
        [OperationContract]
        List<BL_NUM_SEARCH_DBL> getBLNumberDDf(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype, string pc);
        [OperationContract]
        List<MST_CUS_SEARCH_HEAD> getCustomerDetailsJobFiltered(string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string type, string ctype, string jobno);
        [OperationContract]
        List<FIELD_SEARCH_DF_HBL> getAllSearchDfHBL(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string column);

        [OperationContract]
        List<FIELD_SEARCH_DF_HBL> getAllSearchDfHBLJob(DateTime _fromDate, DateTime _toDate, string jobno, string spgeNum, string pgeSize, string searchFld, string searchVal, string column);

        [OperationContract]
        List<JOB_NUM_SEARCH> getJobPouchSearchDateFiltered(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string com);

        [OperationContract]
        List<JOB_NUM_SEARCH> GetJobNumberInClose(DateTime _fromDate, DateTime _toDate, string spgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);

        [OperationContract]
        List<SEARCH_INVOICE> getInvoiceNoDateFiltered(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);

        [OperationContract]
        List<SEARCH_INVOICE> getInvoiceNoDf(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);
        
        [OperationContract]
        List<SEARCH_INVOICE> getInvoiceNoDfApp(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);
        
        [OperationContract]
        List<SEARCH_INVOICE> getInvoiceNoDfNew(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc, string hbl);
        
        [OperationContract]
        List<SEARCH_INVOICE> getInvoiceNoDfNonPC(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);
        
        [OperationContract]
        List<SEARCH_INVOICE> getInvoiceNoDfNonPCApp(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc);
        
        [OperationContract]
        List<MST_COST_ELEMENT_SEARCH> getRevenueElements(string pgeNum, string pgeSize, string searchFld, string searchVal);

        [OperationContract]
        List<PAY_TP_SEARCH> getPaymentTypes(string pgeNum, string pgeSize, string searchFld, string searchVal);

        [OperationContract]
        List<USER_SEARCH> getUserDetails(string pgeNum, string pgeSize, string searchFld, string searchVal,string company);
 
        [OperationContract]
        List<MST_USERROLEID_SEARCH> getUserRoleID(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        [OperationContract]
        List<MST_USERS> getUserList(string pgeNum, string pgeSize, string searchFld, string searchVal);

        [OperationContract]
        List<MST_DEPARTMENT> getDeptList(string pgeNum, string pgeSize, string searchFld, string searchVal);

        [OperationContract]
        List<MST_DESIGNATION> getDesignationtList(string pgeNum, string pgeSize, string searchFld, string searchVal);

        [OperationContract]
        List<MST_COM_SEARCH> getCompanySet(string pgeNum, string pgeSize, string searchFld, string searchVal);
    }
}
