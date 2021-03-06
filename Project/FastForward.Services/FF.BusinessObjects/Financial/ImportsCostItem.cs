using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 22-Jul-2015 02:26:06
    //===========================================================================================================

    public class ImportsCostItem
    {
        public Int32 Ici_seq_no { get; set; }
        public Int32 Ici_line { get; set; }
        public String Ici_doc_no { get; set; }
        public Int32 Ici_ref_line { get; set; }
        public Int32 Ici_f_line { get; set; }
        public Int32 Ici_base_line { get; set; }
        public Int32 Ici_stus { get; set; }
        public String Ici_itm_cd { get; set; }
        public String Ici_itm_stus { get; set; }
        public Decimal Ici_qty { get; set; }
        public Decimal Ici_unit_rt { get; set; }
        public Decimal Ici_unit_amt { get; set; }
        public Decimal Ici_pre_rt { get; set; }
        public Decimal Ici_pre_amt { get; set; }
        public Decimal Ici_actl_rt { get; set; }
        public Decimal Ici_actl_amt { get; set; }
        public Decimal Ici_finl_rt { get; set; }
        public Decimal Ici_finl_amt { get; set; }
        public String Ici_anal_1 { get; set; }
        public String Ici_anal_2 { get; set; }
        public String Ici_anal_3 { get; set; }
        public String Ici_anal_4 { get; set; }
        public String Ici_anal_5 { get; set; }
        public String Ici_cre_by { get; set; }
        public DateTime Ici_cre_dt { get; set; }
        public String Ici_mod_by { get; set; }
        public DateTime Ici_mod_dt { get; set; }
        public String Ici_session_id { get; set; }
        public String Ref_FinNumber { get; set; } // 2016-02-08

        public static ImportsCostItem Converter(DataRow row)
        {
            return new ImportsCostItem
            {
                Ici_seq_no = row["ICI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICI_SEQ_NO"].ToString()),
                Ici_line = row["ICI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICI_LINE"].ToString()),
                Ici_ref_line = row["ICI_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICI_REF_LINE"].ToString()),
                Ici_doc_no = row["ICI_DOC_NO"] == DBNull.Value ? string.Empty : row["ICI_DOC_NO"].ToString(),
                Ici_stus = row["ICI_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICI_STUS"].ToString()),
                Ici_itm_cd = row["ICI_ITM_CD"] == DBNull.Value ? string.Empty : row["ICI_ITM_CD"].ToString(),
                Ici_itm_stus = row["ICI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ICI_ITM_STUS"].ToString(),
                Ici_qty = row["ICI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_QTY"].ToString()),
                Ici_unit_rt = row["ICI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_UNIT_RT"].ToString()),
                Ici_unit_amt = row["ICI_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_UNIT_AMT"].ToString()),
                Ici_pre_rt = row["ICI_PRE_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_PRE_RT"].ToString()),
                Ici_pre_amt = row["ICI_PRE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_PRE_AMT"].ToString()),
                Ici_actl_rt = row["ICI_ACTL_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_ACTL_RT"].ToString()),
                Ici_actl_amt = row["ICI_ACTL_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_ACTL_AMT"].ToString()),
                Ici_finl_rt = row["ICI_FINL_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_FINL_RT"].ToString()),
                Ici_finl_amt = row["ICI_FINL_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_FINL_AMT"].ToString()),
                Ici_anal_1 = row["ICI_ANAL_1"] == DBNull.Value ? string.Empty : row["ICI_ANAL_1"].ToString(),
                Ici_anal_2 = row["ICI_ANAL_2"] == DBNull.Value ? string.Empty : row["ICI_ANAL_2"].ToString(),
                Ici_anal_3 = row["ICI_ANAL_3"] == DBNull.Value ? string.Empty : row["ICI_ANAL_3"].ToString(),
                Ici_anal_4 = row["ICI_ANAL_4"] == DBNull.Value ? string.Empty : row["ICI_ANAL_4"].ToString(),
                Ici_anal_5 = row["ICI_ANAL_5"] == DBNull.Value ? string.Empty : row["ICI_ANAL_5"].ToString(),
                Ici_cre_by = row["ICI_CRE_BY"] == DBNull.Value ? string.Empty : row["ICI_CRE_BY"].ToString(),
                Ici_cre_dt = row["ICI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICI_CRE_DT"].ToString()),
                Ici_mod_by = row["ICI_MOD_BY"] == DBNull.Value ? string.Empty : row["ICI_MOD_BY"].ToString(),
                Ici_mod_dt = row["ICI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICI_MOD_DT"].ToString()),
                Ici_session_id = row["ICI_SESSION_ID"] == DBNull.Value ? string.Empty : row["ICI_SESSION_ID"].ToString()
            };
        }

        public static ImportsCostItem ConverterITMCOST(DataRow row)
        {
            return new ImportsCostItem
            {
                Ici_seq_no = row["ICI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICI_SEQ_NO"].ToString()),
                Ici_line = row["ICI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICI_LINE"].ToString()),
                Ici_doc_no = row["ICI_DOC_NO"] == DBNull.Value ? string.Empty : row["ICI_DOC_NO"].ToString(),
                Ici_qty = row["ICI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_QTY"].ToString()),
                Ici_actl_rt = row["ICI_ACTL_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_ACTL_RT"].ToString()),
                Ici_actl_amt = row["ICI_ACTL_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICI_ACTL_AMT"].ToString())
            };
        }

     
    }
}

