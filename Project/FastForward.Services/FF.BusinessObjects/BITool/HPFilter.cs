using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 

namespace FF.BusinessObjects.BITool
{
    [Serializable]
    public class HPFilter
    {
        public Int32 seq { get; set; }
        public String date_filter { get; set; }
        public DateTime fromdt { get; set; }
        public DateTime todt { get; set; }
        public Int32 option { get; set; }


        //public string RESULT_COUNT { get; set; }
        //public string R__ { get; set; }
        public static HPFilter webConverter(DataRow row)
        {
            return new HPFilter
            {
                seq = row["seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["seq"].ToString()),
                date_filter = row["date_filter"] == DBNull.Value ? string.Empty : row["date_filter"].ToString(),
                fromdt = row["fromdt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["fromdt"]),
                todt = row["todt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["todt"]),
                option = row["option"] == DBNull.Value ? 0 : Convert.ToInt32(row["option"].ToString())
            };
        }
    }
}
