using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
   public class HpCashConversionDefinition
    {

        /// <summary>
        /// Written By Prabhath on 05/07/2012
        /// Table: HPR_CC_DEFN (in EMS)
        /// </summary>

        #region Private Members
        private string _hcc_add_cal_on;
        private decimal _hcc_add_chg_rt;
        private decimal _hcc_add_chg_val;
        private string _hcc_cal_on;
        private DateTime _hcc_cc_upto;
        private string _hcc_chk_on;
        private string _hcc_cre_by;
        private DateTime _hcc_cre_dt;
        private DateTime _hcc_from_cre_dt;
        private Int32 _hcc_from_pd;
        private decimal _hcc_from_val;
        private string _hcc_pb;
        private string _hcc_pb_conv;
        private string _hcc_pb_lvl;
        private string _hcc_pty_cd;
        private string _hcc_pty_tp;
        private string _hcc_sch_cd;
        private Int32 _hcc_seq;
        private decimal _hcc_ser_chg_rt;
        private decimal _hcc_ser_chg_val;
        private DateTime _hcc_to_cre_dt;
        private Int32 _hcc_to_pd;
        private decimal _hcc_to_val;
        #endregion

        public string Hcc_add_cal_on { get { return _hcc_add_cal_on; } set { _hcc_add_cal_on = value; } }
        public decimal Hcc_add_chg_rt { get { return _hcc_add_chg_rt; } set { _hcc_add_chg_rt = value; } }
        public decimal Hcc_add_chg_val { get { return _hcc_add_chg_val; } set { _hcc_add_chg_val = value; } }
        public string Hcc_cal_on { get { return _hcc_cal_on; } set { _hcc_cal_on = value; } }
        public DateTime Hcc_cc_upto { get { return _hcc_cc_upto; } set { _hcc_cc_upto = value; } }
        public string Hcc_chk_on { get { return _hcc_chk_on; } set { _hcc_chk_on = value; } }
        public string Hcc_cre_by { get { return _hcc_cre_by; } set { _hcc_cre_by = value; } }
        public DateTime Hcc_cre_dt { get { return _hcc_cre_dt; } set { _hcc_cre_dt = value; } }
        public DateTime Hcc_from_cre_dt { get { return _hcc_from_cre_dt; } set { _hcc_from_cre_dt = value; } }
        public Int32 Hcc_from_pd { get { return _hcc_from_pd; } set { _hcc_from_pd = value; } }
        public decimal Hcc_from_val { get { return _hcc_from_val; } set { _hcc_from_val = value; } }
        public string Hcc_pb { get { return _hcc_pb; } set { _hcc_pb = value; } }
        public string Hcc_pb_conv { get { return _hcc_pb_conv; } set { _hcc_pb_conv = value; } }
        public string Hcc_pb_lvl { get { return _hcc_pb_lvl; } set { _hcc_pb_lvl = value; } }
        public string Hcc_pty_cd { get { return _hcc_pty_cd; } set { _hcc_pty_cd = value; } }
        public string Hcc_pty_tp { get { return _hcc_pty_tp; } set { _hcc_pty_tp = value; } }
        public string Hcc_sch_cd { get { return _hcc_sch_cd; } set { _hcc_sch_cd = value; } }
        public Int32 Hcc_seq { get { return _hcc_seq; } set { _hcc_seq = value; } }
        public decimal Hcc_ser_chg_rt { get { return _hcc_ser_chg_rt; } set { _hcc_ser_chg_rt = value; } }
        public decimal Hcc_ser_chg_val { get { return _hcc_ser_chg_val; } set { _hcc_ser_chg_val = value; } }
        public DateTime Hcc_to_cre_dt { get { return _hcc_to_cre_dt; } set { _hcc_to_cre_dt = value; } }
        public Int32 Hcc_to_pd { get { return _hcc_to_pd; } set { _hcc_to_pd = value; } }
        public decimal Hcc_to_val { get { return _hcc_to_val; } set { _hcc_to_val = value; } }

        public static HpCashConversionDefinition Converter(DataRow row)
        {
            return new HpCashConversionDefinition
            {
                Hcc_add_cal_on = row["HCC_ADD_CAL_ON"] == DBNull.Value ? string.Empty : row["HCC_ADD_CAL_ON"].ToString(),
                Hcc_add_chg_rt = row["HCC_ADD_CHG_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCC_ADD_CHG_RT"]),
                Hcc_add_chg_val = row["HCC_ADD_CHG_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCC_ADD_CHG_VAL"]),
                Hcc_cal_on = row["HCC_CAL_ON"] == DBNull.Value ? string.Empty : row["HCC_CAL_ON"].ToString(),
                Hcc_cc_upto = row["HCC_CC_UPTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HCC_CC_UPTO"]),
                Hcc_chk_on = row["HCC_CHK_ON"] == DBNull.Value ? string.Empty : row["HCC_CHK_ON"].ToString(),
                Hcc_cre_by = row["HCC_CRE_BY"] == DBNull.Value ? string.Empty : row["HCC_CRE_BY"].ToString(),
                Hcc_cre_dt = row["HCC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HCC_CRE_DT"]),
                Hcc_from_cre_dt = row["HCC_FROM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HCC_FROM_CRE_DT"]),
                Hcc_from_pd = row["HCC_FROM_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["HCC_FROM_PD"]),
                Hcc_from_val = row["HCC_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCC_FROM_VAL"]),
                Hcc_pb = row["HCC_PB"] == DBNull.Value ? string.Empty : row["HCC_PB"].ToString(),
                Hcc_pb_conv = row["HCC_PB_CONV"] == DBNull.Value ? string.Empty : row["HCC_PB_CONV"].ToString(),
                Hcc_pb_lvl = row["HCC_PB_LVL"] == DBNull.Value ? string.Empty : row["HCC_PB_LVL"].ToString(),
                Hcc_pty_cd = row["HCC_PTY_CD"] == DBNull.Value ? string.Empty : row["HCC_PTY_CD"].ToString(),
                Hcc_pty_tp = row["HCC_PTY_TP"] == DBNull.Value ? string.Empty : row["HCC_PTY_TP"].ToString(),
                Hcc_sch_cd = row["HCC_SCH_CD"] == DBNull.Value ? string.Empty : row["HCC_SCH_CD"].ToString(),
                Hcc_seq = row["HCC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HCC_SEQ"]),
                Hcc_ser_chg_rt = row["HCC_SER_CHG_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCC_SER_CHG_RT"]),
                Hcc_ser_chg_val = row["HCC_SER_CHG_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCC_SER_CHG_VAL"]),
                Hcc_to_cre_dt = row["HCC_TO_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HCC_TO_CRE_DT"]),
                Hcc_to_pd = row["HCC_TO_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["HCC_TO_PD"]),
                Hcc_to_val = row["HCC_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCC_TO_VAL"])

            };
        }
    }
}

