using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterPCTax
    {
        public Int32 Mpt_seq { get; set; }
        public String Mpt_com { get; set; }
        public String Mpt_pc { get; set; }
        public String Mpt_taxtp { get; set; }
        public String Mpt_taxrtcd { get; set; }
        public Decimal Mpt_taxrt { get; set; }
        public DateTime Mpt_frm_dt { get; set; }
        public DateTime Mpt_to_dt { get; set; }
        public Int32 Mpt_stus { get; set; }
        public String Mpt_cre_by { get; set; }
        public DateTime Mpt_cre_dt { get; set; }
        public String Mpt_mod_by { get; set; }
        public DateTime Mpt_mod_dt { get; set; }
        public static MasterPCTax Converter(DataRow row)
        {
            return new MasterPCTax
            {
                Mpt_seq = row["MPT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPT_SEQ"].ToString()),
                Mpt_com = row["MPT_COM"] == DBNull.Value ? string.Empty : row["MPT_COM"].ToString(),
                Mpt_pc = row["MPT_PC"] == DBNull.Value ? string.Empty : row["MPT_PC"].ToString(),
                Mpt_taxtp = row["MPT_TAXTP"] == DBNull.Value ? string.Empty : row["MPT_TAXTP"].ToString(),
                Mpt_taxrtcd = row["MPT_TAXRTCD"] == DBNull.Value ? string.Empty : row["MPT_TAXRTCD"].ToString(),
                Mpt_taxrt = row["MPT_TAXRT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPT_TAXRT"].ToString()),
                Mpt_frm_dt = row["MPT_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPT_FRM_DT"].ToString()),
                Mpt_to_dt = row["MPT_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPT_TO_DT"].ToString()),
                Mpt_stus = row["MPT_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPT_STUS"].ToString()),
                Mpt_cre_by = row["MPT_CRE_BY"] == DBNull.Value ? string.Empty : row["MPT_CRE_BY"].ToString(),
                Mpt_cre_dt = row["MPT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPT_CRE_DT"].ToString()),
                Mpt_mod_by = row["MPT_MOD_BY"] == DBNull.Value ? string.Empty : row["MPT_MOD_BY"].ToString(),
                Mpt_mod_dt = row["MPT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPT_MOD_DT"].ToString())
            };
        } 
    }
}
