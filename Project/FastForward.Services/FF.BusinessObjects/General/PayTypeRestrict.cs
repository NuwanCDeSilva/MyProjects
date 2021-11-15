using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class PayTypeRestrict
    {
        #region Private Members
        private Boolean _stpr_act;
        private string _stpr_com;
        private string _stpr_cre_by;
        private DateTime _stpr_cre_dt;
        private DateTime _stpr_from;
        private string _stpr_itm;
        private string _stpr_loty;
        private string _stpr_pay_mode;
        private string _stpr_pc;
        private string _stpr_promo_cd;
        private Int32 _stpr_seq;
        private DateTime _stpr_to;
        private bool _stpr_alw_non_promo;
        #endregion

        public Boolean Stpr_act
        {
            get { return _stpr_act; }
            set { _stpr_act = value; }
        }
        public string Stpr_com
        {
            get { return _stpr_com; }
            set { _stpr_com = value; }
        }
        public string Stpr_cre_by
        {
            get { return _stpr_cre_by; }
            set { _stpr_cre_by = value; }
        }
        public DateTime Stpr_cre_dt
        {
            get { return _stpr_cre_dt; }
            set { _stpr_cre_dt = value; }
        }
        public DateTime Stpr_from
        {
            get { return _stpr_from; }
            set { _stpr_from = value; }
        }
        public string Stpr_itm
        {
            get { return _stpr_itm; }
            set { _stpr_itm = value; }
        }
        public string Stpr_loty
        {
            get { return _stpr_loty; }
            set { _stpr_loty = value; }
        }
        public string Stpr_pay_mode
        {
            get { return _stpr_pay_mode; }
            set { _stpr_pay_mode = value; }
        }
        public string Stpr_pc
        {
            get { return _stpr_pc; }
            set { _stpr_pc = value; }
        }
        public string Stpr_promo_cd
        {
            get { return _stpr_promo_cd; }
            set { _stpr_promo_cd = value; }
        }
        public Int32 Stpr_seq
        {
            get { return _stpr_seq; }
            set { _stpr_seq = value; }
        }
        public DateTime Stpr_to
        {
            get { return _stpr_to; }
            set { _stpr_to = value; }
        }
        public bool Stpr_alw_non_promo
        {
            get { return _stpr_alw_non_promo; }
            set { _stpr_alw_non_promo = value; }
        }

        public static PayTypeRestrict Converter(DataRow row)
        {
            return new PayTypeRestrict
            {
                Stpr_act = row["STPR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["STPR_ACT"]),
                Stpr_com = row["STPR_COM"] == DBNull.Value ? string.Empty : row["STPR_COM"].ToString(),
                Stpr_cre_by = row["STPR_CRE_BY"] == DBNull.Value ? string.Empty : row["STPR_CRE_BY"].ToString(),
                Stpr_cre_dt = row["STPR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["STPR_CRE_DT"]),
                Stpr_from = row["STPR_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["STPR_FROM"]),
                Stpr_itm = row["STPR_ITM"] == DBNull.Value ? string.Empty : row["STPR_ITM"].ToString(),
                Stpr_loty = row["STPR_LOTY"] == DBNull.Value ? string.Empty : row["STPR_LOTY"].ToString(),
                Stpr_pay_mode = row["STPR_PAY_MODE"] == DBNull.Value ? string.Empty : row["STPR_PAY_MODE"].ToString(),
                Stpr_pc = row["STPR_PC"] == DBNull.Value ? string.Empty : row["STPR_PC"].ToString(),
                Stpr_promo_cd = row["STPR_PROMO_CD"] == DBNull.Value ? string.Empty : row["STPR_PROMO_CD"].ToString(),
                Stpr_seq = row["STPR_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["STPR_SEQ"]),
                Stpr_to = row["STPR_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["STPR_TO"]),
                Stpr_alw_non_promo = row["STPR_ALW_NON_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["STPR_ALW_NON_PROMO"])
            };
        }

    }
}
