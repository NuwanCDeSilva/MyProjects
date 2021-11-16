using FF.BusinessObjects.Security;
using FF.DataAccessLayer.BaseDAL;
using FF.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessLogicLayer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SecurityBLL : ISecurity
    {
        SecurityDAL _securityDAL;
        public DateTime GetServerDateTime()
        {
            return DateTime.Now;
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// save user login sessions
        /// </summary>
        /// <param name="_userId">login user id</param>
        /// <param name="_com">company</param>
        /// <param name="_userIp">user login ip</param>
        /// <param name="_userPc">profit center</param>
        /// <param name="_winLogName">windows log name</param>
        /// <param name="_winUser">windows log user</param>
        /// <returns>integer</returns>
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
        /// <summary>
        /// get user company
        /// added by nuwan  2017-05-23
        /// </summary>
        /// <param name="_UserComp">user company</param>
        /// <returns>User company list</returns>
        public List<SystemUserCompany> GetUserCompany(string _usrComp)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserCompany(_usrComp);
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// save user fail details
        /// </summary>
        /// <param name="_user">user id</param>
        /// <param name="_pw">password</param>
        /// <param name="_com">company</param>
        /// <param name="_ip">machine ip</param>
        /// <param name="_winusername">windows user name</param>
        /// <param name="_winuser">win user</param>
        /// <returns>integer</returns>
        public int Save_User_Falis(string _user, string _pw, string _com, string _ip, string _winusername, string _winuser)
        {
            int _save = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _save = _securityDAL.Save_User_Falis(_user, _pw, _com, _ip, _winusername, _winuser);
            _securityDAL.ConnectionClose();
            return _save;
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get location details
        /// </summary>
        /// <param name="_CompCode">company code</param>
        /// <param name="_LocCode">location code</param>
        /// <returns>location details</returns>
        public MasterLocation GetLocationByLocCode(string _CompCode, string _LocCode)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetLocationByLocCode(_CompCode, _LocCode);
        }

        // Added by Chathura on 14-sep-2017 commented below
        public List<SystemUserProf> GetUserProfCenters(string UserID, string Comp, string User_def_chnl)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserProfCenters(UserID, Comp, User_def_chnl);
        }

        /// <summary>
        /// added by nuwan  2017-05-23
        /// get user profit centers
        /// </summary>
        /// <param name="UserID">user id</param>
        /// <param name="Comp">company</param>
        /// <returns>profit center list</returns>
        //public List<SystemUserProf> GetUserProfCenters(string UserID, string Comp)
        //{
        //    _securityDAL = new SecurityDAL();
        //    return _securityDAL.GetUserProfCenters(UserID, Comp);
        //}
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get profit center details
        /// </summary>
        /// <param name="_company">company code</param>
        /// <param name="_profitCenter">profit center</param>
        /// <returns>profit center details</returns>
        public MasterProfitCenter GetProfitCenter(string _company, string _profitCenter)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetProfitCenter(_company, _profitCenter);
        }

        public MasterProfitCenter GetUserCompanySet(string _company, string _profitCenter)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserCompanySet(_company, _profitCenter);
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get user locations
        /// </summary>
        /// <param name="UserID">user id</param>
        /// <param name="Comp">company</param>
        /// <returns>user location list</returns>
        List<SystemUserLoc> ISecurity.GetUserLoc(string UserID, string Comp)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserLoc(UserID, Comp);
        }
        /// <summary>
        /// system login ethod
        /// </summary>
        /// <param name="_userid">user id</param>
        /// <param name="_pw">password</param>
        /// <param name="_com">company</param>
        /// <param name="_verNo">version number</param>
        /// <param name="_ipAddress">ip address</param>
        /// <param name="_hostName">host name</param>
        /// <param name="_inAttempts">syatem attempt count</param>
        /// <param name="_outAttempts">responce attempt count</param>
        /// <param name="_status">status</param>
        /// <param name="_msg">responce message</param>
        /// <param name="_msgTitle">message title</param>
        /// <returns></returns>
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
                    _status = -1;
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
                        _status = -1;
                        return _loginUser;
                    }

                    if (_systemuser.Se_act == -2)
                    {
                        _inAttempts = _inAttempts + 1;// increment retry counter
                        _outAttempts = _inAttempts;
                        _msg = "This user account permanently deactivated.\nPlease contact system administrator.";
                        _msgTitle = "Login Fail";
                        _status = -1;
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
                    List<MasterDepartment> _deptList = _securityDAL.GetDepartment();
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
                        _loginUser.Mst_com = _securityDAL.GetCompByCode(_com);
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
                    _loginUser.SE_LOG_AUTHO = _systemuser.Se_Log_Autho;//added by nuwanc 2017.07.06
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

                    // Added by Chathura on 14-Sep-2017
                    List<SystemUserChannel> _userChanels = _securityDAL.GetUserChannels(_userid, _com);
                    if (_userChanels != null)
                    {
                        var _userprofQuery =
                        from userChnl in _userChanels
                        //where userChnl.Sup_usr_id == _userid && userChnl.Sup_com_cd == _com && userChnl.Sup_def_pccd == true
                        select userChnl;
                        foreach (var _userChnl in _userprofQuery)
                        {
                            _loginUser.User_def_chnl = _userChnl.Msc_cd.ToString().ToUpper();
                            break;
                        }
                    }
                    else
                    {
                        _loginUser.User_def_chnl = "";
                    }

                    // Added by Chathura on 14-Sep-2017 commented below
                    List<SystemUserProf> _userprofs = _securityDAL.GetUserProfCenters(_userid, _com, _loginUser.User_def_chnl);
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
                    else
                    {
                        _loginUser.User_def_pc = "";
                    }

                    //List<SystemUserProf> _userprofs = _securityDAL.GetUserProfCenters(_userid, _com);
                    //if (_userprofs != null)
                    //{
                    //    var _userprofQuery =
                    //    from userProf in _userprofs
                    //    where userProf.Sup_usr_id == _userid && userProf.Sup_com_cd == _com && userProf.Sup_def_pccd == true
                    //    select userProf;
                    //    foreach (var _userprof in _userprofQuery)
                    //    {
                    //        _loginUser.User_def_pc = _userprof.Sup_pc_cd.ToString().ToUpper();
                    //        break;
                    //    }
                    //}

                    if (!string.IsNullOrEmpty(_loginUser.User_def_loc.ToString()))
                    {
                        MasterLocation _mstLoc = _securityDAL.GetLocationByLocCode(_com, _loginUser.User_def_loc.ToString());
                        if (_mstLoc != null)
                        {
                            _loginUser.Is_man_chk_loc = _mstLoc.Ml_is_chk_man_doc;
                            _loginUser.User_def_bin = _securityDAL.Get_default_binCD(_com, _loginUser.User_def_loc);
                        }
                    }
                    if (!string.IsNullOrEmpty(_loginUser.User_def_pc.ToString()))
                    {
                        _loginUser.Mst_pc = _securityDAL.GetProfitCenter(_com, _loginUser.User_def_pc.ToString());
                        if (_loginUser.Mst_pc != null)
                        {
                            _loginUser.Is_man_chk_pc = _loginUser.Mst_pc.Mpc_is_chk_man_doc;
                            _loginUser.Price_defn = _securityDAL.GetPriceDefinitionByBookAndLevel(_com, string.Empty, string.Empty, string.Empty, _loginUser.User_def_pc.ToString());
                            _loginUser.Chnl_defn = _securityDAL.GetChannelDetail(_com, _loginUser.Mst_pc.Mpc_chnl);
                        }
                    }

                    _loginUser.Is_sale_roundup = false;
                    DataTable _result = _securityDAL.IsSaleFigureRoundUp(_com);
                    if (_result != null && _result.Rows.Count > 0)
                    {
                        if (_result.Rows[0].Field<Int16>("MC_ANAL12") == 1) _loginUser.Is_sale_roundup = true;
                    }
                    _result = null;
                    if (!string.IsNullOrEmpty(_loginUser.User_def_pc.ToString()))
                    {
                        _result = _securityDAL.Get_PC_Hierarchy(_com, _loginUser.User_def_pc.ToString());
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


                    _loginUser.Com_itm_status = _securityDAL.GetAllCompanyStatus(_com);

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
        /// <summary>
        /// validate user details
        /// </summary>
        /// <param name="UserID">user id</param>
        /// <param name="UserInfor">user information</param>
        /// <returns>boolean</returns>
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
        public int ExitLoginSession(string UserID, string Comp, string SessionID)
        {
            int _eff = 0;
            _securityDAL = new SecurityDAL();
            _securityDAL.ConnectionOpen();
            _eff = _securityDAL.ExitLoginSession(UserID, Comp, SessionID);
            _securityDAL.ConnectionClose();
            return _eff;
        }
        public List<SEC_SYSTEM_MENU> getUserMenu(string loginUser, string company, string supUsrCode)
        {
            _securityDAL = new SecurityDAL();
            List<SEC_SYSTEM_MENU> menu = new List<SEC_SYSTEM_MENU>();
            List<SEC_SYS_ROLE> userRole = new List<SEC_SYS_ROLE>();
            userRole = _securityDAL.getUserRole(loginUser, company);
            bool exists = userRole.Exists(x => x.SSRR_ROLENAME == supUsrCode);
            string userRoleIds = "";
            if (!exists)
            {
                foreach (SEC_SYS_ROLE usr in userRole)
                {
                    userRoleIds = usr.SSRR_ROLEID + ",";
                }
                userRoleIds = userRoleIds.Remove(userRoleIds.Length - 1);
            }

            menu = _securityDAL.getUserMenus(userRoleIds, exists, company);

            return menu;
        }
        public SEC_SYSTEM_MENU getUserPermission(string userId, string company, Int32 menuId)
        {
            _securityDAL = new SecurityDAL();
            SEC_SYSTEM_MENU menu = new SEC_SYSTEM_MENU();
            List<SEC_SYS_ROLE> userRole = new List<SEC_SYS_ROLE>();
            userRole = _securityDAL.getUserRole(userId, company);
            bool exists = userRole.Exists(x => x.SSRR_ROLENAME == "SUPUSR");
            string userRoleIds = "";
            if (!exists)
            {
                foreach (SEC_SYS_ROLE usr in userRole)
                {
                    userRoleIds = usr.SSRR_ROLEID + ",";
                }
                userRoleIds = userRoleIds.Remove(userRoleIds.Length - 1);
            }

            menu = _securityDAL.getUserMenusPermission(userRoleIds, exists, company, menuId);

            return menu;
        }

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
        public MasterCompany GetCompByCode(string _CompCode)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetCompByCode(_CompCode);
        }
        public SystemUser GetUserByUserID(string UserID)
        {
            _securityDAL = new SecurityDAL();
            return _securityDAL.GetUserByUserID(UserID);
        }


        public int Save_User_Roles(string company, string roleid, string rolename, string description, string createdby, string modifiedby, string choice, string active, string session, out string err)
        {
            int val = 0;
            err = "";
            err = string.Empty;

            try
            {
                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                val = _securityDAL.Save_User_Roles(company, roleid, rolename, description, createdby, modifiedby, choice, active, session);
                _securityDAL.TransactionCommit();
                _securityDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                _securityDAL.TransactionRollback();
                err = ex.Message;
            }
            return val;
        }

        public int Save_User_Details(string p_log_user, string p_Se_usr_id, string p_Se_usr_desc, string p_Se_usr_name, string p_Se_usr_pw, string p_Se_usr_cat, string p_Se_dept_id, string p_Se_emp_id, string p_Se_emp_cd,
                             string p_Se_isdomain, string p_Se_iswinauthend, string p_Se_SUN_ID, string p_se_Email, string p_se_Mob, string p_se_Phone, string p_Se_act, string p_Se_ischange_pw, string p_Se_pw_mustchange,
                             string p_choice, string p_ispassword, string p_se_act_rmk, out string err)
        {
            int val = 0;
            err = "";
            err = string.Empty;
            try
            {
                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                val = _securityDAL.Save_User_Details(p_log_user, p_Se_usr_id, p_Se_usr_desc, p_Se_usr_name, p_Se_usr_pw, p_Se_usr_cat, p_Se_dept_id, p_Se_emp_id, p_Se_emp_cd,
                                                   p_Se_isdomain, p_Se_iswinauthend, p_Se_SUN_ID, p_se_Email, p_se_Mob, p_se_Phone, p_Se_act, p_Se_ischange_pw, p_Se_pw_mustchange, p_choice, p_ispassword, p_se_act_rmk);
                _securityDAL.TransactionCommit();
                _securityDAL.ConnectionClose();


            }
            catch (Exception ex)
            {
                _securityDAL.TransactionRollback();
                err = ex.Message;
            }
            return val;

        }

        public int Remove_User_Company(List<string> ComList, List<string> UserList, out string err)
        {
            int val = 0;
            err = "";
            err = string.Empty;
            try
            {
                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                for (int i = 0; i < ComList.Count; i++)
                {
                    val = _securityDAL.Remove_User_Company(ComList[i].ToString(), UserList[i].ToString());
                }
                _securityDAL.TransactionCommit();
                _securityDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                _securityDAL.TransactionRollback();
                err = ex.Message;
            }
            return val;
        }

        public int Update_User_Company(string p_company, string p_userid, string p_isactive, string p_isdefault, string p_modeuser, string p_session, out string err)
        {
            int val = 0;
            err = "";
            err = string.Empty;
            try
            {
                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                val = _securityDAL.Update_User_Company(p_company, p_userid, p_isactive, p_isdefault, p_modeuser, p_session);
                _securityDAL.TransactionCommit();
                _securityDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                _securityDAL.TransactionRollback();
                err = ex.Message;
            }
            return val;
        }

        public List<SEC_SYSTEM_MENU> getMenuAll()
        {
            _securityDAL = new SecurityDAL();
            List<SEC_SYSTEM_MENU> menu = new List<SEC_SYSTEM_MENU>();

            menu = _securityDAL.getMenusAll();

            return menu;
        }

        public List<SEC_SYSTEM_MENU> getUserMenuByCompanyNRID(string roleid, string company)
        {
            _securityDAL = new SecurityDAL();
            List<SEC_SYSTEM_MENU> menu = new List<SEC_SYSTEM_MENU>();

            menu = _securityDAL.getUserMenus(roleid, false, company);

            return menu;
        }

        public int Update_Company_Menu_List(string ComList, string RoleIds, List<string> Optioidlist, List<string> ActiveStats, out string err)
        {
            int val = 0;
            err = "";
            err = string.Empty;
            try
            {
                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();
                for (int i = 0; i < Optioidlist.Count; i++)
                {
                    val = _securityDAL.Update_Company_Menu_List(ComList, RoleIds, Optioidlist[i].ToString(), ActiveStats[i].ToString());
                }
                _securityDAL.TransactionCommit();
                _securityDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                _securityDAL.TransactionRollback();
                err = ex.Message;
            }
            return val;
        }
    }
}