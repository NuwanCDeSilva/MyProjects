using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class CashDiscountItemPara
    {


        #region 	Private Members
        private string _spdip_brnd;
        private string _spdip_cat1;
        private string _spdip_cat2;
        private string _spdip_cat3;
        private string _spdip_circular;
        private DateTime _spdip_from_dt;
        private Boolean _spdip_is_additm_auto;
        private Int32 _spdip_line;
        private Int32 _spdip_seq;
        private DateTime _spdip_to_dt;
        private string _spdip_tp;



        #endregion

        public string Spdip_brnd
        {
            get { return _spdip_brnd; }
            set { _spdip_brnd = value; }
        }
        public string Spdip_cat1
        {
            get { return _spdip_cat1; }
            set { _spdip_cat1 = value; }
        }
        public string Spdip_cat2
        {
            get { return _spdip_cat2; }
            set { _spdip_cat2 = value; }
        }
        public string Spdip_cat3
        {
            get { return _spdip_cat3; }
            set { _spdip_cat3 = value; }
        }
        public string Spdip_circular
        {
            get { return _spdip_circular; }
            set { _spdip_circular = value; }
        }
        public DateTime Spdip_from_dt
        {
            get { return _spdip_from_dt; }
            set { _spdip_from_dt = value; }
        }
        public Boolean Spdip_is_additm_auto
        {
            get { return _spdip_is_additm_auto; }
            set { _spdip_is_additm_auto = value; }
        }
        public Int32 Spdip_line
        {
            get { return _spdip_line; }
            set { _spdip_line = value; }
        }
        public Int32 Spdip_seq
        {
            get { return _spdip_seq; }
            set { _spdip_seq = value; }
        }
        public DateTime Spdip_to_dt
        {
            get { return _spdip_to_dt; }
            set { _spdip_to_dt = value; }
        }
        public string Spdip_tp
        {
            get { return _spdip_tp; }
            set { _spdip_tp = value; }
        }


        public static CashDiscountItemPara Converter(DataRow row)
        {
            return new CashDiscountItemPara
            {
                Spdip_brnd = row["SPDIP_BRND"] == DBNull.Value ? string.Empty : row["SPDIP_BRND"].ToString(),
                Spdip_cat1 = row["SPDIP_CAT1"] == DBNull.Value ? string.Empty : row["SPDIP_CAT1"].ToString(),
                Spdip_cat2 = row["SPDIP_CAT2"] == DBNull.Value ? string.Empty : row["SPDIP_CAT2"].ToString(),
                Spdip_cat3 = row["SPDIP_CAT3"] == DBNull.Value ? string.Empty : row["SPDIP_CAT3"].ToString(),
                Spdip_circular = row["SPDIP_CIRCULAR"] == DBNull.Value ? string.Empty : row["SPDIP_CIRCULAR"].ToString(),
                Spdip_from_dt = row["SPDIP_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDIP_FROM_DT"]),
                Spdip_is_additm_auto = row["SPDIP_IS_ADDITM_AUTO"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDIP_IS_ADDITM_AUTO"]),
                Spdip_line = row["SPDIP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDIP_LINE"]),
                Spdip_seq = row["SPDIP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDIP_SEQ"]),
                Spdip_to_dt = row["SPDIP_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDIP_TO_DT"]),
                Spdip_tp = row["SPDIP_TP"] == DBNull.Value ? string.Empty : row["SPDIP_TP"].ToString()
            };
        }
    }
}

