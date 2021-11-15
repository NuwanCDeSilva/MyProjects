using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
  public  class PaymentTypeRef
    {
        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// </summary>

        #region Private Members
        private string _sapt_act;
        private string _sapt_cd;
        private string _sapt_cre_by;
        private DateTime _sapt_cre_when;
        private string _sapt_desc;
        private Boolean _sapt_is_settle_bank;
        private string _sapt_mod_by;
        private DateTime _sapt_mod_when;
        #endregion

        public string Sapt_act { get { return _sapt_act; } set { _sapt_act = value; } }
        public string Sapt_cd { get { return _sapt_cd; } set { _sapt_cd = value; } }
        public string Sapt_cre_by { get { return _sapt_cre_by; } set { _sapt_cre_by = value; } }
        public DateTime Sapt_cre_when { get { return _sapt_cre_when; } set { _sapt_cre_when = value; } }
        public string Sapt_desc { get { return _sapt_desc; } set { _sapt_desc = value; } }
        public Boolean Sapt_is_settle_bank { get { return _sapt_is_settle_bank; } set { _sapt_is_settle_bank = value; } }
        public string Sapt_mod_by { get { return _sapt_mod_by; } set { _sapt_mod_by = value; } }
        public DateTime Sapt_mod_when { get { return _sapt_mod_when; } set { _sapt_mod_when = value; } }

        public static PaymentTypeRef ConvertTotal(DataRow row)
        {
            return new PaymentTypeRef
            {
                Sapt_act = row["SAPT_ACT"] == DBNull.Value ? string.Empty : row["SAPT_ACT"].ToString(),
                Sapt_cd = row["SAPT_CD"] == DBNull.Value ? string.Empty : row["SAPT_CD"].ToString(),
                Sapt_cre_by = row["SAPT_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPT_CRE_BY"].ToString(),
                Sapt_cre_when = row["SAPT_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPT_CRE_WHEN"]),
                Sapt_desc = row["SAPT_DESC"] == DBNull.Value ? string.Empty : row["SAPT_DESC"].ToString(),
                Sapt_is_settle_bank = row["SAPT_IS_SETTLE_BANK"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPT_IS_SETTLE_BANK"]),
                Sapt_mod_by = row["SAPT_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPT_MOD_BY"].ToString(),
                Sapt_mod_when = row["SAPT_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPT_MOD_WHEN"])

            };
        }
    }
}
