using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================

    // Computer :- ITPD18  | User :- subodanam On 06-Jul-2017 03:39:06
    //===========================================================================================================

    public class trn_inv_det
    {
        public Int32 Tid_seq_no { get; set; }
        public String Tid_com_cd { get; set; }
        public Int32 Tid_line_no { get; set; }
        public String Tid_inv_no { get; set; }
        public String Tid_cha_code { get; set; }
        public String Tid_cha_desc { get; set; }
        public Decimal Tid_cha_amt { get; set; }
        public Int32 Tid_is_rev { get; set; }
        public Decimal Tid_qty { get; set; }
        public String Tid_units { get; set; }
        public Decimal Tid_unit_amnt { get; set; }
        public String Tid_curr_cd { get; set; }
        public Decimal Tid_ex_rate { get; set; }
        public Decimal Tid_cha_rate { get; set; }
        public String Tid_merge_chacode { get; set; }
        public String Tid_rmk { get; set; }
        public Decimal Tid_merge_val { get; set; }
        public String Tid_job_no { get; set; }
        public String Tid_bl_m_no { get; set; }
        public String Tid_bl_h_no { get; set; }
        public String Tid_bl_d_no { get; set; }
        public Int32 Tid_invr_merge_line { get; set; }
        public String Tid_ser_cd { get; set; }
        public String Tid_inv_method { get; set; }
        public Int32 Tid_docline { get; set; }
        public String Tid_doc_no { get; set; }
        public String Tid_inv_crd_no { get; set; }
        public static trn_inv_det Converter(DataRow row)
        {
            return new trn_inv_det
            {
                Tid_seq_no = row["TID_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_SEQ_NO"].ToString()),
                Tid_com_cd = row["TID_COM_CD"] == DBNull.Value ? string.Empty : row["TID_COM_CD"].ToString(),
                Tid_line_no = row["TID_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_LINE_NO"].ToString()),
                Tid_inv_no = row["TID_INV_NO"] == DBNull.Value ? string.Empty : row["TID_INV_NO"].ToString(),
                Tid_cha_code = row["TID_CHA_CODE"] == DBNull.Value ? string.Empty : row["TID_CHA_CODE"].ToString(),
                Tid_cha_desc = row["TID_CHA_DESC"] == DBNull.Value ? string.Empty : row["TID_CHA_DESC"].ToString(),
                Tid_cha_amt = row["TID_CHA_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_CHA_AMT"].ToString()),
                Tid_is_rev = row["TID_IS_REV"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_IS_REV"].ToString()),
                Tid_qty = row["TID_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_QTY"].ToString()),
                Tid_units = row["TID_UNITS"] == DBNull.Value ? string.Empty : row["TID_UNITS"].ToString(),
                Tid_unit_amnt = row["TID_UNIT_AMNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_UNIT_AMNT"].ToString()),
                Tid_curr_cd = row["TID_CURR_CD"] == DBNull.Value ? string.Empty : row["TID_CURR_CD"].ToString(),
                Tid_ex_rate = row["TID_EX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_EX_RATE"].ToString()),
                Tid_cha_rate = row["TID_CHA_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_CHA_RATE"].ToString()),
                Tid_merge_chacode = row["TID_MERGE_CHACODE"] == DBNull.Value ? string.Empty : row["TID_MERGE_CHACODE"].ToString(),
                Tid_rmk = row["TID_RMK"] == DBNull.Value ? string.Empty : row["TID_RMK"].ToString(),
                Tid_merge_val = row["TID_MERGE_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_MERGE_VAL"].ToString()),
                Tid_job_no = row["TID_JOB_NO"] == DBNull.Value ? string.Empty : row["TID_JOB_NO"].ToString(),
                Tid_bl_m_no = row["TID_BL_M_NO"] == DBNull.Value ? string.Empty : row["TID_BL_M_NO"].ToString(),
                Tid_bl_h_no = row["TID_BL_H_NO"] == DBNull.Value ? string.Empty : row["TID_BL_H_NO"].ToString(),
                Tid_bl_d_no = row["TID_BL_D_NO"] == DBNull.Value ? string.Empty : row["TID_BL_D_NO"].ToString(),
                Tid_invr_merge_line = row["TID_INVR_MERGE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_INVR_MERGE_LINE"].ToString()),
                Tid_ser_cd = row["TID_SER_CD"] == DBNull.Value ? string.Empty : row["TID_SER_CD"].ToString(),
                Tid_inv_method = row["TID_INV_METHOD"] == DBNull.Value ? string.Empty : row["TID_INV_METHOD"].ToString(),
                Tid_docline = row["TID_DOCLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_DOCLINE"].ToString()),
                Tid_doc_no = row["TID_DOC_NO"] == DBNull.Value ? string.Empty : row["TID_DOC_NO"].ToString()
            };
        }
        public static trn_inv_det Converter1(DataRow row)
        {
            return new trn_inv_det
            {
                Tid_seq_no = row["TID_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_SEQ_NO"].ToString()),
                Tid_com_cd = row["TID_COM_CD"] == DBNull.Value ? string.Empty : row["TID_COM_CD"].ToString(),
                Tid_line_no = row["TID_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_LINE_NO"].ToString()),
                Tid_inv_no = row["TID_INV_NO"] == DBNull.Value ? string.Empty : row["TID_INV_NO"].ToString(),
                Tid_cha_code = row["TID_CHA_CODE"] == DBNull.Value ? string.Empty : row["TID_CHA_CODE"].ToString(),
                Tid_cha_desc = row["TID_CHA_DESC"] == DBNull.Value ? string.Empty : row["TID_CHA_DESC"].ToString(),
                Tid_cha_amt = row["TID_CHA_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_CHA_AMT"].ToString()),
                Tid_is_rev = row["TID_IS_REV"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_IS_REV"].ToString()),
                Tid_qty = row["TID_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_QTY"].ToString()),
                Tid_units = row["TID_UNITS"] == DBNull.Value ? string.Empty : row["TID_UNITS"].ToString(),
                Tid_unit_amnt = row["TID_UNIT_AMNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_UNIT_AMNT"].ToString()),
                Tid_curr_cd = row["TID_CURR_CD"] == DBNull.Value ? string.Empty : row["TID_CURR_CD"].ToString(),
                Tid_ex_rate = row["TID_EX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_EX_RATE"].ToString()),
                Tid_cha_rate = row["TID_CHA_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_CHA_RATE"].ToString()),
                Tid_merge_chacode = row["TID_MERGE_CHACODE"] == DBNull.Value ? string.Empty : row["TID_MERGE_CHACODE"].ToString(),
                Tid_rmk = row["TID_RMK"] == DBNull.Value ? string.Empty : row["TID_RMK"].ToString(),
                Tid_merge_val = row["TID_MERGE_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TID_MERGE_VAL"].ToString()),
                Tid_job_no = row["TID_JOB_NO"] == DBNull.Value ? string.Empty : row["TID_JOB_NO"].ToString(),
                Tid_bl_m_no = row["TID_BL_M_NO"] == DBNull.Value ? string.Empty : row["TID_BL_M_NO"].ToString(),
                Tid_bl_h_no = row["TID_BL_H_NO"] == DBNull.Value ? string.Empty : row["TID_BL_H_NO"].ToString(),
                Tid_bl_d_no = row["TID_BL_D_NO"] == DBNull.Value ? string.Empty : row["TID_BL_D_NO"].ToString(),
                Tid_invr_merge_line = row["TID_INVR_MERGE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_INVR_MERGE_LINE"].ToString()),
                Tid_ser_cd = row["TID_SER_CD"] == DBNull.Value ? string.Empty : row["TID_SER_CD"].ToString(),
                Tid_inv_method = row["TID_INV_METHOD"] == DBNull.Value ? string.Empty : row["TID_INV_METHOD"].ToString(),
                Tid_docline = row["TID_DOCLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TID_DOCLINE"].ToString()),
                Tid_doc_no = row["TID_DOC_NO"] == DBNull.Value ? string.Empty : row["TID_DOC_NO"].ToString(),
                Tid_inv_crd_no = row["TID_INV_NO"] == DBNull.Value ? string.Empty : row["TID_INV_NO"].ToString()
            };
        }
    }
}

