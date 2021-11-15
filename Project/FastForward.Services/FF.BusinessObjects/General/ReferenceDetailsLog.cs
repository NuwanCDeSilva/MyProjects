using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ReferenceDetailsLog
    {
        public String GRAD_REF { get; set; }
        public Int32 GRAD_LINE { get; set; }
        public String GRAD_REQ_PARAM { get; set; }
        public Int32 GRAD_VAL1 { get; set; }
        public decimal GRAD_VAL2 { get; set; }
        public decimal GRAD_VAL3 { get; set; }
        public decimal GRAD_VAL4 { get; set; }
        public decimal GRAD_VAL5 { get; set; }
        public String GRAD_ANAL1 { get; set; }
        public String GRAD_ANAL2 { get; set; }
        public String GRAD_ANAL3 { get; set; }
        public String GRAD_ANAL4 { get; set; }
        public String GRAD_ANAL5 { get; set; }
        public DateTime GRAD_DATE_PARAM { get; set; }
        public Decimal GRAD_IS_RT1 { get; set; }
        public Decimal GRAD_IS_RT2 { get; set; }
        public Int32 GRAD_LVL { get; set; }
        public String GRAD_ANAL6 { get; set; }
        public String GRAD_ANAL7 { get; set; }
        public String GRAD_ANAL8 { get; set; }
        public String GRAD_ANAL9 { get; set; }
        public String GRAD_ANAL10 { get; set; }
        public String GRAD_ANAL11 { get; set; }
        public String GRAD_ANAL12 { get; set; }
        public String GRAD_ANAL13 { get; set; }
        public String GRAD_ANAL14 { get; set; }
        public String GRAD_ANAL15 { get; set; }
        public Int32 GRAD_REF_LINE_NO { get; set; }
        public Int32 GRAD_SEQ { get; set; }
        public Decimal GRAD_ANAL16 { get; set; }
        public Decimal GRAD_ANAL17 { get; set; }
        public Decimal GRAD_ANAL18 { get; set; }
        public Int32 GRAD_RCV_FREE_ITM { get; set; }
        public static ReferenceDetailsLog Converter(DataRow row)
        {
            return new ReferenceDetailsLog
            { 
                GRAD_REF = row["GRAD_REF"] == DBNull.Value ? string.Empty : row["GRAD_REF"].ToString(),
                GRAD_LINE = row["GRAD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_LINE"].ToString()),
                GRAD_REQ_PARAM = row["GRAD_REQ_PARAM"] == DBNull.Value ? string.Empty : row["GRAD_REQ_PARAM"].ToString(),
                GRAD_VAL1 = row["GRAD_VAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_VAL1"].ToString()),
                GRAD_VAL2 = row["GRAD_VAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL2"].ToString()),
                GRAD_VAL3 = row["GRAD_VAL3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL3"].ToString()),
                GRAD_VAL4 = row["GRAD_VAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL4"].ToString()),
                GRAD_VAL5 = row["GRAD_VAL5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL5"].ToString()),
                GRAD_ANAL1 = row["GRAD_ANAL1"] == DBNull.Value ? string.Empty : row["GRAD_ANAL1"].ToString(),
                GRAD_ANAL2 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
                GRAD_ANAL3 = row["GRAD_ANAL3"] == DBNull.Value ? string.Empty : row["GRAD_ANAL3"].ToString(),
                GRAD_ANAL4 = row["GRAD_ANAL4"] == DBNull.Value ? string.Empty : row["GRAD_ANAL4"].ToString(),
                GRAD_ANAL5 = row["GRAD_ANAL5"] == DBNull.Value ? string.Empty : row["GRAD_ANAL5"].ToString(),
                GRAD_DATE_PARAM = row["GRAD_DATE_PARAM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAD_DATE_PARAM"].ToString()),
                GRAD_IS_RT1 = row["GRAD_IS_RT1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_IS_RT1"].ToString()),
                GRAD_IS_RT2 = row["GRAD_IS_RT2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_IS_RT2"].ToString()),
                GRAD_LVL = row["GRAD_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_LVL"].ToString()),
                GRAD_ANAL6 = row["GRAD_ANAL6"] == DBNull.Value ? string.Empty : row["GRAD_ANAL6"].ToString(),
                GRAD_ANAL7 = row["GRAD_ANAL7"] == DBNull.Value ? string.Empty : row["GRAD_ANAL7"].ToString(),
                GRAD_ANAL8 = row["GRAD_ANAL8"] == DBNull.Value ? string.Empty : row["GRAD_ANAL8"].ToString(),
                GRAD_ANAL9 = row["GRAD_ANAL9"] == DBNull.Value ? string.Empty : row["GRAD_ANAL9"].ToString(),
                GRAD_ANAL10 = row["GRAD_ANAL10"] == DBNull.Value ? string.Empty : row["GRAD_ANAL10"].ToString(),
                GRAD_ANAL11 = row["GRAD_ANAL11"] == DBNull.Value ? string.Empty : row["GRAD_ANAL11"].ToString(),
                GRAD_ANAL12 = row["GRAD_ANAL12"] == DBNull.Value ? string.Empty : row["GRAD_ANAL12"].ToString(),
                GRAD_ANAL13 = row["GRAD_ANAL13"] == DBNull.Value ? string.Empty : row["GRAD_ANAL13"].ToString(),
                GRAD_ANAL14 = row["GRAD_ANAL14"] == DBNull.Value ? string.Empty : row["GRAD_ANAL14"].ToString(),
                GRAD_ANAL15 = row["GRAD_ANAL15"] == DBNull.Value ? string.Empty : row["GRAD_ANAL15"].ToString(),
                GRAD_REF_LINE_NO = row["GRAD_REF_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_REF_LINE_NO"].ToString()),
                GRAD_SEQ = row["GRAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_SEQ"].ToString()),
                GRAD_ANAL16 = row["GRAD_ANAL16"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL16"].ToString()),
                GRAD_ANAL17 = row["GRAD_ANAL17"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL17"].ToString()),
                GRAD_ANAL18 = row["GRAD_ANAL18"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL18"].ToString()),
                GRAD_RCV_FREE_ITM = row["GRAD_RCV_FREE_ITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_RCV_FREE_ITM"].ToString())
            };
        } 
    }
}
