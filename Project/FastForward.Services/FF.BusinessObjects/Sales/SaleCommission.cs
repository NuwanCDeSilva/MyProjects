using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
   public class SaleCommission
    {

        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// </summary>

        #region Private Members
        private decimal _sac_add_comm;
        private decimal _sac_add_comm_rate;
        private Boolean _sac_add_epf;
        private string _sac_calc_on;
        private decimal _sac_ce_amt;
        private decimal _sac_ce_rate;
        private Int32 _sac_comm_amt;
        private decimal _sac_comm_amt_final;
        private Int32 _sac_comm_line;
        private decimal _sac_comm_rate;
        private decimal _sac_comm_rate_final;
        private DateTime _sac_crt_dt;
        private string _sac_invoice_no;
        private string _sac_itm_cd;
        private Int32 _sac_itm_line;
        private string _sac_pay_mode;
        private Int32 _sac_seq_no;
        #endregion

        public decimal Sac_add_comm
        {
            get { return _sac_add_comm; }
            set { _sac_add_comm = value; }
        }
        public decimal Sac_add_comm_rate
        {
            get { return _sac_add_comm_rate; }
            set { _sac_add_comm_rate = value; }
        }
        public Boolean Sac_add_epf
        {
            get { return _sac_add_epf; }
            set { _sac_add_epf = value; }
        }
        public string Sac_calc_on
        {
            get { return _sac_calc_on; }
            set { _sac_calc_on = value; }
        }
        public decimal Sac_ce_amt
        {
            get { return _sac_ce_amt; }
            set { _sac_ce_amt = value; }
        }
        public decimal Sac_ce_rate
        {
            get { return _sac_ce_rate; }
            set { _sac_ce_rate = value; }
        }
        public Int32 Sac_comm_amt
        {
            get { return _sac_comm_amt; }
            set { _sac_comm_amt = value; }
        }
        public decimal Sac_comm_amt_final
        {
            get { return _sac_comm_amt_final; }
            set { _sac_comm_amt_final = value; }
        }
        public Int32 Sac_comm_line
        {
            get { return _sac_comm_line; }
            set { _sac_comm_line = value; }
        }
        public decimal Sac_comm_rate
        {
            get { return _sac_comm_rate; }
            set { _sac_comm_rate = value; }
        }
        public decimal Sac_comm_rate_final
        {
            get { return _sac_comm_rate_final; }
            set { _sac_comm_rate_final = value; }
        }
        public DateTime Sac_crt_dt
        {
            get { return _sac_crt_dt; }
            set { _sac_crt_dt = value; }
        }
        public string Sac_invoice_no
        {
            get { return _sac_invoice_no; }
            set { _sac_invoice_no = value; }
        }
        public string Sac_itm_cd
        {
            get { return _sac_itm_cd; }
            set { _sac_itm_cd = value; }
        }
        public Int32 Sac_itm_line
        {
            get { return _sac_itm_line; }
            set { _sac_itm_line = value; }
        }
        public string Sac_pay_mode
        {
            get { return _sac_pay_mode; }
            set { _sac_pay_mode = value; }
        }
        public Int32 Sac_seq_no
        {
            get { return _sac_seq_no; }
            set { _sac_seq_no = value; }
        }
        public string sac_comm_cd { get; set; }
        public string sac_comm_circular { get; set; }
        public static SaleCommission ConvertTotal(DataRow row)
        {
            return new SaleCommission
            {
                Sac_add_comm = row["SAC_ADD_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_ADD_COMM"]),
                Sac_add_comm_rate = row["SAC_ADD_COMM_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_ADD_COMM_RATE"]),
                Sac_add_epf = row["SAC_ADD_EPF"] == DBNull.Value ? false : Convert.ToBoolean(row["SAC_ADD_EPF"]),
                Sac_calc_on = row["SAC_CALC_ON"] == DBNull.Value ? "" : Convert.ToString(row["SAC_CALC_ON"]),
                Sac_ce_amt = row["SAC_CE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_CE_AMT"]),
                Sac_ce_rate = row["SAC_CE_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_CE_RATE"]),
                Sac_comm_amt = row["SAC_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAC_COMM_AMT"]),
                Sac_comm_amt_final = row["SAC_COMM_AMT_FINAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_COMM_AMT_FINAL"]),
                Sac_comm_line = row["SAC_COMM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAC_COMM_LINE"]),
                Sac_comm_rate = row["SAC_COMM_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_COMM_RATE"]),
                Sac_comm_rate_final = row["SAC_COMM_RATE_FINAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_COMM_RATE_FINAL"]),
                Sac_crt_dt = row["SAC_CRT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAC_CRT_DT"]),
                Sac_invoice_no = row["SAC_INVOICE_NO"] == DBNull.Value ? string.Empty : row["SAC_INVOICE_NO"].ToString(),
                Sac_itm_cd = row["SAC_ITM_CD"] == DBNull.Value ? string.Empty : row["SAC_ITM_CD"].ToString(),
                Sac_itm_line = row["SAC_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAC_ITM_LINE"]),
                Sac_pay_mode = row["SAC_PAY_MODE"] == DBNull.Value ? string.Empty : row["SAC_PAY_MODE"].ToString(),
                Sac_seq_no = row["SAC_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAC_SEQ_NO"])

            };
        }


    }
}

