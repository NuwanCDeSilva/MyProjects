using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FF.BusinessObjects;
using System.Data;
using FF.BusinessObjects.Services;
using FF.BusinessObjects.Asycuda;
using FF.BusinessObjects.Account;

namespace FF.Interfaces
{

    [ServiceContract]
    public interface ISecurity
    {
        //Chamal 09-02-2013 
        [OperationContract]
        DateTime GetServerDateTime();

        //Chamal 02-Jul-2014 
        [OperationContract]
        string GetServerTimeZoneOffset();

        #region System Roles
        [OperationContract]
        List<SystemRole> GetALLSystemRolesData();

        [OperationContract]
        SystemRole GetSystemRoleByRoleData(SystemRole _userRole);

        [OperationContract]
        Int32 UpdateSystemUserRole(SystemRole _userRole);

        [OperationContract]
        List<SystemUserRole> GetSystemUsersByRoleData(SystemRole _systemRole);

        #endregion

        #region System Role Options

        [OperationContract]
        SystemRoleOption GetCurrentSystemOptionsDataByRole(SystemRole _inputsystemRole);

        [OperationContract]
        int SaveSelectedSystemOptionsRolePrivillages(SystemRoleOption _systemRoleOption);

        //kapila 21/3/2012
        [OperationContract]
        SystemRole GetRoleByCode(string _CompCode, int _RoleID);

        #endregion

        #region System Option Registration
        //
        // Function            - System Option Registration
        // Function Wriiten By - P.Wijetunge
        // Date                - 29/02/2012
        //

        /// <summary>
        /// Get All System Option
        /// </summary>
        /// <returns>List of System Options</returns>
        [OperationContract]
        List<SystemOption> GetAllSystemOptions();

        [OperationContract]
        List<SystemOption> GetUserSystemOptions(string user);

        [OperationContract]
        Int16? CheckSystemOptionOganizePosition(Int16 _parentID, Int16 _newOganizePosition);

        [OperationContract]
        Int32? GetMaxOptionID();

        [OperationContract]
        Int32 UpdateSystemOption(SystemOption _opt, string _user, string _sessionID);

        #endregion

        #region System Users

        [OperationContract]
        List<SystemUser> GetSystemUsers();

        [OperationContract]
        SystemUser GetUserByUserID(string UserID);

        [OperationContract]
        List<SystemUserCompany> GetUserCompany(string UserComp);

        [OperationContract]
        SystemUserCompany GetAssignedUserCompany(string UserComp, string UserID);

        [OperationContract]
        bool CheckValidADUser(string UserID, out SystemUser UserInfor);

        [OperationContract]
        List<SystemUserRole> GetUserRole(string UserID);

        [OperationContract]
        List<SystemUserLoc> GetUserLoc(string UserID);

        [OperationContract]//Chamal 20-03-2012
        List<SystemUserProf> GetUserProfCenters(string UserID, string Comp);

        [OperationContract(Name = "AddUserIdCompany")]//Chamal 20-03-2012
        List<SystemUserLoc> GetUserLoc(string UserID, string Comp);

        [OperationContract]
        int UpdateSystemUserCompany(SystemUserCompany _UsrComp);

        [OperationContract]
        int UpdateSystemUserLocation(SystemUserLoc _UsrLoc);

        [OperationContract]
        int UpdateSystemUserPC(SystemUserProf _userPC);

        [OperationContract]
        int SaveSystemUserRole(SystemUserRole _UsrRole);

        [OperationContract]
        List<SystemRole> GetRoleByCompany(string _Comp, int? _isActive);

        [OperationContract]
        int DeleteUserLoc(string UserID, string Comp, string Loc);

        [OperationContract]
        int DeleteUserPC(string UserID, string Comp, string PC);

        [OperationContract]
        int DeleteUserRole(string UserID, string Comp, int RoleID);

        [OperationContract]
        int DeleteUserComp(string UserID, string Comp);

        [OperationContract]
        double SaveLoginSession(string _userId, string _com, string _userIp, string _userPc, string _winLogName, string _winUser);

        //Chamal 18-05-2013
        [OperationContract]
        int ExitLoginSession(string UserID, string Comp, string SessionID);

        //Dulanga 09-02-2013
        [OperationContract]
        DataTable getActiveSessionInfo(string UserID, string Com);

