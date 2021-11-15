using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//Add darshana 07/08/2012
namespace FF.BusinessObjects
{
    [Serializable]
    public class HPAccountLog
    {
        #region Private Members
        private string _hal_acc_no;
        private decimal _hal_af_val;
        private string _hal_bank;
        private decimal _hal_buy_val;
        private decimal _hal_cash_val;
        private DateTime _hal_cls_dt;
        private string _hal_com;
        private string _hal_cre_by;
        private DateTime _hal_cre_dt;
        private decimal _hal_dp_comm;
        private decimal _hal_dp_val;
        private Boolean _hal_ecd_stus;
        private string _hal_ecd_tp;
        private string _hal_flag;
        private string _hal_grup_cd;
        private decimal _hal_hp_val;
        private decimal _hal_init_ins;
        private decimal _hal_init_ser_chg;
        private decimal _hal_init_stm;
        private decimal _hal_init_vat;
        private decimal _hal_inst_comm;
        private decimal _hal_inst_ins;
        private decimal _hal_inst_ser_chg;
        private decimal _hal_inst_stm;
        private decimal _hal_inst_vat;
        private decimal _hal_intr_rt;
        private string _hal_invc_no;
        private Boolean _hal_is_rsch;
        private DateTime _hal_log_dt;
        private string _hal_mgr_cd;
        private decimal _hal_net_val;
        private decimal _hal_oth_chg;
        private string _hal_pc;
        private Boolean _hal_rev_stus;
        private DateTime _hal_rls_dt;
        private DateTime _hal_rsch_dt;
        private DateTime _hal_rv_dt;
        private string _hal_sa_sub_tp;
        private string _hal_sch_cd;
        private string _hal_sch_tp;
        private Int32 _hal_seq;
        private Int32 _hal_seq_no;
        private decimal _hal_ser_chg;
        private string _hal_stus;
        private decimal _hal_tc_val;
        private Int32 _hal_term;
        private decimal _hal_tot_intr;
        private decimal _hal_tot_vat;
        private decimal _hal_val_01;
        private decimal _hal_val_02;
        private decimal _hal_val_03;
        private decimal _hal_val_04;
        private decimal _hal_val_05;
        private DateTime _hpa_acc_cre_dt;
        #endregion

