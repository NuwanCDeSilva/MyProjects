using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Service_Req_Det_Sub
    {

        #region Private Members
        private Boolean _jrds_availabilty;
        private string _jrds_brand;
        private string _jrds_cre_by;
        private DateTime _jrds_cre_dt;
        private Boolean _jrds_issub;
        private string _jrds_itm_cd;
        private decimal _jrds_itm_cost;
        private string _jrds_itm_desc;
        private string _jrds_itm_stus;
        private string _jrds_itm_tp;
        private Int32 _jrds_jobline;
        private string _jrds_jobno;
        private Int32 _jrds_line;
        private string _jrds_model;
        private Boolean _jrds_need_replace;
        private decimal _jrds_qty;
        private string _jrds_repl_itmcd;
        private Int32 _jrds_repl_serid;
        private Boolean _jrds_rtn_wh;
        private Int32 _jrds_seq_no;
        private string _jrds_ser1;
        private string _jrds_ser2;
        private string _jrds_sjobno;
        private string _jrds_warr;
        private int _jrds_warr_period;
        private string _jrds_warr_rmk;

        public Boolean JBDS_SELECT { get; set; }


        #endregion

        #region Public Property Definition
        public Boolean Jrds_availabilty
        {
            get { return _jrds_availabilty; }
            set { _jrds_availabilty = value; }
        }
        public string Jrds_brand
        {
            get { return _jrds_brand; }
            set { _jrds_brand = value; }
        }
        public string Jrds_cre_by
        {
            get { return _jrds_cre_by; }
            set { _jrds_cre_by = value; }
        }
        public DateTime Jrds_cre_dt
        {
            get { return _jrds_cre_dt; }
            set { _jrds_cre_dt = value; }
        }
        public Boolean Jrds_issub
        {
            get { return _jrds_issub; }
            set { _jrds_issub = value; }
        }
        public string Jrds_itm_cd
        {
            get { return _jrds_itm_cd; }
            set { _jrds_itm_cd = value; }
        }
        public decimal Jrds_itm_cost
        {
            get { return _jrds_itm_cost; }
            set { _jrds_itm_cost = value; }
        }
        public string Jrds_itm_desc
        {
            get { return _jrds_itm_desc; }
            set { _jrds_itm_desc = value; }
        }
        public string Jrds_itm_stus
        {
            get { return _jrds_itm_stus; }
            set { _jrds_itm_stus = value; }
        }
        public string Jrds_itm_tp
        {
            get { return _jrds_itm_tp; }
            set { _jrds_itm_tp = value; }
        }
        public Int32 Jrds_jobline
        {
            get { return _jrds_jobline; }
            set { _jrds_jobline = value; }
        }
        public string Jrds_jobno
        {
            get { return _jrds_jobno; }
            set { _jrds_jobno = value; }
        }
        public Int32 Jrds_line
        {
            get { return _jrds_line; }
            set { _jrds_line = value; }
        }
        public string Jrds_model
        {
            get { return _jrds_model; }
            set { _jrds_model = value; }
        }
        public Boolean Jrds_need_replace
        {
            get { return _jrds_need_replace; }
            set { _jrds_need_replace = value; }
        }
        public decimal Jrds_qty
        {
            get { return _jrds_qty; }
            set { _jrds_qty = value; }
        }
        public string Jrds_repl_itmcd
        {
            get { return _jrds_repl_itmcd; }
            set { _jrds_repl_itmcd = value; }
        }
        public Int32 Jrds_repl_serid
        {
            get { return _jrds_repl_serid; }
            set { _jrds_repl_serid = value; }
        }
        public Boolean Jrds_rtn_wh
        {
            get { return _jrds_rtn_wh; }
            set { _jrds_rtn_wh = value; }
        }
        public Int32 Jrds_seq_no
        {
            get { return _jrds_seq_no; }
            set { _jrds_seq_no = value; }
        }
        public string Jrds_ser1
        {
            get { return _jrds_ser1; }
            set { _jrds_ser1 = value; }
        }
        public string Jrds_ser2
        {
            get { return _jrds_ser2; }
            set { _jrds_ser2 = value; }
        }
        public string Jrds_sjobno
        {
            get { return _jrds_sjobno; }
            set { _jrds_sjobno = value; }
        }
        public string Jrds_warr
        {
            get { return _jrds_warr; }
            set { _jrds_warr = value; }
        }
        public int Jrds_warr_period
        {
            get { return _jrds_warr_period; }
            set { _jrds_warr_period = value; }
        }
        public string Jrds_warr_rmk
        {
            get { return _jrds_warr_rmk; }
            set { _jrds_warr_rmk = value; }
        }
        #endregion

        #region Converters
        public static Service_Req_Det_Sub Converter(DataRow row)
        {
            return new Service_Req_Det_Sub
            {

                Jrds_availabilty = row["JRDS_AVAILABILTY"] == DBNull.Value ? false : Convert.ToBoolean(row["JRDS_AVAILABILTY"]),
                Jrds_brand = row["JRDS_BRAND"] == DBNull.Value ? string.Empty : row["JRDS_BRAND"].ToString(),
                Jrds_cre_by = row["JRDS_CRE_BY"] == DBNull.Value ? string.Empty : row["JRDS_CRE_BY"].ToString(),
                Jrds_cre_dt = row["JRDS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JRDS_CRE_DT"]),
                Jrds_issub = row["JRDS_ISSUB"] == DBNull.Value ? false : Convert.ToBoolean(row["JRDS_ISSUB"]),
                Jrds_itm_cd = row["JRDS_ITM_CD"] == DBNull.Value ? string.Empty : row["JRDS_ITM_CD"].ToString(),
                Jrds_itm_cost = row["JRDS_ITM_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JRDS_ITM_COST"]),
                Jrds_itm_desc = row["JRDS_ITM_DESC"] == DBNull.Value ? string.Empty : row["JRDS_ITM_DESC"].ToString(),
                Jrds_itm_stus = row["JRDS_ITM_STUS"] == DBNull.Value ? string.Empty : row["JRDS_ITM_STUS"].ToString(),
                Jrds_itm_tp = row["JRDS_ITM_TP"] == DBNull.Value ? string.Empty : row["JRDS_ITM_TP"].ToString(),
                Jrds_jobline = row["JRDS_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JRDS_JOBLINE"]),
                Jrds_jobno = row["JRDS_JOBNO"] == DBNull.Value ? string.Empty : row["JRDS_JOBNO"].ToString(),
                Jrds_line = row["JRDS_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JRDS_LINE"]),
                Jrds_model = row["JRDS_MODEL"] == DBNull.Value ? string.Empty : row["JRDS_MODEL"].ToString(),
                Jrds_need_replace = row["JRDS_NEED_REPLACE"] == DBNull.Value ? false : Convert.ToBoolean(row["JRDS_NEED_REPLACE"]),
                Jrds_qty = row["JRDS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JRDS_QTY"]),
                Jrds_repl_itmcd = row["JRDS_REPL_ITMCD"] == DBNull.Value ? string.Empty : row["JRDS_REPL_ITMCD"].ToString(),
                Jrds_repl_serid = row["JRDS_REPL_SERID"] == DBNull.Value ? 0 : Convert.ToInt32(row["JRDS_REPL_SERID"]),
                Jrds_rtn_wh = row["JRDS_RTN_WH"] == DBNull.Value ? false : Convert.ToBoolean(row["JRDS_RTN_WH"]),
                Jrds_seq_no = row["JRDS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JRDS_SEQ_NO"]),
                Jrds_ser1 = row["JRDS_SER1"] == DBNull.Value ? string.Empty : row["JRDS_SER1"].ToString(),
                Jrds_ser2 = row["JRDS_SER2"] == DBNull.Value ? string.Empty : row["JRDS_SER2"].ToString(),
                Jrds_sjobno = row["JRDS_SJOBNO"] == DBNull.Value ? string.Empty : row["JRDS_SJOBNO"].ToString(),
                Jrds_warr = row["JRDS_WARR"] == DBNull.Value ? string.Empty : row["JRDS_WARR"].ToString(),
                Jrds_warr_period = row["JRDS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt16(row["JRDS_WARR_PERIOD"]),
                Jrds_warr_rmk = row["JRDS_WARR_RMK"] == DBNull.Value ? string.Empty : row["JRDS_WARR_RMK"].ToString()

            };
        }
        #endregion
    }
}

