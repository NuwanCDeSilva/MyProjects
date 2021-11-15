using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class SHEDULE_INSTALLEMENTS
    {
        public string monthly_installement { get; set; }
        public string from_period { get; set; }
        public string to_period { get; set; }

        public static SHEDULE_INSTALLEMENTS Converter(DataRow row)
        {
            return new SHEDULE_INSTALLEMENTS
            {
                monthly_installement = row["monthly_installement"] == DBNull.Value ? string.Empty : row["monthly_installement"].ToString(),
                from_period = row["from_period"] == DBNull.Value ? string.Empty : row["from_period"].ToString(),
                to_period = row["to_period"] == DBNull.Value ? string.Empty : row["to_period"].ToString(),
            };
        }

    }
}
