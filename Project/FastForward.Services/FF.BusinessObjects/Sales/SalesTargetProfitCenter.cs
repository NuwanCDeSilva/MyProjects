using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class SalesTargetProfitCenter
    {
        #region Private Members
        private string _sast_cre_by;
        private DateTime _sast_cre_dt;
        private string _sast_pc;
        private Int32 _sast_seq;
        #endregion

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
        public string Sast_pc
        {
            get { return _sast_pc; }
            set { _sast_pc = value; }
        }
        public Int32 Sast_seq
        {
            get { return _sast_seq; }
            set { _sast_seq = value; }
        }

        public static SalesTargetProfitCenter Converter(DataRow row)
        {
            return new SalesTargetProfitCenter
            {
                Sast_cre_by = row["SAST_CRE_BY"] == DBNull.Value ? string.Empty : row["SAST_CRE_BY"].ToString(),
                Sast_cre_dt = row["SAST_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAST_CRE_DT"]),
                Sast_pc = row["SAST_PC"] == DBNull.Value ? string.Empty : row["SAST_PC"].ToString(),
                Sast_seq = row["SAST_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAST_SEQ"])

            };
        }


    }
}
