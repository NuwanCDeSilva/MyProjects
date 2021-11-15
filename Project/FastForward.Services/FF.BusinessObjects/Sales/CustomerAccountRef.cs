using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
   public class CustomerAccountRef
    {
       /// <summary>
       /// Written By Prabhathh on 26/04/2012
       /// </summary>

        #region Private Members
        private decimal _saca_acc_bal;
        private string _saca_com_cd;
        private decimal _saca_crdt_lmt;
        private string _saca_cre_by;
        private DateTime _saca_cre_when;
        private string _saca_cust_cd;
        private string _saca_mod_by;
        private DateTime _saca_mod_when;
        private decimal _saca_ord_bal;
        private string _saca_session_id;
        #endregion

        public decimal Saca_acc_bal { get { return _saca_acc_bal; } set { _saca_acc_bal = value; } }
        public string Saca_com_cd { get { return _saca_com_cd; } set { _saca_com_cd = value; } }
        public decimal Saca_crdt_lmt { get { return _saca_crdt_lmt; } set { _saca_crdt_lmt = value; } }
        public string Saca_cre_by { get { return _saca_cre_by; } set { _saca_cre_by = value; } }
        public DateTime Saca_cre_when { get { return _saca_cre_when; } set { _saca_cre_when = value; } }
        public string Saca_cust_cd { get { return _saca_cust_cd; } set { _saca_cust_cd = value; } }
        public string Saca_mod_by { get { return _saca_mod_by; } set { _saca_mod_by = value; } }
        public DateTime Saca_mod_when { get { return _saca_mod_when; } set { _saca_mod_when = value; } }
        public decimal Saca_ord_bal { get { return _saca_ord_bal; } set { _saca_ord_bal = value; } }
        public string Saca_session_id { get { return _saca_session_id; } set { _saca_session_id = value; } }

        public static CustomerAccountRef ConvertTotal(DataRow row)
        {
            return new CustomerAccountRef
            {
                Saca_acc_bal = row["SACA_ACC_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SACA_ACC_BAL"]),
                Saca_com_cd = row["SACA_COM_CD"] == DBNull.Value ? string.Empty : row["SACA_COM_CD"].ToString(),
                Saca_crdt_lmt = row["SACA_CRDT_LMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SACA_CRDT_LMT"]),
                Saca_cre_by = row["SACA_CRE_BY"] == DBNull.Value ? string.Empty : row["SACA_CRE_BY"].ToString(),
                Saca_cre_when = row["SACA_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SACA_CRE_WHEN"]),
                Saca_cust_cd = row["SACA_CUST_CD"] == DBNull.Value ? string.Empty : row["SACA_CUST_CD"].ToString(),
                Saca_mod_by = row["SACA_MOD_BY"] == DBNull.Value ? string.Empty : row["SACA_MOD_BY"].ToString(),
                Saca_mod_when = row["SACA_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SACA_MOD_WHEN"]),
                Saca_ord_bal = row["SACA_ORD_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SACA_ORD_BAL"]),
                Saca_session_id = row["SACA_SESSION_ID"] == DBNull.Value ? string.Empty : row["SACA_SESSION_ID"].ToString()

            };
        }
    }
}

