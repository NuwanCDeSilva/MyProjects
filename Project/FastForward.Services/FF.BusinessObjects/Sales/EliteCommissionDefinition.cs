using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class EliteCommissionDefinition
    {
        #region Private Members
        private string _saec_circ;
        private string _saec_cre_by;
        private DateTime _saec_cre_dt;
        private DateTime _saec_dt;
        private string _saec_mod_by;
        private DateTime _saec_mod_dt;
        private Int32 _saec_seq;
        private string _saec_stus;
        private DateTime _saec_valid_from;
        private DateTime _saec_valid_to;
        private string _saec_com;
        private string _saec_no;
        private decimal _saec_alw_discount;
        #endregion

        public string Saec_circ
        {
            get { return _saec_circ; }
            set { _saec_circ = value; }
        }
        public string Eaec_com {
            get { return _saec_com; }
            set { _saec_com = value; }
        }
        public string Saec_cre_by
        {
            get { return _saec_cre_by; }
            set { _saec_cre_by = value; }
        }
        public DateTime Saec_cre_dt
        {
            get { return _saec_cre_dt; }
            set { _saec_cre_dt = value; }
        }
        public DateTime Saec_dt
        {
            get { return _saec_dt; }
            set { _saec_dt = value; }
        }
        public string Saec_mod_by
        {
            get { return _saec_mod_by; }
            set { _saec_mod_by = value; }
        }
        public string Saec_no {
            get { return _saec_no; }
            set { _saec_no = value; }
        }
        public DateTime Saec_mod_dt
        {
            get { return _saec_mod_dt; }
            set { _saec_mod_dt = value; }
        }
        public Int32 Saec_seq
        {
            get { return _saec_seq; }
            set { _saec_seq = value; }
        }
        public string Saec_stus
        {
            get { return _saec_stus; }
            set { _saec_stus = value; }
        }
        public DateTime Saec_valid_from
        {
            get { return _saec_valid_from; }
            set { _saec_valid_from = value; }
        }
        public DateTime Saec_valid_to
        {
            get { return _saec_valid_to; }
            set { _saec_valid_to = value; }
        }
        public decimal Saec_alw_discount {
            get { return _saec_alw_discount; }
            set { _saec_alw_discount = value; }
        }

        public static EliteCommissionDefinition Converter(DataRow row)
        {
            return new EliteCommissionDefinition
            {
                Saec_circ = row["SAEC_CIRC"] == DBNull.Value ? string.Empty : row["SAEC_CIRC"].ToString(),
                _saec_com = row["SAEC_COM"] == DBNull.Value ? string.Empty : row["SAEC_COM"].ToString(),
                Saec_cre_by = row["SAEC_CRE_BY"] == DBNull.Value ? string.Empty : row["SAEC_CRE_BY"].ToString(),
                Saec_cre_dt = row["SAEC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_CRE_DT"]),
                Saec_dt = row["SAEC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_DT"]),
                Saec_mod_by = row["SAEC_MOD_BY"] == DBNull.Value ? string.Empty : row["SAEC_MOD_BY"].ToString(),
                Saec_mod_dt = row["SAEC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_MOD_DT"]),
                Saec_no = row["SAEC_NO"] == DBNull.Value ? string.Empty : row["SAEC_NO"].ToString(),
                Saec_seq = row["SAEC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SEQ"]),
                Saec_stus = row["SAEC_STUS"] == DBNull.Value ? string.Empty : row["SAEC_STUS"].ToString(),
                Saec_valid_from = row["SAEC_VALID_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_VALID_FROM"]),
                Saec_valid_to = row["SAEC_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_VALID_TO"]),
                Saec_alw_discount = row["SAEC_ALW_DISCOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_ALW_DISCOUNT"]),
            };
        }

    }
}
