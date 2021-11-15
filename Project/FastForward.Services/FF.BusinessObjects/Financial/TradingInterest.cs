using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class TradingInterest 
    {
        public string mti_com { get; set; }  
        public string mti_sch_cd { get; set; }
        public string sch_type { get; set; } 
        public string mti_sch_cat { get; set; }
        public Int32 mti_sch_trm { get; set; } 
        public string mti_itm { get; set; }
        public string mti_cat1 { get; set; }
        public string mti_cat2 { get; set; }
        public string mti_cat3 { get; set; }
        public string mti_brnd { get; set; }
        public Decimal mti_rt { get; set; }
        public Int32 mti_act { get; set; }
        public DateTime mti_frm { get; set; }
        public DateTime mti_to { get; set; }
        public string mti_cre_by { get; set; }
        public string mti_mod_by { get; set; }
        public static TradingInterest Convertnew(DataRow row)
        {
            return new TradingInterest
            {
                mti_com = row["mti_com"] == DBNull.Value ? string.Empty : row["mti_com"].ToString(),
                mti_sch_cd = row["mti_sch_cd"] == DBNull.Value ? string.Empty : row["mti_sch_cd"].ToString(),
                mti_sch_cat = row["mti_sch_cat"] == DBNull.Value ? string.Empty : row["mti_sch_cat"].ToString(),
              //  mti_sch_trm = row["mti_sch_trm"] == DBNull.Value ? string.Empty : row["mti_sch_trm"].ToString(),
                mti_itm = row["mti_itm"] == DBNull.Value ? string.Empty : row["mti_itm"].ToString(),
                mti_cat2 = row["mti_cat2"] == DBNull.Value ? string.Empty : row["mti_cat2"].ToString(),
                mti_cat1 = row["mti_cat1"] == DBNull.Value ? string.Empty : row["mti_cat1"].ToString(),
                mti_cat3 = row["mti_cat3"] == DBNull.Value ? string.Empty : row["mti_brnd"].ToString(),
                mti_brnd = row["mti_brnd"] == DBNull.Value ? string.Empty : row["mti_brnd"].ToString(),
                mti_rt = row["mti_rt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["mti_rt"].ToString()),

                mti_act = row["mti_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["mti_act"].ToString()),
                mti_frm = row["mti_frm"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mti_frm"]),
                mti_to = row["mti_to"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mti_to"])

            };
        }

    }

 
    
}
