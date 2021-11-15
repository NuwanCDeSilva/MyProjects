using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class RBH_FRMDT_TODT
    {
        public String rbf_from_dt { get; set; }
        public String rbf_to_dt { get; set; }

        public static RBH_FRMDT_TODT Converter(DataRow row)
        {
            return new RBH_FRMDT_TODT {
                rbf_from_dt = row["rbf_from_dt"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["rbf_from_dt"].ToString()).Date.ToString("dd/MMM/yyyy"),
                rbf_to_dt = row["rbf_to_dt"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["rbf_to_dt"].ToString()).Date.ToString("dd/MMM/yyyy"),
            };
        }
    }
}
