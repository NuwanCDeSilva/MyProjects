using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class LoyaltyMemberLog
    {
        #region Private Members
        private decimal _sacml_bal_pt;
        private string _sacml_cd_ser;
        private decimal _sacml_col_pt;
        private string _sacml_cre_by;
        private DateTime _sacml_cre_dt;
        private string _sacml_cus_cd;
        private string _sacml_cus_spec;
        private decimal _sacml_dis_rt;
        private decimal _sacml_exp_pt;
        private string _sacml_loty_tp;
        private string _sacml_no;
        private decimal _sacml_red_pt;
        private Int32 _sacml_seq_no;

        #endregion

        public decimal Sacml_bal_pt
        {
            get { return _sacml_bal_pt; }
            set { _sacml_bal_pt = value; }
        }
        public string Sacml_cd_ser
        {
            get { return _sacml_cd_ser; }
            set { _sacml_cd_ser = value; }
        }
        public decimal Sacml_col_pt
        {
            get { return _sacml_col_pt; }
            set { _sacml_col_pt = value; }
        }
        public string Sacml_cre_by
        {
            get { return _sacml_cre_by; }
            set { _sacml_cre_by = value; }
        }
        public DateTime Sacml_cre_dt
        {
            get { return _sacml_cre_dt; }
            set { _sacml_cre_dt = value; }
        }
        public string Sacml_cus_cd
        {
            get { return _sacml_cus_cd; }
            set { _sacml_cus_cd = value; }
        }
        public string Sacml_cus_spec
        {
            get { return _sacml_cus_spec; }
            set { _sacml_cus_spec = value; }
        }
        public decimal Sacml_dis_rt
        {
            get { return _sacml_dis_rt; }
            set { _sacml_dis_rt = value; }
        }
        public decimal Sacml_exp_pt
        {
            get { return _sacml_exp_pt; }
            set { _sacml_exp_pt = value; }
        }
        public string Sacml_loty_tp
        {
            get { return _sacml_loty_tp; }
            set { _sacml_loty_tp = value; }
        }
        public string Sacml_no
        {
            get { return _sacml_no; }
            set { _sacml_no = value; }
        }
        public decimal Sacml_red_pt
        {
            get { return _sacml_red_pt; }
            set { _sacml_red_pt = value; }
        }
        public Int32 Sacml_seq_no
        {
            get { return _sacml_seq_no; }
            set { _sacml_seq_no = value; }
        }

        public static LoyaltyMemberLog Converter(DataRow row)
        {
            return new LoyaltyMemberLog
            {
                Sacml_bal_pt = row["SACML_BAL_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SACML_BAL_PT"]),
                Sacml_cd_ser = row["SACML_CD_SER"] == DBNull.Value ? string.Empty : row["SACML_CD_SER"].ToString(),
                Sacml_col_pt = row["SACML_COL_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SACML_COL_PT"]),
                Sacml_cre_by = row["SACML_CRE_BY"] == DBNull.Value ? string.Empty : row["SACML_CRE_BY"].ToString(),
                Sacml_cre_dt = row["SACML_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SACML_CRE_DT"]),
                Sacml_cus_cd = row["SACML_CUS_CD"] == DBNull.Value ? string.Empty : row["SACML_CUS_CD"].ToString(),
                Sacml_cus_spec = row["SACML_CUS_SPEC"] == DBNull.Value ? string.Empty : row["SACML_CUS_SPEC"].ToString(),
                Sacml_dis_rt = row["SACML_DIS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SACML_DIS_RT"]),
                Sacml_exp_pt = row["SACML_EXP_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SACML_EXP_PT"]),
                Sacml_loty_tp = row["SACML_LOTY_TP"] == DBNull.Value ? string.Empty : row["SACML_LOTY_TP"].ToString(),
                Sacml_no = row["SACML_NO"] == DBNull.Value ? string.Empty : row["SACML_NO"].ToString(),
                Sacml_red_pt = row["SACML_RED_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SACML_RED_PT"]),
                Sacml_seq_no = row["SACML_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SACML_SEQ_NO"])

            };
        }

    }
}

