using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
  public class InvoicePaymentTypeRef
    {
        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// </summary>
        /// 
        #region Private Members
        private string _saip_act;
        private string _saip_cre_by;
        private DateTime _saip_cre_when;
        private string _saip_inv_tp;
        private Int32 _saip_is_default;
        private Boolean _saip_is_sev;
        private string _saip_mod_by;
        private DateTime _saip_mod_when;
        private string _saip_pay_tp_cd;
        private string _saip_pay_tp_desc;
        private string _saip_session_id;
        #endregion

        public string Saip_act { get { return _saip_act; } set { _saip_act = value; } }
        public string Saip_cre_by { get { return _saip_cre_by; } set { _saip_cre_by = value; } }
        public DateTime Saip_cre_when { get { return _saip_cre_when; } set { _saip_cre_when = value; } }
        public string Saip_inv_tp { get { return _saip_inv_tp; } set { _saip_inv_tp = value; } }
        public Int32 Saip_is_default { get { return _saip_is_default; } set { _saip_is_default = value; } }
        public Boolean Saip_is_sev { get { return _saip_is_sev; } set { _saip_is_sev = value; } }
        public string Saip_mod_by { get { return _saip_mod_by; } set { _saip_mod_by = value; } }
        public DateTime Saip_mod_when { get { return _saip_mod_when; } set { _saip_mod_when = value; } }
        public string Saip_pay_tp_cd { get { return _saip_pay_tp_cd; } set { _saip_pay_tp_cd = value; } }
        public string Saip_pay_tp_desc { get { return _saip_pay_tp_desc; } set { _saip_pay_tp_desc = value; } }
        public string Saip_session_id { get { return _saip_session_id; } set { _saip_session_id = value; } }

        public static InvoicePaymentTypeRef ConvertTotal(DataRow row)
        {
            return new InvoicePaymentTypeRef
            {
                Saip_act = row["SAIP_ACT"] == DBNull.Value ? string.Empty : row["SAIP_ACT"].ToString(),
                Saip_cre_by = row["SAIP_CRE_BY"] == DBNull.Value ? string.Empty : row["SAIP_CRE_BY"].ToString(),
                Saip_cre_when = row["SAIP_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAIP_CRE_WHEN"]),
                Saip_inv_tp = row["SAIP_INV_TP"] == DBNull.Value ? string.Empty : row["SAIP_INV_TP"].ToString(),
                Saip_is_default = row["SAIP_IS_DEFAULT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAIP_IS_DEFAULT"]),
                Saip_is_sev = row["SAIP_IS_SEV"] == DBNull.Value ? false : Convert.ToBoolean(row["SAIP_IS_SEV"]),
                Saip_mod_by = row["SAIP_MOD_BY"] == DBNull.Value ? string.Empty : row["SAIP_MOD_BY"].ToString(),
                Saip_mod_when = row["SAIP_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAIP_MOD_WHEN"]),
                Saip_pay_tp_cd = row["SAIP_PAY_TP_CD"] == DBNull.Value ? string.Empty : row["SAIP_PAY_TP_CD"].ToString(),
                Saip_pay_tp_desc = row["SAIP_PAY_TP_DESC"] == DBNull.Value ? string.Empty : row["SAIP_PAY_TP_DESC"].ToString(),
                Saip_session_id = row["SAIP_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAIP_SESSION_ID"].ToString()

            };
        }
    }
}

