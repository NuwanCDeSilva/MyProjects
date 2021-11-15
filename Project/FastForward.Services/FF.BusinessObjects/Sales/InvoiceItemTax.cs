using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
    public class InvoiceItemTax
    {
        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// </summary>
        /// 
        #region Private Members
        private string _satx_inv_no;
        private string _satx_itm_cd;
        private Int32 _satx_itm_line;
        private decimal _satx_itm_tax_amt;
        private decimal _satx_itm_tax_rt;
        private string _satx_itm_tax_tp;
        private Int32 _satx_job_line;
        private string _satx_job_no;
        private Int32 _satx_seq_no;
        #endregion

        public string Satx_inv_no { get { return _satx_inv_no; } set { _satx_inv_no = value; } }
        public string Satx_itm_cd { get { return _satx_itm_cd; } set { _satx_itm_cd = value; } }
        public Int32 Satx_itm_line { get { return _satx_itm_line; } set { _satx_itm_line = value; } }
        public decimal Satx_itm_tax_amt { get { return _satx_itm_tax_amt; } set { _satx_itm_tax_amt = value; } }
        public decimal Satx_itm_tax_rt { get { return _satx_itm_tax_rt; } set { _satx_itm_tax_rt = value; } }
        public string Satx_itm_tax_tp { get { return _satx_itm_tax_tp; } set { _satx_itm_tax_tp = value; } }
        public Int32 Satx_job_line { get { return _satx_job_line; } set { _satx_job_line = value; } }
        public string Satx_job_no { get { return _satx_job_no; } set { _satx_job_no = value; } }
        public Int32 Satx_seq_no { get { return _satx_seq_no; } set { _satx_seq_no = value; } }

        public static InvoiceItemTax ConvertTotal(DataRow row)
        {
            return new InvoiceItemTax
            {
                Satx_inv_no = row["SATX_INV_NO"] == DBNull.Value ? string.Empty : row["SATX_INV_NO"].ToString(),
                Satx_itm_cd = row["SATX_ITM_CD"] == DBNull.Value ? string.Empty : row["SATX_ITM_CD"].ToString(),
                Satx_itm_line = row["SATX_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SATX_ITM_LINE"]),
                Satx_itm_tax_amt = row["SATX_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SATX_ITM_TAX_AMT"]),
                Satx_itm_tax_rt = row["SATX_ITM_TAX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SATX_ITM_TAX_RT"]),
                Satx_itm_tax_tp = row["SATX_ITM_TAX_TP"] == DBNull.Value ? string.Empty : row["SATX_ITM_TAX_TP"].ToString(),
                Satx_job_line = row["SATX_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SATX_JOB_LINE"]),
                Satx_job_no = row["SATX_JOB_NO"] == DBNull.Value ? string.Empty : row["SATX_JOB_NO"].ToString(),
                Satx_seq_no = row["SATX_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SATX_SEQ_NO"])

            };
        }
    }
}

