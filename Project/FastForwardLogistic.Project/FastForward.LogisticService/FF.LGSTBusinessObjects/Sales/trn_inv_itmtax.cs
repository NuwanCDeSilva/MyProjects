using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // Computer :- ITPD18  | User :- subodanam On 06-Jul-2017 03:55:25
    //===========================================================================================================

    public class trn_inv_itmtax
    {
        public Int32 Tiit_seq_no { get; set; }
        public String Tiit_inv_no { get; set; }
        public String Tiid_com_cd { get; set; }
        public String Tiit_tax_element { get; set; }
        public String Tiid_tax_type { get; set; }
        public Decimal Tiid_tax_rate { get; set; }
        public Decimal Tiid_tax_unc_rate { get; set; }
        public Decimal Tiid_tax_clb_rate { get; set; }
        public Decimal Tiid_tax_unc_amt { get; set; }
        public Decimal Tiid_tax_clb_amt { get; set; }
        public String Tiid_tax_job_no { get; set; }
        public String Tiid_tax_ser_cd { get; set; }
        public static trn_inv_itmtax Converter(DataRow row)
        {
            return new trn_inv_itmtax
            {
                Tiit_seq_no = row["TIIT_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TIIT_SEQ_NO"].ToString()),
                Tiit_inv_no = row["TIIT_INV_NO"] == DBNull.Value ? string.Empty : row["TIIT_INV_NO"].ToString(),
                Tiid_com_cd = row["TIID_COM_CD"] == DBNull.Value ? string.Empty : row["TIID_COM_CD"].ToString(),
                Tiit_tax_element = row["TIIT_TAX_ELEMENT"] == DBNull.Value ? string.Empty : row["TIIT_TAX_ELEMENT"].ToString(),
                Tiid_tax_type = row["TIID_TAX_TYPE"] == DBNull.Value ? string.Empty : row["TIID_TAX_TYPE"].ToString(),
                Tiid_tax_rate = row["TIID_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TIID_TAX_RATE"].ToString()),
                Tiid_tax_unc_rate = row["TIID_TAX_UNC_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TIID_TAX_UNC_RATE"].ToString()),
                Tiid_tax_clb_rate = row["TIID_TAX_CLB_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TIID_TAX_CLB_RATE"].ToString()),
                Tiid_tax_unc_amt = row["TIID_TAX_UNC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TIID_TAX_UNC_AMT"].ToString()),
                Tiid_tax_clb_amt = row["TIID_TAX_CLB_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TIID_TAX_CLB_AMT"].ToString()),
                Tiid_tax_job_no = row["TIID_TAX_JOB_NO"] == DBNull.Value ? string.Empty : row["TIID_TAX_JOB_NO"].ToString(),
                Tiid_tax_ser_cd = row["TIID_TAX_SER_CD"] == DBNull.Value ? string.Empty : row["TIID_TAX_SER_CD"].ToString()
            };
        }
    }
}

