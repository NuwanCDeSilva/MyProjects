using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class EarlyClosingDiscount
    {

        #region Private Members
        private string _hed_acc_no;
        private string _hed_comit;
        private string _hed_cre_by;
        private DateTime _hed_cre_dt;
        private string _hed_ecd_base;
        private decimal _hed_ecd_cls_val;
        private Boolean _hed_ecd_is_rt;
        private decimal _hed_ecd_val;
        private string _hed_eff_acc_tp;
        private string _hed_eff_cre_dt;
        private DateTime _hed_eff_dt;
        private DateTime _hed_from_dt;
        private Int32 _hed_from_pd;
        private Boolean _hed_is_prt;
        private Boolean _hed_is_use;
        private string _hed_pb;
        private string _hed_pb_lvl;
        private string _hed_prt_by;
        private DateTime _hed_prt_dt;
        private string _hed_pty_cd;
        private string _hed_pty_tp;
        private string _hed_sch_cd;
        private Int32 _hed_seq;
        private DateTime _hed_to_dt;
        private Int32 _hed_to_pd;
        private string _hed_tp;
        private DateTime _hed_use_dt;
        private decimal _hed_val;
        private string _hed_vou_no;
        private Int32 _HED_IS_REDUCE_BAL;
        private Int32 _HED_NOOF_AC_FRM;     //kapila 29/9/2015
        private Int32 _HED_NOOF_AC_TO;
        #endregion

        public Int32 HED_NOOF_AC_FRM
        {
            get { return _HED_NOOF_AC_FRM; }
            set { _HED_NOOF_AC_FRM = value; }
        }
        public Int32 HED_NOOF_AC_TO
        {
            get { return _HED_NOOF_AC_TO; }
            set { _HED_NOOF_AC_TO = value; }
        }
        public Int32 HED_IS_REDUCE_BAL
        {
            get { return _HED_IS_REDUCE_BAL; }
            set { _HED_IS_REDUCE_BAL = value; }
        }

        public string Hed_acc_no
        {
            get { return _hed_acc_no; }
            set { _hed_acc_no = value; }
        }
        public string Hed_comit
        {
            get { return _hed_comit; }
            set { _hed_comit = value; }
        }
        public string Hed_cre_by
        {
            get { return _hed_cre_by; }
            set { _hed_cre_by = value; }
        }
        public DateTime Hed_cre_dt
        {
            get { return _hed_cre_dt; }
            set { _hed_cre_dt = value; }
        }
        public string Hed_ecd_base
        {
            get { return _hed_ecd_base; }
            set { _hed_ecd_base = value; }
        }
        public decimal Hed_ecd_cls_val
        {
            get { return _hed_ecd_cls_val; }
            set { _hed_ecd_cls_val = value; }
        }
        public Boolean Hed_ecd_is_rt
        {
            get { return _hed_ecd_is_rt; }
            set { _hed_ecd_is_rt = value; }
        }
        public decimal Hed_ecd_val
        {
            get { return _hed_ecd_val; }
            set { _hed_ecd_val = value; }
        }
        public string Hed_eff_acc_tp
        {
            get { return _hed_eff_acc_tp; }
            set { _hed_eff_acc_tp = value; }
        }
        public string Hed_eff_cre_dt
        {
            get { return _hed_eff_cre_dt; }
            set { _hed_eff_cre_dt = value; }
        }
        public DateTime Hed_eff_dt
        {
            get { return _hed_eff_dt; }
            set { _hed_eff_dt = value; }
        }
        public DateTime Hed_from_dt
        {
            get { return _hed_from_dt; }
            set { _hed_from_dt = value; }
        }
        public Int32 Hed_from_pd
        {
            get { return _hed_from_pd; }
            set { _hed_from_pd = value; }
        }
        public Boolean Hed_is_prt
        {
            get { return _hed_is_prt; }
            set { _hed_is_prt = value; }
        }
        public Boolean Hed_is_use
        {
            get { return _hed_is_use; }
            set { _hed_is_use = value; }
        }
        public string Hed_pb
        {
            get { return _hed_pb; }
            set { _hed_pb = value; }
        }
        public string Hed_pb_lvl
        {
            get { return _hed_pb_lvl; }
            set { _hed_pb_lvl = value; }
        }
        public string Hed_prt_by
        {
            get { return _hed_prt_by; }
            set { _hed_prt_by = value; }
        }
        public DateTime Hed_prt_dt
        {
            get { return _hed_prt_dt; }
            set { _hed_prt_dt = value; }
        }
        public string Hed_pty_cd
        {
            get { return _hed_pty_cd; }
            set { _hed_pty_cd = value; }
        }
        public string Hed_pty_tp
        {
            get { return _hed_pty_tp; }
            set { _hed_pty_tp = value; }
        }
        public string Hed_sch_cd
        {
            get { return _hed_sch_cd; }
            set { _hed_sch_cd = value; }
        }
        public Int32 Hed_seq
        {
            get { return _hed_seq; }
            set { _hed_seq = value; }
        }
        public DateTime Hed_to_dt
        {
            get { return _hed_to_dt; }
            set { _hed_to_dt = value; }
        }
        public Int32 Hed_to_pd
        {
            get { return _hed_to_pd; }
            set { _hed_to_pd = value; }
        }
        public string Hed_tp
        {
            get { return _hed_tp; }
            set { _hed_tp = value; }
        }
        public DateTime Hed_use_dt
        {
            get { return _hed_use_dt; }
            set { _hed_use_dt = value; }
        }
        public decimal Hed_val
        {
            get { return _hed_val; }
            set { _hed_val = value; }
        }
        public string Hed_vou_no
        {
            get { return _hed_vou_no; }
            set { _hed_vou_no = value; }
        }

        public static EarlyClosingDiscount Converter(DataRow row)
        {
            return new EarlyClosingDiscount
            {
                Hed_acc_no = row["HED_ACC_NO"] == DBNull.Value ? string.Empty : row["HED_ACC_NO"].ToString(),
                Hed_comit = row["HED_COMIT"] == DBNull.Value ? string.Empty : row["HED_COMIT"].ToString(),
                Hed_cre_by = row["HED_CRE_BY"] == DBNull.Value ? string.Empty : row["HED_CRE_BY"].ToString(),
                Hed_cre_dt = row["HED_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HED_CRE_DT"]),
                Hed_ecd_base = row["HED_ECD_BASE"] == DBNull.Value ? string.Empty : row["HED_ECD_BASE"].ToString(),
                Hed_ecd_cls_val = row["HED_ECD_CLS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HED_ECD_CLS_VAL"]),
                Hed_ecd_is_rt = row["HED_ECD_IS_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["HED_ECD_IS_RT"]),
                Hed_ecd_val = row["HED_ECD_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HED_ECD_VAL"]),
                Hed_eff_acc_tp = row["HED_EFF_ACC_TP"] == DBNull.Value ? string.Empty : row["HED_EFF_ACC_TP"].ToString(),
                Hed_eff_cre_dt = row["HED_EFF_CRE_DT"] == DBNull.Value ? string.Empty : row["HED_EFF_CRE_DT"].ToString(),
                Hed_eff_dt = row["HED_EFF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HED_EFF_DT"]),
                Hed_from_dt = row["HED_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HED_FROM_DT"]),
                Hed_from_pd = row["HED_FROM_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["HED_FROM_PD"]),
                Hed_is_prt = row["HED_IS_PRT"] == DBNull.Value ? false : Convert.ToBoolean(row["HED_IS_PRT"]),
                Hed_is_use = row["HED_IS_USE"] == DBNull.Value ? false : Convert.ToBoolean(row["HED_IS_USE"]),
                Hed_pb = row["HED_PB"] == DBNull.Value ? string.Empty : row["HED_PB"].ToString(),
                Hed_pb_lvl = row["HED_PB_LVL"] == DBNull.Value ? string.Empty : row["HED_PB_LVL"].ToString(),
                Hed_prt_by = row["HED_PRT_BY"] == DBNull.Value ? string.Empty : row["HED_PRT_BY"].ToString(),
                Hed_prt_dt = row["HED_PRT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HED_PRT_DT"]),
                Hed_pty_cd = row["HED_PTY_CD"] == DBNull.Value ? string.Empty : row["HED_PTY_CD"].ToString(),
                Hed_pty_tp = row["HED_PTY_TP"] == DBNull.Value ? string.Empty : row["HED_PTY_TP"].ToString(),
                Hed_sch_cd = row["HED_SCH_CD"] == DBNull.Value ? string.Empty : row["HED_SCH_CD"].ToString(),
                Hed_seq = row["HED_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HED_SEQ"]),
                Hed_to_dt = row["HED_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HED_TO_DT"]),
                Hed_to_pd = row["HED_TO_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["HED_TO_PD"]),
                Hed_tp = row["HED_TP"] == DBNull.Value ? string.Empty : row["HED_TP"].ToString(),
                Hed_use_dt = row["HED_USE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HED_USE_DT"]),
                Hed_val = row["HED_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HED_VAL"]),
                Hed_vou_no = row["HED_VOU_NO"] == DBNull.Value ? string.Empty : row["HED_VOU_NO"].ToString(),
                HED_NOOF_AC_FRM = row["HED_NOOF_AC_FRM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HED_NOOF_AC_FRM"]),
                HED_NOOF_AC_TO = row["HED_NOOF_AC_TO"] == DBNull.Value ? 0 : Convert.ToInt32(row["HED_NOOF_AC_TO"])

            };
        }

    }
}
