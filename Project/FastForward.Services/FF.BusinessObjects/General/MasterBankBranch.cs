using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterBankBranch
    {
        #region Private Members
        private Boolean _mbb_active;
        private string _mbb_add1;
        private string _mbb_add2;
        private string _mbb_bus_cd;
        private string _mbb_bus_tp;
        private string _mbb_cd;
        private string _mbb_country_cd;
        private string _mbb_cre_by;
        private DateTime _mbb_cre_when;
        private string _mbb_desc;
        private string _mbb_email;
        private string _mbb_fax;
        private string _mbb_mod_by;
        private DateTime _mbb_mod_when;
        private string _mbb_session_id;
        private string _mbb_tel;
        private string _mbb_town_cd;
        private string _mbb_web;
        #endregion

        #region Public Property Definition
        public Boolean Mbb_active
        {
            get { return _mbb_active; }
            set { _mbb_active = value; }
        }
        public string Mbb_add1
        {
            get { return _mbb_add1; }
            set { _mbb_add1 = value; }
        }
        public string Mbb_add2
        {
            get { return _mbb_add2; }
            set { _mbb_add2 = value; }
        }
        public string Mbb_bus_cd
        {
            get { return _mbb_bus_cd; }
            set { _mbb_bus_cd = value; }
        }
        public string Mbb_bus_tp
        {
            get { return _mbb_bus_tp; }
            set { _mbb_bus_tp = value; }
        }
        public string Mbb_cd
        {
            get { return _mbb_cd; }
            set { _mbb_cd = value; }
        }
        public string Mbb_country_cd
        {
            get { return _mbb_country_cd; }
            set { _mbb_country_cd = value; }
        }
        public string Mbb_cre_by
        {
            get { return _mbb_cre_by; }
            set { _mbb_cre_by = value; }
        }
        public DateTime Mbb_cre_when
        {
            get { return _mbb_cre_when; }
            set { _mbb_cre_when = value; }
        }
        public string Mbb_desc
        {
            get { return _mbb_desc; }
            set { _mbb_desc = value; }
        }
        public string Mbb_email
        {
            get { return _mbb_email; }
            set { _mbb_email = value; }
        }
        public string Mbb_fax
        {
            get { return _mbb_fax; }
            set { _mbb_fax = value; }
        }
        public string Mbb_mod_by
        {
            get { return _mbb_mod_by; }
            set { _mbb_mod_by = value; }
        }
        public DateTime Mbb_mod_when
        {
            get { return _mbb_mod_when; }
            set { _mbb_mod_when = value; }
        }
        public string Mbb_session_id
        {
            get { return _mbb_session_id; }
            set { _mbb_session_id = value; }
        }
        public string Mbb_tel
        {
            get { return _mbb_tel; }
            set { _mbb_tel = value; }
        }
        public string Mbb_town_cd
        {
            get { return _mbb_town_cd; }
            set { _mbb_town_cd = value; }
        }
        public string Mbb_web
        {
            get { return _mbb_web; }
            set { _mbb_web = value; }
        }
        #endregion

        public static MasterBankBranch Converter(DataRow row)
        {
            return new MasterBankBranch
            {
                Mbb_active = row["MBB_ACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(row["MBB_ACTIVE"]),
                Mbb_add1 = row["MBB_ADD1"] == DBNull.Value ? string.Empty : row["MBB_ADD1"].ToString(),
                Mbb_add2 = row["MBB_ADD2"] == DBNull.Value ? string.Empty : row["MBB_ADD2"].ToString(),
                Mbb_bus_cd = row["MBB_BUS_CD"] == DBNull.Value ? string.Empty : row["MBB_BUS_CD"].ToString(),
                Mbb_bus_tp = row["MBB_BUS_TP"] == DBNull.Value ? string.Empty : row["MBB_BUS_TP"].ToString(),
                Mbb_cd = row["MBB_CD"] == DBNull.Value ? string.Empty : row["MBB_CD"].ToString(),
                Mbb_country_cd = row["MBB_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBB_COUNTRY_CD"].ToString(),
                Mbb_cre_by = row["MBB_CRE_BY"] == DBNull.Value ? string.Empty : row["MBB_CRE_BY"].ToString(),
                Mbb_cre_when = row["MBB_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBB_CRE_WHEN"]),
                Mbb_desc = row["MBB_DESC"] == DBNull.Value ? string.Empty : row["MBB_DESC"].ToString(),
                Mbb_email = row["MBB_EMAIL"] == DBNull.Value ? string.Empty : row["MBB_EMAIL"].ToString(),
                Mbb_fax = row["MBB_FAX"] == DBNull.Value ? string.Empty : row["MBB_FAX"].ToString(),
                Mbb_mod_by = row["MBB_MOD_BY"] == DBNull.Value ? string.Empty : row["MBB_MOD_BY"].ToString(),
                Mbb_mod_when = row["MBB_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBB_MOD_WHEN"]),
                Mbb_session_id = row["MBB_SESSION_ID"] == DBNull.Value ? string.Empty : row["MBB_SESSION_ID"].ToString(),
                Mbb_tel = row["MBB_TEL"] == DBNull.Value ? string.Empty : row["MBB_TEL"].ToString(),
                Mbb_town_cd = row["MBB_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBB_TOWN_CD"].ToString(),
                Mbb_web = row["MBB_WEB"] == DBNull.Value ? string.Empty : row["MBB_WEB"].ToString()

            };
        }
    }
}


