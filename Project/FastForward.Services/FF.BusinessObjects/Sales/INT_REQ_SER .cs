using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- ITPD12  | User :- pemil On 11-Aug-2015 12:27:48
    //===========================================================================================================
    [Serializable]
    public class INT_REQ_SER
    {
        public Int32 ITRS_SEQ_NO { get; set; }
        public Int32 ITRS_LINE_NO { get; set; }
        public Int32 ITRS_SER_LINE { get; set; }
        public String ITRS_ITM_CD { get; set; }
        public String ITRS_ITM_STUS { get; set; }
        public String ITRS_SER_1 { get; set; }
        public String ITRS_SER_2 { get; set; }
        public String ITRS_SER_3 { get; set; }
        public String ITRS_SER_4 { get; set; }
        public Decimal ITRS_QTY { get; set; }
        public Int32 ITRS_IN_SEQNO { get; set; }
        public String ITRS_IN_DOCNO { get; set; }
        public Int32 ITRS_IN_ITMLINE { get; set; }
        public Int32 ITRS_IN_BATCHLINE { get; set; }
        public Int32 ITRS_IN_SERLINE { get; set; }
        public DateTime ITRS_IN_DOCDT { get; set; }
        public String ITRS_TRNS_TP { get; set; }
        public String ITRS_RMK { get; set; }
        public Int32 ITRS_SER_ID { get; set; }
        public String ITRS_NITM_STUS { get; set; }
        public String ITRS_ITM_model { get; set; }
        public String ITRS_NITM_SER1 { get; set; }
        public String ITRS_ITM_NEW_CD { get; set; } 
        public static INT_REQ_SER Converter(DataRow row)
        {
            return new INT_REQ_SER
            {
                ITRS_SEQ_NO = row["ITRS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SEQ_NO"].ToString()),
                ITRS_LINE_NO = row["ITRS_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_LINE_NO"].ToString()),
                ITRS_SER_LINE = row["ITRS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SER_LINE"].ToString()),
                ITRS_ITM_CD = row["ITRS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRS_ITM_CD"].ToString(),
                ITRS_ITM_STUS = row["ITRS_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRS_ITM_STUS"].ToString(),
                ITRS_SER_1 = row["ITRS_SER_1"] == DBNull.Value ? string.Empty : row["ITRS_SER_1"].ToString(),
                ITRS_SER_2 = row["ITRS_SER_2"] == DBNull.Value ? string.Empty : row["ITRS_SER_2"].ToString(),
                ITRS_SER_3 = row["ITRS_SER_3"] == DBNull.Value ? string.Empty : row["ITRS_SER_3"].ToString(),
                ITRS_SER_4 = row["ITRS_SER_4"] == DBNull.Value ? string.Empty : row["ITRS_SER_4"].ToString(),
                ITRS_QTY = row["ITRS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRS_QTY"].ToString()),
                ITRS_IN_SEQNO = row["ITRS_IN_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_SEQNO"].ToString()),
                ITRS_IN_DOCNO = row["ITRS_IN_DOCNO"] == DBNull.Value ? string.Empty : row["ITRS_IN_DOCNO"].ToString(),
                ITRS_IN_ITMLINE = row["ITRS_IN_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_ITMLINE"].ToString()),
                ITRS_IN_BATCHLINE = row["ITRS_IN_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_BATCHLINE"].ToString()),
                ITRS_IN_SERLINE = row["ITRS_IN_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_SERLINE"].ToString()),
                ITRS_IN_DOCDT = row["ITRS_IN_DOCDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITRS_IN_DOCDT"].ToString()),
                ITRS_TRNS_TP = row["ITRS_TRNS_TP"] == DBNull.Value ? string.Empty : row["ITRS_TRNS_TP"].ToString(),
                ITRS_RMK = row["ITRS_RMK"] == DBNull.Value ? string.Empty : row["ITRS_RMK"].ToString(),
                ITRS_SER_ID = row["ITRS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SER_ID"].ToString()),
                ITRS_NITM_STUS = row["ITRS_NITM_STUS"] == DBNull.Value ? string.Empty : row["ITRS_NITM_STUS"].ToString()
            };
        }
        public static INT_REQ_SER ConverterNEW(DataRow row)
        {
            return new INT_REQ_SER
            {
                ITRS_SEQ_NO = row["ITRS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SEQ_NO"].ToString()),
                ITRS_LINE_NO = row["ITRS_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_LINE_NO"].ToString()),
                ITRS_SER_LINE = row["ITRS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SER_LINE"].ToString()),
                ITRS_ITM_CD = row["ITRS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRS_ITM_CD"].ToString(),
                ITRS_ITM_STUS = row["ITRS_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRS_ITM_STUS"].ToString(),
                ITRS_SER_1 = row["ITRS_SER_1"] == DBNull.Value ? string.Empty : row["ITRS_SER_1"].ToString(),
                ITRS_SER_2 = row["ITRS_SER_2"] == DBNull.Value ? string.Empty : row["ITRS_SER_2"].ToString(),
                ITRS_SER_3 = row["ITRS_SER_3"] == DBNull.Value ? string.Empty : row["ITRS_SER_3"].ToString(),
                ITRS_SER_4 = row["ITRS_SER_4"] == DBNull.Value ? string.Empty : row["ITRS_SER_4"].ToString(),
                ITRS_QTY = row["ITRS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRS_QTY"].ToString()),
                ITRS_IN_SEQNO = row["ITRS_IN_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_SEQNO"].ToString()),
                ITRS_IN_DOCNO = row["ITRS_IN_DOCNO"] == DBNull.Value ? string.Empty : row["ITRS_IN_DOCNO"].ToString(),
                ITRS_IN_ITMLINE = row["ITRS_IN_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_ITMLINE"].ToString()),
                ITRS_IN_BATCHLINE = row["ITRS_IN_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_BATCHLINE"].ToString()),
                ITRS_IN_SERLINE = row["ITRS_IN_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_SERLINE"].ToString()),
                ITRS_IN_DOCDT = row["ITRS_IN_DOCDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITRS_IN_DOCDT"].ToString()),
                ITRS_TRNS_TP = row["ITRS_TRNS_TP"] == DBNull.Value ? string.Empty : row["ITRS_TRNS_TP"].ToString(),
                ITRS_RMK = row["ITRS_RMK"] == DBNull.Value ? string.Empty : row["ITRS_RMK"].ToString(),
                ITRS_SER_ID = row["ITRS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SER_ID"].ToString()),
                ITRS_NITM_STUS = row["ITRS_NITM_STUS"] == DBNull.Value ? string.Empty : row["ITRS_NITM_STUS"].ToString(),
                ITRS_ITM_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString()
            };
        }
    }
}

