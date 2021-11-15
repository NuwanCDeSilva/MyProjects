using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpAdditionalServiceCharges
    {
        #region Private Members
        private Boolean _has_cal_on;
        private decimal _has_chg;
        private Boolean _has_chk_on;
        private string _has_cre_by;
        private DateTime _has_cre_dt;
        private DateTime _has_from_dt;
        private decimal _has_from_val;
        private string _has_pty_cd;
        private string _has_pty_tp;
        private decimal _has_rt;
        private string _has_sch_cd;
        private Int32 _has_seq;
        private DateTime _has_to_dt;
        private decimal _has_to_val;
        #endregion

        public Boolean Has_cal_on
        {
            get { return _has_cal_on; }
            set { _has_cal_on = value; }
        }
        public decimal Has_chg
        {
            get { return _has_chg; }
            set { _has_chg = value; }
        }
        public Boolean Has_chk_on
        {
            get { return _has_chk_on; }
            set { _has_chk_on = value; }
        }
        public string Has_cre_by
        {
            get { return _has_cre_by; }
            set { _has_cre_by = value; }
        }
        public DateTime Has_cre_dt
        {
            get { return _has_cre_dt; }
            set { _has_cre_dt = value; }
        }
        public DateTime Has_from_dt
        {
            get { return _has_from_dt; }
            set { _has_from_dt = value; }
        }
        public decimal Has_from_val
        {
            get { return _has_from_val; }
            set { _has_from_val = value; }
        }
        public string Has_pty_cd
        {
            get { return _has_pty_cd; }
            set { _has_pty_cd = value; }
        }
        public string Has_pty_tp
        {
            get { return _has_pty_tp; }
            set { _has_pty_tp = value; }
        }
        public decimal Has_rt
        {
            get { return _has_rt; }
            set { _has_rt = value; }
        }
        public string Has_sch_cd
        {
            get { return _has_sch_cd; }
            set { _has_sch_cd = value; }
        }
        public Int32 Has_seq
        {
            get { return _has_seq; }
            set { _has_seq = value; }
        }
        public DateTime Has_to_dt
        {
            get { return _has_to_dt; }
            set { _has_to_dt = value; }
        }
        public decimal Has_to_val
        {
            get { return _has_to_val; }
            set { _has_to_val = value; }
        }

        public static HpAdditionalServiceCharges Converter(DataRow row)
        {
            return new HpAdditionalServiceCharges
            {
                Has_cal_on = row["HAS_CAL_ON"] == DBNull.Value ? false : Convert.ToBoolean(row["HAS_CAL_ON"]),
                Has_chg = row["HAS_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAS_CHG"]),
                Has_chk_on = row["HAS_CHK_ON"] == DBNull.Value ? false : Convert.ToBoolean(row["HAS_CHK_ON"]),
                Has_cre_by = row["HAS_CRE_BY"] == DBNull.Value ? string.Empty : row["HAS_CRE_BY"].ToString(),
                Has_cre_dt = row["HAS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAS_CRE_DT"]),
                Has_from_dt = row["HAS_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAS_FROM_DT"]),
                Has_from_val = row["HAS_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAS_FROM_VAL"]),
                Has_pty_cd = row["HAS_PTY_CD"] == DBNull.Value ? string.Empty : row["HAS_PTY_CD"].ToString(),
                Has_pty_tp = row["HAS_PTY_TP"] == DBNull.Value ? string.Empty : row["HAS_PTY_TP"].ToString(),
                Has_rt = row["HAS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAS_RT"]),
                Has_sch_cd = row["HAS_SCH_CD"] == DBNull.Value ? string.Empty : row["HAS_SCH_CD"].ToString(),
                Has_seq = row["HAS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAS_SEQ"]),
                Has_to_dt = row["HAS_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAS_TO_DT"]),
                Has_to_val = row["HAS_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAS_TO_VAL"])

            };
        }

    }
}
