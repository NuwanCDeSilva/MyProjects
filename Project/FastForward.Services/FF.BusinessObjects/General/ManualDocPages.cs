using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ManualDocPages
    {
        #region Private Members
        private string _mdp_itm_cd;
        private Int32 _mdp_line;
        private string _mdp_loc;
        private string _mdp_mod_by;
        private DateTime _mdp_mod_dt;
        private Int32 _mdp_page_no;
        private string _mdp_prefix;
        private string _mdp_ref;
        private string _mdp_stus;
        private DateTime _mdp_txn_dt;
        private string _mdp_txn_no;
        private string _mdp_txn_sb_tp;
        private string _mdp_txn_tp;

        #endregion

        #region Public Property Definition
        public string Mdp_itm_cd
        {
            get { return _mdp_itm_cd; }
            set { _mdp_itm_cd = value; }
        }
        public Int32 Mdp_line
        {
            get { return _mdp_line; }
            set { _mdp_line = value; }
        }
        public string Mdp_loc
        {
            get { return _mdp_loc; }
            set { _mdp_loc = value; }
        }
        public string Mdp_mod_by
        {
            get { return _mdp_mod_by; }
            set { _mdp_mod_by = value; }
        }
        public DateTime Mdp_mod_dt
        {
            get { return _mdp_mod_dt; }
            set { _mdp_mod_dt = value; }
        }
        public Int32 Mdp_page_no
        {
            get { return _mdp_page_no; }
            set { _mdp_page_no = value; }
        }
        public string Mdp_prefix
        {
            get { return _mdp_prefix; }
            set { _mdp_prefix = value; }
        }
        public string Mdp_ref
        {
            get { return _mdp_ref; }
            set { _mdp_ref = value; }
        }
        public string Mdp_stus
        {
            get { return _mdp_stus; }
            set { _mdp_stus = value; }
        }
        public DateTime Mdp_txn_dt
        {
            get { return _mdp_txn_dt; }
            set { _mdp_txn_dt = value; }
        }
        public string Mdp_txn_no
        {
            get { return _mdp_txn_no; }
            set { _mdp_txn_no = value; }
        }
        public string Mdp_txn_sb_tp
        {
            get { return _mdp_txn_sb_tp; }
            set { _mdp_txn_sb_tp = value; }
        }
        public string Mdp_txn_tp
        {
            get { return _mdp_txn_tp; }
            set { _mdp_txn_tp = value; }
        }

        #endregion

        public static ManualDocPages Converter(DataRow row)
        {
            return new ManualDocPages
            {
                Mdp_itm_cd = row["MDP_ITM_CD"] == DBNull.Value ? string.Empty : row["MDP_ITM_CD"].ToString(),
                Mdp_line = row["MDP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDP_LINE"]),
                Mdp_loc = row["MDP_LOC"] == DBNull.Value ? string.Empty : row["MDP_LOC"].ToString(),
                Mdp_mod_by = row["MDP_MOD_BY"] == DBNull.Value ? string.Empty : row["MDP_MOD_BY"].ToString(),
                Mdp_mod_dt = row["MDP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MDP_MOD_DT"]),
                Mdp_page_no = row["MDP_PAGE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDP_PAGE_NO"]),
                Mdp_prefix = row["MDP_PREFIX"] == DBNull.Value ? string.Empty : row["MDP_PREFIX"].ToString(),
                Mdp_ref = row["MDP_REF"] == DBNull.Value ? string.Empty : row["MDP_REF"].ToString(),
                Mdp_stus = row["MDP_STUS"] == DBNull.Value ? string.Empty : row["MDP_STUS"].ToString(),
                Mdp_txn_dt = row["MDP_TXN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MDP_TXN_DT"]),
                Mdp_txn_no = row["MDP_TXN_NO"] == DBNull.Value ? string.Empty : row["MDP_TXN_NO"].ToString(),
                Mdp_txn_sb_tp = row["MDP_TXN_SB_TP"] == DBNull.Value ? string.Empty : row["MDP_TXN_SB_TP"].ToString(),
                Mdp_txn_tp = row["MDP_TXN_TP"] == DBNull.Value ? string.Empty : row["MDP_TXN_TP"].ToString()

            };
        }
    }
}
