using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class EventItems
    {
        public Int32 SERE_ID { get; set; }
        public string SERE_EVE_CD { get; set; }
        public Int32 SERE_LINE { get; set; }
        public string SERE_COM { get; set; }
        public string SERE_PB { get; set; }
        public string SERE_PB_LVL { get; set; }
        public Int32? SERE_PB_SEQ { get; set; }
        public string SERE_ITM_CD { get; set; }
        public string SERE_ITM_STUS { get; set; }
        public decimal SERE_ITM_PRICE { get; set; }
        public decimal SERE_ITM_QTY { get; set; }
        public decimal SERE_ITM_SOLD { get; set; }
        public string SERE_CUST_TP { get; set; }
        public string SERE_CUST_CD { get; set; }
        public Int32 SERE_ACT { get; set; }
        public Int32? SERE_UPDATE { get; set; }
        public string SERE_UPDATE_BY { get; set; }
        public DateTime? SERE_UPDATE_DT { get; set; }
        public string SERE_UPDATE_SESSION { get; set; }
        public bool IsSelected { get; set; }
        public decimal SelectedQty { get; set; }

        public static EventItems Converter(DataRow row)
        {
            return new EventItems
            {
                SERE_ID = row["SERE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_ID"].ToString()),
                SERE_EVE_CD = row["SERE_EVE_CD"] == DBNull.Value ? string.Empty : row["SERE_EVE_CD"].ToString(),
                SERE_LINE = row["SERE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_LINE"].ToString()),
                SERE_COM = row["SERE_COM"] == DBNull.Value ? string.Empty : row["SERE_COM"].ToString(),
                SERE_PB = row["SERE_PB"] == DBNull.Value ? string.Empty : row["SERE_PB"].ToString(),
                SERE_PB_LVL = row["SERE_PB_LVL"] == DBNull.Value ? string.Empty : row["SERE_PB_LVL"].ToString(),
                SERE_PB_SEQ = row["SERE_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_PB_SEQ"].ToString()),
                SERE_ITM_CD = row["SERE_ITM_CD"] == DBNull.Value ? string.Empty : row["SERE_ITM_CD"].ToString(),
                SERE_ITM_STUS = row["SERE_ITM_STUS"] == DBNull.Value ? string.Empty : row["SERE_ITM_STUS"].ToString(),
                SERE_ITM_PRICE = row["SERE_ITM_PRICE"] == DBNull.Value ? 0: Convert.ToDecimal(row["SERE_ITM_PRICE"].ToString()),
                SERE_ITM_QTY = row["SERE_ITM_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SERE_ITM_QTY"].ToString()),
                SERE_ITM_SOLD = row["SERE_ITM_SOLD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SERE_ITM_SOLD"].ToString()),
                SERE_CUST_TP = row["SERE_CUST_TP"] == DBNull.Value ? string.Empty : row["SERE_CUST_TP"].ToString(),
                SERE_CUST_CD = row["SERE_CUST_CD"] == DBNull.Value ? string.Empty : row["SERE_CUST_CD"].ToString(),
                SERE_ACT = row["SERE_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_ACT"].ToString()),
                SERE_UPDATE = row["SERE_UPDATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_UPDATE"].ToString()),
                SERE_UPDATE_BY = row["SERE_UPDATE_BY"] == DBNull.Value ? string.Empty : row["SERE_UPDATE_BY"].ToString(),
                SERE_UPDATE_DT = row["SERE_UPDATE_DT"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["SERE_UPDATE_DT"].ToString()),
                SERE_UPDATE_SESSION = row["SERE_UPDATE_SESSION"] == DBNull.Value ? string.Empty : row["SERE_UPDATE_SESSION"].ToString()
            };
        }
    }
}
