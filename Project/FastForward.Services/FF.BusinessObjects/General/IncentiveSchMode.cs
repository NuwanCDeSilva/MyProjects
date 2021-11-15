using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{

    public class IncentiveSchMode
    {
        #region Private Members
        private string _inpym_circ;
        private Int32 _inpym_dt_line;
        private Int32 _inpym_is_promo;
        private Int32 _inpym_line;
        private string _inpym_mode;
        private Int32 _inpym_promo_mnth;
        private string _inpym_ref;
        #endregion

        public string Inpym_circ
        {
            get { return _inpym_circ; }
            set { _inpym_circ = value; }
        }
        public Int32 Inpym_dt_line
        {
            get { return _inpym_dt_line; }
            set { _inpym_dt_line = value; }
        }
        public Int32 Inpym_is_promo
        {
            get { return _inpym_is_promo; }
            set { _inpym_is_promo = value; }
        }
        public Int32 Inpym_line
        {
            get { return _inpym_line; }
            set { _inpym_line = value; }
        }
        public string Inpym_mode
        {
            get { return _inpym_mode; }
            set { _inpym_mode = value; }
        }
        public Int32 Inpym_promo_mnth
        {
            get { return _inpym_promo_mnth; }
            set { _inpym_promo_mnth = value; }
        }
        public string Inpym_ref
        {
            get { return _inpym_ref; }
            set { _inpym_ref = value; }
        }

        public static IncentiveSchMode Converter(DataRow row)
        {
            return new IncentiveSchMode
            {
                Inpym_circ = row["INPYM_CIRC"] == DBNull.Value ? string.Empty : row["INPYM_CIRC"].ToString(),
                Inpym_dt_line = row["INPYM_DT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INPYM_DT_LINE"]),
                Inpym_is_promo = row["INPYM_IS_PROMO"] == DBNull.Value ? 0 : Convert.ToInt32(row["INPYM_IS_PROMO"]),
                Inpym_line = row["INPYM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INPYM_LINE"]),
                Inpym_mode = row["INPYM_MODE"] == DBNull.Value ? string.Empty : row["INPYM_MODE"].ToString(),
                Inpym_promo_mnth = row["INPYM_PROMO_MNTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["INPYM_PROMO_MNTH"]),
                Inpym_ref = row["INPYM_REF"] == DBNull.Value ? string.Empty : row["INPYM_REF"].ToString()
            };
        }

    }
}
