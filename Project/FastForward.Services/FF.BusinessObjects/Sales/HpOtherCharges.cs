using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//ADD BY DARSHANA 16/07/2012
namespace FF.BusinessObjects
{
    [Serializable]
    public class HpOtherCharges
    {
        #region Private Members
        private string _hoc_brd;
        private string _hoc_cat;
        private string _hoc_comm_cat;
        private string _hoc_cre_by;
        private DateTime _hoc_cre_dt;
        private string _hoc_cus_cd;
        private string _hoc_desc;
        private DateTime _hoc_from_dt;
        private string _hoc_itm;
        private string _hoc_main_cat;
        private string _hoc_pb;
        private string _hoc_pb_lvl;
        private string _hoc_pro;
        private string _hoc_sch_cd;
        private Int32 _hoc_seq;
        private string _hoc_ser;
        private DateTime _hoc_to_dt;
        private string _hoc_tp;
        private decimal _hoc_val;
        #endregion

        public string Hoc_brd
        {
            get { return _hoc_brd; }
            set { _hoc_brd = value; }
        }
        public string Hoc_cat
        {
            get { return _hoc_cat; }
            set { _hoc_cat = value; }
        }
        public string Hoc_comm_cat
        {
            get { return _hoc_comm_cat; }
            set { _hoc_comm_cat = value; }
        }
        public string Hoc_cre_by
        {
            get { return _hoc_cre_by; }
            set { _hoc_cre_by = value; }
        }
        public DateTime Hoc_cre_dt
        {
            get { return _hoc_cre_dt; }
            set { _hoc_cre_dt = value; }
        }
        public string Hoc_cus_cd
        {
            get { return _hoc_cus_cd; }
            set { _hoc_cus_cd = value; }
        }
        public string Hoc_desc
        {
            get { return _hoc_desc; }
            set { _hoc_desc = value; }
        }
        public DateTime Hoc_from_dt
        {
            get { return _hoc_from_dt; }
            set { _hoc_from_dt = value; }
        }
        public string Hoc_itm
        {
            get { return _hoc_itm; }
            set { _hoc_itm = value; }
        }
        public string Hoc_main_cat
        {
            get { return _hoc_main_cat; }
            set { _hoc_main_cat = value; }
        }
        public string Hoc_pb
        {
            get { return _hoc_pb; }
            set { _hoc_pb = value; }
        }
        public string Hoc_pb_lvl
        {
            get { return _hoc_pb_lvl; }
            set { _hoc_pb_lvl = value; }
        }
        public string Hoc_pro
        {
            get { return _hoc_pro; }
            set { _hoc_pro = value; }
        }
        public string Hoc_sch_cd
        {
            get { return _hoc_sch_cd; }
            set { _hoc_sch_cd = value; }
        }
        public Int32 Hoc_seq
        {
            get { return _hoc_seq; }
            set { _hoc_seq = value; }
        }
        public string Hoc_ser
        {
            get { return _hoc_ser; }
            set { _hoc_ser = value; }
        }
        public DateTime Hoc_to_dt
        {
            get { return _hoc_to_dt; }
            set { _hoc_to_dt = value; }
        }
        public string Hoc_tp
        {
            get { return _hoc_tp; }
            set { _hoc_tp = value; }
        }
        public decimal Hoc_val
        {
            get { return _hoc_val; }
            set { _hoc_val = value; }
        }

        public static HpOtherCharges Converter(DataRow row)
        {
            return new HpOtherCharges
            {
                Hoc_brd = row["HOC_BRD"] == DBNull.Value ? string.Empty : row["HOC_BRD"].ToString(),
                Hoc_cat = row["HOC_CAT"] == DBNull.Value ? string.Empty : row["HOC_CAT"].ToString(),
                Hoc_comm_cat = row["HOC_COMM_CAT"] == DBNull.Value ? string.Empty : row["HOC_COMM_CAT"].ToString(),
                Hoc_cre_by = row["HOC_CRE_BY"] == DBNull.Value ? string.Empty : row["HOC_CRE_BY"].ToString(),
                Hoc_cre_dt = row["HOC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HOC_CRE_DT"]),
                Hoc_cus_cd = row["HOC_CUS_CD"] == DBNull.Value ? string.Empty : row["HOC_CUS_CD"].ToString(),
                Hoc_desc = row["HOC_DESC"] == DBNull.Value ? string.Empty : row["HOC_DESC"].ToString(),
                Hoc_from_dt = row["HOC_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HOC_FROM_DT"]),
                Hoc_itm = row["HOC_ITM"] == DBNull.Value ? string.Empty : row["HOC_ITM"].ToString(),
                Hoc_main_cat = row["HOC_MAIN_CAT"] == DBNull.Value ? string.Empty : row["HOC_MAIN_CAT"].ToString(),
                Hoc_pb = row["HOC_PB"] == DBNull.Value ? string.Empty : row["HOC_PB"].ToString(),
                Hoc_pb_lvl = row["HOC_PB_LVL"] == DBNull.Value ? string.Empty : row["HOC_PB_LVL"].ToString(),
                Hoc_pro = row["HOC_PRO"] == DBNull.Value ? string.Empty : row["HOC_PRO"].ToString(),
                Hoc_sch_cd = row["HOC_SCH_CD"] == DBNull.Value ? string.Empty : row["HOC_SCH_CD"].ToString(),
                Hoc_seq = row["HOC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HOC_SEQ"]),
                Hoc_ser = row["HOC_SER"] == DBNull.Value ? string.Empty : row["HOC_SER"].ToString(),
                Hoc_to_dt = row["HOC_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HOC_TO_DT"]),
                Hoc_tp = row["HOC_TP"] == DBNull.Value ? string.Empty : row["HOC_TP"].ToString(),
                Hoc_val = row["HOC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HOC_VAL"])

            };
        }

    }
}
