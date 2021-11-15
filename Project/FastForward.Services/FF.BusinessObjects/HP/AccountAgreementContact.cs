using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //create by = shani on 25-07-2013
    //table = hpt_accounts_agreement_contact
    [Serializable]
    public class AccountAgreementContact
    {

        //#region Private Members
        //private string _cnt_account;
        //private string _cnt_add1;
        //private string _cnt_add2;
        //private string _cnt_add3;
        //private string _cnt_city;
        //private string _cnt_code;
        //private string _cnt_district;
        //private string _cnt_dl;
        //private DateTime _cnt_dob;
        //private string _cnt_fname;
        //private string _cnt_lname;
        //private string _cnt_location;
        //private string _cnt_mname;
        //private string _cnt_mobile;
        //private string _cnt_nic;
        //private string _cnt_passport;
        //private string _cnt_postalcode;
        //private string _cnt_province;
        //private string _cnt_sex;
        //private string _cnt_telhome;
        //private string _cnt_telresident;
        //private string _cnt_title;
        //private string _cnt_type;
        //#endregion

        //public string Cnt_account { get { return _cnt_account; } set { _cnt_account = value; } }
        //public string Cnt_add1 { get { return _cnt_add1; } set { _cnt_add1 = value; } }
        //public string Cnt_add2 { get { return _cnt_add2; } set { _cnt_add2 = value; } }
        //public string Cnt_add3 { get { return _cnt_add3; } set { _cnt_add3 = value; } }
        //public string Cnt_city { get { return _cnt_city; } set { _cnt_city = value; } }
        //public string Cnt_code { get { return _cnt_code; } set { _cnt_code = value; } }
        //public string Cnt_district { get { return _cnt_district; } set { _cnt_district = value; } }
        //public string Cnt_dl { get { return _cnt_dl; } set { _cnt_dl = value; } }
        //public DateTime Cnt_dob { get { return _cnt_dob; } set { _cnt_dob = value; } }
        //public string Cnt_fname { get { return _cnt_fname; } set { _cnt_fname = value; } }
        //public string Cnt_lname { get { return _cnt_lname; } set { _cnt_lname = value; } }
        //public string Cnt_location { get { return _cnt_location; } set { _cnt_location = value; } }
        //public string Cnt_mname { get { return _cnt_mname; } set { _cnt_mname = value; } }
        //public string Cnt_mobile { get { return _cnt_mobile; } set { _cnt_mobile = value; } }
        //public string Cnt_nic { get { return _cnt_nic; } set { _cnt_nic = value; } }
        //public string Cnt_passport { get { return _cnt_passport; } set { _cnt_passport = value; } }
        //public string Cnt_postalcode { get { return _cnt_postalcode; } set { _cnt_postalcode = value; } }
        //public string Cnt_province { get { return _cnt_province; } set { _cnt_province = value; } }
        //public string Cnt_sex { get { return _cnt_sex; } set { _cnt_sex = value; } }
        //public string Cnt_telhome { get { return _cnt_telhome; } set { _cnt_telhome = value; } }
        //public string Cnt_telresident { get { return _cnt_telresident; } set { _cnt_telresident = value; } }
        //public string Cnt_title { get { return _cnt_title; } set { _cnt_title = value; } }
        //public string Cnt_type { get { return _cnt_type; } set { _cnt_type = value; } }

        //public static AccountAgreementContact Converter(DataRow row)
        //{
        //    return new AccountAgreementContact
        //    {
        //        Cnt_account = row["CNT_ACCOUNT"] == DBNull.Value ? string.Empty : row["CNT_ACCOUNT"].ToString(),
        //        Cnt_add1 = row["CNT_ADD1"] == DBNull.Value ? string.Empty : row["CNT_ADD1"].ToString(),
        //        Cnt_add2 = row["CNT_ADD2"] == DBNull.Value ? string.Empty : row["CNT_ADD2"].ToString(),
        //        Cnt_add3 = row["CNT_ADD3"] == DBNull.Value ? string.Empty : row["CNT_ADD3"].ToString(),
        //        Cnt_city = row["CNT_CITY"] == DBNull.Value ? string.Empty : row["CNT_CITY"].ToString(),
        //        Cnt_code = row["CNT_CODE"] == DBNull.Value ? string.Empty : row["CNT_CODE"].ToString(),
        //        Cnt_district = row["CNT_DISTRICT"] == DBNull.Value ? string.Empty : row["CNT_DISTRICT"].ToString(),
        //        Cnt_dl = row["CNT_DL"] == DBNull.Value ? string.Empty : row["CNT_DL"].ToString(),
        //        Cnt_dob = row["CNT_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CNT_DOB"]),
        //        Cnt_fname = row["CNT_FNAME"] == DBNull.Value ? string.Empty : row["CNT_FNAME"].ToString(),
        //        Cnt_lname = row["CNT_LNAME"] == DBNull.Value ? string.Empty : row["CNT_LNAME"].ToString(),
        //        Cnt_location = row["CNT_LOCATION"] == DBNull.Value ? string.Empty : row["CNT_LOCATION"].ToString(),
        //        Cnt_mname = row["CNT_MNAME"] == DBNull.Value ? string.Empty : row["CNT_MNAME"].ToString(),
        //        Cnt_mobile = row["CNT_MOBILE"] == DBNull.Value ? string.Empty : row["CNT_MOBILE"].ToString(),
        //        Cnt_nic = row["CNT_NIC"] == DBNull.Value ? string.Empty : row["CNT_NIC"].ToString(),
        //        Cnt_passport = row["CNT_PASSPORT"] == DBNull.Value ? string.Empty : row["CNT_PASSPORT"].ToString(),
        //        Cnt_postalcode = row["CNT_POSTALCODE"] == DBNull.Value ? string.Empty : row["CNT_POSTALCODE"].ToString(),
        //        Cnt_province = row["CNT_PROVINCE"] == DBNull.Value ? string.Empty : row["CNT_PROVINCE"].ToString(),
        //        Cnt_sex = row["CNT_SEX"] == DBNull.Value ? string.Empty : row["CNT_SEX"].ToString(),
        //        Cnt_telhome = row["CNT_TELHOME"] == DBNull.Value ? string.Empty : row["CNT_TELHOME"].ToString(),
        //        Cnt_telresident = row["CNT_TELRESIDENT"] == DBNull.Value ? string.Empty : row["CNT_TELRESIDENT"].ToString(),
        //        Cnt_title = row["CNT_TITLE"] == DBNull.Value ? string.Empty : row["CNT_TITLE"].ToString(),
        //        Cnt_type = row["CNT_TYPE"] == DBNull.Value ? string.Empty : row["CNT_TYPE"].ToString()

        //    };
        //}

        #region Private Members
        private string _cnt_account;
        private string _cnt_add1;
        private string _cnt_add2;
        private string _cnt_add3;
        private string _cnt_city;
        private string _cnt_code;
        private string _cnt_district;
        private string _cnt_dl;
        private DateTime _cnt_dob;
        private string _cnt_fname;
        private string _cnt_guarantor1_addr1;
        private string _cnt_guarantor1_addr2;
        private string _cnt_guarantor1_code;
        private string _cnt_guarantor1_name;
        private string _cnt_guarantor1_nic;
        private string _cnt_guarantor2_addr1;
        private string _cnt_guarantor2_addr2;
        private string _cnt_guarantor2_code;
        private string _cnt_guarantor2_name;
        private string _cnt_guarantor2_nic;
        private string _cnt_guarantor3_addr1;
        private string _cnt_guarantor3_addr2;
        private string _cnt_guarantor3_code;
        private string _cnt_guarantor3_name;
        private string _cnt_guarantor3_nic;
        private string _cnt_lname;
        private string _cnt_location;
        private string _cnt_mname;
        private string _cnt_mobile;
        private string _cnt_nic;
        private string _cnt_passport;
        private string _cnt_postalcode;
        private string _cnt_province;
        private string _cnt_sex;
        private string _cnt_telhome;
        private string _cnt_telresident;
        private string _cnt_title;
        private string _cnt_type;

        private string _cnt_guarantor1_mobile;//Added by Prabhath on 09/12/2013
        private string _cnt_guarantor2_mobile;//Added by Prabhath on 09/12/2013
        private string _cnt_guarantor3_mobile;//Added by Prabhath on 09/12/2013

        private string _cnt_guarantor1_tel;//Added by kapila 18/12/2013
        private string _cnt_guarantor2_tel;//Added by kapila 18/12/2013
        private string _cnt_guarantor3_tel;//Added by kapila 18/12/2013

        private string _cnt_guarantor1_DL;//Added by kapila 18/12/2013
        private string _cnt_guarantor2_DL;//Added by kapila 18/12/2013
        private string _cnt_guarantor3_DL;//Added by kapila 18/12/2013

        private string _cnt_guarantor1_PP;//Added by kapila 18/12/2013
        private string _cnt_guarantor2_PP;//Added by kapila 18/12/2013
        private string _cnt_guarantor3_PP;//Added by kapila 18/12/2013
        #endregion

        public string Cnt_account { get { return _cnt_account; } set { _cnt_account = value; } }
        public string Cnt_add1 { get { return _cnt_add1; } set { _cnt_add1 = value; } }
        public string Cnt_add2 { get { return _cnt_add2; } set { _cnt_add2 = value; } }
        public string Cnt_add3 { get { return _cnt_add3; } set { _cnt_add3 = value; } }
        public string Cnt_city { get { return _cnt_city; } set { _cnt_city = value; } }
        public string Cnt_code { get { return _cnt_code; } set { _cnt_code = value; } }
        public string Cnt_district { get { return _cnt_district; } set { _cnt_district = value; } }
        public string Cnt_dl { get { return _cnt_dl; } set { _cnt_dl = value; } }
        public DateTime Cnt_dob { get { return _cnt_dob; } set { _cnt_dob = value; } }
        public string Cnt_fname { get { return _cnt_fname; } set { _cnt_fname = value; } }
        public string Cnt_guarantor1_addr1 { get { return _cnt_guarantor1_addr1; } set { _cnt_guarantor1_addr1 = value; } }
        public string Cnt_guarantor1_addr2 { get { return _cnt_guarantor1_addr2; } set { _cnt_guarantor1_addr2 = value; } }
        public string Cnt_guarantor1_code { get { return _cnt_guarantor1_code; } set { _cnt_guarantor1_code = value; } }
        public string Cnt_guarantor1_name { get { return _cnt_guarantor1_name; } set { _cnt_guarantor1_name = value; } }
        public string Cnt_guarantor1_nic { get { return _cnt_guarantor1_nic; } set { _cnt_guarantor1_nic = value; } }
        public string Cnt_guarantor2_addr1 { get { return _cnt_guarantor2_addr1; } set { _cnt_guarantor2_addr1 = value; } }
        public string Cnt_guarantor2_addr2 { get { return _cnt_guarantor2_addr2; } set { _cnt_guarantor2_addr2 = value; } }
        public string Cnt_guarantor2_code { get { return _cnt_guarantor2_code; } set { _cnt_guarantor2_code = value; } }
        public string Cnt_guarantor2_name { get { return _cnt_guarantor2_name; } set { _cnt_guarantor2_name = value; } }
        public string Cnt_guarantor2_nic { get { return _cnt_guarantor2_nic; } set { _cnt_guarantor2_nic = value; } }
        public string Cnt_guarantor3_addr1 { get { return _cnt_guarantor3_addr1; } set { _cnt_guarantor3_addr1 = value; } }
        public string Cnt_guarantor3_addr2 { get { return _cnt_guarantor3_addr2; } set { _cnt_guarantor3_addr2 = value; } }
        public string Cnt_guarantor3_code { get { return _cnt_guarantor3_code; } set { _cnt_guarantor3_code = value; } }
        public string Cnt_guarantor3_name { get { return _cnt_guarantor3_name; } set { _cnt_guarantor3_name = value; } }
        public string Cnt_guarantor3_nic { get { return _cnt_guarantor3_nic; } set { _cnt_guarantor3_nic = value; } }
        public string Cnt_lname { get { return _cnt_lname; } set { _cnt_lname = value; } }
        public string Cnt_location { get { return _cnt_location; } set { _cnt_location = value; } }
        public string Cnt_mname { get { return _cnt_mname; } set { _cnt_mname = value; } }
        public string Cnt_mobile { get { return _cnt_mobile; } set { _cnt_mobile = value; } }
        public string Cnt_nic { get { return _cnt_nic; } set { _cnt_nic = value; } }
        public string Cnt_passport { get { return _cnt_passport; } set { _cnt_passport = value; } }
        public string Cnt_postalcode { get { return _cnt_postalcode; } set { _cnt_postalcode = value; } }
        public string Cnt_province { get { return _cnt_province; } set { _cnt_province = value; } }
        public string Cnt_sex { get { return _cnt_sex; } set { _cnt_sex = value; } }
        public string Cnt_telhome { get { return _cnt_telhome; } set { _cnt_telhome = value; } }
        public string Cnt_telresident { get { return _cnt_telresident; } set { _cnt_telresident = value; } }
        public string Cnt_title { get { return _cnt_title; } set { _cnt_title = value; } }
        public string Cnt_type { get { return _cnt_type; } set { _cnt_type = value; } }
        public string Cnt_guarantor1_mobile { get { return _cnt_guarantor1_mobile; } set { _cnt_guarantor1_mobile = value; } }
        public string Cnt_guarantor2_mobile { get { return _cnt_guarantor2_mobile; } set { _cnt_guarantor2_mobile = value; } }
        public string Cnt_guarantor3_mobile { get { return _cnt_guarantor3_mobile; } set { _cnt_guarantor3_mobile = value; } }
        public string Cnt_guarantor3_tel { get { return _cnt_guarantor3_tel; } set { _cnt_guarantor3_tel = value; } }
        public string Cnt_guarantor2_tel { get { return _cnt_guarantor2_tel; } set { _cnt_guarantor2_tel = value; } }
        public string Cnt_guarantor1_tel { get { return _cnt_guarantor1_tel; } set { _cnt_guarantor1_tel = value; } }
        public string Cnt_guarantor1_DL { get { return _cnt_guarantor1_DL; } set { _cnt_guarantor1_DL = value; } }
        public string Cnt_guarantor2_DL { get { return _cnt_guarantor2_DL; } set { _cnt_guarantor2_DL = value; } }
        public string Cnt_guarantor3_DL { get { return _cnt_guarantor3_DL; } set { _cnt_guarantor3_DL = value; } }
        public string Cnt_guarantor1_PP { get { return _cnt_guarantor1_PP; } set { _cnt_guarantor1_PP = value; } }
        public string Cnt_guarantor2_PP { get { return _cnt_guarantor2_PP; } set { _cnt_guarantor2_PP = value; } }
        public string Cnt_guarantor3_PP { get { return _cnt_guarantor3_PP; } set { _cnt_guarantor3_PP = value; } }

        public static AccountAgreementContact Converter(DataRow row)
        {
            return new AccountAgreementContact
            {
                Cnt_account = row["CNT_ACCOUNT"] == DBNull.Value ? string.Empty : row["CNT_ACCOUNT"].ToString(),
                Cnt_add1 = row["CNT_ADD1"] == DBNull.Value ? string.Empty : row["CNT_ADD1"].ToString(),
                Cnt_add2 = row["CNT_ADD2"] == DBNull.Value ? string.Empty : row["CNT_ADD2"].ToString(),
                Cnt_add3 = row["CNT_ADD3"] == DBNull.Value ? string.Empty : row["CNT_ADD3"].ToString(),
                Cnt_city = row["CNT_CITY"] == DBNull.Value ? string.Empty : row["CNT_CITY"].ToString(),
                Cnt_code = row["CNT_CODE"] == DBNull.Value ? string.Empty : row["CNT_CODE"].ToString(),
                Cnt_district = row["CNT_DISTRICT"] == DBNull.Value ? string.Empty : row["CNT_DISTRICT"].ToString(),
                Cnt_dl = row["CNT_DL"] == DBNull.Value ? string.Empty : row["CNT_DL"].ToString(),
                Cnt_dob = row["CNT_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CNT_DOB"]),
                Cnt_fname = row["CNT_FNAME"] == DBNull.Value ? string.Empty : row["CNT_FNAME"].ToString(),
                Cnt_guarantor1_addr1 = row["CNT_GUARANTOR1_ADDR1"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR1_ADDR1"].ToString(),
                Cnt_guarantor1_addr2 = row["CNT_GUARANTOR1_ADDR2"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR1_ADDR2"].ToString(),
                Cnt_guarantor1_code = row["CNT_GUARANTOR1_CODE"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR1_CODE"].ToString(),
                Cnt_guarantor1_name = row["CNT_GUARANTOR1_NAME"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR1_NAME"].ToString(),
                Cnt_guarantor1_nic = row["CNT_GUARANTOR1_NIC"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR1_NIC"].ToString(),
                Cnt_guarantor2_addr1 = row["CNT_GUARANTOR2_ADDR1"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR2_ADDR1"].ToString(),
                Cnt_guarantor2_addr2 = row["CNT_GUARANTOR2_ADDR2"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR2_ADDR2"].ToString(),
                Cnt_guarantor2_code = row["CNT_GUARANTOR2_CODE"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR2_CODE"].ToString(),
                Cnt_guarantor2_name = row["CNT_GUARANTOR2_NAME"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR2_NAME"].ToString(),
                Cnt_guarantor2_nic = row["CNT_GUARANTOR2_NIC"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR2_NIC"].ToString(),
                Cnt_guarantor3_addr1 = row["CNT_GUARANTOR3_ADDR1"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR3_ADDR1"].ToString(),
                Cnt_guarantor3_addr2 = row["CNT_GUARANTOR3_ADDR2"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR3_ADDR2"].ToString(),
                Cnt_guarantor3_code = row["CNT_GUARANTOR3_CODE"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR3_CODE"].ToString(),
                Cnt_guarantor3_name = row["CNT_GUARANTOR3_NAME"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR3_NAME"].ToString(),
                Cnt_guarantor3_nic = row["CNT_GUARANTOR3_NIC"] == DBNull.Value ? string.Empty : row["CNT_GUARANTOR3_NIC"].ToString(),
                Cnt_lname = row["CNT_LNAME"] == DBNull.Value ? string.Empty : row["CNT_LNAME"].ToString(),
                Cnt_location = row["CNT_LOCATION"] == DBNull.Value ? string.Empty : row["CNT_LOCATION"].ToString(),
                Cnt_mname = row["CNT_MNAME"] == DBNull.Value ? string.Empty : row["CNT_MNAME"].ToString(),
                Cnt_mobile = row["CNT_MOBILE"] == DBNull.Value ? string.Empty : row["CNT_MOBILE"].ToString(),
                Cnt_nic = row["CNT_NIC"] == DBNull.Value ? string.Empty : row["CNT_NIC"].ToString(),
                Cnt_passport = row["CNT_PASSPORT"] == DBNull.Value ? string.Empty : row["CNT_PASSPORT"].ToString(),
                Cnt_postalcode = row["CNT_POSTALCODE"] == DBNull.Value ? string.Empty : row["CNT_POSTALCODE"].ToString(),
                Cnt_province = row["CNT_PROVINCE"] == DBNull.Value ? string.Empty : row["CNT_PROVINCE"].ToString(),
                Cnt_sex = row["CNT_SEX"] == DBNull.Value ? string.Empty : row["CNT_SEX"].ToString(),
                Cnt_telhome = row["CNT_TELHOME"] == DBNull.Value ? string.Empty : row["CNT_TELHOME"].ToString(),
                Cnt_telresident = row["CNT_TELRESIDENT"] == DBNull.Value ? string.Empty : row["CNT_TELRESIDENT"].ToString(),
                Cnt_title = row["CNT_TITLE"] == DBNull.Value ? string.Empty : row["CNT_TITLE"].ToString(),
                Cnt_type = row["CNT_TYPE"] == DBNull.Value ? string.Empty : row["CNT_TYPE"].ToString(),
                Cnt_guarantor1_mobile = row["Cnt_guarantor1_mobile"] == DBNull.Value ? string.Empty : row["Cnt_guarantor1_mobile"].ToString(),
                Cnt_guarantor2_mobile = row["Cnt_guarantor2_mobile"] == DBNull.Value ? string.Empty : row["Cnt_guarantor2_mobile"].ToString(),
                Cnt_guarantor3_mobile = row["Cnt_guarantor3_mobile"] == DBNull.Value ? string.Empty : row["Cnt_guarantor3_mobile"].ToString(),
                Cnt_guarantor1_tel = row["Cnt_guarantor1_tel"] == DBNull.Value ? string.Empty : row["Cnt_guarantor1_tel"].ToString(),
                Cnt_guarantor2_tel = row["Cnt_guarantor2_tel"] == DBNull.Value ? string.Empty : row["Cnt_guarantor2_tel"].ToString(),
                Cnt_guarantor3_tel = row["Cnt_guarantor3_tel"] == DBNull.Value ? string.Empty : row["Cnt_guarantor3_tel"].ToString(),
                Cnt_guarantor1_DL = row["Cnt_guarantor1_DL"] == DBNull.Value ? string.Empty : row["Cnt_guarantor1_DL"].ToString(),
                Cnt_guarantor2_DL = row["Cnt_guarantor2_DL"] == DBNull.Value ? string.Empty : row["Cnt_guarantor2_DL"].ToString(),
                Cnt_guarantor3_DL = row["Cnt_guarantor3_DL"] == DBNull.Value ? string.Empty : row["Cnt_guarantor3_DL"].ToString(),
                Cnt_guarantor1_PP = row["Cnt_guarantor1_PP"] == DBNull.Value ? string.Empty : row["Cnt_guarantor1_PP"].ToString(),
                Cnt_guarantor2_PP = row["Cnt_guarantor2_PP"] == DBNull.Value ? string.Empty : row["Cnt_guarantor2_PP"].ToString(),
                Cnt_guarantor3_PP = row["Cnt_guarantor3_PP"] == DBNull.Value ? string.Empty : row["Cnt_guarantor3_PP"].ToString(),

            };
        }

    }
}
