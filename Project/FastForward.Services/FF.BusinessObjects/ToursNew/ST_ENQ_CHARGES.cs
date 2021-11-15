using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class ST_ENQ_CHARGES
    {
        public Int32 SCH_SEQ_NO { get; set; }
        public string SCH_ITM_SERVICE { get; set; }
        public String SCH_ITM_CD { get; set; }
        public String SCH_ITM_STUS { get; set; }
        public String SCH_ITM_TP { get; set; }
        public decimal SCH_QTY { get; set; }
        public decimal SCH_UNIT_RT { get; set; }
        public decimal SCH_UNIT_AMT { get; set; }
        public decimal SCH_DISC_RT { get; set; }
        public decimal SCH_DISC_AMT { get; set; }
        public decimal SCH_ITM_TAX_AMT { get; set; }
        public decimal SCH_TOT_AMT { get; set; }
        public String SCH_ENQ_NO { get; set; }
        public decimal SCH_EX_RT { get; set; }
        public String SCH_CURR { get; set; }
        public Int32 SCH_INVOICED { get; set; }
        public String SCH_INVOICED_NO { get; set; }
        public string SCH_ALT_ITM_DESC { get; set; }
        public static ST_ENQ_CHARGES Converter(DataRow row)
        {
            return new ST_ENQ_CHARGES
            {
                SCH_SEQ_NO = row["SCH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCH_SEQ_NO"].ToString()),
                SCH_ITM_SERVICE = row["SCH_ITM_SERVICE"] == DBNull.Value ? string.Empty : row["SCH_ITM_SERVICE"].ToString(),
                SCH_ITM_CD = row["SCH_ITM_CD"] == DBNull.Value ? string.Empty : row["SCH_ITM_CD"].ToString(),
                SCH_ITM_STUS = row["SCH_ITM_STUS"] == DBNull.Value ? string.Empty : row["SCH_ITM_STUS"].ToString(),
                SCH_ITM_TP = row["SCH_ITM_TP"] == DBNull.Value ? string.Empty : row["SCH_ITM_TP"].ToString(),
                SCH_QTY = row["SCH_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCH_QTY"].ToString()),
                SCH_UNIT_RT = row["SCH_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCH_UNIT_RT"].ToString()),
                SCH_UNIT_AMT = row["SCH_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCH_UNIT_AMT"].ToString()),
                SCH_DISC_RT = row["SCH_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCH_DISC_RT"].ToString()),
                SCH_DISC_AMT = row["SCH_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCH_DISC_AMT"].ToString()),
                SCH_ITM_TAX_AMT = row["SCH_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCH_ITM_TAX_AMT"].ToString()),
                SCH_TOT_AMT = row["SCH_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCH_TOT_AMT"].ToString()),
                SCH_ENQ_NO = row["SCH_ENQ_ID"] == DBNull.Value ? string.Empty : row["SCH_ENQ_ID"].ToString(),
                SCH_EX_RT = row["SCH_EX_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCH_EX_RT"].ToString()),
                SCH_CURR = row["SCH_CURR"] == DBNull.Value ? string.Empty : row["SCH_CURR"].ToString(),
                SCH_INVOICED = row["SCH_INVOICED"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCH_INVOICED"].ToString()),
                SCH_INVOICED_NO = row["SCH_INVOICED_NO"] == DBNull.Value ? string.Empty : row["SCH_INVOICED_NO"].ToString(),
                SCH_ALT_ITM_DESC = row["SCH_ALT_ITM_DESC"] == DBNull.Value ? string.Empty : row["SCH_ALT_ITM_DESC"].ToString()
            };
        }
    }
}
