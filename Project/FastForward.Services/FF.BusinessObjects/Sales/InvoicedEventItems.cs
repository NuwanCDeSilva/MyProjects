using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class InvoicedEventItems
    {
        public Int32 SERE_ID { get; set; }
        public string SERE_EVE_CD { get; set; }
        public Int32 SERE_LINE { get; set; }
        public Int32 SERE_EVEINVC_LINE { get; set; }
        public string SERE_ITM_CD { get; set; }
        public string SERE_COM { get; set; }
        public string SERE_PC { get; set; }
        public string SERE_INVC_NO { get; set; }
        public string SERE_INVC_LINE { get; set; }
        public decimal SERE_INVC_QTY { get; set; }
        public string SERE_CRE_BY { get; set; }
        public DateTime SERE_CRE_DT { get; set; }
        public string SERE_CRE_SESSION { get; set; }
        public Int32 SERE_IS_ACT { get; set; }
        public static InvoicedEventItems Converter(DataRow row)
        {
            return new InvoicedEventItems
            {
                SERE_ID = row["SERE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_ID"].ToString()),
                SERE_EVE_CD = row["SERE_EVE_CD"] == DBNull.Value ? string.Empty : row["SERE_EVE_CD"].ToString(),
                SERE_LINE = row["SERE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_LINE"].ToString()),
                SERE_EVEINVC_LINE = row["SERE_EVEINVC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_EVEINVC_LINE"].ToString()),
                SERE_ITM_CD = row["SERE_ITM_CD"] == DBNull.Value ? string.Empty : row["SERE_ITM_CD"].ToString(),
                SERE_COM = row["SERE_COM"] == DBNull.Value ? string.Empty : row["SERE_COM"].ToString(),
                SERE_PC = row["SERE_PC"] == DBNull.Value ? string.Empty : row["SERE_PC"].ToString(),
                SERE_INVC_NO = row["SERE_INVC_NO"] == DBNull.Value ? string.Empty : row["SERE_INVC_NO"].ToString(),
                SERE_INVC_LINE = row["SERE_INVC_LINE"] == DBNull.Value ? string.Empty : row["SERE_INVC_LINE"].ToString(),
                SERE_INVC_QTY = row["SERE_INVC_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SERE_INVC_QTY"].ToString()),
                SERE_CRE_BY = row["SERE_CRE_BY"] == DBNull.Value ? string.Empty : row["SERE_CRE_BY"].ToString(),
                SERE_CRE_DT = row["SERE_CRE_DT"] == DBNull.Value ? DateTime.MinValue.Date : Convert.ToDateTime(row["SERE_CRE_DT"].ToString()),
                SERE_CRE_SESSION = row["SERE_CRE_SESSION"] == DBNull.Value ? string.Empty : row["SERE_CRE_SESSION"].ToString(),
                SERE_IS_ACT = row["SERE_IS_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_IS_ACT"].ToString())
            };
        }
    }
}
