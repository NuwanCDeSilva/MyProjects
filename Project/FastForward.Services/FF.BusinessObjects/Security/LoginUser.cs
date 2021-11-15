using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//Add Chamal 23-Sep-2013
namespace FF.BusinessObjects
{
    public class LoginUser
    {
        #region private members

        private string _user_name = string.Empty;
        private string _user_dept_id = string.Empty;
        private string _user_dept_name = string.Empty;
        private string _user_category = string.Empty;
        private string _new_sys_ver = string.Empty;
        private string _ip_address = string.Empty;
        private string _login_time = string.Empty;
        private string _user_def_loc = string.Empty;
        private string _user_def_bin = string.Empty;
        private string _user_def_pc = string.Empty;
        private bool _is_man_chk_loc = true;
        private bool _is_man_chk_pc = true;
        private MasterProfitCenter _mst_pc = null;
        private DataTable _chnl_defn = null;
        private List<PriceDefinitionRef> _price_defn = null;
        private bool _is_sale_roundup = false;
        private string _def_chnl = string.Empty;
        private string _def_schnl = string.Empty;
        private string _def_area = string.Empty;
        private string _def_Region = string.Empty;
        private string _def_zone = string.Empty;
        private DataTable _com_itm_status = null;
        private string _user_session_id = string.Empty;
        private int _login_attempts = 0;
        private bool _pw_must_change = false;
        private bool _pw_expire = false;
        private bool _pw_expire_must_change = false;
        private int _remaining_days = 0;
        private MasterCompany _mst_com = null;
        private Service_Chanal_parameter _chl_para = null;

        //added by nuwan 11/12/2015
        private string _password = string.Empty;
        

        #endregion

        #region public property definition

        public string User_name { get { return _user_name; } set { _user_name = value; } }

        public string User_dept_id { get { return _user_dept_id; } set { _user_dept_id = value; } }

        public string User_dept_name { get { return _user_dept_name; } set { _user_dept_name = value; } }

        public string User_category { get { return _user_category; } set { _user_category = value; } }

        public string New_sys_ver { get { return _new_sys_ver; } set { _new_sys_ver = value; } }

        public string Ip_address { get { return _ip_address; } set { _ip_address = value; } }

        public string Login_time { get { return _login_time; } set { _login_time = value; } }

        public string User_def_loc { get { return _user_def_loc; } set { _user_def_loc = value; } }

        public string User_def_bin { get { return _user_def_bin; } set { _user_def_bin = value; } }

        public string User_def_pc { get { return _user_def_pc; } set { _user_def_pc = value; } }

        public bool Is_man_chk_loc { get { return _is_man_chk_loc; } set { _is_man_chk_loc = value; } }

        public bool Is_man_chk_pc { get { return _is_man_chk_pc; } set { _is_man_chk_pc = value; } }

        public MasterProfitCenter Mst_pc { get { return _mst_pc; } set { _mst_pc = value; } }

        public DataTable Chnl_defn { get { return _chnl_defn; } set { _chnl_defn = value; } }

        public List<PriceDefinitionRef> Price_defn { get { return _price_defn; } set { _price_defn = value; } }

        public bool Is_sale_roundup { get { return _is_sale_roundup; } set { _is_sale_roundup = value; } }

        public string Def_chnl { get { return _def_chnl; } set { _def_chnl = value; } }

        public string Def_schnl { get { return _def_schnl; } set { _def_schnl = value; } }

        public string Def_area { get { return _def_area; } set { _def_area = value; } }

        public string Def_Region { get { return _def_Region; } set { _def_Region = value; } }

        public string Def_zone { get { return _def_zone; } set { _def_zone = value; } }

        public DataTable Com_itm_status { get { return _com_itm_status; } set { _com_itm_status = value; } }

        public string User_session_id { get { return _user_session_id; } set { _user_session_id = value; } }

        public int Login_attempts { get { return _login_attempts; } set { _login_attempts = value; } }

        public bool Pw_must_change { get { return _pw_must_change; } set { _pw_must_change = value; } }

        public bool Pw_expire { get { return _pw_expire; } set { _pw_expire = value; } }

        public bool Pw_expire_must_change { get { return _pw_expire_must_change; } set { _pw_expire_must_change = value; } }

        public int Remaining_days { get { return _remaining_days; } set { _remaining_days = value; } }

        public MasterCompany Mst_com { get { return _mst_com; } set { _mst_com = value; } }

        public Service_Chanal_parameter Chl_para { get { return _chl_para; } set { _chl_para = value; } }


        //added by nuwan 11/12/2015 for asycuda mvc project
        public string Password { get { return _password; } set { _password = value; } }
        #endregion

    }
}
