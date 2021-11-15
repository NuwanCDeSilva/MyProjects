using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class hpt_arr_acc
    {
        public Int32 Haa_seq { get; set; } 
        public String Haa_com { get; set; }
        public String Haa_pc { get; set; }
        public String Haa_pc_cat { get; set; }
        public String Haa_mng_cd { get; set; }
        public DateTime Haa_date { get; set; }
        public DateTime Haa_suppl_dt { get; set; }
        public Decimal Haa_tot_clos_bal { get; set; }
        public Decimal Haa_act_arr_amt { get; set; }
        public Decimal Haa_tot_arr_amt { get; set; }
        public Int32 Haa_tot_no_of_acc { get; set; }
        public Int32 Haa_tot_no_of_act_acc { get; set; }
        public Int32 Haa_peri_ovr_acc { get; set; }
        public Int32 Haa_tot_no_of_arr_acc { get; set; }
        public Decimal Haa_currnt_month_due { get; set; }
        public Int32 Haa_proce_complt { get; set; }
        public String Haa_anal1 { get; set; }
        public String Haa_anal2 { get; set; }
        public String Haa_anal3 { get; set; }
        public DateTime Haa_cre_dt { get; set; }
        public String Haa_cre_by { get; set; }
        public DateTime Haa_mod_dt { get; set; }
        public String Haa_mod_by { get; set; }
        public string HAA_SHOP_COM_ACC { get; set; }
        public Decimal HAA_DIRIYA_ADJ { get; set; }
        public Decimal HAA_ADJ_TOT { get; set; }
        public Decimal HAA_ADJ_AMT { get; set; }
        public Decimal HAA_LOD_ADJ { get; set; }
        public Decimal HAA_SER_PROB { get; set; }
        public Decimal HAA_DISP_ADJ { get; set; }
        public Decimal HAA_OTH { get; set; }
        public decimal HAA_TOT_GRCE_SETT { get; set; }
        public decimal HAA_GRCE_SETT { get; set; }
        public decimal HAA_TOT_GRCE_SETT_ADJ { get; set; }
        public decimal HAA_GRCE_SETT_ADJ { get; set; }
        public decimal HAA_GRCE_PER_COLL { get; set; }
        public DateTime HAA_GRCE_DATE { get; set; }
        public decimal HAA_CURR_DUE_TOT {get;set;}
        public decimal  HAA_ADJ_DUE_TOT {get;set;}
        public decimal HAA_PREV_ARR_TOT{get;set;}
        public decimal HAA_ADJ_PREV_TOTARR{get;set;}
        public decimal HAA_ALL_DUE_TOT{get;set;}
        public decimal HAA_ADJ_CURR_ARR {get;set;}
        public decimal HAA_ADJ_GRA_PER_SETT{get;set;}
        public decimal HAA_SHORT_REMITT{get;set;}
        public decimal HAA_ARR_SCH_ADJ{get;set;}
        public decimal HAA_SHOP_COM_ADJ{get;set;}
        public decimal HAA_ISSUE_CHQ_RTN_ADJ{get;set;}
        public decimal HAA_TOT_REMITT{get;set;}
        public decimal HAA_ADJ_REMITT { get; set; }
        public decimal HAA_SUPP_COLL { get; set; }
        public decimal HAA_SUPP_COLL_ADJ { get; set; }
        public decimal HAA_ADJ_GRA_PER { get; set; }
        public decimal HAA_PREV_GRACE_PER_COLL { get; set; }
        public decimal HAA_ADJ_PRE_GR_PER_COLL { get; set; }
        public decimal HAA_PRE_MON_SUP_COLL { get; set; }
        public decimal HAA_ADJ_PR_MO_SUP_COLL { get; set; }
        public decimal HAA_NET_REMIT { get; set; }
        public decimal HAA_TOT_REC_BAL { get; set; }
        public decimal HAA_DISREG_AMT { get; set; }
        public decimal HAA_GRC_PER_NT_QL { get; set; }
        public decimal HAA_ALL_TOT_ADJ { get; set; }
        public decimal HAA_ADJESMENT { get; set; }
        public decimal HAA_NET_ARR_AMT { get; set; }
        public decimal HAA_ARRE_PER { get; set; }
        public decimal HAA_BONUS_RT { get; set; }
        public decimal HAA_BONUS_AMT { get; set; }
        public decimal HAA_BONUS_REF_DED { get; set; }
        public decimal HAA_GRO_COLL_BONUS { get; set; }
        public decimal HAA_EPF_RT { get; set; }
        public decimal HAA_ESD_RT { get; set; }
        public decimal HAA_TOT_NET_BONUS { get; set; }
        public Int32 HAA_TAG_ACCT { get; set; }
        public decimal HAA_TAG_PER { get; set; }
        public decimal HAA_HAND_OVER { get; set; }
        public decimal HAA_ARR_RELE_MONTHS { get; set; }
        public DateTime HAA_EFFECT_DT { get; set; }
        public decimal HAA_ORIG_GRACE_AMT { get; set; }
        public decimal HAA_ACC_DEPT_REFUND { get; set; }
        public string HAA_ACC_DEPT_REFUREMK { get; set; }
        public decimal HAA_ACC_DEPT_DEDUC { get; set; }
        public string HAA_ACC_DEPT_DEDUCRMK { get; set; }
        public decimal HAA_INV_DEPT_REFUND { get; set; }
        public string HAA_INV_DEPT_REFUREMK { get; set; }
        public decimal HAA_INV_DEPT_DEDUC { get; set; }
        public string HAA_INV_DEPT_DEDUCRMK { get; set; }
        public decimal HAA_CRED_DEPT_REFUND { get; set; }
        public string HAA_CRED_DEPT_REFUREMK { get; set; }
        public decimal HAA_CRED_DEPT_DEDUC { get; set; }
        public string HAA_CRED_DEPT_DEDUCRMK { get; set; }
        public decimal HAA_GRC_PRD_COL_ADJ { get; set; }
        public decimal PrevHand { get; set; }
        public int ActiveStatus { get; set; }
        public decimal oldnetbonus { get; set; }
        public string HAA_SPC_RMK { get; set; }
        public decimal HAA_SPC_VAL { get; set; }

        public decimal Years { get; set; }
        public string Pccat { get; set; }
        public DateTime bonusstartdate { get; set; }
        public DateTime mangercreadate { get; set; }

        public static hpt_arr_acc Converter(DataRow row)
        {
            return new hpt_arr_acc
            {
                Haa_seq = row["HAA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_SEQ"].ToString()),
                Haa_com = row["HAA_COM"] == DBNull.Value ? string.Empty : row["HAA_COM"].ToString(),
                Haa_pc = row["HAA_PC"] == DBNull.Value ? string.Empty : row["HAA_PC"].ToString(),
                Haa_pc_cat = row["HAA_PC_CAT"] == DBNull.Value ? string.Empty : row["HAA_PC_CAT"].ToString(),
                Haa_mng_cd = row["HAA_MNG_CD"] == DBNull.Value ? string.Empty : row["HAA_MNG_CD"].ToString(),
                Haa_date = row["HAA_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_DATE"].ToString()),
                Haa_suppl_dt = row["HAA_SUPPL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_SUPPL_DT"].ToString()),
                Haa_tot_clos_bal = row["HAA_TOT_CLOS_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_CLOS_BAL"].ToString()),
                Haa_act_arr_amt = row["HAA_ACT_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ACT_ARR_AMT"].ToString()),
                Haa_tot_arr_amt = row["HAA_TOT_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_ARR_AMT"].ToString()),
                Haa_tot_no_of_acc = row["HAA_TOT_NO_OF_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_TOT_NO_OF_ACC"].ToString()),
                Haa_tot_no_of_act_acc = row["HAA_TOT_NO_OF_ACT_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_TOT_NO_OF_ACT_ACC"].ToString()),
                Haa_peri_ovr_acc = row["HAA_PERI_OVR_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_PERI_OVR_ACC"].ToString()),
                Haa_tot_no_of_arr_acc = row["HAA_TOT_NO_OF_ARR_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_TOT_NO_OF_ARR_ACC"].ToString()),
                Haa_currnt_month_due = row["HAA_CURRNT_MONTH_DUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_CURRNT_MONTH_DUE"].ToString()),
                Haa_proce_complt = row["HAA_PROCE_COMPLT"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_PROCE_COMPLT"].ToString()),
                Haa_anal1 = row["HAA_ANAL1"] == DBNull.Value ? string.Empty : row["HAA_ANAL1"].ToString(),
                Haa_anal2 = row["HAA_ANAL2"] == DBNull.Value ? string.Empty : row["HAA_ANAL2"].ToString(),
                Haa_anal3 = row["HAA_ANAL3"] == DBNull.Value ? string.Empty : row["HAA_ANAL3"].ToString(),
                Haa_cre_dt = row["HAA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_CRE_DT"].ToString()),
                Haa_cre_by = row["HAA_CRE_BY"] == DBNull.Value ? string.Empty : row["HAA_CRE_BY"].ToString(),
                Haa_mod_dt = row["HAA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_MOD_DT"].ToString()),
                Haa_mod_by = row["HAA_MOD_BY"] == DBNull.Value ? string.Empty : row["HAA_MOD_BY"].ToString(),
                HAA_SHOP_COM_ACC = row["HAA_SHOP_COM_ACC"] == DBNull.Value ? string.Empty : row["HAA_SHOP_COM_ACC"].ToString(),
                HAA_DIRIYA_ADJ = row["HAA_DIRIYA_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_DIRIYA_ADJ"].ToString()),
                HAA_ADJ_TOT = row["HAA_ADJ_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_TOT"].ToString()),
                HAA_ADJ_AMT = row["HAA_ADJ_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_AMT"].ToString()),
                HAA_LOD_ADJ = row["HAA_LOD_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_LOD_ADJ"].ToString()),
                HAA_SER_PROB = row["HAA_SER_PROB"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SER_PROB"].ToString()),
                HAA_DISP_ADJ = row["HAA_DISP_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_DISP_ADJ"].ToString()),
                HAA_OTH = row["HAA_OTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_OTH"].ToString()),
                HAA_TOT_GRCE_SETT = row["HAA_TOT_GRCE_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_GRCE_SETT"].ToString()),
                HAA_GRCE_SETT = row["HAA_GRCE_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRCE_SETT"].ToString()),
                HAA_TOT_GRCE_SETT_ADJ = row["HAA_TOT_GRCE_SETT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_GRCE_SETT_ADJ"].ToString()),
                HAA_GRCE_SETT_ADJ = row["HAA_GRCE_SETT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRCE_SETT_ADJ"].ToString()),
                HAA_GRCE_PER_COLL = row["HAA_GRCE_PER_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRCE_PER_COLL"].ToString()),
                HAA_GRCE_DATE = row["HAA_GRCE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_GRCE_DATE"].ToString()),
                HAA_CURR_DUE_TOT = row["HAA_CURR_DUE_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_CURR_DUE_TOT"].ToString()),
                HAA_ADJ_DUE_TOT = row["HAA_ADJ_DUE_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_DUE_TOT"].ToString()),
                HAA_PREV_ARR_TOT = row["HAA_PREV_ARR_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_PREV_ARR_TOT"].ToString()),
                HAA_ADJ_PREV_TOTARR = row["HAA_ADJ_PREV_TOTARR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_PREV_TOTARR"].ToString()),
                HAA_ALL_DUE_TOT = row["HAA_ALL_DUE_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ALL_DUE_TOT"].ToString()),
                HAA_ADJ_CURR_ARR = row["HAA_ADJ_CURR_ARR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_CURR_ARR"].ToString()),
                HAA_ADJ_GRA_PER_SETT = row["HAA_ADJ_GRA_PER_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_GRA_PER_SETT"].ToString()),
                HAA_SHORT_REMITT = row["HAA_SHORT_REMITT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SHORT_REMITT"].ToString()),
                HAA_ARR_SCH_ADJ = row["HAA_ARR_SCH_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ARR_SCH_ADJ"].ToString()),
                HAA_SHOP_COM_ADJ = row["HAA_SHOP_COM_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SHOP_COM_ADJ"].ToString()),
                HAA_ISSUE_CHQ_RTN_ADJ = row["HAA_ISSUE_CHQ_RTN_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ISSUE_CHQ_RTN_ADJ"].ToString()),
                HAA_TOT_REMITT = row["HAA_TOT_REMITT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_REMITT"].ToString()),
                HAA_ADJ_REMITT = row["HAA_ADJ_REMITT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_REMITT"].ToString()),
                HAA_SUPP_COLL = row["HAA_SUPP_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SUPP_COLL"].ToString()),
                HAA_SUPP_COLL_ADJ = row["HAA_SUPP_COLL_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SUPP_COLL_ADJ"].ToString()),
                HAA_ADJ_GRA_PER = row["HAA_ADJ_GRA_PER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_GRA_PER"].ToString()),
                HAA_PREV_GRACE_PER_COLL = row["HAA_PREV_GRACE_PER_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_PREV_GRACE_PER_COLL"].ToString()),
                HAA_ADJ_PRE_GR_PER_COLL = row["HAA_ADJ_PRE_GR_PER_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_PRE_GR_PER_COLL"].ToString()),
                HAA_PRE_MON_SUP_COLL = row["HAA_PRE_MON_SUP_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_PRE_MON_SUP_COLL"].ToString()),
                HAA_ADJ_PR_MO_SUP_COLL = row["HAA_ADJ_PR_MO_SUP_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_PR_MO_SUP_COLL"].ToString()),
                HAA_NET_REMIT = row["HAA_NET_REMIT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_NET_REMIT"].ToString()),
                HAA_TOT_REC_BAL = row["HAA_TOT_REC_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_REC_BAL"].ToString()),
                HAA_DISREG_AMT = row["HAA_DISREG_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_DISREG_AMT"].ToString()),
                HAA_GRC_PER_NT_QL = row["HAA_GRC_PER_NT_QL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRC_PER_NT_QL"].ToString()),
                HAA_ALL_TOT_ADJ = row["HAA_ALL_TOT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ALL_TOT_ADJ"].ToString()),
                HAA_ADJESMENT = row["HAA_ADJESMENT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJESMENT"].ToString()),
                HAA_NET_ARR_AMT = row["HAA_NET_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_NET_ARR_AMT"].ToString()),
                HAA_ARRE_PER = row["HAA_ARRE_PER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ARRE_PER"].ToString()),
                HAA_BONUS_RT = row["HAA_BONUS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_BONUS_RT"].ToString()),
                HAA_BONUS_AMT = row["HAA_BONUS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_BONUS_AMT"].ToString()),
                HAA_BONUS_REF_DED = row["HAA_BONUS_REF_DED"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_BONUS_REF_DED"].ToString()),
                HAA_GRO_COLL_BONUS = row["HAA_GRO_COLL_BONUS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRO_COLL_BONUS"].ToString()),
                HAA_EPF_RT = row["HAA_EPF_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_EPF_RT"].ToString()),
                HAA_ESD_RT = row["HAA_ESD_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ESD_RT"].ToString()),
                HAA_TOT_NET_BONUS = row["HAA_TOT_NET_BONUS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_NET_BONUS"].ToString()),
                HAA_HAND_OVER = row["HAA_HAND_OVER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_HAND_OVER"].ToString()),
                HAA_ARR_RELE_MONTHS = row["HAA_ARR_RELE_MONTHS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ARR_RELE_MONTHS"].ToString()),
                HAA_EFFECT_DT = row["HAA_EFFECT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_EFFECT_DT"].ToString()),
                HAA_ORIG_GRACE_AMT = row["HAA_ORIG_GRACE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ORIG_GRACE_AMT"].ToString()),
                HAA_GRC_PRD_COL_ADJ = row["HAA_GRC_PRD_COL_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRC_PRD_COL_ADJ"].ToString()),
                HAA_ACC_DEPT_REFUND = row["HAA_ACC_DEPT_REFUND"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ACC_DEPT_REFUND"].ToString()),
                HAA_ACC_DEPT_REFUREMK = row["HAA_ACC_DEPT_REFUREMK"] == DBNull.Value ? string.Empty : row["HAA_ACC_DEPT_REFUREMK"].ToString(),
                HAA_ACC_DEPT_DEDUC = row["HAA_ACC_DEPT_DEDUC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ACC_DEPT_DEDUC"].ToString()),
                HAA_ACC_DEPT_DEDUCRMK = row["HAA_ACC_DEPT_DEDUCRMK"] == DBNull.Value ? string.Empty : row["HAA_ACC_DEPT_DEDUCRMK"].ToString(),
                HAA_INV_DEPT_REFUND = row["HAA_INV_DEPT_REFUND"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_INV_DEPT_REFUND"].ToString()),
                HAA_INV_DEPT_REFUREMK = row["HAA_INV_DEPT_REFUREMK"] == DBNull.Value ? string.Empty : row["HAA_INV_DEPT_REFUREMK"].ToString(),
                HAA_INV_DEPT_DEDUC = row["HAA_INV_DEPT_DEDUC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_INV_DEPT_DEDUC"].ToString()),
                HAA_INV_DEPT_DEDUCRMK = row["HAA_INV_DEPT_DEDUCRMK"] == DBNull.Value ? string.Empty : row["HAA_INV_DEPT_DEDUCRMK"].ToString(),
                HAA_CRED_DEPT_REFUND = row["HAA_CRED_DEPT_REFUND"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_CRED_DEPT_REFUND"].ToString()),
                HAA_CRED_DEPT_REFUREMK = row["HAA_CRED_DEPT_REFUREMK"] == DBNull.Value ? string.Empty : row["HAA_CRED_DEPT_REFUREMK"].ToString(),
                HAA_CRED_DEPT_DEDUC = row["HAA_CRED_DEPT_DEDUC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_CRED_DEPT_DEDUC"].ToString()),
                HAA_CRED_DEPT_DEDUCRMK = row["HAA_CRED_DEPT_DEDUCRMK"] == DBNull.Value ? string.Empty : row["HAA_CRED_DEPT_DEDUCRMK"].ToString(),
                HAA_SPC_RMK = row["HAA_SPC_RMK"] == DBNull.Value ? string.Empty : row["HAA_SPC_RMK"].ToString(),
                HAA_SPC_VAL = row["HAA_SPC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SPC_VAL"].ToString()),
            };
        }
        public static hpt_arr_acc Converter2(DataRow row)
        {
            return new hpt_arr_acc
            {
                Haa_seq = row["HAA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_SEQ"].ToString()),
                Haa_com = row["HAA_COM"] == DBNull.Value ? string.Empty : row["HAA_COM"].ToString(),
                Haa_pc = row["HAA_PC"] == DBNull.Value ? string.Empty : row["HAA_PC"].ToString(),
                Haa_pc_cat = row["HAA_PC_CAT"] == DBNull.Value ? string.Empty : row["HAA_PC_CAT"].ToString(),
                Haa_mng_cd = row["HAA_MNG_CD"] == DBNull.Value ? string.Empty : row["HAA_MNG_CD"].ToString(),
                Haa_date = row["HAA_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_DATE"].ToString()),
                Haa_suppl_dt = row["HAA_SUPPL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_SUPPL_DT"].ToString()),
                Haa_tot_clos_bal = row["HAA_TOT_CLOS_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_CLOS_BAL"].ToString()),
                Haa_act_arr_amt = row["HAA_ACT_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ACT_ARR_AMT"].ToString()),
                Haa_tot_arr_amt = row["HAA_TOT_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_ARR_AMT"].ToString()),
                Haa_tot_no_of_acc = row["HAA_TOT_NO_OF_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_TOT_NO_OF_ACC"].ToString()),
                Haa_tot_no_of_act_acc = row["HAA_TOT_NO_OF_ACT_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_TOT_NO_OF_ACT_ACC"].ToString()),
                Haa_peri_ovr_acc = row["HAA_PERI_OVR_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_PERI_OVR_ACC"].ToString()),
                Haa_tot_no_of_arr_acc = row["HAA_TOT_NO_OF_ARR_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_TOT_NO_OF_ARR_ACC"].ToString()),
                Haa_currnt_month_due = row["HAA_CURRNT_MONTH_DUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_CURRNT_MONTH_DUE"].ToString()),
                Haa_proce_complt = row["HAA_PROCE_COMPLT"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAA_PROCE_COMPLT"].ToString()),
                Haa_anal1 = row["HAA_ANAL1"] == DBNull.Value ? string.Empty : row["HAA_ANAL1"].ToString(),
                Haa_anal2 = row["HAA_ANAL2"] == DBNull.Value ? string.Empty : row["HAA_ANAL2"].ToString(),
                Haa_anal3 = row["HAA_ANAL3"] == DBNull.Value ? string.Empty : row["HAA_ANAL3"].ToString(),
                Haa_cre_dt = row["HAA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_CRE_DT"].ToString()),
                Haa_cre_by = row["HAA_CRE_BY"] == DBNull.Value ? string.Empty : row["HAA_CRE_BY"].ToString(),
                Haa_mod_dt = row["HAA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_MOD_DT"].ToString()),
                Haa_mod_by = row["HAA_MOD_BY"] == DBNull.Value ? string.Empty : row["HAA_MOD_BY"].ToString(),
                HAA_SHOP_COM_ACC = row["HAA_SHOP_COM_ACC"] == DBNull.Value ? string.Empty : row["HAA_SHOP_COM_ACC"].ToString(),
                HAA_DIRIYA_ADJ = row["HAA_DIRIYA_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_DIRIYA_ADJ"].ToString()),
                HAA_ADJ_TOT = row["HAA_ADJ_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_TOT"].ToString()),
                HAA_ADJ_AMT = row["HAA_ADJ_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_AMT"].ToString()),
                HAA_LOD_ADJ = row["HAA_LOD_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_LOD_ADJ"].ToString()),
                HAA_SER_PROB = row["HAA_SER_PROB"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SER_PROB"].ToString()),
                HAA_DISP_ADJ = row["HAA_DISP_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_DISP_ADJ"].ToString()),
                HAA_OTH = row["HAA_OTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_OTH"].ToString()),
                HAA_TOT_GRCE_SETT = row["HAA_TOT_GRCE_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_GRCE_SETT"].ToString()),
                HAA_GRCE_SETT = row["HAA_GRCE_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRCE_SETT"].ToString()),
                HAA_TOT_GRCE_SETT_ADJ = row["HAA_TOT_GRCE_SETT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_GRCE_SETT_ADJ"].ToString()),
                HAA_GRCE_SETT_ADJ = row["HAA_GRCE_SETT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRCE_SETT_ADJ"].ToString()),
                HAA_GRCE_PER_COLL = row["HAA_GRCE_PER_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRCE_PER_COLL"].ToString()),
                HAA_GRCE_DATE = row["HAA_GRCE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_GRCE_DATE"].ToString()),
                HAA_CURR_DUE_TOT = row["HAA_CURR_DUE_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_CURR_DUE_TOT"].ToString()),
                HAA_ADJ_DUE_TOT = row["HAA_ADJ_DUE_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_DUE_TOT"].ToString()),
                HAA_PREV_ARR_TOT = row["HAA_PREV_ARR_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_PREV_ARR_TOT"].ToString()),
                HAA_ADJ_PREV_TOTARR = row["HAA_ADJ_PREV_TOTARR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_PREV_TOTARR"].ToString()),
                HAA_ALL_DUE_TOT = row["HAA_ALL_DUE_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ALL_DUE_TOT"].ToString()),
                HAA_ADJ_CURR_ARR = row["HAA_ADJ_CURR_ARR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_CURR_ARR"].ToString()),
                HAA_ADJ_GRA_PER_SETT = row["HAA_ADJ_GRA_PER_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_GRA_PER_SETT"].ToString()),
                HAA_SHORT_REMITT = row["HAA_SHORT_REMITT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SHORT_REMITT"].ToString()),
                HAA_ARR_SCH_ADJ = row["HAA_ARR_SCH_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ARR_SCH_ADJ"].ToString()),
                HAA_SHOP_COM_ADJ = row["HAA_SHOP_COM_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SHOP_COM_ADJ"].ToString()),
                HAA_ISSUE_CHQ_RTN_ADJ = row["HAA_ISSUE_CHQ_RTN_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ISSUE_CHQ_RTN_ADJ"].ToString()),
                HAA_TOT_REMITT = row["HAA_TOT_REMITT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_REMITT"].ToString()),
                HAA_ADJ_REMITT = row["HAA_ADJ_REMITT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_REMITT"].ToString()),
                HAA_SUPP_COLL = row["HAA_SUPP_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SUPP_COLL"].ToString()),
                HAA_SUPP_COLL_ADJ = row["HAA_SUPP_COLL_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SUPP_COLL_ADJ"].ToString()),
                HAA_ADJ_GRA_PER = row["HAA_ADJ_GRA_PER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_GRA_PER"].ToString()),
                HAA_PREV_GRACE_PER_COLL = row["HAA_PREV_GRACE_PER_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_PREV_GRACE_PER_COLL"].ToString()),
                HAA_ADJ_PRE_GR_PER_COLL = row["HAA_ADJ_PRE_GR_PER_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_PRE_GR_PER_COLL"].ToString()),
                HAA_PRE_MON_SUP_COLL = row["HAA_PRE_MON_SUP_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_PRE_MON_SUP_COLL"].ToString()),
                HAA_ADJ_PR_MO_SUP_COLL = row["HAA_ADJ_PR_MO_SUP_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJ_PR_MO_SUP_COLL"].ToString()),
                HAA_NET_REMIT = row["HAA_NET_REMIT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_NET_REMIT"].ToString()),
                HAA_TOT_REC_BAL = row["HAA_TOT_REC_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_REC_BAL"].ToString()),
                HAA_DISREG_AMT = row["HAA_DISREG_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_DISREG_AMT"].ToString()),
                HAA_GRC_PER_NT_QL = row["HAA_GRC_PER_NT_QL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRC_PER_NT_QL"].ToString()),
                HAA_ALL_TOT_ADJ = row["HAA_ALL_TOT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ALL_TOT_ADJ"].ToString()),
                HAA_ADJESMENT = row["HAA_ADJESMENT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ADJESMENT"].ToString()),
                HAA_NET_ARR_AMT = row["HAA_NET_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_NET_ARR_AMT"].ToString()),
                HAA_ARRE_PER = row["HAA_ARRE_PER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ARRE_PER"].ToString()),
                HAA_BONUS_RT = row["HAA_BONUS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_BONUS_RT"].ToString()),
                HAA_BONUS_AMT = row["HAA_BONUS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_BONUS_AMT"].ToString()),
                HAA_BONUS_REF_DED = row["HAA_BONUS_REF_DED"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_BONUS_REF_DED"].ToString()),
                HAA_GRO_COLL_BONUS = row["HAA_GRO_COLL_BONUS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRO_COLL_BONUS"].ToString()),
                HAA_EPF_RT = row["HAA_EPF_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_EPF_RT"].ToString()),
                HAA_ESD_RT = row["HAA_ESD_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ESD_RT"].ToString()),
                HAA_TOT_NET_BONUS = row["HAA_TOT_NET_BONUS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_TOT_NET_BONUS"].ToString()),
                HAA_HAND_OVER = row["HAA_HAND_OVER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_HAND_OVER"].ToString()),
                HAA_ARR_RELE_MONTHS = row["HAA_ARR_RELE_MONTHS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ARR_RELE_MONTHS"].ToString()),
                PrevHand = row["PrevHand"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PrevHand"].ToString()),
                HAA_EFFECT_DT = row["HAA_EFFECT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAA_EFFECT_DT"].ToString()),
                HAA_ORIG_GRACE_AMT = row["HAA_ORIG_GRACE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ORIG_GRACE_AMT"].ToString()),
                HAA_GRC_PRD_COL_ADJ = row["HAA_GRC_PRD_COL_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_GRC_PRD_COL_ADJ"].ToString()),
                HAA_ACC_DEPT_REFUND = row["HAA_ACC_DEPT_REFUND"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ACC_DEPT_REFUND"].ToString()),
                HAA_ACC_DEPT_REFUREMK = row["HAA_ACC_DEPT_REFUREMK"] == DBNull.Value ? string.Empty : row["HAA_ACC_DEPT_REFUREMK"].ToString(),
                HAA_ACC_DEPT_DEDUC = row["HAA_ACC_DEPT_DEDUC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_ACC_DEPT_DEDUC"].ToString()),
                HAA_ACC_DEPT_DEDUCRMK = row["HAA_ACC_DEPT_DEDUCRMK"] == DBNull.Value ? string.Empty : row["HAA_ACC_DEPT_DEDUCRMK"].ToString(),
                HAA_INV_DEPT_REFUND = row["HAA_INV_DEPT_REFUND"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_INV_DEPT_REFUND"].ToString()),
                HAA_INV_DEPT_REFUREMK = row["HAA_INV_DEPT_REFUREMK"] == DBNull.Value ? string.Empty : row["HAA_INV_DEPT_REFUREMK"].ToString(),
                HAA_INV_DEPT_DEDUC = row["HAA_INV_DEPT_DEDUC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_INV_DEPT_DEDUC"].ToString()),
                HAA_INV_DEPT_DEDUCRMK = row["HAA_INV_DEPT_DEDUCRMK"] == DBNull.Value ? string.Empty : row["HAA_INV_DEPT_DEDUCRMK"].ToString(),
                HAA_CRED_DEPT_REFUND = row["HAA_CRED_DEPT_REFUND"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_CRED_DEPT_REFUND"].ToString()),
                HAA_CRED_DEPT_REFUREMK = row["HAA_CRED_DEPT_REFUREMK"] == DBNull.Value ? string.Empty : row["HAA_CRED_DEPT_REFUREMK"].ToString(),
                HAA_CRED_DEPT_DEDUC = row["HAA_CRED_DEPT_DEDUC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_CRED_DEPT_DEDUC"].ToString()),
                HAA_CRED_DEPT_DEDUCRMK = row["HAA_CRED_DEPT_DEDUCRMK"] == DBNull.Value ? string.Empty : row["HAA_CRED_DEPT_DEDUCRMK"].ToString(),
                HAA_SPC_RMK = row["HAA_SPC_RMK"] == DBNull.Value ? string.Empty : row["HAA_SPC_RMK"].ToString(),
                HAA_SPC_VAL = row["HAA_SPC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAA_SPC_VAL"].ToString()),
            };
        }
    }
}
