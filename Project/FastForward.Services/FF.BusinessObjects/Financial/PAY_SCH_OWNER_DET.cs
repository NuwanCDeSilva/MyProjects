using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class PAY_SCH_OWNERS_DET
    {
        public string psa_fld_cd { get; set; }
        public string psa_hed_cd { get; set; }
        public string psa_clm_id { get; set; }
        public string psa_rec_id { get; set; }
        public string psa_value { get; set; }

        public static PAY_SCH_OWNERS_DET Converter(DataRow row)
        {
            return new PAY_SCH_OWNERS_DET
            {
                psa_fld_cd = row["psa_fld_cd"] == DBNull.Value ? string.Empty : row["psa_fld_cd"].ToString(),
                psa_hed_cd = row["psa_hed_cd"] == DBNull.Value ? string.Empty : row["psa_hed_cd"].ToString(),
                psa_clm_id = row["psa_clm_id"] == DBNull.Value ? string.Empty : row["psa_clm_id"].ToString(),
                psa_rec_id = row["psa_rec_id"] == DBNull.Value ? string.Empty : row["psa_rec_id"].ToString(),
                psa_value = row["psa_value"] == DBNull.Value ? string.Empty : row["psa_value"].ToString(),
            };
        }
    }
}
