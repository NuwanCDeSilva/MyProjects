using System;
using System.Data;

namespace FF.BusinessObjects
{

    public class ref_comm_det
    {
        public Int32 Rcd_seq { get; set; }
        public String Rcd_comm_cd { get; set; }
        public String Rcd_item_cd { get; set; }
        public String Rcd_brand { get; set; }
        public String Rcd_cat1 { get; set; }
        public String Rcd_cat2 { get; set; }
        public decimal Rcd_st_val { get; set; }
        public decimal Rcd_end_val { get; set; }
        public decimal Rcd_comm_val { get; set; }
        public String Rcd_anal1 { get; set; }
        public String Rcd_anal2 { get; set; }
        public String Rcd_model { get; set; }
        public Int32 Rcd_line { get; set; }
        public string Rcd_inv_tp { get; set; }
        public string ExecCode { get; set; }
        public String Rcd_cat3 { get; set; }
        public string Rcd_btu_f { get; set; }
        public string Rcd_btu_e { get; set; }
        public string Rcd_anal3 { get; set; }
        public string Rcd_anal4 { get; set; }
        
        public static ref_comm_det Converter(DataRow row)
        {
            return new ref_comm_det
            {
                Rcd_seq = row["RCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCD_SEQ"].ToString()),
                Rcd_comm_cd = row["RCD_COMM_CD"] == DBNull.Value ? string.Empty : row["RCD_COMM_CD"].ToString(),
                Rcd_item_cd = row["RCD_ITEM_CD"] == DBNull.Value ? string.Empty : row["RCD_ITEM_CD"].ToString(),
                Rcd_brand = row["RCD_BRAND"] == DBNull.Value ? string.Empty : row["RCD_BRAND"].ToString(),
                Rcd_cat1 = row["RCD_CAT1"] == DBNull.Value ? string.Empty : row["RCD_CAT1"].ToString(),
                Rcd_cat2 = row["RCD_CAT2"] == DBNull.Value ? string.Empty : row["RCD_CAT2"].ToString(),
                Rcd_st_val = row["RCD_ST_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCD_ST_VAL"].ToString()),
                Rcd_end_val = row["RCD_END_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCD_END_VAL"].ToString()),
                Rcd_comm_val = row["RCD_COMM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCD_COMM_VAL"].ToString()),
                Rcd_anal1 = row["RCD_ANAL1"] == DBNull.Value ? string.Empty : row["RCD_ANAL1"].ToString(),
                Rcd_anal2 = row["RCD_ANAL2"] == DBNull.Value ? string.Empty : row["RCD_ANAL2"].ToString(),
                Rcd_model = row["Rcd_model"] == DBNull.Value ? string.Empty : row["Rcd_model"].ToString(),
                Rcd_line = row["RCD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCD_LINE"].ToString()),
                Rcd_inv_tp = row["Rcd_inv_tp"] == DBNull.Value ? string.Empty : row["Rcd_inv_tp"].ToString(),
                Rcd_cat3 = row["Rcd_cat3"] == DBNull.Value ? string.Empty : row["Rcd_cat3"].ToString(),
                Rcd_btu_f = row["Rcd_btu_f"] == DBNull.Value ? string.Empty : row["Rcd_btu_f"].ToString(),
                Rcd_btu_e = row["Rcd_btu_e"] == DBNull.Value ? string.Empty : row["Rcd_btu_e"].ToString(),
                Rcd_anal3 = row["Rcd_anal3"] == DBNull.Value ? string.Empty : row["Rcd_anal3"].ToString(),
                Rcd_anal4 = row["Rcd_anal4"] == DBNull.Value ? string.Empty : row["Rcd_anal4"].ToString(),
            };
        }
    }
}

