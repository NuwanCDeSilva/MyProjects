using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
[Serializable]
   public class TempCommonPrice
    {
        #region Private Members
        private string _tcp_ip;
        private string _tcp_itm;
        private string _tcp_pb_cd;
        private string _tcp_pb_desc;
        private string _tcp_pb_lvl;
        private decimal _tcp_price;
        private string _tcp_usr;
        private decimal _tcp_total;
        private decimal _tcp_qty;
        private string _tcp_itm_desc;
        private Int32 _tcp_pb_seq;
        private Int32 _tcp_itm_seq;
        #endregion

        public Int32 Tcp_pb_seq
        {
            get { return _tcp_pb_seq; }
            set { _tcp_pb_seq = value; }
        }

        public Int32 Tcp_itm_seq
        {
            get { return _tcp_itm_seq; }
            set { _tcp_itm_seq = value; }
        }

        public string Tcp_ip
        {
            get { return _tcp_ip; }
            set { _tcp_ip = value; }
        }
        public string Tcp_itm
        {
            get { return _tcp_itm; }
            set { _tcp_itm = value; }
        }
        public string Tcp_pb_cd
        {
            get { return _tcp_pb_cd; }
            set { _tcp_pb_cd = value; }
        }
        public string Tcp_pb_desc
        {
            get { return _tcp_pb_desc; }
            set { _tcp_pb_desc = value; }
        }
        public string Tcp_pb_lvl
        {
            get { return _tcp_pb_lvl; }
            set { _tcp_pb_lvl = value; }
        }
        public decimal Tcp_price
        {
            get { return _tcp_price; }
            set { _tcp_price = value; }
        }
        public string Tcp_usr
        {
            get { return _tcp_usr; }
            set { _tcp_usr = value; }
        }

        public decimal tcp_qty
        {
            get { return _tcp_qty; }
            set { _tcp_qty = value; }
        }

        public decimal tcp_total
        {
            get { return _tcp_total; }
            set { _tcp_total = value; }
        }

        public string tcp_itm_desc
        {
            get { return _tcp_itm_desc; }
            set { _tcp_itm_desc = value; }
        }

        public static TempCommonPrice Converter(DataRow row)
        {
            return new TempCommonPrice
            {
                Tcp_ip = row["TCP_IP"] == DBNull.Value ? string.Empty : row["TCP_IP"].ToString(),
                Tcp_itm = row["TCP_ITM"] == DBNull.Value ? string.Empty : row["TCP_ITM"].ToString(),
                Tcp_pb_cd = row["TCP_PB_CD"] == DBNull.Value ? string.Empty : row["TCP_PB_CD"].ToString(),
                Tcp_pb_desc = row["TCP_PB_DESC"] == DBNull.Value ? string.Empty : row["TCP_PB_DESC"].ToString(),
                Tcp_pb_lvl = row["TCP_PB_LVL"] == DBNull.Value ? string.Empty : row["TCP_PB_LVL"].ToString(),
                Tcp_price = row["TCP_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TCP_PRICE"]),
                Tcp_usr = row["TCP_USR"] == DBNull.Value ? string.Empty : row["TCP_USR"].ToString(),
                tcp_total = row["TCP_TOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TCP_TOTAL"]),
                tcp_qty = row["TCP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TCP_QTY"]),
                tcp_itm_desc = row["TCP_ITM_DESC"] == DBNull.Value ? string.Empty : row["TCP_ITM_DESC"].ToString(),
                Tcp_pb_seq = row["TCP_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["TCP_PB_SEQ"]),
                Tcp_itm_seq = row["TCP_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["TCP_ITM_SEQ"])
            };
        }

        public static TempCommonPrice ConverterTemp(DataRow row)
        {
            return new TempCommonPrice
            {
                Tcp_pb_cd = row["TCP_PB_CD"] == DBNull.Value ? string.Empty : row["TCP_PB_CD"].ToString(),
                Tcp_pb_desc = row["TCP_PB_DESC"] == DBNull.Value ? string.Empty : row["TCP_PB_DESC"].ToString(),
                Tcp_pb_lvl = row["TCP_PB_LVL"] == DBNull.Value ? string.Empty : row["TCP_PB_LVL"].ToString()
            };
        }

        public static TempCommonPrice ConverterWithItem(DataRow row)
        {
            return new TempCommonPrice
            {
                Tcp_itm = row["TCP_ITM"] == DBNull.Value ? string.Empty : row["TCP_ITM"].ToString(),
                tcp_itm_desc = row["TCP_ITM_DESC"] == DBNull.Value ? string.Empty : row["TCP_ITM_DESC"].ToString(),
                tcp_qty = row["TCP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TCP_QTY"]),
                Tcp_price = row["TCP_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TCP_PRICE"]),
                tcp_total = row["TCP_TOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TCP_TOTAL"]),
                Tcp_pb_cd = row["TCP_PB_CD"] == DBNull.Value ? string.Empty : row["TCP_PB_CD"].ToString(),
                Tcp_pb_lvl = row["TCP_PB_LVL"] == DBNull.Value ? string.Empty : row["TCP_PB_LVL"].ToString(),
                Tcp_pb_seq = row["TCP_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["TCP_PB_SEQ"]),
                Tcp_itm_seq = row["TCP_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["TCP_ITM_SEQ"])
            };
        }
    }
}