        //Chamal 25-06-2013
        [OperationContract]
        bool IsActiveSessions(string UserID, string Comp, out string _ip, out string _pc, out string _lastlogdate);

        //Chamal 25-06-2013
        [OperationContract]
        bool IsSessionExpired(string _sessionID, string _userID, string _comp, out string _msg);

        //kapila 23/3/2012
        [OperationContract]
        SystemUserLoc GetAssignedUserLocation(string _UserID, string _Comp, string _Loc);

        //kapila 24/3/2012
        [OperationContract]
        Int16 Check_User_Def_Comp(string _userID);

        //kapila 26/3/2012
        [OperationContract]
        Int16 Check_User_Def_Loc(string _userID, string _com);

        [OperationContract]
        Int16 Check_User_Def_PC(string _userID, string _com);
        #endregion

        #region System User Creation

        [OperationContract]
        int SaveNewUser(SystemUser _UsrNew);

        #endregion

        #region System User Update

        [OperationContract]
        int UpdateUser(SystemUser _UsrNew);

        #endregion

        #region User Request Approval Permission

        [OperationContract]
        UserRequestApprovePermission GetUserRequestApprovalPermissionDataForLocation(string _userId, string _reqSubType, string _companyCode, string _locationCode, string _category, string _type);

        [OperationContract]
        UserRequestApprovePermission GetUserRequestApprovalPermissionDataForProfitCenter(string _userId, string _reqSubType, string _companyCode, string _locationCode, string _category, string _type);


        [OperationContract]
        RequestApproveCycleDefinition GetApproveCycleDefinitionDataforLocation(string _sourceCompanyCode, string _sourceLocCode, string _reqSubType, DateTime _transactionDate, string _destinationCompanyCode, string _destinationLocCode, string _category, string _type);

        [OperationContract]
        RequestApproveCycleDefinition GetApproveCycleDefinitionDataforProfitCenter(string _sourceCompanyCode, string _sourceLocCode, string _reqSubType, DateTime _transactionDate, string _destinationCompanyCode, string _destinationLocCode, string _category, string _type);

        #endregion

        //Written by Prabhath on 05/05/2012
        [OperationContract]
        DataTable GetUserLocTable(string _userID, string _company, string _location);

        //Written by Prabhath on 05/05/2012
        [OperationContract]
        List<SystemUserLoc> GetUserLocList(string _userID, string _company, string _location);

        [OperationContract]
        List<SystemUserProf> GetUserPC(string _UserID);

        //kapila 24/1/2013
        [OperationContract]
        Int16 Check_User_PC(string _userID, string _com, string _pc);

        [OperationContract]
        Int16 Check_User_Loc(string _userID, string _com, string _loc);

        //Written by Chamal on 23/01/2013
        [OperationContract]
        DataTable GetUserSystemMenus(string _user, string _company);

        //Written by Chamal on 07/02/2013
        [OperationContract]
        String GetCurrentVersion();

        //Written by Chamal on 18/03/2013
        [OperationContract]
        int SaveSystemMenu(SystemMenus _menu);

        #region user maintain
        //Written by  Shani 24/08/2012
        [OperationContract]
        Int32 User_Maintain(SystemUser _user, string action);
        #endregion

        #region System Option permissions for user
        //Written by Shani 26/02/2013
        [OperationContract]
        Boolean Is_OptionPerimitted(string userCompany, string userId, Int32 optionCode);

        #endregion


        #region Back Date Function
        //Written by  Shani 07/03/2013
        [OperationContract]
        DataTable Get_childMenus(string _type, string _parentMenuName);

        //Written by  Shani 07/03/2013
        [OperationContract]
        DataTable Get_Menu(string MenuName, out List<SystemMenus> list);

        #endregion Back Date Function

        //Written By Prabhath on 06/04/2013
        [OperationContract]
        DataTable GetUnReadMessages(string _receiver, string _company, string _location, string _profitcenter);
        //Written By Prabhath on 06/04/2013
        [OperationContract]
        Int32 UpdateViewedMessage(string _document);
        //Written By Prabhath on 06/04/2013
        [OperationContract]
        DataTable GetUserMessageType(string _receiver, string _location, string _profitcenter);

        //Shani on 17-05-2013
        [OperationContract]
        SystemRole GetSystemRole_ByRoleData(SystemRole _sysRole);

