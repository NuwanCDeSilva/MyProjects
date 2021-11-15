using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class DisregardValueDefinition
    {
        #region Private Members
        private string _hdvr_cre_by;
        private DateTime _hdvr_cre_dt;
        private decimal _hdvr_from_val;
        private string _hdvr_mod_by;
        private DateTime _hdvr_mod_dt;
        private decimal _hdvr_to_val;
        private Boolean _hdvr_tp;
        private decimal _hdvr_val;
        #endregion

        public string Hdvr_cre_by
        {
            get { return _hdvr_cre_by; }
            set { _hdvr_cre_by = value; }
        }
        public DateTime Hdvr_cre_dt
        {
            get { return _hdvr_cre_dt; }
            set { _hdvr_cre_dt = value; }
        }
        public decimal Hdvr_from_val
        {
            get { return _hdvr_from_val; }
            set { _hdvr_from_val = value; }
        }
        public string Hdvr_mod_by
        {
            get { return _hdvr_mod_by; }
            set { _hdvr_mod_by = value; }
        }
        public DateTime Hdvr_mod_dt
        {
            get { return _hdvr_mod_dt; }
            set { _hdvr_mod_dt = value; }
        }
        public decimal Hdvr_to_val
        {
            get { return _hdvr_to_val; }
            set { _hdvr_to_val = value; }
        }
        public Boolean Hdvr_tp
        {
            get { return _hdvr_tp; }
            set { _hdvr_tp = value; }
        }
        public decimal Hdvr_val
        {
            get { return _hdvr_val; }
            set { _hdvr_val = value; }
        }

        public static DisregardValueDefinition Converter(DataRow row)
        {
            return new DisregardValueDefinition
            {
                Hdvr_cre_by = row["HDVR_CRE_BY"] == DBNull.Value ? string.Empty : row["HDVR_CRE_BY"].ToString(),
                Hdvr_cre_dt = row["HDVR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HDVR_CRE_DT"]),
                Hdvr_from_val = row["HDVR_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HDVR_FROM_VAL"]),
                Hdvr_mod_by = row["HDVR_MOD_BY"] == DBNull.Value ? string.Empty : row["HDVR_MOD_BY"].ToString(),
                Hdvr_mod_dt = row["HDVR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HDVR_MOD_DT"]),
                Hdvr_to_val = row["HDVR_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HDVR_TO_VAL"]),
                Hdvr_tp = row["HDVR_TP"] == DBNull.Value ? false : Convert.ToBoolean(row["HDVR_TP"]),
                Hdvr_val = row["HDVR_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HDVR_VAL"])

            };
        }
    }
}
