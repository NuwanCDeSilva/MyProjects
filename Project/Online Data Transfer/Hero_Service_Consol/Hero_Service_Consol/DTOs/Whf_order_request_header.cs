using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- ITPD11  | User :- suneth On 20-Jan-2015 11:48:37
    //===========================================================================================================

    public class Whf_order_request_header
    {
        public String Wor_ref { get; set; }
        public String Wor_company { get; set; }
        public DateTime Wor_date { get; set; }
        public String Wor_profit_center { get; set; }
        public String Wor_req_type { get; set; }
        public String Wor_executive { get; set; }
        public String Wor_custcode { get; set; }
        public DateTime Wor_exp_date { get; set; }
        public String Wor_remarks { get; set; }
        public Decimal Wor_tot_amt { get; set; }
        public String Wor_active { get; set; }
        public String Wor_createby { get; set; }
        public DateTime Wor_createwhen { get; set; }
        public String Wor_lastmodifyby { get; set; }
        public DateTime Wor_lastmodifywhen { get; set; }
        public String Wor_ref_no { get; set; }
        public String Wor_cus_app { get; set; }
        public String Wor_location { get; set; }

        public static Whf_order_request_header Converter(DataRow row)
        {
            return new Whf_order_request_header
            {
                Wor_ref = row["WOR_REF"] == DBNull.Value ? string.Empty : row["WOR_REF"].ToString(),
                Wor_company = row["WOR_COMPANY"] == DBNull.Value ? string.Empty : row["WOR_COMPANY"].ToString(),
                Wor_date = row["WOR_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["WOR_DATE"].ToString()),
                Wor_profit_center = row["WOR_PROFIT_CENTER"] == DBNull.Value ? string.Empty : row["WOR_PROFIT_CENTER"].ToString(),
                Wor_req_type = row["WOR_REQ_TYPE"] == DBNull.Value ? string.Empty : row["WOR_REQ_TYPE"].ToString(),
                Wor_executive = row["WOR_EXECUTIVE"] == DBNull.Value ? string.Empty : row["WOR_EXECUTIVE"].ToString(),
                Wor_custcode = row["WOR_CUSTCODE"] == DBNull.Value ? string.Empty : row["WOR_CUSTCODE"].ToString(),
                Wor_exp_date = row["WOR_EXP_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["WOR_EXP_DATE"].ToString()),
                Wor_remarks = row["WOR_REMARKS"] == DBNull.Value ? string.Empty : row["WOR_REMARKS"].ToString(),
                Wor_tot_amt = row["WOR_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WOR_TOT_AMT"].ToString()),
                Wor_active = row["WOR_ACTIVE"] == DBNull.Value ? string.Empty : row["WOR_ACTIVE"].ToString(),
                Wor_createby = row["WOR_CREATEBY"] == DBNull.Value ? string.Empty : row["WOR_CREATEBY"].ToString(),
                Wor_createwhen = row["WOR_CREATEWHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["WOR_CREATEWHEN"].ToString()),
                Wor_lastmodifyby = row["WOR_LASTMODIFYBY"] == DBNull.Value ? string.Empty : row["WOR_LASTMODIFYBY"].ToString(),
                Wor_lastmodifywhen = row["WOR_LASTMODIFYWHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["WOR_LASTMODIFYWHEN"].ToString()),
                Wor_ref_no = row["WOR_REF_NO"] == DBNull.Value ? string.Empty : row["WOR_REF_NO"].ToString(),
                Wor_cus_app = row["WOR_CUS_APP"] == DBNull.Value ? string.Empty : row["WOR_CUS_APP"].ToString(),
                Wor_location = row["WOR_LOCATION"] == DBNull.Value ? string.Empty : row["WOR_LOCATION"].ToString()
            };
        }
    }
}
