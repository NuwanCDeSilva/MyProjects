using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //create by = shani on 25-07-2013
    //table = HPT_Acc_Agreement_StartDate
    [Serializable]
    public class AccountAgreementStartDate
    {


        #region Private Members
        private DateTime _agrs_createddate;
        private string _agrs_createuser;
        private DateTime _agrs_date;
        private string _agrs_location;
        private DateTime _agrs_modifieddate;
        private string _agrs_sessionid;
        private DateTime _agrs_timestamp;
        private string _args_modifieduser;
        private string _agrs_com;
        #endregion

        public DateTime Agrs_createddate { get { return _agrs_createddate; } set { _agrs_createddate = value; } }
        public string Agrs_createuser { get { return _agrs_createuser; } set { _agrs_createuser = value; } }
        public DateTime Agrs_date { get { return _agrs_date; } set { _agrs_date = value; } }
        public string Agrs_location { get { return _agrs_location; } set { _agrs_location = value; } }
        public DateTime Agrs_modifieddate { get { return _agrs_modifieddate; } set { _agrs_modifieddate = value; } }
        public string Agrs_sessionid { get { return _agrs_sessionid; } set { _agrs_sessionid = value; } }
        public DateTime Agrs_timestamp { get { return _agrs_timestamp; } set { _agrs_timestamp = value; } }
        public string Args_modifieduser { get { return _args_modifieduser; } set { _args_modifieduser = value; } }
        public string Args_com { get { return _agrs_com; } set { _agrs_com = value; } }

        public static AccountAgreementStartDate Converter(DataRow row)
        {
            return new AccountAgreementStartDate
            {
                Agrs_createddate = row["AGRS_CREATEDDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRS_CREATEDDATE"]),
                Agrs_createuser = row["AGRS_CREATEUSER"] == DBNull.Value ? string.Empty : row["AGRS_CREATEUSER"].ToString(),
                Agrs_date = row["AGRS_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRS_DATE"]),
                Agrs_location = row["AGRS_LOCATION"] == DBNull.Value ? string.Empty : row["AGRS_LOCATION"].ToString(),
                Agrs_modifieddate = row["AGRS_MODIFIEDDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRS_MODIFIEDDATE"]),
                Agrs_sessionid = row["AGRS_SESSIONID"] == DBNull.Value ? string.Empty : row["AGRS_SESSIONID"].ToString(),
                Agrs_timestamp = row["AGRS_TIMESTAMP"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRS_TIMESTAMP"]),
                Args_com = row["AGRS_COM"] == DBNull.Value ? string.Empty : row["AGRS_COM"].ToString(),
                Args_modifieduser = row["ARGS_MODIFIEDUSER"] == DBNull.Value ? string.Empty : row["ARGS_MODIFIEDUSER"].ToString()

            };
        }

    }
}
