using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects;
using System.Data;
using System.Web;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Sales;

namespace FF.Interfaces
{
    [ServiceContract]
    public interface IGenaral
    {
        //Isuru 2017/05/25
        [OperationContract]
        List<MST_COUNTRY> getCustomerCountry();

        //Isuru 2017/05/26
        [OperationContract]
        List<cus_details> getCustomerPassPNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Isuru 2017/05/26
        [OperationContract]
        List<cus_details> getCustomerDLNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Isuru 2017/05/26
        [OperationContract]
        List<cus_details> getCustomerBRNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Isuru 2017/05/26
        [OperationContract]
        List<cus_details> getCustomerTelNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Isuru 2017/05/31
        [OperationContract]
        List<cus_details> getcustomernicno(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);

        //Isuru 2017/05/31
        [OperationContract]
        List<mst_bus_entity_tp> getcustomertype(string pgeNum, string pgeSize, string searchFld, string searchVal, string company);
        //subodana 2017/06/05
        [OperationContract]
        List<MainServices> GetMainServicesCodes();
        //subodana 2017-06-07
        [OperationContract]
        List<PendingServiceRequest> GetPendingJobRequse(string Com);
        //subodana 2017-06-19
        [OperationContract]
        List<cus_details> GetCustormerdata(string Com, string cuscode);
        //Dilshan 2017-09-11
        [OperationContract]
        List<cus_details> GetCountryTown(string Com, string concode);
        //Dilshan 04/09/2017
        [OperationContract]
        List<MST_EMP> GetEmployeedata(string Com, string cuscode);
        //Dilshan 06/09/2017
        [OperationContract]
        List<MST_VESSEL> GetVessaldata(string Com, string vslcode);
        //Dilshan 06/09/2017
        [OperationContract]
        List<MST_VESSEL> GetCostdata(string Com, string eleCode);
        //Dilshan 06/09/2017
        [OperationContract]
        List<MST_VESSEL> GetPortdata(string Com, string prtCode);
        //dilshan
        [OperationContract]
        List<Mst_empcate> Get_mst_empcate();

        [OperationContract]
        Int32 Check_Employeeepf(string epf);
        //Dilshan 06/08/2017
        [OperationContract]
        Int32 Check_vessal(string code);
        //Dilshan 06/08/2017
        [OperationContract]
        Int32 Check_costele(string code);
        //Dilshan 06/08/2017
        [OperationContract]
        Int32 Check_port(string code);
        //Sanjaya
        [OperationContract]
        cus_code_api SaveJobRequest(trn_req_hdr request, CustomerSearchObject customerObject, MasterAutoNumber customerNumber, MasterAutoNumber jobNumber, mst_busentity_grup busentityGrup, trn_req_serdet reqSerdet, trn_req_cus_det reqCusDet);
        //subodana
        [OperationContract]
        Int32 SaveJobDetails(trn_jb_hdr _hdr, List<trn_job_serdet> _jobdet, List<trn_jb_cus_det> _cusdet, MasterAutoNumber _masterAutoNumber, out string err);
          //subodana
        [OperationContract]
        int SaveJobDetailswithPendingRequest(List<PendingServiceRequest> _penging, trn_jb_hdr _hdr, MasterAutoNumber _masterAutoNumber, out string err);
        //subodana
        [OperationContract]
        int SaveBLDraftHdr(trn_bl_header _hdr, List<trn_bl_det> _bl_det, List<trn_bl_cont_det> _con_det, MasterAutoNumber _masterAutoNumber, out string err);
        //subodana
        [OperationContract]
        List<trn_bl_header> GetBLHdr(string doc, string com);
        //subodana
        [OperationContract]
        List<trn_bl_det> GetBLitemdetails(string seq);
        //subodana
        [OperationContract]
        List<trn_bl_cont_det> GetBLContainer(string seq);
        //subodana
        [OperationContract]
        List<trn_jb_hdr> GetJobHdr(string doc);
        //subodana
        [OperationContract]
        List<trn_job_serdet> GetJobServicesdetails(string seq);
        //subodana
        [OperationContract]
        List<trn_jb_cus_det> GetJobCustomerDetails(string seq);
        //subodana
        [OperationContract]
        List<BL_DOC_NO> GetBLDocNo(string type, string searchtype, string docno);
        //subodana
        [OperationContract]
        int SaveBLHousetHdr(trn_bl_header _hdr, List<trn_bl_det> _bl_det, List<trn_bl_cont_det> _con_det, MasterAutoNumber _masterAutoNumber, out string err);
        //subodana
        [OperationContract]
        int SaveBLMastertHdr(trn_bl_header _hdr, List<trn_bl_det> _bl_det, List<trn_bl_cont_det> _con_det, List<HouseBLAll> _houseblall, MasterAutoNumber _masterAutoNumber, out string err);
        //subodana
        [OperationContract] 
        List<GET_CUS_BASIC_DATA> GetCustormerBasicData(string code, string com, string type);
        //subodana
        [OperationContract]
        List<EntityType> GetJobEntity();
        //Nuwan 2017.07.06
        [OperationContract]
        List<MST_CUR> GetAllCurrency(string _currencyCode);
        //Nuwan 2017.07.12
        [OperationContract]
        TRN_MOD_MAX_APPLVL getMaxAppLvlPermission(string modcd,string com);
        //subodana 2017/07/13
        [OperationContract]
        List<MST_COUNTRY> getCountryDetails(string countryCd);
        //subodana 2017/07/13
        [OperationContract]
        DataTable Get_DetBy_town(string town);
        //subodana 2017/07/13
        [OperationContract]
        List<trn_req_cus_det> req_cus_data(string doc);
          //subodana 2017/07/13
        [OperationContract]
        List<PendingServiceRequest> req_all_data(string com, string doc);

