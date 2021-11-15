using System;
using System.Data;

namespace FF.BusinessObjects
{

  public  class ref_bonus_loc
    {
        public Int32 Rbl_seq { get; set; }
        public String Rbl_docno { get; set; }
        public String Rbl_chnl { get; set; }
        public String Rbl_sub_chnl { get; set; }
        public String Rbl_region { get; set; }
        public String Rbl_zone { get; set; }
        public String Rbl_pc { get; set; }
        public String Rbl_anal1 { get; set; }
        public String Rbl_anal2 { get; set; }
        public String Rbl_anal3 { get; set; }
        public Int32 Rbl_line { get; set; }
        public static ref_bonus_loc Converter(DataRow row)
        {
            return new ref_bonus_loc
            {
                Rbl_seq = row["RBL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBL_SEQ"].ToString()),
                Rbl_docno = row["RBL_DOCNO"] == DBNull.Value ? string.Empty : row["RBL_DOCNO"].ToString(),
                Rbl_chnl = row["RBL_CHNL"] == DBNull.Value ? string.Empty : row["RBL_CHNL"].ToString(),
                Rbl_sub_chnl = row["RBL_SUB_CHNL"] == DBNull.Value ? string.Empty : row["RBL_SUB_CHNL"].ToString(),
                Rbl_region = row["RBL_REGION"] == DBNull.Value ? string.Empty : row["RBL_REGION"].ToString(),
                Rbl_zone = row["RBL_ZONE"] == DBNull.Value ? string.Empty : row["RBL_ZONE"].ToString(),
                Rbl_pc = row["RBL_PC"] == DBNull.Value ? string.Empty : row["RBL_PC"].ToString(),
                Rbl_anal1 = row["RBL_ANAL1"] == DBNull.Value ? string.Empty : row["RBL_ANAL1"].ToString(),
                Rbl_anal2 = row["RBL_ANAL2"] == DBNull.Value ? string.Empty : row["RBL_ANAL2"].ToString(),
                Rbl_anal3 = row["RBL_ANAL3"] == DBNull.Value ? string.Empty : row["RBL_ANAL3"].ToString(),
                Rbl_line = row["RBL_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBL_LINE"].ToString()),
            };
        }
    }
}

