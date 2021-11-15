using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class PriceCombinedItemRef : MasterItem
    {
        //Written By Prabhath on 03/05/2012
        // Mapped Table - sar_pb_com
        #region Private Members

        private Boolean _sapc_increse;
        private string _sapc_itm_cd;
        private Int32 _sapc_itm_line;
        private string _sapc_main_itm_cd;
        private Int32 _sapc_main_line;
        private string _sapc_main_ser;
        private Int32 _sapc_pb_seq;
        private decimal _sapc_price;
        private decimal _sapc_qty;
        private string _sapc_sub_ser;
        private Boolean _sapc_tot_com;
        //Added by Prabhath on 1/4/2013 for control similar item
        private string _similer_item;
        //Added by Prabhath on 12/07/2013 for control item status
        private string _status;

        #endregion

        public Boolean Sapc_increse { get { return _sapc_increse; } set { _sapc_increse = value; } }
        public string Sapc_itm_cd { get { return _sapc_itm_cd; } set { _sapc_itm_cd = value; } }
        public Int32 Sapc_itm_line { get { return _sapc_itm_line; } set { _sapc_itm_line = value; } }
        public string Sapc_main_itm_cd { get { return _sapc_main_itm_cd; } set { _sapc_main_itm_cd = value; } }
        public Int32 Sapc_main_line { get { return _sapc_main_line; } set { _sapc_main_line = value; } }
        public string Sapc_main_ser { get { return _sapc_main_ser; } set { _sapc_main_ser = value; } }
        public Int32 Sapc_pb_seq { get { return _sapc_pb_seq; } set { _sapc_pb_seq = value; } }
        public decimal Sapc_price { get { return _sapc_price; } set { _sapc_price = value; } }
        public decimal Sapc_qty { get { return _sapc_qty; } set { _sapc_qty = value; } }
        public string Sapc_sub_ser { get { return _sapc_sub_ser; } set { _sapc_sub_ser = value; } }
        public Boolean Sapc_tot_com { get { return _sapc_tot_com; } set { _sapc_tot_com = value; } }
        //Added by Prabhath on 1/4/2013 for control similar item
        public string Similer_item { get { return _similer_item; } set { _similer_item = value; } }
        //Added by Prabhath on 12/07/2013 for control item status
        public string Status { get { return _status; } set { _status = value; } }


        public static PriceCombinedItemRef ConvertTotal(DataRow row)
        {
            return new PriceCombinedItemRef
            {
                Sapc_increse = row["SAPC_INCRESE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPC_INCRESE"]),
                Sapc_itm_cd = row["SAPC_ITM_CD"] == DBNull.Value ? string.Empty : row["SAPC_ITM_CD"].ToString(),
                Sapc_itm_line = row["SAPC_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPC_ITM_LINE"]),
                Sapc_main_itm_cd = row["SAPC_MAIN_ITM_CD"] == DBNull.Value ? string.Empty : row["SAPC_MAIN_ITM_CD"].ToString(),
                Sapc_main_line = row["SAPC_MAIN_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPC_MAIN_LINE"]),
                Sapc_main_ser = row["SAPC_MAIN_SER"] == DBNull.Value ? string.Empty : row["SAPC_MAIN_SER"].ToString(),
                Sapc_pb_seq = row["SAPC_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPC_PB_SEQ"]),
                Sapc_price = row["SAPC_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPC_PRICE"]),
                Sapc_qty = row["SAPC_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPC_QTY"]),
                Sapc_sub_ser = row["SAPC_SUB_SER"] == DBNull.Value ? string.Empty : row["SAPC_SUB_SER"].ToString(),
                Sapc_tot_com = row["SAPC_TOT_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPC_TOT_COM"])

            };
        }

        public static PriceCombinedItemRef ConvertWithDescription(DataRow row)
        {
            return new PriceCombinedItemRef
            {
                Sapc_increse = row["SAPC_INCRESE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPC_INCRESE"]),
                Sapc_itm_cd = row["SAPC_ITM_CD"] == DBNull.Value ? string.Empty : row["SAPC_ITM_CD"].ToString(),
                Sapc_itm_line = row["SAPC_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPC_ITM_LINE"]),
                Sapc_main_itm_cd = row["SAPC_MAIN_ITM_CD"] == DBNull.Value ? string.Empty : row["SAPC_MAIN_ITM_CD"].ToString(),
                Sapc_main_line = row["SAPC_MAIN_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPC_MAIN_LINE"]),
                Sapc_main_ser = row["SAPC_MAIN_SER"] == DBNull.Value ? string.Empty : row["SAPC_MAIN_SER"].ToString(),
                Sapc_pb_seq = row["SAPC_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPC_PB_SEQ"]),
                Sapc_price = row["SAPC_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPC_PRICE"]),
                Sapc_qty = row["SAPC_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPC_QTY"]),
                Sapc_sub_ser = row["SAPC_SUB_SER"] == DBNull.Value ? string.Empty : row["SAPC_SUB_SER"].ToString(),
                Sapc_tot_com = row["SAPC_TOT_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPC_TOT_COM"]),
                Mi_longdesc = row["Mi_longdesc"] == DBNull.Value ? string.Empty : Convert.ToString(row["Mi_longdesc"]),
                Mi_model = row["Mi_model"] == DBNull.Value ? string.Empty : Convert.ToString(row["Mi_model"]),
                Mi_brand = row["Mi_brand"] == DBNull.Value ? string.Empty : Convert.ToString(row["Mi_brand"])

            };
        }
    }
}
