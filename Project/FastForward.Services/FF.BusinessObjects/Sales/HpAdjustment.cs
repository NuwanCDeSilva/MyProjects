using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class HpAdjustment
    {
        /// <summary>
        /// Written By Prabhath on 05/07/2012
        /// added missing two fields by darshana 03/08/2012 - no idea who is add this new columns to table
        /// Table: HPT_ADJ (in EMS)
        /// </summary>

        #region Private Members
        private string _had_acc_no;
        private string _had_adj_tp;
        private decimal _had_crdt_val;
        private string _had_cre_by;
        private DateTime _had_cre_dt;
        private decimal _had_dbt_val;
        private DateTime _had_dt;
        private string _had_mnl_ref;
        private string _had_pc;
        private string _had_ref;
        private string _had_rmk;
        private string _had_tp;
        private Int32 _had_seq;
        private string _had_com;
        #endregion

        public string Had_acc_no { get { return _had_acc_no; } set { _had_acc_no = value; } }
        public string Had_adj_tp { get { return _had_adj_tp; } set { _had_adj_tp = value; } }
        public decimal Had_crdt_val { get { return _had_crdt_val; } set { _had_crdt_val = value; } }
        public string Had_cre_by { get { return _had_cre_by; } set { _had_cre_by = value; } }
        public DateTime Had_cre_dt { get { return _had_cre_dt; } set { _had_cre_dt = value; } }
        public decimal Had_dbt_val { get { return _had_dbt_val; } set { _had_dbt_val = value; } }
        public DateTime Had_dt { get { return _had_dt; } set { _had_dt = value; } }
        public string Had_mnl_ref { get { return _had_mnl_ref; } set { _had_mnl_ref = value; } }
        public string Had_pc { get { return _had_pc; } set { _had_pc = value; } }
        public string Had_ref { get { return _had_ref; } set { _had_ref = value; } }
        public string Had_rmk { get { return _had_rmk; } set { _had_rmk = value; } }
        public string Had_tp { get { return _had_tp; } set { _had_tp = value; } }
        public Int32 Had_seq { get { return _had_seq; } set { _had_seq = value; } }
        public string Had_com { get { return _had_com; } set { _had_com = value; } }

        public static HpAdjustment Converter(DataRow row)
        {
            return new HpAdjustment
            {
                Had_acc_no = row["HAD_ACC_NO"] == DBNull.Value ? string.Empty : row["HAD_ACC_NO"].ToString(),
                Had_adj_tp = row["HAD_ADJ_TP"] == DBNull.Value ? string.Empty : row["HAD_ADJ_TP"].ToString(),
                Had_crdt_val = row["HAD_CRDT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAD_CRDT_VAL"]),
                Had_cre_by = row["HAD_CRE_BY"] == DBNull.Value ? string.Empty : row["HAD_CRE_BY"].ToString(),
                Had_cre_dt = row["HAD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAD_CRE_DT"]),
                Had_dbt_val = row["HAD_DBT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAD_DBT_VAL"]),
                Had_dt = row["HAD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAD_DT"]),
                Had_mnl_ref = row["HAD_MNL_REF"] == DBNull.Value ? string.Empty : row["HAD_MNL_REF"].ToString(),
                Had_pc = row["HAD_PC"] == DBNull.Value ? string.Empty : row["HAD_PC"].ToString(),
                Had_ref = row["HAD_REF"] == DBNull.Value ? string.Empty : row["HAD_REF"].ToString(),
                Had_rmk = row["HAD_RMK"] == DBNull.Value ? string.Empty : row["HAD_RMK"].ToString(),
                Had_tp = row["HAD_TP"] == DBNull.Value ? string.Empty : row["HAD_TP"].ToString(),
                Had_seq = row["HAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAD_SEQ"]),
                Had_com = row["HAD_COM"] == DBNull.Value ? string.Empty : row["HAD_COM"].ToString()

            };
        }
    }
}

