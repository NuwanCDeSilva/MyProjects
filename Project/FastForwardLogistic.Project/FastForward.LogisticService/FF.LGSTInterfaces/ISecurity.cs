using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FF.Interfaces
{
    [ServiceContract]
    public interface ISecurity
    {
        //Nuwan 2017-05-23
        [OperationContract]
        DateTime GetServerDateTime();
        //Nuwan 2017-05-23
        [OperationContract]
        double SaveLoginSession(string _userId, string _com, string _userIp, string _userPc, string _winLogName, string _winUser);
        //Nuwan 2017-05-23
        [OperationContract]
        List<SystemUserCompany> GetUserCompany(string UserComp);
        //Nuwan 2017-05-23
        [OperationContract]
        int Save_User_Falis(string _user, string _pw, string _com, string _ip, string _winusername, string _winuser);
        //Nuwan 2017-05-23
        [OperationContract]
        MasterLocation GetLocationByLocCode(string _CompCode, string _LocCode);
        // Commented by Chathura on 15-sep-2017
        //Nuwan 2017-05-23
        //[OperationContract]
        //List<SystemUserProf> GetUserProfCenters(string UserID, string Comp);
        //Nuwan 2017-05-23
        [OperationContract]
        MasterProfitCenter GetProfitCenter(string _company, string _profitCenter);
        //Dilshan 31/08/2017
        [OperationContract]
        MasterProfitCenter GetUserCompanySet(string _company, string _profitCenter);
        //Nuwan 2017-05-23
        [OperationContract(Name = "AddUserIdCompany")]
        List<SystemUserLoc> GetUserLoc(string UserID, string Comp);
        //Nuwan 2017-05-23
        [OperationContract]
        LoginUser LoginToSystem(string _userid, string _pw, string _com, string _verNo, string _ipAddress, string _hostName, int _inAttempts, out int _outAttempts, out int _status, out string _msg, out string _msgTitle);

        //nuwan 2017.06.30
        [OperationContract]
        int ExitLoginSession(string UserID, string Comp, string SessionID); 
        [OperationContract]
        List<SEC_SYSTEM_MENU> getUserMenu(string loginUser, string company, string supUsrCode);
        [OperationContract]
        SEC_SYSTEM_MENU getUserPermission(string userId,string company, Int32 menuId);
        [OperationContract]
        Boolean Is_OptionPerimitted(string userCompany, string userId, Int32 optionCode);
        [OperationContract]
        MasterCompany GetCompByCode(string _CompCode);
        [OperationContract]
        SystemUser GetUserByUserID(string _UserID);
        // Added by Chathura on 14-sep-2017
        [OperationContract]
        List<SystemUserProf> GetUserProfCenters(string UserID, string Comp, string User_def_chnl);

        [OperationContract]
        int Save_User_Roles(string company, string roleid, string rolename, string description, string createdby, string modifiedby, string choice, string active, string session, out string err);

        [OperationContract]
        int Save_User_Details(string p_log_user, string p_Se_usr_id, string p_Se_usr_desc, string p_Se_usr_name, string p_Se_usr_pw, string p_Se_usr_cat, string p_Se_dept_id, string p_Se_emp_id, string p_Se_emp_cd,
                     string p_Se_isdomain, string p_Se_iswinauthend, string p_Se_SUN_ID, string p_se_Email, string p_se_Mob, string p_se_Phone, string p_Se_act, string p_Se_ischange_pw, string p_Se_pw_mustchange,
                     string p_choice, string p_ispassword, string p_se_act_rmk, out string err);

        [OperationContract]
        int Remove_User_Company(List<string> ComList, List<string> UserList, out string err);

        [OperationContract]
        int Update_User_Company(string p_company, string p_userid, string p_isactive, string p_isdefault, string p_modeuser, string p_session, out string err);

        [OperationContract]
        List<SEC_SYSTEM_MENU> getMenuAll();

        [OperationContract]
        List<SEC_SYSTEM_MENU> getUserMenuByCompanyNRID(string roleid, string company);
        [OperationContract]
        int Update_Company_Menu_List(string ComList, string RoleIds, List<string> Optioidlist, List<string> ActiveStats, out string err);
    }
}
