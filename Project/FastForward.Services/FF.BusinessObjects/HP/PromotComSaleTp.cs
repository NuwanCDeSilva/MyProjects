using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PromotComSaleTp
    {
        #region Private Members
        private Int32 _hpcst_line;
        private string _hpcst_sale_tp;
        private Int32 _hpcst_seq;
        #endregion

        public Int32 Hpcst_line
        {
            get { return _hpcst_line; }
            set { _hpcst_line = value; }
        }
        public string Hpcst_sale_tp
        {
            get { return _hpcst_sale_tp; }
            set { _hpcst_sale_tp = value; }
        }
        public Int32 Hpcst_seq
        {
            get { return _hpcst_seq; }
            set { _hpcst_seq = value; }
        }
        public static PromotComSaleTp Converter(DataRow row)
        {
            return new PromotComSaleTp
            {
                Hpcst_line = row["HPCST_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCST_LINE"]),
                Hpcst_sale_tp = row["HPCST_SALE_TP"] == DBNull.Value ? string.Empty : row["HPCST_SALE_TP"].ToString(),
                Hpcst_seq = row["HPCST_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCST_SEQ"])

            };
        }

    }
}
