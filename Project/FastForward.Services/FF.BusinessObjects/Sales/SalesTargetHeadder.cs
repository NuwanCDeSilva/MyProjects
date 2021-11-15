using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SalesTargetHeadder
    {
        #region Private Members
        private string _sast_circular;
        private string _sast_cre_by;
        private DateTime _sast_cre_dt;
       // private Int32 _sast_line;
        private Int32 _sast_seq;
        private Int32 _sast_type;
        private DateTime _sast_val_from;
        private DateTime _sast_val_to;
        #endregion

        public string Sast_circular
        {
            get { return _sast_circular; }
            set { _sast_circular = value; }
        }
        public string Sast_cre_by
        {
            get { return _sast_cre_by; }
            set { _sast_cre_by = value; }
        }
        public DateTime Sast_cre_dt
        {
            get { return _sast_cre_dt; }
            set { _sast_cre_dt = value; }
        }
        //public Int32 Sast_line
        //{
        //    get { return _sast_line; }
        //    set { _sast_line = value; }
        //}

        public Int32 Sast_seq
        {
            get { return _sast_seq; }
            set { _sast_seq = value; }
        }
        public Int32 Sast_type
        {
            get { return _sast_type; }
            set { _sast_type = value; }
        }
        public DateTime Sast_val_from
        {
            get { return _sast_val_from; }
            set { _sast_val_from = value; }
        }
        public DateTime Sast_val_to
        {
            get { return _sast_val_to; }
            set { _sast_val_to = value; }
        }

        public static SalesTargetHeadder Converter(DataRow row)
        {
            return new SalesTargetHeadder
            {
                Sast_circular = row["SAST_CIRCULAR"] == DBNull.Value ? string.Empty : row["SAST_CIRCULAR"].ToString(),
                Sast_cre_by = row["SAST_CRE_BY"] == DBNull.Value ? string.Empty : row["SAST_CRE_BY"].ToString(),
                Sast_cre_dt = row["SAST_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAST_CRE_DT"]),
                //Sast_line = row["SAST_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAST_LINE"]),
                Sast_seq = row["SAST_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAST_SEQ"]),
                Sast_type = row["SAST_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAST_TYPE"]),
                Sast_val_from = row["SAST_VAL_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAST_VAL_FROM"]),
                Sast_val_to = row["SAST_VAL_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAST_VAL_TO"])

            };
        }



    }
}
