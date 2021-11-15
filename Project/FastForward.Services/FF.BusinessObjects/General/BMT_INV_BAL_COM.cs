using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class BMT_INV_BAL_COM
    {
        public Int32 BMI_SEQ_NO { get; set; }
        public String BMI_COM_CD { get; set; }
        public String BMI_LOC_CD { get; set; }
        public String BMI_CHNL_CD { get; set; }
        public String BMI_ITM_CD { get; set; }
        public String BMI_ITM_STUS { get; set; }
        public Decimal BMI_AGE_QTY1 { get; set; }
        public Decimal BMI_AGE_COST1 { get; set; }
        public Decimal BMI_AVG_COST1 { get; set; }
        public Decimal BMI_AGE_QTY2 { get; set; }
        public Decimal BMI_AGE_COST2 { get; set; }
        public Decimal BMI_AVG_COST2 { get; set; }
        public Decimal BMI_AGE_QTY3 { get; set; }
        public Decimal BMI_AGE_COST3 { get; set; }
        public Decimal BMI_AVG_COST3 { get; set; }
        public Decimal BMI_AGE_QTY4 { get; set; }
        public Decimal BMI_AGE_COST4 { get; set; }
        public Decimal BMI_AVG_COST4 { get; set; }
        public Decimal BMI_AGE_QTY5 { get; set; }
        public Decimal BMI_AGE_COST5 { get; set; }
        public Decimal BMI_AVG_COST5 { get; set; }
        public Decimal BMI_AGE_QTY6 { get; set; }
        public Decimal BMI_AGE_COST6 { get; set; }
        public Decimal BMI_AVG_COST6 { get; set; }
        public Decimal BMI_AGE_QTY_T { get; set; }
        public Decimal BMI_AGE_COST_T { get; set; }
        public Decimal BMI_AVG_COST_T { get; set; }
        public String BMI_DOC_TP { get; set; }
        public String BMI_DOC_NO { get; set; }
        public DateTime BMI_DOC_DT { get; set; }
        public Int32 BMI_ITEM_LINE { get; set; }
        public Int32 BMI_LOG_SEQ { get; set; }
        public String BMI_GROUP { get; set; }
        public String BMI_ADMIN_TEAM { get; set; }
        public static BMT_INV_BAL_COM Converter(DataRow row)
        {
            return new BMT_INV_BAL_COM
        {
            BMI_SEQ_NO = row["BMI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMI_SEQ_NO"].ToString()),
            BMI_COM_CD = row["BMI_COM_CD"] == DBNull.Value ? string.Empty : row["BMI_COM_CD"].ToString(),
            BMI_LOC_CD = row["BMI_LOC_CD"] == DBNull.Value ? string.Empty : row["BMI_LOC_CD"].ToString(),
            BMI_CHNL_CD = row["BMI_CHNL_CD"] == DBNull.Value ? string.Empty : row["BMI_CHNL_CD"].ToString(),
            BMI_ITM_CD = row["BMI_ITM_CD"] == DBNull.Value ? string.Empty : row["BMI_ITM_CD"].ToString(),
            BMI_ITM_STUS = row["BMI_ITM_STUS"] == DBNull.Value ? string.Empty : row["BMI_ITM_STUS"].ToString(),
            BMI_AGE_QTY1 = row["BMI_AGE_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_QTY1"].ToString()),
            BMI_AGE_COST1 = row["BMI_AGE_COST1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_COST1"].ToString()),
            BMI_AVG_COST1 = row["BMI_AVG_COST1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AVG_COST1"].ToString()),
            BMI_AGE_QTY2 = row["BMI_AGE_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_QTY2"].ToString()),
            BMI_AGE_COST2 = row["BMI_AGE_COST2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_COST2"].ToString()),
            BMI_AVG_COST2 = row["BMI_AVG_COST2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AVG_COST2"].ToString()),
            BMI_AGE_QTY3 = row["BMI_AGE_QTY3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_QTY3"].ToString()),
            BMI_AGE_COST3 = row["BMI_AGE_COST3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_COST3"].ToString()),
            BMI_AVG_COST3 = row["BMI_AVG_COST3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AVG_COST3"].ToString()),
            BMI_AGE_QTY4 = row["BMI_AGE_QTY4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_QTY4"].ToString()),
            BMI_AGE_COST4 = row["BMI_AGE_COST4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_COST4"].ToString()),
            BMI_AVG_COST4 = row["BMI_AVG_COST4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AVG_COST4"].ToString()),
            BMI_AGE_QTY5 = row["BMI_AGE_QTY5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_QTY5"].ToString()),
            BMI_AGE_COST5 = row["BMI_AGE_COST5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_COST5"].ToString()),
            BMI_AVG_COST5 = row["BMI_AVG_COST5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AVG_COST5"].ToString()),
            BMI_AGE_QTY6 = row["BMI_AGE_QTY6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_QTY6"].ToString()),
            BMI_AGE_COST6 = row["BMI_AGE_COST6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_COST6"].ToString()),
            BMI_AVG_COST6 = row["BMI_AVG_COST6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AVG_COST6"].ToString()),
            BMI_AGE_QTY_T = row["BMI_AGE_QTY_T"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_QTY_T"].ToString()),
            BMI_AGE_COST_T = row["BMI_AGE_COST_T"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AGE_COST_T"].ToString()),
            BMI_AVG_COST_T = row["BMI_AVG_COST_T"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMI_AVG_COST_T"].ToString()),
            BMI_DOC_TP = row["BMI_DOC_TP"] == DBNull.Value ? string.Empty : row["BMI_DOC_TP"].ToString(),
            BMI_DOC_NO = row["BMI_DOC_NO"] == DBNull.Value ? string.Empty : row["BMI_DOC_NO"].ToString(),
            BMI_DOC_DT = row["BMI_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BMI_DOC_DT"].ToString()),
            BMI_ITEM_LINE = row["BMI_ITEM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMI_ITEM_LINE"].ToString()),
            BMI_LOG_SEQ = row["BMI_LOG_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMI_LOG_SEQ"].ToString())
        };
        }
    }
}
