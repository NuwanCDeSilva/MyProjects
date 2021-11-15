using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class Mst_Movsubtp
    {
        public string mmst_mct_cd { get; set; }
        public string mmst_mst_cd { get; set; }
           public string mmst_mst_description { get; set; }
          public Int32 mmst_mst_active { get; set; }
           public string mmst_adj_type { get; set; }
          public Int32 mmst_adj_plus { get; set; }
          public Int32 mmst_adj_min { get; set; }
          public static Mst_Movsubtp Converternew(DataRow row)
        {
            return new Mst_Movsubtp
            {
                mmst_mct_cd = row["mmst_mct_cd"] == DBNull.Value ? string.Empty : row["mmst_mct_cd"].ToString(),
                mmst_mst_cd = row["mmst_mst_cd"] == DBNull.Value ? string.Empty : row["mmst_mst_cd"].ToString(),
                mmst_mst_description = row["mmst_mst_description"] == DBNull.Value ? string.Empty : row["mmst_mst_description"].ToString(),
                mmst_mst_active = row["mmst_mst_active"] == DBNull.Value ? 0 : Convert.ToInt32(row["mmst_mst_active"]),
                mmst_adj_type = row["mmst_adj_type"] == DBNull.Value ? string.Empty : row["mmst_adj_type"].ToString(),
                mmst_adj_plus = row["mmst_adj_plus"] == DBNull.Value ? 0 : Convert.ToInt32(row["mmst_adj_plus"]),
                mmst_adj_min = row["mmst_adj_min"] == DBNull.Value ? 0 : Convert.ToInt32(row["mmst_adj_min"])
              
            };
        }
    }
}
