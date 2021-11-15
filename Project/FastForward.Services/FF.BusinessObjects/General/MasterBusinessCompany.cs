using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
   public class MasterBusinessCompany
    {
        #region Private Members
        private string _mbe_acc_cd;
        private Boolean _mbe_act;
        private string _mbe_add1;
        private string _mbe_add2;
        private string _mbe_cd;
        private string _mbe_com;
        private string _mbe_contact;
        private string _mbe_country_cd;
        private string _mbe_distric_cd;
        private string _mbe_email;
        private string _mbe_fax;
        private Boolean _mbe_is_suspend;
        private Boolean _mbe_is_tax;
        private string _mbe_mob;
        private string _mbe_name;
        private string _mbe_nic;
        private string _mbe_province_cd;
        private string _mbe_sub_tp;
        private string _mbe_tax_no;
        private string _mbe_tel;
        private string _mbe_town_cd;
        private string _mbe_tp;

        #endregion

        public string Mbe_acc_cd { get { return _mbe_acc_cd; } set { _mbe_acc_cd = value; } }
        public Boolean Mbe_act { get { return _mbe_act; } set { _mbe_act = value; } }
        public string Mbe_add1 { get { return _mbe_add1; } set { _mbe_add1 = value; } }
        public string Mbe_add2 { get { return _mbe_add2; } set { _mbe_add2 = value; } }
        public string Mbe_cd { get { return _mbe_cd; } set { _mbe_cd = value; } }
        public string Mbe_com { get { return _mbe_com; } set { _mbe_com = value; } }
        public string Mbe_contact { get { return _mbe_contact; } set { _mbe_contact = value; } }
        public string Mbe_country_cd { get { return _mbe_country_cd; } set { _mbe_country_cd = value; } }
        public string Mbe_distric_cd { get { return _mbe_distric_cd; } set { _mbe_distric_cd = value; } }
        public string Mbe_email { get { return _mbe_email; } set { _mbe_email = value; } }
        public string Mbe_fax { get { return _mbe_fax; } set { _mbe_fax = value; } }
        public Boolean Mbe_is_suspend { get { return _mbe_is_suspend; } set { _mbe_is_suspend = value; } }
        public Boolean Mbe_is_tax { get { return _mbe_is_tax; } set { _mbe_is_tax = value; } }
        public string Mbe_mob { get { return _mbe_mob; } set { _mbe_mob = value; } }
        public string Mbe_name { get { return _mbe_name; } set { _mbe_name = value; } }
        public string Mbe_nic { get { return _mbe_nic; } set { _mbe_nic = value; } }
        public string Mbe_province_cd { get { return _mbe_province_cd; } set { _mbe_province_cd = value; } }
        public string Mbe_sub_tp { get { return _mbe_sub_tp; } set { _mbe_sub_tp = value; } }
        public string Mbe_tax_no { get { return _mbe_tax_no; } set { _mbe_tax_no = value; } }
        public string Mbe_tel { get { return _mbe_tel; } set { _mbe_tel = value; } }
        public string Mbe_town_cd { get { return _mbe_town_cd; } set { _mbe_town_cd = value; } }
        public string Mbe_tp { get { return _mbe_tp; } set { _mbe_tp = value; } }


        public static MasterBusinessCompany ConvertTotal(DataRow row)
        {
            return new MasterBusinessCompany
            {
                Mbe_acc_cd = row["MBE_ACC_CD"] == DBNull.Value ? string.Empty : row["MBE_ACC_CD"].ToString(),
                Mbe_act = row["MBE_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_ACT"]),
                Mbe_add1 = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                Mbe_add2 = row["MBE_ADD2"] == DBNull.Value ? string.Empty : row["MBE_ADD2"].ToString(),
                Mbe_cd = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                Mbe_com = row["MBE_COM"] == DBNull.Value ? string.Empty : row["MBE_COM"].ToString(),
                Mbe_contact = row["MBE_CONTACT"] == DBNull.Value ? string.Empty : row["MBE_CONTACT"].ToString(),
                Mbe_country_cd = row["MBE_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_COUNTRY_CD"].ToString(),
                Mbe_distric_cd = row["MBE_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_DISTRIC_CD"].ToString(),
                Mbe_email = row["MBE_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_EMAIL"].ToString(),
                Mbe_fax = row["MBE_FAX"] == DBNull.Value ? string.Empty : row["MBE_FAX"].ToString(),
                Mbe_is_suspend = row["MBE_IS_SUSPEND"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_SUSPEND"]),
                Mbe_is_tax = row["MBE_IS_TAX"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_TAX"]),
                Mbe_mob = row["MBE_MOB"] == DBNull.Value ? string.Empty : row["MBE_MOB"].ToString(),
                Mbe_name = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                Mbe_nic = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString(),
                Mbe_province_cd = row["MBE_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_PROVINCE_CD"].ToString(),
                Mbe_sub_tp = row["MBE_SUB_TP"] == DBNull.Value ? string.Empty : row["MBE_SUB_TP"].ToString(),
                Mbe_tax_no = row["MBE_TAX_NO"] == DBNull.Value ? string.Empty : row["MBE_TAX_NO"].ToString(),
                Mbe_tel = row["MBE_TEL"] == DBNull.Value ? string.Empty : row["MBE_TEL"].ToString(),
                Mbe_town_cd = row["MBE_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_TOWN_CD"].ToString(),
                Mbe_tp = row["MBE_TP"] == DBNull.Value ? string.Empty : row["MBE_TP"].ToString()


            };
        }
    }
}

