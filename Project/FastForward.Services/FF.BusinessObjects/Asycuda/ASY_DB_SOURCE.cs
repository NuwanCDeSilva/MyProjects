using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_DB_SOURCE
    {
        public Int32 Add_db_id { get; set; }
        public String Add_db_tp { get; set; }
        public String Add_db_str { get; set; }
        public String Add_db_name { get; set; }
        public Int32 Add_db_act { get; set; }
        public static ASY_DB_SOURCE Converter(DataRow row)
        {
            return new ASY_DB_SOURCE
            {
                Add_db_id = row["ADD_DB_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADD_DB_ID"].ToString()),
                Add_db_tp = row["ADD_DB_TP"] == DBNull.Value ? string.Empty : row["ADD_DB_TP"].ToString(),
                Add_db_str = row["ADD_DB_STR"] == DBNull.Value ? string.Empty : row["ADD_DB_STR"].ToString(),
                Add_db_name = row["ADD_DB_NAME"] == DBNull.Value ? string.Empty : row["ADD_DB_NAME"].ToString(),
                Add_db_act = row["ADD_DB_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADD_DB_ACT"].ToString())
            };
        } 
    }
}
