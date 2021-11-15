using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{

    public class IncentiveSchIncDet
    {
        #region Private Members
        private Boolean _incid_alt;
        private Decimal _incid_amount;
        private string _incid_circ;
        private string _incid_desc;
        private Int32 _incid_dt_line;
        private Int32 _incid_inc_line;
        private Boolean _incid_is_alt_itm;
        private string _incid_itm;
        private Int32 _incid_line;
        private Int32 _incid_qty;
        private string _incid_ref;
        #endregion

        public Boolean Incid_alt
        {
            get { return _incid_alt; }
            set { _incid_alt = value; }
        }
        public Decimal Incid_amount
        {
            get { return _incid_amount; }
            set { _incid_amount = value; }
        }
        public string Incid_circ
        {
            get { return _incid_circ; }
            set { _incid_circ = value; }
        }
        public string Incid_desc
        {
            get { return _incid_desc; }
            set { _incid_desc = value; }
        }
        public Int32 Incid_dt_line
        {
            get { return _incid_dt_line; }
            set { _incid_dt_line = value; }
        }
        public Int32 Incid_inc_line
        {
            get { return _incid_inc_line; }
            set { _incid_inc_line = value; }
        }
        public Boolean Incid_is_alt_itm
        {
            get { return _incid_is_alt_itm; }
            set { _incid_is_alt_itm = value; }
        }
        public string Incid_itm
        {
            get { return _incid_itm; }
            set { _incid_itm = value; }
        }
        public Int32 Incid_line
        {
            get { return _incid_line; }
            set { _incid_line = value; }
        }
        public Int32 Incid_qty
        {
            get { return _incid_qty; }
            set { _incid_qty = value; }
        }
        public string Incid_ref
        {
            get { return _incid_ref; }
            set { _incid_ref = value; }
        }

        public static IncentiveSchIncDet Converter(DataRow row)
        {
            return new IncentiveSchIncDet
            {
                Incid_alt = row["INCID_ALT"] == DBNull.Value ? false : Convert.ToBoolean(row["INCID_ALT"]),
                Incid_amount = row["INCID_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INCID_AMOUNT"]),
                Incid_circ = row["INCID_CIRC"] == DBNull.Value ? string.Empty : row["INCID_CIRC"].ToString(),
                Incid_desc = row["INCID_DESC"] == DBNull.Value ? string.Empty : row["INCID_DESC"].ToString(),
                Incid_dt_line = row["INCID_DT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCID_DT_LINE"]),
                Incid_inc_line = row["INCID_INC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCID_INC_LINE"]),
                Incid_is_alt_itm = row["INCID_IS_ALT_ITM"] == DBNull.Value ? false : Convert.ToBoolean(row["INCID_IS_ALT_ITM"]),
                Incid_itm = row["INCID_ITM"] == DBNull.Value ? string.Empty : row["INCID_ITM"].ToString(),
                Incid_line = row["INCID_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCID_LINE"]),
                Incid_qty = row["INCID_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCID_QTY"]),
                Incid_ref = row["INCID_REF"] == DBNull.Value ? string.Empty : row["INCID_REF"].ToString()
            };
        }

    }
}
