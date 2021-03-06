using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1
    // Computer :- ITPD11  | User :- suneth On 16-Dec-2014 11:44:00
    //===========================================================================================================

    public class LOG_INTREQSER
    {
        public Int32 Itrs_log_seq { get; set; }

        public Int32 Itrs_seq_no { get; set; }

        public Int32 Itrs_line_no { get; set; }

        public Int32 Itrs_ser_line { get; set; }

        public String Itrs_itm_cd { get; set; }

        public String Itrs_itm_stus { get; set; }

        public String Itrs_ser_1 { get; set; }

        public String Itrs_ser_2 { get; set; }

        public String Itrs_ser_3 { get; set; }

        public String Itrs_ser_4 { get; set; }

        public Decimal Itrs_qty { get; set; }

        public Int32 Itrs_in_seqno { get; set; }

        public String Itrs_in_docno { get; set; }

        public Int32 Itrs_in_itmline { get; set; }

        public Int32 Itrs_in_batchline { get; set; }

        public Int32 Itrs_in_serline { get; set; }

        public DateTime Itrs_in_docdt { get; set; }

        public String Itrs_trns_tp { get; set; }

        public String Itrs_rmk { get; set; }

        public Int32 Itrs_ser_id { get; set; }

        public String Itrs_nitm_stus { get; set; }

        public String Itrs_logtype { get; set; }

        public String Itrs_loguser { get; set; }

        public DateTime Itrs_logdate { get; set; }

        public static LOG_INTREQSER Converter(DataRow row)
        {
            return new LOG_INTREQSER
            {
                Itrs_log_seq = row["ITRS_LOG_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_LOG_SEQ"].ToString()),
                Itrs_seq_no = row["ITRS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SEQ_NO"].ToString()),
                Itrs_line_no = row["ITRS_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_LINE_NO"].ToString()),
                Itrs_ser_line = row["ITRS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SER_LINE"].ToString()),
                Itrs_itm_cd = row["ITRS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRS_ITM_CD"].ToString(),
                Itrs_itm_stus = row["ITRS_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRS_ITM_STUS"].ToString(),
                Itrs_ser_1 = row["ITRS_SER_1"] == DBNull.Value ? string.Empty : row["ITRS_SER_1"].ToString(),
                Itrs_ser_2 = row["ITRS_SER_2"] == DBNull.Value ? string.Empty : row["ITRS_SER_2"].ToString(),
                Itrs_ser_3 = row["ITRS_SER_3"] == DBNull.Value ? string.Empty : row["ITRS_SER_3"].ToString(),
                Itrs_ser_4 = row["ITRS_SER_4"] == DBNull.Value ? string.Empty : row["ITRS_SER_4"].ToString(),
                Itrs_qty = row["ITRS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRS_QTY"].ToString()),
                Itrs_in_seqno = row["ITRS_IN_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_SEQNO"].ToString()),
                Itrs_in_docno = row["ITRS_IN_DOCNO"] == DBNull.Value ? string.Empty : row["ITRS_IN_DOCNO"].ToString(),
                Itrs_in_itmline = row["ITRS_IN_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_ITMLINE"].ToString()),
                Itrs_in_batchline = row["ITRS_IN_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_BATCHLINE"].ToString()),
                Itrs_in_serline = row["ITRS_IN_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_SERLINE"].ToString()),
                Itrs_in_docdt = row["ITRS_IN_DOCDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITRS_IN_DOCDT"].ToString()),
                Itrs_trns_tp = row["ITRS_TRNS_TP"] == DBNull.Value ? string.Empty : row["ITRS_TRNS_TP"].ToString(),
                Itrs_rmk = row["ITRS_RMK"] == DBNull.Value ? string.Empty : row["ITRS_RMK"].ToString(),
                Itrs_ser_id = row["ITRS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SER_ID"].ToString()),
                Itrs_nitm_stus = row["ITRS_NITM_STUS"] == DBNull.Value ? string.Empty : row["ITRS_NITM_STUS"].ToString(),
                Itrs_logtype = row["ITRS_LOGTYPE"] == DBNull.Value ? string.Empty : row["ITRS_LOGTYPE"].ToString(),
                Itrs_loguser = row["ITRS_LOGUSER"] == DBNull.Value ? string.Empty : row["ITRS_LOGUSER"].ToString(),
                Itrs_logdate = row["ITRS_LOGDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITRS_LOGDATE"].ToString())
            };
        }
    }
}