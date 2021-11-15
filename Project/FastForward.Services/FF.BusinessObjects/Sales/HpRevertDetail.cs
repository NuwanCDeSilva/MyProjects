using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
   public class HpRevertDetail
    {
        #region Private Members
        private string _hrd_cre_by;
        private DateTime _hrd_cre_dt;
        private string _hrd_itm;
        private decimal _hrd_itm_bal;
        private decimal _hrd_itm_bal_cap;
        private decimal _hrd_itm_bal_intr;
        private Int32 _hrd_line_no;
        private Int32 _hrd_seq;
        #endregion

        public string Hrd_cre_by { get { return _hrd_cre_by; } set { _hrd_cre_by = value; } }
        public DateTime Hrd_cre_dt { get { return _hrd_cre_dt; } set { _hrd_cre_dt = value; } }
        public string Hrd_itm { get { return _hrd_itm; } set { _hrd_itm = value; } }
        public decimal Hrd_itm_bal { get { return _hrd_itm_bal; } set { _hrd_itm_bal = value; } }
        public decimal Hrd_itm_bal_cap { get { return _hrd_itm_bal_cap; } set { _hrd_itm_bal_cap = value; } }
        public decimal Hrd_itm_bal_intr { get { return _hrd_itm_bal_intr; } set { _hrd_itm_bal_intr = value; } }
        public Int32 Hrd_line_no { get { return _hrd_line_no; } set { _hrd_line_no = value; } }
        public Int32 Hrd_seq { get { return _hrd_seq; } set { _hrd_seq = value; } }

        public decimal Hrd_itm_paid_bal { get; set; } //Tharindu 2018-04-13
        public decimal Hrd_itm_paid_cap { get; set; }
        public decimal Hrd_itm_paid_int { get; set; }

        public static HpRevertDetail Converter(DataRow row)
        {
            return new HpRevertDetail
            {
                Hrd_cre_by = row["HRD_CRE_BY"] == DBNull.Value ? string.Empty : row["HRD_CRE_BY"].ToString(),
                Hrd_cre_dt = row["HRD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRD_CRE_DT"]),
                Hrd_itm = row["HRD_ITM"] == DBNull.Value ? string.Empty : row["HRD_ITM"].ToString(),
                Hrd_itm_bal = row["HRD_ITM_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRD_ITM_BAL"]),
                Hrd_itm_bal_cap = row["HRD_ITM_BAL_CAP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRD_ITM_BAL_CAP"]),
                Hrd_itm_bal_intr = row["HRD_ITM_BAL_INTR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRD_ITM_BAL_INTR"]),
                Hrd_line_no = row["HRD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["HRD_LINE_NO"]),
                Hrd_seq = row["HRD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HRD_SEQ"])

            };
        }
    }
}

