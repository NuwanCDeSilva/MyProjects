using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class CctTransLog
    {
        public int Sctl_seq_no { get; set; }
        public string Sctl_com_code { get; set; }
        public string Sctl_pc { get; set; }
        public DateTime Sctl_Date { get; set; }
        public string Sctl_cus_name { get; set; }
        public string Sctl_crd_no { get; set; }
        public string  Sctl_crd_tp { get; set; }
        public decimal Sctl_app_amt { get; set; }
        public string Sctl_bnk_cd { get; set; }
        public string Sctl_app_cd { get; set; }
        public string Sctl_rrn { get; set; }
        public string Sctl_trans_trace { get; set; }
        public string Sctl_batch_no { get; set; }
        public string Sctl_host_no { get; set; }
        public string Sctl_termianl_id { get; set; }
        public string Sctl_mer_id { get; set; }
        public string Sctl_aid { get; set; }
        public string Sctl_trans_crypto { get; set; }
        public string Sctl_trans_seq { get; set; }
        public string Sctl_Inv_no { get; set; }
        public Int32 Sctl_is_processed { get; set; }
        public string Sctl_cre_by { get; set; }
        public DateTime Sctl_cre_date { get; set; }
        public string Sctl_session_id { get; set; }

        public static CctTransLog Converter(DataRow row)
        {
            return new CctTransLog
            {
                Sctl_seq_no = row["SCTL_SEQ_NO"] == DBNull.Value ? 0: Convert.ToInt32( row["SCTL_SEQ_NO"].ToString()),
                Sctl_com_code = row["SCTL_COM"] == DBNull.Value ? string.Empty : row["SCTL_COM"].ToString(),
                Sctl_pc = row["SCTL_PC"] == DBNull.Value ? string.Empty : row["SCTL_PC"].ToString(),
                Sctl_Date = row["SCTL_DATE"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime( row["SCTL_DATE"].ToString()),
                Sctl_cus_name = row["SCTL_CUS_NAME"] == DBNull.Value ? string.Empty : row["SCTL_CUS_NAME"].ToString(),
                Sctl_crd_no = row["SCTL_CRD_NO"] == DBNull.Value ? string.Empty : row["SCTL_CRD_NO"].ToString(),
                Sctl_crd_tp = row["SCTL_CRD_TP"] == DBNull.Value ? string.Empty : row["SCTL_CRD_TP"].ToString(),
                Sctl_app_amt = row["SCTL_APP_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal( row["SCTL_APP_AMT"].ToString()),
                Sctl_bnk_cd = row["SCTL_BNK_CD"] == DBNull.Value ? string.Empty : row["SCTL_BNK_CD"].ToString(),
                Sctl_app_cd = row["SCTL_APP_CD"] == DBNull.Value ? string.Empty : row["SCTL_APP_CD"].ToString(),
                Sctl_rrn = row["SCTL_RRN"] == DBNull.Value ? string.Empty : row["SCTL_RRN"].ToString(),
                Sctl_trans_trace = row["SCTL_TRAN_TRACE"] == DBNull.Value ? string.Empty : row["SCTL_TRAN_TRACE"].ToString(),
                Sctl_batch_no = row["SCTL_BATCH_NO"] == DBNull.Value ? string.Empty : row["SCTL_BATCH_NO"].ToString(),
                Sctl_host_no = row["SCTL_HOST_NO"] == DBNull.Value ? string.Empty : row["SCTL_HOST_NO"].ToString(),
                Sctl_termianl_id = row["SCTL_TER_ID"] == DBNull.Value ? string.Empty : row["SCTL_TER_ID"].ToString(),
                Sctl_mer_id = row["SCTL_MERCH_ID"] == DBNull.Value ? string.Empty : row["SCTL_MERCH_ID"].ToString(),
                Sctl_aid = row["SCTL_AID"] == DBNull.Value ? string.Empty : row["SCTL_AID"].ToString(),
                Sctl_trans_crypto = row["SCTL_TC"] == DBNull.Value ? string.Empty : row["SCTL_TC"].ToString(),
                Sctl_trans_seq = row["SCTL_TRN_SEQ"] == DBNull.Value ? string.Empty : row["SCTL_TRN_SEQ"].ToString(),
                Sctl_Inv_no = row["SCTL_INV_NO"] == DBNull.Value ? string.Empty : row["SCTL_INV_NO"].ToString(),
                Sctl_is_processed = row["SCTL_IS_PROCESSED"] == DBNull.Value ? 0 :Convert.ToInt32( row["SCTL_IS_PROCESSED"].ToString()),
                Sctl_cre_by = row["SCTL_CRE_BY"] == DBNull.Value ? string.Empty : row["SCTL_CRE_BY"].ToString(),
                Sctl_cre_date = row["SCTL_CRE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime( row["SCTL_CRE_DATE"].ToString()),
                Sctl_session_id = row["SCTL_SESSION_ID"] == DBNull.Value ? string.Empty : row["SCTL_SESSION_ID"].ToString()
            };
        }
    }
}
