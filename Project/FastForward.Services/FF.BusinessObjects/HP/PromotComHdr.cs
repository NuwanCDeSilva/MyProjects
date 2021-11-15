using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PromotComHdr
    {
        #region Private Members
        private Boolean _hpch_act;
        private string _hpch_app;
        private string _hpch_circular;
        private string _hpch_com;
        private decimal _hpch_com_amt;
        private decimal _hpch_com_rt;
        private string _hpch_cre_by;
        private DateTime _hpch_cre_dt;
        private DateTime _hpch_from_dt;
        private string _hpch_mod_by;
        private DateTime _hpch_mod_dt;
        private Int32 _hpch_seq;
        private DateTime _hpch_to_dt;
        #endregion

        public Boolean Hpch_act
        {
            get { return _hpch_act; }
            set { _hpch_act = value; }
        }
        public string Hpch_app
        {
            get { return _hpch_app; }
            set { _hpch_app = value; }
        }
        public string Hpch_circular
        {
            get { return _hpch_circular; }
            set { _hpch_circular = value; }
        }
        public string Hpch_com
        {
            get { return _hpch_com; }
            set { _hpch_com = value; }
        }
        public decimal Hpch_com_amt
        {
            get { return _hpch_com_amt; }
            set { _hpch_com_amt = value; }
        }
        public decimal Hpch_com_rt
        {
            get { return _hpch_com_rt; }
            set { _hpch_com_rt = value; }
        }
        public string Hpch_cre_by
        {
            get { return _hpch_cre_by; }
            set { _hpch_cre_by = value; }
        }
        public DateTime Hpch_cre_dt
        {
            get { return _hpch_cre_dt; }
            set { _hpch_cre_dt = value; }
        }
        public DateTime Hpch_from_dt
        {
            get { return _hpch_from_dt; }
            set { _hpch_from_dt = value; }
        }
        public string Hpch_mod_by
        {
            get { return _hpch_mod_by; }
            set { _hpch_mod_by = value; }
        }
        public DateTime Hpch_mod_dt
        {
            get { return _hpch_mod_dt; }
            set { _hpch_mod_dt = value; }
        }
        public Int32 Hpch_seq
        {
            get { return _hpch_seq; }
            set { _hpch_seq = value; }
        }
        public DateTime Hpch_to_dt
        {
            get { return _hpch_to_dt; }
            set { _hpch_to_dt = value; }
        }

        public static PromotComHdr Converter(DataRow row)
        {
            return new PromotComHdr
            {
                Hpch_act = row["HPCH_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["HPCH_ACT"]),
                Hpch_app = row["HPCH_APP"] == DBNull.Value ? string.Empty : row["HPCH_APP"].ToString(),
                Hpch_circular = row["HPCH_CIRCULAR"] == DBNull.Value ? string.Empty : row["HPCH_CIRCULAR"].ToString(),
                Hpch_com = row["HPCH_COM"] == DBNull.Value ? string.Empty : row["HPCH_COM"].ToString(),
                Hpch_com_amt = row["HPCH_COM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPCH_COM_AMT"]),
                Hpch_com_rt = row["HPCH_COM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPCH_COM_RT"]),
                Hpch_cre_by = row["HPCH_CRE_BY"] == DBNull.Value ? string.Empty : row["HPCH_CRE_BY"].ToString(),
                Hpch_cre_dt = row["HPCH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPCH_CRE_DT"]),
                Hpch_from_dt = row["HPCH_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPCH_FROM_DT"]),
                Hpch_mod_by = row["HPCH_MOD_BY"] == DBNull.Value ? string.Empty : row["HPCH_MOD_BY"].ToString(),
                Hpch_mod_dt = row["HPCH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPCH_MOD_DT"]),
                Hpch_seq = row["HPCH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCH_SEQ"]),
                Hpch_to_dt = row["HPCH_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPCH_TO_DT"])

            };
        }

    }
}
