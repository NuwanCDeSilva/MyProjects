using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //create by = shani on 27-07-2013
    //table = hpt_accounts_agreement_scheme

    [Serializable]
    public class AccountAgreementScheme
    {
        #region Private Members
        private string _sch_account;
        private string _sch_code;
        private string _sch_desc;
        private Int32 _sch_term;
        #endregion

        public string Sch_account { get { return _sch_account; } set { _sch_account = value; } }
        public string Sch_code { get { return _sch_code; } set { _sch_code = value; } }
        public string Sch_desc { get { return _sch_desc; } set { _sch_desc = value; } }
        public Int32 Sch_term { get { return _sch_term; } set { _sch_term = value; } }

        public static AccountAgreementScheme Converter(DataRow row)
        {
            return new AccountAgreementScheme
            {
                Sch_account = row["SCH_ACCOUNT"] == DBNull.Value ? string.Empty : row["SCH_ACCOUNT"].ToString(),
                Sch_code = row["SCH_CODE"] == DBNull.Value ? string.Empty : row["SCH_CODE"].ToString(),
                Sch_desc = row["SCH_DESC"] == DBNull.Value ? string.Empty : row["SCH_DESC"].ToString(),
                Sch_term = row["SCH_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCH_TERM"])

            };
        }

    }
}
