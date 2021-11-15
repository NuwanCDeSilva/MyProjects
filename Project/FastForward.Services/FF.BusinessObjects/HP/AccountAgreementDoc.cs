using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //create by = shani on 25-07-2013
    //table = HPT_ACCOUNTS_AGREEMENT_DOCS
    [Serializable]
    public class AccountAgreementDoc
    {

        #region Private Members
        private string _agrd_accno;
        private string _agrd_doc_name;
        private Boolean _agrd_is_recieve;
        private Int32 _agrd_prd_cd;
        #endregion

        public string Agrd_accno { get { return _agrd_accno; } set { _agrd_accno = value; } }
        public string Agrd_doc_name { get { return _agrd_doc_name; } set { _agrd_doc_name = value; } }
        public Boolean Agrd_is_recieve { get { return _agrd_is_recieve; } set { _agrd_is_recieve = value; } }
        public Int32 Agrd_prd_cd { get { return _agrd_prd_cd; } set { _agrd_prd_cd = value; } }

        public static AccountAgreementDoc Converter(DataRow row)
        {
            return new AccountAgreementDoc
            {
                Agrd_accno = row["AGRD_ACCNO"] == DBNull.Value ? string.Empty : row["AGRD_ACCNO"].ToString(),
                Agrd_doc_name = row["AGRD_DOC_NAME"] == DBNull.Value ? string.Empty : row["AGRD_DOC_NAME"].ToString(),
                Agrd_is_recieve = row["AGRD_IS_RECIEVE"] == DBNull.Value ? false : Convert.ToBoolean(row["AGRD_IS_RECIEVE"]),
                Agrd_prd_cd = row["AGRD_PRD_CD"] == DBNull.Value ? 0 : Convert.ToInt32(row["AGRD_PRD_CD"])

            };
        }

    }
}
