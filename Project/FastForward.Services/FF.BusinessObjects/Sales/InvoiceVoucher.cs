using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class InvoiceVoucher
    {
        #region Private Members
        private Int32 _stvo_bookno;
        private string _stvo_cre_by;
        private DateTime _stvo_cre_when;
        private string _stvo_gv_itm;
        private string _stvo_inv_no;
        private string _stvo_itm_cd;
        private Int32 _stvo_pageno;
        private string _stvo_prefix;
        private decimal _stvo_price;
        private Int32 _stvo_stus;
        #endregion

        public Int32 Stvo_bookno { get { return _stvo_bookno; } set { _stvo_bookno = value; } }
        public string Stvo_cre_by { get { return _stvo_cre_by; } set { _stvo_cre_by = value; } }
        public DateTime Stvo_cre_when { get { return _stvo_cre_when; } set { _stvo_cre_when = value; } }
        public string Stvo_gv_itm { get { return _stvo_gv_itm; } set { _stvo_gv_itm = value; } }
        public string Stvo_inv_no { get { return _stvo_inv_no; } set { _stvo_inv_no = value; } }
        public string Stvo_itm_cd { get { return _stvo_itm_cd; } set { _stvo_itm_cd = value; } }
        public Int32 Stvo_pageno { get { return _stvo_pageno; } set { _stvo_pageno = value; } }
        public string Stvo_prefix { get { return _stvo_prefix; } set { _stvo_prefix = value; } }
        public decimal Stvo_price { get { return _stvo_price; } set { _stvo_price = value; } }
        public Int32 Stvo_stus { get { return _stvo_stus; } set { _stvo_stus = value; } }

        public static InvoiceVoucher Converter(DataRow row)
        {
            return new InvoiceVoucher
            {
                Stvo_bookno = row["STVO_BOOKNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["STVO_BOOKNO"]),
                Stvo_cre_by = row["STVO_CRE_BY"] == DBNull.Value ? string.Empty : row["STVO_CRE_BY"].ToString(),
                Stvo_cre_when = row["STVO_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["STVO_CRE_WHEN"]),
                Stvo_gv_itm = row["STVO_GV_ITM"] == DBNull.Value ? string.Empty : row["STVO_GV_ITM"].ToString(),
                Stvo_inv_no = row["STVO_INV_NO"] == DBNull.Value ? string.Empty : row["STVO_INV_NO"].ToString(),
                Stvo_itm_cd = row["STVO_ITM_CD"] == DBNull.Value ? string.Empty : row["STVO_ITM_CD"].ToString(),
                Stvo_pageno = row["STVO_PAGENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["STVO_PAGENO"]),
                Stvo_prefix = row["STVO_PREFIX"] == DBNull.Value ? string.Empty : row["STVO_PREFIX"].ToString(),
                Stvo_price = row["STVO_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["STVO_PRICE"]),
                Stvo_stus = row["Stvo_stus"] == DBNull.Value ? 0 : Convert.ToInt32(row["Stvo_stus"])

            };
        }
    }
}
