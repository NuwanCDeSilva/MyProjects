using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class SatProjectDetails
    {
        public Int32 SPD_SEQ { get; set; }
        public Int32 SPD_LINE { get; set; }
        public String SPD_NO { get; set; }
        public String SPD_ITM { get; set; }
        public String SPD_ITM_DESC { get; set; }
        public String SPD_MODEL { get; set; }
        public Decimal SPD_EST_QTY { get; set; }
        public Decimal SPD_EST_COST { get; set; }
        public Decimal SPD_ACT_QTY { get; set; }
        public Decimal SPD_ACT_COST { get; set; }
        public Decimal SPD_EST_REV { get; set; }
        public Decimal SPD_ACT_REV { get; set; }
        public Decimal SPD_INV_QTY { get; set; }
        public String SPD_INV_PRT_DESC { get; set; }
        public Int32 SPD_ACTVE { get; set; }
        public string SPD_MI_ITM_UOM { get; set; }
        public string SPD_KIT_ITM { get; set; } //ADD by Lakshan 17Aug2017
        public decimal SPD_MRN_BAL { get; set; } //ADD by Lakshan 17Aug2017
        public static SatProjectDetails Converter(DataRow row)
        {
            return new SatProjectDetails
            {
                SPD_SEQ = row["SPD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_SEQ"].ToString()),
                SPD_LINE = row["SPD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_LINE"].ToString()),
                SPD_NO = row["SPD_NO"] == DBNull.Value ? string.Empty : row["SPD_NO"].ToString(),
                SPD_ITM = row["SPD_ITM"] == DBNull.Value ? string.Empty : row["SPD_ITM"].ToString(),
                SPD_ITM_DESC = row["SPD_ITM_DESC"] == DBNull.Value ? string.Empty : row["SPD_ITM_DESC"].ToString(),
                SPD_MODEL = row["SPD_MODEL"] == DBNull.Value ? string.Empty : row["SPD_MODEL"].ToString(),
                SPD_EST_QTY = row["SPD_EST_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_EST_QTY"].ToString()),
                SPD_EST_COST = row["SPD_EST_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_EST_COST"].ToString()),
                SPD_ACT_QTY = row["SPD_ACT_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_ACT_QTY"].ToString()),
                SPD_ACT_COST = row["SPD_ACT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_ACT_COST"].ToString()),
                SPD_EST_REV = row["SPD_EST_REV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_EST_REV"].ToString()),
                SPD_ACT_REV = row["SPD_ACT_REV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_ACT_REV"].ToString()),
                SPD_INV_QTY = row["SPD_INV_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_INV_QTY"].ToString()),
                SPD_INV_PRT_DESC = row["SPD_KIT_ITM"] == DBNull.Value ? string.Empty : row["SPD_KIT_ITM"].ToString(),
                SPD_ACTVE = row["SPD_ACTVE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_ACTVE"].ToString())
            };
        }
        //ADD by Lakshan 17Aug2017
        public static SatProjectDetails Converter2(DataRow row)
        {
            return new SatProjectDetails
            {
                SPD_SEQ = row["SPD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_SEQ"].ToString()),
                SPD_LINE = row["SPD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_LINE"].ToString()),
                SPD_NO = row["SPD_NO"] == DBNull.Value ? string.Empty : row["SPD_NO"].ToString(),
                SPD_ITM = row["SPD_ITM"] == DBNull.Value ? string.Empty : row["SPD_ITM"].ToString(),
                SPD_ITM_DESC = row["SPD_ITM_DESC"] == DBNull.Value ? string.Empty : row["SPD_ITM_DESC"].ToString(),
                SPD_MODEL = row["SPD_MODEL"] == DBNull.Value ? string.Empty : row["SPD_MODEL"].ToString(),
                SPD_EST_QTY = row["SPD_EST_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_EST_QTY"].ToString()),
                SPD_EST_COST = row["SPD_EST_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_EST_COST"].ToString()),
                SPD_ACT_QTY = row["SPD_ACT_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_ACT_QTY"].ToString()),
                SPD_ACT_COST = row["SPD_ACT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_ACT_COST"].ToString()),
                SPD_EST_REV = row["SPD_EST_REV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_EST_REV"].ToString()),
                SPD_ACT_REV = row["SPD_ACT_REV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_ACT_REV"].ToString()),
                SPD_INV_QTY = row["SPD_INV_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_INV_QTY"].ToString()),
                SPD_INV_PRT_DESC = row["SPD_INV_PRT_DESC"] == DBNull.Value ? string.Empty : row["SPD_INV_PRT_DESC"].ToString(),
                SPD_ACTVE = row["SPD_ACTVE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_ACTVE"].ToString()),
                SPD_KIT_ITM = row["SPD_KIT_ITM"] == DBNull.Value ?  string.Empty: Convert.ToString(row["SPD_KIT_ITM"].ToString()),
                SPD_MRN_BAL = row["SPD_MRN_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_MRN_BAL"].ToString())
            };
        }
    }
}

