using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RemitanceSumHeading
    {
        #region Private Members
        private string _rsd_acc;
        private string _rsd_cd;
        private string _rsd_cre_by;
        private DateTime _rsd_cre_when;
        private string _rsd_desc;
        private Int32 _rsd_fixed;
        private string _rsd_mod_by;
        private DateTime _rsd_mod_when;
        private string _rsd_sec;
        private string _rsd_stus;
        private Int32 _RSD_IS_ONCE;
        #endregion

        #region Public Property Definition
        public string Rsd_acc
        {
            get { return _rsd_acc; }
            set { _rsd_acc = value; }
        }
        public string Rsd_cd
        {
            get { return _rsd_cd; }
            set { _rsd_cd = value; }
        }
        public string Rsd_cre_by
        {
            get { return _rsd_cre_by; }
            set { _rsd_cre_by = value; }
        }
        public DateTime Rsd_cre_when
        {
            get { return _rsd_cre_when; }
            set { _rsd_cre_when = value; }
        }
        public string Rsd_desc
        {
            get { return _rsd_desc; }
            set { _rsd_desc = value; }
        }
        public Int32 Rsd_fixed
        {
            get { return _rsd_fixed; }
            set { _rsd_fixed = value; }
        }

        public string Rsd_mod_by
        {
            get { return _rsd_mod_by; }
            set { _rsd_mod_by = value; }
        }
        public DateTime Rsd_mod_when
        {
            get { return _rsd_mod_when; }
            set { _rsd_mod_when = value; }
        }
        public string Rsd_sec
        {
            get { return _rsd_sec; }
            set { _rsd_sec = value; }
        }
        public string Rsd_stus
        {
            get { return _rsd_stus; }
            set { _rsd_stus = value; }
        }
        public Int32 RSD_IS_ONCE
        {
            get { return _RSD_IS_ONCE; }
            set { _RSD_IS_ONCE = value; }
        }
        #endregion

        #region Converters
        public static RemitanceSumHeading Converter(DataRow row)
        {
            return new RemitanceSumHeading
            {
                Rsd_acc = row["RSD_ACC"] == DBNull.Value ? string.Empty : row["RSD_ACC"].ToString(),
                Rsd_cd = row["RSD_CD"] == DBNull.Value ? string.Empty : row["RSD_CD"].ToString(),
                Rsd_cre_by = row["RSD_CRE_BY"] == DBNull.Value ? string.Empty : row["RSD_CRE_BY"].ToString(),
                Rsd_cre_when = row["RSD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RSD_CRE_WHEN"]),
                Rsd_desc = row["RSD_DESC"] == DBNull.Value ? string.Empty : row["RSD_DESC"].ToString(),
                Rsd_fixed = row["RSD_FIXED"] == DBNull.Value ? 0 : Convert.ToInt32(row["RSD_FIXED"]),
                Rsd_mod_by = row["RSD_MOD_BY"] == DBNull.Value ? string.Empty : row["RSD_MOD_BY"].ToString(),
                Rsd_mod_when = row["RSD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RSD_MOD_WHEN"]),
                Rsd_sec = row["RSD_SEC"] == DBNull.Value ? string.Empty : row["RSD_SEC"].ToString(),
                RSD_IS_ONCE = row["RSD_IS_ONCE"] == DBNull.Value ? 0 : Convert.ToInt32(row["RSD_IS_ONCE"]),
                Rsd_stus = row["RSD_STUS"] == DBNull.Value ? string.Empty : row["RSD_STUS"].ToString()
            };
        }
        #endregion
    }
}

