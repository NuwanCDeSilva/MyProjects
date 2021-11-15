using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class EventRegistry
    {
        public Int32 SERE_ID { get; set; }
        public string SERE_EVE_CD { get; set; }
        public Int32 SERE_EVE_TP_ID { get; set; }
        public string SERE_NAME_ONE { get; set; }
        public string SERE_NAME_TWO { get; set; }
        public string SERE_DESC { get; set; }
        public DateTime SERE_EVE_DT { get; set; }
        public string SERE_CUST_CD { get; set; }
        public string SERE_CUST_PHONE { get; set; }
        public string SERE_CUST_EMAIL { get; set; }
        public DateTime SERE_DEL_DT { get; set; }
        public string SERE_DEL_ADDRESS { get; set; }
        public string SERE_TOWN { get; set; }
        public string SERE_DEL_NOTE { get; set; }
        public Int32 SERE_IS_SHOWROOM { get; set; }
        public DateTime? SERE_EVE_CRE_DT { get; set; }
        public Int32? SERE_UPDATE { get; set; }
        public string SERE_UPDATE_BY { get; set; }
        public DateTime? SERE_UPDATE_DT { get; set; }
        public string SERE_UPDATE_SESSION { get; set; }
        public string SERE_EVE_TP { get; set; }

        public static EventRegistry Converter(DataRow row)
        {
            return new EventRegistry
            {
                SERE_ID = row["SERE_ID"] == DBNull.Value ? 0 : Convert.ToInt32( row["SERE_ID"].ToString()),
                SERE_EVE_CD = row["SERE_EVE_CD"] == DBNull.Value ? string.Empty : row["SERE_EVE_CD"].ToString(),
                SERE_EVE_TP_ID = row["SERE_EVE_TP_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_EVE_TP_ID"].ToString()),
                SERE_NAME_ONE = row["SERE_NAME_ONE"] == DBNull.Value ? string.Empty : row["SERE_NAME_ONE"].ToString(),
                SERE_NAME_TWO = row["SERE_NAME_TWO"] == DBNull.Value ? string.Empty : row["SERE_NAME_TWO"].ToString(),
                SERE_DESC = row["SERE_DESC"] == DBNull.Value ? string.Empty : row["SERE_DESC"].ToString(),
                SERE_EVE_DT = row["SERE_EVE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime( row["SERE_EVE_DT"].ToString()),
                SERE_CUST_CD = row["SERE_CUST_CD"] == DBNull.Value ? string.Empty : row["SERE_CUST_CD"].ToString(),
                SERE_CUST_PHONE = row["SERE_CUST_PHONE"] == DBNull.Value ? string.Empty : row["SERE_CUST_PHONE"].ToString(),
                SERE_CUST_EMAIL = row["SERE_CUST_EMAIL"] == DBNull.Value ? string.Empty : row["SERE_CUST_EMAIL"].ToString(),
                SERE_DEL_DT = row["SERE_DEL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SERE_DEL_DT"].ToString()),
                SERE_DEL_ADDRESS = row["SERE_DEL_ADDRESS"] == DBNull.Value ? string.Empty : row["SERE_DEL_ADDRESS"].ToString(),
                SERE_TOWN = row["SERE_TOWN"] == DBNull.Value ? string.Empty : row["SERE_TOWN"].ToString(),
                SERE_DEL_NOTE = row["SERE_DEL_NOTE"] == DBNull.Value ? string.Empty : row["SERE_DEL_NOTE"].ToString(),
                SERE_IS_SHOWROOM = row["SERE_IS_SHOWROOM"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_IS_SHOWROOM"].ToString()),
                SERE_EVE_CRE_DT = row["SERE_EVE_CRE_DT"] == DBNull.Value ? (DateTime?) null : Convert.ToDateTime(row["SERE_EVE_CRE_DT"].ToString()),
                SERE_UPDATE = row["SERE_UPDATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERE_UPDATE"].ToString()),
                SERE_UPDATE_BY = row["SERE_UPDATE_BY"] == DBNull.Value ? string.Empty : row["SERE_UPDATE_BY"].ToString(),
                SERE_UPDATE_DT = row["SERE_UPDATE_DT"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["SERE_UPDATE_DT"].ToString()),
                SERE_UPDATE_SESSION = row["SERE_UPDATE_SESSION"] == DBNull.Value ? string.Empty : row["SERE_UPDATE_SESSION"].ToString(),
                SERE_EVE_TP = row["SERE_EVE_TP"] == DBNull.Value ? string.Empty : row["SERE_EVE_TP"].ToString()
            };

        }
    }
}
