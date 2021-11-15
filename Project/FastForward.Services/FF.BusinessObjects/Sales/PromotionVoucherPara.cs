using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//kapila 19/1/2016

namespace FF.BusinessObjects
{
    [Serializable]

    public class PromotionVoucherPara
    {
        #region Private Members
        private string _spdp_anal1;
        private string _spdp_anal2;
        private Int32 _spdp_anal3;
        private Int32 _spdp_anal4;
        private DateTime _spdp_anal5;
        private DateTime _spdp_anal6;
        private Int32 _spdp_pay_prd;
        private string _spdp_pay_tp;
        private string _spdp_price_tp;
        private string _spdp_sale_tp;
        private Int32 _spdp_seq;
        private string _spdp_tp;
        private string _spdp_vou_cd;


        #endregion
        public string Spdp_anal1
        {
            get { return _spdp_anal1; }
            set { _spdp_anal1 = value; }
        }
        public string Spdp_anal2
        {
            get { return _spdp_anal2; }
            set { _spdp_anal2 = value; }
        }
        public Int32 Spdp_anal3
        {
            get { return _spdp_anal3; }
            set { _spdp_anal3 = value; }
        }
        public Int32 Spdp_anal4
        {
            get { return _spdp_anal4; }
            set { _spdp_anal4 = value; }
        }
        public DateTime Spdp_anal5
        {
            get { return _spdp_anal5; }
            set { _spdp_anal5 = value; }
        }
        public DateTime Spdp_anal6
        {
            get { return _spdp_anal6; }
            set { _spdp_anal6 = value; }
        }
        public Int32 Spdp_pay_prd
        {
            get { return _spdp_pay_prd; }
            set { _spdp_pay_prd = value; }
        }
        public string Spdp_pay_tp
        {
            get { return _spdp_pay_tp; }
            set { _spdp_pay_tp = value; }
        }
        public string Spdp_price_tp
        {
            get { return _spdp_price_tp; }
            set { _spdp_price_tp = value; }
        }
        public string Spdp_sale_tp
        {
            get { return _spdp_sale_tp; }
            set { _spdp_sale_tp = value; }
        }
        public Int32 Spdp_seq
        {
            get { return _spdp_seq; }
            set { _spdp_seq = value; }
        }
        public string Spdp_tp
        {
            get { return _spdp_tp; }
            set { _spdp_tp = value; }
        }
        public string Spdp_vou_cd
        {
            get { return _spdp_vou_cd; }
            set { _spdp_vou_cd = value; }
        }



        public static PromotionVoucherPara Converter(DataRow row)
        {
            return new PromotionVoucherPara
            {
                Spdp_anal1 = row["SPDP_ANAL1"] == DBNull.Value ? string.Empty : row["SPDP_ANAL1"].ToString(),
                Spdp_anal2 = row["SPDP_ANAL2"] == DBNull.Value ? string.Empty : row["SPDP_ANAL2"].ToString(),
                Spdp_anal3 = row["SPDP_ANAL3"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDP_ANAL3"]),
                Spdp_anal4 = row["SPDP_ANAL4"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDP_ANAL4"]),
                Spdp_anal5 = row["SPDP_ANAL5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDP_ANAL5"]),
                Spdp_anal6 = row["SPDP_ANAL6"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDP_ANAL6"]),
                Spdp_pay_prd = row["SPDP_PAY_PRD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDP_PAY_PRD"]),
                Spdp_pay_tp = row["SPDP_PAY_TP"] == DBNull.Value ? string.Empty : row["SPDP_PAY_TP"].ToString(),
                Spdp_price_tp = row["SPDP_PRICE_TP"] == DBNull.Value ? string.Empty : row["SPDP_PRICE_TP"].ToString(),
                Spdp_sale_tp = row["SPDP_SALE_TP"] == DBNull.Value ? string.Empty : row["SPDP_SALE_TP"].ToString(),
                Spdp_seq = row["SPDP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDP_SEQ"]),
                Spdp_tp = row["SPDP_TP"] == DBNull.Value ? string.Empty : row["SPDP_TP"].ToString(),
                Spdp_vou_cd = row["SPDP_VOU_CD"] == DBNull.Value ? string.Empty : row["SPDP_VOU_CD"].ToString()
            };
        }
    }
}
