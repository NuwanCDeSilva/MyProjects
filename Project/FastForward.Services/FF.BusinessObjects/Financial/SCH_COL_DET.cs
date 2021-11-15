using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class SCH_COL_DET
    {
        public string psi_hed_cd { get; set; }
        public string psi_col_seq { get; set; }

        public static SCH_COL_DET Converter(DataRow row)
        {
            return new SCH_COL_DET
            {
                psi_hed_cd = row["psi_hed_cd"] == DBNull.Value ? string.Empty : row["psi_hed_cd"].ToString(),
                psi_col_seq = row["psi_col_seq"] == DBNull.Value ? string.Empty : row["psi_col_seq"].ToString(),
            };
        }
    }
}
