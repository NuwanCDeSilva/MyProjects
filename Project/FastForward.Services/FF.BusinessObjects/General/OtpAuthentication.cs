using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class OtpAuthentication
    {
        //Written By Udesh on 31/Oct/2018
        //Table - SAR_DUALAUTH_KEY

        #region Private Members
        private string _sdk_tp;
        private string _sdk_sb_tp;
        private string _sdk_cus_cd;
        private string _sdk_mob;
        private Int32 _sdk_key;
        private Int32 _sdk_is_validate;
        private Int32 _sdk_act;
        private string _sdk_validate_by;
        private DateTime _sdk_validate_dt;
        private string _sdk_session;
        private string _sdk_cre_by;
        private DateTime _sdk_cre_dt;
        private string _sdk_hash_key;
        #endregion

        public string SDK_TP { get { return _sdk_tp; } set { _sdk_tp = value; } }
        public string SDK_SB_TP { get { return _sdk_sb_tp; } set { _sdk_sb_tp = value; } }
        public string SDK_CUS_CD { get { return _sdk_cus_cd; } set { _sdk_cus_cd = value; } }
        public string SDK_MOB { get { return _sdk_mob; } set { _sdk_mob = value; } }
        public Int32 SDK_KEY { get { return _sdk_key; } set { _sdk_key = value; } }
        public Int32 SDK_IS_VALIDATE { get { return _sdk_is_validate; } set { _sdk_is_validate = value; } }
        public Int32 SDK_ACT { get { return _sdk_act; } set { _sdk_act = value; } }
        public string SDK_VALIDATE_BY { get { return _sdk_validate_by; } set { _sdk_validate_by = value; } }
        public DateTime SDK_VALIDATE_DT { get { return _sdk_validate_dt; } set { _sdk_validate_dt = value; } }
        public string SDK_SESSION { get { return _sdk_session; } set { _sdk_session = value; } }
        public string SDK_CRE_BY { get { return _sdk_cre_by; } set { _sdk_cre_by = value; } }
        public DateTime SDK_CRE_DT { get { return _sdk_cre_dt; } set { _sdk_cre_dt = value; } }
        public string SDK_HASH_KEY { get { return _sdk_hash_key; } set { _sdk_hash_key = value; } }

        public static OtpAuthentication Converter(DataRow row)
        {
            return new OtpAuthentication
            {
                SDK_TP = row["SDK_TP"] == DBNull.Value ? string.Empty : row["SDK_TP"].ToString(),
                SDK_SB_TP = row["SDK_SB_TP"] == DBNull.Value ? string.Empty : row["SDK_SB_TP"].ToString(),
                SDK_CUS_CD = row["SDK_CUS_CD"] == DBNull.Value ? string.Empty : row["SDK_CUS_CD"].ToString(),
                SDK_MOB = row["SDK_MOB"] == DBNull.Value ? string.Empty : row["SDK_MOB"].ToString(),
                SDK_KEY = row["SDK_KEY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SDK_KEY"]),
                SDK_IS_VALIDATE = row["SDK_IS_VALIDATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SDK_IS_VALIDATE"]),
                SDK_ACT = row["SDK_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SDK_ACT"]),
                SDK_VALIDATE_BY = row["SDK_VALIDATE_BY"] == DBNull.Value ? string.Empty : row["SDK_VALIDATE_BY"].ToString(),
                SDK_VALIDATE_DT = row["SDK_VALIDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SDK_VALIDATE_DT"]),
                SDK_SESSION = row["SDK_SESSION"] == DBNull.Value ? string.Empty : row["SDK_SESSION"].ToString(),
                SDK_CRE_BY = row["SDK_CRE_BY"] == DBNull.Value ? string.Empty : row["SDK_CRE_BY"].ToString(),
                SDK_CRE_DT = row["SDK_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SDK_CRE_DT"]),
                SDK_HASH_KEY = row["SDK_HASH_KEY"] == DBNull.Value ? string.Empty : row["SDK_HASH_KEY"].ToString()

            };
        }
    }
}
