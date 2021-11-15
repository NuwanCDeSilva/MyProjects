using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ExcessShortDet
    {
        #region Private Members
        private string _esrd_cd;
        private string _esrd_desc;
        private Decimal _esrd_exces;
        private Boolean _esrd_fixed;
        private string _esrd_id;
        private string _esrd_invs;
        private Int32 _esrd_line;
        private string _esrd_sec;
        private Decimal _esrd_short;
        private Int32 _esrd_week;
        private Int32 _esrd_is_memo;
        private string _esrd_cre_by;
        private string _esrd_rem;   //kapila 22/12/2015
        private Decimal _esrd_set_amt;
        #endregion

        #region Public Property Definition
        public Decimal Esrd_set_amt
        {
            get { return _esrd_set_amt; }
            set { _esrd_set_amt = value; }
        }
        public string Esrd_rem
        {
            get { return _esrd_rem; }
            set { _esrd_rem = value; }
        }
        public string Esrd_cre_by
        {
            get { return _esrd_cre_by; }
            set { _esrd_cre_by = value; }
        }

        public Int32 Esrd_is_memo
        {
            get { return _esrd_is_memo; }
            set { _esrd_is_memo = value; }
        }

        public string Esrd_cd
        {
            get { return _esrd_cd; }
            set { _esrd_cd = value; }
        }
        public string Esrd_desc
        {
            get { return _esrd_desc; }
            set { _esrd_desc = value; }
        }
        public Decimal Esrd_exces
        {
            get { return _esrd_exces; }
            set { _esrd_exces = value; }
        }
        public Boolean Esrd_fixed
        {
            get { return _esrd_fixed; }
            set { _esrd_fixed = value; }
        }
        public string Esrd_id
        {
            get { return _esrd_id; }
            set { _esrd_id = value; }
        }
        public string Esrd_invs
        {
            get { return _esrd_invs; }
            set { _esrd_invs = value; }
        }
        public Int32 Esrd_line
        {
            get { return _esrd_line; }
            set { _esrd_line = value; }
        }
        public string Esrd_sec
        {
            get { return _esrd_sec; }
            set { _esrd_sec = value; }
        }
        public Decimal Esrd_short
        {
            get { return _esrd_short; }
            set { _esrd_short = value; }
        }
        public Int32 Esrd_week
        {
            get { return _esrd_week; }
            set { _esrd_week = value; }
        }
        #endregion

        #region Converters
        public static ExcessShortDet Converter(DataRow row)
        {
            return new ExcessShortDet
            {
                Esrd_cd = row["ESRD_CD"] == DBNull.Value ? string.Empty : row["ESRD_CD"].ToString(),
                Esrd_desc = row["ESRD_DESC"] == DBNull.Value ? string.Empty : row["ESRD_DESC"].ToString(),
                Esrd_exces = row["ESRD_EXCES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESRD_EXCES"]),
                Esrd_fixed = row["ESRD_FIXED"] == DBNull.Value ? false : Convert.ToBoolean(row["ESRD_FIXED"]),
                Esrd_id = row["ESRD_ID"] == DBNull.Value ? string.Empty : row["ESRD_ID"].ToString(),
                Esrd_invs = row["ESRD_INVS"] == DBNull.Value ? string.Empty : row["ESRD_INVS"].ToString(),
                Esrd_line = row["ESRD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESRD_LINE"]),
                Esrd_sec = row["ESRD_SEC"] == DBNull.Value ? string.Empty : row["ESRD_SEC"].ToString(),
                Esrd_short = row["ESRD_SHORT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESRD_SHORT"]),
                Esrd_is_memo = row["Esrd_is_memo"] == DBNull.Value ? 0 : Convert.ToInt32(row["Esrd_is_memo"]),
                Esrd_week = row["ESRD_WEEK"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESRD_WEEK"]),
                Esrd_rem = row["Esrd_rem"] == DBNull.Value ? string.Empty : row["Esrd_rem"].ToString()

            };
        }
        #endregion
    }
}

