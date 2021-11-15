using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class AGING_DET
    {
        public string mc_cd { get; set; }
        public decimal rags_slot_l1 { get; set; }
        public decimal rags_slot_l2 { get; set; }
        public decimal rags_slot_l3 { get; set; }
        public decimal rags_slot_l4 { get; set; }
        public decimal rags_slot_l5 { get; set; }
        public decimal rags_slot_g1 { get; set; }

        public static AGING_DET Converter(DataRow row)
        {
            return new AGING_DET
            {
                mc_cd = row["mc_cd"] == DBNull.Value ? string.Empty : row["mc_cd"].ToString(),
                rags_slot_l1 = row["rags_slot_l1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rags_slot_l1"].ToString()),
                rags_slot_l2 = row["rags_slot_l2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rags_slot_l2"].ToString()),
                rags_slot_l3 = row["rags_slot_l3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rags_slot_l3"].ToString()),
                rags_slot_l4 = row["rags_slot_l4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rags_slot_l4"].ToString()),
                rags_slot_l5 = row["rags_slot_l5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rags_slot_l5"].ToString()),
                rags_slot_g1 = row["rags_slot_g1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rags_slot_g1"].ToString()),

            };
        }

    }
}
