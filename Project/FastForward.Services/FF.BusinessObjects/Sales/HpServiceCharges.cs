using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpServiceCharges
    {
        #region Private Members
        private Boolean _hps_cal_on;
        private decimal _hps_chg;
        private Boolean _hps_chk_on;
        private string _hps_cre_by;
        private DateTime _hps_cre_dt;
        private decimal _hps_from_val;
        private string _hps_pty_cd;
        private string _hps_pty_tp;
        private decimal _hps_rt;
        private string _hps_sch_cd;
        private Int32 _hps_seq;
        private decimal _hps_to_val;
        private DateTime _hps_valid_from;
        private DateTime _hps_valid_to;
        private decimal  _hps_mgr_comm_amt;
        private decimal _hps_mgr_comm_rt;
        #endregion

        
        public Boolean Hps_cal_on
        {
            get { return _hps_cal_on; }
            set { _hps_cal_on = value; }
        }

        public decimal Hps_chg
        {
            get { return _hps_chg; }
            set { _hps_chg = value; }
        }
        public Boolean Hps_chk_on
        {
            get { return _hps_chk_on; }
            set { _hps_chk_on = value; }
        }
        public string Hps_cre_by
        {
            get { return _hps_cre_by; }
            set { _hps_cre_by = value; }
        }
        public DateTime Hps_cre_dt
        {
            get { return _hps_cre_dt; }
            set { _hps_cre_dt = value; }
        }
        public decimal Hps_from_val
        {
            get { return _hps_from_val; }
            set { _hps_from_val = value; }
        }
        public string Hps_pty_cd
        {
            get { return _hps_pty_cd; }
            set { _hps_pty_cd = value; }
        }
        public string Hps_pty_tp
        {
            get { return _hps_pty_tp; }
            set { _hps_pty_tp = value; }
        }
        public decimal Hps_rt
        {
            get { return _hps_rt; }
            set { _hps_rt = value; }
        }
        public string Hps_sch_cd
        {
            get { return _hps_sch_cd; }
            set { _hps_sch_cd = value; }
        }
        public Int32 Hps_seq
        {
            get { return _hps_seq; }
            set { _hps_seq = value; }
        }
        public decimal Hps_to_val
        {
            get { return _hps_to_val; }
            set { _hps_to_val = value; }
        }

        public DateTime Hps_valid_from
        {
            get { return _hps_valid_from; }
            set { _hps_valid_from = value; }
        }

        public DateTime Hps_valid_to
        {
            get { return _hps_valid_to; }
            set { _hps_valid_to = value; }
        }

        public decimal Hps_mgr_comm_amt
        {
            get { return _hps_mgr_comm_amt; }
            set { _hps_mgr_comm_amt = value; }
        }

        public decimal Hps_mgr_comm_rt
        {
            get { return _hps_mgr_comm_rt; }
            set { _hps_mgr_comm_rt = value; }
        }

        public static HpServiceCharges Converter(DataRow row)
        {
            return new HpServiceCharges
            {
                Hps_cal_on = row["HPS_CAL_ON"] == DBNull.Value ? false : Convert.ToBoolean(row["HPS_CAL_ON"]),
                Hps_chg = row["HPS_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPS_CHG"]),
                Hps_chk_on = row["HPS_CHK_ON"] == DBNull.Value ? false : Convert.ToBoolean(row["HPS_CHK_ON"]),
                Hps_cre_by = row["HPS_CRE_BY"] == DBNull.Value ? string.Empty : row["HPS_CRE_BY"].ToString(),
                Hps_cre_dt = row["HPS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPS_CRE_DT"]),
                Hps_from_val = row["HPS_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPS_FROM_VAL"]),
                Hps_pty_cd = row["HPS_PTY_CD"] == DBNull.Value ? string.Empty : row["HPS_PTY_CD"].ToString(),
                Hps_pty_tp = row["HPS_PTY_TP"] == DBNull.Value ? string.Empty : row["HPS_PTY_TP"].ToString(),
                Hps_rt = row["HPS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPS_RT"]),
                Hps_sch_cd = row["HPS_SCH_CD"] == DBNull.Value ? string.Empty : row["HPS_SCH_CD"].ToString(),
                Hps_seq = row["HPS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPS_SEQ"]),
                Hps_to_val = row["HPS_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPS_TO_VAL"]),
                Hps_valid_from = row["HPS_VALID_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPS_VALID_FROM"]),
                Hps_valid_to = row["HPS_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPS_VALID_TO"]),
                Hps_mgr_comm_amt = row["HPS_MGR_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPS_MGR_COMM_AMT"]),
                Hps_mgr_comm_rt = row["HPS_MGR_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPS_MGR_COMM_RT"])
            };

        }
    }
}