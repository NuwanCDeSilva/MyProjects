using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class GEN_CUS_ENQ_FLEET
    { 
        public Int32 GCF_SEQ { get; set; }
        public String GCF_FLEET { get; set; }
        public String GCF_ENQ_NO { get; set; }
        public DateTime GCF_FROM_DT { get; set; }
        public DateTime GCF_TO_DT { get; set; }
        public Decimal GCF_FLEET_COST { get; set; }
        public Int32 GCF_AMENTMENT { get; set; }
        public Int32 GCF_ACT { get; set; }
        public Decimal GCF_ACTL_FLEET_COST { get; set; }
        public DateTime GCF_ADDED_DT { get; set; }
        public String GCF_ADDED_BY { get; set; }
        public String GCF_MOD_BY { get; set; }
        public DateTime GCF_MOD_DT { get; set; }
        public string GCF_MODEL { get; set; }
        public string GCF_BRAND { get; set; }
        public static GEN_CUS_ENQ_FLEET Converter(DataRow row)
        {
            return new GEN_CUS_ENQ_FLEET
            {
                GCF_SEQ = row["GCF_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCF_SEQ"].ToString()),
                GCF_FLEET = row["GCF_FLEET"] == DBNull.Value ? string.Empty : row["GCF_FLEET"].ToString(),
                GCF_ENQ_NO = row["GCF_ENQ_NO"] == DBNull.Value ? string.Empty : row["GCF_ENQ_NO"].ToString(),
                GCF_FROM_DT = row["GCF_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCF_FROM_DT"].ToString()),
                GCF_TO_DT = row["GCF_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCF_TO_DT"].ToString()),
                GCF_FLEET_COST = row["GCF_FLEET_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCF_FLEET_COST"].ToString()),
                GCF_AMENTMENT = row["GCF_AMENTMENT"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCF_AMENTMENT"].ToString()),
                GCF_ACT = row["GCF_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCF_ACT"].ToString()),
                GCF_ACTL_FLEET_COST = row["GCF_ACTL_FLEET_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCF_ACTL_FLEET_COST"].ToString()),
                GCF_ADDED_DT = row["GCF_ADDED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCF_ADDED_DT"].ToString()),
                GCF_ADDED_BY = row["GCF_ADDED_BY"] == DBNull.Value ? string.Empty : row["GCF_ADDED_BY"].ToString(),
                GCF_MOD_BY = row["GCF_MOD_BY"] == DBNull.Value ? string.Empty : row["GCF_MOD_BY"].ToString(),
                GCF_MOD_DT = row["GCF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCF_MOD_DT"].ToString()),
                GCF_MODEL = row["GCF_MODEL"] == DBNull.Value ? string.Empty : row["GCF_MODEL"].ToString(),
                GCF_BRAND = row["GCF_BRAND"] == DBNull.Value ? string.Empty : row["GCF_BRAND"].ToString()
            };
        } 
    }
}
