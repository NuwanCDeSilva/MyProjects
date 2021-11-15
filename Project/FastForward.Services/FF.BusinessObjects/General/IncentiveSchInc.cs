using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class IncentiveSchInc
    {
        #region Private Members
        private Int32 _inci_alt;
        private string _inci_circ;
        private Int32 _inci_dt_line;
        private string _inci_inc_tp;
        private Boolean _inci_is_alt_inc;
        private Int32 _inci_line;
        private string _inci_ref;
        #endregion

        public Int32 Inci_alt
        {
            get { return _inci_alt; }
            set { _inci_alt = value; }
        }
        public string Inci_circ
        {
            get { return _inci_circ; }
            set { _inci_circ = value; }
        }
        public Int32 Inci_dt_line
        {
            get { return _inci_dt_line; }
            set { _inci_dt_line = value; }
        }
        public string Inci_inc_tp
        {
            get { return _inci_inc_tp; }
            set { _inci_inc_tp = value; }
        }
        public Boolean Inci_is_alt_inc
        {
            get { return _inci_is_alt_inc; }
            set { _inci_is_alt_inc = value; }
        }
        public Int32 Inci_line
        {
            get { return _inci_line; }
            set { _inci_line = value; }
        }
        public string Inci_ref
        {
            get { return _inci_ref; }
            set { _inci_ref = value; }
        }

        public static IncentiveSchInc Converter(DataRow row)
        {
            return new IncentiveSchInc
            {
                Inci_alt = row["INCI_ALT"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCI_ALT"]),
                Inci_circ = row["INCI_CIRC"] == DBNull.Value ? string.Empty : row["INCI_CIRC"].ToString(),
                Inci_dt_line = row["INCI_DT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCI_DT_LINE"]),
                Inci_inc_tp = row["INCI_INC_TP"] == DBNull.Value ? string.Empty : row["INCI_INC_TP"].ToString(),
                Inci_is_alt_inc = row["INCI_IS_ALT_INC"] == DBNull.Value ? false : Convert.ToBoolean(row["INCI_IS_ALT_INC"]),
                Inci_line = row["INCI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCI_LINE"]),
                Inci_ref = row["INCI_REF"] == DBNull.Value ? string.Empty : row["INCI_REF"].ToString()
            };
        }

    }
}
