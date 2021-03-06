using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- ITPD11  | User :- suneth On 20-Jan-2015 11:29:54
    //===========================================================================================================

    public class INT_REQ_ITM
    {
        public Int32 Itri_seq_no { get; set; }
        public Int32 Itri_line_no { get; set; }
        public String Itri_itm_cd { get; set; }
        public String Itri_itm_stus { get; set; }
        public Decimal Itri_qty { get; set; }
        public Decimal Itri_unit_price { get; set; }
        public Decimal Itri_app_qty { get; set; }
        public String Itri_res_no { get; set; }
        public String Itri_note { get; set; }
        public String Itri_mitm_cd { get; set; }
        public String Itri_mitm_stus { get; set; }
        public Decimal Itri_mqty { get; set; }
        public Decimal Itri_bqty { get; set; }
        public String Itri_itm_cond { get; set; }
        public String Itri_job_no { get; set; }
        public Int32 Itri_job_line { get; set; }
        public String MI_LONGDESC { get; set; }

        public static INT_REQ_ITM Converter(DataRow row)
        {
            return new INT_REQ_ITM
            {
                Itri_seq_no = row["ITRI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_SEQ_NO"].ToString()),
                Itri_line_no = row["ITRI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_LINE_NO"].ToString()),
                Itri_itm_cd = row["ITRI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_ITM_CD"].ToString(),
                Itri_itm_stus = row["ITRI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_ITM_STUS"].ToString(),
                Itri_qty = row["ITRI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_QTY"].ToString()),
                Itri_unit_price = row["ITRI_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_UNIT_PRICE"].ToString()),
                Itri_app_qty = row["ITRI_APP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_APP_QTY"].ToString()),
                Itri_res_no = row["ITRI_RES_NO"] == DBNull.Value ? string.Empty : row["ITRI_RES_NO"].ToString(),
                Itri_note = row["ITRI_NOTE"] == DBNull.Value ? string.Empty : row["ITRI_NOTE"].ToString(),
                Itri_mitm_cd = row["ITRI_MITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_MITM_CD"].ToString(),
                Itri_mitm_stus = row["ITRI_MITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_MITM_STUS"].ToString(),
                Itri_mqty = row["ITRI_MQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_MQTY"].ToString()),
                Itri_bqty = row["ITRI_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_BQTY"].ToString()),
                Itri_itm_cond = row["ITRI_ITM_COND"] == DBNull.Value ? string.Empty : row["ITRI_ITM_COND"].ToString(),
                Itri_job_no = row["ITRI_JOB_NO"] == DBNull.Value ? string.Empty : row["ITRI_JOB_NO"].ToString(),
                Itri_job_line = row["ITRI_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_JOB_LINE"].ToString()),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString()
            };
        }
    }
}

