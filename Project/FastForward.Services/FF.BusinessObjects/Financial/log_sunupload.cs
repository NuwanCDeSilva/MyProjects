using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD14  | User :- sahanj On 14-Jul-2015 11:27:58
//===========================================================================================================

    public class log_sunupload
    {
        public Int32 seq { get; set; }
        public String com { get; set; }
        public String loc { get; set; }
        public String upload_type { get; set; }
        public String create_by { get; set; }
        public String session_id { get; set; }

        public DateTime dt_from { get; set; }
        public DateTime dt_to { get; set; }
        public DateTime create_dt { get; set; }

        public static log_sunupload Converter(DataRow row)
        {
            return new log_sunupload
        {
            seq = row["seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["seq"].ToString()),
            com = row["com"] == DBNull.Value ? string.Empty : row["com"].ToString(),
            loc = row["loc"] == DBNull.Value ? string.Empty : row["loc"].ToString(),
            upload_type = row["upload_type"] == DBNull.Value ? string.Empty : row["upload_type"].ToString(),
            create_by = row["create_by"] == DBNull.Value ? string.Empty : row["create_by"].ToString(),
            session_id = row["session_id"] == DBNull.Value ? string.Empty : row["session_id"].ToString(),

            dt_from = row["dt_from"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["dt_from"].ToString()),
            dt_to = row["dt_to"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["dt_to"].ToString()),
            create_dt = row["create_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["create_dt"].ToString())
        };
        }
    }
} 

