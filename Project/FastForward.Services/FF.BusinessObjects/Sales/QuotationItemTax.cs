using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class QuotationItemTax
    {
        #region Private Members
        private string _qdt_itm_cd;
        private string _qdt_itm_tp;
        private Int32 _qdt_line_no;
        private string _qdt_no;
        private Int32 _qdt_seq_no;
        private decimal _qdt_tax_amt;
        private decimal _qdt_tax_rate;
        private string _qdt_tax_tp;
        private Int32 _qdt_tline_no;
        #endregion

        public string Qdt_itm_cd
        {
            get { return _qdt_itm_cd; }
            set { _qdt_itm_cd = value; }
        }
        public string Qdt_itm_tp
        {
            get { return _qdt_itm_tp; }
            set { _qdt_itm_tp = value; }
        }
        public Int32 Qdt_line_no
        {
            get { return _qdt_line_no; }
            set { _qdt_line_no = value; }
        }
        public string Qdt_no
        {
            get { return _qdt_no; }
            set { _qdt_no = value; }
        }
        public Int32 Qdt_seq_no
        {
            get { return _qdt_seq_no; }
            set { _qdt_seq_no = value; }
        }
        public decimal Qdt_tax_amt
        {
            get { return _qdt_tax_amt; }
            set { _qdt_tax_amt = value; }
        }
        public decimal Qdt_tax_rate
        {
            get { return _qdt_tax_rate; }
            set { _qdt_tax_rate = value; }
        }
        public string Qdt_tax_tp
        {
            get { return _qdt_tax_tp; }
            set { _qdt_tax_tp = value; }
        }
        public Int32 Qdt_tline_no
        {
            get { return _qdt_tline_no; }
            set { _qdt_tline_no = value; }
        }

        public static QuotationItemTax Converter(DataRow row)
        {
            return new QuotationItemTax
            {
                Qdt_itm_cd = row["QDT_ITM_CD"] == DBNull.Value ? string.Empty : row["QDT_ITM_CD"].ToString(),
                Qdt_itm_tp = row["QDT_ITM_TP"] == DBNull.Value ? string.Empty : row["QDT_ITM_TP"].ToString(),
                Qdt_line_no = row["QDT_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QDT_LINE_NO"]),
                Qdt_no = row["QDT_NO"] == DBNull.Value ? string.Empty : row["QDT_NO"].ToString(),
                Qdt_seq_no = row["QDT_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QDT_SEQ_NO"]),
                Qdt_tax_amt = row["QDT_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QDT_TAX_AMT"]),
                Qdt_tax_rate = row["QDT_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QDT_TAX_RATE"]),
                Qdt_tax_tp = row["QDT_TAX_TP"] == DBNull.Value ? string.Empty : row["QDT_TAX_TP"].ToString(),
                Qdt_tline_no = row["QDT_TLINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QDT_TLINE_NO"])

            };
        }

    }
}
