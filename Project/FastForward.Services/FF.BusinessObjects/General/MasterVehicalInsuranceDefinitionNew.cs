using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterVehicalInsuranceDefinitionNew
    {
        #region Private Members
        private string _svid_com;
        private string _svid_cre_by;
        private DateTime _svid_cre_dt;
        private DateTime _svid_from_dt;
        private string _svid_ins_com_cd;
        private Boolean _svid_is_req;
        private string _svid_itm;
        private decimal _svid_itm_val;
        private string _svid_pb;
        private string _svid_pb_lvl;
        private string _svid_polc_cd;
        private string _svid_promo;
        private string _svid_pty_cd;
        private string _svid_pty_tp;
        private string _svid_sale_tp;
        private Int32 _svid_seq;
        private Int32 _svid_term;
        private DateTime _svid_to_dt;
        private decimal _svid_val;
        private decimal _svid_frm_val;
        private decimal _svid_to_val;
        private string _svid_ins_tp;
        private string _svid_cat1;
        private string _svid_cat2;
        private string _svid_brnd;
        private Int32 _svid_is_rt;

        #endregion

        public string Svid_cat1
        {
            get { return _svid_cat1; }
            set { _svid_cat1 = value; }
        }
        public string Svid_cat2
        {
            get { return _svid_cat2; }
            set { _svid_cat2 = value; }
        }
        public string Svid_brnd
        {
            get { return _svid_brnd; }
            set { _svid_brnd = value; }
        }
        public Int32 Svid_is_rt
        {
            get { return _svid_is_rt; }
            set { _svid_is_rt = value; }
        }

        public decimal Svid_frm_val
        {
            get { return _svid_frm_val; }
            set { _svid_frm_val = value; }
        }

        public decimal Svid_to_val
        {
            get { return _svid_to_val; }
            set { _svid_to_val = value; }
        }

        public string Svid_ins_tp
        {
            get { return _svid_ins_tp; }
            set { _svid_ins_tp = value; }
        }

        public string Svid_com
        {
            get { return _svid_com; }
            set { _svid_com = value; }
        }
        public string Svid_cre_by
        {
            get { return _svid_cre_by; }
            set { _svid_cre_by = value; }
        }
        public DateTime Svid_cre_dt
        {
            get { return _svid_cre_dt; }
            set { _svid_cre_dt = value; }
        }
        public DateTime Svid_from_dt
        {
            get { return _svid_from_dt; }
            set { _svid_from_dt = value; }
        }
        public string Svid_ins_com_cd
        {
            get { return _svid_ins_com_cd; }
            set { _svid_ins_com_cd = value; }
        }
        public Boolean Svid_is_req
        {
            get { return _svid_is_req; }
            set { _svid_is_req = value; }
        }
        public string Svid_itm
        {
            get { return _svid_itm; }
            set { _svid_itm = value; }
        }
        public decimal Svid_itm_val
        {
            get { return _svid_itm_val; }
            set { _svid_itm_val = value; }
        }
        public string Svid_pb
        {
            get { return _svid_pb; }
            set { _svid_pb = value; }
        }
        public string Svid_pb_lvl
        {
            get { return _svid_pb_lvl; }
            set { _svid_pb_lvl = value; }
        }
        public string Svid_polc_cd
        {
            get { return _svid_polc_cd; }
            set { _svid_polc_cd = value; }
        }
        public string Svid_promo
        {
            get { return _svid_promo; }
            set { _svid_promo = value; }
        }
        public string Svid_pty_cd
        {
            get { return _svid_pty_cd; }
            set { _svid_pty_cd = value; }
        }
        public string Svid_pty_tp
        {
            get { return _svid_pty_tp; }
            set { _svid_pty_tp = value; }
        }
        public string Svid_sale_tp
        {
            get { return _svid_sale_tp; }
            set { _svid_sale_tp = value; }
        }
        public Int32 Svid_seq
        {
            get { return _svid_seq; }
            set { _svid_seq = value; }
        }
        public Int32 Svid_term
        {
            get { return _svid_term; }
            set { _svid_term = value; }
        }
        public DateTime Svid_to_dt
        {
            get { return _svid_to_dt; }
            set { _svid_to_dt = value; }
        }
        public decimal Svid_val
        {
            get { return _svid_val; }
            set { _svid_val = value; }
        }

        public static MasterVehicalInsuranceDefinitionNew Converter(DataRow row)
        {
            return new MasterVehicalInsuranceDefinitionNew
            {
                Svid_com = row["SVID_COM"] == DBNull.Value ? string.Empty : row["SVID_COM"].ToString(),
                Svid_cre_by = row["SVID_CRE_BY"] == DBNull.Value ? string.Empty : row["SVID_CRE_BY"].ToString(),
                Svid_cre_dt = row["SVID_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVID_CRE_DT"]),
                Svid_from_dt = row["SVID_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVID_FROM_DT"]),
                Svid_ins_com_cd = row["SVID_INS_COM_CD"] == DBNull.Value ? string.Empty : row["SVID_INS_COM_CD"].ToString(),
                Svid_is_req = row["SVID_IS_REQ"] == DBNull.Value ? false : Convert.ToBoolean(row["SVID_IS_REQ"]),
                Svid_itm = row["SVID_ITM"] == DBNull.Value ? string.Empty : row["SVID_ITM"].ToString(),
                Svid_itm_val = row["SVID_ITM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVID_ITM_VAL"]),
                Svid_pb = row["SVID_PB"] == DBNull.Value ? string.Empty : row["SVID_PB"].ToString(),
                Svid_pb_lvl = row["SVID_PB_LVL"] == DBNull.Value ? string.Empty : row["SVID_PB_LVL"].ToString(),
                Svid_polc_cd = row["SVID_POLC_CD"] == DBNull.Value ? string.Empty : row["SVID_POLC_CD"].ToString(),
                Svid_promo = row["SVID_PROMO"] == DBNull.Value ? string.Empty : row["SVID_PROMO"].ToString(),
                Svid_pty_cd = row["SVID_PTY_CD"] == DBNull.Value ? string.Empty : row["SVID_PTY_CD"].ToString(),
                Svid_pty_tp = row["SVID_PTY_TP"] == DBNull.Value ? string.Empty : row["SVID_PTY_TP"].ToString(),
                Svid_sale_tp = row["SVID_SALE_TP"] == DBNull.Value ? string.Empty : row["SVID_SALE_TP"].ToString(),
                Svid_seq = row["SVID_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVID_SEQ"]),
                Svid_term = row["SVID_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVID_TERM"]),
                Svid_to_dt = row["SVID_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVID_TO_DT"]),
                Svid_val = row["SVID_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVID_VAL"]),
                Svid_frm_val = row["SVID_FRM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVID_FRM_VAL"]),
                Svid_to_val = row["SVID_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVID_TO_VAL"]),
                Svid_ins_tp = row["SVID_INS_TP"] == DBNull.Value ? string.Empty : row["SVID_INS_TP"].ToString(),
                Svid_cat1 = row["SVID_CAT1"] == DBNull.Value ? string.Empty : row["SVID_CAT1"].ToString(),
                Svid_cat2 = row["SVID_CAT2"] == DBNull.Value ? string.Empty : row["SVID_CAT2"].ToString(),
                Svid_brnd = row["SVID_BRND"] == DBNull.Value ? string.Empty : row["SVID_BRND"].ToString(),
                Svid_is_rt = row["SVID_IS_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVID_IS_RT"])

            };
        }

    }
}
