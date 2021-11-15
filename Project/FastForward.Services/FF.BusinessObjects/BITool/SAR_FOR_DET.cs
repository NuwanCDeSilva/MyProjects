using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class SAR_FOR_DET
    {
        public Int32 Sfd_seq { get; set; }
        public Int32 Sfd_line { get; set; }
        public String Sfd_itm { get; set; }
        public String Sfd_cat1 { get; set; }
        public String Sfd_cat2 { get; set; }
        public String Sfd_cat3 { get; set; }
        public String Sfd_model { get; set; }
        public String Sfd_brnd { get; set; }
        public Decimal Sfd_qty { get; set; }
        public Decimal Sfd_val { get; set; }
        public Decimal Sfd_gp { get; set; }
        public Int32 Sfd_act { get; set; }
        public String tmp_itm_desc { get; set; }
        public String tmp_def_on { get; set; }
        public String tmp_def_by { get; set; }
        public String tmp_def_cd { get; set; }
        public String tmp_err_desc { get; set; }
        public string Sfh_def_cd { get; set; }
        public static SAR_FOR_DET Converter(DataRow row)
        {
            return new SAR_FOR_DET
            {
                Sfd_seq = row["SFD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFD_SEQ"].ToString()),
                Sfd_line = row["SFD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFD_LINE"].ToString()),
                Sfd_itm = row["SFD_ITM"] == DBNull.Value ? string.Empty : row["SFD_ITM"].ToString(),
                Sfd_cat1 = row["SFD_CAT1"] == DBNull.Value ? string.Empty : row["SFD_CAT1"].ToString(),
                Sfd_cat2 = row["SFD_CAT2"] == DBNull.Value ? string.Empty : row["SFD_CAT2"].ToString(),
                Sfd_cat3 = row["SFD_CAT3"] == DBNull.Value ? string.Empty : row["SFD_CAT3"].ToString(),
                Sfd_model = row["SFD_MODEL"] == DBNull.Value ? string.Empty : row["SFD_MODEL"].ToString(),
                Sfd_brnd = row["SFD_BRND"] == DBNull.Value ? string.Empty : row["SFD_BRND"].ToString(),
                Sfd_qty = row["SFD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SFD_QTY"].ToString()),
                Sfd_val = row["SFD_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SFD_VAL"].ToString()),
                Sfd_gp = row["SFD_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SFD_GP"].ToString()),
                Sfd_act = row["SFD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFD_ACT"].ToString()),
                Sfh_def_cd = row["SFH_DEF_CD"] == DBNull.Value ? string.Empty : row["SFH_DEF_CD"].ToString()
            };
        }
    }
}
