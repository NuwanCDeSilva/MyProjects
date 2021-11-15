using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class hpt_arr_acc_det
    {
        public Int32 Haad_seq { get; set; }
        public String Haad_com { get; set; }
        public String Haad_pc { get; set; }
        public String Haad_pc_cat { get; set; }
        public String Haad_acc_cd { get; set; }
        public String Haad_mng_cd { get; set; }
        public DateTime Haad_date { get; set; }
        public DateTime Haad_suppl_dt { get; set; }
        public Decimal Haad_tot_clos_bal { get; set; }
        public Decimal Haad_act_arr_amt { get; set; }
        public Decimal Haad_tot_arr_amt { get; set; }
        public Int32 Haad_tot_no_of_acc { get; set; }
        public Int32 Haad_tot_no_of_act_acc { get; set; }
        public Int32 Haad_peri_ovr_acc { get; set; }
        public Int32 Haad_tot_no_of_arr_acc { get; set; }
        public Decimal Haad_currnt_month_due { get; set; }
        public Int32 Haad_proce_complt { get; set; }
        public String Haad_anal1 { get; set; }
        public String Haad_anal2 { get; set; }
        public String Haad_anal3 { get; set; }
        public DateTime Haad_cre_dt { get; set; }
        public String Haad_cre_by { get; set; }
        public DateTime Haad_mod_dt { get; set; }
        public String Haad_mod_by { get; set; }
        public string HAAD_SHOP_COM_ACC { get; set; }
        public Decimal HAAD_DIRIYA_ADJ { get; set; }
        public Decimal HAAD_ADJ_TOT { get; set; }
        public Decimal HAAD_ADJ_AMT { get; set; }
        public Decimal HAAD_LOD_ADJ { get; set; }
        public Decimal HAAD_SER_PROB { get; set; }
        public Decimal HAAD_DISP_ADJ { get; set; }
        public Decimal HAAD_OTH { get; set; }
        public decimal HAAD_TOT_GRCE_SETT { get; set; }
        public decimal HAAD_GRCE_SETT { get; set; }
        public decimal HAAD_TOT_GRCE_SETT_ADJ { get; set; }
        public decimal HAAD_GRCE_SETT_ADJ { get; set; }
        public decimal HAAD_GRCE_PER_COLL { get; set; }
        public DateTime HAAD_GRCE_DATE { get; set; }
        public decimal HAAD_HAND_OVER { get; set; }
        public decimal HAAD_ARR_RELE_MONTHS { get; set; }
        public decimal HAAD_SHOP_COM_ADJ { get; set; }
        public string HAAD_REMARK { get; set; }
        public string HAAD_SCHEME { get; set; }
        public DateTime HAAD_EFFECT_DT { get; set; }
        public decimal HAAD_ORIG_GRACE_AMT { get; set; }
        public string Scheme { get; set; }
        public int ActStatus { get; set; }

        public decimal handoverreject { get; set; }


        public static hpt_arr_acc_det Converter(DataRow row)
        {
            return new hpt_arr_acc_det
            {
                Haad_seq = row["HAAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_SEQ"].ToString()),
                Haad_com = row["HAAD_COM"] == DBNull.Value ? string.Empty : row["HAAD_COM"].ToString(),
                Haad_pc = row["HAAD_PC"] == DBNull.Value ? string.Empty : row["HAAD_PC"].ToString(),
                Haad_pc_cat = row["HAAD_PC_CAT"] == DBNull.Value ? string.Empty : row["HAAD_PC_CAT"].ToString(),
                Haad_acc_cd = row["HAAD_ACC_CD"] == DBNull.Value ? string.Empty : row["HAAD_ACC_CD"].ToString(),
                Haad_mng_cd = row["HAAD_MNG_CD"] == DBNull.Value ? string.Empty : row["HAAD_MNG_CD"].ToString(),
                Haad_date = row["HAAD_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_DATE"].ToString()),
                Haad_suppl_dt = row["HAAD_SUPPL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_SUPPL_DT"].ToString()),
                Haad_tot_clos_bal = row["HAAD_TOT_CLOS_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_TOT_CLOS_BAL"].ToString()),
                Haad_act_arr_amt = row["HAAD_ACT_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_ACT_ARR_AMT"].ToString()),
                Haad_tot_arr_amt = row["HAAD_TOT_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_TOT_ARR_AMT"].ToString()),
                Haad_tot_no_of_acc = row["HAAD_TOT_NO_OF_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_TOT_NO_OF_ACC"].ToString()),
                Haad_tot_no_of_act_acc = row["HAAD_TOT_NO_OF_ACT_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_TOT_NO_OF_ACT_ACC"].ToString()),
                Haad_peri_ovr_acc = row["HAAD_PERI_OVR_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_PERI_OVR_ACC"].ToString()),
                Haad_tot_no_of_arr_acc = row["HAAD_TOT_NO_OF_ARR_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_TOT_NO_OF_ARR_ACC"].ToString()),
                Haad_currnt_month_due = row["HAAD_CURRNT_MONTH_DUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_CURRNT_MONTH_DUE"].ToString()),
                Haad_proce_complt = row["HAAD_PROCE_COMPLT"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_PROCE_COMPLT"].ToString()),
                Haad_anal1 = row["HAAD_ANAL1"] == DBNull.Value ? string.Empty : row["HAAD_ANAL1"].ToString(),
                Haad_anal2 = row["HAAD_ANAL2"] == DBNull.Value ? string.Empty : row["HAAD_ANAL2"].ToString(),
                Haad_anal3 = row["HAAD_ANAL3"] == DBNull.Value ? string.Empty : row["HAAD_ANAL3"].ToString(),
                Haad_cre_dt = row["HAAD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_CRE_DT"].ToString()),
                Haad_cre_by = row["HAAD_CRE_BY"] == DBNull.Value ? string.Empty : row["HAAD_CRE_BY"].ToString(),
                Haad_mod_dt = row["HAAD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_MOD_DT"].ToString()),
                Haad_mod_by = row["HAAD_MOD_BY"] == DBNull.Value ? string.Empty : row["HAAD_MOD_BY"].ToString(),
                HAAD_SHOP_COM_ACC = row["HAAD_SHOP_COM_ACC"] == DBNull.Value ? string.Empty : row["HAAD_SHOP_COM_ACC"].ToString(),
                HAAD_DIRIYA_ADJ = row["HAAD_DIRIYA_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_DIRIYA_ADJ"].ToString()),
                HAAD_ADJ_TOT = row["HAAD_ADJ_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_ADJ_TOT"].ToString()),
                HAAD_ADJ_AMT = row["HAAD_ADJ_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_ADJ_AMT"].ToString()),
                HAAD_LOD_ADJ = row["HAAD_LOD_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_LOD_ADJ"].ToString()),
                HAAD_SER_PROB = row["HAAD_SER_PROB"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_SER_PROB"].ToString()),
                HAAD_DISP_ADJ = row["HAAD_DISP_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_DISP_ADJ"].ToString()),
                HAAD_OTH = row["HAAD_OTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_OTH"].ToString()),
                HAAD_TOT_GRCE_SETT = row["HAAD_TOT_GRCE_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_TOT_GRCE_SETT"].ToString()),
                HAAD_GRCE_SETT = row["HAAD_GRCE_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_GRCE_SETT"].ToString()),
                HAAD_TOT_GRCE_SETT_ADJ = row["HAAD_TOT_GRCE_SETT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_TOT_GRCE_SETT_ADJ"].ToString()),
                HAAD_GRCE_SETT_ADJ = row["HAAD_GRCE_SETT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_GRCE_SETT_ADJ"].ToString()),
                HAAD_GRCE_PER_COLL = row["HAAD_GRCE_PER_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_GRCE_PER_COLL"].ToString()),
                HAAD_GRCE_DATE = row["HAAD_GRCE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_GRCE_DATE"].ToString()),
                HAAD_HAND_OVER = row["HAAD_HAND_OVER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_HAND_OVER"].ToString()),
                HAAD_ARR_RELE_MONTHS = row["HAAD_ARR_RELE_MONTHS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_ARR_RELE_MONTHS"].ToString()),
                HAAD_SHOP_COM_ADJ = row["HAAD_SHOP_COM_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_SHOP_COM_ADJ"].ToString()),
                HAAD_REMARK = row["HAAD_REMARK"] == DBNull.Value ? string.Empty : row["HAAD_REMARK"].ToString(),
                HAAD_SCHEME = row["HAAD_SCHEME"] == DBNull.Value ? string.Empty : row["HAAD_SCHEME"].ToString(),
            };
        }
        public static hpt_arr_acc_det Converter2(DataRow row)
        {
            return new hpt_arr_acc_det
            {
                Haad_seq = row["HAAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_SEQ"].ToString()),
                Haad_com = row["HAAD_COM"] == DBNull.Value ? string.Empty : row["HAAD_COM"].ToString(),
                Haad_pc = row["HAAD_PC"] == DBNull.Value ? string.Empty : row["HAAD_PC"].ToString(),
                Haad_pc_cat = row["HAAD_PC_CAT"] == DBNull.Value ? string.Empty : row["HAAD_PC_CAT"].ToString(),
                Haad_acc_cd = row["HAAD_ACC_CD"] == DBNull.Value ? string.Empty : row["HAAD_ACC_CD"].ToString(),
                Haad_mng_cd = row["HAAD_MNG_CD"] == DBNull.Value ? string.Empty : row["HAAD_MNG_CD"].ToString(),
                Haad_date = row["HAAD_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_DATE"].ToString()),
                Haad_suppl_dt = row["HAAD_SUPPL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_SUPPL_DT"].ToString()),
                Haad_tot_clos_bal = row["HAAD_TOT_CLOS_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_TOT_CLOS_BAL"].ToString()),
                Haad_act_arr_amt = row["HAAD_ACT_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_ACT_ARR_AMT"].ToString()),
                Haad_tot_arr_amt = row["HAAD_TOT_ARR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_TOT_ARR_AMT"].ToString()),
                Haad_tot_no_of_acc = row["HAAD_TOT_NO_OF_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_TOT_NO_OF_ACC"].ToString()),
                Haad_tot_no_of_act_acc = row["HAAD_TOT_NO_OF_ACT_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_TOT_NO_OF_ACT_ACC"].ToString()),
                Haad_peri_ovr_acc = row["HAAD_PERI_OVR_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_PERI_OVR_ACC"].ToString()),
                Haad_tot_no_of_arr_acc = row["HAAD_TOT_NO_OF_ARR_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_TOT_NO_OF_ARR_ACC"].ToString()),
                Haad_currnt_month_due = row["HAAD_CURRNT_MONTH_DUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_CURRNT_MONTH_DUE"].ToString()),
                Haad_proce_complt = row["HAAD_PROCE_COMPLT"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAAD_PROCE_COMPLT"].ToString()),
                Haad_anal1 = row["HAAD_ANAL1"] == DBNull.Value ? string.Empty : row["HAAD_ANAL1"].ToString(),
                Haad_anal2 = row["HAAD_ANAL2"] == DBNull.Value ? string.Empty : row["HAAD_ANAL2"].ToString(),
                Haad_anal3 = row["HAAD_ANAL3"] == DBNull.Value ? string.Empty : row["HAAD_ANAL3"].ToString(),
                Haad_cre_dt = row["HAAD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_CRE_DT"].ToString()),
                Haad_cre_by = row["HAAD_CRE_BY"] == DBNull.Value ? string.Empty : row["HAAD_CRE_BY"].ToString(),
                Haad_mod_dt = row["HAAD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_MOD_DT"].ToString()),
                Haad_mod_by = row["HAAD_MOD_BY"] == DBNull.Value ? string.Empty : row["HAAD_MOD_BY"].ToString(),
                HAAD_SHOP_COM_ACC = row["HAAD_SHOP_COM_ACC"] == DBNull.Value ? string.Empty : row["HAAD_SHOP_COM_ACC"].ToString(),
                HAAD_DIRIYA_ADJ = row["HAAD_DIRIYA_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_DIRIYA_ADJ"].ToString()),
                HAAD_ADJ_TOT = row["HAAD_ADJ_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_ADJ_TOT"].ToString()),
                HAAD_ADJ_AMT = row["HAAD_ADJ_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_ADJ_AMT"].ToString()),
                HAAD_LOD_ADJ = row["HAAD_LOD_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_LOD_ADJ"].ToString()),
                HAAD_SER_PROB = row["HAAD_SER_PROB"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_SER_PROB"].ToString()),
                HAAD_DISP_ADJ = row["HAAD_DISP_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_DISP_ADJ"].ToString()),
                HAAD_OTH = row["HAAD_OTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_OTH"].ToString()),
                HAAD_TOT_GRCE_SETT = row["HAAD_TOT_GRCE_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_TOT_GRCE_SETT"].ToString()),
                HAAD_GRCE_SETT = row["HAAD_GRCE_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_GRCE_SETT"].ToString()),
                HAAD_TOT_GRCE_SETT_ADJ = row["HAAD_TOT_GRCE_SETT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_TOT_GRCE_SETT_ADJ"].ToString()),
                HAAD_GRCE_SETT_ADJ = row["HAAD_GRCE_SETT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_GRCE_SETT_ADJ"].ToString()),
                HAAD_GRCE_PER_COLL = row["HAAD_GRCE_PER_COLL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_GRCE_PER_COLL"].ToString()),
                HAAD_GRCE_DATE = row["HAAD_GRCE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_GRCE_DATE"].ToString()),
                HAAD_HAND_OVER = row["HAAD_HAND_OVER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_HAND_OVER"].ToString()),
                HAAD_ARR_RELE_MONTHS = row["HAAD_ARR_RELE_MONTHS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_ARR_RELE_MONTHS"].ToString()),
                HAAD_SHOP_COM_ADJ = row["HAAD_SHOP_COM_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_SHOP_COM_ADJ"].ToString()),
                HAAD_REMARK = row["HAAD_REMARK"] == DBNull.Value ? string.Empty : row["HAAD_REMARK"].ToString(),
                HAAD_SCHEME = row["HAAD_SCHEME"] == DBNull.Value ? string.Empty : row["HAAD_SCHEME"].ToString(),
                HAAD_EFFECT_DT = row["HAAD_EFFECT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAAD_EFFECT_DT"].ToString()),
                HAAD_ORIG_GRACE_AMT = row["HAAD_ORIG_GRACE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HAAD_ORIG_GRACE_AMT"].ToString()),
                Scheme = row["Scheme"] == DBNull.Value ? string.Empty : row["Scheme"].ToString(),
            };
        }
    }
}
