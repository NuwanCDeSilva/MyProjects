using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class VoucherPrintExpenseDefinition
    {
        #region Private Members
        private string _gved_com;
        private string _gved_cre_by;
        private DateTime _gved_cre_dt;
        private string _gved_expe_cat;
        private string _gved_expe_cd;
        private string _gved_expe_desc;
        private DateTime _gved_expe_from_dt;
        private DateTime _gved_expe_to_dt;
        private decimal _gved_expe_val;
        private string _gved_mod_by;
        private DateTime _gved_mod_dt;
        private Int32 _gved_seq;
        private Boolean _gved_stus;
        public Int32 Gved_vou_tp { get; set; }

        #endregion

        public string Gved_com
        {
            get { return _gved_com; }
            set { _gved_com = value; }
        }
        public string Gved_cre_by
        {
            get { return _gved_cre_by; }
            set { _gved_cre_by = value; }
        }
        public DateTime Gved_cre_dt
        {
            get { return _gved_cre_dt; }
            set { _gved_cre_dt = value; }
        }
        public string Gved_expe_cat
        {
            get { return _gved_expe_cat; }
            set { _gved_expe_cat = value; }
        }
        public string Gved_expe_cd
        {
            get { return _gved_expe_cd; }
            set { _gved_expe_cd = value; }
        }
        public string Gved_expe_desc
        {
            get { return _gved_expe_desc; }
            set { _gved_expe_desc = value; }
        }
        public DateTime Gved_expe_from_dt
        {
            get { return _gved_expe_from_dt; }
            set { _gved_expe_from_dt = value; }
        }
        public DateTime Gved_expe_to_dt
        {
            get { return _gved_expe_to_dt; }
            set { _gved_expe_to_dt = value; }
        }
        public decimal Gved_expe_val
        {
            get { return _gved_expe_val; }
            set { _gved_expe_val = value; }
        }
        public string Gved_mod_by
        {
            get { return _gved_mod_by; }
            set { _gved_mod_by = value; }
        }
        public DateTime Gved_mod_dt
        {
            get { return _gved_mod_dt; }
            set { _gved_mod_dt = value; }
        }
        public Int32 Gved_seq
        {
            get { return _gved_seq; }
            set { _gved_seq = value; }
        }
        public Boolean Gved_stus
        {
            get { return _gved_stus; }
            set { _gved_stus = value; }
        }

        public static VoucherPrintExpenseDefinition Converter(DataRow row)
        {
            return new VoucherPrintExpenseDefinition
            {
                Gved_com = row["GVED_COM"] == DBNull.Value ? string.Empty : row["GVED_COM"].ToString(),
                Gved_cre_by = row["GVED_CRE_BY"] == DBNull.Value ? string.Empty : row["GVED_CRE_BY"].ToString(),
                Gved_cre_dt = row["GVED_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVED_CRE_DT"]),
                Gved_expe_cat = row["GVED_EXPE_CAT"] == DBNull.Value ? string.Empty : row["GVED_EXPE_CAT"].ToString(),
                Gved_expe_cd = row["GVED_EXPE_CD"] == DBNull.Value ? string.Empty : row["GVED_EXPE_CD"].ToString(),
                Gved_expe_desc = row["GVED_EXPE_DESC"] == DBNull.Value ? string.Empty : row["GVED_EXPE_DESC"].ToString(),
                Gved_expe_from_dt = row["GVED_EXPE_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVED_EXPE_FROM_DT"]),
                Gved_expe_to_dt = row["GVED_EXPE_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVED_EXPE_TO_DT"]),
                Gved_expe_val = row["GVED_EXPE_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GVED_EXPE_VAL"]),
                Gved_mod_by = row["GVED_MOD_BY"] == DBNull.Value ? string.Empty : row["GVED_MOD_BY"].ToString(),
                Gved_mod_dt = row["GVED_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVED_MOD_DT"]),
                Gved_seq = row["GVED_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVED_SEQ"]),
                Gved_stus = row["GVED_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["GVED_STUS"]),
                Gved_vou_tp = row["GVED_VOU_TP"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVED_VOU_TP"])
            };
        }
    }
}
