using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ExcessRemitTypes
    {
        #region Private Members
        private Int32 _esr_act;
        private string _esr_cd;
        private string _esr_desc;
        private Int32 _esr_fixed;
        private Int32 _esr_is_excs;
        private string _esr_sec;
        #endregion

        #region Public Property Definition
        public Int32 Esr_act
        {
            get { return _esr_act; }
            set { _esr_act = value; }
        }
        public string Esr_cd
        {
            get { return _esr_cd; }
            set { _esr_cd = value; }
        }
        public string Esr_desc
        {
            get { return _esr_desc; }
            set { _esr_desc = value; }
        }
        public Int32 Esr_fixed
        {
            get { return _esr_fixed; }
            set { _esr_fixed = value; }
        }
        public Int32 Esr_is_excs
        {
            get { return _esr_is_excs; }
            set { _esr_is_excs = value; }
        }
        public string Esr_sec
        {
            get { return _esr_sec; }
            set { _esr_sec = value; }
        }
        #endregion

        #region Converters
        public static ExcessRemitTypes Converter(DataRow row)
        {
            return new ExcessRemitTypes
            {
                Esr_act = row["ESR_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESR_ACT"]),
                Esr_cd = row["ESR_CD"] == DBNull.Value ? string.Empty : row["ESR_CD"].ToString(),
                Esr_desc = row["ESR_DESC"] == DBNull.Value ? string.Empty : row["ESR_DESC"].ToString(),
                Esr_fixed = row["ESR_FIXED"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESR_FIXED"]),
                Esr_is_excs = row["ESR_IS_EXCS"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESR_IS_EXCS"]),
                Esr_sec = row["ESR_SEC"] == DBNull.Value ? string.Empty : row["ESR_SEC"].ToString()

            };
        }
        #endregion
    }
}

