using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
     [Serializable]
    public class InvoicePaymentMethodRef
    {
        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// </summary>
        /// 
        #region Private Members
        private string _sapm_act;
        private string _sapm_cre_by;
        private DateTime _sapm_cre_when;
        private string _sapm_mod_by;
        private DateTime _sapm_mod_when;
        private string _sapm_pay_desc;
        private string _sapm_pay_method;
        private string _sapm_pay_tp;
        #endregion

        public string Sapm_act { get { return _sapm_act; } set { _sapm_act = value; } }
        public string Sapm_cre_by { get { return _sapm_cre_by; } set { _sapm_cre_by = value; } }
        public DateTime Sapm_cre_when { get { return _sapm_cre_when; } set { _sapm_cre_when = value; } }
        public string Sapm_mod_by { get { return _sapm_mod_by; } set { _sapm_mod_by = value; } }
        public DateTime Sapm_mod_when { get { return _sapm_mod_when; } set { _sapm_mod_when = value; } }
        public string Sapm_pay_desc { get { return _sapm_pay_desc; } set { _sapm_pay_desc = value; } }
        public string Sapm_pay_method { get { return _sapm_pay_method; } set { _sapm_pay_method = value; } }
        public string Sapm_pay_tp { get { return _sapm_pay_tp; } set { _sapm_pay_tp = value; } }

        public static InvoicePaymentMethodRef ConvertTotal(DataRow row)
        {
            return new InvoicePaymentMethodRef
            {
                Sapm_act = row["SAPM_ACT"] == DBNull.Value ? string.Empty : row["SAPM_ACT"].ToString(),
                Sapm_cre_by = row["SAPM_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPM_CRE_BY"].ToString(),
                Sapm_cre_when = row["SAPM_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPM_CRE_WHEN"]),
                Sapm_mod_by = row["SAPM_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPM_MOD_BY"].ToString(),
                Sapm_mod_when = row["SAPM_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPM_MOD_WHEN"]),
                Sapm_pay_desc = row["SAPM_PAY_DESC"] == DBNull.Value ? string.Empty : row["SAPM_PAY_DESC"].ToString(),
                Sapm_pay_method = row["SAPM_PAY_METHOD"] == DBNull.Value ? string.Empty : row["SAPM_PAY_METHOD"].ToString(),
                Sapm_pay_tp = row["SAPM_PAY_TP"] == DBNull.Value ? string.Empty : row["SAPM_PAY_TP"].ToString()

            };
        }
    }
}

