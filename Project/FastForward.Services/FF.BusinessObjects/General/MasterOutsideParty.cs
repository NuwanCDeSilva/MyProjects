using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterOutsideParty
    {
        #region Private Members
        private Boolean _mbi_act;
        private string _mbi_add1;
        private string _mbi_add2;
        private string _mbi_cd;
        private string _mbi_country_cd;
        private string _mbi_cre_by;
        private DateTime _mbi_cre_when;
        private string _mbi_desc;
        private string _mbi_email;
        private string _mbi_fax;
        private string _mbi_id;
        private Boolean _mbi_issub;
        private string _mbi_mod_by;
        private DateTime _mbi_mod_dt;
        private string _mbi_session_id;
        private string _mbi_tax1;
        private string _mbi_tax2;
        private string _mbi_tax3;
        private string _mbi_tel;
        private string _mbi_town_cd;
        private string _mbi_tp;
        private string _mbi_web;
        private string _mbi_br_no;
        private string _mbi_sun_bank;

        #endregion

        public Boolean Mbi_act
        {
            get { return _mbi_act; }
            set { _mbi_act = value; }
        }
        public string Mbi_add1
        {
            get { return _mbi_add1; }
            set { _mbi_add1 = value; }
        }
        public string Mbi_add2
        {
            get { return _mbi_add2; }
            set { _mbi_add2 = value; }
        }
        public string Mbi_cd
        {
            get { return _mbi_cd; }
            set { _mbi_cd = value; }
        }
        public string Mbi_country_cd
        {
            get { return _mbi_country_cd; }
            set { _mbi_country_cd = value; }
        }
        public string Mbi_cre_by
        {
            get { return _mbi_cre_by; }
            set { _mbi_cre_by = value; }
        }
        public DateTime Mbi_cre_when
        {
            get { return _mbi_cre_when; }
            set { _mbi_cre_when = value; }
        }
        public string Mbi_desc
        {
            get { return _mbi_desc; }
            set { _mbi_desc = value; }
        }
        public string Mbi_email
        {
            get { return _mbi_email; }
            set { _mbi_email = value; }
        }
        public string Mbi_fax
        {
            get { return _mbi_fax; }
            set { _mbi_fax = value; }
        }
        public string Mbi_id
        {
            get { return _mbi_id; }
            set { _mbi_id = value; }
        }
        public Boolean Mbi_issub
        {
            get { return _mbi_issub; }
            set { _mbi_issub = value; }
        }
        public string Mbi_mod_by
        {
            get { return _mbi_mod_by; }
            set { _mbi_mod_by = value; }
        }
        public DateTime Mbi_mod_dt
        {
            get { return _mbi_mod_dt; }
            set { _mbi_mod_dt = value; }
        }
        public string Mbi_session_id
        {
            get { return _mbi_session_id; }
            set { _mbi_session_id = value; }
        }
        public string Mbi_tax1
        {
            get { return _mbi_tax1; }
            set { _mbi_tax1 = value; }
        }
        public string Mbi_tax2
        {
            get { return _mbi_tax2; }
            set { _mbi_tax2 = value; }
        }
        public string Mbi_tax3
        {
            get { return _mbi_tax3; }
            set { _mbi_tax3 = value; }
        }
        public string Mbi_tel
        {
            get { return _mbi_tel; }
            set { _mbi_tel = value; }
        }
        public string Mbi_town_cd
        {
            get { return _mbi_town_cd; }
            set { _mbi_town_cd = value; }
        }
        public string Mbi_tp
        {
            get { return _mbi_tp; }
            set { _mbi_tp = value; }
        }
        public string Mbi_web
        {
            get { return _mbi_web; }
            set { _mbi_web = value; }
        }

        public string Mbi_br_no
        {
            get { return _mbi_br_no; }
            set { _mbi_br_no = value; }
        }

        public string Mbi_sun_bank
        {
            get { return _mbi_sun_bank; }
            set { _mbi_sun_bank = value; }
        }


        public static MasterOutsideParty ConvertTotal(DataRow row)
        {
            return new MasterOutsideParty
            {
                Mbi_act = row["MBI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBI_ACT"]),
                Mbi_add1 = row["MBI_ADD1"] == DBNull.Value ? string.Empty : row["MBI_ADD1"].ToString(),
                Mbi_add2 = row["MBI_ADD2"] == DBNull.Value ? string.Empty : row["MBI_ADD2"].ToString(),
                Mbi_cd = row["MBI_CD"] == DBNull.Value ? string.Empty : row["MBI_CD"].ToString(),
                Mbi_country_cd = row["MBI_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBI_COUNTRY_CD"].ToString(),
                Mbi_cre_by = row["MBI_CRE_BY"] == DBNull.Value ? string.Empty : row["MBI_CRE_BY"].ToString(),
                Mbi_cre_when = row["MBI_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBI_CRE_WHEN"]),
                Mbi_desc = row["MBI_DESC"] == DBNull.Value ? string.Empty : row["MBI_DESC"].ToString(),
                Mbi_email = row["MBI_EMAIL"] == DBNull.Value ? string.Empty : row["MBI_EMAIL"].ToString(),
                Mbi_fax = row["MBI_FAX"] == DBNull.Value ? string.Empty : row["MBI_FAX"].ToString(),
                Mbi_id = row["MBI_ID"] == DBNull.Value ? string.Empty : row["MBI_ID"].ToString(),
                Mbi_issub = row["MBI_ISSUB"] == DBNull.Value ? false : Convert.ToBoolean(row["MBI_ISSUB"]),
                Mbi_mod_by = row["MBI_MOD_BY"] == DBNull.Value ? string.Empty : row["MBI_MOD_BY"].ToString(),
                Mbi_mod_dt = row["MBI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBI_MOD_DT"]),
                Mbi_session_id = row["MBI_SESSION_ID"] == DBNull.Value ? string.Empty : row["MBI_SESSION_ID"].ToString(),
                Mbi_tax1 = row["MBI_TAX1"] == DBNull.Value ? string.Empty : row["MBI_TAX1"].ToString(),
                Mbi_tax2 = row["MBI_TAX2"] == DBNull.Value ? string.Empty : row["MBI_TAX2"].ToString(),
                Mbi_tax3 = row["MBI_TAX3"] == DBNull.Value ? string.Empty : row["MBI_TAX3"].ToString(),
                Mbi_tel = row["MBI_TEL"] == DBNull.Value ? string.Empty : row["MBI_TEL"].ToString(),
                Mbi_town_cd = row["MBI_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBI_TOWN_CD"].ToString(),
                Mbi_tp = row["MBI_TP"] == DBNull.Value ? string.Empty : row["MBI_TP"].ToString(),
                Mbi_web = row["MBI_WEB"] == DBNull.Value ? string.Empty : row["MBI_WEB"].ToString(),
                Mbi_sun_bank = row["MBI_SUN_BANK"] == DBNull.Value ? string.Empty : row["MBI_SUN_BANK"].ToString(),
                Mbi_br_no = row["MBI_BR_NO"] == DBNull.Value ? string.Empty : row["MBI_BR_NO"].ToString()

            };
        }
    }
}

