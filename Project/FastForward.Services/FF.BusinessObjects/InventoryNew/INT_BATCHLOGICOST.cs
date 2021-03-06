using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 28-Dec-2015 03:18:27
    //===========================================================================================================

    public class INT_BATCHLOGICOST
    {
        public Int32 Itbc_seq_no { get; set; }
        public Int32 Itbc_itm_line { get; set; }
        public Int32 Itbc_batch_line { get; set; }
        public Int32 Itbc_cost_line { get; set; }
        public String Itbc_batch_no { get; set; }
        public String Itbc_doc_no { get; set; }
        public DateTime Itbc_doc_dt { get; set; }
        public String Itbc_com { get; set; }
        public String Itbc_sbu { get; set; }
        public String Itbc_channel { get; set; }
        public String Itbc_loc { get; set; }
        public String Itbc_bin { get; set; }
        public String Itbc_itm_cd { get; set; }
        public String Itbc_itm_stus { get; set; }
        public Decimal Itbc_qty { get; set; }
        public Decimal Itbc_unit_cost { get; set; }
        public String Itbc_cost_cat { get; set; }
        public String Itbc_cost_tp { get; set; }
        public String Itbc_cost_ele { get; set; }
        public Decimal Itbc_logi_cost { get; set; }
        public Int32 Itbc_direct { get; set; }
        public Decimal Itbc_no_of { get; set; }
        public String Itbc_indoc_no { get; set; }
        public DateTime Itbc_indoc_dt { get; set; }
        public Int32 Itbc_act { get; set; }
        public String Itbc_cre_by { get; set; }
        public DateTime Itbc_cre_dt { get; set; }
        public String Itbc_cre_session { get; set; }
        public String Itbc_mod_by { get; set; }
        public DateTime Itbc_mod_dt { get; set; }
        public String Itbc_mod_session { get; set; }
        public static INT_BATCHLOGICOST Converter(DataRow row)
        {
            return new INT_BATCHLOGICOST
            {
                Itbc_seq_no = row["ITBC_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITBC_SEQ_NO"].ToString()),
                Itbc_itm_line = row["ITBC_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITBC_ITM_LINE"].ToString()),
                Itbc_batch_line = row["ITBC_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITBC_BATCH_LINE"].ToString()),
                Itbc_cost_line = row["ITBC_COST_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITBC_COST_LINE"].ToString()),
                Itbc_batch_no = row["ITBC_BATCH_NO"] == DBNull.Value ? string.Empty : row["ITBC_BATCH_NO"].ToString(),
                Itbc_doc_no = row["ITBC_DOC_NO"] == DBNull.Value ? string.Empty : row["ITBC_DOC_NO"].ToString(),
                Itbc_doc_dt = row["ITBC_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITBC_DOC_DT"].ToString()),
                Itbc_com = row["ITBC_COM"] == DBNull.Value ? string.Empty : row["ITBC_COM"].ToString(),
                Itbc_sbu = row["ITBC_SBU"] == DBNull.Value ? string.Empty : row["ITBC_SBU"].ToString(),
                Itbc_channel = row["ITBC_CHANNEL"] == DBNull.Value ? string.Empty : row["ITBC_CHANNEL"].ToString(),
                Itbc_loc = row["ITBC_LOC"] == DBNull.Value ? string.Empty : row["ITBC_LOC"].ToString(),
                Itbc_bin = row["ITBC_BIN"] == DBNull.Value ? string.Empty : row["ITBC_BIN"].ToString(),
                Itbc_itm_cd = row["ITBC_ITM_CD"] == DBNull.Value ? string.Empty : row["ITBC_ITM_CD"].ToString(),
                Itbc_itm_stus = row["ITBC_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITBC_ITM_STUS"].ToString(),
                Itbc_qty = row["ITBC_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITBC_QTY"].ToString()),
                Itbc_unit_cost = row["ITBC_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITBC_UNIT_COST"].ToString()),
                Itbc_cost_cat = row["ITBC_COST_CAT"] == DBNull.Value ? string.Empty : row["ITBC_COST_CAT"].ToString(),
                Itbc_cost_tp = row["ITBC_COST_TP"] == DBNull.Value ? string.Empty : row["ITBC_COST_TP"].ToString(),
                Itbc_cost_ele = row["ITBC_COST_ELE"] == DBNull.Value ? string.Empty : row["ITBC_COST_ELE"].ToString(),
                Itbc_logi_cost = row["ITBC_LOGI_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITBC_LOGI_COST"].ToString()),
                Itbc_direct = row["ITBC_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITBC_DIRECT"].ToString()),
                Itbc_no_of = row["ITBC_NO_OF"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITBC_NO_OF"].ToString()),
                Itbc_indoc_no = row["ITBC_INDOC_NO"] == DBNull.Value ? string.Empty : row["ITBC_INDOC_NO"].ToString(),
                Itbc_indoc_dt = row["ITBC_INDOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITBC_INDOC_DT"].ToString()),
                Itbc_act = row["ITBC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITBC_ACT"].ToString()),
                Itbc_cre_by = row["ITBC_CRE_BY"] == DBNull.Value ? string.Empty : row["ITBC_CRE_BY"].ToString(),
                Itbc_cre_dt = row["ITBC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITBC_CRE_DT"].ToString()),
                Itbc_cre_session = row["ITBC_CRE_SESSION"] == DBNull.Value ? string.Empty : row["ITBC_CRE_SESSION"].ToString(),
                Itbc_mod_by = row["ITBC_MOD_BY"] == DBNull.Value ? string.Empty : row["ITBC_MOD_BY"].ToString(),
                Itbc_mod_dt = row["ITBC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITBC_MOD_DT"].ToString()),
                Itbc_mod_session = row["ITBC_MOD_SESSION"] == DBNull.Value ? string.Empty : row["ITBC_MOD_SESSION"].ToString()
            };
        }
    }
}

