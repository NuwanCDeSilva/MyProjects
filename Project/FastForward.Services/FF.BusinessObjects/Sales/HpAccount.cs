using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpAccount
    { 
        /// <summary>
        /// Written By Shani on 20/06/2012
        /// Table: HPT_ACC (in EMS)
        /// </summary>
        #region Private Members
        private DateTime _hpa_acc_cre_dt;
        private string _hpa_acc_no;
        private string _hpa_com;
        private decimal _hpa_af_val;
        private string _hpa_bank;
        private decimal _hpa_buy_val;
        private decimal _hpa_cash_val;
        private DateTime _hpa_cls_dt;
        private string _hpa_cre_by;
        private DateTime _hpa_cre_dt;
        private decimal _hpa_dp_comm;
        private decimal _hpa_dp_val;
        private Boolean _hpa_ecd_stus;
        private string _hpa_ecd_tp;
        private string _hpa_flag;
        private string _hpa_grup_cd;
        private decimal _hpa_hp_val;
        private decimal _hpa_init_ins;
        private decimal _hpa_init_ser_chg;
        private decimal _hpa_init_stm;
        private decimal _hpa_init_vat;
        private decimal _hpa_inst_comm;
        private decimal _hpa_inst_ins;
        private decimal _hpa_inst_ser_chg;
        private decimal _hpa_inst_stm;
        private decimal _hpa_inst_vat;
        private decimal _hpa_intr_rt;
        private string _hpa_invc_no;
        private Boolean _hpa_is_rsch;
        private string _hpa_pc;
        private string _hpa_mgr_cd;
        private decimal _hpa_net_val;
        private decimal _hpa_oth_chg;
        private Boolean _hpa_prt_ack;
        private DateTime _hpa_rls_dt;
        private DateTime _hpa_rsch_dt;
        private DateTime _hpa_rv_dt;
        private string _hpa_sch_cd;
        private string _hpa_sch_tp;
        private Int32 _hpa_seq;
        private decimal _hpa_ser_chg;
        private string _hpa_stus;
        private decimal _hpa_tc_val;
        private Int32 _hpa_term;
        private decimal _hpa_tot_intr;
        private decimal _hpa_tot_vat;
        private Int32 _hpa_val_01;
        private Int32 _hpa_val_02;
        private Int32 _hpa_val_03;
        private Int32 _hpa_val_04;
        private Int32 _hpa_val_05;
        private Int32 _hpa_seq_no;
        #endregion

        public DateTime Hpa_acc_cre_dt { get { return _hpa_acc_cre_dt; } set { _hpa_acc_cre_dt = value; } }
        public string Hpa_acc_no { get { return _hpa_acc_no; } set { _hpa_acc_no = value; } }
        public decimal Hpa_af_val { get { return _hpa_af_val; } set { _hpa_af_val = value; } }
        public string Hpa_bank { get { return _hpa_bank; } set { _hpa_bank = value; } }
        public decimal Hpa_buy_val { get { return _hpa_buy_val; } set { _hpa_buy_val = value; } }
        public decimal Hpa_cash_val { get { return _hpa_cash_val; } set { _hpa_cash_val = value; } }
        public DateTime Hpa_cls_dt { get { return _hpa_cls_dt; } set { _hpa_cls_dt = value; } }
        public string Hpa_cre_by { get { return _hpa_cre_by; } set { _hpa_cre_by = value; } }
        public DateTime Hpa_cre_dt { get { return _hpa_cre_dt; } set { _hpa_cre_dt = value; } }
        public decimal Hpa_dp_comm { get { return _hpa_dp_comm; } set { _hpa_dp_comm = value; } }
        public decimal Hpa_dp_val { get { return _hpa_dp_val; } set { _hpa_dp_val = value; } }
        public Boolean Hpa_ecd_stus { get { return _hpa_ecd_stus; } set { _hpa_ecd_stus = value; } }
        public string Hpa_ecd_tp { get { return _hpa_ecd_tp; } set { _hpa_ecd_tp = value; } }
        public string Hpa_flag { get { return _hpa_flag; } set { _hpa_flag = value; } }
        public string Hpa_grup_cd { get { return _hpa_grup_cd; } set { _hpa_grup_cd = value; } }
        public decimal Hpa_hp_val { get { return _hpa_hp_val; } set { _hpa_hp_val = value; } }
        public decimal Hpa_init_ins { get { return _hpa_init_ins; } set { _hpa_init_ins = value; } }
        public decimal Hpa_init_ser_chg { get { return _hpa_init_ser_chg; } set { _hpa_init_ser_chg = value; } }
        public decimal Hpa_init_stm { get { return _hpa_init_stm; } set { _hpa_init_stm = value; } }
        public decimal Hpa_init_vat { get { return _hpa_init_vat; } set { _hpa_init_vat = value; } }
        public decimal Hpa_inst_comm { get { return _hpa_inst_comm; } set { _hpa_inst_comm = value; } }
        public decimal Hpa_inst_ins { get { return _hpa_inst_ins; } set { _hpa_inst_ins = value; } }
        public decimal Hpa_inst_ser_chg { get { return _hpa_inst_ser_chg; } set { _hpa_inst_ser_chg = value; } }
        public decimal Hpa_inst_stm { get { return _hpa_inst_stm; } set { _hpa_inst_stm = value; } }
        public decimal Hpa_inst_vat { get { return _hpa_inst_vat; } set { _hpa_inst_vat = value; } }
        public decimal Hpa_intr_rt { get { return _hpa_intr_rt; } set { _hpa_intr_rt = value; } }
        public string Hpa_invc_no { get { return _hpa_invc_no; } set { _hpa_invc_no = value; } }
        public Boolean Hpa_is_rsch { get { return _hpa_is_rsch; } set { _hpa_is_rsch = value; } }
        public string Hpa_pc { get { return _hpa_pc; } set { _hpa_pc = value; } }
        public string Hpa_mgr_cd { get { return _hpa_mgr_cd; } set { _hpa_mgr_cd = value; } }
        public decimal Hpa_net_val { get { return _hpa_net_val; } set { _hpa_net_val = value; } }
        public decimal Hpa_oth_chg { get { return _hpa_oth_chg; } set { _hpa_oth_chg = value; } }
        public Boolean Hpa_prt_ack { get { return _hpa_prt_ack; } set { _hpa_prt_ack = value; } }
        public DateTime Hpa_rls_dt { get { return _hpa_rls_dt; } set { _hpa_rls_dt = value; } }
        public DateTime Hpa_rsch_dt { get { return _hpa_rsch_dt; } set { _hpa_rsch_dt = value; } }
        public DateTime Hpa_rv_dt { get { return _hpa_rv_dt; } set { _hpa_rv_dt = value; } }
        public string Hpa_sch_cd { get { return _hpa_sch_cd; } set { _hpa_sch_cd = value; } }
        public string Hpa_sch_tp { get { return _hpa_sch_tp; } set { _hpa_sch_tp = value; } }
        public Int32 Hpa_seq { get { return _hpa_seq; } set { _hpa_seq = value; } }
        public decimal Hpa_ser_chg { get { return _hpa_ser_chg; } set { _hpa_ser_chg = value; } }
        public string Hpa_stus { get { return _hpa_stus; } set { _hpa_stus = value; } }
        public decimal Hpa_tc_val { get { return _hpa_tc_val; } set { _hpa_tc_val = value; } }
        public Int32 Hpa_term { get { return _hpa_term; } set { _hpa_term = value; } }
        public decimal Hpa_tot_intr { get { return _hpa_tot_intr; } set { _hpa_tot_intr = value; } }
        public decimal Hpa_tot_vat { get { return _hpa_tot_vat; } set { _hpa_tot_vat = value; } }
        public Int32 Hpa_val_01 { get { return _hpa_val_01; } set { _hpa_val_01 = value; } }
        public Int32 Hpa_val_02 { get { return _hpa_val_02; } set { _hpa_val_02 = value; } }
        public Int32 Hpa_val_03 { get { return _hpa_val_03; } set { _hpa_val_03 = value; } }
        public Int32 Hpa_val_04 { get { return _hpa_val_04; } set { _hpa_val_04 = value; } }
        public Int32 Hpa_val_05 { get { return _hpa_val_05; } set { _hpa_val_05 = value; } }
        public Int32 Hpa_seq_no { get { return _hpa_seq_no; } set { _hpa_seq_no = value; } }
        public string Hpa_com { get { return _hpa_com; } set { _hpa_com = value; } }

        //---------------Create By Sandaruwan==2019/02/13
        //public string Hpa_ChkAccountNo { get { return _hpa_stus; } set { _hpa_stus = value; } }
        //public string Hpa_ChkAccountstus { get { return _hpa_stus; } set { _hpa_stus = value; } }
        //----------------
        // public static HpAccount Converter2(DataRow row)
        //{
        //    return new HpAccount
        //    {
        //        Hpa_ChkAccountNo = row["HPA_CHECKED_ACC_NO"] == DBNull.Value ? string.Empty : row["HPA_CHECKED_ACC_NO"].ToString(),
        //        Hpa_ChkAccountstus = row["HPA_CHECKED_ACC_STATUS"] == DBNull.Value ? string.Empty : row["HPA_ACC_NO"].ToString(),
        //    };
        //}
        public static HpAccount Converter(DataRow row)
        {
            return new HpAccount
            {
                Hpa_acc_cre_dt = row["HPA_ACC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPA_ACC_CRE_DT"]),
                Hpa_acc_no = row["HPA_ACC_NO"] == DBNull.Value ? string.Empty : row["HPA_ACC_NO"].ToString(),
                Hpa_af_val = row["HPA_AF_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_AF_VAL"]),
                Hpa_bank = row["HPA_BANK"] == DBNull.Value ? string.Empty : row["HPA_BANK"].ToString(),
                Hpa_buy_val = row["HPA_BUY_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_BUY_VAL"]),
                Hpa_cash_val = row["HPA_CASH_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_CASH_VAL"]),
                Hpa_cls_dt = row["HPA_CLS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPA_CLS_DT"]),
                Hpa_cre_by = row["HPA_CRE_BY"] == DBNull.Value ? string.Empty : row["HPA_CRE_BY"].ToString(),
                Hpa_cre_dt = row["HPA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPA_CRE_DT"]),
                Hpa_dp_comm = row["HPA_DP_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_DP_COMM"]),
                Hpa_dp_val = row["HPA_DP_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_DP_VAL"]),
                Hpa_ecd_stus = row["HPA_ECD_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["HPA_ECD_STUS"]),
                Hpa_ecd_tp = row["HPA_ECD_TP"] == DBNull.Value ? string.Empty : row["HPA_ECD_TP"].ToString(),
                Hpa_flag = row["HPA_FLAG"] == DBNull.Value ? string.Empty : row["HPA_FLAG"].ToString(),
                Hpa_grup_cd = row["HPA_GRUP_CD"] == DBNull.Value ? string.Empty : row["HPA_GRUP_CD"].ToString(),
                Hpa_hp_val = row["HPA_HP_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_HP_VAL"]),
                Hpa_init_ins = row["HPA_INIT_INS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INIT_INS"]),
                Hpa_init_ser_chg = row["HPA_INIT_SER_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INIT_SER_CHG"]),
                Hpa_init_stm = row["HPA_INIT_STM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INIT_STM"]),
                Hpa_init_vat = row["HPA_INIT_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INIT_VAT"]),
                Hpa_inst_comm = row["HPA_INST_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INST_COMM"]),
                Hpa_inst_ins = row["HPA_INST_INS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INST_INS"]),
                Hpa_inst_ser_chg = row["HPA_INST_SER_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INST_SER_CHG"]),
                Hpa_inst_stm = row["HPA_INST_STM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INST_STM"]),
                Hpa_inst_vat = row["HPA_INST_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INST_VAT"]),
                Hpa_intr_rt = row["HPA_INTR_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_INTR_RT"]),
                Hpa_invc_no = row["HPA_INVC_NO"] == DBNull.Value ? string.Empty : row["HPA_INVC_NO"].ToString(),
                Hpa_is_rsch = row["HPA_IS_RSCH"] == DBNull.Value ? false : Convert.ToBoolean(row["HPA_IS_RSCH"]),
                Hpa_pc = row["HPA_PC"] == DBNull.Value ? string.Empty : row["HPA_PC"].ToString(),
                Hpa_mgr_cd = row["HPA_MGR_CD"] == DBNull.Value ? string.Empty : row["HPA_MGR_CD"].ToString(),
                Hpa_net_val = row["HPA_NET_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_NET_VAL"]),
                Hpa_oth_chg = row["HPA_OTH_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_OTH_CHG"]),
                Hpa_prt_ack = row["HPA_PRT_ACK"] == DBNull.Value ? false : Convert.ToBoolean(row["HPA_PRT_ACK"]),
                Hpa_rls_dt = row["HPA_RLS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPA_RLS_DT"]),
                Hpa_rsch_dt = row["HPA_RSCH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPA_RSCH_DT"]),
                Hpa_rv_dt = row["HPA_RV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPA_RV_DT"]),
                Hpa_sch_cd = row["HPA_SCH_CD"] == DBNull.Value ? string.Empty : row["HPA_SCH_CD"].ToString(),
                Hpa_sch_tp = row["HPA_SCH_TP"] == DBNull.Value ? string.Empty : row["HPA_SCH_TP"].ToString(),
                Hpa_seq = row["HPA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPA_SEQ"]),
                Hpa_ser_chg = row["HPA_SER_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_SER_CHG"]),
                Hpa_stus = row["HPA_STUS"] == DBNull.Value ? string.Empty : row["HPA_STUS"].ToString(),
                Hpa_tc_val = row["HPA_TC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_TC_VAL"]),
                Hpa_term = row["HPA_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPA_TERM"]),
                Hpa_tot_intr = row["HPA_TOT_INTR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_TOT_INTR"]),
                Hpa_tot_vat = row["HPA_TOT_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPA_TOT_VAT"]),
                Hpa_val_01 = row["HPA_VAL_01"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPA_VAL_01"]),
                Hpa_val_02 = row["HPA_VAL_02"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPA_VAL_02"]),
                Hpa_val_03 = row["HPA_VAL_03"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPA_VAL_03"]),
                Hpa_val_04 = row["HPA_VAL_04"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPA_VAL_04"]),
                Hpa_val_05 = row["HPA_VAL_05"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPA_VAL_05"]),
                Hpa_seq_no = row["HPA_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPA_SEQ_NO"]),
                Hpa_com = row["HPA_COM"] == DBNull.Value ? string.Empty : row["HPA_COM"].ToString()
            };
        }
    }//end of class
}
