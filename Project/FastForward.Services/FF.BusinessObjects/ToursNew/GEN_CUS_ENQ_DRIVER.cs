using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class GEN_CUS_ENQ_DRIVER
    {
        public String GCD_DRIVER_CD { get; set; }
        public String GCD_ENQ_NO { get; set; }
        public String GCD_DRIVER_NAME { get; set; }
        public String GCD_DRIVER_CONTACT { get; set; }
        public DateTime GCD_FROM_DT { get; set; }
        public DateTime GCD_TO_DT { get; set; }
        public decimal GCD_DRIVER_COST{get;set;}
        public Int32 GCD_AMENTMENT{get;set;}
        public Int32 GCD_ACT{get;set;}
        public decimal GCD_ACTL_DRIVER_COST { get; set; }
        public DateTime GCD_ADDED_DT { get; set; }
        public string GCD_ADDED_BY { get; set; }
        public DateTime GCD_MOD_DT { get; set; }
        public string GCD_MOD_BY { get; set; }
        public Int32 GCD_SEQ { get; set; }
        public static GEN_CUS_ENQ_DRIVER Converter(DataRow row)
        {
            return new GEN_CUS_ENQ_DRIVER
            {
                GCD_DRIVER_CD = row["GCD_DRIVER_CD"] == DBNull.Value ? string.Empty : row["GCD_DRIVER_CD"].ToString(),
                GCD_ENQ_NO = row["GCD_ENQ_NO"] == DBNull.Value ? string.Empty : row["GCD_ENQ_NO"].ToString(),
                GCD_DRIVER_NAME = row["GCD_DRIVER_NAME"] == DBNull.Value ? string.Empty : row["GCD_DRIVER_NAME"].ToString(),
                GCD_DRIVER_CONTACT = row["GCD_DRIVER_CONTACT"] == DBNull.Value ? string.Empty : row["GCD_DRIVER_CONTACT"].ToString(),
                GCD_FROM_DT = row["GCD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCD_FROM_DT"].ToString()),
                GCD_TO_DT = row["GCD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCD_TO_DT"].ToString()),
                GCD_DRIVER_COST = row["GCD_DRIVER_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCD_DRIVER_COST"].ToString()),
                GCD_AMENTMENT = row["GCD_AMENTMENT"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCD_AMENTMENT"].ToString()),
                GCD_ACT = row["GCD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCD_ACT"].ToString()),
                GCD_ACTL_DRIVER_COST = row["GCD_ACTL_DRIVER_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCD_ACTL_DRIVER_COST"].ToString()),
                GCD_ADDED_DT = row["GCD_ADDED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCD_ADDED_DT"].ToString()),
                GCD_MOD_DT = row["GCD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCD_MOD_DT"].ToString()),
                GCD_ADDED_BY = row["GCD_ADDED_BY"] == DBNull.Value ? string.Empty : row["GCD_ADDED_BY"].ToString(),
                GCD_MOD_BY = row["GCD_MOD_BY"] == DBNull.Value ? string.Empty : row["GCD_MOD_BY"].ToString(),
                GCD_SEQ = row["GCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCD_SEQ"].ToString())
            };
        }
    }
}
