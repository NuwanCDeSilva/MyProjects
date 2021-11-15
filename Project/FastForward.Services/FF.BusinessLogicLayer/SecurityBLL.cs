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
using System.Transactions;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using FF.BusinessObjects.Services;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using FF.BusinessObjects.Asycuda;
using FF.BusinessObjects.Account;

namespace FF.BusinessLogicLayer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SecurityBLL : ISecurity
    {
        SecurityDAL _securityDAL = null;
        InventoryDAL _inventoryDAL = null;
        SalesDAL _salesDAL = null;
        ReptCommonDAL _reptCommonDAL = null;
        GeneralDAL _gneralDAL = null;
        public AsycudaDAL _asycudaDAL = null;
        public FinancialDAL _financialDAL = null;
        //Chamal 09-02-2013
        public DateTime GetServerDateTime()
        {
            return DateTime.Now;
        }

        public string GetServerTimeZoneOffset()
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);
            return offset.ToString();
        }


        #region System Roles

        public List<SystemRole> GetALLSystemRolesData()
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetALLSystemRolesData();
        }

        public SystemRole GetSystemRoleByRoleData(SystemRole _userRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetSystemRoleByRoleData(_userRole);
        }

        public Int32 UpdateSystemUserRole(SystemRole _userRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.UpdateSystemUserRole(_userRole);
        }
        public Int32 UpdateSystemUserRole_NEW(SystemRole _userRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.UpdateSystemUserRole_NEW(_userRole);
        }
        public List<SystemUserRole> GetSystemUsersByRoleData(SystemRole _systemRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetSystemUsersByRoleData(_systemRole);
        }

        //kapila 21/3/2012
        public SystemRole GetRoleByCode(string _CompCode, int _RoleID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetRoleByRoleCode(_CompCode, _RoleID);
        }

        #endregion

        #region System Role Options

        public SystemRoleOption GetCurrentSystemOptionsDataByRole(SystemRole _inputsystemRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetCurrentSystemOptionsDataByRole(_inputsystemRole);
        }

        public int SaveSelectedSystemOptionsRolePrivillages(SystemRoleOption _systemRoleOption)
        {
            _securityDAL = new SecurityDAL();
            int result = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                _securityDAL.ConnectionOpen();
                //Delete and Loged the existing role privillages.
                result = _securityDAL.LogCurrentSystemRoleOptions(_systemRoleOption);

                //If there is a new permission set add it to table.
                if ((_systemRoleOption.SystemOptionList != null) && (_systemRoleOption.SystemOptionList.Count > 0))
                {
                    foreach (SystemOption item in _systemRoleOption.SystemOptionList)
                    {
                        _systemRoleOption.SystemOption = item;
                        int val = _securityDAL.SaveCurrentSystemRoleOptins(_systemRoleOption);
                    }

                }
                result = 1;
                _securityDAL.ConnectionClose();
                scope.Complete();
            }

            return result;

        }

        #endregion

        #region System User
        public List<SystemUser> GetSystemUsers()
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetSystemUsers();
        }

        public SystemUser GetUserByUserID(string _UserID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserByUserID(_UserID);
        }

        //Add Chamal 31/Dec/2013
        public bool ValidateUserID(string _userID, string _userPw)
        {
            _securityDAL = new SecurityDAL();
            SystemUser _systemUser = _securityDAL.GetUserByUserID(_userID);
            if (_systemUser != null)
            {
                if (_userID.ToUpper().ToString() == _systemUser.Se_usr_id.ToUpper().ToString() && _userPw.ToString() == _systemUser.Se_usr_pw.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<SystemUserCompany> GetUserCompany(string _usrComp)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserCompany(_usrComp);
        }

        //kapila 22/3/2012
        public SystemUserCompany GetAssignedUserCompany(string UserComp, string UserID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetAssignedUserCompany(UserComp, UserID);
        }
        //Validate the login or creating User ID was existing/ in active directory :: Chamal 03-Mar-2012
        public bool CheckValidADUser(string UserID, out SystemUser UserInfor)
        {
            _securityDAL = new SecurityDAL();
            string strRootDN = string.Empty;
            string strLDAPPath = string.Empty;
            string strDomainname = string.Empty;
            string strAuthondiuser = string.Empty;
            string strAuthondiuserpw = string.Empty;

            DirectoryEntry objDseSearchRoot = null, objDseUserEntry = null;
            DirectorySearcher objDseSearcher = null;
            SearchResultCollection objResults = null;

            UserInfor = new SystemUser();

            //try
            //{
            //strLDAPPath = _securityDAL.GetADConnectionString();
            //strDomainname = _securityDAL.GetADDomainName();
            //strAuthondiuser = _securityDAL.GetADAuthenticateUser();
            //strAuthondiuserpw = _securityDAL.GetADAuthenticateUserPw();

            //NetworkCredential _objNetWorkC = new NetworkCredential(strAuthondiuser, strAuthondiuserpw, strDomainname);

            //objDseSearchRoot = new DirectoryEntry(strLDAPPath, strDomainname + "\\" + strAuthondiuser, strAuthondiuserpw, AuthenticationTypes.None);
            objDseSearchRoot = new DirectoryEntry(_securityDAL.GetADConnectionString(), _securityDAL.GetADDomainName() + "\\" + _securityDAL.GetADAuthenticateUser(), _securityDAL.GetADAuthenticateUserPw(), AuthenticationTypes.None);
            strRootDN = objDseSearchRoot.Properties["defaultNamingContext"].Value as string;
            objDseSearcher = new DirectorySearcher(objDseSearchRoot);
            objDseSearcher.CacheResults = false;
            objDseSearcher.Filter = String.Format("(&(objectClass=user)(sAMAccountName={0}))", UserID);
            objResults = objDseSearcher.FindAll();
            if (objResults.Count > 0)
            {
                objDseUserEntry = objResults[0].GetDirectoryEntry();
                if (objDseUserEntry.Properties.Contains("Name")) { UserInfor.Ad_full_name = objDseUserEntry.Properties["Name"][0].ToString(); }
                else { UserInfor.Ad_full_name = ""; }
                if (objDseUserEntry.Properties.Contains("Title")) { UserInfor.Ad_title = objDseUserEntry.Properties["Title"][0].ToString(); }
                else { UserInfor.Ad_title = ""; }
                if (objDseUserEntry.Properties.Contains("Department")) { UserInfor.Ad_department = objDseUserEntry.Properties["Department"][0].ToString(); }
                else { UserInfor.Ad_department = ""; }
            }

            if (objDseUserEntry == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            //}

            //catch (Exception e)
            //{
            //    return false;
            //}
            //finally
            //{
            //    //Dipose Object Over Here
            //}

            //return true;
        }

        //Check login user password is expired :: Chamal 23-05-2012
        public bool CheckPasswordIsExpired(string UserID)
        {
            _securityDAL = new SecurityDAL();

            return true;
        }

        public List<SystemUserRole> GetUserRole(string UserID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserRole(UserID);
        }

        public List<SystemUserLoc> GetUserLoc(string _UserID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserLoc(_UserID);
        }

        public int UpdateSystemUserCompany(SystemUserCompany _usrComp)
        {

            _securityDAL = new SecurityDAL();
            return _securityDAL.UpdateSystemUserCompany(_usrComp);

        }

        public int UpdateSystemUserPC(SystemUserProf _userPC)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.UpdateSystemUserPC(_userPC);
        }

        public int UpdateSystemUserLocation(SystemUserLoc _usrLoc)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.UpdateSystemUserLoc(_usrLoc);
        }

        public int SaveSystemUserRole(SystemUserRole _usrRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.SaveSystemUserRole(_usrRole);
        }


        public List<SystemRole> GetRoleByCompany(string _Comp, int? _isActive)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetRoleByCompany(_Comp, _isActive);
        }

        public int DeleteUserLoc(string UserID, string Comp, string Loc)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.DeleteUserLoc(UserID, Comp, Loc);
        }

        public int DeleteUserPC(string UserID, string Comp, string PC)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.DeleteUserPC(UserID, Comp, PC);
        }

        public int DeleteUserRole(string UserID, string Comp, int RoleID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.DeleteUserRole(UserID, Comp, RoleID);
        }

        public int DeleteUserComp(string UserID, string Comp)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.DeleteUserComp(UserID, Comp);
        }

        public Int32 SaveNewUser(SystemUser _UsrNew)
        {
            Int32 _effect = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _effect = _securityDAL.SaveNewUser(_UsrNew);
            _securityDAL.ConnectionClose();
            return _effect;
        }

        public Int32 UpdateUser(SystemUser _UsrNew)
        {
            Int32 _effect = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _effect = _securityDAL.UpdateUser(_UsrNew);
            _securityDAL.ConnectionClose();
            return _effect;
        }

        //Chamal 19-03-2012
        public double SaveLoginSession(string _userId, string _com, string _userIp, string _userPc, string _winLogName, string _winUser)
        {
            double _SessionId = 0;
            //using (TransactionScope scope = new TransactionScope())
            //{
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _SessionId = _securityDAL.SaveLoginSession(_userId, _com, _userIp, _userPc, _winLogName, _winUser);
            _securityDAL.ConnectionClose();
            //    scope.Complete();
            //}
            return _SessionId;
        }

        //Chamal 18-05-2013
        public int ExitLoginSession(string UserID, string Comp, string SessionID)
        {
            int _eff = 0;
            if (SessionID != null)
            {
                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                _eff = _securityDAL.ExitLoginSession(UserID, Comp, SessionID);
                _securityDAL.ConnectionClose();
            }
            return _eff;
        }

        /// <summary>
        /// get All active Session by Dulanga 2017-2-9
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Com"></param>
        /// <returns></returns>
        public DataTable getActiveSessionInfo(string UserID, string Com)
        {
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            DataTable dt = _securityDAL.getActiveSessionInfo(UserID, Com);
            _securityDAL.ConnectionOpen();
            return dt;
        }

        public bool IsActiveSessions(string UserID, string Comp, out string _ip, out string _pc, out string _lastlogdate)
        {
            bool _isTrue = false;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _isTrue = _securityDAL.IsActiveSessions(UserID, Comp, out  _ip, out  _pc, out  _lastlogdate);
            _securityDAL.ConnectionClose();
            return _isTrue;
        }

        public Boolean IsSessionExpired(string _sessionID, string _userID, string _comp, out string _msg)
        {
            bool _isTrue = false;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            string _msgrtn = string.Empty;
            _isTrue = _securityDAL.IsSessionExpired(_sessionID, _userID, _comp, out _msgrtn);
            _securityDAL.ConnectionClose();
            _msg = _msgrtn;
            return _isTrue;
        }

        //Chamal 20-03-2012
        public List<SystemUserProf> GetUserProfCenters(string UserID, string Comp)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserProfCenters(UserID, Comp);
        }

        //Chamal 20-03-2012
        //public List<SystemUserProf> GetUserLoc(string UserID, string Comp)
        //{
        //    _securityDAL = new SecurityDAL();
        //    return _securityDAL.GetUserLoc(UserID, Comp);
        //}

        //kapila 23/3/2012
        public SystemUserLoc GetAssignedUserLocation(string _UserID, string _Comp, string _Loc)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetAssignedUserLocation(_UserID, _Comp, _Loc);
        }

        //kapila 24/3/2012
        public Int16 Check_User_Def_Comp(string _userID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Check_User_Def_Comp(_userID);
        }

        //kapila 26/3/2012
        public Int16 Check_User_Def_Loc(string _userID, string _com)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Check_User_Def_Loc(_userID, _com);
        }

        public Int16 Check_User_Def_PC(string _userID, string _com)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Check_User_Def_PC(_userID, _com);
        }
        public Int16 Check_User_PC(string _userID, string _com, string _pc)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Check_User_PC(_userID, _com, _pc);
        }
        public Int16 Check_User_Loc(string _userID, string _com, string _loc)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Check_User_Loc(_userID, _com, _loc);
        }

        #endregion

        #region System Option Registration
        //
        // Function            - System Option Registration
        // Function Wriiten By - P.Wijetunge
        // Date                - 29/02/2012
        //

        /// <summary>
        /// Get All The System Options
        /// </summary>
        /// <returns>List of System Option</returns>
        public List<SystemOption> GetAllSystemOptions()
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetAllSystemOption();
        }

        // Chamal 11/05/2012
        public List<SystemOption> GetUserSystemOptions(string _user)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserSystemOption(_user);
        }

        public Int16? CheckSystemOptionOganizePosition(Int16 _parentID, Int16 _newOganizePosition)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.CheckSystemOptionOganizePosition(_parentID, _newOganizePosition);
        }

        public Int32? GetMaxOptionID()
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetMaxOptionID();
        }

        public Int32 UpdateSystemOption(SystemOption _opt, string _user, string _sessionID)
        {
            Int32 _effect = 0;
            using (TransactionScope tr = new TransactionScope())
            {
                _securityDAL = new SecurityDAL();
                _effect = _securityDAL.UpdateSystemOption(_opt, _user, _sessionID);
                tr.Complete();
            }
            return _effect;

        }

        #endregion

        List<SystemUserLoc> ISecurity.GetUserLoc(string UserID, string Comp)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserLoc(UserID, Comp);
        }

        #region User Request Approval Permission


        /// <summary>
        /// This is a commom method used in Approve Cycle,to get user request approval permission for paticular function.Permission can have 
        /// either location wise or user wise.
        /// </summary>
        /// <param name="_userId">Logged UserId</param>
        /// <param name="_reqSubType">RequestSubType</param>
        /// <param name="_companyCode">Used to get type hierachy</param>
        /// <param name="_locationCode">Used to get type hierachy</param>
        /// <returns></returns>
        public UserRequestApprovePermission GetUserRequestApprovalPermissionDataForLocation(string _userId, string _reqSubType, string _companyCode, string _locationCode, string _category, string _type)
        {
            UserRequestApprovePermission _userRequestApprovePermission = null;
            _securityDAL = new SecurityDAL();

            //Get Type Collection & loop it with GetUserLocationRequestApprovalPermission.(Ex:- Type = LOCATION,AREA,REGION.......,COMPANY)
            List<PriorityHierarchy> _typePriorityHierarchyList = _securityDAL.GetPriorityHierarchy(_companyCode, _locationCode, _category, _type);

            if ((_typePriorityHierarchyList != null) && (_typePriorityHierarchyList.Count > 0))
            {
                foreach (PriorityHierarchy _item in _typePriorityHierarchyList)
                {
                    //Get user request approval permission by location wise.
                    _userRequestApprovePermission = _securityDAL.GetUserLocationRequestApprovalPermission(_userId, _reqSubType, _item.HierarchyItemName, _item.HierarchyItemValue);

                    if (_userRequestApprovePermission != null)
                        break;
                }
            }

            //If location wise approval permission does not exists,check for user wise permission.
            if (_userRequestApprovePermission == null)
            {
                _userRequestApprovePermission = _securityDAL.GetUserRequestApprovalPermission(_userId, _reqSubType);
            }

            return _userRequestApprovePermission;
        }


        /// <summary>
        /// This is a commom method used in Approve Cycle,to get user request approval permission for paticular function.Permission can have 
        /// either location wise or user wise.
        /// </summary>
        /// <param name="_userId">Logged UserId</param>
        /// <param name="_reqSubType">RequestSubType</param>
        /// <param name="_companyCode">Used to get type hierachy</param>
        /// <param name="_locationCode">Used to get type hierachy</param>
        /// <returns></returns>
        public UserRequestApprovePermission GetUserRequestApprovalPermissionDataForProfitCenter(string _userId, string _reqSubType, string _companyCode, string _locationCode, string _category, string _type)
        {
            UserRequestApprovePermission _userRequestApprovePermission = null;
            _securityDAL = new SecurityDAL();
            _salesDAL = new SalesDAL();

            //Get Type Collection & loop it with GetUserLocationRequestApprovalPermission.(Ex:- Type = LOCATION,AREA,REGION.......,COMPANY)
            List<MasterSalesPriorityHierarchy> _typePriorityHierarchyList = _salesDAL.GetSalesPriorityHierarchy(_companyCode, _locationCode, _category, _type);

            if ((_typePriorityHierarchyList != null) && (_typePriorityHierarchyList.Count > 0))
            {
                foreach (MasterSalesPriorityHierarchy _item in _typePriorityHierarchyList)
                {
                    //Get user request approval permission by location wise.
                    _userRequestApprovePermission = _securityDAL.GetUserLocationRequestApprovalPermission(_userId, _reqSubType, _item.Mpi_cd, _item.Mpi_val);

                    if (_userRequestApprovePermission != null)
                        break;
                }
            }

            //If location wise approval permission does not exists,check for user wise permission.
            if (_userRequestApprovePermission == null)
            {
                _userRequestApprovePermission = _securityDAL.GetUserRequestApprovalPermission(_userId, _reqSubType);
            }

            return _userRequestApprovePermission;
        }






        /// <summary>
        /// This is a commom method used in Approve cycle,to get Approve cycle definiton for particular function.
        /// </summary>
        /// <param name="_sourceCompanyCode">Used to get source location hierachy</param>
        /// <param name="_sourceLocCode">Used to get source location hierachy</param>
        /// <param name="_reqSubType">RequestSubType</param>
        /// <param name="_transactionDate">TransactionDate</param>
        /// <param name="_destinationCompanyCode">Used to get destination location hierachy</param>
        /// <param name="_destinationLocCode">Used to get destination location hierachy</param>
        /// <returns></returns>
        public RequestApproveCycleDefinition GetApproveCycleDefinitionDataforLocation(string _sourceCompanyCode, string _sourceLocCode, string _reqSubType, DateTime _transactionDate, string _destinationCompanyCode, string _destinationLocCode, string _category, string _type)
        {
            _securityDAL = new SecurityDAL();
            RequestApproveCycleDefinition _paramObj = null;
            RequestApproveCycleDefinition _resultObj = null;

            //Get SourceType Collection & loop it with GetApproveCycleDefinitionDetails.
            List<PriorityHierarchy> _sourceTypePriorityHierarchyList = _securityDAL.GetPriorityHierarchy(_sourceCompanyCode, _sourceLocCode, _category, _type);

            //With out destination Loc code.(default)
            if (string.IsNullOrEmpty(_destinationLocCode))
            {
                if ((_sourceTypePriorityHierarchyList != null) && (_sourceTypePriorityHierarchyList.Count > 0))
                {
                    _paramObj = new RequestApproveCycleDefinition();

                    foreach (PriorityHierarchy _sourceItem in _sourceTypePriorityHierarchyList)
                    {
                        _paramObj.SourceType = _sourceItem.HierarchyItemName;
                        _paramObj.SourceTypeCode = _sourceItem.HierarchyItemValue;
                        _paramObj.ReqSubTypeCode = _reqSubType;
                        _paramObj.TransactionDate = _transactionDate;
                        _paramObj.DestinationType = null;
                        _paramObj.DestinationTypeCode = null;

                        _resultObj = _securityDAL.GetApproveCycleDefinitionDetails(_paramObj);

                        if (_resultObj != null)
                            return _resultObj;
                    }
                }

            }
            else//With destination Loc code.
            {
                //Get both SourceType & DestinationType Collection & loop it with GetApproveCycleDefinitionDetails.
                List<PriorityHierarchy> _destTypePriorityHierarchyList = _securityDAL.GetPriorityHierarchy(_destinationCompanyCode, _destinationLocCode, _category, _type);

                if ((_destTypePriorityHierarchyList != null) && (_destTypePriorityHierarchyList.Count > 0))
                {
                    _paramObj = new RequestApproveCycleDefinition();

                    foreach (PriorityHierarchy _sourceItem in _sourceTypePriorityHierarchyList)
                    {
                        _paramObj.SourceType = _sourceItem.HierarchyItemName;
                        _paramObj.SourceTypeCode = _sourceItem.HierarchyItemValue;
                        _paramObj.ReqSubTypeCode = _reqSubType;
                        _paramObj.TransactionDate = _transactionDate;

                        foreach (PriorityHierarchy _destItem in _destTypePriorityHierarchyList)
                        {
                            _paramObj.DestinationType = _destItem.HierarchyItemName;
                            _paramObj.DestinationTypeCode = _destItem.HierarchyItemValue;

                            _resultObj = _securityDAL.GetApproveCycleDefinitionDetails(_paramObj);

                            if (_resultObj != null)
                                return _resultObj;
                        }

                    }

                }
            }

            return _resultObj;

        }

        /// <summary>
        /// This is a commom method used in Approve cycle,to get Approve cycle definiton for particular function.
        /// </summary>
        /// <param name="_sourceCompanyCode">Used to get source location hierachy</param>
        /// <param name="_sourceLocCode">Used to get source location hierachy</param>
        /// <param name="_reqSubType">RequestSubType</param>
        /// <param name="_transactionDate">TransactionDate</param>
        /// <param name="_destinationCompanyCode">Used to get destination location hierachy</param>
        /// <param name="_destinationLocCode">Used to get destination location hierachy</param>
        /// <returns></returns>
        public RequestApproveCycleDefinition GetApproveCycleDefinitionDataforProfitCenter(string _sourceCompanyCode, string _sourceLocCode, string _reqSubType, DateTime _transactionDate, string _destinationCompanyCode, string _destinationLocCode, string _category, string _type)
        {
            _securityDAL = new SecurityDAL();
            _salesDAL = new SalesDAL();
            RequestApproveCycleDefinition _paramObj = null;
            RequestApproveCycleDefinition _resultObj = null;

            //Get SourceType Collection & loop it with GetApproveCycleDefinitionDetails.
            List<MasterSalesPriorityHierarchy> _sourceTypePriorityHierarchyList = _salesDAL.GetSalesPriorityHierarchy(_sourceCompanyCode, _sourceLocCode, _category, _type);

            //With out destination Loc code.(default)
            if (string.IsNullOrEmpty(_destinationLocCode))
            {
                if ((_sourceTypePriorityHierarchyList != null) && (_sourceTypePriorityHierarchyList.Count > 0))
                {
                    _paramObj = new RequestApproveCycleDefinition();

                    foreach (MasterSalesPriorityHierarchy _sourceItem in _sourceTypePriorityHierarchyList)
                    {
                        _paramObj.SourceType = _sourceItem.Mpi_cd;
                        _paramObj.SourceTypeCode = _sourceItem.Mpi_val;
                        _paramObj.ReqSubTypeCode = _reqSubType;
                        _paramObj.TransactionDate = _transactionDate;
                        _paramObj.DestinationType = null;
                        _paramObj.DestinationTypeCode = null;

                        _resultObj = _securityDAL.GetApproveCycleDefinitionDetails(_paramObj);

                        if (_resultObj != null)
                            return _resultObj;
                    }
                }

            }
            else//With destination Loc code.
            {
                //Get both SourceType & DestinationType Collection & loop it with GetApproveCycleDefinitionDetails.
                List<MasterSalesPriorityHierarchy> _destTypePriorityHierarchyList = _salesDAL.GetSalesPriorityHierarchy(_destinationCompanyCode, _destinationLocCode, _category, _type);

                if ((_destTypePriorityHierarchyList != null) && (_destTypePriorityHierarchyList.Count > 0))
                {
                    _paramObj = new RequestApproveCycleDefinition();

                    foreach (MasterSalesPriorityHierarchy _sourceItem in _sourceTypePriorityHierarchyList)
                    {
                        _paramObj.SourceType = _sourceItem.Mpi_cd;
                        _paramObj.SourceTypeCode = _sourceItem.Mpi_val;
                        _paramObj.ReqSubTypeCode = _reqSubType;
                        _paramObj.TransactionDate = _transactionDate;

                        foreach (MasterSalesPriorityHierarchy _destItem in _destTypePriorityHierarchyList)
                        {
                            _paramObj.DestinationType = _destItem.Mpi_cd;
                            _paramObj.DestinationTypeCode = _destItem.Mpi_val;

                            _resultObj = _securityDAL.GetApproveCycleDefinitionDetails(_paramObj);

                            if (_resultObj != null)
                                return _resultObj;
                        }

                    }

                }
            }

            return _resultObj;

        }


        #endregion

        public DataTable GetUserLocTable(string _userID, string _company, string _location)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserLocTable(_userID, _company, _location);
        }

        public List<SystemUserLoc> GetUserLocList(string _userID, string _company, string _location)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserLocList(_userID, _company, _location);
        }


        public List<SystemUserProf> GetUserPC(string _UserID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserPC(_UserID);
        }

        #region user maintain
        public Int32 User_Maintain(SystemUser _user, string action)
        {
            _securityDAL = new SecurityDAL();
            int result = 0;
            _securityDAL.ConnectionOpen();
            result = _securityDAL.User_Maintain(_user, action);
            _securityDAL.ConnectionClose();
            return result;

        }

        #endregion

        #region System Menus / User Roles Menus - Windows Apps
        //
        // Function            - Windows Apps System Menus 
        // Function Wriiten By - Chamal De Silva
        // Date                - 23/01/2013
        //

        // Chamal 23/01/2013
        public DataTable GetUserSystemMenus(string _user, string _company)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserSystemMenus(_user, _company);
        }

        // Chamal 07/02/2013
        public String GetCurrentVersion()
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetCurrentVersion();
        }

        public int SaveSystemMenu(SystemMenus _menu)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.SaveSystemMenu(_menu);
        }
        #endregion


        #region System Option permissions for user
        //
        // Function            - Check System Option permissions for user
        // Function Wriiten By - Shani Waththuhewa
        // Date                - 26/02/2013
        //

        // Shani 26/02/2013
        public Boolean Is_OptionPerimitted(string userCompany, string userId, Int32 optionCode)
        {
            Int32 _effect = 0;

            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _effect = _securityDAL.Is_OptionPerimitted(userCompany, userId, optionCode);
            _securityDAL.ConnectionClose();

            if (_effect > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Back Date Function
        //Created by Shani 07-03-2013
        public DataTable Get_childMenus(string _type, string _parentMenuName)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_childMenus(_type, _parentMenuName);
        }
        //Created by Shani 07-03-2013
        public DataTable Get_Menu(string MenuName, out List<SystemMenus> list)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_Menu(MenuName, out list);
        }
        #endregion

        public DataTable GetUnReadMessages(string _receiver, string _company, string _location, string _profitcenter)
        {
            _reptCommonDAL = new ReptCommonDAL();
            return _reptCommonDAL.GetUnReadMessages(_receiver, _company, _location, _profitcenter);
        }

        public Int32 UpdateViewedMessage(string _document)
        {
            _reptCommonDAL = new ReptCommonDAL();
            _reptCommonDAL.ConnectionOpen();
            int _T = _reptCommonDAL.UpdateViewedMessage(_document);
            _reptCommonDAL.ConnectionClose();
            return _T;
        }

        public DataTable GetUserMessageType(string _receiver, string _location, string _profitcenter)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserMessageType(_receiver, _location, _profitcenter);
        }

        public SystemRole GetSystemRole_ByRoleData(SystemRole _sysRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetSystemRole_ByRoleData(_sysRole);
        }

        public int SaveSelectedSystemOptionsRolePrivillages_NEW(SystemRoleOption _systemRoleOption)
        {
            _securityDAL = new SecurityDAL();
            int result = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                _securityDAL.ConnectionOpen();
                //Delete and Loged the existing role privillages.
                result = _securityDAL.LogCurrentSystemRoleOptions_NEW(_systemRoleOption);

                //If there is a new permission set add it to table.
                if ((_systemRoleOption.SystemOptionList != null) && (_systemRoleOption.SystemOptionList.Count > 0))
                {
                    foreach (SystemOption item in _systemRoleOption.SystemOptionList)
                    {
                        _systemRoleOption.SystemOption = item;
                        int val = _securityDAL.SaveCurrentSystemRoleOptins_NEW(_systemRoleOption);
                    }

                }
                result = 1;
                _securityDAL.ConnectionClose();
                scope.Complete();
            }

            return result;

        }

        public DataTable Get_SystemOptionsData_ByRoleID(string com, Int32 roleId)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SystemOptionsData_ByRoleID(com, roleId);
        }

        public DataTable Get_MenusForRole(string com, Int32 roleId)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_MenusForRole(com, roleId);
        }
        public DataTable Get_UsersForRole(string com, Int32 roleId)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_UsersForRole(com, roleId);
        }

        public DataTable Get_SystemOptionsForGroup(string groupID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SystemOptionsForGroup(groupID);
        }

        public int Save_System_Options_For_Role(SystemRoleOption _systemRoleOption, string OptGroupID)
        {
            _securityDAL = new SecurityDAL();
            int result = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                _securityDAL.ConnectionOpen();
                //Int32 eff = _securityDAL.Update_SEC_SYSTEM_ROLEOPT(_systemRoleOption.SystemRole.CompanyCode, _systemRoleOption.SystemRole.RoleId, -1);

                result = _securityDAL.Update_CurrentSystemRoleOptions(_systemRoleOption, OptGroupID);

                _securityDAL.ConnectionOpen();
                //If there is a new permission set add it to table.
                if ((_systemRoleOption.SystemOptionList != null) && (_systemRoleOption.SystemOptionList.Count > 0))
                {
                    foreach (SystemOption item in _systemRoleOption.SystemOptionList)
                    {
                        _systemRoleOption.SystemOption = item;
                        //int val = _securityDAL.SaveSystemOptins_ForRole(_systemRoleOption);
                        int val = _securityDAL.SaveCurrentSystemRoleOptins_NEW(_systemRoleOption);
                    }
                }
                result = 1;
                _securityDAL.ConnectionClose();
                scope.Complete();
            }
            return result;
        }

        public DataTable Get_Active_System_OptionsFor_Role(string company, Int32 roleId)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_Active_System_OptionsFor_Role(company, roleId);
        }

        public DataTable Get_OptionGroupDetail(string groupID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_OptionGroupDetail(groupID);
        }

        public DataTable GetUser_Company(string _UserId)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUser_Company(_UserId);
        }

        public DataTable Get_SpecialUser_Perm(string _UserID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SpecialUser_Perm(_UserID);
        }

        public int Save_SecUserPerm(SecUserPerm _perm)
        {
            Int32 _effect = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();

            _effect = _securityDAL.Save_SecUserPerm(_perm);

            _securityDAL.ConnectionClose();

            return _effect;
        }

        public int Inactivate_SecUserPerm(List<SecUserPerm> _usrPermList)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();

            using (TransactionScope scope = new TransactionScope())
            {
                _securityDAL.ConnectionOpen();
                foreach (SecUserPerm perm in _usrPermList)
                {
                    eff = _securityDAL.Inactivate_SecUserPerm(perm);
                }
                _securityDAL.ConnectionClose();
                scope.Complete();
            }
            return eff;
        }

        public DataTable GetUserLocations(string UserID, string Comp)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserLocations(UserID, Comp);
        }

        public int UpdateSystemUserLoc_NEW(List<SystemUserLoc> _usrLoc_LIST)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();

            using (TransactionScope scope = new TransactionScope())
            {
                _securityDAL.ConnectionOpen();
                foreach (SystemUserLoc LOC in _usrLoc_LIST)
                {
                    eff = _securityDAL.UpdateSystemUserLoc_NEW(LOC);
                    // eff = _securityDAL.UpdateSystemUserLoc(LOC);
                }
                _securityDAL.ConnectionClose();
                scope.Complete();
            }
            return eff;
        }

        //--
        public int UpdateSystemUserPC_NEW(List<SystemUserProf> _userPC_LIST)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();

            using (TransactionScope scope = new TransactionScope())
            {
                _securityDAL.ConnectionOpen();
                foreach (SystemUserProf PC in _userPC_LIST)
                {
                    eff = _securityDAL.UpdateSystemUserPC_NEW(PC);
                    // eff = _securityDAL.UpdateSystemUserLoc(LOC);
                }
                _securityDAL.ConnectionClose();
                scope.Complete();
            }
            return eff;
        }

        public DataTable GetAllUserPC(string _UserID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetAllUserPC(_UserID);
        }

        public DataTable Get_gen_doc_pro_hdr(string userID, string receiptNo, string invoiceNo, string engineNo, string chassisNo)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_gen_doc_pro_hdr(userID, receiptNo, invoiceNo, engineNo, chassisNo);
        }

        public int DeleteUserLoc_NEW(List<SystemUserLoc> DEL_LIST)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();

            using (TransactionScope scope = new TransactionScope())
            {
                _securityDAL.ConnectionOpen();
                foreach (SystemUserLoc LOC in DEL_LIST)
                {
                    eff = _securityDAL.DeleteUserLoc_NEW(LOC.SEL_USR_ID, LOC.SEL_COM_CD, LOC.SEL_LOC_CD);
                }
                _securityDAL.ConnectionClose();
                scope.Complete();
            }
            return eff;
        }

        public int DeleteUserPC_NEW(List<SystemUserProf> DEL_LIST)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();

            using (TransactionScope scope = new TransactionScope())
            {
                _securityDAL.ConnectionOpen();
                foreach (SystemUserProf PC in DEL_LIST)
                {
                    eff = _securityDAL.DeleteUserPC_NEW(PC.Sup_usr_id, PC.Sup_com_cd, PC.Sup_pc_cd);
                }
                _securityDAL.ConnectionClose();
                scope.Complete();
            }
            return eff;
        }

        public SystemRole GetSystemRoleByRoleData_new(SystemRole _userRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetSystemRoleByRoleData_new(_userRole);
        }

        public int Save_Sec_App_Usr_Prem(SecApproveUserPerm _secApprUserPerm)
        {
            Int32 _effect = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();

            _effect = _securityDAL.Save_Sec_App_Usr_Prem(_secApprUserPerm);

            _securityDAL.ConnectionClose();

            return _effect;
        }

        public DataTable Get_Approve_PermissionInfo(string _ApprPermCd)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_Approve_PermissionInfo(_ApprPermCd);
        }

        public DataTable Get_UserApprove_Permissions(string userID, string permLvlCode)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_UserApprove_Permissions(userID, permLvlCode);
        }

        public int SaveSystemUserRole_NEW(SystemUserRole _usrRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.SaveSystemUserRole_NEW(_usrRole);
        }

        public int DeleteSystemUserRole_NEW(SystemUserRole _usrRole)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.DeleteSystemUserRole_NEW(_usrRole);
        }

        public DataTable GetUserRole_NEW(string UserID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserRole_NEW(UserID);
        }

        public int DeleteUserRole_NEW(string UserID, string Comp, int RoleID)
        {

            Int32 _effect = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();

            _effect = _securityDAL.DeleteUserRole_NEW(UserID, Comp, RoleID);

            _securityDAL.ConnectionClose();

            return _effect;
        }

        public int Save_sec_role_loc(List<SecRoleLocation> _RoleLocList)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();
            //using (TransactionScope scope = new TransactionScope())
            try
            {
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                foreach (SecRoleLocation roleLoc in _RoleLocList)
                {
                    eff = _securityDAL.Save_sec_role_loc(roleLoc);
                }
                // _securityDAL.ConnectionClose();
                eff = 1;
                _securityDAL.TransactionCommit();
                //scope.Complete();
            }
            catch (Exception ex)
            {
                eff = -1;
                _securityDAL.TransactionRollback();
            }
            return eff;
        }

        public int Save_sec_role_LocChanel(List<SecRoleLocChanel> _secLocChnlList)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();
            //using (TransactionScope scope = new TransactionScope())
            try
            {
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                foreach (SecRoleLocChanel roleLocChnl in _secLocChnlList)
                {
                    eff = _securityDAL.Save_sec_role_LocChanel(roleLocChnl);
                }
                // _securityDAL.ConnectionClose();
                eff = 1;
                _securityDAL.TransactionCommit();
                //scope.Complete();
            }
            catch (Exception ex)
            {
                eff = -1;
                _securityDAL.TransactionRollback();
            }
            return eff;

        }


        public int Save_sec_role_pc(List<SecRolePC> _secRolePCList)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();
            //using (TransactionScope scope = new TransactionScope())
            try
            {
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                foreach (SecRolePC rolePC in _secRolePCList)
                {
                    eff = _securityDAL.Save_sec_role_pc(rolePC);
                }
                // _securityDAL.ConnectionClose();
                eff = 1;
                _securityDAL.TransactionCommit();
                //scope.Complete();
            }
            catch (Exception ex)
            {
                eff = -1;
                _securityDAL.TransactionRollback();
            }
            return eff;
        }

        public int Save_sec_role_pcchnl(List<SecRolePcChannel> _secPcChnlList)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();
            try
            {
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                foreach (SecRolePcChannel rolePCChnl in _secPcChnlList)
                {
                    eff = _securityDAL.Save_sec_role_pcchnl(rolePCChnl);
                }
                eff = 1;
                _securityDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                eff = -1;
                _securityDAL.TransactionRollback();
            }
            return eff;
        }


        public int Update_sec_role_loc(List<SecRoleLocation> _RoleLocList)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();
            try
            {
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                foreach (SecRoleLocation roleLoc in _RoleLocList)
                {
                    eff = _securityDAL.Update_secRoleLoc(roleLoc);
                }
                eff = 1;
                _securityDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                eff = -1;
                _securityDAL.TransactionRollback();
            }
            return eff;
        }

        public int Update_sec_role_locChannel(List<SecRoleLocChanel> _RoleLocChannelList)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();
            try
            {
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                foreach (SecRoleLocChanel roleLoc in _RoleLocChannelList)
                {
                    eff = _securityDAL.Update_secRoleLocChannel(roleLoc);
                }
                eff = 1;
                _securityDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                eff = -1;
                _securityDAL.TransactionRollback();
            }
            return eff;
        }

        //-------
        public int Update_sec_role_PC(List<SecRolePC> _RolePc)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();
            try
            {
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                foreach (SecRolePC roleLoc in _RolePc)
                {
                    eff = _securityDAL.Update_secRolePC(roleLoc);
                }
                eff = 1;
                _securityDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                eff = -1;
                _securityDAL.TransactionRollback();
            }
            return eff;
        }

        public int Update_secRolePcChannel(List<SecRolePcChannel> _RolePcChannelList)
        {
            Int32 eff = 0;
            _securityDAL = new SecurityDAL();
            try
            {
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                foreach (SecRolePcChannel roleLoc in _RolePcChannelList)
                {
                    eff = _securityDAL.Update_secRolePcChannel(roleLoc);
                }
                eff = 1;
                _securityDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                eff = -1;
                _securityDAL.TransactionRollback();
            }
            return eff;
        }

        public DataTable Get_Sec_role_loc(string com, Int32 roleID, string loc)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_Sec_role_loc(com, roleID, loc);
        }

        public DataTable Get_Sec_role_locChannel(string com, Int32 roleID, string channel)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_Sec_role_locChannel(com, roleID, channel);
        }

        public DataTable Get_Sec_role_pc(string com, Int32 roleID, string pc)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_Sec_role_pc(com, roleID, pc);
        }

        public DataTable Get_Sec_role_pcChannel(string com, Int32 roleID, string channel)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_Sec_role_pcChannel(com, roleID, channel);
        }

        public DataTable GetCompanyUserRole(string _user, string _com)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetCompanyUserRole(_user, _com);
        }

        public int Change_Password(SystemUser _user)
        {
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            int _ref = _securityDAL.Change_Password(_user);
            _securityDAL.ConnectionClose();
            return _ref;
        }

        public SecurityPolicy GetSecurityPolicy(int _seqNo, out string _errStr)
        {
            SecurityPolicy _securityPolicy = null;
            try
            {
                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                _securityPolicy = _securityDAL.GetSecurityPolicy(_seqNo);
                _securityDAL.ConnectionClose();
                _errStr = string.Empty;
            }
            catch (Exception _ex)
            {
                _securityPolicy = null;
                _errStr = "System Error; " + _ex.Message.ToString();
            }
            return _securityPolicy;
        }

        public LoginUser LoginToSystem(string _userid, string _pw, string _com, string _verNo, string _ipAddress, string _hostName, int _inAttempts, out int _outAttempts, out int _status, out string _msg, out string _msgTitle)
        {
            try
            {
                #region Error Status
                // -1 --> Retry
                // -2 --> Application.Exit();
                // -3 --> Password must change
                // -4 --> User account lock
                #endregion

                _securityDAL = new SecurityDAL();
                _gneralDAL = new GeneralDAL();
                _salesDAL = new SalesDAL();
                _inventoryDAL = new InventoryDAL();

                LoginUser _loginUser = new LoginUser();
                SystemUser _systemuser = new SystemUser();
                SystemUser _systemaduser = new SystemUser();
                SecurityPolicy _securityPolicy = new SecurityPolicy();

                string msg = string.Empty;
                string msgtitle = string.Empty;
                int msgstatus = 0;


                #region Check system version
                string _newSystemVersionNo = _securityDAL.GetCurrentVersion().ToString();
                if (_newSystemVersionNo != _verNo)
                {
                    if (string.IsNullOrEmpty(_newSystemVersionNo)) _newSystemVersionNo = "'UNKNOWN'";

                    _outAttempts = 0;
                    _msg = "Your current system version is expired! \n New system version is " + _newSystemVersionNo + " \n Please download new system update or contact IT department.";
                    _msgTitle = "System version";
                    _status = -2;
                    return null;
                }
                #endregion

                _securityPolicy = _securityDAL.GetSecurityPolicy(1);
                _systemuser = _securityDAL.GetUserByUserID(_userid);
                _loginUser.Login_attempts = _securityPolicy.Spp_lock_err_atmps;
                if (_systemuser == null)
                {
                    _inAttempts = _inAttempts + 1;// increment retry counter
                    _outAttempts = _inAttempts;
                    _msg = "Unsuccessful login.\nPlease contact system administrator.";
                    _msgTitle = "Login Fail";
                    _status = -2;
                    return _loginUser;
                }
                else
                {
                    if (_systemuser.Se_act == 0)
                    {
                        _inAttempts = _inAttempts + 1;// increment retry counter
                        _outAttempts = _inAttempts;
                        _msg = "This user account deactivated.\nPlease contact system administrator.";
                        _msgTitle = "Login Fail";
                        _status = -2;
                        return _loginUser;
                    }

                    if (_systemuser.Se_act == -2)
                    {
                        _inAttempts = _inAttempts + 1;// increment retry counter
                        _outAttempts = _inAttempts;
                        _msg = "This user account permanently deactivated.\nPlease contact system administrator.";
                        _msgTitle = "Login Fail";
                        _status = -2; //Value changed as -2 (Previous value -1 = Retry) by Chamal 02-Feb-2018, due to permanent disable users can update status as lock user 
                        return _loginUser;
                    }

                    if (_systemuser.Se_act == -1)
                    {
                        _inAttempts = _inAttempts + 1;// increment retry counter
                        _outAttempts = _inAttempts;
                        _msg = "The user account has been locked!.\nBecause user entered the invalid passwords more than " + _securityPolicy.Spp_lock_err_atmps + " times.\nPlease contact system administrator.";
                        _msgTitle = "Login Fail";
                        _status = -4;
                        return _loginUser;
                    }

                    if ((_systemuser.Se_isdomain == 1) && (!CheckValidADUser(_systemuser.Se_domain_id, out _systemaduser)))
                    {
                        _inAttempts = _inAttempts + 1;// increment retry counter
                        _outAttempts = _inAttempts;
                        _msg = "Unsuccessful login. Your domain user id has been disable.\nPlease contact system administrator.";
                        _msgTitle = "Login Fail";
                        _status = -1;
                        return _loginUser;
                    }

                    #region Check user department infor
                    List<MasterDepartment> _deptList = _gneralDAL.GetDepartment();
                    if (_deptList != null)
                    {
                        var _lst = _deptList.Where(y => y.Msdt_cd == _systemuser.Se_dept_id).Select(x => x.Msdt_desc).ToList();
                        if (_lst != null)
                        {
                            if (_lst.Count > 0)
                            {
                                _loginUser.User_dept_id = _systemuser.Se_dept_id;
                                _loginUser.User_dept_name = _lst[0].ToString();
                            }
                            else
                            {
                                _inAttempts = _inAttempts + 1;// increment retry counter
                                _outAttempts = _inAttempts;
                                _msg = "Unsuccessful login. Your department not setup.";
                                _msgTitle = "Login Fail";
                                _status = -1;
                                return _loginUser;
                            }
                        }
                        else
                        {
                            _inAttempts = _inAttempts + 1;// increment retry counter
                            _outAttempts = _inAttempts;
                            _msg = "Unsuccessful login. Your department not setup.";
                            _msgTitle = "Login Fail";
                            _status = -1;
                            return _loginUser;
                        }
                    }
                    _deptList = null;
                    #endregion

                    if (!(_systemuser.Se_usr_id.ToUpper().ToString() == _userid.ToUpper().ToString() && _systemuser.Se_usr_pw == _pw.ToString()))
                    {
                        _inAttempts = _inAttempts + 1;// increment retry counter
                        _outAttempts = _inAttempts;
                        _msg = "Invalid user name or password entering.";
                        _msgTitle = "Login Fail";
                        _status = -1;
                        return _loginUser;
                    }
                    else
                    {
                        _loginUser.User_name = _systemuser.Se_usr_name;
                        _loginUser.User_dept_name = _systemuser.Se_dept_id;
                        _loginUser.User_category = _systemuser.Se_usr_cat;
                        _loginUser.Mst_com = _gneralDAL.GetCompByCode(_com);
                        _loginUser.Remaining_days = -1;
                        msgstatus = 1;
                        if (_systemuser.Se_pw_mustchange == 1)
                        {
                            msg = "The user's password must be changed before logging on the this time.";
                            msgtitle = "Change password";
                            msgstatus = -3;
                            _loginUser.Pw_must_change = true;
                        }
                        if (_securityPolicy.Spp_max_pw_age > 0)
                        {
                            _loginUser.Pw_expire = true;
                            int _daysDiff = _securityPolicy.Spp_max_pw_age - (DateTime.Now.Date - _systemuser.Se_pw_chng_dt.Date).Days;
                            if (_daysDiff < 0)
                            {
                                _loginUser.Remaining_days = _daysDiff;
                                _loginUser.Pw_expire = true;
                                _loginUser.Pw_expire_must_change = true;
                            }
                            else
                            {
                                if (3 >= _daysDiff)
                                {
                                    _loginUser.Remaining_days = _daysDiff;
                                    _loginUser.Pw_expire = true;
                                }
                            }
                        }
                    }

                    #region Set user default location and profit center
                    List<SystemUserLoc> _userLocas = _securityDAL.GetUserLoc(_userid, _com);
                    if (_userLocas != null)
                    {
                        var _userLocaQuery =
                            from userLoca in _userLocas
                            where userLoca.SEL_USR_ID == _userid && userLoca.SEL_COM_CD == _com && userLoca.SEL_DEF_LOCCD == 1
                            select userLoca;
                        foreach (var _userLoca in _userLocaQuery)
                        {
                            _loginUser.User_def_loc = _userLoca.SEL_LOC_CD.ToString().ToUpper();
                            break;
                        }
                    }

                    List<SystemUserProf> _userprofs = _securityDAL.GetUserProfCenters(_userid, _com);
                    if (_userprofs != null)
                    {
                        var _userprofQuery =
                        from userProf in _userprofs
                        where userProf.Sup_usr_id == _userid && userProf.Sup_com_cd == _com && userProf.Sup_def_pccd == true
                        select userProf;
                        foreach (var _userprof in _userprofQuery)
                        {
                            _loginUser.User_def_pc = _userprof.Sup_pc_cd.ToString().ToUpper();
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(_loginUser.User_def_loc.ToString()))
                    {
                        MasterLocation _mstLoc = _gneralDAL.GetLocationByLocCode(_com, _loginUser.User_def_loc.ToString());
                        if (_mstLoc != null)
                        {
                            _loginUser.Is_man_chk_loc = _mstLoc.Ml_is_chk_man_doc;
                            _loginUser.User_def_bin = _inventoryDAL.Get_default_binCD(_com, _loginUser.User_def_loc);
                        }
                    }
                    if (!string.IsNullOrEmpty(_loginUser.User_def_pc.ToString()))
                    {
                        _loginUser.Mst_pc = _salesDAL.GetProfitCenter(_com, _loginUser.User_def_pc.ToString());
                        if (_loginUser.Mst_pc != null)
                        {
                            _loginUser.Is_man_chk_pc = _loginUser.Mst_pc.Mpc_is_chk_man_doc;
                            _loginUser.Price_defn = _salesDAL.GetPriceDefinitionByBookAndLevel(_com, string.Empty, string.Empty, string.Empty, _loginUser.User_def_pc.ToString());
                            _loginUser.Chnl_defn = _inventoryDAL.GetChannelDetail(_com, _loginUser.Mst_pc.Mpc_chnl);
                        }
                    }

                    _loginUser.Is_sale_roundup = false;
                    DataTable _result = _gneralDAL.IsSaleFigureRoundUp(_com);
                    if (_result != null && _result.Rows.Count > 0)
                    {
                        if (_result.Rows[0].Field<Int16>("MC_ANAL12") == 1) _loginUser.Is_sale_roundup = true;
                    }
                    _result = null;
                    if (!string.IsNullOrEmpty(_loginUser.User_def_pc.ToString()))
                    {
                        _result = _gneralDAL.Get_PC_Hierarchy(_com, _loginUser.User_def_pc.ToString());
                        if (_result != null && _result.Rows.Count > 0)
                        {
                            foreach (DataRow r in _result.Rows)
                            {
                                if (System.DBNull.Value != r["CHNL"]) _loginUser.Def_chnl = (string)r["CHNL"];
                                if (System.DBNull.Value != r["SCHNL"]) _loginUser.Def_schnl = (string)r["SCHNL"];
                                if (System.DBNull.Value != r["AREA"]) _loginUser.Def_area = (string)r["AREA"];
                                if (System.DBNull.Value != r["REG"]) _loginUser.Def_area = (string)r["REG"];
                                if (System.DBNull.Value != r["ZONE_CD"]) _loginUser.Def_zone = (string)r["ZONE_CD"];
                            }
                        }
                        _result = null;
                    }


                    _loginUser.Com_itm_status = _inventoryDAL.GetAllCompanyStatus(_com);

                    #endregion
                    _loginUser.Login_attempts = _securityPolicy.Spp_lock_err_atmps;

                    //_securityDAL = new SecurityDAL();
                    //_securityDAL.ConnectionOpen(); 
                    //_loginUser.User_session_id = Convert.ToString(_securityDAL.SaveLoginSession(_userid, _com, _ipAddress, _hostName));
                    //_securityDAL.ConnectionClose(); 

                    _outAttempts = 0;
                    _msg = msg; ;
                    _msgTitle = msgtitle;
                    _status = msgstatus;
                    return _loginUser;
                }
            }
            catch (Exception err)
            {
                _outAttempts = 0;
                _msg = err.Message.ToString();
                _msgTitle = "System Error";
                _status = -99;
                return null;
            }
        }

        public bool CheckPasswordPolicy(string _user, string _pw, out string _err)
        {
            bool _result = true;
            try
            {
                string _rtn = string.Empty;
                _err = _rtn;
                SecurityPolicy _securityPolicy = new SecurityPolicy();
                _securityPolicy = GetSecurityPolicy(0, out _rtn);

                if (!string.IsNullOrEmpty(_err))
                {
                    _err = _rtn;
                    return false;
                }

                if (_securityPolicy == null)
                {
                    _err = "Security policy configarations are not setup, Please contact IT Department!";
                    return false;
                }

                if (string.IsNullOrEmpty(_pw))
                {
                    _err = "The password you entered can't be blank!";
                    return false;
                }

                if (_securityPolicy.Spp_notmatch_usr == true)
                {
                    if (_pw.Contains(_user))
                    {
                        _err = "Your new password was rejected because it does not meet the minimum security requirements.";
                        _err = _err + "\n" + "Your password was rejected because it:";
                        _err = _err + "\n" + "- was similar to your current password";
                        return false;
                    }
                }

                if (_securityPolicy.Spp_min_pw_length > 0)
                {
                    if (_pw.Length < _securityPolicy.Spp_min_pw_length)
                    {
                        _err = "Your new password was rejected because it does not meet the minimum security requirements.";
                        _err = _err + "\n" + "Your password was rejected because it:";
                        _err = _err + "\n" + "- did not contain at least " + _securityPolicy.Spp_min_pw_length.ToString() + " character";
                        return false;
                    }
                }

                if (_securityPolicy.Spp_pw_complexity == true)
                {
                    if (CheckPasswordComplexity(_pw) == false)
                    {
                        _err = "Your new password was rejected because it does not meet the minimum security requirements.";
                        _err = _err + "\n" + "Your password was rejected because it:";
                        _err = _err + "\n" + "- did not contain a numeric character";
                        _err = _err + "\n" + "- did not contain a special character";
                        return false;
                    }
                }

                if (_securityPolicy.Spp_pw_histtory > 0)
                {
                    SecurityDAL _securityDAL = new SecurityDAL();
                    _securityDAL.ConnectionOpen();
                    if (_securityDAL.Check_Pw_History(_user, _pw, _securityPolicy.Spp_pw_histtory) == false)
                    {
                        _err = "Your new password was rejected because it does not meet the minimum security requirements.";
                        _err = _err + "\n" + "Your password was rejected because it:";
                        _err = _err + "\n" + "- was same as your previous " + _securityPolicy.Spp_pw_histtory.ToString() + " passwords";
                        _securityDAL.ConnectionClose();
                        return false;
                    }
                    _securityDAL.ConnectionClose();
                }


            }
            catch (Exception _ex)
            {
                _result = false;
                _err = "System Error; \n" + _ex.Message.ToString();

            }
            return _result;
        }

        public bool CheckPasswordComplexity(string password)
        {
            int nonAlnumCount = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i)) nonAlnumCount++;
            }

            if (nonAlnumCount < 1) return false; //should contain atleast one special character

            // if (!string.IsNullOrEmpty(Membership.PasswordStrengthRegularExpression) && !Regex.IsMatch(password, Membership.PasswordStrengthRegularExpression))

            //if (!Regex.IsMatch(password, "[A-Z]+")) return false; //should contain atleast one capital letter

            if (!Regex.IsMatch(password, "[\\d]+")) return false;  //should contain atleast one numeric character

            return true;
        }

        public int Save_User_Falis(string _user, string _pw, string _com, string _ip, string _winusername, string _winuser)
        {
            int _save = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _save = _securityDAL.Save_User_Falis(_user, _pw, _com, _ip, _winusername, _winuser);
            _securityDAL.ConnectionClose();
            return _save;
        }

        public DataTable Get_SystemMenu()
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SystemMenu();
        }

        public DataTable Get_SystemUsers(string _user, string _dept)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SystemUsers(_user, _dept);
        }

        public DataTable Get_SystemUserLoc(string _user, string _dept, string _type)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SystemUserLoc(_user, _dept, _type);
        }

        public DataTable Get_SystemRole()
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SystemRole();
        }

        public DataTable Get_SystemMenuAssgnRole()
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SystemMenuAssgnRole();
        }

        public DataTable Get_SystemSpecialPerm()
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SystemSpecialPerm();
        }

        public DataTable Get_SystemRoleAssgnUser(string _user)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Get_SystemRoleAssgnUser(_user);
        }

        public bool Is_Report_DR(string _repName)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.Is_Report_DR(_repName);
        }

        public void Add_User_Selected_Loc_Pc_DR(string _userid, string _com, string _pc, string _loc, List<string> _list)
        {
            _securityDAL = new SecurityDAL();
            if (_securityDAL.Is_Report_DR("DR_NOW") == true)
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen_DR();

                Int32 _eff = 0;
                _eff = _salesDAL.Delete_TEMP_PC_LOC(_userid, _com, _pc, _loc);
                foreach (string _listItem in _list) _salesDAL.Save_TEMP_PC_LOC(_userid, _com, _listItem, _loc);
                _salesDAL.ConnectionClose();
            }
        }

        //Tharka 2015-05-13
        public List<Main_menu_items> GetUserSystemMenusNew(string _user, string _company)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserSystemMenusNew(_user, _company);
        }


        //Sahan 16 Jun 2015
        public DataTable SP_SCM2_GET_USRPW_STATUS(string P_USER)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.SP_SCM2_GET_USRPW_STATUS(P_USER);
        }
        //Rukshan 04/Aug/2015

        public DataTable GetSBU_Company(string P_COM, string P_SBU)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetSBU_Company(P_COM, P_SBU);
        }

        public int Save_User_SBU(StrategicBusinessUnits _StrategicBusinessUnits)
        {
            Int32 _effect = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _securityDAL.BeginTransaction();
            _effect = _securityDAL.Save_User_SBU(_StrategicBusinessUnits);
            _securityDAL.TransactionCommit();
            return _effect;

        }
        public DataTable GetSBU_User(string _COM, string _UID, string _SBU)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetSBU_User(_COM, _UID, _SBU);
        }

        public int CheckUserAvailability(string userId, string email)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.CheckUserAvailability(userId, email);
        }
        /// <summary>
        /// update password reset hash data
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="id">user id</param>
        /// <param name="message">email body</param>
        /// <returns>
        /// 0 for fail
        /// -1 database update fail
        /// 1 success
        /// </returns>
        public int SendPasswordResetEmail(string email, string id, string message, string host)
        {
            SecurityDAL ssecDal = new SecurityDAL();
            try
            {
                int outVal = 0;
                string securityToken = email + id + DateTime.Now.ToString();
                MD5 md5Hash = MD5.Create();
                string hash = GetMd5Hash(md5Hash, securityToken);
                if (hash.Length > 50)
                {
                    hash = hash.Substring(0, 50);
                }
                hash = hash.ToUpper();

                //int status = 0;
                ssecDal.ConnectionOpen();
                ssecDal.BeginTransaction();
                int update = ssecDal.UpdateUserResetPassword(id, email, hash);
                if (update == 1)
                {
                    Send_SMTPMail(email, "Fast Forward password reset.", message, id, hash, host, "");
                    ssecDal.TransactionCommit();
                    return 1;
                }
                else
                {
                    ssecDal.TransactionRollback();
                    outVal = -1;
                }
                return outVal;
            }
            catch (Exception ex)
            {
                ssecDal.TransactionRollback();
                throw ex;
            }
            finally
            {
                ssecDal.ConnectionClose();
            }

        }
        /// <summary>
        /// genarate password reset hash token
        /// </summary>
        /// <param name="md5Hash">md5 hash</param>
        /// <param name="input">input string</param>
        /// <returns>string</returns>
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public bool CheckPwResetAuth(string secTOkecn, string id)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.CheckPwResetAuth(secTOkecn, id);
        }
        public bool UpdateUserPassword(string password, string secToken, string id, string message, string host)
        {
            _securityDAL = new SecurityDAL();
            SystemUser user = _securityDAL.GetTokecnUser(secToken, id);
            user.Se_usr_pw = password;
            user.Se_pw_chng_by = id;
            if (user.Se_pw_mustchange == 1) user.Se_pw_mustchange = 0;
            int val = Change_Password(user);
            if (val == 1)
            {
                SecurityDAL ssecDal = new SecurityDAL();
                ssecDal.ConnectionOpen();
                ssecDal.UpdateUserResetPassword(id, user.se_Email, "");
                try
                {
                    Send_SMTPMail(user.se_Email, "Fast Forward password reset success.", message, id, "", host, user.Se_usr_name);
                }
                catch (Exception)
                {

                }
                ssecDal.ConnectionClose();
                return true;
            }
            return false;
        }

        public void Send_SMTPMail(string _recipientEmailAddress, string _subject, string _message, string id, string secToken, string host, string username)
        {
            try
            {
                string msgType = _subject;
                SecurityDAL _secDal = new SecurityDAL();
                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(_secDal.GetMailAddress(), _secDal.GetMailDispalyName());
                smtpClient.Host = _secDal.GetMailHost();
                smtpClient.Port = 25;
                message.From = fromAddress;


                message.To.Add(_recipientEmailAddress);
                message.Subject = msgType;
                message.IsBodyHtml = true;
                _message = _message.Replace("@userid", id);
                _message = _message.Replace("@bakcol", ConfigurationSettings.AppSettings.Get("customerColor"));
                if (!string.IsNullOrEmpty(secToken))
                {
                    _message = _message.Replace("@resetUrl", host + "/ResetPassword.aspx?token=" + secToken + "&id=" + id);
                }
                if (!string.IsNullOrEmpty(username))
                {
                    _message = _message.Replace("@userName", username);
                }
                _message = _message.Replace("@baseimage", host + "/images/banners/FFNew-125x41.png");
                _message = _message.Replace("@footermessage", _secDal.GetMailFooterMsg());
                message.Body = _message;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                // Send SMTP mail
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //loop
        }
        //Lakshika 2016-07-09
        public bool CheckValidADUserForVNC(string UserID, string password, string domainName, string sunServer, out string _feedback)
        {
            _securityDAL = new SecurityDAL();
            string strRootDN = string.Empty;
            string strLDAPPath = string.Empty;
            string strDomainname = string.Empty;
            string strAuthondiuser = string.Empty;
            string strAuthondiuserpw = string.Empty;
            string sunOpeID = string.Empty;

            try
            {

                domainName = domainName.ToLower().ToString();

                DirectoryEntry objDseSearchRoot = null, objDseUserEntry = null;
                DirectorySearcher objDseSearcher = null;
                SearchResultCollection objResults = null;

                SystemUser UserInfor = new SystemUser();

                string USER = _securityDAL.GetADAuthenticateUser();
                string PWD = _securityDAL.GetADAuthenticateUserPw();

                //Add by Chamal 13-Jul-2018
                if (!string.IsNullOrEmpty(sunServer))
                {
                    DataTable _sunDBLinks = _securityDAL.GetSUNDBLinks(sunServer);
                    if (_sunDBLinks != null && _sunDBLinks.Rows.Count > 0)
                    {
                        if (_sunDBLinks.Rows[0]["rss_dblinkname"] != DBNull.Value)
                        {
                            string _sundbLink = _sunDBLinks.Rows[0]["rss_dblinkname"].ToString();
                            string _domainID = domainName.ToUpper().ToString() + @"\" + UserID.ToString();
                            string _workfilePath = _sunDBLinks.Rows[0]["rss_file_path"].ToString();
                            _domainID = _domainID.ToUpper().ToString();
                            DataTable _sunUser = _securityDAL.GetSUNUser(_domainID, _sundbLink);
                            if (_sunUser != null && _sunUser.Rows.Count > 0)
                            {
                                sunOpeID = _sunUser.Rows[0]["col_val"].ToString() + "|" + _sunUser.Rows[1]["col_val"].ToString() + "|" + _workfilePath + "|" + _sundbLink;
                                if (string.IsNullOrEmpty(sunOpeID))
                                {
                                    _feedback = "SUN operator ID not setup for domain user " + domainName;
                                    return false;
                                }
                            }
                            else
                            {
                                _feedback = "SUN operator ID not setup for domain user " + domainName;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        _feedback = "SUN Server " + sunServer + " not defined!";
                        return false;
                    }

                }


                //objDseSearchRoot = new DirectoryEntry(strLDAPPath, strDomainname + "\\" + strAuthondiuser, strAuthondiuserpw, AuthenticationTypes.None);
                objDseSearchRoot = new DirectoryEntry(_securityDAL.GetADConnectionString(), _securityDAL.GetADDomainName() + "\\" + _securityDAL.GetADAuthenticateUser(), _securityDAL.GetADAuthenticateUserPw(), AuthenticationTypes.Secure);
                strRootDN = objDseSearchRoot.Properties["defaultNamingContext"].Value as string;
                objDseSearcher = new DirectorySearcher(objDseSearchRoot);
                objDseSearcher.CacheResults = false;

                objDseSearcher.PropertiesToLoad.Add("pwdLastSet");
                objDseSearcher.PropertiesToLoad.Add("memberOf");

                string netbiosDomainName = string.Empty;

                DirectoryEntry searchRoot = new DirectoryEntry(_securityDAL.GetADConnectionString(), _securityDAL.GetADDomainName() + "\\" + _securityDAL.GetADAuthenticateUser(), _securityDAL.GetADAuthenticateUserPw() + strRootDN);

                DirectorySearcher searcher = new DirectorySearcher(searchRoot);
                searcher.SearchScope = SearchScope.Subtree;
                searcher.PropertiesToLoad.Add("memberof");
                searcher.Filter = string.Format("(&(objectcategory=Crossref)(dnsRoot={0})(memberof=*))", domainName);

                SearchResultCollection result = searcher.FindAll();

                if (result.Count > 0)
                {
                    netbiosDomainName = result[0].GetDirectoryEntry().Properties["memberof"][0].ToString();
                }

                objDseSearcher.Filter = String.Format("(&(objectClass=user)(sAMAccountName={0}))", UserID);


                objResults = objDseSearcher.FindAll();
                if (objResults.Count > 0)
                {
                    objDseUserEntry = objResults[0].GetDirectoryEntry();
                    if (objDseUserEntry.Properties.Contains("Name")) { UserInfor.Ad_full_name = objDseUserEntry.Properties["Name"][0].ToString(); }
                    else { UserInfor.Ad_full_name = ""; }
                    if (objDseUserEntry.Properties.Contains("Title")) { UserInfor.Ad_title = objDseUserEntry.Properties["Title"][0].ToString(); }
                    else { UserInfor.Ad_title = ""; }
                    if (objDseUserEntry.Properties.Contains("Department")) { UserInfor.Ad_department = objDseUserEntry.Properties["Department"][0].ToString(); }
                    else { UserInfor.Ad_department = ""; }
                    //string biiosnm = objDseUserEntry.Properties["netbiosname"][0].ToString();

                    //Checking username and Password for login
                    bool valid = false;
                    using (PrincipalContext context = new PrincipalContext(ContextType.Domain, domainName.ToString()))
                    {
                        valid = context.ValidateCredentials(UserID, password);

                        if (valid == false)
                        {
                            _feedback = "Invalid domain user name or password";
                            return false;
                        }
                    }

                    //using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "abans"))
                    //{
                    //    UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, "lakshikas");
                    //    foreach (var group in user.GetGroups())
                    //    {
                    //        Console.WriteLine(group.Name);
                    //    }
                    //}

                }

                if (objDseUserEntry == null)
                {
                    _feedback = "Invalid domain user name or password";
                    return false;
                }
                else
                {
                    _feedback = sunOpeID;
                    return true;
                }
            }

            catch (Exception ex)
            {
                _feedback = ex.Message.ToString();
                return false;
            }
        }

        #region asycuda
        /// <summary>
        /// get asycuda data related database list
        /// </summary>
        /// <returns>List<ASY_DB_SOURCE></returns>
        public List<ASY_DB_SOURCE> GetSystemDatabaseList()
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                List<ASY_DB_SOURCE> dbList = _asycudaDAL.GetSystemDatabaseList();
                List<ASY_DB_SOURCE> newdbList = new List<ASY_DB_SOURCE>();
                if (dbList != null)
                {

                    foreach (ASY_DB_SOURCE db in dbList)
                    {
                        ASY_DB_SOURCE newItm = new ASY_DB_SOURCE();
                        newItm.Add_db_id = Convert.ToInt32(db.Add_db_id);
                        newItm.Add_db_name = db.Add_db_name;
                        newItm.Add_db_tp = db.Add_db_tp;
                        newdbList.Add(newItm);
                    }
                }
                return newdbList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get asycuda xml group list
        /// </summary>
        /// <returns>List<ASY_DOC_GRUP></returns>
        public List<ASY_DOC_GRUP> GetAsycudaGrpList()
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.GetAsycudaGrpList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get asycuda xml type list
        /// </summary>
        /// <param name="_grp">grout id</param>
        /// <param name="_database">database id</param>
        /// <returns>List<ASY_DOC_TP></returns>
        public List<ASY_DOC_TP> GetAsycudaTypeList(string _grp, string _database)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.GetAsycudaTypeList(_grp, _database);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// set asycuda header details
        /// </summary>
        /// <param name="docno">document number</param>
        /// <param name="datasrcid">database source id</param>
        /// <param name="doctype">document type</param>
        /// <returns>ASY_ALT_HEADER_DTLS</returns>
        /// 

        public ASY_ALT_HEADER_DTLS SetAlterHeaderDetails(string docno, int datasrcid, string doctype, out string error)
        {

            error = "";
            ASY_ALT_HEADER_DTLS alterHedDet = null;
            try
            {
                _asycudaDAL = new AsycudaDAL();
                Int32 eff = _asycudaDAL.deleteTempDocument(docno);
                //bool checkDoc = documentIsAvailable(docno);
                //if (checkDoc == true)
                //{

                //    _asycudaDAL = new AsycudaDAL();
                //    alterHedDet = _asycudaDAL.GetAsycudaAlterHederDetails(docno);
                //}
                //else
                //{
                ASY_IMP_CUSDEC_HDR hederDtl = GetCusdecHdrDetails(docno, datasrcid);
                if (hederDtl != null)
                {
                    ASY_DB_SOURCE datasrc = GetDataSourceDetails(datasrcid);
                    AsycudaDAL _altAsycudaDAL = new AsycudaDAL(datasrc);

                    //added by Chamal on 26-Aug-2016
                    decimal _costVal = 0;
                    decimal _freightVal = 0;
                    decimal _insuVal = 0;
                    decimal _othVal = 0;
                    List<ImpCusdecCost> custCost = new List<ImpCusdecCost>();
                    //_financialDAL = new FinancialDAL();
                    custCost = _altAsycudaDAL.GET_CUSDEC_COST_BY_DOC(docno);
                    if (custCost != null)
                    {
                        custCost = custCost.Where(i => i.Cus_ele_cat == "TOT").ToList();
                        foreach (ImpCusdecCost itm in custCost)
                        {
                            if (itm.Cus_ele_cd == "COST")
                            { itm.Cus_ele_cd_name = "Cost"; itm.Cus_line = 1; _costVal = itm.Cus_amt; }
                            if (itm.Cus_ele_cd == "FRGT")
                            { itm.Cus_ele_cd_name = "Freight"; itm.Cus_line = 2; _freightVal = itm.Cus_amt; }
                            if (itm.Cus_ele_cd == "INSU")
                            { itm.Cus_ele_cd_name = "Insurance"; itm.Cus_line = 3; _insuVal = itm.Cus_amt; }
                            if (itm.Cus_ele_cd == "OTH")
                            { itm.Cus_ele_cd_name = "Other"; itm.Cus_line = 4; _othVal = itm.Cus_amt; }
                        }
                    }

                    alterHedDet = new ASY_ALT_HEADER_DTLS();
                    alterHedDet.Ach_sad_flow = "I";
                    alterHedDet.Ach_noof_forms = 1;
                    alterHedDet.Ach_tot_noof_forms = Int32.Parse(getTotalForms(hederDtl.CUH_ITEMS_QTY).ToString());
                    alterHedDet.Ach_items_qty = hederDtl.CUH_ITEMS_QTY;
                    alterHedDet.Ach_tot_pkg = hederDtl.CUH_TOT_PKG;
                    alterHedDet.Ach_tot_pkg_unit = hederDtl.CUH_TOT_PKG_UNIT;
                    alterHedDet.Ach_selected_page = 1;
                    alterHedDet.Ach_off_entry_cd = hederDtl.CUH_OFFICE_OF_ENTRY;
                    alterHedDet.Ach_off_entry_desc = GetOfficeOfEntryDescription(hederDtl.CUH_OFFICE_OF_ENTRY);
                    alterHedDet.Ach_decl_1n2 = hederDtl.CUH_DECL_1 + hederDtl.CUH_DECL_2;
                    alterHedDet.Ach_decl_1 = hederDtl.CUH_DECL_1;
                    alterHedDet.Ach_decl_2 = hederDtl.CUH_DECL_2;
                    alterHedDet.Ach_decl_3 = hederDtl.CUH_DECL_3;
                    alterHedDet.Ach_com = hederDtl.CUH_COM;
                    if (doctype == "EX" || doctype == "RE" || doctype == "TO")
                    {
                        alterHedDet.Ach_manifest_ref_no = docno;
                    }
                    else if (doctype == "AIR" || doctype == "LR" || doctype == "EXP" || doctype == "MV")
                    {
                        alterHedDet.Ach_manifest_ref_no = hederDtl.CUH_FILE_NO;
                    }
                    else
                    {
                        alterHedDet.Ach_manifest_ref_no = hederDtl.CUH_BANK_REF_CD;
                    }

                    if (doctype == "EX" || doctype == "RE")
                    {
                        if (hederDtl.CUH_COM == "SGD" || hederDtl.CUH_COM == "SGL")
                        {
                            alterHedDet.Ach_exp_add = "SINGHAGIRI PVT LTD 515,DARLEY RD,COLOMBO 10";
                            alterHedDet.Ach_exp_cd = "1040354697000";
                            alterHedDet.Ach_exp_name = "SINGHAGIRI PVT LTD";
                        }
                        else
                        {
                            alterHedDet.Ach_exp_add = "ABANS PLC GALLE ROAD, COLOMBO 03";
                            alterHedDet.Ach_exp_cd = "1040800657000";
                            alterHedDet.Ach_exp_name = "ABANS PLC";
                        }

                    }
                    else
                    {
                        alterHedDet.Ach_exp_add = hederDtl.CUH_SUPP_NAME + "\n" + hederDtl.CUH_SUPP_ADDR;
                        alterHedDet.Ach_exp_cd = hederDtl.CUH_SUPP_TIN;
                        alterHedDet.Ach_exp_name = hederDtl.CUH_SUPP_NAME;
                    }
                    alterHedDet.Ach_cons_tin = hederDtl.CUH_CONSI_TIN;
                    alterHedDet.Ach_cons_cd = hederDtl.CUH_CONSI_CD;
                    alterHedDet.Ach_cons_name = hederDtl.CUH_CONSI_NAME;
                    alterHedDet.Ach_cons_add = hederDtl.CUH_CONSI_NAME + "\n" + hederDtl.CUH_CONSI_ADDR;
                    alterHedDet.Ach_dec_cd = hederDtl.CUH_DECL_CD;
                    alterHedDet.Ach_dec_name = hederDtl.CUH_DECL_NAME;
                    alterHedDet.Ach_dec_tin = hederDtl.CUH_DECL_TIN;
                    alterHedDet.Ach_dec_add = hederDtl.CUH_DECL_NAME + "\n" + hederDtl.CUH_DECL_ADDR;
                    alterHedDet.Ach_doc_no = docno;
                    alterHedDet.Ach_trading_cnty = hederDtl.CUH_TRADING_COUNTRY;
                    alterHedDet.Ach_exp_cnty_cd = hederDtl.CUH_CNTY_OF_EXPORT;
                    alterHedDet.Ach_exp_cnty = hederDtl.CUH_EXP_CNTY_NAME;
                    alterHedDet.Ach_dest_cnty_cd = hederDtl.CUH_CNTY_OF_DESTINATION;
                    alterHedDet.Ach_dest_cnty = hederDtl.CUH_DESTI_CNTY_NAME;
                    alterHedDet.Ach_orig_cnty_cd = hederDtl.CUH_CNTY_OF_ORIGIN;
                    alterHedDet.Ach_orig_cnty = hederDtl.CUH_ORIGIN_CNTY_NAME;
                    alterHedDet.Ach_val_det = GetTotalAmount(docno, 1, _altAsycudaDAL) * hederDtl.CUH_EX_RT;
                    alterHedDet.Ach_vessel = hederDtl.CUH_VESSEL;
                    alterHedDet.Ach_voyage = hederDtl.CUH_VOYAGE + " OF " + hederDtl.CUH_VOYAGE_DT.ToString("dd/MM/yyy");
                    alterHedDet.Ach_voyage_dt = hederDtl.CUH_VOYAGE_DT;
                    if (doctype == "AIR")
                    {
                        alterHedDet.Ach_border_info_mode = "4";
                    }
                    else
                    {
                        alterHedDet.Ach_border_info_mode = "1";
                    }



                    alterHedDet.Ach_fcl = hederDtl.CUH_FCL;
                    if (doctype == "TO")
                    {
                        alterHedDet.Ach_marks_and_no = hederDtl.CUH_CONTAINER_FCL;
                    }
                    else
                    {
                        alterHedDet.Ach_marks_and_no = hederDtl.CUH_MARKS_AND_NO;
                    }
                    alterHedDet.Ach_delivery_terms = hederDtl.CUH_DELIVERY_TERMS;
                    alterHedDet.Ach_loc_goods_cd = hederDtl.CUH_LOCATION_OF_GOODS;
                    alterHedDet.Ach_loc_goods_desc = GetLocationofGoods(hederDtl.CUH_LOCATION_OF_GOODS);
                    alterHedDet.Ach_bank_cd = hederDtl.CUH_BANK_CD;
                    alterHedDet.Ach_bank_name = GetBankName(hederDtl.CUH_BANK_CD);
                    alterHedDet.Ach_bank_branch = hederDtl.CUH_BANK_BRANCH;
                    if (doctype == "EX" || doctype == "RE")
                    {
                        alterHedDet.Ach_oth_doc_no = hederDtl.CUH_SUN_BOND_NO;
                    }
                    else
                    {
                        alterHedDet.Ach_oth_doc_no = hederDtl.CUH_BANK_REF_CD;
                    }
                    alterHedDet.Ach_terms_of_payment = hederDtl.CUH_CUSTOM_LC_TP;
                    alterHedDet.Ach_terms_of_payment_desc = getTermsOfPaymentDescription(hederDtl.CUH_CUSTOM_LC_TP);
                    if (doctype == "AIR" || doctype == "LR" || doctype == "MV")
                    {
                        alterHedDet.Ach_acc_no = "";
                    }
                    else
                    {
                        alterHedDet.Ach_acc_no = hederDtl.CUH_ACC_NO;
                    }
                    alterHedDet.Ach_remark = hederDtl.CUH_RMK;
                    alterHedDet.Ach_garentee_amt = 0;
                    alterHedDet.Ach_wh_and_period = hederDtl.CUH_WH_AND_PERIOD;
                    alterHedDet.Ach_wh_delay = 180;
                    alterHedDet.Ach_cal_working_mode = "0";
                    if (doctype == "AIR" || doctype == "LR" || doctype == "EXP" || doctype == "MV")
                    {
                        alterHedDet.Ach_tot_cost = _costVal;
                        alterHedDet.Ach_tot_amt = _costVal;
                    }
                    else
                    {
                        alterHedDet.Ach_tot_cost = hederDtl.CUH_TOT_AMT;
                        alterHedDet.Ach_tot_amt = hederDtl.CUH_TOT_AMT;
                    }
                    alterHedDet.Ach_cur_cd = hederDtl.CUH_CUR_CD;
                    alterHedDet.Ach_cur_name = "No foreign currency";
                    alterHedDet.Ach_doc_dt = DateTime.Now;
                    alterHedDet.Ach_ex_rt = hederDtl.CUH_EX_RT;
                    alterHedDet.Ach_oth_amt = GetTotalOthereAmount(docno, _altAsycudaDAL);
                    alterHedDet.Ach_gross_mass = hederDtl.CUH_TOT_GROSS_MASS;
                    alterHedDet.Ach_lision_no = hederDtl.CUH_LISION_NO;
                    alterHedDet.Ach_main_hs = hederDtl.CUH_MAIN_HS;
                    if (doctype == "EX" || doctype == "RE")
                    {
                        alterHedDet.Ach_fre_amt = 0;
                        alterHedDet.Ach_cost_amt = 0;
                        alterHedDet.Ach_oth_amt = 0;
                        alterHedDet.Ach_insu_amt = 0;
                    }
                    else
                    {
                        alterHedDet.Ach_fre_amt = GetTotalfreightAmount(docno, _altAsycudaDAL);
                        alterHedDet.Ach_cost_amt = GetTotalCostAmount(docno, _altAsycudaDAL);
                        alterHedDet.Ach_oth_amt = GetTotalOthereAmount(docno, _altAsycudaDAL);
                        alterHedDet.Ach_insu_amt = GetTotalInsuaranceAmount(docno, _altAsycudaDAL);
                    }

                    if (doctype == "RE")
                    {
                        alterHedDet.Ach_fin_code = "";
                        alterHedDet.Ach_fin_name = "";
                    }
                    else
                    {
                        alterHedDet.Ach_fin_code = hederDtl.CUH_CONSI_TIN;
                        alterHedDet.Ach_fin_name = hederDtl.CUH_CONSI_NAME + "\n" + hederDtl.CUH_CONSI_ADDR;
                    }

                    alterHedDet.Ach_proc_id_1 = hederDtl.CUH_PROCE_CD_1;
                    alterHedDet.Ach_proc_id_2 = hederDtl.CUH_PROCE_CD_2;
                    alterHedDet.Ach_proc_cd = hederDtl.CUH_PPC_NO;//set for getting othere doc no to RE bond

                    AsycudaDAL _asycudaDALs = new AsycudaDAL();
                    bool returnval = _asycudaDALs.AddAlterDetails(alterHedDet);
                }
                //}
                return alterHedDet;
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                return alterHedDet;
            }
        }
        //public ASY_ALT_HEADER_DTLS SetAlterHeaderDetails(string docno, int datasrcid, string doctype,out string error) 
        //{
        //    error = "";
        //    ASY_ALT_HEADER_DTLS alterHedDet = null;
        //    try
        //    {

        //        bool checkDoc = documentIsAvailable(docno);
        //        if (checkDoc == true)
        //        {

        //            _asycudaDAL = new AsycudaDAL();
        //            alterHedDet = _asycudaDAL.GetAsycudaAlterHederDetails(docno);
        //        }
        //        else
        //        {
        //            ASY_HEADER_DTLS hederDtl = GetAsycudaHederDetails(docno, datasrcid, doctype);
        //            if (hederDtl != null)
        //            {

        //                alterHedDet = new ASY_ALT_HEADER_DTLS();
        //                alterHedDet.Ach_doc_no = docno;
        //                if (doctype == "TO" || doctype == "AF" || doctype == "LR" || doctype == "EXP")
        //                {
        //                    alterHedDet.Ach_oth_doc_no = hederDtl.FINANCIAL_DOCUMENT_NO;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_oth_doc_no = hederDtl.RELATED_DOCUMENT;
        //                }

        //                alterHedDet.Ach_doc_dt = DateTime.Now;
        //                //alterHedDet.Ach_com =
        //                //alterHedDet.Ach_db =
        //                //alterHedDet.Ach_proc_cd =
        //                alterHedDet.Ach_decl_1 = hederDtl.DECLRENT_1;
        //                alterHedDet.Ach_decl_2 = hederDtl.DECLRENT_2;
        //                alterHedDet.Ach_decl_3 = hederDtl.DECLRENT_3;
        //                alterHedDet.Ach_decl_1n2 = hederDtl.DECLRENT_1 + hederDtl.DECLRENT_2;
        //                if (doctype.ToUpper() == "EXP")
        //                {
        //                    alterHedDet.Ach_exp_cd = hederDtl.TIN_NO.Replace(".", "").Replace(" ", "");
        //                }
        //                else //if (doctype == "RE" || doctype == "EX" || doctype == "TO" || doctype == "AF" || doctype == "LR" || doctype == "BOI")
        //                {
        //                    alterHedDet.Ach_exp_cd = "<null/>";
        //                }

        //                alterHedDet.Ach_exp_tin = hederDtl.TIN_NO;
        //                if (doctype == "RE" || doctype == "EX" || doctype == "BOI")
        //                {
        //                    alterHedDet.Ach_exp_name = "ABANS PLC GALLE ROAD, COLOMBO 03";
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_exp_name = hederDtl.SUPPLIER_NAME + "\n" + hederDtl.ADDRESS_LINE_1_EXPR3 + "\n" + hederDtl.ADDRESS_LINE_2_EXPR4;
        //                }


        //                alterHedDet.Ach_exp_add = hederDtl.ADDRESS_LINE_1_EXPR3 + "\n" + hederDtl.ADDRESS_LINE_2_EXPR4;
        //                if (doctype.ToUpper() != "EXP")
        //                    alterHedDet.Ach_cons_cd = hederDtl.TIN_NO.Replace(".", "").Replace(" ", "");
        //                alterHedDet.Ach_cons_tin = hederDtl.TIN_NO;
        //                if (doctype.ToUpper() != "EXP")
        //                    alterHedDet.Ach_cons_name = hederDtl.COMPANY_NAME + "\n" + hederDtl.ADDRESS_LINE1 + "\n" + hederDtl.ADDRESS_LINE2;
        //                alterHedDet.Ach_cons_add = hederDtl.ADDRESS_LINE1 + "\n" + hederDtl.ADDRESS_LINE2;
        //                alterHedDet.Ach_dec_cd = hederDtl.DECLRENT_TIN;
        //                alterHedDet.Ach_dec_tin = hederDtl.DECLRENT_TIN;
        //                alterHedDet.Ach_dec_name = hederDtl.DECLRENT_NAME;
        //                alterHedDet.Ach_dec_add = hederDtl.DECLRENT_ADDRESS;
        //                alterHedDet.Ach_items_qty = hederDtl.ITM_QTY;
        //                alterHedDet.Ach_tot_pkg = hederDtl.TOTAL_PAKAGE;
        //                alterHedDet.Ach_exp_cnty_cd = hederDtl.CITY_OF_LAST_CONSIGNEE;
        //                alterHedDet.Ach_exp_cnty = hederDtl.DESCRIPTION_EXPR1;
        //                alterHedDet.Ach_dest_cnty_cd = hederDtl.COUNTRY_OF_DESTINATION;
        //                alterHedDet.Ach_dest_cnty = hederDtl.DESCRIPTION_EXPR2;
        //                alterHedDet.Ach_orig_cnty_cd = hederDtl.COUNTRY_OF_ORIGIN;

        //                if (doctype != "BOI")
        //                    alterHedDet.Ach_orig_cnty = hederDtl.DESCRIPTION_EXPR1;
        //                //alterHedDet.Ach_load_cnty_cd 
        //                //alterHedDet.Ach_load_cnty 
        //                if (doctype != "BOI")
        //                {
        //                    alterHedDet.Ach_vessel = hederDtl.VESSEL;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_vessel = "LOCAL SALES";
        //                }

        //                if (doctype == "AF" || doctype == "LR" || doctype == "EXP" || doctype == "TO")
        //                {
        //                    alterHedDet.Ach_voyage = hederDtl.VOYAGE;//+ ' ' + hederDtl.VOYAGE_DATE.ToString("dd/MM/yyy");
        //                }
        //                if (doctype == "RE" || doctype == "EX")
        //                {
        //                    alterHedDet.Ach_voyage = hederDtl.VOYAGE_DATE.ToString("dd/MM/yyy");
        //                }
        //                if (doctype == "BOI")
        //                {
        //                    alterHedDet.Ach_voyage = "LOCAL SALES";
        //                }

        //                alterHedDet.Ach_voyage_dt = hederDtl.VOYAGE_DATE;
        //                alterHedDet.Ach_fcl = (hederDtl.FCL.ToString() == "1") ? "true" : "false";
        //                alterHedDet.Ach_marks_and_no = hederDtl.CONTAINER_FCL;
        //                alterHedDet.Ach_delivery_terms = hederDtl.DELIVERY_TERMS;
        //                alterHedDet.Ach_cur_cd = hederDtl.CURRENCY;
        //                alterHedDet.Ach_tot_amt = hederDtl.TOTAL_AMOUNT;
        //                alterHedDet.Ach_ex_rt = hederDtl.EXCHANGE_RATE;
        //                alterHedDet.Ach_bank_cd = hederDtl.BANK_CODE;
        //                alterHedDet.Ach_bank_name = hederDtl.BANK_NAME;
        //                if (doctype == "RE" || doctype == "EX" || doctype == "TO")
        //                {
        //                    alterHedDet.Ach_bank_branch = "001";
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_bank_branch = hederDtl.BANK_BRANCH;
        //                }

        //                alterHedDet.Ach_terms_of_payment = hederDtl.TERMS_OF_PAYMENT;
        //                if (doctype == "LR" || doctype == "AF")
        //                {
        //                    alterHedDet.Ach_acc_no = "<null/>";
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_acc_no = hederDtl.ACCOUNT_NO;
        //                }

        //                alterHedDet.Ach_wh_and_period = hederDtl.WARE_HOUSE_AND_PIRIOD;
        //                alterHedDet.Ach_off_entry_cd = hederDtl.OFFICE_OF_ENTRY;
        //                alterHedDet.Ach_off_entry_desc = hederDtl.OFFICE_ENTRY_DESCRIPTION;
        //                alterHedDet.Ach_loc_goods_cd = getLocationOfgodsCd(hederDtl.LOCATION_OF_GODS_DESC);
        //                alterHedDet.Ach_loc_goods_desc = hederDtl.LOCATION_OF_GODS_DESC;
        //                alterHedDet.Ach_tot_pkg_unit = hederDtl.TOTAL_PAKAGE_UNIT;
        //                alterHedDet.Ach_proc_id_1 = hederDtl.PRINT_1_B37_1;
        //                alterHedDet.Ach_proc_id_2 = hederDtl.PRINT_2_B37_2;
        //                alterHedDet.Ach_lision_no = hederDtl.LISION_NO;
        //                if (doctype == "RE" || doctype == "EX" || doctype == "LR" || doctype == "BOI" || doctype == "TO")
        //                {
        //                    alterHedDet.Ach_trading_cnty = hederDtl.CITY_OF_LAST_CONSIGNEE;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_trading_cnty = hederDtl.TRADING_COUNTRY;
        //                }

        //                alterHedDet.Ach_main_hs = hederDtl.MAIN_HS;
        //                alterHedDet.Ach_noof_forms = 1;
        //                alterHedDet.Ach_tot_noof_forms = Int32.Parse(hederDtl.TOTAL_NUM_OF_FORMS.ToString());
        //                alterHedDet.Ach_reg_dt = DateTime.Now;
        //                alterHedDet.Ach_wh_delay = 180;
        //                alterHedDet.Ach_cost_amt = hederDtl.COST_AMOUNT;
        //                if (doctype == "RE" || doctype == "EX" || doctype == "TO")
        //                {
        //                    alterHedDet.Ach_fre_amt = 0;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_fre_amt = hederDtl.FREIGHT_AMOUNT;
        //                }

        //                if (doctype == "RE" || doctype == "EX" || doctype == "TO")
        //                {
        //                    alterHedDet.Ach_insu_amt = 0;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_insu_amt = hederDtl.INSUARANCE_AMOUNT;
        //                }

        //                if (doctype == "RE")
        //                {
        //                    alterHedDet.Ach_oth_amt = 0;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_oth_amt = hederDtl.OTHER_AMOUNT;
        //                }

        //                alterHedDet.Ach_gross_mass = hederDtl.TOTAL_GROSS_MASS;
        //                alterHedDet.Ach_net_mass = 0;//set for check
        //                alterHedDet.Ach_sad_flow = "I";//set for check
        //                alterHedDet.Ach_selected_page = 1;//set for check
        //                if (doctype == "RE" || doctype == "EX" || doctype == "TO" || doctype == "LR" || doctype == "AF" || doctype == "BOI")
        //                {
        //                    alterHedDet.Ach_val_det = 0;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_val_det = 1;
        //                }
        //                if (doctype == "AF" || doctype == "LR" || doctype == "EXP" || doctype == "BOI")
        //                {
        //                    alterHedDet.Ach_fin_code = hederDtl.TIN_NO.Replace(".", "").Replace(" ", "");
        //                    alterHedDet.Ach_fin_name = hederDtl.COMPANY_NAME + "\n" + hederDtl.ADDRESS_LINE1 + "\n" + hederDtl.ADDRESS_LINE2;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_fin_code = "<null/>";
        //                    alterHedDet.Ach_fin_name = "<null/>";
        //                }
        //                if (doctype == "AF" || doctype == "EXP")
        //                {
        //                    alterHedDet.Ach_border_info_mode = "4";
        //                }
        //                else if (doctype == "LR" || doctype == "RE" || doctype == "TO" || doctype == "EX")
        //                {
        //                    alterHedDet.Ach_border_info_mode = "1";
        //                }
        //                else if (doctype == "BOI")
        //                {
        //                    alterHedDet.Ach_border_info_mode = "3";
        //                }
        //                alterHedDet.Ach_cal_working_mode = "0";
        //                if (doctype.ToUpper() == "AF" || doctype.ToUpper() == "LR")
        //                {
        //                    alterHedDet.Ach_manifest_ref_no = hederDtl.FINANCIAL_DOCUMENT_NO;
        //                }
        //                else if (doctype == "RE" || doctype == "EX" || doctype == "TO")
        //                {
        //                    alterHedDet.Ach_manifest_ref_no = "<null/>";
        //                }
        //                alterHedDet.Ach_terms_of_payment_desc = getTermsOfPaymentDescription(hederDtl.TERMS_OF_PAYMENT);
        //                if (doctype == "RE" || doctype == "TO" || doctype == "EX" || doctype == "AF" || doctype == "LR" || doctype == "EXP" || doctype == "BOI")
        //                {
        //                    alterHedDet.Ach_garentee_amt = 0;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_garentee_amt = hederDtl.TOTAL_GROSS_MASS;
        //                }

        //                alterHedDet.Ach_cur_name = "No foreign currency";
        //                if (doctype == "RE" || doctype == "LR" || doctype == "AF" || doctype == "EXP"
        //                    || doctype == "LR" || doctype == "EXP" || doctype == "BOI" || doctype == "EX" || doctype == "TO")
        //                {
        //                    alterHedDet.Ach_tot_cost = 0;
        //                }
        //                else
        //                {
        //                    alterHedDet.Ach_tot_cost = hederDtl.COST_AMOUNT;
        //                }
        //                alterHedDet.Ach_entry_desc = hederDtl.ENTRY_DESC;

        //                AsycudaDAL _asycudaDAL = new AsycudaDAL();
        //                bool returnval = _asycudaDAL.AddAlterDetails(alterHedDet);
        //            }
        //        }
        //        return alterHedDet;
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.Message.ToString();
        //        return alterHedDet;
        //    }
        //}

        private string getLocationOfgodsCd(string desc)
        {
            try
            {
                string code = "OTHER";
                switch (desc)
                {
                    case "Sri Lanka Ports Authority":
                        code = "SLPA";
                        break;
                    case "Sri Lankan Air Line":
                        code = "SLAL";
                        break;
                    case "South Asia Gateway Terminals":
                        code = "SAGT";
                        break;
                    case "Colombo International Container Terminals":
                        code = "CICT";
                        break;
                    case "Other":
                        code = "OTHER";
                        break;
                }
                return code;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get terms of payment description
        /// </summary>
        /// <param name="termsofpay">terms of payement code</param>
        /// <returns>string</returns>
        public string getTermsOfPaymentDescription(string termsofpay)
        {
            try
            {
                string desc = "";
                AsycudaDAL _asycudaDAL = new AsycudaDAL();
                desc = _asycudaDAL.getTermsOfPaymentFirst(termsofpay);
                //switch (termsofpay)
                //{
                //    case "10":
                //        desc = "Advanced payment";
                //        break;
                //    case "20":
                //        desc = "Consignment basic";
                //        break;
                //    case "31":
                //        desc = "Documents against acceptance (D/A)";
                //        break;
                //    case "35":
                //        desc = "Documents against payment (D/P)";
                //        break;
                //    case "41":
                //        desc = "Consignment";
                //        break;
                //    case "45":
                //        desc = "Contract basis";
                //        break;
                //    case "51":
                //        desc = "Lease agreement";
                //        break;
                //    case "55":
                //        desc = "Cash on arrival";
                //        break;
                //    case "61":
                //        desc = "L/C;Irrevokable cnfmd.sight docs.cr";
                //        break;
                //    case "65":
                //        desc = "Letter of credit at sight";
                //        break;
                //    case "70":
                //        desc = "Open Account";
                //        break;
                //    case "90":
                //        desc = "No foreign Exchange involved";
                //        break;
                //}
                return desc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get asycuda header details
        /// </summary>
        /// <param name="docno">document number</param>
        /// <param name="datasrcid">data source id</param>
        /// <param name="doctype">document type</param>
        /// <returns>ASY_HEADER_DTLS</returns>
        public ASY_HEADER_DTLS GetAsycudaHederDetails(string docno, int datasrcid, string doctype)
        {
            try
            {
                ASY_DB_SOURCE datasrc = GetDataSourceDetails(datasrcid);

                ASY_HEADER_DTLS hederDtl = null;

                _asycudaDAL = new AsycudaDAL(datasrc);
                hederDtl = _asycudaDAL.GetAsycudaHederDetails(docno);
                if (hederDtl != null)
                {
                    hederDtl.TOTAL_NUM_OF_FORMS = getTotalForms(hederDtl.ITM_QTY);
                    hederDtl.OFFICE_ENTRY_DESCRIPTION = GetOfficeOfEntryDescription(hederDtl.OFFICE_OF_ENTRY);
                    hederDtl.TYPE_OF_DECLARATION = hederDtl.DECLRENT_1 + hederDtl.DECLRENT_2;
                    hederDtl.EXPORTER_NAME = hederDtl.SUPPLIER_NAME + hederDtl.ADDRESS_LINE_1_EXPR3 + hederDtl.ADDRESS_LINE_2_EXPR4;
                    hederDtl.CONSIGNEE_CODE = hederDtl.TIN_NO.Replace(".", "").Replace(" ", "");
                    hederDtl.FINANCIAL_CODE = hederDtl.TIN_NO.Replace(".", "").Replace(" ", "");
                    hederDtl.FINANCIAL_NAME = hederDtl.COMPANY_NAME + hederDtl.ADDRESS_LINE1 + hederDtl.ADDRESS_LINE2;
                    hederDtl.LOCATION_OF_GODS_DESC = GetLocationofGoods(hederDtl.LOCATION_OF_GOODS);
                    hederDtl.FREIGHT_AMOUNT = GetTotalfreightAmount(docno, _asycudaDAL);
                    hederDtl.COST_AMOUNT = GetTotalCostAmount(docno, _asycudaDAL);
                    hederDtl.OTHER_AMOUNT = GetTotalOthereAmount(docno, _asycudaDAL);
                    if (doctype == "LR" || doctype == "AIR" || doctype == "EXP" || doctype == "MV")
                    {
                        hederDtl.TOTAL_AMOUNT = GetTotalAmount(docno, 0, _asycudaDAL);
                    }
                    else
                    {
                        hederDtl.TOTAL_AMOUNT = GetTotalAmount(docno, 1, _asycudaDAL);
                    }

                    hederDtl.TOTAL_FOB_AMOUNT = GetTotalFOBAmount(docno, _asycudaDAL);
                    hederDtl.TOTAL_GROSS_MASS = GetTotalGrossMass(docno, _asycudaDAL);
                    hederDtl.INSUARANCE_AMOUNT = GetTotalInsuaranceAmount(docno, _asycudaDAL);
                    if (doctype.ToUpper() == "BOI")
                        hederDtl.TOTAL_PAKAGE = getTotalPkgForBoi(_asycudaDAL, docno);
                }


                return hederDtl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get total package for boi
        /// </summary>
        /// <param name="_asycudaDAL">data access layer object</param>
        /// <param name="docno">document number</param>
        /// <returns>string</returns>
        private string getTotalPkgForBoi(AsycudaDAL _asycudaDAL, string docno)
        {
            try
            {
                return _asycudaDAL.getTotalPkgForBoi(docno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// check the document availability
        /// </summary>
        /// <param name="docno">document number</param>
        /// <returns>boolean</returns>
        private bool documentIsAvailable(string docno)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.documentIsAvailable(docno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// get the related database source details
        /// </summary>
        /// <param name="sourceId">database source id</param>
        /// <returns>ASY_DB_SOURCE</returns>
        public ASY_DB_SOURCE GetDataSourceDetails(int sourceId)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.GetDataSourceDetails(sourceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get the total number of forms
        /// </summary>
        /// <param name="totalQty">total quantity</param>
        /// <returns>decimal</returns>
        public decimal getTotalForms(decimal totalQty)
        {
            try
            {
                decimal numberOfForms = 1;
                if (totalQty > 1)
                {
                    numberOfForms += Math.Ceiling((totalQty - 1) / 3);
                }
                return numberOfForms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get the office of entry description
        /// </summary>
        /// <param name="OfficeOfEntryCode">office of entry code</param>
        /// <param name="_asycudaDAL">data access layer object</param>
        /// <returns>string</returns>
        public string GetOfficeOfEntryDescription(string OfficeOfEntryCode)
        {
            try
            {
                AsycudaDAL _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getOfficeOfEntryDescription(OfficeOfEntryCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get the item locations
        /// </summary>
        /// <param name="locationcode">location code</param>
        /// <param name="_asycudaDAL">data access layer object</param>
        /// <returns></returns>
        public string GetLocationofGoods(string locationcode)
        {
            try
            {
                AsycudaDAL _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getLocationofGoods(locationcode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// format the bank code
        /// </summary>
        /// <param name="bankcode">bank code</param>
        /// <returns>string </returns>
        public string GetBankCode(string bankcode)
        {
            try
            {
                if (!string.IsNullOrEmpty(bankcode))
                {
                    if (bankcode.Length >= 4)
                    {
                        string tempCode = bankcode.Substring(0, 4);
                        int result;
                        if (!int.TryParse(tempCode, out result))
                        {
                            return tempCode;
                        }
                        else
                        {
                            return null;
                        }

                    }
                    else
                    {
                        return bankcode;
                    }
                }
                else
                {

                    return bankcode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get name of the bank using bank code
        /// </summary>
        /// <param name="bnkcode">bank code</param>
        /// <param name="_asycudaDAL">data access layer object</param>
        /// <returns>string</returns>
        public string GetBankName(string bnkcode)
        {
            try
            {
                AsycudaDAL _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.GetBankName(bnkcode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get total amount of  freight
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <param name="_asycudaDAL"> data access layer object</param>
        /// <returns>decimal</returns>
        public decimal GetTotalfreightAmount(string docnum, AsycudaDAL _asycudaDAL)
        {
            try
            {
                return _asycudaDAL.GetTotalfreightAmount(docnum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get total amount of  insuarance
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <param name="_asycudaDAL"> data access layer object</param>
        /// <returns>decimal</returns>
        public decimal GetTotalInsuaranceAmount(string docnum, AsycudaDAL _asycudaDAL)
        {
            try
            {
                return _asycudaDAL.GetTotalInsuaranceAmount(docnum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///get total amount of  other
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <param name="_asycudaDAL"> data access layer object</param>
        /// <returns></returns>
        public decimal GetTotalOthereAmount(string docnum, AsycudaDAL _asycudaDAL)
        {
            try
            {
                return _asycudaDAL.GetTotalOthereAmount(docnum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// get total amount of  other
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <param name="_asycudaDAL">get total amount of  other</param>
        /// <returns>decimal</returns>
        public decimal GetTotalCostAmount(string docnum, AsycudaDAL _asycudaDAL)
        {
            try
            {
                return _asycudaDAL.GetTotalCostAmount(docnum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// get total amount of  insuarance/cose/other/freight
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <param name="_asycudaDAL"> data access layer object</param>
        /// <returns>decimal</returns>
        public decimal GetTotalAmount(string docnum, int num, AsycudaDAL _asycudaDAL)
        {
            try
            {
                return _asycudaDAL.GetTotalAmount(docnum, num);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get total amount of  insuarance/cose/other/freight
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <param name="_asycudaDAL"> data access layer object</param>
        /// <returns>decimel</returns>
        public decimal GetTotalFOBAmount(string docnum, AsycudaDAL _asycudaDAL)
        {
            try
            {
                return _asycudaDAL.GetTotalFOBAmount(docnum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get total gross mass for document
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <param name="_asycudaDAL"> AsycudaDAL object</param>
        /// <returns> decimal</returns>
        private decimal GetTotalGrossMass(string docnum, AsycudaDAL _asycudaDAL)
        {
            try
            {
                return _asycudaDAL.GetTotalGrossMass(docnum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get items for xml
        /// </summary>
        /// <param name="docno">document no</param>
        /// <param name="dbsrcId">database source id</param>
        /// <param name="asyType">asycuda xml tyoe</param>
        /// <param name="_altDet">hedaer details object</param>
        /// <returns> List<ASY_CUSDEC_ITM> </returns>
        public List<ASY_CUSDEC_ITM> GetItems(string docno, int dbsrcId, string asyType, ASY_ALT_HEADER_DTLS _altDet, out string error)
        {
            AsycudaDAL _asycudaDAL = new AsycudaDAL();
            error = "";
            List<ASY_CUSDEC_ITM> itemList = null;
            _asycudaDAL.ConnectionOpen();
            _asycudaDAL.BeginTransaction();
            List<ASY_CUSDEC_ITEM_DTLS_MODEL> oldItemList = null;
            try
            {
                itemList = _asycudaDAL.GetAltItems(docno);
                if (itemList == null)
                {
                    oldItemList = GetAsycudaItemsListDetails(docno, dbsrcId, asyType);
                    if (oldItemList != null)
                    {
                        int i = 1;
                        itemList = new List<ASY_CUSDEC_ITM>();
                        foreach (ASY_CUSDEC_ITEM_DTLS_MODEL item in oldItemList)
                        {
                            ASY_CUSDEC_ITM newItem = new ASY_CUSDEC_ITM();
                            newItem.Ach_doc_no = docno;
                            newItem.Aci_line = Int32.Parse(item.BOI_LINE_NO.ToString());
                            newItem.Aci_itm_cd = item.ITEM_CODE;
                            newItem.Aci_hs_cd = item.HS_CODE;
                            newItem.Aci_model = item.MODEL;
                            newItem.Aci_itm_desc = item.DESCRIPTION;
                            newItem.Aci_qty = item.BOND_QTY;
                            newItem.Aci_uom = item.UOM;
                            newItem.Aci_unit_cost = item.UNIT_COST;

                            //Dulaj 2018/Nov/13
                            newItem.Ach_to_bond_line_no = Convert.ToDecimal(item.CUI_OTH_DOC_LINE);
                            newItem.Ach_to_bond_no = item.OTHER_BOND_NO;
                            newItem.Ach_cus_dec_no = item.CUH_CUSDEC_ENTRY_NO;
                            //if (asyType == "TO" || asyType == "LR" || asyType == "EX")
                            //{
                            //    newItem.Aci_item_price = item.UNIT_COST * item.BOND_QTY;
                            //}
                            //else if (asyType == "RE" || asyType == "EX")
                            //{
                            //    if (item.ITEM_PRICE.ToString() == "")
                            //    {
                            //        newItem.Aci_item_price = 0;
                            //    }
                            //    else
                            //    {
                            //        newItem.Aci_item_price = item.ITEM_PRICE;
                            //    }
                            //}
                            newItem.Aci_item_price = getDocItemPrice(docno, item.ITEM_CODE, item.TO_BOND_ITEM_LINE_NO, _asycudaDAL);

                            newItem.Aci_gross_mass = item.GROSS_MASS;
                            newItem.Aci_net_mass = item.NET_MASS;
                            newItem.Aci_preferance = (checkPreference(item.PREFERANCE)) ? item.PREFERANCE : "N/A";
                            if (asyType == "RE" || asyType == "EX")
                            {
                                newItem.Aci_quota = ""; //"TO BOND LINE NO: " + item.TO_BOND_ITEM_LINE_NO.ToString();
                            }
                            else
                            {
                                newItem.Aci_quota = ".";
                            }

                            //if (asyType != "TO")
                            //{
                            //    newItem.Aci_bl_no = _altDet.Ach_voyage;
                            //}
                            //else
                            //{
                            newItem.Aci_bl_no = _altDet.Ach_proc_cd;
                            //}

                            newItem.Ach_othdoc1_no = "1";
                            newItem.Ach_othdoc1_line = 0;
                            newItem.Ach_othdoc2_no = "1";
                            newItem.Ach_othdoc2_line = 0;
                            if (asyType == "AIR" || asyType == "LR" || asyType == "MV")
                            {
                                //decimal _freightVal = 0;
                                //List<ImpCusdecCost> custCost = new List<ImpCusdecCost>();
                                //_financialDAL = new FinancialDAL();
                                //custCost = _financialDAL.GET_CUSDEC_COST_BY_DOC(docno);
                                //if (custCost != null)
                                //{
                                //    custCost = custCost.Where(k => k.Cus_ele_cat == "TOT").ToList();
                                //    foreach (ImpCusdecCost itm in custCost)
                                //    {
                                //        if (itm.Cus_ele_cd == "FRGT")
                                //        { itm.Cus_ele_cd_name = "Freight"; itm.Cus_line = 2; _freightVal = itm.Cus_amt; }
                                //    }
                                //}
                                //decimal tot = oldItemList.Sum(x => x.TOT_NUMOF_PKG);
                                //newItem.Ach_num_of_pkg = Math.Round((tot / _freightVal) * item.TOT_NUMOF_PKG,2);
                                newItem.Ach_num_of_pkg = item.TOT_NUMOF_PKG;
                            }
                            else
                            {
                                if (i == 1)
                                {
                                    newItem.Ach_num_of_pkg = decimal.Parse(_altDet.Ach_tot_pkg) - (oldItemList.Count - 1);
                                    //if (asyType == "RE" || asyType == "EX" && oldItemList.Count == 1)
                                    //{
                                    //    newItem.Ach_num_of_pkg = decimal.Parse(_altDet.Ach_tot_pkg);
                                    //}
                                    //else
                                    //{
                                    //    newItem.Ach_num_of_pkg = decimal.Parse(_altDet.Ach_tot_pkg) - (_altDet.Ach_items_qty + 1);
                                    //}
                                }
                                else
                                {
                                    newItem.Ach_num_of_pkg = 1;
                                }
                            }
                            string mk1 = ".";
                            //if (asyType == "EX" || asyType == "RE")
                            //{
                            //    //if (_altDet.Ach_bank_branch != "" || _altDet.Ach_bank_branch != "N/A") {
                            //    mk1 = _altDet.Ach_marks_and_no;
                            //    //}

                            //}
                            //else 

                            if (i == 1)
                            {
                                mk1 = _altDet.Ach_marks_and_no;
                            }
                            if (_altDet.Ach_com == "SGL" || _altDet.Ach_com == "SGD")
                            {
                                mk1 = item.TO_BOND_ITEM_LINE_NO.ToString();
                            }
                            newItem.Ach_mrk1_pkg = mk1;
                            MST_UOM_CATE pkg = getPackageDetails(_altDet.Ach_tot_pkg_unit, dbsrcId);

                            newItem.Ach_knd_of_pkg = pkg.MSUC_ASY_CD;//getPackegeCode(_altDet.Ach_tot_pkg_unit);
                            newItem.Ach_knd_of_pkg_name = pkg.MSUC_ASY_DESC;//getPackegeName(_altDet.Ach_tot_pkg_unit);
                            newItem.Aci_comd_cd = item.HS_CODE.Replace(".", "").Replace(" ", "").PadRight(8, '0');
                            newItem.Aci_prec_1 = "00";
                            newItem.Aci_prec_4 = i.ToString();
                            newItem.Ach_ext_cust_proc = _altDet.Ach_proc_id_1;
                            newItem.Ach_nat_cust_proc = _altDet.Ach_proc_id_2;
                            if (asyType == "EX" || asyType == "RE")
                            {
                                newItem.Ach_val_itm = (i == 1) ? "0+0+0+0-0" : "";
                            }
                            else if (asyType == "TO")
                            {
                                newItem.Ach_val_itm = (i == 1) ? "123,347+0+1,953+0-0" : "";
                            }

                            newItem.Ach_cnty_of_oregn = _altDet.Ach_orig_cnty_cd;
                            if (asyType == "TO" || asyType == "RE" || asyType == "EX")
                            {
                                newItem.Aci_desc_of_goods = item.BOND_QTY + " U " + item.DESCRIPTION;
                            }

                            newItem.Ach_hs_cd_desc = getHsgrpDesc(item.HS_CODE, dbsrcId);
                            newItem.Aci_itm_stat_val = item.ITEM_PRICE * _altDet.Ach_ex_rt;
                            newItem.Aci_tot_cost_itm = 0;
                            newItem.Aci_rte_of_adj = "1";
                            newItem.Ach_cur_cd = _altDet.Ach_cur_cd;
                            newItem.Aci_inv_nat_curr = item.ITEM_PRICE * _altDet.Ach_ex_rt;
                            newItem.Aci_inv_forgn_curr = getDocItemPrice(docno, item.ITEM_CODE,item.TO_BOND_ITEM_LINE_NO, _asycudaDAL); //item.ITEM_PRICE;
                            newItem.Aci_ext_fre_nat_curr = 0;
                            newItem.Aci_ext_fre_forgn_curr = 0;
                            newItem.Aci_int_fre_nat_curr = 0;
                            newItem.Aci_int_fre_forgn_curr = 0;
                            newItem.Aci_ins_nat_curr = 0;
                            newItem.Aci_ins_forgn_curr = 0;
                            newItem.Aci_oth_cst_nat_curr = 0;
                            newItem.Aci_oth_cst_forgn_curr = 0;
                            newItem.Aci_dedu_nat_curr = 0;
                            newItem.Aci_dedu_forgn_curr = 0;
                            newItem.Aci_curr_amnt = 0;
                            newItem.Ach_hs_cd_desc = getHsgrpDesc(item.HS_CODE, dbsrcId);
                            _asycudaDAL.AddXmlItems(newItem);
                            i++;
                            itemList.Add(newItem);
                        }

                    }
                    _asycudaDAL.TransactionCommit();
                }
                ASY_IMP_CUSDEC_HDR hederDtl = GetCusdecHdrDetails(docno, dbsrcId);
                if (itemList != null && (asyType == "LR" || asyType == "EXP" || asyType == "AIR" || asyType == "MV"))
                {
                    List<ASY_CUSDEC_HS_ITM> itemL = new List<ASY_CUSDEC_HS_ITM>();
                    itemL = _asycudaDAL.getHsGrpItems(docno, _altDet.Ach_main_hs);
                    List<ASY_CUSDEC_ITM> itemListAlt = new List<ASY_CUSDEC_ITM>();
                    int i = 1;
                    foreach (ASY_CUSDEC_HS_ITM itm in itemL)
                    {
                        List<ASY_CUSDEC_ITM_DET> itmDet = _asycudaDAL.getHsGrpItemsDet(docno, itm.Aci_hs_cd);
                        ASY_CUSDEC_ITM newIt = new ASY_CUSDEC_ITM();
                        string mod = itmDet[0].Aci_model;
                        newIt.Ach_doc_no = docno;


                        newIt.Aci_gross_mass = itm.Aci_gross_mass;
                        newIt.Aci_net_mass = itm.Aci_net_mass;
                        newIt.Aci_hs_cd = itm.Aci_hs_cd;
                        newIt.Aci_item_price = getHsItemsPrice(docno, itm.Aci_hs_cd);
                        if (asyType != "MV")
                        {
                            newIt.Aci_qty = itm.Aci_net_mass;
                        }
                        else
                        {
                            newIt.Aci_qty = itm.Aci_qty;
                        }
                        string mk1 = ".";
                        //if (i == 1)
                        //{
                        //    if (asyType == "AIR" || asyType == "LR" || asyType == "EXP")
                        //    {
                        //        mk1 = hederDtl.CUH_PPC_NO;
                        //    }
                        //    else
                        //    {
                        //        mk1 = newIt.Aci_bl_no;
                        //    }
                        //}
                        newIt.Ach_mrk1_pkg = mk1;
                        if (i == 1)
                        {
                            newIt.Ach_mrk1_pkg = _altDet.Ach_marks_and_no;
                        }
                        if (asyType == "AIR" || asyType == "LR" || asyType == "AIR" || asyType == "MV")
                        {
                            newIt.Aci_bl_no = hederDtl.CUH_PPC_NO;
                        }
                        else
                        {
                            newIt.Aci_bl_no = getAsyBlNo(itm.Aci_hs_cd, docno);
                        }
                        MST_UOM_CATE pkg = getPackageDetails(_altDet.Ach_tot_pkg_unit, dbsrcId);

                        newIt.Ach_knd_of_pkg = pkg.MSUC_ASY_CD;//getPackegeCode(hederDtl.CUH_TOT_PKG_UNIT);
                        newIt.Ach_knd_of_pkg_name = pkg.MSUC_ASY_DESC;//getPackegeName(hederDtl.CUH_TOT_PKG_UNIT);
                        newIt.Aci_comd_cd = itm.Aci_hs_cd.Replace(".", "").Replace(" ", "").PadRight(8, '0');
                        newIt.Aci_prec_1 = "00";
                        newIt.Ach_ext_cust_proc = _altDet.Ach_proc_id_1;
                        newIt.Ach_nat_cust_proc = _altDet.Ach_proc_id_2;
                        newIt.Ach_cnty_of_oregn = _altDet.Ach_orig_cnty_cd;
                        newIt.Aci_prec_4 = i.ToString();
                        newIt.Aci_model = "(" + itm.Aci_qty + " U)";
                        newIt.Ach_val_itm = (i == 1) ? "123,347+0+1,953+0-0" : "";

                        if (asyType == "LR" || asyType == "AIR" || asyType == "MV")
                        {
                            if (i == 1)
                            {
                                newIt.Aci_desc_of_goods = _altDet.Ach_remark;
                                newIt.Aci_model = itm.Aci_hs_cd_desc + " (" + itm.Aci_qty + " U)";
                            }
                            else
                            {
                                newIt.Aci_desc_of_goods = itm.Aci_hs_cd_desc;
                            }
                            if (asyType == "MV")
                            {
                                newIt.Aci_model = mod + " " + newIt.Aci_model;
                            }
                        }
                        else
                        {
                            newIt.Aci_desc_of_goods = itm.Aci_hs_cd_desc;
                        }
                        if (asyType == "AIR" || asyType == "LR" || asyType == "EXP" || asyType == "MV")
                        {
                            if (i == 1)
                            {
                                newIt.Aci_quota = hederDtl.CUH_CONTAINER_FCL;
                            }
                            else
                            {
                                newIt.Aci_quota = ".";
                            }
                        }
                        else
                        {
                            newIt.Aci_quota = ".";
                        }
                        if (asyType == "AIR" || asyType == "LR" || asyType == "MV")
                        {
                            List<ASY_CUSDEC_ITM> tot = itemList.Where(y => y.Aci_hs_cd == itm.Aci_hs_cd).ToList();

                            decimal costhst = _asycudaDAL.GET_CUSDEC_FRGHTBYHS(docno, itm.Aci_hs_cd);
                            decimal costALL = _asycudaDAL.GET_CUSDEC_FRGHTBYHS(docno, "");

                            newIt.Ach_num_of_pkg = Math.Round((tot[0].Ach_num_of_pkg / costALL) * costhst, 2);
                        }
                        else
                        {
                            newIt.Ach_num_of_pkg = getNumOfPackgesHsGroup(itm.Aci_hs_cd, docno);
                        }

                        newIt.Aci_itm_stat_val = getSatissticalValueforHsCode(itm.Aci_hs_cd, docno);
                        newIt.Aci_tot_cost_itm = 0;
                        newIt.Aci_rte_of_adj = "1";
                        newIt.Ach_cur_cd = _altDet.Ach_cur_cd;
                        newIt.Aci_inv_nat_curr = getSatissticalValueforHsCode(itm.Aci_hs_cd, docno);
                        newIt.Aci_inv_forgn_curr = itm.Aci_item_price;
                        newIt.Aci_ext_fre_nat_curr = 0;
                        newIt.Aci_ext_fre_forgn_curr = 0;
                        newIt.Aci_int_fre_nat_curr = 0;
                        newIt.Aci_int_fre_forgn_curr = 0;
                        newIt.Aci_ins_nat_curr = 0;
                        newIt.Aci_ins_forgn_curr = 0;
                        newIt.Aci_oth_cst_nat_curr = 0;
                        newIt.Aci_oth_cst_forgn_curr = 0;
                        newIt.Aci_dedu_nat_curr = 0;
                        newIt.Aci_dedu_forgn_curr = 0;
                        newIt.Aci_curr_amnt = 0;
                        itemListAlt.Add(newIt);
                        i++;
                    }
                    itemList = itemListAlt;
                }
                return itemList;
            }
            catch (Exception ex)
            {
                _asycudaDAL.TransactionRollback();
                _asycudaDAL.ConnectionClose();
                error = ex.Message.ToString();
                return itemList;
            }
        }

        private decimal getDocItemPrice(string docno, string itemCode,decimal itmline, AsycudaDAL _asycudaDAL)
        {
            try
            {
                return _asycudaDAL.getDocItemPrice(docno,itmline, itemCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get hs group description
        /// </summary>
        /// <param name="hscode">hs code</param>
        /// <param name="dbsrcId">database source id</param>
        /// <returns>string</returns>
        private string getHsgrpDesc(string hscode, int dbsrcId)
        {
            try
            {
                ASY_DB_SOURCE datasrc = GetDataSourceDetails(dbsrcId);
                _asycudaDAL = new AsycudaDAL(datasrc);
                string hscodedesc = _asycudaDAL.getHsgrpDesc(hscode);
                return hscodedesc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get asycuda bl no
        /// </summary>
        /// <param name="hscode">hs code</param>
        /// <param name="docno">document no</param>
        /// <returns>string</returns>
        private string getAsyBlNo(string hscode, string docno)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getAsyBlNo(hscode, docno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get statistical value for hs code
        /// </summary>
        /// <param name="hscode">hs code</param>
        /// <param name="docno">document no</param>
        /// <returns>decimal</returns>
        private decimal getSatissticalValueforHsCode(string hscode, string docno)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getSatissticalValueforHsCode(hscode, docno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get item price for hs group
        /// </summary>
        /// <param name="docno">document number</param>
        /// <param name="hscode">hs code</param>
        /// <returns>decimal</returns>
        private decimal getHsItemsPrice(string docno, string hscode)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getHsItemsPrice(hscode, docno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get number of package for hs code
        /// </summary>
        /// <param name="hscode">hs code</param>
        /// <param name="docno">document number</param>
        /// <returns>decimal</returns>
        private decimal getNumOfPackgesHsGroup(string hscode, string docno)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getNumOfPackgesHsGroup(hscode, docno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get the package code form name
        /// </summary>
        /// <param name="packageCode"> package name</param>
        /// <returns>string</returns>
        public string getPackegeCode(string packageCode)
        {
            string tempCode = "CT";
            //List<ASY_UOM_DET> Uom = getUOMDetails();
            //if (Uom.Count > 0) {
            //    tempCode = Uom.Where(a => a.MSUC_CD == packageCode).First().ToString();
            //}

            if (packageCode == "CT" || packageCode == "CTNS" || packageCode == "CTN")
            {
                return tempCode;
            }
            else if (packageCode == "PALLETS" || packageCode == "PX" || packageCode == "PKS")
            {
                return "PX";
            }
            else if (packageCode == "PKGS" || packageCode == "PK")
            {
                return "PK";
            }
            else if (packageCode == "CREATS" || packageCode == "CR")
            {
                return "CR";
            }
            else
            {
                return "";
            }
            return tempCode;
        }
        public MST_UOM_CATE getPackageDetails(string pkgCd, int datasrcid)
        {
            try
            {
                ASY_DB_SOURCE datasrc = GetDataSourceDetails(datasrcid);

                MST_UOM_CATE pkgDtl = new MST_UOM_CATE();

                _asycudaDAL = new AsycudaDAL(datasrc);
                pkgDtl = _asycudaDAL.GetPkgDetails(pkgCd);
                return pkgDtl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get package name from code
        /// </summary>
        /// <param name="packageCode">package code</param>
        /// <returns>string</returns>
        public string getPackegeName(string packageCode)
        {
            string tempCode = "Carton";
            if (packageCode == "CT" || packageCode == "CTNS" || packageCode == "CTN")
            {
                return tempCode;
            }
            else if (packageCode == "PALLETS" || packageCode == "PX" || packageCode == "PKS")
            {
                return "Pallet";
            }
            else if (packageCode == "PKGS" || packageCode == "PK")
            {
                return "Package";
            }
            else if (packageCode == "CREATS" || packageCode == "CR")
            {
                return "Crate";
            }
            else
            {
                return "";
            }
        }
        public List<ASY_UOM_DET> getUOMDetails()
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getUOMDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get asycuda item list details
        /// </summary>
        /// <param name="docno">document number</param>
        /// <param name="dbsrcId">database source id</param>
        /// <param name="asyType">asycuda document type</param>
        /// <returns> List<ASY_CUSDEC_ITEM_DTLS_MODEL> </returns>
        public List<ASY_CUSDEC_ITEM_DTLS_MODEL> GetAsycudaItemsListDetails(string docno, int dbsrcId, string asyType)
        {
            try
            {
                ASY_DB_SOURCE datasrc = GetDataSourceDetails(dbsrcId);
                _asycudaDAL = new AsycudaDAL(datasrc);
                List<ASY_CUSDEC_ITEM_DTLS_MODEL> _itemList = _asycudaDAL.GetAsycudaItemListDetails(docno);
                return _itemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get asycuda type from id
        /// </summary>
        /// <param name="typeid">type id</param>
        /// <param name="grpId">group id</param>
        /// <returns>string</returns>
        public string getAsyTypeCodefromId(int typeid, int grpId)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getAsyTypeCodefromId(typeid, grpId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get xml tag list
        /// </summary>
        /// <param name="parentid">parent tag id</param>
        /// <returns>List<ASY_XML_TAG></returns>
        public List<ASY_XML_TAG> getXmlTagList(decimal parentid)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getXmlTagList(parentid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get xml tag list for items
        /// </summary>get xml tag list for items
        /// <param name="parentid">parent tag id</param>
        /// <returns>List<ASY_XML_TAG></returns>
        public List<ASY_XML_TAG> getXmlTagListForItems(decimal parentid)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getXmlTagListForItems(parentid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// check document availabily from main source
        /// </summary>
        /// <param name="documentNo">document number</param>
        /// <param name="dbsrcId">database source id</param>
        /// <returns>boolean</returns>
        public bool CheckDocumentAvailability(string documentNo, int dbsrcId, string docType, out string error)
        {
            error = "";
            try
            {
                ASY_DB_SOURCE datasrc = GetDataSourceDetails(dbsrcId);
                _asycudaDAL = new AsycudaDAL(datasrc);
                bool result = _asycudaDAL.CheckDocumentAvailability(documentNo, docType);
                if (!result)
                {
                    error = "Invalid document type or document selected.";
                }
                return result;
            }

            catch (Exception ex)
            {
                error = ex.Message.ToString();
                return false;
            }

        }
        /// <summary>
        /// check preferences
        /// </summary>
        /// <param name="preferanceCode">preference code</param>
        /// <returns>boolean</returns>
        public bool checkPreference(string preferanceCode)
        {
            string[] arrayInvalid = new string[] { "NOS", "NDIA", "N/A", "398", ":", "0", ".", "RR", "1761.2359550561797752808988764", "INDIA", "22050", "BANGKOK", "BANKOOK", "ISFTS", "GGGGGG" };
            if (arrayInvalid.Contains(preferanceCode))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// get xml tag details
        /// </summary>
        /// <param name="type">type of tag list HED or ITM</param>
        /// <returns>string</returns>
        public string getDocumentXml(string type)
        {
            try
            {
                _asycudaDAL = new AsycudaDAL();
                return _asycudaDAL.getDocumentXml(type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get document details for search
        /// </summary>
        /// <param name="dbsrc">db source</param>
        /// <param name="pgeNum">page number</param>
        /// <param name="pgeSize">page size</param>
        /// <param name="searchFld">search field</param>
        /// <param name="searchVal">search value</param>
        /// <returns>List<ASY_DOC_SEARCH_HEAD></returns>
        public List<ASY_DOC_SEARCH_HEAD> getDocumentDetails(int dbsrc, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                ASY_DB_SOURCE datasrc = GetDataSourceDetails(dbsrc);
                _asycudaDAL = new AsycudaDAL(datasrc);
                List<ASY_DOC_SEARCH_HEAD> doclist = _asycudaDAL.getDocumentDetails(pgeNum, pgeSize, searchFld, searchVal);
                return doclist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get total document count
        /// </summary>
        /// <param name="dbsrc">database source</param>
        /// <returns>int</returns>
        //public int getDocumentDetailscCount(int dbsrc, string searchFld, string searchVal)
        //{
        //    ASY_DB_SOURCE datasrc = GetDataSourceDetails(dbsrc);
        //    _asycudaDAL = new AsycudaDAL(datasrc);
        //    string field = "";
        //    switch (searchFld)
        //    {
        //        case "Document No":
        //            field = "DOCUMENT_NO";
        //            break;
        //        case "Document Date":
        //            field = "DOCUMENT_DATE";
        //            break;
        //        case "Document Type":
        //            field = "DOCUMENT_TYPE";
        //            break;
        //        case "Location Of Gods":
        //            field = "LOCATION_OF_GOODS";
        //            break;
        //        case "Place of Loading":
        //            field = "PLACE_OF_LOADING";
        //            break;
        //        case "Procedure Code":
        //            field = "PROCEDUER_CODE";
        //            break;
        //    }
        //    int doccount = _asycudaDAL.getDocumentDetailscCount(field, searchVal);
        //    return doccount;
        //}
        public ASY_IMP_CUSDEC_HDR GetCusdecHdrDetails(string docNo, int datasrcid)
        {
            try
            {
                ASY_DB_SOURCE datasrc = GetDataSourceDetails(datasrcid);

                ASY_IMP_CUSDEC_HDR hederDtl = new ASY_IMP_CUSDEC_HDR();

                _asycudaDAL = new AsycudaDAL(datasrc);
                hederDtl = _asycudaDAL.GetCusdecHdrDetails(docNo);
                return hederDtl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MST_UOM_CATE getPackageDetailsforcode(string pkgCode)
        {
            AsycudaDAL _asycudaDAL = new AsycudaDAL();
            return _asycudaDAL.GetPkgDetails(pkgCode);

        }
        #endregion asycuda

        public int SendCostSheetEmail(string email, string id, string message, string host)
        {
            try
            {
                int outVal = 0;
                string securityToken = email + id + DateTime.Now.ToString();
                MD5 md5Hash = MD5.Create();
                string hash = GetMd5Hash(md5Hash, securityToken);
                if (hash.Length > 50)
                {
                    hash = hash.Substring(0, 50);
                }
                hash = hash.ToUpper();
                Send_SMTPMail(email, "Cost Item Details", message, id, hash, host, "");
                return outVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetUserLastLogTrans(string _com, string _userid, int _isFirst)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserLastLogTrans(_com, _userid, _isFirst);
        }

        public Int32 CheckMenuPermission(string _user, string _company, string _url)
        {
            int permission = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            permission = _securityDAL.CheckMenuPermission(_user, _company, _url);
            _securityDAL.ConnectionClose();
            return permission;
            //return 1;
        }
        public DataTable getCurrencyBreakDown(string docno)
        {
            _asycudaDAL = new AsycudaDAL();
            return _asycudaDAL.getCurrencyBreakDown(docno);
        }
        public Int32 CheckUrl(string _url)
        {
            Int32 _effect = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _effect = _securityDAL.CheckUrl(_url);
            _securityDAL.ConnectionClose();
            return _effect;
        }
        public Int32 getUserSpecialPermission(string userid, string company, string permcd, out string error)
        {
            error = "";
            Int32 eff = -1;
            try
            {
                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                eff = _securityDAL.getUserSpecialPermission(userid, company, permcd);
                _securityDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                eff = -1;
                error = ex.Message.ToString();
            }
            return eff;
        }
        public DataTable GetDocNoListForAsyCuda(string docno01, string docno02, string type)
        {
            _asycudaDAL = new AsycudaDAL();
            return _asycudaDAL.getDocNoListForAsyCuda(docno01, docno02, type);
        }
        public string GetMasterCompanyPath(string company)
        {
            string _exportPath = string.Empty;
            try
            {
                InventoryDAL _invDal = new InventoryDAL();
                _invDal.ConnectionOpen();
                MasterCompany mstCompany = _invDal.GetCompByCode(company);
                _exportPath = mstCompany.Mc_anal17.ToString();
                _invDal.ConnectionClose();
            }
            catch (Exception e)
            {
                _exportPath = string.Empty;
            }
            return _exportPath;
        }

        //Chamal 14-Jul-2018
        public DataTable GetSUNDBLinks(string _sunServer)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetSUNDBLinks(_sunServer);
        }

        //Chamal 18-Jul-2018
        public int ClearSunSession(string _sunID, string _sunDB, out string _outMsg)
        {
            try
            {
                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                int _ok = _securityDAL.ClearSunSession(_sunID, _sunDB);
                _securityDAL.TransactionCommit();
                _securityDAL.ConnectionClose();
                _outMsg = string.Empty;
                return _ok;
            }
            catch (Exception ex)
            {
                _outMsg = ex.Message.ToString();
                _securityDAL.TransactionRollback();
                _securityDAL.ConnectionClose();
                return 0;
            }
        }

        //Chamal 23-Jul-2018
        public bool IsLoginSunRemote(string _sunClientIP)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.IsLoginSunRemote(_sunClientIP);
        }
        public List<SEC_SYSTEM_MENU> getSystemUserMenu(string userid, string company, string system)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.getSystemUserMenu(userid, company, system);
        }
    }

}
