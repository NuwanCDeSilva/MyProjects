using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Oracle.DataAccess.Client;
using FF.BusinessObjects;
using System.Security.Cryptography;
using System.IO;
using FF.BusinessObjects.Services;
using FF.BusinessObjects.Account;

namespace FF.DataAccessLayer
{
    public class SecurityDAL : BaseDAL
    {
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        #region System Users
        public List<SystemUser> GetSystemUsers()
        {
            List<SystemUser> _userList = null;

            DataTable _dtResults = QueryDataTable("tblSystemUsers", "SP_GETUSERS", CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUser>(_dtResults, SystemUser.converter);
            }
            return _userList;
        }

        public SystemUser GetUserByUserID(string _UserID)
        {
            SystemUser _userList = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserID;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUser", "SP_GET_USER_BY_USERID", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUser>(_dtResults, SystemUser.converter)[0];
            }
            if (_userList != null)
            {
                string _pw = Decrypt(_userList.Se_usr_pw);
                _userList.Se_usr_pw = _pw;
            }

            return _userList;
        }

        public List<SystemRole> GetRoleByCompany(string _Comp, int? _isActive)
        {
            List<SystemRole> _userList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_comp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Comp;
            (param[1] = new OracleParameter("p_active", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _isActive;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRoel", "sp_get_role_by_company", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemRole>(_dtResults, SystemRole.ConverterNew);
            }
            return _userList;

        }

        public int DeleteUserComp(string UserID, string Comp)
        {
            int row_aff = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
                (param[1] = new OracleParameter("p_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;


                ConnectionOpen();
                row_aff = UpdateRecords("sp_del_user_comp", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {

            }
            finally
            {
                ConnectionClose();
            }
            return row_aff;

        }

        public int DeleteUserRole(string UserID, string Comp, int Role)
        {
            int row_aff = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_ser_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
                (param[1] = new OracleParameter("p_ser_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
                (param[2] = new OracleParameter("p_ser_role_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Role;

                ConnectionOpen();
                row_aff = UpdateRecords("sp_del_user_role", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {

            }
            finally
            {
                ConnectionClose();
            }
            return row_aff;

        }

        public int DeleteUserPC(string UserID, string Comp, string PC)
        {
            int row_aff = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_sel_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
                (param[1] = new OracleParameter("p_sel_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
                (param[2] = new OracleParameter("p_sel_loc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;

                ConnectionOpen();
                row_aff = UpdateRecords("sp_del_user_pc", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {

            }
            finally
            {
                ConnectionClose();
            }
            return row_aff;

        }

        public int DeleteUserLoc(string UserID, string Comp, string Loc)
        {
            int row_aff = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_sel_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
                (param[1] = new OracleParameter("p_sel_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
                (param[2] = new OracleParameter("p_sel_loc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;

                ConnectionOpen();
                row_aff = UpdateRecords("sp_del_user_loc", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {

            }
            finally
            {
                ConnectionClose();
            }
            return row_aff;

        }

        public int UpdateSystemUserCompany(SystemUserCompany _userComp)
        {
            int rows_aff = 0;
            //try
            //{
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userComp.SEC_USR_ID;
            (param[1] = new OracleParameter("p_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userComp.SEC_COM_CD;
            (param[2] = new OracleParameter("p_def_comcd", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userComp.SEC_DEF_COMCD;
            (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userComp.SEC_ACT;
            (param[4] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userComp.SEC_CRE_BY;
            (param[5] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userComp.SEC_MOD_BY;
            (param[6] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userComp.SEC_SESSION_ID;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            ConnectionOpen();
            rows_aff = UpdateRecords("sp_update_user_company", CommandType.StoredProcedure, param);

            //}
            //catch (Exception e)
            //{
            //    rows_aff = -1;
            //}
            //finally
            //{
            //    ConnectionClose();
            //}

            return rows_aff;
        }

        public int UpdateSystemUserLoc(SystemUserLoc _userLoc)
        {
            int rows_aff = 0;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_sel_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userLoc.SEL_USR_ID;
            (param[1] = new OracleParameter("p_sel_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userLoc.SEL_COM_CD;
            (param[2] = new OracleParameter("p_sel_loc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userLoc.SEL_LOC_CD;
            (param[3] = new OracleParameter("p_sel_def_loccd", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userLoc.SEL_DEF_LOCCD;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            ConnectionOpen();
            rows_aff = UpdateRecords("sp_update_user_loc", CommandType.StoredProcedure, param);
            ConnectionClose();
            return rows_aff;
        }

        public int UpdateSystemUserPC(SystemUserProf _userPC)
        {
            int rows_aff = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_SUP_USR_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userPC.Sup_usr_id;
                (param[1] = new OracleParameter("p_SUP_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userPC.Sup_com_cd;
                (param[2] = new OracleParameter("p_SUP_PC_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userPC.Sup_pc_cd;
                (param[3] = new OracleParameter("p_SUP_DEF_PCCD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userPC.Sup_def_pccd;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                ConnectionOpen();
                rows_aff = UpdateRecords("sp_update_user_pc", CommandType.StoredProcedure, param);

            }
            catch (Exception e)
            {
                rows_aff = -1;
            }
            finally
            {
                ConnectionClose();
            }

            return rows_aff;
        }

        public int SaveSystemUserRole(SystemUserRole _userRole)
        {
            int rows_aff = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_ser_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.SER_USR_ID;
                (param[1] = new OracleParameter("p_ser_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.SER_COM_CD;
                (param[2] = new OracleParameter("p_ser_role_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.SER_ROLE_ID;
                param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                ConnectionOpen();
                rows_aff = UpdateRecords("sp_update_user_role", CommandType.StoredProcedure, param);

            }
            catch (Exception e)
            {
                rows_aff = -1;
            }
            finally
            {
                ConnectionClose();
            }

            return rows_aff;
        }

        public Int16 SaveNewUser(SystemUser _usrNew)
        {
            OracleParameter[] param = new OracleParameter[28];
            string _pw = Encrypt(_usrNew.Se_usr_pw);
            (param[0] = new OracleParameter("p_usrid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_usr_id;
            (param[1] = new OracleParameter("p_usrpw", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pw;
            (param[2] = new OracleParameter("p_usrname", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_usr_name;
            (param[3] = new OracleParameter("p_usrdesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_usr_desc;
            (param[4] = new OracleParameter("p_usrcat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_usr_cat;
            (param[5] = new OracleParameter("p_depid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_dept_id;
            (param[6] = new OracleParameter("p_empid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_emp_id;
            (param[7] = new OracleParameter("p_domainid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_domain_id;
            (param[8] = new OracleParameter("p_isadhoc", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_isadhoc;
            (param[9] = new OracleParameter("p_isdomain", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_isdomain;
            (param[10] = new OracleParameter("p_iswin", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_iswinauthend;
            (param[11] = new OracleParameter("p_noofdays", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_noofdays;
            (param[12] = new OracleParameter("p_mindays", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_mindays;
            (param[13] = new OracleParameter("p_sessionperiod", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_sessionperiod;
            (param[14] = new OracleParameter("p_ischangepw", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_ischange_pw;
            (param[15] = new OracleParameter("p_ispwexpire", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_pw_expire;
            (param[16] = new OracleParameter("p_pwchange", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_pw_mustchange;
            (param[17] = new OracleParameter("p_active", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_act;
            (param[18] = new OracleParameter("p_creby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_cre_by;
            (param[19] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_mod_by;
            (param[20] = new OracleParameter("p_sessionid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_session_id;
            (param[21] = new OracleParameter("p_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.se_Email;
            (param[22] = new OracleParameter("p_mob", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.se_Mob;
            (param[23] = new OracleParameter("p_phone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.se_Phone;
            (param[24] = new OracleParameter("p_sunid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_SUN_ID;
            (param[25] = new OracleParameter("p_se_emp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_emp_cd;
            (param[26] = new OracleParameter("P_se_act_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_act_rmk;
            param[27] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("sp_savenewuser", CommandType.StoredProcedure, param);
        }

        //Darshana 03/05/2012
        public Int16 UpdateUser(SystemUser _usrNew)
        {
            //Create parameters and assign values.
            OracleParameter[] param = new OracleParameter[28];
            string _pw = Encrypt(_usrNew.Se_usr_pw);
            (param[0] = new OracleParameter("p_usrid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_usr_id;
            (param[1] = new OracleParameter("p_usrpw", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pw;
            (param[2] = new OracleParameter("p_usrname", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_usr_name;
            (param[3] = new OracleParameter("p_usrdesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_usr_desc;
            (param[4] = new OracleParameter("p_usrcat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_usr_cat;
            (param[5] = new OracleParameter("p_depid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_dept_id;
            (param[6] = new OracleParameter("p_empid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_emp_id;
            (param[7] = new OracleParameter("p_domainid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_domain_id;
            (param[8] = new OracleParameter("p_isadhoc", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_isadhoc;
            (param[9] = new OracleParameter("p_isdomain", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_isdomain;
            (param[10] = new OracleParameter("p_iswin", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_iswinauthend;
            (param[11] = new OracleParameter("p_noofdays", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_noofdays;
            (param[12] = new OracleParameter("p_mindays", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_mindays;
            (param[13] = new OracleParameter("p_sessionperiod", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_sessionperiod;
            (param[14] = new OracleParameter("p_ischangepw", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_ischange_pw;
            (param[15] = new OracleParameter("p_ispwexpire", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_pw_expire;
            (param[16] = new OracleParameter("p_pwchange", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_pw_mustchange;
            (param[17] = new OracleParameter("p_active", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _usrNew.Se_act;
            (param[18] = new OracleParameter("p_creby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_cre_by;
            (param[19] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_mod_by;
            (param[20] = new OracleParameter("p_sessionid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_session_id;
            (param[21] = new OracleParameter("p_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.se_Email;
            (param[22] = new OracleParameter("p_mob", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.se_Mob;
            (param[23] = new OracleParameter("p_phone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.se_Phone;
            (param[24] = new OracleParameter("p_sunid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_SUN_ID;
            (param[25] = new OracleParameter("p_se_emp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_emp_cd;
            (param[26] = new OracleParameter("P_se_act_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usrNew.Se_act_rmk;
            param[27] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("sp_savenewuser", CommandType.StoredProcedure, param);
        }

        //Chamal 19-03-2012
        public double SaveLoginSession(string _userId, string _com, string _userIp, string _userPc, string _winLogName, string _winUser)
        {
            double row_aff = 0;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userId;
            (param[1] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("p_ip", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userIp;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userPc;
            (param[4] = new OracleParameter("p_wname", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _winLogName;//Chamal 24-09-2014
            (param[5] = new OracleParameter("p_wuser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _winUser;//Chamal 24-09-2014
            param[6] = new OracleParameter("o_effect", OracleDbType.Double, null, ParameterDirection.Output);
            row_aff = UpdateRecords("sp_saveloginsession", CommandType.StoredProcedure, param);
            return row_aff;
        }

        //Chamal 18-05-2013
        public int ExitLoginSession(string UserID, string Comp, string SessionID)
        {
            int eff = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            (param[2] = new OracleParameter("p_sessionid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = SessionID;
            param[3] = new OracleParameter("o_effect", OracleDbType.Double, null, ParameterDirection.Output);
            eff = UpdateRecords("sp_exitloginsession", CommandType.StoredProcedure, param);
            return eff;
        }

        public Boolean IsActiveSessions(string UserID, string Comp, out string _ip, out string _pc, out string _lastlogdate)
        {
            bool _rtn = false;
            string p_ip = string.Empty;
            string p_pc = string.Empty;
            string p_lastlogdate = string.Empty;

            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (_para[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            //return QueryDataTable("tblItem", "sp_isactivesessions", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            DataTable _dt = QueryDataTable("tblItem", "sp_isactivesessions", CommandType.StoredProcedure, false, _para);
            if (_dt.Rows.Count > 0)
            {
                _rtn = true;
                foreach (DataRow _dr in _dt.Rows)
                {
                    p_ip = _dr["SUT_USR_IP"].ToString();
                    p_ip = p_ip + "\n" + " * Logged Domain        :  " + _dr["sut_win_log_usr"].ToString();
                    p_pc = _dr["SUT_USR_PC"].ToString();
                    p_lastlogdate = _dr["SUT_LOG_ON"].ToString();

                }
            }
            _ip = p_ip;
            _pc = p_pc;
            _lastlogdate = p_lastlogdate;
            return _rtn;
        }

        /// <summary>
        /// get Active session detail by DULANGA 2017-2-9
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Com"></param>
        /// <returns></returns>
        public DataTable getActiveSessionInfo(string UserID, string Com)
        {

            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (_para[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dt = QueryDataTable("tblItem", "sp_isactivesessions", CommandType.StoredProcedure, false, _para);
            return _dt;
        }

        public Boolean IsSessionExpired(string _sessionID, string _userID, string _comp, out string _msg)
        {
            bool _rtn = false;
            string p_ip = string.Empty;
            string p_pc = string.Empty;
            string p_user = string.Empty;
            string p_lastlogdate = string.Empty;

            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (_para[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _comp;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            //return QueryDataTable("tblItem", "sp_isactivesessions", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            DataTable _dt = QueryDataTable("tblItem", "sp_isactivesessions", CommandType.StoredProcedure, false, _para);
            if (_dt.Rows.Count > 0)
            {
                _rtn = true;
                foreach (DataRow _dr in _dt.Rows)
                {
                    p_ip = _dr["SUT_USR_IP"].ToString();
                    p_pc = _dr["SUT_USR_PC"].ToString();
                    p_user = _dr["sut_win_log_usr"].ToString();
                    p_lastlogdate = _dr["SUT_LOG_ON"].ToString();

                }
            }
            if (_rtn == true)
            {
                _msg = "Your current session has been closed by domain user " + p_user + " [ " + p_ip + " | " + p_pc + " ] on " + p_lastlogdate;
            }
            else
            {
                _msg = "Your current session is expired or has been closed by administrator!";
            }


            _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("p_session", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sessionID;
            (_para[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (_para[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _comp;
            _para[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblItem", "sp_isexpiredsession", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;
        }

        //kapila 22/3/2012
        public SystemUserCompany GetAssignedUserCompany(string _userComp, string _userID)
        {
            SystemUserCompany _assignCompList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userComp;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAssComp", "sp_get_usercomp_by_usercomp", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _assignCompList = DataTableExtensions.ToGenericList<SystemUserCompany>(_dtResults, SystemUserCompany.converter)[0];
            }
            return _assignCompList;
        }

        //kapila 23/3/2012
        public SystemUserLoc GetAssignedUserLocation(string _userID, string _userComp, string _Loc)
        {
            SystemUserLoc _assignLocList = null;


            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userComp;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAssLoc", "sp_get_assign_user_loc", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _assignLocList = DataTableExtensions.ToGenericList<SystemUserLoc>(_dtResults, SystemUserLoc.converter)[0];
            }
            return _assignLocList;
        }

        //kapila 24/3/2012
        public Int16 Check_User_Def_Comp(string _userID)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAssComp", "sp_chk_is_usr_def_comp", CommandType.StoredProcedure, false, param);

            Int16 _isDefCompFound = 0;
            if (_dtResults.Rows.Count > 0)
            {
                _isDefCompFound = 1;
            }
            //ConnectionOpen();
            //Int16 _isDefCompFound = Convert.ToInt16(QueryFunction("fn_chk_is_usr_def_comp", CommandType.StoredProcedure, param));
            //ConnectionClose();

            return _isDefCompFound;
        }

        public Int16 Check_User_Def_PC(string _userID, string _com)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAssLoc", "sp_chk_is_usr_def_pc", CommandType.StoredProcedure, false, param);

            Int16 _isDefLocFound = 0;
            if (_dtResults.Rows.Count > 0)
            {
                _isDefLocFound = 1;
            }

            return _isDefLocFound;
        }

        //kapila 26/3/2012
        public Int16 Check_User_Def_Loc(string _userID, string _com)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAssLoc", "sp_chk_is_usr_def_loc", CommandType.StoredProcedure, false, param);

            Int16 _isDefLocFound = 0;
            if (_dtResults.Rows.Count > 0)
            {
                _isDefLocFound = 1;
            }

            return _isDefLocFound;
        }

        public List<SystemUserProf> GetUserPC(string _UserID)
        {
            List<SystemUserProf> _userList = null;
            SystemUserProf _systemUserLoc = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserID;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserLoc", "SP_GETUSERPC", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = new List<SystemUserProf>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _systemUserLoc = new SystemUserProf();

                    _systemUserLoc.Sup_com_cd = _dtResults.Rows[i]["SUP_COM_CD"].ToString();
                    _systemUserLoc.Sup_def_pccd = Convert.ToBoolean(_dtResults.Rows[i]["SUP_DEF_PCCD"]);
                    _systemUserLoc.Sup_pc_cd = _dtResults.Rows[i]["SUP_PC_CD"].ToString();
                    _systemUserLoc.Sup_usr_id = _dtResults.Rows[i]["SUP_USR_ID"].ToString();

                    _systemUserLoc.MasterPC = new MasterProfitCenter()
                    {
                        Mpc_desc = _dtResults.Rows[i]["mpc_desc"].ToString()
                    };

                    _userList.Add(_systemUserLoc);
                }
            }
            return _userList;
        }
        #endregion

        #region System User Roles
        public List<SystemUserRole> GetUserRole(string _UserRole)
        {
            List<SystemUserRole> _userList = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserRole;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserRole", "SP_GETUSERROLE", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUserRole>(_dtResults, SystemUserRole.converter);
            }
            return _userList;
        }
        #endregion

        #region System User Locations
        public List<SystemUserLoc> GetUserLoc(string _UserID)
        {
            List<SystemUserLoc> _userList = null;
            SystemUserLoc _systemUserLoc = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserID;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserLoc", "SP_GETUSERLOC", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = new List<SystemUserLoc>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _systemUserLoc = new SystemUserLoc();

                    _systemUserLoc.SEL_COM_CD = _dtResults.Rows[i]["SEL_COM_CD"].ToString();
                    _systemUserLoc.SEL_DEF_LOCCD = Convert.ToInt32(_dtResults.Rows[i]["SEL_DEF_LOCCD"]);
                    _systemUserLoc.SEL_LOC_CD = _dtResults.Rows[i]["SEL_LOC_CD"].ToString();
                    _systemUserLoc.SEL_USR_ID = _dtResults.Rows[i]["SEL_USR_ID"].ToString();

                    _systemUserLoc.MasterLoc = new MasterLocation()
                    {
                        Ml_loc_desc = _dtResults.Rows[i]["Ml_loc_desc"].ToString()
                    };

                    _userList.Add(_systemUserLoc);
                }
            }
            return _userList;
        }

        //Chamal 20-03-2012
        public List<SystemUserLoc> GetUserLoc(string UserID, string Comp)
        {
            List<SystemUserLoc> _userList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserLoc", "SP_GETUSERLOC", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUserLoc>(_dtResults, SystemUserLoc.converter);
            }
            return _userList;
        }

        //Written By Prabhath on 05/05/2012
        public List<SystemUserLoc> GetUserLocList(string _userID, string _company, string _location)
        {
            List<SystemUserLoc> _userList = null;

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserLoc", "sp_getuserlocbylocationcode", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUserLoc>(_dtResults, SystemUserLoc.convertWithDescription);
            }
            return _userList;
        }

        //Written By Prabhath on 05/05/2012
        public DataTable GetUserLocTable(string _userID, string _company, string _location)
        {

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserLoc", "sp_getuserlocbylocationcode", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        #endregion

        #region System User Profit Centers
        //Chamal 20-03-2012
        public List<SystemUserProf> GetUserProfCenters(string UserID, string Comp)
        {
            List<SystemUserProf> _userList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (param[1] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserProf", "SP_GETUSERPROF", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUserProf>(_dtResults, SystemUserProf.Converter);
            }
            return _userList;
        }
        #endregion

        #region system user company
        public List<SystemUserCompany> GetUserCompany(string _UserComp)
        {
            List<SystemUserCompany> _userCompList = null;
            SystemUserCompany _systemUserComp = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserComp;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserComp", "SP_GETUSERCOMPANY", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userCompList = new List<SystemUserCompany>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _systemUserComp = new SystemUserCompany();

                    _systemUserComp.SEC_USR_ID = _dtResults.Rows[i]["SEC_USR_ID"].ToString();
                    _systemUserComp.SEC_COM_CD = _dtResults.Rows[i]["SEC_COM_CD"].ToString();
                    _systemUserComp.SEC_ACT = Convert.ToInt32(_dtResults.Rows[i]["SEC_ACT"]);
                    _systemUserComp.SEC_DEF_COMCD = Convert.ToInt32(_dtResults.Rows[i]["SEC_DEF_COMCD"]);

                    _systemUserComp.MasterComp = new MasterCompany()
                    {
                        Mc_desc = _dtResults.Rows[i]["mc_desc"].ToString()
                    };

                    _userCompList.Add(_systemUserComp);
                }
            }
            return _userCompList;
        }
        #endregion

        #region System Roles

        /// <summary>
        /// Get ALL UserRoles(System User Roles) Details.
        /// </summary>
        /// <returns></returns>
        public List<SystemRole> GetALLSystemRolesData()
        {
            List<SystemRole> _systemRoleList = null;

            //Query Data base.         
            DataTable _dtResults = QueryDataTable("tblSystemRoles", "sp_getalluserroles", CommandType.StoredProcedure, false,
                                                   new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));

            if (_dtResults.Rows.Count > 0)
            {
                //Convert datatable to relavant generic List.
                _systemRoleList = DataTableExtensions.ToGenericList<SystemRole>(_dtResults, SystemRole.Converter);
            }

            return _systemRoleList;
        }

        /// <summary>
        /// Get System Role by roleId.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public SystemRole GetSystemRoleByRoleData(SystemRole _userRole)
        {
            SystemRole _resultURole = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.RoleId;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.CompanyCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.

            DataTable _dtResults = QueryDataTable("tblSystemRole", "SP_GETUSERROLEBYROLEDATA", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //Convert datatable to relavant generic List.
                _resultURole = DataTableExtensions.ToGenericList<SystemRole>(_dtResults, SystemRole.Converter)[0];
            }

            return _resultURole;
        }

        /// <summary>
        /// Use for both Insert and Update SystemRole functionality.
        /// </summary>
        /// <param name="_userRole"></param>
        /// <returns>No of rows effected in DB</returns>
        public Int32 UpdateSystemUserRole(SystemRole _userRole)
        {
            Int32 rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_companyCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.CompanyCode;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.RoleId;
                (param[2] = new OracleParameter("p_rolename", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.RoleName;
                (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.Description;
                (param[4] = new OracleParameter("p_isActive", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.IsActive;
                (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.CreatedBy;
                (param[6] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.ModifiedBy;
                (param[7] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.SessionId;
                param[8] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                //Open the connection and call the stored procedure.
                ConnectionOpen();
                rows_affected = UpdateRecords("sp_updatesystemrole", CommandType.StoredProcedure, param);
                //rows_affected = UpdateRecords("sp_updatesystemrole_new", CommandType.StoredProcedure, param); //returns the role ID.

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                ConnectionClose();
            }

            return rows_affected;
        }
        public Int32 UpdateSystemUserRole_NEW(SystemRole _userRole)
        {
            Int32 rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_companyCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.CompanyCode;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.RoleId;
                (param[2] = new OracleParameter("p_rolename", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.RoleName;
                (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.Description;
                (param[4] = new OracleParameter("p_isActive", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.IsActive;
                (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.CreatedBy;
                (param[6] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.ModifiedBy;
                (param[7] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.SessionId;
                param[8] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                //Open the connection and call the stored procedure.
                ConnectionOpen();
                //  rows_affected = UpdateRecords("sp_updatesystemrole", CommandType.StoredProcedure, param);
                rows_affected = UpdateRecords("sp_updatesystemrole_new", CommandType.StoredProcedure, param); //returns the role ID.

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                ConnectionClose();
            }

            return rows_affected;
        }
        public SystemRoleOption GetCurrentSystemOptionsDataByRole(SystemRole _inputsystemRole)
        {
            SystemRoleOption _systemRoleOption = null;
            SystemRole _systemRole = null;
            SystemOption _systemOption = null;
            List<SystemOption> _systemOptionList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inputsystemRole.RoleId;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inputsystemRole.CompanyCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.

            DataTable _dtResults = QueryDataTable("tblSystemRoleOptions", "sp_getcurrentsystemroleoptions", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _systemRoleOption = new SystemRoleOption();
                _systemRole = new SystemRole();
                _systemRole.CompanyCode = _dtResults.Rows[0]["ssro_comcd"].ToString();
                _systemRole.RoleId = Convert.ToInt32(_dtResults.Rows[0]["ssro_roleid"].ToString());
                _systemRoleOption.SystemRole = _systemRole;

                _systemOptionList = new List<SystemOption>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _systemOption = new SystemOption();
                    _systemOption.Ssp_optid = Convert.ToInt32(_dtResults.Rows[i]["ssro_optid"].ToString());//Null check
                    _systemOption.Ssp_act = Convert.ToInt32(_dtResults.Rows[i]["ssro_act"].ToString());
                    _systemOptionList.Add(_systemOption);
                }

                _systemRoleOption.SystemOptionList = _systemOptionList;
            }

            return _systemRoleOption;
        }

        public int SaveCurrentSystemRoleOptins(SystemRoleOption _systemRoleOption)
        {
            int rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_companyCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.CompanyCode;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.RoleId;
                (param[2] = new OracleParameter("p_optid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemOption.Ssp_optid;
                (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.IsActive;

                //Open the connection and call the stored procedure.
                //ConnectionOpen();
                UpdateRecords("sp_savecurrentsysroleoptins", CommandType.StoredProcedure, param);


            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                //ConnectionClose();
            }
            return rows_affected;
        }


        public int LogCurrentSystemRoleOptions(SystemRoleOption _systemRoleOption)
        {
            int rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.CompanyCode;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.RoleId;
                (param[2] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRoleOption.CreatedBy;
                //param[8] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                //Open the connection and call the stored procedure.
                //ConnectionOpen();
                rows_affected = UpdateRecords("sp_updatecurrentsysroleoptions", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                //ConnectionClose();
            }

            return rows_affected;
        }


        public List<SystemUserRole> GetSystemUsersByRoleData(SystemRole _systemRole)
        {
            SystemUserRole _systemUserRole = null;
            List<SystemUserRole> _systemUserRoleList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRole.RoleId;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRole.CompanyCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.

            DataTable _dtResults = QueryDataTable("tblStemRoleUsers", "sp_getsystemusersbyrole", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _systemUserRoleList = new List<SystemUserRole>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _systemUserRole = new SystemUserRole();
                    _systemUserRole.SystemUser = new SystemUser()
                    {
                        Se_usr_id = ((_dtResults.Rows[i]["ser_usr_id"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["ser_usr_id"].ToString()),
                        Se_usr_name = ((_dtResults.Rows[i]["se_usr_name"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["se_usr_name"].ToString()),
                        Se_usr_desc = ((_dtResults.Rows[i]["se_usr_desc"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["se_usr_desc"].ToString()),
                        se_Email = ((_dtResults.Rows[i]["se_email"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["se_email"].ToString()),
                        se_Mob = ((_dtResults.Rows[i]["se_mob"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["se_mob"].ToString()),
                        se_Phone = ((_dtResults.Rows[i]["se_phone"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["se_phone"].ToString()),
                        Se_domain_id = ((_dtResults.Rows[i]["se_domain_id"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["se_domain_id"].ToString())
                    };

                    _systemUserRole.SystemRole = new SystemRole()
                    {
                        CompanyCode = _dtResults.Rows[i]["ser_com_cd"].ToString(),
                        RoleId = Convert.ToInt32(_dtResults.Rows[i]["ser_role_id"].ToString())
                    };

                    _systemUserRoleList.Add(_systemUserRole);
                }
            }

            return _systemUserRoleList;
        }

        public SystemRole GetRoleByRoleCode(string _CompCode, int _roleID)
        {
            SystemRole _roleList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _CompCode;
            (param[1] = new OracleParameter("p_role_code", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _roleID;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblloc", "sp_get_role_by_code", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //_roleList = DataTableExtensions.ToGenericList<SystemRole>(_dtResults, SystemRole.Converter)[0];
                _roleList = DataTableExtensions.ToGenericList<SystemRole>(_dtResults, SystemRole.ConverterNew)[0]; //updated by shani 20-05-2013
            }
            return _roleList;
        }

        #endregion

        #region Testing Methods


        #endregion

        #region System Option Registration
        //
        // Function            - System Option Registration
        // Function Wriiten By - P.Wijetunge
        // Date                - 29/02/2012
        //

        /// <summary>
        /// Common private function to get all system option as per the given stored procedure
        /// </summary>
        /// <param name="_spName">Used to transfer the stored procedure name</param>
        /// <returns>List of system options</returns>
        private List<SystemOption> GetSpecificSystemOptions(string _spName)
        {
            List<SystemOption> _systemOptions = null;
            //Query Data base.
            ConnectionOpen();
            DataTable _dtSystemOpt = QueryDataTable("tblSystemOption", _spName, CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));
            ConnectionClose();
            #region Data Access
            try
            {
                if (_dtSystemOpt.Rows.Count > 0) _systemOptions = DataTableExtensions.ToGenericList<SystemOption>(_dtSystemOpt, SystemOption.ConvertTotal);
            }
            catch (Exception e)
            {
                throw e; //TODO: Throw the exception to general           
            }
            #endregion
            return _systemOptions;
        }
        /// <summary>
        /// Specified public function for get all the system options
        /// </summary>
        /// <returns>list of system options</returns>
        public List<SystemOption> GetAllSystemOption()
        {
            return GetSpecificSystemOptions("sp_getallsystemoptions");
        }

        // Chamal 11/05/2012
        public List<SystemOption> GetUserSystemOption(string _user)
        {
            List<SystemOption> _systemOptions = null;
            //Query Data base.
            ConnectionOpen();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtSystemOpt = QueryDataTable("tblSystemOption", "sp_getusersystemoptions", CommandType.StoredProcedure, false, param);
            ConnectionClose();
            #region Data Access
            try
            {
                if (_dtSystemOpt.Rows.Count > 0) _systemOptions = DataTableExtensions.ToGenericList<SystemOption>(_dtSystemOpt, SystemOption.ConvertTotal);
            }
            catch (Exception e)
            {
                throw e; //TODO: Throw the exception to general           
            }
            #endregion
            return _systemOptions;
        }


        public Int16? CheckSystemOptionOganizePosition(Int16 _parentID, Int16 _newOganizePosition)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_parentID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _parentID;
            (param[1] = new OracleParameter("p_neworgposition", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _newOganizePosition;
            ConnectionOpen();
            Int16 _effect = Convert.ToInt16(QueryFunction("fn_checksysoptorgposition", CommandType.StoredProcedure, param));
            ConnectionClose();
            return _effect;
        }

        public Int32 GetMaxOptionID()
        {
            ConnectionOpen();
            Int32 _maxNo = (Int32)QueryFunction("fn_checkmaxoptid", CommandType.StoredProcedure, null);
            ConnectionClose();
            return _maxNo;

        }

        public Int16 UpdateSystemOption(SystemOption _opt, string _user, string _sessionID)
        {
            Int16 _effect = 0;

            OracleParameter[] param = new OracleParameter[12];
            (param[0] = new OracleParameter("sp_optid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _opt.Ssp_optid;
            (param[1] = new OracleParameter("sp_title", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _opt.Ssp_title;
            (param[2] = new OracleParameter("sp_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _opt.Ssp_desc;
            (param[3] = new OracleParameter("sp_url", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _opt.Ssp_url;
            (param[4] = new OracleParameter("sp_parentid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _opt.Ssp_parentid;
            (param[5] = new OracleParameter("sp_orgposition", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _opt.Ssp_orgposition;
            (param[6] = new OracleParameter("sp_isenabled", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _opt.Ssp_isenabled;
            (param[7] = new OracleParameter("sp_ishide", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _opt.Ssp_ishide;
            (param[8] = new OracleParameter("sp_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _opt.Ssp_act;
            (param[9] = new OracleParameter("sp_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[10] = new OracleParameter("sp_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sessionID;
            param[11] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            _effect = (Int16)this.UpdateRecords("sp_updatesystemoptions", CommandType.StoredProcedure, param);
            ConnectionClose();
            return _effect;

        }
        #endregion

        #region User Request Approval Permission

        //Miginda Geeganage On 07/04/2012
        public UserRequestApprovePermission GetUserRequestApprovalPermission(string _userId, string _reqSubType)
        {
            UserRequestApprovePermission _userRequestApprovePermission = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_se_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userId.ToUpper();
            (param[1] = new OracleParameter("p_req_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqSubType.ToUpper();
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblUserApprovePermission", "sp_getuserapprovepermission", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userRequestApprovePermission = new UserRequestApprovePermission();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _userRequestApprovePermission.UserId = (_dtResults.Rows[i]["User_Id"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["User_Id"].ToString();
                    _userRequestApprovePermission.UserPermissionCode = (_dtResults.Rows[i]["User_PermissionCode"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["User_PermissionCode"].ToString();
                    _userRequestApprovePermission.MaxApproveLimit = (_dtResults.Rows[i]["Max_Approve_Limit"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Max_Approve_Limit"]);
                    _userRequestApprovePermission.ValueLimit = (_dtResults.Rows[i]["Value_Limit"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Value_Limit"]);
                    _userRequestApprovePermission.UserPermissionLevel = (_dtResults.Rows[i]["User_Permission_Level"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["User_Permission_Level"]);
                    _userRequestApprovePermission.RequestMainType = (_dtResults.Rows[i]["Req_MainType"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Req_MainType"].ToString();
                    _userRequestApprovePermission.RequestSubType = (_dtResults.Rows[i]["Req_SubType"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Req_SubType"].ToString();
                    _userRequestApprovePermission.Description = (_dtResults.Rows[i]["Description"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Description"].ToString();
                    _userRequestApprovePermission.RequestApproveLevel = (_dtResults.Rows[i]["Req_Approve_Level"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Req_Approve_Level"]);
                    _userRequestApprovePermission.RequestLevel = (_dtResults.Rows[i]["Req_Level"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Req_Level"]);

                }

            }

            return _userRequestApprovePermission;
        }

        //Miginda Geeganage On 09/04/2012
        public UserRequestApprovePermission GetUserLocationRequestApprovalPermission(string _userId, string _reqSubType, string _type, string _typeCode)
        {
            UserRequestApprovePermission _userRequestApprovePermission = null;

            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_se_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userId.ToUpper();
            (param[1] = new OracleParameter("p_req_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqSubType.ToUpper();
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type.ToUpper();
            (param[3] = new OracleParameter("p_type_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _typeCode.ToUpper();
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblUserLocationApprovePermission", "sp_getuserlocapprovepermission", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userRequestApprovePermission = new UserRequestApprovePermission();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _userRequestApprovePermission.UserId = (_dtResults.Rows[i]["User_Id"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["User_Id"].ToString();
                    _userRequestApprovePermission.Type = (_dtResults.Rows[i]["Type"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Type"].ToString();
                    _userRequestApprovePermission.TypeCode = (_dtResults.Rows[i]["Type_Code"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Type_Code"].ToString();
                    _userRequestApprovePermission.UserPermissionCode = (_dtResults.Rows[i]["User_PermissionCode"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["User_PermissionCode"].ToString();
                    _userRequestApprovePermission.MaxApproveLimit = (_dtResults.Rows[i]["Max_Approve_Limit"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Max_Approve_Limit"]);
                    _userRequestApprovePermission.ValueLimit = (_dtResults.Rows[i]["Value_Limit"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Value_Limit"]);
                    _userRequestApprovePermission.UserPermissionLevel = (_dtResults.Rows[i]["User_Permission_Level"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["User_Permission_Level"]);
                    _userRequestApprovePermission.RequestMainType = (_dtResults.Rows[i]["Req_MainType"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Req_MainType"].ToString();
                    _userRequestApprovePermission.RequestSubType = (_dtResults.Rows[i]["Req_SubType"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Req_SubType"].ToString();
                    _userRequestApprovePermission.Description = (_dtResults.Rows[i]["Description"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Description"].ToString();
                    _userRequestApprovePermission.RequestApproveLevel = (_dtResults.Rows[i]["Req_Approve_Level"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Req_Approve_Level"]);
                    _userRequestApprovePermission.RequestLevel = (_dtResults.Rows[i]["Req_Level"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Req_Level"]);

                }

            }

            return _userRequestApprovePermission;
        }

        //Miginda Geeganage On 17/04/2012
        public RequestApproveCycleDefinition GetApproveCycleDefinitionDetails(RequestApproveCycleDefinition _paramObject)
        {
            RequestApproveCycleDefinition _requestApproveCycleDefinition = null;

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_source_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramObject.SourceType;
            (param[1] = new OracleParameter("p_source_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramObject.SourceTypeCode;
            (param[2] = new OracleParameter("p_req_subtype_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramObject.ReqSubTypeCode;
            (param[3] = new OracleParameter("p_tran_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _paramObject.TransactionDate;
            (param[4] = new OracleParameter("p_dest_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramObject.DestinationType;
            (param[5] = new OracleParameter("p_dest_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramObject.DestinationTypeCode;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblApproveCycleDefinition", "sp_get_app_cyc_definition", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _requestApproveCycleDefinition = new RequestApproveCycleDefinition();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _requestApproveCycleDefinition.SeqNo = (_dtResults.Rows[i]["Seq_No"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Seq_No"]);
                    _requestApproveCycleDefinition.SourceType = (_dtResults.Rows[i]["Source_Type"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Source_Type"].ToString();
                    _requestApproveCycleDefinition.SourceTypeCode = (_dtResults.Rows[i]["Source_Code"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Source_Code"].ToString();
                    _requestApproveCycleDefinition.DestinationType = (_dtResults.Rows[i]["Destination_Type"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Destination_Type"].ToString();
                    _requestApproveCycleDefinition.DestinationTypeCode = (_dtResults.Rows[i]["Destination_Code"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Destination_Code"].ToString();
                    int isApprovalNeeded = (_dtResults.Rows[i]["Is_Approval_Needed"] == DBNull.Value) ? 1 : Convert.ToInt32(_dtResults.Rows[i]["Is_Approval_Needed"]);
                    _requestApproveCycleDefinition.IsApprovalNeeded = (isApprovalNeeded == 1) ? true : false;
                    _requestApproveCycleDefinition.UserRequestApprovePermission = new UserRequestApprovePermission()
                    {
                        RequestMainType = (_dtResults.Rows[i]["Request_MainType"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Request_MainType"].ToString(),
                        RequestSubType = (_dtResults.Rows[i]["Request_SubType"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Request_SubType"].ToString(),
                        Description = (_dtResults.Rows[i]["Description"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Description"].ToString(),

                    };

                    _requestApproveCycleDefinition.FromDate = (_dtResults.Rows[i]["From_Date"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_dtResults.Rows[i]["From_Date"]);
                    _requestApproveCycleDefinition.ToDate = (_dtResults.Rows[i]["To_Date"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_dtResults.Rows[i]["To_Date"]);
                    _requestApproveCycleDefinition.CreatedBy = (_dtResults.Rows[i]["Cre_By"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Cre_By"].ToString();
                    _requestApproveCycleDefinition.ModifiedBy = (_dtResults.Rows[i]["Mod_By"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Mod_By"].ToString();
                    _requestApproveCycleDefinition.CreatedDate = (_dtResults.Rows[i]["Cre_Dt"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_dtResults.Rows[i]["Cre_Dt"]);
                    _requestApproveCycleDefinition.ModifiedDate = (_dtResults.Rows[i]["Mod_Dt"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_dtResults.Rows[i]["Mod_Dt"]);
                    _requestApproveCycleDefinition.SessionId = (_dtResults.Rows[i]["SessionId"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["SessionId"].ToString();
                }

            }

            return _requestApproveCycleDefinition;
        }

        //Miginda Geeganage On 23/04/2012
        public List<PriorityHierarchy> GetPriorityHierarchy(string _compCode, string _locCode, string _category, string _type)
        {
            List<PriorityHierarchy> _priorityHierarchyList = null;
            PriorityHierarchy _priorityHierarchy = null;

            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _compCode;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _locCode;
            (param[2] = new OracleParameter("p_category", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _category;
            (param[3] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblPriorityHierarchy", "sp_get_priority_hierarchy", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _priorityHierarchyList = new List<PriorityHierarchy>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _priorityHierarchy = new PriorityHierarchy();
                    _priorityHierarchy.CompanyCode = ((_dtResults.Rows[i]["mli_com_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mli_com_cd"].ToString());
                    _priorityHierarchy.LocationCode = ((_dtResults.Rows[i]["mli_loc_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mli_loc_cd"].ToString());
                    _priorityHierarchy.HierarchyType = ((_dtResults.Rows[i]["mli_tp"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mli_tp"].ToString());
                    _priorityHierarchy.HierarchyItemName = ((_dtResults.Rows[i]["mli_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mli_cd"].ToString());
                    _priorityHierarchy.HierarchyItemValue = ((_dtResults.Rows[i]["mli_val"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mli_val"].ToString());
                    int isactive = ((_dtResults.Rows[i]["mli_act"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["mli_act"]));
                    _priorityHierarchy.HierarchyItemIsActive = (isactive == 0) ? false : true;

                    _priorityHierarchyList.Add(_priorityHierarchy);

                }

            }
            return _priorityHierarchyList;

        }


        #endregion

        #region user maintain
        public Int32 User_Maintain(SystemUser _user, string action)
        {
            Int32 rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_userID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user.Se_usr_id;
                (param[1] = new OracleParameter("p_domainID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user.Se_domain_id;
                (param[2] = new OracleParameter("p_newPW", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user.Se_usr_pw;

                (param[3] = new OracleParameter("p_PWmustChange", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _user.Se_pw_mustchange;
                (param[4] = new OracleParameter("p_activate", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _user.Se_act;
                (param[5] = new OracleParameter("p_action", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = action;

                param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);


                rows_affected = UpdateRecords("sp_updateSecUser_Maintain", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {

            }

            return rows_affected;
        }
        #endregion

        #region System Menus / User Roles Menus - Windows Apps
        //
        // Function            - Windows Apps System Menus 
        // Function Wriiten By - Chamal De Silva
        // Date                - 23/01/2013
        //

        //kapila 24/1/2013
        public Int16 Check_User_PC(string _userID, string _com, string _pc)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAssLoc", "sp_chk_usr_pc", CommandType.StoredProcedure, false, param);

            Int16 _isDefLocFound = 0;
            if (_dtResults.Rows.Count > 0)
            {
                _isDefLocFound = 1;
            }

            return _isDefLocFound;
        }
        //kapila 24/1/2013
        public Int16 Check_User_Loc(string _userID, string _com, string _loc)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAssLoc", "sp_chk_usr_loc", CommandType.StoredProcedure, false, param);

            Int16 _isDefLocFound = 0;
            if (_dtResults.Rows.Count > 0)
            {
                _isDefLocFound = 1;
            }

            return _isDefLocFound;
        }
        // Chamal 23/01/2013
        public DataTable GetUserSystemMenus(string _user, string _company)
        {
            ConnectionOpen();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtSystemOpt = QueryDataTable("tblSystemOption", "sp_getusersystemmenus", CommandType.StoredProcedure, false, param);
            ConnectionClose();
            return _dtSystemOpt;
        }

        // Chamal 07/02/2013
        public String GetCurrentVersion()
        {
            string _getCurrentVersion = string.Empty;
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtSystemOpt = QueryDataTable("tblSystemOption", "sp_checklatestversion", CommandType.StoredProcedure, false, param);
            if (_dtSystemOpt.Rows.Count == 1)
            {
                for (int i = 0; i < _dtSystemOpt.Rows.Count; i++)
                {
                    _getCurrentVersion = (_dtSystemOpt.Rows[i]["VER_NO"] == DBNull.Value) ? string.Empty : _dtSystemOpt.Rows[i]["VER_NO"].ToString();
                }
            }
            return _getCurrentVersion;
        }

        //Chamal 18/03/2013
        public int SaveSystemMenu(SystemMenus _menu)
        {
            int rows_aff = 0;

            OracleParameter[] param = new OracleParameter[14];
            (param[0] = new OracleParameter("p_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_name;
            (param[1] = new OracleParameter("p_disp_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_disp_name;
            (param[2] = new OracleParameter("p_menu_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_menu_tp;
            (param[3] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_tp;
            (param[4] = new OracleParameter("p_isallowbackdt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _menu.Ssm_isallowbackdt;
            (param[5] = new OracleParameter("p_needdayend", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _menu.Ssm_needdayend;
            (param[6] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _menu.Ssm_act;
            (param[7] = new OracleParameter("p_anal1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_anal1;
            (param[8] = new OracleParameter("p_anal2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_anal2;
            (param[9] = new OracleParameter("p_anal3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_anal3;
            (param[10] = new OracleParameter("p_anal4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_anal4;
            (param[11] = new OracleParameter("p_anal5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_anal5;
            (param[12] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _menu.Ssm_cre_by;
            param[13] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            ConnectionOpen();
            rows_aff = UpdateRecords("sp_save_sec_system_menu", CommandType.StoredProcedure, param);
            ConnectionClose();

            return rows_aff;
        }
        #endregion

        #region System Option permissions for user
        //
        // Function            - Check System Option permissions for user
        // Function Wriiten By - Shani Waththuhewa
        // Date                - 26/02/2013
        //

        // Shani 26/02/2013
        public Int32 Is_OptionPerimitted(string userCompany, string userId, Int32 optionCode)
        {
            try
            {
                int _effect = 0;
                OracleParameter[] param = new OracleParameter[3];

                //sp_Is_OptionPerimitted(p_userCom in NVARCHAR2, p_userid in NVARCHAR2,p_optionCode in NUMBER,c_result OUT NUMBER)
                (param[0] = new OracleParameter("p_userCom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userCompany;
                (param[1] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
                (param[2] = new OracleParameter("p_optionCode", OracleDbType.Int32, null, ParameterDirection.Input)).Value = optionCode;

                OracleParameter orP = new OracleParameter("c_result", OracleDbType.Int32, null, ParameterDirection.Output);
                _effect = ReturnSP_SingleValue("sp_Is_OptionPerimitted", CommandType.StoredProcedure, orP, param);
                return _effect;
            }
            catch (Exception) { return -1; }
        }
        #endregion

        #region Back Date Function
        //Created by Shani 07-03-2013
        public DataTable Get_childMenus(string _type, string _parentMenuName)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_Type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            (param[1] = new OracleParameter("p_parentMenuName", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _parentMenuName;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblchilds", "get_childMenus", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }
        //Created by Shani 07-03-2013
        public DataTable Get_Menu(string MenuName, out List<SystemMenus> list)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_MenuName", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MenuName;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblchilds", "get_Menu", CommandType.StoredProcedure, false, param);

            list = new List<SystemMenus>();
            if (_dtResults.Rows.Count > 0)
            {
                list = DataTableExtensions.ToGenericList<SystemMenus>(_dtResults, SystemMenus.Converter);
            }

            return _dtResults;

        }

        #endregion

        public DataTable GetUserMessageType(string _receiver, string _location, string _profitcenter)
        {
            //sp_getmessagetype(p_receiver in NVARCHAR2,p_loc in NVARCHAR2,p_pc in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_receiver", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _receiver;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblchilds", "sp_getmessagetype", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public SystemRole GetSystemRole_ByRoleData(SystemRole _userRole)
        {
            SystemRole _resultURole = null;

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.RoleId;
            (param[1] = new OracleParameter("p_roleName", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.RoleName;
            (param[2] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.CompanyCode;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.

            DataTable _dtResults = QueryDataTable("tblSystemRole", "SP_GET_ROLE_byDATA", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //Convert datatable to relavant generic List.
                _resultURole = DataTableExtensions.ToGenericList<SystemRole>(_dtResults, SystemRole.ConverterNew)[0];
            }

            return _resultURole;
        }
        public int SaveCurrentSystemRoleOptins_NEW(SystemRoleOption _systemRoleOption)
        {
            int rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_companyCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.CompanyCode;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.RoleId;
                (param[2] = new OracleParameter("p_optid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemOption.Ssp_optid;
                (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.IsActive;

                //Open the connection and call the stored procedure.
                //ConnectionOpen();
                // UpdateRecords("sp_savecurrentsysroleoptins", CommandType.StoredProcedure, param);
                Int32 eff = UpdateRecords("sp_save_SysRole_Menu", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                //ConnectionClose();
            }
            return rows_affected;
        }

        public int LogCurrentSystemRoleOptions_NEW(SystemRoleOption _systemRoleOption)
        {
            int rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.CompanyCode;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(_systemRoleOption.SystemRole.RoleId);
                (param[2] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRoleOption.CreatedBy;
                param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                //Open the connection and call the stored procedure.
                //ConnectionOpen();
                rows_affected = UpdateRecords("sp_updatecurrent_sys_roleMenus", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                //ConnectionClose();
            }

            return rows_affected;
        }

        public DataTable Get_SystemOptionsData_ByRoleID(string com, Int32 roleId)
        {
            //SystemRoleOption _systemRoleOption = null;
            //SystemRole _systemRole = null;
            //SystemOption _systemOption = null;
            //List<SystemOption> _systemOptionList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = roleId;//_inputsystemRole.RoleId;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;//_inputsystemRole.CompanyCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.

            DataTable _dtResults = QueryDataTable("tblSystemRoleOptions", "sp_getSystemroleMenus", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable Get_MenusForRole(string com, Int32 roleId)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_roleID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = roleId;//_inputsystemRole.RoleId;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;//_inputsystemRole.CompanyCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.

            DataTable _dtResults = QueryDataTable("tblSystemRoleOptions", "get_menusForRole", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable Get_UsersForRole(string com, Int32 roleId)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = roleId;//_inputsystemRole.RoleId;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;//_inputsystemRole.CompanyCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.

            DataTable _dtResults = QueryDataTable("tblSystemRoleOptions", "sp_getsystem_MENU_usersbyrole", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Created by Shani 21-05-2013
        public DataTable Get_SystemOptionsForGroup(string groupID)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_groupID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = groupID;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblchilds", "sp_get_sysOptForGrop", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }

        public int SaveSystemOptins_ForRole(SystemRoleOption _systemRoleOption)
        {
            int rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_companyCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.CompanyCode;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.RoleId;
                (param[2] = new OracleParameter("p_optid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemOption.Ssp_optid;
                (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _systemRoleOption.IsActive;

                UpdateRecords("sp_save_sys_role_optins", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                //ConnectionClose();
            }
            return rows_affected;
        }

        // public Int32 UpdateSystemUserRole(string com, Int32 roleId,Int32 OptID)
        public Int32 Update_SEC_SYSTEM_ROLEOPT(string com, Int32 roleId, Int32 OptID)
        {
            Int32 rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[1] = new OracleParameter("p_roleID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = roleId;
                (param[2] = new OracleParameter("p_optID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = OptID;

                param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                //Open the connection and call the stored procedure.
                // ConnectionOpen();
                rows_affected = UpdateRecords("sp_update_SEC_SYSTEM_ROLEOPT", CommandType.StoredProcedure, param);
            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                // ConnectionClose();
            }

            return rows_affected;
        }

        //Created by Shani 22-05-2013
        public DataTable Get_Active_System_OptionsFor_Role(string company, Int32 roleId)
        {
            //p_com in NVARCHAR2,p_roleID in NUMBER, c_data OUT sys_refcursor)
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_roleID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = roleId;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblchilds", "get_Active_options_OfRole", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }

        public int Update_CurrentSystemRoleOptions(SystemRoleOption _systemRoleOption, string Opt_groupID)
        {
            int rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRoleOption.SystemRole.CompanyCode;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(_systemRoleOption.SystemRole.RoleId);
                (param[2] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _systemRoleOption.CreatedBy;
                (param[3] = new OracleParameter("p_gropID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Opt_groupID;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                //Open the connection and call the stored procedure.

                rows_affected = UpdateRecords("sp_update_sys_roleOPTIONS", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {

            }

            return rows_affected;
        }

        public DataTable Get_OptionGroupDetail(string groupID)
        {

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_gropID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = groupID;

            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblgrp", "get_Option_GroupDetail", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }

        public DataTable GetUser_Company(string _UserID)
        {
            //List<SystemUserCompany> _userCompList = null;
            //SystemUserCompany _systemUserComp = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserID;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserComp", "SP_GETUSERCOMPANY", CommandType.StoredProcedure, false, param);
            return _dtResults;

        }
        public DataTable Get_SpecialUser_Perm(string _UserID)
        {
            //List<SystemUserCompany> _userCompList = null;
            //get_Special_userPerm(p_userID in NVARCHAR2, c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserID;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblUserComp", "get_Special_userPerm", CommandType.StoredProcedure, false, param);
        }

        public int Save_SecUserPerm(SecUserPerm _perm)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_usr_id;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_com;
            (param[2] = new OracleParameter("p_party", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_party;
            (param[3] = new OracleParameter("p_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_cd;
            (param[4] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _perm.Seur_act;
            (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_cre_by;
            (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _perm.Seur_cre_dt;
            (param[7] = new OracleParameter("p_mod_dt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_mod_dt;
            (param[8] = new OracleParameter("p_mod_by", OracleDbType.Date, null, ParameterDirection.Input)).Value = _perm.Seur_mod_by;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_save_sec_user_perm", CommandType.StoredProcedure, param);
        }

        public int Inactivate_SecUserPerm(SecUserPerm _perm)
        {
            //            p_usr_id IN ems.sec_user_perm.seur_usr_id%TYPE ,
            //p_com IN ems.sec_user_perm.seur_com%TYPE ,
            //p_party IN ems.sec_user_perm.seur_party%TYPE ,
            //p_cd IN ems.sec_user_perm.seur_cd%TYPE ,
            //p_mod_dt IN ems.sec_user_perm.seur_mod_dt%TYPE ,
            //p_mod_by IN ems.sec_user_perm.seur_mod_by%TYPE ,
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_usr_id;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_com;
            (param[2] = new OracleParameter("p_party", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_party;
            (param[3] = new OracleParameter("p_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_cd;
            (param[4] = new OracleParameter("p_mod_dt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _perm.Seur_mod_dt;
            (param[5] = new OracleParameter("p_mod_by", OracleDbType.Date, null, ParameterDirection.Input)).Value = _perm.Seur_mod_by;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_INACTIVATE_sec_user_perm", CommandType.StoredProcedure, param);
        }
        public DataTable GetUserLocations(string UserID, string Comp)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblUserLoc", "SP_GETUSERLOC", CommandType.StoredProcedure, false, param);
        }

        public int UpdateSystemUserLoc_NEW(SystemUserLoc _userLoc)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_sel_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userLoc.SEL_USR_ID;
            (param[1] = new OracleParameter("p_sel_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userLoc.SEL_COM_CD;
            (param[2] = new OracleParameter("p_sel_loc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userLoc.SEL_LOC_CD;
            (param[3] = new OracleParameter("p_sel_def_loccd", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userLoc.SEL_DEF_LOCCD;
            //  (param[4] = new OracleParameter("p_sel_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userLoc.SEL_CRE_BY;
            //  (param[5] = new OracleParameter("p_sel_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userLoc.SEL_MOD_BY;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_update_user_loc", CommandType.StoredProcedure, param);
        }

        public int UpdateSystemUserPC_NEW(SystemUserProf _userPC)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_SUP_USR_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userPC.Sup_usr_id;
            (param[1] = new OracleParameter("p_SUP_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userPC.Sup_com_cd;
            (param[2] = new OracleParameter("p_SUP_PC_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userPC.Sup_pc_cd;
            (param[3] = new OracleParameter("p_SUP_DEF_PCCD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userPC.Sup_def_pccd;
            //  (param[4] = new OracleParameter("p_sup_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userPC.SUP_CRE_BY;
            //  (param[5] = new OracleParameter("p_sup_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userPC.SUP_MOD_BY;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_update_user_pc", CommandType.StoredProcedure, param);
        }

        public DataTable GetAllUserPC(string _UserID)
        {
            //List<SystemUserProf> _userList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserID;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserLoc", "SP_GETUSERPC", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable Get_gen_doc_pro_hdr(string userID, string receiptNo, string invoiceNo, string engineNo, string chassisNo)
        {
            //sp_get_Reg_IssuedDocs(p_userID in NVARCHAR2,p_receiptNo in NVARCHAR2,p_invoiceNo in NVARCHAR2,p_engine in NVARCHAR2,p_chassis in NVARCHAR2,c_data out SYS_REFCURSOR)
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_userID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userID;
            (param[1] = new OracleParameter("p_receiptNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = receiptNo;
            (param[2] = new OracleParameter("p_invoiceNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoiceNo;

            (param[3] = new OracleParameter("p_engine", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = engineNo;
            (param[4] = new OracleParameter("p_chassis", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chassisNo;

            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            return QueryDataTable("tblDoc", "sp_get_gen_doc_pro_hdr", CommandType.StoredProcedure, false, param);
        }

        public int DeleteUserLoc_NEW(string UserID, string Comp, string Loc)
        {
            int row_aff = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_sel_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
                (param[1] = new OracleParameter("p_sel_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
                (param[2] = new OracleParameter("p_sel_loc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;

                row_aff = UpdateRecords("sp_del_user_loc", CommandType.StoredProcedure, param);
            }
            catch (Exception)
            {
                row_aff = -1;
            }
            return row_aff;

        }

        public int DeleteUserPC_NEW(string UserID, string Comp, string PC)
        {
            int row_aff = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_sel_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
                (param[1] = new OracleParameter("p_sel_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
                (param[2] = new OracleParameter("p_sel_loc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;

                row_aff = UpdateRecords("sp_del_user_pc", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                row_aff = -1;
            }
            return row_aff;
        }

        public string GetModuleWiseEmail(string _modName)
        {
            string _rtn = string.Empty;
            int _count = 0;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_mod", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _modName;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl_", "SP_Mod_Emails", CommandType.StoredProcedure, false, param);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    _count += 1;
                    if (_count == 1)
                    {
                        _rtn = r[0].ToString();
                    }
                    if (_count > 1)
                    {
                        _rtn += ";" + r[0].ToString();
                    }
                }
            }
            return _rtn;
        }

        public SystemRole GetSystemRoleByRoleData_new(SystemRole _userRole)
        {
            SystemRole _resultURole = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.RoleId;
            (param[1] = new OracleParameter("p_comcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.CompanyCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.

            DataTable _dtResults = QueryDataTable("tblSystemRole", "SP_GETUSERROLEBYROLEDATA_new", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //Convert datatable to relavant generic List.
                _resultURole = DataTableExtensions.ToGenericList<SystemRole>(_dtResults, SystemRole.ConverterNew)[0];
            }

            return _resultURole;
        }

        public int Save_Sec_App_Usr_Prem(SecApproveUserPerm _secUserPerm)
        {
            int rows_aff = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[11];
                (param[0] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_usr_id;
                (param[1] = new OracleParameter("p_prem_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_prem_cd;
                (param[2] = new OracleParameter("p_max_app_limit", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_max_app_limit;
                (param[3] = new OracleParameter("p_val_limit", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_val_limit;
                (param[4] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_act == true ? 1 : 0;
                (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_cre_by;
                (param[6] = new OracleParameter("p_cre_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_cre_when;
                (param[7] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_mod_by;
                (param[8] = new OracleParameter("p_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_mod_when;
                (param[9] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secUserPerm.Saup_session_id;

                param[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                //ConnectionOpen();
                rows_aff = UpdateRecords("sp_save_sec_app_usr_prem", CommandType.StoredProcedure, param);
            }
            catch (Exception)
            {
                rows_aff = -1;
            }
            finally
            {
                //ConnectionClose();
            }
            return rows_aff;
        }

        public DataTable Get_Approve_PermissionInfo(string _ApprPermCd)
        {
            //List<SystemUserProf> _userList = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_permCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ApprPermCd;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblappr", "sp_GetApprovePermission", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable Get_UserApprove_Permissions(string userID, string permLvlCode)
        {
            // sp_Get_Sec_app_usr_prem(p_userID in NVARCHAR2,p_permLvlCode in NVARCHAR2, c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userID;
            (param[1] = new OracleParameter("p_permLvlCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = permLvlCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblappr", "sp_Get_Sec_app_usr_prem", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public int SaveSystemUserRole_NEW(SystemUserRole _userRole)
        {
            int rows_aff = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_ser_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.SER_USR_ID;
            (param[1] = new OracleParameter("p_ser_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.SER_COM_CD;
            (param[2] = new OracleParameter("p_ser_role_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.SER_ROLE_ID;
            (param[3] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.Se_cre_by; //Add by Chamal 09-Jun-2014
            (param[4] = new OracleParameter("p_session", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.Se_session_id; //Add by Chamal 09-Jun-2014
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            ConnectionOpen();
            rows_aff = UpdateRecords("sp_update_user_role_NEW", CommandType.StoredProcedure, param);
            ConnectionClose();
            return rows_aff;
        }

        public int DeleteSystemUserRole_NEW(SystemUserRole _userRole)
        {
            int rows_aff = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_ser_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.SER_USR_ID;
            (param[1] = new OracleParameter("p_ser_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.SER_COM_CD;
            (param[2] = new OracleParameter("p_ser_role_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userRole.SER_ROLE_ID;
            (param[3] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.Se_cre_by; //Add by Chamal 09-Jun-2014
            (param[4] = new OracleParameter("p_session", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userRole.Se_session_id; //Add by Chamal 09-Jun-2014
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            rows_aff = UpdateRecords("sp_del_user_role_NEW", CommandType.StoredProcedure, param);
            ConnectionClose();
            return rows_aff;
        }

        public DataTable GetUserRole_NEW(string _UserRole)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserRole;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblUserRole", "SP_GETUSERROLE_NEW", CommandType.StoredProcedure, false, param);
        }

        //public int DeleteUserRole_NEW(string UserID, string Comp, int Role)
        //{
        //    int row_aff = 0;
        //    try
        //    {
        //        OracleParameter[] param = new OracleParameter[3];
        //        (param[0] = new OracleParameter("p_ser_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
        //        (param[1] = new OracleParameter("p_ser_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
        //        (param[2] = new OracleParameter("p_ser_role_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Role;

        //        ConnectionOpen();
        //        row_aff = UpdateRecords("sp_del_user_role_NEW", CommandType.StoredProcedure, param);

        //    }
        //    catch (Exception)
        //    {

        //    }
        //    finally
        //    {
        //        ConnectionClose();
        //    }
        //    return row_aff;

        //}

        public Int32 DeleteUserRole_NEW(string UserID, string Comp, int Role)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_ser_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (param[1] = new OracleParameter("p_ser_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            (param[2] = new OracleParameter("p_ser_role_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Role;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("sp_del_user_role_NEW", CommandType.StoredProcedure, param);
        }

        public int Save_sec_role_loc(SecRoleLocation _secLoc)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_com;
            (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_roleid;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_loc;
            (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_act == true ? 1 : 0;
            (param[4] = new OracleParameter("p_readonly", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_readonly == true ? 1 : 0;
            (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_cre_by;
            (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_cre_dt;
            (param[7] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_mod_by;
            (param[8] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_mod_dt;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int16)UpdateRecords("sp_save_sec_role_loc", CommandType.StoredProcedure, param);
        }

        public int Save_sec_role_LocChanel(SecRoleLocChanel _secLocChnl)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_com;
            (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_roleid;
            (param[2] = new OracleParameter("p_chnnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_chnnl;
            (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_act == true ? 1 : 0;
            (param[4] = new OracleParameter("p_readonly", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_readonly == true ? 1 : 0;
            (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrl_cre_by;
            (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_cre_dt;
            (param[7] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_mod_by;
            (param[8] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_mod_dt;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int16)UpdateRecords("sp_save_sec_role_locchnl", CommandType.StoredProcedure, param);
        }

        public int Save_sec_role_pc(SecRolePC _secRolePC)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_com;
            (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_roleid;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_pc;
            (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_act;
            (param[4] = new OracleParameter("p_readonly", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_readonly;
            (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_cre_by;
            (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_cre_dt;
            (param[7] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_mod_by;
            (param[8] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_mod_dt;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int16)UpdateRecords("sp_save_sec_role_pc", CommandType.StoredProcedure, param);
        }


        public int Save_sec_role_pcchnl(SecRolePcChannel _secPcChnl)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_com;
            (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_roleid;
            (param[2] = new OracleParameter("p_chnnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_chnnl;
            (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_act;
            (param[4] = new OracleParameter("p_readonly", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_readonly;
            (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_cre_by;
            (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_cre_dt;
            (param[7] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_mod_by;
            (param[8] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_mod_dt;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int16)UpdateRecords("sp_save_sec_role_pcchnl", CommandType.StoredProcedure, param);
        }

        public Int32 Update_secRoleLoc(SecRoleLocation _secLoc)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_com;
            (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_roleid;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_loc;
            (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_act == true ? 1 : 0;
            (param[4] = new OracleParameter("p_readonly", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_readonly == true ? 1 : 0;
            (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_cre_by;
            (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_cre_dt;
            (param[7] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_mod_by;
            (param[8] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secLoc.Ssrl_mod_dt;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            //effects = (Int32)UpdateRecords("sp_updat_secRoleLoc", CommandType.StoredProcedure, param);
            return UpdateRecords("sp_updat_secRoleLoc", CommandType.StoredProcedure, param);
        }

        public Int32 Update_secRoleLocChannel(SecRoleLocChanel _secLocChnl)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_com;
            (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_roleid;
            (param[2] = new OracleParameter("p_chnnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_chnnl;
            (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_act == true ? 1 : 0;
            (param[4] = new OracleParameter("p_readonly", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_readonly == true ? 1 : 0;
            (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrl_cre_by;
            (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_cre_dt;
            (param[7] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_mod_by;
            (param[8] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secLocChnl.Ssrlc_mod_dt;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("sp_update_secRoleLocChnl", CommandType.StoredProcedure, param);
        }

        //-----------
        public Int32 Update_secRolePC(SecRolePC _secRolePC)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_com;
            (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_roleid;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_pc;
            (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_act;
            (param[4] = new OracleParameter("p_readonly", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_readonly;
            (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_cre_by;
            (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_cre_dt;
            (param[7] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_mod_by;
            (param[8] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secRolePC.Ssrp_mod_dt;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("sp_update_secRolePC", CommandType.StoredProcedure, param);
        }


        public Int32 Update_secRolePcChannel(SecRolePcChannel _secPcChnl)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_com;
            (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_roleid;
            (param[2] = new OracleParameter("p_chnnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_chnnl;
            (param[3] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_act;
            (param[4] = new OracleParameter("p_readonly", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_readonly;
            (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_cre_by;
            (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_cre_dt;
            (param[7] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_mod_by;
            (param[8] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _secPcChnl.Ssrpc_mod_dt;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("sp_update_secRolePcChnl", CommandType.StoredProcedure, param);
        }

        public DataTable Get_Sec_role_loc(string com, Int32 roleID, string loc)
        {
            //sp_get_SEC_ROLE_LOC(p_com in NVARCHAR2,p_roleID in NUMBER,p_loc in NVARCHAR2, c_data OUT sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_roleID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleID;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblloc", "sp_get_SEC_ROLE_LOC", CommandType.StoredProcedure, false, param);
        }

        public DataTable Get_Sec_role_locChannel(string com, Int32 roleID, string channel)
        {
            // sp_get_SEC_ROLE_LOC_CHANNEL(p_com in NVARCHAR2,p_roleID in NUMBER,p_chanel in NVARCHAR2, c_data OUT sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_roleID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleID;
            (param[2] = new OracleParameter("p_chanel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tbllch", "sp_get_SEC_ROLE_LOC_CHANNEL", CommandType.StoredProcedure, false, param);
        }

        public DataTable Get_Sec_role_pc(string com, Int32 roleID, string pc)
        {
            //sp_get_SEC_ROLE_PC(p_com in NVARCHAR2,p_roleID in NUMBER,p_pc in NVARCHAR2, c_data OUT sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_roleID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleID;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblpc", "sp_get_SEC_ROLE_PC", CommandType.StoredProcedure, false, param);
        }

        public DataTable Get_Sec_role_pcChannel(string com, Int32 roleID, string channel)
        {
            // sp_get_SEC_ROLE_PC_CHANNEL(p_com in NVARCHAR2,p_roleID in NUMBER,p_chanel in NVARCHAR2, c_data OUT sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_roleID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleID;
            (param[2] = new OracleParameter("p_chanel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblpch", "sp_get_SEC_ROLE_PC_CHANNEL", CommandType.StoredProcedure, false, param);
        }

        public static string Encrypt(string _plainText)
        {
            string _Encrypt = string.Empty;
            BaseDAL _bdal = new BaseDAL();
            string PasswordHash = _bdal.GetPasswordHash();
            if (string.IsNullOrEmpty(PasswordHash)) return _plainText;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(_plainText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            _Encrypt = Convert.ToBase64String(cipherTextBytes);

            return _Encrypt;
        }

        public static string Decrypt(string _encryptedText)
        {
            string _Decrypt = string.Empty;
            BaseDAL _bdal = new BaseDAL();
            string PasswordHash = _bdal.GetPasswordHash();
            if (string.IsNullOrEmpty(PasswordHash)) return _encryptedText;
            byte[] cipherTextBytes = Convert.FromBase64String(_encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            _Decrypt = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            return _Decrypt;
        }

        public DataTable GetCompanyUserRole(string _user, string _com)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblpch", "get_user_role", CommandType.StoredProcedure, false, param);
        }

        public int Change_Password(SystemUser _user)
        {
            string _pw = Encrypt(_user.Se_usr_pw);
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_user_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user.Se_usr_id;
            (param[1] = new OracleParameter("p_pw", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pw;
            (param[2] = new OracleParameter("p_chng_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user.Se_pw_chng_by;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_change_pw", CommandType.StoredProcedure, param);
        }

        public SecurityPolicy GetSecurityPolicy(int _seqNo)
        {
            SecurityPolicy _securityPolicy = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblUser", "sp_get_security_policy", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _securityPolicy = DataTableExtensions.ToGenericList<SecurityPolicy>(_dtResults, SecurityPolicy.Converter)[0];
            }
            return _securityPolicy;
        }

        public bool Check_Pw_History(string _user, string _pw, int _rem_pws)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[1] = new OracleParameter("p_num", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _rem_pws;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dt = QueryDataTable("tblloc", "sp_get_hist_pws", CommandType.StoredProcedure, false, param);
            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    if (_pw.ToString() == Decrypt(_dt.Rows[i]["sep_usr_pw"].ToString()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int Save_User_Falis(string _user, string _pw, string _com, string _ip, string _winusername, string _winuser)
        {
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_usrid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[1] = new OracleParameter("p_usrpw", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pw;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[3] = new OracleParameter("p_ip", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ip;
            (param[4] = new OracleParameter("p_winusrname", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _winusername;
            (param[5] = new OracleParameter("p_winusr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _winuser;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("sp_savesecuserfalis", CommandType.StoredProcedure, param);
        }

        public DataTable Get_SystemMenu()
        {
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_menu_permission", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable Get_SystemUsers(string _user, string _dept)
        {
            OracleParameter[] param = new OracleParameter[3];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[2] = new OracleParameter("in_dept", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dept;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_user_previlages", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable Get_SystemUserLoc(string _user, string _dept, string _type)
        {
            OracleParameter[] param = new OracleParameter[4];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[2] = new OracleParameter("in_dept", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dept;
            (param[3] = new OracleParameter("in_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_user_location", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable Get_SystemRole()
        {
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_role_def", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable Get_SystemMenuAssgnRole()
        {
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_menu_asgn_role", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable Get_SystemSpecialPerm()
        {
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_special_permission", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable Get_SystemRoleAssgnUser(string _user)
        {
            OracleParameter[] param = new OracleParameter[2];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_role_asgn_user", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Chamal 07/Apr/2015
        public Boolean Is_Report_DR(string _repName)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_rep", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _repName;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tbl", "sp_is_rep_dr", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;
        }

        //Tharka 2015-05-13
        public List<Main_menu_items> GetUserSystemMenusNew(string _user, string _company)
        {
            List<Main_menu_items> result = new List<Main_menu_items>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtSystemOpt = QueryDataTable("tblSystemOption", "SP_GETUSERSYSTEMMENUS", CommandType.StoredProcedure, false, param);
            ConnectionClose();
            if (_dtSystemOpt.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Main_menu_items>(_dtSystemOpt, Main_menu_items.Converter);
            }
            return result;
        }

        //Sahan 16 Jun 2015
        public DataTable SP_SCM2_GET_USRPW_STATUS(string P_USER)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = P_USER;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("sec_user", "SP_SCM2_GET_USRPW_STATUS", CommandType.StoredProcedure, false, param);
        }

        //Rukshan 04/Aug/2015

        public DataTable GetSBU_Company(string _COM, string _SBU)
        {
            //List<SystemUserCompany> _userCompList = null;
            //SystemUserCompany _systemUserComp = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_Com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _COM;
            (param[1] = new OracleParameter("p_Sbu", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _SBU;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbSBU", "SP_GetSBU", CommandType.StoredProcedure, false, param);
            return _dtResults;

        }

        public int Save_User_SBU(StrategicBusinessUnits _StrategicBusinessUnits)
        {
            OracleParameter[] param = new OracleParameter[11];
            (param[0] = new OracleParameter("p_seo_user_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_usr_id;
            (param[1] = new OracleParameter("p_seo_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_com_cd;
            (param[2] = new OracleParameter("p_seo_ope_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_ope_cd;
            (param[3] = new OracleParameter("p_seo_def_opecd", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_def_opecd;
            (param[4] = new OracleParameter("p_seo_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_act;
            (param[5] = new OracleParameter("p_seo_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_cre_by;
            (param[6] = new OracleParameter("p_seo_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_cre_dt;
            (param[7] = new OracleParameter("p_seo_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_mod_by;
            (param[8] = new OracleParameter("p_seo_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_mod_dt;
            (param[9] = new OracleParameter("p_seo_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _StrategicBusinessUnits.Seo_session_id;
            param[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("SP_SaveSBU", CommandType.StoredProcedure, param);
        }

        public DataTable GetSBU_User(string _COM, string _UID, string _SBU)
        {
            //List<SystemUserCompany> _userCompList = null;
            //SystemUserCompany _systemUserComp = null;

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_Com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _COM;
            (param[1] = new OracleParameter("p_Uid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UID;
            (param[2] = new OracleParameter("p_SBUCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _SBU;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbSBU", "SP_GetUserSBU", CommandType.StoredProcedure, false, param);
            return _dtResults;

        }

        public int CheckUserAvailability(string userId, string email)
        {
            int status = -100;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            (param[1] = new OracleParameter("p_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = email;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_CHECK_USERSTATUS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                status = Convert.ToInt32(_dtResults.Rows[0]["SE_ACT"].ToString());
            }
            return status;
        }

        public int UpdateUserResetPassword(string id, string email, string hash)
        {
            int row_aff = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = id;
                (param[1] = new OracleParameter("p_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = email;
                (param[2] = new OracleParameter("p_hash", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hash;
                param[3] = new OracleParameter("p_affect", OracleDbType.Int32, null, ParameterDirection.Output);


                row_aff = UpdateRecords("SP_UPDATE_SECUSERPWRESET", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row_aff;
        }

        public bool CheckPwResetAuth(string secTOkecn, string id)
        {
            bool user = false;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_token", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = secTOkecn;
            (param[1] = new OracleParameter("p_user_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = id;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbUser", "SP_GET_USERBYTOKEN", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                user = true;
            }
            return user;
        }


        public SystemUser GetTokecnUser(string secTOkecn, string id)
        {
            SystemUser user = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_token", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = secTOkecn;
            (param[1] = new OracleParameter("p_user_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = id;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbUser", "SP_GET_USERBYTOKEN", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                user = DataTableExtensions.ToGenericList<SystemUser>(_dtResults, SystemUser.converter)[0];

            }
            return user;
        }

        //Chamal 22/Feb/2017
        public DataTable GetUserLastLogTrans(string _com, string _userid, int _isFirst)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userid;
            (param[2] = new OracleParameter("p_first", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _isFirst;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tbluserlogtrans", "pkg_notifications.SP_LastLoginTransLog", CommandType.StoredProcedure, false, param);
        }

        //Dulaj 24-Jan-2018 
        public Int32 CheckMenuPermission(string _user, string _company, string _url)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
                (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
                (param[2] = new OracleParameter("p_url", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _url;

                OracleParameter orP = new OracleParameter("c_result", OracleDbType.Int32, null, ParameterDirection.Output);

                return ReturnSP_SingleValue("check_menu_permission", CommandType.StoredProcedure, orP, param);
            }
            catch (Exception) { return 0; }
        }
        //Dulaj 06-FEb-2018
        public Int32 CheckUrl(string _url)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[1];
                (param[0] = new OracleParameter("p_url", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _url;

                OracleParameter orP = new OracleParameter("c_result", OracleDbType.Int32, null, ParameterDirection.Output);

                return ReturnSP_SingleValue("check_url", CommandType.StoredProcedure, orP, param);
            }
            catch (Exception) { return 0; }
        }
        // Nuwan 2018.06.29
        public Int32 getUserSpecialPermission(string userid, string company, string permcd)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = permcd;
                OracleParameter orP = new OracleParameter("p_out", OracleDbType.Int32, null, ParameterDirection.Output);
                return ReturnSP_SingleValue("sp_check_rep_perm", CommandType.StoredProcedure, orP, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Chamal 12-Jul-2018
        public DataTable GetSUNDBLinks(string _sunServer)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("in_sunserver", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sunServer;
                param[1] = new OracleParameter("o_searchData", OracleDbType.RefCursor, null, ParameterDirection.Output);
                return QueryDataTable("tblusers", "sp_getsundblink", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Chamal 12-Jul-2018
        public DataTable GetSUNUser(string _domainID, string _sunDB)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("in_domainid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _domainID;
                (param[1] = new OracleParameter("in_sundb", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sunDB;
                param[2] = new OracleParameter("o_searchData", OracleDbType.RefCursor, null, ParameterDirection.Output);
                return QueryDataTable("tblusers", "sp_getsunuserid", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Chamal 13-Jul-2018
        public int ClearSunSession(string _sunID, string _sunDB)
        {
            int row_aff = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("IN_SUNID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sunID;
            (param[1] = new OracleParameter("IN_SUNDB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sunDB;
            param[2] = new OracleParameter("OUT_AFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            row_aff = UpdateRecords("SP_SUN_OWN_SESSION_CLEAR", CommandType.StoredProcedure, param);
            return row_aff;
        }

        //Chamal 23-JUl-2018
        public bool IsLoginSunRemote(string _ipAddress)
        {
            bool row_aff = false;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("in_ip", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ipAddress;
            param[1] = new OracleParameter("o_searchData", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dt =QueryDataTable("tblusers", "sp_issunremote", CommandType.StoredProcedure, false, param);
            if (_dt != null)
            {
                if (_dt.Rows.Count > 0)
                { row_aff = true; }
            }
            return row_aff;
        }

        public List<SEC_SYSTEM_MENU> getSystemUserMenu(string userid, string company, string system)
        {
            List<SEC_SYSTEM_MENU> menu = new List<SEC_SYSTEM_MENU>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[2] = new OracleParameter("p_system", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = system;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbUser", "SP_GET_PERMITEDMENUDET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                menu = DataTableExtensions.ToGenericList<SEC_SYSTEM_MENU>(_dtResults, SEC_SYSTEM_MENU.Converter);

            }
            return menu;
        }
    }
}
