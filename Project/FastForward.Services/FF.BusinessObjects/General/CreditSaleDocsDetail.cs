using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class CreditSaleDocsDetail
    {
        #region Private Members
        private string _gdd_doc;
        private string _gdd_iss_by;
        private DateTime _gdd_iss_dt;
        private string _gdd_iss_rmks;
        private Boolean _gdd_is_issu;
        private Boolean _gdd_is_rec;
        private Int32 _gdd_line;
        private string _gdd_rec_by;
        private DateTime _gdd_rec_dt;
        private string _gdd_rec_rmks;
        private Int32 _gdd_seq;
        //kapila
        private Boolean _gdd_is_ret;
        private DateTime _gdd_ret_dt;
        private string _gdd_ret_rmks;
        private Boolean _gdd_is_rerec;
        private DateTime _gdd_rerec_dt;
        #endregion

        #region Public Property Definition
        public Boolean Gdd_is_ret
        {
            get { return _gdd_is_ret; }
            set { _gdd_is_ret = value; }
        }
        public DateTime Gdd_ret_dt
        {
            get { return _gdd_ret_dt; }
            set { _gdd_ret_dt = value; }
        }
        public string Gdd_ret_rmks
        {
            get { return _gdd_ret_rmks; }
            set { _gdd_ret_rmks = value; }
        }
        public Boolean Gdd_is_rerec
        {
            get { return _gdd_is_rerec; }
            set { _gdd_is_rerec = value; }
        }
        public DateTime Gdd_rerec_dt
        {
            get { return _gdd_rerec_dt; }
            set { _gdd_rerec_dt = value; }
        }


        public string Gdd_doc
        {
            get { return _gdd_doc; }
            set { _gdd_doc = value; }
        }
        public string Gdd_iss_by
        {
            get { return _gdd_iss_by; }
            set { _gdd_iss_by = value; }
        }
        public DateTime Gdd_iss_dt
        {
            get { return _gdd_iss_dt; }
            set { _gdd_iss_dt = value; }
        }
        public string Gdd_iss_rmks
        {
            get { return _gdd_iss_rmks; }
            set { _gdd_iss_rmks = value; }
        }
        public Boolean Gdd_is_issu
        {
            get { return _gdd_is_issu; }
            set { _gdd_is_issu = value; }
        }
        public Boolean Gdd_is_rec
        {
            get { return _gdd_is_rec; }
            set { _gdd_is_rec = value; }
        }
        public Int32 Gdd_line
        {
            get { return _gdd_line; }
            set { _gdd_line = value; }
        }
        public string Gdd_rec_by
        {
            get { return _gdd_rec_by; }
            set { _gdd_rec_by = value; }
        }
        public DateTime Gdd_rec_dt
        {
            get { return _gdd_rec_dt; }
            set { _gdd_rec_dt = value; }
        }
        public string Gdd_rec_rmks
        {
            get { return _gdd_rec_rmks; }
            set { _gdd_rec_rmks = value; }
        }
        public Int32 Gdd_seq
        {
            get { return _gdd_seq; }
            set { _gdd_seq = value; }
        }
        #endregion

        public static CreditSaleDocsDetail Converter(DataRow row)
        {
            return new CreditSaleDocsDetail
            {
                Gdd_doc = row["GDD_DOC"] == DBNull.Value ? string.Empty : row["GDD_DOC"].ToString(),
                Gdd_iss_by = row["GDD_ISS_BY"] == DBNull.Value ? string.Empty : row["GDD_ISS_BY"].ToString(),
                Gdd_iss_dt = row["GDD_ISS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDD_ISS_DT"]),
                Gdd_iss_rmks = row["GDD_ISS_RMKS"] == DBNull.Value ? string.Empty : row["GDD_ISS_RMKS"].ToString(),
                Gdd_is_issu = row["GDD_IS_ISSU"] == DBNull.Value ? false : Convert.ToBoolean(row["GDD_IS_ISSU"]),
                Gdd_is_rec = row["GDD_IS_REC"] == DBNull.Value ? false : Convert.ToBoolean(row["GDD_IS_REC"]),
                Gdd_line = row["GDD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GDD_LINE"]),
                Gdd_rec_by = row["GDD_REC_BY"] == DBNull.Value ? string.Empty : row["GDD_REC_BY"].ToString(),
                Gdd_rec_dt = row["GDD_REC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDD_REC_DT"]),
                Gdd_rec_rmks = row["GDD_REC_RMKS"] == DBNull.Value ? string.Empty : row["GDD_REC_RMKS"].ToString(),
                Gdd_seq = row["GDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GDD_SEQ"]),
                Gdd_is_ret = row["GDD_IS_RET"] == DBNull.Value ? false : Convert.ToBoolean(row["GDD_IS_RET"]),
                Gdd_ret_dt = row["GDD_RET_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDD_RET_DT"]),
                Gdd_ret_rmks = row["GDD_RET_REM"] == DBNull.Value ? string.Empty : row["GDD_RET_REM"].ToString(),
                Gdd_is_rerec = row["GDD_IS_REREC"] == DBNull.Value ? false : Convert.ToBoolean(row["GDD_IS_REREC"]),
                Gdd_rerec_dt = row["GDD_REREC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDD_REREC_DT"])

            };
        }
    }
}


