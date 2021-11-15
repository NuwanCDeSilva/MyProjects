using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
   public class SatProjectHeader
    {
        public Int32 SPH_SEQ { get; set; }
        public String SPH_COM { get; set; }
        public String SPH_PC { get; set; }
        public String SPH_NO { get; set; }
        public DateTime SPH_DT { get; set; }
        public DateTime SPH_OTH_DT { get; set; }
        public String SPH_REF { get; set; }
        public String SPH_EX { get; set; }
        public String SPH_OTH_EX { get; set; }
        public Decimal SPH_EST_COST { get; set; }
        public Decimal SPH_ACT_COST { get; set; }
        public Decimal SPH_EST_REV { get; set; }
        public Decimal SPH_ACT_REV { get; set; }
        public String SPH_CUS_CD { get; set; }
        public String SPH_CUS_NAME { get; set; }
        public String SPH_CUS_ADD1 { get; set; }
        public String SPH_CUS_ADD2 { get; set; }
        public String SPH_CUS_ADD3 { get; set; }
        public String SPH_RMK { get; set; }
        public String SPH_PRO_LOC { get; set; }
        public String SPH_LOC_DESC { get; set; }
        public String SPH_PB { get; set; }
        public String SPH_PB_LVL { get; set; }
        public String SPH_CRE_BY { get; set; }
        public DateTime SPH_CRE_DT { get; set; }
        public String SPH_MOD_BY { get; set; }
        public DateTime SPH_MOD_DT { get; set; }
        public String SPH_APP_BY { get; set; }
        public DateTime SPH_APP_DT { get; set; }
        public String SPH_ANAL1 { get; set; }
        public String SPH_ANAL2 { get; set; }
        public String SPH_ANAL3 { get; set; }
        public DateTime SPH_ANAL4 { get; set; }
        public DateTime SPH_ANAL5 { get; set; }
        public Int32 SPH_ANAL6 { get; set; }
        public Int32 SPH_ANAL7 { get; set; }
        public Int32 SPH_ANAL8 { get; set; }
        public String SPH_COM_BY { get; set; }
        public DateTime SPH_COM_DT { get; set; }

        public String SPH_STATUS { get; set; }
        public static SatProjectHeader Converter(DataRow row)
        {
            return new SatProjectHeader
            {
                SPH_SEQ = row["SPH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPH_SEQ"].ToString()),
                SPH_COM = row["SPH_COM"] == DBNull.Value ? string.Empty : row["SPH_COM"].ToString(),
                SPH_PC = row["SPH_PC"] == DBNull.Value ? string.Empty : row["SPH_PC"].ToString(),
                SPH_NO = row["SPH_NO"] == DBNull.Value ? string.Empty : row["SPH_NO"].ToString(),
                SPH_DT = row["SPH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPH_DT"].ToString()),
                SPH_OTH_DT = row["SPH_OTH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPH_OTH_DT"].ToString()),
                SPH_REF = row["SPH_REF"] == DBNull.Value ? string.Empty : row["SPH_REF"].ToString(),
                SPH_EX = row["SPH_EX"] == DBNull.Value ? string.Empty : row["SPH_EX"].ToString(),
                SPH_OTH_EX = row["SPH_OTH_EX"] == DBNull.Value ? string.Empty : row["SPH_OTH_EX"].ToString(),
                SPH_EST_COST = row["SPH_EST_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPH_EST_COST"].ToString()),
                SPH_ACT_COST = row["SPH_ACT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPH_ACT_COST"].ToString()),
                SPH_EST_REV = row["SPH_EST_REV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPH_EST_REV"].ToString()),
                SPH_ACT_REV = row["SPH_ACT_REV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPH_ACT_REV"].ToString()),
                SPH_CUS_CD = row["SPH_CUS_CD"] == DBNull.Value ? string.Empty : row["SPH_CUS_CD"].ToString(),
                SPH_CUS_NAME = row["SPH_CUS_NAME"] == DBNull.Value ? string.Empty : row["SPH_CUS_NAME"].ToString(),
                SPH_CUS_ADD1 = row["SPH_CUS_ADD1"] == DBNull.Value ? string.Empty : row["SPH_CUS_ADD1"].ToString(),
                SPH_CUS_ADD2 = row["SPH_CUS_ADD2"] == DBNull.Value ? string.Empty : row["SPH_CUS_ADD2"].ToString(),
                SPH_CUS_ADD3 = row["SPH_CUS_ADD3"] == DBNull.Value ? string.Empty : row["SPH_CUS_ADD3"].ToString(),
                SPH_RMK = row["SPH_RMK"] == DBNull.Value ? string.Empty : row["SPH_RMK"].ToString(),
                SPH_PRO_LOC = row["SPH_PRO_LOC"] == DBNull.Value ? string.Empty : row["SPH_PRO_LOC"].ToString(),
                SPH_LOC_DESC = row["SPH_LOC_DESC"] == DBNull.Value ? string.Empty : row["SPH_LOC_DESC"].ToString(),
                SPH_PB = row["SPH_PB"] == DBNull.Value ? string.Empty : row["SPH_PB"].ToString(),
                SPH_PB_LVL = row["SPH_PB_LVL"] == DBNull.Value ? string.Empty : row["SPH_PB_LVL"].ToString(),
                SPH_CRE_BY = row["SPH_CRE_BY"] == DBNull.Value ? string.Empty : row["SPH_CRE_BY"].ToString(),
                SPH_CRE_DT = row["SPH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPH_CRE_DT"].ToString()),
                SPH_MOD_BY = row["SPH_MOD_BY"] == DBNull.Value ? string.Empty : row["SPH_MOD_BY"].ToString(),
                SPH_MOD_DT = row["SPH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPH_MOD_DT"].ToString()),
                SPH_APP_BY = row["SPH_APP_BY"] == DBNull.Value ? string.Empty : row["SPH_APP_BY"].ToString(),
                SPH_APP_DT = row["SPH_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPH_APP_DT"].ToString()),
                SPH_ANAL1 = row["SPH_ANAL1"] == DBNull.Value ? string.Empty : row["SPH_ANAL1"].ToString(),
                SPH_ANAL2 = row["SPH_ANAL2"] == DBNull.Value ? string.Empty : row["SPH_ANAL2"].ToString(),
                SPH_ANAL3 = row["SPH_ANAL3"] == DBNull.Value ? string.Empty : row["SPH_ANAL3"].ToString(),
                SPH_ANAL4 = row["SPH_ANAL4"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPH_ANAL4"].ToString()),
                SPH_ANAL5 = row["SPH_ANAL5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPH_ANAL5"].ToString()),
                SPH_ANAL6 = row["SPH_ANAL6"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPH_ANAL6"].ToString()),
                SPH_ANAL7 = row["SPH_ANAL7"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPH_ANAL7"].ToString()),
                SPH_ANAL8 = row["SPH_ANAL8"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPH_ANAL8"].ToString()),
                SPH_COM_BY = row["SPH_COM_BY"] == DBNull.Value ? string.Empty : row["SPH_COM_BY"].ToString(),
                SPH_COM_DT = row["SPH_COM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPH_COM_DT"].ToString()),
                SPH_STATUS = row["SPH_STATUS"] == DBNull.Value ? string.Empty : row["SPH_STATUS"].ToString()
            };
        }
    }
} 