        //Sanjaya De Silva 2017-07-13
        [OperationContract]
        List<trn_jb_hdr> GetCustomerJobs(string cus_code);

        [OperationContract]
        DataTable getSubChannelDet(string _company, string _code);

        [OperationContract]
        List<cus_invoices> getCustomerInvoices(string _cus_code);
        [OperationContract]
        List<PortAgentDet> getPortDetails(DateTime fromdate,DateTime todate,string company,out string error);
        [OperationContract]
        DataTable  getShipmentContainers(DateTime fromdate,DateTime todate,string company,string port, string agent);
        [OperationContract]
        List<BarChartDataPort> getPortTotal(DateTime frmdt, DateTime toDt, string company);
        [OperationContract]
        List<BarChartDataAgent> getAgentTotal(DateTime frmdt, DateTime toDt, string company);
         [OperationContract]
        List<trn_jb_hdr> GetJobHdrbypouch(string pouch);
        [OperationContract]
         List<MST_TITLE> GetTitleList();

        // Added by Chathura on 18-sep-2017
        [OperationContract]
        List<HBLSelectedData> GetHBLNumbersForMaster(string MBLno);
        // Added by Chathura on 3-oct-2017
        [OperationContract]
        List<BLData> LoadBLDetails(string BLno);
        [OperationContract]
        List<CustomerBasicData> GetCustomerBasicDetails(string cuscode);
        //Dilshan 2018-03-05
        [OperationContract]
        Int32 SaveReportErrorLog(string _erropt, string _errform, string _error, string _user);
        //dilshan
        [OperationContract]
        bool SendSms(string mobileNo, string message, string customerName, string sender, string company);

        [OperationContract]
        List<USER_ROLE> getUserRoleID(string company, string roleid);

        [OperationContract]
        List<UsersRoleData> getUserDetailsByRID(string company, string roleid);

        [OperationContract]
        List<ROLE_MENU_BRID> getMenuDetailsByRID(string userid, string company, string roleid);

        [OperationContract]
        List<OptionId_Details> getOptions(string company, string roleid);

        [OperationContract]
        int Save_Option_IDS(List<string> OptionStatList, string company, string roleid, List<string> OptionIdList, out string err);

        [OperationContract]
        List<USER_COMPANY_LIST> getUserListCom(string userid);

        [OperationContract]
        List<MST_SHIPMENTS> getShipmentList(string company);

        [OperationContract]
        int Update_Modeshipment_Details(string company, List<string> ModeList, string ShipDesc, List<string> StatList, string choice, out string err);

    }
}
