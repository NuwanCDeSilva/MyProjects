using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PromotComDet
    {
        #region Private Members
        private decimal _hpcd_com_amt;
        private decimal _hpcd_com_rt;
        private Int32 _hpcd_from_qty;
        private Int32 _hpcd_line;
        private Int32 _hpcd_seq;
        private Int32 _hpcd_to_qty;
        #endregion

        public decimal Hpcd_com_amt
        {
            get { return _hpcd_com_amt; }
            set { _hpcd_com_amt = value; }
        }
        public decimal Hpcd_com_rt
        {
            get { return _hpcd_com_rt; }
            set { _hpcd_com_rt = value; }
        }
        public Int32 Hpcd_from_qty
        {
            get { return _hpcd_from_qty; }
            set { _hpcd_from_qty = value; }
        }
        public Int32 Hpcd_line
        {
            get { return _hpcd_line; }
            set { _hpcd_line = value; }
        }
        public Int32 Hpcd_seq
        {
            get { return _hpcd_seq; }
            set { _hpcd_seq = value; }
        }
        public Int32 Hpcd_to_qty
        {
            get { return _hpcd_to_qty; }
            set { _hpcd_to_qty = value; }
        }

        public static PromotComDet Converter(DataRow row)
        {
            return new PromotComDet
            {
                Hpcd_com_amt = row["HPCD_COM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPCD_COM_AMT"]),
                Hpcd_com_rt = row["HPCD_COM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPCD_COM_RT"]),
                Hpcd_from_qty = row["HPCD_FROM_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCD_FROM_QTY"]),
                Hpcd_line = row["HPCD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCD_LINE"]),
                Hpcd_seq = row["HPCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCD_SEQ"]),
                Hpcd_to_qty = row["HPCD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCD_TO_QTY"])

            };
        }

    }
}
