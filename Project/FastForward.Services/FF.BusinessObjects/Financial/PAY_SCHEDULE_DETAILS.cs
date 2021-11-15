using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class PAY_SCHEDULE_DETAILS
    {
        public string psd_no { get; set; }
        public string psd_line { get; set; }
        public string psd_due { get; set; }
        public string psd_amt { get; set; }
        public string psd_ded_amt { get; set; }
        public string psd_net_amt { get; set; }
        public string psd_pay_amt { get; set; }

        public static PAY_SCHEDULE_DETAILS Converter(DataRow row)
        {
            return new PAY_SCHEDULE_DETAILS
            {
                psd_no = row["psd_no"] == DBNull.Value ? string.Empty : row["psd_no"].ToString(),
                psd_line = row["psd_line"] == DBNull.Value ? string.Empty : row["psd_line"].ToString(),
                psd_due = row["psd_due"] == DBNull.Value ? string.Empty : row["psd_due"].ToString(),
                psd_amt = row["psd_amt"] == DBNull.Value ? string.Empty : row["psd_amt"].ToString(),
                psd_ded_amt = row["psd_ded_amt"] == DBNull.Value ? string.Empty : row["psd_ded_amt"].ToString(),
                psd_net_amt = row["psd_net_amt"] == DBNull.Value ? string.Empty : row["psd_net_amt"].ToString(),
                psd_pay_amt = row["psd_pay_amt"] == DBNull.Value ? string.Empty : row["psd_pay_amt"].ToString(),
            };
        }
    }
}
