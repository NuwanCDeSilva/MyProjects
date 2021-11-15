using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
    public class InvoiceTax
    {
        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// </summary>

        #region Private Members
        private string _sadx_inv_no;
        private Int32 _sadx_seq_no;
        private decimal _sadx_tax_amt;
        private decimal _sadx_tax_rt;
        private string _sadx_tax_tp;
        #endregion

        public string Sadx_inv_no { get { return _sadx_inv_no; } set { _sadx_inv_no = value; } }
        public Int32 Sadx_seq_no { get { return _sadx_seq_no; } set { _sadx_seq_no = value; } }
        public decimal Sadx_tax_amt { get { return _sadx_tax_amt; } set { _sadx_tax_amt = value; } }
        public decimal Sadx_tax_rt { get { return _sadx_tax_rt; } set { _sadx_tax_rt = value; } }
        public string Sadx_tax_tp { get { return _sadx_tax_tp; } set { _sadx_tax_tp = value; } }

        public static InvoiceTax ConvertTotal(DataRow row)
        {
            return new InvoiceTax
            {
                Sadx_inv_no = row["SADX_INV_NO"] == DBNull.Value ? string.Empty : row["SADX_INV_NO"].ToString(),
                Sadx_seq_no = row["SADX_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADX_SEQ_NO"]),
                Sadx_tax_amt = row["SADX_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SADX_TAX_AMT"]),
                Sadx_tax_rt = row["SADX_TAX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SADX_TAX_RT"]),
                Sadx_tax_tp = row["SADX_TAX_TP"] == DBNull.Value ? string.Empty : row["SADX_TAX_TP"].ToString()

            };
        }
    }
}

