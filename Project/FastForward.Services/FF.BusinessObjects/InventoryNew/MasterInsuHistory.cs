using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterInsuHistory
    {
        #region Private Members
        private string _mih_com;
        private string _mih_cre_by;
        private DateTime _mih_cre_dt;
        private decimal _mih_ins_amt;
        private string _mih_ins_com;
        private DateTime _mih_ins_dt;
        private string _mih_ins_ref;
        private string _mih_mloc;
        private string _mih_mod_by;
        private DateTime _mih_mod_dt;
        private Int32 _mih_seq;
        private string _mih_sloc;
        #endregion

        public string Mih_com
        {
            get { return _mih_com; }
            set { _mih_com = value; }
        }
        public string Mih_cre_by
        {
            get { return _mih_cre_by; }
            set { _mih_cre_by = value; }
        }
        public DateTime Mih_cre_dt
        {
            get { return _mih_cre_dt; }
            set { _mih_cre_dt = value; }
        }
        public decimal Mih_ins_amt
        {
            get { return _mih_ins_amt; }
            set { _mih_ins_amt = value; }
        }
        public string Mih_ins_com
        {
            get { return _mih_ins_com; }
            set { _mih_ins_com = value; }
        }
        public DateTime Mih_ins_dt
        {
            get { return _mih_ins_dt; }
            set { _mih_ins_dt = value; }
        }
        public string Mih_ins_ref
        {
            get { return _mih_ins_ref; }
            set { _mih_ins_ref = value; }
        }
        public string Mih_mloc
        {
            get { return _mih_mloc; }
            set { _mih_mloc = value; }
        }
        public string Mih_mod_by
        {
            get { return _mih_mod_by; }
            set { _mih_mod_by = value; }
        }
        public DateTime Mih_mod_dt
        {
            get { return _mih_mod_dt; }
            set { _mih_mod_dt = value; }
        }
        public Int32 Mih_seq
        {
            get { return _mih_seq; }
            set { _mih_seq = value; }
        }
        public string Mih_sloc
        {
            get { return _mih_sloc; }
            set { _mih_sloc = value; }
        }


        public static MasterInsuHistory Converter(DataRow row)
        {
            return new MasterInsuHistory
            {
                Mih_com = row["MIH_COM"] == DBNull.Value ? string.Empty : row["MIH_COM"].ToString(),
                Mih_cre_by = row["MIH_CRE_BY"] == DBNull.Value ? string.Empty : row["MIH_CRE_BY"].ToString(),
                Mih_cre_dt = row["MIH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIH_CRE_DT"]),
                Mih_ins_amt = row["MIH_INS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MIH_INS_AMT"]),
                Mih_ins_com = row["MIH_INS_COM"] == DBNull.Value ? string.Empty : row["MIH_INS_COM"].ToString(),
                Mih_ins_dt = row["MIH_INS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIH_INS_DT"]),
                Mih_ins_ref = row["MIH_INS_REF"] == DBNull.Value ? string.Empty : row["MIH_INS_REF"].ToString(),
                Mih_mloc = row["MIH_MLOC"] == DBNull.Value ? string.Empty : row["MIH_MLOC"].ToString(),
                Mih_mod_by = row["MIH_MOD_BY"] == DBNull.Value ? string.Empty : row["MIH_MOD_BY"].ToString(),
                Mih_mod_dt = row["MIH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIH_MOD_DT"]),
                Mih_seq = row["MIH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MIH_SEQ"]),
                Mih_sloc = row["MIH_SLOC"] == DBNull.Value ? string.Empty : row["MIH_SLOC"].ToString()

            };
        }

    }
}
