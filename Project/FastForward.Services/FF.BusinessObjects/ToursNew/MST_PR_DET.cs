using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_PR_DET
    {
        public Int32 PD_SEQ_NO { get; set; }
        public Int32 PD_LINE_NO { get; set; }
        public String PD_ITM_CD { get; set; }
        public String PD_ITM_STUS { get; set; }
        public String PD_ITM_TP { get; set; }
        public decimal PD_QTY { get; set; }
        public String PD_UOM { get; set; }
        public decimal PD_UNIT_PRICE { get; set; }
        public Int32 PD_LINE_VAL { get; set; }
        public Int32 PD_DIS_RT { get; set; }
        public Int32 PD_DIS_AMT { get; set; }
        public Int32 PD_LINE_TAX { get; set; }
        public Int32 PD_LINE_AMT { get; set; }
        public Int32 PD_PI_BAL { get; set; }
        public Int32 PD_LC_BAL { get; set; }
        public Int32 PD_SI_BAL { get; set; }
        public Int32 PD_GRN_BAL { get; set; }
        public String PD_REF_NO { get; set; }
        public Int32 PD_ACT_UNIT_PRICE { get; set; }
        public String PD_ITEM_DESC { get; set; }
        public Int32 PD_KIT_LINE_NO { get; set; }
        public String PD_KIT_ITM_CD { get; set; }
        public Int32 PD_VAT { get; set; }
        public Int32 PD_NBT { get; set; }
        public Int32 PD_VAT_BEFORE { get; set; }
        public Int32 PD_NBT_BEFORE { get; set; }
        public Int32 PD_TOT_TAX_BEFORE { get; set; }
        public static MST_PR_DET Converter(DataRow row)
        {
            return new MST_PR_DET
            {
                PD_SEQ_NO = row["PD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_SEQ_NO"].ToString()),
                PD_LINE_NO = row["PD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_LINE_NO"].ToString()),
                PD_ITM_CD = row["PD_ITM_CD"] == DBNull.Value ? string.Empty : row["PD_ITM_CD"].ToString(),
                PD_ITM_STUS = row["PD_ITM_STUS"] == DBNull.Value ? string.Empty : row["PD_ITM_STUS"].ToString(),
                PD_ITM_TP = row["PD_ITM_TP"] == DBNull.Value ? string.Empty : row["PD_ITM_TP"].ToString(),
                PD_QTY = row["PD_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_QTY"].ToString()),
                PD_UOM = row["PD_UOM"] == DBNull.Value ? string.Empty : row["PD_UOM"].ToString(),
                PD_UNIT_PRICE = row["PD_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_UNIT_PRICE"].ToString()),
                PD_LINE_VAL = row["PD_LINE_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_LINE_VAL"].ToString()),
                PD_DIS_RT = row["PD_DIS_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_DIS_RT"].ToString()),
                PD_DIS_AMT = row["PD_DIS_AMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_DIS_AMT"].ToString()),
                PD_LINE_TAX = row["PD_LINE_TAX"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_LINE_TAX"].ToString()),
                PD_LINE_AMT = row["PD_LINE_AMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_LINE_AMT"].ToString()),
                PD_PI_BAL = row["PD_PI_BAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_PI_BAL"].ToString()),
                PD_LC_BAL = row["PD_LC_BAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_LC_BAL"].ToString()),
                PD_SI_BAL = row["PD_SI_BAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_SI_BAL"].ToString()),
                PD_GRN_BAL = row["PD_GRN_BAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_GRN_BAL"].ToString()),
                PD_REF_NO = row["PD_REF_NO"] == DBNull.Value ? string.Empty : row["PD_REF_NO"].ToString(),
                PD_ACT_UNIT_PRICE = row["PD_ACT_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_ACT_UNIT_PRICE"].ToString()),
                PD_ITEM_DESC = row["PD_ITEM_DESC"] == DBNull.Value ? string.Empty : row["PD_ITEM_DESC"].ToString(),
                PD_KIT_LINE_NO = row["PD_KIT_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_KIT_LINE_NO"].ToString()),
                PD_KIT_ITM_CD = row["PD_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["PD_KIT_ITM_CD"].ToString(),
                PD_VAT = row["PD_VAT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_VAT"].ToString()),
                PD_NBT = row["PD_NBT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_NBT"].ToString()),
                PD_VAT_BEFORE = row["PD_VAT_BEFORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_VAT_BEFORE"].ToString()),
                PD_NBT_BEFORE = row["PD_NBT_BEFORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_NBT_BEFORE"].ToString()),
                PD_TOT_TAX_BEFORE = row["PD_TOT_TAX_BEFORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["PD_TOT_TAX_BEFORE"].ToString())
            };
        } 
    }
}
