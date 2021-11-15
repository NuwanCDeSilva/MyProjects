using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
   public class HpTransaction
    {
        /// <summary>
        /// Written By Shani on 20/06/2012
        /// Table: HPT_TXN (in EMS)
        /// </summary>
        #region Private Members
        private string _hpt_acc_no;
        private decimal _hpt_ars;
        private decimal _hpt_bal;
        private string _hpt_com;
        private decimal _hpt_crdt;
        private string _hpt_cre_by;
        private DateTime _hpt_cre_dt;
        private decimal _hpt_dbt;
        private string _hpt_desc;
        private string _hpt_mnl_ref;
        private string _hpt_pc;
        private string _hpt_ref_no;
        private decimal _hpt_seq;
        private DateTime _hpt_txn_dt;
        private string _hpt_txn_ref;
        private string _hpt_txn_tp;

        #endregion


        public string Hpt_acc_no { get { return _hpt_acc_no; } set { _hpt_acc_no = value; } }
        public decimal Hpt_ars { get { return _hpt_ars; } set { _hpt_ars = value; } }
        public decimal Hpt_bal { get { return _hpt_bal; } set { _hpt_bal = value; } }
        public string Hpt_com { get { return _hpt_com; } set { _hpt_com = value; } }
        public decimal Hpt_crdt { get { return _hpt_crdt; } set { _hpt_crdt = value; } }
        public string Hpt_cre_by { get { return _hpt_cre_by; } set { _hpt_cre_by = value; } }
        public DateTime Hpt_cre_dt { get { return _hpt_cre_dt; } set { _hpt_cre_dt = value; } }
        public decimal Hpt_dbt { get { return _hpt_dbt; } set { _hpt_dbt = value; } }
        public string Hpt_desc { get { return _hpt_desc; } set { _hpt_desc = value; } }
        public string Hpt_mnl_ref { get { return _hpt_mnl_ref; } set { _hpt_mnl_ref = value; } }
        public string Hpt_pc { get { return _hpt_pc; } set { _hpt_pc = value; } }
        public string Hpt_ref_no { get { return _hpt_ref_no; } set { _hpt_ref_no = value; } }
        public decimal Hpt_seq { get { return _hpt_seq; } set { _hpt_seq = value; } }
        public DateTime Hpt_txn_dt { get { return _hpt_txn_dt; } set { _hpt_txn_dt = value; } }
        public string Hpt_txn_ref { get { return _hpt_txn_ref; } set { _hpt_txn_ref = value; } }
        public string Hpt_txn_tp { get { return _hpt_txn_tp; } set { _hpt_txn_tp = value; } }


        public static HpTransaction Converter(DataRow row)
        {
            return new HpTransaction
            {
                Hpt_acc_no = row["HPT_ACC_NO"] == DBNull.Value ? string.Empty : row["HPT_ACC_NO"].ToString(),
                Hpt_ars = row["HPT_ARS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPT_ARS"]),
                Hpt_bal = row["HPT_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPT_BAL"]),
                Hpt_com = row["HPT_COM"] == DBNull.Value ? string.Empty : row["HPT_COM"].ToString(),
                Hpt_crdt = row["HPT_CRDT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPT_CRDT"]),
                Hpt_cre_by = row["HPT_CRE_BY"] == DBNull.Value ? string.Empty : row["HPT_CRE_BY"].ToString(),
                Hpt_cre_dt = row["HPT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPT_CRE_DT"]),
                Hpt_dbt = row["HPT_DBT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPT_DBT"]),
                Hpt_desc = row["HPT_DESC"] == DBNull.Value ? string.Empty : row["HPT_DESC"].ToString(),
                Hpt_mnl_ref = row["HPT_MNL_REF"] == DBNull.Value ? string.Empty : row["HPT_MNL_REF"].ToString(),
                Hpt_pc = row["HPT_PC"] == DBNull.Value ? string.Empty : row["HPT_PC"].ToString(),
                Hpt_ref_no = row["HPT_REF_NO"] == DBNull.Value ? string.Empty : row["HPT_REF_NO"].ToString(),
                Hpt_seq = row["HPT_SEQ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPT_SEQ"]),
                Hpt_txn_dt = row["HPT_TXN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPT_TXN_DT"]),
                Hpt_txn_ref = row["HPT_TXN_REF"] == DBNull.Value ? string.Empty : row["HPT_TXN_REF"].ToString(),
                Hpt_txn_tp = row["HPT_TXN_TP"] == DBNull.Value ? string.Empty : row["HPT_TXN_TP"].ToString()



            };
        }

    }
}