        //Shani on 17-05-2013
        [OperationContract]
        int SaveSelectedSystemOptionsRolePrivillages_NEW(SystemRoleOption _systemRoleOption);

        //Shani on 17-05-2013
        [OperationContract]
        DataTable Get_SystemOptionsData_ByRoleID(string com, Int32 roleId);

        //Shani on 18-05-2013
        [OperationContract]
        DataTable Get_MenusForRole(string com, Int32 roleId);

        //Shani on 20-05-2013
        [OperationContract]
        DataTable Get_UsersForRole(string com, Int32 roleId);

        //Shani on 20-05-2013
        [OperationContract]
        Int32 UpdateSystemUserRole_NEW(SystemRole _userRole);

        //Shani on 21-05-2013
        [OperationContract]
        DataTable Get_SystemOptionsForGroup(string groupID);

        //Shani on 21-05-2013
        [OperationContract]
        int Save_System_Options_For_Role(SystemRoleOption _systemRoleOption, string OptGroupID);

        //Shani on 22-05-2013
        [OperationContract]
        DataTable Get_Active_System_OptionsFor_Role(string company, Int32 roleId);

        //Shani on 23-05-2013
        [OperationContract]
        DataTable Get_OptionGroupDetail(string groupID);

        //Shani on 17-06-2013
        [OperationContract]
        DataTable GetUser_Company(string _UserID);

        //Shani on 17-06-2013
        [OperationContract]
        DataTable Get_SpecialUser_Perm(string _UserID);

        //Shani on 18-06-2013
        [OperationContract]
        int Save_SecUserPerm(SecUserPerm _perm);

        //Shani on 18-06-2013
        [OperationContract]
        int Inactivate_SecUserPerm(List<SecUserPerm> _usrPermList);

        //Shani on 18-06-2013
        [OperationContract]
        DataTable GetUserLocations(string UserID, string Comp);

        //Shani on 18-06-2013
        [OperationContract]
        int UpdateSystemUserLoc_NEW(List<SystemUserLoc> _usrLoc_LIST);

        //Shani on 19-06-2013
        [OperationContract]
        int UpdateSystemUserPC_NEW(List<SystemUserProf> _userPC_LIST);

        //Shani on 19-06-2013
        [OperationContract]
        DataTable GetAllUserPC(string _UserID);

        //Shani on 19-06-2013
        [OperationContract]
        DataTable Get_gen_doc_pro_hdr(string userID, string receiptNo, string invoiceNo, string engineNo, string chassisNo);

        //Shani on 19-06-2013
        [OperationContract]
        int DeleteUserLoc_NEW(List<SystemUserLoc> DEL_LIST);

        //Shani on 19-06-2013
        [OperationContract]
        int DeleteUserPC_NEW(List<SystemUserProf> DEL_LIST);

        //Shani on 19-06-2013
        [OperationContract]
        SystemRole GetSystemRoleByRoleData_new(SystemRole _userRole);

        //Shani on 12-07-2013
        [OperationContract]
        int Save_Sec_App_Usr_Prem(SecApproveUserPerm _secApprUserPerm);

        //Shani on 12-07-2013
        [OperationContract]
        DataTable Get_Approve_PermissionInfo(string _ApprPermCd);

        //Shani on 13-07-2013
        [OperationContract]
        DataTable Get_UserApprove_Permissions(string userID, string permLvlCode);

        //Shani on 15-07-2013
        [OperationContract]
        int SaveSystemUserRole_NEW(SystemUserRole _usrRole);

        //Chamal on 06-Jun-2014
        [OperationContract]
        int DeleteSystemUserRole_NEW(SystemUserRole _usrRole);

        //Shani on 15-07-2013
        [OperationContract]
        DataTable GetUserRole_NEW(string UserID);

        //Shani on 15-07-2013
        [OperationContract]
        int DeleteUserRole_NEW(string UserID, string Comp, int RoleID);

        //Shani on 03-08-2013
        [OperationContract]
        int Save_sec_role_loc(List<SecRoleLocation> _RoleLocList);

        //Shani on 03-08-2013
        [OperationContract]
        int Save_sec_role_LocChanel(List<SecRoleLocChanel> _secLocChnlList);

        //Shani on 03-08-2013
        [OperationContract]
        int Save_sec_role_pc(List<SecRolePC> _secRolePCList);

