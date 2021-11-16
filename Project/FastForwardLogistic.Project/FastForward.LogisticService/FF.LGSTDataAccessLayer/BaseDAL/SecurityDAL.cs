using FF.BusinessObjects.Security;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FF.DataAccessLayer.BaseDAL
{
    public class SecurityDAL : BaseDAL
    {
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
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
        /// <summary>
        /// get user company
        /// added by nuwan  2017-05-23
        /// </summary>
        /// <param name="_UserComp">user company</param>
        /// <returns>User company list</returns>
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
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get location details
        /// </summary>
        /// <param name="_CompCode">company code</param>
        /// <param name="_LocCode">location code</param>
        /// <returns>location details</returns>
        public MasterLocation GetLocationByLocCode(string _CompCode, string _LocCode)
        {
            MasterLocation _locList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _CompCode;
            (param[1] = new OracleParameter("p_loc_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _LocCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblloc", "sp_get_location_by_code", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _locList = DataTableExtensions.ToGenericList<MasterLocation>(_dtResults, MasterLocation.ConverterTotal)[0];
            }
            return _locList;
        }

        // Added by Chathura on 14-sep-2017 commented below
        public List<SystemUserProf> GetUserProfCenters(string UserID, string Comp, string User_def_chnl)
        {
            List<SystemUserProf> _userList = null;

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (param[1] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            (param[2] = new OracleParameter("p_chnlcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User_def_chnl;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserProf", "SP_GETUSERPROFBYCHNL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUserProf>(_dtResults, SystemUserProf.Converter);
            }
            return _userList;
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
        //    List<SystemUserProf> _userList = null;

        //    OracleParameter[] param = new OracleParameter[3];
        //    (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
        //    (param[1] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
        //    param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

        //    DataTable _dtResults = QueryDataTable("tblUserProf", "SP_GETUSERPROF", CommandType.StoredProcedure, false, param);

        //    if (_dtResults.Rows.Count > 0)
        //    {
        //        _userList = DataTableExtensions.ToGenericList<SystemUserProf>(_dtResults, SystemUserProf.Converter);
        //    }
        //    return _userList;
        //}
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get user locations
        /// </summary>
        /// <param name="UserID">user id</param>
        /// <param name="Comp">company</param>
        /// <returns>user location list</returns>
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
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get system current version number
        /// </summary>
        /// <returns>string</returns>
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
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get security policy details
        /// </summary>
        /// <param name="_seqNo">seq number</param>
        /// <returns> security policy</returns>
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
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get user details
        /// </summary>
        /// <param name="_UserID">user id</param>
        /// <returns>systemuser</returns>
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
        /// <summary>
        /// added by nuwan  2017-05-23
        /// decrypt password
        /// </summary>
        /// <param name="_encryptedText">encrypted text</param>
        /// <returns>decrypt string</returns>
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
        /// <summary>
        /// added by nuwan  2017-05-23
        /// encrypt text
        /// </summary>
        /// <param name="_plainText">plain text</param>
        /// <returns>encrypted text</returns>
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
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get all departments
        /// </summary>
        /// <returns>list of departments</returns>
        public List<MasterDepartment> GetDepartment()
        {
            List<MasterDepartment> _userDepartment = null;

            DataTable _dtResults = QueryDataTable("tblUserDepartment", "sp_getdept", CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));

            if (_dtResults.Rows.Count > 0)
            {
                _userDepartment = DataTableExtensions.ToGenericList<MasterDepartment>(_dtResults, MasterDepartment.DeptConverter);
            }
            return _userDepartment;
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get company details by code
        /// </summary>
        /// <param name="_CompCode">company code</param>
        /// <returns>company details</returns>
        public MasterCompany GetCompByCode(string _CompCode)
        {
            MasterCompany _userList = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _CompCode;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblcomp", "SP_GET_COMP_BY_CODE", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<MasterCompany>(_dtResults, MasterCompany.Converter)[0];
            }
            return _userList;
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get user default bin code
        /// </summary>
        /// <param name="company">company code</param>
        /// <param name="location">location code</param>
        /// <returns>default bin code</returns>
        public string Get_default_binCD(string company, string location)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query inr_bin_loc.
            DataTable _dtResults = QueryDataTable("tblBin_loc", "sp_getdefbin", CommandType.StoredProcedure, false, param);
            string binCD = null;
            if (_dtResults.Rows.Count > 0)
            {
                foreach (DataRow tr in _dtResults.Rows)
                {
                    binCD = Convert.ToString(tr["ibl_bin_cd"]);
                }
            }

            return binCD;
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get profit center details
        /// </summary>
        /// <param name="_company">company code</param>
        /// <param name="_profitCenter">profit center</param>
        /// <returns>profit center details</returns>
        public MasterProfitCenter GetProfitCenter(string _company, string _profitCenter)
        {
            MasterProfitCenter _masterProfitCenter = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pcenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitCenter;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblProfitCenter = QueryDataTable("tblPc", "sp_getprofitcenterdetail", CommandType.StoredProcedure, false, param);

            if (_tblProfitCenter.Rows.Count > 0)
            {
                _masterProfitCenter = DataTableExtensions.ToGenericList<MasterProfitCenter>(_tblProfitCenter, MasterProfitCenter.ConvertTotal)[0];
            }

            return _masterProfitCenter;
        }
        //Dilshan 31/08/2017
        public MasterProfitCenter GetUserCompanySet(string _company, string _profitCenter)
        {
            MasterProfitCenter _masterProfitCenter = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pcenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitCenter;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //DataTable _tblProfitCenter = QueryDataTable("tblPc", "sp_getprofitcenterdetail", CommandType.StoredProcedure, false, param);
            DataTable _tblProfitCenter = QueryDataTable("tblPc", "sp_getcompanydetail", CommandType.StoredProcedure, false, param);

            if (_tblProfitCenter.Rows.Count > 0)
            {
                _masterProfitCenter = DataTableExtensions.ToGenericList<MasterProfitCenter>(_tblProfitCenter, MasterProfitCenter.ConvertTotal)[0];
            }

            return _masterProfitCenter;
        }

        /// <summary>
        /// added by nuwan  2017-05-23
        /// Return Price definition for the specific profit center,invoice type ,book and the level
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_profitCenter"></param>
        /// <param name="_invType"></param>
        /// <param name="_book"></param>
        /// <param name="_level"></param>
        /// <returns>Return Price definition for the specific profit center,invoice type ,book and the level</returns>
        public List<PriceDefinitionRef> GetPriceDefinitionByBookAndLevel(string _company, string _book, string _level, string _invoiceType, string _profitCenter)
        {
            // sp_getpricedefinitionbybook   (p_com in NVARCHAR2,p_book in NVARCHAR2,p_level in NVARCHAR2,p_invtype in NVARCHAR2,p_profit in NVARCHAR2,c_data OUT sys_refcursor)
            List<PriceDefinitionRef> _priceDefinitionRef = new List<PriceDefinitionRef>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pbook", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[2] = new OracleParameter("p_plevel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _level;

            (param[3] = new OracleParameter("p_invtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceType;
            (param[4] = new OracleParameter("p_profit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitCenter;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblPriceDefinition = QueryDataTable("tblPriceDefinition", "sp_getpricedefinitionbybook", CommandType.StoredProcedure, false, param);

            if (_tblPriceDefinition.Rows.Count > 0)
            {
                _priceDefinitionRef = DataTableExtensions.ToGenericList<PriceDefinitionRef>(_tblPriceDefinition, PriceDefinitionRef.ConvertTotal);
            }

            return _priceDefinitionRef;

        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get company channel details
        /// </summary>
        /// <param name="_company">company code</param>
        /// <param name="_channel">channel code</param>
        /// <returns>datatable</returns>
        public DataTable GetChannelDetail(string _company, string _channel)
        {
            //sp_getchannel(p_com in NVARCHAR2,p_chnl in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _channel;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResult = QueryDataTable("tblSpgetchannel", "sp_getchannel", CommandType.StoredProcedure, false, param);
            return _dtResult;
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// 
        /// </summary>
        /// <param name="_company">company</param>
        /// <returns>Datatable</returns>
        public DataTable IsSaleFigureRoundUp(string _company)
        {
            //sp_getcompanyroundup(p_com in NVARCHAR2,c_data out sys_refcursor) is
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _result = QueryDataTable("tblCat", "sp_getcompanyroundup", CommandType.StoredProcedure, false, param);
            return _result;
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// get profit center hiarachy
        /// </summary>
        /// <param name="_company">company</param>
        /// <param name="_profitCenter">profit center</param>
        /// <returns>hiarachy datatable</returns>
        public DataTable Get_PC_Hierarchy(string _company, string _profitCenter)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitCenter;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_pc_heirachy", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        /// <summary>
        /// added by nuwan  2017-05-23
        /// Get All Company Item Status according to the logged user company
        /// </summary>
        /// <param name="_company">Company code</param>
        /// <returns>Status/Description</returns>
        public DataTable GetAllCompanyStatus(string _company)
        {
            //Query Data base.
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            ConnectionOpen();
            DataTable _dtItemStatus = QueryDataTable("tblCompanyItemStatus", "sp_scansearchstatus", CommandType.StoredProcedure, false, param);
            //ConnectionClose();

            return _dtItemStatus;
        }

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

        public List<SEC_SYS_ROLE> getUserRole(string loginUser, string company)
        {
            List<SEC_SYS_ROLE> role = new List<SEC_SYS_ROLE>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loginUser;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tbl = QueryDataTable("tbl", "SP_GET_USERROLES", CommandType.StoredProcedure, false, param);

            if (_tbl.Rows.Count > 0)
            {
                role = DataTableExtensions.ToGenericList<SEC_SYS_ROLE>(_tbl, SEC_SYS_ROLE.Converter);
            }

            return role;
        }

        public List<SEC_SYSTEM_MENU> getUserMenus(string userRoleIds, bool exists, string company)
        {
            List<SEC_SYSTEM_MENU> menu = new List<SEC_SYSTEM_MENU>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_all", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = (exists) ? 1 : 0;
            (param[2] = new OracleParameter("p_roleid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userRoleIds;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tbl = QueryDataTable("tbl", "SP_GET_USERMENU", CommandType.StoredProcedure, false, param);

            if (_tbl.Rows.Count > 0)
            {
                menu = DataTableExtensions.ToGenericList<SEC_SYSTEM_MENU>(_tbl, SEC_SYSTEM_MENU.Converter);
            }

            return menu;
        }

        public SEC_SYSTEM_MENU getUserMenusPermission(string userRoleIds, bool exists, string company, Int32 menuId)
        {
            SEC_SYSTEM_MENU menu = new SEC_SYSTEM_MENU();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_all", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = (exists) ? 1 : 0;
            (param[2] = new OracleParameter("p_roleid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userRoleIds;
            (param[3] = new OracleParameter("p_menuid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = menuId;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tbl = QueryDataTable("tbl", "SP_GET_USERMENUPERMISSION", CommandType.StoredProcedure, false, param);

            if (_tbl.Rows.Count > 0)
            {
                menu = DataTableExtensions.ToGenericList<SEC_SYSTEM_MENU>(_tbl, SEC_SYSTEM_MENU.Converter)[0];
            }

            return menu;
        }

        public int Is_OptionPerimitted(string userCompany, string userId, int optionCode)
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

        // Added by Chathura on 14-Sep-2017
        public List<SystemUserChannel> GetUserChannels(string UserID, string Comp)
        {
            List<SystemUserChannel> _userList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (param[1] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserProf", "SP_GETUSERCHANEL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUserChannel>(_dtResults, SystemUserChannel.Converter);
            }
            return _userList;
        }

        public int Save_User_Roles(string company, string roleid, string rolename, string description, string createdby, string modifiedby, string choice, string active, string session)
        {
            try
            {
                Int32 effects = 0;
                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleid;
                (param[2] = new OracleParameter("p_rolename", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = rolename;
                (param[3] = new OracleParameter("p_description", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = description;
                (param[4] = new OracleParameter("p_createdby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = createdby;
                (param[5] = new OracleParameter("p_modifiedby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = modifiedby;
                (param[6] = new OracleParameter("p_choice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = choice;
                (param[7] = new OracleParameter("p_session", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = session;
                (param[8] = new OracleParameter("p_active", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = active;
                param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_USER_ROLE", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save_User_Details(string p_log_user, string p_Se_usr_id, string p_Se_usr_desc, string p_Se_usr_name, string p_Se_usr_pw, string p_Se_usr_cat, string p_Se_dept_id, string p_Se_emp_id, string p_Se_emp_cd,
                                     string p_Se_isdomain, string p_Se_iswinauthend, string p_Se_SUN_ID, string p_se_Email, string p_se_Mob, string p_se_Phone, string p_Se_act, string p_Se_ischange_pw, string p_Se_pw_mustchange,
                                     string p_choice, string p_ispassword, string p_se_act_rmk)
        {
            try
            {
                Int32 effects = 0;
                OracleParameter[] param = new OracleParameter[22];
                (param[0] = new OracleParameter("p_log_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_log_user;
                (param[1] = new OracleParameter("p_Se_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_usr_id;
                (param[2] = new OracleParameter("p_Se_usr_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_usr_desc;
                (param[3] = new OracleParameter("p_Se_usr_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_usr_name;
                (param[4] = new OracleParameter("p_Se_usr_pw", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_usr_pw;
                (param[5] = new OracleParameter("p_Se_usr_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_usr_cat;
                (param[6] = new OracleParameter("p_Se_dept_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_dept_id;
                (param[7] = new OracleParameter("p_Se_emp_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_emp_id;
                (param[8] = new OracleParameter("p_Se_emp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_emp_cd;
                (param[9] = new OracleParameter("p_Se_isdomain", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_isdomain;
                (param[10] = new OracleParameter("p_Se_iswinauthend", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_iswinauthend;
                (param[11] = new OracleParameter("p_Se_SUN_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_SUN_ID;
                (param[12] = new OracleParameter("p_se_Email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_se_Email;
                (param[13] = new OracleParameter("p_se_Mob", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_se_Mob;
                (param[14] = new OracleParameter("p_se_Phone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_se_Phone;
                (param[15] = new OracleParameter("p_Se_act", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_act;
                (param[16] = new OracleParameter("p_Se_ischange_pw", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_ischange_pw;
                (param[17] = new OracleParameter("p_Se_pw_mustchange", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_Se_pw_mustchange;
                (param[18] = new OracleParameter("p_choice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_choice;
                (param[19] = new OracleParameter("p_ispassword", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_ispassword;
                (param[20] = new OracleParameter("p_se_act_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_se_act_rmk;
                param[21] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_USER_DETAILS", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Remove_User_Company(string company, string userid)
        {
            try
            {
                Int32 effects = 0;
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_DELETE_USER_COMPANY", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Update_User_Company(string p_company, string p_userid, string p_isactive, string p_isdefault, string p_modeuser, string p_session)
        {
            try
            {
                Int32 effects = 0;
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_userid;
                (param[2] = new OracleParameter("p_isactive", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_isactive;
                (param[3] = new OracleParameter("p_isdefault", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_isdefault;
                (param[4] = new OracleParameter("p_modeuser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_modeuser;
                (param[5] = new OracleParameter("p_session", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_session;
                param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_USER_COMPANY", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SEC_SYSTEM_MENU> getMenusAll()
        {
            List<SEC_SYSTEM_MENU> menu = new List<SEC_SYSTEM_MENU>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tbl = QueryDataTable("tblMenu", "SP_GET_USERMENU_ALL", CommandType.StoredProcedure, false, param);

            if (_tbl.Rows.Count > 0)
            {
                menu = DataTableExtensions.ToGenericList<SEC_SYSTEM_MENU>(_tbl, SEC_SYSTEM_MENU.Converter);
            }

            return menu;
        }

        public int Update_Company_Menu_List(string p_company, string roleid, string p_optiid, string p_isactive)
        {
            try
            {
                Int32 effects = 0;
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleid;
                (param[2] = new OracleParameter("p_active", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(p_isactive);
                (param[3] = new OracleParameter("p_optiid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(p_optiid);
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_MENU_DETAILS", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
