using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class SCH_PAY_COLUMN
    {
        public string psc_seq { get; set; }
        public string psc_name { get; set; }
        public string psi_hed_cd { get; set; }
        public string psi_col_seq { get; set; }
        public string psc_type { get; set; }

        public static SCH_PAY_COLUMN Converter(DataRow row)
        {
            return new SCH_PAY_COLUMN
            {
                psc_seq = row["psc_seq"] == DBNull.Value ? string.Empty : row["psc_seq"].ToString(),
                psc_name = row["psc_name"] == DBNull.Value ? string.Empty : row["psc_name"].ToString(),
                psi_hed_cd = row["psi_hed_cd"] == DBNull.Value ? string.Empty : row["psi_hed_cd"].ToString(),
                psi_col_seq = row["psi_col_seq"] == DBNull.Value ? string.Empty : row["psi_col_seq"].ToString(),
                psc_type = row["psc_type"] == DBNull.Value ? string.Empty : row["psc_type"].ToString(),
            };
        }
    }
}