        //Shani on 03-08-2013
        [OperationContract]
        int Save_sec_role_pcchnl(List<SecRolePcChannel> _secPcChnlList);

        //Shani on 05-08-2013
        [OperationContract]
        int Update_sec_role_loc(List<SecRoleLocation> _RoleLocList);

        //Shani on 05-08-2013
        [OperationContract]
        int Update_sec_role_locChannel(List<SecRoleLocChanel> _RoleLocChannelList);

        //Shani on 05-08-2013
        [OperationContract]
        int Update_sec_role_PC(List<SecRolePC> _RolePc);

        //Shani on 05-08-2013
        [OperationContract]
        int Update_secRolePcChannel(List<SecRolePcChannel> _RolePcChannelList);

        //Shani on 05-08-2013
        [OperationContract]
        DataTable Get_Sec_role_loc(string com, Int32 roleID, string loc);

        //Shani on 05-08-2013
        [OperationContract]
        DataTable Get_Sec_role_locChannel(string com, Int32 roleID, string channel);

        //Shani on 05-08-2013
        [OperationContract]
        DataTable Get_Sec_role_pc(string com, Int32 roleID, string pc);

        //Shani on 05-08-2013
        [OperationContract]
        DataTable Get_Sec_role_pcChannel(string com, Int32 roleID, string channel);

        //sachith on 28-08-2013
        [OperationContract]
        DataTable GetCompanyUserRole(string _user, string _com);

        //Chamal on 16-Sep-2013
        [OperationContract]
        int Change_Password(SystemUser _user);

        //Chamal on 23-Sep-2013
        [OperationContract]
        SecurityPolicy GetSecurityPolicy(int _seqNo, out string _err);

        //Chamal on 30-Sep-2013
        [OperationContract]
        LoginUser LoginToSystem(string _userid, string _pw, string _com, string _verNo, string _ipAddress, string _hostName, int _inAttempts, out int _outAttempts, out int _status, out string _msg, out string _msgTitle);

        //Chamal on 31-Dec-2013
        [OperationContract]
        bool ValidateUserID(string _userID, string _userPw);

        //Chamal on 06-Jan-2014
        [OperationContract]
        bool CheckPasswordPolicy(string _user, string _pw, out string _err);

        //Chamal on 06-Jan-2014
        [OperationContract]
        int Save_User_Falis(string _user, string _pw, string _com, string _ip, string _winusername, string _winuser);

        //Sanjeewa on 17-06-2014
        [OperationContract]
        DataTable Get_SystemMenu();

        //Sanjeewa on 17-06-2014
        [OperationContract]
        DataTable Get_SystemUsers(string _user, string _dept);

        //Sanjeewa on 17-06-2014
        [OperationContract]
        DataTable Get_SystemUserLoc(string _user, string _dept, string _type);

        //Sanjeewa on 17-06-2014
        [OperationContract]
        DataTable Get_SystemRole();

        //Sanjeewa on 17-06-2014
        [OperationContract]
        DataTable Get_SystemMenuAssgnRole();

        //Sanjeewa on 17-06-2014
        [OperationContract]
        DataTable Get_SystemSpecialPerm();

        //Sanjeewa on 17-06-2014
        [OperationContract]
        DataTable Get_SystemRoleAssgnUser(string _user);

        //Chamal 07-Apr-2015
        [OperationContract]
        void Add_User_Selected_Loc_Pc_DR(string _userid, string _com, string _pc, string _loc, List<string> _list);

        //Tharka 2015-05-13
        [OperationContract]
        List<Main_menu_items> GetUserSystemMenusNew(string _user, string _company);

        //Sahan 16 Jun 2015
        [OperationContract]
        DataTable SP_SCM2_GET_USRPW_STATUS(string P_USER);

        //Rukshan 04/Aug/2015
        [OperationContract]
        DataTable GetSBU_Company(string P_COM, string P_SBU);

        //Rukshan 04/Aug/2015
        [OperationContract]
        int Save_User_SBU(StrategicBusinessUnits _StrategicBusinessUnits);

        //Rukshan 04/Aug/2015
        [OperationContract]
        DataTable GetSBU_User(string P_COM, string P_UID, string _SBU);

        //Nuwan 21/01/2016
        [OperationContract]
        int CheckUserAvailability(string userId, string email);

        //Nuwan 21/01/2016
        [OperationContract]
        int SendPasswordResetEmail(string email, string id, string message, string host);

