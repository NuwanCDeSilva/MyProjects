using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class MsgInformation
    {
       //Written by Prabhath on 24/08/2012
        //Table : MST_MSG_INFO_BASE

        #region Private Members
        private string _mmi_com;
        private string _mmi_cre_by;
        private DateTime _mmi_cre_dt;
        private Int32 _mmi_dividesectiondays;
        private string _mmi_doc_tp;
        private string _mmi_email;
        private string _mmi_loc;
        private string _mmi_mobi_no;
        private string _mmi_mod_by;
        private DateTime _mmi_mod_dt;
        private string _mmi_msg_tp;
        private Int32 _mmi_pd;
        private string _mmi_receiver;
        private string _mmi_rep_format;
        private Int32 _mmi_send_day;
        private Boolean _mmi_send_spc_time;
        private Int32 _mmi_seq_no;
        private Boolean _mmi_showdividesections;
        private DateTime _mmi_spc_time;
        private string _mmi_sp_msg;
        private string _mmi_stage;
        private Boolean _mmi_stus;
        private string _mmi_superior_mail;
        private Boolean _mmi_with_sr;
        #endregion

        public string Mmi_com { get { return _mmi_com; } set { _mmi_com = value; } }
        public string Mmi_cre_by { get { return _mmi_cre_by; } set { _mmi_cre_by = value; } }
        public DateTime Mmi_cre_dt { get { return _mmi_cre_dt; } set { _mmi_cre_dt = value; } }
        public Int32 Mmi_dividesectiondays { get { return _mmi_dividesectiondays; } set { _mmi_dividesectiondays = value; } }
        public string Mmi_doc_tp { get { return _mmi_doc_tp; } set { _mmi_doc_tp = value; } }
        public string Mmi_email { get { return _mmi_email; } set { _mmi_email = value; } }
        public string Mmi_loc { get { return _mmi_loc; } set { _mmi_loc = value; } }
        public string Mmi_mobi_no { get { return _mmi_mobi_no; } set { _mmi_mobi_no = value; } }
        public string Mmi_mod_by { get { return _mmi_mod_by; } set { _mmi_mod_by = value; } }
        public DateTime Mmi_mod_dt { get { return _mmi_mod_dt; } set { _mmi_mod_dt = value; } }
        public string Mmi_msg_tp { get { return _mmi_msg_tp; } set { _mmi_msg_tp = value; } }
        public Int32 Mmi_pd { get { return _mmi_pd; } set { _mmi_pd = value; } }
        public string Mmi_receiver { get { return _mmi_receiver; } set { _mmi_receiver = value; } }
        public string Mmi_rep_format { get { return _mmi_rep_format; } set { _mmi_rep_format = value; } }
        public Int32 Mmi_send_day { get { return _mmi_send_day; } set { _mmi_send_day = value; } }
        public Boolean Mmi_send_spc_time { get { return _mmi_send_spc_time; } set { _mmi_send_spc_time = value; } }
        public Int32 Mmi_seq_no { get { return _mmi_seq_no; } set { _mmi_seq_no = value; } }
        public Boolean Mmi_showdividesections { get { return _mmi_showdividesections; } set { _mmi_showdividesections = value; } }
        public DateTime Mmi_spc_time { get { return _mmi_spc_time; } set { _mmi_spc_time = value; } }
        public string Mmi_sp_msg { get { return _mmi_sp_msg; } set { _mmi_sp_msg = value; } }
        public string Mmi_stage { get { return _mmi_stage; } set { _mmi_stage = value; } }
        public Boolean Mmi_stus { get { return _mmi_stus; } set { _mmi_stus = value; } }
        public string Mmi_superior_mail { get { return _mmi_superior_mail; } set { _mmi_superior_mail = value; } }
        public Boolean Mmi_with_sr { get { return _mmi_with_sr; } set { _mmi_with_sr = value; } }

        public static MsgInformation Converter(DataRow row)
        {
            return new MsgInformation
            {
                Mmi_com = row["MMI_COM"] == DBNull.Value ? string.Empty : row["MMI_COM"].ToString(),
                Mmi_cre_by = row["MMI_CRE_BY"] == DBNull.Value ? string.Empty : row["MMI_CRE_BY"].ToString(),
                Mmi_cre_dt = row["MMI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MMI_CRE_DT"]),
                Mmi_dividesectiondays = row["MMI_DIVIDESECTIONDAYS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMI_DIVIDESECTIONDAYS"]),
                Mmi_doc_tp = row["MMI_DOC_TP"] == DBNull.Value ? string.Empty : row["MMI_DOC_TP"].ToString(),
                Mmi_email = row["MMI_EMAIL"] == DBNull.Value ? string.Empty : row["MMI_EMAIL"].ToString(),
                Mmi_loc = row["MMI_LOC"] == DBNull.Value ? string.Empty : row["MMI_LOC"].ToString(),
                Mmi_mobi_no = row["MMI_MOBI_NO"] == DBNull.Value ? string.Empty : row["MMI_MOBI_NO"].ToString(),
                Mmi_mod_by = row["MMI_MOD_BY"] == DBNull.Value ? string.Empty : row["MMI_MOD_BY"].ToString(),
                Mmi_mod_dt = row["MMI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MMI_MOD_DT"]),
                Mmi_msg_tp = row["MMI_MSG_TP"] == DBNull.Value ? string.Empty : row["MMI_MSG_TP"].ToString(),
                Mmi_pd = row["MMI_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMI_PD"]),
                Mmi_receiver = row["MMI_RECEIVER"] == DBNull.Value ? string.Empty : row["MMI_RECEIVER"].ToString(),
                Mmi_rep_format = row["MMI_REP_FORMAT"] == DBNull.Value ? string.Empty : row["MMI_REP_FORMAT"].ToString(),
                Mmi_send_day = row["MMI_SEND_DAY"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMI_SEND_DAY"]),
                Mmi_send_spc_time = row["MMI_SEND_SPC_TIME"] == DBNull.Value ? false : Convert.ToBoolean(row["MMI_SEND_SPC_TIME"]),
                Mmi_seq_no = row["MMI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMI_SEQ_NO"]),
                Mmi_showdividesections = row["MMI_SHOWDIVIDESECTIONS"] == DBNull.Value ? false : Convert.ToBoolean(row["MMI_SHOWDIVIDESECTIONS"]),
                Mmi_spc_time = row["MMI_SPC_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MMI_SPC_TIME"]),
                Mmi_sp_msg = row["MMI_SP_MSG"] == DBNull.Value ? string.Empty : row["MMI_SP_MSG"].ToString(),
                Mmi_stage = row["MMI_STAGE"] == DBNull.Value ? string.Empty : row["MMI_STAGE"].ToString(),
                Mmi_stus = row["MMI_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["MMI_STUS"]),
                Mmi_superior_mail = row["MMI_SUPERIOR_MAIL"] == DBNull.Value ? string.Empty : row["MMI_SUPERIOR_MAIL"].ToString(),
                Mmi_with_sr = row["MMI_WITH_SR"] == DBNull.Value ? false : Convert.ToBoolean(row["MMI_WITH_SR"])

            };
        }
    }
}

