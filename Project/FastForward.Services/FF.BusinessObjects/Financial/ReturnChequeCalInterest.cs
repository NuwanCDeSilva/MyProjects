using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
    public class ReturnChequeCalInterest
    {
        #region Private Members
        private decimal _src_cal_cap;
        private Int32 _src_cal_from;
        private Int32 _src_cal_to;
        private string _src_com;
        private DateTime _src_cre_dt;
        private DateTime _src_due_cal_dt;
        private Int32 _src_due_days;
        private decimal _src_int;
        private string _src_pc;
        private string _src_ref_no;
        private string _src_usr;
        private Boolean _src_active;
        private string _src_chq_bank;
        private string _src_rtn_bank;
        #endregion

        public string Src_rtn_bank
        {
            get { return _src_rtn_bank; }
            set { _src_rtn_bank = value; }
        }

        public string Src_chq_bank
        {
            get { return _src_chq_bank; }
            set { _src_chq_bank = value; }
        }

        public decimal Src_cal_cap
        {
            get { return _src_cal_cap; }
            set { _src_cal_cap = value; }
        }
        public Int32 Src_cal_from
        {
            get { return _src_cal_from; }
            set { _src_cal_from = value; }
        }
        public Int32 Src_cal_to
        {
            get { return _src_cal_to; }
            set { _src_cal_to = value; }
        }
        public string Src_com
        {
            get { return _src_com; }
            set { _src_com = value; }
        }
        public DateTime Src_cre_dt
        {
            get { return _src_cre_dt; }
            set { _src_cre_dt = value; }
        }
        public DateTime Src_due_cal_dt
        {
            get { return _src_due_cal_dt; }
            set { _src_due_cal_dt = value; }
        }
        public Int32 Src_due_days
        {
            get { return _src_due_days; }
            set { _src_due_days = value; }
        }
        public decimal Src_int
        {
            get { return _src_int; }
            set { _src_int = value; }
        }
        public string Src_pc
        {
            get { return _src_pc; }
            set { _src_pc = value; }
        }
        public string Src_ref_no
        {
            get { return _src_ref_no; }
            set { _src_ref_no = value; }
        }
        public string Src_usr
        {
            get { return _src_usr; }
            set { _src_usr = value; }
        }
        public Boolean Src_active
        {
            get { return _src_active; }
            set { _src_active = value; }
        }

        public static ReturnChequeCalInterest Converter(DataRow row)
        {
            return new ReturnChequeCalInterest
            {
                Src_cal_cap = row["SRC_CAL_CAP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRC_CAL_CAP"]),
                Src_cal_from = row["SRC_CAL_FROM"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRC_CAL_FROM"]),
                Src_cal_to = row["SRC_CAL_TO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRC_CAL_TO"]),
                Src_com = row["SRC_COM"] == DBNull.Value ? string.Empty : row["SRC_COM"].ToString(),
                Src_cre_dt = row["SRC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRC_CRE_DT"]),
                Src_due_cal_dt = row["SRC_DUE_CAL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRC_DUE_CAL_DT"]),
                Src_due_days = row["SRC_DUE_DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRC_DUE_DAYS"]),
                Src_int = row["SRC_INT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRC_INT"]),
                Src_pc = row["SRC_PC"] == DBNull.Value ? string.Empty : row["SRC_PC"].ToString(),
                Src_ref_no = row["SRC_REF_NO"] == DBNull.Value ? string.Empty : row["SRC_REF_NO"].ToString(),
                Src_usr = row["SRC_USR"] == DBNull.Value ? string.Empty : row["SRC_USR"].ToString(),
                Src_active = row["SRC_ACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(row["SRC_ACTIVE"]),
                Src_rtn_bank = row["SRC_RTN_BANK"] == DBNull.Value ? string.Empty : row["SRC_RTN_BANK"].ToString(),
                Src_chq_bank = row["SRC_CHQ_BANK"] == DBNull.Value ? string.Empty : row["SRC_CHQ_BANK"].ToString()
            };
        }

    }
}
