using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Refund_credit_definition
    {
        public Int32 Rrd_seq { get; set; }
        public String Rrd_com { get; set; }
        public String Rrd_pt_tp { get; set; }
        public String Rrd_pt_cd { get; set; }
        public String Rrd_itm { get; set; }
        public String Rrd_cat1 { get; set; }
        public String Rrd_cat2 { get; set; }
        public String Rrd_cat3 { get; set; }
        public String Rrd_cat4 { get; set; }
        public String Rrd_cat5 { get; set; }
        public String Rrd_brnd { get; set; }
        public Int32 Rrd_frm_pd { get; set; }
        public Int32 Rrd_to_pd { get; set; }
        public DateTime Rrd_valid_frm { get; set; }
        public DateTime Rrd_valid_to { get; set; }
        public DateTime Rrd_chk_frm { get; set; }
        public DateTime Rrd_chk_to { get; set; }
        public Int32 Rrd_act { get; set; }
        public Int32 Rrd_is_rt { get; set; }
        public Decimal Rrd_val { get; set; }
        public String Rrd_cre_by { get; set; }
        public DateTime Rrd_cre_dt { get; set; }
        public String Rrd_mod_by { get; set; }
        public DateTime Rrd_mod_dt { get; set; }
        public static Refund_credit_definition Converter(DataRow row)
        {
            return new Refund_credit_definition
            {
                Rrd_seq = row["RRD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RRD_SEQ"].ToString()),
                Rrd_com = row["RRD_COM"] == DBNull.Value ? string.Empty : row["RRD_COM"].ToString(),
                Rrd_pt_tp = row["RRD_PT_TP"] == DBNull.Value ? string.Empty : row["RRD_PT_TP"].ToString(),
                Rrd_pt_cd = row["RRD_PT_CD"] == DBNull.Value ? string.Empty : row["RRD_PT_CD"].ToString(),
                Rrd_itm = row["RRD_ITM"] == DBNull.Value ? string.Empty : row["RRD_ITM"].ToString(),
                Rrd_cat1 = row["RRD_CAT1"] == DBNull.Value ? string.Empty : row["RRD_CAT1"].ToString(),
                Rrd_cat2 = row["RRD_CAT2"] == DBNull.Value ? string.Empty : row["RRD_CAT2"].ToString(),
                Rrd_cat3 = row["RRD_CAT3"] == DBNull.Value ? string.Empty : row["RRD_CAT3"].ToString(),
                Rrd_cat4 = row["RRD_CAT4"] == DBNull.Value ? string.Empty : row["RRD_CAT4"].ToString(),
                Rrd_cat5 = row["RRD_CAT5"] == DBNull.Value ? string.Empty : row["RRD_CAT5"].ToString(),
                Rrd_brnd = row["RRD_BRND"] == DBNull.Value ? string.Empty : row["RRD_BRND"].ToString(),
                Rrd_frm_pd = row["RRD_FRM_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RRD_FRM_PD"].ToString()),
                Rrd_to_pd = row["RRD_TO_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RRD_TO_PD"].ToString()),
                Rrd_valid_frm = row["RRD_VALID_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RRD_VALID_FRM"].ToString()),
                Rrd_valid_to = row["RRD_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RRD_VALID_TO"].ToString()),
                Rrd_chk_frm = row["RRD_CHK_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RRD_CHK_FRM"].ToString()),
                Rrd_chk_to = row["RRD_CHK_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RRD_CHK_TO"].ToString()),
                Rrd_act = row["RRD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RRD_ACT"].ToString()),
                Rrd_is_rt = row["RRD_IS_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RRD_IS_RT"].ToString()),
                Rrd_val = row["RRD_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RRD_VAL"].ToString()),
                Rrd_cre_by = row["RRD_CRE_BY"] == DBNull.Value ? string.Empty : row["RRD_CRE_BY"].ToString(),
                Rrd_cre_dt = row["RRD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RRD_CRE_DT"].ToString()),
                Rrd_mod_by = row["RRD_MOD_BY"] == DBNull.Value ? string.Empty : row["RRD_MOD_BY"].ToString(),
                Rrd_mod_dt = row["RRD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RRD_MOD_DT"].ToString())
            };
        } 
    }
}
