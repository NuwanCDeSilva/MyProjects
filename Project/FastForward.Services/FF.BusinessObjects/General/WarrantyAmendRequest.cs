using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
   public class WarrantyAmendRequest
    {
        public Int32 Rwr_seq { get; set; }
        public String Rwr_req_by { get; set; }
        public DateTime Rwr_req_dt { get; set; }
        public String Rwr_req_session { get; set; }
        public Int32 Rwr_ser_id { get; set; }
        public String Rwr_stus { get; set; }
        public String Rwr_app_by { get; set; }
        public DateTime Rwr_app_dt { get; set; }
        public String Rwr_app_session { get; set; }
        public Int32 Rwr_warr_period { get; set; }
        public String Rwr_warr_rmk { get; set; }
        public DateTime Rwr_st_dt { get; set; }
        public String Rwr_cust_cd { get; set; }
        public static WarrantyAmendRequest Converter(DataRow row)
        {
            return new WarrantyAmendRequest
            {
                Rwr_seq = row["RWR_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RWR_SEQ"].ToString()),
                Rwr_req_by = row["RWR_REQ_BY"] == DBNull.Value ? string.Empty : row["RWR_REQ_BY"].ToString(),
                Rwr_req_dt = row["RWR_REQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RWR_REQ_DT"].ToString()),
                Rwr_req_session = row["RWR_REQ_SESSION"] == DBNull.Value ? string.Empty : row["RWR_REQ_SESSION"].ToString(),
                Rwr_ser_id = row["RWR_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RWR_SER_ID"].ToString()),
                Rwr_stus = row["RWR_STUS"] == DBNull.Value ? string.Empty : row["RWR_STUS"].ToString(),
                Rwr_app_by = row["RWR_APP_BY"] == DBNull.Value ? string.Empty : row["RWR_APP_BY"].ToString(),
                Rwr_app_dt = row["RWR_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RWR_APP_DT"].ToString()),
                Rwr_app_session = row["RWR_APP_SESSION"] == DBNull.Value ? string.Empty : row["RWR_APP_SESSION"].ToString(),
                Rwr_warr_period = row["RWR_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RWR_WARR_PERIOD"].ToString()),
                Rwr_warr_rmk = row["RWR_WARR_RMK"] == DBNull.Value ? string.Empty : row["RWR_WARR_RMK"].ToString(),
                Rwr_st_dt = row["RWR_ST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RWR_ST_DT"].ToString()),
                Rwr_cust_cd = row["RWR_CUST_CD"] == DBNull.Value ? string.Empty : row["RWR_CUST_CD"].ToString()
            };
        }
    }
}
