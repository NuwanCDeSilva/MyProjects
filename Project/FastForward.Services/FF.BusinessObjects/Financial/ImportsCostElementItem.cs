using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 22-Jul-2015 02:27:04
    //===========================================================================================================

    public class ImportsCostElementItem
    {
        public Int32 Ice_seq_no { get; set; }
        public Int32 Ice_line { get; set; }
        public String Ice_doc_no { get; set; }
        public Int32 Ice_ref_line { get; set; }
        public Int32 Ice_stus { get; set; }
        public String Ice_itm_cd { get; set; }
        public String Ice_itm_stus { get; set; }
        public String Ice_ele_cat { get; set; }
        public String Ice_ele_tp { get; set; }
        public String Ice_ele_cd { get; set; }
        public Decimal Ice_ele_rt { get; set; }
        public Decimal Ice_ele_amnt { get; set; }
        public Decimal Ice_pre_rt { get; set; }
        public Decimal Ice_pre_amnt { get; set; }
        public Decimal Ice_actl_rt { get; set; }
        public Decimal Ice_actl_amnt { get; set; }
        public Decimal Ice_finl_rt { get; set; }
        public Decimal Ice_finl_amnt { get; set; }
        public String Ice_anal_1 { get; set; }
        public String Ice_anal_2 { get; set; }
        public String Ice_anal_3 { get; set; }
        public String Ice_anal_4 { get; set; }
        public String Ice_anal_5 { get; set; }
        public String Ice_cre_by { get; set; }
        public DateTime Ice_cre_dt { get; set; }
        public String Ice_mod_by { get; set; }
        public DateTime Ice_mod_dt { get; set; }
        public String Ice_session_id { get; set; }

        //Additional
        public String Ice_ele_cd_Desc { get; set; }

        //darshana
        public Decimal Ice_pre_amt_claim { get; set; }
        public Decimal Ice_actl_amt_claim { get; set; }
        public Decimal Ice_finl_amt_claim { get; set; }

        public static ImportsCostElementItem Converter(DataRow row)
        {
            return new ImportsCostElementItem
            {
                Ice_seq_no = row["ICE_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICE_SEQ_NO"].ToString()),
                Ice_line = row["ICE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICE_LINE"].ToString()),
                Ice_doc_no = row["ICE_DOC_NO"] == DBNull.Value ? string.Empty : row["ICE_DOC_NO"].ToString(),
                Ice_ref_line = row["ICE_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICE_REF_LINE"].ToString()),
                Ice_stus = row["ICE_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICE_STUS"].ToString()),
                Ice_itm_cd = row["ICE_ITM_CD"] == DBNull.Value ? string.Empty : row["ICE_ITM_CD"].ToString(),
                Ice_itm_stus = row["ICE_ITM_STUS"] == DBNull.Value ? string.Empty : row["ICE_ITM_STUS"].ToString(),
                Ice_ele_cat = row["ICE_ELE_CAT"] == DBNull.Value ? string.Empty : row["ICE_ELE_CAT"].ToString(),
                Ice_ele_tp = row["ICE_ELE_TP"] == DBNull.Value ? string.Empty : row["ICE_ELE_TP"].ToString(),
                Ice_ele_cd = row["ICE_ELE_CD"] == DBNull.Value ? string.Empty : row["ICE_ELE_CD"].ToString(),
                Ice_ele_rt = row["ICE_ELE_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ELE_RT"].ToString()),
                Ice_ele_amnt = row["ICE_ELE_AMNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ELE_AMNT"].ToString()),
                Ice_pre_rt = row["ICE_PRE_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_PRE_RT"].ToString()),
                Ice_pre_amnt = row["ICE_PRE_AMNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_PRE_AMNT"].ToString()),
                Ice_actl_rt = row["ICE_ACTL_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ACTL_RT"].ToString()),
                Ice_actl_amnt = row["ICE_ACTL_AMNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ACTL_AMNT"].ToString()),
                Ice_finl_rt = row["ICE_FINL_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_FINL_RT"].ToString()),
                Ice_finl_amnt = row["Ice_finl_amnt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Ice_finl_amnt"].ToString()),
                Ice_anal_1 = row["ICE_ANAL_1"] == DBNull.Value ? string.Empty : row["ICE_ANAL_1"].ToString(),
                Ice_anal_2 = row["ICE_ANAL_2"] == DBNull.Value ? string.Empty : row["ICE_ANAL_2"].ToString(),
                Ice_anal_3 = row["ICE_ANAL_3"] == DBNull.Value ? string.Empty : row["ICE_ANAL_3"].ToString(),
                Ice_anal_4 = row["ICE_ANAL_4"] == DBNull.Value ? string.Empty : row["ICE_ANAL_4"].ToString(),
                Ice_anal_5 = row["ICE_ANAL_5"] == DBNull.Value ? string.Empty : row["ICE_ANAL_5"].ToString(),
                Ice_cre_by = row["ICE_CRE_BY"] == DBNull.Value ? string.Empty : row["ICE_CRE_BY"].ToString(),
                Ice_cre_dt = row["ICE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICE_CRE_DT"].ToString()),
                Ice_mod_by = row["ICE_MOD_BY"] == DBNull.Value ? string.Empty : row["ICE_MOD_BY"].ToString(),
                Ice_mod_dt = row["ICE_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICE_MOD_DT"].ToString()),
                Ice_session_id = row["ICE_SESSION_ID"] == DBNull.Value ? string.Empty : row["ICE_SESSION_ID"].ToString(),
                Ice_pre_amt_claim = row["ICE_PRE_AMT_CLAIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_PRE_AMT_CLAIM"].ToString()),
                Ice_actl_amt_claim = row["ICE_ACTL_AMT_CLAIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ACTL_AMT_CLAIM"].ToString()),
                Ice_finl_amt_claim = row["ICE_FINL_AMT_CLAIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_FINL_AMT_CLAIM"].ToString())
            };
        }

        public static ImportsCostElementItem Converter2(DataRow row)
        {
            return new ImportsCostElementItem
            {
                Ice_seq_no = row["ICE_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICE_SEQ_NO"].ToString()),
                Ice_line = row["ICE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICE_LINE"].ToString()),
                Ice_doc_no = row["ICE_DOC_NO"] == DBNull.Value ? string.Empty : row["ICE_DOC_NO"].ToString(),
                Ice_ref_line = row["ICE_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICE_REF_LINE"].ToString()),
                Ice_stus = row["ICE_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICE_STUS"].ToString()),
                Ice_itm_cd = row["ICE_ITM_CD"] == DBNull.Value ? string.Empty : row["ICE_ITM_CD"].ToString(),
                Ice_itm_stus = row["ICE_ITM_STUS"] == DBNull.Value ? string.Empty : row["ICE_ITM_STUS"].ToString(),
                Ice_ele_cat = row["ICE_ELE_CAT"] == DBNull.Value ? string.Empty : row["ICE_ELE_CAT"].ToString(),
                Ice_ele_tp = row["ICE_ELE_TP"] == DBNull.Value ? string.Empty : row["ICE_ELE_TP"].ToString(),
                Ice_ele_cd = row["ICE_ELE_CD"] == DBNull.Value ? string.Empty : row["ICE_ELE_CD"].ToString(),
                Ice_ele_rt = row["ICE_ELE_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ELE_RT"].ToString()),
                Ice_ele_amnt = row["ICE_ELE_AMNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ELE_AMNT"].ToString()),
                Ice_pre_rt = row["ICE_PRE_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_PRE_RT"].ToString()),
                Ice_pre_amnt = row["ICE_PRE_AMNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_PRE_AMNT"].ToString()),
                Ice_actl_rt = row["ICE_ACTL_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ACTL_RT"].ToString()),
                Ice_actl_amnt = row["ICE_ACTL_AMNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ACTL_AMNT"].ToString()),
                Ice_finl_rt = row["ICE_FINL_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_FINL_RT"].ToString()),
                Ice_finl_amnt = row["Ice_finl_amnt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Ice_finl_amnt"].ToString()),
                Ice_anal_1 = row["ICE_ANAL_1"] == DBNull.Value ? string.Empty : row["ICE_ANAL_1"].ToString(),
                Ice_anal_2 = row["ICE_ANAL_2"] == DBNull.Value ? string.Empty : row["ICE_ANAL_2"].ToString(),
                Ice_anal_3 = row["ICE_ANAL_3"] == DBNull.Value ? string.Empty : row["ICE_ANAL_3"].ToString(),
                Ice_anal_4 = row["ICE_ANAL_4"] == DBNull.Value ? string.Empty : row["ICE_ANAL_4"].ToString(),
                Ice_anal_5 = row["ICE_ANAL_5"] == DBNull.Value ? string.Empty : row["ICE_ANAL_5"].ToString(),
                Ice_cre_by = row["ICE_CRE_BY"] == DBNull.Value ? string.Empty : row["ICE_CRE_BY"].ToString(),
                Ice_cre_dt = row["ICE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICE_CRE_DT"].ToString()),
                Ice_mod_by = row["ICE_MOD_BY"] == DBNull.Value ? string.Empty : row["ICE_MOD_BY"].ToString(),
                Ice_mod_dt = row["ICE_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICE_MOD_DT"].ToString()),
                Ice_session_id = row["ICE_SESSION_ID"] == DBNull.Value ? string.Empty : row["ICE_SESSION_ID"].ToString(),
                Ice_ele_cd_Desc = row["Ice_ele_cd_Desc"] == DBNull.Value ? string.Empty : row["Ice_ele_cd_Desc"].ToString(),
                Ice_pre_amt_claim = row["ICE_PRE_AMT_CLAIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_PRE_AMT_CLAIM"].ToString()),
                Ice_actl_amt_claim = row["ICE_ACTL_AMT_CLAIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_ACTL_AMT_CLAIM"].ToString()),
                Ice_finl_amt_claim = row["ICE_FINL_AMT_CLAIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICE_FINL_AMT_CLAIM"].ToString())
            };
        }
    }
}