        //Nuwan 22/01/2016
        [OperationContract]
        bool CheckPwResetAuth(string secTOkecn, string id);

        //Nuwan  22/01/2016
        [OperationContract]
        bool UpdateUserPassword(string password, string secToken, string id, string message, string host);

        [OperationContract]
        //bool CheckValidADUserForVNC(string UserID, string password);
        bool CheckValidADUserForVNC(string UserID, string password, string domainName, string sunServer, out string _feedback);
        //Lakshan 10 Dec 2016
        [OperationContract]
        int SendCostSheetEmail(string email, string id, string message, string host);

        #region asycuda
        [OperationContract]
        List<ASY_DB_SOURCE> GetSystemDatabaseList();
        [OperationContract]
        List<ASY_DOC_GRUP> GetAsycudaGrpList();

        [OperationContract]
        List<ASY_DOC_TP> GetAsycudaTypeList(string group, string database);

        [OperationContract]
        ASY_HEADER_DTLS GetAsycudaHederDetails(string docno, int datasrcid, string doctype);

        [OperationContract]
        ASY_DB_SOURCE GetDataSourceDetails(int sourceId);

        [OperationContract]
        List<ASY_CUSDEC_ITEM_DTLS_MODEL> GetAsycudaItemsListDetails(string documentNo, int dbsrcId, string asyType);

        [OperationContract]
        string getAsyTypeCodefromId(int typeid, int grpId);

        [OperationContract]
        List<ASY_XML_TAG> getXmlTagList(decimal parentid);

        [OperationContract]
        List<ASY_XML_TAG> getXmlTagListForItems(decimal parentid);

        [OperationContract]
        ASY_ALT_HEADER_DTLS SetAlterHeaderDetails(string docno, int datasrcid, string doctype, out string error);

        [OperationContract]
        bool CheckDocumentAvailability(string documentNo, int dbsrcId, string docType, out string error);

        [OperationContract]
        List<ASY_CUSDEC_ITM> GetItems(string docno, int dbsrcId, string asyType, ASY_ALT_HEADER_DTLS _altDet, out string error);

        [OperationContract]
        string getDocumentXml(string type);

        [OperationContract]
        List<ASY_DOC_SEARCH_HEAD> getDocumentDetails(int dbsrc, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //[OperationContract]
        //int getDocumentDetailscCount(int dbsrc, string searchFld, string searchVal);
        [OperationContract]
        ASY_IMP_CUSDEC_HDR GetCusdecHdrDetails(string docNo, int datasrcid);
        [OperationContract]
        decimal getTotalForms(decimal totalQty);
        [OperationContract]
        string getTermsOfPaymentDescription(string termsofpay);
        [OperationContract]
        MST_UOM_CATE getPackageDetailsforcode(string pkgCode);
        [OperationContract]
        string GetOfficeOfEntryDescription(string OfficeOfEntryCode);
        [OperationContract]
        string GetBankName(string bnkcode);
        #endregion asycuda

        //Chamal 22-Feb-2017
        [OperationContract]
        DataTable GetUserLastLogTrans(string _com, string _userid, int _isFirst);

        //Dulaj 24-Jan-2018
        [OperationContract]
        Int32 CheckMenuPermission(string _user, string _company, string _url);
        //Nuwan 2018.03.10
        [OperationContract]
        DataTable getCurrencyBreakDown(string docno);

        //Dulaj 06-Feb-2018
        [OperationContract]
        Int32 CheckUrl(string _url);
        //Nuwan 2018.06.29
        [OperationContract]
        Int32 getUserSpecialPermission(string userid, string company, string permcd, out string error);

        //Dulaj 2018/Jul/09
        [OperationContract]
        DataTable GetDocNoListForAsyCuda(string docno01, string docno02, string type);

        //Dulaj 2018/Jul/10
        [OperationContract]
        string GetMasterCompanyPath(string company);

        //Chamal 2018/Jul/19
        [OperationContract]
        int ClearSunSession(string _sunID, string _sunDB, out string _outMsg);
        
        //Chamal 2018/Jul/23
        [OperationContract]
        bool IsLoginSunRemote(string _sunClientIP);
        //Nuwan 2018.09.18
        [OperationContract]
        List<SEC_SYSTEM_MENU> getSystemUserMenu(string userid, string company, string system);
    }
}
