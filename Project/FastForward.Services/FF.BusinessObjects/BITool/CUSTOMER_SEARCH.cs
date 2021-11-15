using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class CUSTOMER_SEARCH
    {
        public string cust_code { get; set; }
        public string cust_name { get; set; }
        public string cust_add { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static CUSTOMER_SEARCH Converter(DataRow row)
        {
            return new CUSTOMER_SEARCH
            {
                cust_code = row["Code"] == DBNull.Value ? string.Empty : row["Code"].ToString(),
                cust_name = row["Name"] == DBNull.Value ? string.Empty : row["Name"].ToString(),
                cust_add = row["Address"] == DBNull.Value ? string.Empty : row["Address"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
