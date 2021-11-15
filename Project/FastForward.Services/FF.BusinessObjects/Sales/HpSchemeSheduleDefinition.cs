using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpSchemeSheduleDefinition
    {
        #region Private Members
        private string _hss_cre_by;
        private DateTime _hss_cre_dt;
        private Boolean _hss_is_rt;
        private decimal _hss_rnt;
        private Int32 _hss_rnt_no;
        private string _hss_sch_cd;
        private Int32 _hss_seq;
        #endregion

        public string Hss_cre_by
        {
            get { return _hss_cre_by; }
            set { _hss_cre_by = value; }
        }
        public DateTime Hss_cre_dt
        {
            get { return _hss_cre_dt; }
            set { _hss_cre_dt = value; }
        }
        public Boolean Hss_is_rt
        {
            get { return _hss_is_rt; }
            set { _hss_is_rt = value; }
        }
        public decimal Hss_rnt
        {
            get { return _hss_rnt; }
            set { _hss_rnt = value; }
        }
        public Int32 Hss_rnt_no
        {
            get { return _hss_rnt_no; }
            set { _hss_rnt_no = value; }
        }
        public string Hss_sch_cd
        {
            get { return _hss_sch_cd; }
            set { _hss_sch_cd = value; }
        }
        public Int32 Hss_seq
        {
            get { return _hss_seq; }
            set { _hss_seq = value; }
        }

        public static HpSchemeSheduleDefinition Converter(DataRow row)
        {
            return new HpSchemeSheduleDefinition
            {
                Hss_cre_by = row["HSS_CRE_BY"] == DBNull.Value ? string.Empty : row["HSS_CRE_BY"].ToString(),
                Hss_cre_dt = row["HSS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSS_CRE_DT"]),
                Hss_is_rt = row["HSS_IS_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSS_IS_RT"]),
                Hss_rnt = row["HSS_RNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSS_RNT"]),
                Hss_rnt_no = row["HSS_RNT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSS_RNT_NO"]),
                Hss_sch_cd = row["HSS_SCH_CD"] == DBNull.Value ? string.Empty : row["HSS_SCH_CD"].ToString(),
                Hss_seq = row["HSS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSS_SEQ"])

            };
        }

    }
}
