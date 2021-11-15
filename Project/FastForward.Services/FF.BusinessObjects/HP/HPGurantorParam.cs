using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


//By darsahan on 17-10-2012

namespace FF.BusinessObjects
{
    [Serializable]
    public class HPGurantorParam
    {
        #region Private Members
        private string _hpg_chk_on;
        private string _hpg_cre_by;
        private DateTime _hpg_cre_dt;
        private DateTime _hpg_from_dt;
        private decimal _hpg_from_val;
        private Int32 _hpg_no_of_gua;
        private string _hpg_pty_cd;
        private string _hpg_pty_tp;
        private string _hpg_sch_cd;
        private Int32 _hpg_seq;
        private DateTime _hpg_to_dt;
        private decimal _hpg_to_val;
        #endregion

        public string Hpg_chk_on
        {
            get { return _hpg_chk_on; }
            set { _hpg_chk_on = value; }
        }
        public string Hpg_cre_by
        {
            get { return _hpg_cre_by; }
            set { _hpg_cre_by = value; }
        }
        public DateTime Hpg_cre_dt
        {
            get { return _hpg_cre_dt; }
            set { _hpg_cre_dt = value; }
        }
        public DateTime Hpg_from_dt
        {
            get { return _hpg_from_dt; }
            set { _hpg_from_dt = value; }
        }
        public decimal Hpg_from_val
        {
            get { return _hpg_from_val; }
            set { _hpg_from_val = value; }
        }
        public Int32 Hpg_no_of_gua
        {
            get { return _hpg_no_of_gua; }
            set { _hpg_no_of_gua = value; }
        }
        public string Hpg_pty_cd
        {
            get { return _hpg_pty_cd; }
            set { _hpg_pty_cd = value; }
        }
        public string Hpg_pty_tp
        {
            get { return _hpg_pty_tp; }
            set { _hpg_pty_tp = value; }
        }
        public string Hpg_sch_cd
        {
            get { return _hpg_sch_cd; }
            set { _hpg_sch_cd = value; }
        }
        public Int32 Hpg_seq
        {
            get { return _hpg_seq; }
            set { _hpg_seq = value; }
        }
        public DateTime Hpg_to_dt
        {
            get { return _hpg_to_dt; }
            set { _hpg_to_dt = value; }
        }
        public decimal Hpg_to_val
        {
            get { return _hpg_to_val; }
            set { _hpg_to_val = value; }
        }

        public static HPGurantorParam Converter(DataRow row)
        {
            return new HPGurantorParam
            {
                Hpg_chk_on = row["HPG_CHK_ON"] == DBNull.Value ? string.Empty : row["HPG_CHK_ON"].ToString(),
                Hpg_cre_by = row["HPG_CRE_BY"] == DBNull.Value ? string.Empty : row["HPG_CRE_BY"].ToString(),
                Hpg_cre_dt = row["HPG_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPG_CRE_DT"]),
                Hpg_from_dt = row["HPG_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPG_FROM_DT"]),
                Hpg_from_val = row["HPG_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPG_FROM_VAL"]),
                Hpg_no_of_gua = row["HPG_NO_OF_GUA"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPG_NO_OF_GUA"]),
                Hpg_pty_cd = row["HPG_PTY_CD"] == DBNull.Value ? string.Empty : row["HPG_PTY_CD"].ToString(),
                Hpg_pty_tp = row["HPG_PTY_TP"] == DBNull.Value ? string.Empty : row["HPG_PTY_TP"].ToString(),
                Hpg_sch_cd = row["HPG_SCH_CD"] == DBNull.Value ? string.Empty : row["HPG_SCH_CD"].ToString(),
                Hpg_seq = row["HPG_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPG_SEQ"]),
                Hpg_to_dt = row["HPG_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPG_TO_DT"]),
                Hpg_to_val = row["HPG_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPG_TO_VAL"])

            };
        }

    }
}
