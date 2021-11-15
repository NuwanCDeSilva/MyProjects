using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Reprint_Docs
    {
        #region Private Members
        private DateTime _drp_app_dt;
        private DateTime _drp_can_dt;
        private DateTime _drp_doc_dt;
        private string _drp_doc_no;
        private Int32 _drp_is_add_pending;
        private string _drp_loc;
        private Int32 _drp_printed;
        private DateTime _drp_print_dt;
        private string _drp_reason;
        private DateTime _drp_rej_dt;
        private DateTime _drp_req_dt;
        private Int32 _drp_seq_no;
        private string _drp_stus;
        private string _drp_stus_change_by;
        private string _drp_tp;

        #endregion

        #region public property definition
        public DateTime Drp_app_dt
        {
            get { return _drp_app_dt; }
            set { _drp_app_dt = value; }
        }
        public DateTime Drp_can_dt
        {
            get { return _drp_can_dt; }
            set { _drp_can_dt = value; }
        }
        public DateTime Drp_doc_dt
        {
            get { return _drp_doc_dt; }
            set { _drp_doc_dt = value; }
        }
        public string Drp_doc_no
        {
            get { return _drp_doc_no; }
            set { _drp_doc_no = value; }
        }
        public Int32 Drp_is_add_pending
        {
            get { return _drp_is_add_pending; }
            set { _drp_is_add_pending = value; }
        }
        public string Drp_loc
        {
            get { return _drp_loc; }
            set { _drp_loc = value; }
        }
        public Int32 Drp_printed
        {
            get { return _drp_printed; }
            set { _drp_printed = value; }
        }
        public DateTime Drp_print_dt
        {
            get { return _drp_print_dt; }
            set { _drp_print_dt = value; }
        }
        public string Drp_reason
        {
            get { return _drp_reason; }
            set { _drp_reason = value; }
        }
        public DateTime Drp_rej_dt
        {
            get { return _drp_rej_dt; }
            set { _drp_rej_dt = value; }
        }
        public DateTime Drp_req_dt
        {
            get { return _drp_req_dt; }
            set { _drp_req_dt = value; }
        }
        public Int32 Drp_seq_no
        {
            get { return _drp_seq_no; }
            set { _drp_seq_no = value; }
        }
        public string Drp_stus
        {
            get { return _drp_stus; }
            set { _drp_stus = value; }
        }
        public string Drp_stus_change_by
        {
            get { return _drp_stus_change_by; }
            set { _drp_stus_change_by = value; }
        }
        public string Drp_tp
        {
            get { return _drp_tp; }
            set { _drp_tp = value; }
        }

        #endregion

        #region converter
        public static Reprint_Docs Converter(DataRow row)
        {
            return new Reprint_Docs
            {
                Drp_app_dt = row["DRP_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DRP_APP_DT"]),
                Drp_can_dt = row["DRP_CAN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DRP_CAN_DT"]),
                Drp_doc_dt = row["DRP_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DRP_DOC_DT"]),
                Drp_doc_no = row["DRP_DOC_NO"] == DBNull.Value ? string.Empty : row["DRP_DOC_NO"].ToString(),
                Drp_is_add_pending = row["DRP_IS_ADD_PENDING"] == DBNull.Value ? 0 : Convert.ToInt32(row["DRP_IS_ADD_PENDING"]),
                Drp_loc = row["DRP_LOC"] == DBNull.Value ? string.Empty : row["DRP_LOC"].ToString(),
                Drp_printed = row["DRP_PRINTED"] == DBNull.Value ? 0 : Convert.ToInt32(row["DRP_PRINTED"]),
                Drp_print_dt = row["DRP_PRINT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DRP_PRINT_DT"]),
                Drp_reason = row["DRP_REASON"] == DBNull.Value ? string.Empty : row["DRP_REASON"].ToString(),
                Drp_rej_dt = row["DRP_REJ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DRP_REJ_DT"]),
                Drp_req_dt = row["DRP_REQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DRP_REQ_DT"]),
                Drp_seq_no = row["DRP_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["DRP_SEQ_NO"]),
                Drp_stus = row["DRP_STUS"] == DBNull.Value ? string.Empty : row["DRP_STUS"].ToString(),
                Drp_stus_change_by = row["DRP_STUS_CHANGE_BY"] == DBNull.Value ? string.Empty : row["DRP_STUS_CHANGE_BY"].ToString(),
                Drp_tp = row["DRP_TP"] == DBNull.Value ? string.Empty : row["DRP_TP"].ToString()

            };
        }

        #endregion
    }
}