        public string Hal_acc_no
        {
            get { return _hal_acc_no; }
            set { _hal_acc_no = value; }
        }
        public decimal Hal_af_val
        {
            get { return _hal_af_val; }
            set { _hal_af_val = value; }
        }
        public string Hal_bank
        {
            get { return _hal_bank; }
            set { _hal_bank = value; }
        }
        public decimal Hal_buy_val
        {
            get { return _hal_buy_val; }
            set { _hal_buy_val = value; }
        }
        public decimal Hal_cash_val
        {
            get { return _hal_cash_val; }
            set { _hal_cash_val = value; }
        }
        public DateTime Hal_cls_dt
        {
            get { return _hal_cls_dt; }
            set { _hal_cls_dt = value; }
        }
        public string Hal_com
        {
            get { return _hal_com; }
            set { _hal_com = value; }
        }
        public string Hal_cre_by
        {
            get { return _hal_cre_by; }
            set { _hal_cre_by = value; }
        }
        public DateTime Hal_cre_dt
        {
            get { return _hal_cre_dt; }
            set { _hal_cre_dt = value; }
        }
        public decimal Hal_dp_comm
        {
            get { return _hal_dp_comm; }
            set { _hal_dp_comm = value; }
        }
        public decimal Hal_dp_val
        {
            get { return _hal_dp_val; }
            set { _hal_dp_val = value; }
        }
        public Boolean Hal_ecd_stus
        {
            get { return _hal_ecd_stus; }
            set { _hal_ecd_stus = value; }
        }
        public string Hal_ecd_tp
        {
            get { return _hal_ecd_tp; }
            set { _hal_ecd_tp = value; }
        }
        public string Hal_flag
        {
            get { return _hal_flag; }
            set { _hal_flag = value; }
        }
        public string Hal_grup_cd
        {
            get { return _hal_grup_cd; }
            set { _hal_grup_cd = value; }
        }
        public decimal Hal_hp_val
        {
            get { return _hal_hp_val; }
            set { _hal_hp_val = value; }
        }
        public decimal Hal_init_ins
        {
            get { return _hal_init_ins; }
            set { _hal_init_ins = value; }
        }
        public decimal Hal_init_ser_chg
        {
            get { return _hal_init_ser_chg; }
            set { _hal_init_ser_chg = value; }
        }
        public decimal Hal_init_stm
        {
            get { return _hal_init_stm; }
            set { _hal_init_stm = value; }
        }
        public decimal Hal_init_vat
        {
            get { return _hal_init_vat; }
            set { _hal_init_vat = value; }
        }
        public decimal Hal_inst_comm
        {
            get { return _hal_inst_comm; }
            set { _hal_inst_comm = value; }
        }
        public decimal Hal_inst_ins
        {
            get { return _hal_inst_ins; }
            set { _hal_inst_ins = value; }
        }
        public decimal Hal_inst_ser_chg
        {
            get { return _hal_inst_ser_chg; }
            set { _hal_inst_ser_chg = value; }
        }
        public decimal Hal_inst_stm
        {
            get { return _hal_inst_stm; }
            set { _hal_inst_stm = value; }
        }
        public decimal Hal_inst_vat
        {
            get { return _hal_inst_vat; }
            set { _hal_inst_vat = value; }
        }
        public decimal Hal_intr_rt
        {
            get { return _hal_intr_rt; }
            set { _hal_intr_rt = value; }
        }
        public string Hal_invc_no
        {
            get { return _hal_invc_no; }
            set { _hal_invc_no = value; }
        }
        public Boolean Hal_is_rsch
        {
            get { return _hal_is_rsch; }
            set { _hal_is_rsch = value; }
        }
        public DateTime Hal_log_dt
        {
            get { return _hal_log_dt; }
            set { _hal_log_dt = value; }
        }
        public string Hal_mgr_cd
        {
            get { return _hal_mgr_cd; }
            set { _hal_mgr_cd = value; }
        }
        public decimal Hal_net_val
        {
            get { return _hal_net_val; }
            set { _hal_net_val = value; }
        }
        public decimal Hal_oth_chg
        {
            get { return _hal_oth_chg; }
            set { _hal_oth_chg = value; }
        }
        public string Hal_pc
        {
            get { return _hal_pc; }
            set { _hal_pc = value; }
        }
        public Boolean Hal_rev_stus
        {
            get { return _hal_rev_stus; }
            set { _hal_rev_stus = value; }
        }
        public DateTime Hal_rls_dt
        {
            get { return _hal_rls_dt; }
            set { _hal_rls_dt = value; }
        }
        public DateTime Hal_rsch_dt
        {
            get { return _hal_rsch_dt; }
            set { _hal_rsch_dt = value; }
        }
        public DateTime Hal_rv_dt
        {
            get { return _hal_rv_dt; }
            set { _hal_rv_dt = value; }
        }
        public string Hal_sa_sub_tp
        {
            get { return _hal_sa_sub_tp; }
            set { _hal_sa_sub_tp = value; }
        }
        public string Hal_sch_cd
        {
            get { return _hal_sch_cd; }
            set { _hal_sch_cd = value; }
        }
        public string Hal_sch_tp
        {
            get { return _hal_sch_tp; }
            set { _hal_sch_tp = value; }
        }
        public Int32 Hal_seq
        {
            get { return _hal_seq; }
            set { _hal_seq = value; }
        }
        public Int32 Hal_seq_no
        {
            get { return _hal_seq_no; }
            set { _hal_seq_no = value; }
        }
        public decimal Hal_ser_chg
        {
            get { return _hal_ser_chg; }
            set { _hal_ser_chg = value; }
        }
        public string Hal_stus
        {
            get { return _hal_stus; }
            set { _hal_stus = value; }
        }
        public decimal Hal_tc_val
        {
            get { return _hal_tc_val; }
            set { _hal_tc_val = value; }
        }
        public Int32 Hal_term
        {
            get { return _hal_term; }
            set { _hal_term = value; }
        }
        public decimal Hal_tot_intr
        {
            get { return _hal_tot_intr; }
            set { _hal_tot_intr = value; }
        }
        public decimal Hal_tot_vat
        {
            get { return _hal_tot_vat; }
            set { _hal_tot_vat = value; }
        }
        public decimal Hal_val_01
        {
            get { return _hal_val_01; }
            set { _hal_val_01 = value; }
        }
        public decimal Hal_val_02
        {
            get { return _hal_val_02; }
            set { _hal_val_02 = value; }
        }
        public decimal Hal_val_03
        {
            get { return _hal_val_03; }
            set { _hal_val_03 = value; }
        }
        public decimal Hal_val_04
        {
            get { return _hal_val_04; }
            set { _hal_val_04 = value; }
        }
        public decimal Hal_val_05
        {
            get { return _hal_val_05; }
            set { _hal_val_05 = value; }
        }
        public DateTime Hpa_acc_cre_dt
        {
            get { return _hpa_acc_cre_dt; }
            set { _hpa_acc_cre_dt = value; }
        }

