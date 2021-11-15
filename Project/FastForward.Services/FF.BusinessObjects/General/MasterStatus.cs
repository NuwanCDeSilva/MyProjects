using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class MasterStatus
    {
        public String Mss_cd { get; set; }
        public String Mss_desc { get; set; }
        public Int32 Mss_is_app { get; set; }
        public static MasterStatus Converter(DataRow row)
        {
            return new MasterStatus
            {
                Mss_cd = row["MSS_CD"] == DBNull.Value ? string.Empty : row["MSS_CD"].ToString(),
                Mss_desc = row["MSS_DESC"] == DBNull.Value ? string.Empty : row["MSS_DESC"].ToString(),
                Mss_is_app = row["MSS_IS_APP"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSS_IS_APP"].ToString())
            };
        }
    }
}
