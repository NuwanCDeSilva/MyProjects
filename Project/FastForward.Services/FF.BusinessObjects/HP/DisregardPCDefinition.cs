using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class DisregardPCDefinition
    {
        #region Private Members
        private string _hdpd_com;
        private string _hdpd_cre_by;
        private DateTime _hdpd_cre_dt;
        private DateTime _hdpd_from_dt;
        private string _hdpd_mod_by;
        private DateTime _hdpd_mod_dt;
        private string _hdpd_pc;
        private Int32 _hdpd_seq;
        private DateTime _hdpd_to_dt;
        #endregion

        public string Hdpd_com
        {
            get { return _hdpd_com; }
            set { _hdpd_com = value; }
        }
        public string Hdpd_cre_by
        {
            get { return _hdpd_cre_by; }
            set { _hdpd_cre_by = value; }
        }
        public DateTime Hdpd_cre_dt
        {
            get { return _hdpd_cre_dt; }
            set { _hdpd_cre_dt = value; }
        }
        public DateTime Hdpd_from_dt
        {
            get { return _hdpd_from_dt; }
            set { _hdpd_from_dt = value; }
        }
        public string Hdpd_mod_by
        {
            get { return _hdpd_mod_by; }
            set { _hdpd_mod_by = value; }
        }
        public DateTime Hdpd_mod_dt
        {
            get { return _hdpd_mod_dt; }
            set { _hdpd_mod_dt = value; }
        }
        public string Hdpd_pc
        {
            get { return _hdpd_pc; }
            set { _hdpd_pc = value; }
        }
        public Int32 Hdpd_seq
        {
            get { return _hdpd_seq; }
            set { _hdpd_seq = value; }
        }
        public DateTime Hdpd_to_dt
        {
            get { return _hdpd_to_dt; }
            set { _hdpd_to_dt = value; }
        }

        public static DisregardPCDefinition Converter(DataRow row)
        {
            return new DisregardPCDefinition
            {
                Hdpd_com = row["HDPD_COM"] == DBNull.Value ? string.Empty : row["HDPD_COM"].ToString(),
                Hdpd_cre_by = row["HDPD_CRE_BY"] == DBNull.Value ? string.Empty : row["HDPD_CRE_BY"].ToString(),
                Hdpd_cre_dt = row["HDPD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HDPD_CRE_DT"]),
                Hdpd_from_dt = row["HDPD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HDPD_FROM_DT"]),
                Hdpd_mod_by = row["HDPD_MOD_BY"] == DBNull.Value ? string.Empty : row["HDPD_MOD_BY"].ToString(),
                Hdpd_mod_dt = row["HDPD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HDPD_MOD_DT"]),
                Hdpd_pc = row["HDPD_PC"] == DBNull.Value ? string.Empty : row["HDPD_PC"].ToString(),
                Hdpd_seq = row["HDPD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HDPD_SEQ"]),
                Hdpd_to_dt = row["HDPD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HDPD_TO_DT"])

            };
        }
    }
}
