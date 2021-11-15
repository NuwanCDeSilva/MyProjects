using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 16-Mar-2015 09:25:59
    //===========================================================================================================

    public class Service_Message
    {
        public Int32 Sm_seq { get; set; }
        public String Sm_jobno { get; set; }
        public Int32 Sm_joboline { get; set; }
        public Decimal Sm_jobstage { get; set; }
        public Int32 Sm_msg_tmlt_id { get; set; }
        public Int32 Sm_status { get; set; }
        public String Sm_com { get; set; }
        public String Sm_ref_num { get; set; }
        public String Sm_cre_by { get; set; }
        public DateTime Sm_cre_dt { get; set; }
        public String Sm_mod_by { get; set; }
        public DateTime Sm_mod_dt { get; set; }
        public String Sm_sms_text { get; set; }
        public Int32 Sm_sms_gap { get; set; }
        public Int32 Sm_sms_done { get; set; }
        public String Sm_mail_text { get; set; }
        public Int32 Sm_mail_gap { get; set; }
        public Int32 Sm_email_done { get; set; }
        public String Sm_email { get; set; }


        public static Service_Message Converter(DataRow row)
        {
            return new Service_Message
            {
                Sm_seq = row["SM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SM_SEQ"].ToString()),
                Sm_jobno = row["SM_JOBNO"] == DBNull.Value ? string.Empty : row["SM_JOBNO"].ToString(),
                Sm_joboline = row["SM_JOBOLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SM_JOBOLINE"].ToString()),
                Sm_jobstage = row["SM_JOBSTAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SM_JOBSTAGE"].ToString()),
                Sm_msg_tmlt_id = row["SM_MSG_TMLT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SM_MSG_TMLT_ID"].ToString()),
                Sm_status = row["SM_STATUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SM_STATUS"].ToString()),
                Sm_com = row["SM_COM"] == DBNull.Value ? string.Empty : row["SM_COM"].ToString(),
                Sm_ref_num = row["SM_REF_NUM"] == DBNull.Value ? string.Empty : row["SM_REF_NUM"].ToString(),
                Sm_cre_by = row["SM_CRE_BY"] == DBNull.Value ? string.Empty : row["SM_CRE_BY"].ToString(),
                Sm_cre_dt = row["SM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SM_CRE_DT"].ToString()),
                Sm_mod_by = row["SM_MOD_BY"] == DBNull.Value ? string.Empty : row["SM_MOD_BY"].ToString(),
                Sm_mod_dt = row["SM_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SM_MOD_DT"].ToString()),
                Sm_sms_text = row["SM_SMS_TEXT"] == DBNull.Value ? string.Empty : row["SM_SMS_TEXT"].ToString(),
                Sm_sms_gap = row["SM_SMS_GAP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SM_SMS_GAP"].ToString()),
                Sm_sms_done = row["SM_SMS_DONE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SM_SMS_DONE"].ToString()),
                Sm_mail_text = row["SM_MAIL_TEXT"] == DBNull.Value ? string.Empty : row["SM_MAIL_TEXT"].ToString(),
                Sm_mail_gap = row["SM_MAIL_GAP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SM_MAIL_GAP"].ToString()),
                Sm_email_done = row["SM_EMAIL_DONE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SM_EMAIL_DONE"].ToString()),
                Sm_email = row["SM_EMAIL"] == DBNull.Value ? string.Empty : row["SM_EMAIL"].ToString()
            };
        }
    }
}