        public static HPAccountLog Converter(DataRow row)
        {
            return new HPAccountLog
            {
                Hal_acc_no = row["HAL_ACC_NO"] == DBNull.Value ? string.Empty : row["HAL_ACC_NO"].ToString(),
                Hal_af_val = row["HAL_AF_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_AF_VAL"]),
                Hal_bank = row["HAL_BANK"] == DBNull.Value ? string.Empty : row["HAL_BANK"].ToString(),
                Hal_buy_val = row["HAL_BUY_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_BUY_VAL"]),
                Hal_cash_val = row["HAL_CASH_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_CASH_VAL"]),
                Hal_cls_dt = row["HAL_CLS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAL_CLS_DT"]),
                Hal_com = row["HAL_COM"] == DBNull.Value ? string.Empty : row["HAL_COM"].ToString(),
                Hal_cre_by = row["HAL_CRE_BY"] == DBNull.Value ? string.Empty : row["HAL_CRE_BY"].ToString(),
                Hal_cre_dt = row["HAL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAL_CRE_DT"]),
                Hal_dp_comm = row["HAL_DP_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_DP_COMM"]),
                Hal_dp_val = row["HAL_DP_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_DP_VAL"]),
                Hal_ecd_stus = row["HAL_ECD_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["HAL_ECD_STUS"]),
                Hal_ecd_tp = row["HAL_ECD_TP"] == DBNull.Value ? string.Empty : row["HAL_ECD_TP"].ToString(),
                Hal_flag = row["HAL_FLAG"] == DBNull.Value ? string.Empty : row["HAL_FLAG"].ToString(),
                Hal_grup_cd = row["HAL_GRUP_CD"] == DBNull.Value ? string.Empty : row["HAL_GRUP_CD"].ToString(),
                Hal_hp_val = row["HAL_HP_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_HP_VAL"]),
                Hal_init_ins = row["HAL_INIT_INS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INIT_INS"]),
                Hal_init_ser_chg = row["HAL_INIT_SER_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INIT_SER_CHG"]),
                Hal_init_stm = row["HAL_INIT_STM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INIT_STM"]),
                Hal_init_vat = row["HAL_INIT_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INIT_VAT"]),
                Hal_inst_comm = row["HAL_INST_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INST_COMM"]),
                Hal_inst_ins = row["HAL_INST_INS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INST_INS"]),
                Hal_inst_ser_chg = row["HAL_INST_SER_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INST_SER_CHG"]),
                Hal_inst_stm = row["HAL_INST_STM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INST_STM"]),
                Hal_inst_vat = row["HAL_INST_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INST_VAT"]),
                Hal_intr_rt = row["HAL_INTR_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_INTR_RT"]),
                Hal_invc_no = row["HAL_INVC_NO"] == DBNull.Value ? string.Empty : row["HAL_INVC_NO"].ToString(),
                Hal_is_rsch = row["HAL_IS_RSCH"] == DBNull.Value ? false : Convert.ToBoolean(row["HAL_IS_RSCH"]),
                Hal_log_dt = row["HAL_LOG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAL_LOG_DT"]),
                Hal_mgr_cd = row["HAL_MGR_CD"] == DBNull.Value ? string.Empty : row["HAL_MGR_CD"].ToString(),
                Hal_net_val = row["HAL_NET_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_NET_VAL"]),
                Hal_oth_chg = row["HAL_OTH_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_OTH_CHG"]),
                Hal_pc = row["HAL_PC"] == DBNull.Value ? string.Empty : row["HAL_PC"].ToString(),
                Hal_rev_stus = row["HAL_REV_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["HAL_REV_STUS"]),
                Hal_rls_dt = row["HAL_RLS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAL_RLS_DT"]),
                Hal_rsch_dt = row["HAL_RSCH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAL_RSCH_DT"]),
                Hal_rv_dt = row["HAL_RV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAL_RV_DT"]),
                Hal_sa_sub_tp = row["HAL_SA_SUB_TP"] == DBNull.Value ? string.Empty : row["HAL_SA_SUB_TP"].ToString(),
                Hal_sch_cd = row["HAL_SCH_CD"] == DBNull.Value ? string.Empty : row["HAL_SCH_CD"].ToString(),
                Hal_sch_tp = row["HAL_SCH_TP"] == DBNull.Value ? string.Empty : row["HAL_SCH_TP"].ToString(),
                Hal_seq = row["HAL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAL_SEQ"]),
                Hal_seq_no = row["HAL_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAL_SEQ_NO"]),
                Hal_ser_chg = row["HAL_SER_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_SER_CHG"]),
                Hal_stus = row["HAL_STUS"] == DBNull.Value ? string.Empty : row["HAL_STUS"].ToString(),
                Hal_tc_val = row["HAL_TC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_TC_VAL"]),
                Hal_term = row["HAL_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAL_TERM"]),
                Hal_tot_intr = row["HAL_TOT_INTR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_TOT_INTR"]),
                Hal_tot_vat = row["HAL_TOT_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_TOT_VAT"]),
                Hal_val_01 = row["HAL_VAL_01"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_VAL_01"]),
                Hal_val_02 = row["HAL_VAL_02"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_VAL_02"]),
                Hal_val_03 = row["HAL_VAL_03"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_VAL_03"]),
                Hal_val_04 = row["HAL_VAL_04"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_VAL_04"]),
                Hal_val_05 = row["HAL_VAL_05"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAL_VAL_05"]),
                Hpa_acc_cre_dt = row["HPA_ACC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPA_ACC_CRE_DT"])

            };
        }

    }
}
